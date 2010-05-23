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
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.Core.UI.Silverlight.Controls;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Containers
{
    [ViewTestClass]
    public class CollapsingContentViewTest
    {
        #region Head
        private Placeholder child;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(CollapsingContent control)
        {
            control.Width = 250;
            child = new Placeholder {Height = 250};
            control.Content = child;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Toggle_IsOpen(CollapsingContent control)
        {
            control.IsOpen = !control.IsOpen;
        }

        [ViewTest]
        public void Animate_Fast(CollapsingContent control)
        {
            control.AnimationDuration = 0.15;
            Debug.WriteLine("AnimationDuration: " + control.AnimationDuration);
        }

        [ViewTest]
        public void Animate_Slow(CollapsingContent control)
        {
            control.AnimationDuration = 1.5;
            Debug.WriteLine("AnimationDuration: " + control.AnimationDuration);
        }

        [ViewTest]
        public void Child_Height_0(CollapsingContent control)
        {
            child.Height = 0;
        }

        [ViewTest]
        public void Child_Height_10(CollapsingContent control)
        {
            child.Height = 10;
        }

        [ViewTest]
        public void Child_Height_250(CollapsingContent control)
        {
            child.Height = 250;
        }

        [ViewTest]
        public void Child_Height_300(CollapsingContent control)
        {
            child.Height = 300;
        }

        [ViewTest]
        public void Child_Height_Random(CollapsingContent control)
        {
            child.Height = RandomData.Random.Next(10, 300);
        }
        #endregion
    }
}
