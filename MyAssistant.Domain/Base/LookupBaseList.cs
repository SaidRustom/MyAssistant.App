
namespace MyAssistant.Domain.Base
{
    [Serializable]
    public abstract class LookupBaseList<T>: List<T> where T : LookupBase<T>, new()
    {
        public static LookupBaseList<T>? CachedList { get; set; }

        public static LookupBase<T> Get(int code)
        {
            var value = CachedList?.Where(x => x.Code == code).FirstOrDefault();

            if(value is null)
                return LookupBase<T>.EmptyValue();

            return value;
        }

        public void ClearCachedList() { throw new NotImplementedException(); }
    }
}
