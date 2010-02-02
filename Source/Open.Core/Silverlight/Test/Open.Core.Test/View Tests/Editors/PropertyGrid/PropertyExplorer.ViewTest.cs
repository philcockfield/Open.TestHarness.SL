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
using System.Collections.Generic;
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
using Open.Core.Common.Controls.Editors;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Editors
{
    [ViewTestClass]
    public class PropertyExplorerViewTest
    {
        #region Head
        private readonly Car carInstance = new Car();
        private readonly Person personInstance = new Person();

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(PropertyExplorer control)
        {
            control.Width = 350;
            control.Height = 550;
            control.BorderBrush = StyleResources.Colors["Brush.Black.050"] as Brush;
            control.BorderThickness = new Thickness(1);
            control.ViewModel = new PropertyExplorerViewModel();
        }
        #endregion

        #region Tests
        [ViewTest]
        public void SelectedObject_Person(PropertyExplorer control)
        {
            control.ViewModel.SelectedObject = personInstance;
        }

        [ViewTest]
        public void SelectedObject_Car(PropertyExplorer control)
        {
            control.ViewModel.SelectedObject = carInstance;
        }

        [ViewTest]
        public void SelectedObject_PropertyGrid_Self(PropertyExplorer control)
        {
            control.ViewModel.SelectedObject = control;
        }

        [ViewTest]
        public void SelectedObject_Null(PropertyExplorer control)
        {
            control.ViewModel.SelectedObject = null;
        }

        [ViewTest]
        public void Setup_Custom_Categories(PropertyExplorer control)
        {
            control.ViewModel.IncludeHierarchy = true;
            control.ViewModel.SelectedObject = null;
            var names = new List<string> { "IsEnabled", "Width", "Height" };

            control.ViewModel.GetCategory = p => names.Contains(p.Definition.Name) ? "Category 1" : "Category 2";
            control.ViewModel.SelectedObject = control;
        }
        #endregion
    }
}
