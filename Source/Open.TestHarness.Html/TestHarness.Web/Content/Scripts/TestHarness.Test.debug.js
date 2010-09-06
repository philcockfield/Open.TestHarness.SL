//! TestHarness.Test.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Test.Samples');

////////////////////////////////////////////////////////////////////////////////
// Test.Samples.ControlsTest

Test.Samples.ControlsTest = function Test_Samples_ControlsTest() {
}
Test.Samples.ControlsTest.prototype = {
    
    add_Control__Default: function Test_Samples_ControlsTest$add_Control__Default() {
        var div1 = Open.Testing.TestHarness.addControl(Open.Testing.SizeMode.control);
        div1.append('Control 1');
        var div2 = Open.Testing.TestHarness.addControl(Open.Testing.SizeMode.control);
        div2.append('Control 2');
    },
    
    clear: function Test_Samples_ControlsTest$clear() {
        Open.Testing.TestHarness.clearControls();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Test.Samples.MyTest_Class__2

Test.Samples.MyTest_Class__2 = function Test_Samples_MyTest_Class__2() {
}
Test.Samples.MyTest_Class__2.prototype = {
    
    test1: function Test_Samples_MyTest_Class__2$test1() {
        Open.Core.Log.info('MyTestClass2 : Test 1 Invoked');
    }
}


////////////////////////////////////////////////////////////////////////////////
// Test.Samples.MyTestClass1

Test.Samples.MyTestClass1 = function Test_Samples_MyTestClass1() {
    /// <field name="publicField" type="String">
    /// </field>
    /// <field name="_privateField" type="String">
    /// </field>
}
Test.Samples.MyTestClass1.prototype = {
    
    classInitialize: function Test_Samples_MyTestClass1$classInitialize() {
        Open.Core.Log.info('Class Initialize: ' + Type.getInstanceType(this).get_name());
    },
    
    classCleanup: function Test_Samples_MyTestClass1$classCleanup() {
        Open.Core.Log.info('Class Cleanup: ' + Type.getInstanceType(this).get_name());
    },
    
    testInitialize: function Test_Samples_MyTestClass1$testInitialize() {
        Open.Core.Log.info('Test Initialize');
    },
    
    testCleanup: function Test_Samples_MyTestClass1$testCleanup() {
        Open.Core.Log.info('Test Cleanup');
    },
    
    publicField: 'Foo',
    _privateField: 'Foo',
    
    get_publicProperty: function Test_Samples_MyTestClass1$get_publicProperty() {
        /// <value type="String"></value>
        return this.publicField;
    },
    set_publicProperty: function Test_Samples_MyTestClass1$set_publicProperty(value) {
        /// <value type="String"></value>
        this.publicField = value;
        return value;
    },
    
    get__privateField1: function Test_Samples_MyTestClass1$get__privateField1() {
        /// <value type="String"></value>
        return this._privateField;
    },
    set__privateField1: function Test_Samples_MyTestClass1$set__privateField1(value) {
        /// <value type="String"></value>
        this._privateField = value;
        return value;
    },
    
    test1: function Test_Samples_MyTestClass1$test1() {
        Open.Core.Log.info('MyTestClass1 : Test 1 Invoked');
    },
    
    test_Two: function Test_Samples_MyTestClass1$test_Two() {
        Open.Core.Log.info('MyTestClass1 : Test 2 Invoked');
    },
    
    test__Three: function Test_Samples_MyTestClass1$test__Three() {
        Open.Core.Log.info('MyTestClass1 : Test 3 Invoked');
    },
    
    contains_Error: function Test_Samples_MyTestClass1$contains_Error() {
        throw new Error('My error.');
    },
    
    _privateMethod: function Test_Samples_MyTestClass1$_privateMethod() {
    }
}


Type.registerNamespace('Test');

////////////////////////////////////////////////////////////////////////////////
// Test.Application

Test.Application = function Test_Application() {
}
Test.Application.main = function Test_Application$main(args) {
    /// <param name="args" type="Object">
    /// </param>
    Open.Testing.TestHarness.registerClass(Test.Samples.MyTestClass1);
    Open.Testing.TestHarness.registerClass(Test.Samples.MyTestClass1);
    Open.Testing.TestHarness.registerClass(Test.Samples.MyTest_Class__2);
    Open.Testing.TestHarness.registerClass(Test.Samples.ControlsTest);
}


Test.Samples.ControlsTest.registerClass('Test.Samples.ControlsTest');
Test.Samples.MyTest_Class__2.registerClass('Test.Samples.MyTest_Class__2');
Test.Samples.MyTestClass1.registerClass('Test.Samples.MyTestClass1');
Test.Application.registerClass('Test.Application');

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('TestHarness.Script.Test', ['Open.Core.Script'], executeScript);
})();
