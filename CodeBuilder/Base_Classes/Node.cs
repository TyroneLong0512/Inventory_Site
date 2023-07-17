using CodeBuilder.Interfaces;

namespace CodeBuilder.Base_Classes
{
    /// <summary>
    /// Base class to be derived in other Node types or to be used as default implementation for INode
    /// </summary>
    public class Node : INode
    {
        #region Properties
        public INode? OpeningNode { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public IEnumerable<INode> Children { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a new instance of Node with the name and children collection set
        /// </summary>
        /// <param name="name">The name of the node</param>
        /// <param name="children">The children to set in the node</param>
        /// <param name="code">The code contained in the node</param>
        /// <param name="openNode">The node listed as the parent node to the current node</param>
        public Node(string name, IEnumerable<INode> children, string code, INode? openNode = null)
        {
            OpeningNode = openNode;
            Name = name;
            Children = children;
            Code = code;
        }

        /// <summary>
        /// Constructs a new instance of Node
        /// </summary>
        /// <param name="code">The code contained in the node</param>
        /// <param name="openNode">The node listed as the parent node to the current node</param>
        public Node(string code, INode? openNode = null)
        {
            OpeningNode = openNode;
            Code = code;
            Name = string.Empty;
            Children = new List<INode>();
        }

        /// <summary>
        /// Constructs a new instance of Node with the children collection set
        /// </summary>
        /// <param name="children">The children to set in the node</param>
        /// <param name="code">The code contained in the node</param>
        /// <param name="openNode">The node listed as the parent node to the current node</param>
        public Node(IEnumerable<INode> children, string code, INode? openNode = null)
        {
            OpeningNode = openNode;
            Code = code;
            Children = children;
            Name = string.Empty;
        }

        /// <summary>
        /// Constructs a new instance of Node with the name set
        /// </summary>
        /// <param name="name">The name of the Node</param>
        /// <param name="code">The code contained in the node</param>
        /// <param name="openNode">The node listed as the parent node to the current node</param>
        public Node(string name, string code, INode? openNode = null)
        {
            OpeningNode = openNode;
            Code = code;
            Name = name;
            Children = new List<INode>();
        }
        #endregion
    }
}
