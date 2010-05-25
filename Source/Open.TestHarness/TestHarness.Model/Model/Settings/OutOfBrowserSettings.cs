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
using Open.Core.Common;
using T = Open.TestHarness.Model.OutOfBrowserSettings;

namespace Open.TestHarness.Model
{
    /// <summary>Settings related to the out of browser window.</summary>
    public class OutOfBrowserSettings : IsolatedStorageModelBase
    {
        #region Head
        /// <summary>Constructor.</summary>
        public OutOfBrowserSettings() : base(IsolatedStorageType.Application, "TestHarness.OutOfBrowserSettings")
        {
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the size of the window.</summary>
        public Size WindowSize
        {
            get { return GetPropertyValue<T, Size>(m => m.WindowSize, new Size(1152, 864)); }
            set { SetPropertyValue<T, Size>(m => m.WindowSize, value, new Size(1152, 864)); }
        }
        #endregion

        #region Properties - Internal
        private static bool IsOutOfBrowser { get { return Application.Current.IsRunningOutOfBrowser; } }
        private static Window Window { get { return IsOutOfBrowser ? null : Application.Current.MainWindow; } }
        #endregion

        #region Methods
        /// <summary>Initializes the window (called at startup).</summary>
        public void InitializeWindow()
        {
            if (!IsOutOfBrowser) return;
            Window.Width = WindowSize.Width;
            Window.Height = WindowSize.Height;
        }

        /// <summary>Saves the current state of the window.</summary>
        public void SaveWindowState()
        {


            MessageBox.Show(WindowSize.ToString() + " | " + Window.Width
                + " | " + IsOutOfBrowser); //TEMP 
            if (!IsOutOfBrowser) return;
            WindowSize = new Size(Window.Width, Window.Height);
            Save();

        }
        #endregion
    }
}
