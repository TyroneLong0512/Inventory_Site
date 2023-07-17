using Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Reflection;
using System.Text;
using CustomTagHelpers.Interfaces;
using CustomTagHelpers.Structs;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace CustomTagHelpers.Base_Classes
{
    /// <summary>
    /// Base class to be derived in TagHelper classes
    /// </summary>
    /// <typeparam name="TModel">The Model to use for control generation</typeparam>
    public abstract class ControlTagHelper : TagHelper, IControlTagHelper
    {
        #region Fields
        protected IHttpContextAccessor _contextAccesor;
        protected string _callingPage;
        #endregion

        #region Properties
        [HtmlAttributeName("html-for")]
        public object Model { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default contructor for the ControlTagHelper.
        /// </summary>
        public ControlTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccesor = contextAccessor;
            _callingPage = _contextAccesor.HttpContext.Request.Path.Value.Split('/').Last();
            Model = new {};
        }
        #endregion

        #region Public Methods
        public abstract string GenerateControls();

        public IEnumerable<PropertyInfo> GetProperties() =>
            Model.GetType().GetProperties();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent(GenerateControls());
            output.TagMode = TagMode.StartTagAndEndTag;
        }
        #endregion
    }
}
