using System;
using jQueryApi;

namespace Open.Core
{
    /// <summary>Handles downloading a part.</summary>
    internal class PartDownloader
    {
        #region Head
        private readonly PartDefinition definition;
        private readonly jQueryObject container;
        private readonly PartCallback onComplete;
        private readonly bool initializeOnComplete;
//        private DelayedAction timeout;

        public PartDownloader(PartDefinition definition, jQueryObject container, PartCallback onComplete, bool initializeOnComplete)
        {
            this.definition = definition;
            this.container = container;
            this.onComplete = onComplete;
            this.initializeOnComplete = initializeOnComplete;
        }
        #endregion

        #region Internal

        private DelayedAction CreateTimout()
        {
            // TODO : PartDownloader
            //DelayedAction timer = new DelayedAction(definition.DownloadTimeout, delegate
            //                {
            //                    string msg = downloaded
            //                                ? string.Format("Failed to initialize the Part at '{0}'.  The Part did not call back from its 'OnInitialize' method.", EntryPoint)
            //                                : string.Format("Failed to download the Part at '{0}'.  Timed out.", EntryPoint);
            //                    SetDownloadError(msg);
            //                    if (onComplete != null) onComplete(null);
            //                });
//            return timer;

            return null;
        }

        private void SetDownloadError(string msg)
        {
            Log.Error(msg);
            definition.LoadError = new Exception(msg);
        }
        #endregion

    }
}
