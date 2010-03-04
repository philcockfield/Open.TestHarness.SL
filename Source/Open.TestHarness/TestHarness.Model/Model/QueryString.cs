using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;
using Open.TestHarness.Automation;

namespace Open.TestHarness.Model
{
    /// <summary>Interprets the query string.</summary>
    public class QueryString : ModelBase
    {
        #region Head
        private const string keyXap = "xap";
        private const string keyClass = "class";
        private const string keyRunTests = "runTests";

        /// <summary>Constructor.</summary>
        public QueryString() : this(Application.Current.GetQueryString()){}

        /// <summary>Constructor.</summary>
        /// <param name="queryStringItems">The query-string items.</param>
        public QueryString(IEnumerable<KeyValuePair<string, string>> queryStringItems)
        {
            // Setup initial conditions.
            if (queryStringItems == null) queryStringItems = new List<KeyValuePair<string, string>>();
            Items = queryStringItems;

            // Extract values from query string.
            RunTests = ExtractRunTests();
            ExtractXapFiles();
            ExtractClassNames();

            // Initialize.
            LoadXapModules();
        }
        #endregion

        #region Properties
        /// <summary>Gets the query-string as a set of key:value pairs. </summary>
        public IEnumerable<KeyValuePair<string, string>> Items { get; private set; }

        /// <summary>Gets the collection of XAP files that are specified within the query-string.</summary>
        public IEnumerable<string> XapFiles { get; private set; }

        /// <summary>Gets the collection of class names that are specified within the query-string.</summary>
        public IEnumerable<string> ClassNames { get; private set; }

        /// <summary>Gets whether the tests should be run.</summary>
        public bool RunTests { get; private set; }
        #endregion

        #region Methods
        /// <summary>Causes the all assemblies that have been referenced via a "XAP=FileName" in the query-string to load.</summary>
        /// <param name="callback">Callback to invoke when the assemblies have loaded.</param>
        public void LoadAssemblies(Action callback)
        {
            LoadAssemblies(modules => { if (callback != null) callback(); });
        }

        /// <summary>Causes the all assemblies that have been referenced via a "XAP=FileName" in the query-string to load.</summary>
        /// <param name="callback">Callback to invoke when the assemblies have loaded.</param>
        public void LoadAssemblies(Action<IEnumerable<ViewTestClassesAssemblyModule>> callback)
        {
            // Setup initial conditions.
            if (XapFiles.Count() == 0) return;
            var modules = GetAssemblyModules().Where(m => XapFiles.Count(f => f.ToLower() == m.XapFileName.ToLower()) > 0);

            // Load each module.
            var loadedCount = 0;
            foreach (var item in modules)
            {
                item.LoadAssembly(() =>
                              {
                                  loadedCount++;
                                  if (loadedCount == modules.Count() && callback != null) callback(modules);
                              });
            }
        }

        /// <summary>Executes tests.</summary>
        /// <param name="callback">Callback to invoke when test run is complete.</param>
        public void StartTestRun(Action callback)
        {
            // Setup initial conditions.
            if (! RunTests || ClassNames.IsEmpty())
            {
                if (callback != null) callback();
                return;
            }

            LoadAssemblies(modules =>
                   {
                       // Create and populate the test-runner.
                       var testRunner = new TestRunner();
                       Populate(testRunner, modules);

                       // Start the test run.
                       testRunner.Start(() =>
                                            {
                                                if (callback != null) callback();
                                            });
                   });
        }
        #endregion

        #region Internal
        private IEnumerable<KeyValuePair<string, string>> GetItems(string key)
        {
            return Items.Where(m => 
                                m.Key.ToLower() == key.ToLower() 
                                && !m.Value.IsNullOrEmpty(true));
        }

        private void ExtractXapFiles()
        {
            var list = new List<string>();
            foreach (var item in GetItems(keyXap))
            {
                var value = item.Value.RemoveEnd(".xap").TrimEnd(".".ToCharArray());
                list.Add(value);
            }
            XapFiles = list;
        }

        private void ExtractClassNames()
        {
            var list = new List<string>();
            foreach (var item in GetItems(keyClass))
            {
                var value = item.Value.Trim(" ".ToCharArray());
                list.Add(value);
            }
            ClassNames = list;
        }

        private bool ExtractRunTests()
        {
            var items = GetItems(keyRunTests);
            if (items.Count() == 0) return false;
            var value = items.First().Value.Trim(" ".ToCharArray());
            return Convert.ToBoolean(value);
        }

        private void LoadXapModules()
        {
            if (XapFiles.Count() == 0) return;
            var modules = GetAssemblyModules();

            foreach (var fileName in XapFiles)
            {
                var name = fileName.ToLower();
                if (modules.Count(m => m.XapFileName.ToLower() == name) == 0)
                {
                    TestHarnessModel.Instance.AddModule(fileName);
                }
            }
        }

        private static IEnumerable<ViewTestClassesAssemblyModule> GetAssemblyModules()
        {
            return TestHarnessModel.Instance.Modules
                            .Where(m => m.GetType() == typeof(ViewTestClassesAssemblyModule))
                            .Cast<ViewTestClassesAssemblyModule>();
        }

        private void Populate(TestRunner testRunner, IEnumerable<ViewTestClassesAssemblyModule> modules)
        {
            foreach (var assemblyModule in modules)
            {
                foreach (var className in ClassNames)
                {
                    var testClasses = assemblyModule.GetTestClasses(className);
                    testRunner.Add(testClasses.ToArray());
                }
            }
        }
        #endregion
    }
}
