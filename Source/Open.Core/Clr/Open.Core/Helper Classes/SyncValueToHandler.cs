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
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;

namespace Open.Core.Common
{
    /// <summary>Handles syncing a value from properties decorated with the [SyncValueTo] attribute to a target.</summary>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    public class SyncValueToHandler<TSource> : INotifyDisposed where TSource : INotifyPropertyChanged
    {
        #region Head
        private readonly SyncValueToAttribute syncAttribute;
        private readonly PropertyObserver<TSource> propertyObserver;

        public SyncValueToHandler(Expression<Func<TSource, object>> sourceProperty, TSource sourceInstance, object targetInstance)
        {
            // Setup initial conditions.
            if (sourceProperty == null) throw new ArgumentNullException("sourceProperty");
            if (Equals(sourceInstance, default(TSource))) throw new ArgumentNullException("sourceInstance");
            if (targetInstance == null) throw new ArgumentNullException("targetInstance");

            // Get the source property.
            var sourcePropertyName = sourceProperty.GetPropertyName();
            SourceProperty = sourceInstance.GetType().GetProperty(sourcePropertyName, BindingFlags.Instance | BindingFlags.Public);
            if (SourceProperty == null) throw new ArgumentNullException(string.Format("The source object '{0}' does not have a property named '{1}'.  Must be a public instance property", typeof(TSource).Name, sourcePropertyName));

            // Retrieve the [SyncValueTo] attribute.
            syncAttribute = SourceProperty.GetCustomAttributes(typeof (SyncValueToAttribute), true).FirstOrDefault() as SyncValueToAttribute;
            if (syncAttribute == null) throw new ArgumentException(string.Format("The property '{0}' is not decorated with the attribute [{1}]", sourcePropertyName, typeof(SyncValueToAttribute).Name));

            // Retrieve the target property.
            TargetProperty = targetInstance.GetType().GetProperty(syncAttribute.PropertyName, BindingFlags.Instance | BindingFlags.Public);
            if (TargetProperty == null) throw new ArgumentNullException(string.Format("The target object '{0}' does not have a property named '{1}'.  Must be a public instance property", targetInstance.GetType().FullName, syncAttribute.PropertyName));

            // Store values.
            SourceInstance = sourceInstance;
            TargetInstance = targetInstance;

            // Wire up events.
            propertyObserver = new PropertyObserver<TSource>(sourceInstance)
                .RegisterHandler(sourceProperty, source => OnPropertyChanged());
        }
        #endregion

        #region Dispose | Finalize
        ~SyncValueToHandler()
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
                propertyObserver.Dispose();
                SourceInstance = default(TSource);
                TargetInstance = null;
                SourceProperty = null;
                TargetProperty = null;
            }

            // Finish up.
            IsDisposed = true;
            OnDisposed();
        }

        /// <summary>Gets whether the object has been disposed.</summary>
        public bool IsDisposed { get; private set; }

        /// <summary>Fires when the object has been disposed of (via the 'Dispose' method.  See 'IDisposable' interface).</summary>
        public event EventHandler Disposed;

        private void OnDisposed()
        {
            if (Disposed != null) Disposed(this, new EventArgs());
        }

        #endregion

        #region Event Handlers
        private void OnPropertyChanged()
        {
            // Ensure the source and/or target object have not been disposed.
            if (IsDisposed || IsSourceDisposed || IsTargetDisposed)
            {
                Dispose();
                return;
            }

            // Update the target property.
            UpdateTarget();
        }
        #endregion

        #region Properties
        /// <summary>Gets the source property who's value is synced to the 'TargetInstance'.</summary>
        public PropertyInfo SourceProperty { get; private set; }

        /// <summary>Gets the target property to sync the source-property value to.</summary>
        public PropertyInfo TargetProperty { get; private set; }

        /// <summary>Gets the instance of the source object that contains the property to write from.</summary>
        public TSource SourceInstance { get; private set; }

        /// <summary>Gets the instance of the target object that contains the property to write to.</summary>
        public object TargetInstance { get; private set; }

        /// <summary>Gets whether either the Source instance has been disposed.</summary>
        public bool IsSourceDisposed { get { return IsDisposed || GetIsDisposed(SourceInstance); } }

        /// <summary>Gets whether either the Target instance has been disposed.</summary>
        public bool IsTargetDisposed { get { return IsDisposed || GetIsDisposed(TargetInstance); } }
        #endregion

        #region Internal
        private static bool GetIsDisposed(object obj)
        {
            var disposableSource = obj as INotifyDisposed;
            return disposableSource == null ? false : disposableSource.IsDisposed;
        }

        private void UpdateTarget()
        {
            var value = SourceProperty.GetValue(SourceInstance, null);
            TargetProperty.SetValue(TargetInstance, value, null);
        }
        #endregion
    }
}
