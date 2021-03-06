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

using Open.Core.Common;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Trigger
{
    [ViewTestClass]
    public class ExecuteMethodViewTest
    {
        #region Head
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ExecuteMethodTestControl control)
        {
            control.Width = 200;
            control.Height = 200;
            control.DataContext = new StubViewModel();
        }
        #endregion

        #region Stubs
        public class StubViewModel : ViewModelBase
        {
            #region Head

            private int counter;
            private string text = "Click to Execute";
            #endregion

            #region Properties
            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged<StubViewModel>(m => m.Text); }
            }
            #endregion

            #region Methods
            public void IncrementCounter()
            {
                counter++;
                Text = string.Format("Counter: " + counter);
            }
            #endregion
        }
        #endregion
    }
}
