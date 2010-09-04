using System;
using System.Runtime.CompilerServices;
using Open.TestHarness;

namespace Open
{
    /// <summary>
    ///     Shared functionality for working with the TestHarness 
    ///     (so that test assemblies don't have to reference to TestHarness project [and corresponding dependences]).
    /// </summary>
    public static class Testing
    {
        #region Methods
        /// <summary>Registers a test-class with the harness.</summary>
        /// <param name="entryPoint">Type representing the test-package (normally the 'Application' class).</param>
        /// <param name="testClass">The type of the test class.</param>
        public static void RegisterClass(Type entryPoint, Type testClass)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(entryPoint) || Script.IsNullOrUndefined(testClass)) return;

            // Alert the test-harness via an event.
            TestClassEventArgs e = new TestClassEventArgs();
            e.EntryPoint = entryPoint;
            e.TestClass = testClass;
            TestHarnessEvents.FireTestClassRegistered(e);
        }
        #endregion
    }
}

namespace Open.TestHarness
{
    public delegate void TestClassHandler(object sender, TestClassEventArgs e);
    public class TestClassEventArgs
    {
        public Type EntryPoint;
        public Type TestClass;
    }

    public static class TestHarnessEvents
    {
        /// <summary>Fires when a test class is registered.</summary>
        public static event TestClassHandler TestClassRegistered;
        internal static void FireTestClassRegistered(TestClassEventArgs e) { if (TestClassRegistered != null) TestClassRegistered(typeof(TestHarnessEvents), e); }
    }
}