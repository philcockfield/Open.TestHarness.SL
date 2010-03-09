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

using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.Core.UI.Silverlight.Test;

namespace Open.Core.Test.ViewTests.Core.Controls.Lists
{
    [ViewTestClass]
    public class SelectionListViewTest
    {
        #region Head
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        public ISelectionList ViewModel { get; set; }

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(SelectionList control)
        {
            CompositionInitializer.SatisfyImports(this);
            control.Width = 300;
            control.Height = 500;
            control.ViewModel = ViewModel;

            // Wire up events.
            ViewModel.SelectionChanged += delegate { Output.Write("Selection Changed: " + ViewModel.SelectedItem); };

            // Set selector.
            ViewModel.ItemTemplateSelector = o =>
                                                 {
                                                     var index = ViewModel.Items.IndexOf(o);
                                                     return index.IsEven() ? SampleTemplates.Placeholder1 : null;
                                                 };

            // Finish up.
            Add_Random_Strings(control);
        }
        #endregion

        #region Tests

        [ViewTest]
        public void Add_Random_Strings(SelectionList control)
        {
            for (int i = 0; i < 20; i++)
            {
                ViewModel.Items.Add(RandomData.LoremIpsum(3, 5));
            }
        }

        [ViewTest]
        public void Clear_List(SelectionList control)
        {
            ViewModel.Items.Clear();
        }


        public enum ValueOrNull { Value, Null, }
        [ViewTest]
        public void Set_EmptyListMessage(SelectionList control, ValueOrNull action = ValueOrNull.Value)
        {
            ViewModel.EmptyMessage = action == ValueOrNull.Value
                              ? new Placeholder{Text = "Custom Empty Message Control"}
                              : null;
        }

        [ViewTest]
        public void Toggle_ItemPadding(SelectionList control)
        {
            ViewModel.ItemPadding = ViewModel.ItemPadding.Left == 0
                                        ? new Thickness(5)
                                        : new Thickness(0);
        }

        [ViewTest]
        public void Set_ItemDividerColor__Red(SelectionList control)
        {
            ViewModel.ItemDividerColor = Colors.Red.ToBrush(0.3);
        }
        #endregion
    }
}
