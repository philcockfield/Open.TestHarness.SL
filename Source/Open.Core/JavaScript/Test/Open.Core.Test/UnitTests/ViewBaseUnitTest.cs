using jQueryApi;
using Open.Core.Test.Samples;
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
            Assert.That(view.Background).IsNull();
        }

        public void ShouldSetCssValue()
        {
            view.Background = "orange";
            Assert.That(view.Background).Is("orange");
        }

        public void ShouldResetCssValueToNull()
        {
            view.Background = "orange";
            view.Background = "";
            Assert.That(view.Background).IsNull();
        }

        public void ShouldFireBackgroundChangedEvent()
        {
            view.LastPropertyChanged = null;
            view.Background = "orange";
            Assert.That(view.LastPropertyChanged.Name).Is(ViewBase.PropBackground);
        }

        public void ShouldBeVisibleByDefault()
        {
            Assert.That(view.IsVisible).IsTrue();
            Assert.That(view.GetCss(Css.Display)).Is(Css.Block);
        }

        public void ShouldSetDisplayToBlockWhenIsVisible()
        {
            view.IsVisible = false;
            view.IsVisible = true;
            Assert.That(view.GetCss(Css.Display)).Is(Css.Block);
        }

        public void ShouldSetDisplayToNoneWhenNotIsVisible()
        {
            view.IsVisible = false;
            Assert.That(view.GetCss(Css.Display)).Is(Css.None);
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

            Assert.That(propChangedCount).Is(1);
            Assert.That(visibilityChangedCount).Is(1);
        }

        public void ShouldBeFullOpacityByDefault()
        {
            Assert.That(view.Opacity).Is(1);
        }

        public void ShouldBoundOpacityValue()
        {
            view.Opacity = 2;
            Assert.That(view.Opacity).Is(1);

            view.Opacity = -1;
            Assert.That(view.Opacity).Is(0);
        }

        public void ShouldSetOpacityCss()
        {
            view.Opacity = 0.5;
            Assert.That(view.GetCss("opacity")).Is("0.5");
        }

        public void ShouldChangeWidth()
        {
            int fireCount = 0;
            view.SizeChanged += delegate { fireCount++; };
            Assert.That(view.Width).Is(0);
            view.Width = 50;
            view.Width = 50;
            Assert.That(view.Width).Is(50);
            Assert.That(fireCount).Is(1);
        }

        public void ShouldChangeHeight()
        {
            int fireCount = 0;
            view.SizeChanged += delegate { fireCount++; };
            Assert.That(view.Height).Is(0);
            view.Height = 50;
            view.Height = 50;
            Assert.That(view.Height).Is(50);
            Assert.That(fireCount).Is(1);
        }

        public void ShouldSetSize()
        {
            view.SetSize(80, 80);
            Assert.That(view.Width).Is(80);
            Assert.That(view.Height).Is(80);
        }

        public void ShouldFireSizeChangedOnce()
        {
            int fireCount = 0;
            view.SizeChanged += delegate { fireCount++; };

            view.SetSize(80, 80);
            view.SetSize(80, 80);
            Assert.That(fireCount).Is(1);
        }

        public void ShouldNowAllowNegativeSizing()
        {
            view.SetSize(-1, -1);
            Assert.That(view.Width).Is(0);
            Assert.That(view.Height).Is(0);

            view.Width = -15;
            view.Height = -5;
            Assert.That(view.Width).Is(0);
            Assert.That(view.Height).Is(0);
        }

        public void ShouldBeEnabledByDefault()
        {
            Assert.That(view.IsEnabled).IsTrue();
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

            Assert.That(view.IsEnabled).IsFalse();
            Assert.That(isEnabledChangedCount).Is(1);
            Assert.That(propChangedCount).Is(1);

            view.IsEnabled = true;

            Assert.That(view.IsEnabled).IsTrue();
            Assert.That(isEnabledChangedCount).Is(2);
            Assert.That(propChangedCount).Is(2);
        }
        #endregion
    }
}