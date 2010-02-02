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
using Open.Core.Common.AttachedBehavior;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Behavior
{
    [ViewTestClass]
    public class Behavior__UpdateOnKeyPressViewTest
    {
        #region Head
        private readonly UpdateOnKeyPress behavior = new UpdateOnKeyPress();
        private readonly SampleModel model = new SampleModel();

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(UpdateOnKeyPressTestControl control)
        {
            control.Width = 200;
            Behaviors.SetUpdateOnKeyPress(control.textbox, behavior);

            behavior.Updated += delegate
                                    {
                                        Debug.WriteLine("Updated - model.Text: " + model.Text);
                                    };

            Set_DataContext(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Set_DataContext(UpdateOnKeyPressTestControl control)
        {
            control.DataContext = model;
        }

        [ViewTest]
        public void Set_DataContext_Null(UpdateOnKeyPressTestControl control)
        {
            control.DataContext = null;
        }

        [ViewTest]
        public void Set_Delay_0(UpdateOnKeyPressTestControl control) { behavior.Delay = 0; }

        [ViewTest]
        public void Set_Delay_Fast(UpdateOnKeyPressTestControl control) { behavior.Delay = 0.1; }

        [ViewTest]
        public void Set_Delay_Slow(UpdateOnKeyPressTestControl control) { behavior.Delay = 1; }

        [ViewTest]
        public void Toggle_IsActive(UpdateOnKeyPressTestControl control)
        {
            behavior.IsActive = !behavior.IsActive;
            Debug.WriteLine("IsActive: " + behavior.IsActive);
        }
        #endregion

        #region Sample Data
        public class SampleModel : ModelBase
        {
            public const string PropText = "Text";
            private string text = "My Value";
            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged(PropText); }
            }
        }
        #endregion
    }
}
