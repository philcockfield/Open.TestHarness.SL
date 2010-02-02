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
using System.Linq.Expressions;
using System.Windows;

namespace Open.Core.Common
{
    /// <summary>Provides a simple mechanism for responding to events on a DataContext object (allowing for the DataContext to change).</summary>
    /// <typeparam name="TPropertySource">The type of object to monitor for property changes.</typeparam>
    public class DataContextPropertyObserver<TPropertySource> : DataContextObserver where TPropertySource : INotifyPropertyChanged
    {
        #region Head
        private PropertyObserver<TPropertySource> propertyObserver;
        private TPropertySource previousViewModel;
        private readonly Dictionary<string, HandlerItem> handlers = new Dictionary<string, HandlerItem>();
        private class HandlerItem
        {
            public Expression<Func<TPropertySource, object>> Expression { get; set; }
            public Action<TPropertySource> Action { get; set; }
        }

        public DataContextPropertyObserver(FrameworkElement source) : base(source, null)
        {
            OnDataContextChangedAction = OnDataContextChanged;
        }

        protected override void DisposeOfManagedResources()
        {
            if (propertyObserver != null) propertyObserver.Dispose();
            handlers.Clear();
        }
        #endregion

        #region Event Handlers
        private void OnDataContextChanged()
        {
            // Setup initial conditions.
            if (Equals(ViewModel, previousViewModel)) return;

            // Setup the property observer.
            if (propertyObserver != null) propertyObserver.Dispose();
            if (HasViewModel) propertyObserver = CreatePropertyObserver();

            // Finish up.
            previousViewModel = ViewModel;
        }
        #endregion

        #region Properties - Internal
        private TPropertySource ViewModel
        {
            get
            {
                return Source.DataContext == null 
                    ? default(TPropertySource) 
                    : (TPropertySource)Source.DataContext;
            }
        }

        private bool HasViewModel { get { return !Equals(ViewModel, default(TPropertySource)); } }
        #endregion

        #region Methods
        /// <summary>
        ///   Registers a callback to be invoked when the PropertyChanged event has been raised for the specified property.
        /// </summary>
        /// <param name="expression">A lambda expression like 'n => n.PropertyName'.</param>
        /// <param name="handler">The callback to invoke when the property has changed.</param>
        /// <returns>The object on which this method was invoked, to allow for multiple invocations chained together.</returns>
        public DataContextPropertyObserver<TPropertySource> RegisterHandler(
                        Expression<Func<TPropertySource, object>> expression,
                        Action<TPropertySource> handler)
        {
            // Setup initial conditions.
            if (expression == null) throw new ArgumentNullException("expression");
            var key = expression.GetPropertyName();

            // Remove any existing handlers.
            if (handlers.ContainsKey(key)) UnregisterHandler(handlers[key].Expression);

            // Create the item and store it.
            var item = new HandlerItem {Expression = expression, Action = handler};
            handlers[key] = item;

            // Update the property-observer.
            if (propertyObserver != null) propertyObserver.RegisterHandler(expression, handler);

            //// Finish up.
            return this;
        }

        /// <summary>Removes the callback associated with the specified property.</summary>
        /// <param name="expression">A lambda expression like 'n => n.PropertyName'.</param>
        /// <returns>The object on which this method was invoked, to allow for multiple invocations chained together.</returns>
        public DataContextPropertyObserver<TPropertySource> UnregisterHandler(Expression<Func<TPropertySource, object>> expression)
        {
            // Setup initial conditions.
            if (expression == null) throw new ArgumentNullException("expression");

            // Remove from the store.
            var key = expression.GetPropertyName();
            if (handlers.ContainsKey(key)) handlers.Remove(key);

            // Update the property-observer.
            if (propertyObserver != null) propertyObserver.UnregisterHandler(expression);

            // Finish up.
            return this;
        }
        #endregion 

        #region Internal
        private PropertyObserver<TPropertySource> CreatePropertyObserver()
        {
            // Setup initial conditions.
            var observer = new PropertyObserver<TPropertySource>(ViewModel);

            // Load handlers.
            foreach (var item in handlers.Values)
            {
                observer.RegisterHandler(item.Expression, item.Action);
            }

            // Finish up.
            return observer;
        }
        #endregion
    }
}
