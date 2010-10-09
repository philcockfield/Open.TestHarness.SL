using System;
using Open.Core.Controls.Buttons;
using Open.Core.Test.Samples;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public class ImageButtonTest
    {
        #region Head
        private SampleImageButton button;

        public void ClassInitialize()
        {
            // Setup initial conditions.
            button = new SampleImageButton();
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
        #endregion
    }
}
