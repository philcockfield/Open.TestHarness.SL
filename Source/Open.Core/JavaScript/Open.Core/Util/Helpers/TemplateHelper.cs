using System;
using System.Collections;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core.Helpers
{
    /// <summary>A callback the returns a Template.</summary>
    /// <param name="template">The Template return value.</param>
    public delegate void TemplateCallback(Template template);

    /// <summary>Utility methods for working with jQuery Templates.</summary>
    public class TemplateHelper
    {
        #region Head
        private readonly ArrayList downloadCallbacks = new ArrayList();
        private readonly ArrayList downloadedUrls = new ArrayList();
        private readonly ArrayList templates = new ArrayList();
        #endregion

        #region Methods
        /// <summary>Retrieves a Template singleton instance (downloading it if it has not already been pulled).</summary>
        /// <param name="url">The URL that contains the template.</param>
        /// <param name="selector">The CSS selector for the script block containing the template HTML.</param>
        /// <param name="callback">Callback to return the template in.</param>
        public void GetAsync(string url, string selector, TemplateCallback callback)
        {
            // Setup initial conditions.
            if (callback == null) return;

            // Check cache for pre-existing template.
            Template template = Get(selector);
            if (template != null)
            {
                callback(template);
                return;
            }

            // NB: The actual download is only invoked once (logic handled within the 'Download' method).
            Download(url, delegate
                              {
                                  template = GetInternal(selector, false); // Don't recheck cahce.  Cache has already checked prior to downloading.
                                  callback(template);
                              });
        }

        /// <summary>Retrieves the specified Template singleton instance (downloading it if it has not already been pulled).</summary>
        /// <param name="selector">The CSS selector for the script block containing the template HTML.</param>
        /// <returns>The specified template, or Null if it doesn't exist.</returns>
        public Template Get(string selector) { return GetInternal(selector, true); }
        public Template GetInternal(string selector, bool checkCache)
        {
            // Setup initial conditions.
            Template template = null;

            // Check cache for pre-existing template.
            if (!Script.IsUndefined(checkCache) && checkCache)
            {
                template = Helper.Collection.First(templates, delegate(object o)
                                                        {
                                                            Template item = (Template)o;
                                                            return item.Selector == selector;
                                                        }) as Template;
                if (template != null) return template;
            }

            // Attempt to create the template from the given selector (and add it to the cache).
            try
            {
                template = new Template(selector);
                templates.Add(template);
                return template;
            }
            catch (Exception)
            {
                return null; // Selector not found.  No template to return.
            }
        }

        /// <summary>Gets whether a template at the given selector is available to use (it exists within the page).</summary>
        public bool IsAvailable(string selector) { return Get(selector) != null; }

        /// <summary>Determines whether the specified URL has been downloaded.</summary>
        /// <param name="url">The URL of the template(s) to check.</param>
        public bool IsDownloaded(string url) { return downloadedUrls.Contains(url.ToLowerCase()); }

        /// <summary>Downloads the template(s) at the specified URL and appends them to the body.</summary>
        /// <param name="url">The URL of the template(s) to download.</param>
        [AlternateSignature]
        public extern void Download(string url);

        /// <summary>Downloads the template(s) at the specified URL and appends them to the body.</summary>
        /// <param name="url">The URL of the template(s) to download.</param>
        /// <param name="onComplete">Callback to invoke upon completion</param>
        public void Download(string url, Action onComplete)
        {
            // Setup initial conditions.
            if (IsDownloaded(url))
            {
                Helper.Invoke(onComplete);
                return;
            }
            bool isDownloading = IsDownloading(url);

            // Store reference to callback.
            // NB: This is done in case multiple calls are made.  This allows a collection
            //       of callbacks to be made when the download completes.
            DownloadCallback callback = new DownloadCallback();
            callback.Url = url.ToLowerCase();
            callback.OnComplete = onComplete;
            downloadCallbacks.Add(callback);
            if (isDownloading) return;

            // Download the template.
            jQuery.Get(url, delegate(object data)
                                {
                                    // Store reference.
                                    downloadedUrls.Add(url.ToLowerCase());

                                    // Append the templates to the BODY.
                                    jQuery.Select(Html.Body).Append(data.ToString());

                                    // Invoke the collection of callbacks to invoke.
                                    foreach (DownloadCallback item in GetDownloadCallbacks(url))
                                    {
                                        Helper.Invoke(item.OnComplete);
                                        downloadCallbacks.Remove(item);
                                    }
                                });
        }
        #endregion

        #region Internal
        private bool IsDownloading(string url) { return GetDownloadCallbacks(url).Count > 0; }
        private ArrayList GetDownloadCallbacks(string url)
        {
            url = url.ToLowerCase();
            return Helper.Collection.Filter(downloadCallbacks, delegate(object o)
                                                             {
                                                                 DownloadCallback item = (DownloadCallback) o;
                                                                 return url == item.Url;
                                                             });
        }
        #endregion
    }

    internal class DownloadCallback
    {
        public string Url;
        public Action OnComplete;
    }
}
