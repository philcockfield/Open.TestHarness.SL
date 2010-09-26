using System;
using jQueryApi;
using Open.Core;
using Open.Testing.Internal;

namespace Open.Testing.Models
{
    /// <summary>Handles loading a test-package and executing the entry point assembly.</summary>
    public class PackageLoader : TestHarnessControllerBase, IDisposable
    {
        #region Head
        private readonly PackageInfo parent;
        private readonly string scriptUrl;
        private readonly string initMethod;
        private bool isLoaded;
        private Exception error;
        private bool isInitializing;
        private readonly TestHarnessEvents events;

        /// <summary>Constructor.</summary>
        /// <param name="parent">The test-package this object is loading.</param>
        /// <param name="scriptUrl">The URL to the JavaScript file to load.</param>
        /// <param name="initMethod">The entry point method to invoke upon load completion.</param>
        public PackageLoader(PackageInfo parent, string scriptUrl, string initMethod)
        {
            // Store values.
            this.parent = parent;
            this.scriptUrl = scriptUrl;
            this.initMethod = initMethod;
            events = Common.Events;

            // Wire up events.
            events.TestClassRegistered += OnTestClassRegistered;
        }

        protected override void OnDisposed()
        {
            events.TestClassRegistered -= OnTestClassRegistered;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnTestClassRegistered(object sender, TestClassEventArgs e)
        {
            if (!isInitializing) return;
            parent.AddClass(e.TestClass);
        }
        #endregion

        #region Properties
        /// <summary>Gets the URL to the JavaScript file to load.</summary>
        public string ScriptUrl { get { return scriptUrl; } }

        /// <summary>Gets the entry point method to invoke upon load completion.</summary>
        public string InitMethod { get { return initMethod; } }

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
                Helper.Invoke(onComplete);
                return;
            }

            // Download the script.
            jQuery.GetScript(scriptUrl, delegate(object data)
                                            {
                                                if (IsDisposed) return; // Bail out if the downloader has been disposed.

                                                // Execute the entry-point method.
                                                try
                                                {
                                                    isInitializing = true;
                                                    Script.Eval(initMethod + "();");
                                                }
                                                catch (Exception e)
                                                {
                                                    Log.Error(string.Format(
                                                                    "<b>Failed</b> to initialize the script-file at '{0}' with the entry method '{1}()'.<br/>Please ensure there aren't errors in any of the test-class constructors.<br/>Message: '{2}'",
                                                                    Html.ToHyperlink(scriptUrl), 
                                                                    initMethod, 
                                                                    e.Message));
                                                    error = e;
                                                }
                                                finally
                                                {
                                                    isInitializing = false;
                                                }

                                                // Finish up.
                                                isLoaded = Succeeded;
                                                Helper.Invoke(onComplete);
                                            });
        }
        #endregion
    }
}