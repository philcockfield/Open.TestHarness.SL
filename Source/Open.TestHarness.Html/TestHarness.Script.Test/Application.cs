using System.Collections;
using Open;
using Testing;

namespace Test
{
    public class Application
    {
        public static void Main(Dictionary args)
        {
            TestHarness.RegisterClass(typeof(MyTestClass1));
            TestHarness.RegisterClass(typeof(MyTestClass1));
            TestHarness.RegisterClass(typeof(MyTest_Class__2));
            TestHarness.RegisterClass(typeof(LoadHelperTest));
        }
    }
}
