using System;
using jQueryApi;
using Open.Core;
using Open.Testing;

namespace Test.Samples
{
    public class ConstructorParams
    {
        public ConstructorParams(string myParam)
        {
        }

        public void Test1(string param1, int param2)
        {
            Log.Title("Test1");
            Log.Info("param1: " + param1);
            Log.Info("param2: " + param2);
            Log.LineBreak();
        }
        public void Test2() { }
        public void Test3() { }
        public void Test4() { }
    }
}