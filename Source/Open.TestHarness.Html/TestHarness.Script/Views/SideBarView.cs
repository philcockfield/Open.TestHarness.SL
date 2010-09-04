using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Lists;
using Open.TestHarness.Models;

namespace Open.TestHarness.Views
{
    /// <summary>The view for the side-bar.</summary>
    public class SidebarView : ViewBase
    {
        #region Head
        public const double SlideDuration = 0.2; // secs.
        public const string PropIsTestListVisible = "IsTestListVisible";

        private readonly ListTreeView rootList;
        private readonly ListTreeBackController backController;
        private readonly TestListView testList;

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
            testList = new TestListView(jQuery.Select(CssSelectors.TestList));

            // Create controllers.
            backController = new ListTreeBackController(
                rootList,
                jQuery.Select(CssSelectors.SidebarToolbar),
                jQuery.Select(CssSelectors.BackMask));

            // Wire up events.
            rootList.SelectedParentChanged += delegate { SyncTestListVisibility(); };

            // Finish up.
            UpdateVisualState();


            //TEMP ==============
            RootList.Container.Click(delegate(jQueryEvent @event)
                                         {
//                                             TestList.TEMP();
                                             //IsTestListVisible = !IsTestListVisible;
                                         });
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
        public TestListView TestList{get { return testList; }}

        /// <summary>Gets or sets whether the TestList panel is visible.</summary>
        public bool IsTestListVisible
        {
            get { return (bool) Get(PropIsTestListVisible, false); }
            set
            {
                if (Set(PropIsTestListVisible, value, false))
                {
                    if (value) { ShowTestList(null); } else { HideTestList(null); }
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
        public void ShowTestList(Action onComplete)
        {
            // Setup initial conditions.
            IsTestListVisible = true;

            // Prepare properties.
            int height = GetTargetTestListHeight();
            AnimateHeights(height, onComplete);
        }

        /// <summary>Hides the test list.</summary>
        /// <param name="onComplete">The action to invoke when complete</param>
        public void HideTestList(Action onComplete)
        {
            IsTestListVisible = false;
            AnimateHeights(0, onComplete);
        }
        #endregion

        #region Internal
        private void AnimateHeights(int testListHeight, Action onComplete)
        {
            // Prepare properties
            Dictionary testListProps = new Dictionary();
            testListProps[Css.Height] = testListHeight;

            Dictionary rootListProps = new Dictionary();
            rootListProps[Css.Bottom] = testListHeight;

            //Animate.
            Animate(TestList.Container, testListProps, null);
            Animate(RootList.Container, rootListProps, onComplete);
        }

        private static void Animate(jQueryObject div, Dictionary properties, Action onComplete)
        {
            div.Animate(
                    properties,
                    Helper.Number.ToMsecs(SlideDuration),
                    EffectEasing.Swing,
                                    delegate
                                    {
                                        Helper.InvokeOrDefault(onComplete);
                                    });
        }

        private void SyncRootListHeight()
        {
            RootList.Container.CSS(Css.Bottom, TestList.Container.GetHeight() + Css.Px);
        }

        private void SyncTestListVisibility()
        {
            // Show or hide the TestList based on the kind of root-tree-node that is currently selected.
            object node = rootList.SelectedParent;
            IsTestListVisible = node != null && (node is TestPackageListItem);
        }

        private int GetTargetTestListHeight()
        {
            return 250; // TODO
        }
        #endregion
    }
}
