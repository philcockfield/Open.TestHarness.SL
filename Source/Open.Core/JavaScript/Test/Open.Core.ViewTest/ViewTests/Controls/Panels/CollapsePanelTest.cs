using jQueryApi;
using Open.Core.Controls;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Panels
{
    public class CollapsePanelTest
    {
        #region Head
        private CollapsePanel panel;
        private IView view;
        private readonly jQueryObject content = Html.CreateDiv();

        public void ClassInitialize()
        {
            // Setup initial conditions.
            panel = new CollapsePanel();
            view = panel.CreateView(content);
            TestHarness.AddControl(view);

            // Initialize panel settings.
            panel.Padding.Change(10);

            // Wire up events.
            panel.Inflating += delegate { Log.Warning("!! Inflating"); };
            panel.Inflated += delegate { Log.Warning("!! Inflated"); };
            panel.Collapsing += delegate { Log.Warning("!! Collapsing"); };
            panel.Collapsed += delegate { Log.Warning("!! Collapsed"); };

            // Setup test styles.
            view.SetSize(200, 450);
            view.Background = Color.Red(0.1);
            content.CSS(Css.Background, Color.Red(0.4));
            content.Append("123456789 123456789");
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
            view.Width = view.Width >= 200 ? 60 : 200;
            TestHarness.UpdateLayout();
        }

        public void Write_Properties()
        {
            Log.Info("Plane: " + panel.Plane.ToString());
            Log.Info("IsCollapsed: " + panel.IsCollapsed);
            Log.Info("IsInflated: " + panel.IsInflated);
            Log.Info("IsCollapsing: " + panel.IsCollapsing);
            Log.Info("IsInflating: " + panel.IsInflating);
            Log.Info("Slide (Settings): " + panel.Slide.ToString());
            Log.Info("Padding: " + panel.Padding.ToString());
        }
        #endregion
    }
}
