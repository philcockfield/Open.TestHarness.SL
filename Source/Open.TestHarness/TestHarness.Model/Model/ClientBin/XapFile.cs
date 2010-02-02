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

using Open.Core.Common;

namespace Open.TestHarness.Model
{
    /// <summary>Represents a XAP (Silverlight Application) file.</summary>
    public class XapFile : ModelBase
    {
        #region Head
        public const string PropName = "Name";
        public const string PropKilobytes = "Kilobytes";

        /// <summary>The filename extension.</summary>
        public const string FileExtension = "xap";

        private string name;
        private double kilobytes;
        #endregion

        #region Properties
        /// <summary>Gets or sets the name of the XAP file.</summary>
        /// <remarks>The '.xap' is trimmed from the value if present.</remarks>
        public string Name
        {
            get { return name; }
            set
            {
                value = value.AsNullWhenEmpty();
                if (value != null) value = value.StripExtension(FileExtension);
                if (value == Name) return;
                name = value; 
                OnPropertyChanged(PropName);
            }
        }

        /// <summary>Gets or sets the size in kilobytes of the file.</summary>
        public double Kilobytes
        {
            get { return kilobytes; }
            set
            {
                if (value < 0) value = 0;
                if (value == Kilobytes) return;
                kilobytes = value; 
                OnPropertyChanged(PropKilobytes);
            }
        }

        /// <summary>Gets the size of the file formatted with the display unit.</summary>
        public string Size
        {
            get { return string.Format("{0} KB", Kilobytes); }
        }
        #endregion
    }
}
