using Open.Core.Controls.Buttons;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public class SystemButtonTest
    {
        #region Head
        private SystemButton button;

        public void ClassInitialize()
        {
            // Setup initial conditions.
            button = new SystemButton();
            TestHarness.AddControl(button);

            // Wire up events.
            button.Click += delegate { Log.Info("!! Click | IsPressed: " + button.IsPressed); };
            button.IsPressedChanged += delegate { Log.Info("!! IsPressedChanged | IsPressed: " + button.IsPressed); };
            button.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args)
                                          {
//                                              Log.Info("!! PropertyChanged: " + args.Property.Name + " | MouseState: " + button.MouseState.ToString());
                                          };
        }
        public void ClassCleanup() { }

        public void TestInitialize() { }
        public void TestCleanup() { }
        #endregion

        #region Methods
        public void Toggle_IsEnabled() { button.IsEnabled = !button.IsEnabled; }

        public void Change_Html()
        {
            button.HtmlContent = button.HtmlContent == SystemButton.Untitled ? "My <b>Button</b>" : SystemButton.Untitled;
        }

        public void Change_Padding()
        {
            button.Padding = button.Padding == null ? SystemButton.DefaultPadding : null;
        }
        
        public void Change_FontSize()
        {
            button.FontSize = button.FontSize == "40pt" ? "8pt" : "40pt";
        }

        public void InvokeClick__Force() { button.InvokeClick(true); }
        public void InvokeClick__Not_Forced() { button.InvokeClick(false); }

        public void CanToggle_True__Error() { button.CanToggle = true; }

        public void Write_Properties() { LogButtons.WriteSystemButton(button); }
        #endregion
    }
}