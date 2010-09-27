using System;
using System.Collections;
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
            Template template = Helper.Collection.First(templates, delegate(object o)
                                                                       {
                                                                           Template item = (Template) o;
                                                                           return item.Selector == selector;
                                                                       }) as Template;
            if (template != null)
            {
                callback(template);
                return;
            }

            // NB: Actual download is only invoked once (logic handled within the 'Download' method).
            Download(url, delegate
                                {
                                    // Add to cache.
                                    template = new Template(selector);
                                    templates.Add(template);

                                    // Finish up.
                                    callback(template);
                                });
        }

        /// <summary>Determines whether the specified URL has been downloaded.</summary>
        /// <param name="url">The URL of the template(s) to check.</param>
        public bool IsDownloaded(string url) { return downloadedUrls.Contains(url.ToLowerCase()); }

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

            // Download the template.
            downloadedUrls.Add(url.ToLowerCase());
            jQuery.Get(url, delegate(object data)
                                {
                                    // Append the templates to the BODY.
                                    jQuery.Select(Html.Body).Append(data.ToString());

                                    // Finish up.
                                    Helper.Invoke(onComplete);
                                });
        }
        #endregion
    }
}
