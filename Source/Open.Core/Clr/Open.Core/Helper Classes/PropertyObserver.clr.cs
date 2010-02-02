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
using System.Linq;
using System.Windows;

namespace Open.Core.Common
{
    public partial class PropertyObserver<TPropertySource> : IWeakEventListener
    {
        #region Event Handlers
        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(PropertyChangedEventManager))
            {
                return ProcessEvent(
                                (TPropertySource)sender, 
                                ((PropertyChangedEventArgs)e).PropertyName);
            }
            return false;
        }
        #endregion

        #region Internal (calls made from master class)
        private void RegisterHandler(TPropertySource propertySource, string propertyName)
        {
            PropertyChangedEventManager.AddListener(propertySource, this, propertyName);
        }

        private void UnregisterHandler(TPropertySource propertySource, string propertyName)
        {
            PropertyChangedEventManager.RemoveListener(propertySource, this, propertyName);
        }
        #endregion
    }
}
