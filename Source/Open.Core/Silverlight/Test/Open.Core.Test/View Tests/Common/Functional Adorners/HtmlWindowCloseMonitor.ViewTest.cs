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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Open.Core.Common;
using System.Diagnostics;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Functional_Adorners
{
    [ViewTestClass]
    public class HtmlWindowCloseMonitorViewTest
    {
        #region Head
        private string dialogMessage = "Message from Event Handler";
        private int count;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize()
        {
            Register_Cancelling_Handler();
        }
        #endregion

        #region Event Handlers
        private void CancelOnClose(object sender, HtmlWindowCloseEventArgs e)
        {
            Debug.WriteLine("!! Event: WindowClosing (Cancel Handler)");
            e.DialogMessage = GetMessage();
            e.Cancel = true;
        }

        private void DontCancelOnClose(object sender, HtmlWindowCloseEventArgs e)
        {
            Debug.WriteLine("!! Event: WindowClosing (Non-Cancel Handler)");
            e.Cancel = false;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Register_Cancelling_Handler()
        {
            Unregister_Handlers();
            HtmlWindowCloseMonitor.WindowClosing += CancelOnClose;
            Debug.WriteLine("Cancelling Handler Registered");
        }

        [ViewTest]
        public void Register_NonCancelling_Handler()
        {
            Unregister_Handlers();
            HtmlWindowCloseMonitor.WindowClosing += DontCancelOnClose;
            Debug.WriteLine("Non-Cancelling Handler Registered");
        }

        [ViewTest]
        public void Unregister_Handlers()
        {
            HtmlWindowCloseMonitor.WindowClosing -= CancelOnClose;
            HtmlWindowCloseMonitor.WindowClosing -= DontCancelOnClose;
            Debug.WriteLine("All Handlers Removed");
        }

        [ViewTest]
        public void Clear_EventHander_DialogMessage()
        {
            dialogMessage = null;
        }
        #endregion

        #region Internal
        private string GetMessage()
        {
            if (dialogMessage == null) return null;
            count++;
            return string.Format("{0} ({1})", dialogMessage, count);
        }
        #endregion
    }
}
