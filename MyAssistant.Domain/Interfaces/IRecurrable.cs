﻿
namespace MyAssistant.Domain.Interfaces
{
    public interface IRecurrable
    {
        bool IsRecurring { get; set; }

        int? RecurrenceTypeCode { get; set; }

        public DateTime? RecurrenceEndDate { get; set; }
    }
}
