using Librarian.Interfaces;

namespace Librarian.Base_Classes
{
    public abstract class DataOperator<TConnection, TCommand> : IDataOperator
    {
        #region Fields
        protected IDataContext<TConnection, TCommand> _context;
        #endregion

        #region Constructors
        public DataOperator(IDataContext<TConnection, TCommand> context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public abstract bool CreateData(string query, object parameters = null);

        public abstract bool DeleteData(string query, object parameters = null);

        public abstract IEnumerable<TOutput> GetData<TOutput>(string query, object parameters = null) where TOutput : class, new();

        public abstract bool UpdateData(string query, object parameters = null);
        #endregion
    }
}
