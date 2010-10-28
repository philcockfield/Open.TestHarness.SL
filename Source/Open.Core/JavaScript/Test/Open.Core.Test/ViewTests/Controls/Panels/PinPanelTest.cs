using System;
using Open.Core.Controls;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Panels
{
    public class PinPanelTest
    {
        #region Head
        private PinPanel panel;

        public void ClassInitialize()
        {
            // Setup initial conditions.
            panel = new PinPanel();
            TestHarness.AddControl(panel);

            // Wire up events.
            CollapsePanelTest.LogEvents(panel);
            panel.Pinned += delegate { Log.Event("Pinned"); };
            panel.Unpinned += delegate { Log.Event("Unpinned"); };

            // Setup test styles.
            panel.Padding.Top = 24;
            panel.SetSize(200, 350);
            panel.Background = Color.Red(0.1);
            panel.Content.CSS(Css.Background, Color.Red(0.4));
            panel.Content.Append("123456789 abcdefg");
        }
        #endregion

        #region Tests
        public void Toggle__IsCollapsed() { panel.IsCollapsed = !panel.IsCollapsed; }
        public void Toggle__IsPinned() { panel.IsPinned = !panel.IsPinned; }

        public void Write_Properties()
        {
            Log.WriteProperties(panel);
        }
        #endregion
    }
}
