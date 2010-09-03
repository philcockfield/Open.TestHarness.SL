using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>Represents a node within a tree data-structure.</summary>
    public interface ITreeNode
    {
        /// <summary>Fires when the IsSelected value changes.</summary>
        event EventHandler SelectionChanged;

        /// <summary>Fires when when the IsSelected value on a child node changes.</summary>
        event EventHandler ChildSelectionChanged;

        /// <summary>Fires when when a child node is added.</summary>
        event TreeNodeHandler ChildAdded;

        /// <summary>Fires when when a child node is removed.</summary>
        event TreeNodeHandler ChildRemoved;

        /// <summary>Fires when a child node is either added or removed.</summary>
        event EventHandler ChildrenChanged;

        /// <summary>Gets the parent node.</summary>
        ITreeNode Parent { get;  }

        /// <summary>Gets whether the node is at the root of the tree (ie. does not have a parent).</summary>
        bool IsRoot { get; }

        /// <summary>Gets or sets whether the node is currently selected.</summary>
        bool IsSelected { get; set; }

        /// <summary>Gets the colleciton of ITreeNode children.</summary>
        IEnumerable Children { get;  }

        /// <summary>Gets the total number of children.</summary>
        int TotalChildren { get; }

        /// <summary>Adds the given node to the end of the Children collection.</summary>
        /// <param name="node">The node to add.</param>
        /// <remarks>Nodes cannot be added more than once.  Subsequent adding of the same node is ignored.</remarks>
        void AddChild(ITreeNode node);

        /// <summary>Inserts the given node at the specified index within the Children collection.</summary>
        /// <param name="index">The index to insert at (0-based).</param>
        /// <param name="node">The node to add.</param>
        /// <remarks>Nodes cannot be added more than once.  Subsequent adding of the same node is ignored.</remarks>
        void InsertChild(int index, ITreeNode node);

        /// <summary>Removes the given node from the Children collection.</summary>
        /// <param name="node">The node to remove.</param>
        void RemoveChild(ITreeNode node);

        /// <summary>Removes all children.</summary>
        void ClearChildren();

        /// <summary>Determines whether the given node is contained directly within the Children collection.</summary>
        /// <param name="node">The node to look for.</param>
        bool Contains(ITreeNode node);

        /// <summary>Determines whether the given node is a descendent (anywhere within the child hierarchy).</summary>
        /// <param name="node">The node to examine.</param>
        bool ContainsDescendent(ITreeNode node);

        /// <summary>Retrieves the child at the given index.</summary>
        /// <param name="index">The index of the child (0-based).</param>
        /// <remarks>If the index is out of range a null value is returned.</remarks>
        ITreeNode ChildAt(int index);
    }
}
