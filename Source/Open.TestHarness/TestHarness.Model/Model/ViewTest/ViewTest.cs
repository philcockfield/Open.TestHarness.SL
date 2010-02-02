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
using System.Reflection;
using System.Windows;
using Open.Core.Common;

namespace Open.TestHarness.Model
{
    /// <summary>Represents a method decorated with the [ViewTest] attribute.</summary>
    public class ViewTest : ModelBase
    {
        #region Head
        /// <summary>Fires when a view test needs to be executed (see ExecuteTest method).</summary>
        public event EventHandler ExecuteRequest;
        protected void OnExecuteRequest() { if (ExecuteRequest != null) ExecuteRequest(this, new EventArgs()); }

        public const string PropExecuteCount = "ExecuteCount";
        private int executeCount;

        public ViewTest(MethodInfo methodInfo)
        {
            // Setup initial conditions.
            MethodInfo = methodInfo;

            // Extract the attribute.
            var attributes = methodInfo.GetCustomAttributes(typeof (ViewTestAttribute), true);
            if (attributes.Length == 0) throw new ArgumentOutOfRangeException(string.Format("The given method is not decorated with the [{0}] attribute.", typeof(ViewTestAttribute).Name));
            Attribute = attributes[0] as ViewTestAttribute;
        }
        #endregion

        #region Properties
        /// <summary>Gets the reflection of the method this model represents.</summary>
        public MethodInfo MethodInfo{ get; private set; }

        /// <summary>Gets the [ViewTest] attribute adorning the method.</summary>
        public ViewTestAttribute Attribute { get; private set; }

        /// <summary>Gets the number of times the command has been executed.</summary>
        public int ExecuteCount
        {
            get { return executeCount; }
            private set
            {
                executeCount = value;
                OnPropertyChanged(PropExecuteCount);
            }
        }
        #endregion

        #region Methods
        /// <summary>Requests that the [ViewTest] method be executed.</summary>
        public void Execute()
        {
            OnExecuteRequest();
        }

        /// <summary>Executes the method against the specifeid set of control instances.</summary>
        /// <param name="instance">The object instance to execute against (null if the method is static).</param>
        /// <param name="controls">The set of controls.</param>
        internal void Execute(object instance, ObservableCollection<UIElement> controls)
        {
            // Setup initial conditions.
            if (controls == null) return;

            // Execute the method.
            var parameters = controls.ToArray();
            MethodInfo.Invoke(instance, parameters);

            // Finish up.
            ExecuteCount++;
        }
        #endregion

        #region Methods - Static
        /// <summary>Retrieves the method definitions for the given class.</summary>
        /// <param name="classType">The class to retrieve the methods from.</param>
        /// <returns>A list of method definitions.</returns>
        public static List<ViewTest> GetMethods(Type classType)
        {
            // Setup initial conditions.
            var list = new List<ViewTest>();
            var methodDefs = classType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            // Walk the set of methods looking for the public methods marked as [ViewTest]'s.
            foreach (var methodInfo in methodDefs)
            {
                var attributes = methodInfo.GetCustomAttributes(typeof (ViewTestAttribute), false);
                if (attributes.Length > 0) list.Add(new ViewTest(methodInfo));
            }

            // Finish up.
            return list;
        }
        #endregion
    }
}
