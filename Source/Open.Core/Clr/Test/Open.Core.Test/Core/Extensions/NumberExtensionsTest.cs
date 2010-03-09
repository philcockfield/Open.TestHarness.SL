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
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Common.Extensions
{
    [TestClass]
    public class NumberExtensionsTest
    {
        #region Head
        [TestInitialize]
        public void TestSetup()
        {
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldAverageSetOfNumbers()
        {
            new []{1, 2, 3}.Average().ShouldBe(2d);
        }

        [TestMethod]
        public void ShouldLockWithinBounds()
        {
            (-1).WithinBounds(0, 5).ShouldBe(0);
            10.WithinBounds(0,5).ShouldBe(5);
            2.WithinBounds(0,5).ShouldBe(2);
        }

        [TestMethod]
        public void ShouldGetDisplayUnit()
        {
            (-10.0).GetFileSizeUnit(FileSizeUnit.Kilobyte).ShouldBe(FileSizeUnit.Kilobyte);
            (0.0).GetFileSizeUnit(FileSizeUnit.Kilobyte).ShouldBe(FileSizeUnit.Kilobyte);

            (511.0).GetFileSizeUnit(FileSizeUnit.Kilobyte).ShouldBe(FileSizeUnit.Kilobyte);
            (512.0).GetFileSizeUnit(FileSizeUnit.Kilobyte).ShouldBe(FileSizeUnit.Megabyte);

            (511999.0).GetFileSizeUnit(FileSizeUnit.Kilobyte).ShouldBe(FileSizeUnit.Megabyte);
            (512000.0).GetFileSizeUnit(FileSizeUnit.Kilobyte).ShouldBe(FileSizeUnit.Gigabyte);

            (1073741823.0).GetFileSizeUnit(FileSizeUnit.Kilobyte).ShouldBe(FileSizeUnit.Gigabyte);
            (1073741824.0).GetFileSizeUnit(FileSizeUnit.Kilobyte).ShouldBe(FileSizeUnit.Terabyte);
            (9999999999999999999999999.0).GetFileSizeUnit(FileSizeUnit.Kilobyte).ShouldBe(FileSizeUnit.Terabyte);
        }

        [TestMethod]
        public void ShouldConvertToFileSizeUnitFromKilobytes()
        {
            (-10.0).ToFileSize().ShouldBe("-10 KB");
            (0.0).ToFileSize().ShouldBe("0 KB");

            (511.0).ToFileSize().ShouldBe("511 KB");
            (512.0).ToFileSize().ShouldBe("0.5 MB");

            (511999.0).ToFileSize().ShouldBe("500 MB");
            (512000.0).ToFileSize().ShouldBe("0.49 GB");

            (1073741823.0).ToFileSize().ShouldBe("1,024 GB");
            (1073741824.0).ToFileSize().ShouldBe("1 TB");
            (999999999999.0).ToFileSize().ShouldBe("931.32 TB");
            (9999999999999999999999999.0).ToFileSize().ShouldBe("9,313,225,746,154,790 TB");
        }

        [TestMethod]
        public void ShouldConvertFromSpecificUnit()
        {
            (1048576.0).ToFileSize(FileSizeUnit.Byte).ShouldBe("1 MB");
            (1536.0).ToFileSize(FileSizeUnit.Megabyte).ShouldBe("1.5 GB");
            (1572864.0).ToFileSize(FileSizeUnit.Kilobyte).ShouldBe("1.5 GB");
        }

        [TestMethod]
        public void ShouldConvertFromSpecificUnitToSpecificUnit()
        {
            (1048576.0).ToFileSize(FileSizeUnit.Byte, FileSizeUnit.Kilobyte).ShouldBe("1,024 KB");
            (1572864.0).ToFileSize(FileSizeUnit.Kilobyte, FileSizeUnit.Megabyte).ShouldBe("1,536 MB");
            (1610612736.0).ToFileSize(FileSizeUnit.Byte, FileSizeUnit.Gigabyte).ShouldBe("1.5 GB");
        }

        [TestMethod]
        public void ShouldDetermineIfNumberIsOddOrEven()
        {
            1.IsOdd().ShouldBe(true);
            1.IsEven().ShouldBe(false);

            2.IsOdd().ShouldBe(false);
            2.IsEven().ShouldBe(true);
        }
        #endregion
    }
}
