using System.Runtime.CompilerServices;

namespace Open.Core
{
    /// <summary>The source and dependency references that define Part.</summary>
    public class PartDefinition : ModelBase
    {
        #region Head
        public const string PropScriptUrls = "ScriptUrls";
        public const string PropCssUrls = "CssUrls";
        public const string PropEntryPoint = "EntryPoint";

        /// <summary>Constructor.</summary>
        /// <param name="entryPoint">The entry point method to invoke.  This method must return a [Part] instance.</param>
        [AlternateSignature]
        public extern PartDefinition(string entryPoint);

        /// <summary>Constructor.</summary>
        /// <param name="entryPoint">The entry point method to invoke.  This static method must return a [Part] instance.</param>
        /// <param name="scriptUrls">
        ///     The paths to the script(s) required for the part.
        ///     If multiple scripts a semi-colon (;) seperated list is expected.
        /// </param>
        [AlternateSignature]
        public extern PartDefinition(string entryPoint, string scriptUrls);

        /// <summary>Constructor.</summary>
        /// <param name="entryPoint">The entry point method to invoke.  This static method must return a [Part] instance.</param>
        /// <param name="scriptUrls">
        ///     The paths to the script(s) required for the part.
        ///     If multiple scripts a semi-colon (;) seperated list is expected.
        /// </param>
        /// <param name="cssUrls">
        ///     The paths to the CSS file(s) required for the part.
        ///     If multiple CSS fiels a semi-colon (;) seperated list is expected.
        /// </param>
        public PartDefinition(string entryPoint, string scriptUrls, string cssUrls)
        {
            EntryPoint = entryPoint;
            ScriptUrls = scriptUrls;
            CssUrls = cssUrls;
        }
        #endregion

        #region Properties
        /// <summary>
        ///     Gets or sets the paths to the script(s) required for the part.
        ///     If multiple scripts a semi-colon (;) seperated list is expected.
        /// </summary>
        public string ScriptUrls
        {
            get { return (string)Get(PropScriptUrls, null); }
            set { Set(PropScriptUrls, value, null); }
        }

        /// <summary>
        ///     Gets or sets the paths to the CSS file(s) required for the part.
        ///     If multiple CSS fiels a semi-colon (;) seperated list is expected.
        /// </summary>
        public string CssUrls
        {
            get { return (string)Get(PropCssUrls, null); }
            set { Set(PropCssUrls, value, null); }
        }

        /// <summary>
        ///     Gets or sets the entry point method to invoke.  This static method must return a [Part] instance.
        /// </summary>
        public string EntryPoint
        {
            get { return (string)Get(PropEntryPoint, null); }
            set { Set(PropEntryPoint, value, null); }
        }
        #endregion
    }
}
