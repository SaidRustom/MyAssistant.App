
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Core.Responses
{
    public class ApiResponse<T> 
    {
        public bool Success { get; set; } = true;
        public T? Data { get; set; }
        public string? Message { get; set; } = string.Empty;
        public List<string>? ErrorMessages { get; set; }

        public ApiResponse(T data, string? message = "Success")
        {
            Data = data;
            Message = message;
            Success = true;
        }

        public ApiResponse(List<string> errors, string message = "An error occurred.")
        {
            ErrorMessages = errors;
            Message = message;
            Success = false;
        }
    }

    public class ApiResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string>? ErrorMessages { get; set; } 

        public ApiResponse(string message = "Success")
        {
            Message = message;
            Success = true;
        }

        public ApiResponse(List<string> errors, string message = "An error occurred.")
        {
            ErrorMessages = errors;
            Message = message;
            Success = false;
        }
    }
}
