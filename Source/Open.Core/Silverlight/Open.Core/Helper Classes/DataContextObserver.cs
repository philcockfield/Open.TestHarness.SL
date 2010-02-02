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
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Open.Core.Common
{
    /// <summary>Monitors changes to an element DataContext property.</summary>
    public class DataContextObserver : DependencyObject, INotifyDisposed
    {
        #region Head
        private static readonly List<WeakReference> instances = new List<WeakReference>();

        public DataContextObserver(FrameworkElement source, Action onDataContextChanged)
        {
            // Setup initial conditions.
            Source = source;
            OnDataContextChangedAction = onDataContextChanged;

            // Create the dependency-property.
            RegisterProperty();

            // Store this instance in the static list (so it can be retrieved from the static event handler).
            instances.Add(new WeakReference(this));
        }
        #endregion

        #region Dispose | Finalize
        ~DataContextObserver()
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
                // Dispose of managed resources.
                RemoveInstances(instances);
                DisposeOfManagedResources();
            }

            // Finish up.
            IsDisposed = true;
            OnDisposed();
        }

        /// <summary>
        ///    Invoked when the Dispose method is called.  
        ///    Use this to dispose of any managed resources or unwire from events etc.
        /// </summary>
        /// <remarks>
        ///    It is not necessary to call base in the overridden method. 
        ///    This is a convenience method that is called from the main 'Dispose' virtual method.
        /// </remarks>
        protected virtual void DisposeOfManagedResources() { }

        /// <summary>Gets whether the object has been disposed.</summary>
        public bool IsDisposed { get; private set; }

        /// <summary>Fires when the object has been disposed of (via the 'Dispose' method.  See 'IDisposable' interface).</summary>
        public event EventHandler Disposed;
        private void OnDisposed(){if (Disposed != null) Disposed(this, new EventArgs());}
        #endregion

        #region Event Handlers
        private static void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var instance = GetInstance(sender as FrameworkElement);
            if (instance != null) instance.InvokeAction();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the action to invoked when the data-context changes.</summary>
        protected Action OnDataContextChangedAction { get; set; }

        /// <summary>Gets or sets the source eolement that is being monitored.</summary>
        protected FrameworkElement Source { get; private set; }
        #endregion

        #region Internal
        private void RegisterProperty()
        {
            // Create the dependency property.
            var dp = DependencyProperty.Register("DataContextObserverProperty",
                            typeof(Object),
                            Source.GetType(),
                            new PropertyMetadata(null, OnDataContextChanged));
            
            // Bind it to the source (this causes the 'changed' event handler to be invoked then the actual DataContext changes.
            Source.SetBinding(dp, new Binding());
        }

        private static DataContextObserver GetInstance(FrameworkElement sender)
        {
            // Setup initial conditions.
            var deadReferences = new List<WeakReference>();
            DataContextObserver instance = null;
            var wasFound = false;
            
            foreach (var item in instances)
            {
                // Collect dead references.
                if (!item.IsAlive)
                {
                    deadReferences.Add(item);
                    continue;
                }

                // Check for match with given source object.
                instance = ((DataContextObserver) item.Target);
                if (instance.Source == sender)
                {
                    wasFound = true;
                    break;
                }
            }

            // Remove any dead references that were found while looking up the instance.
            RemoveInstances(deadReferences);

            // Finish up.
            return wasFound ? instance : null;
       }

        private static void RemoveInstances(IEnumerable<WeakReference> items)
        {
            foreach (var item in items.ToList())
            {
                instances.Remove(item);
            }
        }

        private void InvokeAction()
        {
            if (IsDisposed) return;
            if (OnDataContextChangedAction != null) OnDataContextChangedAction();
        }
        #endregion
    }
}
