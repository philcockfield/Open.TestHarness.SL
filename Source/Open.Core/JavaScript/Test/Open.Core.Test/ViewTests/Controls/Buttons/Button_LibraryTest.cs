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
            SampleTemplatedButtonView.DownloadContent(delegate
                                        {
                                            SampleTemplatedButtonView view = new SampleTemplatedButtonView();
                                            view.Text = "Sample Button";

                                            TestHarness.AddControl(view);
                                            ButtonTest.WireClickEvents(view.Model);
                                            view.UpdateLayout();
                                        });
        }

        public void ImageButton__PlusDark() { CreateImageButton(ImageButtons.PlusDark, 24, 24); }
        public void ImageButton__PlayDark() { CreateImageButton(ImageButtons.PlayDark, 24, 24); }
        public void ImageButton__RefreshDark() { CreateImageButton(ImageButtons.RefreshDark, 24, 24); }
        public void ImageButton__SearchDark() { CreateImageButton(ImageButtons.SearchDark, 24, 24); }
        public void ImageButton__PushRemove() { CreateImageButton(ImageButtons.Remove, 24, 24); }
        public void ImageButton__PushPin() { CreateImageButton(ImageButtons.PushPin, 24, 24); }
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

        private static void AddButton(ButtonBase button)
        {
            TestHarness.AddControl(button.CreateView() as IView);
            ButtonTest.WireClickEvents(button);
        }
        #endregion
    }
}
