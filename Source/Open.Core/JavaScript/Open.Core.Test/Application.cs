using System.Collections;
using Open.Core.Test.UnitTests;
using Open.Core.Test.ViewTests.Controls;
using Open.Core.Test.ViewTests.Core;
using Open.Core.Test.ViewTests.Lists;
using Open.Testing;

namespace Open.Core.Test
{
    public class Application
    {
        public static void Main(Dictionary args)
        {
            // Unit Tests.
            TestHarness.RegisterClass(typeof(ScriptLoadHelperTest));
            TestHarness.RegisterClass(typeof(DiContainerTest));
            TestHarness.RegisterClass(typeof(ModelBaseUnitTest));
            TestHarness.RegisterClass(typeof(ViewUnitTest));

            // View Tests
            TestHarness.RegisterClass(typeof(ViewBaseTest));
            TestHarness.RegisterClass(typeof(ListTreeTest));
            TestHarness.RegisterClass(typeof(ListItemViewTest));
            TestHarness.RegisterClass(typeof(LogTest));
        }
    }
}
