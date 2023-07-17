namespace Librarian.Interfaces
{
    public interface IDataOperator
    {
        IEnumerable<TOutput> GetData<TOutput>(string query, object parameters = null) where TOutput : class, new();

        bool UpdateData(string query, object parameters = null);

        bool DeleteData(string query, object parameters = null);

        bool CreateData(string query, object parameters = null);
    }
}
