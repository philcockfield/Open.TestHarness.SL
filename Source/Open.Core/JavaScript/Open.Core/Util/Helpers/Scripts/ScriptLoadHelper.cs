using System;
using System.Collections;

namespace Open.Core.Helpers
{
    /// <summary>The various Core script libraries.</summary>
    public enum ScriptLibrary
    {
        /// <summary>The general set of UI controls.</summary>
        Controls = 0,

        /// <summary>The List controls.</summary>
        Lists = 1,
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
            loader.AddUrl(Helper.ScriptLoader.Url(string.Empty, ToLibraryName(library), true));
            loader.Start();
        }
        #endregion

        #region Methods : Internal
        internal string Url(string path, string fileName, bool hasDebug)
        {
            return string.Format(RootScriptFolder + path + FileName(fileName, hasDebug));
        }

        internal string FileName(string name, bool hasDebug)
        {
            string debug = hasDebug && UseDebug ? ".debug" : null;
            return string.Format("{0}{1}.js", name, debug);
        }

        private static string ToLibraryName(ScriptLibrary library)
        {
            switch (library)
            {
                case ScriptLibrary.Controls: return "Open.Core.Controls";
                case ScriptLibrary.Lists: return "Open.Core.Lists";
                default: throw new Exception(string.Format("{0} not supported.", library.ToString()));
            }
        }
        #endregion
    }
}
