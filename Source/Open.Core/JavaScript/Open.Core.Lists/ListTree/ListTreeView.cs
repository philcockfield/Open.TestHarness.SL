using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>Represents a tree structure of lists.</summary>
    public class ListTreeView : ViewBase
    {
        #region Events
        /// <summary>Fires when the selected-node property changes.</summary>
        public event EventHandler SelectedNodeChanged;
        private void FireSelectedNodeChanged() { if (SelectedNodeChanged != null) SelectedNodeChanged(this, new EventArgs()); }

        /// <summary>Fires when the selected-parent property changes.</summary>
        public event EventHandler SelectedParentChanged;
        private void FireSelectedParentChanged(){if (SelectedParentChanged != null) SelectedParentChanged(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropRootNode = "RootNode";
        public const string PropSelectedNode = "SelectedNode";
        public const string PropSelectedParent = "SelectedParent";

        private readonly jQueryObject divInner;
        private readonly ArrayList panels = new ArrayList();
        private ITreeNode previousNode;
        private readonly AnimationSettings slide = new AnimationSettings();

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing element.</param>
        public ListTreeView(jQueryObject container) : base(container)
        {
            ListCss.InsertCss();
            divInner = Html.AppendDiv(container);
            Css.AbsoluteFill(divInner);
            Css.SetOverflow(divInner, CssOverflow.Hidden);
        }

        protected override void OnDisposed()
        {
            Reset();
            base.OnDisposed();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the root node that the tree of lists built from.</summary>
        public ITreeNode RootNode
        {
            get { return Get(PropRootNode, null) as ITreeNode; }
            set
            {
                if (Set(PropRootNode, value, null))
                {
                    Reset();
                    SelectedNode = value;
                }
            }
        }

        /// <summary>Gets or sets the currently selected node (it's Children are what is displayed in the visible list).</summary>
        public ITreeNode SelectedNode
        {
            get { return Get(PropSelectedNode, null) as ITreeNode; }
            set
            {
                if (Set(PropSelectedNode, value, null))
                {
                    if (value != null && (value.ChildCount > 0 || value.IsRoot)) SelectedParent = value;
                    FireSelectedNodeChanged();
                }
            }
        }

        /// <summary>Gets or sets the node which is the root of the current list (may be the same as SelectedNode).</summary>
        public ITreeNode SelectedParent
        {
            get { return (ITreeNode)Get(PropSelectedParent, null); }
            set
            {
                if (Set(PropSelectedParent, value, null))
                {
                    // Deselect children of the previous value.
                    if (previousNode != null) Helper.Tree.DeselectChildren(previousNode);

                    // Update the list.
                    if (value != null)
                    {
                        if (previousNode == null)
                        {
                            // There is no previous node to slide from.  Show the list immediately.
                            GetOrCreatePanel(value).CenterStage();
                        }
                        else
                        {
                            // Slide the panel into view.
                            SlidePanels(previousNode, value);
                        }
                    }

                    // Finish up.
                    FireSelectedParentChanged();
                    previousNode = value;
                }
            }
        }

        /// <summary>Gets the slide animation settings.</summary>
        public AnimationSettings Slide { get { return slide; } }

        /// <summary>Gets the scroll height of the list.</summary>
        public int ScrollHeight
        {
            get
            {
                ListTreePanel panel = SelectedPanel;
                return panel == null
                            ? Container.GetHeight()
                            : panel.ListView.ScrollHeight;
            }
        }

        /// <summary>Gets the offset height of the items within the list.</summary>
        public int ContentHeight
        {
            get
            {
                ListTreePanel panel = SelectedPanel;
                return panel == null
                            ? 0
                            : panel.ListView.ContentHeight;
            }
        }

        private ListTreePanel SelectedPanel { get { return GetPanel(SelectedParent); } }
        #endregion

        #region Methods
        /// <summary>Moves the selected node to the parent of the current node.</summary>
        public void Back()
        {
            if (SelectedParent == null || SelectedParent.IsRoot) return;
            SelectedNode = SelectedParent.Parent;
        }

        /// <summary>Moves the selected node to the root node.</summary>
        public void Home()
        {
            SelectedNode = RootNode;
        }

        /// <summary>Updates the visual state of the control.</summary>
        public void UpdateLayout()
        {
            foreach (ListTreePanel panel in panels)
            {
                panel.SyncWidth();
            }
        }
        #endregion

        #region Internal
        private void SlidePanels(ITreeNode previousNode, ITreeNode newNode)
        {
            // Setup initial conditions.
            HorizontalDirection direction = GetSlideDirection(previousNode, newNode);

            // Slide off the old panel.
            if (previousNode != null)
            {
                ListTreePanel oldPanel = GetOrCreatePanel(previousNode);
                oldPanel.SlideOff(direction, null);
            }

            // Slide on the new panel.
            ListTreePanel panel = GetOrCreatePanel(newNode);
            panel.SlideOn(direction, null);
        }

        private static HorizontalDirection GetSlideDirection(ITreeNode previousNode, ITreeNode newNode)
        {
            if (previousNode == null) return HorizontalDirection.Left;
            return previousNode.ContainsDescendent(newNode) 
                       ? HorizontalDirection.Left 
                       : HorizontalDirection.Right;
        }

        private ListTreePanel GetOrCreatePanel(ITreeNode node)
        {
            return GetPanel(node) ?? CreatePanel(node);
        }

        private ListTreePanel GetPanel(ITreeNode node)
        {
            if (node == null) return null;
            foreach (ListTreePanel panel in panels)
            {
                if (panel.Node == node) return panel;
            }
            return null;
        }

        private ListTreePanel CreatePanel(ITreeNode node)
        {
            ListTreePanel panel = new ListTreePanel(this, node);
            panel.Container.AppendTo(divInner);
            panels.Add(panel);
            return panel;
        }

        private void Reset()
        {
            Helper.Collection.DisposeAndClear(panels);
            divInner.Empty();
        }
        #endregion
    }
}
