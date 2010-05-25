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
using T = Open.TestHarness.Model.ControlDisplayOptionSettings;

namespace Open.TestHarness.Model
{
    /// <summary>The Settings for how the test control(s) are displayed.</summary>
    public class ControlDisplayOptionSettings : IsolatedStorageModelBase
    {
        #region Head
        /// <summary>Constructor.</summary>
        public ControlDisplayOptionSettings() : base(IsolatedStorageType.Application, "TestHarness.ControlDisplayOptionSettings")
        {
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the border is visible.</summary>
        public bool ShowBorder
        {
            get { return GetPropertyValue<T, bool>(m => m.ShowBorder, true); }
            set { SetPropertyValue<T, bool>(m => m.ShowBorder, value, true); }
        }

        /// <summary>Gets or sets whether the output log is writing entries.</summary>
        public bool IsOutputLogActive
        {
            get { return GetPropertyValue<T, bool>(m => m.IsOutputLogActive, true); }
            set { SetPropertyValue<T, bool>(m => m.IsOutputLogActive, value, true); }
        }

        /// <summary>Gets or sets whether the time-stamp is shown for output log items.</summary>
        public bool ShowTimeStampOnOutputLog
        {
            get { return GetPropertyValue<T, bool>(m => m.ShowTimeStampOnOutputLog); }
            set { SetPropertyValue<T, bool>(m => m.ShowTimeStampOnOutputLog, value); }
        }

        /// <summary>Gets or sets the tag to limit the unit-test run to.</summary>
        public string TestTag
        {
            get { return GetPropertyValue<T, string>(m => m.TestTag); }
            set { SetPropertyValue<T, string>(m => m.TestTag, value); }
        }
        #endregion
    }
}
