namespace Models.Interfaces
{
    /// <summary>
    /// Base interface to be implemented by all model classes
    /// </summary>
    /// <typeparam name="TKey">The type of the Id field for the model</typeparam>
    public interface IModel<TKey>
    {
        TKey Id { get; set; }
    }
}
