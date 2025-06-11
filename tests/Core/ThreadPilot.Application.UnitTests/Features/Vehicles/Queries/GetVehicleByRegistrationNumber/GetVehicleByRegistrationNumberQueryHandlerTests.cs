using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using ThreadPilot.Application.Common.Interfaces;
using ThreadPilot.Application.Common.Mappings;
using ThreadPilot.Application.Features.Vehicles.Queries.GetVehicleByRegistrationNumber;
using ThreadPilot.Domain.Entities;
using Xunit;

namespace ThreadPilot.Application.UnitTests.Features.Vehicles.Queries.GetVehicleByRegistrationNumber;

public class GetVehicleByRegistrationNumberQueryHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly IMapper _mapper;
    private readonly GetVehicleByRegistrationNumberQueryHandler _handler;

    public GetVehicleByRegistrationNumberQueryHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = configuration.CreateMapper();

        _handler = new GetVehicleByRegistrationNumberQueryHandler(_contextMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_ValidRegistrationNumber_ReturnsVehicle()
    {
        // Arrange
        var registrationNumber = "ABC123";
        var vehicle = new Vehicle
        {
            Id = 1,
            RegistrationNumber = registrationNumber,
            Color = "Red",
            Model = "Tesla Model 3"
        };

        var vehicles = new List<Vehicle> { vehicle }.AsQueryable().BuildMockDbSet();

        _contextMock.Setup(c => c.Vehicles).Returns(vehicles.Object);

        var query = new GetVehicleByRegistrationNumberQuery(registrationNumber);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.RegistrationNumber.Should().Be(registrationNumber);
        result.Value.Color.Should().Be("Red");
        result.Value.Model.Should().Be("Tesla Model 3");
    }

    [Fact]
    public async Task Handle_NonExistentRegistrationNumber_ReturnsFailure()
    {
        // Arrange
        var vehicles = new List<Vehicle>().AsQueryable().BuildMockDbSet();

        _contextMock.Setup(c => c.Vehicles).Returns(vehicles.Object);

        var query = new GetVehicleByRegistrationNumberQuery("NONEXIST");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.First().Message.Should().Contain("not found");
    }
}