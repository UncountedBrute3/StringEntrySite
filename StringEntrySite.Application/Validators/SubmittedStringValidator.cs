using StringEntrySite.Application.Validators.Interfaces;

namespace StringEntrySite.Application.Validators;

public class SubmittedStringValidator : IValidator<string>
{
    public (bool IsValid, IReadOnlyCollection<string>? Errors) IsValid(string input)
    {
        List<string> errors = [];
        if (!IsValidLength(input))
        {
            errors.Add("Size of submitted value is invalid.");
        }

        bool hasUpper = false;
        bool hasLower = false;
        bool hasDigit = false;
        
        HashSet<char> usedCharacters = [];
        foreach (char character in input)
        {
            if (!char.IsLetterOrDigit(character) && character != ' ')
            {
                errors.Add("Input must only contain alphanumeric characters and spaces.");
                break;
            }

            if (!usedCharacters.Add(character))
            {
                errors.Add("No character should be repeated.");
                break;
            }

            if (char.IsUpper(character))
            {
                hasUpper = true;
            }

            if (char.IsLower(character))
            {
                hasLower = true;
            }

            if (char.IsDigit(character))
            {
                hasDigit = true;
            }
        }

        if (!hasUpper)
        {
            errors.Add("Input must contain at least one uppercase letter.");
        }

        if (!hasLower)
        {
            errors.Add("Input must contain at least one lowercase letter.");
        }

        if (!hasDigit)
        {
            errors.Add("Input must contain at least one number.");
        }

        return errors.Count > 0 ? (false, errors.AsReadOnly()) : (true, null);
    }

    /// <summary>
    /// Determines if string is between 8 and 500 characters
    /// as stipulated by requirements.
    /// </summary>
    /// <param name="input">The input of the user.</param>
    /// <returns>true if the length of the string is valid, otherwise false.</returns>
    private static bool IsValidLength(string input) => input.Length is >= 8 and <= 500;
}