//! Open.TestHarness.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Testing');

////////////////////////////////////////////////////////////////////////////////
// Open.Testing.TestHarnessViewBase

Open.Testing.TestHarnessViewBase = function Open_Testing_TestHarnessViewBase(container) {
    /// <summary>
    /// The base class for views within the TestHarness.
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// </param>
    /// <field name="_common$2" type="Open.Testing.Common">
    /// </field>
    Open.Testing.TestHarnessViewBase.initializeBase(this, [ container ]);
}
Open.Testing.TestHarnessViewBase.prototype = {
    _common$2: null,
    
    get_common: function Open_Testing_TestHarnessViewBase$get_common() {
        /// <summary>
        /// Gets the common global properties (via the DI Container).
        /// </summary>
        /// <value type="Open.Testing.Common"></value>
        return this._common$2 || (this._common$2 = Open.Testing.Common.getFromContainer());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.TestHarnessControllerBase

Open.Testing.TestHarnessControllerBase = function Open_Testing_TestHarnessControllerBase() {
    /// <summary>
    /// The base class for controllers within the TestHarness.
    /// </summary>
    /// <field name="_common$2" type="Open.Testing.Common">
    /// </field>
    Open.Testing.TestHarnessControllerBase.initializeBase(this);
}
Open.Testing.TestHarnessControllerBase.prototype = {
    _common$2: null,
    
    get_common: function Open_Testing_TestHarnessControllerBase$get_common() {
        /// <summary>
        /// Gets the common global properties (via the DI Container).
        /// </summary>
        /// <value type="Open.Testing.Common"></value>
        return this._common$2 || (this._common$2 = Open.Testing.Common.getFromContainer());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.CssSelectors

Open.Testing.CssSelectors = function Open_Testing_CssSelectors() {
    /// <summary>
    /// Constants for common CSS selectors.
    /// </summary>
    /// <field name="classes" type="Open.Testing.Classes" static="true">
    /// </field>
    /// <field name="root" type="String" static="true">
    /// </field>
    /// <field name="sidebar" type="String" static="true">
    /// </field>
    /// <field name="sidebarContent" type="String" static="true">
    /// </field>
    /// <field name="sidebarRootList" type="String" static="true">
    /// </field>
    /// <field name="sidebarToolbar" type="String" static="true">
    /// </field>
    /// <field name="backMask" type="String" static="true">
    /// </field>
    /// <field name="methodList" type="String" static="true">
    /// </field>
    /// <field name="methodListTitlebar" type="String" static="true">
    /// </field>
    /// <field name="methodListContent" type="String" static="true">
    /// </field>
    /// <field name="methodListRunButton" type="String" static="true">
    /// </field>
    /// <field name="main" type="String" static="true">
    /// </field>
    /// <field name="mainContent" type="String" static="true">
    /// </field>
    /// <field name="controlHost" type="String" static="true">
    /// </field>
    /// <field name="logContainer" type="String" static="true">
    /// </field>
    /// <field name="logTitlebar" type="String" static="true">
    /// </field>
    /// <field name="log" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Classes

Open.Testing.Classes = function Open_Testing_Classes() {
    /// <field name="logIndentedList" type="String">
    /// </field>
}
Open.Testing.Classes.prototype = {
    logIndentedList: 'indentedList'
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Elements

Open.Testing.Elements = function Open_Testing_Elements() {
    /// <summary>
    /// Constants for element IDs.
    /// </summary>
    /// <field name="root" type="String" static="true">
    /// </field>
    /// <field name="outputLog" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.TestHarnessEvents

Open.Testing.TestHarnessEvents = function Open_Testing_TestHarnessEvents() {
    /// <field name="__testClassRegistered" type="Open.Testing.Internal.TestClassHandler">
    /// </field>
    /// <field name="__controlAdded" type="Open.Testing.Internal.TestControlHandler">
    /// </field>
    /// <field name="__clearControls" type="EventHandler">
    /// </field>
    /// <field name="__updateLayout" type="EventHandler">
    /// </field>
    /// <field name="__methodClicked" type="Open.Testing.MethodEventHandler">
    /// </field>
    /// <field name="__selectedClassChanged" type="Open.Testing.ClassEventHandler">
    /// </field>
    /// <field name="__controlHostSizeChanged" type="EventHandler">
    /// </field>
}
Open.Testing.TestHarnessEvents.prototype = {
    
    add_testClassRegistered: function Open_Testing_TestHarnessEvents$add_testClassRegistered(value) {
        /// <param name="value" type="Function" />
        this.__testClassRegistered = ss.Delegate.combine(this.__testClassRegistered, value);
    },
    remove_testClassRegistered: function Open_Testing_TestHarnessEvents$remove_testClassRegistered(value) {
        /// <param name="value" type="Function" />
        this.__testClassRegistered = ss.Delegate.remove(this.__testClassRegistered, value);
    },
    
    __testClassRegistered: null,
    
    fireTestClassRegistered: function Open_Testing_TestHarnessEvents$fireTestClassRegistered(e) {
        /// <param name="e" type="Open.Testing.Internal.TestClassEventArgs">
        /// </param>
        if (this.__testClassRegistered != null) {
            this.__testClassRegistered.invoke(this, e);
        }
    },
    
    add_controlAdded: function Open_Testing_TestHarnessEvents$add_controlAdded(value) {
        /// <param name="value" type="Function" />
        this.__controlAdded = ss.Delegate.combine(this.__controlAdded, value);
    },
    remove_controlAdded: function Open_Testing_TestHarnessEvents$remove_controlAdded(value) {
        /// <param name="value" type="Function" />
        this.__controlAdded = ss.Delegate.remove(this.__controlAdded, value);
    },
    
    __controlAdded: null,
    
    fireControlAdded: function Open_Testing_TestHarnessEvents$fireControlAdded(e) {
        /// <param name="e" type="Open.Testing.Internal.TestControlEventArgs">
        /// </param>
        if (this.__controlAdded != null) {
            this.__controlAdded.invoke(this, e);
        }
    },
    
    add_clearControls: function Open_Testing_TestHarnessEvents$add_clearControls(value) {
        /// <param name="value" type="Function" />
        this.__clearControls = ss.Delegate.combine(this.__clearControls, value);
    },
    remove_clearControls: function Open_Testing_TestHarnessEvents$remove_clearControls(value) {
        /// <param name="value" type="Function" />
        this.__clearControls = ss.Delegate.remove(this.__clearControls, value);
    },
    
    __clearControls: null,
    
    fireClearControls: function Open_Testing_TestHarnessEvents$fireClearControls() {
        if (this.__clearControls != null) {
            this.__clearControls.invoke(this, new ss.EventArgs());
        }
    },
    
    add_updateLayout: function Open_Testing_TestHarnessEvents$add_updateLayout(value) {
        /// <param name="value" type="Function" />
        this.__updateLayout = ss.Delegate.combine(this.__updateLayout, value);
    },
    remove_updateLayout: function Open_Testing_TestHarnessEvents$remove_updateLayout(value) {
        /// <param name="value" type="Function" />
        this.__updateLayout = ss.Delegate.remove(this.__updateLayout, value);
    },
    
    __updateLayout: null,
    
    fireUpdateLayout: function Open_Testing_TestHarnessEvents$fireUpdateLayout() {
        if (this.__updateLayout != null) {
            this.__updateLayout.invoke(this, new ss.EventArgs());
        }
    },
    
    add_methodClicked: function Open_Testing_TestHarnessEvents$add_methodClicked(value) {
        /// <summary>
        /// Fires when each time a method in the list is clicked.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__methodClicked = ss.Delegate.combine(this.__methodClicked, value);
    },
    remove_methodClicked: function Open_Testing_TestHarnessEvents$remove_methodClicked(value) {
        /// <summary>
        /// Fires when each time a method in the list is clicked.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__methodClicked = ss.Delegate.remove(this.__methodClicked, value);
    },
    
    __methodClicked: null,
    
    _fireMethodClicked: function Open_Testing_TestHarnessEvents$_fireMethodClicked(methodInfo) {
        /// <param name="methodInfo" type="Open.Testing.Models.MethodInfo">
        /// </param>
        if (this.__methodClicked != null) {
            this.__methodClicked.invoke(this, new Open.Testing.MethodEventArgs(methodInfo));
        }
    },
    
    add_selectedClassChanged: function Open_Testing_TestHarnessEvents$add_selectedClassChanged(value) {
        /// <summary>
        /// Fires when when the selected class changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectedClassChanged = ss.Delegate.combine(this.__selectedClassChanged, value);
    },
    remove_selectedClassChanged: function Open_Testing_TestHarnessEvents$remove_selectedClassChanged(value) {
        /// <summary>
        /// Fires when when the selected class changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectedClassChanged = ss.Delegate.remove(this.__selectedClassChanged, value);
    },
    
    __selectedClassChanged: null,
    
    _fireSelectedClassChanged: function Open_Testing_TestHarnessEvents$_fireSelectedClassChanged(classInfo) {
        /// <param name="classInfo" type="Open.Testing.Models.ClassInfo">
        /// </param>
        if (this.__selectedClassChanged != null) {
            this.__selectedClassChanged.invoke(this, new Open.Testing.ClassEventArgs(classInfo));
        }
    },
    
    add_controlHostSizeChanged: function Open_Testing_TestHarnessEvents$add_controlHostSizeChanged(value) {
        /// <summary>
        /// Fires when the width of the control-host changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__controlHostSizeChanged = ss.Delegate.combine(this.__controlHostSizeChanged, value);
    },
    remove_controlHostSizeChanged: function Open_Testing_TestHarnessEvents$remove_controlHostSizeChanged(value) {
        /// <summary>
        /// Fires when the width of the control-host changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__controlHostSizeChanged = ss.Delegate.remove(this.__controlHostSizeChanged, value);
    },
    
    __controlHostSizeChanged: null,
    
    _fireControlHostSizeChanged: function Open_Testing_TestHarnessEvents$_fireControlHostSizeChanged() {
        if (this.__controlHostSizeChanged != null) {
            this.__controlHostSizeChanged.invoke(this, new ss.EventArgs());
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.MethodEventArgs

Open.Testing.MethodEventArgs = function Open_Testing_MethodEventArgs(methodInfo) {
    /// <param name="methodInfo" type="Open.Testing.Models.MethodInfo">
    /// </param>
    /// <field name="methodInfo" type="Open.Testing.Models.MethodInfo">
    /// </field>
    this.methodInfo = methodInfo;
}
Open.Testing.MethodEventArgs.prototype = {
    methodInfo: null
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.ClassEventArgs

Open.Testing.ClassEventArgs = function Open_Testing_ClassEventArgs(classInfo) {
    /// <param name="classInfo" type="Open.Testing.Models.ClassInfo">
    /// </param>
    /// <field name="classInfo" type="Open.Testing.Models.ClassInfo">
    /// </field>
    this.classInfo = classInfo;
}
Open.Testing.ClassEventArgs.prototype = {
    classInfo: null
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Common

Open.Testing.Common = function Open_Testing_Common() {
    /// <summary>
    /// Provides access to common global object.
    /// </summary>
}
Open.Testing.Common.getFromContainer = function Open_Testing_Common$getFromContainer() {
    /// <summary>
    /// Retrieves the common object from the container.
    /// </summary>
    /// <returns type="Open.Testing.Common"></returns>
    return Type.safeCast(Open.Core.DiContainer.get_defaultContainer().getSingleton(Open.Testing.Common), Open.Testing.Common);
}
Open.Testing.Common.prototype = {
    
    get_container: function Open_Testing_Common$get_container() {
        /// <value type="Open.Core.DiContainer"></value>
        return Open.Core.DiContainer.get_defaultContainer();
    },
    
    get_events: function Open_Testing_Common$get_events() {
        /// <value type="Open.Testing.TestHarnessEvents"></value>
        return Type.safeCast(this.get_container().getSingleton(Open.Testing.Internal.ITestHarnessEvents), Open.Testing.TestHarnessEvents);
    },
    
    get_shell: function Open_Testing_Common$get_shell() {
        /// <value type="Open.Testing.Views.ShellView"></value>
        return Type.safeCast(this.get_container().getSingleton(Open.Testing.Views.ShellView), Open.Testing.Views.ShellView);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing._methodHelper

Open.Testing._methodHelper = function Open_Testing__methodHelper() {
    /// <summary>
    /// Helper classes for examining methods.
    /// </summary>
    /// <field name="keyConstructor" type="String" static="true">
    /// </field>
    /// <field name="keyClassInitialize" type="String" static="true">
    /// </field>
    /// <field name="keyClassCleanup" type="String" static="true">
    /// </field>
    /// <field name="keyTestInitialize" type="String" static="true">
    /// </field>
    /// <field name="keyTestCleanup" type="String" static="true">
    /// </field>
}
Open.Testing._methodHelper.isConstructor = function Open_Testing__methodHelper$isConstructor(methodName) {
    /// <summary>
    /// Determines whether the specified method-name represents a constructor.
    /// </summary>
    /// <param name="methodName" type="String">
    /// The name of the method.
    /// </param>
    /// <returns type="Boolean"></returns>
    return methodName === Open.Testing._methodHelper.keyConstructor;
}
Open.Testing._methodHelper.isClassInitialize = function Open_Testing__methodHelper$isClassInitialize(methodName) {
    /// <summary>
    /// Determines whether the specified method-name represents the 'ClassInitialize' method.
    /// </summary>
    /// <param name="methodName" type="String">
    /// The name of the method.
    /// </param>
    /// <returns type="Boolean"></returns>
    return methodName === Open.Testing._methodHelper.keyClassInitialize;
}
Open.Testing._methodHelper.isClassCleanup = function Open_Testing__methodHelper$isClassCleanup(methodName) {
    /// <summary>
    /// Determines whether the specified method-name represents the 'ClassCleanup' method.
    /// </summary>
    /// <param name="methodName" type="String">
    /// The name of the method.
    /// </param>
    /// <returns type="Boolean"></returns>
    return methodName === Open.Testing._methodHelper.keyClassCleanup;
}
Open.Testing._methodHelper.isTestInitialize = function Open_Testing__methodHelper$isTestInitialize(methodName) {
    /// <summary>
    /// Determines whether the specified method-name represents the 'TestInitialize' method.
    /// </summary>
    /// <param name="methodName" type="String">
    /// The name of the method.
    /// </param>
    /// <returns type="Boolean"></returns>
    return methodName === Open.Testing._methodHelper.keyTestInitialize;
}
Open.Testing._methodHelper.isTestCleanup = function Open_Testing__methodHelper$isTestCleanup(methodName) {
    /// <summary>
    /// Determines whether the specified method-name represents the 'TestCleanup' method.
    /// </summary>
    /// <param name="methodName" type="String">
    /// The name of the method.
    /// </param>
    /// <returns type="Boolean"></returns>
    return methodName === Open.Testing._methodHelper.keyTestCleanup;
}
Open.Testing._methodHelper.isSpecial = function Open_Testing__methodHelper$isSpecial(methodName) {
    /// <summary>
    /// Determines whether the specified DictionaryEntry is one of the special Setup/Teardown methods.
    /// </summary>
    /// <param name="methodName" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    if (Open.Testing._methodHelper.isClassInitialize(methodName)) {
        return true;
    }
    if (Open.Testing._methodHelper.isClassCleanup(methodName)) {
        return true;
    }
    if (Open.Testing._methodHelper.isTestInitialize(methodName)) {
        return true;
    }
    if (Open.Testing._methodHelper.isTestCleanup(methodName)) {
        return true;
    }
    return false;
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Application

Open.Testing.Application = function Open_Testing_Application() {
    /// <field name="_shell" type="Open.Testing.Views.ShellView" static="true">
    /// </field>
    /// <field name="_container" type="Open.Core.DiContainer" static="true">
    /// </field>
    /// <field name="_resizeController" type="Open.Testing.Controllers.PanelResizeController" static="true">
    /// </field>
    /// <field name="_sidebarController" type="Open.Testing.Controllers.SidebarController" static="true">
    /// </field>
    /// <field name="_hostCanvasController" type="Open.Testing.Controllers.HostCanvasController" static="true">
    /// </field>
}
Open.Testing.Application.get_container = function Open_Testing_Application$get_container() {
    /// <summary>
    /// Gets the DI container.
    /// </summary>
    /// <value type="Open.Core.DiContainer"></value>
    return Open.Testing.Application._container || (Open.Testing.Application._container = Open.Core.DiContainer.get_defaultContainer());
}
Open.Testing.Application.main = function Open_Testing_Application$main(args) {
    /// <summary>
    /// Application entry point.
    /// </summary>
    /// <param name="args" type="Object">
    /// Init parameters.
    /// </param>
    Open.Testing.Application.get_container().registerSingleton(Open.Testing.Internal.ITestHarnessEvents, new Open.Testing.TestHarnessEvents());
    Open.Testing.Application.get_container().registerSingleton(Open.Testing.Common, new Open.Testing.Common());
    Open.Core.Log.set_view(new Open.Core.Controls.LogView($(Open.Testing.CssSelectors.log).first()));
    Open.Testing.Application._shell = new Open.Testing.Views.ShellView($(Open.Testing.CssSelectors.root));
    Open.Testing.Application.get_container().registerSingleton(Open.Testing.Views.ShellView, Open.Testing.Application._shell);
    Open.Testing.Application._resizeController = new Open.Testing.Controllers.PanelResizeController();
    Open.Testing.Application._sidebarController = new Open.Testing.Controllers.SidebarController();
    Open.Testing.Application._hostCanvasController = new Open.Testing.Controllers.HostCanvasController();
    Open.Testing.Application._addPackage('/Content/Scripts/TestHarness.Test.debug.js', 'Test.Application.main');
    Open.Testing.Application._addPackage('/Content/Scripts/Open.Core.Test.debug.js', 'Open.Core.Test.Application.main');
    Open.Testing.Application._addPackage('/Content/Scripts/Quest.Rogue.Test.debug.js', 'Quest.Rogue.Test.Application.main');
    Open.Testing.Application._addPackage('/Content/Scripts/Quest.OnDemand.Test.debug.js', 'Quest.OnDemand.Test.Application.main');
}
Open.Testing.Application._addPackage = function Open_Testing_Application$_addPackage(scriptUrl, initMethod) {
    /// <param name="scriptUrl" type="String">
    /// </param>
    /// <param name="initMethod" type="String">
    /// </param>
    var testHarnessPackage = Open.Testing.Models.PackageInfo.singletonFromUrl(scriptUrl, initMethod);
    Open.Testing.Application._sidebarController.addPackage(testHarnessPackage);
}


Type.registerNamespace('Open.Testing.Automation');

////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Automation.ClassTestRunner

Open.Testing.Automation.ClassTestRunner = function Open_Testing_Automation_ClassTestRunner(classInfo) {
    /// <summary>
    /// Runs all tests in a single class.
    /// </summary>
    /// <param name="classInfo" type="Open.Testing.Models.ClassInfo">
    /// The class to run.
    /// </param>
    /// <field name="_classInfo" type="Open.Testing.Models.ClassInfo">
    /// </field>
    /// <field name="_results" type="Array">
    /// </field>
    this._results = [];
    this._classInfo = classInfo;
}
Open.Testing.Automation.ClassTestRunner.prototype = {
    _classInfo: null,
    
    get_total: function Open_Testing_Automation_ClassTestRunner$get_total() {
        /// <summary>
        /// Gets the total number of tests that have been run.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._results.length;
    },
    
    get_successes: function Open_Testing_Automation_ClassTestRunner$get_successes() {
        /// <summary>
        /// Gets the number of successfully executed methods..
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return Open.Core.Helper.get_collection().total(this._results, ss.Delegate.create(this, function(o) {
            return (o).error == null;
        }));
    },
    
    get_failures: function Open_Testing_Automation_ClassTestRunner$get_failures() {
        /// <summary>
        /// Gets the number of failed tests.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this.get_total() - this.get_successes();
    },
    
    run: function Open_Testing_Automation_ClassTestRunner$run() {
        /// <summary>
        /// Runs all the tests in the class.
        /// </summary>
        var $enum1 = ss.IEnumerator.getEnumerator(this._classInfo);
        while ($enum1.moveNext()) {
            var method = $enum1.get_current();
            var item = new Open.Testing.Automation._executedTest();
            item.method = method;
            item.error = method.invoke();
            this._results.add(item);
        }
    },
    
    writeResults: function Open_Testing_Automation_ClassTestRunner$writeResults(log) {
        /// <summary>
        /// Writes the results of a test run to the output log.
        /// </summary>
        /// <param name="log" type="Open.Core.ILog">
        /// The log to write to.
        /// </param>
        var successes = this.get_successes();
        var failures = this.get_failures();
        var hasFailures = failures > 0;
        var list = new Open.Core.Controls.HtmlPrimitive.HtmlList(Open.Core.HtmlListType.unordered, Open.Testing.CssSelectors.classes.logIndentedList);
        list.add(String.format('Successes: {0} ({1}%)', successes, this._toPercent(successes)));
        list.add(String.format('Failures: {0} ({1}%)', failures, this._toPercent(failures)));
        var summary = String.format('Test run for class <b>{0}</b><br/>{1}', this._classInfo.get_displayName(), list.get_outerHtml());
        log.write(summary, (hasFailures) ? Open.Core.LogSeverity.error : Open.Core.LogSeverity.success);
        if (hasFailures) {
            log.lineBreak();
        }
        var $enum1 = ss.IEnumerator.getEnumerator(this._results);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            if (item.error == null) {
                continue;
            }
            log.write(item.method.formatError(item.error), Open.Core.LogSeverity.error);
        }
        log.newSection();
    },
    
    _toPercent: function Open_Testing_Automation_ClassTestRunner$_toPercent(count) {
        /// <param name="count" type="Number" integer="true">
        /// </param>
        /// <returns type="Number"></returns>
        if (this.get_total() === 0) {
            return 0;
        }
        return Math.round((count / this.get_total()) * 100);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Automation._executedTest

Open.Testing.Automation._executedTest = function Open_Testing_Automation__executedTest() {
    /// <field name="method" type="Open.Testing.Models.MethodInfo">
    /// </field>
    /// <field name="error" type="Error">
    /// </field>
}
Open.Testing.Automation._executedTest.prototype = {
    method: null,
    error: null
}


Type.registerNamespace('Open.Testing.Controllers');

////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.ClassController

Open.Testing.Controllers.ClassController = function Open_Testing_Controllers_ClassController(classInfo) {
    /// <summary>
    /// Controlls the selected test-class.
    /// </summary>
    /// <param name="classInfo" type="Open.Testing.Models.ClassInfo">
    /// The test-class that is under control.
    /// </param>
    /// <field name="_classInfo$3" type="Open.Testing.Models.ClassInfo">
    /// </field>
    /// <field name="_sidebarView$3" type="Open.Testing.Views.SidebarView">
    /// </field>
    /// <field name="_events$3" type="Open.Testing.TestHarnessEvents">
    /// </field>
    Open.Testing.Controllers.ClassController.initializeBase(this);
    this._classInfo$3 = classInfo;
    this._sidebarView$3 = this.get_common().get_shell().get_sidebar();
    this._events$3 = this.get_common().get_events();
    this._events$3.add_methodClicked(ss.Delegate.create(this, this._onMethodClicked$3));
    this._sidebarView$3.get_methodList().add_runClick(ss.Delegate.create(this, this._onRunClick$3));
    this.reset();
}
Open.Testing.Controllers.ClassController.prototype = {
    _classInfo$3: null,
    _sidebarView$3: null,
    _events$3: null,
    
    onDisposed: function Open_Testing_Controllers_ClassController$onDisposed() {
        this._events$3.remove_methodClicked(ss.Delegate.create(this, this._onMethodClicked$3));
        this._sidebarView$3.get_methodList().remove_runClick(ss.Delegate.create(this, this._onRunClick$3));
        if (this._classInfo$3.get_classCleanup() != null) {
            this._classInfo$3.get_classCleanup().invoke();
        }
        Open.Testing.TestHarness.reset();
        Open.Testing.Controllers.ClassController.callBaseMethod(this, 'onDisposed');
    },
    
    _onMethodClicked$3: function Open_Testing_Controllers_ClassController$_onMethodClicked$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Testing.MethodEventArgs">
        /// </param>
        this.invokeSelectedMethod();
    },
    
    _onRunClick$3: function Open_Testing_Controllers_ClassController$_onRunClick$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.runAll();
    },
    
    get__selectedMethod$3: function Open_Testing_Controllers_ClassController$get__selectedMethod$3() {
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._sidebarView$3.get_methodList().get_selectedMethod();
    },
    
    reset: function Open_Testing_Controllers_ClassController$reset() {
        /// <summary>
        /// Initializes the current class.
        /// </summary>
        if (this._classInfo$3.get_classInitialize() != null) {
            this._classInfo$3.get_classInitialize().invoke();
        }
        Open.Core.Log.newSection();
    },
    
    invokeSelectedMethod: function Open_Testing_Controllers_ClassController$invokeSelectedMethod() {
        /// <summary>
        /// Invokes the currently selected method (including pre/post TestInitialize and TestCleanup methods).
        /// </summary>
        /// <returns type="Boolean"></returns>
        var method = this.get__selectedMethod$3();
        if (method == null) {
            return false;
        }
        method.invoke();
        Open.Core.Log.newSection();
        return true;
    },
    
    runAll: function Open_Testing_Controllers_ClassController$runAll() {
        /// <summary>
        /// Runs all tests within the class.
        /// </summary>
        var runner = new Open.Testing.Automation.ClassTestRunner(this._classInfo$3);
        var originalState = Open.Core.Log.get_isActive();
        Open.Core.Log.set_isActive(false);
        runner.run();
        Open.Testing.TestHarness.reset();
        this.reset();
        Open.Core.Log.set_isActive(originalState);
        Open.Core.Log.clear();
        runner.writeResults(Open.Core.Log.get_writer());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.HostCanvasController

Open.Testing.Controllers.HostCanvasController = function Open_Testing_Controllers_HostCanvasController() {
    /// <summary>
    /// Controls the 'Host Canvas' panel where controls under test are displayed.
    /// </summary>
    /// <field name="_divControlHost$3" type="jQueryObject">
    /// </field>
    /// <field name="_views$3" type="Array">
    /// </field>
    /// <field name="_events$3" type="Open.Testing.TestHarnessEvents">
    /// </field>
    this._views$3 = [];
    Open.Testing.Controllers.HostCanvasController.initializeBase(this);
    this._divControlHost$3 = $(Open.Testing.CssSelectors.controlHost);
    this._events$3 = this.get_common().get_events();
    this._events$3.add_controlAdded(ss.Delegate.create(this, this._onControlAdded$3));
    this._events$3.add_clearControls(ss.Delegate.create(this, this._onClearControls$3));
    this._events$3.add_updateLayout(ss.Delegate.create(this, this._onUpdateLayout$3));
}
Open.Testing.Controllers.HostCanvasController.prototype = {
    _divControlHost$3: null,
    _events$3: null,
    
    onDisposed: function Open_Testing_Controllers_HostCanvasController$onDisposed() {
        this.clear();
        this._events$3.remove_controlAdded(ss.Delegate.create(this, this._onControlAdded$3));
        this._events$3.remove_clearControls(ss.Delegate.create(this, this._onClearControls$3));
        this._events$3.remove_updateLayout(ss.Delegate.create(this, this._onUpdateLayout$3));
        Open.Testing.Controllers.HostCanvasController.callBaseMethod(this, 'onDisposed');
    },
    
    _onControlAdded$3: function Open_Testing_Controllers_HostCanvasController$_onControlAdded$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Testing.Internal.TestControlEventArgs">
        /// </param>
        this._addView$3(e.control, e.htmlElement, e.controlDisplayMode);
    },
    
    _onClearControls$3: function Open_Testing_Controllers_HostCanvasController$_onClearControls$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.clear();
    },
    
    _onControlSizeChanged$3: function Open_Testing_Controllers_HostCanvasController$_onControlSizeChanged$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._updateLayout$3();
    },
    
    _onUpdateLayout$3: function Open_Testing_Controllers_HostCanvasController$_onUpdateLayout$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._updateLayout$3();
    },
    
    clear: function Open_Testing_Controllers_HostCanvasController$clear() {
        /// <summary>
        /// Clears all views.
        /// </summary>
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$3);
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            if (view.get_control() != null) {
                view.get_control().remove_sizeChanged(ss.Delegate.create(this, this._onControlSizeChanged$3));
            }
        }
        Open.Core.Helper.get_collection().disposeAndClear(this._views$3);
    },
    
    _addView$3: function Open_Testing_Controllers_HostCanvasController$_addView$3(control, htmlElement, controlDisplayMode) {
        /// <param name="control" type="Open.Core.IView">
        /// </param>
        /// <param name="htmlElement" type="jQueryObject">
        /// </param>
        /// <param name="controlDisplayMode" type="Open.Testing.ControlDisplayMode">
        /// </param>
        var view = new Open.Testing.Views.ControlWrapperView(this._divControlHost$3, control, htmlElement, controlDisplayMode, this._views$3);
        this._views$3.add(view);
        if (control != null) {
            control.add_sizeChanged(ss.Delegate.create(this, this._onControlSizeChanged$3));
        }
        if (this._views$3.length > 1) {
            var $enum1 = ss.IEnumerator.getEnumerator(this._views$3);
            while ($enum1.moveNext()) {
                var item = $enum1.get_current();
                if (item.get_displayMode() === Open.Testing.ControlDisplayMode.fill) {
                    item.set_displayMode(Open.Testing.ControlDisplayMode.fillWithMargin);
                }
            }
        }
        this._updateLayout$3();
    },
    
    _updateLayout$3: function Open_Testing_Controllers_HostCanvasController$_updateLayout$3() {
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$3);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            item.updateLayout();
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.MethodListController

Open.Testing.Controllers.MethodListController = function Open_Testing_Controllers_MethodListController() {
    /// <summary>
    /// Controller for the test-method list.
    /// </summary>
    Open.Testing.Controllers.MethodListController.initializeBase(this);
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers._methodListHeightController

Open.Testing.Controllers._methodListHeightController = function Open_Testing_Controllers__methodListHeightController(sidebarView) {
    /// <summary>
    /// Controls the height of the MethodList.
    /// </summary>
    /// <param name="sidebarView" type="Open.Testing.Views.SidebarView">
    /// The sidebar.
    /// </param>
    /// <field name="_sidebarView$3" type="Open.Testing.Views.SidebarView">
    /// </field>
    /// <field name="_methodList$3" type="Open.Testing.Views.MethodListView">
    /// </field>
    /// <field name="_divSidebarContent$3" type="jQueryObject">
    /// </field>
    /// <field name="_events$3" type="Open.Testing.TestHarnessEvents">
    /// </field>
    Open.Testing.Controllers._methodListHeightController.initializeBase(this);
    this._sidebarView$3 = sidebarView;
    this._methodList$3 = sidebarView.get_methodList();
    this._divSidebarContent$3 = sidebarView.get_container().children(Open.Testing.CssSelectors.sidebarContent);
    this._events$3 = this.get_common().get_events();
    this._events$3.add_selectedClassChanged(ss.Delegate.create(this, this._onSelectedClassChanged$3));
    this._hideList$3(null);
}
Open.Testing.Controllers._methodListHeightController.prototype = {
    _sidebarView$3: null,
    _methodList$3: null,
    _divSidebarContent$3: null,
    _events$3: null,
    
    onDisposed: function Open_Testing_Controllers__methodListHeightController$onDisposed() {
        this._events$3.remove_selectedClassChanged(ss.Delegate.create(this, this._onSelectedClassChanged$3));
        Open.Testing.Controllers._methodListHeightController.callBaseMethod(this, 'onDisposed');
    },
    
    _onSelectedClassChanged$3: function Open_Testing_Controllers__methodListHeightController$_onSelectedClassChanged$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Testing.ClassEventArgs">
        /// </param>
        if (e.classInfo != null) {
            this._showList$3(null);
        }
        else {
            this._hideList$3(null);
        }
    },
    
    _showList$3: function Open_Testing_Controllers__methodListHeightController$_showList$3(onComplete) {
        /// <summary>
        /// Reveals the method-list.
        /// </summary>
        /// <param name="onComplete" type="Action">
        /// The action to invoke when complete
        /// </param>
        this._sidebarView$3.set_isMethodListVisible(true);
        var height = this._getHeight$3();
        this._animateHeights$3(height, onComplete);
    },
    
    _hideList$3: function Open_Testing_Controllers__methodListHeightController$_hideList$3(onComplete) {
        /// <summary>
        /// Hides the method-list.
        /// </summary>
        /// <param name="onComplete" type="Action">
        /// The action to invoke when complete
        /// </param>
        this._sidebarView$3.set_isMethodListVisible(false);
        this._animateHeights$3(0, onComplete);
    },
    
    updateLayout: function Open_Testing_Controllers__methodListHeightController$updateLayout() {
        /// <summary>
        /// Updates the height of the method-list (if it's currently showing).
        /// </summary>
        if (!this._sidebarView$3.get_isMethodListVisible()) {
            return;
        }
        this._methodList$3.get_container().css(Open.Core.Css.height, this._getHeight$3() + Open.Core.Css.px);
    },
    
    _animateHeights$3: function Open_Testing_Controllers__methodListHeightController$_animateHeights$3(methodListHeight, onComplete) {
        /// <param name="methodListHeight" type="Number" integer="true">
        /// </param>
        /// <param name="onComplete" type="Action">
        /// </param>
        var methodListProps = {};
        methodListProps[Open.Core.Css.height] = methodListHeight;
        var rootListProps = {};
        rootListProps[Open.Core.Css.bottom] = methodListHeight;
        var isShowing = methodListHeight > 0;
        if (isShowing) {
            Open.Core.Css.setVisible(this._methodList$3.get_container(), true);
        }
        this._methodList$3.updateLayout();
        this._animate$3(isShowing, this._methodList$3.get_container(), methodListProps, null);
        this._animate$3(isShowing, this._sidebarView$3.get_rootList().get_container(), rootListProps, onComplete);
    },
    
    _animate$3: function Open_Testing_Controllers__methodListHeightController$_animate$3(isShowing, div, properties, onComplete) {
        /// <param name="isShowing" type="Boolean">
        /// </param>
        /// <param name="div" type="jQueryObject">
        /// </param>
        /// <param name="properties" type="Object">
        /// </param>
        /// <param name="onComplete" type="Action">
        /// </param>
        div.animate(properties, Open.Core.Helper.get_time().toMsecs(Open.Testing.Views.SidebarView.slideDuration), 'swing', ss.Delegate.create(this, function() {
            if (!isShowing) {
                Open.Core.Css.setVisible(this._methodList$3.get_container(), false);
            }
            Open.Core.Helper.invokeOrDefault(onComplete);
        }));
    },
    
    _getHeight$3: function Open_Testing_Controllers__methodListHeightController$_getHeight$3() {
        /// <returns type="Number" integer="true"></returns>
        var divList = this._methodList$3.get_container();
        var originalVisibility = Open.Core.Css.isVisible(divList);
        Open.Core.Css.setVisible(divList, true);
        var listHeight = this._methodList$3.get_offsetHeight();
        Open.Core.Css.setVisible(divList, originalVisibility);
        var maxHeight = (this._divSidebarContent$3.height() * 0.66);
        if (listHeight > maxHeight) {
            listHeight = maxHeight;
        }
        return listHeight;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.PanelResizeController

Open.Testing.Controllers.PanelResizeController = function Open_Testing_Controllers_PanelResizeController() {
    /// <summary>
    /// Handles resizing of panels within the shell.
    /// </summary>
    /// <field name="_sidebarMinWidth$3" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_sidebarMaxWidthMargin$3" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_outputLogMaxHeightMargin$3" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_sideBarResizer$3" type="Open.Core.UI.HorizontalPanelResizer">
    /// </field>
    /// <field name="_outputResizer$3" type="Open.Core.UI.VerticalPanelResizer">
    /// </field>
    /// <field name="_events$3" type="Open.Testing.TestHarnessEvents">
    /// </field>
    Open.Testing.Controllers.PanelResizeController.initializeBase(this);
    this._events$3 = this.get_common().get_events();
    this._sideBarResizer$3 = new Open.Core.UI.HorizontalPanelResizer(Open.Testing.CssSelectors.sidebar, 'TH_SB');
    this._sideBarResizer$3.add_resized(ss.Delegate.create(this, function() {
        this._syncMainPanelWidth$3();
    }));
    this._sideBarResizer$3.set_minWidth(Open.Testing.Controllers.PanelResizeController._sidebarMinWidth$3);
    this._sideBarResizer$3.set_maxWidthMargin(Open.Testing.Controllers.PanelResizeController._sidebarMaxWidthMargin$3);
    Open.Testing.Controllers.PanelResizeController._initializeResizer$3(this._sideBarResizer$3);
    this._outputResizer$3 = new Open.Core.UI.VerticalPanelResizer(Open.Core.Css.toId(Open.Testing.Elements.outputLog), 'TH_OL');
    this._outputResizer$3.add_resized(ss.Delegate.create(this, function() {
        this._syncControlHostHeight$3();
    }));
    this._outputResizer$3.set_minHeight(Open.Core.Html.height(Open.Testing.CssSelectors.logTitlebar));
    this._outputResizer$3.set_maxHeightMargin(Open.Testing.Controllers.PanelResizeController._outputLogMaxHeightMargin$3);
    Open.Testing.Controllers.PanelResizeController._initializeResizer$3(this._outputResizer$3);
    Open.Core.GlobalEvents.add_windowResize(ss.Delegate.create(this, function() {
        this._syncControlHostHeight$3();
    }));
    this.updateLayout();
}
Open.Testing.Controllers.PanelResizeController._initializeResizer$3 = function Open_Testing_Controllers_PanelResizeController$_initializeResizer$3(resizer) {
    /// <param name="resizer" type="Open.Core.UI.PanelResizerBase">
    /// </param>
    resizer.set_rootContainerId(Open.Testing.Elements.root);
    resizer.initialize();
}
Open.Testing.Controllers.PanelResizeController.prototype = {
    _sideBarResizer$3: null,
    _outputResizer$3: null,
    _events$3: null,
    
    updateLayout: function Open_Testing_Controllers_PanelResizeController$updateLayout() {
        /// <summary>
        /// Updates the layout of the panels.
        /// </summary>
        this._syncMainPanelWidth$3();
        this._syncControlHostHeight$3();
    },
    
    _syncMainPanelWidth$3: function Open_Testing_Controllers_PanelResizeController$_syncMainPanelWidth$3() {
        $(Open.Testing.CssSelectors.main).css(Open.Core.Css.left, (Open.Core.Html.width(Open.Testing.CssSelectors.sidebar) + 1) + Open.Core.Css.px);
        this._events$3._fireControlHostSizeChanged();
    },
    
    _syncControlHostHeight$3: function Open_Testing_Controllers_PanelResizeController$_syncControlHostHeight$3() {
        var height = Open.Core.Html.height(Open.Testing.CssSelectors.mainContent) - Open.Core.Html.height(Open.Testing.CssSelectors.logContainer);
        $(Open.Testing.CssSelectors.controlHost).css(Open.Core.Css.height, (height - 1) + Open.Core.Css.px);
        this._events$3._fireControlHostSizeChanged();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.SidebarController

Open.Testing.Controllers.SidebarController = function Open_Testing_Controllers_SidebarController() {
    /// <summary>
    /// Controller for the side-bar.
    /// </summary>
    /// <field name="_packageControllers$3" type="Array">
    /// </field>
    /// <field name="_view$3" type="Open.Testing.Views.SidebarView">
    /// </field>
    /// <field name="_methodListController$3" type="Open.Testing.Controllers.MethodListController">
    /// </field>
    this._packageControllers$3 = [];
    Open.Testing.Controllers.SidebarController.initializeBase(this);
    this._view$3 = this.get_common().get_shell().get_sidebar();
    this._methodListController$3 = new Open.Testing.Controllers.MethodListController();
    this._TEMP$3();
}
Open.Testing.Controllers.SidebarController.prototype = {
    _view$3: null,
    _methodListController$3: null,
    
    onDisposed: function Open_Testing_Controllers_SidebarController$onDisposed() {
        this._view$3.dispose();
        this._methodListController$3.dispose();
        var $enum1 = ss.IEnumerator.getEnumerator(this._packageControllers$3);
        while ($enum1.moveNext()) {
            var controller = $enum1.get_current();
            controller.dispose();
        }
        Open.Testing.Controllers.SidebarController.callBaseMethod(this, 'onDisposed');
    },
    
    _TEMP$3: function Open_Testing_Controllers_SidebarController$_TEMP$3() {
        var rootNode = new Open.Testing.Controllers.MyNode('Root');
        this._view$3.get_rootList().set_rootNode(rootNode);
        rootNode.addChild(new Open.Testing.Controllers.MyNode('Recent'));
        var child1 = Type.safeCast(rootNode.childAt(0), Open.Testing.Controllers.MyNode);
        var child2 = (rootNode.childAt(1));
        var child3 = (rootNode.childAt(2));
        child1.addChild(new Open.Testing.Controllers.MyNode('Recent Child 1'));
        child1.addChild(new Open.Testing.Controllers.MyNode('Recent Child 2'));
        child1.addChild(new Open.Testing.Controllers.MyNode('Recent Child 3'));
        var recent1 = Type.safeCast(child1.childAt(0), Open.Testing.Controllers.MyNode);
        recent1.addChild(new Open.Testing.Controllers.MyNode('Recent Grandchild 1'));
        recent1.addChild(new Open.Testing.Controllers.MyNode('Recent Grandchild 2'));
        recent1.addChild(new Open.Testing.Controllers.MyNode('Recent Grandchild 3'));
    },
    
    addPackage: function Open_Testing_Controllers_SidebarController$addPackage(testPackage) {
        /// <summary>
        /// Adds a test-package to the controller.
        /// </summary>
        /// <param name="testPackage" type="Open.Testing.Models.PackageInfo">
        /// The test-package to add.
        /// </param>
        if (testPackage == null) {
            return;
        }
        var node = new Open.Testing.Models.PackageListItem(testPackage);
        this._view$3.get_rootList().get_rootNode().addChild(node);
        var controller = new Open.Testing.Controllers.PackageController(node);
        this._packageControllers$3.add(controller);
        controller.add_loaded(ss.Delegate.create(this, function() {
            this._view$3.get_rootList().set_selectedParent(controller.get_rootNode());
        }));
    },
    
    removePackage: function Open_Testing_Controllers_SidebarController$removePackage(testPackage) {
        /// <summary>
        /// Removes the specified package.
        /// </summary>
        /// <param name="testPackage" type="Open.Testing.Models.PackageInfo">
        /// The test-package to remove.
        /// </param>
        if (testPackage == null) {
            return;
        }
        var controller = this._getController$3(testPackage);
        if (controller == null) {
            return;
        }
        this._view$3.get_rootList().get_rootNode().removeChild(controller.get_rootNode());
        Open.Core.Log.info(String.format('Test package unloaded: {0}', Open.Core.Html.toHyperlink(testPackage.get_id(), null, Open.Core.LinkTarget.blank)));
        Open.Core.Log.lineBreak();
    },
    
    _getController$3: function Open_Testing_Controllers_SidebarController$_getController$3(testPackage) {
        /// <param name="testPackage" type="Open.Testing.Models.PackageInfo">
        /// </param>
        /// <returns type="Open.Testing.Controllers.PackageController"></returns>
        return Type.safeCast(Open.Core.Helper.get_collection().first(this._packageControllers$3, ss.Delegate.create(this, function(o) {
            return (o).get_testPackage() === testPackage;
        })), Open.Testing.Controllers.PackageController);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.MyNode

Open.Testing.Controllers.MyNode = function Open_Testing_Controllers_MyNode(text) {
    /// <param name="text" type="String">
    /// </param>
    Open.Testing.Controllers.MyNode.initializeBase(this);
    this.set_text(text);
}
Open.Testing.Controllers.MyNode.prototype = {
    
    toString: function Open_Testing_Controllers_MyNode$toString() {
        /// <returns type="String"></returns>
        return Open.Testing.Controllers.MyNode.callBaseMethod(this, 'toString');
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.PackageController

Open.Testing.Controllers.PackageController = function Open_Testing_Controllers_PackageController(rootNode) {
    /// <summary>
    /// Controller for a single test package.
    /// </summary>
    /// <param name="rootNode" type="Open.Testing.Models.PackageListItem">
    /// The root list-item node.
    /// </param>
    /// <field name="__loaded$3" type="EventHandler">
    /// </field>
    /// <field name="propSelectedClass" type="String" static="true">
    /// </field>
    /// <field name="_loadTimeout$3" type="Number" static="true">
    /// </field>
    /// <field name="_rootNode$3" type="Open.Testing.Models.PackageListItem">
    /// </field>
    /// <field name="_sidebarView$3" type="Open.Testing.Views.SidebarView">
    /// </field>
    /// <field name="_events$3" type="Open.Testing.TestHarnessEvents">
    /// </field>
    /// <field name="_selectedClassController$3" type="Open.Testing.Controllers.ClassController">
    /// </field>
    Open.Testing.Controllers.PackageController.initializeBase(this);
    this._rootNode$3 = rootNode;
    this._sidebarView$3 = this.get_common().get_shell().get_sidebar();
    this._events$3 = this.get_common().get_events();
    rootNode.add_selectionChanged(ss.Delegate.create(this, this._onSelectionChanged$3));
    rootNode.add_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$3));
}
Open.Testing.Controllers.PackageController.prototype = {
    
    add_loaded: function Open_Testing_Controllers_PackageController$add_loaded(value) {
        /// <summary>
        /// Fires when the package has laoded.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__loaded$3 = ss.Delegate.combine(this.__loaded$3, value);
    },
    remove_loaded: function Open_Testing_Controllers_PackageController$remove_loaded(value) {
        /// <summary>
        /// Fires when the package has laoded.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__loaded$3 = ss.Delegate.remove(this.__loaded$3, value);
    },
    
    __loaded$3: null,
    
    _fireLoaded$3: function Open_Testing_Controllers_PackageController$_fireLoaded$3() {
        if (this.__loaded$3 != null) {
            this.__loaded$3.invoke(this, new ss.EventArgs());
        }
    },
    
    _rootNode$3: null,
    _sidebarView$3: null,
    _events$3: null,
    _selectedClassController$3: null,
    
    onDisposed: function Open_Testing_Controllers_PackageController$onDisposed() {
        this._rootNode$3.remove_selectionChanged(ss.Delegate.create(this, this._onSelectionChanged$3));
        this._rootNode$3.remove_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$3));
        if (this._selectedClassController$3 != null) {
            this._selectedClassController$3.dispose();
        }
        Open.Testing.Controllers.PackageController.callBaseMethod(this, 'onDisposed');
    },
    
    _onSelectionChanged$3: function Open_Testing_Controllers_PackageController$_onSelectionChanged$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (this.get_rootNode().get_isSelected()) {
            this._download$3();
        }
    },
    
    _onChildSelectionChanged$3: function Open_Testing_Controllers_PackageController$_onChildSelectionChanged$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        var item = Type.safeCast(Open.Core.Helper.get_tree().firstSelectedChild(this.get_rootNode()), Open.Testing.Models.ClassListItem);
        this.set_selectedClass((item == null) ? null : item.get_classInfo());
    },
    
    get_isSelected: function Open_Testing_Controllers_PackageController$get_isSelected() {
        /// <summary>
        /// Gets whether this package is currently selected within the tree..
        /// </summary>
        /// <value type="Boolean"></value>
        return this._sidebarView$3.get_rootList().get_selectedParent() === this.get_rootNode();
    },
    
    get_testPackage: function Open_Testing_Controllers_PackageController$get_testPackage() {
        /// <summary>
        /// Gets the test-package that is under control.
        /// </summary>
        /// <value type="Open.Testing.Models.PackageInfo"></value>
        return this._rootNode$3.get_testPackage();
    },
    
    get_rootNode: function Open_Testing_Controllers_PackageController$get_rootNode() {
        /// <summary>
        /// Gets the root list-item node.
        /// </summary>
        /// <value type="Open.Testing.Models.PackageListItem"></value>
        return this._rootNode$3;
    },
    
    get_selectedClass: function Open_Testing_Controllers_PackageController$get_selectedClass() {
        /// <summary>
        /// Gets or sets the currently selected test class.
        /// </summary>
        /// <value type="Open.Testing.Models.ClassInfo"></value>
        return this.get(Open.Testing.Controllers.PackageController.propSelectedClass, null);
    },
    set_selectedClass: function Open_Testing_Controllers_PackageController$set_selectedClass(value) {
        /// <summary>
        /// Gets or sets the currently selected test class.
        /// </summary>
        /// <value type="Open.Testing.Models.ClassInfo"></value>
        if (this.set(Open.Testing.Controllers.PackageController.propSelectedClass, value, null)) {
            if (this._selectedClassController$3 != null) {
                this._selectedClassController$3.dispose();
            }
            if (value != null) {
                this._selectedClassController$3 = new Open.Testing.Controllers.ClassController(value);
            }
            this._sidebarView$3.get_methodList().set_classInfo(value);
            this._events$3._fireSelectedClassChanged(value);
        }
        return value;
    },
    
    _download$3: function Open_Testing_Controllers_PackageController$_download$3() {
        if (this.get_testPackage().get_isLoaded()) {
            return;
        }
        var loader = this.get_testPackage().get_loader();
        var link = Open.Core.Html.toHyperlink(loader.get_scriptUrl(), null, Open.Core.LinkTarget.blank);
        var timeout = new Open.Core.DelayedAction(Open.Testing.Controllers.PackageController._loadTimeout$3, ss.Delegate.create(this, function() {
            loader.dispose();
            Open.Core.Log.error(String.format('<b>Failed</b> to download and initialize the test-package at \'{0}\'.  Please ensure the file exists.', link));
            Open.Core.Log.lineBreak();
        }));
        Open.Core.Log.info(String.format('Downloading test-package: {0} ...', link));
        loader.load(ss.Delegate.create(this, function() {
            timeout.stop();
            if (loader.get_succeeded()) {
                Open.Core.Log.success('Test-package loaded successfully.');
                this._addChildNodes$3();
                this._fireLoaded$3();
            }
            Open.Core.Log.newSection();
        }));
        timeout.start();
    },
    
    _addChildNodes$3: function Open_Testing_Controllers_PackageController$_addChildNodes$3() {
        var $enum1 = ss.IEnumerator.getEnumerator(this.get_testPackage());
        while ($enum1.moveNext()) {
            var testClass = $enum1.get_current();
            var node = new Open.Testing.Models.ClassListItem(testClass);
            this.get_rootNode().addChild(node);
        }
    }
}


Type.registerNamespace('Open.Testing.Models');

////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.MethodListItem

Open.Testing.Models.MethodListItem = function Open_Testing_Models_MethodListItem(method) {
    /// <summary>
    /// A list-item node for a single Test-Method.
    /// </summary>
    /// <param name="method" type="Open.Testing.Models.MethodInfo">
    /// The test-method this node represents.
    /// </param>
    /// <field name="_method$3" type="Open.Testing.Models.MethodInfo">
    /// </field>
    Open.Testing.Models.MethodListItem.initializeBase(this);
    this._method$3 = method;
    this.set_text(method.get_displayName());
}
Open.Testing.Models.MethodListItem.prototype = {
    _method$3: null,
    
    get_method: function Open_Testing_Models_MethodListItem$get_method() {
        /// <summary>
        /// Gets the test-package this node represents.
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._method$3;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.ClassListItem

Open.Testing.Models.ClassListItem = function Open_Testing_Models_ClassListItem(classInfo) {
    /// <summary>
    /// A list-item node for a TestClass.
    /// </summary>
    /// <param name="classInfo" type="Open.Testing.Models.ClassInfo">
    /// The test-class this node represents.
    /// </param>
    /// <field name="_classInfo$3" type="Open.Testing.Models.ClassInfo">
    /// </field>
    Open.Testing.Models.ClassListItem.initializeBase(this);
    this._classInfo$3 = classInfo;
    this.set_text(classInfo.get_displayName());
}
Open.Testing.Models.ClassListItem.prototype = {
    _classInfo$3: null,
    
    get_classInfo: function Open_Testing_Models_ClassListItem$get_classInfo() {
        /// <summary>
        /// Gets the test-package this node represents.
        /// </summary>
        /// <value type="Open.Testing.Models.ClassInfo"></value>
        return this._classInfo$3;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.MethodInfo

Open.Testing.Models.MethodInfo = function Open_Testing_Models_MethodInfo(classInfo, name) {
    /// <summary>
    /// Represents a single test method.
    /// </summary>
    /// <param name="classInfo" type="Open.Testing.Models.ClassInfo">
    /// The test-class that this method is a member of.
    /// </param>
    /// <param name="name" type="String">
    /// The name of the method.
    /// </param>
    /// <field name="keyGetter" type="String" static="true">
    /// </field>
    /// <field name="keySetter" type="String" static="true">
    /// </field>
    /// <field name="keyField" type="String" static="true">
    /// </field>
    /// <field name="keyFunction" type="String" static="true">
    /// </field>
    /// <field name="_classInfo$1" type="Open.Testing.Models.ClassInfo">
    /// </field>
    /// <field name="_name$1" type="String">
    /// </field>
    /// <field name="_displayName$1" type="String">
    /// </field>
    /// <field name="_isSpecial$1" type="Boolean">
    /// </field>
    Open.Testing.Models.MethodInfo.initializeBase(this);
    this._classInfo$1 = classInfo;
    this._name$1 = name;
    this._displayName$1 = Open.Testing.Models.MethodInfo.formatName(name);
    this._isSpecial$1 = Open.Testing._methodHelper.isSpecial(this.get_name());
}
Open.Testing.Models.MethodInfo.isTestMethod = function Open_Testing_Models_MethodInfo$isTestMethod(item) {
    /// <summary>
    /// Determines whether the specified DictionaryEntry represents a valid test-method.
    /// </summary>
    /// <param name="item" type="DictionaryEntry">
    /// The Dictionaty item to examine.
    /// </param>
    /// <returns type="Boolean"></returns>
    var key = item.key;
    if (typeof(item.value) !== Open.Testing.Models.MethodInfo.keyFunction) {
        return false;
    }
    if (Open.Testing._methodHelper.isConstructor(key)) {
        return false;
    }
    if (Open.Testing._methodHelper.isSpecial(key)) {
        return false;
    }
    if (key.startsWith(Open.Testing.Models.MethodInfo.keyField)) {
        return false;
    }
    if (key.startsWith(Open.Testing.Models.MethodInfo.keyGetter)) {
        return false;
    }
    if (key.startsWith(Open.Testing.Models.MethodInfo.keySetter)) {
        return false;
    }
    return true;
}
Open.Testing.Models.MethodInfo.formatName = function Open_Testing_Models_MethodInfo$formatName(name) {
    /// <summary>
    /// Formats a name into a display name (replace underscores with spaces etc.).
    /// </summary>
    /// <param name="name" type="String">
    /// The name to format.
    /// </param>
    /// <returns type="String"></returns>
    name = name.replaceAll('__', ': ');
    name = name.replaceAll('_', ' ');
    name = Open.Core.Helper.get_string().toSentenceCase(name);
    return name;
}
Open.Testing.Models.MethodInfo.prototype = {
    _classInfo$1: null,
    _name$1: null,
    _displayName$1: null,
    _isSpecial$1: false,
    
    get_classInfo: function Open_Testing_Models_MethodInfo$get_classInfo() {
        /// <summary>
        /// Gets the test-class that this method is a member of.
        /// </summary>
        /// <value type="Open.Testing.Models.ClassInfo"></value>
        return this._classInfo$1;
    },
    
    get_name: function Open_Testing_Models_MethodInfo$get_name() {
        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        /// <value type="String"></value>
        return this._name$1;
    },
    
    get_displayName: function Open_Testing_Models_MethodInfo$get_displayName() {
        /// <summary>
        /// Gets the display version of the name.
        /// </summary>
        /// <value type="String"></value>
        return this._displayName$1;
    },
    
    get_isSpecial: function Open_Testing_Models_MethodInfo$get_isSpecial() {
        /// <summary>
        /// Gets or sets whether this method is one of the special Setup/Teardown methods.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isSpecial$1;
    },
    
    invoke: function Open_Testing_Models_MethodInfo$invoke() {
        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <returns type="Error"></returns>
        var instance = this.get_classInfo().get_instance();
        if (!this.get_isSpecial() && this.get_classInfo().get_testInitialize() != null) {
            this.get_classInfo().get_testInitialize().invoke();
        }
        var error = null;
        try {
            var func = Open.Core.Helper.get_reflection().getFunction(instance, this.get_name());
            if (func != null) {
                func.call(instance);
            }
        }
        catch (e) {
            error = e;
            Open.Core.Log.error(this.formatError(error));
        }
        if (!this.get_isSpecial() && this.get_classInfo().get_testCleanup() != null) {
            this.get_classInfo().get_testCleanup().invoke();
        }
        return error;
    },
    
    formatError: function Open_Testing_Models_MethodInfo$formatError(error) {
        /// <summary>
        /// Formats an error message.
        /// </summary>
        /// <param name="error" type="Error">
        /// The invoke error.
        /// </param>
        /// <returns type="String"></returns>
        var htmlList = new Open.Core.Controls.HtmlPrimitive.HtmlList(Open.Core.HtmlListType.unordered, Open.Testing.CssSelectors.classes.logIndentedList);
        htmlList.add(String.format('Message: \'{0}\'', error.message));
        htmlList.add('Method: ' + Open.Core.Helper.get_string().toCamelCase(this.get_name()));
        htmlList.add('Class: ' + this.get_classInfo().get_classType().get_fullName());
        htmlList.add('Package: ' + Open.Core.Html.toHyperlink(this.get_classInfo().get_packageInfo().get_loader().get_scriptUrl(), null, Open.Core.LinkTarget.blank));
        return String.format('<b>Exception</b> Failed while executing \'<b>{0}</b>\'.<br/>{1}', this.get_displayName(), htmlList.get_outerHtml());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.PackageInfo

Open.Testing.Models.PackageInfo = function Open_Testing_Models_PackageInfo(scriptUrl, initMethod) {
    /// <summary>
    /// Represents a package of tests from a single JavaScript file.
    /// </summary>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <field name="_singletons$1" type="Array" static="true">
    /// </field>
    /// <field name="_classes$1" type="Array">
    /// </field>
    /// <field name="_loader$1" type="Open.Testing.Models.PackageLoader">
    /// </field>
    /// <field name="_name$1" type="String">
    /// </field>
    this._classes$1 = [];
    Open.Testing.Models.PackageInfo.initializeBase(this);
    if (String.isNullOrEmpty(scriptUrl)) {
        throw new Error('A URL to the test-package script must be specified.');
    }
    if (String.isNullOrEmpty(initMethod)) {
        throw new Error('An entry point method must be specified.');
    }
    this._name$1 = Open.Testing.Models.PackageInfo._getName$1(scriptUrl);
    this._loader$1 = new Open.Testing.Models.PackageLoader(this, scriptUrl.toLowerCase(), initMethod);
}
Open.Testing.Models.PackageInfo.get_singletons = function Open_Testing_Models_PackageInfo$get_singletons() {
    /// <summary>
    /// Gets the collection of singleton TestPackageDef instances.
    /// </summary>
    /// <value type="Array"></value>
    return Open.Testing.Models.PackageInfo._singletons$1;
}
Open.Testing.Models.PackageInfo.singletonFromUrl = function Open_Testing_Models_PackageInfo$singletonFromUrl(scriptUrl, initMethod) {
    /// <summary>
    /// Retrieves (or creates) the singleton instance of the definition for the given package type.
    /// </summary>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <returns type="Open.Testing.Models.PackageInfo"></returns>
    var def = Type.safeCast(Open.Core.Helper.get_collection().first(Open.Testing.Models.PackageInfo._singletons$1, function(o) {
        return (o).get_id() === scriptUrl.toLowerCase();
    }), Open.Testing.Models.PackageInfo);
    if (def == null) {
        def = new Open.Testing.Models.PackageInfo(scriptUrl, initMethod);
        Open.Testing.Models.PackageInfo._singletons$1.add(def);
    }
    return def;
}
Open.Testing.Models.PackageInfo._getName$1 = function Open_Testing_Models_PackageInfo$_getName$1(scriptUrl) {
    /// <param name="scriptUrl" type="String">
    /// </param>
    /// <returns type="String"></returns>
    var s = Open.Core.Helper.get_string();
    scriptUrl = s.removeEnd(scriptUrl, '.js');
    scriptUrl = s.removeEnd(scriptUrl, '.debug');
    scriptUrl = s.stripPath(scriptUrl);
    if (String.isNullOrEmpty(scriptUrl.trim())) {
        scriptUrl = '<Untitled>'.htmlEncode();
    }
    return scriptUrl;
}
Open.Testing.Models.PackageInfo.prototype = {
    _loader$1: null,
    _name$1: null,
    
    get_id: function Open_Testing_Models_PackageInfo$get_id() {
        /// <summary>
        /// Gets the unique ID of the package.
        /// </summary>
        /// <value type="String"></value>
        return this.get_loader().get_scriptUrl();
    },
    
    get_name: function Open_Testing_Models_PackageInfo$get_name() {
        /// <summary>
        /// Gets the display name of the package.
        /// </summary>
        /// <value type="String"></value>
        return this._name$1;
    },
    
    get_loader: function Open_Testing_Models_PackageInfo$get_loader() {
        /// <summary>
        /// Gets the package loader.
        /// </summary>
        /// <value type="Open.Testing.Models.PackageLoader"></value>
        return this._loader$1;
    },
    
    get_isLoaded: function Open_Testing_Models_PackageInfo$get_isLoaded() {
        /// <summary>
        /// Gets or sets whether the package has been loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        return this.get_loader().get_isLoaded();
    },
    
    get_count: function Open_Testing_Models_PackageInfo$get_count() {
        /// <summary>
        /// Gets the number of test classes within the package.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._classes$1.length;
    },
    
    getEnumerator: function Open_Testing_Models_PackageInfo$getEnumerator() {
        /// <returns type="ss.IEnumerator"></returns>
        return this._classes$1.getEnumerator();
    },
    
    addClass: function Open_Testing_Models_PackageInfo$addClass(testClass) {
        /// <summary>
        /// Adds a test-class to the package.
        /// </summary>
        /// <param name="testClass" type="Type">
        /// The type of the test class.
        /// </param>
        if (testClass == null) {
            return;
        }
        if (this.contains(testClass)) {
            return;
        }
        this._classes$1.add(Open.Testing.Models.ClassInfo.getSingleton(testClass, this));
    },
    
    contains: function Open_Testing_Models_PackageInfo$contains(testClass) {
        /// <summary>
        /// Determines whether the test-class has already been added to the package.
        /// </summary>
        /// <param name="testClass" type="Type">
        /// The type of the test class.
        /// </param>
        /// <returns type="Boolean"></returns>
        return this.getTestClassDef(testClass) != null;
    },
    
    getTestClassDef: function Open_Testing_Models_PackageInfo$getTestClassDef(testClass) {
        /// <summary>
        /// Determines whether the test-class has already been added to the package.
        /// </summary>
        /// <param name="testClass" type="Type">
        /// The type of the test class.
        /// </param>
        /// <returns type="Open.Testing.Models.ClassInfo"></returns>
        if (testClass == null) {
            return null;
        }
        var typeName = testClass.get_fullName();
        var $enum1 = ss.IEnumerator.getEnumerator(this._classes$1);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            if (item.get_classType().get_fullName() === typeName) {
                return item;
            }
        }
        return null;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.ClassInfo

Open.Testing.Models.ClassInfo = function Open_Testing_Models_ClassInfo(classType, packageInfo) {
    /// <summary>
    /// Represents a class with tests.
    /// </summary>
    /// <param name="classType" type="Type">
    /// The type of the test class.
    /// </param>
    /// <param name="packageInfo" type="Open.Testing.Models.PackageInfo">
    /// The package the class belongs to.
    /// </param>
    /// <field name="_singletons$1" type="Object" static="true">
    /// </field>
    /// <field name="_classType$1" type="Type">
    /// </field>
    /// <field name="_packageInfo$1" type="Open.Testing.Models.PackageInfo">
    /// </field>
    /// <field name="_instance$1" type="Object">
    /// </field>
    /// <field name="_methods$1" type="Array">
    /// </field>
    /// <field name="_displayName$1" type="String">
    /// </field>
    /// <field name="_classInitialize$1" type="Open.Testing.Models.MethodInfo">
    /// </field>
    /// <field name="_classCleanup$1" type="Open.Testing.Models.MethodInfo">
    /// </field>
    /// <field name="_testInitialize$1" type="Open.Testing.Models.MethodInfo">
    /// </field>
    /// <field name="_testCleanup$1" type="Open.Testing.Models.MethodInfo">
    /// </field>
    this._methods$1 = [];
    Open.Testing.Models.ClassInfo.initializeBase(this);
    this._classType$1 = classType;
    this._packageInfo$1 = packageInfo;
    this._displayName$1 = Open.Testing.Models.ClassInfo._formatName$1(classType.get_name());
    this._getMethods$1();
}
Open.Testing.Models.ClassInfo.getSingleton = function Open_Testing_Models_ClassInfo$getSingleton(testClass, packageInfo) {
    /// <summary>
    /// Retrieves the singleton instance of the definition for the given package type.
    /// </summary>
    /// <param name="testClass" type="Type">
    /// The Type of the test class.
    /// </param>
    /// <param name="packageInfo" type="Open.Testing.Models.PackageInfo">
    /// The package the class belongs to.
    /// </param>
    /// <returns type="Open.Testing.Models.ClassInfo"></returns>
    if (Open.Testing.Models.ClassInfo._singletons$1 == null) {
        Open.Testing.Models.ClassInfo._singletons$1 = {};
    }
    var key = String.format('{0}::{1}', packageInfo.get_id(), testClass.get_fullName());
    if (Object.keyExists(Open.Testing.Models.ClassInfo._singletons$1, key)) {
        return Type.safeCast(Open.Testing.Models.ClassInfo._singletons$1[key], Open.Testing.Models.ClassInfo);
    }
    var def = new Open.Testing.Models.ClassInfo(testClass, packageInfo);
    Open.Testing.Models.ClassInfo._singletons$1[key] = def;
    return def;
}
Open.Testing.Models.ClassInfo._formatName$1 = function Open_Testing_Models_ClassInfo$_formatName$1(name) {
    /// <param name="name" type="String">
    /// </param>
    /// <returns type="String"></returns>
    name = Open.Testing.Models.MethodInfo.formatName(name);
    if (!name.endsWith('UnitTest')) {
        name = Open.Core.Helper.get_string().removeEnd(name, 'Test');
    }
    return name;
}
Open.Testing.Models.ClassInfo.prototype = {
    _classType$1: null,
    _packageInfo$1: null,
    _instance$1: null,
    _displayName$1: null,
    _classInitialize$1: null,
    _classCleanup$1: null,
    _testInitialize$1: null,
    _testCleanup$1: null,
    
    get_classType: function Open_Testing_Models_ClassInfo$get_classType() {
        /// <summary>
        /// Gets the type of the test class.
        /// </summary>
        /// <value type="Type"></value>
        return this._classType$1;
    },
    
    get_packageInfo: function Open_Testing_Models_ClassInfo$get_packageInfo() {
        /// <summary>
        /// Gets the package the class belongs to.
        /// </summary>
        /// <value type="Open.Testing.Models.PackageInfo"></value>
        return this._packageInfo$1;
    },
    
    get_displayName: function Open_Testing_Models_ClassInfo$get_displayName() {
        /// <summary>
        /// Gets the display version of the class name.
        /// </summary>
        /// <value type="String"></value>
        return this._displayName$1;
    },
    
    get_instance: function Open_Testing_Models_ClassInfo$get_instance() {
        /// <summary>
        /// Gets the test instance of the class.
        /// </summary>
        /// <value type="Object"></value>
        return this._instance$1 || (this._instance$1 = Type.safeCast(new this._classType$1(), Object));
    },
    
    get_count: function Open_Testing_Models_ClassInfo$get_count() {
        /// <summary>
        /// Gets the number of test-methods within the class.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._methods$1.length;
    },
    
    get_classInitialize: function Open_Testing_Models_ClassInfo$get_classInitialize() {
        /// <summary>
        /// Gets the 'ClassInitialize' special method (or null if one isn't declared).
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._classInitialize$1;
    },
    
    get_classCleanup: function Open_Testing_Models_ClassInfo$get_classCleanup() {
        /// <summary>
        /// Gets the 'ClassCleanup' special method (or null if one isn't declared).
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._classCleanup$1;
    },
    
    get_testInitialize: function Open_Testing_Models_ClassInfo$get_testInitialize() {
        /// <summary>
        /// Gets the 'TestInitialize' special method (or null if one isn't declared).
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._testInitialize$1;
    },
    
    get_testCleanup: function Open_Testing_Models_ClassInfo$get_testCleanup() {
        /// <summary>
        /// Gets the 'TestCleanup' special method (or null if one isn't declared).
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._testCleanup$1;
    },
    
    reset: function Open_Testing_Models_ClassInfo$reset() {
        /// <summary>
        /// Resets the test-class instance.
        /// </summary>
        this._instance$1 = null;
    },
    
    getEnumerator: function Open_Testing_Models_ClassInfo$getEnumerator() {
        /// <returns type="ss.IEnumerator"></returns>
        return this._methods$1.getEnumerator();
    },
    
    toString: function Open_Testing_Models_ClassInfo$toString() {
        /// <returns type="String"></returns>
        return String.format('[{0}:{1}]', Type.getInstanceType(this).get_name(), this.get_classType().get_name());
    },
    
    _getMethods$1: function Open_Testing_Models_ClassInfo$_getMethods$1() {
        if (this.get_instance() == null) {
            return;
        }
        var $dict1 = this.get_instance();
        for (var $key2 in $dict1) {
            var item = { key: $key2, value: $dict1[$key2] };
            if (Open.Testing.Models.MethodInfo.isTestMethod(item)) {
                this._methods$1.add(this._createMethod$1(item));
            }
            else {
                this._assignSpecialMethod$1(item);
            }
        }
    },
    
    _assignSpecialMethod$1: function Open_Testing_Models_ClassInfo$_assignSpecialMethod$1(item) {
        /// <param name="item" type="DictionaryEntry">
        /// </param>
        var key = item.key;
        if (!Open.Testing._methodHelper.isSpecial(key)) {
            return;
        }
        var method = this._createMethod$1(item);
        if (Open.Testing._methodHelper.isClassInitialize(key)) {
            this._classInitialize$1 = method;
        }
        else if (Open.Testing._methodHelper.isClassCleanup(key)) {
            this._classCleanup$1 = method;
        }
        else if (Open.Testing._methodHelper.isTestInitialize(key)) {
            this._testInitialize$1 = method;
        }
        else if (Open.Testing._methodHelper.isTestCleanup(key)) {
            this._testCleanup$1 = method;
        }
    },
    
    _createMethod$1: function Open_Testing_Models_ClassInfo$_createMethod$1(item) {
        /// <param name="item" type="DictionaryEntry">
        /// </param>
        /// <returns type="Open.Testing.Models.MethodInfo"></returns>
        return new Open.Testing.Models.MethodInfo(this, item.key);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.PackageListItem

Open.Testing.Models.PackageListItem = function Open_Testing_Models_PackageListItem(testPackage) {
    /// <summary>
    /// A list-item node for a TestPackage.
    /// </summary>
    /// <param name="testPackage" type="Open.Testing.Models.PackageInfo">
    /// The test-package this node represents.
    /// </param>
    /// <field name="_testPackage$3" type="Open.Testing.Models.PackageInfo">
    /// </field>
    Open.Testing.Models.PackageListItem.initializeBase(this);
    this._testPackage$3 = testPackage;
    this.set_text(testPackage.get_name());
}
Open.Testing.Models.PackageListItem.prototype = {
    _testPackage$3: null,
    
    get_testPackage: function Open_Testing_Models_PackageListItem$get_testPackage() {
        /// <summary>
        /// Gets the test-package this node represents.
        /// </summary>
        /// <value type="Open.Testing.Models.PackageInfo"></value>
        return this._testPackage$3;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.PackageLoader

Open.Testing.Models.PackageLoader = function Open_Testing_Models_PackageLoader(parent, scriptUrl, initMethod) {
    /// <summary>
    /// Handles loading a test-package and executing the entry point assembly.
    /// </summary>
    /// <param name="parent" type="Open.Testing.Models.PackageInfo">
    /// The test-package this object is loading.
    /// </param>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <field name="_parent$3" type="Open.Testing.Models.PackageInfo">
    /// </field>
    /// <field name="_scriptUrl$3" type="String">
    /// </field>
    /// <field name="_initMethod$3" type="String">
    /// </field>
    /// <field name="_isLoaded$3" type="Boolean">
    /// </field>
    /// <field name="_error$3" type="Error">
    /// </field>
    /// <field name="_isInitializing$3" type="Boolean">
    /// </field>
    /// <field name="_events$3" type="Open.Testing.TestHarnessEvents">
    /// </field>
    Open.Testing.Models.PackageLoader.initializeBase(this);
    this._parent$3 = parent;
    this._scriptUrl$3 = scriptUrl;
    this._initMethod$3 = initMethod;
    this._events$3 = this.get_common().get_events();
    this._events$3.add_testClassRegistered(ss.Delegate.create(this, this._onTestClassRegistered$3));
}
Open.Testing.Models.PackageLoader.prototype = {
    _parent$3: null,
    _scriptUrl$3: null,
    _initMethod$3: null,
    _isLoaded$3: false,
    _error$3: null,
    _isInitializing$3: false,
    _events$3: null,
    
    onDisposed: function Open_Testing_Models_PackageLoader$onDisposed() {
        this._events$3.remove_testClassRegistered(ss.Delegate.create(this, this._onTestClassRegistered$3));
        Open.Testing.Models.PackageLoader.callBaseMethod(this, 'onDisposed');
    },
    
    _onTestClassRegistered$3: function Open_Testing_Models_PackageLoader$_onTestClassRegistered$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Testing.Internal.TestClassEventArgs">
        /// </param>
        if (!this._isInitializing$3) {
            return;
        }
        this._parent$3.addClass(e.testClass);
    },
    
    get_scriptUrl: function Open_Testing_Models_PackageLoader$get_scriptUrl() {
        /// <summary>
        /// Gets the URL to the JavaScript file to load.
        /// </summary>
        /// <value type="String"></value>
        return this._scriptUrl$3;
    },
    
    get_initMethod: function Open_Testing_Models_PackageLoader$get_initMethod() {
        /// <summary>
        /// Gets the entry point method to invoke upon load completion.
        /// </summary>
        /// <value type="String"></value>
        return this._initMethod$3;
    },
    
    get_isLoaded: function Open_Testing_Models_PackageLoader$get_isLoaded() {
        /// <summary>
        /// Gets whether the script has been loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isLoaded$3;
    },
    
    get_error: function Open_Testing_Models_PackageLoader$get_error() {
        /// <summary>
        /// Gets the error (if any) that occured during the Load operation.
        /// </summary>
        /// <value type="Error"></value>
        return this._error$3;
    },
    
    get_succeeded: function Open_Testing_Models_PackageLoader$get_succeeded() {
        /// <summary>
        /// Gets or sets whether the load operation failed.
        /// </summary>
        /// <value type="Boolean"></value>
        return this.get_error() == null;
    },
    
    load: function Open_Testing_Models_PackageLoader$load(onComplete) {
        /// <summary>
        /// Downloads the test-package.
        /// </summary>
        /// <param name="onComplete" type="Action">
        /// Action to invoke upon completion.
        /// </param>
        if (this.get_isLoaded()) {
            Open.Core.Helper.invokeOrDefault(onComplete);
            return;
        }
        $.getScript(this._scriptUrl$3, ss.Delegate.create(this, function(data) {
            if (this.get_isDisposed()) {
                return;
            }
            try {
                this._isInitializing$3 = true;
                eval(this._initMethod$3 + '();');
            }
            catch (e) {
                Open.Core.Log.error(String.format('<b>Failed</b> to initialize the script-file at \'{0}\' with the entry method \'{1}()\'.<br/>Please ensure there aren\'t errors in any of the test-class constructors.<br/>Message: \'{2}\'', Open.Core.Html.toHyperlink(this._scriptUrl$3), this._initMethod$3, e.message));
                this._error$3 = e;
            }
            finally {
                this._isInitializing$3 = false;
            }
            this._isLoaded$3 = this.get_succeeded();
            Open.Core.Helper.invokeOrDefault(onComplete);
        }));
    }
}


Type.registerNamespace('Open.Testing.Views');

////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Views.ControlWrapperView

Open.Testing.Views.ControlWrapperView = function Open_Testing_Views_ControlWrapperView(divHost, control, htmlElement, displayMode, allViews) {
    /// <summary>
    /// Represents the container for a test-control.
    /// </summary>
    /// <param name="divHost" type="jQueryObject">
    /// The control host DIV.
    /// </param>
    /// <param name="control" type="Open.Core.IView">
    /// The logical IView control (null if not available).
    /// </param>
    /// <param name="htmlElement" type="jQueryObject">
    /// The control content (supplied by the test class. This is the control that is under test).
    /// </param>
    /// <param name="displayMode" type="Open.Testing.ControlDisplayMode">
    /// The sizing strategy to use for the control.
    /// </param>
    /// <param name="allViews" type="ss.IEnumerable">
    /// The Collection of all controls.
    /// </param>
    /// <field name="_fillMargin$3" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_divRoot$3" type="jQueryObject">
    /// </field>
    /// <field name="_htmlElement$3" type="jQueryObject">
    /// </field>
    /// <field name="_control$3" type="Open.Core.IView">
    /// </field>
    /// <field name="_displayMode$3" type="Open.Testing.ControlDisplayMode">
    /// </field>
    /// <field name="_allViews$3" type="ss.IEnumerable">
    /// </field>
    /// <field name="_index$3" type="Number" integer="true">
    /// </field>
    /// <field name="_events$3" type="Open.Testing.TestHarnessEvents">
    /// </field>
    /// <field name="_sizeDelay$3" type="Open.Core.DelayedAction">
    /// </field>
    Open.Testing.Views.ControlWrapperView.initializeBase(this, [ divHost ]);
    this._control$3 = control;
    this._htmlElement$3 = htmlElement;
    this._displayMode$3 = displayMode;
    this._allViews$3 = allViews;
    this._index$3 = divHost.children().length;
    this._events$3 = this.get_common().get_events();
    this._sizeDelay$3 = new Open.Core.DelayedAction(0.2, ss.Delegate.create(this, this.updateLayout));
    this._divRoot$3 = Open.Core.Html.createDiv();
    this._divRoot$3.css(Open.Core.Css.position, Open.Core.Css.absolute);
    this._divRoot$3.appendTo(divHost);
    htmlElement.css(Open.Core.Css.position, Open.Core.Css.absolute);
    htmlElement.appendTo(this._divRoot$3);
    this._events$3.add_controlHostSizeChanged(ss.Delegate.create(this, this._onHostResized$3));
    this.updateLayout();
}
Open.Testing.Views.ControlWrapperView.prototype = {
    _divRoot$3: null,
    _htmlElement$3: null,
    _control$3: null,
    _displayMode$3: 0,
    _allViews$3: null,
    _index$3: 0,
    _events$3: null,
    _sizeDelay$3: null,
    
    onDisposed: function Open_Testing_Views_ControlWrapperView$onDisposed() {
        /// <summary>
        /// Destructor.
        /// </summary>
        this._events$3.remove_controlHostSizeChanged(ss.Delegate.create(this, this._onHostResized$3));
        this._divRoot$3.remove();
        Open.Testing.Views.ControlWrapperView.callBaseMethod(this, 'onDisposed');
    },
    
    _onHostResized$3: function Open_Testing_Views_ControlWrapperView$_onHostResized$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        Open.Core.Css.setOverflow(this.get_container(), Open.Core.CssOverflow.hidden);
        this._sizeDelay$3.start();
    },
    
    get_displayMode: function Open_Testing_Views_ControlWrapperView$get_displayMode() {
        /// <summary>
        /// Gets or sets the items size mode.
        /// </summary>
        /// <value type="Open.Testing.ControlDisplayMode"></value>
        return this._displayMode$3;
    },
    set_displayMode: function Open_Testing_Views_ControlWrapperView$set_displayMode(value) {
        /// <summary>
        /// Gets or sets the items size mode.
        /// </summary>
        /// <value type="Open.Testing.ControlDisplayMode"></value>
        this._displayMode$3 = value;
        return value;
    },
    
    get_htmlElement: function Open_Testing_Views_ControlWrapperView$get_htmlElement() {
        /// <summary>
        /// Gets the HTML content.
        /// </summary>
        /// <value type="jQueryObject"></value>
        return this._htmlElement$3;
    },
    
    get_control: function Open_Testing_Views_ControlWrapperView$get_control() {
        /// <summary>
        /// Gets the logical control if available (otherwise null).
        /// </summary>
        /// <value type="Open.Core.IView"></value>
        return this._control$3;
    },
    
    updateLayout: function Open_Testing_Views_ControlWrapperView$updateLayout() {
        /// <summary>
        /// Updates the layout of the control.
        /// </summary>
        this._updateSize$3();
        this._updatePosition$3();
    },
    
    _updateSize$3: function Open_Testing_Views_ControlWrapperView$_updateSize$3() {
        switch (this._displayMode$3) {
            case Open.Testing.ControlDisplayMode.none:
            case Open.Testing.ControlDisplayMode.center:
                break;
            case Open.Testing.ControlDisplayMode.fill:
                this._setSizeWithPadding$3(0, 0);
                break;
            case Open.Testing.ControlDisplayMode.fillWithMargin:
                this._setSizeWithPadding$3(Open.Testing.Views.ControlWrapperView._fillMargin$3, Open.Testing.Views.ControlWrapperView._fillMargin$3);
                break;
            default:
                throw new Error(Open.Testing.ControlDisplayMode.toString(this._displayMode$3));
        }
        Open.Core.Css.setOverflow(this.get_container(), Open.Core.CssOverflow.auto);
    },
    
    _setSizeWithPadding$3: function Open_Testing_Views_ControlWrapperView$_setSizeWithPadding$3(xPadding, yPadding) {
        /// <param name="xPadding" type="Number" integer="true">
        /// </param>
        /// <param name="yPadding" type="Number" integer="true">
        /// </param>
        var width = (this.get_container().width() - (xPadding * 2));
        var height = (this.get_container().height() - (yPadding * 2));
        Open.Core.Css.setSize(this._htmlElement$3, width, height);
    },
    
    _updatePosition$3: function Open_Testing_Views_ControlWrapperView$_updatePosition$3() {
        if (this.get_displayMode() === Open.Testing.ControlDisplayMode.none) {
            return;
        }
        this._divRoot$3.css(Open.Core.Css.left, this._getLeft$3() + Open.Core.Css.px);
        var top = (this.get_container().children().length === 1) ? this._getTop$3() : this._getStackedTop$3();
        if (this._displayMode$3 !== Open.Testing.ControlDisplayMode.fill && top < Open.Testing.Views.ControlWrapperView._fillMargin$3) {
            top = Open.Testing.Views.ControlWrapperView._fillMargin$3;
        }
        this._divRoot$3.css(Open.Core.Css.top, top + Open.Core.Css.px);
    },
    
    _getLeft$3: function Open_Testing_Views_ControlWrapperView$_getLeft$3() {
        /// <returns type="Number" integer="true"></returns>
        switch (this._displayMode$3) {
            case Open.Testing.ControlDisplayMode.none:
                return -1;
            case Open.Testing.ControlDisplayMode.center:
                return (this.get_container().width() / 2) - (this._htmlElement$3.width() / 2);
            case Open.Testing.ControlDisplayMode.fill:
                return 0;
            case Open.Testing.ControlDisplayMode.fillWithMargin:
                return Open.Testing.Views.ControlWrapperView._fillMargin$3;
            default:
                throw new Error(Open.Testing.ControlDisplayMode.toString(this._displayMode$3));
        }
    },
    
    _getTop$3: function Open_Testing_Views_ControlWrapperView$_getTop$3() {
        /// <returns type="Number" integer="true"></returns>
        switch (this._displayMode$3) {
            case Open.Testing.ControlDisplayMode.none:
                return -1;
            case Open.Testing.ControlDisplayMode.center:
                return (this.get_container().height() / 2) - (this._htmlElement$3.height() / 2);
            case Open.Testing.ControlDisplayMode.fill:
                return 0;
            case Open.Testing.ControlDisplayMode.fillWithMargin:
                return Open.Testing.Views.ControlWrapperView._fillMargin$3;
            default:
                throw new Error(Open.Testing.ControlDisplayMode.toString(this._displayMode$3));
        }
    },
    
    _getStackedTop$3: function Open_Testing_Views_ControlWrapperView$_getStackedTop$3() {
        /// <returns type="Number" integer="true"></returns>
        return this._getOffsetHeight$3() + ((this._index$3 + 1) * Open.Testing.Views.ControlWrapperView._fillMargin$3);
    },
    
    _getOffsetHeight$3: function Open_Testing_Views_ControlWrapperView$_getOffsetHeight$3() {
        /// <returns type="Number" integer="true"></returns>
        var height = 0;
        var $enum1 = ss.IEnumerator.getEnumerator(this._allViews$3);
        while ($enum1.moveNext()) {
            var wrapper = $enum1.get_current();
            if (wrapper === this) {
                break;
            }
            height += wrapper.get_htmlElement().height();
        }
        return height;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Views.ShellView

Open.Testing.Views.ShellView = function Open_Testing_Views_ShellView(container) {
    /// <summary>
    /// The root view of the application shell.
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// The containing DIV.
    /// </param>
    /// <field name="_sidebar$3" type="Open.Testing.Views.SidebarView">
    /// </field>
    Open.Testing.Views.ShellView.initializeBase(this, [ container ]);
    this._sidebar$3 = new Open.Testing.Views.SidebarView($(Open.Testing.CssSelectors.sidebar));
}
Open.Testing.Views.ShellView.prototype = {
    _sidebar$3: null,
    
    get_sidebar: function Open_Testing_Views_ShellView$get_sidebar() {
        /// <summary>
        /// Gets the view for the SideBar.
        /// </summary>
        /// <value type="Open.Testing.Views.SidebarView"></value>
        return this._sidebar$3;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Views.SidebarView

Open.Testing.Views.SidebarView = function Open_Testing_Views_SidebarView(container) {
    /// <summary>
    /// The view for the side-bar.
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// The containing DIV.
    /// </param>
    /// <field name="slideDuration" type="Number" static="true">
    /// </field>
    /// <field name="propIsTestListVisible" type="String" static="true">
    /// </field>
    /// <field name="_rootList$3" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_backController$3" type="Open.Core.Lists.ListTreeBackController">
    /// </field>
    /// <field name="_methodList$3" type="Open.Testing.Views.MethodListView">
    /// </field>
    /// <field name="_methodListHeightController$3" type="Open.Testing.Controllers._methodListHeightController">
    /// </field>
    Open.Testing.Views.SidebarView.initializeBase(this, [ container ]);
    this._rootList$3 = new Open.Core.Lists.ListTreeView($(Open.Testing.CssSelectors.sidebarRootList));
    this._rootList$3.set_slideDuration(Open.Testing.Views.SidebarView.slideDuration);
    this._methodList$3 = new Open.Testing.Views.MethodListView($(Open.Testing.CssSelectors.methodList));
    this._backController$3 = new Open.Core.Lists.ListTreeBackController(this._rootList$3, $(Open.Testing.CssSelectors.sidebarToolbar), $(Open.Testing.CssSelectors.backMask));
    this._methodListHeightController$3 = new Open.Testing.Controllers._methodListHeightController(this);
    Open.Core.GlobalEvents.add_windowResizeComplete(ss.Delegate.create(this, this._onSizeChanged$3));
    Open.Core.GlobalEvents.add_panelResizeComplete(ss.Delegate.create(this, this._onSizeChanged$3));
    this.updateLayout();
}
Open.Testing.Views.SidebarView.prototype = {
    _rootList$3: null,
    _backController$3: null,
    _methodList$3: null,
    _methodListHeightController$3: null,
    
    onDisposed: function Open_Testing_Views_SidebarView$onDisposed() {
        Open.Core.GlobalEvents.remove_windowResizeComplete(ss.Delegate.create(this, this._onSizeChanged$3));
        Open.Core.GlobalEvents.remove_panelResizeComplete(ss.Delegate.create(this, this._onSizeChanged$3));
        this._rootList$3.dispose();
        this._backController$3.dispose();
        this._methodListHeightController$3.dispose();
        Open.Testing.Views.SidebarView.callBaseMethod(this, 'onDisposed');
    },
    
    _onSizeChanged$3: function Open_Testing_Views_SidebarView$_onSizeChanged$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.updateLayout();
    },
    
    get_rootList: function Open_Testing_Views_SidebarView$get_rootList() {
        /// <summary>
        /// Gets the main List-Tree view.
        /// </summary>
        /// <value type="Open.Core.Lists.ListTreeView"></value>
        return this._rootList$3;
    },
    
    get_methodList: function Open_Testing_Views_SidebarView$get_methodList() {
        /// <summary>
        /// Gets the Test-List view.
        /// </summary>
        /// <value type="Open.Testing.Views.MethodListView"></value>
        return this._methodList$3;
    },
    
    get_isMethodListVisible: function Open_Testing_Views_SidebarView$get_isMethodListVisible() {
        /// <summary>
        /// Gets or sets whether the TestList panel is visible.
        /// </summary>
        /// <value type="Boolean"></value>
        return this.get(Open.Testing.Views.SidebarView.propIsTestListVisible, false);
    },
    set_isMethodListVisible: function Open_Testing_Views_SidebarView$set_isMethodListVisible(value) {
        /// <summary>
        /// Gets or sets whether the TestList panel is visible.
        /// </summary>
        /// <value type="Boolean"></value>
        this.set(Open.Testing.Views.SidebarView.propIsTestListVisible, value, false);
        return value;
    },
    
    updateLayout: function Open_Testing_Views_SidebarView$updateLayout() {
        /// <summary>
        /// Refreshes the visual state.
        /// </summary>
        this._methodListHeightController$3.updateLayout();
        this._syncRootListHeight$3();
    },
    
    _syncRootListHeight$3: function Open_Testing_Views_SidebarView$_syncRootListHeight$3() {
        this.get_rootList().get_container().css(Open.Core.Css.bottom, this.get_methodList().get_container().height() + Open.Core.Css.px);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Views.MethodListView

Open.Testing.Views.MethodListView = function Open_Testing_Views_MethodListView(container) {
    /// <summary>
    /// The list of tests.
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// The containing div.
    /// </param>
    /// <field name="__runClick$3" type="EventHandler">
    /// </field>
    /// <field name="propClassInfo" type="String" static="true">
    /// </field>
    /// <field name="propSelectedMethod" type="String" static="true">
    /// </field>
    /// <field name="_listView$3" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_rootNode$3" type="Open.Core.Lists.ListItem">
    /// </field>
    /// <field name="_events$3" type="Open.Testing.TestHarnessEvents">
    /// </field>
    /// <field name="_btnRun$3" type="Open.Core.Controls.Buttons.SystemButton">
    /// </field>
    Open.Testing.Views.MethodListView.initializeBase(this, [ container ]);
    this._events$3 = this.get_common().get_events();
    this._listView$3 = new Open.Core.Lists.ListTreeView($(Open.Testing.CssSelectors.methodListContent));
    this._listView$3.set_slideDuration(Open.Testing.Views.SidebarView.slideDuration);
    this._rootNode$3 = new Open.Core.Lists.ListItem();
    this._listView$3.set_rootNode(this._rootNode$3);
    this._btnRun$3 = new Open.Core.Controls.Buttons.SystemButton();
    this._btnRun$3.set_fontSize('8pt');
    this._btnRun$3.set_padding('3px 8px');
    this._btnRun$3.insert(Open.Testing.CssSelectors.methodListRunButton, Open.Core.InsertMode.replace);
    this._btnRun$3.add_click(ss.Delegate.create(this, function() {
        this._fireRunClick$3();
    }));
}
Open.Testing.Views.MethodListView.prototype = {
    
    add_runClick: function Open_Testing_Views_MethodListView$add_runClick(value) {
        /// <summary>
        /// Fires when the 'Run' button is clicked.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__runClick$3 = ss.Delegate.combine(this.__runClick$3, value);
    },
    remove_runClick: function Open_Testing_Views_MethodListView$remove_runClick(value) {
        /// <summary>
        /// Fires when the 'Run' button is clicked.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__runClick$3 = ss.Delegate.remove(this.__runClick$3, value);
    },
    
    __runClick$3: null,
    
    _fireRunClick$3: function Open_Testing_Views_MethodListView$_fireRunClick$3() {
        if (this.__runClick$3 != null) {
            this.__runClick$3.invoke(this, new ss.EventArgs());
        }
    },
    
    _listView$3: null,
    _rootNode$3: null,
    _events$3: null,
    _btnRun$3: null,
    
    _onItemClick$3: function Open_Testing_Views_MethodListView$_onItemClick$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.set_selectedMethod((sender).get_method());
        this._events$3._fireMethodClicked(this.get_selectedMethod());
    },
    
    get_classInfo: function Open_Testing_Views_MethodListView$get_classInfo() {
        /// <summary>
        /// Gets or sets the test class the view is listing methods for.
        /// </summary>
        /// <value type="Open.Testing.Models.ClassInfo"></value>
        return this.get(Open.Testing.Views.MethodListView.propClassInfo, null);
    },
    set_classInfo: function Open_Testing_Views_MethodListView$set_classInfo(value) {
        /// <summary>
        /// Gets or sets the test class the view is listing methods for.
        /// </summary>
        /// <value type="Open.Testing.Models.ClassInfo"></value>
        if (this.set(Open.Testing.Views.MethodListView.propClassInfo, value, null)) {
            this._populateList$3(value);
        }
        return value;
    },
    
    get_selectedMethod: function Open_Testing_Views_MethodListView$get_selectedMethod() {
        /// <summary>
        /// Gets or sets the currently selected method..
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this.get(Open.Testing.Views.MethodListView.propSelectedMethod, null);
    },
    set_selectedMethod: function Open_Testing_Views_MethodListView$set_selectedMethod(value) {
        /// <summary>
        /// Gets or sets the currently selected method..
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        this.set(Open.Testing.Views.MethodListView.propSelectedMethod, value, null);
        return value;
    },
    
    get_offsetHeight: function Open_Testing_Views_MethodListView$get_offsetHeight() {
        /// <summary>
        /// Gets the offset height of the items within the list and the title bar.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._listView$3.get_contentHeight() + this.get__divTitleBar$3().height() + 1;
    },
    
    get__divTitleBar$3: function Open_Testing_Views_MethodListView$get__divTitleBar$3() {
        /// <value type="jQueryObject"></value>
        return this.get_container().children(Open.Testing.CssSelectors.methodListTitlebar);
    },
    
    updateLayout: function Open_Testing_Views_MethodListView$updateLayout() {
        /// <summary>
        /// Updates the visual state of the control.
        /// </summary>
        this._listView$3.updateLayout();
    },
    
    _populateList$3: function Open_Testing_Views_MethodListView$_populateList$3(classInfo) {
        /// <param name="classInfo" type="Open.Testing.Models.ClassInfo">
        /// </param>
        this._clearChildren$3();
        if (classInfo == null) {
            return;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(classInfo);
        while ($enum1.moveNext()) {
            var method = $enum1.get_current();
            this._rootNode$3.addChild(this._createListItem$3(method));
        }
    },
    
    _createListItem$3: function Open_Testing_Views_MethodListView$_createListItem$3(methodInfo) {
        /// <param name="methodInfo" type="Open.Testing.Models.MethodInfo">
        /// </param>
        /// <returns type="Open.Testing.Models.MethodListItem"></returns>
        var item = new Open.Testing.Models.MethodListItem(methodInfo);
        item.add_click(ss.Delegate.create(this, this._onItemClick$3));
        return item;
    },
    
    _clearChildren$3: function Open_Testing_Views_MethodListView$_clearChildren$3() {
        var $enum1 = ss.IEnumerator.getEnumerator(this._rootNode$3.get_children());
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            child.remove_click(ss.Delegate.create(this, this._onItemClick$3));
        }
        this._rootNode$3.clearChildren();
        this.set_selectedMethod(null);
    }
}


Open.Testing.TestHarnessViewBase.registerClass('Open.Testing.TestHarnessViewBase', Open.Core.ViewBase);
Open.Testing.TestHarnessControllerBase.registerClass('Open.Testing.TestHarnessControllerBase', Open.Core.ControllerBase);
Open.Testing.CssSelectors.registerClass('Open.Testing.CssSelectors');
Open.Testing.Classes.registerClass('Open.Testing.Classes');
Open.Testing.Elements.registerClass('Open.Testing.Elements');
Open.Testing.TestHarnessEvents.registerClass('Open.Testing.TestHarnessEvents', null, Open.Testing.Internal.ITestHarnessEvents);
Open.Testing.MethodEventArgs.registerClass('Open.Testing.MethodEventArgs');
Open.Testing.ClassEventArgs.registerClass('Open.Testing.ClassEventArgs');
Open.Testing.Common.registerClass('Open.Testing.Common');
Open.Testing._methodHelper.registerClass('Open.Testing._methodHelper');
Open.Testing.Application.registerClass('Open.Testing.Application');
Open.Testing.Automation.ClassTestRunner.registerClass('Open.Testing.Automation.ClassTestRunner');
Open.Testing.Automation._executedTest.registerClass('Open.Testing.Automation._executedTest');
Open.Testing.Controllers.ClassController.registerClass('Open.Testing.Controllers.ClassController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.HostCanvasController.registerClass('Open.Testing.Controllers.HostCanvasController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.MethodListController.registerClass('Open.Testing.Controllers.MethodListController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers._methodListHeightController.registerClass('Open.Testing.Controllers._methodListHeightController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.PanelResizeController.registerClass('Open.Testing.Controllers.PanelResizeController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.SidebarController.registerClass('Open.Testing.Controllers.SidebarController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.MyNode.registerClass('Open.Testing.Controllers.MyNode', Open.Core.Lists.ListItem);
Open.Testing.Controllers.PackageController.registerClass('Open.Testing.Controllers.PackageController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Models.MethodListItem.registerClass('Open.Testing.Models.MethodListItem', Open.Core.Lists.ListItem);
Open.Testing.Models.ClassListItem.registerClass('Open.Testing.Models.ClassListItem', Open.Core.Lists.ListItem);
Open.Testing.Models.MethodInfo.registerClass('Open.Testing.Models.MethodInfo', Open.Core.ModelBase);
Open.Testing.Models.PackageInfo.registerClass('Open.Testing.Models.PackageInfo', Open.Core.ModelBase, ss.IEnumerable);
Open.Testing.Models.ClassInfo.registerClass('Open.Testing.Models.ClassInfo', Open.Core.ModelBase, ss.IEnumerable);
Open.Testing.Models.PackageListItem.registerClass('Open.Testing.Models.PackageListItem', Open.Core.Lists.ListItem);
Open.Testing.Models.PackageLoader.registerClass('Open.Testing.Models.PackageLoader', Open.Testing.TestHarnessControllerBase, ss.IDisposable);
Open.Testing.Views.ControlWrapperView.registerClass('Open.Testing.Views.ControlWrapperView', Open.Testing.TestHarnessViewBase);
Open.Testing.Views.ShellView.registerClass('Open.Testing.Views.ShellView', Open.Testing.TestHarnessViewBase);
Open.Testing.Views.SidebarView.registerClass('Open.Testing.Views.SidebarView', Open.Testing.TestHarnessViewBase);
Open.Testing.Views.MethodListView.registerClass('Open.Testing.Views.MethodListView', Open.Testing.TestHarnessViewBase);
Open.Testing.CssSelectors.classes = new Open.Testing.Classes();
Open.Testing.CssSelectors.root = '#testHarness';
Open.Testing.CssSelectors.sidebar = '#testHarnessSidebar';
Open.Testing.CssSelectors.sidebarContent = '#testHarnessSidebar .th-content';
Open.Testing.CssSelectors.sidebarRootList = '#testHarnessSidebar .th-sidebarRootList';
Open.Testing.CssSelectors.sidebarToolbar = '#testHarnessSidebar div.th-toolbar';
Open.Testing.CssSelectors.backMask = '#testHarnessSidebar img.th-backMask';
Open.Testing.CssSelectors.methodList = '#testHarnessSidebar .th-testList';
Open.Testing.CssSelectors.methodListTitlebar = '#testHarnessSidebar .th-testList-tb';
Open.Testing.CssSelectors.methodListContent = '#testHarnessSidebar .th-testList-content';
Open.Testing.CssSelectors.methodListRunButton = '#testHarnessSidebar .th-testList button.runTests';
Open.Testing.CssSelectors.main = '#testHarness .th-main';
Open.Testing.CssSelectors.mainContent = '#testHarness .th-main .th-content';
Open.Testing.CssSelectors.controlHost = '#testHarness .th-main .th-content .th-controlHost';
Open.Testing.CssSelectors.logContainer = '#testHarnessLog';
Open.Testing.CssSelectors.logTitlebar = '#testHarnessLog .th-log-tb';
Open.Testing.CssSelectors.log = '#testHarnessLog .c-log';
Open.Testing.Elements.root = 'testHarness';
Open.Testing.Elements.outputLog = 'testHarnessLog';
Open.Testing._methodHelper.keyConstructor = 'constructor';
Open.Testing._methodHelper.keyClassInitialize = 'classInitialize';
Open.Testing._methodHelper.keyClassCleanup = 'classCleanup';
Open.Testing._methodHelper.keyTestInitialize = 'testInitialize';
Open.Testing._methodHelper.keyTestCleanup = 'testCleanup';
Open.Testing.Application._shell = null;
Open.Testing.Application._container = null;
Open.Testing.Application._resizeController = null;
Open.Testing.Application._sidebarController = null;
Open.Testing.Application._hostCanvasController = null;
Open.Testing.Controllers.PanelResizeController._sidebarMinWidth$3 = 200;
Open.Testing.Controllers.PanelResizeController._sidebarMaxWidthMargin$3 = 80;
Open.Testing.Controllers.PanelResizeController._outputLogMaxHeightMargin$3 = 80;
Open.Testing.Controllers.PackageController.propSelectedClass = 'SelectedClass';
Open.Testing.Controllers.PackageController._loadTimeout$3 = 10;
Open.Testing.Models.MethodInfo.keyGetter = 'get_';
Open.Testing.Models.MethodInfo.keySetter = 'set_';
Open.Testing.Models.MethodInfo.keyField = '_';
Open.Testing.Models.MethodInfo.keyFunction = 'function';
Open.Testing.Models.PackageInfo._singletons$1 = [];
Open.Testing.Models.ClassInfo._singletons$1 = null;
Open.Testing.Views.ControlWrapperView._fillMargin$3 = 30;
Open.Testing.Views.SidebarView.slideDuration = 0.2;
Open.Testing.Views.SidebarView.propIsTestListVisible = 'IsTestListVisible';
Open.Testing.Views.MethodListView.propClassInfo = 'ClassInfo';
Open.Testing.Views.MethodListView.propSelectedMethod = 'SelectedMethod';

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('TestHarness.Script3', ['Open.Core.Script','Open.Core.Views','Open.Core.Lists'], executeScript);
})();
