using System;
using Open.Core.Helpers;

namespace Open.Core
{
    /// <summary>Static index of helpers.</summary>
    public static class Helper
    {
        #region Head
        private static readonly DelegateHelper DelegateHelper = new DelegateHelper();
        private static readonly JsonHelper JsonHelper = new JsonHelper();
        private static readonly ReflectionHelper ReflectionHelper = new ReflectionHelper();
        private static readonly ScriptLoadHelper ScriptLoadHelper = new ScriptLoadHelper();
        private static int idCounter;
        #endregion

        #region Properties
        /// <summary>Gets the helper for working with Delegates.</summary>
        public static DelegateHelper Delegate{get { return DelegateHelper; }}

        /// <summary>Gets the helper for working with Delegates.</summary>
        public static JsonHelper Json { get { return JsonHelper; } }

        /// <summary>Gets the helper for working with reflection.</summary>
        public static ReflectionHelper Reflection { get { return ReflectionHelper; } }

        /// <summary>Gets the helper for downloading scripts.</summary>
        public static ScriptLoadHelper ScriptLoader { get { return ScriptLoadHelper; } }
        #endregion

        #region Methods
        /// <summary>Invokes the given action if it's not Null/Undefined.</summary>
        /// <param name="action">The action to invoke.</param>
        public static void InvokeOrDefault(Action action)
        {
            if (!Script.IsNullOrUndefined(action)) action();
        }

        /// <summary>Creates a unique identifier.</summary>
        public static string CreateId()
        {
            idCounter++;
            return string.Format("g.{0}", idCounter);
        }
        #endregion
    }
}
