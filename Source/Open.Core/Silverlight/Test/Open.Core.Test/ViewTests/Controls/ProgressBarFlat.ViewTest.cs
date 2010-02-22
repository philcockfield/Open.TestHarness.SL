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
using Open.Core.UI.Controls.Controls.ProgressBar;

namespace Open.Core.UI.Silverlight.Test.View_Tests
{
    [ViewTestClass]
    public class ProgressBarFlatViewTest
    {
        #region Head
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ProgressBarFlat control)
        {
            control.Width = 200;
            control.Height = 10;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Background__Orange(ProgressBarFlat control) { control.Background = new SolidColorBrush(Colors.Orange); }

        [ViewTest]
        public void Background__Transarent(ProgressBarFlat control) { control.Background = new SolidColorBrush(Colors.Transparent); }

        [ViewTest]
        public void Set_PercentComplete__0(ProgressBarFlat control) { control.PercentComplete = 0; }

        [ViewTest]
        public void Set_PercentComplete__5(ProgressBarFlat control) { control.PercentComplete = 0.05; }

        [ViewTest]
        public void Set_PercentComplete__25(ProgressBarFlat control) { control.PercentComplete = 0.25; }

        [ViewTest]
        public void Set_PercentComplete__50(ProgressBarFlat control) { control.PercentComplete = 0.5; }

        [ViewTest]
        public void Set_PercentComplete__75(ProgressBarFlat control) { control.PercentComplete = 0.75; }

        [ViewTest]
        public void Set_PercentComplete__100(ProgressBarFlat control) { control.PercentComplete = 1; }

        [ViewTest]
        public void Set_PercentComplete__200(ProgressBarFlat control) { control.PercentComplete = 2; }
        #endregion
    }
}
