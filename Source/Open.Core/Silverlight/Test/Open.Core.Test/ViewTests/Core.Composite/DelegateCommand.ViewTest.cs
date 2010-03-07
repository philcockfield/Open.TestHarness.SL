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

using System.Diagnostics;
using System.Windows.Controls;
using Open.Core.Common;
using Open.Core.Composite.Command;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Composite
{
    [ViewTestClass]
    public class DelegateCommandViewTest
    {
        #region Head
        private ViewModel viewModel;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(DelegateCommandTestControl control)
        {
            viewModel = new ViewModel();
            control.DataContext = viewModel;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void IsEnabled__True(DelegateCommandTestControl control)
        {
            viewModel.IsEnabled = true;
            Debug.WriteLine("IsEnabled: " + viewModel.IsEnabled);
        }

        [ViewTest]
        public void IsEnabled__False(DelegateCommandTestControl control)
        {
            viewModel.IsEnabled = false;
            Debug.WriteLine("IsEnabled: " + viewModel.IsEnabled);
        }
        #endregion

        public class ViewModel : ViewModelBase
        {
            #region Event Handlers
            private void OnClick()
            {
                Debug.WriteLine("Click");
            }
            #endregion

            #region Properties
            /// <summary>Gets or sets .</summary>
            public DelegateCommand<Button> MyCommand
            {
                get
                {
                    return GetPropertyValue<ViewModel, DelegateCommand<Button>>(
                        m => m.MyCommand,
                        new DelegateCommand<Button>(button => OnClick(), m => IsEnabled));
                }
            }

            public bool IsEnabled
            {
                get { return GetPropertyValue<ViewModel, bool>(m => m.IsEnabled, true); }
                set
                {
                    SetPropertyValue<ViewModel, bool>(m => m.IsEnabled, value, true);
                    MyCommand.RaiseCanExecuteChanged();
                }
            }
            #endregion
        }
    }
}
