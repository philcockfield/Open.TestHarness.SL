using System;
using Open.Core.Helpers;

namespace Open.Core.Test.ViewTests.Util
{
    public class Helper_ScriptLoadHelper
    {
        const string urlOpenCore = "/Content/Scripts/Open.Core.Test.debug.js";
        const string urlTestHarness = "http://localhost:8022/Content/Scripts/Open.TestHarness.debug.js";

        public void Script_IsDeclared()
        {
            IsDeclared(urlTestHarness);
            IsDeclared(urlOpenCore);
        }
        private static void IsDeclared(string url)
        {
            bool exists = Helper.ScriptLoader.IsDeclared(url);
            Log.Info(string.Format("IsDeclared ({0}): {1}", Html.ToHyperlink(url), exists));
        }

        public void Script_IsLoaded()
        {
            IsLoaded(urlTestHarness);
            IsLoaded(urlOpenCore);
        }
        private static void IsLoaded(string url)
        {
            bool exists = Helper.ScriptLoader.IsLoaded(url);
            Log.Info(string.Format("IsLoaded ({0}): {1}", Html.ToHyperlink(url), exists));
        }

        public void Load_TestUrl()
        {
            const string url = urlOpenCore;
            Log.Info("Loading: " + Html.ToHyperlink(url));
            Helper.ScriptLoader.Load(urlOpenCore, delegate { Log.Success("Loaded"); });
        }

        public void LoadControls()
        {
            Log.Info("Helper.ScriptLoader.IsLoaded: " + Helper.ScriptLoader.IsLibraryLoaded(ScriptLibrary.Controls));
            Helper.ScriptLoader.LoadLibrary(ScriptLibrary.Controls, delegate
                                        {
                                            Log.Info("Callback - " + Helper.ScriptLoader.IsLibraryLoaded(ScriptLibrary.Controls));
                                        });
        }

        public void Add_Delimited_Urls()
        {
            ScriptLoader loader = new ScriptLoader();
            loader.AddUrl("one.js; two.js", ";");
            foreach (string url in loader)
            {
                Log.Info("> URL: " + url);
            }
        }

    }
}
