using System.Collections;

namespace StringEntrySite.Application.Tests.Unit.Validators.TestData;

public class SubmittedStringValidatorInvalidTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        const string repetitionError = "No character should be repeated.";
        const string submissionSizeError = "Size of submitted value is invalid.";
        const string lowerCaseError = "Input must contain at least one lowercase letter.";
        const string oneNumberError = "Input must contain at least one number.";
        yield return
        [
            "TopScore",
            new[]
            {
                repetitionError,
                oneNumberError
            }
        ];
        yield return
        [
            "M",
            new[]
            {
                submissionSizeError,
                lowerCaseError,
                oneNumberError
            }
        ];
        yield return
        [
            "One",
            new[]
            {
                submissionSizeError,
                oneNumberError
            }
        ];
        yield return
        [
            "Test",
            new[]
            {
                submissionSizeError,
                oneNumberError
            }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}