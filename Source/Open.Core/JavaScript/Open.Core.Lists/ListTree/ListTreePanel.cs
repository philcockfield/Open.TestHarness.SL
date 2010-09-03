using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>Renders a single list within a tree-of lists.</summary>
    internal class ListTreePanel : ViewBase
    {
        #region Head
        private jQueryObject div;
        private readonly ListTreeView listTreeView;
        private ListView listView;
        private readonly jQueryObject rootDiv;
        private readonly ITreeNode rootNode;

        public ListTreePanel(ListTreeView listTreeView, jQueryObject rootDiv, ITreeNode rootNode)
        {
            // Store value.
            this.listTreeView = listTreeView;
            this.rootDiv = rootDiv;
            this.rootNode = rootNode;

            // Wire up events.
            GlobalEvents.HorizontalPanelResized += OnHorizontalPanelResized;
            rootNode.ChildSelectionChanged += OnChildSelectionChanged;
            rootNode.ChildAdded += OnChildAdded;
            rootNode.ChildRemoved += OnChildRemoved;
        }

        protected override void OnDisposed()
        {
            // Setup initial conditions.
            div.Empty();

            // Unwire events.
            GlobalEvents.HorizontalPanelResized -= OnHorizontalPanelResized;
            rootNode.ChildSelectionChanged -= OnChildSelectionChanged;
            rootNode.ChildAdded -= OnChildAdded;
            rootNode.ChildRemoved -= OnChildRemoved;

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
                listTreeView.SelectedNode = selectedNode;
            }
        }

        private void OnHorizontalPanelResized(object sender, EventArgs e) { SyncWidth(); }
        private void OnChildAdded(object sender, TreeNodeEventArgs e) { listView.Insert(e.Index, e.Node); }
        private void OnChildRemoved(object sender, TreeNodeEventArgs e) { listView.Remove(e.Node); }
        #endregion

        #region Properties
        public ITreeNode RootNode { get { return rootNode; } }
        public bool IsCenterStage { get { return div.GetCSS(Css.Left) == "0px"; } }

        private int Width { get { return rootDiv.GetWidth(); } }
        private int SlideDuration { get { return Helper.Number.ToMsecs(listTreeView.SlideDuration); } }
        #endregion

        #region Methods
        protected override void OnInitialize(jQueryObject container)
        {
            // Insert HTML container.
            div = Html.AppendDiv(container);
            div = container.Children(Html.Div).Last(); // NB: Ensure we get a stable reference to the div.
            Hide();
            Css.AbsoluteFill(div);

            // Create list.
            listView = new ListView(div);
            listView.Load(rootNode.Children);

            // Finish up.
            SyncWidth();
        }

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
                        listTreeView.SlideEasing, 
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
                        listTreeView.SlideEasing,
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
            div.CSS(Css.Left, startLeft + "px");
            div.CSS(Css.Display, isVisible ? Css.Block : Css.None);
            SyncWidth();
        }
        #endregion

        #region Internal
        private void Hide() { div.CSS(Css.Display, Css.None); }
        private void SyncWidth() { div.Width(Width); }

        private ITreeNode GetSelectedChild()
        {
            foreach (ITreeNode node in rootNode.Children)
            {
                if (node.IsSelected) return node;
            }
            return null;
        }
        #endregion
    }
}