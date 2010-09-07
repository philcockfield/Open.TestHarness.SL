using System;
using jQueryApi;
using Open.Core;
using Open.Testing;

namespace Test.Samples
{
    public class MyTestClass1
    {
        #region Head
        public MyTestClass1() { }

        public void ClassInitialize() { Log.Info("Class Initialize: " + GetType().Name); }
        public void ClassCleanup() { Log.Info("Class Cleanup: " + GetType().Name); }
        public void TestInitialize() { Log.Info("Test Initialize"); }
        public void TestCleanup() { Log.Info("Test Cleanup"); Log.LineBreak(); }
        #endregion

        #region Fields and Properties
        public string PublicField = "Foo";
        private string PrivateField = "Foo";

        public string PublicProperty
        {
            get { return PublicField; }
            set { PublicField = value; }
        }

        private string PrivateField1
        {
            get { return PrivateField; }
            set { PrivateField = value; }
        }
        #endregion

        #region Methods : Tests
        public void Test1()
        {
            Log.Info("MyTestClass1 : Test 1 Invoked");
        }

        public void Test_Two()
        {
            Log.Info("MyTestClass1 : Test 2 Invoked");
        }

        public void Test__Three()
        {
            Log.Info("MyTestClass1 : Test 3 Invoked");
        }

        public void Contains_Error()
        {
            throw new Exception("My error.");
        }

        private void PrivateMethod() { }
        #endregion
    }
}