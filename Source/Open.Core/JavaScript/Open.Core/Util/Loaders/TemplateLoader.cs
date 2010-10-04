using System;

namespace Open.Core.Helpers
{
    /// <summary>Handles loading a collection of templates.</summary>
    public class TemplateLoader : ResourceLoader
    {
        protected override void LoadResource(string url, Action onDownloaded)
        {
            Helper.Template.Download(url, onDownloaded);
        }
    }
}