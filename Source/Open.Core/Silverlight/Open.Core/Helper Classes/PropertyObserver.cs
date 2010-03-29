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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Open.Core.Common
{
    /// <summary>
    ///   Monitors the PropertyChanged event of an object that implements INotifyPropertyChanged,
    ///   executing callback methods (i.e. handlers) registered for properties of that object.
    /// </summary>
    /// <typeparam name="TPropertySource">The type of object to monitor for property changes.</typeparam>
    public partial class PropertyObserver<TPropertySource> : DisposableBase where TPropertySource : INotifyPropertyChanged
    {
        #region Head
        private readonly Dictionary<string, Action<TPropertySource>> propertyNameToHandlerMap;
        private readonly WeakReference propertySourceRef;

        /// <summary>
        ///   Initializes a new instance of PropertyObserver, which
        ///   observes the 'propertySource' object for property changes.
        /// </summary>
        /// <param name="propertySource">The object to monitor for property changes.</param>
        public PropertyObserver(TPropertySource propertySource)
        {
            // Setup initial conditions.
            if (IsNull(propertySource)) throw new ArgumentNullException("propertySource");

            // Store values.
            propertySourceRef = new WeakReference(propertySource, false);
            propertyNameToHandlerMap = new Dictionary<string, Action<TPropertySource>>();

            // Wire up events.
            var disposableSource = propertySource as INotifyDisposed;
            if (disposableSource != null) disposableSource.Disposed += Handle_Source_Disposed;
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            
            // Unregister all handlers.
            foreach (var propertyName in propertyNameToHandlerMap.Keys.ToArray())
            {
                UnregisterHandler(propertyName);
            }
        }
        #endregion

        #region Event Handlers
        private void Handle_Source_Disposed(object sender, EventArgs e)
        {
            // Setup initial conditions.
            var disposableSource = (INotifyDisposed)sender;
            disposableSource.Disposed -= Handle_Source_Disposed;

            // Dispose.
            Dispose();
        }
        #endregion

        #region Properties
        /// <summary>Gets the number of handlers that have been registered.</summary>
        public int Count { get { return propertyNameToHandlerMap.Count; } }

        /// <summary>Gets the object being monitored.</summary>
        public TPropertySource PropertySource
        {
            get
            {
                if (IsDisposed) return default(TPropertySource);
                return propertySourceRef.IsAlive
                                ? (TPropertySource)propertySourceRef.Target 
                                : default(TPropertySource);
            }
        }
        #endregion

        #region Methods
                /// <summary>
        ///   Registers a callback to be invoked when the PropertyChanged event has been raised for the specified property.
        /// </summary>
        /// <param name="expression">A lambda expression like 'n => n.PropertyName'.</param>
        /// <param name="handler">The callback to invoke when the property has changed.</param>
        /// <returns>The object on which this method was invoked, to allow for multiple invocations chained together.</returns>
        public PropertyObserver<TPropertySource> RegisterHandler(
                            Expression<Func<TPropertySource, object>> expression,
                            Action handler)
        {
            return RegisterHandler(expression, m => { if (handler != null) handler(); });
        }

        /// <summary>
        ///   Registers a callback to be invoked when the PropertyChanged event has been raised for the specified property.
        /// </summary>
        /// <param name="expression">A lambda expression like 'n => n.PropertyName'.</param>
        /// <param name="handler">The callback to invoke when the property has changed.</param>
        /// <returns>The object on which this method was invoked, to allow for multiple invocations chained together.</returns>
        public PropertyObserver<TPropertySource> RegisterHandler(
                            Expression<Func<TPropertySource, object>> expression,
                            Action<TPropertySource> handler)
        {
            // Setup initial conditions.
            if (expression == null) throw new ArgumentNullException("expression");

            var propertyName = expression.GetPropertyName();
            if (String.IsNullOrEmpty(propertyName)) throw new ArgumentException("'expression' did not provide a property name.");

            if (handler == null) throw new ArgumentNullException("handler");

            // Register the property.
            var propertySource = PropertySource;
            if (!IsNull(propertySource))
            {
                propertyNameToHandlerMap[propertyName] = handler;
                RegisterHandler(propertySource, propertyName);
            }

            // Finish up.
            return this;
        }

        /// <summary>Removes the callback associated with the specified property.</summary>
        /// <param name="expression">A lambda expression like 'n => n.PropertyName'.</param>
        /// <returns>The object on which this method was invoked, to allow for multiple invocations chained together.</returns>
        public PropertyObserver<TPropertySource> UnregisterHandler(Expression<Func<TPropertySource, object>> expression)
        {
            // Setup initial conditions.
            if (expression == null) throw new ArgumentNullException("expression");

            var propertyName = expression.GetPropertyName();
            if (String.IsNullOrEmpty(propertyName)) throw new ArgumentException("'expression' did not provide a property name.");

            // Unregister the property.
            UnregisterHandler(propertyName);

            // Finish up.
            return this;
        }
        #endregion 

        #region Internal
        private void UnregisterHandler(string propertyName)
        {
            // Setup initial conditions.
            var propertySource = PropertySource;
            if (IsNull(propertySource)) return;
            if (!propertyNameToHandlerMap.ContainsKey(propertyName)) return;

            // Perform the unregistration.
            propertyNameToHandlerMap.Remove(propertyName);
            UnregisterHandler(propertySource, propertyName); // NB: This calls out to partial-class implementations.
        }

        private static bool IsNull(TPropertySource source)
        {
            return Equals(source, default(TPropertySource));
        }

        private bool ProcessEvent(TPropertySource source, string propertyName)
        {
            if (propertyName.AsNullWhenEmpty() == null)
            {
                // When the property name is empty, all properties are considered to be invalidated.
                // Iterate over a copy of the list of handlers, in case a handler is registered by a callback.
                foreach (var handler in propertyNameToHandlerMap.Values.ToArray())
                {
                    handler(source);
                }
                return true;
            }
            else
            {
                Action<TPropertySource> handler;
                if (propertyNameToHandlerMap.TryGetValue(propertyName, out handler))
                {
                    handler(source);
                    return true;
                }
            }
            return false;
        }
        #endregion 
    }
}
