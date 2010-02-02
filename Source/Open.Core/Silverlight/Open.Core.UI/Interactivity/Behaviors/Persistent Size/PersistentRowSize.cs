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
using System.Windows.Controls;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>A behavior that stores the size of a Grid row.</summary>
    public class PersistentRowSize : PersistentGridSizeBase
    {
        #region Head
        private RowDefinition rowDefinition;

        public PersistentRowSize()
        {
            Dimension = SizeDimension.Width;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the column to persist.</summary>
        public RowDefinition RowDefinition
        {
            get { return rowDefinition; }
            set
            {
                rowDefinition = value;
                SyncSizeWithValue();
            }
        }

        protected override bool IsAttached { get { return RowDefinition == null ? false : base.IsAttached; } }

        protected override GridLength GridLength
        {
            get { return RowDefinition == null ? default(GridLength) : RowDefinition.Height; }
            set { if (RowDefinition != null) RowDefinition.Height = value; }
        }
        #endregion
    }
}
