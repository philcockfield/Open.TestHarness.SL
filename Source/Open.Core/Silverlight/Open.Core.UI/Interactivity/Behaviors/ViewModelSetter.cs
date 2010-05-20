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
using System.Reflection;
using System.Windows;
using System.Windows.Interactivity;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Wraps the DataContext on the attached element within a specified View-Model.</summary>
    public class ViewModelSetter : Behavior<FrameworkElement>
    {
        #region Head
        private static readonly List<FactoryReference> factoryRefs = new List<FactoryReference>();
        #endregion

        #region Event Handlers
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= OnLoaded;
            SwapDataContext();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the unique identifier of the behavior (used by factories to determine a match).</summary>
        public string Id { get; set; }
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += OnLoaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= OnLoaded;
        }

        /// <summary>Registers a factory to use to create the view-model-wrapper.</summary>
        /// <param name="factoryId">The unique ID used to filter for the corresponding factory.  This should match the 'Id' property value on the instance.</param>
        /// <param name="factory">
        ///     The factory that creates the view-model-wrapper.<br/>
        ///     Param1: The behavior instance, used for ID matching (to see if the factory is the appropriate one to use).<br/>
        ///     Param2: The model to be wrapped (current DataContext of the element).<br/>
        ///     Param3: The return view-model value.
        /// </param>
        public static void RegisterFactory(string factoryId, Func<object, object> factory)
        {
            if (factory == null) return;
            factoryRefs.Add(FactoryReference.Create(factoryId, factory));
        }
        #endregion

        #region Internal
        private bool IsMatch(string id)
        {
            var identifer1 = Id.AsNullWhenEmpty() == null ? null : Id.ToLower();
            var identifer2 = id.AsNullWhenEmpty() == null ? null : id.ToLower();
            return identifer1.AsNullWhenEmpty() == identifer2.AsNullWhenEmpty();
        }

        private void SwapDataContext()
        {
            var viewModel = CreateViewModelWrapper();
            if (viewModel != null) AssociatedObject.DataContext = viewModel;
        }

        private object CreateViewModelWrapper()
        {
            foreach (var factoryReference in factoryRefs.ToArray())
            {
                if (!IsMatch(factoryReference.Id)) continue;
                object viewModel = null;
                var isAlive = factoryReference.Invoke(AssociatedObject.DataContext, out viewModel);
                if (isAlive)
                {
                    if (viewModel != null) return viewModel;
                }
                else
                {
                    factoryRefs.Remove(factoryReference);
                }
            }
            return null;
        }
        #endregion

        public class FactoryReference : WeakDelegateReference
        {
            #region Head
            private FactoryReference(string id, object actionTarget, MethodInfo actionMethod, Type actionType)
                : base(actionTarget, actionMethod, actionType)
            {
                Id = id;
            }

            public static FactoryReference Create(string id, Func<object, object> factory)
            {
                return new FactoryReference(id, factory.Target, factory.Method, factory.GetType());
            }
            #endregion

            #region Properties
            public string Id { get; private set; }
            #endregion

            #region Methods
            public bool Invoke(object model, out object result)
            {
                // Setup initial conditions.
                result = null;
                if (!TargetWeakReference.IsAlive) return false;

                // Invoke the Action.
                var factory = TryGetDelegate() as Func<object, object>;
                if (factory != null) result = factory(model);

                // Finish up.
                return true;
            }
            #endregion
        }
    }
}
