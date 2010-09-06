using System;

namespace Open.Testing.Internal
{
    public interface ITestHarnessEvents
    {
        /// <summary>Fires when a test class is registered.</summary>
        event TestClassHandler TestClassRegistered;
        void FireTestClassRegistered(TestClassEventArgs e);

        /// <summary>Fires when a control is added to the host canvas.</summary>
        event TestControlHandler ControlAdded;
        void FireControlAdded(TestControlEventArgs e);

        /// <summary>Fires when the host canvas is to be cleared of controls.</summary>
        event EventHandler ClearControls;
        void FireClearControls();
    }
}