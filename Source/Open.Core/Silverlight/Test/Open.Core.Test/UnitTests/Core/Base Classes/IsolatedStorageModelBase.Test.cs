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
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Base_Classes
{
    [Tag("store")]
    [TestClass]
    public class IsolatedStorageModelBaseTest : SilverlightUnitTest
    {
        #region Head
        private const string PropKeyMyString = "TestId:~:MyString";
        private const string PropKeySiteAddresses = "TestId:~:SiteAddresses";
        private IsolatedStorageSettings appSettings;

        [TestInitialize]
        public void TestSetup()
        {
            IsolatedStorageSettings.ApplicationSettings.Clear();
            appSettings = IsolatedStorageSettings.ApplicationSettings;
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldConstructWithDefaultValues()
        {
            var stub = new Stub(IsolatedStorageType.Application);
            stub.AutoSave.ShouldBe(true);
            stub.AutoIncrementQuotaBy.ShouldBe(2d);
            stub.StoreAsXml.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldStoreId()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false };
            stub.Id.ShouldBe("TestId");
        }

        [TestMethod]
        public void ShouldStoreReferenceValue()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false };
            stub.SiteAddresses.ShouldBeInstanceOfType<ObservableCollection<Uri>>();

            var child = new ObservableCollection<Uri>();
            stub.SiteAddresses = child;

            stub.SiteAddresses.ShouldBe(child);
        }

        [TestMethod]
        public void ShouldReturnSameValueAfterInitialValueIsCreated()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false };
            var child = stub.SiteAddresses;

            child.ShouldNotBe(null);
            stub.SiteAddresses.ShouldBe(child);
        }

        [TestMethod]
        public void ShouldAutoSaveByDeafult()
        {
            var stub = new Stub(IsolatedStorageType.Application);
            stub.AutoSave.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldConstructAsApplicationSettings()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false };
            stub.StoreType.ShouldBe(IsolatedStorageType.Application);
            stub.Store.ShouldBe(IsolatedStorageSettings.ApplicationSettings);
        }

        [TestMethod]
        public void ShouldConstructAsSiteSettings()
        {
            var stub = new Stub(IsolatedStorageType.Site) { AutoSave = false };
            stub.StoreType.ShouldBe(IsolatedStorageType.Site);
            stub.Store.ShouldBe(IsolatedStorageSettings.SiteSettings);
        }

        [TestMethod]
        public void ShouldStoreValueInIsolatedStorage()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false };
            appSettings.Contains(PropKeyMyString).ShouldBe(false);

            stub.MyString = "Hello";

            appSettings.Contains(PropKeyMyString).ShouldBe(true);
            appSettings[PropKeyMyString].ShouldBe("Hello");

            stub.MyString.ShouldBe("Hello");
        }

        [TestMethod]
        public void ShouldFirePropertyChanged()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false };
            appSettings.Contains(PropKeyMyString).ShouldBe(false);

            stub.ShouldFirePropertyChanged<Stub>(1, () =>
                                                        {
                                                            stub.MyString = "Fire";
                                                            stub.MyString = "Fire";
                                                            stub.MyString = "Fire";
                                                        }, m => m.MyString);
            
        }

        [TestMethod]
        public void ShouldFireSavedEvent()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false };

            var count = 0;
            stub.Saved += delegate { count++; };

            stub.Save();
            count.ShouldBe(1);
        }

        [TestMethod]
        [Asynchronous]
        public void ShouldSaveAutomatically()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = true };
            stub.Saved += delegate
                              {
                                  EnqueueTestComplete();
                              };
            stub.MyString = "Value";
        }

        [TestMethod]
        public void ShouldClearOnlySpecificStub()
        {
            var stub1 = new Stub(IsolatedStorageType.Application, "Stub") {AutoSave = false, MyString = "value"};
            stub1.Store.Count.ShouldNotBe(0);

            var stub2 = new Stub(IsolatedStorageType.Application, "Stub/1") { AutoSave = false, MyString = "value" };
            stub2.Store.Count.ShouldNotBe(0);

            var stub3 = new Stub(IsolatedStorageType.Application, "Stub.MyString") { AutoSave = false, MyString = "value" };
            stub3.Store.Count.ShouldNotBe(0);

            // ---

            stub1.Store.ShouldBe(stub2.Store);
            stub1.Store.Count.ShouldBe(3);

            stub1.Clear();
            stub1.Store.Count.ShouldBe(2);
        }

        [TestMethod]
        public void ShouldSaveCollectionOfDifferentTypes()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false};
            stub.SiteAddresses.Add(new Uri("http://www.com"));
            stub.TextCollection.Add("Hello");
            stub.Save();
        }

        [TestMethod]
        public void ShouldBeAbleToSerializeAndDeserializeUriCollection()
        {
            var collection = new ObservableCollection<Uri> {new Uri("http://site.com")};
            var xml = collection.ToSerializedXml();
            xml.ShouldNotBe(null);

            var deserialized = xml.Deserialize(typeof (ObservableCollection<Uri>));
            deserialized.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldStoreValuesInSerializedForm()
        {
            appSettings.Contains(PropKeySiteAddresses).ShouldBe(false);
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false, StoreAsXml =  true};

            var collection = stub.SiteAddresses;
            collection.Add(new Uri("http://site.com"));
            var xml = collection.ToSerializedXml();

            stub.SiteAddresses = collection;

            appSettings.Contains(PropKeySiteAddresses).ShouldBe(true);
            appSettings[PropKeySiteAddresses].ShouldBe(xml);
        }

        [TestMethod]
        public void ShouldDeserializeValues()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false, StoreAsXml = true };
            var collection = stub.SiteAddresses;
            collection.Add(new Uri("http://site.com/"));
            stub.SiteAddresses = collection;
            stub.Save();

            stub = new Stub(IsolatedStorageType.Application) { AutoSave = false, StoreAsXml = true };
            var readCollection = stub.SiteAddresses;
            readCollection.Count.ShouldBe(1);
            readCollection[0].ToString().ShouldBe("http://site.com/");
        }

        [TestMethod]
        [Ignore]
        public void ShouldReturnSameInstanceAfterCreatingDefaultValue()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false, StoreAsXml = true };
            var value1 = stub.List;
            var value2 = stub.List;
            value1.ShouldBe(value2);
        }


        [TestMethod]
        [Ignore]
        public void ShouldSerializeList()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false, StoreAsXml = true };
            stub.List.ShouldBeInstanceOfType<List<string>>();

            stub.List.Add("one");
            stub.List.Add("two");
            stub.List.Count.ShouldBe(2);

            stub.Save();
            stub.List.Count.ShouldBe(2);
        }

        #endregion

        #region Stubs
        private class Stub : IsolatedStorageModelBase
        {
            #region Head
            public Stub(IsolatedStorageType storeType, string id) : base(storeType, id) { }
            public Stub(IsolatedStorageType storeType) : base(storeType, "TestId") { }
            #endregion

            #region Properties
            public string MyString
            {
                get{ return GetPropertyValue<Stub, string>(m => m.MyString);}
                set { SetPropertyValue<Stub, string>(m => m.MyString, value); }
            }

            public ObservableCollection<Uri> SiteAddresses
            {
                get { return GetPropertyValue<Stub, ObservableCollection<Uri>>(m => m.SiteAddresses, new ObservableCollection<Uri>()); }
                set { SetPropertyValue<Stub, ObservableCollection<Uri>>(m => m.SiteAddresses, value, new ObservableCollection<Uri>()); }
            }

            public ObservableCollection<string> TextCollection
            {
                get { return GetPropertyValue<Stub, ObservableCollection<string>>(m => m.TextCollection, new ObservableCollection<string>()); }
            }

            public List<string> List
            {
                get { return GetPropertyValue<Stub, List<string>>(m => m.List, new List<string>()); }
            }
            #endregion
        }
        #endregion
    }
}
