using System;
using Open.Testing.Internal;
using Open.Testing.Models;

namespace Open.Testing
{
    public class TestHarnessEvents : ITestHarnessEvents
    {
        #region Events : ITestHarnessEvents
        /// <summary>Fires when a test class is registered.</summary>
        public event TestClassHandler TestClassRegistered;
        public void FireTestClassRegistered(TestClassEventArgs e) { if (TestClassRegistered != null) TestClassRegistered(this, e); }

        /// <summary>Fires when a control is added to the host canvas.</summary>
        public event TestControlHandler ControlAdded;
        public void FireControlAdded(TestControlEventArgs e) { if (ControlAdded != null) ControlAdded(this, e); }

        /// <summary>Fires when the host canvas is to be cleared of controls.</summary>
        public event EventHandler ClearControls;
        public void FireClearControls() { if (ClearControls != null) ClearControls(this, new EventArgs()); }
        #endregion

        #region Events : Methods | Classes | Packages
        /// <summary>Fires when each time a method in the list is clicked.</summary>
        public event MethodEventHandler MethodClicked;
        internal void FireMethodClicked(MethodInfo methodInfo){if (MethodClicked != null) MethodClicked(this, new MethodEventArgs(methodInfo));}

        /// <summary>Fires when when the selected class changes.</summary>
        public event ClassEventHandler SelectedClassChanged;
        internal void FireSelectedClassChanged(ClassInfo classInfo) { if (SelectedClassChanged != null) SelectedClassChanged(this, new ClassEventArgs(classInfo)); }
        #endregion

        #region Methods : Display
        /// <summary>Fires when the width of the control-host changes.</summary>
        public event EventHandler ControlHostSizeChanged;
        internal void FireControlHostSizeChanged() { if (ControlHostSizeChanged != null) ControlHostSizeChanged(this, new EventArgs()); }
        #endregion
    }


    public delegate void MethodEventHandler(object sender, MethodEventArgs e);
    public class MethodEventArgs
    {
        public MethodEventArgs(MethodInfo methodInfo){MethodInfo = methodInfo;}
        public MethodInfo MethodInfo;
    }

    public delegate void ClassEventHandler(object sender, ClassEventArgs e);
    public class ClassEventArgs
    {
        public ClassEventArgs(ClassInfo classInfo) { ClassInfo = classInfo; }
        public ClassInfo ClassInfo;
    }

}
