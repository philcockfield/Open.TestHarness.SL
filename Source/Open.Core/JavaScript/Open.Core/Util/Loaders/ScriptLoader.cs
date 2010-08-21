using System;
using jQueryApi;

namespace Open.Core.Helpers
{
    /// <summary>Handles loading a collection of scripts.</summary>
    public class ScriptLoader : ResourceLoader
    {
        protected override void LoadResource(string url, Action onDownloaded)
        {
            jQuery.GetScript(url, delegate(object data) { onDownloaded(); });
        }
    }
}