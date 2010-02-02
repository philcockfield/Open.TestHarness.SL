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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Moq;

namespace Open.Core.Common.Test.Extensions
{
    [TestClass]
    public class FrameworkElementExtensionsTest
    {
        [TestMethod]
        public void ShouldFindVisualAncestor()
        {
            var root = new Grid();
            var child1a = new Border();
            var child1b = new TextBlock();
            var child3 = new TextBlock();

            root.Children.Add(child1a);
            root.Children.Add(child1b);
            child1a.Child = child3;

            child3.FindFirstVisualAncestor<Grid>().ShouldBe(root);
            child3.FindFirstVisualAncestor<Border>().ShouldBe(child1a);
            child3.FindFirstVisualAncestor<Border>().ShouldNotBe(child1b);
            child3.FindFirstVisualAncestor<StackPanel>().ShouldBe(null);
        }
    }
}
