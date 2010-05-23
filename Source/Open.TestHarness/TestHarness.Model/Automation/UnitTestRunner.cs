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
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Silverlight.Testing;
using Microsoft.Silverlight.Testing.Harness;
using Open.Core.Common;
using Open.TestHarness.Model;

namespace Open.TestHarness.Automation
{
    /// <summary>Renders the UnitTest runner in the main window.</summary>
    [ViewTestClass]
    public class UnitTestRunner
    {
        #region Head
        private static UnitTestMonitor monitor;
        private ContentControl container;

        [ViewTest(Default = true, IsVisible = false, SizeMode = TestControlSize.Fill)]
        public void Initialize(ContentControl control)
        {
            container = control;
            container.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            container.VerticalContentAlignment = VerticalAlignment.Stretch;
        }
        #endregion

        #region Methods - Run
        public void RunTests(IEnumerable<Assembly> assemblies = null, IEnumerable<string> tags = null)
        {
            Action<IEnumerable<Assembly>> launchRunner = loadedAssemblies =>
                                    {
                                        container.Content = UnitTestSystem.CreateTestPage(CreateSettings(tags, loadedAssemblies));
                                    };


            if (assemblies != null)
            {
                // Assemblies were specified.
                launchRunner(assemblies);
            }
            else
            {
                // Assemblies not specified, load them...
                GetAssemblies(result =>
                                {
                                    if (result.Count() == 0)
                                    {
                                        Output.Write("No test assemblies to run");
                                        return;
                                    }
                                    launchRunner(result);
                                });
            }
        }

        public static void Run(IEnumerable<string> tags = null, IEnumerable<Assembly> assemblies = null)
        {
            var viewTestClass = ViewTestClass.GetSingleton(typeof(UnitTestRunner), null);
            if (!viewTestClass.IsCurrent)
            {
                viewTestClass.Activate();
                viewTestClass.IsCurrent = true;
            }
            else
            {
                viewTestClass.Reload();
            }
            Run(viewTestClass, assemblies, tags);
        }

        private static void Run(ViewTestClass viewTestClass, IEnumerable<Assembly> assemblies, IEnumerable<string> tags)
        {
            var runner = viewTestClass.Instance as UnitTestRunner;
            runner.RunTests(assemblies, tags);
        }
        #endregion

        #region Internal
        private static UnitTestSettings CreateSettings(IEnumerable<string> tags, IEnumerable<Assembly> assemblies)
        {
            // Setup initial conditions.
            var settings = UnitTestSystem.CreateDefaultSettings();
            
            // Setup the test-run monitor.
            if (monitor != null) monitor.Dispose();
            monitor = new UnitTestMonitor(settings);

            // Create auto-run settings.
            settings.StartRunImmediately = true;
            settings.TestAssemblies.AddRange(assemblies);
            settings.TagExpression = CreateTagExpression(tags);

            // Finish up.
            return settings;
        }

        private static string CreateTagExpression(IEnumerable<string> tags)
        {
            if (tags == null || tags.Count() == 0) return null;
            var expression = "";
            foreach (var tag in tags)
            {
                if (tag.IsNullOrEmpty(true)) continue;
                expression += tag.Trim(" ".ToCharArray()) + "+";
            }
            return expression.TrimEnd("+".ToCharArray()).AsNullWhenEmpty();
        }

        private static void GetAssemblies(Action<IEnumerable<Assembly>> callback)
        {
            // Setup initial conditions.
            if (callback == null) throw new ArgumentNullException("callback");
            var modules = TestHarnessModel.Instance.AssemblyModules;

            // Loaded handler.
            var list = new List<Assembly>();
            var loadCount = 0;
            Action<ViewTestClassesAssemblyModule> onLoaded = module =>
                                                                 {
                                                                     loadCount++;
                                                                     list.Add(module.Assembly);
                                                                     if (loadCount == modules.Count()) callback(list);
                                                                 };

            // Build the list.
            foreach (var module in modules)
            {
                if (module.IsLoaded)
                {
                    onLoaded(module);
                }
                else
                {
                    module.LoadAssembly(callback: () => { onLoaded(module); });
                }
            }

            // Finish up.
            callback(list);
        }
        #endregion
    }
}