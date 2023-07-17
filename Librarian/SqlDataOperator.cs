using Librarian.Base_Classes;
using Librarian.Interfaces;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace Librarian
{
    public class SqlDataOperator : DataOperator<SqlConnection, SqlCommand>
    {
        #region Constructors
        public SqlDataOperator(IDataContext<SqlConnection, SqlCommand> context) : base(context)
        {

        }
        #endregion

        #region Private Methods
        private void ClearCommand()
        {
            _context.Command.Dispose();
            _context.Command = new SqlCommand();
            _context.Command.Connection = _context.Connection;
        }

        private void AssignParameters(object parameters)
        {
            if (parameters != null)
                foreach (PropertyInfo property in parameters.GetType().GetProperties())
                    _context.Command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(parameters));
        }

        private bool ExecuteNonQuery(string query, object parameters)
        {
            try
            {
                AssignParameters(parameters);
                _context.Command.CommandType = CommandType.Text;
                _context.Command.CommandText = query;
                _context.Connection.Open();
                bool returnValue = _context.Command.ExecuteNonQuery() > 0;
                _context.Connection.Close();
                ClearCommand();
                return returnValue;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Public Methods
        public override bool CreateData(string query, object parameters = null) =>
            ExecuteNonQuery(query, parameters);

        public override bool DeleteData(string query, object parameters = null) =>
            ExecuteNonQuery(query, parameters);

        public override IEnumerable<TOutput> GetData<TOutput>(string query, object parameters = null)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            List<TOutput> outputs = new List<TOutput>();

            try
            {
                AssignParameters(parameters);
                _context.Command.CommandType = CommandType.Text;
                _context.Command.CommandText = query;
                sda.SelectCommand = _context.Command;
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    TOutput output = new TOutput();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        PropertyInfo property = typeof(TOutput).GetProperty(dc.ColumnName);
                        if (property != null)
                            property.SetValue(output, dr[dc].GetType() != typeof(DBNull) ? dr[dc] : null);
                    }

                    outputs.Add(output);
                }

                ClearCommand();
                return outputs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool UpdateData(string query, object parameters = null) =>
            ExecuteNonQuery(query, parameters);
        #endregion
    }
}
