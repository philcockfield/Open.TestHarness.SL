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
using System.Windows.Interactivity;
using System.Reflection;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Invokes a method on the data-bound view-model.</summary>
    public class ExecuteMethod : TriggerAction<FrameworkElement>
    {
        #region Head
        public const string PropMethodName = "MethodName";
        private MethodInfo method;
        #endregion

        #region Properties
        /// <summary>Gets the data-context of the associated element.</summary>
        public object ViewModel
        {
            get { return AssociatedObject == null ? null : AssociatedObject.DataContext; }
        }

        /// <summary>Gets the method to invoke.</summary>
        public MethodInfo Method
        {
            get
            {
                if (method == null) method = GetMethodInfo();
                return method;
            }
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the name of the method to invoke when the event fires.</summary>
        public string MethodName
        {
            get { return (string) (GetValue(MethodNameProperty)); }
            set { SetValue(MethodNameProperty, value); }
        }
        /// <summary>Gets or sets the name of the method to invoke when the event fires.</summary>
        public static readonly DependencyProperty MethodNameProperty =
            DependencyProperty.Register(
                PropMethodName,
                typeof (string),
                typeof (ExecuteMethod),
                new PropertyMetadata(null, (s, e) => ((ExecuteMethod) s).OnMethodNameChanged()));
        private void OnMethodNameChanged()
        {
            method = null;
        }
        #endregion

        #region Methods
        /// <summary>Invoked when the Triggers event fires.</summary>
        /// <param name="eventArgs">The event-arguments passed by the event.</param>
        protected override void Invoke(object eventArgs)
        {
            // Setup initial conditions.
            if (Method == null) return;

            // Prepare the parameter list.
            var parameterCount = Method.GetParameters().Length;
            var parameters = parameterCount == 0 ? null : new [] {eventArgs};

            // Invoke the method.
            Method.Invoke(ViewModel, parameters);
        }
        #endregion

        #region Internal
        private MethodInfo GetMethodInfo()
        {
            // Setup initial conditions.
            if (ViewModel == null) return null;
            var name = MethodName.AsNullWhenEmpty();
            if (name == null) return null;

            // Retrieve the method.
            MethodInfo m;
            try
            {
                m = ViewModel.GetType().GetMethod(MethodName, BindingFlags.Instance | BindingFlags.Public);
            }
            catch (AmbiguousMatchException) { return null; }
            catch (Exception) { throw; }
            if (m == null) return null;

            // Ensure it is parameterless.
            if (m.GetParameters().Length > 1) return null;

            // Finish up.
            return m;
        }
        #endregion
    }
}
