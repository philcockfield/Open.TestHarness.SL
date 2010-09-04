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
            Testing.RegisterClass(typeof(MyTestClass1));
            Testing.RegisterClass(typeof(MyTestClass1));
            Testing.RegisterClass(typeof(MyTest_Class__2));
        }
    }
}
