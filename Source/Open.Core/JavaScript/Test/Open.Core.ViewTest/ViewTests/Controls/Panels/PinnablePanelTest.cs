using System;
using Open.Core.Controls;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Panels
{
    public class PinnablePanelTest
    {
        #region Head
        private PinnablePanel panel;

        public void ClassInitialize()
        {
            // Setup initial conditions.
            panel = new PinnablePanel();
            TestHarness.AddControl(panel);

            // Wire up events.
            CollapsePanelTest.LogEvents(panel);

            // Setup test styles.
            panel.SetSize(200, 350);
            panel.Background = Color.Red(0.1);
            panel.Content.CSS(Css.Background, Color.Red(0.4));
            panel.Content.Append("123456789 abcdefg");
        }
        #endregion

        #region Tests
        public void Toggle__IsCollapsed()
        {
            panel.IsCollapsed = !panel.IsCollapsed;
            Write_Properties();
        }

        public void Write_Properties()
        {
            Log.Info("foo", Icons.SilkEmoticonSmile); //TEMP 
            Log.WriteProperties(panel);
        }
        #endregion
    }
}
