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
using System.Windows.Data;
using System.Windows.Media;
using System.Diagnostics;
using Microsoft.Silverlight.Testing;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls
{
    [ViewTestClass(DisplayName = "Placeholder (Testing)")]
    public class PlaceholderViewTest
    {
        #region Head
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(Placeholder control)
        {
            control.Width = 200;
            control.Height = 200;
        }
        #endregion

        #region Tests
        [ViewTest(DisplayName = "The 'Color' Red.")]
        public void Color_Red(Placeholder control)
        {
            control.Color = Colors.Red;
        }

        [Tag("MyTag")]
        [ViewTest]
        public void Color_Green(Placeholder control)
        {
            control.Color = Colors.Green;
        }

        [ViewTest] public void Color_Blue(Placeholder control)
        {
            control.Color = Colors.Blue;
        }

        [ViewTest]
        public void Toggle_CornerRadius(Placeholder control)
        {
            control.CornerRadius = control.CornerRadius.BottomLeft == 0 
                ? new CornerRadius(20) 
                : new CornerRadius(0);
        }

        [ViewTest]
        public void Set_Text(Placeholder control)
        {
            control.Text = RandomData.LoremIpsum(1, 3);
        }

        [ViewTest]
        public void Toggle_ShowInstanceCount(Placeholder control)
        {
            control.ShowInstanceCount = !control.ShowInstanceCount;
            Debug.WriteLine("ShowInstanceCount: " + control.ShowInstanceCount);
        }

        [ViewTest]
        public void Show_Three_Placeholders(Placeholder control1, Placeholder control2, Placeholder control3)
        {
            control1.Color = Colors.Red;
            control2.Color = Colors.Green;
            control3.Color = Colors.Blue;
        }

        [ViewTest]
        public void Set_DataContext(Placeholder control)
        {
            control.DataContext = new SampleModel {Text = "Sample Text"};
        }

        [ViewTest]
        public void DataBind_To_Text(Placeholder control)
        {
            var model = new SampleModel { Text = "Sample Text" };
            control.DataContext = model;
            control.SetBinding(Placeholder.TextProperty, new Binding("Text"));
        }

        [ViewTest]
        public void Set_Focus(Placeholder control)
        {
            control.IsTabStop = true;
            DelayedAction.Invoke(0.1, () => control.Focus());
        }
        #endregion

        #region Sample Data
        public class SampleModel : ViewModelBase
        {
            public const string PropText = "Text";
            private string text;
            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged(PropText); }
            }
        }
        #endregion
    }
}
