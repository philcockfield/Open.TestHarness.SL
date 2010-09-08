using System.Collections;
using Open.Core.Test.UnitTests;
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

            // View Tests
            TestHarness.RegisterClass(typeof(ListTreeTest));
            TestHarness.RegisterClass(typeof(ViewUnitTest));
            TestHarness.RegisterClass(typeof(ViewBaseTest));
        }
    }
}
