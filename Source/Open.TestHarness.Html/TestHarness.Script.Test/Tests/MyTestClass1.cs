using Open.Core;

namespace TestHarness.Test
{
    public class MyTestClass1
    {
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

        private void PrivateMethod() { }
    }
}
