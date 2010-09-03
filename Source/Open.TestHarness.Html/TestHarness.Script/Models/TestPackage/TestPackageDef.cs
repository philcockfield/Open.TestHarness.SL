using System;
using System.Collections;
using Open.Core;

namespace Open.TestHarness.Models
{
    /// <summary>Represents a package of tests from a single JavaScript file.</summary>
    /// <remarks>
    ///     Test classes will only be picked up if the declared initMethod registers test-classes
    ///     by using the 'RegisterTestClass()' method, eg:
    /// 
    ///                 Type testPackage = typeof (Application);
    ///                 TestHarness.RegisterTestClass(testPackage, typeof(MyTestClass1));
    /// 
    /// </remarks>
    public class TestPackageDef : IEnumerable
    {
        #region Head
        private static readonly ArrayList singletons = new ArrayList();
        private readonly ArrayList classes = new ArrayList();
        private readonly TestPackageLoader loader;

        /// <summary>Constructor.</summary>
        /// <param name="scriptUrl">The URL to the JavaScript file to load.</param>
        /// <param name="initMethod">The entry point method to invoke upon load completion.</param>
        private TestPackageDef(string scriptUrl, string initMethod)
        {
            loader = new TestPackageLoader(this, scriptUrl.ToLowerCase(), initMethod);
        }
        #endregion

        #region Properties
        /// <summary>Gets the unique ID of the package.</summary>
        public string Id { get { return Loader.ScriptUrl; } }

        /// <summary>Gets the package loader.</summary>
        public TestPackageLoader Loader{get { return loader; }}

        /// <summary>Gets or sets whether the package has been loaded.</summary>
        /// <remarks>If not loaded use the 'Loader'.</remarks>
        public bool IsLoaded { get { return Loader.IsLoaded; } }

        /// <summary>Gets the number of test classes within the package.</summary>
        public int Count { get { return classes.Count; } }

        /// <summary>Gets the collection of singleton TestPackageDef instances.</summary>
        public static ArrayList Singletons { get { return singletons; } }
        #endregion

        #region Methods
        public IEnumerator GetEnumerator() { return classes.GetEnumerator(); }

        /// <summary>Adds a test-class to the package.</summary>
        /// <param name="testClass">The type of the test class.</param>
        public void AddClass(Type testClass)
        {
            if (testClass == null) return;
            if (Contains(testClass)) return;
            classes.Add(TestClassDef.GetSingleton(testClass));
        }

        /// <summary>Determines whether the test-class has already been added to the package.</summary>
        /// <param name="testClass">The type of the test class.</param>
        public bool Contains(Type testClass) { return GetTestClassDef(testClass) != null; }

        /// <summary>Determines whether the test-class has already been added to the package.</summary>
        /// <param name="testClass">The type of the test class.</param>
        public TestClassDef GetTestClassDef(Type testClass)
        {
            if (testClass == null) return null;
            string typeName = testClass.FullName;
            foreach (TestClassDef item in classes)
            {
                if (item.Type.FullName == typeName) return item;
            }
            return null;
        }
        #endregion

        #region Methods : Static (Singletons)
        /// <summary>Retrieves (or creates) the singleton instance of the definition for the given package type.</summary>
        /// <param name="scriptUrl">The URL to the JavaScript file to load.</param>
        /// <param name="initMethod">The entry point method to invoke upon load completion.</param>
        public static TestPackageDef SingletonFromUrl(string scriptUrl, string initMethod)
        {
            // Retrieve the existing singleton (if there is one).
            TestPackageDef def = Helper.Collection.First(singletons, delegate(object o)
                                                                         {
                                                                             return ((TestPackageDef)o).Id == scriptUrl.ToLowerCase();
                                                                         }) as TestPackageDef;

            // Create and return the package-def.
            if (def == null)
            {
                def = new TestPackageDef(scriptUrl, initMethod);
                singletons.Add(def);
            }

            // Finish up.
            return def;
        }
        #endregion
    }
}