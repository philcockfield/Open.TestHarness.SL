// TestHarness.Test.js
(function(){function executeScript(){
Type.registerNamespace('Test.Samples');Test.Samples.ControlsTest=function(){}
Test.Samples.ControlsTest.prototype={add_Control__Default:function(){var $0=Open.Testing.TestHarness.addControl(0);$0.append('Control 1');var $1=Open.Testing.TestHarness.addControl(0);$1.append('Control 2');},clear:function(){Open.Testing.TestHarness.clearControls();}}
Test.Samples.MyTest_Class__2=function(){}
Test.Samples.MyTest_Class__2.prototype={test1:function(){Open.Core.Log.info('MyTestClass2 : Test 1 Invoked');}}
Test.Samples.MyTestClass1=function(){}
Test.Samples.MyTestClass1.prototype={classInitialize:function(){Open.Core.Log.info('Class Initialize: '+Type.getInstanceType(this).get_name());},classCleanup:function(){Open.Core.Log.info('Class Cleanup: '+Type.getInstanceType(this).get_name());},testInitialize:function(){Open.Core.Log.info('Test Initialize');},testCleanup:function(){Open.Core.Log.info('Test Cleanup');},publicField:'Foo',$0:'Foo',get_publicProperty:function(){return this.publicField;},set_publicProperty:function(value){this.publicField=value;return value;},get_$1:function(){return this.$0;},set_$1:function($p0){this.$0=$p0;return $p0;},test1:function(){Open.Core.Log.info('MyTestClass1 : Test 1 Invoked');},test_Two:function(){Open.Core.Log.info('MyTestClass1 : Test 2 Invoked');},test__Three:function(){Open.Core.Log.info('MyTestClass1 : Test 3 Invoked');},contains_Error:function(){throw new Error('My error.');},$2:function(){}}
Type.registerNamespace('Test');Test.Application=function(){}
Test.Application.main=function(args){Open.Testing.TestHarness.registerClass(Test.Samples.MyTestClass1);Open.Testing.TestHarness.registerClass(Test.Samples.MyTestClass1);Open.Testing.TestHarness.registerClass(Test.Samples.MyTest_Class__2);Open.Testing.TestHarness.registerClass(Test.Samples.ControlsTest);}
Test.Samples.ControlsTest.registerClass('Test.Samples.ControlsTest');Test.Samples.MyTest_Class__2.registerClass('Test.Samples.MyTest_Class__2');Test.Samples.MyTestClass1.registerClass('Test.Samples.MyTestClass1');Test.Application.registerClass('Test.Application');
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('TestHarness.Script.Test',['Open.Core.Script'],executeScript);})();