using System;
using Open.Core;
using Open.Testing.Internal;

namespace Open.Testing.Models
{
    /// <summary>Handles loading a test-package and executing the entry point assembly.</summary>
    public class PackageLoader : Package
    {
        #region Head
        private readonly PackageInfo parent;
        private readonly TestHarnessEvents events;

        /// <summary>Constructor.</summary>
        /// <param name="parent">The test-package this object is loading.</param>
        /// <param name="initMethod">The entry point method to invoke upon load completion.</param>
        /// <param name="scriptUrl">The URL to the JavaScript file to load.</param>
        public PackageLoader(PackageInfo parent, string initMethod, string scriptUrl) : base(initMethod, scriptUrl)
        {
            // Store values.
            this.parent = parent;
            events = Common.GetFromContainer().Events;

            // Wire up events.
            events.TestClassRegistered += OnTestClassRegistered;

            // Finish up.
            LogErrors = false;
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
            if (!IsLoading) return;
            parent.AddClass(e.TestClass);
        }
        #endregion

        #region Methods
        public override void Load(Action onComplete)
        {
            // Setup initial conditions.
            events.TestClassRegistered += OnTestClassRegistered;
            string link = Html.ToHyperlink(ScriptUrls, null, LinkTarget.Blank);

            // Start the load.
            Log.Info(string.Format("Downloading test-package: {0} ...", link));
            base.Load(delegate
                    {
                        if (!HasError)
                        {
                            // Success.
                            Log.Success("Test-package loaded successfully.");
                        }
                        else
                        {
                            // Failure.
                            string msg = TimedOut
                                    ? string.Format("<b>Failed</b> to download and initialize the test-package at '{0}'.  Please ensure the file exists.", link)
                                    : string.Format(
                                        "<b>Failed</b> to initialize the script-file at '{0}' with the entry method '{1}()'.<br/>Please ensure there aren't errors in any of the test-class constructors.<br/>Message: '{2}'",
                                        Html.ToHyperlink(ScriptUrls),
                                        EntryPoint,
                                        LoadError.Message);
                            Log.Error(msg);
                        }
                        Log.NewSection();

                        // Finish up.
                        events.TestClassRegistered -= OnTestClassRegistered;
                        Helper.Invoke(onComplete);
                    });
        }
        #endregion
    }
}