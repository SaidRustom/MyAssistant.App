using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Lookups
{
    public class MyAssistantServiceType : LookupBase<MyAssistantServiceType>
    {
        //public static readonly int  = 1;
    }

    public class MyAssistantServiceTypeList : LookupBaseList<MyAssistantServiceType>
    {
        public MyAssistantServiceTypeList CachedList
        {
            get { return _myAssistantServiceTypeList; }
            set { _myAssistantServiceTypeList = value; }
        }

        public static new void ClearCachedList()
        {
            _myAssistantServiceTypeList.Clear();
        }

        private static MyAssistantServiceTypeList _myAssistantServiceTypeList { get; set; } = new();
    }
}
