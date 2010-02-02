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

using Open.Core.Common.Network;
namespace Open.TestHarness.Model
{
    /// <summary>Represents a module in the settings database.</summary>
    public class ModuleSetting
    {
        #region Head
        public ModuleSetting() { }
        public ModuleSetting(string xapFileName) : this(null, xapFileName) { }
        public ModuleSetting(string assemblyName, string xapFileName) 
        {
            AssemblyName = assemblyName;
            XapFileName = AssemblyLoader.StripExtensions(xapFileName);
        }
        #endregion

        #region Properties
        /// <summary>Gets the name of the containing XAP file (without the '.xap' extension).</summary>
        public string XapFileName { get; set; }

        /// <summary>Gets or sets the name of the entry-point assembly.</summary>
        public string AssemblyName { get; set; }
        #endregion
    }
}
