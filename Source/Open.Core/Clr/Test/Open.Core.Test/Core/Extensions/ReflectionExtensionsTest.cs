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

using System;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Open.Core.Common.Test.Extensions
{
    [TestClass]
    public class ReflectionExtensionsTest
    {
        #region Tests
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void ShouldFailToGetResourceDictionaryFromAssembly()
        {
            var assembly = GetType().Assembly;
            var dictionary = assembly.GetResourceDictionary("PathNotExist/Dictionary.xaml");
        }


        [TestMethod]
        public void ShouldDetermineIfATypeIsNumeric()
        {
            ((object)(null)).IsNumeric().ShouldBe(false);

            1.IsNumeric().ShouldBe(true);
            1.5.IsNumeric().ShouldBe(true);
            1d.IsNumeric().ShouldBe(true);

            Int16 int16 = 16;
            Int32 int32 = 32;
            Int64 int64 = 64;

            UInt16 uInt16 = 16;
            UInt32 uInt32 = 32;
            UInt64 uInt64 = 64;

            Single singleNumber = 6;
            double doubleNumber = 1.33;
            var decimalNumber = new decimal(1.663);

            int16.IsNumeric().ShouldBe(true);
            int32.IsNumeric().ShouldBe(true);
            int64.IsNumeric().ShouldBe(true);

            uInt16.IsNumeric().ShouldBe(true);
            uInt32.IsNumeric().ShouldBe(true);
            uInt64.IsNumeric().ShouldBe(true);

            singleNumber.IsNumeric().ShouldBe(true);
            doubleNumber.IsNumeric().ShouldBe(true);
            decimalNumber.IsNumeric().ShouldBe(true);

            // -----

            DateTime.Now.IsNumeric().ShouldBe(false);
            "value".IsNumeric().ShouldBe(false);
            new Exception().IsNumeric().ShouldBe(false);
        }

        [TestMethod]
        public void ShouldDetermineIfPropertyInfoIsStaticOrNot()
        {
            var instanceProp = typeof(Stub).GetProperty("Instance");
            var staticProp = typeof(Stub).GetProperty("Static");

            var privateInstanceProp = typeof(Stub).GetProperty("PrivateInstance", BindingFlags.NonPublic | BindingFlags.Instance);
            var privateStaticProp = typeof(Stub).GetProperty("PrivateStatic", BindingFlags.NonPublic | BindingFlags.Static);

            instanceProp.ShouldNotBe(null);
            staticProp.ShouldNotBe(null);
            privateInstanceProp.ShouldNotBe(null);
            privateStaticProp.ShouldNotBe(null);
            
            // ---

            instanceProp.IsStatic().ShouldBe(false);
            staticProp.IsStatic().ShouldBe(true);

            privateInstanceProp.IsStatic().ShouldBe(false);
            privateStaticProp.IsStatic().ShouldBe(true);

            // ---

            Should.Throw<ArgumentNullException>(() => ((PropertyInfo)null).IsStatic());
        }

        public enum MyEnum
        {
            One,
            Two,
            Three
        }

        [TestMethod]
        public void ShouldGetValuesOfEnum()
        {
            var values = typeof(MyEnum).GetEnumValues();
            values.Length.ShouldBe(3);

            values.ShouldContain(MyEnum.One);
            values.ShouldContain(MyEnum.Two);
            values.ShouldContain(MyEnum.Three);

            Should.Throw<ArgumentException>(() => typeof(string).GetEnumValues());
        }

        [TestMethod]
        public void ShouldGetNextEnumValue()
        {
            var value = MyEnum.One;
            value.NextValue<MyEnum>().ShouldBe(MyEnum.Two);

            value = MyEnum.Two;
            value.NextValue<MyEnum>().ShouldBe(MyEnum.Three);

            value = MyEnum.Three;
            value.NextValue<MyEnum>().ShouldBe(MyEnum.One);
        }

        [TestMethod]
        public void ShouldGetType()
        {
            (default(Visibility)).GetTypeOrNull().ShouldBe(typeof(Visibility)); // Enum
            (default(Color)).GetTypeOrNull().ShouldBe(typeof(Color)); // Struct
            "string".GetTypeOrNull().ShouldBe(typeof(string));
            this.GetTypeOrNull().ShouldBe(this.GetType());
        }

        [TestMethod]
        public void ShouldNotGetType()
        {
            ((string)null).GetTypeOrNull().ShouldBe(null);
        }
        #endregion

        #region Stubs
        public class Stub
        {
            public string Instance { get; set; }
            public static string Static { get; set; }

            private string PrivateInstance { get; set; }
            private static string PrivateStatic { get; set; }
        }
        #endregion
    }
}
