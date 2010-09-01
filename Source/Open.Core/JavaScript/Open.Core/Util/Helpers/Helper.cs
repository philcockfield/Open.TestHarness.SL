using System;
using Open.Core.Helpers;

namespace Open.Core
{
    /// <summary>Static index of helpers.</summary>
    public static class Helper
    {
        #region Head
        private static DelegateHelper DelegateHelper;
        private static JsonHelper JsonHelper;
        private static ReflectionHelper ReflectionHelper;
        private static ScriptLoadHelper ScriptLoadHelper;
        private static CollectionHelper CollectionHelper;
        private static StringHelper StringHelper;
        private static NumberHelper NumberHelper;
        private static ScrollHelper ScrollHelper;

        private static int idCounter;
        #endregion

        #region Properties
        /// <summary>Gets the helper for working with Delegates.</summary>
        public static DelegateHelper Delegate { get { return DelegateHelper ?? (DelegateHelper = new DelegateHelper()); } }

        /// <summary>Gets the helper for working with Delegates.</summary>
        public static JsonHelper Json { get { return JsonHelper ?? (JsonHelper = new JsonHelper()); } }

        /// <summary>Gets the helper for working with reflection.</summary>
        public static ReflectionHelper Reflection { get { return ReflectionHelper ?? (ReflectionHelper = new ReflectionHelper()); } }

        /// <summary>Gets the helper for downloading scripts.</summary>
        public static ScriptLoadHelper ScriptLoader { get { return ScriptLoadHelper ?? (ScriptLoadHelper = new ScriptLoadHelper()); } }

        /// <summary>Gets the helper for working with collections.</summary>
        public static CollectionHelper Collection { get { return CollectionHelper ?? (CollectionHelper = new CollectionHelper()); } }

        /// <summary>Gets the helper for working with strings.</summary>
        public static StringHelper String { get { return StringHelper ?? (StringHelper = new StringHelper()); } }

        /// <summary>Gets the helper for working with numbers.</summary>
        public static NumberHelper Number { get { return NumberHelper ?? (NumberHelper = new NumberHelper()); } }

        /// <summary>Gets the helper for working with scrolling.</summary>
        public static ScrollHelper Scroll { get { return ScrollHelper ?? (ScrollHelper = new ScrollHelper()); } }
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
