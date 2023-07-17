using CodeBuilder.Structs;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq.Expressions;

namespace CodeBuilder.Interfaces
{
    /// <summary>
    /// Interface to be derived in code builder classes
    /// </summary>
    public interface ICodeBuilder
    {
        /// <summary>
        /// Generates the code for the current builder
        /// </summary>
        /// <returns>string</returns>
        string GenerateCode();

        /// <summary>
        /// Renders controls from a model
        /// </summary>
        /// <typeparam name="TModel">The model to render controls from</typeparam>
        /// <returns>IEnumerable of INode</returns>
        IEnumerable<INode> RenderControls(object o);

        /// <summary>
        /// Creates and returns a collection of PropertyStructs, which contain the name and type of properties listed in TModel
        /// </summary>
        /// <returns>IEnumerable of PropertyStruct</returns>
        IEnumerable<PropertyStruct> GetPropertyStruct(object o);

        /// <summary>
        /// Creates and returns a collection of PropertyStructs, which contain the name and type of properties listed in TModel as well as logic for the ShouldRender flag
        /// </summary>
        /// <param name="o">The object of TModel</param>
        /// <param name="context">The TagHelperContext for the TagHelper implementing this method</param>
        /// <returns>IEnumerable of INode</returns>
        public abstract IEnumerable<INode> RenderControls(object o, string callingPage);

        /// <summary>
        /// Retrieves all current nodes in the builder
        /// </summary>
        /// <returns>IEnumerable of INode</returns>
        IEnumerable<INode> GetNodes();

        /// <summary>
        /// Retrieves the given node based on the name property set in the node
        /// </summary>
        /// <param name="name">The name given to the node</param>
        /// <returns>INode or null if none are found</returns>
        INode? GetNodeByName(string name);

        /// <summary>
        /// Retrieves a node by it's index
        /// </summary>
        /// <param name="index">The index of the node to be retrieved</param>
        /// <returns>INode</returns>
        INode? GetNodeByIndex(int index);

        /// <summary>
        /// Injects a child node into a parent
        /// </summary>
        /// <param name="injectionExpression">The expression to execute</param>
        /// <param name="node">The node to add</param>
        bool Inject(Expression<Func<IEnumerable<INode>, INode, bool>> injectionExpression, INode node);

        /// <summary>
        /// Injects a child node into a parent
        /// </summary>
        /// <param name="parentNode">The parent node to inject into</param>
        /// <param name="childNode">The child node to inject</param>
        void Inject(INode parentNode, INode childNode);

        /// <summary>
        /// Injects a child node into a parent at a specified index
        /// </summary>
        /// <param name="parentNode">The parent node to inject into</param>
        /// <param name="childNode">The child node to inject</param>
        /// <param name="index">The index of all child nodes to inject underneath</param>
        void Inject(INode parentNode, INode childNode, int index);

        /// <summary>
        /// Appends a node to the most outer nodes
        /// </summary>
        /// <param name="node">The node to append</param>
        void Append(INode node);

        /// <summary>
        /// Removes a node at the location specified in the expression
        /// </summary>
        /// <param name="removalExpression">The expression to remove the node</param>
        /// <param name="node">The node to remove</param>
        /// <returns>bool</returns>
        bool Remove(Expression<Func<IEnumerable<INode>, INode, bool>> removalExpression, INode node);

        /// <summary>
        /// Removes a node on the most outer nodes
        /// </summary>
        /// <param name="node">The node to remove</param>
        void Remove(INode node);

        /// <summary>
        /// Removes a node on the most outer nodes
        /// </summary>
        /// <param name="index">The index of the node to remove</param>
        void Remove(int index);

        /// <summary>
        /// Removes all nodes after a specific node on the outer most nodes.
        /// </summary>
        /// <param name="node">The node to remove nodes after</param>
        void RemoveAllAfter(INode node);

        /// <summary>
        /// Clears the current builder
        /// </summary>
        void Clear();
    }
}
