using System;
using System.Html;
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
        public void TestCleanup() { Log.Info("Test Cleanup"); }
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

        public void RemoveStart()
        {
            //Log.Info(Helper.String.RemoveStart("_one", ""));
            //Log.Info(Helper.String.RemoveStart("", "Foo"));
            //Log.Info(Helper.String.RemoveStart("_one", "_O"));
            //Log.Info(Helper.String.RemoveStart("_one", "fo"));

            Log.Info(Helper.Url.PrependDomain("/MyPath"));
            Log.Info(Helper.Url.PrependDomain("MyPath"));
        }

        public void Write_Url()
        {
            //TEMP 
            Log.Info("Hash: " + Window.Location.Hash);
            Log.Info("Hostname: " + Window.Location.Hostname);
            Log.Info("HostnameAndPort: " + Window.Location.HostnameAndPort);
            Log.Info("Href: " + Window.Location.Href);
            Log.Info("Pathname: " + Window.Location.Pathname);
            Log.Info("Port: " + Window.Location.Port);
            Log.Info("Protocol: " + Window.Location.Protocol);
            Log.Info("Search: " + Window.Location.Search);
        }

        private void PrivateMethod() { }
        #endregion
    }
}