using Open.Core;
using Open.Core.Helpers;

namespace Test
{
    public class LoadHelperTest
    {

        public void LoadControls()
        {
            Log.Info("Helper.ScriptLoader.IsLoaded: " + Helper.ScriptLoader.IsLoaded(ScriptLibrary.Controls));

            Helper.ScriptLoader.LoadLibrary(ScriptLibrary.Controls, delegate
                                                                        {
                                                                            Log.Info("Callback - " + Helper.ScriptLoader.IsLoaded(ScriptLibrary.Controls));
                                                                        });

            Log.LineBreak();
        }

    }
}
