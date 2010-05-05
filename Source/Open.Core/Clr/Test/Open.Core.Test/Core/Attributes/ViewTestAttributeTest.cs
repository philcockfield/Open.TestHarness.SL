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
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Attributes
{
    [TestClass]
    public class ViewTestAttributeTest
    {

        [TestMethod]
        public void ShouldGetReflectionDataFromMock()
        {
            var mock = new Mock();
            mock.MethodInfo1.ShouldBeInstanceOfType<MethodInfo>();
            mock.MethodInfo2.ShouldBeInstanceOfType<MethodInfo>();

            mock.ViewTestAttribute1.ShouldBeInstanceOfType<ViewTestAttribute>();
            mock.ViewTestAttribute2.ShouldBeInstanceOfType<ViewTestAttribute>();
        }

        [TestMethod]
        public void ShouldBeVisibleByDefault()
        {
            var mock = new Mock();
            mock.ViewTestAttribute1.IsVisible.ShouldBe(true);
            mock.ViewTestAttribute2.IsVisible.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldAllowAutoRunByDefault()
        {
            var mock = new Mock();
            mock.ViewTestAttribute1.AllowAutoRun.ShouldBe(true);
            mock.ViewTestAttribute2.AllowAutoRun.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldBeManualSizeByDefault()
        {
            var mock = new Mock();
            mock.ViewTestAttribute1.SizeMode.ShouldBe(TestControlSize.Manual);
        }



        [ViewTestClass]
        private class Mock
        {
            public Mock()
            {
                MethodInfo1 = GetType().GetMethod("Method1");
                MethodInfo2 = GetType().GetMethod("Method2");
                ViewTestAttribute1 = MethodInfo1.GetCustomAttributes(typeof(ViewTestAttribute), true).FirstOrDefault() as ViewTestAttribute;
                ViewTestAttribute2 = MethodInfo2.GetCustomAttributes(typeof(ViewTestAttribute), true).FirstOrDefault() as ViewTestAttribute;
            }

            public MethodInfo MethodInfo1 { get; private set; }
            public MethodInfo MethodInfo2 { get; private set; }

            public ViewTestAttribute ViewTestAttribute1 { get; private set; }
            public ViewTestAttribute ViewTestAttribute2 { get; private set; }

            [ViewTest]
            public void Method1(Border control){}

            [ViewTest(AllowAutoRun = false, IsVisible = false)]
            public void Method2(Border control){}
        }
    }
}
