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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Attributes
{
    [TestClass]
    public class ViewTestClassAttributeTest
    {
        #region Test
        [TestMethod]
        public void ShouldReportDisplayNameFromClassNameWithSpaces()
        {
            var instance = new Sample_Class();
            var attribute = instance.GetType().GetCustomAttributes(typeof(ViewTestClassAttribute), false).FirstOrDefault() as ViewTestClassAttribute;
            Assert.IsNotNull(attribute);

            Assert.AreEqual(null, attribute.DisplayName);
            Assert.AreEqual("Sample Class", ViewTestClassAttribute.GetDisplayName(instance));
        }

        [TestMethod]
        public void ShouldReportDisplayNameFromAttribute()
        {
            var instance = new SampleClass();
            Assert.AreEqual("My Name", ViewTestClassAttribute.GetDisplayName(instance));
        }


        [TestMethod][ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowErrorWhenNonDecoratedClassPassedToGetDisplayName()
        {
            ViewTestClassAttribute.GetDisplayName("not an instance");
        }

        [TestMethod]
        public void ShouldShowPropertyExplorerByDefault()
        {
            var attr = new ViewTestClassAttribute();
            attr.IsPropertyExplorerVisible.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldNotShowPropertyExplorer()
        {
            var attr = new ViewTestClassAttribute{IsPropertyExplorerVisible = false};
            attr.IsPropertyExplorerVisible.ShouldBe(false);
        }
        #endregion

        #region Sample Data
        [ViewTestClass]
        private class Sample_Class
        {
        }

        [ViewTestClass(DisplayName = "My Name")]
        private class SampleClass
        {
        }
        #endregion
    }
}
