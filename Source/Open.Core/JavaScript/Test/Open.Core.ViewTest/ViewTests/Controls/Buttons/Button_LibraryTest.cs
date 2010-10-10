using System;
using System.Runtime.CompilerServices;
using Open.Core.Controls.Buttons;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public class Button_LibraryTest
    {
        #region Head
        public void ClassInitialize()
        {
            SampleButton();
        }

        public void TestInitialize()
        {
            TestHarness.Reset();
        }
        #endregion

        #region Tests
        public void SampleButton()
        {
            MyButtonView.DownloadContent(delegate
                                        {
                                            MyButtonView view = new MyButtonView();
                                            view.Text = "Sample Button";

                                            TestHarness.AddControl(view);
                                            ButtonTest.WireClickEvents(view.Model);
                                            view.UpdateLayout();
                                        });
        }

        public void ImageButton__PlusDark(){ CreateImageButton(ImageButtons.PlusDark, 24, 24); }
        #endregion

        #region Internal
        [AlternateSignature]
        private static extern void CreateImageButton(ImageButtons type);
        private static void CreateImageButton(ImageButtons type, int width, int height)
        {
            ImageButton button = ImageButtonFactory.Create(type);
            if (!Script.IsNullOrUndefined(width))
            {
                button.SetSize(width, height);
            }
            AddButton(button);


        }

        private static void AddButton(ButtonModel button)
        {
            TestHarness.AddControl(button.CreateView() as IView);
            ButtonTest.WireClickEvents(button);
        }
        #endregion
    }
}
