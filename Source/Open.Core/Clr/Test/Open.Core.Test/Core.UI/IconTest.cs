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
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.UI.Icon
{
    [TestClass]
    public class IconTest
    {
        [TestMethod]
        public void ShouldDetermineIfIconIsFromSilkSet()
        {
            Icons.SilkZoomOut.IsSilk().ShouldBe(true);
        }

        [TestMethod]
        public void ShouldConvertSilkIconToUri()
        {
            var icon = Icons.SilkAdd;
            var path = "/Images/Icons/Silk/SilkAdd.png";

            var uri = icon.ToUri();
            uri.ToString().ShouldBe(path);
        }

        [TestMethod]
        public void ShouldHaveImageForEveryEnumFlag()
        {
            // Get the path.
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = path + @"\..\..\..\..\Open.Core\Clr\Open.Core.UI";
            var folder = new DirectoryInfo(path);
            if (!folder.Exists) throw new NotFoundException("The path to the icon set cannot be found on the server. Looked in: " + path);

            // Match names.
            foreach (Icons icon in Enum.GetValues(typeof(Icons)))
            {
                if (!icon.IsSilk()) continue;
                var iconPath = folder.FullName + icon.ToUri().ToString().Replace("/", "\\");
                new FileInfo(iconPath).Exists.ShouldBe(true);
            }
        }
    }
}
