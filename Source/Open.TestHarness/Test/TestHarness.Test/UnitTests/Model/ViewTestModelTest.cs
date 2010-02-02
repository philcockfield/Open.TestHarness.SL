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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.TestHarness.Model;

namespace Open.TestHarness.Test.Model
{
    [TestClass]
    public class ViewTestModelTest
    {
        #region Tests
        [TestMethod]
        public void ShouldRetrieveMethodsFromClass()
        {
            var list = ViewTest.GetMethods(typeof(SampleTestClass));
            Assert.AreEqual(3, list.Count);

            var method1 = list[0];
            Assert.IsNotNull(method1.MethodInfo);
        }
        #endregion

        #region Sample Data

        private class SampleTestClass
        {
            [Core.Common.ViewTest]
            public void Method1()
            {
            }

            [Core.Common.ViewTest]
            public void Method2(Border border)
            {
            }

            public void Method3(Border border)
            {
            }

            [Core.Common.ViewTest]
            public static void StaticMethod(Border border)
            {
            }

            [ViewTest]
            private void PrivateMethod(Border border)
            {
            }
        }
        #endregion
    }
}
