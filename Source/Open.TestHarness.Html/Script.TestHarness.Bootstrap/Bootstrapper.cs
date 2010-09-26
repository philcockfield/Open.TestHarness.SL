using System;
using jQueryApi;
using Open.Core;
using Open.Core.Helpers;

namespace Open.Testing.Bootstrap
{
    /// <summary>Bootstraps the TestHarness.</summary>
    public class Bootstrapper
    {
        #region Head
        private const string Domain = "http://TestHarness.org";
        private const string UrlTestHarnessScript = "/Content/Scripts/Open.TestHarness.debug.js";
        private const string UrlBody = "/TestHarness/Body";
        private const string UrlCss = "/Content/Css/Open.TestHarness.css";
        private const string UrlCssIe = "/Content/Css/Open.TestHarness.IE.css";
        private const string InitMethod = "Open.Testing.Application.main()";

        private bool isScriptsLoaded;
        private bool isHtmlLoaded;
        private bool isInitialized;
        #endregion

        #region Event Handlers
        private void OnAssetLoaded()
        {
            if (isInitialized) return;
            if (!IsLoaded) return;
            ExecuteStartMethods();
        }
        #endregion

        #region Properties
        /// <summary>Gets whether all assets have been loaded.</summary>
        public bool IsLoaded { get { return isScriptsLoaded && isHtmlLoaded; } }
        #endregion

        #region Methods
        /// <summary>Starts the process of initialization.</summary>
        public void Start()
        {
            if (isInitialized) return;
            InsertCss();
            LoadScripts(delegate { isScriptsLoaded = true; OnAssetLoaded(); });
            LoadHtml(delegate { isHtmlLoaded = true; OnAssetLoaded(); });
        }
        #endregion

        #region Internal
        private void ExecuteStartMethods()
        {
            Script.Eval(InitMethod);
            isInitialized = true;
        }

        private static void InsertCss()
        {
            Css.InsertLink(Url(Css.Urls.Core));
            Css.InsertLink(Url(Css.Urls.CoreControls));
            Css.InsertLink(Url(Css.Urls.CoreLists));
            Css.InsertLink(Url(Css.Urls.JQueryUi));
            Css.InsertLink(Url(UrlCss));
            Css.InsertLink(Url(UrlCssIe));
        }

        /// <remarks>
        ///     Assumes existince of scripts:
        ///     - jQuery (core)
        ///     - mscorlib
        ///     - Open.Core
        /// </remarks>
        private static void LoadScripts(Action onComplete)
        {
            // Setup initial conditions.
            Helper.ScriptLoader.UseDebug = true;
            ScriptLoader loader = new ScriptLoader();

            // JQuery.
            loader.AddUrl(ScriptUrl(ScriptLibrary.JQueryUi));
            loader.AddUrl(ScriptUrl(ScriptLibrary.JQueryCookie));

            // Core scripts.
            loader.AddUrl(ScriptUrl(ScriptLibrary.Controls));
            loader.AddUrl(ScriptUrl(ScriptLibrary.Lists));

            // TestHarness.
            loader.AddUrl(Url(UrlTestHarnessScript));

            // Start the load operation.
            loader.LoadComplete += delegate { Helper.Invoke(onComplete); };
            loader.Start();
        }

        private static void LoadHtml(Action onComplete)
        {
            jQuery.Get(Url(UrlBody), delegate(object data)
                                    {
                                        jQuery.Select(Html.Body).Html(data.ToString());
                                        Helper.Invoke(onComplete);
                                    });
        }

        private static string ScriptUrl(ScriptLibrary library)
        {
            return string.Format("{0}{1}", Domain, Helper.ScriptLoader.Scripts.Url(library));
        }

        private static string Url(string path){return string.Format("{0}{1}", Domain, path);}
        #endregion
    }
}
