using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ThreadPilot.Application.Common.Interfaces;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Application.Features.Insurances.Queries.GetInsurancesByPersonalId;

public class GetInsurancesByPersonalIdQueryHandler : IRequestHandler<GetInsurancesByPersonalIdQuery, Result<PersonInsurancesDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPersonalNumberNormalizer _personalNumberNormalizer;
    private readonly IVehicleServiceClient _vehicleServiceClient;

    public GetInsurancesByPersonalIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IPersonalNumberNormalizer personalNumberNormalizer,
        IVehicleServiceClient vehicleServiceClient)
    {
        _context = context;
        _mapper = mapper;
        _personalNumberNormalizer = personalNumberNormalizer;
        _vehicleServiceClient = vehicleServiceClient;
    }

    public async Task<Result<PersonInsurancesDto>> Handle(GetInsurancesByPersonalIdQuery request, CancellationToken cancellationToken)
    {
        var normalizedPersonalNumber = _personalNumberNormalizer.Normalize(request.PersonalIdentificationNumber);

        var personData = await _context.Persons
            .AsNoTracking()
            .Where(p => p.PersonalIdentificationNumber == normalizedPersonalNumber)
            .Select(p => new
            {
                p.PersonalIdentificationNumber,
                InsurancePolicies = p.InsurancePolicies.Select(ip => new
                {
                    ip.PolicyNumber,
                    InsuranceType = new { ip.InsuranceType.TypeName },
                    VehicleInsuranceDetail = ip.VehicleInsuranceDetail == null ? null : new
                    {
                        ip.VehicleInsuranceDetail.MonthlyCost,
                        Currency = ip.VehicleInsuranceDetail.Currency == null ? null : new { ip.VehicleInsuranceDetail.Currency.Code },
                        VehicleInsuranceType = ip.VehicleInsuranceDetail.VehicleInsuranceType == null ? null : new { ip.VehicleInsuranceDetail.VehicleInsuranceType.TypeName },
                        Vehicle = ip.VehicleInsuranceDetail.Vehicle == null ? null : new { ip.VehicleInsuranceDetail.Vehicle.RegistrationNumber }
                    },
                    PetInsuranceDetail = ip.PetInsuranceDetail == null ? null : new
                    {
                        ip.PetInsuranceDetail.MonthlyCost,
                        Currency = ip.PetInsuranceDetail.Currency == null ? null : new { ip.PetInsuranceDetail.Currency.Code },
                        PetInsuranceType = ip.PetInsuranceDetail.PetInsuranceType == null ? null : new { ip.PetInsuranceDetail.PetInsuranceType.TypeName }
                    },
                    HealthInsuranceDetail = ip.HealthInsuranceDetail == null ? null : new
                    {
                        ip.HealthInsuranceDetail.MonthlyCost,
                        Currency = ip.HealthInsuranceDetail.Currency == null ? null : new { ip.HealthInsuranceDetail.Currency.Code },
                        HealthInsuranceType = ip.HealthInsuranceDetail.HealthInsuranceType == null ? null : new { ip.HealthInsuranceDetail.HealthInsuranceType.TypeName }
                    }
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (personData == null)
        {
            return Result.Fail<PersonInsurancesDto>("Person not found");
        }

        // Map projected data to domain entities for further processing
        var insurancePolicies = personData.InsurancePolicies.Select(ip => new InsurancePolicy
        {
            PolicyNumber = ip.PolicyNumber,
            InsuranceType = new InsuranceType { TypeName = ip.InsuranceType.TypeName },
            VehicleInsuranceDetail = ip.VehicleInsuranceDetail == null ? null : new VehicleInsuranceDetail
            {
                MonthlyCost = ip.VehicleInsuranceDetail.MonthlyCost,
                Currency = ip.VehicleInsuranceDetail.Currency == null ? null : new Currency { Code = ip.VehicleInsuranceDetail.Currency.Code },
                VehicleInsuranceType = ip.VehicleInsuranceDetail.VehicleInsuranceType == null ? null : new VehicleInsuranceType { TypeName = ip.VehicleInsuranceDetail.VehicleInsuranceType.TypeName },
                Vehicle = ip.VehicleInsuranceDetail.Vehicle == null ? null : new Vehicle { RegistrationNumber = ip.VehicleInsuranceDetail.Vehicle.RegistrationNumber }
            },
            PetInsuranceDetail = ip.PetInsuranceDetail == null ? null : new PetInsuranceDetail
            {
                MonthlyCost = ip.PetInsuranceDetail.MonthlyCost,
                Currency = ip.PetInsuranceDetail.Currency == null ? null : new Currency { Code = ip.PetInsuranceDetail.Currency.Code },
                PetInsuranceType = ip.PetInsuranceDetail.PetInsuranceType == null ? null : new PetInsuranceType { TypeName = ip.PetInsuranceDetail.PetInsuranceType.TypeName }
            },
            HealthInsuranceDetail = ip.HealthInsuranceDetail == null ? null : new HealthInsuranceDetail
            {
                MonthlyCost = ip.HealthInsuranceDetail.MonthlyCost,
                Currency = ip.HealthInsuranceDetail.Currency == null ? null : new Currency { Code = ip.HealthInsuranceDetail.Currency.Code },
                HealthInsuranceType = ip.HealthInsuranceDetail.HealthInsuranceType == null ? null : new HealthInsuranceType { TypeName = ip.HealthInsuranceDetail.HealthInsuranceType.TypeName }
            }
        }).ToList();

        // Process vehicle insurances with parallel vehicle API calls
        var vehicleInsurances = await ProcessVehicleInsurances(
            insurancePolicies.Where(p => p.InsuranceType.TypeName.ToLower() == "vehicle"),
            cancellationToken);

        // Process health insurances
        var healthInsurances = ProcessHealthInsurances(
            insurancePolicies.Where(p => p.InsuranceType.TypeName.ToLower() == "health"));

        // Process pet insurances
        var petInsurances = ProcessPetInsurances(
            insurancePolicies.Where(p => p.InsuranceType.TypeName.ToLower() == "pet"));

        var result = new PersonInsurancesDto
        {
            PersonalIdentificationNumber = request.PersonalIdentificationNumber,
            VehicleInsurances = vehicleInsurances,
            HealthInsurances = healthInsurances,
            PetInsurances = petInsurances
        };

        return Result.Ok(result);
    }

    private async Task<(string PolicyNumber, VehicleInfoDto VehicleInfo)> GetVehicleInfoAsync(
        string policyNumber,
        string registrationNumber,
        CancellationToken cancellationToken)
    {
        try
        {
            var vehicleInfo = await _vehicleServiceClient.GetVehicleAsync(
                registrationNumber,
                cancellationToken);
            return (policyNumber, vehicleInfo);
        }
        catch
        {
            // If the API call fails, return null vehicle info
            return (policyNumber, null);
        }
    }

    private async Task<List<VehicleInsuranceDto>> ProcessVehicleInsurances(
        IEnumerable<InsurancePolicy> vehiclePolicies,
        CancellationToken cancellationToken)
    {
        var vehicleInsurances = new List<VehicleInsuranceDto>();
        var vehicleTasks = new List<Task<(string PolicyNumber, VehicleInfoDto VehicleInfo)>>();

        foreach (var policy in vehiclePolicies)
        {
            if (policy.VehicleInsuranceDetail == null) continue;

            var vehicleInsurance = new VehicleInsuranceDto
            {
                PolicyNumber = policy.PolicyNumber,
                InsuranceTypeName = policy.VehicleInsuranceDetail.VehicleInsuranceType?.TypeName,
                MonthlyCost = policy.VehicleInsuranceDetail.MonthlyCost,
                Currency = policy.VehicleInsuranceDetail.Currency?.Code
            };

            vehicleInsurances.Add(vehicleInsurance);

            // Create a task for fetching vehicle info
            if (policy.VehicleInsuranceDetail.Vehicle != null)
            {
                var registrationNumber = policy.VehicleInsuranceDetail.Vehicle.RegistrationNumber;
                var policyNumber = policy.PolicyNumber;

                var vehicleTask = GetVehicleInfoAsync(policyNumber, registrationNumber, cancellationToken);
                vehicleTasks.Add(vehicleTask);
            }
        }

        // Wait for all vehicle API calls to complete
        if (vehicleTasks.Any())
        {
            var vehicleResults = await Task.WhenAll(vehicleTasks);

            // Update vehicle insurances with vehicle info
            foreach (var (policyNumber, vehicleInfo) in vehicleResults)
            {
                var vehicleInsurance = vehicleInsurances.FirstOrDefault(v => v.PolicyNumber == policyNumber);
                if (vehicleInsurance != null && vehicleInfo != null)
                {
                    vehicleInsurance.VehicleInfo = vehicleInfo;
                }
            }
        }

        return vehicleInsurances;
    }

    private List<HealthInsuranceDto> ProcessHealthInsurances(IEnumerable<InsurancePolicy> healthPolicies)
    {
        var healthInsurances = new List<HealthInsuranceDto>();

        foreach (var policy in healthPolicies)
        {
            if (policy.HealthInsuranceDetail == null) continue;

            healthInsurances.Add(new HealthInsuranceDto
            {
                PolicyNumber = policy.PolicyNumber,
                InsuranceTypeName = policy.HealthInsuranceDetail.HealthInsuranceType?.TypeName,
                MonthlyCost = policy.HealthInsuranceDetail.MonthlyCost,
                Currency = policy.HealthInsuranceDetail.Currency?.Code
            });
        }

        return healthInsurances;
    }

    private List<PetInsuranceDto> ProcessPetInsurances(IEnumerable<InsurancePolicy> petPolicies)
    {
        var petInsurances = new List<PetInsuranceDto>();

        foreach (var policy in petPolicies)
        {
            petInsurances.Add(new PetInsuranceDto
            {
                PolicyNumber = policy.PolicyNumber,
                InsuranceTypeName = policy.PetInsuranceDetail.PetInsuranceType?.TypeName,
                MonthlyCost = policy.PetInsuranceDetail.MonthlyCost,
                Currency = policy.PetInsuranceDetail.Currency?.Code
            });
        }

        return petInsurances;
    }
}