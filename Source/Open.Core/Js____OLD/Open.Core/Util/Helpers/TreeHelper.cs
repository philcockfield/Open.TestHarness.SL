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
        /// <param name="parent">The node to look within.</param>
        /// <returns>The first selected child, or null if no children are selected.</returns>
        public ITreeNode FirstSelectedChild(ITreeNode parent)
        {
            if (parent == null) return null;
            foreach (ITreeNode child in parent.Children)
            {
                if (child.IsSelected) return child;
            }
            return null;
        }

        /// <summary>Determines whether at least one of the children of the given node are selected.</summary>
        /// <param name="parent">The parent to examine.</param>
        public bool HasSelectedChild(ITreeNode parent) { return FirstSelectedChild(parent) != null; }

        /// <summary>Deselects all children of the given node.</summary>
        /// <param name="parent">The node to deselect the children of.</param>
        /// <returns>The total number of children that were deselected.</returns>
        public int DeselectChildren(ITreeNode parent)
        {
            if (parent == null) return 0;
            int total = 0;
            foreach (ITreeNode child in parent.Children)
            {
                if (child.IsSelected)
                {
                    child.IsSelected = false;
                    total++;
                }
            }
            return total;
        }

        /// <summary>Gets the first descendent node that matches the given predicate.</summary>
        /// <param name="parent">The parent to look within.</param>
        /// <param name="predicate">The predicate used to match.</param>
        /// <returns></returns>
        public ITreeNode FirstDescendent(ITreeNode parent, FuncBool predicate)
        {
            // Setup initial conditions.
            if (parent == null || predicate == null) return null;

            // Look for item in direct children.
            object item = Helper.Collection.First(parent.Children, predicate);
            if (item != null) return item as ITreeNode;

            // Not found - recursively call back for each child.
            foreach (ITreeNode child in parent.Children)
            {
                ITreeNode descendent = FirstDescendent(child, predicate);
                if (descendent != null) return descendent;
            }

            // Finish up (not found).
            return null;
        }
    }
}
