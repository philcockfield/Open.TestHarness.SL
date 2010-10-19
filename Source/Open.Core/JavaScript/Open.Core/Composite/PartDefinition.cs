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
        public const string PropResourceUrls = "ResourceUrls";
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
        /// <param name="resourceUrls">
        ///     The paths to the resource file(s) that are required by the part (images, CSS).
        ///     Multiple files are seperated by a semi-colon (;).
        ///     Images (.png .jpg) are pre-loaded using the [ImagePreloader].
        /// </param>
        public PartDefinition(string entryPoint, string scriptUrls, string resourceUrls)
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
        #endregion

        #region Methods : Download
        /// <summary>Loads and initializes the part (downloading resources as required).</summary>
        /// <param name="container">The container that the part resides within.</param>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        [AlternateSignature]
        public extern virtual void Load(jQueryObject container, PartCallback onComplete);

        /// <summary>Loads and optionally initializes the part (downloading resources as required).</summary>
        /// <param name="container">The container that the part resides within.</param>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        /// <param name="initializeOnComplete">Flag indicating if the part should be initialized after download.</param>
        public virtual void Load(jQueryObject container, PartCallback onComplete, bool initializeOnComplete)
        {
            // Setup initial conditions.
            if (!HasEntryPoint) throw new Exception("There is no entry point method for the Part.");
            if (Script.IsNullOrUndefined(initializeOnComplete)) initializeOnComplete = true;

            // Insert resources.
            InsertCssLinks();
            // TODO - Extend to include all resource types (images + CSS).

            // Progress flags.
            bool downloaded = false;

            // Setup failure timeout.
            DelayedAction timeout = new DelayedAction(DownloadTimeout, delegate
                                    {
                                        string msg = downloaded
                                                    ? string.Format("Failed to initialize the Part at '{0}'.  The Part did not call back from its 'OnInitialize' method.", EntryPoint)
                                                    : string.Format("Failed to download the Part at '{0}'.  Timed out.", EntryPoint);
                                        SetDownloadError(msg);
                                        FinishLoad(null, null, onComplete);
                                    });
            timeout.Start();

            // Download the scripts
            DownloadScripts(delegate
                                {
                                    downloaded = true;

                                    // Retrieve the part.
                                    Part part = CreatePart();
                                    if (part == null && HasError)
                                    {
                                        FinishLoad(timeout, part, onComplete);
                                        return;
                                    }

                                    // Initialize the part.
                                    SetupPart(part, container);
                                    if (initializeOnComplete)
                                    {
                                        if (!timeout.IsRunning) return; // Callback occurs after timeout elapsed.
                                        part.Initialize(delegate
                                                            {
                                                                FinishLoad(timeout, part, onComplete);
                                                            });
                                    }
                                    else
                                    {
                                        FinishLoad(timeout, part, onComplete);
                                    }
                                });
        }

        private static void FinishLoad(DelayedAction timeout, Part part, PartCallback onComplete)
        {
            if (timeout != null)
            {
                if (!timeout.IsRunning) return; // Callback occurs after timeout elapsed.
                timeout.Dispose();
            }
            if (part != null) part.UpdateLayout();
            if (onComplete != null) onComplete(part);
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

        private Part CreatePart()
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
