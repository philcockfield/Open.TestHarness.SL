using System;
using Open.Testing.Internal;
using Open.Testing.Models;

namespace Open.Testing
{
    public class TestHarnessEvents : ITestHarnessEvents
    {
        #region Events : ITestHarnessEvents
        public event TestClassHandler TestClassRegistered;
        public void FireTestClassRegistered(TestClassEventArgs e) { if (TestClassRegistered != null) TestClassRegistered(this, e); }

        public event TestControlHandler ControlAdded;
        public void FireControlAdded(TestControlEventArgs e) { if (ControlAdded != null) ControlAdded(this, e); }

        public event EventHandler ClearControls;
        public void FireClearControls() { if (ClearControls != null) ClearControls(this, new EventArgs()); }

        public event EventHandler UpdateLayout;
        public void FireUpdateLayout() { if (UpdateLayout != null) UpdateLayout(this, new EventArgs()); }
        #endregion

        #region Events : Methods | Classes | Packages
        /// <summary>Fires when each time a method in the list is clicked.</summary>
        public event MethodEventHandler MethodClicked;
        internal void FireMethodClicked(MethodInfo methodInfo){if (MethodClicked != null) MethodClicked(this, new MethodEventArgs(methodInfo));}

        /// <summary>Fires when when the selected class changes.</summary>
        public event ClassEventHandler SelectedClassChanged;
        internal void FireSelectedClassChanged(ClassInfo classInfo) { if (SelectedClassChanged != null) SelectedClassChanged(this, new ClassEventArgs(classInfo)); }

        /// <summary>Fires when a new package is to be added.</summary>
        public event PackageEventHandler AddPackage;
        internal void FireAddPackage(PackageInfo packageInfo) { if (AddPackage != null) AddPackage(this, new PackageEventArgs(packageInfo)); }
        #endregion

        #region Methods : Display
        /// <summary>Fires when the width of the control-host changes.</summary>
        internal event EventHandler ControlHostSizeChanged;
        internal void FireControlHostSizeChanged() { if (ControlHostSizeChanged != null) ControlHostSizeChanged(this, new EventArgs()); }

        /// <summary>Fires when the log-height is to be changed.</summary>
        internal event ChangeHeightEventHandler ChangeLogHeight;
        internal void FireChangeLogHeight(int height) { if (ChangeLogHeight != null) ChangeLogHeight(this, new ChangeHeightEventArgs(height)); }
        #endregion
    }

    public delegate void MethodEventHandler(object sender, MethodEventArgs e);
    public class MethodEventArgs
    {
        public MethodEventArgs(MethodInfo methodInfo){MethodInfo = methodInfo;}
        public MethodInfo MethodInfo;
    }

    public delegate void PackageEventHandler(object sender, PackageEventArgs e);
    public class PackageEventArgs 
    {
        public PackageEventArgs(PackageInfo packageInfo) { PackageInfo = packageInfo; }
        public PackageInfo PackageInfo;
    }

    public delegate void ClassEventHandler(object sender, ClassEventArgs e);
    public class ClassEventArgs
    {
        public ClassEventArgs(ClassInfo classInfo) { ClassInfo = classInfo; }
        public ClassInfo ClassInfo;
    }

    internal  delegate void ChangeHeightEventHandler(object sender, ChangeHeightEventArgs e);
    internal class ChangeHeightEventArgs
    {
        public ChangeHeightEventArgs(int height) { Height = height; }
        public int Height;
    }


}
