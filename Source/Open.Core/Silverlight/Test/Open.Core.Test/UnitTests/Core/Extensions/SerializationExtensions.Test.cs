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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Extensions
{
    [TestClass]
    public class SerializationExtensionsTest
    {
        #region Tests - Base64 and Stream Encoding
        public const string ImageBase64 = "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";

        [TestMethod]
        public void ShouldLoadImageFromBase64EncodedString()
        {
            var image = ImageBase64.FromBase64ToImage();
            image.ShouldNotBe(null);

            image = "".FromBase64ToImage();
            image.ShouldBe(null);

            image = "     ".FromBase64ToImage();
            image.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldFailWhenBadFormattedStringWhenLoadingImageFromBase64EncodedString()
        {
            Should.Throw<FormatException>(() =>
                                              {
                                                  var image = "not an image".FromBase64ToImage();
                                              });
        }

        [TestMethod]
        public void ShouldLoadImageFromStringThenTranslateBackAgain()
        {
            var stream = ImageBase64.FromBase64ToStream();
            var text = stream.ToBase64String();
            text.ShouldBe(ImageBase64);
        }
        #endregion
    }
}
