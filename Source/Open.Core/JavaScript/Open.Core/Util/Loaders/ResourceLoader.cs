using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Open.Core.Helpers
{
    /// <summary>Handles loading a collection of resources.</summary>
    public abstract class ResourceLoader : IEnumerable
    {
        #region Head
        public event EventHandler LoadComplete;
        private void FireLoadComplete() { if (LoadComplete != null) LoadComplete(this, new EventArgs()); }

        private int loadedCallbackTotal;
        private readonly ArrayList listUrls = new ArrayList();
        private readonly ArrayList listLoaders = new ArrayList();
        #endregion

        #region Properties
        public bool IsLoaded
        {
            get
            {
                if (loadedCallbackTotal < listUrls.Count) return false;
                foreach (ResourceLoader loader in listLoaders)
                {
                    if (!loader.IsLoaded) return false;
                }
                return true;
            }
        }
        #endregion

        #region Methods
        /// <summary>Retrieves the enumerator for the collection of URL's.</summary>
        public IEnumerator GetEnumerator() { return listUrls.GetEnumerator(); }

        /// <summary>Adds a URL to download.</summary>
        /// <param name="url">The URL.</param>
        [AlternateSignature]
        public extern void AddUrl(string url);

        /// <summary>Splits a set of URL's from a string and adds each one to the list to download.</summary>
        /// <param name="urls">The string of delimited URLs.</param>
        /// <param name="delimiter">The delimiter.</param>
        public void AddUrl(string urls, string delimiter)
        {
            if (string.IsNullOrEmpty(urls)) return;
            if (Script.IsNullOrUndefined(delimiter))
            {
                // Add single URL.
                listUrls.Add(urls);
            }
            else
            {
                // Split and add range of URL's.
                foreach (string item in urls.Split(delimiter))
                {
                    string url = item.Trim();
                    if (!string.IsNullOrEmpty(url)) listUrls.Add(url);
                }
            }
        }

        /// <summary>Add a new loader.</summary>
        /// <param name="loader">The loader to add.</param>
        public void AddLoader(ResourceLoader loader) { listLoaders.Add(loader); }

        /// <summary>Start the download process.</summary>
        public void Start()
        {
            foreach (string url in listUrls)
            {
                LoadResource(url, delegate
                                      {
                                          loadedCallbackTotal++;
                                          OnDownloaded();
                                      });
            }

            foreach (ResourceLoader loader in listLoaders)
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