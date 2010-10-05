using jQueryApi;
using Open.Testing;

namespace Open.Core.Test.UnitTests
{
    public class ViewBaseUnitTest
    {
        #region Head
        private SampleView view;

        public void TestInitialize()
        {
            view = new SampleView();
            TestHarness.AddControl(view);
        }

        public void TestCleanup()
        {
            view.Dispose();
            TestHarness.Reset();
        }
        #endregion

        #region Tests
        public void ShouldNotHaveBackground()
        {
            Should.BeNull(view.Background);
        }

        public void ShouldSetCssValue()
        {
            view.Background = "orange";
            Should.Equal(view.Background, "orange");
        }

        public void ShouldResetCssValueToNull()
        {
            view.Background = "orange";
            view.Background = "";
            Should.BeNull(view.Background);
        }

        public void ShouldFireBackgroundChangedEvent()
        {
            view.LastPropertyChanged = null;
            view.Background = "orange";
            Should.Equal(view.LastPropertyChanged.Name, ViewBase.PropBackground);
        }

        public void ShouldBeVisibleByDefault()
        {
            Should.BeTrue(view.IsVisible);
            Should.Equal(view.GetCss(Css.Display), Css.Block);
        }

        public void ShouldSetDisplayToBlockWhenIsVisible()
        {
            view.IsVisible = false;
            view.IsVisible = true;
            Should.Equal(view.GetCss(Css.Display), Css.Block);
        }

        public void ShouldSetDisplayToNoneWhenNotIsVisible()
        {
            view.IsVisible = false;
            Should.Equal(view.GetCss(Css.Display), Css.None);
        }

        public void ShouldFireVisibilityChangedEventsOnce()
        {
            int propChangedCount = 0;
            int visibilityChangedCount = 0;

            view.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args) 
                            { 
                                if (args.Property.Name == ViewBase.PropIsVisible) propChangedCount++; 
                            };
            view.IsVisibleChanged += delegate { visibilityChangedCount++; };

            view.IsVisible = false;
            view.IsVisible = false;
            view.IsVisible = false;

            Should.Equal(propChangedCount, 1);
            Should.Equal(visibilityChangedCount, 1);
        }

        public void ShouldBeFullOpacityByDefault()
        {
            Should.Equal(view.Opacity, 1);
        }

        public void ShouldBoundOpacityValue()
        {
            view.Opacity = 2;
            Should.Equal(view.Opacity, 1);

            view.Opacity = -1;
            Should.Equal(view.Opacity, 0);
        }

        public void ShouldSetOpacityCss()
        {
            view.Opacity = 0.5;
            Should.Equal(view.GetCss("opacity"), "0.5");
        }

        public void ShouldChangeWidth()
        {
            int fireCount = 0;
            view.SizeChanged += delegate { fireCount++; };
            Should.Equal(view.Width, 0);
            view.Width = 50;
            view.Width = 50;
            Should.Equal(view.Width, 50);
            Should.Equal(fireCount, 1);
        }

        public void ShouldChangeHeight()
        {
            int fireCount = 0;
            view.SizeChanged += delegate { fireCount++; };
            Should.Equal(view.Height, 0);
            view.Height = 50;
            view.Height = 50;
            Should.Equal(view.Height, 50);
            Should.Equal(fireCount, 1);
        }

        public void ShouldSetSize()
        {
            view.SetSize(80, 80);
            Should.Equal(view.Width, 80);
            Should.Equal(view.Height, 80);
        }

        public void ShouldFireSizeChangedOnce()
        {
            int fireCount = 0;
            view.SizeChanged += delegate { fireCount++; };

            view.SetSize(80, 80);
            view.SetSize(80, 80);
            Should.Equal(fireCount, 1);
        }

        public void ShouldNowAllowNegativeSizing()
        {
            view.SetSize(-1, -1);
            Should.Equal(view.Width, 0);
            Should.Equal(view.Height, 0);

            view.Width = -15;
            view.Height = -5;
            Should.Equal(view.Width, 0);
            Should.Equal(view.Height, 0);
        }

        public void ShouldBeEnabledByDefault()
        {
            Should.BeTrue(view.IsEnabled);
        }

        public void ShouldFireIsEnabledChanged()
        {
            int isEnabledChangedCount = 0;
            int propChangedCount = 0;

            view.IsEnabledChanged += delegate { isEnabledChangedCount++; };
            view.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args)
                                        {
                                            if (args.Property.Name == ViewBase.PropIsEnabled) propChangedCount++;
                                        };

            view.IsEnabled = false;
            view.IsEnabled = false;
            view.IsEnabled = false;

            Should.BeFalse(view.IsEnabled);
            Should.Equal(isEnabledChangedCount, 1);
            Should.Equal(propChangedCount, 1);

            view.IsEnabled = true;

            Should.BeTrue(view.IsEnabled);
            Should.Equal(isEnabledChangedCount, 2);
            Should.Equal(propChangedCount, 2);
        }
        #endregion
    }
}