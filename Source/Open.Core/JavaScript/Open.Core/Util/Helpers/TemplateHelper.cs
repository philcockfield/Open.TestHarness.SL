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
        /// <summary>Retrieves a Template instance (only downloading it if required).</summary>
        /// <param name="url">The URL that contains the template.</param>
        /// <param name="selector">The CSS selector for the script block containing the template HTML.</param>
        /// <param name="callback">Callback to return the template in.</param>
        public void Get(string url, string selector, TemplateCallback callback)
        {
            // Setup initial conditions.
            if (callback == null) return;

            // Check cache for pre-existing template.
            string lowercaseUrl = url.ToLowerCase();
            TemplateItem cached = Helper.Collection.First(templates, delegate(object o)
                                                {
                                                    TemplateItem item = (TemplateItem) o;
                                                    return item.Template.Selector == selector && item.Url == lowercaseUrl;
                                                }) as TemplateItem;
            if (cached != null)
            {
                callback(cached.Template);
                return;
            }

            // NB: Download is only invoked once.
            Download(url, delegate
                                {
                                    // Add to cache.
                                    TemplateItem item = new TemplateItem();
                                    item.Url = url.ToLowerCase();
                                    item.Template = new Template(selector);
                                    templates.Add(item);

                                    // Finish up.
                                    callback(item.Template);
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
                Helper.InvokeOrDefault(onComplete);
                return;
            }

            // Download the template.
            downloadedUrls.Add(url.ToLowerCase());
            jQuery.Get(url, delegate(object data)
                                {
                                    // Append the templates to the BODY.
                                    jQuery.Select(Html.Body).Append(data.ToString());

                                    // Finish up.
                                    Helper.InvokeOrDefault(onComplete);
                                });
        }
        #endregion
    }

    internal class TemplateItem
    {
        public string Url;
        public Template Template;
    }
}
