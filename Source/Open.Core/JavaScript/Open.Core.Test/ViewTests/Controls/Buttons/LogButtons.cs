using Open.Core.Controls.Buttons;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public static class LogButtons
    {
        public static void WriteButtonModel(IButton button)
        {
            Log.Info("IsEnabled: " + button.IsEnabled);
            Log.Info("CanToggle: " + button.CanToggle);
            Log.Info("IsPressed: " + button.IsPressed);
        }

        public static void WriteButtonView(IButtonView button)
        {
            Log.Info("MouseState: " + button.State.ToString());
            Log.Info("IsMouseOver: " + button.IsMouseOver);
            Log.Info("IsMouseDown: " + button.IsMouseDown);
        }

        //TEMP 
        //public static void WriteSystemButton(SystemButton button)
        //{
        //    Log.Info("Html: " + button.HtmlContent);
        //    Log.Info("Type: " + button.Type);
        //    Log.Info("Value: " + button.Value);
        //    Log.Info("Padding: " + button.Padding);
        //    Log.Info("FontSize: " + button.FontSize);
        //    WriteIButton(button);
        //}
    }
}
