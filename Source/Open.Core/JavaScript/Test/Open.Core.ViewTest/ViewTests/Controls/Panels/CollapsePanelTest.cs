using System;
using Open.Core.Controls;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Panels
{
    public class CollapsePanelTest
    {
        #region Head
        private CollapsePanel panel;
        private IView view;

        public void ClassInitialize()
        {
            // Setup initial conditions.
            panel = new CollapsePanel();
            view = TestHarness.AddModel(panel);

            // Wire up events.
            panel.Inflating += delegate { Log.Warning("!! Inflating"); };
            panel.Inflated += delegate { Log.Warning("!! Inflated"); };
            panel.Collapsing += delegate { Log.Warning("!! Collapsing"); };
            panel.Collapsed += delegate { Log.Warning("!! Collapsed"); };

            // Finish up.
            view.Background = Color.Red(0.1);
            view.SetSize(200, 450);
        }
        #endregion

        #region Tests
        public void Toggle__IsCollapsed()
        {
            panel.IsCollapsed = !panel.IsCollapsed;
            Write_Properties();
        }

        public void Toggle__IsInflated()
        {
            panel.IsInflated = !panel.IsInflated;
            Write_Properties();
        }

        public void Collapse()
        {
            panel.Collapse(delegate { Log.Info("CALLBACK - Collapse"); }); 
            Write_Properties();
        }

        public void Inflate_to_Narrow()
        {
            panel.Inflate(delegate { Log.Info("CALLBACK - Inflate"); }, 200); 
            Write_Properties();
        }

        public void Inflate_to_Wide()
        {
            panel.Inflate(delegate { Log.Info("CALLBACK - Inflate"); }, 370);
            Write_Properties();
        }

        public void Write_Properties()
        {
            Log.Info("IsCollapsed: " + panel.IsCollapsed);
            Log.Info("IsInflated: " + panel.IsInflated);
            Log.Info("IsCollapsing: " + panel.IsCollapsing);
            Log.Info("IsInflating: " + panel.IsInflating);
            Log.Info("Slide (Settings): " + panel.Slide.ToString());
        }
        #endregion
    }
}
