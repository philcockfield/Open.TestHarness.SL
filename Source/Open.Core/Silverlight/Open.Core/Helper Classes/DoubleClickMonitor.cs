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
using System.Windows;
using System.Windows.Input;

namespace Open.Core.Common
{
    /// <summary>Monitors an element and reports a double click.</summary>
    public class DoubleClickMonitor : IDisposable
    {
        #region Head
        /// <summary>Fires when a double-click is occurs.</summary>
        public event EventHandler DoubleClick;
        protected void OnDoubleClick()
        {
            if (DoubleClick != null) DoubleClick(this, new EventArgs());
            if (onDoubleClickAction != null) onDoubleClickAction(); // Invoke the action.
        }

        private readonly Action onDoubleClickAction;
        private DateTime lastClick;
        private static double doubleClickDuration = 0.4;

        public DoubleClickMonitor(UIElement element, Action onDoubleClick)
        {
            // Store values.
            Element = element;
            onDoubleClickAction = onDoubleClick;

            // Wire up events.
            Element.MouseLeftButtonDown += Handle_MouseClick;
        }
        #endregion

        #region Dispose | Finalize
        ~DoubleClickMonitor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            // Setup initial conditions.
            if (IsDisposed) return;

            // Perform disposal or managed resources.
            if (isDisposing)
            {
                Element.MouseLeftButtonDown -= Handle_MouseClick;
            }

            // Finish up.
            IsDisposed = true;
        }

        /// <summary>Gets whether the object has been disposed.</summary>
        public bool IsDisposed { get; private set; }
        #endregion

        #region Event Handlers
        private void Handle_MouseClick(object sender, MouseButtonEventArgs e)
        {
            if (IsDoubleClick())
            {
                OnDoubleClick();
                lastClick = default(DateTime); // Reset to avoid detecting triple clicks.
            }
            else
            {
                lastClick = DateTime.Now;
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets the element being monitored.</summary>
        public UIElement Element { get; private set; }


        /// <summary>Gets or sets the duraction (in seconds) for registering a double click.</summary>
        public static double DoubleClickDuration
        {
            get { return doubleClickDuration; }
            set { doubleClickDuration = value; }
        }
        #endregion

        #region Internal
        private bool IsDoubleClick()
        {
            if (lastClick == default(DateTime)) return false;
            return (DateTime.Now.Subtract(lastClick).TotalSeconds <= DoubleClickDuration);
        }
        #endregion
    }
}
