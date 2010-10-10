using System;
using Open.Core.Controls.Buttons;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public class ImageButtonTest
    {
        #region Head
        private ImageButton button;

        public void ClassInitialize()
        {
            // Setup initial conditions.
            button = new ImageButton();
            ResetUrls();
            TestHarness.AddControl(button.CreateView() as IView);

            // Wire up events.
            ButtonTest.WireClickEvents(button);
        }
        #endregion

        #region Tests
        public void Toggle_Model__IsEnabled()
        {
            button.IsEnabled = !button.IsEnabled;
            Log.Info("IsEnabled: " + button.IsEnabled);
        }

        public void Toggle__CanToggle()
        {
            button.CanToggle = !button.CanToggle;
            Log.Info("CanToggle: " + button.CanToggle);
        }

        public void Toggle__IsPressed()
        {
            button.IsPressed = !button.IsPressed;
            Log.Info("IsPressed: " + button.IsPressed);
        }

        public void Toggle__BackgroundHighlighting()
        {
            button.BackgroundHighlighting = !button.BackgroundHighlighting;
            Log.Info("BackgroundHighlighting: " + button.BackgroundHighlighting);
        }

        public void Remove__Focus_Urls()
        {
            ResetUrls();
            button.FocusedUrls.Reset();
        }

        public void Remove__Disabled_Urls()
        {
            ResetUrls();
            button.DisabledUrls.Reset();
        }

        public void Normal_Over_Default_URLs_Only()
        {
            Reset();
            ImageButton mock = new ImageButton();
            InitializeSampleButton(mock);
            button.BasePath = mock.BasePath;
            button.DefaultUrls.Normal = mock.DefaultUrls.Normal;
            button.DefaultUrls.Over = mock.DefaultUrls.Over;
        }
        #endregion

        #region Internal
        private void ResetUrls()
        {
            Reset();
            InitializeSampleButton(button);
        }

        private void Reset()
        {
            button.DefaultUrls.Reset();
            button.DisabledUrls.Reset();
            button.FocusedUrls.Reset();
        }
        #endregion

        #region Methods : Static
        public static void InitializeSampleButton(ImageButton button)
        {
            // Setup initial conditions.
            button.SetSize(550, 220);
            button.BackgroundHighlighting = true;

            // Set image URLs.
            button.BasePath = "/Content/Images/ImageButton.Sample/";

            button.DefaultUrls.Normal = "Normal.png";
            button.DefaultUrls.Over = "Over.png";
            button.DefaultUrls.Down = "Down.png";
            button.DefaultUrls.Pressed = "Pressed.png";

            button.DisabledUrls.Normal = "Normal.Disabled.png";
            button.DisabledUrls.Over = "Over.Disabled.png";
            button.DisabledUrls.Down = "Down.Disabled.png";
            button.DisabledUrls.Pressed = "Pressed.Disabled.png";

            button.FocusedUrls.Normal = "Normal.Focused.png";
            button.FocusedUrls.Over = "Over.Focused.png";
            button.FocusedUrls.Down = "Down.Focused.png";
            button.FocusedUrls.Pressed = "Pressed.Focused.png";
        }
        #endregion
    }
}
