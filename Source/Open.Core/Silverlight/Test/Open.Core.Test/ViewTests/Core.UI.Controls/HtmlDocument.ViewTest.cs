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
using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls
{
    [ViewTestClass]
    public class HtmlDocumentViewTest
    {
        #region Head
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(HtmlDocument control)
        {
            control.Width = 600;
            control.Height = 550;
            Set_SourceUri__Url_1(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Set_SourceUri__Url_1(HtmlDocument control)
        {
            SetSource(control, "http://martinfowler.com/eaaDev/PresentationModel.html");
        }

        [ViewTest]
        public void Set_SourceUri__Url_2(HtmlDocument control)
        {
            SetSource(control, "http://martinfowler.com/eaaDev/EventAggregator.html");
        }

        [ViewTest]
        public void Set_SourceUri__Url_Null(HtmlDocument control)
        {
            control.SourceUri = null;
        }

        [ViewTest]
        public void Dispose(HtmlDocument control)
        {
            control.Dispose();
        }
        #endregion

        #region Internal
        private static void SetSource(HtmlDocument control, string url)
        {
            control.SourceUri = new Uri(url);
            Output.Write("SourceUri: " + url);
        }
        #endregion
    }
}
