using CodeBuilder.Base_Classes;
using CodeBuilder.Interfaces;
using CodeBuilder.Structs;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeBuilder
{
    public class HtmlCodeBuilder : BaseCodeBuilder
    {
        #region Control Type Dictionary
        private Dictionary<Type, string> _controlTypeDictionary;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an instance of HtmlCodeBuilder
        /// </summary>
        public HtmlCodeBuilder() : base()
        {
            _controlTypeDictionary = new Dictionary<Type, string>();
            InitializeTypes();
        }
        #endregion

        #region Private Methods
        private void InitializeTypes()
        {
            _controlTypeDictionary.Add(typeof(string), "<div class='form-outline mb-4'>\n<input id='~|PropertyName|~_Id' class='form-control' type='text' />\n<label class='form-label' for='~|PropertyName|~_Id'>~|PropertyName|~</label>\n</div>");
            _controlTypeDictionary.Add(typeof(int), "<div class='form-outline mb-4'>\n<input id='~|PropertyName|~_Id' class='form-control' type='number' />\n<label class='form-label' for='~|PropertyName|~_Id' >~|PropertyName|~</label>\n</div>");
            _controlTypeDictionary.Add(typeof(decimal), "<div class='form-outline mb-4'>\n<input id='~|PropertyName|~_Id' class='form-control' type='text' />\n<label class='form-label' for='~|PropertyName|~_Id' >~|PropertyName|~</label>\n</div>");
            _controlTypeDictionary.Add(typeof(DateTime), "<div class='form-outline mb-4'>\n<input id='~|PropertyName|~_Id' class='form-control' type='date' />\n<label class='form-label' for='~|PropertyName|~_Id' >~|PropertyName|~</label>\n</div>");
        }

        private string SerializeControl(string propertyName, string controlTemplate)
        {
            Regex regex = new Regex("(~\\|[a-zA-Z]*\\|~)");
            return regex.Replace(controlTemplate, propertyName);
        }

        private void SerializeNodes(StringBuilder builder, INode node)
        {
            builder.AppendLine(node.Code);
            foreach (INode childNode in node.Children)
                SerializeNodes(builder, childNode);
        }
        #endregion

        #region Public Methods
        public override string GenerateCode()
        {
            StringBuilder codeBuilder = new StringBuilder();
            foreach (INode node in GetNodes())
                SerializeNodes(codeBuilder, node);

            return codeBuilder.ToString();
        }

        public override IEnumerable<INode> RenderControls(object o)
        {
            IEnumerable<INode> controlNodes = new List<INode>();
            foreach (PropertyStruct property in GetPropertyStruct(o).Where(propStruct => propStruct.ShouldRender))
                controlNodes.Add(new Node(property.Name, SerializeControl(property.Name, _controlTypeDictionary[property.Type])));

            return controlNodes;
        }

        public override IEnumerable<INode> RenderControls(object o, string callingPage)
        {
            IEnumerable<INode> controlNodes = new List<INode>();
            foreach (PropertyStruct property in GetPropertyStruct(o, callingPage).Where(propStruct => propStruct.ShouldRender))
                controlNodes.Add(new Node(property.Name, SerializeControl(property.Name, _controlTypeDictionary[property.Type])));

            return controlNodes;
        }
        #endregion
    }
}
