using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Helpers
{
    public class ScriptLoader
    {
        #region Head
        public event EventHandler LoadComplete;
        private void FireLoadComplete() { if (LoadComplete != null) LoadComplete(this, new EventArgs()); }

        private int totalLoadedScripts;
        private readonly ArrayList urls = new ArrayList();
        private readonly ArrayList loaders = new ArrayList();
        #endregion

        #region Properties
        public bool IsLoaded
        {
            get
            {
                if (totalLoadedScripts < urls.Count) return false;
                foreach (ScriptLoader loader in loaders)
                {
                    if (!loader.IsLoaded) return false;
                }
                return true;
            }
        }
        #endregion

        #region Methods
        public void AddUrl(string url) { urls.Add(url); }
        public void AddLoader(ScriptLoader loader) { loaders.Add(loader); }
        public void Start()
        {
            foreach (string url in urls)
            {
                jQuery.GetScript(url, delegate(object data)
                                          {
                                              totalLoadedScripts++;
                                              OnDownloaded();
                                          });
            }

            foreach (ScriptLoader loader in loaders)
            {
                if (loader.IsLoaded) continue;
                loader.LoadComplete += delegate { OnDownloaded(); };
                loader.Start();
            }
        }
        #endregion

        #region Internal
        private void OnDownloaded()
        {
            if (IsLoaded) FireLoadComplete();
        }
        #endregion
    }
}