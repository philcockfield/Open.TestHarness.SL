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
    /// <field name="_shell" type="Open.TestHarness.Views.ShellView" static="true">
    /// </field>
    /// <field name="_resizeController" type="Open.TestHarness.Controllers.PanelResizeController" static="true">
    /// </field>
    /// <field name="_sidebarController" type="Open.TestHarness.Controllers.SidebarController" static="true">
    /// </field>
}
Open.TestHarness.Application.get_shell = function Open_TestHarness_Application$get_shell() {
    /// <summary>
    /// Gets the root view of the application shell.
    /// </summary>
    /// <value type="Open.TestHarness.Views.ShellView"></value>
    return Open.TestHarness.Application._shell;
}
Open.TestHarness.Application.main = function Open_TestHarness_Application$main(args) {
    /// <param name="args" type="Object">
    /// </param>
    var logView = new Open.Core.Controls.LogView($(Open.TestHarness.CssSelectors.log).first());
    Open.Core.Log.registerView(logView);
    Open.TestHarness.Application._shell = new Open.TestHarness.Views.ShellView($(Open.TestHarness.CssSelectors.root));
    Open.TestHarness.Application._resizeController = new Open.TestHarness.Controllers.PanelResizeController();
    Open.TestHarness.Application._sidebarController = new Open.TestHarness.Controllers.SidebarController();
    var scriptUrl = '/Content/Scripts/Test.debug.js';
    var initMethod = 'Test.Application.main';
    var packageDef = Open.TestHarness.Models.PackageInfo.singletonFromUrl(scriptUrl, initMethod);
    Open.TestHarness.Application._sidebarController.addPackage(packageDef);
}


Type.registerNamespace('Open.TestHarness.Models');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Models.MethodListItem

Open.TestHarness.Models.MethodListItem = function Open_TestHarness_Models_MethodListItem(method) {
    /// <summary>
    /// A list-item node for a single Test-Method.
    /// </summary>
    /// <param name="method" type="Open.TestHarness.Models.MethodInfo">
    /// The test-method this node represents.
    /// </param>
    /// <field name="_method$3" type="Open.TestHarness.Models.MethodInfo">
    /// </field>
    Open.TestHarness.Models.MethodListItem.initializeBase(this);
    this._method$3 = method;
    this.set_text(method.get_displayName());
}
Open.TestHarness.Models.MethodListItem.prototype = {
    _method$3: null,
    
    get_method: function Open_TestHarness_Models_MethodListItem$get_method() {
        /// <summary>
        /// Gets the test-package this node represents.
        /// </summary>
        /// <value type="Open.TestHarness.Models.MethodInfo"></value>
        return this._method$3;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Models.ClassListItem

Open.TestHarness.Models.ClassListItem = function Open_TestHarness_Models_ClassListItem(classInfo) {
    /// <summary>
    /// A list-item node for a TestClass.
    /// </summary>
    /// <param name="classInfo" type="Open.TestHarness.Models.ClassInfo">
    /// The test-class this node represents.
    /// </param>
    /// <field name="_classInfo$3" type="Open.TestHarness.Models.ClassInfo">
    /// </field>
    Open.TestHarness.Models.ClassListItem.initializeBase(this);
    this._classInfo$3 = classInfo;
    this.set_text(classInfo.get_displayName());
}
Open.TestHarness.Models.ClassListItem.prototype = {
    _classInfo$3: null,
    
    get_classInfo: function Open_TestHarness_Models_ClassListItem$get_classInfo() {
        /// <summary>
        /// Gets the test-package this node represents.
        /// </summary>
        /// <value type="Open.TestHarness.Models.ClassInfo"></value>
        return this._classInfo$3;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Models.MethodInfo

Open.TestHarness.Models.MethodInfo = function Open_TestHarness_Models_MethodInfo(classInfo, name) {
    /// <summary>
    /// Represents a single test method.
    /// </summary>
    /// <param name="classInfo" type="Open.TestHarness.Models.ClassInfo">
    /// The test-class that this method is a member of.
    /// </param>
    /// <param name="name" type="String">
    /// The name of the method.
    /// </param>
    /// <field name="keyConstructor" type="String" static="true">
    /// </field>
    /// <field name="keyGetter" type="String" static="true">
    /// </field>
    /// <field name="keySetter" type="String" static="true">
    /// </field>
    /// <field name="keyField" type="String" static="true">
    /// </field>
    /// <field name="keyFunction" type="String" static="true">
    /// </field>
    /// <field name="_classInfo$1" type="Open.TestHarness.Models.ClassInfo">
    /// </field>
    /// <field name="_name$1" type="String">
    /// </field>
    /// <field name="_displayName$1" type="String">
    /// </field>
    Open.TestHarness.Models.MethodInfo.initializeBase(this);
    this._classInfo$1 = classInfo;
    this._name$1 = name;
    this._displayName$1 = Open.TestHarness.Models.MethodInfo.formatName(name);
}
Open.TestHarness.Models.MethodInfo.isTestMethod = function Open_TestHarness_Models_MethodInfo$isTestMethod(item) {
    /// <summary>
    /// Determines whether the specified DictionaryEntry represents a valid test-method.
    /// </summary>
    /// <param name="item" type="DictionaryEntry">
    /// The Dictionaty item to examine.
    /// </param>
    /// <returns type="Boolean"></returns>
    var key = item.key;
    if (typeof(item.value) !== Open.TestHarness.Models.MethodInfo.keyFunction) {
        return false;
    }
    if (key.startsWith(Open.TestHarness.Models.MethodInfo.keyField)) {
        return false;
    }
    if (key.startsWith(Open.TestHarness.Models.MethodInfo.keyGetter)) {
        return false;
    }
    if (key.startsWith(Open.TestHarness.Models.MethodInfo.keySetter)) {
        return false;
    }
    if (key === Open.TestHarness.Models.MethodInfo.keyConstructor) {
        return false;
    }
    return true;
}
Open.TestHarness.Models.MethodInfo.formatName = function Open_TestHarness_Models_MethodInfo$formatName(name) {
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
Open.TestHarness.Models.MethodInfo.prototype = {
    _classInfo$1: null,
    _name$1: null,
    _displayName$1: null,
    
    get_classInfo: function Open_TestHarness_Models_MethodInfo$get_classInfo() {
        /// <summary>
        /// Gets the test-class that this method is a member of.
        /// </summary>
        /// <value type="Open.TestHarness.Models.ClassInfo"></value>
        return this._classInfo$1;
    },
    
    get_name: function Open_TestHarness_Models_MethodInfo$get_name() {
        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        /// <value type="String"></value>
        return this._name$1;
    },
    
    get_displayName: function Open_TestHarness_Models_MethodInfo$get_displayName() {
        /// <summary>
        /// Gets the display version of the name.
        /// </summary>
        /// <value type="String"></value>
        return this._displayName$1;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Models.PackageInfo

Open.TestHarness.Models.PackageInfo = function Open_TestHarness_Models_PackageInfo(scriptUrl, initMethod) {
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
    /// <field name="_loader$1" type="Open.TestHarness.Models.TestPackageLoader">
    /// </field>
    /// <field name="_name$1" type="String">
    /// </field>
    this._classes$1 = [];
    Open.TestHarness.Models.PackageInfo.initializeBase(this);
    if (String.isNullOrEmpty(scriptUrl)) {
        throw new Error('A URL to the test-package script must be specified.');
    }
    if (String.isNullOrEmpty(initMethod)) {
        throw new Error('An entry point method must be specified.');
    }
    this._name$1 = Open.TestHarness.Models.PackageInfo._getName$1(scriptUrl);
    this._loader$1 = new Open.TestHarness.Models.TestPackageLoader(this, scriptUrl.toLowerCase(), initMethod);
}
Open.TestHarness.Models.PackageInfo.get_singletons = function Open_TestHarness_Models_PackageInfo$get_singletons() {
    /// <summary>
    /// Gets the collection of singleton TestPackageDef instances.
    /// </summary>
    /// <value type="Array"></value>
    return Open.TestHarness.Models.PackageInfo._singletons$1;
}
Open.TestHarness.Models.PackageInfo.singletonFromUrl = function Open_TestHarness_Models_PackageInfo$singletonFromUrl(scriptUrl, initMethod) {
    /// <summary>
    /// Retrieves (or creates) the singleton instance of the definition for the given package type.
    /// </summary>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <returns type="Open.TestHarness.Models.PackageInfo"></returns>
    var def = Type.safeCast(Open.Core.Helper.get_collection().first(Open.TestHarness.Models.PackageInfo._singletons$1, function(o) {
        return (o).get_id() === scriptUrl.toLowerCase();
    }), Open.TestHarness.Models.PackageInfo);
    if (def == null) {
        def = new Open.TestHarness.Models.PackageInfo(scriptUrl, initMethod);
        Open.TestHarness.Models.PackageInfo._singletons$1.add(def);
    }
    return def;
}
Open.TestHarness.Models.PackageInfo._getName$1 = function Open_TestHarness_Models_PackageInfo$_getName$1(scriptUrl) {
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
Open.TestHarness.Models.PackageInfo.prototype = {
    _loader$1: null,
    _name$1: null,
    
    get_id: function Open_TestHarness_Models_PackageInfo$get_id() {
        /// <summary>
        /// Gets the unique ID of the package.
        /// </summary>
        /// <value type="String"></value>
        return this.get_loader().get_scriptUrl();
    },
    
    get_name: function Open_TestHarness_Models_PackageInfo$get_name() {
        /// <summary>
        /// Gets the display name of the package.
        /// </summary>
        /// <value type="String"></value>
        return this._name$1;
    },
    
    get_loader: function Open_TestHarness_Models_PackageInfo$get_loader() {
        /// <summary>
        /// Gets the package loader.
        /// </summary>
        /// <value type="Open.TestHarness.Models.TestPackageLoader"></value>
        return this._loader$1;
    },
    
    get_isLoaded: function Open_TestHarness_Models_PackageInfo$get_isLoaded() {
        /// <summary>
        /// Gets or sets whether the package has been loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        return this.get_loader().get_isLoaded();
    },
    
    get_count: function Open_TestHarness_Models_PackageInfo$get_count() {
        /// <summary>
        /// Gets the number of test classes within the package.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._classes$1.length;
    },
    
    getEnumerator: function Open_TestHarness_Models_PackageInfo$getEnumerator() {
        /// <returns type="ss.IEnumerator"></returns>
        return this._classes$1.getEnumerator();
    },
    
    addClass: function Open_TestHarness_Models_PackageInfo$addClass(testClass) {
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
        this._classes$1.add(Open.TestHarness.Models.ClassInfo.getSingleton(testClass));
    },
    
    contains: function Open_TestHarness_Models_PackageInfo$contains(testClass) {
        /// <summary>
        /// Determines whether the test-class has already been added to the package.
        /// </summary>
        /// <param name="testClass" type="Type">
        /// The type of the test class.
        /// </param>
        /// <returns type="Boolean"></returns>
        return this.getTestClassDef(testClass) != null;
    },
    
    getTestClassDef: function Open_TestHarness_Models_PackageInfo$getTestClassDef(testClass) {
        /// <summary>
        /// Determines whether the test-class has already been added to the package.
        /// </summary>
        /// <param name="testClass" type="Type">
        /// The type of the test class.
        /// </param>
        /// <returns type="Open.TestHarness.Models.ClassInfo"></returns>
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
// Open.TestHarness.Models.ClassInfo

Open.TestHarness.Models.ClassInfo = function Open_TestHarness_Models_ClassInfo(classType) {
    /// <summary>
    /// Represents a class with tests.
    /// </summary>
    /// <param name="classType" type="Type">
    /// The type of the test class.
    /// </param>
    /// <field name="_singletons$1" type="Object" static="true">
    /// </field>
    /// <field name="_classType$1" type="Type">
    /// </field>
    /// <field name="_instance$1" type="Object">
    /// </field>
    /// <field name="_methods$1" type="Array">
    /// </field>
    /// <field name="_displayName$1" type="String">
    /// </field>
    this._methods$1 = [];
    Open.TestHarness.Models.ClassInfo.initializeBase(this);
    this._classType$1 = classType;
    this._displayName$1 = Open.TestHarness.Models.MethodInfo.formatName(classType.get_name());
    this._getMethods$1();
}
Open.TestHarness.Models.ClassInfo.getSingleton = function Open_TestHarness_Models_ClassInfo$getSingleton(testClass) {
    /// <summary>
    /// Retrieves the singleton instance of the definition for the given package type.
    /// </summary>
    /// <param name="testClass" type="Type">
    /// The Type of the test class.
    /// </param>
    /// <returns type="Open.TestHarness.Models.ClassInfo"></returns>
    if (Open.TestHarness.Models.ClassInfo._singletons$1 == null) {
        Open.TestHarness.Models.ClassInfo._singletons$1 = {};
    }
    var key = testClass.get_fullName();
    if (Object.keyExists(Open.TestHarness.Models.ClassInfo._singletons$1, key)) {
        return Type.safeCast(Open.TestHarness.Models.ClassInfo._singletons$1[key], Open.TestHarness.Models.ClassInfo);
    }
    var def = new Open.TestHarness.Models.ClassInfo(testClass);
    Open.TestHarness.Models.ClassInfo._singletons$1[key] = def;
    return def;
}
Open.TestHarness.Models.ClassInfo.prototype = {
    _classType$1: null,
    _instance$1: null,
    _displayName$1: null,
    
    get_classType: function Open_TestHarness_Models_ClassInfo$get_classType() {
        /// <summary>
        /// Gets the type of the test class.
        /// </summary>
        /// <value type="Type"></value>
        return this._classType$1;
    },
    
    get_displayName: function Open_TestHarness_Models_ClassInfo$get_displayName() {
        /// <summary>
        /// Gets the display version of the class name.
        /// </summary>
        /// <value type="String"></value>
        return this._displayName$1;
    },
    
    get_instance: function Open_TestHarness_Models_ClassInfo$get_instance() {
        /// <summary>
        /// Gets the test instance of the class.
        /// </summary>
        /// <value type="Object"></value>
        return this._instance$1 || (this._instance$1 = Type.safeCast(new this._classType$1(), Object));
    },
    
    get_count: function Open_TestHarness_Models_ClassInfo$get_count() {
        /// <summary>
        /// Gets the number of test-methods within the class.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._methods$1.length;
    },
    
    reset: function Open_TestHarness_Models_ClassInfo$reset() {
        /// <summary>
        /// Resets the test-class instance.
        /// </summary>
        this._instance$1 = null;
    },
    
    getEnumerator: function Open_TestHarness_Models_ClassInfo$getEnumerator() {
        /// <returns type="ss.IEnumerator"></returns>
        return this._methods$1.getEnumerator();
    },
    
    toString: function Open_TestHarness_Models_ClassInfo$toString() {
        /// <returns type="String"></returns>
        return String.format('[{0}:{1}]', Type.getInstanceType(this).get_name(), this.get_classType().get_name());
    },
    
    _getMethods$1: function Open_TestHarness_Models_ClassInfo$_getMethods$1() {
        if (this.get_instance() == null) {
            return;
        }
        var $dict1 = this.get_instance();
        for (var $key2 in $dict1) {
            var item = { key: $key2, value: $dict1[$key2] };
            if (!Open.TestHarness.Models.MethodInfo.isTestMethod(item)) {
                continue;
            }
            this._methods$1.add(new Open.TestHarness.Models.MethodInfo(this, item.key));
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Models.PackageListItem

Open.TestHarness.Models.PackageListItem = function Open_TestHarness_Models_PackageListItem(testPackage) {
    /// <summary>
    /// A list-item node for a TestPackage.
    /// </summary>
    /// <param name="testPackage" type="Open.TestHarness.Models.PackageInfo">
    /// The test-package this node represents.
    /// </param>
    /// <field name="_testPackage$3" type="Open.TestHarness.Models.PackageInfo">
    /// </field>
    Open.TestHarness.Models.PackageListItem.initializeBase(this);
    this._testPackage$3 = testPackage;
    this.set_text(testPackage.get_name());
}
Open.TestHarness.Models.PackageListItem.prototype = {
    _testPackage$3: null,
    
    get_testPackage: function Open_TestHarness_Models_PackageListItem$get_testPackage() {
        /// <summary>
        /// Gets the test-package this node represents.
        /// </summary>
        /// <value type="Open.TestHarness.Models.PackageInfo"></value>
        return this._testPackage$3;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Models.TestPackageLoader

Open.TestHarness.Models.TestPackageLoader = function Open_TestHarness_Models_TestPackageLoader(parent, scriptUrl, initMethod) {
    /// <summary>
    /// Handles loading a test-package and executing the entry point assembly.
    /// </summary>
    /// <param name="parent" type="Open.TestHarness.Models.PackageInfo">
    /// The test-package this object is loading.
    /// </param>
    /// <param name="scriptUrl" type="String">
    /// The URL to the JavaScript file to load.
    /// </param>
    /// <param name="initMethod" type="String">
    /// The entry point method to invoke upon load completion.
    /// </param>
    /// <field name="_parent" type="Open.TestHarness.Models.PackageInfo">
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
    Open.TestHarness.TestHarnessEvents.add_testClassRegistered(ss.Delegate.create(this, this._onTestClassRegistered));
}
Open.TestHarness.Models.TestPackageLoader.prototype = {
    _parent: null,
    _scriptUrl: null,
    _initMethod: null,
    _isLoaded: false,
    _error: null,
    _isInitializing: false,
    
    dispose: function Open_TestHarness_Models_TestPackageLoader$dispose() {
        Open.TestHarness.TestHarnessEvents.remove_testClassRegistered(ss.Delegate.create(this, this._onTestClassRegistered));
    },
    
    _onTestClassRegistered: function Open_TestHarness_Models_TestPackageLoader$_onTestClassRegistered(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.TestHarness.TestClassEventArgs">
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
    /// <field name="_packageControllers$2" type="Array">
    /// </field>
    /// <field name="_view$2" type="Open.TestHarness.Views.SidebarView">
    /// </field>
    this._packageControllers$2 = [];
    Open.TestHarness.Controllers.SidebarController.initializeBase(this);
    this._view$2 = Open.TestHarness.Application.get_shell().get_sidebar();
    this._TEMP$2();
}
Open.TestHarness.Controllers.SidebarController.prototype = {
    _view$2: null,
    
    onDisposed: function Open_TestHarness_Controllers_SidebarController$onDisposed() {
        this._view$2.dispose();
        var $enum1 = ss.IEnumerator.getEnumerator(this._packageControllers$2);
        while ($enum1.moveNext()) {
            var controller = $enum1.get_current();
            controller.dispose();
        }
        Open.TestHarness.Controllers.SidebarController.callBaseMethod(this, 'onDisposed');
    },
    
    _TEMP$2: function Open_TestHarness_Controllers_SidebarController$_TEMP$2() {
        var rootNode = new Open.TestHarness.Controllers.MyNode('Root');
        this._view$2.get_rootList().set_rootNode(rootNode);
        rootNode.addChild(new Open.TestHarness.Controllers.MyNode('Recent'));
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
    },
    
    addPackage: function Open_TestHarness_Controllers_SidebarController$addPackage(testPackage) {
        /// <summary>
        /// Adds a test-package to the controller.
        /// </summary>
        /// <param name="testPackage" type="Open.TestHarness.Models.PackageInfo">
        /// The test-package to add.
        /// </param>
        if (testPackage == null) {
            return;
        }
        var node = new Open.TestHarness.Models.PackageListItem(testPackage);
        this._view$2.get_rootList().get_rootNode().addChild(node);
        var controller = new Open.TestHarness.Controllers.TestPackageController(node, this._view$2);
        this._packageControllers$2.add(controller);
        controller.add_loaded(ss.Delegate.create(this, function() {
            this._view$2.get_rootList().set_selectedParent(controller.get_rootNode());
        }));
    },
    
    removePackage: function Open_TestHarness_Controllers_SidebarController$removePackage(testPackage) {
        /// <summary>
        /// Removes the specified package.
        /// </summary>
        /// <param name="testPackage" type="Open.TestHarness.Models.PackageInfo">
        /// The test-package to remove.
        /// </param>
        if (testPackage == null) {
            return;
        }
        var controller = this._getController$2(testPackage);
        if (controller == null) {
            return;
        }
        this._view$2.get_rootList().get_rootNode().removeChild(controller.get_rootNode());
        Open.Core.Log.info(String.format('Test package unloaded: {0}', Open.Core.Html.toHyperlink(testPackage.get_id(), null, Open.Core.LinkTarget.blank)));
        Open.Core.Log.lineBreak();
    },
    
    _getController$2: function Open_TestHarness_Controllers_SidebarController$_getController$2(testPackage) {
        /// <param name="testPackage" type="Open.TestHarness.Models.PackageInfo">
        /// </param>
        /// <returns type="Open.TestHarness.Controllers.TestPackageController"></returns>
        return Type.safeCast(Open.Core.Helper.get_collection().first(this._packageControllers$2, ss.Delegate.create(this, function(o) {
            return (o).get_testPackage() === testPackage;
        })), Open.TestHarness.Controllers.TestPackageController);
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

Open.TestHarness.Controllers.TestPackageController = function Open_TestHarness_Controllers_TestPackageController(rootNode, sidebarView) {
    /// <summary>
    /// Controller for a single test package.
    /// </summary>
    /// <param name="rootNode" type="Open.TestHarness.Models.PackageListItem">
    /// The root list-item node.
    /// </param>
    /// <param name="sidebarView" type="Open.TestHarness.Views.SidebarView">
    /// The Sidebar control.
    /// </param>
    /// <field name="__loaded$2" type="EventHandler">
    /// </field>
    /// <field name="propSelectedClass" type="String" static="true">
    /// </field>
    /// <field name="_loadTimeout$2" type="Number" static="true">
    /// </field>
    /// <field name="_rootNode$2" type="Open.TestHarness.Models.PackageListItem">
    /// </field>
    /// <field name="_sidebarView$2" type="Open.TestHarness.Views.SidebarView">
    /// </field>
    Open.TestHarness.Controllers.TestPackageController.initializeBase(this);
    this._rootNode$2 = rootNode;
    this._sidebarView$2 = sidebarView;
    rootNode.add_selectionChanged(ss.Delegate.create(this, this._onSelectionChanged$2));
    rootNode.add_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$2));
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
    _sidebarView$2: null,
    
    onDisposed: function Open_TestHarness_Controllers_TestPackageController$onDisposed() {
        this._rootNode$2.remove_selectionChanged(ss.Delegate.create(this, this._onSelectionChanged$2));
        this._rootNode$2.remove_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$2));
        Open.TestHarness.Controllers.TestPackageController.callBaseMethod(this, 'onDisposed');
    },
    
    _onSelectionChanged$2: function Open_TestHarness_Controllers_TestPackageController$_onSelectionChanged$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (this.get_rootNode().get_isSelected()) {
            this._download$2();
        }
    },
    
    _onChildSelectionChanged$2: function Open_TestHarness_Controllers_TestPackageController$_onChildSelectionChanged$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        var item = Type.safeCast(Open.Core.Helper.get_tree().firstSelectedChild(this.get_rootNode()), Open.TestHarness.Models.ClassListItem);
        this.set_selectedClass((item == null) ? null : item.get_classInfo());
    },
    
    get_testPackage: function Open_TestHarness_Controllers_TestPackageController$get_testPackage() {
        /// <summary>
        /// Gets the test-package that is under control.
        /// </summary>
        /// <value type="Open.TestHarness.Models.PackageInfo"></value>
        return this._rootNode$2.get_testPackage();
    },
    
    get_rootNode: function Open_TestHarness_Controllers_TestPackageController$get_rootNode() {
        /// <summary>
        /// Gets the root list-item node.
        /// </summary>
        /// <value type="Open.TestHarness.Models.PackageListItem"></value>
        return this._rootNode$2;
    },
    
    get_selectedClass: function Open_TestHarness_Controllers_TestPackageController$get_selectedClass() {
        /// <summary>
        /// Gets or sets the currently selected test class.
        /// </summary>
        /// <value type="Open.TestHarness.Models.ClassInfo"></value>
        return this.get(Open.TestHarness.Controllers.TestPackageController.propSelectedClass, null);
    },
    set_selectedClass: function Open_TestHarness_Controllers_TestPackageController$set_selectedClass(value) {
        /// <summary>
        /// Gets or sets the currently selected test class.
        /// </summary>
        /// <value type="Open.TestHarness.Models.ClassInfo"></value>
        if (this.set(Open.TestHarness.Controllers.TestPackageController.propSelectedClass, value, null)) {
            this._sidebarView$2.get_methodList().set_classInfo(value);
        }
        return value;
    },
    
    _download$2: function Open_TestHarness_Controllers_TestPackageController$_download$2() {
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
            var node = new Open.TestHarness.Models.ClassListItem(testClass);
            this.get_rootNode().addChild(node);
        }
    }
}


Type.registerNamespace('Open.TestHarness.Views');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Views.ShellView

Open.TestHarness.Views.ShellView = function Open_TestHarness_Views_ShellView(container) {
    /// <summary>
    /// The root view of the application shell.
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// The containing DIV.
    /// </param>
    /// <field name="_sidebar$2" type="Open.TestHarness.Views.SidebarView">
    /// </field>
    Open.TestHarness.Views.ShellView.initializeBase(this);
    this.initialize(container);
    this._sidebar$2 = new Open.TestHarness.Views.SidebarView($(Open.TestHarness.CssSelectors.sidebar));
}
Open.TestHarness.Views.ShellView.prototype = {
    _sidebar$2: null,
    
    get_sidebar: function Open_TestHarness_Views_ShellView$get_sidebar() {
        /// <summary>
        /// Gets the view for the SideBar.
        /// </summary>
        /// <value type="Open.TestHarness.Views.SidebarView"></value>
        return this._sidebar$2;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Views.SidebarView

Open.TestHarness.Views.SidebarView = function Open_TestHarness_Views_SidebarView(container) {
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
    /// <field name="_rootList$2" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_backController$2" type="Open.Core.Lists.ListTreeBackController">
    /// </field>
    /// <field name="_methodList$2" type="Open.TestHarness.Views.MethodListView">
    /// </field>
    Open.TestHarness.Views.SidebarView.initializeBase(this);
    this.initialize(container);
    this._rootList$2 = new Open.Core.Lists.ListTreeView($(Open.TestHarness.CssSelectors.sidebarRootList));
    this._rootList$2.set_slideDuration(Open.TestHarness.Views.SidebarView.slideDuration);
    this._methodList$2 = new Open.TestHarness.Views.MethodListView($(Open.TestHarness.CssSelectors.testList));
    this._backController$2 = new Open.Core.Lists.ListTreeBackController(this._rootList$2, $(Open.TestHarness.CssSelectors.sidebarToolbar), $(Open.TestHarness.CssSelectors.backMask));
    this._rootList$2.add_selectedParentChanged(ss.Delegate.create(this, function() {
        this._syncTestListVisibility$2();
    }));
    this.updateVisualState();
    this.get_rootList().get_container().click(ss.Delegate.create(this, function(eevent) {
    }));
}
Open.TestHarness.Views.SidebarView.prototype = {
    _rootList$2: null,
    _backController$2: null,
    _methodList$2: null,
    
    onDisposed: function Open_TestHarness_Views_SidebarView$onDisposed() {
        this._backController$2.dispose();
        this._rootList$2.dispose();
        Open.TestHarness.Views.SidebarView.callBaseMethod(this, 'onDisposed');
    },
    
    get_rootList: function Open_TestHarness_Views_SidebarView$get_rootList() {
        /// <summary>
        /// Gets the main List-Tree view.
        /// </summary>
        /// <value type="Open.Core.Lists.ListTreeView"></value>
        return this._rootList$2;
    },
    
    get_methodList: function Open_TestHarness_Views_SidebarView$get_methodList() {
        /// <summary>
        /// Gets the Test-List view.
        /// </summary>
        /// <value type="Open.TestHarness.Views.MethodListView"></value>
        return this._methodList$2;
    },
    
    get_isTestListVisible: function Open_TestHarness_Views_SidebarView$get_isTestListVisible() {
        /// <summary>
        /// Gets or sets whether the TestList panel is visible.
        /// </summary>
        /// <value type="Boolean"></value>
        return this.get(Open.TestHarness.Views.SidebarView.propIsTestListVisible, false);
    },
    set_isTestListVisible: function Open_TestHarness_Views_SidebarView$set_isTestListVisible(value) {
        /// <summary>
        /// Gets or sets whether the TestList panel is visible.
        /// </summary>
        /// <value type="Boolean"></value>
        if (this.set(Open.TestHarness.Views.SidebarView.propIsTestListVisible, value, false)) {
            if (value) {
                this.showTestList(null);
            }
            else {
                this.hideTestList(null);
            }
        }
        return value;
    },
    
    updateVisualState: function Open_TestHarness_Views_SidebarView$updateVisualState() {
        /// <summary>
        /// Refreshes the visual state.
        /// </summary>
        this._syncRootListHeight$2();
    },
    
    showTestList: function Open_TestHarness_Views_SidebarView$showTestList(onComplete) {
        /// <summary>
        /// Reveals the test list.
        /// </summary>
        /// <param name="onComplete" type="Action">
        /// The action to invoke when complete
        /// </param>
        this.set_isTestListVisible(true);
        var height = this._getTargetTestListHeight$2();
        this._animateHeights$2(height, onComplete);
    },
    
    hideTestList: function Open_TestHarness_Views_SidebarView$hideTestList(onComplete) {
        /// <summary>
        /// Hides the test list.
        /// </summary>
        /// <param name="onComplete" type="Action">
        /// The action to invoke when complete
        /// </param>
        this.set_isTestListVisible(false);
        this._animateHeights$2(0, onComplete);
    },
    
    _animateHeights$2: function Open_TestHarness_Views_SidebarView$_animateHeights$2(testListHeight, onComplete) {
        /// <param name="testListHeight" type="Number" integer="true">
        /// </param>
        /// <param name="onComplete" type="Action">
        /// </param>
        var testListProps = {};
        testListProps[Open.Core.Css.height] = testListHeight;
        var rootListProps = {};
        rootListProps[Open.Core.Css.bottom] = testListHeight;
        var isShowing = testListHeight > 0;
        if (isShowing) {
            Open.Core.Css.setVisible(this.get_methodList().get_container(), true);
        }
        this.get_methodList().updateLayout();
        this._animate$2(isShowing, this.get_methodList().get_container(), testListProps, null);
        this._animate$2(isShowing, this.get_rootList().get_container(), rootListProps, onComplete);
    },
    
    _animate$2: function Open_TestHarness_Views_SidebarView$_animate$2(isShowing, div, properties, onComplete) {
        /// <param name="isShowing" type="Boolean">
        /// </param>
        /// <param name="div" type="jQueryObject">
        /// </param>
        /// <param name="properties" type="Object">
        /// </param>
        /// <param name="onComplete" type="Action">
        /// </param>
        div.animate(properties, Open.Core.Helper.get_number().toMsecs(Open.TestHarness.Views.SidebarView.slideDuration), 'swing', ss.Delegate.create(this, function() {
            if (!isShowing) {
                Open.Core.Css.setVisible(this.get_methodList().get_container(), false);
            }
            Open.Core.Helper.invokeOrDefault(onComplete);
        }));
    },
    
    _syncRootListHeight$2: function Open_TestHarness_Views_SidebarView$_syncRootListHeight$2() {
        this.get_rootList().get_container().css(Open.Core.Css.bottom, this.get_methodList().get_container().height() + Open.Core.Css.px);
    },
    
    _syncTestListVisibility$2: function Open_TestHarness_Views_SidebarView$_syncTestListVisibility$2() {
        var node = this._rootList$2.get_selectedParent();
        this.set_isTestListVisible(node != null && (Type.canCast(node, Open.TestHarness.Models.PackageListItem)));
    },
    
    _getTargetTestListHeight$2: function Open_TestHarness_Views_SidebarView$_getTargetTestListHeight$2() {
        /// <returns type="Number" integer="true"></returns>
        return 250;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Views.MethodListView

Open.TestHarness.Views.MethodListView = function Open_TestHarness_Views_MethodListView(container) {
    /// <summary>
    /// The list of tests.
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// The containing div.
    /// </param>
    /// <field name="__methodClicked$2" type="EventHandler">
    /// </field>
    /// <field name="propClassInfo" type="String" static="true">
    /// </field>
    /// <field name="propSelectedMethod" type="String" static="true">
    /// </field>
    /// <field name="_listView$2" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_rootNode$2" type="Open.Core.Lists.ListItem">
    /// </field>
    Open.TestHarness.Views.MethodListView.initializeBase(this);
    this.initialize(container);
    this._listView$2 = new Open.Core.Lists.ListTreeView($(Open.TestHarness.CssSelectors.testListContent));
    this._listView$2.set_slideDuration(Open.TestHarness.Views.SidebarView.slideDuration);
    this._rootNode$2 = new Open.Core.Lists.ListItem();
    this._listView$2.set_rootNode(this._rootNode$2);
    this.add_methodClicked(ss.Delegate.create(this, function() {
        Open.Core.Log.debug('!! Method Clicked: ' + this.get_selectedMethod().get_displayName());
    }));
}
Open.TestHarness.Views.MethodListView.prototype = {
    
    add_methodClicked: function Open_TestHarness_Views_MethodListView$add_methodClicked(value) {
        /// <summary>
        /// Fires when each time a method in the list is clicked (see the 'SelectedMethod' property).
        /// </summary>
        /// <param name="value" type="Function" />
        this.__methodClicked$2 = ss.Delegate.combine(this.__methodClicked$2, value);
    },
    remove_methodClicked: function Open_TestHarness_Views_MethodListView$remove_methodClicked(value) {
        /// <summary>
        /// Fires when each time a method in the list is clicked (see the 'SelectedMethod' property).
        /// </summary>
        /// <param name="value" type="Function" />
        this.__methodClicked$2 = ss.Delegate.remove(this.__methodClicked$2, value);
    },
    
    __methodClicked$2: null,
    
    _fireMethodClicked$2: function Open_TestHarness_Views_MethodListView$_fireMethodClicked$2() {
        if (this.__methodClicked$2 != null) {
            this.__methodClicked$2.invoke(this, new ss.EventArgs());
        }
    },
    
    _listView$2: null,
    _rootNode$2: null,
    
    _onItemClick$2: function Open_TestHarness_Views_MethodListView$_onItemClick$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.set_selectedMethod((sender).get_method());
        this._fireMethodClicked$2();
    },
    
    get_classInfo: function Open_TestHarness_Views_MethodListView$get_classInfo() {
        /// <summary>
        /// Gets or sets the test class the view is listing methods for.
        /// </summary>
        /// <value type="Open.TestHarness.Models.ClassInfo"></value>
        return this.get(Open.TestHarness.Views.MethodListView.propClassInfo, null);
    },
    set_classInfo: function Open_TestHarness_Views_MethodListView$set_classInfo(value) {
        /// <summary>
        /// Gets or sets the test class the view is listing methods for.
        /// </summary>
        /// <value type="Open.TestHarness.Models.ClassInfo"></value>
        if (this.set(Open.TestHarness.Views.MethodListView.propClassInfo, value, null)) {
            this._populateList$2(value);
        }
        return value;
    },
    
    get_selectedMethod: function Open_TestHarness_Views_MethodListView$get_selectedMethod() {
        /// <summary>
        /// Gets or sets the currently selected method..
        /// </summary>
        /// <value type="Open.TestHarness.Models.MethodInfo"></value>
        return this.get(Open.TestHarness.Views.MethodListView.propSelectedMethod, null);
    },
    set_selectedMethod: function Open_TestHarness_Views_MethodListView$set_selectedMethod(value) {
        /// <summary>
        /// Gets or sets the currently selected method..
        /// </summary>
        /// <value type="Open.TestHarness.Models.MethodInfo"></value>
        this.set(Open.TestHarness.Views.MethodListView.propSelectedMethod, value, null);
        return value;
    },
    
    updateLayout: function Open_TestHarness_Views_MethodListView$updateLayout() {
        /// <summary>
        /// Updates the visual state of the control.
        /// </summary>
        this._listView$2.updateLayout();
    },
    
    _populateList$2: function Open_TestHarness_Views_MethodListView$_populateList$2(cclass) {
        /// <param name="cclass" type="Open.TestHarness.Models.ClassInfo">
        /// </param>
        this._clearChildren$2();
        if (cclass == null) {
            return;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(cclass);
        while ($enum1.moveNext()) {
            var method = $enum1.get_current();
            this._rootNode$2.addChild(this._createListItem$2(method));
        }
    },
    
    _createListItem$2: function Open_TestHarness_Views_MethodListView$_createListItem$2(method) {
        /// <param name="method" type="Open.TestHarness.Models.MethodInfo">
        /// </param>
        /// <returns type="Open.TestHarness.Models.MethodListItem"></returns>
        var item = new Open.TestHarness.Models.MethodListItem(method);
        item.add_click(ss.Delegate.create(this, this._onItemClick$2));
        return item;
    },
    
    _clearChildren$2: function Open_TestHarness_Views_MethodListView$_clearChildren$2() {
        var $enum1 = ss.IEnumerator.getEnumerator(this._rootNode$2.get_children());
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            child.remove_click(ss.Delegate.create(this, this._onItemClick$2));
        }
        this._rootNode$2.clearChildren();
        this.set_selectedMethod(null);
    }
}


Open.TestHarness.CssSelectors.registerClass('Open.TestHarness.CssSelectors');
Open.TestHarness.Elements.registerClass('Open.TestHarness.Elements');
Open.TestHarness.Application.registerClass('Open.TestHarness.Application');
Open.TestHarness.Models.MethodListItem.registerClass('Open.TestHarness.Models.MethodListItem', Open.Core.Lists.ListItem);
Open.TestHarness.Models.ClassListItem.registerClass('Open.TestHarness.Models.ClassListItem', Open.Core.Lists.ListItem);
Open.TestHarness.Models.MethodInfo.registerClass('Open.TestHarness.Models.MethodInfo', Open.Core.ModelBase);
Open.TestHarness.Models.PackageInfo.registerClass('Open.TestHarness.Models.PackageInfo', Open.Core.ModelBase, ss.IEnumerable);
Open.TestHarness.Models.ClassInfo.registerClass('Open.TestHarness.Models.ClassInfo', Open.Core.ModelBase, ss.IEnumerable);
Open.TestHarness.Models.PackageListItem.registerClass('Open.TestHarness.Models.PackageListItem', Open.Core.Lists.ListItem);
Open.TestHarness.Models.TestPackageLoader.registerClass('Open.TestHarness.Models.TestPackageLoader', null, ss.IDisposable);
Open.TestHarness.Controllers.PanelResizeController.registerClass('Open.TestHarness.Controllers.PanelResizeController', Open.Core.ControllerBase);
Open.TestHarness.Controllers.SidebarController.registerClass('Open.TestHarness.Controllers.SidebarController', Open.Core.ControllerBase);
Open.TestHarness.Controllers.MyNode.registerClass('Open.TestHarness.Controllers.MyNode', Open.Core.Lists.ListItem);
Open.TestHarness.Controllers.TestPackageController.registerClass('Open.TestHarness.Controllers.TestPackageController', Open.Core.ControllerBase);
Open.TestHarness.Views.ShellView.registerClass('Open.TestHarness.Views.ShellView', Open.Core.ViewBase);
Open.TestHarness.Views.SidebarView.registerClass('Open.TestHarness.Views.SidebarView', Open.Core.ViewBase);
Open.TestHarness.Views.MethodListView.registerClass('Open.TestHarness.Views.MethodListView', Open.Core.ViewBase);
Open.TestHarness.CssSelectors.root = '#testHarness';
Open.TestHarness.CssSelectors.sidebar = '#testHarnessSidebar';
Open.TestHarness.CssSelectors.sidebarRootList = '#testHarnessSidebar .th-sidebarRootList';
Open.TestHarness.CssSelectors.sidebarToolbar = '#testHarnessSidebar div.th-toolbar';
Open.TestHarness.CssSelectors.backMask = '#testHarnessSidebar img.th-backMask';
Open.TestHarness.CssSelectors.testList = '#testHarnessSidebar .th-testList';
Open.TestHarness.CssSelectors.testListContent = '#testHarnessSidebar .th-testList-content';
Open.TestHarness.CssSelectors.main = '#testHarness .th-main';
Open.TestHarness.CssSelectors.logTitlebar = '#testHarnessLog .th-log-tb';
Open.TestHarness.CssSelectors.log = '#testHarnessLog .c-log';
Open.TestHarness.Elements.root = 'testHarness';
Open.TestHarness.Elements.outputLog = 'testHarnessLog';
Open.TestHarness.Application._shell = null;
Open.TestHarness.Application._resizeController = null;
Open.TestHarness.Application._sidebarController = null;
Open.TestHarness.Models.MethodInfo.keyConstructor = 'constructor';
Open.TestHarness.Models.MethodInfo.keyGetter = 'get_';
Open.TestHarness.Models.MethodInfo.keySetter = 'set_';
Open.TestHarness.Models.MethodInfo.keyField = '_';
Open.TestHarness.Models.MethodInfo.keyFunction = 'function';
Open.TestHarness.Models.PackageInfo._singletons$1 = [];
Open.TestHarness.Models.ClassInfo._singletons$1 = null;
Open.TestHarness.Controllers.PanelResizeController._sidebarMinWidth$2 = 200;
Open.TestHarness.Controllers.PanelResizeController._sidebarMaxWidthMargin$2 = 80;
Open.TestHarness.Controllers.PanelResizeController._outputLogMaxHeightMargin$2 = 80;
Open.TestHarness.Controllers.TestPackageController.propSelectedClass = 'SelectedClass';
Open.TestHarness.Controllers.TestPackageController._loadTimeout$2 = 10;
Open.TestHarness.Views.SidebarView.slideDuration = 0.2;
Open.TestHarness.Views.SidebarView.propIsTestListVisible = 'IsTestListVisible';
Open.TestHarness.Views.MethodListView.propClassInfo = 'ClassInfo';
Open.TestHarness.Views.MethodListView.propSelectedMethod = 'SelectedMethod';

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('TestHarness.Script3', ['Open.Core.Views','Open.Core.Script','Open.Core.Lists'], executeScript);
})();
