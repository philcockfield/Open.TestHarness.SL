using System;
using System.Collections;
using jQueryApi;
using Open.Core.Test.Samples;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Core
{
    public class ViewBaseTest
    {
        #region Head
        private SampleView view;

        public void ClassInitialize()
        {
            view = new SampleView();
            view.Background = Color.HotPink;
            view.SetSize(150, 100);

            TestHarness.AddControl(view);

            view.SizeChanged += delegate { Log.Info("!! SizeChanged - Width: " + view.Width + ", Height: " + view.Height); };
            view.GotFocus += delegate { view.Text = "GotFocus"; };
            view.LostFocus += delegate { view.Text = "LostFocus"; };
//            view.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args) { Log.Info("!! PropertyChanged: " + args.Property.Name + " | IsFocused: " + view.IsFocused); };
        }
        public void ClassCleanup() { view.Dispose(); }

        public void TestInitialize() { }
        public void TestCleanup() { }
        #endregion

        #region Tests
        public void Toggle__IsEnabled()
        {
            view.IsEnabled = !view.IsEnabled;
            Log.Info("IsEnabled: " + view.IsEnabled);
        }

        public void Toggle__IsVisible()
        {
            view.IsVisible = !view.IsVisible;
            Log.Info("IsVisible: " + view.IsVisible);
        }

        public void Toggle__Background()
        {
            view.Background = view.Background == "orange" ? "red" : "orange";
            Log.Info("Background: " + view.Background);
        }

        public void Toggle__BrowserHighlighting()
        {
            view.Focus.BrowserHighlighting = !view.Focus.BrowserHighlighting;
            Log.Info("BrowserHighlighting: " + view.Focus.BrowserHighlighting);
        }

        public void Toggle__Opacity()
        {
            view.Opacity = view.Opacity == 1 ? 0.3 : 1;
            Log.Info("Opacity: " + view.Opacity);
        }

        public void Change__Size()
        {
            if (view.Width == 150)
            {
                view.SetSize(400, 300);
            }
            else
            {
                view.SetSize(150, 100);
            }
        }

        public void Toggle__CanFocus()
        {
            view.Focus.CanFocus = !view.Focus.CanFocus;
            view.Text = null;
            Write_Properties();
        }

        public void Focus() { Log.Info("Focus: " + view.Focus.Apply()); }
        public void Blur() { Log.Info("Blur: " + view.Focus.Blur()); }

        public void Position__None() { view.Position = CssPosition.None; }
        public void Position__Relative() { view.Position = CssPosition.Relative; }
        public void Position__Absolute() { view.Position = CssPosition.Absolute; }
        public void Position__Fixed() { view.Position = CssPosition.Fixed; }

        public void Write_Properties()
        {
            Log.Info("CanFocus: " + view.Focus.CanFocus);
            Log.Info("TabIndex: " + view.Focus.TabIndex);
            Log.Info("IsFocused: " + view.Focus.IsFocused);
            Log.Info("Position: " + view.Position.ToString());
        }
        #endregion
    }
}
