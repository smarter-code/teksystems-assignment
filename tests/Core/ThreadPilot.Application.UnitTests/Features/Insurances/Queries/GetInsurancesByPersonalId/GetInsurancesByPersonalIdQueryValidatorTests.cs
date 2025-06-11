using FluentAssertions;
using Moq;
using ThreadPilot.Application.Common.Interfaces;
using ThreadPilot.Application.Features.Insurances.Queries.GetInsurancesByPersonalId;
using Xunit;

namespace ThreadPilot.Application.UnitTests.Features.Insurances.Queries.GetInsurancesByPersonalId;

public class GetInsurancesByPersonalIdQueryValidatorTests
{
    private readonly GetInsurancesByPersonalIdQueryValidator _validator;
    private readonly Mock<IPersonalNumberNormalizer> _normalizerMock;

    public GetInsurancesByPersonalIdQueryValidatorTests()
    {
        _normalizerMock = new Mock<IPersonalNumberNormalizer>();
        _validator = new GetInsurancesByPersonalIdQueryValidator(_normalizerMock.Object);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Validate_EmptyOrNullPersonalIdentificationNumber_ReturnsError(string personalNumber)
    {
        // Arrange
        var query = new GetInsurancesByPersonalIdQuery(personalNumber);

        // Act
        var result = await _validator.ValidateAsync(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("required"));
    }

    [Theory]
    [InlineData("8905152384")] // Valid 10 digits
    [InlineData("9211302391")] // Valid 10 digits
    [InlineData("0107089012")] // Born in 2001
    public async Task Validate_ValidSwedishPersonalNumber_NoErrors(string personalNumber)
    {
        // Arrange
        var query = new GetInsurancesByPersonalIdQuery(personalNumber);

        // Act
        var result = await _validator.ValidateAsync(query);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("890515-2384")] // With hyphen (11 chars)
    [InlineData("19890515-2384")] // 12 digits with hyphen
    [InlineData("198905152384")] // 12 digits
    [InlineData("199211302391")] // 12 digits
    [InlineData("200107089012")] // 12 digits
    [InlineData("123456789")] // Too short
    [InlineData("12345678901")] // Too long
    public async Task Validate_InvalidLength_ReturnsError(string personalNumber)
    {
        // Arrange
        var query = new GetInsurancesByPersonalIdQuery(personalNumber);

        // Act
        var result = await _validator.ValidateAsync(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("10 digits"));
    }

    [Theory]
    [InlineData("ABCDEFGHIJ")] // Letters
    public async Task Validate_InvalidPersonalNumber_ReturnsError(string personalNumber)
    {
        // Arrange
        var query = new GetInsurancesByPersonalIdQuery(personalNumber);

        // Act
        var result = await _validator.ValidateAsync(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e =>
            e.PropertyName == nameof(query.PersonalIdentificationNumber));
    }
}