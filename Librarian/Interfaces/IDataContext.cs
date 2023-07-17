using System.Data.SqlClient;

namespace Librarian.Interfaces
{
    public interface IDataContext<TConnection, TCommand>
    {
        TConnection Connection { get; set; }

        TCommand Command { get; set; }
    }
}
