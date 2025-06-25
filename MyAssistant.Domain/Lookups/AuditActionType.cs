
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Lookups
{
    public class AuditActionType : LookupBase<AuditActionType>
    {
        public static readonly int Create = 1;
        public static readonly int Update = 2;
        public static readonly int Delete = 3;

        public AuditActionType() { }
    }

    public class AuditActionTypeList : LookupBaseList<AuditActionType>
    {
        public new static AuditActionTypeList CachedList
        {
            get { return _auditActionTypeList; }
            set { _auditActionTypeList = value; }
        }

        public static new void ClearCachedList()
        {
            _auditActionTypeList.Clear();
        }

        private static AuditActionTypeList _auditActionTypeList { get; set; } = new();
    }
}
