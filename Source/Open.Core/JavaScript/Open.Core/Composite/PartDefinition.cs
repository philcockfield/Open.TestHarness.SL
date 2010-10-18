using System;
using System.Runtime.CompilerServices;
using jQueryApi;
using Open.Core.Helpers;

namespace Open.Core
{
    /// <summary>A callback that passes back a Part.</summary>
    /// <param name="part">The part that has been loaded.</param>
    public delegate void PartCallback(Part part);

    /// <summary>The source and dependency references that define Part.</summary>
    public class PartDefinition : ModelBase
    {
        #region Head
        public const string PropScriptUrls = "ScriptUrls";
        public const string PropCssUrls = "CssUrls";
        public const string PropEntryPoint = "EntryPoint";
        public const string PropLoadError = "LoadError";
        public const string PropHasError = "HasError";
        public const string PropDownloadTimeout = "DownloadTimeout";

        public const string PathDivider = ";";
        public const double DefaultDownloadTimeout = 8;

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

        /// <summary>Gets the exception that occured during download or initialization (if one occured).</summary>
        public Exception LoadError
        {
            get { return (Exception)Get(PropLoadError, null); }
            set
            {
                if (Set(PropLoadError, value, null))
                {
                    FirePropertyChanged(PropHasError);
                }
            }
        }

        /// <summary>Gets or sets the timeout (in seconds) to wait while downloading the part before it is considered a failure.</summary>
        public double DownloadTimeout
        {
            get { return (double) Get(PropDownloadTimeout, DefaultDownloadTimeout); }
            set { Set(PropDownloadTimeout, value, DefaultDownloadTimeout); }
        }
        #endregion

        #region Properties : Boolean
        /// <summary>Gets whether a download error occured.</summary>
        public bool HasError { get { return LoadError != null; } }

        /// <summary>Gets whether the part has Script URLs.</summary>
        public bool HasScriptUrls { get { return Helper.String.HasValue(ScriptUrls); } }

        /// <summary>Gets whether the part has CSS file URLs.</summary>
        public bool HasCssUrls { get { return Helper.String.HasValue(CssUrls); } }

        /// <summary>Gets whether the part has an entry point.</summary>
        public bool HasEntryPoint { get { return Helper.String.HasValue(EntryPoint); } }
        #endregion

        #region Properties : Boolean (IsDownloaded)
        /// <summary>Gets whether all the required files have been downloaded.</summary>
        public bool IsDownloaded
        {
            get { return IsScriptsDownloaded && IsCssDownloaded; }
        }

        /// <summary>Gets whether the script file(s) have been downloaded.</summary>
        public bool IsScriptsDownloaded
        {
            get
            {
                if (!HasScriptUrls) return true; // Nothing to download.
                return CreateScriptLoader().AlreadyDownloaded;
            }
        }

        /// <summary>Gets whether the CSS file(s) have been inserted.</summary>
        public bool IsCssDownloaded
        {
            get
            {
                if (!HasCssUrls) return true; // Nothing to download.
                foreach (string url in CssUrls.Split(PathDivider))
                {
                    if (!Helper.String.HasValue(url)) continue;
                    if (!Css.IsLinked(url)) return false;
                }
                return true;
            }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return string.Format("[{0}]", GetType().Name);
        }
        #endregion

        #region Methods : Download
        /// <summary>Downloads and initializes the part.</summary>
        /// <param name="container">The container that the part resides within.</param>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        [AlternateSignature]
        public extern void Download(jQueryObject container, PartCallback onComplete);

        /// <summary>Downloads and initializes the part.</summary>
        /// <param name="container">The container that the part resides within.</param>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        /// <param name="initialize">Flag indicating if the part should be initialized after download.</param>
        public void Download(jQueryObject container, PartCallback onComplete, bool initialize)
        {
            // Setup initial conditions.
            if (!HasEntryPoint) throw new Exception("There is no entry point method to invoke.");
            InsertCssLinks();

            // Setup failure timeout.
            DelayedAction timeout = new DelayedAction(DownloadTimeout, delegate
                                    {
                                        string msg = string.Format("Failed to download the module at '{0}'.  Timed out.", ScriptUrls);
                                        SetDownloadError(msg);
                                        if (onComplete != null) onComplete(null);
                                    });
            timeout.Start();

            // Download the scripts
            DownloadScripts(delegate
                                {
                                    timeout.Dispose();

                                    // Retrieve and initialize the part.
                                    Part part = DynamicallyCreatePart();
                                    SetupPart(part, container);

                                    if (Script.IsNullOrUndefined(initialize) || initialize)
                                    {
                                        part.Initialize(delegate
                                                            {
                                                                // Finish up.
                                                                if (onComplete != null) onComplete(part);
                                                            });
                                    }
                                });
        }

        private void SetupPart(Part part, jQueryObject container)
        {
            // Setup initial conditions.
            if (part == null) return;

            // Assign default values.
            part.Definition = this;
            if (!Script.IsNullOrUndefined(container)) part.Container = container;
        }
        #endregion

        #region Internal
        private void InsertCssLinks()
        {
            if (!HasCssUrls) return;
            foreach (string url in CssUrls.Split(PathDivider))
            {
                // NB: Does not insert the same link multiple times within the page.
                Css.InsertLink(FormatUrl(url));
            }
        }

        private void DownloadScripts(Action callback)
        {
            // Setup initial conditions.
            if (!HasScriptUrls) callback();

            // Prepare the loader.
            ScriptLoader loader = CreateScriptLoader();

            // Load the script(s).
            loader.LoadComplete += delegate { callback(); };
            loader.Start();
        }

        private ScriptLoader CreateScriptLoader()
        {
            ScriptLoader loader = new ScriptLoader();
            foreach (string url in ScriptUrls.Split(PathDivider))
            {
                loader.AddUrl(FormatUrl(url), PathDivider);
            }
            return loader;
        }

        private static string FormatUrl(string url)
        {
            url = Helper.String.HasValue(url) ? url.Trim() : null;
            return url;
        }

        private string FormatMethod()
        {
            StringHelper helper = Helper.String;
            string entryPoint = EntryPoint;
            entryPoint = helper.RemoveEnd(entryPoint, ";");
            entryPoint = helper.RemoveEnd(entryPoint, "()");

            string[] parts = entryPoint.Split(".");
            string name = parts[parts.Length - 1];

            return string.Format("{0}{1}()",
                                helper.RemoveEnd(entryPoint, name),
                                name.ToLocaleLowerCase());
        }

        private Part DynamicallyCreatePart()
        {
            // Invoke the entry point method.
            string method = FormatMethod();
            try
            {
                // Create the part.
                Part part = null;
                string script = string.Format("part = {0};", method);
                Script.Eval(script);

                // Finish up.
                return part;
            }
            catch (Exception error)
            {
                // Ignore.
                string msg = string.Format(
                                "Failed to initialize the Part at '{0}' with the entry method '{1}'.  Ensure the method exists and returns a [Part].<br/>Message: {2}",
                                ScriptUrls,
                                method,
                                error.Message);
                SetDownloadError(msg);
            }
            return null;
        }

        private void SetDownloadError(string msg)
        {
            Log.Error(msg);
            LoadError = new Exception(msg);
        }
        #endregion
    }
}
