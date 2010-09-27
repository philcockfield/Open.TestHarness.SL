using System;
using System.Collections;
using jQueryApi;
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
            view.Background = "orange";
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
        public void Toggle__IsVisible()
        {
            view.IsVisible = !view.IsVisible;
        }

        public void Toggle__Background()
        {
            view.Background = view.Background == "orange" ? "red" : "orange";
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
            view.CanFocus = !view.CanFocus;
            Write_Properties();
        }

        public void Focus() { Log.Info("Focus: " + view.Focus()); }
        public void Blur() { Log.Info("Blur: " + view.Blur()); }

        public void Write_Properties()
        {
            Log.Info("CanFocus: " + view.CanFocus);
            Log.Info("TabIndex: " + view.TabIndex);
            Log.Info("IsFocused: " + view.IsFocused);
        }
        #endregion
    }
}
