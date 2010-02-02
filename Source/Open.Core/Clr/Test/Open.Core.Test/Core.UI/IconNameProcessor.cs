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
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Open.Core.Common.Test.Core.UI.Icon
{
    /// <summary>Used to process a set of icon names.  Do not run this test unless you are wanting to process icons.</summary>
    [TestClass]
    public class IconNameProcessor
    {
        //[Ignore]
        //[TestMethod]
        //public void ProcessNames()
        //{
        //    var folder = new DirectoryInfo(@"C:\TEMP\icons");
        //    var processor = new IconProcessor(folder);
        //    processor.Process();
        //}

        private class IconProcessor
        {
            #region Head
            private readonly DirectoryInfo sourceFolder;
            private readonly DirectoryInfo outputFolder;

            public IconProcessor(DirectoryInfo sourceFolder)
            {
                this.sourceFolder = sourceFolder;
                outputFolder = sourceFolder.CreateSubdirectory("Output");
            }
            #endregion

            #region Properties
            private IEnumerable<FileInfo> SourceIcons { get { return sourceFolder.GetFiles("*.png"); } }
            #endregion

            #region Methods
            public void Process()
            {
                // Setup initial conditions.
                Debug.WriteLine("Processing: " + SourceIcons.Count() + " icons.");
                Debug.WriteLine("");
                DeleteContents(outputFolder);

                // Copy icons to output folder.
                foreach (var icon in SourceIcons)
                {
                    Copy(icon);
                }
                Debug.WriteLine("");
                Debug.WriteLine("");
            }
            #endregion

            #region Internal
            private static void DeleteContents(DirectoryInfo folder)
            {
                foreach (var  file in folder.EnumerateFiles())
                {
                    file.Delete();
                }
            }

            private void Copy(FileInfo icon)
            {
                // Setup initial conditions.
                var name = GetName(icon);
                Debug.WriteLine(name + ",");

                // Copy.
                var fileName = string.Format(@"{0}\{1}.png", outputFolder.FullName, name);
                icon.CopyTo(fileName);
            }

            private static string GetName(FileInfo icon)
            {
                // Setup initial conditions.
                var name = icon.Name.RemoveEnd(".png");

                // Remove underscores.
                var parts = name.Split("_".ToCharArray());
                var capitalized = parts.Aggregate("", (current, part) => current + part.ToSentenceCase());

                // Prepend 'Silk'.
                name = "Silk" + capitalized;

                // Finish up.
                return name;
            }
            #endregion
        }
    }
}
