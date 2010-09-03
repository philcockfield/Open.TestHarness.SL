using System;

namespace Open.Core
{
    /// <summary>
    ///     Shared functionality for working with the TestHarness 
    ///     (so that test assemblies don't have to reference to TestHarness project [and corresponding dependences]).
    /// </summary>
    public static class TestHarness
    {
        #region Events
        /// <summary>Fires when a test class is registered.</summary>
        public static event TestClassHandler TestClassRegistered;
        #endregion

        #region Methods
        /// <summary>Registers a test-class with the harness.</summary>
        /// <param name="entryPoint">Type representing the test-package (normally the 'Application' class).</param>
        /// <param name="testClass">The type of the test class.</param>
        public static void RegisterTestClass(Type entryPoint, Type testClass)
        {
            if (Script.IsNullOrUndefined(entryPoint) || Script.IsNullOrUndefined(testClass)) return;
            if (TestClassRegistered != null)
            {
                // Alert the test-harness via an event.
                TestClassEventArgs e = new TestClassEventArgs();
                e.EntryPoint = entryPoint;
                e.TestClass = testClass;
                TestClassRegistered(typeof (TestHarness), e);
            }
        }
        #endregion
    }

    public delegate void TestClassHandler(object sender, TestClassEventArgs e);
    public class TestClassEventArgs
    {
        public Type EntryPoint;
        public Type TestClass;
    }
}
