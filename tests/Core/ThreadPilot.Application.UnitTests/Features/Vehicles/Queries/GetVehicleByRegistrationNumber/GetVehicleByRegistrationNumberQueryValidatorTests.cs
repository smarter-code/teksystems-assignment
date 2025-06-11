using FluentAssertions;
using ThreadPilot.Application.Features.Vehicles.Queries.GetVehicleByRegistrationNumber;
using Xunit;

namespace ThreadPilot.Application.UnitTests.Features.Vehicles.Queries.GetVehicleByRegistrationNumber;

public class GetVehicleByRegistrationNumberQueryValidatorTests
{
    private readonly GetVehicleByRegistrationNumberQueryValidator _validator;

    public GetVehicleByRegistrationNumberQueryValidatorTests()
    {
        _validator = new GetVehicleByRegistrationNumberQueryValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Validate_EmptyOrNullRegistrationNumber_ReturnsError(string registrationNumber)
    {
        // Arrange
        var query = new GetVehicleByRegistrationNumberQuery(registrationNumber);

        // Act
        var result = await _validator.ValidateAsync(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("required"));
    }

    [Theory]
    [InlineData("ABC123")]
    [InlineData("DEF456")]
    [InlineData("XYZ789")]
    [InlineData("ABC12D")]
    [InlineData("DEF45G")]
    public async Task Validate_ValidRegistrationNumber_NoErrors(string registrationNumber)
    {
        // Arrange
        var query = new GetVehicleByRegistrationNumberQuery(registrationNumber);

        // Act
        var result = await _validator.ValidateAsync(query);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("AB123")] // Too short (5 chars)
    [InlineData("ABCD123")] // Too long (7 chars)
    [InlineData("ABC12")] // Too short (5 chars)
    public async Task Validate_InvalidLength_ReturnsError(string registrationNumber)
    {
        // Arrange
        var query = new GetVehicleByRegistrationNumberQuery(registrationNumber);

        // Act
        var result = await _validator.ValidateAsync(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("6 characters"));
    }

    [Theory]
    [InlineData("123ABC")] // Wrong format
    [InlineData("AB12CD")] // Two letters in the middle
    [InlineData("ABCDEF")] // All letters
    [InlineData("123456")] // All numbers
    public async Task Validate_InvalidRegistrationNumberFormat_ReturnsError(string registrationNumber)
    {
        // Arrange
        var query = new GetVehicleByRegistrationNumberQuery(registrationNumber);

        // Act
        var result = await _validator.ValidateAsync(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("format"));
    }
}