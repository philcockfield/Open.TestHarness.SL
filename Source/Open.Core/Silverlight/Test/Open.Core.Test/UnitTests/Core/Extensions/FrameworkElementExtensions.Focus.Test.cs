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

using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.Common;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Extensions
{
    [TestClass]
    public class FrameworkElementExtensionsFocusTest
    {
        #region Head
        private Grid root;
        private Grid child1;
        private Grid child2;
        private Grid child3;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;

        [TestInitialize]
        public void TestSetup()
        {
            // Create elements.
            root = new Grid();
            child1 = new Grid();
            child2 = new Grid();
            child3 = new Grid();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();

            // Construct hierarchy.
            root.Children.Add(child1);
            root.Children.Add(child2);
            root.Children.Add(child3);

            child2.Children.Add(textBox1);
            child2.Children.Add(textBox2);

            child3.Children.Add(textBox3);
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldReturnNullWhenNullPassed()
        {
            ((FrameworkElement)null).GetDefaultFocusChild().ShouldBe(null);
        }

        [TestMethod]
        public void ShouldFindFirstChildAtSameLevel()
        {
            Focus.SetIsDefault(textBox1, true);
            Focus.SetIsDefault(textBox2, true);
            root.GetDefaultFocusChild().ShouldBe(textBox1);
        }

        [TestMethod]
        public void ShouldFindFirstChildAtDifferentLevels()
        {
            Focus.SetIsDefault(textBox1, true);
            Focus.SetIsDefault(textBox3, true);
            root.GetDefaultFocusChild().ShouldBe(textBox1);
        }
        #endregion
    }
}
