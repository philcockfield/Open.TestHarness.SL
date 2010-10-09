using System;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public class Button_LibraryTest
    {
        #region Tests
        public void Show__SampleButton()
        {
            MyButtonView.DownloadContent(delegate
                                        {
                                            MyButtonView button = new MyButtonView();
                                            button.Text = "Sample Button";

                                            TestHarness.AddControl(button);
                                            ButtonTest.WireClickEvents(button.Model);
                                            button.UpdateLayout();
                                        });
        }

        public void Clear_All() { TestHarness.Reset(); }
        #endregion
    }
}
