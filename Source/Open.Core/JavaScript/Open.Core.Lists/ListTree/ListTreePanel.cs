using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>Renders a single list within a tree-of lists.</summary>
    internal class ListTreePanel : ViewBase
    {
        #region Head
        private readonly jQueryObject rootDiv;
        private jQueryObject div;
        private readonly ITreeNode node;
        private readonly ListTreeView parentList;
        private ListView listView;

        public ListTreePanel(ListTreeView parentList, jQueryObject rootDiv, ITreeNode node)
        {
            // Store value.
            this.parentList = parentList;
            this.rootDiv = rootDiv;
            this.node = node;

            // Wire up events.
            GlobalEvents.HorizontalPanelResized += OnHorizontalPanelResized;
            node.ChildSelectionChanged += OnChildSelectionChanged;
            node.AddedChild += OnAddedChild;
            node.RemovedChild += OnRemovedChild;
            if (node.Parent != null) node.Parent.RemovingChild += OnParentRemovingChild;
        }

        protected override void OnInitialize(jQueryObject container)
        {
            // Insert HTML container.
            div = Html.AppendDiv(container);
            div = container.Children(Html.Div).Last(); // NB: Ensure we get a stable reference to the div.
            Hide();
            Css.AbsoluteFill(div);

            // Create list.
            listView = new ListView(div);
            listView.Load(node.Children);

            // Finish up.
            SyncWidth();
        }

        protected override void OnDisposed()
        {
            // Setup initial conditions.
            div.Remove();

            // Unwire events.
            GlobalEvents.HorizontalPanelResized -= OnHorizontalPanelResized;
            node.ChildSelectionChanged -= OnChildSelectionChanged;
            node.AddedChild -= OnAddedChild;
            node.RemovedChild -= OnRemovedChild;
            if (node.Parent != null) node.Parent.RemovingChild -= OnParentRemovingChild;

            // Finish up.
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnChildSelectionChanged(object sender, EventArgs e)
        {
            ITreeNode selectedNode = GetSelectedChild();
            if (!Script.IsNullOrUndefined(selectedNode))
            {
                parentList.SelectedNode = selectedNode;
            }
        }

        private void OnHorizontalPanelResized(object sender, EventArgs e) { SyncWidth(); }
        private void OnAddedChild(object sender, TreeNodeEventArgs e) { listView.Insert(e.Index, e.Node); }
        private void OnRemovedChild(object sender, TreeNodeEventArgs e) { listView.Remove(e.Node); }

        private void OnParentRemovingChild(object sender, TreeNodeEventArgs e)
        {
            // Setup initial conditions.
            if (e.Node != node) return;

            // Slide the list-tree back to the next remaining parent.
            if (parentList.RootNode != null)
            {
                ITreeNode ancestor = Helper.Tree.FirstRemainingParent(parentList.RootNode, node);
                parentList.SelectedParent = ancestor ?? parentList.RootNode;
            }

            // The node for this panel has been removed.  Dispose of the panel.
            Dispose();
        }
        #endregion

        #region Properties
        public ITreeNode Node { get { return node; } }
        public bool IsCenterStage { get { return div.GetCSS(Css.Left) == "0px"; } }
        public ListView ListView { get { return listView; } }

        private int Width { get { return rootDiv.GetWidth(); } }
        private int SlideDuration { get { return Helper.Number.ToMsecs(parentList.SlideDuration); } }
        #endregion

        #region Methods
        public void SlideOff(HorizontalDirection direction, Action onComplete)
        {
            // Setup initial conditions.
            if (!IsInitialized) return;

            // Ensure the panel is on stage.
            CenterStage();

            // Configure the animation.
            Dictionary properties = new Dictionary();
            properties[Css.Left] = direction == HorizontalDirection.Left ? 0 - Width : Width;

            // Perform animation.
            div.Animate(
                        properties, 
                        SlideDuration, 
                        parentList.SlideEasing, 
                        delegate
                            {
                                // On complete.
                                Hide();
                                Helper.InvokeOrDefault(onComplete);
                            });
        }

        public void SlideOn(HorizontalDirection direction, Action onComplete)
        {
            // Setup initial conditions.
            if (!IsInitialized) return;

            // Prepare the panels starting position.
            SetPosition(direction, true);

            // Configure the animation.
            Dictionary properties = new Dictionary();
            properties[Css.Left] = 0;

            // Perform animation.
            div.Animate(
                        properties,
                        SlideDuration,
                        parentList.SlideEasing,
                        delegate
                            {
                                // On complete.
                                Helper.InvokeOrDefault(onComplete);
                            });
        }

        public void CenterStage()
        {
            div.CSS(Css.Left, "0px");
            div.CSS(Css.Display, Css.Block);
            SyncWidth();
        }

        public void SetPosition(HorizontalDirection direction, bool isVisible)
        {
            int startLeft = direction == HorizontalDirection.Right ? 0 - Width : Width;
            div.CSS(Css.Left, startLeft + Css.Px);
            div.CSS(Css.Display, isVisible ? Css.Block : Css.None);
            SyncWidth();
        }
        public void SyncWidth() { div.Width(Width); }
        #endregion

        #region Internal
        private void Hide() { div.CSS(Css.Display, Css.None); }

        private ITreeNode GetSelectedChild()
        {
            foreach (ITreeNode item in node.Children)
            {
                if (item.IsSelected) return item;
            }
            return null;
        }
        #endregion
    }
}