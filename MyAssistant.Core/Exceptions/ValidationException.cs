using FluentValidation.Results;

namespace MyAssistant.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            Errors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                Errors.Add(validationError.ErrorMessage);
            }
        }
    }
}
