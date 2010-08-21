using System;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for loading scripts.</summary>
    public class ScriptLoadHelper
    {
        #region Head
        private string rootScriptFolder = "/Open.Core/Scripts/";
        private bool useDebug;
        private JitScriptLoader jit;
        private bool isListsLoaded;
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

        /// <summary>Gets whether the Lists library has been loaded.</summary>
        public bool IsListsLoaded { get { return isListsLoaded; } }
        #endregion

        #region Methods
        /// <summary>Loads the Lists library.</summary>
        /// <param name="callback">Callback to invoke upon completion.</param>
        public void LoadLists(Action callback)
        {
            // Setup initial conditions.
            if (IsListsLoaded)
            {
                Helper.InvokeOrDefault(callback);
                return;
            }

            // Download scripts.
            ScriptLoader loader = new ScriptLoader();
            loader.LoadComplete += delegate
                                       {
                                           isListsLoaded = true;
                                           Helper.InvokeOrDefault(callback);
                                       };
            loader.AddUrl(Helper.ScriptLoader.Url(string.Empty, "Open.Core.Lists", true));
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
        #endregion
    }
}
