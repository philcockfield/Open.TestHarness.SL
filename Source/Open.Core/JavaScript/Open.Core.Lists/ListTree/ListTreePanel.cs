using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>Renders a single list within a tree-of lists.</summary>
    internal class ListTreePanel : ViewBase
    {
        #region Head
        private readonly ITreeNode node;
        private readonly ListTreeView parentList;
        private readonly ListView listView;

        public ListTreePanel(ListTreeView parentList, ITreeNode node) : base(Html.CreateDiv())
        {
            // Store value.
            this.parentList = parentList;
            this.node = node;

            // Insert HTML container.
            Hide();
            Css.AbsoluteFill(Container);

            // Create list.
            listView = new ListView(Container);
            listView.Load(node.Children);

            // Wire up events.
            GlobalEvents.HorizontalPanelResized += OnHorizontalPanelResized;
            node.ChildSelectionChanged += OnChildSelectionChanged;
            node.AddedChild += OnAddedChild;
            node.RemovedChild += OnRemovedChild;
            if (node.Parent != null) node.Parent.RemovingChild += OnParentRemovingChild;

            // Finish up.
            SyncWidth();
        }

        protected override void OnDisposed()
        {
            // Setup initial conditions.
            Container.Remove();

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
        public bool IsCenterStage { get { return GetCss(Css.Left) == "0px"; } }
        public ListView ListView { get { return listView; } }
        #endregion

        #region Methods
        public void SlideOff(HorizontalEdge direction, Action onComplete)
        {
            // Ensure the panel is on stage.
            CenterStage();

            // Configure the animation.
            Dictionary properties = new Dictionary();
            properties[Css.Left] = direction == HorizontalEdge.Left ? 0 - Width : Width;

            // Perform animation.
            Container.Animate(
                        properties,
                        parentList.Slide.ToMsecs(), 
                        parentList.Slide.Easing, 
                        delegate
                            {
                                // On complete.
                                Hide();
                                Helper.Invoke(onComplete);
                            });
        }

        public void SlideOn(HorizontalEdge direction, Action onComplete)
        {
            // Prepare the panels starting position.
            SetPosition(direction, true);

            // Configure the animation.
            Dictionary properties = new Dictionary();
            properties[Css.Left] = 0;

            // Perform animation.
            Container.Animate(
                        properties,
                        parentList.Slide.ToMsecs(), 
                        parentList.Slide.Easing,
                        delegate
                            {
                                // On complete.
                                Helper.Invoke(onComplete);
                            });
        }

        public void CenterStage()
        {
            Container.CSS(Css.Left, "0px");
            Container.CSS(Css.Display, Css.Block);
            SyncWidth();
        }

        public void SetPosition(HorizontalEdge direction, bool isVisible)
        {
            int startLeft = direction == HorizontalEdge.Right ? 0 - Width : Width;
            Container.CSS(Css.Left, startLeft + Css.Px);
            Container.CSS(Css.Display, isVisible ? Css.Block : Css.None);
            SyncWidth();
        }
        public void SyncWidth() { Width = parentList.Width; }
        #endregion

        #region Internal
        private void Hide() { Container.CSS(Css.Display, Css.None); }

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