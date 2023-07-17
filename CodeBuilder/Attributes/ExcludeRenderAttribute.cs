using CodeBuilder.Interfaces;
using System.Reflection;

namespace CodeBuilder.Attributes
{
    /// <summary>
    /// Excludes a property from being rendered by the tag helpers
    /// </summary>
    public class ExcludeRenderAttribute : Attribute, IAttribute
    {
        #region Fields
        private string[] _excludedPages;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an instance of the attribute and sets pages to render the property for while everywhere else, the property is not rendered
        /// </summary>
        /// <param name="pages">A params string array that contains the pages to render the property for</param>
        public ExcludeRenderAttribute(params string[] pages)
        {
            _excludedPages = pages;
        }

        /// <summary>
        /// Constructs an instance of the attribute that excludes the property from rendering on all pages
        /// </summary>
        public ExcludeRenderAttribute()
        {
            _excludedPages = new string[0];
        }
        #endregion

        #region Public Methods
        public IEnumerable<FieldInfo> GetFields() =>
            GetType().GetFields();

        /// <summary>
        /// Returns the pages to render for the attribute
        /// </summary>
        /// <returns>IEnumerable of string</returns>
        public IEnumerable<string> GetExcludedPages() =>
            _excludedPages;
        
        #endregion
    }
}
