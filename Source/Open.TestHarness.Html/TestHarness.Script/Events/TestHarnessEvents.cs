// TestHarnessEvents.cs
//

using System;
using System.Collections;
using Open.Testing.Internal;

namespace Open.Testing
{
    public class TestHarnessEvents : ITestHarnessEvents
    {
        /// <summary>Fires when a test class is registered.</summary>
        public event TestClassHandler TestClassRegistered;
        public void FireTestClassRegistered(TestClassEventArgs e) { if (TestClassRegistered != null) TestClassRegistered(this, e); }

        /// <summary>Fires when a control is added to the host canvas.</summary>
        public event TestControlHandler ControlAdded;
        public void FireControlAdded(TestControlEventArgs e) { if (ControlAdded != null) ControlAdded(this, e); }

        /// <summary>Fires when the host canvas is to be cleared of controls.</summary>
        public event EventHandler ClearControls;
        public void FireClearControls() { if (ClearControls != null) ClearControls(this, new EventArgs()); }
    }
}
