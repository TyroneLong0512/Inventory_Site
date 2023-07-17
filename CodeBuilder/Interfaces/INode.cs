namespace CodeBuilder.Interfaces
{
    /// <summary>
    /// Interface to be derived in the Node base class
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// The closing node relating to this node (optional)
        /// </summary>
        INode? OpeningNode { get; set; }

        /// <summary>
        /// The name of the node (optional)
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The code contained in the node
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// The children contained in the node
        /// </summary>
        IEnumerable<INode> Children { get; set; }
    }
}
