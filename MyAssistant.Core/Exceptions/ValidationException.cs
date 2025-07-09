using FluentValidation.Results;

namespace MyAssistant.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; set; }

        /// <summary>
        /// Initializes a new instance using the specified <see cref="ValidationResult"/>. 
        /// Extracts and stores all error messages from the validation result into the <see cref="Errors"/> list.
        /// </summary>
        /// <remarks>Use with Fluent Validation</remarks>
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
