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
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Extensions
{
    [TestClass]
    public class XExtensionsTest
    {
        #region Tests
        [TestMethod]
        public void ShouldGetElementChildAsDouble()
        {
            XElement root = null;
            root.GetChildValueAsDouble("child", -1d).ShouldBe(-1d);

            root = new XElement("root", new XElement("child", 1.5d));

            root.GetChildValueAsDouble("child", 0).ShouldBe(1.5d);
            root.GetChildValueAsDouble("not-a-node", 0d).ShouldBe(0d);
        }

        [TestMethod]
        public void ShouldThowErrorWhenGettingDoubleChildInWrongFormat()
        {
            var root = new XElement("root", new XElement("child", "text value"));
            Should.Throw<FormatException>(() => root.GetChildValueAsDouble("child", 0));
        }

        [TestMethod]
        public void ShouldGetAttributeValue()
        {
            var element = new XElement("root");

            element.GetAttributeValue("id").ShouldBe(null);
            element.GetAttributeValue("id", "default").ShouldBe("default");

            element.SetAttributeValue("id", "1234");
            element.GetAttributeValue("id").ShouldBe("1234");
        }
        #endregion
    }
}
