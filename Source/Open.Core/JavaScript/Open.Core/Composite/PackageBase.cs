using System;
using System.Runtime.CompilerServices;
using Open.Core.Helpers;

namespace Open.Core
{
    /// <summary>A set of script(s) and other dependencies with an entry-point method.</summary>
    public abstract class PackageBase : ModelBase
    {
        #region Head
        public const string PropScriptUrls = "ScriptUrls";
        public const string PropResourceUrls = "ResourceUrls";
        public const string PropEntryPoint = "EntryPoint";
        public const string PropLoadError = "LoadError";
        public const string PropHasError = "HasError";
        public const string PropDownloadTimeout = "DownloadTimeout";
        public const string PropTimedOut = "TimedOut";
        public const string PropIsLoading = "IsLoading";
        public const string PropLogErrors = "LogErrors";

        public const string PathDivider = ";";
        public const double DefaultDownloadTimeout = 8;

        /// <summary>Constructor.</summary>
        /// <param name="entryPoint">The entry point method to invoke.</param>
        [AlternateSignature]
        protected extern PackageBase(string entryPoint);

        /// <summary>Constructor.</summary>
        /// <param name="entryPoint">The entry point method to invoke.</param>
        /// <param name="scriptUrls">
        ///     The paths to the script(s) required for the part.
        ///     If multiple scripts a semi-colon (;) seperated list is expected.
        /// </param>
        [AlternateSignature]
        protected extern PackageBase(string entryPoint, string scriptUrls);

        /// <summary>Constructor.</summary>
        /// <param name="entryPoint">The entry point method to invoke.</param>
        /// <param name="scriptUrls">
        ///     The paths to the script(s) required for the part.
        ///     If multiple scripts a semi-colon (;) seperated list is expected.
        /// </param>
        /// <param name="resourceUrls">
        ///     The paths to the resource file(s) that are required by the part (images, CSS).
        ///     Multiple files are seperated by a semi-colon (;).
        ///     Images (.png .jpg) are pre-loaded using the [ImagePreloader].
        /// </param>
        protected PackageBase(string entryPoint, string scriptUrls, string resourceUrls)
        {
            EntryPoint = entryPoint;
            ScriptUrls = scriptUrls;
            ResourceUrls = resourceUrls;
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
        ///     Gets or sets the paths to the CSS and image file(s) required for the part.
        ///     Multiple files are seperated by a semi-colon (;).
        ///     Images (.png .jpg) are pre-loaded using the [ImagePreloader].
        /// </summary>
        public string ResourceUrls
        {
            get { return (string)Get(PropResourceUrls, null); }
            set { Set(PropResourceUrls, value, null); }
        }

        /// <summary>
        ///     Gets or sets the entry point method to invoke.
        /// </summary>
        public string EntryPoint
        {
            get { return (string)Get(PropEntryPoint, null); }
            set
            {
                value = FormatMethod(value);
                Set(PropEntryPoint, value, null);
            }
        }

        /// <summary>Gets the exception that occured during download or initialization (if one occured).</summary>
        public Exception LoadError
        {
            get { return (Exception)Get(PropLoadError, null); }
            internal set
            {
                if (Set(PropLoadError, value, null))
                {
                    FirePropertyChanged(PropHasError);
                }
            }
        }

        /// <summary>Gets whether a download error occured.</summary>
        public bool HasError { get { return LoadError != null; } }

        /// <summary>Gets or sets the timeout (in seconds) to wait while downloading the part before it is considered a failure.</summary>
        public double DownloadTimeout
        {
            get { return (double) Get(PropDownloadTimeout, DefaultDownloadTimeout); }
            set { Set(PropDownloadTimeout, value, DefaultDownloadTimeout); }
        }

        /// <summary>Gets whether the load error is due to the operation timing out.</summary>
        public bool TimedOut
        {
            get { return (bool) Get(PropTimedOut, false); }
            private set { Set(PropTimedOut, value, false); }
        }

        /// <summary>Gets whether the package is currently in the process of loading.</summary>
        public bool IsLoading
        {
            get { return (bool) Get(PropIsLoading, false); }
            set { Set(PropIsLoading, value, false); }
        }

        /// <summary>Gets or sets whether error are logged.</summary>
        public bool LogErrors
        {
            get { return (bool) Get(PropLogErrors, true); }
            set { Set(PropLogErrors, value, true); }
        }
        #endregion

        #region Properties : Boolean (IsDownloaded)
        /// <summary>Gets whether all the required files have been downloaded.</summary>
        public bool IsDownloaded
        {
            get { return IsScriptsDownloaded && IsResourcesDownloaded; }
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

        /// <summary>Gets whether the defined resource file(s) have been loaded into the page.</summary>
        public bool IsResourcesDownloaded
        {
            get
            {
                if (!HasResourceUrls) return true; // Nothing to download.
                foreach (string url in ResourceUrls.Split(PathDivider))
                {
                    if (!Helper.String.HasValue(url)) continue;
                    if (!Css.IsLinked(url)) return false;

                    // TODO - Extend to include all resource types (images + CSS).
                }
                return true;
            }
        }
        #endregion

        #region Properties (Internal)
        /// <summary>Gets whether the part has Script URLs.</summary>
        private bool HasScriptUrls { get { return Helper.String.HasValue(ScriptUrls); } }

        /// <summary>Gets whether the part has CSS file URLs.</summary>
        private bool HasResourceUrls { get { return Helper.String.HasValue(ResourceUrls); } }

        /// <summary>Gets whether the part has an entry point.</summary>
        private bool HasEntryPoint { get { return Helper.String.HasValue(EntryPoint); } }
        #endregion

        #region Methods
        public override string ToString()
        {
            return string.Format("[{0}]", GetType().Name);
        }

        /// <summary>Downloads the resources defined for the package.</summary>
        /// <param name="onScriptsDownloaded">Invoked when the scripts have completed downloading.</param>
        /// <param name="onTimedOut">Invoked when/if the operation times out (NB: 'onScriptsDownloaded' is never called if the operation times out).</param>
        protected void DownloadAsync(Action onScriptsDownloaded, Action onTimedOut)
        {
            // Setup initial conditions.
            if (!HasEntryPoint) throw new Exception("There is no entry point method for the Part.");
            TimedOut = false;
            IsLoading = true;

            // Insert resources.
            InsertCssLinks();
            // TODO - Extend to include all resource types (images + CSS).

            // Setup failure timeout.
            DelayedAction timeout = new DelayedAction(DownloadTimeout, delegate
                                    {
                                        TimedOut = true;
                                        IsLoading = false;
                                        Helper.Invoke(onTimedOut);
                                    });
            timeout.Start();

            // Download the scripts
            DownloadScripts(delegate
                                {
                                    if (!TimedOut) // Only invoke if the timeout hasn't elapsed before this callback.
                                    {
                                        Helper.Invoke(onScriptsDownloaded);
                                    }
                                    timeout.Dispose();
                                    IsLoading = false;
                                });
        }

        /// <summary>Logs the given error message and assigns it to an Exception held within the 'LoadError' property.</summary>
        /// <param name="message">The error message.</param>
        protected void SetDownloadError(string message)
        {
            if (LogErrors) Log.Error(message);
            LoadError = new Exception(message);
        }
        #endregion

        #region Internal
        private void InsertCssLinks()
        {
            if (!HasResourceUrls) return;
            foreach (string url in ResourceUrls.Split(PathDivider))
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

        private static string FormatMethod(string method)
        {
            if (Script.IsUndefined(method) || string.IsNullOrEmpty(method)) return null;

            StringHelper helper = Helper.String;
            method = helper.RemoveEnd(method, ";");
            method = helper.RemoveEnd(method, "()");

            string[] parts = method.Split(".");
            string name = parts[parts.Length - 1];

            return string.Format("{0}{1}()",
                                 helper.RemoveEnd(method, name),
                                 helper.ToCamelCase(name));
        }
        #endregion
    }
}
