
namespace MyAssistant.Domain.Base
{
    [Serializable]
    public abstract class LookupBaseList<T>: List<T> where T : LookupBase<T>, new()
    {
        public static LookupBaseList<T>? CachedList { get; set; }

        public static T Get(int code)
        {
            var value = CachedList.Where(x => x.Code == code).FirstOrDefault();

            if (value is null)
                throw new NullReferenceException($"Lookup:{typeof(T).Name}, code:{code} - not found.");

            return value;
        }

        public void ClearCachedList() { throw new NotImplementedException(); }
    }
}
