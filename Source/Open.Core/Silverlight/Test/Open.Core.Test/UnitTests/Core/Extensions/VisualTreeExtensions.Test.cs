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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Extensions
{
    [TestClass]
    public class VisualTreeExtensionsTest : SilverlightUnitTest
    {
        #region Tests
        //[TestMethod]
        //[Asynchronous]
        //public void ShouldGetElementFromViewModel()
        //{
        //    var itemsControl = new ItemsControl
        //                           {
        //                               ItemTemplate = SampleTemplates.GetDataTemplate("GetElementFromViewModel.Test")
        //                           };

        //    var model1 = new Stub{Text = "one"};
        //    var model2 = new Stub{Text = "two"};
        //    var model3 = new Stub{Text = "three"};
        //    var collection = new ObservableCollection<Stub> { model1, model2, model3 };
        //    itemsControl.ItemsSource = collection;

        //    AddAndWaitForLoaded(itemsControl);

        //    // ---

        //    var element1 = itemsControl.GetElementFromViewModel<Stub>(model1);
        //    element1.ShouldBeInstanceOfType<Placeholder>();
        //}
        #endregion

        #region Stubs
        public class Stub : ViewModelBase
        {
            /// <summary>Gets or sets .</summary>
            public string Text
            {
                get { return GetPropertyValue<Stub, string>(m => m.Text); }
                set { SetPropertyValue<Stub, string>(m => m.Text, value); }
            }
        }
        #endregion
    }
}