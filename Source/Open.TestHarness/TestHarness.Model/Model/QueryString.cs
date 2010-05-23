using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;
using Open.TestHarness.Automation;

namespace Open.TestHarness.Model
{
    /// <summary>Flags representing the type of test.</summary>
    public enum TestType
    {
        ViewTest,
        UnitTest
    }

    /// <summary>Interprets the query string.</summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QueryString : ModelBase
    {
        #region Head
        private const string keyXap = "xap";
        private const string keyClass = "class";
        private const string keyRunTests = "runTests";
        private const string keyTestType = "testType";
        private const string keyTag = "tag";

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
            TestType = ExtractTestType();
            ExtractXapFiles();
            ExtractClassNames();
            ExtractsTags();

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

        /// <summary>Gets the collection of space-delimited tags that are specified within the query-string.</summary>
        public IEnumerable<string> Tags { get; private set; }

        /// <summary>Gets whether the tests should be run.</summary>
        public bool RunTests { get; private set; }

        /// <summary>Gets the type of test specified within the query-string.</summary>
        public TestType TestType { get; private set; }
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
        #endregion

        #region Methods - Run Tests
        /// <summary>Executes tests.</summary>
        /// <param name="callback">Callback to invoke when test run is complete.</param>
        public void StartTestRun(Action callback = null)
        {
            // Setup initial conditions.
            if (! RunTests)
            {
                callback.InvokeOrDefault();
                return;
            }

            LoadAssemblies(modules =>
                               {
                                   // Create and populate the test-runner.
                                   var testRunner = new ViewTestRunner();
                                   Populate(testRunner, modules);

                                   // Start the test run.
                                   switch (TestType)
                                   {
                                       case TestType.ViewTest: RunViewTests(modules, callback); break;
                                       case TestType.UnitTest: RunUnitTests(modules, callback); break;
                                       default: throw new NotSupportedException(TestType.ToString());
                                   }
                                });
        }

        private void RunViewTests(IEnumerable<ViewTestClassesAssemblyModule> modules, Action callback)
        {
            // Create and populate the test-runner.
            var testRunner = new ViewTestRunner();
            Populate(testRunner, modules);
            testRunner.Start(() => callback.InvokeOrDefault());
        }

        private void RunUnitTests(IEnumerable<ViewTestClassesAssemblyModule> modules, Action callback)
        {
            var assemblies = modules.Select(m => m.Assembly);
            var testRunner = new UnitTestRunner();
            testRunner.RunTests(assemblies);
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
            ClassNames = GetDelimitedValues(keyClass);
        }

        private void ExtractsTags()
        {
            Tags = GetDelimitedValues(keyTag);
        }

        private IEnumerable<string> GetDelimitedValues(string key)
        {
            var list = new List<string>();
            foreach (var item in GetItems(key))
            {
                var value = item.Value.Trim(" ".ToCharArray());
                list.Add(value);
            }
            return list;
        }

        private bool ExtractRunTests()
        {
            var items = GetItems(keyRunTests);
            if (items.Count() == 0) return false;
            var value = items.First().Value.Trim(" ".ToCharArray());
            return Convert.ToBoolean(value);
        }


        private TestType ExtractTestType()
        {
            var items = GetItems(keyTestType);
            if (items.Count() == 0) return default(TestType);
            var value = items.First().Value.Trim(" ".ToCharArray());
            try
            {
                return (TestType)Enum.Parse(typeof(TestType), value, true);
            }
            catch (Exception)
            {
                return default(TestType);
            }
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

        private void Populate(ViewTestRunner viewTestRunner, IEnumerable<ViewTestClassesAssemblyModule> modules)
        {
            // Only add complete modules if a more narrow criteria has not been specified.
            var addModules = ClassNames.IsEmpty();

            foreach (var module in modules)
            {
                if (addModules)
                {
                    viewTestRunner.Add(Tags, module);
                }
                else
                {
                    AddClasses(viewTestRunner, module);
                }
            }
        }

        private void AddClasses(ViewTestRunner viewTestRunner, ViewTestClassesAssemblyModule module)
        {
            foreach (var className in ClassNames)
            {
                var testClasses = module.GetTestClasses(className);
                viewTestRunner.Add(Tags, testClasses.ToArray());
            }
        }
        #endregion
    }
}
