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
    public class PartDefinition : PackageBase
    {
        #region Head
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
        public PartDefinition(string entryPoint, string scriptUrls, string resourceUrls) : base(entryPoint, scriptUrls, resourceUrls)
        {
        }
        #endregion

        #region Methods
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
            if (Script.IsNullOrUndefined(initializeOnComplete)) initializeOnComplete = true;

            // Progress flags.
            bool wasDownloaded = false;

            // Download async.
            Download(
                    delegate // Script downloaded.
                    {
                        wasDownloaded = true;

                        // Retrieve the part.
                        Part part = CreatePart();
                        if (part == null && HasError)
                        {
                            FinishLoad(part, onComplete);
                            return;
                        }

                        // Initialize the part.
                        SetupPart(part, container);
                        if (initializeOnComplete)
                        {
                            part.Initialize(delegate
                                                {
                                                    FinishLoad(part, onComplete);
                                                });
                        }
                        else
                        {
                            FinishLoad(part, onComplete);
                        }
                    }, 

                    delegate // Timed out (failure).
                    {
                        string msg = wasDownloaded
                                    ? string.Format("Failed to initialize the Part at '{0}'.  The Part did not call back from its 'OnInitialize' method.", EntryPoint)
                                    : string.Format("Failed to download the Part at '{0}'.  Timed out.", EntryPoint);
                        SetDownloadError(msg);
                        FinishLoad(null, onComplete);     
                    });
        }
        #endregion

        #region Internal

        private static void FinishLoad(Part part, PartCallback onComplete)
        {
            if (part != null) part.UpdateLayout();
            if (onComplete != null) onComplete(part);
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

        private void SetupPart(Part part, jQueryObject container)
        {
            // Setup initial conditions.
            if (part == null) return;

            // Assign default values.
            part.Definition = this;
            if (!Script.IsNullOrUndefined(container)) part.Container = container;
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
        #endregion
    }
}
