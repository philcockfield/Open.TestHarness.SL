using System;
using Open.Core.Helpers;

namespace Open.Core.Test.UnitTests
{
    public class ScriptLoadHelperTest
    {
        public void LoadControls()
        {
            Log.Info("Helper.ScriptLoader.IsLoaded: " + Helper.ScriptLoader.IsLoaded(ScriptLibrary.Controls));
            Helper.ScriptLoader.LoadLibrary(ScriptLibrary.Controls, delegate
                                    {
                                        Log.Info("Callback - " + Helper.ScriptLoader.IsLoaded(ScriptLibrary.Controls));
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
