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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test
{
    [TestClass]
    public class NotifyPropertyChangedBaseTest
    {
        #region Test
        [TestMethod]
        public void ShouldFirePropertyChangedEvent()
        {
            var sample = new Stub();
            sample.ShouldFirePropertyChanged(() => sample.Text = "value", Stub.PropText);
        }

        [TestMethod]
        public void ShouldFireMultipleProperties()
        {
            var sample = new Stub();
            sample.ShouldFirePropertyChanged(() => sample.OnPropertyChangedTest("one", "two"), "one", "two");
        }

        [TestMethod]
        public void ShouldFireFromStronglyTypedLinqDerivedPropertyName()
        {
            var sample = new Stub();
            sample.ShouldFirePropertyChanged(
                                () => sample.Number++, 
                                LinqExtensions.GetPropertyName<Stub>(o => o.Number));
        }

        [TestMethod]
        public void ShouldFireFromMultipleStronglyTypedLinqDerivedPropertyName()
        {
            var sample = new Stub();
            sample.ShouldFirePropertyChanged(
                                sample.OnPropertyChangedViaLinkTest,
                                        LinqExtensions.GetPropertyName<Stub>(o => o.Text),
                                        LinqExtensions.GetPropertyName<Stub>(o => o.Number));
        }
        #endregion

        #region Stubs
        private class Stub : NotifyPropertyChangedBase
        {
            #region Head
            public const string PropText = "Text";

            private string text;
            private int number;
            #endregion

            #region Properties
            public string Text
            {
                get { return text; }
                set
                {
                    text = value;
                    OnPropertyChanged(PropText);
                }
            }

            public int Number
            {
                get { return number; }
                set
                {
                    number = value;
                    OnPropertyChanged<Stub>(m => m.Number);
                }
            }
            #endregion

            #region Methods
            public void OnPropertyChangedTest(params string[] propertyNames)
            {
                OnPropertyChanged(propertyNames);
            }

            public void OnPropertyChangedViaLinkTest()
            {
                OnPropertyChanged<Stub>(m => m.Text, m => m.Number);
            }
            #endregion
        }
        #endregion
    }
}
