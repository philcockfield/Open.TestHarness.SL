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

using System.Windows;
using System.Linq;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using System.Diagnostics;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Primitives.Buttons
{
    [ViewTestClass]
    public class CoreComboBoxViewTest
    {
        #region Head
        private ComboBoxViewModel viewModel;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(CoreComboBox control)
        {
            // Setup initial conditions.
            control.Width = 300;
            control.Padding = new Thickness(20, 5, 20, 5);

            // Setup view-model.
            viewModel = new ComboBoxViewModel();
            control.ViewModel = viewModel;

            // Wire up events.
            viewModel.SelectionChanged += delegate { Debug.WriteLine("!! SelectionChanged: " + viewModel.SelectedItem); };

            // Finish up.
            Build_Items_Collection(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Clear(CoreComboBox control)
        {
            viewModel.Items.RemoveAll();
        }

        [ViewTest]
        public void Build_Items_Collection(CoreComboBox control)
        {

            foreach (MyEnum value in typeof(MyEnum).GetEnumValues())
            {
                viewModel.Add(value.ToString(), value);
            }
            viewModel.SelectFirst();
        }

        [ViewTest]
        public void SelectFirst(CoreComboBox control)
        {
            viewModel.SelectFirst();
        }

        [ViewTest]
        public void SelectLast(CoreComboBox control)
        {
            viewModel.SelectLast();
        }

        [ViewTest]
        public void Select(CoreComboBox control)
        {
            viewModel.Select(2);
        }

        [ViewTest]
        public void SelectValue_Last(CoreComboBox control)
        {
            var item = viewModel.Items.Last();
            viewModel.SelectValue(item.Value);
        }

        [ViewTest]
        public void SelectedItem__Null(CoreComboBox control)
        {
            viewModel.SelectedItem = null;
        }

        [ViewTest]
        public void Change__FontSize(CoreComboBox control)
        {
            control.FontSize = 25;
        }
        #endregion

        #region Stubs
        public enum MyEnum
        {
            Value1,
            Value2,
            Value3,
            Value4,
            Value5
        }
        #endregion
    }
}
