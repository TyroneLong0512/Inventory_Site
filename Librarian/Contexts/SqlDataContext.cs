using Librarian.Interfaces;
using System.Data.SqlClient;

namespace Librarian.Contexts
{
    public class SqlDataContext : IDataContext<SqlConnection, SqlCommand>
    {
        #region Properties
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        #endregion

        #region Constructors
        public SqlDataContext(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Command = new SqlCommand();
            Command.Connection = Connection;
        }
        #endregion
    }
}
