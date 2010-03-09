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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Common.Extensions
{
    [TestClass]
    public class VisualTreeExtensionsTest 
    {
        #region Tests
        [TestMethod]
        public void ShouldGetParent()
        {
            var element = new Border();
            var canvas = new Canvas();

            element.GetParentVisual().ShouldBe(null);

            canvas.Children.Add(element);
            element.GetParentVisual().ShouldBe(canvas);

            canvas.Children.Remove(element);
            element.GetParentVisual().ShouldBe(null);
        }

        [TestMethod]
        public void ShouldFindAncestorOfType()
        {
            var level1 = new Grid();
            var level2 = new Border();
            var level3 = new TextBlock();

            level1.Children.Add(level2);
            level2.Child = level3;

            level3.FindFirstVisualAncestor<Grid>().ShouldBe(level1);
            level3.FindFirstVisualAncestor<Border>().ShouldBe(level2);
            level3.FindFirstVisualAncestor<Canvas>().ShouldBe(null);
        }

        [TestMethod]
        public void ShouldFindFirstChildOfType()
        {
            var level1 = new Grid();
            var level2 = new Canvas();
            var level3a = new Border();
            var level3b = new Border();
            var level4 = new TextBlock();

            level1.Children.Add(level2);
            level2.Children.Add(level3a);
            level2.Children.Add(level3b);
            level3b.Child = level4;

            level1.FindFirstChildOfType<Canvas>().ShouldBe(level2);
            level1.FindFirstChildOfType<Border>().ShouldBe(level3a);
            level1.FindFirstChildOfType<TextBlock>().ShouldBe(level4);
            level1.FindFirstChildOfType<Grid>().ShouldBe(null);
        }

        [TestMethod]
        public void ShouldRemoveFromVisualTreeInGrid()
        {
            var item = new TextBlock();
            var parent = new Grid();
            parent.Children.Add(item);

            item.Parent.ShouldBe(parent);
            item.RemoveFromVisualTree();
            item.Parent.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldRemoveFromVisualTreeInBorder()
        {
            var item = new TextBlock();
            var parent = new Border {Child = item};

            item.Parent.ShouldBe(parent);
            item.RemoveFromVisualTree();
            item.Parent.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldNotFailWhenRemoveFromVisualTreeIsCalled()
        {
            ((UIElement)null).RemoveFromVisualTree();
           new TextBlock().RemoveFromVisualTree();
        }
        #endregion
    }
}
