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

using System.Windows.Media;
using Open.Core.Common;
using Open.Core.Common.Controls.Editors;
using System.Diagnostics;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Editors
{
    [ViewTestClass]
    public class ColorSelectorViewTest
    {
        #region Head
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ColorSelector control)
        {
            control.ColorChanged += delegate { Debug.WriteLine("!! ColorChanged - " + control.Color.ToColorString()); };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Color_Red(ColorSelector control)
        {
            control.Color = Colors.Red;
        }

        [ViewTest]
        public void Color_Brown(ColorSelector control)
        {
            control.Color = Colors.Brown;
        }

        [ViewTest]
        public void Color_LightGray(ColorSelector control)
        {
            control.Color = Colors.LightGray;
        }

        [ViewTest]
        public void Color_Blue(ColorSelector control)
        {
            control.Color = Colors.Blue;
        }
        #endregion
    }
}
