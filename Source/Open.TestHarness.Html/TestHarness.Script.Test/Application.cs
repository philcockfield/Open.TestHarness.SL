using System;
using System.Collections;
using Open.Core;
using TH = Open.Core.TestHarness;

namespace TestHarness.Test
{
    public class Application
    {
        public static void Main(Dictionary args)
        {
            Type testPackage = typeof (Application);
            TH.RegisterTestClass(testPackage, typeof(MyTestClass1));
            TH.RegisterTestClass(testPackage, typeof(MyTestClass1));
            TH.RegisterTestClass(testPackage, typeof(MyTestClass2));
        }
    }
}
