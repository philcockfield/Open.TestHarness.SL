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
            textbox.UpdateLayout();

            textbox.Width = 250;
            textbox.Height = 40;

            // Wire up events.
            textbox.TextChanged += delegate { Log.Event("TextChanged | Text: " + textbox.Text); };
            textbox.TextChangedDelay += delegate { Log.Event("TextChangedDelay | Text: " + textbox.Text); };
            textbox.EnterPress += delegate { Log.Event("EnterPress"); };

            // Finish up.
            Change_Text();
            Toggle__LeftIcon();
        }
        #endregion

        #region Tests
        public void Toggle__IsEnabled()
        {
            textbox.IsEnabled = !textbox.IsEnabled;
            Log.Info("IsEnabled: " + textbox.IsEnabled);
        }

        public void Toggle_SelectOnFocus()
        {
            textbox.SelectOnFocus = !textbox.SelectOnFocus;
            Log.Info("SelectOnFocus: " + textbox.SelectOnFocus);
        }

        public void Change_Text()
        {
            textbox.Text = "My Value";
        }

        public void Assign_Focus()
        {
            DelayedAction.Invoke(0.5, delegate { textbox.Focus.Apply(); });
        }

        public void Toggle__LeftIcon()
        {
            textbox.LeftIcon = !textbox.HasLeftIcon ? Helper.Icon.Path("SilkAccept") : string.Empty;
            textbox.Padding.Left = textbox.HasLeftIcon ? 5 : 10;
        }

        public void Toggle__CornerRadius()
        {
            textbox.CornerRadius = textbox.CornerRadius == 5 ? 10 : 5;
        }

        public void UpdateLayout()
        {
            textbox.UpdateLayout();
        }

        public void Write_Properties()
        {
            Log.WriteProperties(textbox);
            //Log.Info("IsEnabled: " + textbox.IsEnabled);
            //Log.Info("Text: " + textbox.Text);
            //Log.Info("EventDelay: " + textbox.EventDelay);
            //Log.Info("Padding: " + textbox.Padding.ToString());
            //Log.Info("LeftIconSrc: " + textbox.LeftIcon);
            //Log.Info("LeftIconMargin: " + textbox.LeftIconMargin.ToString());
            //Log.Info("SelectOnFocus: " + textbox.SelectOnFocus);
            //Log.Info("CornerRadius: " + textbox.CornerRadius);
            //Log.Info("Width: " + textbox.Width);
            //Log.Info("Height: " + textbox.Height);
        }
        #endregion
    }
}
