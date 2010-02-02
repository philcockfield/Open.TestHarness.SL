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
using System.ComponentModel;
using System.Threading;

namespace Open.Core.Common
{
    /// <summary>
    ///    Encapsulates common logic for implementing the 'INotifyPropertyChanged' interface, 
    ///    ensuring the event is fired on the UI thread.
    /// </summary>
    /// <remarks>
    ///    In most cases derive from 'NotifyPropertyChangedBase'.  Use this class if you need property notification
    ///    behavior in a class that is already deriving from another type.
    /// </remarks>
    public class NotifyPropertyChangedInvoker
    {
        #region Head
        private readonly Action<PropertyChangedEventArgs> fireEvent;
        private readonly SynchronizationContext syncContext;

        public NotifyPropertyChangedInvoker(SynchronizationContext syncContext, Action<PropertyChangedEventArgs> fireEvent)
        {
            this.fireEvent = fireEvent;
            this.syncContext = syncContext;
        }
        #endregion

        #region Methods

        /// <summary>Fires the 'PropertyChanged' event.</summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (syncContext != null)
            {
                syncContext.Send(obj => FirePropertyChanged(propertyName), null);
            }
            else
            {
                FirePropertyChanged(propertyName);
            }
        }

        /// <summary>Fires the 'PropertyChanged' event for a collection of properties.</summary>
        /// <param name="propertyNames">Collection of names of the properties that have changed.</param>
        public void OnPropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null) return;
            foreach (var name in propertyNames)
            {
                OnPropertyChanged(name);
            }
        }
        #endregion

        #region Internal
        private void FirePropertyChanged(string propertyName)
        {
            if (propertyName.AsNullWhenEmpty() == null) throw new ArgumentNullException("propertyName");
            fireEvent(new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
