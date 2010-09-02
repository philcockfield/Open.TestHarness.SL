using System;
using jQueryApi;
using Open.Core;
using Open.Core.Helpers;

namespace Open.TestHarness
{
    /// <summary>Handles loading a test-package and executing the entry point assembly.</summary>
    internal class TestPackageLoader
    {
        #region Head
        private readonly string scriptUrl;
        private readonly string initMethod;
        private bool isLoaded;
        private Exception error;

        /// <summary>Constructor.</summary>
        /// <param name="scriptUrl">The URL to the JavaScript file to load.</param>
        /// <param name="initMethod">The entry point method to invoke upon load completion.</param>
        public TestPackageLoader(string scriptUrl, string initMethod)
        {
            this.scriptUrl = scriptUrl;
            this.initMethod = initMethod;
        }
        #endregion

        #region Properties
        /// <summary>Gets whether the script has been loaded.</summary>
        public bool IsLoaded { get { return isLoaded; } }

        /// <summary>Gets the error (if any) that occured during the Load operation.</summary>
        public Exception Error { get { return error; } }

        /// <summary>Gets or sets whether the load operation failed.</summary>
        public bool Succeeded { get { return Error == null; } }
        #endregion

        #region Methods
        /// <summary>Downloads the test-package.</summary>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        public void Load(Action onComplete)
        {
            // Setup initial conditions.);
            if (IsLoaded)
            {
                Helper.InvokeOrDefault(onComplete);
                return;
            }

            // Download the script.
            jQuery.GetScript(scriptUrl, delegate(object data)
                                            {
                                                // Invoke the entry-point method.
//                                                string eval = initMethod + "();";
                                                try
                                                {
                                                    Script.Eval(initMethod + "();");
                                                }
                                                catch (Exception e)
                                                {
                                                    Log.Error(
                                                        string.Format(
                                                                "Failed to initialize the script-file at '{0}' with the entry method '{1}()'.<br/>Message: {2}",
                                                                scriptUrl, initMethod, e.Message));
                                                    error = e;
                                                }

                                                // Finish up.
                                                isLoaded = true;
                                                Helper.InvokeOrDefault(onComplete);
                                            });
        }
        #endregion
    }
}
