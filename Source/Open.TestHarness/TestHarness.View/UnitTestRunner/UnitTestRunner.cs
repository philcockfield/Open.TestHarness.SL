using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Silverlight.Testing;
using Microsoft.Silverlight.Testing.Harness;
using Open.Core.Common;
using Open.TestHarness.Automation;
using Open.TestHarness.Model;

namespace Open.TestHarness.View
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
        public void RunTests(string tag = null)
        {
            // Retrieve the test assemblies.
            GetAssemblies(assemblies =>
                        {
                            if (assemblies.Count() == 0)
                            {
                                Output.Write("No test assemblies to run");
                            }

                            // Embed the control.
                            container.Content = UnitTestSystem.CreateTestPage(CreateSettings(tag, assemblies));
                        });
        }

        public void RunTests(IEnumerable<Assembly> assemblies, string tag = null)
        {
            container.Content = UnitTestSystem.CreateTestPage(CreateSettings(tag, assemblies));
        }

        public static void Run(string tag = null)
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
            Run(viewTestClass, tag);
        }

        private static void Run(ViewTestClass viewTestClass, string tag)
        {
            var runner = viewTestClass.Instance as UnitTestRunner;
            runner.RunTests(tag);
        }
        #endregion

        #region Internal
        private static UnitTestSettings CreateSettings(string tag, IEnumerable<Assembly> assemblies)
        {
            // Setup initial conditions.
            var settings = UnitTestSystem.CreateDefaultSettings();
            
            // Setup the test-run monitor.
            if (monitor != null) monitor.Dispose();
            monitor = new UnitTestMonitor(settings);

            // Create auto-run settings.
            settings.StartRunImmediately = true;
            settings.TestAssemblies.AddRange(assemblies);
            settings.TagExpression = tag.AsNullWhenEmpty();

            // Finish up.
            return settings;
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
