namespace Open.TestHarness
{
    /// <summary>Helper classes for examining methods.</summary>
    internal class MethodHelper
    {
        #region Head
        public const string KeyConstructor = "constructor";
        public const string KeyClassInitialize = "classInitialize";
        public const string KeyClassCleanup = "classCleanup";
        public const string KeyTestInitialize = "testInitialize";
        public const string KeyTestCleanup = "testCleanup";
        #endregion

        #region Methods
        /// <summary>Determines whether the specified method-name represents a constructor.</summary>
        /// <param name="methodName">The name of the method.</param>
        public static bool IsConstructor(string methodName) { return methodName == KeyConstructor; }

        /// <summary>Determines whether the specified method-name represents the 'ClassInitialize' method.</summary>
        /// <param name="methodName">The name of the method.</param>
        public static bool IsClassInitialize(string methodName) { return methodName == KeyClassInitialize; }

        /// <summary>Determines whether the specified method-name represents the 'ClassCleanup' method.</summary>
        /// <param name="methodName">The name of the method.</param>
        public static bool IsClassCleanup(string methodName) { return methodName == KeyClassCleanup; }

        /// <summary>Determines whether the specified method-name represents the 'TestInitialize' method.</summary>
        /// <param name="methodName">The name of the method.</param>
        public static bool IsTestInitialize(string methodName) { return methodName == KeyTestInitialize; }

        /// <summary>Determines whether the specified method-name represents the 'TestCleanup' method.</summary>
        /// <param name="methodName">The name of the method.</param>
        public static bool IsTestCleanup(string methodName) { return methodName == KeyTestCleanup; }

        /// <summary>Determines whether the specified DictionaryEntry is one of the special Setup/Teardown methods.</summary>
        public static bool IsSpecial(string methodName)
        {
            if (IsClassInitialize(methodName)) return true;
            if (IsClassCleanup(methodName)) return true;
            if (IsTestInitialize(methodName)) return true;
            if (IsTestCleanup(methodName)) return true;
            return false;
        }
        #endregion
    }
}
