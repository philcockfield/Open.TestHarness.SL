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
using Open.Core.UI.Controls;
using Open.Core.UI.Silverlight.Controls;

namespace Open.TestHarness.Test.Model
{
    [ViewTestClass]
    public class SampleViewTestClass2
    {
        #region Head
        public const string PropMyMethod = "MyMethod";
        public const string PropMethod_With_Underscores = "Method_With_Underscores";
        #endregion

        #region Methods
        [ViewTest]
        public void MyMethod()
        {
        }

        [ViewTest]
        public void MyMethod5(Placeholder control1, Placeholder control2, Placeholder control3, Placeholder control4, Placeholder control5)
        {
            control2.Width = 100;
        }

        [ViewTest]
        public void Method_With_Underscores(Placeholder control1, Placeholder control2)
        {
        }
        #endregion
    }
}
