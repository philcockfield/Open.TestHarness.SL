using System;
using System.Runtime.CompilerServices;

namespace Open.Core
{
    public class Package : PackageBase
    {
        #region Head
        /// <summary>Constructor.</summary>
        /// <param name="entryPoint">The entry point method to invoke.</param>
        [AlternateSignature]
        public extern Package(string entryPoint);

        /// <summary>Constructor.</summary>
        /// <param name="entryPoint">The entry point method to invoke.</param>
        /// <param name="scriptUrls">
        ///     The paths to the script(s) required for the package.
        ///     If multiple scripts a semi-colon (;) seperated list is expected.
        /// </param>
        [AlternateSignature]
        public extern Package(string entryPoint, string scriptUrls);

        /// <summary>Constructor.</summary>
        /// <param name="entryPoint">The entry point method to invoke.</param>
        /// <param name="scriptUrls">
        ///     The paths to the script(s) required for the package.
        ///     If multiple scripts a semi-colon (;) seperated list is expected.
        /// </param>
        /// <param name="resourceUrls">
        ///     The paths to the resource file(s) that are required by the package (images, CSS).
        ///     Multiple files are seperated by a semi-colon (;).
        ///     Images (.png .jpg) are pre-loaded using the [ImagePreloader].
        /// </param>
        public Package(string entryPoint, string scriptUrls, string resourceUrls) : base(entryPoint, scriptUrls, resourceUrls)
        {
        }
        #endregion

        #region Methods
        /// <summary>Loads the package (downloading resources as required) and invokes the EntryPoint method upon completion.</summary>
        [AlternateSignature]
        public virtual extern void Load();

        /// <summary>Loads the package (downloading resources as required) and invokes the EntryPoint method upon completion.</summary>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        public virtual void Load(Action onComplete)
        {
            // Start the download.
            DownloadAsync(
                    delegate // Script(s) downloaded.
                    {
                        // Finish up.
                        InvokeEntryPoint();
                        Helper.Invoke(onComplete);
                    },
                    delegate // Timed out (failure).
                    {
                        SetDownloadError(
                                        string.Format("Failed to download the package at '{0}'. Timed out after {1} seconds.", 
                                        EntryPoint, 
                                        DownloadTimeout));
                        Helper.Invoke(onComplete);
                    });
        }
        #endregion

        #region Internal
        private void InvokeEntryPoint()
        {
            try
            {
                Script.Eval(EntryPoint + ";");
            }
            catch (Exception error)
            {
                // Ignore.
                string msg = string.Format(
                                "Failed to initialize the package with the entry method '{0}'. Ensure the static method exists.<br/>Message: {1}",
                                EntryPoint,
                                error.Message);
                SetDownloadError(msg);
            }
        }
        #endregion
    }
}
