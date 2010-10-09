using System;

namespace Open.Core.Helpers
{
    /// <summary>Handles loading a collection of scripts.</summary>
    public class ScriptLoader : ResourceLoader
    {
        protected override void LoadResource(string url, Action onDownloaded)
        {
            Helper.ScriptLoader.Load(url, delegate(object data)
                                              {
                                                  Helper.Invoke(onDownloaded);
                                              });
        }
    }
}