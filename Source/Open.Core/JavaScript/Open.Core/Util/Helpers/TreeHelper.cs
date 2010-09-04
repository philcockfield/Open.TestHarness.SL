namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with Tree data-structures.</summary>
    public class TreeHelper
    {
        /// <summary>Retrieves the first ancestor of a node that still exists as a descendent of the root node.</summary>
        /// <param name="root">The root node.</param>
        /// <param name="orphan">The orphaned node.</param>
        /// <remarks>Useful for when a node is removed, and you want to move a view to the reamining parent.</remarks>
        public ITreeNode FirstRemainingParent(ITreeNode root, ITreeNode orphan)
        {
            if (root == null || orphan == null) return null;
            ITreeNode parent = orphan.Parent;
            do
            {
                if (parent == null) break;
                if (parent == root || root.ContainsDescendent(parent)) return parent;
                parent = parent.Parent;
            } while (parent != null);
            return null;
        }

        /// <summary>Retrieves the first selected child node.</summary>
        /// <param name="node">The node to look within.</param>
        /// <returns>The first selected child, or null if no children are selected.</returns>
        public ITreeNode FirstSelectedChild(ITreeNode node)
        {
            if (node == null) return null;
            foreach (ITreeNode child in node.Children)
            {
                if (child.IsSelected) return child;
            }
            return null;
        }
    }
}
