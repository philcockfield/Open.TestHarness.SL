//! Open.Core.Test.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core.Test.ViewTests');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.ViewTests.DiContainerTest

Open.Core.Test.ViewTests.DiContainerTest = function Open_Core_Test_ViewTests_DiContainerTest() {
    /// <field name="_container" type="Open.Core.DiContainer">
    /// </field>
}
Open.Core.Test.ViewTests.DiContainerTest.prototype = {
    _container: null,
    
    testInitialize: function Open_Core_Test_ViewTests_DiContainerTest$testInitialize() {
        this._container = new Open.Core.DiContainer();
    },
    
    testCleanup: function Open_Core_Test_ViewTests_DiContainerTest$testCleanup() {
        this._container.dispose();
    },
    
    shouldNotHaveSingleton: function Open_Core_Test_ViewTests_DiContainerTest$shouldNotHaveSingleton() {
        Open.Core.Should.beNull(this._container.getSingleton(Open.Core.Test.ViewTests.IMyInterface));
    },
    
    shouldHaveSingletonAfterRegistering: function Open_Core_Test_ViewTests_DiContainerTest$shouldHaveSingletonAfterRegistering() {
        var instance = new Open.Core.Test.ViewTests.MyClass();
        this._container.registerSingleton(Open.Core.Test.ViewTests.IMyInterface, instance);
        var retreived = this._container.getSingleton(Open.Core.Test.ViewTests.IMyInterface);
        Open.Core.Should.equal(retreived, instance);
    },
    
    shouldReplaceSingleton: function Open_Core_Test_ViewTests_DiContainerTest$shouldReplaceSingleton() {
        var instance1 = new Open.Core.Test.ViewTests.MyClass();
        var instance2 = new Open.Core.Test.ViewTests.MyClass();
        this._container.registerSingleton(Open.Core.Test.ViewTests.IMyInterface, instance1);
        this._container.registerSingleton(Open.Core.Test.ViewTests.IMyInterface, instance2);
        var retreived = this._container.getSingleton(Open.Core.Test.ViewTests.IMyInterface);
        Open.Core.Should.notEqual(retreived, instance1);
        Open.Core.Should.equal(retreived, instance2);
    },
    
    shouldReturnNullWhenNoKeySpecified: function Open_Core_Test_ViewTests_DiContainerTest$shouldReturnNullWhenNoKeySpecified() {
        Open.Core.Should.beNull(this._container.getSingleton(null));
    },
    
    shouldNotContainSingleton: function Open_Core_Test_ViewTests_DiContainerTest$shouldNotContainSingleton() {
        Open.Core.Should.beFalse(this._container.containsSingleton(Open.Core.Test.ViewTests.IMyInterface));
    },
    
    shouldContainSingleton: function Open_Core_Test_ViewTests_DiContainerTest$shouldContainSingleton() {
        this._container.registerSingleton(Open.Core.Test.ViewTests.IMyInterface, new Open.Core.Test.ViewTests.MyClass());
        Open.Core.Should.beTrue(this._container.containsSingleton(Open.Core.Test.ViewTests.IMyInterface));
    },
    
    shouldUnregisterContainSingleton: function Open_Core_Test_ViewTests_DiContainerTest$shouldUnregisterContainSingleton() {
        var instance1 = new Open.Core.Test.ViewTests.MyClass();
        this._container.registerSingleton(Open.Core.Test.ViewTests.IMyInterface, instance1);
        this._container.registerSingleton(Open.Core.Test.ViewTests.IMyInterface, instance1);
        Open.Core.Should.equal(this._container.getSingleton(Open.Core.Test.ViewTests.IMyInterface), instance1);
        this._container.unregisterSingleton(Open.Core.Test.ViewTests.IMyInterface);
        Open.Core.Should.equal(this._container.getSingleton(Open.Core.Test.ViewTests.IMyInterface), null);
    },
    
    shouldCreateSingletonInstance: function Open_Core_Test_ViewTests_DiContainerTest$shouldCreateSingletonInstance() {
        Open.Core.Should.beNull(this._container.getSingleton(Open.Core.Test.ViewTests.IMyInterface));
        var instance1 = new Open.Core.Test.ViewTests.MyClass();
        var retrieved = this._container.getOrCreateSingleton(Open.Core.Test.ViewTests.IMyInterface, ss.Delegate.create(this, function() {
            return instance1;
        }));
        Open.Core.Should.equal(retrieved, instance1);
        Open.Core.Should.equal(this._container.getSingleton(Open.Core.Test.ViewTests.IMyInterface), instance1);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.ViewTests.MyClass

Open.Core.Test.ViewTests.MyClass = function Open_Core_Test_ViewTests_MyClass() {
    Open.Core.Test.ViewTests.MyClass.initializeBase(this);
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.ViewTests.IMyInterface

Open.Core.Test.ViewTests.IMyInterface = function Open_Core_Test_ViewTests_IMyInterface() {
}


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
    Open.Testing.TestHarness.registerClass(Open.Core.Test.ViewTests.DiContainerTest);
}


Open.Core.Test.ViewTests.DiContainerTest.registerClass('Open.Core.Test.ViewTests.DiContainerTest');
Open.Core.Test.ViewTests.IMyInterface.registerClass('Open.Core.Test.ViewTests.IMyInterface');
Open.Core.Test.ViewTests.MyClass.registerClass('Open.Core.Test.ViewTests.MyClass', Open.Core.Test.ViewTests.IMyInterface);
Open.Core.Test.ViewTests.LoadHelperTest.registerClass('Open.Core.Test.ViewTests.LoadHelperTest');
Open.Core.Test.Application.registerClass('Open.Core.Test.Application');

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Test', ['Open.Core.Script'], executeScript);
})();
