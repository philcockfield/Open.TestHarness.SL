﻿using System;

namespace Open.Core.Helpers
{
    /// <summary>Handles loading a collection of scripts.</summary>
    public class ScriptLoader : ResourceLoader
    {
        /// <summary>Gets whether all the scripts within the loader have already been downloaded.</summary>
        public bool AlreadyDownloaded
        {
            get
            {
                foreach (string url in this)
                {
                    if (url != null && !Helper.ScriptLoader.IsLoaded(url)) return false;
                }
                return true;
            }
        }

        protected override void LoadResource(string url, Action onDownloaded)
        {
            if (Helper.ScriptLoader.IsLoaded(url))
            {
                // No need to download, already exists in the page.
                Helper.Invoke(onDownloaded);
            }
            else
            {
                Helper.ScriptLoader.Load(url, delegate(object data)
                                                {
                                                    Helper.Invoke(onDownloaded);
                                                });
            }
        }
    }
}