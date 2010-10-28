using jQueryApi;
using Open.Core.Controls;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Panels
{
    public class CollapsePanelTest
    {
        #region Head
        private CollapsePanel panel;

        public void ClassInitialize()
        {
            // Setup initial conditions.
            panel = new CollapsePanel();
            TestHarness.AddControl(panel);

            // Initialize panel settings.
            panel.Padding.Change(10);

            // Wire up events.
            LogEvents(panel);

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

        public void Toggle__IsInflated()
        {
            panel.IsInflated = !panel.IsInflated;
            Write_Properties();
        }

        public void Plane__Horizontal() { panel.Plane = Plane.Horizontal; Write_Properties(); }
        public void Plane__Vertical() { panel.Plane = Plane.Vertical; Write_Properties(); }

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
            panel.Inflate(delegate { Log.Info("CALLBACK - Inflate"); }, 350);
            Write_Properties();
        }

        public void Toggle__Padding()
        {
            int padding = panel.Padding.Left == 0 ? 10 : 0;
            panel.Padding.Change(padding);
            Log.Info("Padding" + panel.Padding.ToString());
        }

        public void Toggle__Speed()
        {
            panel.Slide.Duration = (panel.Slide.Duration == AnimationSettings.DefaultDuration)
                                            ? 2
                                            : AnimationSettings.DefaultDuration;
            Log.Info("Slide: " + panel.Slide.ToString());
        }

        public void Toggle__Size()
        {
            panel.Width = panel.Width >= 200 ? 60 : 200;
            TestHarness.UpdateLayout();
        }

        public void Write_Properties()
        {
            Log.WriteProperties(panel);
            //Log.Info("Plane: " + panel.Plane.ToString());
            //Log.Info("IsCollapsed: " + panel.IsCollapsed);
            //Log.Info("IsInflated: " + panel.IsInflated);
            //Log.Info("IsCollapsing: " + panel.IsCollapsing);
            //Log.Info("IsInflating: " + panel.IsInflating);
            //Log.Info("Slide (Settings): " + panel.Slide.ToString());
            //Log.Info("Padding: " + panel.Padding.ToString());
        }
        #endregion

        #region Methods
        public static void LogEvents(CollapsePanel panel)
        {
            panel.Inflating += delegate { Log.Event("Inflating"); };
            panel.Inflated += delegate { Log.Event("Inflated"); };
            panel.Collapsing += delegate { Log.Event("Collapsing"); };
            panel.Collapsed += delegate { Log.Event("Collapsed"); };
        }
        #endregion
    }
}
