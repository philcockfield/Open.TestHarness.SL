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
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Base_Classes
{
    [Tag("foo")]
    [TestClass]
    public class IsolatedStorageModelBaseTest : SilverlightUnitTest
    {
        #region Head
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
            stub.StoreAsSerializedString.ShouldBe(false);
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
            appSettings.Contains(stub.Id).ShouldBe(false);

            stub.MyString = "Hello";

            appSettings.Contains(stub.Id).ShouldBe(false);
            stub.Save();
            appSettings.Contains(stub.Id).ShouldBe(true);

            var stub2 = new Stub(IsolatedStorageType.Application) { AutoSave = false };
            stub2.MyString.ShouldBe("Hello");
        }

        [TestMethod]
        public void ShouldFirePropertyChanged()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false };
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

        [Asynchronous]
        [TestMethod]
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
        public void ShouldClearOnlySpecificStubInMemory()
        {
            var stub1 = new Stub(IsolatedStorageType.Application, "Stub") {AutoSave = false, MyString = "value"};
            var stub2 = new Stub(IsolatedStorageType.Application, "Stub/1") { AutoSave = false, MyString = "value" };
            var stub3 = new Stub(IsolatedStorageType.Application, "Stub.MyString") { AutoSave = false, MyString = "value" };

            stub1.Save();
            stub2.Save();
            stub3.Save();

            var store = stub1.Store;
            store.Keys.ShouldContain(stub1.Id);
            store.Keys.ShouldContain(stub2.Id);
            store.Keys.ShouldContain(stub3.Id);

            // ---

            stub1.Store.ShouldBe(stub2.Store);

            stub1.Clear();
            store.Keys.ShouldNotContain(stub1.Id);
            store.Keys.ShouldContain(stub2.Id);
            store.Keys.ShouldContain(stub3.Id);
        }

        [TestMethod]
        public void ShouldClearInIsolatedStoreWhenSerializing()
        {
            var stub1 = new Stub(IsolatedStorageType.Application, "Stub") { AutoSave = false, MyString = "value", StoreAsSerializedString = true};
            stub1.MyString.ShouldBe("value");
            stub1.Save();

            stub1 = new Stub(IsolatedStorageType.Application, "Stub") { AutoSave = false, StoreAsSerializedString = true };
            stub1.MyString.ShouldBe("value");

            stub1.Clear();
            stub1.Save();

            stub1 = new Stub(IsolatedStorageType.Application, "Stub") { AutoSave = false, StoreAsSerializedString = true };
            stub1.MyString.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldClearInIsolatedStoreWhenNotSerializing()
        {
            var stub1 = new Stub(IsolatedStorageType.Application, "Stub") { AutoSave = false, MyString = "value" };
            stub1.MyString.ShouldBe("value");
            stub1.Save();

            stub1 = new Stub(IsolatedStorageType.Application, "Stub") { AutoSave = false };
            stub1.MyString.ShouldBe("value");

            stub1.Clear();
            stub1.Save();

            stub1 = new Stub(IsolatedStorageType.Application, "Stub") { AutoSave = false };
            stub1.MyString.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldAllowClearToBeCalledMultipleTimes()
        {
            var stub1 = new Stub(IsolatedStorageType.Application, "Stub") { AutoSave = false, MyString = "value" };
            stub1.Clear();
            stub1.Clear();
            stub1.Clear();
            stub1.MyString.ShouldBe(null);
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

            var deserialized = xml.FromSerializedXml(typeof (ObservableCollection<Uri>));
            deserialized.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldStoreValuesInSerializedForm()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false, StoreAsSerializedString =  true};

            var collection = stub.SiteAddresses;
            collection.Add(new Uri("http://site.com"));
            var xml = collection.ToSerializedXml();

            stub.SiteAddresses = collection;
            stub.Save();

            appSettings.Contains(stub.Id).ShouldBe(true);

            var stub2 = new Stub(IsolatedStorageType.Application) { AutoSave = false, StoreAsSerializedString = true };
            stub2.SiteAddresses.ToSerializedXml().ShouldBe(xml);
        }

        [TestMethod]
        public void ShouldDeserializeValues()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false, StoreAsSerializedString = true };
            var collection = stub.SiteAddresses;
            collection.Add(new Uri("http://site.com/"));
            stub.SiteAddresses = collection;
            stub.Save();

            stub = new Stub(IsolatedStorageType.Application) { AutoSave = false, StoreAsSerializedString = true };
            var readCollection = stub.SiteAddresses;
            readCollection.Count.ShouldBe(1);
            readCollection[0].ToString().ShouldBe("http://site.com/");
        }

        [TestMethod]
        public void ListShouldBeSerializableToJson()
        {
            var list = new List<ListItem>();
            list.Add(new ListItem { Text = "Foo" });
            list.ToSerializedJson();
        }

        [TestMethod]
        public void ShouldSerializeList()
        {
            var mock = new ListMock();
            mock.StoreAsSerializedString.ShouldBe(true);

            mock.List.Count().ShouldBe(0);

            mock.Add("one");
            mock.Add("two");
            mock.List.Count().ShouldBe(2);

            mock.Save();
            mock.List.Count().ShouldBe(2);

            // ---

            mock = new ListMock();
            mock.List.Count().ShouldBe(2);
            mock.List.ElementAt(0).Text.ShouldBe("one");
            mock.List.ElementAt(1).Text.ShouldBe("two");
        }

        [TestMethod]
        public void ShouldDetermineIfIsFirstLoad()
        {
            var stub = new Stub(IsolatedStorageType.Application) { AutoSave = false };
            stub.Clear();
            stub.Store.Save();

            stub.IsFirstLoad.ShouldBe(true);
            stub = new Stub(IsolatedStorageType.Application) { AutoSave = false };
            stub.IsFirstLoad.ShouldBe(true);

            stub.Save();
            stub.IsFirstLoad.ShouldBe(false);
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
            #endregion
        }

        private class ListMock : IsolatedStorageModelBase
        {
            public ListMock() : base(IsolatedStorageType.Application, "Test.IsoStore.ListMock")
            {
                StoreAsSerializedString = true;
                AutoSave = false;
            }
            public void Add(string text) { ListInternal.Add(new ListItem { Text = text, Time = DateTime.Now }); }
            private List<ListItem> ListInternal
            {
                get { return GetPropertyValue<ListMock, List<ListItem>>(m => m.ListInternal, new List<ListItem>()); }
            }
            public IEnumerable<ListItem> List { get { return ListInternal; } }
        }

        [DataContract]
        public class ListItem
        {
            [DataMember]
            public DateTime Time { get; set; }

            [DataMember]
            public string Text { get; set; }
        }
        #endregion
    }
}
