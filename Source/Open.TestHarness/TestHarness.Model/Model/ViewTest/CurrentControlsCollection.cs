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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Open.Core.Common;

namespace Open.TestHarness.Model
{
    /// <summary>A collection of Control instances corresponding to a view test.</summary>
    public class CurrentControlsCollection : ObservableCollection<object>
    {
        #region Head
        private readonly Dictionary<Type, List<object>> allControls = new Dictionary<Type, List<object>>();
        #endregion

        #region Methods
        /// <summary>Populates the collection with controls for the specified [ViewTest].</summary>
        /// <param name="test">The [ViewTest] to populate controls for.</param>
        public void Populate(ViewTest test)
        {
            // Setup initial conditions.
            if (test != null && !IsDifferent(test)) return;

            // Clear the collection.
            this.RemoveAll();
            if (test == null) return;
            var counter = new Dictionary<Type, int>();

            // Walk the list of parameter types.
            foreach (var type in GetParameterTypes(test))
            {
                // Get the counter for the parameter.
                var index = 0;
                if (counter.ContainsKey(type)) index = counter[type] + 1;
                counter[type] = index;

                // Construct the control.
                var control = GetControl(type, index);
                if (control != null) Add(control);
            }
        }

        /// <summary>
        ///     Populates the collection with controls for the specified [ViewTest] related to the
        ///     given parameters-collection, and assigns the control instances to the params collection.
        /// </summary>
        /// <param name="parametersCollection">The view-test parameters collection to populate.</param>
        public void Populate(ViewTestParametersCollection parametersCollection)
        {
            // Setup initial conditions.
            if (parametersCollection != null) Populate(parametersCollection.ViewTest);
            if (parametersCollection == null) return;

            // Populate the parameters collection with the new instances.
            var index = 0;
            foreach (var parameter in parametersCollection.Items)
            {

                if (!parameter.Type.IsA(typeof(UIElement))) continue;
                parameter.Value = this[index];
                index++;
            }
        }

        /// <summary>Resets the collection of controls.</summary>
        public void Reset()
        {
            this.RemoveAll();
            allControls.Clear();
        }
        #endregion

        #region Internal
        private UIElement GetControl(Type type, int index)
        {
            // Setup initial conditions.
            if (!(type.IsA<UIElement>() && !type.IsA<IViewFactory>())) return null;

            // Determine if an instance of the control already exists.
            if (allControls.ContainsKey(type) && allControls[type].Count > index)
            {
                return allControls[type][index] as UIElement;
            }

            // Construct the control and store a reference to it.
            var control = CreateControl(type);
            if (!allControls.ContainsKey(type)) allControls.Add(type, new List<object>());
            allControls[type].Add(control);

            // Finish up.
            return control;
        }

        private static UIElement CreateControl(Type type)
        {
            return type.IsA<IViewFactory>() 
                                    ? CreateFromViewFactory(type) 
                                    : Activator.CreateInstance(type) as UIElement;
        }

        private static UIElement CreateFromViewFactory(Type type)
        {
            if (type.IsInterface)
            {
                // Create from MEF.
                // TODO - Phil : "Need to create abstract View-Factory from MEF"
                throw new NotImplementedException("Need to create abstract View-Factory from MEF");
            }
            else
            {
                var viewModel = Activator.CreateInstance(type) as IViewFactory;
                var view = viewModel.CreateView();
                view.DataContext = viewModel;
                return view;
            }
        }

        private static IEnumerable<Type> GetParameterTypes(ViewTest test)
        {
            return from p in test.MethodInfo.GetParameters()
                   select p.ParameterType;
        }

        private bool IsDifferent(ViewTest test)
        {
            // Setup initial conditions.
            var paramTypes = GetParameterTypes(test);
            if (paramTypes.Count() != Count) return true;

            // Check each parameter for a perfect match.
            var index = 0;
            foreach (var type in paramTypes)
            {
                var control = this[index];
                if (control.GetType() != type) return true;
                index++;
            }

            // Finish up (no difference found).
            return false;
        }
        #endregion
    }
}
