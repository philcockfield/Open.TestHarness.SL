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

using T = Open.TestHarness.Model.PropertyExplorerSettings;

namespace Open.TestHarness.Model
{
    /// <summary>Stored settings for the PropertyExplorerPanel.</summary>
    public class PropertyExplorerSettings : SettingsModelBase
    {
        #region Head
        public PropertyExplorerSettings() : base(SettingsStoreType.Application, "TestHarness.PropertyExplorerSettings")
        {
        }
        #endregion

        #region Properties
        public ReadPropertyDataFrom ReadPropertyDataFrom
        {
            get { return GetPropertyValue<PropertyExplorerSettings, ReadPropertyDataFrom>(m => m.ReadPropertyDataFrom); }
            set { SetPropertyValue<PropertyExplorerSettings, ReadPropertyDataFrom>(m => m.ReadPropertyDataFrom, value); }
        }

        public bool IncludeHierarchy
        {
            get { return GetPropertyValue<PropertyExplorerSettings, bool>(m => m.IncludeHierarchy); }
            set { SetPropertyValue<PropertyExplorerSettings, bool>(m => m.IncludeHierarchy, value); }
        }
        #endregion
    }
}
