using System.Reflection;

namespace CodeBuilder.Interfaces
{
    /// <summary>
    /// Interface to be derived in classes that are used as Attribute classes
    /// </summary>
    public interface IAttribute
    {
        /// <summary>
        /// Retrieves any fields that the attribute contains
        /// </summary>
        /// <returns>IEnumerable of FieldInfo</returns>
        IEnumerable<FieldInfo> GetFields();
    }
}
