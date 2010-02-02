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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Containers
{
    [ViewTestClass]
    public class WidgetGroupViewTest
    {
        #region Head
        private WidgetGroup widgetGroup;
        private PlaceholderViewTest.SampleModel model;
        private Placeholder content;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(Border control)
        {
            control.Width = 400;
            control.Height = 300;
            control.Background = StyleResources.Colors["Mac.Lavender"] as Brush;
            control.Padding = new Thickness(10);

            widgetGroup = new WidgetGroup{Title = "My Title", Padding = new Thickness(5)};
            control.Child = widgetGroup;

            // Sample content.
            content = new Placeholder();
            Change_DataContext_On_Content(control);
            content.SetBinding(Placeholder.TextProperty, new Binding("Text"));
            widgetGroup.Content = content;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Toggle_IsTitleVisible(Border control)
        {
            widgetGroup.IsTitleVisible = !widgetGroup.IsTitleVisible;
        }

        [ViewTest]
        public void Toggle_TitleHeight(Border control)
        {
            widgetGroup.TitleHeight = widgetGroup.TitleHeight == 30 ? 45: 30;
        }

        [ViewTest]
        public void Change_Text(Border control)
        {
            widgetGroup.Title = RandomData.LoremIpsum(1, 5);
            model.Text = RandomData.LoremIpsum(2, 5);
        }

        [ViewTest]
        public void Change_Padding(Border control)
        {
            widgetGroup.Padding = widgetGroup.Padding.Left ==0 ? new Thickness(20) : new Thickness(0);
        }

        [ViewTest]
        public void Change_DataContext_On_Content(Border control)
        {
            model = new PlaceholderViewTest.SampleModel { Text = RandomData.LoremIpsum(5,8) };
            content.DataContext = model;
        }
        #endregion
    }
}
