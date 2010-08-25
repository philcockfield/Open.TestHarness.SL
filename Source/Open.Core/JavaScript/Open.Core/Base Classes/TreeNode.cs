using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>Factory delegate used in JSON parsing.</summary>
    /// <param name="node">The dictionary representing the node to create.</param>
    public delegate TreeNode TreeNodeFactory(Dictionary node);


    public abstract class TreeNode : ModelBase, ITreeNode, IDisposable
    {
        #region Event Handlers
        public event EventHandler SelectionChanged;
        private void FireSelectionChanged() { if (SelectionChanged != null) SelectionChanged(this, new EventArgs()); }

        public event EventHandler ChildSelectionChanged;
        private void FireChildSelectionChanged() { if (ChildSelectionChanged != null) ChildSelectionChanged(this, new EventArgs()); }
        #endregion

        #region Head
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

        #region Properties
        public ITreeNode Parent { get { return parent; } }
        public bool IsRoot { get { return Parent == null; } }
        public IEnumerable Children { get { return ChildList; } }
        public int TotalChildren { get { return childList == null ? 0 : childList.Count; } }

        public bool IsSelected
        {
            get { return (bool)Get(PropIsSelected, false); }
            set { if (Set(PropIsSelected, value, false)) FireSelectionChanged(); }
        }
        #endregion

        #region Properties : Private
        private ArrayList ChildList { get { return childList ?? (childList = new ArrayList()); } }
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
        public void Add(ITreeNode node)
        {
            // Ignore if the node has already been added.
            if (Contains(node)) return;

            // Store the node.
            ChildList.Add(node);

            // Wire up events.
            node.SelectionChanged += OnChildSelectionChanged;

            // Ensure the parent node is set to this.
            if (node.Parent != this) SetParent(node, this);
        }

        public void Remove(ITreeNode node)
        {
            // Ignore if the node has already been added.
            if (!Contains(node)) return;

            // Remove the child.
            ChildList.Remove(node);

            // Unwire events.
            node.SelectionChanged -= OnChildSelectionChanged;

            // De-register this as the nodes parent.
            if (node.Parent == this) SetParent(node, null);
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
                    node.Add(childNode);
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
