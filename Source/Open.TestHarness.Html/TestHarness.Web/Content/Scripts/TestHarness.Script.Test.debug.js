//! TestHarness.Script.Test.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('TestHarness.Test');

////////////////////////////////////////////////////////////////////////////////
// TestHarness.Test.MyTestClass2

TestHarness.Test.MyTestClass2 = function TestHarness_Test_MyTestClass2() {
}
TestHarness.Test.MyTestClass2.prototype = {
    
    test1: function TestHarness_Test_MyTestClass2$test1() {
        Open.Core.Log.info('MyTestClass2 : Test 1 Invoked');
    }
}


////////////////////////////////////////////////////////////////////////////////
// TestHarness.Test.MyTestClass1

TestHarness.Test.MyTestClass1 = function TestHarness_Test_MyTestClass1() {
}
TestHarness.Test.MyTestClass1.prototype = {
    
    test1: function TestHarness_Test_MyTestClass1$test1() {
        Open.Core.Log.info('MyTestClass1 : Test 1 Invoked');
    }
}


////////////////////////////////////////////////////////////////////////////////
// TestHarness.Test.Application

TestHarness.Test.Application = function TestHarness_Test_Application() {
}
TestHarness.Test.Application.main = function TestHarness_Test_Application$main(args) {
    /// <param name="args" type="Object">
    /// </param>
    var testPackage = TestHarness.Test.Application;
    Open.Core.TestHarness.registerTestClass(testPackage, TestHarness.Test.MyTestClass1);
    Open.Core.TestHarness.registerTestClass(testPackage, TestHarness.Test.MyTestClass1);
    Open.Core.TestHarness.registerTestClass(testPackage, TestHarness.Test.MyTestClass2);
}


TestHarness.Test.MyTestClass2.registerClass('TestHarness.Test.MyTestClass2');
TestHarness.Test.MyTestClass1.registerClass('TestHarness.Test.MyTestClass1');
TestHarness.Test.Application.registerClass('TestHarness.Test.Application');

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('TestHarness.Script.Test', ['Open.Core.Script'], executeScript);
})();
