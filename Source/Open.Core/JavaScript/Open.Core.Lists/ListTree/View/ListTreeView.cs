using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>Represents a tree structure of lists.</summary>
    public class ListTreeView : ViewBase
    {
        #region Head
        /// <summary>Fires when the selected node changes.</summary>
        public event EventHandler SelectionChanged;
        private void FireSelectionChanged(){if (SelectionChanged != null) SelectionChanged(this, new EventArgs());}

        public const string PropRootNode = "RootNode";
        public const string PropSelectedNode = "SelectedNode";
        public const string PropCurrentListRoot = "CurrentListRoot";

        private jQueryObject div;
        private double slideDuration = 0.4;
        EffectEasing slideEasing = EffectEasing.Swing;
        private readonly ArrayList panels = new ArrayList();

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing element.</param>
        public ListTreeView(jQueryObject container)
        {
            Initialize(container);
            ListCss.InsertCss();
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
                    if (value != null && value.TotalChildren > 0) CurrentListRoot = value;
                    FireSelectionChanged();
                }
            }
        }

        /// <summary>Gets or sets the node which is the root of the current list (may be the same as SelectedNode).</summary>
        public ITreeNode CurrentListRoot
        {
            get { return (ITreeNode)Get(PropCurrentListRoot, null); }
            set
            {
                ITreeNode previousNode = CurrentListRoot;
                if (Set(PropCurrentListRoot, value, null))
                {
                    // Update the list.
                    if (value != null)
                    {
                        DeselectChildren(value);
                        if (value.TotalChildren > 0)
                        {
                            if (previousNode == null)
                            {
                                // There is not previous node to slide from.  Show the list immediately.
                                GetOrCreatePanel(value, true).CenterStage();
                            }
                            else
                            {
                                // Slide the panel into view.
                                SlidePanels(previousNode, value);
                            }
                        }
                    }
                    // Finish up.
                    FirePropertyChanged(PropCurrentListRoot);
                }
            }
        }

        /// <summary>Gets or sets the slide duration (in seconds).</summary>
        public double SlideDuration
        {
            get { return slideDuration; }
            set { slideDuration = value; }
        }

        /// <summary>Gets or sets the easing effect applied to the slide.</summary>
        public EffectEasing SlideEasing
        {
            get { return slideEasing; }
            set { slideEasing = value; }
        }
        #endregion

        #region Methods
        protected override void OnInitialize(jQueryObject container)
        {
            div = Html.AppendDiv(container);
            Css.AbsoluteFill(div);
            Css.SetOverflow(div, CssOverflow.Hidden);
        }

        /// <summary>Moves the selected node to the parent of the current node.</summary>
        public void Back()
        {
            if (CurrentListRoot == null || CurrentListRoot.IsRoot) return;
            SelectedNode = CurrentListRoot.Parent;
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
                ListTreePanel oldPanel = GetOrCreatePanel(previousNode, true);
                oldPanel.SlideOff(direction, null);
            }

            // Slide on the new panel.
            ListTreePanel panel = GetOrCreatePanel(newNode, true);
            panel.SlideOn(direction, null);
        }

        private static HorizontalDirection GetSlideDirection(ITreeNode previousNode, ITreeNode newNode)
        {
            if (previousNode == null) return HorizontalDirection.Left;
            return previousNode.ContainsDescendent(newNode) 
                       ? HorizontalDirection.Left 
                       : HorizontalDirection.Right;
        }

        private void DeselectChildren(ITreeNode node)
        {
            foreach (ITreeNode child in node.Children)
            {
                child.IsSelected = false;
            }
        }

        private ListTreePanel GetOrCreatePanel(ITreeNode node, bool initialize)
        {
            // Get or create the panel if it doesn't exist.
            ListTreePanel panel = GetPanel(node) ?? CreatePanel(node);

            // Finish up.
            if (initialize && !panel.IsInitialized) panel.Initialize(div);
            return panel;
        }

        private ListTreePanel GetPanel(ITreeNode node)
        {
            foreach (ListTreePanel panel in panels)
            {
                if (panel.RootNode == node) return panel;
            }
            return null;
        }

        private ListTreePanel CreatePanel(ITreeNode node)
        {
            ListTreePanel panel = new ListTreePanel(this, div, node);
            panels.Add(panel);
            return panel;
        }

        private void Reset()
        {
            foreach (ListTreePanel panel in panels)
            {
                panel.Dispose();
            }
        }
        #endregion
    }
}
