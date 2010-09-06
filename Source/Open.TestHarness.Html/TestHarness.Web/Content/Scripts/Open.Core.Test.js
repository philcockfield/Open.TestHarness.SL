// Open.Core.Test.js
(function(){function executeScript(){
Type.registerNamespace('Open.Core.Test.ViewTests');Open.Core.Test.ViewTests.LoadHelperTest=function(){}
Open.Core.Test.ViewTests.LoadHelperTest.prototype={loadControls:function(){Open.Core.Log.info('Helper.ScriptLoader.IsLoaded: '+Open.Core.Helper.get_scriptLoader().isLoaded(0));Open.Core.Helper.get_scriptLoader().loadLibrary(0,ss.Delegate.create(this,function(){
Open.Core.Log.info('Callback - '+Open.Core.Helper.get_scriptLoader().isLoaded(0));}));Open.Core.Log.lineBreak();}}
Type.registerNamespace('Open.Core.Test');Open.Core.Test.Application=function(){}
Open.Core.Test.Application.main=function(args){Open.Testing.TestHarness.registerClass(Open.Core.Test.ViewTests.LoadHelperTest);}
Open.Core.Test.ViewTests.LoadHelperTest.registerClass('Open.Core.Test.ViewTests.LoadHelperTest');Open.Core.Test.Application.registerClass('Open.Core.Test.Application');
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('Open.Core.Test',['Open.Core.Script'],executeScript);})();