using System;
using System.Collections;

namespace Open.TestHarness.Models
{
    /// <summary>Represents a class with tests.</summary>
    public class TestClassDef
    {
        #region Head
        private static Dictionary singletons;
        private readonly Type type;

        /// <summary>Constructor.</summary>
        /// <param name="type">The type of the test class.</param>
        private TestClassDef(Type type)
        {
            this.type = type;
        }
        #endregion

        #region Properties
        /// <summary>Gets the type of the test class.</summary>
        public Type Type { get { return type; } }
        #endregion

        #region Methods
        /// <summary>Retrieves the singleton instance of the definition for the given package type.</summary>
        /// <param name="testClass">The Type of the test class.</param>
        public static TestClassDef GetSingleton(Type testClass)
        {
            // Setup initial conditions.
            if (singletons == null) singletons = new Dictionary();
            string key = testClass.FullName;
            if (singletons.ContainsKey(key)) return singletons[key] as TestClassDef;

            // Create the package-def.
            TestClassDef def = new TestClassDef(testClass);
            singletons[key] = def;

            // Finish up.
            return def;
        }
        #endregion
    }
}