using System;
using System.Collections;

namespace Open.Core.Controls.Buttons
{
    public class SampleButton : ButtonView
    {
        public SampleButton()
        {
            SetSize(24, 24);
            Background = Color.Orange;
            Css.RoundedCorners(this.Container, 5);
        }
    }
}
