using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Open.Core.Controls
{
    /// <summary>Represents a set of tab panels (mutually exclusive panel views).</summary>
    public class TabPanelSet : ModelBase, IViewFactory, IEnumerable
    {
        #region Events
        /// <summary>Fires when a new panel has been added.</summary>
        public event TabPanelEventHandler PanelAdded;
        private void FirePanelAdded(TabPanel panel)
        {
            if (PanelAdded != null) PanelAdded(this, new TabPanelEventArgs(panel));
        }
        #endregion

        #region Head
        private readonly ArrayList panels = new ArrayList();
        #endregion

        #region Properties
        /// <summary>Gets the total number of panels.</summary>
        public int Count { get { return panels.Count; } }

        /// <summary>Gets the collection of panels that have been added.</summary>
        public IEnumerable Panels { get { return panels; } }

        /// <summary>Gets the first panel in the collection.</summary>
        public TabPanel FirstPanel
        {
            get { return Count == 0 ? null : panels[0] as TabPanel; }
        }

        /// <summary>Gets the last panel in the collection.</summary>
        public TabPanel LastPanel
        {
            get { return Count == 0 ? null : panels[Count - 1] as TabPanel; }
        }

        /// <summary>Gets the first panel that is visible..</summary>
        public TabPanel FirstVisiblePanel
        {
            get
            {
                foreach (TabPanel panel in Panels)
                {
                    if (panel.IsVisible) return panel;
                }
                return null;
            }
        }

        /// <summary>Gets whether there are any tabs selected.</summary>
        public bool HasSelection { get { return SelectedPanel != null; } }

        /// <summary>Gets the selected panel.</summary>
        public TabPanel SelectedPanel
        {
            get
            {
                foreach (TabPanel panel in Panels)
                {
                    if (panel.IsVisible && panel.IsSelected) return panel;
                }
                return null;
            }
        }
        #endregion

        #region Methods
        public IView CreateView()
        {
            TabPanelSetView view = new TabPanelSetView(this);
            new TabPanelSetController(this, view);
            return view;
        }

        public IEnumerator GetEnumerator()
        {
            return panels.GetEnumerator();
        }

        /// <summary>Adds a new panel to the set.</summary>
        /// <param name="title">The title of the panel (shown in the tab button).</param>
        [AlternateSignature]
        extern public TabPanel AddPanel(string title);

        /// <summary>Adds a new panel to the set.</summary>
        /// <param name="title">The title of the panel (shown in the tab button).</param>
        /// <param name="buttonWidth">The pixel width of the button.</param>
        public TabPanel AddPanel(string title, int buttonWidth)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(buttonWidth)) buttonWidth = 100;
            TabPanel panel = new TabPanel(title, buttonWidth);

            // Wire up events.
            panel.Disposed += delegate
                                  {
                                      panels.Remove(panel);
                                      EnsureSelection();
                                  };
            panel.VisibilityChanged += delegate { EnsureSelection(); };

            // Insert the panel.
            panels.Add(panel);

            // Finish up.
            FirePanelAdded(panel);
            EnsureSelection();
            return panel;
        }
        #endregion

        #region Internal
        private void EnsureSelection()
        {
            if (Count == 0) return;
            if (HasSelection) return;
            TabPanel first = FirstVisiblePanel;
            if (first != null) first.Select();
        }
        #endregion
    }

    /// <summary>Handler for tab-panel related events.</summary>
    /// <param name="sender">The object firing the event.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void TabPanelEventHandler(object sender, TabPanelEventArgs e);

    /// <summary>Event arguments for a tab-panel related event.</summary>
    public class TabPanelEventArgs
    {
        public TabPanelEventArgs(TabPanel panel) { Panel = panel; }
        public readonly TabPanel Panel;
    }
}

