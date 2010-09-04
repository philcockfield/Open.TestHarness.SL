using System;
using System.Collections;
using Open.Core;

namespace Open.TestHarness.Models
{
    /// <summary>Represents a class with tests.</summary>
    /// <remarks>Enumerates on the [MethodInfo]'s within the class.</remarks>
    public class ClassInfo : ModelBase, IEnumerable
    {
        #region Head
        private static Dictionary singletons;
        private readonly Type classType;
        private readonly PackageInfo packageInfo;
        private Dictionary instance;
        private readonly ArrayList methods = new ArrayList();
        private readonly string displayName;

        /// <summary>Constructor.</summary>
        /// <param name="classType">The type of the test class.</param>
        /// <param name="packageInfo">The package the class belongs to.</param>
        private ClassInfo(Type classType, PackageInfo packageInfo)
        {
            this.classType = classType;
            this.packageInfo = packageInfo;
            displayName = MethodInfo.FormatName(classType.Name);
            GetMethods();
        }
        #endregion

        #region Properties
        /// <summary>Gets the type of the test class.</summary>
        public Type ClassType { get { return classType; } }

        /// <summary>Gets the package the class belongs to.</summary>
        public PackageInfo PackageInfo { get { return packageInfo; } }

        /// <summary>Gets the display version of the class name.</summary>
        public string DisplayName { get { return displayName; } }

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
        /// <param name="packageInfo">The package the class belongs to.</param>
        public static ClassInfo GetSingleton(Type testClass, PackageInfo packageInfo)
        {
            // Setup initial conditions.
            if (singletons == null) singletons = new Dictionary();
            string key = string.Format("{0}::{1}", packageInfo.Id, testClass.FullName);
            if (singletons.ContainsKey(key)) return singletons[key] as ClassInfo;

            // Create the package-def.
            ClassInfo def = new ClassInfo(testClass, packageInfo);
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
                if (!MethodInfo.IsTestMethod(item)) continue;
                methods.Add(new MethodInfo(this, item.Key));
            }
        }
        #endregion
    }
}