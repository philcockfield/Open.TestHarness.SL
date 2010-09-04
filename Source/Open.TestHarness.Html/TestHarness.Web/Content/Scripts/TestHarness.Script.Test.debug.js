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
    /// <field name="publicField" type="String">
    /// </field>
    /// <field name="_privateField" type="String">
    /// </field>
}
TestHarness.Test.MyTestClass1.prototype = {
    publicField: 'Foo',
    _privateField: 'Foo',
    
    get_publicProperty: function TestHarness_Test_MyTestClass1$get_publicProperty() {
        /// <value type="String"></value>
        return this.publicField;
    },
    set_publicProperty: function TestHarness_Test_MyTestClass1$set_publicProperty(value) {
        /// <value type="String"></value>
        this.publicField = value;
        return value;
    },
    
    get__privateField1: function TestHarness_Test_MyTestClass1$get__privateField1() {
        /// <value type="String"></value>
        return this._privateField;
    },
    set__privateField1: function TestHarness_Test_MyTestClass1$set__privateField1(value) {
        /// <value type="String"></value>
        this._privateField = value;
        return value;
    },
    
    test1: function TestHarness_Test_MyTestClass1$test1() {
        Open.Core.Log.info('MyTestClass1 : Test 1 Invoked');
    },
    
    test_Two: function TestHarness_Test_MyTestClass1$test_Two() {
        Open.Core.Log.info('MyTestClass1 : Test 2 Invoked');
    },
    
    test__Three: function TestHarness_Test_MyTestClass1$test__Three() {
        Open.Core.Log.info('MyTestClass1 : Test 3 Invoked');
    },
    
    _privateMethod: function TestHarness_Test_MyTestClass1$_privateMethod() {
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
