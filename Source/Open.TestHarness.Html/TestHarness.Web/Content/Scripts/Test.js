// Test.js
(function(){function executeScript(){
Type.registerNamespace('Test');Test.MyTest_Class__2=function(){}
Test.MyTest_Class__2.prototype={test1:function(){Open.Core.Log.info('MyTestClass2 : Test 1 Invoked');}}
Test.MyTestClass1=function(){}
Test.MyTestClass1.prototype={classInitialize:function(){Open.Core.Log.info('Class Initialize: '+Type.getInstanceType(this).get_name());},classCleanup:function(){Open.Core.Log.info('Class Cleanup: '+Type.getInstanceType(this).get_name());},testInitialize:function(){Open.Core.Log.info('Test Initialize');},testCleanup:function(){Open.Core.Log.info('Test Cleanup');},publicField:'Foo',$0:'Foo',get_publicProperty:function(){return this.publicField;},set_publicProperty:function(value){this.publicField=value;return value;},get_$1:function(){return this.$0;},set_$1:function($p0){this.$0=$p0;return $p0;},test1:function(){Open.Core.Log.info('MyTestClass1 : Test 1 Invoked');},test_Two:function(){Open.Core.Log.info('MyTestClass1 : Test 2 Invoked');},test__Three:function(){Open.Core.Log.info('MyTestClass1 : Test 3 Invoked');},contains_Error:function(){throw new Error('My error.');},$2:function(){}}
Test.Application=function(){}
Test.Application.main=function(args){Open.Testing.registerClass(Test.MyTestClass1);Open.Testing.registerClass(Test.MyTestClass1);Open.Testing.registerClass(Test.MyTest_Class__2);}
Test.MyTest_Class__2.registerClass('Test.MyTest_Class__2');Test.MyTestClass1.registerClass('Test.MyTestClass1');Test.Application.registerClass('Test.Application');
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('TestHarness.Script.Test',['Open.Core.Script'],executeScript);})();