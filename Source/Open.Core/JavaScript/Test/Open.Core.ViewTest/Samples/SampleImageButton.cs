using System;
using Open.Core.Controls.Buttons;

namespace Open.Core.Test.Samples
{
    public class SampleImageButton : ImageButton
    {
        public SampleImageButton()
        {
            // Setup initial conditions.
            SetSize(550, 220);

            // Set image URLs.
            BasePath = "/Content/Images/ImageButton.Sample/";

            DefaultUrls.Normal = "Normal.png";
            DefaultUrls.Over = "Over.png";
            DefaultUrls.Down = "Down.png";
            DefaultUrls.Pressed = "Pressed.png";

            DisabledUrls.Normal = "Normal.Disabled.png";
            DisabledUrls.Over = "Over.Disabled.png";
            DisabledUrls.Down = "Down.Disabled.png";
            DisabledUrls.Pressed = "Pressed.Disabled.png";

            FocusedUrls.Normal = "Normal.Focused.png";
            FocusedUrls.Over = "Over.Focused.png";
            FocusedUrls.Down = "Down.Focused.png";
            FocusedUrls.Pressed = "Pressed.Focused.png";
        }
    }
}
