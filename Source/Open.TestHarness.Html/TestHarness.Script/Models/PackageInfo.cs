using System;
using System.Collections;
using Open.Core;
using Open.Core.Helpers;

namespace Open.Testing.Models
{
    /// <summary>Represents a package of tests from a single JavaScript file.</summary>
    /// <remarks>
    ///     Test classes will only be picked up if the declared initMethod registers them
    ///     by using the 'RegisterClass()' method, eg:
    /// 
    ///           Testing.RegisterClass(typeof(MyClass1));
    /// 
    /// </remarks>
    public class PackageInfo : ModelBase, IEnumerable
    {
        #region Head
        private static readonly ArrayList singletons = new ArrayList();
        private readonly ArrayList classes = new ArrayList();
        private readonly PackageLoader loader;
        private readonly string name;

        /// <summary>Constructor.</summary>
        /// <param name="scriptUrl">The URL to the JavaScript file to load.</param>
        /// <param name="initMethod">The entry point method to invoke upon load completion.</param>
        private PackageInfo(string scriptUrl, string initMethod)
        {
            // Setup initial conditions.
            if (string.IsNullOrEmpty(scriptUrl)) throw new Exception("A URL to the test-package script must be specified.");
            if (string.IsNullOrEmpty(initMethod)) throw new Exception("An entry point method must be specified.");

            // Store values.
            name = GetName(scriptUrl);
            loader = new PackageLoader(this, scriptUrl.ToLowerCase(), initMethod);
        }
        #endregion

        #region Properties
        /// <summary>Gets the unique ID of the package.</summary>
        public string Id { get { return Loader.ScriptUrl; } }

        /// <summary>Gets the display name of the package.</summary>
        public string Name { get { return name; } }

        /// <summary>Gets the package loader.</summary>
        public PackageLoader Loader{get { return loader; }}

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
            classes.Add(ClassInfo.GetSingleton(testClass, this));
        }

        /// <summary>Determines whether the test-class has already been added to the package.</summary>
        /// <param name="testClass">The type of the test class.</param>
        public bool Contains(Type testClass) { return GetTestClassDef(testClass) != null; }

        /// <summary>Determines whether the test-class has already been added to the package.</summary>
        /// <param name="testClass">The type of the test class.</param>
        public ClassInfo GetTestClassDef(Type testClass)
        {
            if (testClass == null) return null;
            string typeName = testClass.FullName;
            foreach (ClassInfo item in classes)
            {
                if (item.ClassType.FullName == typeName) return item;
            }
            return null;
        }
        #endregion

        #region Methods : Static (Singletons)
        /// <summary>Retrieves (or creates) the singleton instance of the definition for the given package type.</summary>
        /// <param name="scriptUrl">The URL to the JavaScript file to load.</param>
        /// <param name="initMethod">The entry point method to invoke upon load completion.</param>
        public static PackageInfo SingletonFromUrl(string scriptUrl, string initMethod)
        {
            // Retrieve the existing singleton (if there is one).
            PackageInfo def = Helper.Collection.First(singletons, delegate(object o)
                                                                         {
                                                                             return ((PackageInfo)o).Id == scriptUrl.ToLowerCase();
                                                                         }) as PackageInfo;

            // Create and return the package-def.
            if (def == null)
            {
                def = new PackageInfo(scriptUrl, initMethod);
                singletons.Add(def);
            }

            // Finish up.
            return def;
        }
        #endregion
        
        #region Internal
        private static string GetName(string scriptUrl)
        {
            // Remove the '.js' and '.debug' end.
            StringHelper s = Helper.String;
            scriptUrl = s.RemoveEnd(scriptUrl, ".js");
            scriptUrl = s.RemoveEnd(scriptUrl, ".debug");

            // Remove the path.
            scriptUrl = s.StripPath(scriptUrl);

            // Finish up.)
            if (string.IsNullOrEmpty(scriptUrl.Trim())) scriptUrl = "<Untitled>".HtmlEncode();
            return scriptUrl;
        }
        #endregion
    }
}