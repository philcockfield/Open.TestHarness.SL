using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Lists;
using Open.Testing.Models;

namespace Open.Testing.Views
{
    /// <summary>The view for the side-bar.</summary>
    public class SidebarView : ViewBase
    {
        #region Head
        public const double SlideDuration = 0.2; // secs.
        public const string PropIsTestListVisible = "IsTestListVisible";

        private readonly ListTreeView rootList;
        private readonly ListTreeBackController backController;
        private readonly MethodListView methodList;

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing DIV.</param>
        public SidebarView(jQueryObject container)
        {
            // Setup initial conditions.
            Initialize(container);

            // Create the list-tree.
            rootList = new ListTreeView(jQuery.Select(CssSelectors.SidebarRootList));
            rootList.SlideDuration = SlideDuration;

            // Create the test-list.
            methodList = new MethodListView(jQuery.Select(CssSelectors.TestList));

            // Create controllers.
            backController = new ListTreeBackController(
                rootList,
                jQuery.Select(CssSelectors.SidebarToolbar),
                jQuery.Select(CssSelectors.BackMask));

            // Finish up.
            UpdateVisualState();
        }

        protected override void OnDisposed()
        {
            backController.Dispose();
            rootList.Dispose();
            base.OnDisposed();
        }
        #endregion

        #region Properties
        /// <summary>Gets the main List-Tree view.</summary>
        public ListTreeView RootList { get { return rootList; } }

        /// <summary>Gets the Test-List view.</summary>
        public MethodListView MethodList{get { return methodList; }}

        /// <summary>Gets or sets whether the TestList panel is visible.</summary>
        public bool IsMethodListVisible
        {
            get { return (bool) Get(PropIsTestListVisible, false); }
            set
            {
                if (Set(PropIsTestListVisible, value, false))
                {
                    if (value) { ShowMethodList(null); } else { HideMethodList(null); }
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>Refreshes the visual state.</summary>
        public void UpdateVisualState()
        {
            SyncRootListHeight();
        }

        /// <summary>Reveals the test list.</summary>
        /// <param name="onComplete">The action to invoke when complete</param>
        public void ShowMethodList(Action onComplete)
        {
            // Setup initial conditions.
            IsMethodListVisible = true;

            // Prepare properties.
            int height = GetTargetMethodListHeight();
            AnimateHeights(height, onComplete);
        }

        /// <summary>Hides the test list.</summary>
        /// <param name="onComplete">The action to invoke when complete</param>
        public void HideMethodList(Action onComplete)
        {
            IsMethodListVisible = false;
            AnimateHeights(0, onComplete);
        }
        #endregion

        #region Internal
        private void AnimateHeights(int methodListHeight, Action onComplete)
        {
            // Prepare properties.
            Dictionary methodListProps = new Dictionary();
            methodListProps[Css.Height] = methodListHeight;

            Dictionary rootListProps = new Dictionary();
            rootListProps[Css.Bottom] = methodListHeight;

            // Show or hide.
            bool isShowing = methodListHeight > 0;
            if (isShowing) Css.SetVisible(MethodList.Container, true);
            MethodList.UpdateLayout();

            //Animate.
            Animate(isShowing, MethodList.Container, methodListProps, null);
            Animate(isShowing, RootList.Container, rootListProps, onComplete);
        }

        private void Animate(bool isShowing, jQueryObject div, Dictionary properties, Action onComplete)
        {
            div.Animate(
                    properties,
                    Helper.Number.ToMsecs(SlideDuration),
                    EffectEasing.Swing,
                                    delegate
                                    {
                                        if (!isShowing) Css.SetVisible(MethodList.Container, false);
                                        Helper.InvokeOrDefault(onComplete);
                                    });
        }

        private void SyncRootListHeight()
        {
            RootList.Container.CSS(Css.Bottom, MethodList.Container.GetHeight() + Css.Px);
        }

        private int GetTargetMethodListHeight()
        {
            return 250; // TODO - GetTargetTestListHeight
        }
        #endregion
    }
}
