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
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using System.IO;

namespace Open.Core.Common.Test.Model
{
    [TestClass]
    public class ModelBaseAutoPropertyTest
    {
        #region Tests
        [TestMethod]
        public void ShouldInvokeReadPropertyValue()
        {
            var stub = new Stub();
            stub.GetPropertyValueTest<Stub, string>(m => m.MyText, null);
            stub.ReadPropertyValueCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldInvokeWritePropertyValue()
        {
            var stub = new Stub();
            stub.SetPropertyValueTest<Stub, string>(m => m.MyText, "Value", null);
            stub.WritePropertyValueCount.ShouldBe(2); // NB: 2 times.  First time is stored the default value.
        }

        [TestMethod]
        public void ShouldReturnDefaultValue()
        {
            var stub = new Stub();
            stub.GetPropertyValueTest<Stub, string>(m => m.MyText, null).ShouldBe(null);

            stub = new Stub();
            stub.GetPropertyValueTest<Stub, string>(m => m.MyText, "Default Value").ShouldBe("Default Value");

            stub = new Stub();
            var child = new ObservableCollection<Uri>();
            stub.GetPropertyValueTest<Stub, ObservableCollection<Uri>>(m => m.Child, child).ShouldBe(child);
        }

        [TestMethod]
        public void ShouldStoreValue()
        {
            var stub = new Stub();
            stub.MyText.ShouldBe(null);
            stub.MyText = "Value";
            stub.MyText.ShouldBe("Value");

            stub.MyText = null;
            stub.MyText.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldStoreReferenceValue()
        {
            var stub = new Stub();
            stub.Child.ShouldBeInstanceOfType<ObservableCollection<Uri>>();

            var child = new ObservableCollection<Uri>();
            stub.Child = child;

            stub.Child.ShouldBe(child);
        }

        [TestMethod]
        public void ShouldReturnSameValueAfterInitialValueIsCreated()
        {
            var stub = new Stub();
            var child = stub.Child;
            
            child.ShouldNotBe(null);
            stub.Child.ShouldBe(child);
        }

        [TestMethod]
        public void ShouldFirePropertyChanged()
        {
            var stub = new Stub();
            stub.ShouldFirePropertyChanged<Stub>(1, () =>
                                                                {
                                                                    stub.MyText = "Value";
                                                                    stub.MyText = "Value";
                                                                    stub.MyText = "Value";
                                                                }, m => m.MyText);
        }

        [TestMethod]
        public void ShouldFireOtherProperties()
        {
            var stub = new Stub();
            stub.ShouldFirePropertyChanged<Stub>(
                        () => stub.SetPropertyValueTest<Stub, string>(m => m.MyText, "Value", null, m => m.Number), 
                        m => m.MyText, m => m.Number);
        }

        [TestMethod]
        public void ShouldFireSubjectPropertyOnlyOnceEvenIfPassedInSecondaryList()
        {
            var stub = new Stub();
            stub.ShouldFirePropertyChanged<Stub>(
                        1,
                        () => stub.SetPropertyValueTest<Stub, string>(m => m.MyText, "Value", null, m => m.MyText, m => m.Number),
                        m => m.MyText);
        }

        [TestMethod]
        public void ShouldNotFireWhenPropertyValueNotChanged()
        {
            var stub = new Stub();

            stub.ShouldNotFirePropertyChanged<Stub>(
                            () => stub.SetPropertyValueTest<Stub, string>(m => m.MyText, null, null, m => m.Number),
                            m => m.MyText, m => m.Number);

            stub.MyText = "Value";
            stub.ShouldNotFirePropertyChanged<Stub>(
                            () => stub.SetPropertyValueTest<Stub, string>(m => m.MyText, "Value", null, m => m.Number),
                            m => m.MyText, m => m.Number);
        }

        [TestMethod]
        public void ShouldClearStoreWhenDisposed()
        {
            var stub = new Stub {MyText = "Value"};
            stub.Dispose();
            stub.MyText.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldReturnFalseWhenNoChange()
        {
            var stub = new Stub();
            stub.MyText.ShouldBe(null);
            stub.SetPropertyValueTest<Stub, string>(m => m.MyText, null, null).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldReturnTrueWhenChanged()
        {
            var stub = new Stub();
            stub.MyText.ShouldBe(null);
            stub.SetPropertyValueTest<Stub, string>(m => m.MyText, "Value", null).ShouldBe(true);
            stub.SetPropertyValueTest<Stub, string>(m => m.MyText, "Value", null).ShouldBe(false);
        }
        #endregion

        #region Stubs
        public class Stub : ModelBase
        {
            #region Head
            public int ReadPropertyValueCount { get; set; }
            public int WritePropertyValueCount { get; set; }
            #endregion

            #region Properties
            public string MyText
            {
                get { return GetPropertyValue<Stub, string>(m => m.MyText); }
                set { SetPropertyValue<Stub, string>(m => m.MyText, value); }
            }

            public ObservableCollection<Uri> Child
            {
                get { return GetPropertyValue<Stub, ObservableCollection<Uri>>(m => m.Child, new ObservableCollection<Uri>()); }
                set { SetPropertyValue<Stub, ObservableCollection<Uri>>(m => m.Child, value, new ObservableCollection<Uri>()); }
            }

            public int Number { get; set; }
            #endregion

            #region Methods
            public TResult GetPropertyValueTest<T, TResult>(Expression<Func<T, object>> property, TResult defaultValue)
            {
                return GetPropertyValue(property, defaultValue);
            }

            public bool SetPropertyValueTest<T, TResult>(Expression<Func<T, object>> property, TResult value, TResult defaultValue, params Expression<Func<T, object>>[] fireAlso)
            {
                return SetPropertyValue(property, value, defaultValue, fireAlso);
            }

            protected override bool ReadPropertyValue<TResult>(string key, out TResult value)
            {
                ReadPropertyValueCount++;
                return base.ReadPropertyValue(key, out value);
            }

            protected override void WritePropertyValue<T>(string key, T value)
            {
                WritePropertyValueCount++;
                base.WritePropertyValue(key, value);
            }
            #endregion
        }
        #endregion
    }
}
