using System.Text.RegularExpressions;
using ThreadPilot.Application.Common.Interfaces;

namespace ThreadPilot.Infrastructure.Services;

public class PersonalNumberNormalizer : IPersonalNumberNormalizer
{
    /// <summary>
    /// Normalizes different personal number formats to the 10 digits format (YYMMDDXXXX)
    /// </summary>
    public string Normalize(string personalNumber)
    {
        if (string.IsNullOrWhiteSpace(personalNumber))
            throw new ArgumentException("Personal number cannot be null or whitespace.", nameof(personalNumber));

        // Remove all non-digit characters except + and -
        var cleanedNumber = Regex.Replace(personalNumber, @"[^\d\+\-]", "");

        // Handle 12-digit format (YYYYMMDDXXXX)
        if (Regex.IsMatch(cleanedNumber, @"^\d{12}$"))
        {
            // Convert to 10-digit format by removing century
            return cleanedNumber.Substring(2);
        }

        // Handle 10-digit format with delimiter (YYMMDD-XXXX or YYMMDD+XXXX)
        if (Regex.IsMatch(cleanedNumber, @"^\d{6}[\+\-]\d{4}$"))
        {
            // Remove delimiter
            return cleanedNumber.Replace("+", "").Replace("-", "");
        }

        // Handle 10-digit format without delimiter (YYMMDDXXXX)
        if (Regex.IsMatch(cleanedNumber, @"^\d{10}$"))
        {
            return cleanedNumber;
        }

        // Handle 13-digit format with delimiter (YYYYMMDD-XXXX or YYYYMMDD+XXXX)
        if (Regex.IsMatch(cleanedNumber, @"^\d{8}[\+\-]\d{4}$"))
        {
            // Remove century and delimiter
            var withoutDelimiter = cleanedNumber.Replace("+", "").Replace("-", "");
            return withoutDelimiter.Substring(2);
        }

        throw new ArgumentException($"Invalid personal number format: '{personalNumber}'", nameof(personalNumber));
    }
}