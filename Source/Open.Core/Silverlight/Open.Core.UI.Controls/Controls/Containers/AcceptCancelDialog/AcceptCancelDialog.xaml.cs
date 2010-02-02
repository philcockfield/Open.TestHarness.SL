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
using System.Windows.Input;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>An 'AcceptCancelPresenter' within a 'DialogPresenter' driven by a view-model.</summary>
    public partial class AcceptCancelDialog : UserControl
    {
        #region Head

        public AcceptCancelDialog()
        {
            // Setup initial conditions.
            InitializeComponent();

            // Wire up events.
            KeyDown += OnKeyDown;
        }
        #endregion

        #region Event Handlers
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Setup initial conditions.
            if (ViewModel == null) return;
            if (!ViewModel.IsShowing) return;

            // Cancel on Escape.
            if (e.Key == Key.Escape && ViewModel.IsCancelEnabled) ViewModel.CancelCommand.Execute(null);

            // Accept (OK) on Enter.
            if (e.Key == Key.Enter && ViewModel.IsAcceptEnabled) ViewModel.AcceptCommand.Execute(null);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public IAcceptCancelDialog ViewModel
        {
            get { return DataContext as IAcceptCancelDialog; }
            set { DataContext = value; }
        }
        #endregion
    }
}
