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

using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Common.Base_Classes
{
    [TestClass]
    public class NotifyPropertyChangedBaseTest : SilverlightTest
    {
        #region Tests
        [TestMethod]
        public void ShouldFirePropertyChangedEvent()
        {
            var model = new Stub();
            PropertyChangedEventArgs args = null;
            model.PropertyChanged += (sender, e) => args = e;

            model.Text = "New Value";
            args.PropertyName.ShouldBe(Stub.PropText);
        }


        [TestMethod]
        [Asynchronous]
        public void ShouldFirePropertyChangedOnUiThread()
        {
            var uiThread = Thread.CurrentThread;
            Thread backgroundThread = null;

            var model = new Stub();
            model.PropertyChanged += delegate
                                         {
                                             // The event notification should arrive on the UI Thread.
                                             model.Text.ShouldBe("New Value");
                                             Thread.CurrentThread.ShouldBe(uiThread);
                                             EnqueueTestComplete();
                                         };

            ThreadStart backgroundAction = delegate
                                     {
                                         // Cause a property change to occur on the background thread.
                                         Thread.CurrentThread.ShouldBe(backgroundThread);
                                         model.Text = "New Value";
                                     };
            backgroundThread = new Thread(backgroundAction){Name = "Background"};
            uiThread.ShouldNotBe(backgroundThread);
            backgroundThread.Start();
        }

        [TestMethod]
        public void ShouldHaveSyncContext()
        {
            var model = new Stub();
            model.SyncContext.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldReturnSameInstanceAfterCreatingDefaultValue()
        {
            var stub = new Sample();
            var value1 = stub.Child;
            var value2 = stub.Child;
            value1.ShouldBe(value2);
        }
        #endregion

        #region Stubs
        private class Sample : NotifyPropertyChangedBase
        {
            public List<string> Child
            {
                get { return GetPropertyValue<Sample, List<string>>(m => m.Child, new List<string>()); }
            }
        }

        private class Stub : NotifyPropertyChangedBase
        {
            #region Head
            public const string PropText = "Text";

            private string text;
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

            public SynchronizationContext SyncContext { get { return SynchronizationContext; } }
            #endregion
        }
        #endregion
    }
}
