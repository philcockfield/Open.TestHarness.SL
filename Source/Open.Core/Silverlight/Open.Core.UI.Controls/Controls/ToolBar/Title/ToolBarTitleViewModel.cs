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
using Open.Core.Common;

using T = Open.Core.UI.Controls.ToolBarTitleViewModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(IToolBarTitle))]
    public class ToolBarTitleViewModel : ViewModelBase, IToolBarTitle
    {
        #region Head
        private const string defaultTitle = "Name";
        #endregion

        #region Properties : IToolBarTitle
        public string Name
        {
            get { return GetPropertyValue<T, string>(m => m.Name, defaultTitle); }
            set { SetPropertyValue<T, string>(m => m.Name, value, defaultTitle); }
        }

        public bool IsVisible
        {
            get { return GetPropertyValue<T, bool>(m => m.IsVisible, true); }
            set { SetPropertyValue<T, bool>(m => m.IsVisible, value, true); }
        }
        #endregion

        #region Methods
        public FrameworkElement CreateView()
        {
            return new ToolBarTitle {ViewModel = this};
        }
        #endregion
    }
}
