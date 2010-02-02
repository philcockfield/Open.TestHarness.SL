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

using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;
using Open.Core.UI.Silverlight.Controls;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Animation
{
    [ViewTestClass]
    public class AnimationBehaviorViewTest
    {
        #region Head
        private readonly SampleViewModel sampleViewModel = new SampleViewModel();

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(AnimationBehaviorTestControl control)
        {
            control.DataContext = sampleViewModel;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void RotateAnimation__Toggle_Angle(AnimationBehaviorTestControl control)
        {
            sampleViewModel.Angle = sampleViewModel.Angle == 0 ? 120 : 0;
        }

        [ViewTest]
        public void SizeAnimation__Toggle_Width(AnimationBehaviorTestControl control)
        {
            sampleViewModel.Width = sampleViewModel.Width == 100 ? 5 : 100;
        }

        [ViewTest]
        public void SizeAnimation__Toggle_Height(AnimationBehaviorTestControl control)
        {
            sampleViewModel.Height = sampleViewModel.Height == 100 ? 5 : 100;
        }

        [ViewTest]
        public void OpacityAnimation__Toggle_Opacity(AnimationBehaviorTestControl control)
        {
            sampleViewModel.Opacity = sampleViewModel.Opacity == 1 ? 0.3 : 1;
        }
        #endregion

        #region Sample View Model
        public class SampleViewModel : ViewModelBase
        {
            private double width = 100;
            private double height = 100;
            private double angle;
            private double opacity = 1;

            public double Width
            {
                get { return width; }
                set { width = value; OnPropertyChanged("Width"); }
            }

            public double Height
            {
                get { return height; }
                set { height = value; OnPropertyChanged("Height"); }
            }
            public double Angle
            {
                get { return angle; }
                set { angle = value; OnPropertyChanged("Angle"); }
            }

            public double Opacity
            {
                get { return opacity; }
                set { opacity = value; OnPropertyChanged("Opacity"); }
            }
        }
        #endregion
    }
}
