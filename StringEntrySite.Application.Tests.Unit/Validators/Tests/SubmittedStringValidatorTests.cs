using StringEntrySite.Application.Tests.Unit.Validators.TestData;
using StringEntrySite.Application.Validators;

namespace StringEntrySite.Application.Tests.Unit.Validators.Tests;

public class SubmittedStringValidatorTests
{
    private static readonly SubmittedStringValidator Sut = new();
    
    [Theory]
    [InlineData("HJkl6789")]
    [InlineData("Qwerty123")]
    public void IsValid_WhenInputIsValid_ReturnsTrue(string input)
    {
        // Act
        (bool isValid, IReadOnlyCollection<string>? errors) = Sut.IsValid(input);
        
        // Assert
        Assert.True(isValid);
        Assert.Null(errors);
    }
    
    [Theory]
    [ClassData(typeof(SubmittedStringValidatorInvalidTestData))]
    public void IsValid_WhenInputIsInvalid_ReturnsFalse(string input, string[] expectedErrors)
    {
        // Act
        (bool isValid, IReadOnlyCollection<string>? errors) = Sut.IsValid(input);
        
        // Assert
        Assert.False(isValid);
        Assert.NotNull(errors);
        Assert.Equal(expectedErrors.Length, errors.Count);
        foreach (string expectedError in expectedErrors)
        {
            Assert.Contains(expectedError, errors);
        }
    }
}