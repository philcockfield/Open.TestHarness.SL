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
using System.Linq;

namespace Open.TestHarness.Model
{
    /// <summary>Represents the parameters of a view-test.</summary>
    public class ViewTestParametersCollection
    {
        private readonly List<ViewTestParameter> items = new List<ViewTestParameter>();

        public ViewTestParametersCollection(ViewTest viewTest)
        {
            // Setup initial conditions.
            if (viewTest == null) throw new ArgumentNullException("viewTest");

            // Store values.
            ViewTest = viewTest;

            // Build collection.
            foreach (var parameterInfo in viewTest.MethodInfo.GetParameters())
            {
               items.Add(new ViewTestParameter(parameterInfo));
            }
        }

        #region Properties
        /// <summary>Gets the parent ViewTest that this model represents the parameters of.</summary>
        public ViewTest ViewTest { get; private set; }

        /// <summary>Gets the collection of parameters.</summary>
        public IEnumerable<ViewTestParameter> Items { get { return items; } }
        #endregion

        #region Methods
        /// <summary>Gets the parameter values as an array.</summary>
        public object[] ToArray()
        {
            return (from p in Items select p.Value).ToArray();
        }
        #endregion
    }
}
