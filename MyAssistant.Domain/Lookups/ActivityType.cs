
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Lookups
{
    [Table("ActivityType")]
    public class ActivityType : LookupBase<ActivityType>
    {
        public static readonly int Active = 1;
        public static readonly int Inactive = 2;
        public static readonly int Urgent = 3;
        public static readonly int NotUrgent = 4;

        public ActivityType() { }
    }

    public class ActivityTypeList : LookupBaseList<ActivityType>
    {
        public new static ActivityTypeList CachedList
        {
            get { return _activityTypeList; }
            set { _activityTypeList = value; }
        }

        public static new void ClearCachedList()
        {
            _activityTypeList.Clear();
        }

        private static ActivityTypeList _activityTypeList { get; set; } = new();
    }
}
