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

using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using System.IO;

namespace Open.Core.Common.Test.Model
{
    [TestClass]
    public class ModelBaseTest
    {
        #region Test
        [TestMethod]
        public void ShouldExist()
        {
            var model = new ModelStub();
        }

        [TestMethod]
        public void ShouldDispose()
        {
            var model = new ModelStub();
            model.IsDisposed.ShouldBe(false);
            model.Dispose();
            model.IsDisposed.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldFireWhenDisposed()
        {
            var model = new ModelStub();

            var fireCount = 0;
            model.Disposed += delegate { fireCount++; };

            model.Dispose();
            fireCount.ShouldBe(1);

            // ---

            model.Dispose();
            model.Dispose();
            model.Dispose();
            fireCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldCallOnDisposedVirtualMethodWhenDisposedOf()
        {
            var model = new ModelStub();
            model.DisposeOfManagedResourcesCount.ShouldBe(0);

            model.Dispose();
            model.DisposeOfManagedResourcesCount.ShouldBe(1);

            model.Dispose();
            model.Dispose();
            model.Dispose();
            model.DisposeOfManagedResourcesCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldReportPropertyChanged()
        {
            var model = new ModelStub();
            model.ShouldFirePropertyChanged(() => model.Text1 = "value", ModelStub.PropText1);

            var eventCount = 0;
            model.PropertyChanged += (sender, e) => eventCount++;

            model.TestOnPropertyChanged("Name");
            eventCount.ShouldBe(1);

            eventCount = 0;
            model.TestOnPropertyChanged("Name1", "Name2");
            eventCount.ShouldBe(2);
        }


        [TestMethod]
        public void ShouldBeSerializable()
        {
            var model = new ModelStub();
            var dcs = new DataContractSerializer(typeof(ModelStub));
            var stream = new MemoryStream();

            dcs.WriteObject(stream, model);
        }
        #endregion

        #region Stubs
        public class ModelStub : ModelBase
        {
            #region Head
            public const string PropText1 = "Text1";
            public const string PropText2 = "Text2";

            private string text1;

            public int DisposeOfManagedResourcesCount;
            #endregion

            #region Properties
            public string Text1
            {
                get { return text1; }
                set { text1 = value; OnPropertyChanged(PropText1); }
            }
            #endregion

            #region Methods
            public void TestOnPropertyChanged(string propertyName)
            {
                OnPropertyChanged(propertyName);
            }

            public void TestOnPropertyChanged(params string[] propertyNames)
            {
                OnPropertyChanged(propertyNames);
            }

            protected override void OnDisposed()
            {
                DisposeOfManagedResourcesCount++;
            }
            #endregion
        }
        #endregion
    }
}
