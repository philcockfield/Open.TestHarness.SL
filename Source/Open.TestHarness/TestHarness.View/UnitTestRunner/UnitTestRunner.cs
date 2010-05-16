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

        [ViewTest(Default = true, IsVisible = false, SizeMode = TestControlSize.Fill)]
        public void Initialize(ContentControl control)
        {
            // Setup the display.
            control.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            control.VerticalContentAlignment = VerticalAlignment.Stretch;

            // Retrieve the test assemblies.
            GetAssemblies(assemblies =>
                              {
                                  if (assemblies.Count() == 0)
                                  {
                                      Output.Write("No test assemblies to run");
                                  }

                                  // Embed the control.
                                  control.Content = UnitTestSystem.CreateTestPage(CreateSettings(assemblies));
                              });
        }
        #endregion

        #region Methods
        public static void Run()
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
        }
        #endregion

        #region Internal
        private static UnitTestSettings CreateSettings(IEnumerable<Assembly> assemblies)
        {
            // Setup initial conditions.
            var settings = UnitTestSystem.CreateDefaultSettings();
            
            // Setup the test-run monitor.
            if (monitor != null) monitor.Dispose();
            monitor = new UnitTestMonitor(settings);

            // Create auto-run settings.
            settings.StartRunImmediately = true;
            CollectionExtensions.AddRange(settings.TestAssemblies, assemblies);

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
