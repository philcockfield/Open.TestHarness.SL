namespace Open.Core
{
    /// <summary>Static index of helpers.</summary>
    public static class Helper
    {
        #region Head
        private static readonly DelegateHelper DelegateHelper = new DelegateHelper();
        private static readonly JsonHelper JsonHelper = new JsonHelper();
        private static readonly ReflectionHelper ReflectionHelper = new ReflectionHelper();
        #endregion

        #region Properties
        /// <summary>Gets the helper for working with Delegates.</summary>
        public static DelegateHelper Delegate{get { return DelegateHelper; }}

        /// <summary>Gets the helper for working with Delegates.</summary>
        public static JsonHelper Json { get { return JsonHelper; } }

        /// <summary>Gets the helper for working with reflection.</summary>
        public static ReflectionHelper Reflection { get { return ReflectionHelper; } }
        #endregion
    }
}
