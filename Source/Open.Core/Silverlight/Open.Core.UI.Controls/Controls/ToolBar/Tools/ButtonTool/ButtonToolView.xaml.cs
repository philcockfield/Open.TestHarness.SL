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

using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    public partial class ButtonToolView : UserControl
    {
        #region Head
        /// <summary>Constructor.</summary>
        public ButtonToolView()
        {
            // Setup initial conditions.
            InitializeComponent();

            // Wire up events.
            IsEnabledChanged += delegate { SyncIsEnabled(); };
            Loaded += delegate { SyncIsEnabled(); };
            MouseEnter += delegate { if (ViewModel != null) ViewModel.OnMouseEnter(); };
            MouseLeave += delegate { if (ViewModel != null) ViewModel.OnMouseLeave(); };
            MouseLeftButtonDown += delegate
                                       {
                                           CaptureMouse();
                                           if (ViewModel != null) ViewModel.OnMouseDown();
                                       };
            MouseLeftButtonUp += delegate
                                     {
                                         ReleaseMouseCapture();
                                         if (ViewModel != null) ViewModel.OnMouseUp();
                                     };
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public ButtonToolViewModel ViewModel
        {
            get { return DataContext as ButtonToolViewModel; }
            set { DataContext = value; }
        }
        #endregion

        #region Internal
        private void SyncIsEnabled( )
        {
            if (ViewModel != null) ViewModel.IsViewEnabled = IsEnabled;
        }
        #endregion
    }
}
