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
        public void Change_Text()
        {
            button.Text = button.Text == "My Button" ? "Foo" : "MyButton";
            button.UpdateLayout();
        }

        public void UpdateLayout()
        {
            button.UpdateLayout();
        }

        public void Write_Properties()
        {
            Log.WriteProperties(button);
        }
        #endregion
    }
}
