using System;
using System.Collections;
using Open.Core;

namespace Open.TestHarness.Models
{
    /// <summary>Represents a single test method.</summary>
    public class TestMethodInfo : ModelBase
    {
        #region Head
        public const string KeyConstructor = "constructor";
        public const string KeyGetter = "get_";
        public const string KeySetter = "set_";
        public const string KeyField = "_";
        public const string KeyFunction = "function";

        private readonly TestClassInfo testClass;
        private readonly string name;
        private readonly string displayName;

        /// <summary>Constructor.</summary>
        /// <param name="testClass">The test-class that this method is a member of.</param>
        /// <param name="name">The name of the method.</param>
        public TestMethodInfo(TestClassInfo testClass, string name)
        {
            this.testClass = testClass;
            this.name = name;
            displayName = FormatName(name);
        }
        #endregion

        #region Properties
        /// <summary>Gets the test-class that this method is a member of.</summary>
        public TestClassInfo TestClass { get { return testClass; } }

        /// <summary>Gets the name of the method.</summary>
        public string Name { get { return name; } }

        /// <summary>Gets the display version of the name.</summary>
        public string DisplayName { get { return displayName; } }
        #endregion

        #region Methods
        /// <summary>Determines whether the specified DictionaryEntry represents a valid test-method.</summary>
        /// <param name="item">The Dictionaty item to examine.</param>
        public static bool IsTestMethod(DictionaryEntry item)
        {
            string key = item.Key;
            if (Type.GetScriptType(item.Value) != KeyFunction) return false;
            if (key.StartsWith(KeyField)) return false;
            if (key.StartsWith(KeyGetter)) return false;
            if (key.StartsWith(KeySetter)) return false;
            if (key == KeyConstructor) return false;
            return true;
        }

        /// <summary>Formats a name into a display name (replace underscores with spaces etc.).</summary>
        /// <param name="name">The name to format.</param>
        public static string FormatName(string name)
        {
            name = name.Replace("__", ": ");
            name = name.Replace("_", " ");
            name = Helper.String.ToSentenceCase(name);
            return name;
        }
        #endregion
    }
}
