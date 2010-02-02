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
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Editors
{
    [ViewTestClass]
    public class PropertyGridViewTest
    {
        #region Head
        private readonly Car modelCar = new Car();
        private readonly BoolModel modelBool = new BoolModel();
        private readonly StringModel modelString = new StringModel();
        private readonly Person modelPerson = new Person();

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(PropertyGrid control)
        {
            control.Width = 350;
            control.Height = 550;
            control.Background = StyleResources.Colors["Mac.Lavender"] as Brush;

            SelectedObject_Car(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void SelectedObject_Car(PropertyGrid control)
        {
            control.SelectedObject = modelCar;
        }

        [ViewTest]
        public void SelectedObject_BoolModel(PropertyGrid control)
        {
            control.SelectedObject = modelBool;
        }

        [ViewTest]
        public void SelectedObject_StringModel(PropertyGrid control)
        {
            control.SelectedObject = modelString;
        }

        [ViewTest]
        public void SelectedObject_Person(PropertyGrid control)
        {
            control.SelectedObject = modelPerson;
        }

        [ViewTest]
        public void SelectedObject_UserControl(PropertyGrid control)
        {
            control.SelectedObject = control;
        }

        [ViewTest]
        public void SelectedObject_Null(PropertyGrid control)
        {
            control.SelectedObject = null;
        }

        [ViewTest]
        public void Toggle_IncludeHierarchy(PropertyGrid control)
        {
            control.IncludeHierarchy = !control.IncludeHierarchy;
        }

        [ViewTest]
        public void Toggle_FilterByPropertyName(PropertyGrid control)
        {
            control.FilterByPropertyName = control.FilterByPropertyName == null ? "is" : null;
        }

        [ViewTest]
        public void Refresh(PropertyGrid control)
        {
            control.Refresh();
        }

        [ViewTest]
        public void Setup_Custom_Categories(PropertyGrid control)
        {
            control.IncludeHierarchy = true;
            control.SelectedObject = null;
            var names = new List<string> { "IsEnabled", "Width", "Height" };

            control.ViewModel.GetCategory = p => names.Contains(p.Definition.Name) ? "Category 1" : "Category 2";
            control.SelectedObject = control;
        }
        #endregion
    }
}
