using System;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>A controller for attaching a 'Back' and 'Home' button to a ListTree.</summary>
    public class ListTreeBackController : ControllerBase
    {
        #region Head
        private readonly ListTreeView listTree;
        private readonly jQueryObject backButton;
        private readonly jQueryObject homeButton;

        /// <summary>Constructor.</summary>
        /// <param name="listTree">The list tree under control.</param>
        /// <param name="backButton">The back button.</param>
        /// <param name="homeButton">The home button.</param>
        public ListTreeBackController(ListTreeView listTree, jQueryObject backButton, jQueryObject homeButton)
        {
            // Store values.
            this.listTree = listTree;
            this.backButton = backButton;
            this.homeButton = homeButton;

            // Wire up events.
            listTree.SelectionChanged += OnSelectionChanged;
            backButton.Click(OnBackClick);
            homeButton.Click(OnHomeClick);
        }

        protected override void OnDisposed()
        {
            listTree.SelectionChanged -= OnSelectionChanged;
            backButton.Unbind(Html.Click, OnBackClick);
            homeButton.Unbind(Html.Click, OnHomeClick);
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnSelectionChanged(object sender, EventArgs e) { UpdateHomeButton(); }
        private void OnBackClick(jQueryEvent e) { listTree.Back(); }
        private void OnHomeClick(jQueryEvent e) { listTree.SelectedNode = listTree.RootNode; }
        #endregion

        #region Properties
        /// <summary>Gets the list-tree which is under control.</summary>
        public ListTreeView ListTree{get { return listTree; }}

        /// <summary>Gets the 'Back' button.</summary>
        public jQueryObject BackButton{get { return backButton; }}

        /// <summary>Gets the 'Home' button.</summary>
        public jQueryObject HomeButton{get { return homeButton; }}

        private bool ShowHome
        {
            get
            {
                ITreeNode node = listTree.SelectedNode;
                if (node == null) return false;
                if (node.IsRoot) return false;
                if (node.TotalChildren == 0 && node.Parent.IsRoot) return false;
                return true;
            }
        }
        #endregion

        #region Internal
        private void UpdateHomeButton()
        {
            int duration = Helper.Number.ToMsecs(listTree.SlideDuration);
            bool isVisible = Css.IsVisible(homeButton);
            if (ShowHome)
            {
                if (!isVisible) homeButton.FadeIn(duration);
            }
            else
            {
                if (isVisible) homeButton.FadeOut(duration);
            }
        }
        #endregion
    }
}
