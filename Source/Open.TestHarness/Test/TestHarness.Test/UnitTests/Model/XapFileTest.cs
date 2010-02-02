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
using Open.Core.Common.Testing;
using Open.TestHarness.Model;

namespace Open.TestHarness.Test.Unit_Tests.Model
{
    [TestClass]
    public class XapFileTest
    {
        #region Tests
        [TestMethod]
        public void ShouldStripXapExtensionFromName()
        {
            var file = new XapFile();
            file.Name.ShouldBe(null);

            file.Name = "FileName.xap";
            file.Name.ShouldBe("FileName");

            file.Name = "FileName.XAP";
            file.Name.ShouldBe("FileName");

            file.Name = "FileName.Xap";
            file.Name.ShouldBe("FileName");

            file.Name = "  ";
            file.Name.ShouldBe(null);

            file.Name = null;
            file.Name.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldNotAllowNegativeSize()
        {
            var file = new XapFile();
            file.Kilobytes.ShouldBe(0.0);

            file.Kilobytes = -10;
            file.Kilobytes.ShouldBe(0.0);

            file.Kilobytes = 5;
            file.Kilobytes.ShouldBe(5.0);
        }
        #endregion
    }
}
