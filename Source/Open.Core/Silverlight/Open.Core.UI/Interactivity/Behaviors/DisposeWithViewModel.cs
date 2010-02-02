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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>A behavior that disposes of the control it's attached to when it's View-Model (DataContext) is disposed of.</summary>
    /// <remarks>
    ///    For this behavior to work, the control (AssociatedObject) must implement IDisposable, and
    ///    the View-Model (DataContext) must implement INotifyDisposed.
    /// </remarks>
    public class DisposeWithViewModel : Behavior<FrameworkElement>
    {
        #region Head
        private DataContextObserver dataContextObserver;
        #endregion

        #region Event Handlers
        private void OnDataContextChanged()
        {
            // Setup initial conditions.
            if (DisposableViewModel == null) return;
            if (DisposableViewModel.IsDisposed)
            {
                UnwireEvents();
                return;
            }

            // Wire up events.
            DisposableViewModel.Disposed += OnViewModelDisposed;
        }

        private void OnViewModelDisposed(object sender, EventArgs e)
        {
            try
            {
                if (DisposableControl != null) DisposableControl.Dispose();
            }
            catch (UnauthorizedAccessException){}// Thrown during finalization.  Safe to ignore.
            catch (Exception) { throw; }
            finally
            {
                UnwireEvents();
            }
        }
        #endregion

        #region Properties - Internal
        private INotifyDisposed DisposableViewModel { get { return ViewModel as INotifyDisposed; } }
        private INotifyPropertyChanged ViewModel
        {
            get
            {
                try
                {
                    return AssociatedObject == null
                                    ? null
                                    : AssociatedObject.DataContext as INotifyPropertyChanged;
                }
                catch (UnauthorizedAccessException)
                {
                    // Thrown during finalization.  Safe to ignore.
                    UnwireEvents();
                    return null;
                }
                catch (Exception) { throw; }
            }
        }

        private IDisposable DisposableControl { get { return AssociatedObject as IDisposable; } }
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            // Setup initial conditions.
            base.OnAttached();
            if (!(AssociatedObject is IDisposable))
            {
                Debug.WriteLine(
                                "Failed to initialize '{0}'. Cannot dispose of control type '{1}' when its view-model disposes because '{1}' does not implment {2}.", 
                                typeof(DisposeWithViewModel).Name,
                                AssociatedObject.GetType().Name, 
                                typeof(IDisposable).Name);
                return;
            }

            // Setup a monitor to track when the 'DataContent' (ViewModel) changes.
            dataContextObserver = new DataContextObserver(AssociatedObject, OnDataContextChanged);

            // Finish up.
            if (AssociatedObject.DataContext != null) OnDataContextChanged();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (dataContextObserver != null) dataContextObserver.Dispose();
            UnwireEvents();
        }
        #endregion

        #region Internal
        private void UnwireEvents( )
        {
            if (DisposableViewModel != null) DisposableViewModel.Disposed -= OnViewModelDisposed;
        }
        #endregion
    }
}
