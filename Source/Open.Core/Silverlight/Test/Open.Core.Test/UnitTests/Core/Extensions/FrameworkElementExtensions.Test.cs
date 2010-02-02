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
using System.Windows.Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;

using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.Common.Extensions
{
    [TestClass]
    public class FrameworkElementExtensionsTest : SilverlightTest
    {
        [TestMethod]
        public void ShouldSetPosition()
        {
            var element = new Placeholder();
            var canvas = new Canvas();
            canvas.Children.Add(element);

            canvas.GetChildPosition(element).ShouldBe(new Point(0,0));

            canvas.SetPosition(element, 10, 5);
            canvas.GetChildPosition(element).ShouldBe(new Point(10, 5));

            canvas.SetPosition(element, new Point(20, 40));
            canvas.GetChildPosition(element).ShouldBe(new Point(20, 40));
        }

        [TestMethod]
        public void ShouldGetDataContext()
        {
            var element = new Placeholder{DataContext = null};
            var model = new ObservableCollection<string>();

            element.GetDataContext().ShouldBe(null);

            element.DataContext = model;
            element.GetDataContext().ShouldBe(model);

            var shape = new Rectangle();
            shape.GetDataContext().ShouldBe(null);
        }


    }
}
