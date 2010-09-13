using System.Collections;
using Open.Testing;
using Test.Samples;

namespace Test
{
    public class Application
    {
        public static void Main(Dictionary args)
        {
            TestHarness.RegisterClass(typeof(MyTestClass1));
            TestHarness.RegisterClass(typeof(MyTestClass1)); // Registered twice.
            TestHarness.RegisterClass(typeof(MyTest_Class__2));
            TestHarness.RegisterClass(typeof(Canvas_Control_Insertion_Test));
            TestHarness.RegisterClass(typeof(ConstructorParams));
            TestHarness.RegisterClass(typeof(CssTest));
        }
    }
}
