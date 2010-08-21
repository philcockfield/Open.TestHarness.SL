using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Helpers
{
    /// <summary>Handles loading a collection of resources.</summary>
    public abstract class ResourceLoader
    {
        #region Head
        public event EventHandler LoadComplete;
        private void FireLoadComplete() { if (LoadComplete != null) LoadComplete(this, new EventArgs()); }

        private int loadedCallbackTotal;
        private readonly ArrayList urls = new ArrayList();
        private readonly ArrayList loaders = new ArrayList();
        #endregion

        #region Properties
        public bool IsLoaded
        {
            get
            {
                if (loadedCallbackTotal < urls.Count) return false;
                foreach (ResourceLoader loader in loaders)
                {
                    if (!loader.IsLoaded) return false;
                }
                return true;
            }
        }
        #endregion

        #region Methods
        public void AddUrl(string url) { urls.Add(url); }
        public void AddLoader(ResourceLoader loader) { loaders.Add(loader); }
        public void Start()
        {
            foreach (string url in urls)
            {
                LoadResource(url, delegate
                                              {
                                                  loadedCallbackTotal++;
                                                  OnDownloaded();
                                              });
            }

            foreach (ResourceLoader loader in loaders)
            {
                if (loader.IsLoaded) continue;
                loader.LoadComplete += delegate { OnDownloaded(); };
                loader.Start();
            }
        }

        protected abstract void LoadResource(string url, Action onDownloaded);
        #endregion

        #region Internal
        private void OnDownloaded()
        {
            if (IsLoaded) FireLoadComplete();
        }
        #endregion
    }
}