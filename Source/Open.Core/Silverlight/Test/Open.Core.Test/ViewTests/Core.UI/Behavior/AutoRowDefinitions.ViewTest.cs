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

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Behavior
{
    [ViewTestClass]
    public class Behavior__AutoRowDefinitionsViewTest
    {
        #region Head
        private readonly ObservableCollection<string> collection = new ObservableCollection<string>();
        private readonly AutoRowDefinitions behavior = new AutoRowDefinitions();

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(Grid control)
        {
            control.Width = 200;
            control.Height = 400;
            control.ShowGridLines = true;

            behavior.ItemsSource = collection;
            behavior.RowHeight = new GridLength(20);

            Behaviors.SetAutoRowDefinitions(control, behavior);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Add_Item(Grid control)
        {
            collection.Add(RandomData.LoremIpsum(1));
        }

        [ViewTest]
        public void Remove_Item(Grid control)
        {
            if (collection.Count ==0) return;
            collection.RemoveAt(0);
        }
        #endregion
    }
}
