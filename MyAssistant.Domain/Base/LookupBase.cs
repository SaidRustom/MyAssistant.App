using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAssistant.Domain.Base
{
    /// <summary>
    /// Base class for Lookups which are small objects consisting of Code & Desc
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class LookupBase<T> where T : new()
    {
        [Key]
        public int Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;

        public static T EmptyValue()
        {
            return new T(); 
        }

        public LookupBase() { }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (this.GetType().Equals(obj.GetType()))
                return this.Code == ((LookupBase<T>)obj).Code;

            else
                return false;
        }

        public static bool operator ==(LookupBase<T> a, LookupBase<T> b)
        {
            if (object.ReferenceEquals(null, a))
                return object.ReferenceEquals(null, b);

            if (object.ReferenceEquals(null, b))
                return object.ReferenceEquals(null, a);

            return a.Equals(b);
        }

        public static bool operator !=(LookupBase<T> a, LookupBase<T> b)
        {
            if (object.ReferenceEquals(null, a))
                return !object.ReferenceEquals(null, b);

            if (object.ReferenceEquals(null, b))
                return !object.ReferenceEquals(null, a);

            return !(a == b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
