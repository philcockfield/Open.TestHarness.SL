using System;
using System.Collections;
using System.Html;
using jQueryApi;

namespace Open.Core.Helpers
{
    /// <summary>The various Core script libraries.</summary>
    public enum ScriptLibrary
    {
        /// <summary>The root Core library.</summary>
        Core = 0,

        /// <summary>The general set of UI controls.</summary>
        Controls = 1,

        /// <summary>The List controls.</summary>
        Lists = 2,

        /// <summary>Core jQuery library</summary>
        JQuery = 3,

        /// <summary>The jQuery UI library.</summary>
        JQueryUi = 4,

        /// <summary>The jQuery Cookie plugin.</summary>
        JQueryCookie = 5,
    }

    /// <summary>Utility methods for loading scripts.</summary>
    public class ScriptLoadHelper : ModelBase
    {
        #region Head
        public const string PropIsListsLoaded = "IsListsLoaded";
        public const string PropIsViewsLoaded = "IsViewsLoaded";

        private string rootScriptFolder = "/Open.Core/Scripts/";
        private bool useDebug;
        private JitScriptLoader jit;
        private readonly ArrayList loadedUrls = new ArrayList();
        private ScriptNames scripts;
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the debug version of scripts should be used.</summary>
        public bool UseDebug
        {
            get { return useDebug; }
            set { useDebug = value; }
        }

        /// <summary>Gets or sets the root folder where the script libraries are housed.</summary>
        public string RootScriptFolder
        {
            get { return rootScriptFolder; }
            set { rootScriptFolder = value; }
        }

        /// <summary>Gets the JIT (visualization library) loader.</summary>
        public JitScriptLoader Jit { get { return jit ?? (jit = new JitScriptLoader()); } }

        /// <summary>An index of script names.</summary>
        public ScriptNames Scripts { get { return scripts ?? (scripts = new ScriptNames()); } }
        #endregion

        #region Methods : Load
        /// <summary>Determines whether a SCRIPT block with the specified url exists in the DOM.</summary>
        /// <param name="url">The URL of the script block to look for.</param>
        public bool IsDeclared(string url)
        {
            // Setup initial conditions.
            url = url.ToLowerCase();
            jQueryObject allScripts = jQuery.Select("script[type='text/javascript']");

            // Loop all SCRIPT references looking for a URL match.
            bool exists = false;
            allScripts.Each(delegate(int index, Element element)
                             {
                                 object attr = element.GetAttribute(Html.Src);
                                 if (attr != null && attr.ToString().ToLowerCase() == url)
                                 {
                                     exists = true;
                                     return false; // Exit loop.
                                 }
                                 return true; // Continue loop
                             });

            // Finish up.
            return exists; 
        }

        /// <summary>
        ///     Determines whether the specified URL has been loaded (either via one of the Load methods, 
        ///     or is decalred within the page).
        /// </summary>
        /// <param name="url">The URL to look for.</param>
        public bool IsLoaded(string url)
        {
            if (Script.IsNullOrUndefined(url)) return false;
            url = url.ToLowerCase();
            if (loadedUrls.Contains(url)) return true;
            if (IsDeclared(url)) return true;
            return false;
        }

        /// <summary>Determines whether the specified library has been loaded.</summary>
        /// <param name="library">The library to look for.</param>
        public bool IsLibraryLoaded(ScriptLibrary library)
        {
            if (loadedUrls.Contains(Scripts.Url(library))) return true;
            if (IsDeclared(Scripts.Url(library))) return true;
            return false;
        }

        /// <summary>
        ///     Loads the specified SCRIPT file.
        ///     This passes execution to 'jQuery.GetScript' but keep track of the URL as being loaded
        ///     so future checks to IsLoaded will report the script is in existence on the client.
        /// </summary>
        /// <param name="url">The URL of the SCRIPT file to load.</param>
        /// <param name="callback">Callback to invoke upon completion.</param>
        public void Load(string url, AjaxCallback callback)
        {
            jQuery.GetScript(url, delegate(object data)
                                      {
                                          CacheUrl(url);
                                          if (callback != null) callback(data);
                                      });
        }

        /// <summary>Loads the specified library.</summary>
        /// <param name="library">Flag indicating the library to load.</param>
        /// <param name="callback">Callback to invoke upon completion.</param>
        public void LoadLibrary(ScriptLibrary library, Action callback)
        {
            // Setup initial conditions.
            if (IsLibraryLoaded(library))
            {
                Helper.Invoke(callback);
                return;
            }

            // Download script.
            ScriptLoader loader = new ScriptLoader();
            string url = Scripts.Url(library);
            loader.LoadComplete += delegate
                                       {
                                           CacheUrl(url);
                                           Helper.Invoke(callback);
                                       };
            loader.AddUrl(url);
            loader.Start();
        }

        /// <summary>Retrieves the URL of a script.</summary>
        /// <param name="path">Sub path to the URL (null if in root Scripts folder).</param>
        /// <param name="fileName">The script file name.</param>
        /// <param name="debugVersion">Flag indicating if the Debug version should be used.</param>
        internal string Url(string path, string fileName, bool debugVersion)
        {
            return string.Format(RootScriptFolder + path + FileName(fileName, debugVersion));
        }
        #endregion

        #region Methods : Internal
        internal string FileName(string name, bool hasDebug)
        {
            string debug = hasDebug && UseDebug ? ".debug" : null;
            return string.Format("{0}{1}.js", name, debug);
        }
        #endregion

        #region Internal
        private void CacheUrl(string url)
        {
            loadedUrls.Add(url.ToLowerCase());
        }
        #endregion
    }

    /// <summary>Index of script names.</summary>
    public class ScriptNames
    {
        public static readonly string Core = "Open.Core";
        public static readonly string CoreControls = "Open.Core.Controls";
        public static readonly string CoreLists = "Open.Core.Lists";
        public static readonly string LibraryJit = "Open.Library.Jit";
        public static readonly string JQuery = "jquery-1.4.2.min";
        public static readonly string JQueryUi = "jquery-ui-1.8.4.custom.min";
        public static readonly string JQueryCookie = "jquery.cookie";

        /// <summary>Retrives the of the file corresponding to the given library.</summary>
        /// <param name="library">Flag for the library to retreive.</param>
        public string ToName(ScriptLibrary library)
        {
            switch (library)
            {
                case ScriptLibrary.Core: return Core;
                case ScriptLibrary.Controls: return CoreControls;
                case ScriptLibrary.Lists: return CoreLists;
                case ScriptLibrary.JQuery: return JQuery;
                case ScriptLibrary.JQueryUi: return JQueryUi;
                case ScriptLibrary.JQueryCookie: return JQueryCookie;
                default: throw new Exception(string.Format("{0} not supported.", library.ToString()));
            }
        }

        /// <summary>Get the URL for the given library.</summary>
        /// <param name="library">Flag for the library to retreive.</param>
        public string Url(ScriptLibrary library)
        {
            // Setup initial conditions.
            bool useDebug = Helper.ScriptLoader.UseDebug;

            // Get the sub path.
            string path = string.Empty;
            switch (library)
            {
                case ScriptLibrary.JQuery:
                case ScriptLibrary.JQueryUi:
                case ScriptLibrary.JQueryCookie:
                    path = "JQuery/";
                    useDebug = false;
                    break;
            }

            // Retrieve the URL.
            return Helper.ScriptLoader.Url(path, ToName(library), useDebug);
        }
    }
}
