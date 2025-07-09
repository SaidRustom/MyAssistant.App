namespace MyAssistant.Shared
{
    /// <summary>
    /// Marker interface used to indicate that a type can be mapped to the specified type <typeparamref name="T"/>.
    /// This interface does not define any members and serves solely for identification and mapping purposes (e.g., in auto-mapping configurations).
    /// </summary>
    public interface IMapWith<T>
    {
        // Marker interface, no members needed
    }
}
