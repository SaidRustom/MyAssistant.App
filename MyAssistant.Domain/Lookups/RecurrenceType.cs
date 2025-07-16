using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Lookups
{
    [Table("RecurrenceType")]
    public class RecurrenceType : LookupBase<RecurrenceType>
    {
        public static readonly int None = 1;
        public static readonly int Hourly = 2;
        public static readonly int Daily = 3;
        public static readonly int Weekly = 4;
        public static readonly int Monthly = 5;
        public static readonly int Annually = 6;

        public RecurrenceType() { }
    }

    public class RecurrenceTypeList : LookupBaseList<RecurrenceType>
    {
        public RecurrenceTypeList CachedList
        {
            get { return _recurrenceTypeList; }
            set { _recurrenceTypeList = value; }
        }

        public static new void ClearCachedList()
        {
            _recurrenceTypeList.Clear();
        }

        private static RecurrenceTypeList _recurrenceTypeList { get; set; } = new();
    }
}
