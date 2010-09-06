using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Testing.Views;

namespace Open.Testing.Controllers
{
    /// <summary>Controls the height of the MethodList.</summary>
    internal class MethodListHeightController : TestHarnessControllerBase
    {
        #region Head
        private readonly SidebarView sidebarView;
        private readonly MethodListView methodList;
        private readonly jQueryObject divSidebarContent;
        private readonly TestHarnessEvents events;

        /// <summary>Constructor.</summary>
        /// <param name="sidebarView">The sidebar.</param>
        public MethodListHeightController(SidebarView sidebarView)
        {
            // Setup initial conditions.
            this.sidebarView = sidebarView;
            methodList = sidebarView.MethodList;
            divSidebarContent = sidebarView.Container.Children(CssSelectors.SidebarContent);
            events = Common.Events;

            // Wire up events.
            events.SelectedClassChanged += OnSelectedClassChanged;

            // Finish up.
            HideMethodList(null);
        }

        protected override void OnDisposed()
        {
            events.SelectedClassChanged -= OnSelectedClassChanged;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnSelectedClassChanged(object sender, ClassEventArgs e)
        {
            if (e.ClassInfo != null) { ShowMethodList(null); }
            else { HideMethodList(null); }
        }
        #endregion

        #region Methods
        /// <summary>Reveals the method-list.</summary>
        /// <param name="onComplete">The action to invoke when complete</param>
        private void ShowMethodList(Action onComplete)
        {
            // Setup initial conditions.
            sidebarView.IsMethodListVisible = true;

            // Prepare properties.
            int height = GetHeight();
            AnimateHeights(height, onComplete);
        }

        /// <summary>Hides the method-list.</summary>
        /// <param name="onComplete">The action to invoke when complete</param>
        private void HideMethodList(Action onComplete)
        {
            sidebarView.IsMethodListVisible = false;
            AnimateHeights(0, onComplete);
        }

        /// <summary>Updates the height of the method-list (if it's currently showing).</summary>
        public void UpdateLayout()
        {
            if (!sidebarView.IsMethodListVisible) return;
            methodList.Container.CSS(Css.Height, GetHeight() + Css.Px);
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
            if (isShowing) Css.SetVisible(methodList.Container, true);
            methodList.UpdateLayout();

            //Animate.
            Animate(isShowing, methodList.Container, methodListProps, null);
            Animate(isShowing, sidebarView.RootList.Container, rootListProps, onComplete);
        }

        private void Animate(bool isShowing, jQueryObject div, Dictionary properties, Action onComplete)
        {
            div.Animate(
                    properties,
                    Helper.Number.ToMsecs(SidebarView.SlideDuration),
                    EffectEasing.Swing,
                    delegate
                            {
                                if (!isShowing) Css.SetVisible(methodList.Container, false);
                                Helper.InvokeOrDefault(onComplete);
                            });
        }

        private int GetHeight()
        {
            // Setup initial conditions.
            jQueryObject divList = methodList.Container;

            // Get the final height of the list.
            //     NB: The list needs to made visible before calculating so that height values are registered.
            bool originalVisibility = Css.IsVisible(divList);
            Css.SetVisible(divList, true);
            int listHeight = methodList.OffsetHeight;
            Css.SetVisible(divList, originalVisibility);

            // Ensure the height is not over 2/3 of side-bar.
            int maxHeight = (int)(divSidebarContent.GetHeight() * 0.66);
            if (listHeight > maxHeight) listHeight = maxHeight;

            // Finish up.
            return listHeight;
        }
        #endregion
    }
}
