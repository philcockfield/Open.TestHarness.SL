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
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Controls.Editors;
using Open.Core.Common.Testing;
using Open.Core.UI.Silverlight.Test.View_Tests.Editors;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Editors
{
    [TestClass]
    public class PropertyGridViewModelTest
    {
        #region Tests
        [TestMethod]
        public void ShouldPopulateCategoriesFromAttributeWhenSelectedObjectSet()
        {
            var model = new PropertyGridViewModel();
            model.SelectedObject.ShouldBe(null);
            model.Categories.Count.ShouldBe(0);

            model.SelectedObject = new Sample1();
            model.Categories.Count.ShouldBe(2);

            model.Categories.FirstOrDefault(item => item.CategoryName == "Miscellaneous").ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldClearCategoriesWhenSelectedObjectSetToNull()
        {
            var model = new PropertyGridViewModel {SelectedObject = new Sample1()};
            model.Categories.Count.ShouldNotBe(0);

            model.SelectedObject = null;
            model.Categories.Count.ShouldBe(0);
        }


        [TestMethod]
        public void ShouldDestroyOldCategories()
        {
            var model = new PropertyGridViewModel { SelectedObject = new Sample1() };
            var propViewModel = model.Categories[0].GridViewModel.Properties[0];

            propViewModel.IsDestroyed.ShouldBe(false);

            model.SelectedObject = null;

            propViewModel.IsDestroyed.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldChangeSelectedObjectOnModelWhenControlChanges()
        {
            var control = new Core.Common.Controls.Editors.PropertyGrid();
            var model = control.ViewModel;
            
            control.SelectedObject.ShouldBe(null);
            model.SelectedObject.ShouldBe(null);

            control.SelectedObject = new Car();
            model.SelectedObject.ShouldBe(control.SelectedObject);
        }
        #endregion

        #region Sample Data
        public class Sample1
        {
            [Category("Cat1")]
            public string Text1 { get; set; }

            [Category("Cat1")]
            public string Text2 { get; set; }

            public string Text3 { get; set; }
        }
        #endregion
    }
}
