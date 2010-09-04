using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>Factory delegate used in JSON parsing.</summary>
    /// <param name="node">The dictionary representing the node to create.</param>
    public delegate TreeNode TreeNodeFactory(Dictionary node);


    /// <summary>Represents a node within a tree data-structure.</summary>
    public abstract class TreeNode : ModelBase, ITreeNode, IDisposable
    {
        #region Events : ITreeNode
        public event EventHandler SelectionChanged;
        private void FireSelectionChanged() { if (SelectionChanged != null) SelectionChanged(this, new EventArgs()); }

        public event EventHandler Click;
        internal void FireClick(){if (Click != null) Click(this, new EventArgs());} // NB: Access = 'internal' so the FireClick helper method can access it.

        public event EventHandler ChildSelectionChanged;
        private void FireChildSelectionChanged() { if (ChildSelectionChanged != null) ChildSelectionChanged(this, new EventArgs()); }

        public event TreeNodeHandler AddingChild;
        private void FireAddingChild(TreeNodeEventArgs e) { if (AddingChild != null) AddingChild(this, e); }

        public event TreeNodeHandler AddedChild;
        private void FireChildAdded(TreeNodeEventArgs e) { if (AddedChild != null) AddedChild(this, e); FireChildrenChanged(); }

        public event TreeNodeHandler RemovedChild;
        private void FireChildRemoved(TreeNodeEventArgs e) { if (RemovedChild != null) RemovedChild(this, e); FireChildrenChanged(); }

        public event TreeNodeHandler RemovingChild;
        private void FireRemovingChild(TreeNodeEventArgs e) { if (RemovingChild != null) RemovingChild(this, e); }

        public event EventHandler ChildrenChanged;
        private void FireChildrenChanged(){if (ChildrenChanged != null) ChildrenChanged(this, new EventArgs());}
        #endregion

        #region Head
        /// <summary>The index number of a node if it's not-known or is not applicable to the scenario.</summary>
        public const int NullIndex = -1;

        public const string PropIsSelected = "IsSelected";
        public const string PropChildren = "Children";

        private ITreeNode parent;
        private ArrayList childList;


        protected override void OnDisposed()
        {
            // Dispose of child nodes.
            if (childList != null)
            {
                foreach (IDisposable child in Children)
                {
                    child.Dispose();
                }
            }

            // Finish up.
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnChildSelectionChanged(object sender, EventArgs e)
        {
            FireChildSelectionChanged();
        }
        #endregion

        #region Properties : ITreeNode
        public ITreeNode Parent { get { return parent; } }
        public ITreeNode Root { get { return GetRoot(); } }
        public bool IsRoot { get { return Parent == null; } }
        public IEnumerable Children { get { return ChildList; } }
        public int ChildCount { get { return childList == null ? 0 : childList.Count; } }

        public bool IsSelected
        {
            get { return (bool)Get(PropIsSelected, false); }
            set { if (Set(PropIsSelected, value, false)) FireSelectionChanged(); }
        }
        #endregion

        #region Properties : Private
        private ArrayList ChildList { get { return childList ?? (childList = new ArrayList()); } }
        #endregion

        #region Methods
        public override string ToString() { return string.Format("[{0}({1})]", GetType().Name, ChildCount); }
        #endregion

        #region Methods : JSON
        public override string ToJson() { return Helper.Json.Serialize(ToDictionary()); }

        /// <summary>Creates a new instance of the node from JSON.</summary>
        /// <param name="json">The JSON string to parse.</param>
        /// <param name="factory">The factory method for creating new nodes.</param>
        public static TreeNode FromJson(string json, TreeNodeFactory factory)
        {
            return FromDictionary(Helper.Json.Parse(json), factory);
        }

        /// <summary>Allows deriving classes to suppliment the dictionary used for JSON serialization.</summary>
        /// <param name="node">The dictionary representing the node to process.</param>
        /// <remarks>Use this to add custom properties only.  Do not worry about the Children collection.</remarks>
        protected virtual void SerializingJson(Dictionary node) { }
        #endregion

        #region Methods : Children Collection
        public void AddChild(ITreeNode node) { InsertChild(NullIndex, node); }

        public void InsertChild(int index, ITreeNode node)
        {
            // Setup initial conditions.
            if (node == null) return;
            if (Contains(node)) return; // Ignore if the node has already been added.
            if (index < 0) index = ChildCount;

            // Fire pre-event.
            TreeNodeEventArgs args = new TreeNodeEventArgs(node, index);
            FireAddingChild(args);

            // Store the node.
            ChildList.Insert(index, node);

            // Wire up events.
            node.SelectionChanged += OnChildSelectionChanged;

            // Ensure the parent node is set to this.
            if (node.Parent != this) SetParent(node, this);

            // Fire post-event.
            FireChildAdded(args);
        }

        public void RemoveChild(ITreeNode node)
        {
            // Ignore if the node has already been added.
            if (!Contains(node)) return;

            // Fire pre-event.
            TreeNodeEventArgs args = new TreeNodeEventArgs(node, NullIndex);
            FireRemovingChild(args);

            // Remove the child.
            ChildList.Remove(node);

            // Unwire events.
            node.SelectionChanged -= OnChildSelectionChanged;

            // De-register this as the nodes parent.
            if (node.Parent == this) SetParent(node, null);

            // Fire post-event.
            FireChildRemoved(args);
        }

        public void ClearChildren()
        {
            foreach (ITreeNode child in ChildList.Clone())
            {
                RemoveChild(child);
            }
        }

        public ITreeNode ChildAt(int index) { return childList == null ? null : ChildList[index] as ITreeNode; }
        public bool Contains(ITreeNode node) { return ChildList.Contains(node); }
        public bool ContainsDescendent(ITreeNode node) { return IsDescendent(this, node); }
        #endregion

        #region Internal
        private Dictionary ToDictionary()
        {
            // Setup initial conditions.
            Dictionary json = new Dictionary();
            SerializingJson(json);

            // Add children.
            ArrayList children = new ArrayList();
            foreach (TreeNode child in Children)
            {
                children.Add(child.ToDictionary());
            }
            json[PropChildren] = children;

            // Finish up.
            return json;
        }

        private static TreeNode FromDictionary(Dictionary dic, TreeNodeFactory factory)
        {
            // Setup initial conditions.
            TreeNode node = factory(dic);

            // Enumerate children.
            Array children = dic[PropChildren] as Array;
            if (children != null)
            {
                foreach (Dictionary child in children)
                {
                    TreeNode childNode = FromDictionary(child, factory);
                    node.AddChild(childNode);
                }
            }

            // Finish up.
            return node;
        }

        private static void SetParent(ITreeNode node, ITreeNode value)
        {
            TreeNode concrete = node as TreeNode;
            if (concrete == null) return;
            concrete.parent = value;
        }

        private ITreeNode GetRoot()
        {
            if (IsRoot) return null;
            ITreeNode parentNode = Parent;
            do
            {
                if (parentNode == null) break;
                if (parentNode.IsRoot) return parentNode;
                parentNode = parentNode.Parent;
            } while (parentNode != null);
            return null;
        }

        private static bool IsDescendent(ITreeNode parent, ITreeNode node)
        {
            if (Script.IsNullOrUndefined(node)) return false;
            if (parent.Contains(node)) return true;
            foreach (ITreeNode child in parent.Children)
            {
                if (IsDescendent(child, node)) return true;
            }
            return false;
        }
        #endregion
    }
}
