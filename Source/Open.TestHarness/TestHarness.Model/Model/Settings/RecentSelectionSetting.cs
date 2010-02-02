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

namespace Open.TestHarness.Model
{
    /// <summary>Represents a selected [ViewTestClass].</summary>
    public class RecentSelectionSetting
    {
        #region Head
        public RecentSelectionSetting(){}
        public RecentSelectionSetting(ViewTestClass model, string xapFileName) : this(model.TypeName, model.Attribute.DisplayName, model.AssemblyName, xapFileName) { }
        public RecentSelectionSetting(string className, string assemblyName, string xapFileName) : this(className, null, assemblyName, xapFileName) { }
        public RecentSelectionSetting(string className, string customName, string assemblyName, string xapFileName)
        {
            ClassName = className;
            CustomName = customName;
            Module = new ModuleSetting(assemblyName, xapFileName);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the assembly name.</summary>
        public ModuleSetting Module { get; set; }

        /// <summary>Gets or sets the name of the [ViewTestClass].</summary>
        public string ClassName { get; set; }

        /// <summary>Gets or sets the custom name of the [ViewTestClass] if there is one.</summary>
        public string CustomName { get; set; }
        #endregion
    }
}
