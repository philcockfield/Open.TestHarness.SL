using System;
using Open.Core.Controls;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Input
{
    public class TextboxTest
    {
        #region Head
        private Textbox textbox;

        public void ClassInitialize()
        {
            // Setup initial conditions.
            textbox = new Textbox();
            TestHarness.AddControl(textbox);

            textbox.Width = 250;

            // Wire up events.
            textbox.TextChanged += delegate { Log.Info("!! TextChanged | Text: " + textbox.Text); };
            textbox.TextChangedDelay += delegate { Log.Success("!! TextChangedDelay | Text: " + textbox.Text); };
        }
        #endregion

        #region Tests
        public void Change_Text()
        {
            textbox.Text = "My Value";
        }

        public void Assign_Focus()
        {
            DelayedAction.Invoke(0.5, delegate { textbox.Focus.Apply(); });
        }

        public void Change_Padding()
        {
            textbox.Padding.Left = 30;
            textbox.Padding.Top = 1;
            textbox.Padding.Bottom = 5;
            textbox.Padding.Right = 50;
        }

        public void Toggle__LeftIcon()
        {
            textbox.LeftIcon = !textbox.HasLeftIcon ? Helper.Icon.Path("SilkAccept") : string.Empty;
            textbox.Padding.Left = textbox.HasLeftIcon ? 5 : 10;
        }

        public void Write_Properties()
        {
            Log.Info("Text: " + textbox.Text);
            Log.Info("EventDelay: " + textbox.EventDelay);
            Log.Info("Padding: " + textbox.Padding.ToString());
            Log.Info("LeftIconSrc: " + textbox.LeftIcon);
            Log.Info("LeftIconMargin: " + textbox.LeftIconMargin.ToString());
        }
        #endregion
    }
}
