using System;
using System.Collections;
using Open.Core.Controls.Buttons;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Buttons
{
    public class LinkButtonTest
    {
        #region Head
        private LinkButton link;

        public void ClassInitialize()
        {
            link = new LinkButton("My Link Button");
            link.Width = 120;
            TestHarness.AddControl(link);

            link.Click += delegate { Log.Info("!! Click"); };

        }
        #endregion
    }
}
