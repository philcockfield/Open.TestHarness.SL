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
    /// <field name="backMask" type="String" static="true">
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
// Open.TestHarness.Application

Open.TestHarness.Application = function Open_TestHarness_Application() {
    /// <field name="_resizeController" type="Open.TestHarness.Controllers.PanelResizeController" static="true">
    /// </field>
    /// <field name="_sidebarController" type="Open.TestHarness.Controllers.SidebarController" static="true">
    /// </field>
}
Open.TestHarness.Application.main = function Open_TestHarness_Application$main(args) {
    /// <param name="args" type="Object">
    /// </param>
    var logView = new Open.Core.Controls.LogView($(Open.TestHarness.CssSelectors.log).first());
    Open.Core.Log.registerView(logView);
    Open.TestHarness.Application._resizeController = new Open.TestHarness.Controllers.PanelResizeController();
    Open.TestHarness.Application._sidebarController = new Open.TestHarness.Controllers.SidebarController();
    var scriptUrl = '/Content/Scripts/TestHarness.Script.Test.debug.js';
    var initMethod = 'TestHarness.Test.Application.main';
    var packageDef = Open.TestHarness.Models.TestPackageDef.singletonFromUrl(scriptUrl, initMethod);
    Open.TestHarness.Application._sidebarController.addPackage(packageDef);
}


Type.registerNamespace('Open.TestHarness.Models');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Models.TestClassListItem

Open.TestHarness.Models.TestClassListItem = function Open_TestHarness_Models_TestClassListItem(testClass) {
    /// <summary>
    /// A list-item node for a TestClass.
    /// </summary>
    /// <param name="testClass" type="Open.TestHarness.Models.TestClassDef">
    /// The test-class this node represents.
    /// </param>
    /// <field name="_testClass$3" type="Open.TestHarness.Models.TestClassDef">
    /// </field>
    Open.TestHarness.Models.TestClassListItem.initializeBase(this);
    this._testClass$3 = testClass;
    this.set_text(testClass.get_type().get_name());
}
Open.TestHarness.Models.TestClassListItem.prototype = {
    _testClass$3: null,
    
    get_testClass: function Open_TestHarness_Models_TestClassListItem$get_testClass() {
        /// <summary>
        /// Gets the test-package this node represents.
        /// </summary>
        /// <value type="Open.TestHarness.Models.TestClassDef"></value>
        return this._testClass$3;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Models.TestPackageDef

Open.TestHarness.Models.TestPackageDef = function Open_TestHarness_Models_TestPackageDef(scriptUrl, initMethod) {
    /// <summary>
    /// Represents a package of tests from a single JavaScript file.
    /// </summary>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <field name="_singletons" type="Array" static="true">
    /// </field>
    /// <field name="_classes" type="Array">
    /// </field>
    /// <field name="_loader" type="Open.TestHarness.Models.TestPackageLoader">
    /// </field>
    this._classes = [];
    this._loader = new Open.TestHarness.Models.TestPackageLoader(this, scriptUrl.toLowerCase(), initMethod);
}
Open.TestHarness.Models.TestPackageDef.get_singletons = function Open_TestHarness_Models_TestPackageDef$get_singletons() {
    /// <summary>
    /// Gets the collection of singleton TestPackageDef instances.
    /// </summary>
    /// <value type="Array"></value>
    return Open.TestHarness.Models.TestPackageDef._singletons;
}
Open.TestHarness.Models.TestPackageDef.singletonFromUrl = function Open_TestHarness_Models_TestPackageDef$singletonFromUrl(scriptUrl, initMethod) {
    /// <summary>
    /// Retrieves (or creates) the singleton instance of the definition for the given package type.
    /// </summary>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <returns type="Open.TestHarness.Models.TestPackageDef"></returns>
    var def = Type.safeCast(Open.Core.Helper.get_collection().first(Open.TestHarness.Models.TestPackageDef._singletons, function(o) {
        return (o).get_id() === scriptUrl.toLowerCase();
    }), Open.TestHarness.Models.TestPackageDef);
    if (def == null) {
        def = new Open.TestHarness.Models.TestPackageDef(scriptUrl, initMethod);
        Open.TestHarness.Models.TestPackageDef._singletons.add(def);
    }
    return def;
}
Open.TestHarness.Models.TestPackageDef.prototype = {
    _loader: null,
    
    get_id: function Open_TestHarness_Models_TestPackageDef$get_id() {
        /// <summary>
        /// Gets the unique ID of the package.
        /// </summary>
        /// <value type="String"></value>
        return this.get_loader().get_scriptUrl();
    },
    
    get_loader: function Open_TestHarness_Models_TestPackageDef$get_loader() {
        /// <summary>
        /// Gets the package loader.
        /// </summary>
        /// <value type="Open.TestHarness.Models.TestPackageLoader"></value>
        return this._loader;
    },
    
    get_isLoaded: function Open_TestHarness_Models_TestPackageDef$get_isLoaded() {
        /// <summary>
        /// Gets or sets whether the package has been loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        return this.get_loader().get_isLoaded();
    },
    
    get_count: function Open_TestHarness_Models_TestPackageDef$get_count() {
        /// <summary>
        /// Gets the number of test classes within the package.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._classes.length;
    },
    
    getEnumerator: function Open_TestHarness_Models_TestPackageDef$getEnumerator() {
        /// <returns type="ss.IEnumerator"></returns>
        return this._classes.getEnumerator();
    },
    
    addClass: function Open_TestHarness_Models_TestPackageDef$addClass(testClass) {
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
        this._classes.add(Open.TestHarness.Models.TestClassDef.getSingleton(testClass));
    },
    
    contains: function Open_TestHarness_Models_TestPackageDef$contains(testClass) {
        /// <summary>
        /// Determines whether the test-class has already been added to the package.
        /// </summary>
        /// <param name="testClass" type="Type">
        /// The type of the test class.
        /// </param>
        /// <returns type="Boolean"></returns>
        return this.getTestClassDef(testClass) != null;
    },
    
    getTestClassDef: function Open_TestHarness_Models_TestPackageDef$getTestClassDef(testClass) {
        /// <summary>
        /// Determines whether the test-class has already been added to the package.
        /// </summary>
        /// <param name="testClass" type="Type">
        /// The type of the test class.
        /// </param>
        /// <returns type="Open.TestHarness.Models.TestClassDef"></returns>
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
// Open.TestHarness.Models.TestClassDef

Open.TestHarness.Models.TestClassDef = function Open_TestHarness_Models_TestClassDef(type) {
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
Open.TestHarness.Models.TestClassDef.getSingleton = function Open_TestHarness_Models_TestClassDef$getSingleton(testClass) {
    /// <summary>
    /// Retrieves the singleton instance of the definition for the given package type.
    /// </summary>
    /// <param name="testClass" type="Type">
    /// The Type of the test class.
    /// </param>
    /// <returns type="Open.TestHarness.Models.TestClassDef"></returns>
    if (Open.TestHarness.Models.TestClassDef._singletons == null) {
        Open.TestHarness.Models.TestClassDef._singletons = {};
    }
    var key = testClass.get_fullName();
    if (Object.keyExists(Open.TestHarness.Models.TestClassDef._singletons, key)) {
        return Type.safeCast(Open.TestHarness.Models.TestClassDef._singletons[key], Open.TestHarness.Models.TestClassDef);
    }
    var def = new Open.TestHarness.Models.TestClassDef(testClass);
    Open.TestHarness.Models.TestClassDef._singletons[key] = def;
    return def;
}
Open.TestHarness.Models.TestClassDef.prototype = {
    _type: null,
    
    get_type: function Open_TestHarness_Models_TestClassDef$get_type() {
        /// <summary>
        /// Gets the type of the test class.
        /// </summary>
        /// <value type="Type"></value>
        return this._type;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Models.TestPackageListItem

Open.TestHarness.Models.TestPackageListItem = function Open_TestHarness_Models_TestPackageListItem(testPackage) {
    /// <summary>
    /// A list-item node for a TestPackage.
    /// </summary>
    /// <param name="testPackage" type="Open.TestHarness.Models.TestPackageDef">
    /// The test-package this node represents.
    /// </param>
    /// <field name="_testPackage$3" type="Open.TestHarness.Models.TestPackageDef">
    /// </field>
    Open.TestHarness.Models.TestPackageListItem.initializeBase(this);
    this._testPackage$3 = testPackage;
    this.set_text('FOO! TestPackage');
}
Open.TestHarness.Models.TestPackageListItem.prototype = {
    _testPackage$3: null,
    
    get_testPackage: function Open_TestHarness_Models_TestPackageListItem$get_testPackage() {
        /// <summary>
        /// Gets the test-package this node represents.
        /// </summary>
        /// <value type="Open.TestHarness.Models.TestPackageDef"></value>
        return this._testPackage$3;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Models.TestPackageLoader

Open.TestHarness.Models.TestPackageLoader = function Open_TestHarness_Models_TestPackageLoader(parent, scriptUrl, initMethod) {
    /// <summary>
    /// Handles loading a test-package and executing the entry point assembly.
    /// </summary>
    /// <param name="parent" type="Open.TestHarness.Models.TestPackageDef">
    /// The test-package this object is loading.
    /// </param>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <field name="_parent" type="Open.TestHarness.Models.TestPackageDef">
    /// </field>
    /// <field name="_scriptUrl" type="String">
    /// </field>
    /// <field name="_initMethod" type="String">
    /// </field>
    /// <field name="_isLoaded" type="Boolean">
    /// </field>
    /// <field name="_error" type="Error">
    /// </field>
    /// <field name="_isInitializing" type="Boolean">
    /// </field>
    this._parent = parent;
    this._scriptUrl = scriptUrl;
    this._initMethod = initMethod;
    Open.Core.TestHarness.add_testClassRegistered(ss.Delegate.create(this, this._onTestClassRegistered));
}
Open.TestHarness.Models.TestPackageLoader.prototype = {
    _parent: null,
    _scriptUrl: null,
    _initMethod: null,
    _isLoaded: false,
    _error: null,
    _isInitializing: false,
    
    dispose: function Open_TestHarness_Models_TestPackageLoader$dispose() {
        Open.Core.TestHarness.remove_testClassRegistered(ss.Delegate.create(this, this._onTestClassRegistered));
    },
    
    _onTestClassRegistered: function Open_TestHarness_Models_TestPackageLoader$_onTestClassRegistered(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Core.TestClassEventArgs">
        /// </param>
        if (!this._isInitializing) {
            return;
        }
        this._parent.addClass(e.testClass);
    },
    
    get_scriptUrl: function Open_TestHarness_Models_TestPackageLoader$get_scriptUrl() {
        /// <summary>
        /// Gets the URL to the JavaScript file to load.
        /// </summary>
        /// <value type="String"></value>
        return this._scriptUrl;
    },
    
    get_initMethod: function Open_TestHarness_Models_TestPackageLoader$get_initMethod() {
        /// <summary>
        /// Gets the entry point method to invoke upon load completion.
        /// </summary>
        /// <value type="String"></value>
        return this._initMethod;
    },
    
    get_isLoaded: function Open_TestHarness_Models_TestPackageLoader$get_isLoaded() {
        /// <summary>
        /// Gets whether the script has been loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isLoaded;
    },
    
    get_error: function Open_TestHarness_Models_TestPackageLoader$get_error() {
        /// <summary>
        /// Gets the error (if any) that occured during the Load operation.
        /// </summary>
        /// <value type="Error"></value>
        return this._error;
    },
    
    get_succeeded: function Open_TestHarness_Models_TestPackageLoader$get_succeeded() {
        /// <summary>
        /// Gets or sets whether the load operation failed.
        /// </summary>
        /// <value type="Boolean"></value>
        return this.get_error() == null;
    },
    
    load: function Open_TestHarness_Models_TestPackageLoader$load(onComplete) {
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
                this._isInitializing = true;
                eval(this._initMethod + '();');
            }
            catch (e) {
                Open.Core.Log.error(String.format('Failed to initialize the script-file at \'{0}\' with the entry method \'{1}()\'.<br/>Message: {2}', this._scriptUrl, this._initMethod, e.message));
                this._error = e;
            }
            finally {
                this._isInitializing = false;
            }
            this._isLoaded = this.get_succeeded();
            Open.Core.Helper.invokeOrDefault(onComplete);
        }));
    }
}


Type.registerNamespace('Open.TestHarness.Controllers');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Controllers.PanelResizeController

Open.TestHarness.Controllers.PanelResizeController = function Open_TestHarness_Controllers_PanelResizeController() {
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
    Open.TestHarness.Controllers.PanelResizeController.initializeBase(this);
    this._sideBarResizer$2 = new Open.Core.UI.HorizontalPanelResizer(Open.TestHarness.CssSelectors.sidebar, 'TH_SB');
    this._sideBarResizer$2.add_resized(ss.Delegate.create(this, function() {
        Open.TestHarness.Controllers.PanelResizeController._syncMainPanelWidth$2();
    }));
    this._sideBarResizer$2.set_minWidth(Open.TestHarness.Controllers.PanelResizeController._sidebarMinWidth$2);
    this._sideBarResizer$2.set_maxWidthMargin(Open.TestHarness.Controllers.PanelResizeController._sidebarMaxWidthMargin$2);
    Open.TestHarness.Controllers.PanelResizeController._initializeResizer$2(this._sideBarResizer$2);
    Open.TestHarness.Controllers.PanelResizeController._syncMainPanelWidth$2();
    this._outputResizer$2 = new Open.Core.UI.VerticalPanelResizer(Open.Core.Css.toId(Open.TestHarness.Elements.outputLog), 'TH_OL');
    this._outputResizer$2.add_resized(ss.Delegate.create(this, function() {
    }));
    this._outputResizer$2.set_minHeight($(Open.TestHarness.CssSelectors.logTitlebar).height());
    this._outputResizer$2.set_maxHeightMargin(Open.TestHarness.Controllers.PanelResizeController._outputLogMaxHeightMargin$2);
    Open.TestHarness.Controllers.PanelResizeController._initializeResizer$2(this._outputResizer$2);
}
Open.TestHarness.Controllers.PanelResizeController._initializeResizer$2 = function Open_TestHarness_Controllers_PanelResizeController$_initializeResizer$2(resizer) {
    /// <param name="resizer" type="Open.Core.UI.PanelResizerBase">
    /// </param>
    resizer.set_rootContainerId(Open.TestHarness.Elements.root);
    resizer.initialize();
}
Open.TestHarness.Controllers.PanelResizeController._syncMainPanelWidth$2 = function Open_TestHarness_Controllers_PanelResizeController$_syncMainPanelWidth$2() {
    $(Open.TestHarness.CssSelectors.main).css(Open.Core.Css.left, $(Open.TestHarness.CssSelectors.sidebar).width() + 1 + Open.Core.Css.px);
}
Open.TestHarness.Controllers.PanelResizeController.prototype = {
    _sideBarResizer$2: null,
    _outputResizer$2: null
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Controllers.SidebarController

Open.TestHarness.Controllers.SidebarController = function Open_TestHarness_Controllers_SidebarController() {
    /// <summary>
    /// Controller for the side-bar.
    /// </summary>
    /// <field name="_listTree$2" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_backController$2" type="Open.Core.Lists.ListTreeBackController">
    /// </field>
    /// <field name="_packageControllers$2" type="Array">
    /// </field>
    this._packageControllers$2 = [];
    Open.TestHarness.Controllers.SidebarController.initializeBase(this);
    this._listTree$2 = new Open.Core.Lists.ListTreeView($(Open.TestHarness.CssSelectors.sidebarList));
    this._listTree$2.set_slideDuration(0.2);
    this._backController$2 = new Open.Core.Lists.ListTreeBackController(this._listTree$2, $(Open.TestHarness.CssSelectors.sidebarToolbar), $(Open.TestHarness.CssSelectors.backMask));
    this._TEMP$2();
}
Open.TestHarness.Controllers.SidebarController.prototype = {
    _listTree$2: null,
    _backController$2: null,
    
    onDisposed: function Open_TestHarness_Controllers_SidebarController$onDisposed() {
        this._backController$2.dispose();
        var $enum1 = ss.IEnumerator.getEnumerator(this._packageControllers$2);
        while ($enum1.moveNext()) {
            var controller = $enum1.get_current();
            controller.dispose();
        }
        Open.TestHarness.Controllers.SidebarController.callBaseMethod(this, 'onDisposed');
    },
    
    _TEMP$2: function Open_TestHarness_Controllers_SidebarController$_TEMP$2() {
        var rootNode = new Open.TestHarness.Controllers.MyNode('Root');
        rootNode.addChild(new Open.TestHarness.Controllers.MyNode('Recent'));
        rootNode.addChild(new Open.TestHarness.Controllers.MyNode('Child 2 (Can\'t Select)'));
        rootNode.addChild(new Open.TestHarness.Controllers.MyNode('Child 3'));
        var child1 = Type.safeCast(rootNode.childAt(0), Open.TestHarness.Controllers.MyNode);
        var child2 = (rootNode.childAt(1));
        var child3 = (rootNode.childAt(2));
        child1.addChild(new Open.TestHarness.Controllers.MyNode('Recent Child 1'));
        child1.addChild(new Open.TestHarness.Controllers.MyNode('Recent Child 2'));
        child1.addChild(new Open.TestHarness.Controllers.MyNode('Recent Child 3'));
        var recent1 = Type.safeCast(child1.childAt(0), Open.TestHarness.Controllers.MyNode);
        recent1.addChild(new Open.TestHarness.Controllers.MyNode('Recent Grandchild 1'));
        recent1.addChild(new Open.TestHarness.Controllers.MyNode('Recent Grandchild 2'));
        recent1.addChild(new Open.TestHarness.Controllers.MyNode('Recent Grandchild 3'));
        child2.addChild(new Open.TestHarness.Controllers.MyNode('Yo Child'));
        child3.addChild(new Open.TestHarness.Controllers.MyNode('Yo Child'));
        this._listTree$2.set_rootNode(rootNode);
        child1.set_text('My Recent Foo');
        child2.set_canSelect(false);
        child2.set_isSelected(true);
    },
    
    addPackage: function Open_TestHarness_Controllers_SidebarController$addPackage(testPackage) {
        /// <summary>
        /// Adds a test-package to the controller.
        /// </summary>
        /// <param name="testPackage" type="Open.TestHarness.Models.TestPackageDef">
        /// The test-package to add.
        /// </param>
        if (testPackage == null) {
            return;
        }
        var node = new Open.TestHarness.Models.TestPackageListItem(testPackage);
        this._listTree$2.get_rootNode().addChild(node);
        var controller = new Open.TestHarness.Controllers.TestPackageController(node);
        this._packageControllers$2.add(controller);
        controller.add_loaded(ss.Delegate.create(this, function() {
            this._listTree$2.set_currentListRoot(controller.get_rootNode());
        }));
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Controllers.MyNode

Open.TestHarness.Controllers.MyNode = function Open_TestHarness_Controllers_MyNode(text) {
    /// <param name="text" type="String">
    /// </param>
    Open.TestHarness.Controllers.MyNode.initializeBase(this);
    this.set_text(text);
}
Open.TestHarness.Controllers.MyNode.prototype = {
    
    toString: function Open_TestHarness_Controllers_MyNode$toString() {
        /// <returns type="String"></returns>
        return Open.TestHarness.Controllers.MyNode.callBaseMethod(this, 'toString');
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Controllers.TestPackageController

Open.TestHarness.Controllers.TestPackageController = function Open_TestHarness_Controllers_TestPackageController(rootNode) {
    /// <summary>
    /// Controller for a single test package.
    /// </summary>
    /// <param name="rootNode" type="Open.TestHarness.Models.TestPackageListItem">
    /// The root list-item node.
    /// </param>
    /// <field name="__loaded$2" type="EventHandler">
    /// </field>
    /// <field name="_loadTimeout$2" type="Number" static="true">
    /// </field>
    /// <field name="_rootNode$2" type="Open.TestHarness.Models.TestPackageListItem">
    /// </field>
    Open.TestHarness.Controllers.TestPackageController.initializeBase(this);
    this._rootNode$2 = rootNode;
    rootNode.add_selectionChanged(ss.Delegate.create(this, this._onSelectionChanged$2));
}
Open.TestHarness.Controllers.TestPackageController.prototype = {
    
    add_loaded: function Open_TestHarness_Controllers_TestPackageController$add_loaded(value) {
        /// <summary>
        /// Fires when the package has laoded.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__loaded$2 = ss.Delegate.combine(this.__loaded$2, value);
    },
    remove_loaded: function Open_TestHarness_Controllers_TestPackageController$remove_loaded(value) {
        /// <summary>
        /// Fires when the package has laoded.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__loaded$2 = ss.Delegate.remove(this.__loaded$2, value);
    },
    
    __loaded$2: null,
    
    _fireLoaded$2: function Open_TestHarness_Controllers_TestPackageController$_fireLoaded$2() {
        if (this.__loaded$2 != null) {
            this.__loaded$2.invoke(this, new ss.EventArgs());
        }
    },
    
    _rootNode$2: null,
    
    _onSelectionChanged$2: function Open_TestHarness_Controllers_TestPackageController$_onSelectionChanged$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (this.get_rootNode().get_isSelected()) {
            this._load$2();
        }
    },
    
    get_testPackage: function Open_TestHarness_Controllers_TestPackageController$get_testPackage() {
        /// <summary>
        /// Gets the test-package that is under control.
        /// </summary>
        /// <value type="Open.TestHarness.Models.TestPackageDef"></value>
        return this._rootNode$2.get_testPackage();
    },
    
    get_rootNode: function Open_TestHarness_Controllers_TestPackageController$get_rootNode() {
        /// <summary>
        /// Gets the root list-item node.
        /// </summary>
        /// <value type="Open.TestHarness.Models.TestPackageListItem"></value>
        return this._rootNode$2;
    },
    
    _load$2: function Open_TestHarness_Controllers_TestPackageController$_load$2() {
        if (this.get_testPackage().get_isLoaded()) {
            return;
        }
        var loader = this.get_testPackage().get_loader();
        var link = Open.Core.Html.toHyperlink(loader.get_scriptUrl(), null, Open.Core.LinkTarget.blank);
        var timeout = new Open.Core.DelayedAction(Open.TestHarness.Controllers.TestPackageController._loadTimeout$2, ss.Delegate.create(this, function() {
            Open.Core.Log.error(String.format('Failed to download the test-package at \'{0}\'.  Please ensure the file exists.', link));
            Open.Core.Log.lineBreak();
        }));
        Open.Core.Log.info(String.format('Downloading test-package: {0} ...', link));
        loader.load(ss.Delegate.create(this, function() {
            timeout.stop();
            if (loader.get_succeeded()) {
                Open.Core.Log.success('Test-package loaded successfully.');
                this._addChildNodes$2();
                this._fireLoaded$2();
            }
            Open.Core.Log.lineBreak();
        }));
        timeout.start();
    },
    
    _addChildNodes$2: function Open_TestHarness_Controllers_TestPackageController$_addChildNodes$2() {
        var $enum1 = ss.IEnumerator.getEnumerator(this.get_testPackage());
        while ($enum1.moveNext()) {
            var testClass = $enum1.get_current();
            var node = new Open.TestHarness.Models.TestClassListItem(testClass);
            this.get_rootNode().addChild(node);
        }
    }
}


Open.TestHarness.CssSelectors.registerClass('Open.TestHarness.CssSelectors');
Open.TestHarness.Elements.registerClass('Open.TestHarness.Elements');
Open.TestHarness.Application.registerClass('Open.TestHarness.Application');
Open.TestHarness.Models.TestClassListItem.registerClass('Open.TestHarness.Models.TestClassListItem', Open.Core.Lists.ListItem);
Open.TestHarness.Models.TestPackageDef.registerClass('Open.TestHarness.Models.TestPackageDef', null, ss.IEnumerable);
Open.TestHarness.Models.TestClassDef.registerClass('Open.TestHarness.Models.TestClassDef');
Open.TestHarness.Models.TestPackageListItem.registerClass('Open.TestHarness.Models.TestPackageListItem', Open.Core.Lists.ListItem);
Open.TestHarness.Models.TestPackageLoader.registerClass('Open.TestHarness.Models.TestPackageLoader', null, ss.IDisposable);
Open.TestHarness.Controllers.PanelResizeController.registerClass('Open.TestHarness.Controllers.PanelResizeController', Open.Core.ControllerBase);
Open.TestHarness.Controllers.SidebarController.registerClass('Open.TestHarness.Controllers.SidebarController', Open.Core.ControllerBase);
Open.TestHarness.Controllers.MyNode.registerClass('Open.TestHarness.Controllers.MyNode', Open.Core.Lists.ListItem);
Open.TestHarness.Controllers.TestPackageController.registerClass('Open.TestHarness.Controllers.TestPackageController', Open.Core.ControllerBase);
Open.TestHarness.CssSelectors.sidebar = '#testHarnessSidebar';
Open.TestHarness.CssSelectors.sidebarList = '#testHarnessSidebar .th-sidebarList';
Open.TestHarness.CssSelectors.sidebarToolbar = '#testHarnessSidebar div.th-toolbar';
Open.TestHarness.CssSelectors.backMask = '#testHarnessSidebar img.th-backMask';
Open.TestHarness.CssSelectors.main = '#testHarness .th-main';
Open.TestHarness.CssSelectors.logTitlebar = '#testHarnessLog .th-log-titlebar';
Open.TestHarness.CssSelectors.log = '#testHarnessLog .coreLog';
Open.TestHarness.Elements.root = 'testHarness';
Open.TestHarness.Elements.outputLog = 'testHarnessLog';
Open.TestHarness.Application._resizeController = null;
Open.TestHarness.Application._sidebarController = null;
Open.TestHarness.Models.TestPackageDef._singletons = [];
Open.TestHarness.Models.TestClassDef._singletons = null;
Open.TestHarness.Controllers.PanelResizeController._sidebarMinWidth$2 = 200;
Open.TestHarness.Controllers.PanelResizeController._sidebarMaxWidthMargin$2 = 80;
Open.TestHarness.Controllers.PanelResizeController._outputLogMaxHeightMargin$2 = 80;
Open.TestHarness.Controllers.TestPackageController._loadTimeout$2 = 5;

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('TestHarness.Script3', ['Open.Core.Views','Open.Core.Script','Open.Core.Lists'], executeScript);
})();
