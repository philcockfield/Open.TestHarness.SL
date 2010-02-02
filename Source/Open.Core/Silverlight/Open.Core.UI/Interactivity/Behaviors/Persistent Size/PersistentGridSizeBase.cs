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

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Base class for persisting the size of a Grid's Row or Column.</summary>
    public abstract class PersistentGridSizeBase : PersistentSize
    {
        #region Properties
        /// <summary>Gets or sets the grid length value.</summary>
        protected abstract GridLength GridLength { get; set; }
        #endregion

        // TODO - Add DP - IsVisible - make grid col/row 0 size when not visible.

        #region Methods - Override
        protected override void SyncValueWithSize()
        {
            if (! IsAttached) return;
            SettingValue = GridLength.Value;
        }

        protected override void SyncSizeWithValue()
        {
            if (!IsAttached) return;
            if (SettingValue == null) return;
            GridLength = new GridLength(SettingValue.Value);
        }
        #endregion
    }
}
