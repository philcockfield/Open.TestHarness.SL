using System;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>A controller for attaching a 'Back' button to a ListTree.</summary>
    public class ListTreeBackController : ControllerBase
    {
        #region Head
        private readonly ListTreeView listTree;
        private readonly jQueryObject backButton;
        private readonly jQueryObject backMask;

        /// <summary>Constructor.</summary>
        /// <param name="listTree">The list tree under control.</param>
        /// <param name="backButton">The back button.</param>
        /// <param name="backMask">The mask which causes the back-button to look like a back button.</param>
        public ListTreeBackController(ListTreeView listTree, jQueryObject backButton, jQueryObject backMask)
        {
            // Store values.
            this.listTree = listTree;
            this.backButton = backButton;
            this.backMask = backMask;

            // Wire up events.
            listTree.PropertyChanged += OnPropertyChanged;

            backButton.Click(OnBackClick);
            backButton.DoubleClick(OnBackDoubleClick);

            backMask.Click(OnBackClick);
            backMask.DoubleClick(OnBackDoubleClick);
        }
        #endregion

        #region Event Handlers
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.Property.Name == ListTreeView.PropCurrentListRoot) FadeBackMask();
        }

        private void OnBackClick(jQueryEvent e) { listTree.Back(); }
        private void OnBackDoubleClick(jQueryEvent e) { listTree.SelectedNode = listTree.RootNode; } // DblClick => Home.
        #endregion

        #region Properties
        /// <summary>Gets the list-tree which is under control.</summary>
        public ListTreeView ListTree{get { return listTree; }}

        /// <summary>Gets the 'Back' button.</summary>
        public jQueryObject BackButton{get { return backButton; }}

        /// <summary>Gets the 'Back Mask'.</summary>
        public jQueryObject BackMask{get { return backMask; }}

        private bool ShowBackMask
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
        private void FadeBackMask()
        {
            int duration = Helper.Number.ToMsecs(listTree.SlideDuration);
            bool isVisible = Css.IsVisible(backMask);
            if (ShowBackMask)
            {
                if (!isVisible) backMask.FadeIn(duration);
            }
            else
            {
                if (isVisible) backMask.FadeOut(duration);
            }
        }
        #endregion
    }
}
