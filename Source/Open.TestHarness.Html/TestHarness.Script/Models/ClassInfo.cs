using System;
using System.Collections;
using Open.Core;

namespace Open.Testing.Models
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

        private MethodInfo classInitialize;
        private MethodInfo classCleanup;
        private MethodInfo testInitialize;
        private MethodInfo testCleanup;

        /// <summary>Constructor.</summary>
        /// <param name="classType">The type of the test class.</param>
        /// <param name="packageInfo">The package the class belongs to.</param>
        private ClassInfo(Type classType, PackageInfo packageInfo)
        {
            // Setup initial conditions.
            this.classType = classType;
            this.packageInfo = packageInfo;
            displayName = Helper.String.RemoveEnd(MethodInfo.FormatName(classType.Name), "Test");

            // Get methods.
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

        #region Properties : Special Methods
        /// <summary>Gets the 'ClassInitialize' special method (or null if one isn't declared).</summary>
        public MethodInfo ClassInitialize { get { return classInitialize; } }

        /// <summary>Gets the 'ClassCleanup' special method (or null if one isn't declared).</summary>
        public MethodInfo ClassCleanup { get { return classCleanup; } }

        /// <summary>Gets the 'TestInitialize' special method (or null if one isn't declared).</summary>
        public MethodInfo TestInitialize { get { return testInitialize; } }

        /// <summary>Gets the 'TestCleanup' special method (or null if one isn't declared).</summary>
        public MethodInfo TestCleanup { get { return testCleanup; } }
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
                if (MethodInfo.IsTestMethod(item))
                {
                    methods.Add(CreateMethod(item));
                }
                else
                {
                    AssignSpecialMethod(item);
                }
            }
        }

        private void AssignSpecialMethod(DictionaryEntry item)
        {
            // Setup initial conditions.
            string key = item.Key;
            if (!MethodHelper.IsSpecial(key)) return;
            MethodInfo method = CreateMethod(item);

            // Match and assign the methods.
            if (MethodHelper.IsClassInitialize(key)) { classInitialize = method; }
            else if (MethodHelper.IsClassCleanup(key)) { classCleanup = method; }
            else if (MethodHelper.IsTestInitialize(key)) { testInitialize = method; }
            else if (MethodHelper.IsTestCleanup(key)) { testCleanup = method; }
        }

        private MethodInfo CreateMethod(DictionaryEntry item) { return new MethodInfo(this, item.Key); }
        #endregion
    }
}