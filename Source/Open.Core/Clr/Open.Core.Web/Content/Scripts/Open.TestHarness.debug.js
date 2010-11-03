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
    /// <field name="_common$3" type="Open.Testing.Common">
    /// </field>
    Open.Testing.TestHarnessViewBase.initializeBase(this, [ container ]);
}
Open.Testing.TestHarnessViewBase.prototype = {
    _common$3: null,
    
    get_common: function Open_Testing_TestHarnessViewBase$get_common() {
        /// <summary>
        /// Gets the common global properties (via the DI Container).
        /// </summary>
        /// <value type="Open.Testing.Common"></value>
        return this._common$3 || (this._common$3 = Open.Testing.Common.getFromContainer());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.TestHarnessControllerBase

Open.Testing.TestHarnessControllerBase = function Open_Testing_TestHarnessControllerBase() {
    /// <summary>
    /// The base class for controllers within the TestHarness.
    /// </summary>
    /// <field name="_common$3" type="Open.Testing.Common">
    /// </field>
    Open.Testing.TestHarnessControllerBase.initializeBase(this);
}
Open.Testing.TestHarnessControllerBase.prototype = {
    _common$3: null,
    
    get_common: function Open_Testing_TestHarnessControllerBase$get_common() {
        /// <summary>
        /// Gets the common global properties (via the DI Container).
        /// </summary>
        /// <value type="Open.Testing.Common"></value>
        return this._common$3 || (this._common$3 = Open.Testing.Common.getFromContainer());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.CssSelectors

Open.Testing.CssSelectors = function Open_Testing_CssSelectors() {
    /// <summary>
    /// Constants for common CSS selectors.
    /// </summary>
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
    /// <field name="methodListButtons" type="String" static="true">
    /// </field>
    /// <field name="methodListRunButton" type="String" static="true">
    /// </field>
    /// <field name="methodListRefreshButton" type="String" static="true">
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
    /// <field name="logControl" type="String" static="true">
    /// </field>
    /// <field name="logClearButton" type="String" static="true">
    /// </field>
    /// <field name="addPackageInnerSlide" type="String" static="true">
    /// </field>
    /// <field name="addPackageTxtScript" type="String" static="true">
    /// </field>
    /// <field name="addPackageTxtMethod" type="String" static="true">
    /// </field>
    /// <field name="addPackageButtons" type="String" static="true">
    /// </field>
    /// <field name="addPackageBtnAdd" type="String" static="true">
    /// </field>
    /// <field name="addPackageBtnCancel" type="String" static="true">
    /// </field>
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
// Open.Testing.StringLibrary

Open.Testing.StringLibrary = function Open_Testing_StringLibrary() {
    /// <summary>
    /// String resources.
    /// </summary>
    /// <field name="add" type="String" static="true">
    /// </field>
    /// <field name="cancel" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing._buttonHelper

Open.Testing._buttonHelper = function Open_Testing__buttonHelper() {
}
Open.Testing._buttonHelper.insertButton = function Open_Testing__buttonHelper$insertButton(type, replaceSeletor, size, onClick) {
    /// <param name="type" type="Open.Core.Controls.Buttons.ImageButtons">
    /// </param>
    /// <param name="replaceSeletor" type="String">
    /// </param>
    /// <param name="size" type="Number" integer="true">
    /// </param>
    /// <param name="onClick" type="EventHandler">
    /// </param>
    var button = Open.Core.Controls.Buttons.ImageButtonFactory.create(type);
    button.set_backgroundHighlighting(true);
    button.setSize(size, size);
    var view = Type.safeCast(button.createView(), Open.Core.Controls.Buttons.ButtonView);
    button.add_click(onClick);
    view.insert(replaceSeletor, Open.Core.InsertMode.replace);
    view.updateLayout();
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
    /// <field name="__changeLogHeight" type="Open.Testing._changeHeightEventHandler">
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
    
    add__controlHostSizeChanged: function Open_Testing_TestHarnessEvents$add__controlHostSizeChanged(value) {
        /// <summary>
        /// Fires when the width of the control-host changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__controlHostSizeChanged = ss.Delegate.combine(this.__controlHostSizeChanged, value);
    },
    remove__controlHostSizeChanged: function Open_Testing_TestHarnessEvents$remove__controlHostSizeChanged(value) {
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
    },
    
    add__changeLogHeight: function Open_Testing_TestHarnessEvents$add__changeLogHeight(value) {
        /// <summary>
        /// Fires when the log-height is to be changed.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__changeLogHeight = ss.Delegate.combine(this.__changeLogHeight, value);
    },
    remove__changeLogHeight: function Open_Testing_TestHarnessEvents$remove__changeLogHeight(value) {
        /// <summary>
        /// Fires when the log-height is to be changed.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__changeLogHeight = ss.Delegate.remove(this.__changeLogHeight, value);
    },
    
    __changeLogHeight: null,
    
    _fireChangeLogHeight: function Open_Testing_TestHarnessEvents$_fireChangeLogHeight(height) {
        /// <param name="height" type="Number" integer="true">
        /// </param>
        if (this.__changeLogHeight != null) {
            this.__changeLogHeight.invoke(this, new Open.Testing._changeHeightEventArgs(height));
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
// Open.Testing._changeHeightEventArgs

Open.Testing._changeHeightEventArgs = function Open_Testing__changeHeightEventArgs(height) {
    /// <param name="height" type="Number" integer="true">
    /// </param>
    /// <field name="height" type="Number" integer="true">
    /// </field>
    this.height = height;
}
Open.Testing._changeHeightEventArgs.prototype = {
    height: 0
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
    },
    
    get_buttons: function Open_Testing_Common$get_buttons() {
        /// <value type="Open.Testing.Models.CommonButtons"></value>
        return Type.safeCast(this.get_container().getSingleton(Open.Testing.Models.CommonButtons), Open.Testing.Models.CommonButtons);
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
    /// <field name="publicDomain" type="String" static="true">
    /// </field>
    /// <field name="_shell" type="Open.Testing.Views.ShellView" static="true">
    /// </field>
    /// <field name="_container" type="Open.Core.DiContainer" static="true">
    /// </field>
    /// <field name="_sidebarController" type="Open.Testing.Controllers.SidebarController" static="true">
    /// </field>
    /// <field name="_controlHostController" type="Open.Testing.Controllers.ControlHostController" static="true">
    /// </field>
    /// <field name="_logController" type="Open.Testing.Controllers.LogController" static="true">
    /// </field>
    /// <field name="_addPackageController" type="Open.Testing.Controllers.AddPackageController" static="true">
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
    Open.Testing.Application.get_container().registerSingleton(Open.Testing.Models.CommonButtons, new Open.Testing.Models.CommonButtons());
    Open.Testing.Application._shell = new Open.Testing.Views.ShellView($(Open.Testing.CssSelectors.root));
    Open.Testing.Application.get_container().registerSingleton(Open.Testing.Views.ShellView, Open.Testing.Application._shell);
    Open.Testing.Application.get_container().registerSingleton(Open.Testing.Controllers.IPanelResizeController, new Open.Testing.Controllers.PanelResizeController());
    Open.Testing.Application._sidebarController = new Open.Testing.Controllers.SidebarController();
    Open.Testing.Application._controlHostController = new Open.Testing.Controllers.ControlHostController();
    Open.Testing.Application._logController = new Open.Testing.Controllers.LogController();
    Open.Testing.Application._addPackageController = new Open.Testing.Controllers.AddPackageController();
    Open.Testing.Application._preloadImages();
    Open.Testing.Application._addPackage('/Content/Scripts/Open.Core.Test.debug.js', 'Open.Core.Test.Application.main');
    Open.Testing.Application._addPackage('/Content/Scripts/Quest.Rogue.Test.debug.js', 'Quest.Rogue.Test.Application.main');
    Open.Testing.Application._addPackage('/Content/Scripts/Quest.OnDemand.Test.debug.js', 'Quest.OnDemand.Test.Application.main');
    Open.Testing.Application._addPackage('/Content/Scripts/Quest.EventViewer.Test.debug.js', 'Quest.EventViewer.Test.Application.main');
}
Open.Testing.Application._preloadImages = function Open_Testing_Application$_preloadImages() {
    var icon = Open.Core.Helper.get_icon();
    Open.Core.Helpers.ImagePreloader.preload('/Open.Assets/Icons/Api/Property.png');
    Open.Core.Helpers.ImagePreloader.preload('/Open.Assets/Icons/Api/Class.png');
    Open.Core.Helpers.ImagePreloader.preload(icon.path(Open.Core.Icons.silkAccept));
    Open.Core.Helpers.ImagePreloader.preload(icon.path(Open.Core.Icons.silkExclamation));
    Open.Core.Helpers.ImagePreloader.preload(icon.path(Open.Core.Icons.silkError));
}
Open.Testing.Application._addPackage = function Open_Testing_Application$_addPackage(scriptUrl, initMethod) {
    /// <param name="scriptUrl" type="String">
    /// </param>
    /// <param name="initMethod" type="String">
    /// </param>
    var testHarnessPackage = Open.Testing.Models.PackageInfo.singletonFromUrl(initMethod, scriptUrl);
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
        var summary = String.format('Test run for class <b>{0}</b>', this._classInfo.get_displayName());
        var list = log.writeListSeverity(summary, (hasFailures) ? Open.Core.LogSeverity.error : Open.Core.LogSeverity.success);
        list.add(String.format('Successes: {0} ({1}%)', successes, this._toPercent(successes)));
        list.add(String.format('Failures: {0} ({1}%)', failures, this._toPercent(failures)));
        if (hasFailures) {
            log.lineBreak();
        }
        var $enum1 = ss.IEnumerator.getEnumerator(this._results);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            if (item.error == null) {
                continue;
            }
            item.method.logError(item.error);
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
// Open.Testing.Controllers.IPanelResizeController

Open.Testing.Controllers.IPanelResizeController = function() { 
    /// <summary>
    /// Handles resizing of panels within the shell.
    /// </summary>
};
Open.Testing.Controllers.IPanelResizeController.prototype = {
    get_logResizer : null,
    get_sideBarResizer : null,
    updateLayout : null,
    save : null
}
Open.Testing.Controllers.IPanelResizeController.registerInterface('Open.Testing.Controllers.IPanelResizeController');


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.AddPackageController

Open.Testing.Controllers.AddPackageController = function Open_Testing_Controllers_AddPackageController() {
    /// <summary>
    /// Constroller for adding new test packages.
    /// </summary>
    /// <field name="_minHeight$4" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_events$4" type="Open.Testing.TestHarnessEvents">
    /// </field>
    /// <field name="_shell$4" type="Open.Testing.Views.ShellView">
    /// </field>
    /// <field name="_addButton$4" type="Open.Core.IButton">
    /// </field>
    /// <field name="_isShowing$4" type="Boolean">
    /// </field>
    Open.Testing.Controllers.AddPackageController.initializeBase(this);
    this._events$4 = this.get_common().get_events();
    this._shell$4 = this.get_common().get_shell();
    this._addButton$4 = this.get_common().get_buttons().get_addPackage();
    this._addButton$4.add_click(ss.Delegate.create(this, this._onAddPackageClick$4));
    Open.Testing.Views.AddPackageView.add_showing(ss.Delegate.create(this, this._onViewShowing$4));
    Open.Testing.Views.AddPackageView.add_hidden(ss.Delegate.create(this, this._onViewHidden$4));
    this._events$4.add_clearControls(ss.Delegate.create(this, this._onViewHidden$4));
}
Open.Testing.Controllers.AddPackageController.prototype = {
    _events$4: null,
    _shell$4: null,
    _addButton$4: null,
    _isShowing$4: false,
    
    onDisposed: function Open_Testing_Controllers_AddPackageController$onDisposed() {
        /// <summary>
        /// Destroy.
        /// </summary>
        this._addButton$4.remove_click(ss.Delegate.create(this, this._onAddPackageClick$4));
        Open.Testing.Views.AddPackageView.remove_showing(ss.Delegate.create(this, this._onViewShowing$4));
        Open.Testing.Views.AddPackageView.remove_hidden(ss.Delegate.create(this, this._onViewHidden$4));
        Open.Testing.Controllers.AddPackageController.callBaseMethod(this, 'onDisposed');
    },
    
    _onAddPackageClick$4: function Open_Testing_Controllers_AddPackageController$_onAddPackageClick$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._addButton$4.set_isEnabled(false);
        this.show();
    },
    
    _onViewShowing$4: function Open_Testing_Controllers_AddPackageController$_onViewShowing$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._isShowing$4 = true;
        this._syncButtonState$4();
    },
    
    _onViewHidden$4: function Open_Testing_Controllers_AddPackageController$_onViewHidden$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (!this._isShowing$4) {
            return;
        }
        this._isShowing$4 = false;
        this._syncButtonState$4();
    },
    
    show: function Open_Testing_Controllers_AddPackageController$show() {
        /// <summary>
        /// Shows the 'Add Package' screen.
        /// </summary>
        if (this._shell$4.get_controlHost().get_height() < Open.Testing.Controllers.AddPackageController._minHeight$4) {
            this._events$4._fireChangeLogHeight(this._shell$4.get_controlHost().get_divMain().height() - Open.Testing.Controllers.AddPackageController._minHeight$4);
        }
        Open.Testing.Views.AddPackageView.addToTestHarness();
    },
    
    _syncButtonState$4: function Open_Testing_Controllers_AddPackageController$_syncButtonState$4() {
        this._addButton$4.set_isEnabled(!this._isShowing$4);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.ClassController

Open.Testing.Controllers.ClassController = function Open_Testing_Controllers_ClassController(classInfo) {
    /// <summary>
    /// Controlls the selected test-class.
    /// </summary>
    /// <param name="classInfo" type="Open.Testing.Models.ClassInfo">
    /// The test-class that is under control.
    /// </param>
    /// <field name="_classInfo$4" type="Open.Testing.Models.ClassInfo">
    /// </field>
    /// <field name="_sidebarView$4" type="Open.Testing.Views.SidebarView">
    /// </field>
    /// <field name="_events$4" type="Open.Testing.TestHarnessEvents">
    /// </field>
    Open.Testing.Controllers.ClassController.initializeBase(this);
    this._classInfo$4 = classInfo;
    this._sidebarView$4 = this.get_common().get_shell().get_sidebar();
    this._events$4 = this.get_common().get_events();
    this._events$4.add_methodClicked(ss.Delegate.create(this, this._onMethodClicked$4));
    this._sidebarView$4.get_methodList().add_runClick(ss.Delegate.create(this, this._onRunClick$4));
    this._sidebarView$4.get_methodList().add_refreshClick(ss.Delegate.create(this, this._onRefreshClick$4));
    this.reset();
}
Open.Testing.Controllers.ClassController.prototype = {
    _classInfo$4: null,
    _sidebarView$4: null,
    _events$4: null,
    
    onDisposed: function Open_Testing_Controllers_ClassController$onDisposed() {
        this._events$4.remove_methodClicked(ss.Delegate.create(this, this._onMethodClicked$4));
        this._sidebarView$4.get_methodList().remove_runClick(ss.Delegate.create(this, this._onRunClick$4));
        this._sidebarView$4.get_methodList().remove_refreshClick(ss.Delegate.create(this, this._onRefreshClick$4));
        if (this._classInfo$4.get_classCleanup() != null) {
            this._classInfo$4.get_classCleanup().invoke();
        }
        Open.Testing.TestHarness.reset();
        Open.Testing.Controllers.ClassController.callBaseMethod(this, 'onDisposed');
    },
    
    _onMethodClicked$4: function Open_Testing_Controllers_ClassController$_onMethodClicked$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Testing.MethodEventArgs">
        /// </param>
        this.invokeSelectedMethod();
    },
    
    _onRunClick$4: function Open_Testing_Controllers_ClassController$_onRunClick$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.runAll();
    },
    
    _onRefreshClick$4: function Open_Testing_Controllers_ClassController$_onRefreshClick$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (Open.Core.Keyboard.get_isCtrlPressed()) {
            Open.Core.Log.clear();
        }
        Open.Core.Log.success(String.format('Reload: <b>{0}</b>', this._classInfo$4.get_displayName()));
        this.reset();
    },
    
    get__selectedMethod$4: function Open_Testing_Controllers_ClassController$get__selectedMethod$4() {
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._sidebarView$4.get_methodList().get_selectedMethod();
    },
    
    reset: function Open_Testing_Controllers_ClassController$reset() {
        /// <summary>
        /// Initializes the current class.
        /// </summary>
        Open.Testing.TestHarness.reset();
        if (this._classInfo$4.get_classInitialize() != null) {
            this._classInfo$4.get_classInitialize().invoke();
        }
        Open.Core.Log.newSection();
    },
    
    invokeSelectedMethod: function Open_Testing_Controllers_ClassController$invokeSelectedMethod() {
        /// <summary>
        /// Invokes the currently selected method (including pre/post TestInitialize and TestCleanup methods).
        /// </summary>
        /// <returns type="Boolean"></returns>
        var method = this.get__selectedMethod$4();
        if (method == null) {
            return false;
        }
        Open.Core.Log.newSection();
        method.invoke();
        return true;
    },
    
    runAll: function Open_Testing_Controllers_ClassController$runAll() {
        /// <summary>
        /// Runs all tests within the class.
        /// </summary>
        var runner = new Open.Testing.Automation.ClassTestRunner(this._classInfo$4);
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
// Open.Testing.Controllers.ControlHostController

Open.Testing.Controllers.ControlHostController = function Open_Testing_Controllers_ControlHostController() {
    /// <summary>
    /// Controls the 'Host Canvas' panel where controls under test are displayed.
    /// </summary>
    /// <field name="_divControlHost$4" type="jQueryObject">
    /// </field>
    /// <field name="_views$4" type="Array">
    /// </field>
    /// <field name="_events$4" type="Open.Testing.TestHarnessEvents">
    /// </field>
    /// <field name="_canScroll$4" type="Boolean">
    /// </field>
    this._views$4 = [];
    Open.Testing.Controllers.ControlHostController.initializeBase(this);
    this._divControlHost$4 = $(Open.Testing.CssSelectors.controlHost);
    this._events$4 = this.get_common().get_events();
    this._events$4.add_controlAdded(ss.Delegate.create(this, this._onControlAdded$4));
    this._events$4.add_clearControls(ss.Delegate.create(this, this._onClearControls$4));
    this._events$4.add_updateLayout(ss.Delegate.create(this, this._onUpdateLayout$4));
    this._updateLayout$4();
}
Open.Testing.Controllers.ControlHostController.prototype = {
    _divControlHost$4: null,
    _events$4: null,
    _canScroll$4: true,
    
    onDisposed: function Open_Testing_Controllers_ControlHostController$onDisposed() {
        this.clear();
        this._events$4.remove_controlAdded(ss.Delegate.create(this, this._onControlAdded$4));
        this._events$4.remove_clearControls(ss.Delegate.create(this, this._onClearControls$4));
        this._events$4.remove_updateLayout(ss.Delegate.create(this, this._onUpdateLayout$4));
        Open.Testing.Controllers.ControlHostController.callBaseMethod(this, 'onDisposed');
    },
    
    _onControlAdded$4: function Open_Testing_Controllers_ControlHostController$_onControlAdded$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Testing.Internal.TestControlEventArgs">
        /// </param>
        this._addView$4(e.control, e.htmlElement, e.controlDisplayMode);
    },
    
    _onClearControls$4: function Open_Testing_Controllers_ControlHostController$_onClearControls$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.clear();
    },
    
    _onControlSizeChanged$4: function Open_Testing_Controllers_ControlHostController$_onControlSizeChanged$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._updateLayout$4();
    },
    
    _onUpdateLayout$4: function Open_Testing_Controllers_ControlHostController$_onUpdateLayout$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._updateLayout$4();
    },
    
    get_canScroll: function Open_Testing_Controllers_ControlHostController$get_canScroll() {
        /// <summary>
        /// Gets or sets whether the control host canvas can scroll.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._canScroll$4;
    },
    set_canScroll: function Open_Testing_Controllers_ControlHostController$set_canScroll(value) {
        /// <summary>
        /// Gets or sets whether the control host canvas can scroll.
        /// </summary>
        /// <value type="Boolean"></value>
        if (value === this.get_canScroll()) {
            return;
        }
        this._canScroll$4 = value;
        Open.Core.Css.setOverflow(this._divControlHost$4, (value) ? Open.Core.CssOverflow.auto : Open.Core.CssOverflow.hidden);
        return value;
    },
    
    clear: function Open_Testing_Controllers_ControlHostController$clear() {
        /// <summary>
        /// Clears all views.
        /// </summary>
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$4);
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            if (view.get_control() != null) {
                view.get_control().remove_sizeChanged(ss.Delegate.create(this, this._onControlSizeChanged$4));
            }
        }
        Open.Core.Helper.get_collection().disposeAndClear(this._views$4);
    },
    
    _addView$4: function Open_Testing_Controllers_ControlHostController$_addView$4(control, htmlElement, controlDisplayMode) {
        /// <param name="control" type="Open.Core.IView">
        /// </param>
        /// <param name="htmlElement" type="jQueryObject">
        /// </param>
        /// <param name="controlDisplayMode" type="Open.Testing.ControlDisplayMode">
        /// </param>
        var view = new Open.Testing.Views.ControlWrapperView(this._divControlHost$4, control, htmlElement, controlDisplayMode, this._views$4);
        this._views$4.add(view);
        if (control != null) {
            control.add_sizeChanged(ss.Delegate.create(this, this._onControlSizeChanged$4));
        }
        if (this._views$4.length > 1) {
            var $enum1 = ss.IEnumerator.getEnumerator(this._views$4);
            while ($enum1.moveNext()) {
                var item = $enum1.get_current();
                if (item.get_displayMode() === Open.Testing.ControlDisplayMode.fill) {
                    item.set_displayMode(Open.Testing.ControlDisplayMode.fillWithMargin);
                }
            }
        }
        this._updateLayout$4();
    },
    
    _updateLayout$4: function Open_Testing_Controllers_ControlHostController$_updateLayout$4() {
        this.set_canScroll(Open.Testing.TestHarness.get_canScroll());
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$4);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            item.updateLayout();
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.LogController

Open.Testing.Controllers.LogController = function Open_Testing_Controllers_LogController() {
    /// <summary>
    /// Constroller for the Log panel.
    /// </summary>
    /// <field name="_events$4" type="Open.Testing.TestHarnessEvents">
    /// </field>
    /// <field name="_divLogContainer$4" type="jQueryObject">
    /// </field>
    /// <field name="_slideDuration$4" type="Number" static="true">
    /// </field>
    /// <field name="_panelResizer$4" type="Open.Testing.Controllers.IPanelResizeController">
    /// </field>
    Open.Testing.Controllers.LogController.initializeBase(this);
    this._events$4 = this.get_common().get_events();
    this._divLogContainer$4 = $(Open.Testing.CssSelectors.logContainer);
    this._panelResizer$4 = Type.safeCast(this.get_common().get_container().getSingleton(Open.Testing.Controllers.IPanelResizeController), Open.Testing.Controllers.IPanelResizeController);
    Open.Core.Log.set_view(new Open.Core.Controls.LogView($(Open.Testing.CssSelectors.logControl).first()));
    this._events$4.add__changeLogHeight(ss.Delegate.create(this, this._onChangeLogHeight$4));
    this.get_common().get_shell().get_logContainer().add_clearClick(ss.Delegate.create(this, function() {
        Open.Core.Log.clear();
    }));
}
Open.Testing.Controllers.LogController.prototype = {
    _events$4: null,
    _divLogContainer$4: null,
    _panelResizer$4: null,
    
    onDisposed: function Open_Testing_Controllers_LogController$onDisposed() {
        /// <summary>
        /// Destroy.
        /// </summary>
        this._events$4.remove__changeLogHeight(ss.Delegate.create(this, this._onChangeLogHeight$4));
        Open.Testing.Controllers.LogController.callBaseMethod(this, 'onDisposed');
    },
    
    _onChangeLogHeight$4: function Open_Testing_Controllers_LogController$_onChangeLogHeight$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Testing._changeHeightEventArgs">
        /// </param>
        this._animateHeight$4(e.height, ss.Delegate.create(this, function() {
            this._events$4.fireUpdateLayout();
            this._panelResizer$4.updateLayout();
            this._panelResizer$4.save();
        }));
    },
    
    _animateHeight$4: function Open_Testing_Controllers_LogController$_animateHeight$4(height, onComplete) {
        /// <param name="height" type="Number" integer="true">
        /// </param>
        /// <param name="onComplete" type="Action">
        /// </param>
        var properties = {};
        properties[Open.Core.Css.height] = this._heightWithinRange$4(height) + Open.Core.Css.px;
        this._divLogContainer$4.animate(properties, Open.Core.Helper.get_time().toMsecs(Open.Testing.Controllers.LogController._slideDuration$4), 'swing', ss.Delegate.create(this, function() {
            Open.Core.Helper.invoke(onComplete);
        }));
    },
    
    _heightWithinRange$4: function Open_Testing_Controllers_LogController$_heightWithinRange$4(height) {
        /// <param name="height" type="Number" integer="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        if (height < Open.Testing.Controllers.PanelResizeController.logMinHeight) {
            height = Open.Testing.Controllers.PanelResizeController.logMinHeight;
        }
        if (height > this._panelResizer$4.get_logResizer().get_maxHeight()) {
            height = this._panelResizer$4.get_logResizer().get_maxHeight();
        }
        return height;
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
    /// <field name="_sidebarView$4" type="Open.Testing.Views.SidebarView">
    /// </field>
    /// <field name="_methodList$4" type="Open.Testing.Views.MethodListView">
    /// </field>
    /// <field name="_divSidebarContent$4" type="jQueryObject">
    /// </field>
    /// <field name="_events$4" type="Open.Testing.TestHarnessEvents">
    /// </field>
    Open.Testing.Controllers._methodListHeightController.initializeBase(this);
    this._sidebarView$4 = sidebarView;
    this._methodList$4 = sidebarView.get_methodList();
    this._divSidebarContent$4 = sidebarView.get_container().children(Open.Testing.CssSelectors.sidebarContent);
    this._events$4 = this.get_common().get_events();
    this._events$4.add_selectedClassChanged(ss.Delegate.create(this, this._onSelectedClassChanged$4));
    this._hideList$4(null);
}
Open.Testing.Controllers._methodListHeightController.prototype = {
    _sidebarView$4: null,
    _methodList$4: null,
    _divSidebarContent$4: null,
    _events$4: null,
    
    onDisposed: function Open_Testing_Controllers__methodListHeightController$onDisposed() {
        this._events$4.remove_selectedClassChanged(ss.Delegate.create(this, this._onSelectedClassChanged$4));
        Open.Testing.Controllers._methodListHeightController.callBaseMethod(this, 'onDisposed');
    },
    
    _onSelectedClassChanged$4: function Open_Testing_Controllers__methodListHeightController$_onSelectedClassChanged$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Testing.ClassEventArgs">
        /// </param>
        if (e.classInfo != null) {
            this._showList$4(null);
        }
        else {
            this._hideList$4(null);
        }
    },
    
    _showList$4: function Open_Testing_Controllers__methodListHeightController$_showList$4(onComplete) {
        /// <summary>
        /// Reveals the method-list.
        /// </summary>
        /// <param name="onComplete" type="Action">
        /// The action to invoke when complete
        /// </param>
        this._sidebarView$4.set_isMethodListVisible(true);
        var height = this._getHeight$4();
        this._animateHeights$4(height, onComplete);
    },
    
    _hideList$4: function Open_Testing_Controllers__methodListHeightController$_hideList$4(onComplete) {
        /// <summary>
        /// Hides the method-list.
        /// </summary>
        /// <param name="onComplete" type="Action">
        /// The action to invoke when complete
        /// </param>
        this._sidebarView$4.set_isMethodListVisible(false);
        this._animateHeights$4(0, onComplete);
    },
    
    updateLayout: function Open_Testing_Controllers__methodListHeightController$updateLayout() {
        /// <summary>
        /// Updates the height of the method-list (if it's currently showing).
        /// </summary>
        if (!this._sidebarView$4.get_isMethodListVisible()) {
            return;
        }
        this._methodList$4.get_container().css(Open.Core.Css.height, this._getHeight$4() + Open.Core.Css.px);
    },
    
    _animateHeights$4: function Open_Testing_Controllers__methodListHeightController$_animateHeights$4(methodListHeight, onComplete) {
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
            Open.Core.Css.setDisplay(this._methodList$4.get_container(), true);
        }
        this._methodList$4.updateLayout();
        this._animate$4(isShowing, this._methodList$4.get_container(), methodListProps, null);
        this._animate$4(isShowing, this._sidebarView$4.get_rootList().get_container(), rootListProps, onComplete);
    },
    
    _animate$4: function Open_Testing_Controllers__methodListHeightController$_animate$4(isShowing, div, properties, onComplete) {
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
                Open.Core.Css.setDisplay(this._methodList$4.get_container(), false);
            }
            Open.Core.Helper.invoke(onComplete);
        }));
    },
    
    _getHeight$4: function Open_Testing_Controllers__methodListHeightController$_getHeight$4() {
        /// <returns type="Number" integer="true"></returns>
        var divList = this._methodList$4.get_container();
        var originalVisibility = Open.Core.Css.isVisible(divList);
        Open.Core.Css.setDisplay(divList, true);
        var listHeight = this._methodList$4.get_offsetHeight();
        Open.Core.Css.setDisplay(divList, originalVisibility);
        var maxHeight = (this._divSidebarContent$4.height() * 0.66);
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
    /// <field name="_sidebarMinWidth$4" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_sidebarMaxWidthMargin$4" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="logMinHeight" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="logMaxHeightMargin" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_sideBarResizer$4" type="Open.Core.UI.HorizontalPanelResizer">
    /// </field>
    /// <field name="_logResizer$4" type="Open.Core.UI.VerticalPanelResizer">
    /// </field>
    /// <field name="_events$4" type="Open.Testing.TestHarnessEvents">
    /// </field>
    Open.Testing.Controllers.PanelResizeController.initializeBase(this);
    this._events$4 = this.get_common().get_events();
    this._sideBarResizer$4 = new Open.Core.UI.HorizontalPanelResizer(Open.Testing.CssSelectors.sidebar, 'TH_SB');
    this._sideBarResizer$4.add_resized(ss.Delegate.create(this, function() {
        this._syncMainPanelWidth$4();
    }));
    this._sideBarResizer$4.set_minWidth(Open.Testing.Controllers.PanelResizeController._sidebarMinWidth$4);
    this._sideBarResizer$4.set_maxWidthMargin(Open.Testing.Controllers.PanelResizeController._sidebarMaxWidthMargin$4);
    Open.Testing.Controllers.PanelResizeController._initializeResizer$4(this._sideBarResizer$4);
    this._logResizer$4 = new Open.Core.UI.VerticalPanelResizer(Open.Core.Css.toId(Open.Testing.Elements.outputLog), 'TH_OL');
    this._logResizer$4.add_resized(ss.Delegate.create(this, function() {
        this._syncControlHostHeight$4();
    }));
    this._logResizer$4.set_minHeight(Open.Core.Html.height(Open.Testing.CssSelectors.logTitlebar));
    this._logResizer$4.set_maxHeightMargin(Open.Testing.Controllers.PanelResizeController.logMaxHeightMargin);
    Open.Testing.Controllers.PanelResizeController._initializeResizer$4(this._logResizer$4);
    Open.Core.GlobalEvents.add_windowResize(ss.Delegate.create(this, function() {
        this._syncControlHostHeight$4();
    }));
    this.updateLayout();
}
Open.Testing.Controllers.PanelResizeController._initializeResizer$4 = function Open_Testing_Controllers_PanelResizeController$_initializeResizer$4(resizer) {
    /// <param name="resizer" type="Open.Core.UI.PanelResizerBase">
    /// </param>
    resizer.set_rootContainerId(Open.Testing.Elements.root);
    resizer.initialize();
}
Open.Testing.Controllers.PanelResizeController.prototype = {
    _sideBarResizer$4: null,
    _logResizer$4: null,
    _events$4: null,
    
    get_logResizer: function Open_Testing_Controllers_PanelResizeController$get_logResizer() {
        /// <summary>
        /// Gets the Log resizer.
        /// </summary>
        /// <value type="Open.Core.UI.VerticalPanelResizer"></value>
        return this._logResizer$4;
    },
    
    get_sideBarResizer: function Open_Testing_Controllers_PanelResizeController$get_sideBarResizer() {
        /// <summary>
        /// Gets the SideBar resizer.
        /// </summary>
        /// <value type="Open.Core.UI.HorizontalPanelResizer"></value>
        return this._sideBarResizer$4;
    },
    
    updateLayout: function Open_Testing_Controllers_PanelResizeController$updateLayout() {
        /// <summary>
        /// Updates the layout of the panels.
        /// </summary>
        this._syncMainPanelWidth$4();
        this._syncControlHostHeight$4();
    },
    
    save: function Open_Testing_Controllers_PanelResizeController$save() {
        /// <summary>
        /// Saves panel sizes to storage.
        /// </summary>
        this._sideBarResizer$4.save();
        this._logResizer$4.save();
    },
    
    _syncMainPanelWidth$4: function Open_Testing_Controllers_PanelResizeController$_syncMainPanelWidth$4() {
        $(Open.Testing.CssSelectors.main).css(Open.Core.Css.left, (Open.Core.Html.width(Open.Testing.CssSelectors.sidebar) + 1) + Open.Core.Css.px);
        this._events$4._fireControlHostSizeChanged();
    },
    
    _syncControlHostHeight$4: function Open_Testing_Controllers_PanelResizeController$_syncControlHostHeight$4() {
        var height = Open.Core.Html.height(Open.Testing.CssSelectors.mainContent) - Open.Core.Html.height(Open.Testing.CssSelectors.logContainer);
        $(Open.Testing.CssSelectors.controlHost).css(Open.Core.Css.height, (height - 1) + Open.Core.Css.px);
        this._events$4._fireControlHostSizeChanged();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.SidebarController

Open.Testing.Controllers.SidebarController = function Open_Testing_Controllers_SidebarController() {
    /// <summary>
    /// Controller for the side-bar.
    /// </summary>
    /// <field name="_packageControllers$4" type="Array">
    /// </field>
    /// <field name="_view$4" type="Open.Testing.Views.SidebarView">
    /// </field>
    /// <field name="_methodListController$4" type="Open.Testing.Controllers.MethodListController">
    /// </field>
    /// <field name="_listRoot$4" type="Open.Core.Lists.ListItem">
    /// </field>
    this._packageControllers$4 = [];
    Open.Testing.Controllers.SidebarController.initializeBase(this);
    this._listRoot$4 = new Open.Core.Lists.ListItem();
    this._view$4 = this.get_common().get_shell().get_sidebar();
    this._view$4.get_rootList().set_rootNode(this._listRoot$4);
    this._methodListController$4 = new Open.Testing.Controllers.MethodListController();
    this._listRoot$4.addChild(new Open.Testing.Models.CustomListItem(Open.Testing.Models.CustomListItemType.addPackage));
    this._listRoot$4.add_childSelectionChanged(Open.Testing.Controllers.SidebarController._onChildSelectionChanged$4);
}
Open.Testing.Controllers.SidebarController._onChildSelectionChanged$4 = function Open_Testing_Controllers_SidebarController$_onChildSelectionChanged$4(sender, e) {
    /// <param name="sender" type="Object">
    /// </param>
    /// <param name="e" type="ss.EventArgs">
    /// </param>
    Open.Testing.TestHarness.reset();
}
Open.Testing.Controllers.SidebarController.prototype = {
    _view$4: null,
    _methodListController$4: null,
    _listRoot$4: null,
    
    onDisposed: function Open_Testing_Controllers_SidebarController$onDisposed() {
        this._listRoot$4.remove_childSelectionChanged(Open.Testing.Controllers.SidebarController._onChildSelectionChanged$4);
        this._view$4.dispose();
        this._methodListController$4.dispose();
        var $enum1 = ss.IEnumerator.getEnumerator(this._packageControllers$4);
        while ($enum1.moveNext()) {
            var controller = $enum1.get_current();
            controller.dispose();
        }
        Open.Testing.Controllers.SidebarController.callBaseMethod(this, 'onDisposed');
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
        this._listRoot$4.insertChild((this._listRoot$4.get_childCount() === 0) ? 0 : this._listRoot$4.get_childCount() - 1, node);
        var controller = new Open.Testing.Controllers.PackageController(node);
        this._packageControllers$4.add(controller);
        controller.add_loaded(ss.Delegate.create(this, function() {
            this._view$4.get_rootList().set_selectedParent(controller.get_rootNode());
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
        var controller = this._getController$4(testPackage);
        if (controller == null) {
            return;
        }
        this._view$4.get_rootList().get_rootNode().removeChild(controller.get_rootNode());
        Open.Core.Log.info(String.format('Test package unloaded: {0}', Open.Core.Html.toHyperlink(testPackage.get_id(), null, Open.Core.LinkTarget.blank)));
        Open.Core.Log.lineBreak();
    },
    
    _getController$4: function Open_Testing_Controllers_SidebarController$_getController$4(testPackage) {
        /// <param name="testPackage" type="Open.Testing.Models.PackageInfo">
        /// </param>
        /// <returns type="Open.Testing.Controllers.PackageController"></returns>
        return Type.safeCast(Open.Core.Helper.get_collection().first(this._packageControllers$4, ss.Delegate.create(this, function(o) {
            return (o).get_testPackage() === testPackage;
        })), Open.Testing.Controllers.PackageController);
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
    /// <field name="__loaded$4" type="EventHandler">
    /// </field>
    /// <field name="propSelectedClass" type="String" static="true">
    /// </field>
    /// <field name="_loadTimeout$4" type="Number" static="true">
    /// </field>
    /// <field name="_rootNode$4" type="Open.Testing.Models.PackageListItem">
    /// </field>
    /// <field name="_sidebarView$4" type="Open.Testing.Views.SidebarView">
    /// </field>
    /// <field name="_events$4" type="Open.Testing.TestHarnessEvents">
    /// </field>
    /// <field name="_selectedClassController$4" type="Open.Testing.Controllers.ClassController">
    /// </field>
    Open.Testing.Controllers.PackageController.initializeBase(this);
    this._rootNode$4 = rootNode;
    this._sidebarView$4 = this.get_common().get_shell().get_sidebar();
    this._events$4 = this.get_common().get_events();
    rootNode.add_selectionChanged(ss.Delegate.create(this, this._onSelectionChanged$4));
    rootNode.add_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$4));
}
Open.Testing.Controllers.PackageController.prototype = {
    
    add_loaded: function Open_Testing_Controllers_PackageController$add_loaded(value) {
        /// <summary>
        /// Fires when the package has laoded.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__loaded$4 = ss.Delegate.combine(this.__loaded$4, value);
    },
    remove_loaded: function Open_Testing_Controllers_PackageController$remove_loaded(value) {
        /// <summary>
        /// Fires when the package has laoded.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__loaded$4 = ss.Delegate.remove(this.__loaded$4, value);
    },
    
    __loaded$4: null,
    
    _fireLoaded$4: function Open_Testing_Controllers_PackageController$_fireLoaded$4() {
        if (this.__loaded$4 != null) {
            this.__loaded$4.invoke(this, new ss.EventArgs());
        }
    },
    
    _rootNode$4: null,
    _sidebarView$4: null,
    _events$4: null,
    _selectedClassController$4: null,
    
    onDisposed: function Open_Testing_Controllers_PackageController$onDisposed() {
        this._rootNode$4.remove_selectionChanged(ss.Delegate.create(this, this._onSelectionChanged$4));
        this._rootNode$4.remove_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$4));
        if (this._selectedClassController$4 != null) {
            this._selectedClassController$4.dispose();
        }
        Open.Testing.Controllers.PackageController.callBaseMethod(this, 'onDisposed');
    },
    
    _onSelectionChanged$4: function Open_Testing_Controllers_PackageController$_onSelectionChanged$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (this.get_rootNode().get_isSelected()) {
            this._download$4();
        }
    },
    
    _onChildSelectionChanged$4: function Open_Testing_Controllers_PackageController$_onChildSelectionChanged$4(sender, e) {
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
        return this._sidebarView$4.get_rootList().get_selectedParent() === this.get_rootNode();
    },
    
    get_testPackage: function Open_Testing_Controllers_PackageController$get_testPackage() {
        /// <summary>
        /// Gets the test-package that is under control.
        /// </summary>
        /// <value type="Open.Testing.Models.PackageInfo"></value>
        return this._rootNode$4.get_testPackage();
    },
    
    get_rootNode: function Open_Testing_Controllers_PackageController$get_rootNode() {
        /// <summary>
        /// Gets the root list-item node.
        /// </summary>
        /// <value type="Open.Testing.Models.PackageListItem"></value>
        return this._rootNode$4;
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
            if (this._selectedClassController$4 != null) {
                this._selectedClassController$4.dispose();
            }
            if (value != null) {
                this._selectedClassController$4 = new Open.Testing.Controllers.ClassController(value);
            }
            this._sidebarView$4.get_methodList().set_classInfo(value);
            this._events$4._fireSelectedClassChanged(value);
        }
        return value;
    },
    
    _download$4: function Open_Testing_Controllers_PackageController$_download$4() {
        var loader = this.get_testPackage().get_loader();
        if (loader.get_isDownloaded()) {
            return;
        }
        loader.initialize(ss.Delegate.create(this, function() {
            if (!loader.get_hasError()) {
                this._addChildNodes$4();
                this._fireLoaded$4();
            }
        }));
    },
    
    _addChildNodes$4: function Open_Testing_Controllers_PackageController$_addChildNodes$4() {
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
// Open.Testing.Models.CustomListItemType

Open.Testing.Models.CustomListItemType = function() { 
    /// <summary>
    /// Flag representing the custom view to produce.
    /// </summary>
    /// <field name="addPackage" type="Number" integer="true" static="true">
    /// Produces the view that gives the option to add a new test-package to the sidebar.
    /// </field>
};
Open.Testing.Models.CustomListItemType.prototype = {
    addPackage: 0
}
Open.Testing.Models.CustomListItemType.registerEnum('Open.Testing.Models.CustomListItemType', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.PackageLoader

Open.Testing.Models.PackageLoader = function Open_Testing_Models_PackageLoader(parent, initMethod, scriptUrl) {
    /// <summary>
    /// Handles loading a test-package and executing the entry point assembly.
    /// </summary>
    /// <param name="parent" type="Open.Testing.Models.PackageInfo">
    /// The test-package this object is loading.
    /// </param>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <field name="_parent$4" type="Open.Testing.Models.PackageInfo">
    /// </field>
    /// <field name="_events$4" type="Open.Testing.TestHarnessEvents">
    /// </field>
    Open.Testing.Models.PackageLoader.initializeBase(this, [ initMethod, scriptUrl ]);
    this._parent$4 = parent;
    this._events$4 = Open.Testing.Common.getFromContainer().get_events();
    this._events$4.add_testClassRegistered(ss.Delegate.create(this, this._onTestClassRegistered$4));
    this.set_logErrors(false);
}
Open.Testing.Models.PackageLoader.prototype = {
    _parent$4: null,
    _events$4: null,
    
    onDisposed: function Open_Testing_Models_PackageLoader$onDisposed() {
        this._events$4.remove_testClassRegistered(ss.Delegate.create(this, this._onTestClassRegistered$4));
        Open.Testing.Models.PackageLoader.callBaseMethod(this, 'onDisposed');
    },
    
    _onTestClassRegistered$4: function Open_Testing_Models_PackageLoader$_onTestClassRegistered$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Testing.Internal.TestClassEventArgs">
        /// </param>
        if (!this.get_isLoading()) {
            return;
        }
        this._parent$4.addClass(e.testClass);
    },
    
    initialize: function Open_Testing_Models_PackageLoader$initialize(onComplete) {
        /// <param name="onComplete" type="Action">
        /// </param>
        this._events$4.add_testClassRegistered(ss.Delegate.create(this, this._onTestClassRegistered$4));
        var link = Open.Core.Html.toHyperlink(this.get_scriptUrls(), null, Open.Core.LinkTarget.blank);
        Open.Core.Log.info(String.format('Downloading test-package: {0} ...', link));
        Open.Testing.Models.PackageLoader.callBaseMethod(this, 'initialize', [ ss.Delegate.create(this, function() {
            if (!this.get_hasError()) {
                Open.Core.Log.success('Test-package loaded successfully.');
            }
            else {
                var msg = (this.get_timedOut()) ? String.format('<b>Failed</b> to download and initialize the test-package at \'{0}\'.  Please ensure the file exists.', link) : String.format('<b>Failed</b> to initialize the script-file at \'{0}\' with the entry method \'{1}()\'.<br/>Please ensure there aren\'t errors in any of the test-class constructors.<br/>Message: \'{2}\'', Open.Core.Html.toHyperlink(this.get_scriptUrls()), this.get_entryPoint(), this.get_loadError().message);
                Open.Core.Log.error(msg);
            }
            Open.Core.Log.newSection();
            this._events$4.remove_testClassRegistered(ss.Delegate.create(this, this._onTestClassRegistered$4));
            Open.Core.Helper.invoke(onComplete);
        }) ]);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.CommonButtons

Open.Testing.Models.CommonButtons = function Open_Testing_Models_CommonButtons() {
    /// <summary>
    /// The index of common buttons.
    /// </summary>
    /// <field name="_addPackage$2" type="Open.Core.IButton">
    /// </field>
    Open.Testing.Models.CommonButtons.initializeBase(this);
}
Open.Testing.Models.CommonButtons.prototype = {
    _addPackage$2: null,
    
    get_addPackage: function Open_Testing_Models_CommonButtons$get_addPackage() {
        /// <summary>
        /// Gets the [+] button used to add a new test package.
        /// </summary>
        /// <value type="Open.Core.IButton"></value>
        return this._addPackage$2 || (this._addPackage$2 = Open.Core.Controls.Buttons.ImageButtonFactory.create(Open.Core.Controls.Buttons.ImageButtons.plusDark));
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.CustomListItem

Open.Testing.Models.CustomListItem = function Open_Testing_Models_CustomListItem(viewType) {
    /// <summary>
    /// A list-item node for custom UI.
    /// </summary>
    /// <param name="viewType" type="Open.Testing.Models.CustomListItemType">
    /// </param>
    /// <field name="_viewType$4" type="Open.Testing.Models.CustomListItemType">
    /// </field>
    Open.Testing.Models.CustomListItem.initializeBase(this);
    this._viewType$4 = viewType;
}
Open.Testing.Models.CustomListItem.prototype = {
    _viewType$4: 0,
    
    get_viewType: function Open_Testing_Models_CustomListItem$get_viewType() {
        /// <summary>
        /// Gets the type of view this list item produces.
        /// </summary>
        /// <value type="Open.Testing.Models.CustomListItemType"></value>
        return this._viewType$4;
    },
    
    createView: function Open_Testing_Models_CustomListItem$createView() {
        /// <returns type="Open.Core.IView"></returns>
        switch (this.get_viewType()) {
            case Open.Testing.Models.CustomListItemType.addPackage:
                return new Open.Testing.Views.AddPackageListItemView();
            default:
                throw Open.Core.Helper.get_exception().notSupported(Open.Testing.Models.CustomListItemType.toString(this.get_viewType()));
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.MethodListItem

Open.Testing.Models.MethodListItem = function Open_Testing_Models_MethodListItem(method) {
    /// <summary>
    /// A list-item node for a single Test-Method.
    /// </summary>
    /// <param name="method" type="Open.Testing.Models.MethodInfo">
    /// The test-method this node represents.
    /// </param>
    /// <field name="_method$4" type="Open.Testing.Models.MethodInfo">
    /// </field>
    Open.Testing.Models.MethodListItem.initializeBase(this);
    this._method$4 = method;
    this.set_text(method.get_displayName());
}
Open.Testing.Models.MethodListItem.prototype = {
    _method$4: null,
    
    get_method: function Open_Testing_Models_MethodListItem$get_method() {
        /// <summary>
        /// Gets the test-package this node represents.
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._method$4;
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
    /// <field name="_classInfo$4" type="Open.Testing.Models.ClassInfo">
    /// </field>
    Open.Testing.Models.ClassListItem.initializeBase(this);
    this._classInfo$4 = classInfo;
    this.set_text(classInfo.get_displayName());
}
Open.Testing.Models.ClassListItem.prototype = {
    _classInfo$4: null,
    
    get_classInfo: function Open_Testing_Models_ClassListItem$get_classInfo() {
        /// <summary>
        /// Gets the test-package this node represents.
        /// </summary>
        /// <value type="Open.Testing.Models.ClassInfo"></value>
        return this._classInfo$4;
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
    /// <field name="_classInfo$2" type="Open.Testing.Models.ClassInfo">
    /// </field>
    /// <field name="_name$2" type="String">
    /// </field>
    /// <field name="_displayName$2" type="String">
    /// </field>
    /// <field name="_isSpecial$2" type="Boolean">
    /// </field>
    Open.Testing.Models.MethodInfo.initializeBase(this);
    this._classInfo$2 = classInfo;
    this._name$2 = name;
    this._displayName$2 = Open.Testing.Models.MethodInfo.formatName(name);
    this._isSpecial$2 = Open.Testing._methodHelper.isSpecial(this.get_name());
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
    _classInfo$2: null,
    _name$2: null,
    _displayName$2: null,
    _isSpecial$2: false,
    
    get_classInfo: function Open_Testing_Models_MethodInfo$get_classInfo() {
        /// <summary>
        /// Gets the test-class that this method is a member of.
        /// </summary>
        /// <value type="Open.Testing.Models.ClassInfo"></value>
        return this._classInfo$2;
    },
    
    get_name: function Open_Testing_Models_MethodInfo$get_name() {
        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        /// <value type="String"></value>
        return this._name$2;
    },
    
    get_displayName: function Open_Testing_Models_MethodInfo$get_displayName() {
        /// <summary>
        /// Gets the display version of the name.
        /// </summary>
        /// <value type="String"></value>
        return this._displayName$2;
    },
    
    get_isSpecial: function Open_Testing_Models_MethodInfo$get_isSpecial() {
        /// <summary>
        /// Gets or sets whether this method is one of the special Setup/Teardown methods.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isSpecial$2;
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
            this.logError(error);
        }
        if (!this.get_isSpecial() && this.get_classInfo().get_testCleanup() != null) {
            this.get_classInfo().get_testCleanup().invoke();
        }
        return error;
    },
    
    logError: function Open_Testing_Models_MethodInfo$logError(error) {
        /// <summary>
        /// Formats an error message.
        /// </summary>
        /// <param name="error" type="Error">
        /// The invoke error.
        /// </param>
        var htmlList = Open.Core.Log.writeListSeverity(String.format('<b>Exception</b> Failed while executing \'<b>{0}</b>\'.', this.get_displayName()), Open.Core.LogSeverity.error);
        htmlList.add(String.format('Message: \'{0}\'', error.message));
        htmlList.add('Method: ' + Open.Core.Helper.get_string().toCamelCase(this.get_name()));
        htmlList.add('Class: ' + this.get_classInfo().get_classType().get_fullName());
        htmlList.add('Package: ' + Open.Core.Html.toHyperlink(this.get_classInfo().get_packageInfo().get_loader().get_scriptUrls(), null, Open.Core.LinkTarget.blank));
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Models.PackageInfo

Open.Testing.Models.PackageInfo = function Open_Testing_Models_PackageInfo(initMethod, scriptUrl) {
    /// <summary>
    /// Represents a package of tests from a single JavaScript file.
    /// </summary>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <field name="_singletons$2" type="Array" static="true">
    /// </field>
    /// <field name="_classes$2" type="Array">
    /// </field>
    /// <field name="_name$2" type="String">
    /// </field>
    /// <field name="_loader$2" type="Open.Testing.Models.PackageLoader">
    /// </field>
    this._classes$2 = [];
    Open.Testing.Models.PackageInfo.initializeBase(this);
    if (String.isNullOrEmpty(scriptUrl)) {
        throw new Error('A URL to the test-package script must be specified.');
    }
    if (String.isNullOrEmpty(initMethod)) {
        throw new Error('An entry point method must be specified.');
    }
    this._name$2 = Open.Testing.Models.PackageInfo._getName$2(scriptUrl);
    this._loader$2 = new Open.Testing.Models.PackageLoader(this, initMethod, scriptUrl);
}
Open.Testing.Models.PackageInfo.get_singletons = function Open_Testing_Models_PackageInfo$get_singletons() {
    /// <summary>
    /// Gets the collection of singleton TestPackageDef instances.
    /// </summary>
    /// <value type="Array"></value>
    return Open.Testing.Models.PackageInfo._singletons$2;
}
Open.Testing.Models.PackageInfo.singletonFromUrl = function Open_Testing_Models_PackageInfo$singletonFromUrl(initMethod, scriptUrl) {
    /// <summary>
    /// Retrieves (or creates) the singleton instance of the definition for the given package type.
    /// </summary>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <returns type="Open.Testing.Models.PackageInfo"></returns>
    var def = Type.safeCast(Open.Core.Helper.get_collection().first(Open.Testing.Models.PackageInfo._singletons$2, function(o) {
        return (o).get_id() === scriptUrl.toLowerCase();
    }), Open.Testing.Models.PackageInfo);
    if (def == null) {
        def = new Open.Testing.Models.PackageInfo(initMethod, scriptUrl);
        Open.Testing.Models.PackageInfo._singletons$2.add(def);
    }
    return def;
}
Open.Testing.Models.PackageInfo._getName$2 = function Open_Testing_Models_PackageInfo$_getName$2(scriptUrl) {
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
    _name$2: null,
    _loader$2: null,
    
    get_id: function Open_Testing_Models_PackageInfo$get_id() {
        /// <summary>
        /// Gets the unique ID of the package.
        /// </summary>
        /// <value type="String"></value>
        return this.get_loader().get_scriptUrls();
    },
    
    get_name: function Open_Testing_Models_PackageInfo$get_name() {
        /// <summary>
        /// Gets the display name of the package.
        /// </summary>
        /// <value type="String"></value>
        return this._name$2;
    },
    
    get_count: function Open_Testing_Models_PackageInfo$get_count() {
        /// <summary>
        /// Gets the number of test classes within the package.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._classes$2.length;
    },
    
    get_loader: function Open_Testing_Models_PackageInfo$get_loader() {
        /// <summary>
        /// Gets the package loader.
        /// </summary>
        /// <value type="Open.Testing.Models.PackageLoader"></value>
        return this._loader$2;
    },
    
    getEnumerator: function Open_Testing_Models_PackageInfo$getEnumerator() {
        /// <returns type="ss.IEnumerator"></returns>
        this._classes$2.sort(ss.Delegate.create(this, function(o1, o2) {
            return String.compare((o1).get_displayName(), (o2).get_displayName());
        }));
        return this._classes$2.getEnumerator();
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
        this._classes$2.add(Open.Testing.Models.ClassInfo.getSingleton(testClass, this));
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
        var $enum1 = ss.IEnumerator.getEnumerator(this._classes$2);
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
    /// <field name="_singletons$2" type="Object" static="true">
    /// </field>
    /// <field name="_classType$2" type="Type">
    /// </field>
    /// <field name="_packageInfo$2" type="Open.Testing.Models.PackageInfo">
    /// </field>
    /// <field name="_instance$2" type="Object">
    /// </field>
    /// <field name="_methods$2" type="Array">
    /// </field>
    /// <field name="_displayName$2" type="String">
    /// </field>
    /// <field name="_classInitialize$2" type="Open.Testing.Models.MethodInfo">
    /// </field>
    /// <field name="_classCleanup$2" type="Open.Testing.Models.MethodInfo">
    /// </field>
    /// <field name="_testInitialize$2" type="Open.Testing.Models.MethodInfo">
    /// </field>
    /// <field name="_testCleanup$2" type="Open.Testing.Models.MethodInfo">
    /// </field>
    this._methods$2 = [];
    Open.Testing.Models.ClassInfo.initializeBase(this);
    this._classType$2 = classType;
    this._packageInfo$2 = packageInfo;
    this._displayName$2 = Open.Testing.Models.ClassInfo._formatName$2(classType.get_name());
    this._getMethods$2();
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
    if (Open.Testing.Models.ClassInfo._singletons$2 == null) {
        Open.Testing.Models.ClassInfo._singletons$2 = {};
    }
    var key = String.format('{0}::{1}', packageInfo.get_id(), testClass.get_fullName());
    if (Object.keyExists(Open.Testing.Models.ClassInfo._singletons$2, key)) {
        return Type.safeCast(Open.Testing.Models.ClassInfo._singletons$2[key], Open.Testing.Models.ClassInfo);
    }
    var def = new Open.Testing.Models.ClassInfo(testClass, packageInfo);
    Open.Testing.Models.ClassInfo._singletons$2[key] = def;
    return def;
}
Open.Testing.Models.ClassInfo._formatName$2 = function Open_Testing_Models_ClassInfo$_formatName$2(name) {
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
    _classType$2: null,
    _packageInfo$2: null,
    _instance$2: null,
    _displayName$2: null,
    _classInitialize$2: null,
    _classCleanup$2: null,
    _testInitialize$2: null,
    _testCleanup$2: null,
    
    get_classType: function Open_Testing_Models_ClassInfo$get_classType() {
        /// <summary>
        /// Gets the type of the test class.
        /// </summary>
        /// <value type="Type"></value>
        return this._classType$2;
    },
    
    get_packageInfo: function Open_Testing_Models_ClassInfo$get_packageInfo() {
        /// <summary>
        /// Gets the package the class belongs to.
        /// </summary>
        /// <value type="Open.Testing.Models.PackageInfo"></value>
        return this._packageInfo$2;
    },
    
    get_displayName: function Open_Testing_Models_ClassInfo$get_displayName() {
        /// <summary>
        /// Gets the display version of the class name.
        /// </summary>
        /// <value type="String"></value>
        return this._displayName$2;
    },
    
    get_instance: function Open_Testing_Models_ClassInfo$get_instance() {
        /// <summary>
        /// Gets the test instance of the class.
        /// </summary>
        /// <value type="Object"></value>
        return this._instance$2 || (this._instance$2 = Type.safeCast(new this._classType$2(), Object));
    },
    
    get_count: function Open_Testing_Models_ClassInfo$get_count() {
        /// <summary>
        /// Gets the number of test-methods within the class.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._methods$2.length;
    },
    
    get_classInitialize: function Open_Testing_Models_ClassInfo$get_classInitialize() {
        /// <summary>
        /// Gets the 'ClassInitialize' special method (or null if one isn't declared).
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._classInitialize$2;
    },
    
    get_classCleanup: function Open_Testing_Models_ClassInfo$get_classCleanup() {
        /// <summary>
        /// Gets the 'ClassCleanup' special method (or null if one isn't declared).
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._classCleanup$2;
    },
    
    get_testInitialize: function Open_Testing_Models_ClassInfo$get_testInitialize() {
        /// <summary>
        /// Gets the 'TestInitialize' special method (or null if one isn't declared).
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._testInitialize$2;
    },
    
    get_testCleanup: function Open_Testing_Models_ClassInfo$get_testCleanup() {
        /// <summary>
        /// Gets the 'TestCleanup' special method (or null if one isn't declared).
        /// </summary>
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._testCleanup$2;
    },
    
    reset: function Open_Testing_Models_ClassInfo$reset() {
        /// <summary>
        /// Resets the test-class instance.
        /// </summary>
        this._instance$2 = null;
    },
    
    getEnumerator: function Open_Testing_Models_ClassInfo$getEnumerator() {
        /// <returns type="ss.IEnumerator"></returns>
        return this._methods$2.getEnumerator();
    },
    
    toString: function Open_Testing_Models_ClassInfo$toString() {
        /// <returns type="String"></returns>
        return String.format('[{0}:{1}]', Type.getInstanceType(this).get_name(), this.get_classType().get_name());
    },
    
    _getMethods$2: function Open_Testing_Models_ClassInfo$_getMethods$2() {
        if (this.get_instance() == null) {
            return;
        }
        var $dict1 = this.get_instance();
        for (var $key2 in $dict1) {
            var item = { key: $key2, value: $dict1[$key2] };
            if (Open.Testing.Models.MethodInfo.isTestMethod(item)) {
                this._methods$2.add(this._createMethod$2(item));
            }
            else {
                this._assignSpecialMethod$2(item);
            }
        }
    },
    
    _assignSpecialMethod$2: function Open_Testing_Models_ClassInfo$_assignSpecialMethod$2(item) {
        /// <param name="item" type="DictionaryEntry">
        /// </param>
        var key = item.key;
        if (!Open.Testing._methodHelper.isSpecial(key)) {
            return;
        }
        var method = this._createMethod$2(item);
        if (Open.Testing._methodHelper.isClassInitialize(key)) {
            this._classInitialize$2 = method;
        }
        else if (Open.Testing._methodHelper.isClassCleanup(key)) {
            this._classCleanup$2 = method;
        }
        else if (Open.Testing._methodHelper.isTestInitialize(key)) {
            this._testInitialize$2 = method;
        }
        else if (Open.Testing._methodHelper.isTestCleanup(key)) {
            this._testCleanup$2 = method;
        }
    },
    
    _createMethod$2: function Open_Testing_Models_ClassInfo$_createMethod$2(item) {
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
    /// <field name="_testPackage$4" type="Open.Testing.Models.PackageInfo">
    /// </field>
    Open.Testing.Models.PackageListItem.initializeBase(this);
    this._testPackage$4 = testPackage;
    this.set_text(testPackage.get_name());
}
Open.Testing.Models.PackageListItem.prototype = {
    _testPackage$4: null,
    
    get_testPackage: function Open_Testing_Models_PackageListItem$get_testPackage() {
        /// <summary>
        /// Gets the test-package this node represents.
        /// </summary>
        /// <value type="Open.Testing.Models.PackageInfo"></value>
        return this._testPackage$4;
    }
}


Type.registerNamespace('Open.Testing.Views');

////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Views.AddPackageListItemView

Open.Testing.Views.AddPackageListItemView = function Open_Testing_Views_AddPackageListItemView() {
    /// <summary>
    /// List item view for adding a new test-package.
    /// </summary>
    /// <field name="_addButton$4" type="Open.Core.Controls.Buttons.ImageButton">
    /// </field>
    /// <field name="_itemHeight$4" type="Number" integer="true" static="true">
    /// </field>
    Open.Testing.Views.AddPackageListItemView.initializeBase(this);
    this.set_height(Open.Testing.Views.AddPackageListItemView._itemHeight$4);
    this.set_position(Open.Core.CssPosition.relative);
    this._insertAddButton$4();
}
Open.Testing.Views.AddPackageListItemView.prototype = {
    _addButton$4: null,
    
    _insertAddButton$4: function Open_Testing_Views_AddPackageListItemView$_insertAddButton$4() {
        this._addButton$4 = Type.safeCast(this.get_common().get_buttons().get_addPackage(), Open.Core.Controls.Buttons.ImageButton);
        this._addButton$4.setSize(Open.Testing.Views.AddPackageListItemView._itemHeight$4, Open.Testing.Views.AddPackageListItemView._itemHeight$4);
        var view = Type.safeCast(this._addButton$4.createView(), Open.Core.Controls.Buttons.ButtonView);
        view.setCss(Open.Core.Css.position, Open.Core.Css.absolute);
        view.setCss(Open.Core.Css.right, 0 + Open.Core.Css.px);
        view.setCss(Open.Core.Css.top, 0 + Open.Core.Css.px);
        this.get_container().append(view.get_container());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Views.AddPackageView

Open.Testing.Views.AddPackageView = function Open_Testing_Views_AddPackageView() {
    /// <summary>
    /// View for defining a new test-package to add to the side bar.
    /// </summary>
    /// <field name="__showing$4" type="EventHandler" static="true">
    /// </field>
    /// <field name="__hidden$4" type="EventHandler" static="true">
    /// </field>
    /// <field name="_contentUrl$4" type="String" static="true">
    /// </field>
    /// <field name="iconJs" type="String" static="true">
    /// </field>
    /// <field name="iconMethod" type="String" static="true">
    /// </field>
    /// <field name="_slideDuration$4" type="Number" static="true">
    /// </field>
    /// <field name="_divInnerSlide$4" type="jQueryObject">
    /// </field>
    /// <field name="_offLeft$4" type="String">
    /// </field>
    /// <field name="_txtScriptUrl$4" type="Open.Core.Controls.Textbox">
    /// </field>
    /// <field name="_txtInitMethod$4" type="Open.Core.Controls.Textbox">
    /// </field>
    /// <field name="_btnAdd$4" type="Open.Core.Controls.Buttons.IconTextButton">
    /// </field>
    /// <field name="_btnCancel$4" type="Open.Core.Controls.Buttons.IconTextButton">
    /// </field>
    Open.Testing.Views.AddPackageView.initializeBase(this);
    this.retrieveHtml(Open.Testing.Views.AddPackageView._contentUrl$4, ss.Delegate.create(this, function() {
        this._divInnerSlide$4 = $(Open.Testing.CssSelectors.addPackageInnerSlide);
        this._offLeft$4 = this._divInnerSlide$4.css(Open.Core.Css.left);
        this._initializeTextboxes$4();
        this._initializeButtons$4();
        this.slideOn(null);
    }));
}
Open.Testing.Views.AddPackageView.add_showing = function Open_Testing_Views_AddPackageView$add_showing(value) {
    /// <summary>
    /// Fires when the 'Add Package' view is shown (at start of slide animation).
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Testing.Views.AddPackageView.__showing$4 = ss.Delegate.combine(Open.Testing.Views.AddPackageView.__showing$4, value);
}
Open.Testing.Views.AddPackageView.remove_showing = function Open_Testing_Views_AddPackageView$remove_showing(value) {
    /// <summary>
    /// Fires when the 'Add Package' view is shown (at start of slide animation).
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Testing.Views.AddPackageView.__showing$4 = ss.Delegate.remove(Open.Testing.Views.AddPackageView.__showing$4, value);
}
Open.Testing.Views.AddPackageView._fireShowing$4 = function Open_Testing_Views_AddPackageView$_fireShowing$4() {
    if (Open.Testing.Views.AddPackageView.__showing$4 != null) {
        Open.Testing.Views.AddPackageView.__showing$4.invoke(Open.Testing.Views.AddPackageView, new ss.EventArgs());
    }
}
Open.Testing.Views.AddPackageView.add_hidden = function Open_Testing_Views_AddPackageView$add_hidden(value) {
    /// <summary>
    /// Fires when the 'Add Package' view is hidden (at end of slide animation).
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Testing.Views.AddPackageView.__hidden$4 = ss.Delegate.combine(Open.Testing.Views.AddPackageView.__hidden$4, value);
}
Open.Testing.Views.AddPackageView.remove_hidden = function Open_Testing_Views_AddPackageView$remove_hidden(value) {
    /// <summary>
    /// Fires when the 'Add Package' view is hidden (at end of slide animation).
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Testing.Views.AddPackageView.__hidden$4 = ss.Delegate.remove(Open.Testing.Views.AddPackageView.__hidden$4, value);
}
Open.Testing.Views.AddPackageView._fireHidden$4 = function Open_Testing_Views_AddPackageView$_fireHidden$4() {
    if (Open.Testing.Views.AddPackageView.__hidden$4 != null) {
        Open.Testing.Views.AddPackageView.__hidden$4.invoke(Open.Testing.Views.AddPackageView, new ss.EventArgs());
    }
}
Open.Testing.Views.AddPackageView.addToTestHarness = function Open_Testing_Views_AddPackageView$addToTestHarness() {
    /// <summary>
    /// Inserts an instance of the view into the TestHarness' main canvas.
    /// </summary>
    /// <returns type="Open.Testing.Views.AddPackageView"></returns>
    var view = new Open.Testing.Views.AddPackageView();
    Open.Testing.TestHarness.reset();
    Open.Testing.TestHarness.set_canScroll(false);
    Open.Testing.TestHarness.set_displayMode(Open.Testing.ControlDisplayMode.fill);
    Open.Testing.TestHarness.addControl(view);
    return view;
}
Open.Testing.Views.AddPackageView._initializeTextbox$4 = function Open_Testing_Views_AddPackageView$_initializeTextbox$4(selector, icon) {
    /// <param name="selector" type="String">
    /// </param>
    /// <param name="icon" type="String">
    /// </param>
    /// <returns type="Open.Core.Controls.Textbox"></returns>
    var textbox = new Open.Core.Controls.Textbox();
    textbox.get_padding().change(10, 5);
    textbox.insert(selector, Open.Core.InsertMode.replace);
    textbox.set_leftIcon(Open.Core.Helper.get_url().prependDomain(icon));
    return textbox;
}
Open.Testing.Views.AddPackageView._initializeButton$4 = function Open_Testing_Views_AddPackageView$_initializeButton$4(selector, text, handler) {
    /// <param name="selector" type="String">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="handler" type="EventHandler">
    /// </param>
    /// <returns type="Open.Core.Controls.Buttons.IconTextButton"></returns>
    var button = new Open.Core.Controls.Buttons.IconTextButton();
    button.set_text(text);
    button.createView().insert(selector, Open.Core.InsertMode.replace);
    button.add_click(handler);
    return button;
}
Open.Testing.Views.AddPackageView.prototype = {
    _divInnerSlide$4: null,
    _offLeft$4: null,
    _txtScriptUrl$4: null,
    _txtInitMethod$4: null,
    _btnAdd$4: null,
    _btnCancel$4: null,
    
    _onAddClick$4: function Open_Testing_Views_AddPackageView$_onAddClick$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        Open.Core.Log.event('Add Click');
    },
    
    _onCancelClick$4: function Open_Testing_Views_AddPackageView$_onCancelClick$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        Open.Core.Log.event('Cancel Click');
    },
    
    slideOn: function Open_Testing_Views_AddPackageView$slideOn(onComplete) {
        /// <summary>
        /// Slides the panel on screen.
        /// </summary>
        /// <param name="onComplete" type="Action">
        /// Action to invoke upon completion.
        /// </param>
        Open.Testing.Views.AddPackageView._fireShowing$4();
        this._slide$4('0px', onComplete);
    },
    
    slideOff: function Open_Testing_Views_AddPackageView$slideOff(onComplete) {
        /// <summary>
        /// Slides the panel off screen.
        /// </summary>
        /// <param name="onComplete" type="Action">
        /// Action to invoke upon completion.
        /// </param>
        this._slide$4(this._offLeft$4, ss.Delegate.create(this, function() {
            Open.Testing.Views.AddPackageView._fireHidden$4();
            Open.Core.Helper.invoke(onComplete);
        }));
    },
    
    _initializeTextboxes$4: function Open_Testing_Views_AddPackageView$_initializeTextboxes$4() {
        this._txtScriptUrl$4 = Open.Testing.Views.AddPackageView._initializeTextbox$4(Open.Testing.CssSelectors.addPackageTxtScript, Open.Testing.Views.AddPackageView.iconJs);
        this._txtInitMethod$4 = Open.Testing.Views.AddPackageView._initializeTextbox$4(Open.Testing.CssSelectors.addPackageTxtMethod, Open.Testing.Views.AddPackageView.iconMethod);
    },
    
    _initializeButtons$4: function Open_Testing_Views_AddPackageView$_initializeButtons$4() {
        this._btnAdd$4 = Open.Testing.Views.AddPackageView._initializeButton$4(Open.Testing.CssSelectors.addPackageBtnAdd, Open.Testing.StringLibrary.add, ss.Delegate.create(this, this._onAddClick$4));
        this._btnCancel$4 = Open.Testing.Views.AddPackageView._initializeButton$4(Open.Testing.CssSelectors.addPackageBtnCancel, Open.Testing.StringLibrary.cancel, ss.Delegate.create(this, this._onCancelClick$4));
    },
    
    _slide$4: function Open_Testing_Views_AddPackageView$_slide$4(left, onComplete) {
        /// <param name="left" type="String">
        /// </param>
        /// <param name="onComplete" type="Action">
        /// </param>
        var properties = {};
        properties[Open.Core.Css.left] = left;
        this._divInnerSlide$4.animate(properties, Open.Core.Helper.get_time().toMsecs(Open.Testing.Views.AddPackageView._slideDuration$4), 'swing', ss.Delegate.create(this, function() {
            Open.Core.Helper.invoke(onComplete);
        }));
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Views.ControlHostView

Open.Testing.Views.ControlHostView = function Open_Testing_Views_ControlHostView() {
    /// <summary>
    /// The view of the control-host, the canvas which hosts test controls.
    /// </summary>
    /// <field name="_divMain$4" type="jQueryObject">
    /// </field>
    Open.Testing.Views.ControlHostView.initializeBase(this, [ $(Open.Testing.CssSelectors.controlHost) ]);
    this._divMain$4 = $(Open.Testing.CssSelectors.main);
}
Open.Testing.Views.ControlHostView.prototype = {
    _divMain$4: null,
    
    get_divMain: function Open_Testing_Views_ControlHostView$get_divMain() {
        /// <summary>
        /// Gets the main container DIV.
        /// </summary>
        /// <value type="jQueryObject"></value>
        return this._divMain$4;
    }
}


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
    /// <field name="_fillMargin$4" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_divRoot$4" type="jQueryObject">
    /// </field>
    /// <field name="_htmlElement$4" type="jQueryObject">
    /// </field>
    /// <field name="_control$4" type="Open.Core.IView">
    /// </field>
    /// <field name="_displayMode$4" type="Open.Testing.ControlDisplayMode">
    /// </field>
    /// <field name="_allViews$4" type="ss.IEnumerable">
    /// </field>
    /// <field name="_index$4" type="Number" integer="true">
    /// </field>
    /// <field name="_events$4" type="Open.Testing.TestHarnessEvents">
    /// </field>
    Open.Testing.Views.ControlWrapperView.initializeBase(this, [ divHost ]);
    this._control$4 = control;
    this._htmlElement$4 = htmlElement;
    this._displayMode$4 = displayMode;
    this._allViews$4 = allViews;
    this._index$4 = divHost.children().length;
    this._events$4 = this.get_common().get_events();
    this._divRoot$4 = Open.Core.Html.createDiv();
    this._divRoot$4.css(Open.Core.Css.position, Open.Core.Css.absolute);
    this._divRoot$4.appendTo(divHost);
    htmlElement.css(Open.Core.Css.position, Open.Core.Css.absolute);
    htmlElement.appendTo(this._divRoot$4);
    this._events$4.add__controlHostSizeChanged(ss.Delegate.create(this, this._onHostResized$4));
    this.updateLayout();
}
Open.Testing.Views.ControlWrapperView.prototype = {
    _divRoot$4: null,
    _htmlElement$4: null,
    _control$4: null,
    _displayMode$4: 0,
    _allViews$4: null,
    _index$4: 0,
    _events$4: null,
    
    onDisposed: function Open_Testing_Views_ControlWrapperView$onDisposed() {
        /// <summary>
        /// Destructor.
        /// </summary>
        this._events$4.remove__controlHostSizeChanged(ss.Delegate.create(this, this._onHostResized$4));
        this._divRoot$4.remove();
        Open.Testing.Views.ControlWrapperView.callBaseMethod(this, 'onDisposed');
    },
    
    _onHostResized$4: function Open_Testing_Views_ControlWrapperView$_onHostResized$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        Open.Core.Css.setOverflow(this.get_container(), Open.Core.CssOverflow.hidden);
        this.updateLayout();
    },
    
    get_displayMode: function Open_Testing_Views_ControlWrapperView$get_displayMode() {
        /// <summary>
        /// Gets or sets the items size mode.
        /// </summary>
        /// <value type="Open.Testing.ControlDisplayMode"></value>
        return this._displayMode$4;
    },
    set_displayMode: function Open_Testing_Views_ControlWrapperView$set_displayMode(value) {
        /// <summary>
        /// Gets or sets the items size mode.
        /// </summary>
        /// <value type="Open.Testing.ControlDisplayMode"></value>
        this._displayMode$4 = value;
        return value;
    },
    
    get_htmlElement: function Open_Testing_Views_ControlWrapperView$get_htmlElement() {
        /// <summary>
        /// Gets the HTML content.
        /// </summary>
        /// <value type="jQueryObject"></value>
        return this._htmlElement$4;
    },
    
    get_control: function Open_Testing_Views_ControlWrapperView$get_control() {
        /// <summary>
        /// Gets the logical control if available (otherwise null).
        /// </summary>
        /// <value type="Open.Core.IView"></value>
        return this._control$4;
    },
    
    onUpdateLayout: function Open_Testing_Views_ControlWrapperView$onUpdateLayout() {
        this._updateSize$4();
        this._updatePosition$4();
    },
    
    _updateSize$4: function Open_Testing_Views_ControlWrapperView$_updateSize$4() {
        switch (this._displayMode$4) {
            case Open.Testing.ControlDisplayMode.none:
            case Open.Testing.ControlDisplayMode.center:
                break;
            case Open.Testing.ControlDisplayMode.fill:
                this._setSizeWithPadding$4(0, 0);
                break;
            case Open.Testing.ControlDisplayMode.fillWithMargin:
                this._setSizeWithPadding$4(Open.Testing.Views.ControlWrapperView._fillMargin$4, Open.Testing.Views.ControlWrapperView._fillMargin$4);
                break;
            default:
                throw new Error(Open.Testing.ControlDisplayMode.toString(this._displayMode$4));
        }
        Open.Core.Css.setOverflow(this.get_container(), (Open.Testing.TestHarness.get_canScroll()) ? Open.Core.CssOverflow.auto : Open.Core.CssOverflow.hidden);
    },
    
    _setSizeWithPadding$4: function Open_Testing_Views_ControlWrapperView$_setSizeWithPadding$4(xPadding, yPadding) {
        /// <param name="xPadding" type="Number" integer="true">
        /// </param>
        /// <param name="yPadding" type="Number" integer="true">
        /// </param>
        var width = (this.get_container().width() - (xPadding * 2));
        var height = (this.get_container().height() - (yPadding * 2));
        Open.Core.Css.setSize(this._htmlElement$4, width, height);
    },
    
    _updatePosition$4: function Open_Testing_Views_ControlWrapperView$_updatePosition$4() {
        if (this.get_displayMode() === Open.Testing.ControlDisplayMode.none) {
            return;
        }
        this._divRoot$4.css(Open.Core.Css.left, this._getLeft$4() + Open.Core.Css.px);
        var top = (this.get_container().children().length === 1) ? this._getTop$4() : this._getStackedTop$4();
        if (this._displayMode$4 !== Open.Testing.ControlDisplayMode.fill && top < Open.Testing.Views.ControlWrapperView._fillMargin$4) {
            top = Open.Testing.Views.ControlWrapperView._fillMargin$4;
        }
        this._divRoot$4.css(Open.Core.Css.top, top + Open.Core.Css.px);
    },
    
    _getLeft$4: function Open_Testing_Views_ControlWrapperView$_getLeft$4() {
        /// <returns type="Number" integer="true"></returns>
        switch (this._displayMode$4) {
            case Open.Testing.ControlDisplayMode.none:
                return -1;
            case Open.Testing.ControlDisplayMode.center:
                return (this.get_container().width() / 2) - (this._htmlElement$4.width() / 2);
            case Open.Testing.ControlDisplayMode.fill:
                return 0;
            case Open.Testing.ControlDisplayMode.fillWithMargin:
                return Open.Testing.Views.ControlWrapperView._fillMargin$4;
            default:
                throw new Error(Open.Testing.ControlDisplayMode.toString(this._displayMode$4));
        }
    },
    
    _getTop$4: function Open_Testing_Views_ControlWrapperView$_getTop$4() {
        /// <returns type="Number" integer="true"></returns>
        switch (this._displayMode$4) {
            case Open.Testing.ControlDisplayMode.none:
                return -1;
            case Open.Testing.ControlDisplayMode.center:
                return (this.get_container().height() / 2) - (this._htmlElement$4.height() / 2);
            case Open.Testing.ControlDisplayMode.fill:
                return 0;
            case Open.Testing.ControlDisplayMode.fillWithMargin:
                return Open.Testing.Views.ControlWrapperView._fillMargin$4;
            default:
                throw new Error(Open.Testing.ControlDisplayMode.toString(this._displayMode$4));
        }
    },
    
    _getStackedTop$4: function Open_Testing_Views_ControlWrapperView$_getStackedTop$4() {
        /// <returns type="Number" integer="true"></returns>
        return this._getOffsetHeight$4() + ((this._index$4 + 1) * Open.Testing.Views.ControlWrapperView._fillMargin$4);
    },
    
    _getOffsetHeight$4: function Open_Testing_Views_ControlWrapperView$_getOffsetHeight$4() {
        /// <returns type="Number" integer="true"></returns>
        var height = 0;
        var $enum1 = ss.IEnumerator.getEnumerator(this._allViews$4);
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
// Open.Testing.Views.LogContainerView

Open.Testing.Views.LogContainerView = function Open_Testing_Views_LogContainerView() {
    /// <summary>
    /// The log container view.
    /// </summary>
    /// <field name="__clearClick$4" type="EventHandler">
    /// </field>
    /// <field name="_buttonHeight$4" type="Number" integer="true" static="true">
    /// </field>
    Open.Testing.Views.LogContainerView.initializeBase(this, [ $(Open.Testing.CssSelectors.logContainer) ]);
    Open.Testing._buttonHelper.insertButton(Open.Core.Controls.Buttons.ImageButtons.remove, Open.Testing.CssSelectors.logClearButton, Open.Testing.Views.LogContainerView._buttonHeight$4, ss.Delegate.create(this, function() {
        this._fireClearClick$4();
    }));
}
Open.Testing.Views.LogContainerView.prototype = {
    
    add_clearClick: function Open_Testing_Views_LogContainerView$add_clearClick(value) {
        /// <summary>
        /// Fires when the 'Clear' button is clicked.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__clearClick$4 = ss.Delegate.combine(this.__clearClick$4, value);
    },
    remove_clearClick: function Open_Testing_Views_LogContainerView$remove_clearClick(value) {
        /// <summary>
        /// Fires when the 'Clear' button is clicked.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__clearClick$4 = ss.Delegate.remove(this.__clearClick$4, value);
    },
    
    __clearClick$4: null,
    
    _fireClearClick$4: function Open_Testing_Views_LogContainerView$_fireClearClick$4() {
        if (this.__clearClick$4 != null) {
            this.__clearClick$4.invoke(this, new ss.EventArgs());
        }
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
    /// <field name="_sidebar$4" type="Open.Testing.Views.SidebarView">
    /// </field>
    /// <field name="_controlHost$4" type="Open.Testing.Views.ControlHostView">
    /// </field>
    /// <field name="_logContainer$4" type="Open.Testing.Views.LogContainerView">
    /// </field>
    Open.Testing.Views.ShellView.initializeBase(this, [ container ]);
    this._sidebar$4 = new Open.Testing.Views.SidebarView();
    this._controlHost$4 = new Open.Testing.Views.ControlHostView();
    this._logContainer$4 = new Open.Testing.Views.LogContainerView();
}
Open.Testing.Views.ShellView.prototype = {
    _sidebar$4: null,
    _controlHost$4: null,
    _logContainer$4: null,
    
    onDisposed: function Open_Testing_Views_ShellView$onDisposed() {
        /// <summary>
        /// Destroy.
        /// </summary>
        this._sidebar$4.dispose();
        this._controlHost$4.dispose();
        this._logContainer$4.dispose();
        Open.Testing.Views.ShellView.callBaseMethod(this, 'onDisposed');
    },
    
    get_sidebar: function Open_Testing_Views_ShellView$get_sidebar() {
        /// <summary>
        /// Gets the view for the SideBar.
        /// </summary>
        /// <value type="Open.Testing.Views.SidebarView"></value>
        return this._sidebar$4;
    },
    
    get_controlHost: function Open_Testing_Views_ShellView$get_controlHost() {
        /// <summary>
        /// Gets the view for the ControlHost.
        /// </summary>
        /// <value type="Open.Testing.Views.ControlHostView"></value>
        return this._controlHost$4;
    },
    
    get_logContainer: function Open_Testing_Views_ShellView$get_logContainer() {
        /// <summary>
        /// Gets the view for the Log container.
        /// </summary>
        /// <value type="Open.Testing.Views.LogContainerView"></value>
        return this._logContainer$4;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Views.SidebarView

Open.Testing.Views.SidebarView = function Open_Testing_Views_SidebarView() {
    /// <summary>
    /// The view for the side-bar.
    /// </summary>
    /// <field name="slideDuration" type="Number" static="true">
    /// </field>
    /// <field name="propIsTestListVisible" type="String" static="true">
    /// </field>
    /// <field name="_rootList$4" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_backController$4" type="Open.Core.Lists.ListTreeBackController">
    /// </field>
    /// <field name="_methodList$4" type="Open.Testing.Views.MethodListView">
    /// </field>
    /// <field name="_methodListHeightController$4" type="Open.Testing.Controllers._methodListHeightController">
    /// </field>
    Open.Testing.Views.SidebarView.initializeBase(this, [ $(Open.Testing.CssSelectors.sidebar) ]);
    this._rootList$4 = new Open.Core.Lists.ListTreeView($(Open.Testing.CssSelectors.sidebarRootList));
    this._rootList$4.get_slide().set_duration(Open.Testing.Views.SidebarView.slideDuration);
    this._methodList$4 = new Open.Testing.Views.MethodListView($(Open.Testing.CssSelectors.methodList));
    this._backController$4 = new Open.Core.Lists.ListTreeBackController(this._rootList$4, $(Open.Testing.CssSelectors.sidebarToolbar), $(Open.Testing.CssSelectors.backMask));
    this._methodListHeightController$4 = new Open.Testing.Controllers._methodListHeightController(this);
    Open.Core.GlobalEvents.add_windowResizeComplete(ss.Delegate.create(this, this._onSizeChanged$4));
    Open.Core.GlobalEvents.add_panelResizeComplete(ss.Delegate.create(this, this._onSizeChanged$4));
    this.updateLayout();
}
Open.Testing.Views.SidebarView.prototype = {
    _rootList$4: null,
    _backController$4: null,
    _methodList$4: null,
    _methodListHeightController$4: null,
    
    onDisposed: function Open_Testing_Views_SidebarView$onDisposed() {
        Open.Core.GlobalEvents.remove_windowResizeComplete(ss.Delegate.create(this, this._onSizeChanged$4));
        Open.Core.GlobalEvents.remove_panelResizeComplete(ss.Delegate.create(this, this._onSizeChanged$4));
        this._rootList$4.dispose();
        this._backController$4.dispose();
        this._methodListHeightController$4.dispose();
        Open.Testing.Views.SidebarView.callBaseMethod(this, 'onDisposed');
    },
    
    _onSizeChanged$4: function Open_Testing_Views_SidebarView$_onSizeChanged$4(sender, e) {
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
        return this._rootList$4;
    },
    
    get_methodList: function Open_Testing_Views_SidebarView$get_methodList() {
        /// <summary>
        /// Gets the Test-List view.
        /// </summary>
        /// <value type="Open.Testing.Views.MethodListView"></value>
        return this._methodList$4;
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
    
    onUpdateLayout: function Open_Testing_Views_SidebarView$onUpdateLayout() {
        this._methodListHeightController$4.updateLayout();
        this._syncRootListHeight$4();
    },
    
    _syncRootListHeight$4: function Open_Testing_Views_SidebarView$_syncRootListHeight$4() {
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
    /// <field name="__runClick$4" type="EventHandler">
    /// </field>
    /// <field name="__refreshClick$4" type="EventHandler">
    /// </field>
    /// <field name="propClassInfo" type="String" static="true">
    /// </field>
    /// <field name="propSelectedMethod" type="String" static="true">
    /// </field>
    /// <field name="_buttonHeight$4" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_listView$4" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_rootNode$4" type="Open.Core.Lists.ListItem">
    /// </field>
    /// <field name="_events$4" type="Open.Testing.TestHarnessEvents">
    /// </field>
    Open.Testing.Views.MethodListView.initializeBase(this, [ container ]);
    this._events$4 = this.get_common().get_events();
    this._listView$4 = new Open.Core.Lists.ListTreeView($(Open.Testing.CssSelectors.methodListContent));
    this._listView$4.get_slide().set_duration(Open.Testing.Views.SidebarView.slideDuration);
    this._rootNode$4 = new Open.Core.Lists.ListItem();
    this._listView$4.set_rootNode(this._rootNode$4);
    this._insertButtons$4();
}
Open.Testing.Views.MethodListView.prototype = {
    
    add_runClick: function Open_Testing_Views_MethodListView$add_runClick(value) {
        /// <summary>
        /// Fires when the 'Run' button is clicked.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__runClick$4 = ss.Delegate.combine(this.__runClick$4, value);
    },
    remove_runClick: function Open_Testing_Views_MethodListView$remove_runClick(value) {
        /// <summary>
        /// Fires when the 'Run' button is clicked.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__runClick$4 = ss.Delegate.remove(this.__runClick$4, value);
    },
    
    __runClick$4: null,
    
    _fireRunClick$4: function Open_Testing_Views_MethodListView$_fireRunClick$4() {
        if (this.__runClick$4 != null) {
            this.__runClick$4.invoke(this, new ss.EventArgs());
        }
    },
    
    add_refreshClick: function Open_Testing_Views_MethodListView$add_refreshClick(value) {
        /// <summary>
        /// Fires when the 'Refresh' button is clicked.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__refreshClick$4 = ss.Delegate.combine(this.__refreshClick$4, value);
    },
    remove_refreshClick: function Open_Testing_Views_MethodListView$remove_refreshClick(value) {
        /// <summary>
        /// Fires when the 'Refresh' button is clicked.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__refreshClick$4 = ss.Delegate.remove(this.__refreshClick$4, value);
    },
    
    __refreshClick$4: null,
    
    _fireRefreshClick$4: function Open_Testing_Views_MethodListView$_fireRefreshClick$4() {
        if (this.__refreshClick$4 != null) {
            this.__refreshClick$4.invoke(this, new ss.EventArgs());
        }
    },
    
    _listView$4: null,
    _rootNode$4: null,
    _events$4: null,
    
    _onItemClick$4: function Open_Testing_Views_MethodListView$_onItemClick$4(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.set_selectedMethod((sender).get_method());
        this._events$4._fireMethodClicked(this.get_selectedMethod());
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
            this._populateList$4(value);
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
        return this._listView$4.get_contentHeight() + this.get__divTitleBar$4().height() + 1;
    },
    
    get__divTitleBar$4: function Open_Testing_Views_MethodListView$get__divTitleBar$4() {
        /// <value type="jQueryObject"></value>
        return this.get_container().children(Open.Testing.CssSelectors.methodListTitlebar);
    },
    
    onUpdateLayout: function Open_Testing_Views_MethodListView$onUpdateLayout() {
        this._listView$4.updateLayout();
    },
    
    _populateList$4: function Open_Testing_Views_MethodListView$_populateList$4(classInfo) {
        /// <param name="classInfo" type="Open.Testing.Models.ClassInfo">
        /// </param>
        this._clearChildren$4();
        if (classInfo == null) {
            return;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(classInfo);
        while ($enum1.moveNext()) {
            var method = $enum1.get_current();
            this._rootNode$4.addChild(this._createListItem$4(method));
        }
    },
    
    _createListItem$4: function Open_Testing_Views_MethodListView$_createListItem$4(methodInfo) {
        /// <param name="methodInfo" type="Open.Testing.Models.MethodInfo">
        /// </param>
        /// <returns type="Open.Testing.Models.MethodListItem"></returns>
        var item = new Open.Testing.Models.MethodListItem(methodInfo);
        item.add_click(ss.Delegate.create(this, this._onItemClick$4));
        return item;
    },
    
    _clearChildren$4: function Open_Testing_Views_MethodListView$_clearChildren$4() {
        var $enum1 = ss.IEnumerator.getEnumerator(this._rootNode$4.get_children());
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            child.remove_click(ss.Delegate.create(this, this._onItemClick$4));
        }
        this._rootNode$4.clearChildren();
        this.set_selectedMethod(null);
    },
    
    _insertButtons$4: function Open_Testing_Views_MethodListView$_insertButtons$4() {
        Open.Testing._buttonHelper.insertButton(Open.Core.Controls.Buttons.ImageButtons.playDark, Open.Testing.CssSelectors.methodListRunButton, Open.Testing.Views.MethodListView._buttonHeight$4, ss.Delegate.create(this, function() {
            this._fireRunClick$4();
        }));
        Open.Testing._buttonHelper.insertButton(Open.Core.Controls.Buttons.ImageButtons.refreshDark, Open.Testing.CssSelectors.methodListRefreshButton, Open.Testing.Views.MethodListView._buttonHeight$4, ss.Delegate.create(this, function() {
            this._fireRefreshClick$4();
        }));
    }
}


Open.Testing.TestHarnessViewBase.registerClass('Open.Testing.TestHarnessViewBase', Open.Core.ViewBase);
Open.Testing.TestHarnessControllerBase.registerClass('Open.Testing.TestHarnessControllerBase', Open.Core.ControllerBase);
Open.Testing.CssSelectors.registerClass('Open.Testing.CssSelectors');
Open.Testing.Elements.registerClass('Open.Testing.Elements');
Open.Testing.StringLibrary.registerClass('Open.Testing.StringLibrary');
Open.Testing._buttonHelper.registerClass('Open.Testing._buttonHelper');
Open.Testing.TestHarnessEvents.registerClass('Open.Testing.TestHarnessEvents', null, Open.Testing.Internal.ITestHarnessEvents);
Open.Testing.MethodEventArgs.registerClass('Open.Testing.MethodEventArgs');
Open.Testing.ClassEventArgs.registerClass('Open.Testing.ClassEventArgs');
Open.Testing._changeHeightEventArgs.registerClass('Open.Testing._changeHeightEventArgs');
Open.Testing.Common.registerClass('Open.Testing.Common');
Open.Testing._methodHelper.registerClass('Open.Testing._methodHelper');
Open.Testing.Application.registerClass('Open.Testing.Application');
Open.Testing.Automation.ClassTestRunner.registerClass('Open.Testing.Automation.ClassTestRunner');
Open.Testing.Automation._executedTest.registerClass('Open.Testing.Automation._executedTest');
Open.Testing.Controllers.AddPackageController.registerClass('Open.Testing.Controllers.AddPackageController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.ClassController.registerClass('Open.Testing.Controllers.ClassController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.ControlHostController.registerClass('Open.Testing.Controllers.ControlHostController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.LogController.registerClass('Open.Testing.Controllers.LogController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.MethodListController.registerClass('Open.Testing.Controllers.MethodListController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers._methodListHeightController.registerClass('Open.Testing.Controllers._methodListHeightController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.PanelResizeController.registerClass('Open.Testing.Controllers.PanelResizeController', Open.Testing.TestHarnessControllerBase, Open.Testing.Controllers.IPanelResizeController);
Open.Testing.Controllers.SidebarController.registerClass('Open.Testing.Controllers.SidebarController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.PackageController.registerClass('Open.Testing.Controllers.PackageController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Models.PackageLoader.registerClass('Open.Testing.Models.PackageLoader', Open.Core.Package);
Open.Testing.Models.CommonButtons.registerClass('Open.Testing.Models.CommonButtons', Open.Core.ModelBase);
Open.Testing.Models.CustomListItem.registerClass('Open.Testing.Models.CustomListItem', Open.Core.Lists.ListItem, Open.Core.IViewFactory);
Open.Testing.Models.MethodListItem.registerClass('Open.Testing.Models.MethodListItem', Open.Core.Lists.ListItem);
Open.Testing.Models.ClassListItem.registerClass('Open.Testing.Models.ClassListItem', Open.Core.Lists.ListItem);
Open.Testing.Models.MethodInfo.registerClass('Open.Testing.Models.MethodInfo', Open.Core.ModelBase);
Open.Testing.Models.PackageInfo.registerClass('Open.Testing.Models.PackageInfo', Open.Core.ModelBase, ss.IEnumerable);
Open.Testing.Models.ClassInfo.registerClass('Open.Testing.Models.ClassInfo', Open.Core.ModelBase, ss.IEnumerable);
Open.Testing.Models.PackageListItem.registerClass('Open.Testing.Models.PackageListItem', Open.Core.Lists.ListItem);
Open.Testing.Views.AddPackageListItemView.registerClass('Open.Testing.Views.AddPackageListItemView', Open.Testing.TestHarnessViewBase);
Open.Testing.Views.AddPackageView.registerClass('Open.Testing.Views.AddPackageView', Open.Testing.TestHarnessViewBase);
Open.Testing.Views.ControlHostView.registerClass('Open.Testing.Views.ControlHostView', Open.Testing.TestHarnessViewBase);
Open.Testing.Views.ControlWrapperView.registerClass('Open.Testing.Views.ControlWrapperView', Open.Testing.TestHarnessViewBase);
Open.Testing.Views.LogContainerView.registerClass('Open.Testing.Views.LogContainerView', Open.Testing.TestHarnessViewBase);
Open.Testing.Views.ShellView.registerClass('Open.Testing.Views.ShellView', Open.Testing.TestHarnessViewBase);
Open.Testing.Views.SidebarView.registerClass('Open.Testing.Views.SidebarView', Open.Testing.TestHarnessViewBase);
Open.Testing.Views.MethodListView.registerClass('Open.Testing.Views.MethodListView', Open.Testing.TestHarnessViewBase);
Open.Testing.CssSelectors.root = '#testHarness';
Open.Testing.CssSelectors.sidebar = '#testHarnessSidebar';
Open.Testing.CssSelectors.sidebarContent = '#testHarnessSidebar .th-content';
Open.Testing.CssSelectors.sidebarRootList = '#testHarnessSidebar .th-sidebarRootList';
Open.Testing.CssSelectors.sidebarToolbar = '#testHarnessSidebar div.th-toolbar';
Open.Testing.CssSelectors.backMask = '#testHarnessSidebar img.th-backMask';
Open.Testing.CssSelectors.methodList = '#testHarnessSidebar .th-testList';
Open.Testing.CssSelectors.methodListTitlebar = '#testHarnessSidebar .th-testList-tb';
Open.Testing.CssSelectors.methodListContent = '#testHarnessSidebar .th-testList-content';
Open.Testing.CssSelectors.methodListButtons = '#testHarnessSidebar .th-testList .buttons';
Open.Testing.CssSelectors.methodListRunButton = '#testHarnessSidebar .th-testList button.runTests';
Open.Testing.CssSelectors.methodListRefreshButton = '#testHarnessSidebar .th-testList button.refresh';
Open.Testing.CssSelectors.main = '#testHarness .th-main';
Open.Testing.CssSelectors.mainContent = '#testHarness .th-main .th-content';
Open.Testing.CssSelectors.controlHost = '#testHarness .th-main .th-content .th-controlHost';
Open.Testing.CssSelectors.logContainer = '#testHarnessLog';
Open.Testing.CssSelectors.logTitlebar = '#testHarnessLog .th-log-tb';
Open.Testing.CssSelectors.logControl = '#testHarnessLog .c_log';
Open.Testing.CssSelectors.logClearButton = '#testHarnessLog .th-log-tb .button.clear';
Open.Testing.CssSelectors.addPackageInnerSlide = '#testHarness div.th_addPackage div.innerSlide';
Open.Testing.CssSelectors.addPackageTxtScript = '.field_set_scriptUrl > input';
Open.Testing.CssSelectors.addPackageTxtMethod = '.field_set_initMethod > input';
Open.Testing.CssSelectors.addPackageButtons = '#testHarness .th_addPackage div.buttons';
Open.Testing.CssSelectors.addPackageBtnAdd = Open.Testing.CssSelectors.addPackageButtons + ' .button.add';
Open.Testing.CssSelectors.addPackageBtnCancel = Open.Testing.CssSelectors.addPackageButtons + ' .button.cancel';
Open.Testing.Elements.root = 'testHarness';
Open.Testing.Elements.outputLog = 'testHarnessLog';
Open.Testing.StringLibrary.add = 'Add';
Open.Testing.StringLibrary.cancel = 'Cancel';
Open.Testing._methodHelper.keyConstructor = 'constructor';
Open.Testing._methodHelper.keyClassInitialize = 'classInitialize';
Open.Testing._methodHelper.keyClassCleanup = 'classCleanup';
Open.Testing._methodHelper.keyTestInitialize = 'testInitialize';
Open.Testing._methodHelper.keyTestCleanup = 'testCleanup';
Open.Testing.Application.publicDomain = 'http://TestHarness.org';
Open.Testing.Application._shell = null;
Open.Testing.Application._container = null;
Open.Testing.Application._sidebarController = null;
Open.Testing.Application._controlHostController = null;
Open.Testing.Application._logController = null;
Open.Testing.Application._addPackageController = null;
Open.Testing.Controllers.AddPackageController._minHeight$4 = 450;
Open.Testing.Controllers.LogController._slideDuration$4 = 0.3;
Open.Testing.Controllers.PanelResizeController._sidebarMinWidth$4 = 200;
Open.Testing.Controllers.PanelResizeController._sidebarMaxWidthMargin$4 = 80;
Open.Testing.Controllers.PanelResizeController.logMinHeight = 32;
Open.Testing.Controllers.PanelResizeController.logMaxHeightMargin = 80;
Open.Testing.Controllers.PackageController.propSelectedClass = 'SelectedClass';
Open.Testing.Controllers.PackageController._loadTimeout$4 = 10;
Open.Testing.Models.MethodInfo.keyGetter = 'get_';
Open.Testing.Models.MethodInfo.keySetter = 'set_';
Open.Testing.Models.MethodInfo.keyField = '_';
Open.Testing.Models.MethodInfo.keyFunction = 'function';
Open.Testing.Models.PackageInfo._singletons$2 = [];
Open.Testing.Models.ClassInfo._singletons$2 = null;
Open.Testing.Views.AddPackageListItemView._itemHeight$4 = 34;
Open.Testing.Views.AddPackageView.__showing$4 = null;
Open.Testing.Views.AddPackageView.__hidden$4 = null;
Open.Testing.Views.AddPackageView._contentUrl$4 = '/Open.Core/TestHarness/AddPackage/';
Open.Testing.Views.AddPackageView.iconJs = Open.Core.Helper.get_url().prependDomain('/Open.Core/TestHarness/Images/Icon.JavaScript.png');
Open.Testing.Views.AddPackageView.iconMethod = Open.Core.Helper.get_url().prependDomain(Open.Core.ImagePaths.apiIconPath + 'Method.png');
Open.Testing.Views.AddPackageView._slideDuration$4 = 0.25;
Open.Testing.Views.ControlWrapperView._fillMargin$4 = 30;
Open.Testing.Views.LogContainerView._buttonHeight$4 = 32;
Open.Testing.Views.SidebarView.slideDuration = 0.2;
Open.Testing.Views.SidebarView.propIsTestListVisible = 'IsTestListVisible';
Open.Testing.Views.MethodListView.propClassInfo = 'ClassInfo';
Open.Testing.Views.MethodListView.propSelectedMethod = 'SelectedMethod';
Open.Testing.Views.MethodListView._buttonHeight$4 = 33;

}
ss.loader.registerScript('TestHarness.Script3', ['Open.Core.Views','Open.Core2','Open.Core.Lists'], executeScript);
})();
