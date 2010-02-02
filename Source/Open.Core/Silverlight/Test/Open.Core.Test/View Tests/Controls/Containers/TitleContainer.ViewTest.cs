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
using System.Windows.Media;
using System.Diagnostics;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.Core.UI.Silverlight.Controls;

namespace Open.Core.UI.Silverlight.Test.Controls
{
    [ViewTestClass]
    public class TitleContainerViewTest
    {
        #region Head
        private Placeholder child;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(TitleContainer control)
        {
            control.Width = 300;

            child = new Placeholder{Text = "My Child", Height = 250};

            control.Title = "My Title";
            control.Child = child;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Toggle_IsOpen(TitleContainer control)
        {
            control.IsOpen = !control.IsOpen;
            Debug.WriteLine("IsOpen" + control.IsOpen);
        }

        [ViewTest]
        public void Animate_Default_Speed(TitleContainer control)
        {
            control.AnimationDuration = 0.15;
        }

        [ViewTest]
        public void Animate_Slow(TitleContainer control)
        {
            control.AnimationDuration = 1;
        }

        [ViewTest]
        public void Toggle_AnimateIcon(TitleContainer control)
        {
            control.AnimateIcon = !control.AnimateIcon;
            Debug.WriteLine("AnimateIcon: " + control.AnimateIcon);
        }

        [ViewTest]
        public void Child_Height_0(TitleContainer control)
        {
            child.Height = 0;
        }

        [ViewTest]
        public void Child_Height_10(TitleContainer control)
        {
            child.Height = 10;
        }

        [ViewTest]
        public void Child_Height_250(TitleContainer control)
        {
            child.Height = 250;
        }

        [ViewTest]
        public void Child_Height_Random(TitleContainer control)
        {
            child.Height = RandomData.Random.Next(10, 300);
        }

        [ViewTest]
        public void ToggleIsOpenOn(TitleContainer control)
        {
            control.ToggleIsOpenOn = control.ToggleIsOpenOn == ClickGesture.SingleClick
                                                        ? ClickGesture.DoubleClick 
                                                        : ClickGesture.SingleClick;
            Debug.WriteLine("ToggleIsOpenOn: " + control.ToggleIsOpenOn);
        }
        #endregion
    }
}
