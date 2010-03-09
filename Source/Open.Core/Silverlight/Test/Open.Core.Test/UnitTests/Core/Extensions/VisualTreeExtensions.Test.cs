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
using System.Collections.ObjectModel;
using System.Net;
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
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Extensions
{
    [Tag("current")]
    [TestClass]
    public class VisualTreeExtensionsTest : SilverlightUnitTest
    {
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
            var parent = new Border { Child = item };

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



    }
}
