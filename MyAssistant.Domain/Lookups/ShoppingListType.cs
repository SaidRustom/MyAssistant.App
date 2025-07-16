using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Lookups
{
    [Table("ShoppingListType")]
    public class ShoppingListType : LookupBase<ShoppingListType>
    {
        public static readonly int Groceries = 1;
        public static readonly int Pharmaceuticals = 2;
        public static readonly int Electronics = 3;
        public static readonly int Clothing = 4;
        public static readonly int HomeGoods = 5;
        public static readonly int Beauty = 6;
        public static readonly int Toys = 7;
        public static readonly int Books = 8;
        public static readonly int OfficeSupplies = 9;
        public static readonly int SportsEquipment = 10;
        public static readonly int Automotive = 11;
        public static readonly int PetSupplies = 12;
        public static readonly int Garden = 13;
        public static readonly int BabyProducts = 14;
        public static readonly int Furniture = 15;

        public ShoppingListType() { }
    }

    public class ShoppingListTypeList : LookupBaseList<ShoppingListType>
    {
        public new static ShoppingListTypeList CachedList
        {
            get { return _shoppingListTypeList; }
            set { _shoppingListTypeList = value; }
        }

        public static new void ClearCachedList()
        {
            _shoppingListTypeList.Clear();
        }

        private static ShoppingListTypeList _shoppingListTypeList { get; set; } = new();
    }
}
