namespace MyAssistant.Domain.Interfaces
{
    public interface IRecurrable
    {
        bool IsRecurring { get; set; }

        RecurrenceType? RecurrencePattern { get; set; }

        public enum RecurrenceType
        {
            None,
            Hourly,
            Daily,
            Weekly,
            Monthly,
            Yearly
        }
    }
}
