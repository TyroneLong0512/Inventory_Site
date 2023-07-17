using CodeBuilder.Interfaces;
using System.Linq.Expressions;
using Extensions;
using CodeBuilder.Structs;
using System.Reflection;
using Microsoft.AspNetCore.Razor.TagHelpers;
using CodeBuilder.Attributes;

namespace CodeBuilder.Base_Classes
{
    /// <summary>
    /// Base class to be derived in code builder classes
    /// </summary>
    public abstract class BaseCodeBuilder : ICodeBuilder
    {
        #region Fields
        private IEnumerable<INode> _nodes;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an instance of CodeBuilder
        /// </summary>
        public BaseCodeBuilder()
        {
            _nodes = new List<INode>();
        }
        #endregion

        #region Private Methods
        private IEnumerable<PropertyStruct> generatePropertyStruct(object o)
        {
            IEnumerable<PropertyStruct> propertyStructs = new List<PropertyStruct>();
            foreach (PropertyInfo property in o.GetType().GetProperties())
                propertyStructs.Add(new PropertyStruct { Name = property.Name, Type = property.PropertyType, ShouldRender = true });

            return propertyStructs;
        }

        private IEnumerable<PropertyStruct> generatePropertyStruct(object o, string callingPage)
        {
            IEnumerable<PropertyStruct> propertyStructs = new List<PropertyStruct>();
            foreach (PropertyInfo property in o.GetType().GetProperties())
            {
                ExcludeRenderAttribute? renderExclude = (ExcludeRenderAttribute?)property.GetCustomAttribute(typeof(ExcludeRenderAttribute));

                if (renderExclude == null || renderExclude.GetExcludedPages().Contains(callingPage))
                    propertyStructs.Add(new PropertyStruct { Name = property.Name, Type = property.PropertyType, ShouldRender = true });
                else
                    propertyStructs.Add(new PropertyStruct { Name = property.Name, Type = property.PropertyType, ShouldRender = false });
            }

            return propertyStructs;
        }
        #endregion

        #region Public Methods
        public void Append(INode node) =>
            _nodes.Add(node);

        public void Clear() =>
            _nodes = new List<INode>();

        public INode? GetNodeByIndex(int index) =>
            _nodes.ElementAt(index);

        public INode? GetNodeByName(string name) =>
            _nodes.Where(node => node.Name == name).FirstOrDefault();

        public IEnumerable<INode> GetNodes() =>
            _nodes;

        public bool Inject(Expression<Func<IEnumerable<INode>, INode, bool>> injectionExpression, INode node) =>
            injectionExpression.Compile().Invoke(_nodes, node);

        public void Inject(INode parentNode, INode childNode) =>
            _nodes.Where(node => node == parentNode).FirstOrDefault()?.Children.Add(childNode);

        public void Inject(INode parentNode, INode childNode, int index) =>
            _nodes.Where(node => node == parentNode).FirstOrDefault()?.Children.AddAt(childNode, index);

        public bool Remove(Expression<Func<IEnumerable<INode>, INode, bool>> removalExpression, INode node) =>
            removalExpression.Compile().Invoke(_nodes, node);

        public void Remove(INode node) =>
            _nodes.Except(new INode[] { node });

        public void Remove(int index) =>
            _nodes.RemoveAt(index);

        public void RemoveAllAfter(INode node) =>
            _nodes.RemoveAfter(node);

        public IEnumerable<PropertyStruct> GetPropertyStruct(object o) =>
            generatePropertyStruct(o);

        public IEnumerable<PropertyStruct> GetPropertyStruct(object o, string callingPage) =>
            generatePropertyStruct(o, callingPage);
        #endregion

        #region Abstract Methods
        public abstract string GenerateCode();

        public abstract IEnumerable<INode> RenderControls(object o);

        public abstract IEnumerable<INode> RenderControls(object o, string callingPage);
        #endregion
    }
}
