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
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.Core.Common.Testing;
using System;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls
{
    [ViewTestClass]
    public class HtmlBlockViewTest
    {
        #region Head
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(HtmlBlock control)
        {
            control.Width = 300;
            control.Height = 300;

            control.BackgroundStyle = "orange";
            control.InnerHtml = GetShortHtml();
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Show_HtmlBlock(HtmlBlock control) { }

        [ViewTest]
        public void Show_Something_Else(Border control) { }

        [ViewTest]
        public void Toggle_Color(HtmlBlock control)
        {
            control.BackgroundStyle = control.BackgroundStyle == "red" ? "green" : "red";
        }

        [ViewTest]
        public void Change_InnerHtml(HtmlBlock control)
        {
            control.InnerHtml = GetShortHtml();
        }

        [ViewTest]
        public void Set_InnerHTML_Long(HtmlBlock control)
        {
            var html = GetShortHtml() + RandomData.LoremIpsum(300);
            control.InnerHtml = html;
        }

        [ViewTest]
        public void Set_InnerHtml_Null(HtmlBlock control)
        {
            control.InnerHtml = null;
        }

        [ViewTest]
        public void Set_SourceUri(HtmlBlock control)
        {
            var html = new Uri("Test/HtmlContent.xml", UriKind.Relative);
            control.SourceUri = html;
        }

        [ViewTest]
        public void Visibility__Collapsed(HtmlBlock control) { control.Visibility = Visibility.Collapsed; }

        [ViewTest]
        public void Visibility__Visible(HtmlBlock control) { control.Visibility = Visibility.Visible; }


        [ViewTest]
        public void Change__Offset(HtmlBlock control)
        {
            control.Offset = control.Offset.X == 0 ? new Point(50, 100) : default(Point);
        }

        [ViewTest]
        public void Dispose(HtmlBlock control)
        {
            control.Dispose();
        }
        #endregion

        #region Internal
        private static string GetShortHtml()
        {
            var html = string.Format("<SPAN style=\"font-size:50px; font-family:Verdana;\">{0}</SPAN> <B>{1}</B> {2}",
                                RandomData.LoremIpsum(1),
                                RandomData.LoremIpsum(1),
                                RandomData.LoremIpsum(1));
            return html;
        }
        #endregion
    }
}
