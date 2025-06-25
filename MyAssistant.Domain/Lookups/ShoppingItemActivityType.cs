
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Lookups
{
    public class ShoppingItemActivityType : LookupBase<ShoppingItemActivityType>
    {
        public static readonly int Active = 1;
        public static readonly int Inactive = 2;
        public static readonly int Urgent = 3;
        public static readonly int NotUrgent = 4;

        public ShoppingItemActivityType() { }
    }

    public class ShoppingItemActivityTypeList : LookupBaseList<ShoppingItemActivityType>
    {
        public new static ShoppingItemActivityTypeList CachedList
        {
            get { return _ShoppingItemActivityTypeList; }
            set { _ShoppingItemActivityTypeList = value; }
        }

        public static new void ClearCachedList()
        {
            _ShoppingItemActivityTypeList.Clear();
        }

        private static ShoppingItemActivityTypeList _ShoppingItemActivityTypeList { get; set; } = new();
    }
}
