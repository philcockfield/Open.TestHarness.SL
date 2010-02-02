//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using System;

namespace Open.Core.Common.Test.Core.Common.Testing
{
    [TestClass]
    public class ReflectionTestingExtensionsTest
    {
        #region Head
        [TestInitialize]
        public void TestSetup()
        {
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldHaveValuesForDifferentPropertyTypes()
        {
            var stub = Stub1.CreatePopulated();

            stub.ShouldHaveValuesForAllProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            stub.ShouldHaveValuesForAllProperties();
            stub.ShouldHaveValuesForAllProperties(BindingFlags.Public);
            stub.ShouldHaveValuesForAllProperties(BindingFlags.NonPublic);
            stub.ShouldHaveValuesForAllProperties(BindingFlags.Static);
            stub.ShouldHaveValuesForAllProperties(BindingFlags.Instance);
        }

        [TestMethod]
        public void ShouldThrowWhenNoValuesForDifferentPropertyTypes()
        {
            var stub = Stub1.CreatePopulated();

            stub.PublicInstanceString = null;
            Should.Throw<AssertionException>(() => stub.ShouldHaveValuesForAllProperties());

            stub = Stub1.CreatePopulated();
            Stub1.PublicStaticString = null;
            Should.Throw<AssertionException>(() => stub.ShouldHaveValuesForAllProperties());

            stub = Stub1.CreatePopulated();
            stub.SetPrivateProperties(null, "value");
            Should.Throw<AssertionException>(() => stub.ShouldHaveValuesForAllProperties());

            stub = Stub1.CreatePopulated();
            stub.SetPrivateProperties("value", null);
            Should.Throw<AssertionException>(() => stub.ShouldHaveValuesForAllProperties());
        }

        [TestMethod]
        public void ShouldHaveValuesForSpecificTypeOnly()
        {
            var stub = new Stub2{Stub = new Stub1()};
            stub.Text.ShouldBe(null);
            stub.ShouldHaveValuesForAllProperties<Stub1>();
        }

        [TestMethod]
        public void ShouldNotHaveValuesForSpecificTypeOnly()
        {
            var stub = new Stub2();
            stub.Text.ShouldBe(null);
            Should.Throw<AssertionException>(() => stub.ShouldHaveValuesForAllProperties<Stub1>());
        }

        [TestMethod]
        public void ShouldThrowIfSelfNotPassed()
        {
            Should.Throw<ArgumentNullException>(() => ReflectionTestingExtensions.ShouldHaveValuesForAllProperties(null));
        }
        #endregion

        #region Stubs
        public class Stub1
        {
            public string PublicInstanceString { get; set; }
            public static string PublicStaticString { get; set; }
            public string WriteOnly { set { } } // Edge case - this should never really happen.
            private string PrivateInstanceString { get; set; }
            private static string PrivateStaticString { get; set; }

            public void SetPrivateProperties(string instanceValue, string staticValue)
            {
                PrivateInstanceString = instanceValue;
                PrivateStaticString = staticValue;
            }

            public static Stub1 CreatePopulated()
            {
                var value = new Stub1
                                {
                                    PublicInstanceString = "value",
                                    PrivateInstanceString = "value",
                                };
                PublicStaticString = "value";
                PrivateStaticString = "value";
                return value;
            }
        }

        public class Stub2
        {
            public string Text { get; set; }
            public Stub1 Stub { get; set; }
        }
        #endregion
    }
}
