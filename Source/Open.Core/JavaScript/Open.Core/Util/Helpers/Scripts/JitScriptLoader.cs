using System;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for loading the JIT (Visualization) scripts.</summary>
    public class JitScriptLoader
    {
        #region Head
        private const string JitFolder = "Jit/";

        private bool isBaseLoaded;
        private bool isHypertreeLoaded;
        #endregion

        #region Properties
        /// <summary>Gets whether the base JIT libraries are loaded.</summary>
        public bool IsBaseLoaded { get { return isBaseLoaded; } }

        /// <summary>Gets whether the Hypertree libraries are loaded.</summary>
        public bool IsHypertreeLoaded { get { return isHypertreeLoaded; } }
        #endregion

        #region Methods - JIT (Visualization Libraries)
        /// <summary>Loads the JIT visualization libraries.</summary>
        /// <param name="callback">Callback to invoke upon completion.</param>
        public void LoadBase(Action callback)
        {
            // Setup initial conditions.
            if (IsBaseLoaded)
            {
                Helper.Invoke(callback);
                return;
            }

            // Download scripts.
            ScriptLoader loader = GetBaseLoader();
            loader.LoadComplete += delegate { Helper.Invoke(callback); };
            loader.Start();
        }
        private ScriptLoader GetBaseLoader()
        {
            ScriptLoader loader = new ScriptLoader();
            loader.LoadComplete += delegate { isBaseLoaded = true; };
            loader.AddUrl(Helper.ScriptLoader.Url(string.Empty, "Open.Library.Jit", true));
            loader.AddUrl(Helper.ScriptLoader.Url(JitFolder, "excanvas", false));
            loader.AddUrl(Helper.ScriptLoader.Url(JitFolder, "Jit.Initialize", false));
            return loader;
        }

        /// <summary>Loads the Hypertree (and associated) libraries.</summary>
        /// <param name="callback">Callback to invoke upon completion.</param>
        public void LoadHypertree(Action callback)
        {
            // Setup initial conditions.
            if (IsHypertreeLoaded)
            {
                Helper.Invoke(callback);
                return;
            }

            // Download scripts.
            ScriptLoader loader = new ScriptLoader();
            loader.LoadComplete += delegate
                                                    {
                                                        isHypertreeLoaded = true;
                                                        Helper.Invoke(callback);
                                                    };
            loader.AddLoader(GetBaseLoader());
            loader.AddUrl(Helper.ScriptLoader.Url(JitFolder, "HyperTree", true));
            loader.AddUrl(Helper.ScriptLoader.Url(JitFolder, "HyperTree.Initialize", false));
            loader.Start();
        }
        #endregion
    }
}
