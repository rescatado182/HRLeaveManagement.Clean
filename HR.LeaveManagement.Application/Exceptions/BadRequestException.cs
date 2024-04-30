using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public IDictionary<string, string[]> ValidationErrors { get; set; }
        public BadRequestException(string message) : base(message) 
        {
            
        }

        public BadRequestException(string message, ValidationResult validationResult) : base(message)
        {
            ValidationErrors = validationResult.ToDictionary();
        }


    }
}
