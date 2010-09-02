// TestHarness.Script.Test.js
(function(){function executeScript(){
Type.registerNamespace('TestHarness.Test');TestHarness.Test.MyTestClass2=function(){}
TestHarness.Test.MyTestClass2.prototype={test1:function(){Open.Core.Log.info('MyTestClass2 : Test 1 Invoked');}}
TestHarness.Test.MyTestClass1=function(){}
TestHarness.Test.MyTestClass1.prototype={test1:function(){Open.Core.Log.info('MyTestClass1 : Test 1 Invoked');}}
TestHarness.Test.Application=function(){}
TestHarness.Test.Application.main=function(args){Open.Core.Log.success('Tests loaded');var $0=TestHarness.Test.Application;Open.Core.TestHarness.registerTestClass($0,TestHarness.Test.MyTestClass1);Open.Core.TestHarness.registerTestClass($0,TestHarness.Test.MyTestClass1);Open.Core.TestHarness.registerTestClass($0,TestHarness.Test.MyTestClass2);}
TestHarness.Test.MyTestClass2.registerClass('TestHarness.Test.MyTestClass2');TestHarness.Test.MyTestClass1.registerClass('TestHarness.Test.MyTestClass1');TestHarness.Test.Application.registerClass('TestHarness.Test.Application');
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('TestHarness.Script.Test',['Open.Core.Script'],executeScript);})();