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
using Open.TestHarness.Model;

namespace Open.TestHarness.Automation
{
    /// <summary>Manages automatically running [ViewTest]'s.</summary>
    public class TestRunner
    {
        #region Head
        /// <summary>Constructor.</summary>
        /// <param name="testHarness">instance of the root TestHarness model.</param>
        public TestRunner(TestHarnessModel testHarness)
        {
            if (testHarness == null) throw new ArgumentNullException("testHarness");
            TestHarness = testHarness;
        }
        #endregion

        #region Properties
        /// <summary>Gets the instance of the root TestHarness model.</summary>
        public TestHarnessModel TestHarness { get; private set; }

        /// <summary>Gets the set of methods to be executed during the test run.</summary>
        public IEnumerable<ViewTest> Methods { get; private set; }
        #endregion

        #region Methods
        
        public void Add(ViewTestClass testClass, ViewTest method)
        {
            // Setup initial conditions.
            if (testClass == null) throw new ArgumentNullException("testClass");
            if (method == null) throw new ArgumentNullException("method");

            
//            testClass.p
        }

        #endregion

    }
}
