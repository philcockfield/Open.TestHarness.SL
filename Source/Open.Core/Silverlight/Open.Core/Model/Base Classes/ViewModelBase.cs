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

namespace Open.Core.Common
{
    /// <summary>Base class for all ViewModel's.</summary>
    public abstract class ViewModelBase : ModelBase
    {
        #region Events
        /// <summary>Fires when the IsActive property changes (this is accompanied also by the PropertyChanged event).</summary>
        public event EventHandler IsActiveChanged;
        private void OnIsActiveChanged(){if (IsActiveChanged != null) IsActiveChanged(this, new EventArgs());}
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the view-model is currently active.</summary>
        /// <remarks>
        ///    Use this flag to suppress behavior when the view-model has been loaded into memory
        ///    but is not currently associated with an active screen.
        /// </remarks>
        public bool IsActive
        {
            get { return GetPropertyValue<ViewModelBase, bool>(m => m.IsActive, true); }
            set
            {
                if (value == IsActive) return;
                SetPropertyValue<ViewModelBase, bool>(m => m.IsActive, value, true);
                OnIsActiveChanged();
            }
        }
        #endregion
    }
}
