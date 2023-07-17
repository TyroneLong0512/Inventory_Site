using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using CustomTagHelpers.Structs;
using System.Text;

namespace CustomTagHelpers.Interfaces
{
    /// <summary>
    /// Interface to be derived in Tag Helper base classes
    /// </summary>
    /// <typeparam name="TModel">A POCO model</typeparam>
    public interface IControlTagHelper
    {
        /// <summary>
        /// Retrieves all the properties listed in TModel
        /// </summary>
        /// <returns>IEnumerable of PropertyInfo</returns>
        IEnumerable<PropertyInfo> GetProperties();

        /// <summary>
        /// Returns a string representation of HTML controls based off of the properties in TModel
        /// </summary>
        /// <returns>string</returns>
        string GenerateControls();
    }
}
