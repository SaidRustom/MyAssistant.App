
namespace MyAssistant.Shared.DTOs
{
    public class LookupDto
    {
        public int Code { get; set; }

        public string Description { get; set; }

        public LookupDto(int code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
