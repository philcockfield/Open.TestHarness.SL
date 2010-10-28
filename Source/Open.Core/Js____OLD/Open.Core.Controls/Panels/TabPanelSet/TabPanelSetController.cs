using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Controls
{
    internal class TabPanelSetController : ControllerBase
    {
        #region Head

        private readonly TabPanelSet model;
        private readonly TabPanelSetView view;
        private jQueryObject panelsContainer;
        private jQueryObject toolbarElement;
        private readonly ArrayList buttonViews = new ArrayList();


        /// <summary>Constructor.</summary>
        public TabPanelSetController(TabPanelSet model, TabPanelSetView view)
        {
            // Setup initial conditions.
            this.model = model;
            this.view = view;

            // Wire up events.
            model.PanelAdded += OnPanelAdded;
            model.Disposed += delegate { Dispose(); };
            view.Disposed += delegate { Dispose(); };
            GlobalEvents.WindowResizeComplete += OnWindowResizeComplete;

            // Initialize when view is loaded.
            if (view.IsLoaded)
            {
                Initialize();
            }
            else
            {
                view.Loaded += delegate { Initialize(); };
            }
        }

        private void Initialize()
        {
            // Retrieve elements.
            toolbarElement = view.Container.Find(".c_tabPanelSet .toolbar");
            panelsContainer = view.Container.Find(".c_tabPanelSet .container");

            // Load pre-existing tabs from the model.
            foreach (TabPanel tabPanel in model)
            {
                AddPanel(tabPanel);
                tabPanel.Button.IsPressed = false;
            }

            // Finish up.
            TabPanel first = model.FirstPanel;
            if (first != null) first.Select();
        }

        protected override void OnDisposed()
        {
            model.PanelAdded -= OnPanelAdded;
            GlobalEvents.WindowResizeComplete -= OnWindowResizeComplete;
            view.Dispose();
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnWindowResizeComplete(object sender, EventArgs e)
        {
            UpdateButtonPositions();
        }

        private void OnPanelAdded(object sender, TabPanelEventArgs e)
        {
            AddPanel(e.Panel);
        }

        private void OnIsPressedChanged(object sender, EventArgs e)
        {
            // Setup initial conditions.
            IButton buttonModel = sender as IButton;
            
            // Ensure the at least one button is selected.
            if (!buttonModel.IsPressed && !model.HasSelection)
            {
                buttonModel.IsPressed = true;
                return;
            }

            // Don't condinue if the button was not selected.
            if (!buttonModel.IsPressed) return;

            // Update the UI.
            TabPanelButton buttonView = TabButtonFromModel(buttonModel);
            Select(buttonView);
        }

        private void HandlePanelDisposed(object sender, EventArgs e)
        {
            // Setup initial conditions.
            TabPanel panel = sender as TabPanel;
            TabPanelButton tab = TabButtonFromModel(panel.Button);

            // Unwire events.
            WirePanelEvents(false, panel, tab);

            // Remove the button.
            tab.Container.Remove();
            tab.Dispose();

            // Remove the div.
            panel.Div.Remove();

            // Finish up.
            buttonViews.Remove(tab);
            UpdateButtonPositions();
        }

        private void HandlePanelVisibilityChanged(object sender, EventArgs e)
        {
            UpdateButtonPositions();
        }
        #endregion

        #region Internal
        private TabPanelButton TabButtonFromModel(IButton model)
        {
            foreach (TabPanelButton view in buttonViews)
            {
                if (view.Model == model) return view;
            }
            return null;
        }

        private void AddPanel(TabPanel panel)
        {
            // Setup initial conditions.
            if (!view.IsLoaded) return;

            // Add the button.
            AddButton(panel);

            // Add the DIV.
            Css.SetVisibility(panel.Div, false);
            panelsContainer.Append(panel.Div);
        }

        private void AddButton(TabPanel panel)
        {
            // Create button.
            TabPanelButton tab = new TabPanelButton(panel);
            toolbarElement.Append(tab.Container);

            // Wire up events.
            WirePanelEvents(true, panel, tab);

            // Finish up.
            buttonViews.Add(tab);
            UpdateButtonPositions();
        }

        private static void SetLeft(IView tab, int left)
        {
            tab.Container.CSS(Css.Left, left + Css.Px);
        }

        private void Select(TabPanelButton tab)
        {
            HideAll(tab);
            Show(tab);
        }

        private void HideAll(TabPanelButton exclude)
        {
            foreach (TabPanelButton tab in buttonViews)
            {
                if (tab != exclude) Hide(tab);
            }
        }

        private static void Hide(TabPanelButton tab)
        {
            tab.Model.IsPressed = false;
            Css.SetVisibility(tab.Panel.Div, false);
        }

        private static void Show(TabPanelButton tab)
        {
            tab.Model.IsPressed = true;
            Css.SetVisibility(tab.Panel.Div, true);
        }

        private int GetButtonOffset(TabPanelButton tab)
        {
            int offset = 0;
            foreach (TabPanelButton item in buttonViews)
            {
                if (tab == item) return offset;
                if (item.IsVisible) offset += item.Container.GetWidth();
            }
            return offset;
        }

        private void UpdateButtonPositions()
        {
            // Setup initial conditions.
            bool isOverflowing = false;

            // Enumerate each tab.
            foreach (TabPanelButton tab in buttonViews)
            {
                if (tab.IsVisible)
                {
                    // Set the tabs position.
                    int left = GetButtonOffset(tab);
                    SetLeft(tab, left);

                    // Determine if the tab is overflowing the bounds of the control.
                    bool isTabOverflowing = IsOverflowing(tab, left);
                    tab.Panel.IsOverflowing = isTabOverflowing;
                    if (isTabOverflowing) isOverflowing = true;
                }
            }

            // Finish up.
            view.IsOverflowing = isOverflowing;
        }

        private bool IsOverflowing(TabPanelButton tab, int left)
        {
            return (left + tab.Width) > (view.Container.GetWidth() - 20);
        }

        private void WirePanelEvents(bool add, TabPanel panel, TabPanelButton tab)
        {
            if (add)
            {
                panel.Disposed += HandlePanelDisposed;
                panel.VisibilityChanged += HandlePanelVisibilityChanged;
                tab.Model.IsPressedChanged += OnIsPressedChanged;
            }
            else
            {
                panel.Disposed -= HandlePanelDisposed;
                panel.VisibilityChanged -= HandlePanelVisibilityChanged;
                tab.Model.IsPressedChanged -= OnIsPressedChanged;
            }
        }
        #endregion
    }
}