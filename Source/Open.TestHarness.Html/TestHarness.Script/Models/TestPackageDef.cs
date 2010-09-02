using System;
using System.Collections;

namespace Open.TestHarness
{
    /// <summary>Represents a package of tests from a single JavaScript file.</summary>
    public class TestPackageDef : IEnumerable
    {
        #region Head
        private static readonly ArrayList singletons = new ArrayList();
        private readonly ArrayList classes = new ArrayList();
        private readonly Type packageType;

        /// <summary>Constructor.</summary>
        /// <param name="packageType">The Type representing the test-package (normally the 'Application' class).</param>
        private TestPackageDef(Type packageType)
        {
            this.packageType = packageType;
        }
        #endregion

        #region Properties
        /// <summary>Gets the Type representing the test-package (normally the 'Application' class).</summary>
        public Type PackageType { get { return packageType; } }

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

        /// <summary>Retrieves the singleton instance of the definition for the given package type.</summary>
        /// <param name="testPackage">The Type representing the test-package (normally the 'Application' class).</param>
        public static TestPackageDef GetSingleton(Type testPackage)
        {
            // Retrieve the existing singleton (if there is one).
            string typeName = testPackage.FullName;
            foreach (TestPackageDef item in singletons)
            {
                if (item.PackageType.FullName == typeName) return item;
            }

            // Create the package-def.
            TestPackageDef def = new TestPackageDef(testPackage);
            singletons.Add(def);

            // Finish up.);
            return def;
        }
        #endregion
    }
}
