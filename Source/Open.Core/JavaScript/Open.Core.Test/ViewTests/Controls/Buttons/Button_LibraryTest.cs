using System;
using System.Collections;
using Open.Core.Controls.Buttons;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public class Button_LibraryTest
    {
        #region Tests
        public void Show__SampleButton()
        {
            SampleButton button = new SampleButton();
            TestHarness.AddControl(button);
            WireUp(button);
        }
        #endregion

        #region Internal
        private static void WireUp(IButtonView button)
        {
            IButton model = button.Model;
            model.Click += delegate { Log.Info("!! Click"); };
            model.IsPressedChanged += delegate
                                    {
                                        if (model.IsPressed)
                                        {
                                            Log.Warning("!! IsPressedChanged | IsPressed: " + model.IsPressed);
                                        }
                                        else
                                        {
                                            Log.Success("!! IsPressedChanged | IsPressed: " + model.IsPressed);
                                        }
                                    };
        }
        #endregion
    }
}
