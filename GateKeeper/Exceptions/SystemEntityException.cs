namespace GateKeeper.Exceptions
{
    /// <summary>
    /// An exception class to handle all system entity exceptions
    /// </summary>
    public class SystemEntityException : Exception
    {
        public SystemEntityException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

        public SystemEntityException(string message) : base(message)
        {
            
        }
    }
}
