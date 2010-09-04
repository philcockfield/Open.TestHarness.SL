using System;
using System.Collections;
using Open.Core;

namespace Open.TestHarness.Models
{
    /// <summary>Represents a class with tests.</summary>
    /// <remarks>Enumerates on the [TestMethodInfo]'s within the class.</remarks>
    public class TestClassInfo : ModelBase, IEnumerable
    {
        #region Head
        private static Dictionary singletons;
        private readonly Type classType;
        private Dictionary instance;
        private readonly ArrayList methods = new ArrayList();

        /// <summary>Constructor.</summary>
        /// <param name="classType">The type of the test class.</param>
        private TestClassInfo(Type classType)
        {
            this.classType = classType;
            GetMethods();
        }
        #endregion

        #region Properties
        /// <summary>Gets the type of the test class.</summary>
        public Type ClassType { get { return classType; } }

        /// <summary>Gets the test instance of the class.</summary>
        public Dictionary Instance { get { return instance ?? (instance = Type.CreateInstance(classType) as Dictionary); } }

        /// <summary>Gets the number of test-methods within the class.</summary>
        public int Count { get { return methods.Count; } }
        #endregion

        #region Methods
        /// <summary>Resets the test-class instance.</summary>
        public void Reset()
        {
            instance = null;
        }

        public IEnumerator GetEnumerator() { return methods.GetEnumerator(); }
        public override string ToString() { return string.Format("[{0}:{1}]", GetType().Name, ClassType.Name); }
        #endregion

        #region Methods : Static
        /// <summary>Retrieves the singleton instance of the definition for the given package type.</summary>
        /// <param name="testClass">The Type of the test class.</param>
        public static TestClassInfo GetSingleton(Type testClass)
        {
            // Setup initial conditions.
            if (singletons == null) singletons = new Dictionary();
            string key = testClass.FullName;
            if (singletons.ContainsKey(key)) return singletons[key] as TestClassInfo;

            // Create the package-def.
            TestClassInfo def = new TestClassInfo(testClass);
            singletons[key] = def;

            // Finish up.
            return def;
        }
        #endregion

        #region Internal
        private void GetMethods()
        {
            if (Instance == null) return;
            foreach (DictionaryEntry item in Instance)
            {
                if (!TestMethodInfo.IsTestMethod(item)) continue;
                methods.Add(new TestMethodInfo(this, item.Key));
            }
        }
        #endregion
    }
}