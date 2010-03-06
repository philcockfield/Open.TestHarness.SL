﻿//------------------------------------------------------
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
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Browser;
using System.Windows.Media;
using System.Xml.Linq;
using Open.Core.Common;
using Open.TestHarness.Model;

namespace Open.TestHarness.Automation
{
    /// <summary>Manages automatically running [ViewTest]'s.</summary>
    public class TestRunner
    {
        #region Head
        public const string HtmlOutputId = "TestHarness.Output";
        private double interval = 0.3;
        private readonly List<MethodItem> methods = new List<MethodItem>();
        private readonly List<MethodInfo> passed = new List<MethodInfo>();
        private readonly List<MethodInfo> failed = new List<MethodInfo>();

        private class MethodItem
        {
            public ViewTestClass TestClass { get; set; }
            public MethodInfo MethodInfo { get; set; }
            public ViewTest Method { get { return TestClass.GetTestMethod(MethodInfo.Name); } }
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the interval (in seconds) between each method being executed.</summary>
        public double Interval
        {
            get { return interval; }
            set { interval = value.WithinBounds(0, double.MaxValue); }
        }

        /// <summary>Gets the set of methods that passed (after the set has been executed).</summary>
        public List<MethodInfo> Passed { get { return passed; } }

        /// <summary>Gets the set of methods that failed (after the set has been executed).</summary>
        public List<MethodInfo> Failed { get { return failed; } }

        /// <summary>Gets whether the runner is currently executing tests.</summary>
        public bool IsRunning { get; private set; }
        #endregion

        #region Methods - Add
        /// <summary>Gets the set of methods to be executed during the test run.</summary>
        public IEnumerable<ViewTest> GetMethods() { return methods.Select(m => m.Method); }

        /// <summary>Adds all test methods within the given assembly to the execution queue.</summary>
        /// <param name="modules">The assembly module(s) to add methods from.</param>
        public void Add(params ViewTestClassesAssemblyModule[] modules)
        {
            if (modules == null) return;
            foreach (var module in modules)
            {
                if (!module.IsLoaded) throw new ArgumentOutOfRangeException("modules", "Module must be loaded before adding to the test runner");
                Add(module.Classes.ToArray());
            }
        }

        /// <summary>Adds all test method within the given class to the execution queue.</summary>
        /// <param name="testClasses">The class(s) to add methods from.</param>
        public void Add(params ViewTestClass[] testClasses)
        {
            if (testClasses == null) return;
            foreach (var testClass in testClasses)
            {
                foreach (var method in testClass.ViewTests)
                {
                    Add(testClass, method.MethodInfo);
                }
            }
        }

        /// <summary>Adds the specified test method to the execution queue.</summary>
        /// <param name="testClass">The class that the method belongs to.</param>
        /// <param name="method">The test method.</param>
        public void Add(ViewTestClass testClass, MethodInfo method)
        {
            if (testClass == null) throw new ArgumentNullException("testClass");
            if (method == null) throw new ArgumentNullException("method");
            if (GetMethods().Count(m => m.MethodInfo== method) > 0) return;
            methods.Add(new MethodItem
                            {
                                TestClass = testClass,
                                MethodInfo = method
                            });
        }
        #endregion

        #region Methods - Execution
        /// <summary>Starts the test-runner.</summary>
        /// <param name="callback">The action to invoke when complete.</param>
        public void Start(Action callback)
        {
            // Setup initial conditions.
            if (IsRunning) throw new Exception("Tests execution has already been started.");
            if (methods.IsEmpty())
            {
                Output.Write(Colors.Red, "No tests have been added to the TestRunner.");
                if (callback != null) callback();
                return;
            }

            // Reset pass/fail collections.
            passed.Clear();
            failed.Clear();

            // Start executing tests.
            IsRunning = true;
            var startTime = DateTime.Now;
            DelayedInvoke(methods.First(), () =>
                                               {
                                                   // Update state.
                                                   IsRunning = false;
                                                   var elapsed = DateTime.Now.Subtract(startTime);

                                                   // Write results.
                                                   WriteToOutput(elapsed);
                                                   WriteToHtmlPage(elapsed);

                                                   // Finish up.
                                                   if (callback != null) callback();
                                               });
        }

        private void DelayedInvoke(MethodItem item, Action callback)
        {
            DelayedAction.Invoke(Interval, () => Invoke(item, callback));
        }

        private void Invoke(MethodItem item, Action callback)
        {
            try
            {
                // Reset the test-class.  This will cause the Default initialize method to be invoked.
                item.TestClass.Reload();

                // NB: The 'DefaultViewTest' is exeuted during the 'Reload()' operation.
                var method = item.Method;
                if (method != item.TestClass.DefaultViewTest) method.Execute();

                // Log success.
                passed.Add(item.MethodInfo);
            }
            catch (Exception)
            {
                failed.Add(item.MethodInfo);
            }
            
            // Execute next test in sequence.
            var next = methods.NextItem(item, false);
            if (next != null)
            {
                DelayedInvoke(next, callback);
            }
            else
            {
                if (callback != null) callback();
            }
        }

        private void WriteToOutput(TimeSpan elapsedTime)
        {
            // Setup initial conditions.
            Output.Clear();
            var failedCount = Failed.Count;
            var passedCount = Passed.Count;
            var color = failedCount > 0 ? Colors.Red : Colors.Green;
            var successOfFailureText = failedCount > 0 ? "with failures" : "successfully";

            // Write summary.
            Output.Write(color, string.Format("{0} tests ran {1}", GetMethods().Count(), successOfFailureText));
            Output.Write(color, string.Format("Completed in: {0} seconds", elapsedTime.TotalSeconds.Round(1)));
            if (passedCount > 0) Output.Write(Colors.Green, string.Format("Passed: {0}", passedCount));
            if (failedCount > 0) Output.Write(Colors.Red, string.Format("Failed: {0}", failedCount));
            Output.Break();
            if (failedCount == 0) return;

            // Write failure details.
            Output.Write("Failed Tests:");
            foreach (var methodInfo in Failed)
            {
                Output.Write(string.Format(" - {0}.{1}()", methodInfo.DeclaringType.FullName, methodInfo.Name));
            }
            Output.Break();
        }

        private void WriteToHtmlPage(TimeSpan elapsedTime)
        {
            // Create the DIV element.
            var doc = HtmlPage.Document;
            var div = doc.GetElementById(HtmlOutputId) ?? CreateElement("div");
            doc.Body.AppendChild(div);

            // Insert root XML element.
            var xml = CreateElement("xml");
            xml.SetAttribute("id", HtmlOutputId);
            xml.SetAttribute("duration", elapsedTime.ToString());
            xml.SetAttribute("passed", Passed.Count.ToString());
            xml.SetAttribute("failed", Failed.Count.ToString());
            div.AppendChild(xml);

            // Insert method report.
            InsertMethods(div, "passed", Passed);
            InsertMethods(div, "failed", Failed);
        }

        private void InsertMethods(HtmlElement parent, string name, IEnumerable<MethodInfo> methods)
        {
            // Setup initial conditions.
            if (methods.IsEmpty()) return;

            // Create the method container.
            var methodContainer = CreateElement(name);
            parent.AppendChild(methodContainer);

            // Insert each method.
            foreach (var methodInfo in methods)
            {
                var htmMethod = CreateElement("method");
                htmMethod.SetAttribute("name", methodInfo.Name);
                htmMethod.SetAttribute("class", methodInfo.DeclaringType.FullName);
                methodContainer.AppendChild(htmMethod);
            }
        }

        private static HtmlElement CreateElement(string tag)
        {
            return HtmlPage.Document.CreateElement(tag);
        }
       #endregion
    }
}
