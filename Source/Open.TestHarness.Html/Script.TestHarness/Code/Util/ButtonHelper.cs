using System;
using Open.Core;
using Open.Core.Controls.Buttons;

namespace Open.Testing
{
    internal  static class ButtonHelper
    {
        public static void InsertButton(ImageButtons type, string replaceSeletor, int size, EventHandler onClick)
        {
            // Create the buttons.
            ImageButton button = ImageButtonFactory.Create(type);
            button.BackgroundHighlighting = true;
            button.SetSize(size, size);

            // Setup CSS.
            ButtonView view = button.CreateView() as ButtonView;

            // Wire up events.
            button.Click += onClick;

            // Insert the button.
            view.Insert(replaceSeletor, InsertMode.Replace);
            view.UpdateLayout();
        }
    }
}
