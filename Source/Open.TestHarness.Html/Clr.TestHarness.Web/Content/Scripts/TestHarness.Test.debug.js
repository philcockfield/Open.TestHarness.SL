//! TestHarness.Test.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Test.Samples');

////////////////////////////////////////////////////////////////////////////////
// Test.Samples.Canvas_Control_Insertion_Test

Test.Samples.Canvas_Control_Insertion_Test = function Test_Samples_Canvas_Control_Insertion_Test() {
}
Test.Samples.Canvas_Control_Insertion_Test._addControl = function Test_Samples_Canvas_Control_Insertion_Test$_addControl(displayMode) {
    /// <param name="displayMode" type="Open.Testing.ControlDisplayMode">
    /// </param>
    var control = new Test.Samples.MyView(displayMode);
    Open.Testing.TestHarness.set_displayMode(displayMode);
    Open.Testing.TestHarness.addControl(control);
    Open.Core.Log.info('Control added. DisplayMode: ' + Open.Testing.ControlDisplayMode.toString(displayMode));
}
Test.Samples.Canvas_Control_Insertion_Test.prototype = {
    
    add_Control__None: function Test_Samples_Canvas_Control_Insertion_Test$add_Control__None() {
        Test.Samples.Canvas_Control_Insertion_Test._addControl(Open.Testing.ControlDisplayMode.none);
    },
    
    add_Control__Center: function Test_Samples_Canvas_Control_Insertion_Test$add_Control__Center() {
        Test.Samples.Canvas_Control_Insertion_Test._addControl(Open.Testing.ControlDisplayMode.center);
    },
    
    add_Control__Fill: function Test_Samples_Canvas_Control_Insertion_Test$add_Control__Fill() {
        Test.Samples.Canvas_Control_Insertion_Test._addControl(Open.Testing.ControlDisplayMode.fill);
    },
    
    add_Control__FillWithMargin: function Test_Samples_Canvas_Control_Insertion_Test$add_Control__FillWithMargin() {
        Test.Samples.Canvas_Control_Insertion_Test._addControl(Open.Testing.ControlDisplayMode.fillWithMargin);
    },
    
    reset: function Test_Samples_Canvas_Control_Insertion_Test$reset() {
        Open.Testing.TestHarness.reset();
        Open.Core.Log.title('Reset');
        Open.Core.Log.info('SizeMode: ' + Open.Testing.ControlDisplayMode.toString(Open.Testing.TestHarness.get_displayMode()));
    }
}


////////////////////////////////////////////////////////////////////////////////
// Test.Samples.MyView

Test.Samples.MyView = function Test_Samples_MyView(controlDisplayMode) {
    /// <param name="controlDisplayMode" type="Open.Testing.ControlDisplayMode">
    /// </param>
    Test.Samples.MyView.initializeBase(this);
    this.get_container().append(String.format('Control [sizeMode:{0}]', Open.Testing.ControlDisplayMode.toString(controlDisplayMode)));
    this.get_container().css(Open.Core.Css.background, '#f0ebc5');
    this.get_container().css(Open.Core.Css.width, '300px');
    this.get_container().css(Open.Core.Css.height, '200px');
    this.get_container().css('-webkit-border-radius', '10px');
    this.get_container().css('-moz-border-radius', '10px');
    this.get_container().css('border-radius', '10px');
}


////////////////////////////////////////////////////////////////////////////////
// Test.Samples.ConstructorParams

Test.Samples.ConstructorParams = function Test_Samples_ConstructorParams(myParam) {
    /// <param name="myParam" type="String">
    /// </param>
}
Test.Samples.ConstructorParams.prototype = {
    
    test1: function Test_Samples_ConstructorParams$test1(param1, param2) {
        /// <param name="param1" type="String">
        /// </param>
        /// <param name="param2" type="Number" integer="true">
        /// </param>
        Open.Core.Log.title('Test1');
        Open.Core.Log.info('param1: ' + param1);
        Open.Core.Log.info('param2: ' + param2);
        Open.Core.Log.lineBreak();
    },
    
    test2: function Test_Samples_ConstructorParams$test2() {
    },
    
    test3: function Test_Samples_ConstructorParams$test3() {
    },
    
    test4: function Test_Samples_ConstructorParams$test4() {
    }
}


////////////////////////////////////////////////////////////////////////////////
// Test.Samples.CssTest

Test.Samples.CssTest = function Test_Samples_CssTest() {
    /// <field name="_view" type="Test.Samples.MyCssView">
    /// </field>
}
Test.Samples.CssTest._addControl = function Test_Samples_CssTest$_addControl(controlDisplayMode) {
    /// <param name="controlDisplayMode" type="Open.Testing.ControlDisplayMode">
    /// </param>
    /// <returns type="Test.Samples.MyCssView"></returns>
    var view = new Test.Samples.MyCssView(Open.Core.Html.createDiv(), controlDisplayMode);
    Open.Testing.TestHarness.set_displayMode(controlDisplayMode);
    Open.Testing.TestHarness.addControl(view);
    return view;
}
Test.Samples.CssTest.prototype = {
    _view: null,
    
    testInitialize: function Test_Samples_CssTest$testInitialize() {
        this._view = Test.Samples.CssTest._addControl(Open.Testing.ControlDisplayMode.center);
    },
    
    perspective: function Test_Samples_CssTest$perspective() {
        this._view.apply('transform', 'perspective(500px);');
    }
}


////////////////////////////////////////////////////////////////////////////////
// Test.Samples.MyCssView

Test.Samples.MyCssView = function Test_Samples_MyCssView(container, controlDisplayMode) {
    /// <param name="container" type="jQueryObject">
    /// </param>
    /// <param name="controlDisplayMode" type="Open.Testing.ControlDisplayMode">
    /// </param>
    Test.Samples.MyCssView.initializeBase(this, [ container ]);
    container.append(String.format('Control [sizeMode:{0}]', Open.Testing.ControlDisplayMode.toString(controlDisplayMode)));
    container.css(Open.Core.Css.background, '#f0ebc5');
    container.css(Open.Core.Css.width, '300px');
    container.css(Open.Core.Css.height, '200px');
    container.css('-webkit-border-radius', '10px');
    container.css('-moz-border-radius', '10px');
    container.css('border-radius', '10px');
}
Test.Samples.MyCssView.prototype = {
    
    apply: function Test_Samples_MyCssView$apply(prop, value) {
        /// <param name="prop" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        this.get_container().css(prop, value);
        Open.Core.Log.info(String.format('{0}: {1}', prop, value));
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
    
    removeStart: function Test_Samples_MyTestClass1$removeStart() {
        Open.Core.Log.info(Open.Core.Helper.get_url().prependDomain('/MyPath'));
        Open.Core.Log.info(Open.Core.Helper.get_url().prependDomain('MyPath'));
    },
    
    write_Url: function Test_Samples_MyTestClass1$write_Url() {
        Open.Core.Log.info('Hash: ' + window.location.hash);
        Open.Core.Log.info('Hostname: ' + window.location.hostname);
        Open.Core.Log.info('HostnameAndPort: ' + window.location.host);
        Open.Core.Log.info('Href: ' + window.location.href);
        Open.Core.Log.info('Pathname: ' + window.location.pathname);
        Open.Core.Log.info('Port: ' + window.location.port);
        Open.Core.Log.info('Protocol: ' + window.location.protocol);
        Open.Core.Log.info('Search: ' + window.location.search);
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
    Open.Testing.TestHarness.registerClass(Test.Samples.Canvas_Control_Insertion_Test);
    Open.Testing.TestHarness.registerClass(Test.Samples.ConstructorParams);
    Open.Testing.TestHarness.registerClass(Test.Samples.CssTest);
}


Test.Samples.Canvas_Control_Insertion_Test.registerClass('Test.Samples.Canvas_Control_Insertion_Test');
Test.Samples.MyView.registerClass('Test.Samples.MyView', Open.Core.ViewBase);
Test.Samples.ConstructorParams.registerClass('Test.Samples.ConstructorParams');
Test.Samples.CssTest.registerClass('Test.Samples.CssTest');
Test.Samples.MyCssView.registerClass('Test.Samples.MyCssView', Open.Core.ViewBase);
Test.Samples.MyTest_Class__2.registerClass('Test.Samples.MyTest_Class__2');
Test.Samples.MyTestClass1.registerClass('Test.Samples.MyTestClass1');
Test.Application.registerClass('Test.Application');

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('TestHarness.Script.Test', ['Open.Core.Script'], executeScript);
})();
