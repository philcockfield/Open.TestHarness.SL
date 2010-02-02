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
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.Test.Extensions
{
    [TestClass]
    public class KeyEventExtensionsTest
    {
        [TestMethod]
        public void ShouldDetermineIfKeyIsNumeric()
        {
            Key.D0.IsNumeric().ShouldBe(true);
            Key.D1.IsNumeric().ShouldBe(true);
            Key.D2.IsNumeric().ShouldBe(true);
            Key.D3.IsNumeric().ShouldBe(true);
            Key.D4.IsNumeric().ShouldBe(true);
            Key.D5.IsNumeric().ShouldBe(true);
            Key.D6.IsNumeric().ShouldBe(true);
            Key.D7.IsNumeric().ShouldBe(true);
            Key.D8.IsNumeric().ShouldBe(true);
            Key.D9.IsNumeric().ShouldBe(true);

            Key.NumPad0.IsNumeric().ShouldBe(true);
            Key.NumPad1.IsNumeric().ShouldBe(true);
            Key.NumPad2.IsNumeric().ShouldBe(true);
            Key.NumPad3.IsNumeric().ShouldBe(true);
            Key.NumPad4.IsNumeric().ShouldBe(true);
            Key.NumPad5.IsNumeric().ShouldBe(true);
            Key.NumPad6.IsNumeric().ShouldBe(true);
            Key.NumPad7.IsNumeric().ShouldBe(true);
            Key.NumPad8.IsNumeric().ShouldBe(true);
            Key.NumPad9.IsNumeric().ShouldBe(true);

            Key.A.IsNumeric().ShouldBe(false);
            Key.Up.IsNumeric().ShouldBe(false);
            Key.Apps.IsNumeric().ShouldBe(false);
            Key.F19.IsNumeric().ShouldBe(false);
        }
    }
}
