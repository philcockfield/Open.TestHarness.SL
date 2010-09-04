using System;
using System.Collections;
using Open;
using Open.Core;

namespace Test
{
    public class Application
    {
        public static void Main(Dictionary args)
        {
            Type testPackage = typeof (Application);

            Testing.RegisterClass(testPackage, typeof(MyTestClass1));
            Testing.RegisterClass(testPackage, typeof(MyTestClass1));
            Testing.RegisterClass(testPackage, typeof(MyTestClass2));
        }
    }
}
