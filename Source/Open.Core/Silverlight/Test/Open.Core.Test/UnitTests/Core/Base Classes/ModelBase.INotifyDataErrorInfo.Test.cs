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
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using System.IO;

namespace Open.Core.UI.Test
{
    [Tag("current")]
    [TestClass]
    public class ModelBaseINotifyDataErrorInfoTest
    {
        #region Head
        private Mock mock;

        [TestInitialize]
        public void TestSetup()
        {
            mock = new Mock();
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldAddErrorsForProperty()
        {
            mock.Setup1();
            mock.GetErrors<Mock>(m => m.Text).Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldReturnAllErrorsWhenNullPassedToGetErrors()
        {
            mock.Setup1();
            mock.Setup2();
            mock.GetErrors<Mock>(null).Count().ShouldBe(3);
        }

        [TestMethod]
        public void ShouldReportIfErrorsExist()
        {
            mock.HasErrors.ShouldBe(false);

            mock.Setup1();
            mock.HasErrors.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldThrowOnNullParams()
        {
            Should.Throw<ArgumentNullException>(() => mock.AddError(null, new ErrorInfo()));
            Should.Throw<ArgumentNullException>(() => mock.AddError(m => m.Number, null));
            Should.Throw<ArgumentNullException>(() => mock.RemoveError(null, 1));
        }

        [TestMethod]
        public void ShouldReplaceError()
        {
            var error1 = new ErrorInfo { ErrorCode = 1 };
            var error2 = new ErrorInfo { ErrorCode = 1 };

            mock.AddError(m => m.Text, error1);
            var errors = mock.GetErrors<Mock>(m => m.Text);
            errors.Count().ShouldBe(1);
            errors.First().ShouldBe(error1);

            mock.AddError(m => m.Text, error2);
            errors = mock.GetErrors<Mock>(m => m.Text);
            errors.Count().ShouldBe(1);
            errors.First().ShouldBe(error2);

            mock.AddError(m => m.Text, error1);
            errors = mock.GetErrors<Mock>(m => m.Text);
            errors.Count().ShouldBe(1);
            errors.First().ShouldBe(error1);
        }
    
        [TestMethod]
        public void ShouldAddMulipleErrorsInOrder()
        {
            var error1 = new ErrorInfo { ErrorCode = 1 };
            var error2 = new ErrorInfo { ErrorCode = 2 };

            mock.AddError(m => m.Text, error1);
            mock.AddError(m => m.Text, error2);

            var errors = mock.GetErrors<Mock>(m => m.Text);
            errors.ElementAt(0).ShouldBe(error1);
            errors.ElementAt(1).ShouldBe(error2);
        }

        [TestMethod]
        public void ShouldNotReturnErrorForProperty()
        {
            var error1 = new ErrorInfo { ErrorCode = 1 };
            mock.AddError(m => m.Text, error1);
            mock.GetErrors<Mock>(m => m.Number).Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldFireEvent()
        {
            var fireCount = 0;
            mock.ErrorsChanged += delegate { fireCount++; };

            var error1 = new ErrorInfo { ErrorCode = 1 };
            var error2 = new ErrorInfo { ErrorCode = 2 };

            mock.AddError(m => m.Number, error1);
            fireCount.ShouldBe(1);

            mock.AddError(m => m.Number, error2);
            fireCount.ShouldBe(2);

            mock.AddError(m => m.Text, error2);
            fireCount.ShouldBe(3);
        }

        [TestMethod]
        public void ShouldAllowSameErrorToExistForDifferentProperties()
        {
            var error1 = new ErrorInfo { ErrorCode = 1 };

            mock.AddError(m => m.Text, error1);
            mock.AddError(m => m.Number, error1);

            var errors = mock.GetErrors();
            errors.Count().ShouldBe(2);

            mock.GetErrors<Mock>(m => m.Text).First().ShouldBe(error1);
            mock.GetErrors<Mock>(m => m.Number).First().ShouldBe(error1);
            mock.GetErrors<Mock>(m => m.Date).Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldRemoveSpecficErrorFromProperty()
        {
            var error1 = new ErrorInfo { ErrorCode = 1 };
            var error2 = new ErrorInfo { ErrorCode = 2 };

            mock.AddError(m => m.Number, error1);
            mock.AddError(m => m.Number, error2);
            var errors = mock.GetErrors<Mock>(m => m.Number);
            errors.Count().ShouldBe(2);
            errors.ShouldContain(error1);
            errors.ShouldContain(error2);

            mock.RemoveError(m => m.Number, error2.ErrorCode);
            errors = mock.GetErrors<Mock>(m => m.Number);
            errors.Count().ShouldBe(1);
            errors.ShouldContain(error1);
            errors.ShouldNotContain(error2);
        }

        [TestMethod]
        public void ShouldFireWhenRemovingSingleError()
        {
            var error1 = new ErrorInfo { ErrorCode = 1 };
            mock.AddError(m => m.Number, error1);

            var fireCount = 0;
            mock.ErrorsChanged += delegate { fireCount++; };

            mock.RemoveError(m => m.Number, error1.ErrorCode);
            fireCount.ShouldBe(1);

            fireCount = 0;
            mock.RemoveError(m => m.Number, error1.ErrorCode);
            fireCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldRemoveAllErrorsFromProperty()
        {
            var error1 = new ErrorInfo { ErrorCode = 1 };
            var error2 = new ErrorInfo { ErrorCode = 2 };

            mock.AddError(m => m.Number, error1);
            mock.AddError(m => m.Number, error2);

            var fireCount = 0;
            mock.ErrorsChanged += delegate { fireCount++; };

            mock.GetErrors<Mock>(m => m.Number).Count().ShouldBe(2);

            // ---

            mock.ClearErrors(m => m.Number);
            mock.GetErrors<Mock>(m => m.Number).Count().ShouldBe(0);
            fireCount.ShouldBe(1);

            mock.ClearErrors(m => m.Number);
            fireCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldClearAllErrors()
        {
            mock.Setup1();
            mock.Setup2();
            mock.GetErrors().Count().ShouldBe(3);

            var fireCount = 0;
            mock.ErrorsChanged += delegate { fireCount++; };

            // ---

            mock.ClearErrors();
            mock.GetErrors().Count().ShouldBe(0);
            fireCount.ShouldBe(3);

            fireCount = 0;
            mock.ClearErrors();
            fireCount.ShouldBe(0);
        }
        #endregion

        private class Mock : ModelBase
        {
            public string Text { get; set; }
            public int Number { get; set; }
            public DateTime Date { get; set; }

            public void Setup1()
            {
                AddError<Mock>(m => m.Text, new ErrorInfo { ErrorCode = 1 });
            }

            public void Setup2()
            {
                AddError<Mock>(m => m.Number, new ErrorInfo { ErrorCode = 2 });
                AddError<Mock>(m => m.Date, new ErrorInfo { ErrorCode = 3 });
            }

            public void AddError(Expression<Func<Mock, object>> property, IErrorInfo error)
            {
                base.AddError(property, error);
            }

            public  void RemoveError(Expression<Func<Mock, object>> property, int errorCode)
            {
                base.RemoveError(property, errorCode);
            }

            public void ClearErrors(Expression<Func<Mock, object>> property)
            {
                base.ClearErrors(property);
            }

            public new void ClearErrors()
            {
                base.ClearErrors();
            }
        }
    }
}
