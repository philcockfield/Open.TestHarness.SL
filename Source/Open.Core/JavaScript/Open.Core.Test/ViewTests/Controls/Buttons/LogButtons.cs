using Open.Core.Controls.Buttons;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public static class LogButtons
    {
        public static void WriteIButton(IButton button)
        {
            Log.Info("IsEnabled: " + button.IsEnabled);
            Log.Info("CanToggle: " + button.CanToggle);
            Log.Info("MouseState: " + button.MouseState.ToString());
            Log.Info("IsPressed: " + button.IsPressed);
            Log.Info("IsMouseOver: " + button.IsMouseOver);
            Log.Info("IsMouseDown: " + button.IsMouseDown);
        }

        public static void WriteSystemButton(SystemButton button)
        {
            Log.Info("Html: " + button.Html);
            Log.Info("Type: " + button.Type);
            Log.Info("Value: " + button.Value);
            Log.Info("Padding: " + button.Padding);
            Log.Info("FontSize: " + button.FontSize);
            WriteIButton(button);
        }
    }
}
