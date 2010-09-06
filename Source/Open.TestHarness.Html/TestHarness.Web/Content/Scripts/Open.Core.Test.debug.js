//! Open.Core.Test.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core.Test.ViewTests');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.ViewTests.LoadHelperTest

Open.Core.Test.ViewTests.LoadHelperTest = function Open_Core_Test_ViewTests_LoadHelperTest() {
}
Open.Core.Test.ViewTests.LoadHelperTest.prototype = {
    
    loadControls: function Open_Core_Test_ViewTests_LoadHelperTest$loadControls() {
        Open.Core.Log.info('Helper.ScriptLoader.IsLoaded: ' + Open.Core.Helper.get_scriptLoader().isLoaded(Open.Core.Helpers.ScriptLibrary.controls));
        Open.Core.Helper.get_scriptLoader().loadLibrary(Open.Core.Helpers.ScriptLibrary.controls, ss.Delegate.create(this, function() {
            Open.Core.Log.info('Callback - ' + Open.Core.Helper.get_scriptLoader().isLoaded(Open.Core.Helpers.ScriptLibrary.controls));
        }));
        Open.Core.Log.lineBreak();
    }
}


Type.registerNamespace('Open.Core.Test');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.Application

Open.Core.Test.Application = function Open_Core_Test_Application() {
}
Open.Core.Test.Application.main = function Open_Core_Test_Application$main(args) {
    /// <param name="args" type="Object">
    /// </param>
    Open.Testing.TestHarness.registerClass(Open.Core.Test.ViewTests.LoadHelperTest);
}


Open.Core.Test.ViewTests.LoadHelperTest.registerClass('Open.Core.Test.ViewTests.LoadHelperTest');
Open.Core.Test.Application.registerClass('Open.Core.Test.Application');

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Test', ['Open.Core.Script'], executeScript);
})();
