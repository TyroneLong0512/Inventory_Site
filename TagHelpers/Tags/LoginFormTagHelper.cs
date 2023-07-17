using CodeBuilder.Base_Classes;
using CodeBuilder.Interfaces;
using CustomTagHelpers.Base_Classes;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CustomTagHelpers
{
    /// <summary>
    /// Tag Helper for generating Form templates
    /// </summary>
    /// <typeparam name="TModel">The model to generate the form from</typeparam>
    [HtmlTargetElement("custom-form-input")]
    public class LoginFormTagHelper : ControlTagHelper
    {
        #region Fields
        private ICodeBuilder _builder;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an instance of the FormTagHelper
        /// </summary>
        /// <param name="builder">The code builder injected by the service manager</param>
        /// <param name="contextAccessor">The HttpContextAccessor injected by the code builder</param>
        public LoginFormTagHelper(ICodeBuilder builder, IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _builder = builder;
        }
        #endregion

        #region Private Methods
        private IEnumerable<INode> RenderControls() =>
            _builder.RenderControls(Model, _callingPage);

        private void GenerateFormContainer()
        {
            INode formNodeOpen = new Node("FormOpen", "<form method='post' asp-page-handler='Login'>");
            formNodeOpen.Children.Add(RenderControls());
            INode formNodeClose = new Node("FormClose", "</form>", formNodeOpen);
            _builder.Append(formNodeOpen);
            _builder.Append(formNodeClose);
        }
        #endregion

        #region Public Methods
        public override string GenerateControls()
        {
            GenerateFormContainer();
            return _builder.GenerateCode();
        }
        #endregion
    }
}
