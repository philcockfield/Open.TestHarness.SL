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
using Open.Core.UI.Controls;

namespace Open.TestHarness.Test.Model
{
    [ViewTestClass]
    public class SampleViewTestClass3
    {
        #region Head
        private int loaded1;
        private int loaded2;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(Placeholder control1, Placeholder control2)
        {
            control1.Loaded += delegate
                                   {
                                       loaded1++;
                                       control1.Text = string.Format("A. Loaded:{0}", loaded1);
                                   };

            control2.Loaded += delegate
                                    {
                                        loaded2++;
                                        control2.Text = string.Format("B. Loaded:{0}", loaded2);
                                    };
        }
        #endregion

        #region Methods
        [ViewTest]
        public void Same1(Placeholder control1, Placeholder control2)
        {
        }

        [ViewTest]
        public void Same2(Placeholder control1, Placeholder control2)
        {
        }

        [ViewTest]
        public void Different(Placeholder control1)
        {
        }
        #endregion
    }
}
