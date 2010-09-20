using System;
using System.Runtime.CompilerServices;
using jQueryApi;
using Open.Core.Controls.Buttons;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public class ButtonBaseTest
    {
        #region Head
        private MyButton button;

        public void ClassInitialize()
        {
            button = new MyButton();
            button.Click += delegate { Log.Info("!! Click"); };
            button.IsPressedChanged += delegate { Log.Info("!! IsPressedChanged | IsPressed: " + button.IsPressed); };

            TestHarness.AddControl(button);
        }
        #endregion

        #region Methods
        public void Toggle__CanToggle()
        {
            button.CanToggle = !button.CanToggle;
            Log.Info("CanToggle: " + button.CanToggle);
        }
        #endregion
    }

    public class MyButton : ButtonBase
    {
        public MyButton()
        {
            Background = "orange";
            SetSize(120, 34);
        }

        [AlternateSignature]
        public extern void TestStateContent(ButtonState state, jQueryObject html);

        [AlternateSignature]
        public extern void TestStateContent(ButtonState state, string cssClasses);
        
        public void TestStateContent(ButtonState state, jQueryObject html, string cssClasses) { base.StateContent(state, html, cssClasses); }
    }
}
