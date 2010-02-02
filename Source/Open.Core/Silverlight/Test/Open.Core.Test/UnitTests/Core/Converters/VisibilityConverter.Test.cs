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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Converter;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Converters
{
    [TestClass]
    public class VisibilityConverterTest : SilverlightUnitTest
    {

        [TestMethod]
        public void ShouldConvertVisibility()
        {
            var converter = new VisibilityConverter();

            converter.Convert(true, null, null, null).ShouldBe(Visibility.Visible);
            converter.Convert(false, null, null, null).ShouldBe(Visibility.Collapsed);

            converter.ConvertBack(Visibility.Visible, null, null, null).ShouldBe(true);
            converter.ConvertBack(Visibility.Collapsed, null, null, null).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldInvertConversions()
        {
            var converter = new InvertedVisibilityConverter();

            converter.Convert(true, null, null, null).ShouldBe(Visibility.Collapsed);
            converter.Convert(false, null, null, null).ShouldBe(Visibility.Visible);

            converter.ConvertBack(Visibility.Visible, null, null, null).ShouldBe(false);
            converter.ConvertBack(Visibility.Collapsed, null, null, null).ShouldBe(true);
        }
    }
}
