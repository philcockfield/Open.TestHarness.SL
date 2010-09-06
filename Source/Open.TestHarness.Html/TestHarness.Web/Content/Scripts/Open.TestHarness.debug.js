//! Open.TestHarness.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Testing');

////////////////////////////////////////////////////////////////////////////////
// Open.Testing.TestHarnessViewBase

Open.Testing.TestHarnessViewBase = function Open_Testing_TestHarnessViewBase() {
    /// <summary>
    /// The base class for views within the TestHarness.
    /// </summary>
    /// <field name="_common$2" type="Open.Testing.Common">
    /// </field>
    Open.Testing.TestHarnessViewBase.initializeBase(this);
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
    /// <field name="root" type="String" static="true">
    /// </field>
    /// <field name="sidebar" type="String" static="true">
    /// </field>
    /// <field name="sidebarRootList" type="String" static="true">
    /// </field>
    /// <field name="sidebarToolbar" type="String" static="true">
    /// </field>
    /// <field name="backMask" type="String" static="true">
    /// </field>
    /// <field name="testList" type="String" static="true">
    /// </field>
    /// <field name="testListContent" type="String" static="true">
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
    /// <field name="__methodClicked" type="Open.Testing.MethodEventHandler">
    /// </field>
}
Open.Testing.TestHarnessEvents.prototype = {
    
    add_testClassRegistered: function Open_Testing_TestHarnessEvents$add_testClassRegistered(value) {
        /// <summary>
        /// Fires when a test class is registered.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__testClassRegistered = ss.Delegate.combine(this.__testClassRegistered, value);
    },
    remove_testClassRegistered: function Open_Testing_TestHarnessEvents$remove_testClassRegistered(value) {
        /// <summary>
        /// Fires when a test class is registered.
        /// </summary>
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
        /// <summary>
        /// Fires when a control is added to the host canvas.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__controlAdded = ss.Delegate.combine(this.__controlAdded, value);
    },
    remove_controlAdded: function Open_Testing_TestHarnessEvents$remove_controlAdded(value) {
        /// <summary>
        /// Fires when a control is added to the host canvas.
        /// </summary>
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
        /// <summary>
        /// Fires when the host canvas is to be cleared of controls.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__clearControls = ss.Delegate.combine(this.__clearControls, value);
    },
    remove_clearControls: function Open_Testing_TestHarnessEvents$remove_clearControls(value) {
        /// <summary>
        /// Fires when the host canvas is to be cleared of controls.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__clearControls = ss.Delegate.remove(this.__clearControls, value);
    },
    
    __clearControls: null,
    
    fireClearControls: function Open_Testing_TestHarnessEvents$fireClearControls() {
        if (this.__clearControls != null) {
            this.__clearControls.invoke(this, new ss.EventArgs());
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
    
    _fireMethodClicked: function Open_Testing_TestHarnessEvents$_fireMethodClicked(e) {
        /// <param name="e" type="Open.Testing.MethodEventArgs">
        /// </param>
        if (this.__methodClicked != null) {
            this.__methodClicked.invoke(this, e);
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
    /// <field name="_controlHostController" type="Open.Testing.Controllers.ControlHostController" static="true">
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
    /// <param name="args" type="Object">
    /// </param>
    Open.Testing.Application.get_container().registerSingleton(Open.Testing.Internal.ITestHarnessEvents, new Open.Testing.TestHarnessEvents());
    Open.Testing.Application.get_container().registerSingleton(Open.Testing.Common, new Open.Testing.Common());
    var logView = new Open.Core.Controls.LogView($(Open.Testing.CssSelectors.log).first());
    Open.Core.Log.registerView(logView);
    Open.Testing.Application._shell = new Open.Testing.Views.ShellView($(Open.Testing.CssSelectors.root));
    Open.Testing.Application.get_container().registerSingleton(Open.Testing.Views.ShellView, Open.Testing.Application._shell);
    Open.Testing.Application._resizeController = new Open.Testing.Controllers.PanelResizeController();
    Open.Testing.Application._sidebarController = new Open.Testing.Controllers.SidebarController();
    Open.Testing.Application._controlHostController = new Open.Testing.Controllers.ControlHostController();
    Open.Testing.Application._addTestHarnessPackage();
    Open.Testing.Application._addCorePackage();
}
Open.Testing.Application._addTestHarnessPackage = function Open_Testing_Application$_addTestHarnessPackage() {
    var scriptUrl = '/Content/Scripts/TestHarness.Test.debug.js';
    var initMethod = 'Test.Application.main';
    var testHarnessPackage = Open.Testing.Models.PackageInfo.singletonFromUrl(scriptUrl, initMethod);
    Open.Testing.Application._sidebarController.addPackage(testHarnessPackage);
}
Open.Testing.Application._addCorePackage = function Open_Testing_Application$_addCorePackage() {
    var scriptUrl = '/Content/Scripts/Open.Core.Test.debug.js';
    var initMethod = 'Open.Core.Test.Application.main';
    var testHarnessPackage = Open.Testing.Models.PackageInfo.singletonFromUrl(scriptUrl, initMethod);
    Open.Testing.Application._sidebarController.addPackage(testHarnessPackage);
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
    if (classInfo.get_classInitialize() != null) {
        classInfo.get_classInitialize().invoke();
    }
}
Open.Testing.Controllers.ClassController.prototype = {
    _classInfo$3: null,
    _sidebarView$3: null,
    _events$3: null,
    
    onDisposed: function Open_Testing_Controllers_ClassController$onDisposed() {
        this._events$3.remove_methodClicked(ss.Delegate.create(this, this._onMethodClicked$3));
        if (this._classInfo$3.get_classCleanup() != null) {
            this._classInfo$3.get_classCleanup().invoke();
        }
        Open.Testing.TestHarness.clearControls();
        Open.Testing.Controllers.ClassController.callBaseMethod(this, 'onDisposed');
    },
    
    get__selectedMethod$3: function Open_Testing_Controllers_ClassController$get__selectedMethod$3() {
        /// <value type="Open.Testing.Models.MethodInfo"></value>
        return this._sidebarView$3.get_methodList().get_selectedMethod();
    },
    
    _onMethodClicked$3: function Open_Testing_Controllers_ClassController$_onMethodClicked$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Testing.MethodEventArgs">
        /// </param>
        this.invokeSelectedMethod();
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
        return true;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Controllers.ControlHostController

Open.Testing.Controllers.ControlHostController = function Open_Testing_Controllers_ControlHostController() {
    /// <summary>
    /// Controls the 'Control Host' panel where test-controls are displayed.
    /// </summary>
    /// <field name="_divControlHost$3" type="jQueryObject">
    /// </field>
    /// <field name="_views$3" type="Array">
    /// </field>
    /// <field name="_events$3" type="Open.Testing.TestHarnessEvents">
    /// </field>
    this._views$3 = [];
    Open.Testing.Controllers.ControlHostController.initializeBase(this);
    this._divControlHost$3 = $(Open.Testing.CssSelectors.controlHost);
    this._events$3 = this.get_common().get_events();
    this._events$3.add_controlAdded(ss.Delegate.create(this, this._onControlAdded$3));
    this._events$3.add_clearControls(ss.Delegate.create(this, this._onClearControls$3));
}
Open.Testing.Controllers.ControlHostController.prototype = {
    _divControlHost$3: null,
    _events$3: null,
    
    onDisposed: function Open_Testing_Controllers_ControlHostController$onDisposed() {
        this.clear();
        this._events$3.remove_controlAdded(ss.Delegate.create(this, this._onControlAdded$3));
        this._events$3.remove_clearControls(ss.Delegate.create(this, this._onClearControls$3));
        Open.Testing.Controllers.ControlHostController.callBaseMethod(this, 'onDisposed');
    },
    
    _onControlAdded$3: function Open_Testing_Controllers_ControlHostController$_onControlAdded$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Testing.Internal.TestControlEventArgs">
        /// </param>
        this._addView$3(e.controlContainer);
    },
    
    _onClearControls$3: function Open_Testing_Controllers_ControlHostController$_onClearControls$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.clear();
    },
    
    clear: function Open_Testing_Controllers_ControlHostController$clear() {
        /// <summary>
        /// Clears all views.
        /// </summary>
        Open.Core.Helper.get_collection().disposeAndClear(this._views$3);
    },
    
    _addView$3: function Open_Testing_Controllers_ControlHostController$_addView$3(controlContainer) {
        /// <param name="controlContainer" type="jQueryObject">
        /// </param>
        var view = new Open.Testing.Views.ControlWrapperView(this._divControlHost$3, controlContainer);
        this._views$3.add(view);
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
    Open.Testing.Controllers.PanelResizeController.initializeBase(this);
    this._sideBarResizer$3 = new Open.Core.UI.HorizontalPanelResizer(Open.Testing.CssSelectors.sidebar, 'TH_SB');
    this._sideBarResizer$3.add_resized(ss.Delegate.create(this, function() {
        Open.Testing.Controllers.PanelResizeController._syncMainPanelWidth$3();
    }));
    this._sideBarResizer$3.set_minWidth(Open.Testing.Controllers.PanelResizeController._sidebarMinWidth$3);
    this._sideBarResizer$3.set_maxWidthMargin(Open.Testing.Controllers.PanelResizeController._sidebarMaxWidthMargin$3);
    Open.Testing.Controllers.PanelResizeController._initializeResizer$3(this._sideBarResizer$3);
    this._outputResizer$3 = new Open.Core.UI.VerticalPanelResizer(Open.Core.Css.toId(Open.Testing.Elements.outputLog), 'TH_OL');
    this._outputResizer$3.add_resized(ss.Delegate.create(this, function() {
        Open.Testing.Controllers.PanelResizeController._syncControlHostHeight$3();
    }));
    this._outputResizer$3.set_minHeight(Open.Core.Html.height(Open.Testing.CssSelectors.logTitlebar));
    this._outputResizer$3.set_maxHeightMargin(Open.Testing.Controllers.PanelResizeController._outputLogMaxHeightMargin$3);
    Open.Testing.Controllers.PanelResizeController._initializeResizer$3(this._outputResizer$3);
    Open.Core.GlobalEvents.add_windowResize(ss.Delegate.create(this, function() {
        Open.Testing.Controllers.PanelResizeController._syncControlHostHeight$3();
    }));
    this.updateLayout();
}
Open.Testing.Controllers.PanelResizeController._initializeResizer$3 = function Open_Testing_Controllers_PanelResizeController$_initializeResizer$3(resizer) {
    /// <param name="resizer" type="Open.Core.UI.PanelResizerBase">
    /// </param>
    resizer.set_rootContainerId(Open.Testing.Elements.root);
    resizer.initialize();
}
Open.Testing.Controllers.PanelResizeController._syncMainPanelWidth$3 = function Open_Testing_Controllers_PanelResizeController$_syncMainPanelWidth$3() {
    $(Open.Testing.CssSelectors.main).css(Open.Core.Css.left, (Open.Core.Html.width(Open.Testing.CssSelectors.sidebar) + 1) + Open.Core.Css.px);
}
Open.Testing.Controllers.PanelResizeController._syncControlHostHeight$3 = function Open_Testing_Controllers_PanelResizeController$_syncControlHostHeight$3() {
    var height = Open.Core.Html.height(Open.Testing.CssSelectors.mainContent) - Open.Core.Html.height(Open.Testing.CssSelectors.logContainer);
    $(Open.Testing.CssSelectors.controlHost).css(Open.Core.Css.height, (height - 1) + Open.Core.Css.px);
}
Open.Testing.Controllers.PanelResizeController.prototype = {
    _sideBarResizer$3: null,
    _outputResizer$3: null,
    
    updateLayout: function Open_Testing_Controllers_PanelResizeController$updateLayout() {
        /// <summary>
        /// Updates the layout of the panels.
        /// </summary>
        Open.Testing.Controllers.PanelResizeController._syncMainPanelWidth$3();
        Open.Testing.Controllers.PanelResizeController._syncControlHostHeight$3();
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
    this._packageControllers$3 = [];
    Open.Testing.Controllers.SidebarController.initializeBase(this);
    this._view$3 = this.get_common().get_shell().get_sidebar();
    this._TEMP$3();
}
Open.Testing.Controllers.SidebarController.prototype = {
    _view$3: null,
    
    onDisposed: function Open_Testing_Controllers_SidebarController$onDisposed() {
        this._view$3.dispose();
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
    /// <field name="_selectedClassController$3" type="Open.Testing.Controllers.ClassController">
    /// </field>
    Open.Testing.Controllers.PackageController.initializeBase(this);
    this._rootNode$3 = rootNode;
    this._sidebarView$3 = this.get_common().get_shell().get_sidebar();
    rootNode.add_selectionChanged(ss.Delegate.create(this, this._onSelectionChanged$3));
    rootNode.add_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$3));
    this.get__rootList$3().add_selectedParentChanged(ss.Delegate.create(this, this._onRootListSelectedParentChanged$3));
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
    _selectedClassController$3: null,
    
    onDisposed: function Open_Testing_Controllers_PackageController$onDisposed() {
        this._rootNode$3.remove_selectionChanged(ss.Delegate.create(this, this._onSelectionChanged$3));
        this._rootNode$3.remove_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$3));
        this.get__rootList$3().remove_selectedParentChanged(ss.Delegate.create(this, this._onRootListSelectedParentChanged$3));
        if (this._selectedClassController$3 != null) {
            this._selectedClassController$3.dispose();
        }
        Open.Testing.Controllers.PackageController.callBaseMethod(this, 'onDisposed');
    },
    
    _onRootListSelectedParentChanged$3: function Open_Testing_Controllers_PackageController$_onRootListSelectedParentChanged$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._updateMethodListVisibility$3();
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
        this._updateMethodListVisibility$3();
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
        }
        return value;
    },
    
    get__rootList$3: function Open_Testing_Controllers_PackageController$get__rootList$3() {
        /// <value type="Open.Core.Lists.ListTreeView"></value>
        return this._sidebarView$3.get_rootList();
    },
    
    _download$3: function Open_Testing_Controllers_PackageController$_download$3() {
        if (this.get_testPackage().get_isLoaded()) {
            return;
        }
        var loader = this.get_testPackage().get_loader();
        var link = Open.Core.Html.toHyperlink(loader.get_scriptUrl(), null, Open.Core.LinkTarget.blank);
        var timeout = new Open.Core.DelayedAction(Open.Testing.Controllers.PackageController._loadTimeout$3, ss.Delegate.create(this, function() {
            Open.Core.Log.error(String.format('Failed to download the test-package at \'{0}\'.  Please ensure the file exists.', link));
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
            Open.Core.Log.lineBreak();
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
    },
    
    _updateMethodListVisibility$3: function Open_Testing_Controllers_PackageController$_updateMethodListVisibility$3() {
        var node = Type.safeCast(this.get__rootList$3().get_selectedParent(), Open.Testing.Models.PackageListItem);
        this._sidebarView$3.set_isMethodListVisible((node != null) && Open.Core.Helper.get_tree().hasSelectedChild(node));
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
        var instance = this.get_classInfo().get_instance();
        if (!this.get_isSpecial() && this.get_classInfo().get_testInitialize() != null) {
            this.get_classInfo().get_testInitialize().invoke();
        }
        try {
            var func = Open.Core.Helper.get_reflection().getFunction(instance, this.get_name());
            if (func == null) {
                return;
            }
            func.call(instance);
        }
        catch (error) {
            Open.Core.Log.error(String.format('<b>Exception</b> Failed while executing \'<b>{1}</b>\'.<br/>{0}Message: {2}<br/>{0}Method: {3}<br/>{0}Class: {4}<br/>{0}Package: {5}', Open.Core.Html.spanIndent(30), this.get_displayName(), error.message, Open.Core.Helper.get_string().toCamelCase(this.get_name()), this.get_classInfo().get_classType().get_fullName(), Open.Core.Html.toHyperlink(this.get_classInfo().get_packageInfo().get_loader().get_scriptUrl(), null, Open.Core.LinkTarget.blank)));
        }
        if (!this.get_isSpecial() && this.get_classInfo().get_testCleanup() != null) {
            this.get_classInfo().get_testCleanup().invoke();
        }
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
    this._displayName$1 = Open.Core.Helper.get_string().removeEnd(Open.Testing.Models.MethodInfo.formatName(classType.get_name()), 'Test');
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
            try {
                this._isInitializing$3 = true;
                eval(this._initMethod$3 + '();');
            }
            catch (e) {
                Open.Core.Log.error(String.format('Failed to initialize the script-file at \'{0}\' with the entry method \'{1}()\'.<br/>Message: {2}', this._scriptUrl$3, this._initMethod$3, e.message));
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

Open.Testing.Views.ControlWrapperView = function Open_Testing_Views_ControlWrapperView(divControlHost, controlContainer) {
    /// <summary>
    /// Represents the container for a test-control.
    /// </summary>
    /// <param name="divControlHost" type="jQueryObject">
    /// The control host DIV.
    /// </param>
    /// <param name="controlContainer" type="jQueryObject">
    /// The control content (supplied by the test class. This is the control that is under test).
    /// </param>
    /// <field name="_divRoot$3" type="jQueryObject">
    /// </field>
    /// <field name="_controlContainer$3" type="jQueryObject">
    /// </field>
    Open.Testing.Views.ControlWrapperView.initializeBase(this);
    this._controlContainer$3 = controlContainer;
    this.initialize(divControlHost);
    this._divRoot$3 = Open.Core.Html.createDiv();
    this._divRoot$3.appendTo(divControlHost);
    this._divRoot$3.append('Yo!');
    this._divRoot$3.css('background', 'orange');
    this._divRoot$3.css('border', 'solid 1px black');
}
Open.Testing.Views.ControlWrapperView.prototype = {
    _divRoot$3: null,
    _controlContainer$3: null,
    
    onDisposed: function Open_Testing_Views_ControlWrapperView$onDisposed() {
        this._divRoot$3.remove();
        Open.Testing.Views.ControlWrapperView.callBaseMethod(this, 'onDisposed');
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
    Open.Testing.Views.ShellView.initializeBase(this);
    this.initialize(container);
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
    Open.Testing.Views.SidebarView.initializeBase(this);
    this.initialize(container);
    this._rootList$3 = new Open.Core.Lists.ListTreeView($(Open.Testing.CssSelectors.sidebarRootList));
    this._rootList$3.set_slideDuration(Open.Testing.Views.SidebarView.slideDuration);
    this._methodList$3 = new Open.Testing.Views.MethodListView($(Open.Testing.CssSelectors.testList));
    this._backController$3 = new Open.Core.Lists.ListTreeBackController(this._rootList$3, $(Open.Testing.CssSelectors.sidebarToolbar), $(Open.Testing.CssSelectors.backMask));
    this.updateVisualState();
}
Open.Testing.Views.SidebarView.prototype = {
    _rootList$3: null,
    _backController$3: null,
    _methodList$3: null,
    
    onDisposed: function Open_Testing_Views_SidebarView$onDisposed() {
        this._backController$3.dispose();
        this._rootList$3.dispose();
        Open.Testing.Views.SidebarView.callBaseMethod(this, 'onDisposed');
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
        if (this.set(Open.Testing.Views.SidebarView.propIsTestListVisible, value, false)) {
            if (value) {
                this.showMethodList(null);
            }
            else {
                this.hideMethodList(null);
            }
        }
        return value;
    },
    
    updateVisualState: function Open_Testing_Views_SidebarView$updateVisualState() {
        /// <summary>
        /// Refreshes the visual state.
        /// </summary>
        this._syncRootListHeight$3();
    },
    
    showMethodList: function Open_Testing_Views_SidebarView$showMethodList(onComplete) {
        /// <summary>
        /// Reveals the test list.
        /// </summary>
        /// <param name="onComplete" type="Action">
        /// The action to invoke when complete
        /// </param>
        this.set_isMethodListVisible(true);
        var height = this._getTargetMethodListHeight$3();
        this._animateHeights$3(height, onComplete);
    },
    
    hideMethodList: function Open_Testing_Views_SidebarView$hideMethodList(onComplete) {
        /// <summary>
        /// Hides the test list.
        /// </summary>
        /// <param name="onComplete" type="Action">
        /// The action to invoke when complete
        /// </param>
        this.set_isMethodListVisible(false);
        this._animateHeights$3(0, onComplete);
    },
    
    _animateHeights$3: function Open_Testing_Views_SidebarView$_animateHeights$3(methodListHeight, onComplete) {
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
            Open.Core.Css.setVisible(this.get_methodList().get_container(), true);
        }
        this.get_methodList().updateLayout();
        this._animate$3(isShowing, this.get_methodList().get_container(), methodListProps, null);
        this._animate$3(isShowing, this.get_rootList().get_container(), rootListProps, onComplete);
    },
    
    _animate$3: function Open_Testing_Views_SidebarView$_animate$3(isShowing, div, properties, onComplete) {
        /// <param name="isShowing" type="Boolean">
        /// </param>
        /// <param name="div" type="jQueryObject">
        /// </param>
        /// <param name="properties" type="Object">
        /// </param>
        /// <param name="onComplete" type="Action">
        /// </param>
        div.animate(properties, Open.Core.Helper.get_number().toMsecs(Open.Testing.Views.SidebarView.slideDuration), 'swing', ss.Delegate.create(this, function() {
            if (!isShowing) {
                Open.Core.Css.setVisible(this.get_methodList().get_container(), false);
            }
            Open.Core.Helper.invokeOrDefault(onComplete);
        }));
    },
    
    _syncRootListHeight$3: function Open_Testing_Views_SidebarView$_syncRootListHeight$3() {
        this.get_rootList().get_container().css(Open.Core.Css.bottom, this.get_methodList().get_container().height() + Open.Core.Css.px);
    },
    
    _getTargetMethodListHeight$3: function Open_Testing_Views_SidebarView$_getTargetMethodListHeight$3() {
        /// <returns type="Number" integer="true"></returns>
        return 450;
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
    Open.Testing.Views.MethodListView.initializeBase(this);
    this.initialize(container);
    this._events$3 = this.get_common().get_events();
    this._listView$3 = new Open.Core.Lists.ListTreeView($(Open.Testing.CssSelectors.testListContent));
    this._listView$3.set_slideDuration(Open.Testing.Views.SidebarView.slideDuration);
    this._rootNode$3 = new Open.Core.Lists.ListItem();
    this._listView$3.set_rootNode(this._rootNode$3);
}
Open.Testing.Views.MethodListView.prototype = {
    _listView$3: null,
    _rootNode$3: null,
    _events$3: null,
    
    _onItemClick$3: function Open_Testing_Views_MethodListView$_onItemClick$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.set_selectedMethod((sender).get_method());
        this._events$3._fireMethodClicked(new Open.Testing.MethodEventArgs(this.get_selectedMethod()));
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
    
    updateLayout: function Open_Testing_Views_MethodListView$updateLayout() {
        /// <summary>
        /// Updates the visual state of the control.
        /// </summary>
        this._listView$3.updateLayout();
    },
    
    _populateList$3: function Open_Testing_Views_MethodListView$_populateList$3(cclass) {
        /// <param name="cclass" type="Open.Testing.Models.ClassInfo">
        /// </param>
        this._clearChildren$3();
        if (cclass == null) {
            return;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(cclass);
        while ($enum1.moveNext()) {
            var method = $enum1.get_current();
            this._rootNode$3.addChild(this._createListItem$3(method));
        }
    },
    
    _createListItem$3: function Open_Testing_Views_MethodListView$_createListItem$3(method) {
        /// <param name="method" type="Open.Testing.Models.MethodInfo">
        /// </param>
        /// <returns type="Open.Testing.Models.MethodListItem"></returns>
        var item = new Open.Testing.Models.MethodListItem(method);
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
Open.Testing.Elements.registerClass('Open.Testing.Elements');
Open.Testing.TestHarnessEvents.registerClass('Open.Testing.TestHarnessEvents', null, Open.Testing.Internal.ITestHarnessEvents);
Open.Testing.MethodEventArgs.registerClass('Open.Testing.MethodEventArgs');
Open.Testing.Common.registerClass('Open.Testing.Common');
Open.Testing._methodHelper.registerClass('Open.Testing._methodHelper');
Open.Testing.Application.registerClass('Open.Testing.Application');
Open.Testing.Controllers.ClassController.registerClass('Open.Testing.Controllers.ClassController', Open.Testing.TestHarnessControllerBase);
Open.Testing.Controllers.ControlHostController.registerClass('Open.Testing.Controllers.ControlHostController', Open.Testing.TestHarnessControllerBase);
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
Open.Testing.CssSelectors.root = '#testHarness';
Open.Testing.CssSelectors.sidebar = '#testHarnessSidebar';
Open.Testing.CssSelectors.sidebarRootList = '#testHarnessSidebar .th-sidebarRootList';
Open.Testing.CssSelectors.sidebarToolbar = '#testHarnessSidebar div.th-toolbar';
Open.Testing.CssSelectors.backMask = '#testHarnessSidebar img.th-backMask';
Open.Testing.CssSelectors.testList = '#testHarnessSidebar .th-testList';
Open.Testing.CssSelectors.testListContent = '#testHarnessSidebar .th-testList-content';
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
Open.Testing.Application._controlHostController = null;
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
