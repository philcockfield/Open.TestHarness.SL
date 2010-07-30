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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Open.Core.Common.Collection
{
    /// <summary>Monitors an ObservableCollection and reports when items within it fire the 'PropertyChanged' event.</summary>
    /// <typeparam name="T">The type of item within the collection.</typeparam>
    public class NotifyPropertyChangedCollectionMonitor<T> : ObservableCollectionMonitor<T> where T : INotifyPropertyChanged
    {
        #region Head
        /// <summary>Fires when a property changes on a child within the collection.</summary>
        public event EventHandler<ChildPropertyChangedEventArgs<T>> PropertyChanged;
        protected void OnPropertyChanged(string propertyName, T source) { if (PropertyChanged != null) PropertyChanged(this, new ChildPropertyChangedEventArgs<T>(propertyName, source)); }

        public NotifyPropertyChangedCollectionMonitor(ObservableCollection<T> collection)
            : base(
                    collection,
                    (c, item) => WireEvent(c, item, true),
                    (c, item) => WireEvent(c, item, false))
        {
        }
        #endregion

        #region Event Handlers
        private void Handle_Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName, (T)sender);
        }
        
        #endregion

        #region Internal
        private static void WireEvent(ObservableCollectionMonitor<T> obj, T item, bool add)
        {
            var collection = (NotifyPropertyChangedCollectionMonitor<T>)obj;
            if (add) item.PropertyChanged += collection.Handle_Item_PropertyChanged;
            else item.PropertyChanged -= collection.Handle_Item_PropertyChanged;
        }
        #endregion
    }
}
