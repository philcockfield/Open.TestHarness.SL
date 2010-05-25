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
using System.Windows.Media;
using Open.Core.Common;
using System.Diagnostics;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common
{
    [ViewTestClass]
    public class Model__SettingsModelBaseViewTest
    {
        #region Head
        private SettingsStub stub;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize()
        {
            stub = new SettingsStub();
            stub.Saved += delegate { Output.Write(Colors.Green, "!! Saved"); };
            Read_Properties();
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Set_Text_Property__Null()
        {
            stub.Text = null;
        }

        [ViewTest]
        public void Set_Text_Property__Short()
        {
            stub.Text = RandomData.LoremIpsum(5, 10);
        }

        [ViewTest]
        public void Increment_Text_Property_by_100000()
        {
            stub.Text += RandomData.LoremIpsum(100000);
            Read_Properties();
        }

        [ViewTest]
        public void Save()
        {
            stub.Save();
        }

        [ViewTest]
        public void Clear()
        {
            stub.Clear();
        }

        [ViewTest]
        public void Increase_Quota_by_1_Megabyte()
        {
            Output.Write("IncreaseQuotaBy(1): " + stub.IncreaseQuotaBy(1));
        }

        [ViewTest]
        public void Increase_Quota_by_10_Megabytes()
        {
            Output.Write("IncreaseQuotaBy(10): " + stub.IncreaseQuotaBy(10));
        }

        [ViewTest]
        public void Increase_Quota_to_10_Megabytes()
        {
            try
            {
                Output.Write("IncreaseQuotaTo(10): " + stub.IncreaseQuotaTo(10));
            }
            catch (Exception error)
            {
                Output.WriteException(error);
            }
        }

        [ViewTest]
        public void Toggle__AutoSave()
        {
            stub.AutoSave = !stub.AutoSave;
            Output.Write("AutoSave: " + stub.AutoSave);
        }

        [ViewTest]
        public void Read_Properties()
        {
            Output.Write("> Id: " + stub.Id);
            Output.Write("> AutoSave: " + stub.AutoSave);
            Output.Write("> AvailableFreeBytes: " + stub.AvailableFreeBytes + " Bytes");
            Output.Write("> AvailableFreeMegabytes: " + stub.AvailableFreeMegabytes + " MB");
            Output.Write("> QuotaBytes: " + stub.QuotaBytes + " Bytes");
            Output.Write("> QuotaMegabytes: " + stub.QuotaMegabytes + " MB");
            Output.Write("> Store: " + stub.Store);
            Output.Write("> StoreType: " + stub.StoreType);

            if (stub.Text == null)
            {
                Output.Write("- Text.Length: null");
            }
            else
            {
                Output.Write("- Text.Length: " + stub.Text.Length);
            }

            Output.Break();
        }
        #endregion

        #region Stubs

        public class SettingsStub : IsolatedStorageModelBase
        {
            public SettingsStub() : base(IsolatedStorageType.Application, "IsolatedStorageModelBase.ViewTest")
            {
                AutoSave = false;
            }

            public string Text
            {
                get { return GetPropertyValue<SettingsStub, string>(m => m.Text); }
                set { SetPropertyValue<SettingsStub, string>(m => m.Text, value); }
            }
        }
        #endregion
    }
}
