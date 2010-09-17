using System;
using System.Collections;

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
        private readonly ArrayList loadedLibraries = new ArrayList();
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
        /// <summary>Determines whether the specified library has been loaded.</summary>
        /// <param name="library">The library to look for.</param>
        public bool IsLoaded(ScriptLibrary library) { return loadedLibraries.Contains(library); }

        /// <summary>Loads the specified library.</summary>
        /// <param name="library">Flag indicating the library to load.</param>
        /// <param name="callback">Callback to invoke upon completion.</param>
        public void LoadLibrary(ScriptLibrary library, Action callback)
        {
            // Setup initial conditions.
            if (IsLoaded(library))
            {
                Helper.InvokeOrDefault(callback);
                return;
            }

            // Download script.
            ScriptLoader loader = new ScriptLoader();
            loader.LoadComplete += delegate
                                       {
                                           loadedLibraries.Add(library);
                                           Helper.InvokeOrDefault(callback);
                                       };
            loader.AddUrl(Scripts.Url(library));
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
