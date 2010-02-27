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
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Common.Extensions
{
    [TestClass]
    public class EnumExtensionsTest
    {
        [TestMethod]
        public void ShouldConvertToShortFileSizeUnit()
        {
            FileSizeUnit.Byte.ToString(true).ShouldBe("B");
            FileSizeUnit.Kilobyte.ToString(true).ShouldBe("KB");
            FileSizeUnit.Megabyte.ToString(true).ShouldBe("MB");
            FileSizeUnit.Gigabyte.ToString(true).ShouldBe("GB");
            FileSizeUnit.Terabyte.ToString(true).ShouldBe("TB");
        }

        [TestMethod]
        public void ShouldConvertToLongFileSizeUnit()
        {
            FileSizeUnit.Byte.ToString(false).ShouldBe("Byte");
            FileSizeUnit.Kilobyte.ToString(false).ShouldBe("Kilobyte");
            FileSizeUnit.Megabyte.ToString(false).ShouldBe("Megabyte");
            FileSizeUnit.Gigabyte.ToString(false).ShouldBe("Gigabyte");
            FileSizeUnit.Terabyte.ToString(false).ShouldBe("Terabyte");
        }

        [TestMethod]
        public void ShouldConvertToLongFileSizeUnitAndPluralize()
        {
            FileSizeUnit.Byte.ToString(0.0).ShouldBe("Bytes");
            FileSizeUnit.Kilobyte.ToString(0.0).ShouldBe("Kilobytes");
            FileSizeUnit.Megabyte.ToString(0.0).ShouldBe("Megabytes");
            FileSizeUnit.Gigabyte.ToString(0.0).ShouldBe("Gigabytes");
            FileSizeUnit.Terabyte.ToString(0.0).ShouldBe("Terabytes");

            FileSizeUnit.Byte.ToString(1.0).ShouldBe("Byte");
            FileSizeUnit.Kilobyte.ToString(1.0).ShouldBe("Kilobyte");
            FileSizeUnit.Megabyte.ToString(1.0).ShouldBe("Megabyte");
            FileSizeUnit.Gigabyte.ToString(1.0).ShouldBe("Gigabyte");
            FileSizeUnit.Terabyte.ToString(1.0).ShouldBe("Terabyte");

            FileSizeUnit.Byte.ToString(-1.0).ShouldBe("Byte");
            FileSizeUnit.Kilobyte.ToString(-1.0).ShouldBe("Kilobyte");
            FileSizeUnit.Megabyte.ToString(-1.0).ShouldBe("Megabyte");
            FileSizeUnit.Gigabyte.ToString(-1.0).ShouldBe("Gigabyte");
            FileSizeUnit.Terabyte.ToString(-1.0).ShouldBe("Terabyte");

            FileSizeUnit.Byte.ToString(2.0).ShouldBe("Bytes");
            FileSizeUnit.Kilobyte.ToString(2.0).ShouldBe("Kilobytes");
            FileSizeUnit.Megabyte.ToString(2.0).ShouldBe("Megabytes");
            FileSizeUnit.Gigabyte.ToString(2.0).ShouldBe("Gigabytes");
            FileSizeUnit.Terabyte.ToString(2.0).ShouldBe("Terabytes");

            FileSizeUnit.Byte.ToString(-2.0).ShouldBe("Bytes");
            FileSizeUnit.Kilobyte.ToString(-10.0).ShouldBe("Kilobytes");
            FileSizeUnit.Megabyte.ToString(-2.0).ShouldBe("Megabytes");
            FileSizeUnit.Gigabyte.ToString(-2.0).ShouldBe("Gigabytes");
            FileSizeUnit.Terabyte.ToString(-2.0).ShouldBe("Terabytes");
        }

        [TestMethod]
        public void ShouldNotChangeDuringConversion()
        {
            FileSizeUnit.Byte.ConvertTo(FileSizeUnit.Byte, 1d).ShouldBe(1d);
            FileSizeUnit.Kilobyte.ConvertTo(FileSizeUnit.Kilobyte, 1d).ShouldBe(1d);
            FileSizeUnit.Megabyte.ConvertTo(FileSizeUnit.Megabyte, 1d).ShouldBe(1d);
            FileSizeUnit.Gigabyte.ConvertTo(FileSizeUnit.Gigabyte, 1d).ShouldBe(1d);
            FileSizeUnit.Terabyte.ConvertTo(FileSizeUnit.Terabyte, 1d).ShouldBe(1d);
        }

        [TestMethod]
        public void ShouldNotConvertFileSize()
        {
            FileSizeUnit.Byte.ConvertTo(FileSizeUnit.Byte, 1d).ShouldBe(1d);
            FileSizeUnit.Byte.ConvertTo(FileSizeUnit.Kilobyte, 1d).Round(5).ShouldBe(0.00098);
            FileSizeUnit.Byte.ConvertTo(FileSizeUnit.Megabyte, 1d).Round(5).ShouldBe(0d);
            FileSizeUnit.Byte.ConvertTo(FileSizeUnit.Gigabyte, 1d).Round(5).ShouldBe(0d);
            FileSizeUnit.Byte.ConvertTo(FileSizeUnit.Terabyte, 1d).Round(5).ShouldBe(0d);

            FileSizeUnit.Kilobyte.ConvertTo(FileSizeUnit.Byte, 1d).Round(5).ShouldBe(1024.0);
            FileSizeUnit.Kilobyte.ConvertTo(FileSizeUnit.Kilobyte, 1d).Round(5).ShouldBe(1d);
            FileSizeUnit.Kilobyte.ConvertTo(FileSizeUnit.Megabyte, 1d).Round(5).ShouldBe(0.00098);
            FileSizeUnit.Kilobyte.ConvertTo(FileSizeUnit.Gigabyte, 1d).Round(5).ShouldBe(0d);
            FileSizeUnit.Kilobyte.ConvertTo(FileSizeUnit.Terabyte, 1d).Round(5).ShouldBe(0d);

            FileSizeUnit.Megabyte.ConvertTo(FileSizeUnit.Byte, 1d).Round(5).ShouldBe(1048576.0);
            FileSizeUnit.Megabyte.ConvertTo(FileSizeUnit.Kilobyte, 1d).Round(5).ShouldBe(1024.0);
            FileSizeUnit.Megabyte.ConvertTo(FileSizeUnit.Megabyte, 1d).Round(5).ShouldBe(1d);
            FileSizeUnit.Megabyte.ConvertTo(FileSizeUnit.Gigabyte, 1d).Round(5).ShouldBe(0.00098);
            FileSizeUnit.Megabyte.ConvertTo(FileSizeUnit.Terabyte, 1d).Round(5).ShouldBe(0d);

            FileSizeUnit.Gigabyte.ConvertTo(FileSizeUnit.Byte, 1d).Round(5).ShouldBe(1073741824.0);
            FileSizeUnit.Gigabyte.ConvertTo(FileSizeUnit.Kilobyte, 1d).Round(5).ShouldBe(1048576.0);
            FileSizeUnit.Gigabyte.ConvertTo(FileSizeUnit.Megabyte, 1d).Round(5).ShouldBe(1024.0);
            FileSizeUnit.Gigabyte.ConvertTo(FileSizeUnit.Gigabyte, 1d).Round(5).ShouldBe(1d);
            FileSizeUnit.Gigabyte.ConvertTo(FileSizeUnit.Terabyte, 1d).Round(5).ShouldBe(0.00098);

            FileSizeUnit.Terabyte.ConvertTo(FileSizeUnit.Byte, 1d).Round(5).ShouldBe(1099511627776.0);
            FileSizeUnit.Terabyte.ConvertTo(FileSizeUnit.Kilobyte, 1d).Round(5).ShouldBe(1073741824.0);
            FileSizeUnit.Terabyte.ConvertTo(FileSizeUnit.Megabyte, 1d).Round(5).ShouldBe(1048576.0);
            FileSizeUnit.Terabyte.ConvertTo(FileSizeUnit.Gigabyte, 1d).Round(5).ShouldBe(1024.0);
            FileSizeUnit.Terabyte.ConvertTo(FileSizeUnit.Terabyte, 1d).Round(5).ShouldBe(1.0);
        }

        [TestMethod]
        public void ShouldConvertToBytes()
        {
            FileSizeUnit.Byte.ToBytes(1d).ShouldBe(1d);
            FileSizeUnit.Kilobyte.ToBytes(1d).ShouldBe(1024.0);
            FileSizeUnit.Megabyte.ToBytes(1d).ShouldBe(1048576.0);
            FileSizeUnit.Gigabyte.ToBytes(1d).ShouldBe(1073741824.0);
            FileSizeUnit.Terabyte.ToBytes(1d).ShouldBe(1099511627776.0);

            FileSizeUnit.Byte.ToBytes(-1d).ShouldBe(-1d);
            FileSizeUnit.Kilobyte.ToBytes(-1d).ShouldBe(-1024.0);
            FileSizeUnit.Megabyte.ToBytes(-1d).ShouldBe(-1048576.0);
            FileSizeUnit.Gigabyte.ToBytes(-1d).ShouldBe(-1073741824.0);
            FileSizeUnit.Terabyte.ToBytes(-1d).ShouldBe(-1099511627776.0);
        }

        [TestMethod]
        public void ShouldConvertToThickness()
        {
            default(RectEdgeFlag).ToThickness().ShouldBe(new Thickness(0));
            RectEdgeFlag.None.ToThickness().ShouldBe(new Thickness(0));

            RectEdgeFlag.Left.ToThickness().ShouldBe(new Thickness(1, 0, 0, 0));
            RectEdgeFlag.Top.ToThickness().ShouldBe(new Thickness(0, 1, 0, 0));
            RectEdgeFlag.Right.ToThickness().ShouldBe(new Thickness(0, 0, 1, 0));
            RectEdgeFlag.Bottom.ToThickness().ShouldBe(new Thickness(0, 0, 0, 1));

            (RectEdgeFlag.Left | RectEdgeFlag.Top).ToThickness().ShouldBe(new Thickness(1, 1, 0, 0));
            (RectEdgeFlag.Bottom | RectEdgeFlag.Top).ToThickness().ShouldBe(new Thickness(0, 1, 0, 1));
            (RectEdgeFlag.Left | RectEdgeFlag.Top | RectEdgeFlag.Right | RectEdgeFlag.Bottom).ToThickness().ShouldBe(new Thickness(1));

            RectEdgeFlag.Left.ToThickness(5).ShouldBe(new Thickness(5, 0, 0, 0));
        }
    }
}
