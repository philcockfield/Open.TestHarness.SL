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
using Microsoft.Silverlight.Testing;
using Microsoft.Silverlight.Testing.Harness;
using Open.Core.Common;

namespace Open.TestHarness.Automation
{
    /// <summary>Monitors the unit-test system.</summary>
    public class UnitTestMonitor : DisposableBase
    {
        #region Head
        private readonly UnitTestHarness harness;
        private RunState runState;


        /// <summary>Constructor.</summary>
        public UnitTestMonitor(UnitTestSettings settings)
        {
            // Setup initial conditions.
            if (settings == null) throw new ArgumentNullException("settings");
            harness = settings.TestHarness;

            // Wire up events.
            WireEvents(true);
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            WireEvents(false);
            runState = null;
        }
        #endregion

        #region Event Handlers
        private void OnTestHarnessCompleted(object sender, TestHarnessCompletedEventArgs e)
        {
            // Setup initial conditions.
            if (!IsRunning) return;

            // Write output to HTML DOM.
            runState.Write();
            runState = null;

            // Finish up.
            IsRunning = false;
        }

        private void OnTestClassStarting(object sender, TestClassStartingEventArgs e)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                runState = new RunState();
            }
        }

        private void OnTestMethodCompleted(object sender, TestMethodCompletedEventArgs e)
        {
            // Setup initial conditions.
            if (runState == null || !IsRunning) return;
            var methodInfo = e.Result.TestMethod.Method;
            var success = e.Result.Exception == null;

            // Store appropriate value.
            (success ? runState.Passed : runState.Failed).Add(methodInfo);
        }
        #endregion

        #region Properties
        /// <summary>Gets whether the unit-test system is currently running.</summary>
        public static bool IsRunning { get; private set; }
        #endregion

        #region Internal
        private void WireEvents(bool addHandler)
        {
            if (addHandler)
            {
                harness.TestClassStarting += OnTestClassStarting;
                harness.TestMethodCompleted += OnTestMethodCompleted;
                harness.TestHarnessCompleted += OnTestHarnessCompleted;
            }
            else
            {
                harness.TestClassStarting -= OnTestClassStarting;
                harness.TestMethodCompleted -= OnTestMethodCompleted;
                harness.TestHarnessCompleted -= OnTestHarnessCompleted;
            }
        }
        #endregion

        private class RunState
        {
            private readonly DateTime startTime;
            public RunState()
            {
                startTime = DateTime.Now;
                Passed = new List<MethodInfo>();
                Failed = new List<MethodInfo>();
            }
            public List<MethodInfo> Passed { get; private set; }
            public List<MethodInfo> Failed { get; private set; }
            public void Write()
            {
                new TestRunHtmlOutputWriter("results.unit-test", DateTime.Now.Subtract(startTime), Passed, Failed).Write();
            }
        }

    }
}