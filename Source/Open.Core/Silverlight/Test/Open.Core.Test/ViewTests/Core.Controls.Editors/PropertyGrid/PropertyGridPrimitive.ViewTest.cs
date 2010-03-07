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

using System.Collections.ObjectModel;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.Common.Controls.Editors;
using Open.Core.Common.Controls.Editors.PropertyGridStructure;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Editors
{
    [ViewTestClass]
    public class PropertyGridPrimitiveViewTest
    {
        #region Head
        private Car model;
        private ObservableCollection<PropertyModel> collection;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(PropertyGridPrimitive control)
        {
            control.Width = 300;
            control.Height = 450;
            control.Background = StyleResources.Colors["Mac.Lavender"] as Brush;

            model = new Car{ChildCar = new Car()};
            collection = PropertyModel.GetProperties(model, true);
            control.ViewModel = new PropertyGridPrimitiveViewModel(collection);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Change_Property_Value(PropertyGridPrimitive control)
        {
            model.Brand = RandomData.LoremIpsum(1, 3);
        }

        [ViewTest]
        public void Change_Property_Value_Null(PropertyGridPrimitive control)
        {
            model.Brand = null;
        }

        [ViewTest]
        public void Destroy_ViewModel(PropertyGridPrimitive control)
        {
            control.ViewModel.Dispose();
        }

        [ViewTest]
        public void Change_Properties(PropertyGridPrimitive control)
        {
            var person = new Person();
            var properties = PropertyModel.GetProperties(person, true);

            collection.RemoveAll();
            foreach (var item in properties) collection.Add(item);
        }
        #endregion
    }
}
