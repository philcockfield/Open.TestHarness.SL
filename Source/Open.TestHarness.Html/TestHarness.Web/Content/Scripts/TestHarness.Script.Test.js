// TestHarness.Script.Test.js
(function(){function executeScript(){
Type.registerNamespace('TestHarness.Test');TestHarness.Test.MyTestClass2=function(){}
TestHarness.Test.MyTestClass2.prototype={test1:function(){Open.Core.Log.info('MyTestClass2 : Test 1 Invoked');}}
TestHarness.Test.MyTestClass1=function(){}
TestHarness.Test.MyTestClass1.prototype={publicField:'Foo',$0:'Foo',get_publicProperty:function(){return this.publicField;},set_publicProperty:function(value){this.publicField=value;return value;},get_$1:function(){return this.$0;},set_$1:function($p0){this.$0=$p0;return $p0;},test1:function(){Open.Core.Log.info('MyTestClass1 : Test 1 Invoked');},test_Two:function(){Open.Core.Log.info('MyTestClass1 : Test 2 Invoked');},test__Three:function(){Open.Core.Log.info('MyTestClass1 : Test 3 Invoked');},$2:function(){}}
TestHarness.Test.Application=function(){}
TestHarness.Test.Application.main=function(args){var $0=TestHarness.Test.Application;Open.Core.TestHarness.registerTestClass($0,TestHarness.Test.MyTestClass1);Open.Core.TestHarness.registerTestClass($0,TestHarness.Test.MyTestClass1);Open.Core.TestHarness.registerTestClass($0,TestHarness.Test.MyTestClass2);}
TestHarness.Test.MyTestClass2.registerClass('TestHarness.Test.MyTestClass2');TestHarness.Test.MyTestClass1.registerClass('TestHarness.Test.MyTestClass1');TestHarness.Test.Application.registerClass('TestHarness.Test.Application');
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('TestHarness.Script.Test',['Open.Core.Script'],executeScript);})();