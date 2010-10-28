using System;

namespace Open.Core
{
    /// <summary>Event arguments accompanying a 'TreeNode' operation.</summary>
    public class TreeNodeEventArgs : EventArgs
    {
        #region Head
        private readonly ITreeNode node;
        private readonly int index;

        /// <summary>Constructor.</summary>
        /// <param name="node">The tree-node which is the subject of the event.</param>
        /// <param name="index">The index of the node within it's parent (-1 if not known or applicable).</param>
        public TreeNodeEventArgs(ITreeNode node, int index)
        {
            this.node = node;
            this.index = index;
        }
        #endregion

        #region Properties
        /// <summary>Gets the tree-node which is the subject of the event.</summary>
        public ITreeNode Node { get { return node; } }

        /// <summary>Gets the index of the node within it's parent (-1 if not known or applicable).</summary>
        public int Index { get { return index; } }

        /// <summary>Gets whether an index value exists.</summary>
        public bool HasIndex { get { return Index != TreeNode.NullIndex; } }
        #endregion
    }

    /// <summary>Defines the handler for an event pertaining to a 'TreeNode'.</summary>
    /// <param name="sender">The object that fired the event.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void TreeNodeHandler(object sender, TreeNodeEventArgs e);

}
