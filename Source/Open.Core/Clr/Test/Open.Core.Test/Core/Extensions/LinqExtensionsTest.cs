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
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Common.Extensions
{
    [TestClass]
    public class LinqExtensionsTest
    {
        #region Tests
        [TestMethod]
        public void ShouldGetPropertyInfo()
        {
            var propInfo = typeof (MyStub).GetProperty("MyProp");
            LinqExtensions.GetPropertyInfo<MyStub>(m => m.MyProp).ShouldEqual(propInfo);
        }

        [TestMethod]
        public void ShouldGetPropertyName()
        {
            LinqExtensions.GetPropertyName<MyStub>(m => m.MyProp).ShouldBe("MyProp");
        }

        [TestMethod]
        public void ShouldThrowWhenExpressionNotPassed()
        {
            Should.Throw<ArgumentNullException>(() => LinqExtensions.GetPropertyName<MyStub>(null));
        }

        [TestMethod]
        public void ShouldInvokeGetPropertyNameAsExtensionMethod()
        {
            var expr = GetExpression<MyStub>(m => m.MyProp);
            expr.GetPropertyName().ShouldBe("MyProp");
        }

        [TestMethod]
        public void ShouldConvertListOfLambdaExpressionsToListOfPropertyNames()
        {
            var props1 = new Expression<Func<MyStub, object>>[]
                            {
                                m => m.MyProp,
                                m => m.Text,
                            };
            props1.ToList().Count().ShouldBe(2);

            var props2 = null as Expression<Func<MyStub, object>>[];
            props2.ToList().Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldDetermineIfEventArgsMatchesProperty()
        {
            var e = new PropertyChangedEventArgs("MyProp");
            e.IsProperty<MyStub>(m => m.MyProp).ShouldBe(true);
            e.IsProperty<MyStub>(m => m.Number).ShouldBe(false);
        }


        [TestMethod]
        public void ShouldInvokeIfSpecifiedProperty()
        {
            var count = 0;
            "MyProp".InvokeIf<MyStub>(m => m.MyProp, () => count++);
            count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotInvokeIfSpecifiedProperty()
        {
            var count = 0;

            "".InvokeIf<MyStub>(m => m.MyProp, () => count++);
            count.ShouldBe(0);

            "  ".InvokeIf<MyStub>(m => m.MyProp, () => count++);
            count.ShouldBe(0);

            ((string)null).InvokeIf<MyStub>(m => m.MyProp, () => count++);
            count.ShouldBe(0);

            "NotAProperty".InvokeIf<MyStub>(m => m.MyProp, () => count++);
            count.ShouldBe(0);

            // --

            "MyProp".InvokeIf<MyStub>(m => m.MyProp, null);
            count.ShouldBe(0);

            "MyProp".InvokeIf<MyStub>(null, () => count++);
            count.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldReturnBooleanFromInvokeIf()
        {
            var count = 0;

            "MyProp".InvokeIf<MyStub>(m => m.MyProp, () => count++).ShouldBe(true);

            // ---

            "NotAProperty".InvokeIf<MyStub>(m => m.MyProp, () => count++).ShouldBe(false);
            "MyProp".InvokeIf<MyStub>(m => m.MyProp, null).ShouldBe(false);
            "MyProp".InvokeIf<MyStub>(null, () => count++).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldInvokeFromMultipleSpecifiedProperties()
        {
            var count = 0;

            "MyProp".InvokeIf<MyStub>(() => count++, m => m.MyProp, m => m.Text);
            count.ShouldBe(1);
            count = 0;

            "Not".InvokeIf<MyStub>(() => count++, m => m.MyProp, m => m.Text, m => m.Number);
            count.ShouldBe(0);
        }
        #endregion

        #region Internal
        private static Expression<Func<T, object>> GetExpression<T>(Expression<Func<T, object>> expression)
        {
            return expression;
        }
        #endregion

        #region Stubs
        public class MyStub
        {
            public string MyProp { get; set; }
            public string Text { get; set; }
            public int Number { get; set; }
            public MyChild Child { get; set; }
        }

        public class MyChild
        {
            public MyChild GrandChild { get; set; }
        }
        #endregion
    }
}
