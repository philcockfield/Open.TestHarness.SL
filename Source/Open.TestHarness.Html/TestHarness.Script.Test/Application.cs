using System.Collections;
using Open;

namespace Test
{
    public class Application
    {
        public static void Main(Dictionary args)
        {
            Testing.RegisterClass(typeof(MyTestClass1));
            Testing.RegisterClass(typeof(MyTestClass1));
            Testing.RegisterClass(typeof(MyTest_Class__2));
            Testing.RegisterClass(typeof(LoadHelperTest));
        }
    }
}
