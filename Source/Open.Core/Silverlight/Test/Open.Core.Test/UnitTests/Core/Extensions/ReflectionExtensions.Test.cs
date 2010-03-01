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
using System.Reflection;
using Open.Core.Common.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Open.Core.Common.Test.Extensions
{
    [TestClass]
    public class ReflectionExtensionsTest
    {
        [TestMethod]
        public void ShouldGetResourceDictionaryFromAssembly()
        {
            var assembly = GetType().Assembly;

            var dictionary = assembly.GetResourceDictionary("UnitTests/Core/Extensions/ResourceTest.xaml");
            dictionary["testStyle"].ShouldNotBe(null);

            dictionary = assembly.GetResourceDictionary("/UnitTests/Core/Extensions/ResourceTest.xaml");
            dictionary["testStyle"].ShouldNotBe(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void ShouldFailToGetResourceDictionaryFromAssembly()
        {
            var assembly = GetType().Assembly;
            var dictionary = assembly.GetResourceDictionary("PathNotExist/Dictionary.xaml");
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
    }
}
