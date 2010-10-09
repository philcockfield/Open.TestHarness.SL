using Open.Core.Controls.Buttons;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public static class WriteLogForButtons
    {
        public static void WriteButtonModel(IButton button)
        {
            Log.Info("IsEnabled: " + button.IsEnabled);
            Log.Info("IsPressed: " + button.IsPressed);
            Log.Info("CanToggle: " + button.CanToggle);
            Log.Info("CanFocus: " + button.CanFocus);
        }

        public static void WriteButtonView(IButtonView button)
        {
            Log.Info("MouseState: " + button.State.ToString());
            Log.Info("IsMouseOver: " + button.IsMouseOver);
            Log.Info("IsMouseDown: " + button.IsMouseDown);
        }
    }
}
