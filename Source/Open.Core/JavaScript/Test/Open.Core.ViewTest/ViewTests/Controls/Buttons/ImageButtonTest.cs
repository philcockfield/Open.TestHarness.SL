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
            button = new SampleImageButton();
            TestHarness.AddControl(button.CreateView() as IView);
        }
        #endregion

        #region Tests
        #endregion
    }
}
