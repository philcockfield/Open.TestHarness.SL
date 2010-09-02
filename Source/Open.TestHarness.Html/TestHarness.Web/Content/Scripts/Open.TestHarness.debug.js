//! Open.TestHarness.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.TestHarness');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.CssSelectors

Open.TestHarness.CssSelectors = function Open_TestHarness_CssSelectors() {
    /// <summary>
    /// Constants for common CSS selectors.
    /// </summary>
    /// <field name="sidebar" type="String" static="true">
    /// </field>
    /// <field name="sidebarList" type="String" static="true">
    /// </field>
    /// <field name="sidebarToolbar" type="String" static="true">
    /// </field>
    /// <field name="homeButton" type="String" static="true">
    /// </field>
    /// <field name="main" type="String" static="true">
    /// </field>
    /// <field name="logTitlebar" type="String" static="true">
    /// </field>
    /// <field name="log" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Elements

Open.TestHarness.Elements = function Open_TestHarness_Elements() {
    /// <summary>
    /// Constants for element IDs.
    /// </summary>
    /// <field name="root" type="String" static="true">
    /// </field>
    /// <field name="outputLog" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.TestPackageDef

Open.TestHarness.TestPackageDef = function Open_TestHarness_TestPackageDef(packageType) {
    /// <summary>
    /// Represents a package of tests from a single JavaScript file.
    /// </summary>
    /// <param name="packageType" type="Type">
    /// The Type representing the test-package (normally the 'Application' class).
    /// </param>
    /// <field name="_singletons" type="Array" static="true">
    /// </field>
    /// <field name="_classes" type="Array">
    /// </field>
    /// <field name="_packageType" type="Type">
    /// </field>
    this._classes = [];
    this._packageType = packageType;
}
Open.TestHarness.TestPackageDef.get_singletons = function Open_TestHarness_TestPackageDef$get_singletons() {
    /// <summary>
    /// Gets the collection of singleton TestPackageDef instances.
    /// </summary>
    /// <value type="Array"></value>
    return Open.TestHarness.TestPackageDef._singletons;
}
Open.TestHarness.TestPackageDef.getSingleton = function Open_TestHarness_TestPackageDef$getSingleton(testPackage) {
    /// <summary>
    /// Retrieves the singleton instance of the definition for the given package type.
    /// </summary>
    /// <param name="testPackage" type="Type">
    /// The Type representing the test-package (normally the 'Application' class).
    /// </param>
    /// <returns type="Open.TestHarness.TestPackageDef"></returns>
    var typeName = testPackage.get_fullName();
    var $enum1 = ss.IEnumerator.getEnumerator(Open.TestHarness.TestPackageDef._singletons);
    while ($enum1.moveNext()) {
        var item = $enum1.get_current();
        if (item.get_packageType().get_fullName() === typeName) {
            return item;
        }
    }
    var def = new Open.TestHarness.TestPackageDef(testPackage);
    Open.TestHarness.TestPackageDef._singletons.add(def);
    return def;
}
Open.TestHarness.TestPackageDef.prototype = {
    _packageType: null,
    
    get_packageType: function Open_TestHarness_TestPackageDef$get_packageType() {
        /// <summary>
        /// Gets the Type representing the test-package (normally the 'Application' class).
        /// </summary>
        /// <value type="Type"></value>
        return this._packageType;
    },
    
    get_count: function Open_TestHarness_TestPackageDef$get_count() {
        /// <summary>
        /// Gets the number of test classes within the package.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._classes.length;
    },
    
    getEnumerator: function Open_TestHarness_TestPackageDef$getEnumerator() {
        /// <returns type="ss.IEnumerator"></returns>
        return this._classes.getEnumerator();
    },
    
    addClass: function Open_TestHarness_TestPackageDef$addClass(testClass) {
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
        this._classes.add(Open.TestHarness.TestClassDef.getSingleton(testClass));
    },
    
    contains: function Open_TestHarness_TestPackageDef$contains(testClass) {
        /// <summary>
        /// Determines whether the test-class has already been added to the package.
        /// </summary>
        /// <param name="testClass" type="Type">
        /// The type of the test class.
        /// </param>
        /// <returns type="Boolean"></returns>
        return this.getTestClassDef(testClass) != null;
    },
    
    getTestClassDef: function Open_TestHarness_TestPackageDef$getTestClassDef(testClass) {
        /// <summary>
        /// Determines whether the test-class has already been added to the package.
        /// </summary>
        /// <param name="testClass" type="Type">
        /// The type of the test class.
        /// </param>
        /// <returns type="Open.TestHarness.TestClassDef"></returns>
        if (testClass == null) {
            return null;
        }
        var typeName = testClass.get_fullName();
        var $enum1 = ss.IEnumerator.getEnumerator(this._classes);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            if (item.get_type().get_fullName() === typeName) {
                return item;
            }
        }
        return null;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.TestClassDef

Open.TestHarness.TestClassDef = function Open_TestHarness_TestClassDef(type) {
    /// <summary>
    /// Represents a class with tests.
    /// </summary>
    /// <param name="type" type="Type">
    /// The type of the test class.
    /// </param>
    /// <field name="_singletons" type="Object" static="true">
    /// </field>
    /// <field name="_type" type="Type">
    /// </field>
    this._type = type;
}
Open.TestHarness.TestClassDef.getSingleton = function Open_TestHarness_TestClassDef$getSingleton(testClass) {
    /// <summary>
    /// Retrieves the singleton instance of the definition for the given package type.
    /// </summary>
    /// <param name="testClass" type="Type">
    /// The Type of the test class.
    /// </param>
    /// <returns type="Open.TestHarness.TestClassDef"></returns>
    if (Open.TestHarness.TestClassDef._singletons == null) {
        Open.TestHarness.TestClassDef._singletons = {};
    }
    var key = testClass.get_fullName();
    if (Object.keyExists(Open.TestHarness.TestClassDef._singletons, key)) {
        return Type.safeCast(Open.TestHarness.TestClassDef._singletons[key], Open.TestHarness.TestClassDef);
    }
    var def = new Open.TestHarness.TestClassDef(testClass);
    Open.TestHarness.TestClassDef._singletons[key] = def;
    return def;
}
Open.TestHarness.TestClassDef.prototype = {
    _type: null,
    
    get_type: function Open_TestHarness_TestClassDef$get_type() {
        /// <summary>
        /// Gets the type of the test class.
        /// </summary>
        /// <value type="Type"></value>
        return this._type;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness._globalEventController

Open.TestHarness._globalEventController = function Open_TestHarness__globalEventController() {
    /// <summary>
    /// Listens for global events within the environment.
    /// </summary>
    Open.Core.TestHarness.add_testClassRegistered(ss.Delegate.create(this, this._onTestClassRegistered));
}
Open.TestHarness._globalEventController.prototype = {
    
    _onTestClassRegistered: function Open_TestHarness__globalEventController$_onTestClassRegistered(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Core.TestClassEventArgs">
        /// </param>
        if (e.testPackage == null) {
            return;
        }
        var def = Open.TestHarness.TestPackageDef.getSingleton(e.testPackage);
        def.addClass(e.testClass);
        Open.Core.Log.info('!! From Event');
        Open.Core.Log.info('Class: ' + e.testClass.get_fullName());
        Open.Core.Log.info('def.Count: ' + def.get_count());
        var $enum1 = ss.IEnumerator.getEnumerator(def);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            Open.Core.Log.info('> ' + item.get_type().get_name());
        }
        Open.Core.Log.lineBreak();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness._testPackageLoader

Open.TestHarness._testPackageLoader = function Open_TestHarness__testPackageLoader(scriptUrl, initMethod) {
    /// <summary>
    /// Handles loading a test-package and executing the entry point assembly.
    /// </summary>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <field name="_scriptUrl" type="String">
    /// </field>
    /// <field name="_initMethod" type="String">
    /// </field>
    /// <field name="_isLoaded" type="Boolean">
    /// </field>
    /// <field name="_error" type="Error">
    /// </field>
    this._scriptUrl = scriptUrl;
    this._initMethod = initMethod;
}
Open.TestHarness._testPackageLoader.prototype = {
    _scriptUrl: null,
    _initMethod: null,
    _isLoaded: false,
    _error: null,
    
    get_isLoaded: function Open_TestHarness__testPackageLoader$get_isLoaded() {
        /// <summary>
        /// Gets whether the script has been loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isLoaded;
    },
    
    get_error: function Open_TestHarness__testPackageLoader$get_error() {
        /// <summary>
        /// Gets the error (if any) that occured during the Load operation.
        /// </summary>
        /// <value type="Error"></value>
        return this._error;
    },
    
    get_succeeded: function Open_TestHarness__testPackageLoader$get_succeeded() {
        /// <summary>
        /// Gets or sets whether the load operation failed.
        /// </summary>
        /// <value type="Boolean"></value>
        return this.get_error() == null;
    },
    
    load: function Open_TestHarness__testPackageLoader$load(onComplete) {
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
        $.getScript(this._scriptUrl, ss.Delegate.create(this, function(data) {
            try {
                eval(this._initMethod + '();');
            }
            catch (e) {
                Open.Core.Log.error(String.format('Failed to initialize the script-file at \'{0}\' with the entry method \'{1}()\'.<br/>Message: {2}', this._scriptUrl, this._initMethod, e.message));
                this._error = e;
            }
            this._isLoaded = true;
            Open.Core.Helper.invokeOrDefault(onComplete);
        }));
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Application

Open.TestHarness.Application = function Open_TestHarness_Application() {
    /// <field name="_resizeController" type="Open.TestHarness.Shell.PanelResizeController" static="true">
    /// </field>
    /// <field name="_sidebarController" type="Open.TestHarness.Sidebar.SidebarController" static="true">
    /// </field>
    /// <field name="_eventController" type="Open.TestHarness._globalEventController" static="true">
    /// </field>
}
Open.TestHarness.Application.main = function Open_TestHarness_Application$main(args) {
    /// <param name="args" type="Object">
    /// </param>
    Open.TestHarness.Application._resizeController = new Open.TestHarness.Shell.PanelResizeController();
    Open.TestHarness.Application._sidebarController = new Open.TestHarness.Sidebar.SidebarController();
    Open.TestHarness.Application._eventController = new Open.TestHarness._globalEventController();
    var logView = new Open.Core.Controls.LogView($(Open.TestHarness.CssSelectors.log).first());
    Open.Core.Log.registerView(logView);
    Open.Core.DelayedAction.invoke(1, function() {
        Open.Core.Log.info('Starting download...');
        var loader = new Open.TestHarness._testPackageLoader('/Content/Scripts/TestHarness.Script.Test.debug.js', 'TestHarness.Test.Application.main');
        loader.load(function() {
            Open.Core.Log.info('Complete.  Succeeded: ' + loader.get_succeeded());
            Open.Core.Log.lineBreak();
        });
    });
}


Type.registerNamespace('Open.TestHarness.Shell');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Shell.PanelResizeController

Open.TestHarness.Shell.PanelResizeController = function Open_TestHarness_Shell_PanelResizeController() {
    /// <summary>
    /// Handles resizing of panels within the shell.
    /// </summary>
    /// <field name="_sidebarMinWidth$2" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_sidebarMaxWidthMargin$2" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_outputLogMaxHeightMargin$2" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_sideBarResizer$2" type="Open.Core.UI.HorizontalPanelResizer">
    /// </field>
    /// <field name="_outputResizer$2" type="Open.Core.UI.VerticalPanelResizer">
    /// </field>
    Open.TestHarness.Shell.PanelResizeController.initializeBase(this);
    this._sideBarResizer$2 = new Open.Core.UI.HorizontalPanelResizer(Open.TestHarness.CssSelectors.sidebar, 'TH_SB');
    this._sideBarResizer$2.add_resized(ss.Delegate.create(this, function() {
        Open.TestHarness.Shell.PanelResizeController._syncMainPanelWidth$2();
    }));
    this._sideBarResizer$2.set_minWidth(Open.TestHarness.Shell.PanelResizeController._sidebarMinWidth$2);
    this._sideBarResizer$2.set_maxWidthMargin(Open.TestHarness.Shell.PanelResizeController._sidebarMaxWidthMargin$2);
    Open.TestHarness.Shell.PanelResizeController._initializeResizer$2(this._sideBarResizer$2);
    Open.TestHarness.Shell.PanelResizeController._syncMainPanelWidth$2();
    this._outputResizer$2 = new Open.Core.UI.VerticalPanelResizer(Open.Core.Css.toId(Open.TestHarness.Elements.outputLog), 'TH_OL');
    this._outputResizer$2.add_resized(ss.Delegate.create(this, function() {
    }));
    this._outputResizer$2.set_minHeight($(Open.TestHarness.CssSelectors.logTitlebar).height());
    this._outputResizer$2.set_maxHeightMargin(Open.TestHarness.Shell.PanelResizeController._outputLogMaxHeightMargin$2);
    Open.TestHarness.Shell.PanelResizeController._initializeResizer$2(this._outputResizer$2);
}
Open.TestHarness.Shell.PanelResizeController._initializeResizer$2 = function Open_TestHarness_Shell_PanelResizeController$_initializeResizer$2(resizer) {
    /// <param name="resizer" type="Open.Core.UI.PanelResizerBase">
    /// </param>
    resizer.set_rootContainerId(Open.TestHarness.Elements.root);
    resizer.initialize();
}
Open.TestHarness.Shell.PanelResizeController._syncMainPanelWidth$2 = function Open_TestHarness_Shell_PanelResizeController$_syncMainPanelWidth$2() {
    $(Open.TestHarness.CssSelectors.main).css(Open.Core.Css.left, $(Open.TestHarness.CssSelectors.sidebar).width() + 1 + Open.Core.Css.px);
}
Open.TestHarness.Shell.PanelResizeController.prototype = {
    _sideBarResizer$2: null,
    _outputResizer$2: null
}


Type.registerNamespace('Open.TestHarness.Sidebar');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Sidebar.SidebarController

Open.TestHarness.Sidebar.SidebarController = function Open_TestHarness_Sidebar_SidebarController() {
    /// <summary>
    /// Controller for the side-bar.
    /// </summary>
    /// <field name="_listTree$2" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_backController$2" type="Open.Core.Lists.ListTreeBackController">
    /// </field>
    Open.TestHarness.Sidebar.SidebarController.initializeBase(this);
    this._listTree$2 = new Open.Core.Lists.ListTreeView($(Open.TestHarness.CssSelectors.sidebarList));
    this._listTree$2.set_slideDuration(0.2);
    this._backController$2 = new Open.Core.Lists.ListTreeBackController(this._listTree$2, $(Open.TestHarness.CssSelectors.sidebarToolbar), $(Open.TestHarness.CssSelectors.homeButton));
    this._TEMP$2();
}
Open.TestHarness.Sidebar.SidebarController.prototype = {
    _listTree$2: null,
    _backController$2: null,
    
    onDisposed: function Open_TestHarness_Sidebar_SidebarController$onDisposed() {
        this._backController$2.dispose();
        Open.TestHarness.Sidebar.SidebarController.callBaseMethod(this, 'onDisposed');
    },
    
    _TEMP$2: function Open_TestHarness_Sidebar_SidebarController$_TEMP$2() {
        var rootNode = new Open.TestHarness.Sidebar.MyNode('Root');
        rootNode.add(new Open.TestHarness.Sidebar.MyNode('Recent'));
        rootNode.add(new Open.TestHarness.Sidebar.MyNode('Child 2'));
        rootNode.add(new Open.TestHarness.Sidebar.MyNode('Child 3'));
        var child1 = Type.safeCast(rootNode.childAt(0), Open.TestHarness.Sidebar.MyNode);
        var child2 = (rootNode.childAt(1));
        var child3 = (rootNode.childAt(2));
        child1.add(new Open.TestHarness.Sidebar.MyNode('Recent Child 1'));
        child1.add(new Open.TestHarness.Sidebar.MyNode('Recent Child 2'));
        child1.add(new Open.TestHarness.Sidebar.MyNode('Recent Child 3'));
        var recent1 = Type.safeCast(child1.childAt(0), Open.TestHarness.Sidebar.MyNode);
        recent1.add(new Open.TestHarness.Sidebar.MyNode('Recent Grandchild 1'));
        recent1.add(new Open.TestHarness.Sidebar.MyNode('Recent Grandchild 2'));
        recent1.add(new Open.TestHarness.Sidebar.MyNode('Recent Grandchild 3'));
        child2.add(new Open.TestHarness.Sidebar.MyNode('Yo Child'));
        child3.add(new Open.TestHarness.Sidebar.MyNode('Yo Child'));
        this._listTree$2.set_rootNode(rootNode);
        child1.set_text('My Recent Foo');
        child2.set_canSelect(false);
        child2.set_isSelected(true);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Sidebar.MyNode

Open.TestHarness.Sidebar.MyNode = function Open_TestHarness_Sidebar_MyNode(text) {
    /// <param name="text" type="String">
    /// </param>
    Open.TestHarness.Sidebar.MyNode.initializeBase(this);
    this.set_text(text);
}


Open.TestHarness.CssSelectors.registerClass('Open.TestHarness.CssSelectors');
Open.TestHarness.Elements.registerClass('Open.TestHarness.Elements');
Open.TestHarness.TestPackageDef.registerClass('Open.TestHarness.TestPackageDef', null, ss.IEnumerable);
Open.TestHarness.TestClassDef.registerClass('Open.TestHarness.TestClassDef');
Open.TestHarness._globalEventController.registerClass('Open.TestHarness._globalEventController');
Open.TestHarness._testPackageLoader.registerClass('Open.TestHarness._testPackageLoader');
Open.TestHarness.Application.registerClass('Open.TestHarness.Application');
Open.TestHarness.Shell.PanelResizeController.registerClass('Open.TestHarness.Shell.PanelResizeController', Open.Core.ControllerBase);
Open.TestHarness.Sidebar.SidebarController.registerClass('Open.TestHarness.Sidebar.SidebarController', Open.Core.ControllerBase);
Open.TestHarness.Sidebar.MyNode.registerClass('Open.TestHarness.Sidebar.MyNode', Open.Core.Lists.ListItem);
Open.TestHarness.CssSelectors.sidebar = '#testHarnessSidebar';
Open.TestHarness.CssSelectors.sidebarList = '#testHarnessSidebar .th-sidebarList';
Open.TestHarness.CssSelectors.sidebarToolbar = '#testHarnessSidebar div.th-toolbar';
Open.TestHarness.CssSelectors.homeButton = '#testHarnessSidebar img.th-home';
Open.TestHarness.CssSelectors.main = '#testHarness .th-main';
Open.TestHarness.CssSelectors.logTitlebar = '#testHarnessLog .th-log-titlebar';
Open.TestHarness.CssSelectors.log = '#testHarnessLog .coreLog';
Open.TestHarness.Elements.root = 'testHarness';
Open.TestHarness.Elements.outputLog = 'testHarnessLog';
Open.TestHarness.TestPackageDef._singletons = [];
Open.TestHarness.TestClassDef._singletons = null;
Open.TestHarness.Application._resizeController = null;
Open.TestHarness.Application._sidebarController = null;
Open.TestHarness.Application._eventController = null;
Open.TestHarness.Shell.PanelResizeController._sidebarMinWidth$2 = 200;
Open.TestHarness.Shell.PanelResizeController._sidebarMaxWidthMargin$2 = 80;
Open.TestHarness.Shell.PanelResizeController._outputLogMaxHeightMargin$2 = 80;

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('TestHarness.Script3', ['Open.Core.Script','Open.Core.Views','Open.Core.Lists'], executeScript);
})();
