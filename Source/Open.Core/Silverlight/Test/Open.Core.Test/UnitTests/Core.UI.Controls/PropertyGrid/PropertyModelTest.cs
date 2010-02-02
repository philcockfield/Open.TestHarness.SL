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
using System.Linq;
using System.ComponentModel;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Controls.Editors;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Editors
{
    [TestClass]
    public class PropertyModelTest
    {
        #region Tests
        [TestMethod]
        public void ShouldHaveDefinitionAndInstance()
        {
            var instance = new Sample();
            var propModel = new PropertyModel(instance, instance.GetType().GetProperty("Text"));

            propModel.Definition.Name.ShouldBe("Text");
            propModel.ParentInstance.ShouldBe(instance);
        }

        [TestMethod]
        public void ShouldGetAllPropertiesOnAModel()
        {
            var obj = new SampleDerived();
            var propModels = PropertyModel.GetProperties(obj, true);

            propModels.Count().ShouldNotBe(0);
            propModels.FirstOrDefault(item => item.Definition.Name == "Text").ShouldNotBe(null);
            propModels.FirstOrDefault(item => item.Definition.Name == "Number").ShouldNotBe(null);
            propModels.FirstOrDefault(item => item.Definition.Name == "Date").ShouldNotBe(null);

            propModels = PropertyModel.GetProperties(obj, false);
            propModels.FirstOrDefault(item => item.Definition.Name == "Date").ShouldNotBe(null);
            propModels.FirstOrDefault(item => item.Definition.Name == "Text").ShouldBe(null);
            propModels.FirstOrDefault(item => item.Definition.Name == "Number").ShouldBe(null);
        }

        [TestMethod]
        public void ShouldGetValue()
        {
            var instance = new Sample();
            var propModel = new PropertyModel(instance, instance.GetType().GetProperty("Text"));
            propModel.Value.ShouldBe(null);

            instance.Text = "Hello";
            propModel.Value.ShouldBe("Hello");
        }

        [TestMethod]
        public void ShouldSetValue()
        {
            var instance = new Sample();
            var propModel = new PropertyModel(instance, instance.GetType().GetProperty("Text"));
            propModel.Value.ShouldBe(null);

            propModel.Value = "Hello";
            instance.Text.ShouldBe("Hello");
        }

        [TestMethod]
        public void ShouldGetCategoryAttribute()
        {
            var instance = new Sample();

            var propModel = new PropertyModel(instance, instance.GetType().GetProperty("Text"));
            propModel.CategoryAttribute.Category.ShouldBe("Common");

            propModel = new PropertyModel(instance, instance.GetType().GetProperty("Number"));
            propModel.CategoryAttribute.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldGetPropertyGridAttribute()
        {
            var instance = new Sample();

            var propModel = new PropertyModel(instance, instance.GetType().GetProperty("Text"));
            propModel.PropertyGridAttribute.Name.ShouldBe("My Text");
            propModel.DisplayName.ShouldBe("My Text");

            propModel = new PropertyModel(instance, instance.GetType().GetProperty("Number"));
            propModel.PropertyGridAttribute.ShouldBe(null);
            propModel.DisplayName.ShouldBe("Number");
        }
        #endregion

        #region Sample Data
        public class Sample : ModelBase
        {
            public const string PropText = "Text";
            private string text;

            [Category("Common")]
            [PropertyGrid(Name = "My Text")]
            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged(PropText); }
            }

            public double Number { get; set; }
        }

        public class SampleDerived : Sample
        {
            public DateTime Date { get; set; }
        }
        #endregion
    }
}
