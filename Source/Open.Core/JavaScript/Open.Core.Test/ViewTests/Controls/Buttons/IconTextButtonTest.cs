using System;
using Open.Core.Controls.Buttons;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public class IconTextButtonTest
    {
        #region Head
        private IconTextButton button;

        public void ClassInitialize()
        {
            button = new IconTextButton();
            button.Text = "My Button";

            TestHarness.AddControl(button.CreateView() as IView);
        }
        #endregion

        #region Tests
        public void Write_Properties()
        {
            Log.Info("Text: " + button.Text);
        }
        #endregion
    }
}
