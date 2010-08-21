//! Open.Core.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.IViewFactory

Open.Core.IViewFactory = function() { 
    /// <summary>
    /// Factory for constructing views.
    /// </summary>
};
Open.Core.IViewFactory.prototype = {
    createView : null
}
Open.Core.IViewFactory.registerInterface('Open.Core.IViewFactory');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.IView

Open.Core.IView = function() { 
    /// <summary>
    /// The logical controller for a view (visual UI) contained with an HTML element.
    /// </summary>
};
Open.Core.IView.prototype = {
    get_isDisposed : null,
    get_isInitialized : null,
    initialize : null,
    dispose : null
}
Open.Core.IView.registerInterface('Open.Core.IView');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.ViewBase

Open.Core.ViewBase = function Open_Core_ViewBase() {
    /// <field name="_isDisposed" type="Boolean">
    /// </field>
    /// <field name="_isInitialized" type="Boolean">
    /// </field>
    /// <field name="_model" type="Object">
    /// </field>
    /// <field name="_element" type="jQueryObject">
    /// </field>
}
Open.Core.ViewBase.prototype = {
    _isDisposed: false,
    _isInitialized: false,
    _model: null,
    _element: null,
    
    dispose: function Open_Core_ViewBase$dispose() {
        /// <summary>
        /// Destroys the view and cleans up resources.
        /// </summary>
        if (this._isDisposed) {
            return;
        }
        this.onDispose();
        this._isDisposed = true;
    },
    
    onDispose: function Open_Core_ViewBase$onDispose() {
        /// <summary>
        /// Deriving implementation of Dispose.
        /// </summary>
    },
    
    get_isDisposed: function Open_Core_ViewBase$get_isDisposed() {
        /// <value type="Boolean"></value>
        return this._isDisposed;
    },
    
    get_isInitialized: function Open_Core_ViewBase$get_isInitialized() {
        /// <value type="Boolean"></value>
        return this._isInitialized;
    },
    
    get_element: function Open_Core_ViewBase$get_element() {
        /// <summary>
        /// Gets the element that the view is contained within.
        /// </summary>
        /// <value type="jQueryObject"></value>
        return this._element;
    },
    
    initialize: function Open_Core_ViewBase$initialize(element) {
        /// <param name="element" type="jQueryObject">
        /// </param>
        if (this.get_isInitialized()) {
            throw new Error('View is already initialized.');
        }
        this._element = element;
        this.onInitialize(element);
        this._isInitialized = true;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Css

Open.Core.Css = function Open_Core_Css() {
    /// <summary>
    /// CSS constants.
    /// </summary>
    /// <field name="left" type="String" static="true">
    /// </field>
    /// <field name="right" type="String" static="true">
    /// </field>
    /// <field name="top" type="String" static="true">
    /// </field>
    /// <field name="bottom" type="String" static="true">
    /// </field>
    /// <field name="width" type="String" static="true">
    /// </field>
    /// <field name="height" type="String" static="true">
    /// </field>
    /// <field name="px" type="String" static="true">
    /// </field>
}
Open.Core.Css.toId = function Open_Core_Css$toId(identifier) {
    /// <summary>
    /// Prepends the # to a CSS identifier (eg: id='one' would be '#one').
    /// </summary>
    /// <param name="identifier" type="String">
    /// The ID value.
    /// </param>
    /// <returns type="String"></returns>
    if (String.isNullOrEmpty(identifier)) {
        return identifier;
    }
    return (identifier.substr(0, 1) === '#') ? identifier : '#' + identifier;
}
Open.Core.Css.selectFromId = function Open_Core_Css$selectFromId(identifier) {
    /// <summary>
    /// Performs a jQuery CSS selection with the given ID
    /// (pre-processing the ID format using the ToId() method).
    /// </summary>
    /// <param name="identifier" type="String">
    /// The ID of the element.
    /// </param>
    /// <returns type="jQueryObject"></returns>
    return $(Open.Core.Css.toId(identifier));
}
Open.Core.Css.insert = function Open_Core_Css$insert(url) {
    /// <summary>
    /// Inserts a CSS link witin the document head.
    /// </summary>
    /// <param name="url" type="String">
    /// The URL of the CSS to load.
    /// </param>
    var link = String.format('<link rel=\'Stylesheet\' href=\'{0}\' type=\'text/css\' />', url);
    $('head').append(link);
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Events

Open.Core.Events = function Open_Core_Events() {
    /// <field name="resize" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Cookie

Open.Core.Cookie = function Open_Core_Cookie(cookieId) {
    /// <summary>
    /// Stores a set of properties within a cookie.
    /// </summary>
    /// <param name="cookieId" type="String">
    /// The unique identifier of the cookie.
    /// </param>
    /// <field name="_id" type="String">
    /// </field>
    /// <field name="_expires" type="Number" integer="true">
    /// </field>
    /// <field name="_propertyBag" type="Open.Core.PropertyBag">
    /// </field>
    this._id = cookieId;
    this._createPropertyBag();
}
Open.Core.Cookie.prototype = {
    _id: null,
    _expires: 0,
    _propertyBag: null,
    
    get_id: function Open_Core_Cookie$get_id() {
        /// <summary>
        /// Gets the unique identifier of the cookie.
        /// </summary>
        /// <value type="String"></value>
        return this._id;
    },
    set_id: function Open_Core_Cookie$set_id(value) {
        /// <summary>
        /// Gets the unique identifier of the cookie.
        /// </summary>
        /// <value type="String"></value>
        this._id = value;
        return value;
    },
    
    get_expires: function Open_Core_Cookie$get_expires() {
        /// <summary>
        /// Gets or sets the lifespan of the cookie (days).
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._expires;
    },
    set_expires: function Open_Core_Cookie$set_expires(value) {
        /// <summary>
        /// Gets or sets the lifespan of the cookie (days).
        /// </summary>
        /// <value type="Number" integer="true"></value>
        if (value < 0) {
            value = 0;
        }
        this._expires = value;
        return value;
    },
    
    save: function Open_Core_Cookie$save() {
        /// <summary>
        /// Saves the properties to the cookie.
        /// </summary>
        $.cookie(this.get_id(), this._propertyBag.toJson(), { expires: this.get_expires() });
    },
    
    clear: function Open_Core_Cookie$clear() {
        /// <summary>
        /// Deletes the cookie (and all associated property values).
        /// </summary>
        $.cookie(this.get_id(), null);
        this._createPropertyBag();
    },
    
    set: function Open_Core_Cookie$set(key, value) {
        /// <summary>
        /// Stores the given value.
        /// </summary>
        /// <param name="key" type="String">
        /// The unique identifier of the property.
        /// </param>
        /// <param name="value" type="Object">
        /// The value to store.
        /// </param>
        this._propertyBag.set(key, value);
    },
    
    get: function Open_Core_Cookie$get(key) {
        /// <summary>
        /// Retrieve the specified value.
        /// </summary>
        /// <param name="key" type="String">
        /// The unique identifier of the property.
        /// </param>
        /// <returns type="Object"></returns>
        return this._propertyBag.get(key);
    },
    
    hasValue: function Open_Core_Cookie$hasValue(key) {
        /// <summary>
        /// Determines whether there is a value for the given key.
        /// </summary>
        /// <param name="key" type="String">
        /// The unique identifier of the property.
        /// </param>
        /// <returns type="Boolean"></returns>
        return this._propertyBag.hasValue(key);
    },
    
    _createPropertyBag: function Open_Core_Cookie$_createPropertyBag() {
        var json = this._readCookie();
        this._propertyBag = (String.isNullOrEmpty(json)) ? Open.Core.PropertyBag.create() : Open.Core.PropertyBag.fromJson(json);
    },
    
    _readCookie: function Open_Core_Cookie$_readCookie() {
        /// <returns type="String"></returns>
        return Type.safeCast($.cookie(this.get_id()), String);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.PropertyBag

Open.Core.PropertyBag = function Open_Core_PropertyBag(json) {
    /// <summary>
    /// Stores properties in a backing JavaScript object.
    /// </summary>
    /// <param name="json" type="String">
    /// </param>
    /// <field name="_backingObject" type="Object">
    /// </field>
    if (ss.isNullOrUndefined(json)) {
        this._backingObject =  {};
    }
    else {
        this._backingObject = Open.Core.Helper.get_json().parse(json);
    }
}
Open.Core.PropertyBag.create = function Open_Core_PropertyBag$create() {
    /// <summary>
    /// Factory method.  Create an empty property bag.
    /// </summary>
    /// <returns type="Open.Core.PropertyBag"></returns>
    return new Open.Core.PropertyBag(null);
}
Open.Core.PropertyBag.fromJson = function Open_Core_PropertyBag$fromJson(json) {
    /// <summary>
    /// Reconstructs a property-bag from the given JSON string.
    /// </summary>
    /// <param name="json" type="String">
    /// The JSON string to parse.
    /// </param>
    /// <returns type="Open.Core.PropertyBag"></returns>
    return new Open.Core.PropertyBag(json);
}
Open.Core.PropertyBag.prototype = {
    _backingObject: null,
    
    get_data: function Open_Core_PropertyBag$get_data() {
        /// <summary>
        /// Gets the backing JavaScript JSON object.
        /// </summary>
        /// <value type="Object"></value>
        return this._backingObject;
    },
    
    get: function Open_Core_PropertyBag$get(key) {
        /// <summary>
        /// Retrieve the specified value.
        /// </summary>
        /// <param name="key" type="String">
        /// The unique identifier of the property.
        /// </param>
        /// <returns type="Object"></returns>
        var script = String.format('this._backingObject.{0}', key);
        return eval(script);
    },
    
    set: function Open_Core_PropertyBag$set(key, value) {
        /// <summary>
        /// Stores the given value.
        /// </summary>
        /// <param name="key" type="String">
        /// The unique identifier of the property.
        /// </param>
        /// <param name="value" type="Object">
        /// The value to store.
        /// </param>
        if (Open.Core.Helper.get_reflection().isString(value)) {
            value = String.format('\'{0}\'', value);
        }
        var script = String.format('this._backingObject.{0} = {1}', key, value);
        eval(script);
    },
    
    hasValue: function Open_Core_PropertyBag$hasValue(key) {
        /// <summary>
        /// Determines whether there is a value for the given key.
        /// </summary>
        /// <param name="key" type="String">
        /// The unique identifier of the property.
        /// </param>
        /// <returns type="Boolean"></returns>
        return !ss.isNullOrUndefined(this.get(key));
    },
    
    toJson: function Open_Core_PropertyBag$toJson() {
        /// <summary>
        /// Converts the property-bag to a JSON string.
        /// </summary>
        /// <returns type="String"></returns>
        return Open.Core.Helper.get_json().serialize(this._backingObject);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Size

Open.Core.Size = function Open_Core_Size(width, height) {
    /// <summary>
    /// Represents a width and a height.
    /// </summary>
    /// <param name="width" type="Number">
    /// The pixel width of the element.
    /// </param>
    /// <param name="height" type="Number">
    /// The pixel height of the element.
    /// </param>
    /// <field name="_width" type="Number">
    /// </field>
    /// <field name="_height" type="Number">
    /// </field>
    this._width = width;
    this._height = height;
}
Open.Core.Size.prototype = {
    _width: 0,
    _height: 0,
    
    get_width: function Open_Core_Size$get_width() {
        /// <summary>
        /// Gets or sets the pixel width of the element.
        /// </summary>
        /// <value type="Number"></value>
        return this._width;
    },
    
    get_height: function Open_Core_Size$get_height() {
        /// <summary>
        /// Gets or sets the pixel height of the element.
        /// </summary>
        /// <value type="Number"></value>
        return this._height;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helper

Open.Core.Helper = function Open_Core_Helper() {
    /// <summary>
    /// Static index of helpers.
    /// </summary>
    /// <field name="_delegateHelper" type="Open.Core.Helpers.DelegateHelper" static="true">
    /// </field>
    /// <field name="_jsonHelper" type="Open.Core.Helpers.JsonHelper" static="true">
    /// </field>
    /// <field name="_reflectionHelper" type="Open.Core.Helpers.ReflectionHelper" static="true">
    /// </field>
    /// <field name="_scriptLoadHelper" type="Open.Core.Helpers.ScriptLoadHelper" static="true">
    /// </field>
    /// <field name="_idCounter" type="Number" integer="true" static="true">
    /// </field>
}
Open.Core.Helper.get_delegate = function Open_Core_Helper$get_delegate() {
    /// <summary>
    /// Gets the helper for working with Delegates.
    /// </summary>
    /// <value type="Open.Core.Helpers.DelegateHelper"></value>
    return Open.Core.Helper._delegateHelper;
}
Open.Core.Helper.get_json = function Open_Core_Helper$get_json() {
    /// <summary>
    /// Gets the helper for working with Delegates.
    /// </summary>
    /// <value type="Open.Core.Helpers.JsonHelper"></value>
    return Open.Core.Helper._jsonHelper;
}
Open.Core.Helper.get_reflection = function Open_Core_Helper$get_reflection() {
    /// <summary>
    /// Gets the helper for working with reflection.
    /// </summary>
    /// <value type="Open.Core.Helpers.ReflectionHelper"></value>
    return Open.Core.Helper._reflectionHelper;
}
Open.Core.Helper.get_scriptLoader = function Open_Core_Helper$get_scriptLoader() {
    /// <summary>
    /// Gets the helper for downloading scripts.
    /// </summary>
    /// <value type="Open.Core.Helpers.ScriptLoadHelper"></value>
    return Open.Core.Helper._scriptLoadHelper;
}
Open.Core.Helper.invokeOrDefault = function Open_Core_Helper$invokeOrDefault(action) {
    /// <summary>
    /// Invokes the given action if it's not Null/Undefined.
    /// </summary>
    /// <param name="action" type="Action">
    /// The action to invoke.
    /// </param>
    if (!ss.isNullOrUndefined(action)) {
        action.invoke();
    }
}
Open.Core.Helper.createId = function Open_Core_Helper$createId() {
    /// <summary>
    /// Creates a unique identifier.
    /// </summary>
    /// <returns type="String"></returns>
    Open.Core.Helper._idCounter++;
    return String.format('g.{0}', Open.Core.Helper._idCounter);
}


Type.registerNamespace('Open.Core.Helpers');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.JsonHelper

Open.Core.Helpers.JsonHelper = function Open_Core_Helpers_JsonHelper() {
    /// <summary>
    /// Utility methods for working with JSON.
    /// </summary>
}
Open.Core.Helpers.JsonHelper.prototype = {
    
    serialize: function Open_Core_Helpers_JsonHelper$serialize(value) {
        /// <summary>
        /// Serialized the given object to a JSON string.
        /// </summary>
        /// <param name="value" type="Object">
        /// The object to serialize.
        /// </param>
        /// <returns type="String"></returns>
        return Type.safeCast(JSON.stringify( value ), String);
    },
    
    parse: function Open_Core_Helpers_JsonHelper$parse(json) {
        /// <summary>
        /// Parses the given JSON into an object.
        /// </summary>
        /// <param name="json" type="String">
        /// The JSON to parse.
        /// </param>
        /// <returns type="Object"></returns>
        return JSON.parse( json );
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.ReflectionHelper

Open.Core.Helpers.ReflectionHelper = function Open_Core_Helpers_ReflectionHelper() {
    /// <summary>
    /// Utility methods for working with reflection.
    /// </summary>
}
Open.Core.Helpers.ReflectionHelper.prototype = {
    
    isString: function Open_Core_Helpers_ReflectionHelper$isString(value) {
        /// <summary>
        /// Determines whether the given object is a string.
        /// </summary>
        /// <param name="value" type="Object">
        /// The object to examine.
        /// </param>
        /// <returns type="Boolean"></returns>
        return Type.getInstanceType(value).get_name() === 'String';
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.JitScriptLoader

Open.Core.Helpers.JitScriptLoader = function Open_Core_Helpers_JitScriptLoader() {
    /// <summary>
    /// Utility methods for loading the JIT (Visualization) scripts.
    /// </summary>
    /// <field name="_jitFolder" type="String" static="true">
    /// </field>
    /// <field name="_isBaseLoaded" type="Boolean">
    /// </field>
    /// <field name="_isHypertreeLoaded" type="Boolean">
    /// </field>
}
Open.Core.Helpers.JitScriptLoader.prototype = {
    _isBaseLoaded: false,
    _isHypertreeLoaded: false,
    
    get_isBaseLoaded: function Open_Core_Helpers_JitScriptLoader$get_isBaseLoaded() {
        /// <summary>
        /// Gets whether the base JIT libraries are loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isBaseLoaded;
    },
    
    get_isHypertreeLoaded: function Open_Core_Helpers_JitScriptLoader$get_isHypertreeLoaded() {
        /// <summary>
        /// Gets whether the Hypertree libraries are loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isHypertreeLoaded;
    },
    
    loadBase: function Open_Core_Helpers_JitScriptLoader$loadBase(callback) {
        /// <summary>
        /// Loads the JIT visualization libraries.
        /// </summary>
        /// <param name="callback" type="Action">
        /// Callback to invoke upon completion.
        /// </param>
        if (this.get_isBaseLoaded()) {
            Open.Core.Helper.invokeOrDefault(callback);
            return;
        }
        var loader = this._getBaseLoader();
        loader.add_loadComplete(ss.Delegate.create(this, function() {
            Open.Core.Helper.invokeOrDefault(callback);
        }));
        loader.start();
    },
    
    _getBaseLoader: function Open_Core_Helpers_JitScriptLoader$_getBaseLoader() {
        /// <returns type="Open.Core.Helpers.ScriptLoader"></returns>
        var loader = new Open.Core.Helpers.ScriptLoader();
        loader.add_loadComplete(ss.Delegate.create(this, function() {
            this._isBaseLoaded = true;
        }));
        loader.addUrl(Open.Core.Helper.get_scriptLoader()._url(String.Empty, 'Open.Library.Jit', true));
        loader.addUrl(Open.Core.Helper.get_scriptLoader()._url(Open.Core.Helpers.JitScriptLoader._jitFolder, 'excanvas', false));
        loader.addUrl(Open.Core.Helper.get_scriptLoader()._url(Open.Core.Helpers.JitScriptLoader._jitFolder, 'Jit.Initialize', false));
        return loader;
    },
    
    loadHypertree: function Open_Core_Helpers_JitScriptLoader$loadHypertree(callback) {
        /// <summary>
        /// Loads the Hypertree (and associated) libraries.
        /// </summary>
        /// <param name="callback" type="Action">
        /// Callback to invoke upon completion.
        /// </param>
        if (this.get_isHypertreeLoaded()) {
            Open.Core.Helper.invokeOrDefault(callback);
            return;
        }
        var loader = new Open.Core.Helpers.ScriptLoader();
        loader.add_loadComplete(ss.Delegate.create(this, function() {
            this._isHypertreeLoaded = true;
            Open.Core.Helper.invokeOrDefault(callback);
        }));
        loader.addLoader(this._getBaseLoader());
        loader.addUrl(Open.Core.Helper.get_scriptLoader()._url(Open.Core.Helpers.JitScriptLoader._jitFolder, 'HyperTree', true));
        loader.addUrl(Open.Core.Helper.get_scriptLoader()._url(Open.Core.Helpers.JitScriptLoader._jitFolder, 'HyperTree.Initialize', false));
        loader.start();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.ResourceLoader

Open.Core.Helpers.ResourceLoader = function Open_Core_Helpers_ResourceLoader() {
    /// <summary>
    /// Handles loading a collection of resources.
    /// </summary>
    /// <field name="__loadComplete" type="EventHandler">
    /// </field>
    /// <field name="_loadedCallbackTotal" type="Number" integer="true">
    /// </field>
    /// <field name="_urls" type="Array">
    /// </field>
    /// <field name="_loaders" type="Array">
    /// </field>
    this._urls = [];
    this._loaders = [];
}
Open.Core.Helpers.ResourceLoader.prototype = {
    
    add_loadComplete: function Open_Core_Helpers_ResourceLoader$add_loadComplete(value) {
        /// <param name="value" type="Function" />
        this.__loadComplete = ss.Delegate.combine(this.__loadComplete, value);
    },
    remove_loadComplete: function Open_Core_Helpers_ResourceLoader$remove_loadComplete(value) {
        /// <param name="value" type="Function" />
        this.__loadComplete = ss.Delegate.remove(this.__loadComplete, value);
    },
    
    __loadComplete: null,
    
    _fireLoadComplete: function Open_Core_Helpers_ResourceLoader$_fireLoadComplete() {
        if (this.__loadComplete != null) {
            this.__loadComplete.invoke(this, new ss.EventArgs());
        }
    },
    
    _loadedCallbackTotal: 0,
    
    get_isLoaded: function Open_Core_Helpers_ResourceLoader$get_isLoaded() {
        /// <value type="Boolean"></value>
        if (this._loadedCallbackTotal < this._urls.length) {
            return false;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(this._loaders);
        while ($enum1.moveNext()) {
            var loader = $enum1.get_current();
            if (!loader.get_isLoaded()) {
                return false;
            }
        }
        return true;
    },
    
    addUrl: function Open_Core_Helpers_ResourceLoader$addUrl(url) {
        /// <param name="url" type="String">
        /// </param>
        this._urls.add(url);
    },
    
    addLoader: function Open_Core_Helpers_ResourceLoader$addLoader(loader) {
        /// <param name="loader" type="Open.Core.Helpers.ResourceLoader">
        /// </param>
        this._loaders.add(loader);
    },
    
    start: function Open_Core_Helpers_ResourceLoader$start() {
        var $enum1 = ss.IEnumerator.getEnumerator(this._urls);
        while ($enum1.moveNext()) {
            var url = $enum1.get_current();
            this.loadResource(url, ss.Delegate.create(this, function() {
                this._loadedCallbackTotal++;
                this._onDownloaded();
            }));
        }
        var $enum2 = ss.IEnumerator.getEnumerator(this._loaders);
        while ($enum2.moveNext()) {
            var loader = $enum2.get_current();
            if (loader.get_isLoaded()) {
                continue;
            }
            loader.add_loadComplete(ss.Delegate.create(this, function() {
                this._onDownloaded();
            }));
            loader.start();
        }
    },
    
    _onDownloaded: function Open_Core_Helpers_ResourceLoader$_onDownloaded() {
        if (this.get_isLoaded()) {
            this._fireLoadComplete();
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.ScriptLoader

Open.Core.Helpers.ScriptLoader = function Open_Core_Helpers_ScriptLoader() {
    /// <summary>
    /// Handles loading a collection of scripts.
    /// </summary>
    Open.Core.Helpers.ScriptLoader.initializeBase(this);
}
Open.Core.Helpers.ScriptLoader.prototype = {
    
    loadResource: function Open_Core_Helpers_ScriptLoader$loadResource(url, onDownloaded) {
        /// <param name="url" type="String">
        /// </param>
        /// <param name="onDownloaded" type="Action">
        /// </param>
        $.getScript(url, ss.Delegate.create(this, function(data) {
            onDownloaded.invoke();
        }));
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.ScriptLoadHelper

Open.Core.Helpers.ScriptLoadHelper = function Open_Core_Helpers_ScriptLoadHelper() {
    /// <summary>
    /// Utility methods for loading scripts.
    /// </summary>
    /// <field name="_rootScriptFolder" type="String">
    /// </field>
    /// <field name="_useDebug" type="Boolean">
    /// </field>
    /// <field name="_jit" type="Open.Core.Helpers.JitScriptLoader">
    /// </field>
    /// <field name="_isListsLoaded" type="Boolean">
    /// </field>
}
Open.Core.Helpers.ScriptLoadHelper.prototype = {
    _rootScriptFolder: '/Open.Core/Scripts/',
    _useDebug: false,
    _jit: null,
    _isListsLoaded: false,
    
    get_useDebug: function Open_Core_Helpers_ScriptLoadHelper$get_useDebug() {
        /// <summary>
        /// Gets or sets whether the debug version of scripts should be used.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._useDebug;
    },
    set_useDebug: function Open_Core_Helpers_ScriptLoadHelper$set_useDebug(value) {
        /// <summary>
        /// Gets or sets whether the debug version of scripts should be used.
        /// </summary>
        /// <value type="Boolean"></value>
        this._useDebug = value;
        return value;
    },
    
    get_rootScriptFolder: function Open_Core_Helpers_ScriptLoadHelper$get_rootScriptFolder() {
        /// <summary>
        /// Gets or sets the root folder where the script libraries are housed.
        /// </summary>
        /// <value type="String"></value>
        return this._rootScriptFolder;
    },
    set_rootScriptFolder: function Open_Core_Helpers_ScriptLoadHelper$set_rootScriptFolder(value) {
        /// <summary>
        /// Gets or sets the root folder where the script libraries are housed.
        /// </summary>
        /// <value type="String"></value>
        this._rootScriptFolder = value;
        return value;
    },
    
    get_jit: function Open_Core_Helpers_ScriptLoadHelper$get_jit() {
        /// <summary>
        /// Gets the JIT (visualization library) loader.
        /// </summary>
        /// <value type="Open.Core.Helpers.JitScriptLoader"></value>
        return this._jit || (this._jit = new Open.Core.Helpers.JitScriptLoader());
    },
    
    get_isListsLoaded: function Open_Core_Helpers_ScriptLoadHelper$get_isListsLoaded() {
        /// <summary>
        /// Gets whether the Lists library has been loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isListsLoaded;
    },
    
    loadLists: function Open_Core_Helpers_ScriptLoadHelper$loadLists(callback) {
        /// <summary>
        /// Loads the Lists library.
        /// </summary>
        /// <param name="callback" type="Action">
        /// Callback to invoke upon completion.
        /// </param>
        if (this.get_isListsLoaded()) {
            Open.Core.Helper.invokeOrDefault(callback);
            return;
        }
        var loader = new Open.Core.Helpers.ScriptLoader();
        loader.add_loadComplete(ss.Delegate.create(this, function() {
            this._isListsLoaded = true;
            Open.Core.Helper.invokeOrDefault(callback);
        }));
        loader.addUrl(Open.Core.Helper.get_scriptLoader()._url(String.Empty, 'Open.Core.Lists', true));
        loader.start();
    },
    
    _url: function Open_Core_Helpers_ScriptLoadHelper$_url(path, fileName, hasDebug) {
        /// <param name="path" type="String">
        /// </param>
        /// <param name="fileName" type="String">
        /// </param>
        /// <param name="hasDebug" type="Boolean">
        /// </param>
        /// <returns type="String"></returns>
        return String.format(this.get_rootScriptFolder() + path + this._fileName(fileName, hasDebug));
    },
    
    _fileName: function Open_Core_Helpers_ScriptLoadHelper$_fileName(name, hasDebug) {
        /// <param name="name" type="String">
        /// </param>
        /// <param name="hasDebug" type="Boolean">
        /// </param>
        /// <returns type="String"></returns>
        var debug = (hasDebug && this.get_useDebug()) ? '.debug' : null;
        return String.format('{0}{1}.js', name, debug);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.DelegateHelper

Open.Core.Helpers.DelegateHelper = function Open_Core_Helpers_DelegateHelper() {
    /// <summary>
    /// Utility methods for working with delegates.
    /// </summary>
}
Open.Core.Helpers.DelegateHelper.prototype = {
    
    toCallbackString: function Open_Core_Helpers_DelegateHelper$toCallbackString(callback) {
        /// <summary>
        /// Formats a callback function to a JavaScript function name.
        /// </summary>
        /// <param name="callback" type="ss.Delegate">
        /// The callback delegate.
        /// </param>
        /// <returns type="String"></returns>
        return 'ss.Delegate.' + ss.Delegate.createExport(callback, true);
    },
    
    toEventCallbackString: function Open_Core_Helpers_DelegateHelper$toEventCallbackString(callback, eventIdentifier) {
        /// <summary>
        /// Formats a callback function with the specified event identifier.
        /// </summary>
        /// <param name="callback" type="Open.Core.EventCallback">
        /// The callback delegate.
        /// </param>
        /// <param name="eventIdentifier" type="String">
        /// The event identifier.
        /// </param>
        /// <returns type="String"></returns>
        var func = String.format('{0}(\'{1}\');', this.toCallbackString(callback), eventIdentifier);
        return 'function(e,ui){ ' + func + ' }';
    }
}


Type.registerNamespace('Open.Core.UI');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.UI.HorizontalPanelResizer

Open.Core.UI.HorizontalPanelResizer = function Open_Core_UI_HorizontalPanelResizer(cssSelector, cookieKey) {
    /// <summary>
    /// Controls the resizing of a panel on the X plane.
    /// </summary>
    /// <param name="cssSelector" type="String">
    /// The CSS selector used to retrieve the panel being resized.
    /// </param>
    /// <param name="cookieKey" type="String">
    /// The unique key to store the panel size within (null if saving not required).
    /// </param>
    /// <field name="_minWidth$1" type="Number">
    /// </field>
    /// <field name="_maxWidthMargin$1" type="Number">
    /// </field>
    Open.Core.UI.HorizontalPanelResizer.initializeBase(this, [ cssSelector, cookieKey ]);
}
Open.Core.UI.HorizontalPanelResizer.prototype = {
    _minWidth$1: 0,
    _maxWidthMargin$1: 0,
    
    get_minWidth: function Open_Core_UI_HorizontalPanelResizer$get_minWidth() {
        /// <summary>
        /// Gets or sets the minimum width the panel can be.
        /// </summary>
        /// <value type="Number"></value>
        return this._minWidth$1;
    },
    set_minWidth: function Open_Core_UI_HorizontalPanelResizer$set_minWidth(value) {
        /// <summary>
        /// Gets or sets the minimum width the panel can be.
        /// </summary>
        /// <value type="Number"></value>
        if (value === this._minWidth$1) {
            return;
        }
        this._minWidth$1 = value;
        this._setMinWidth$1();
        return value;
    },
    
    get_maxWidthMargin: function Open_Core_UI_HorizontalPanelResizer$get_maxWidthMargin() {
        /// <summary>
        /// Gets or sets the margin buffer used to calculate the max-width of the panel relative to the root container.
        /// </summary>
        /// <value type="Number"></value>
        return this._maxWidthMargin$1;
    },
    set_maxWidthMargin: function Open_Core_UI_HorizontalPanelResizer$set_maxWidthMargin(value) {
        /// <summary>
        /// Gets or sets the margin buffer used to calculate the max-width of the panel relative to the root container.
        /// </summary>
        /// <value type="Number"></value>
        this._maxWidthMargin$1 = value;
        return value;
    },
    
    get__rootContainerWidth$1: function Open_Core_UI_HorizontalPanelResizer$get__rootContainerWidth$1() {
        /// <value type="Number"></value>
        return (this.get_hasRootContainer()) ? this.getRootContainer().width() : -1;
    },
    
    get__maxWidth$1: function Open_Core_UI_HorizontalPanelResizer$get__maxWidth$1() {
        /// <value type="Number"></value>
        return (this.get_hasRootContainer()) ? this.get__rootContainerWidth$1() - this.get_maxWidthMargin() : -1;
    },
    
    getHandles: function Open_Core_UI_HorizontalPanelResizer$getHandles() {
        /// <returns type="String"></returns>
        return 'e';
    },
    
    onInitialize: function Open_Core_UI_HorizontalPanelResizer$onInitialize() {
        this._setMinMaxWidth$1();
    },
    
    onStopped: function Open_Core_UI_HorizontalPanelResizer$onStopped() {
        this.get_panel().css(Open.Core.Css.height, String.Empty);
    },
    
    onWindowSizeChanged: function Open_Core_UI_HorizontalPanelResizer$onWindowSizeChanged() {
        if (!this.isInitialized) {
            return;
        }
        this._setMinMaxWidth$1();
        if (this.get_hasRootContainer()) {
            this.shrinkIfOverflowing(this.getCurrentSize(), this.get_minWidth(), this.get__maxWidth$1(), Open.Core.Css.width);
        }
    },
    
    getCurrentSize: function Open_Core_UI_HorizontalPanelResizer$getCurrentSize() {
        /// <returns type="Number"></returns>
        return this.get_panel().width();
    },
    
    setCurrentSize: function Open_Core_UI_HorizontalPanelResizer$setCurrentSize(size) {
        /// <param name="size" type="Number">
        /// </param>
        this.get_panel().css(Open.Core.Css.width, size + Open.Core.Css.px);
    },
    
    _setMinMaxWidth$1: function Open_Core_UI_HorizontalPanelResizer$_setMinMaxWidth$1() {
        this._setMinWidth$1();
        this._setMaxWidth$1();
    },
    
    _setMinWidth$1: function Open_Core_UI_HorizontalPanelResizer$_setMinWidth$1() {
        this.setResizeOption('minWidth', this.get_minWidth().toString());
    },
    
    _setMaxWidth$1: function Open_Core_UI_HorizontalPanelResizer$_setMaxWidth$1() {
        var width = (this.get_hasRootContainer()) ? this.get__maxWidth$1().toString() : String.Empty;
        this.setResizeOption('maxWidth', width);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.UI.PanelResizerBase

Open.Core.UI.PanelResizerBase = function Open_Core_UI_PanelResizerBase(cssSelector, cookieKey) {
    /// <summary>
    /// Base class for resizing panels.
    /// </summary>
    /// <param name="cssSelector" type="String">
    /// The CSS selector used to retrieve the panel being resized.
    /// </param>
    /// <param name="cookieKey" type="String">
    /// The unique key to store the panel size within (null if saving not required).
    /// </param>
    /// <field name="__resized" type="EventHandler">
    /// </field>
    /// <field name="__resizeStarted" type="EventHandler">
    /// </field>
    /// <field name="__resizeStopped" type="EventHandler">
    /// </field>
    /// <field name="_eventStart" type="String" static="true">
    /// </field>
    /// <field name="_eventStop" type="String" static="true">
    /// </field>
    /// <field name="_eventResize" type="String" static="true">
    /// </field>
    /// <field name="_rootContainerId" type="String">
    /// </field>
    /// <field name="_panel" type="jQueryObject">
    /// </field>
    /// <field name="_cookieKey" type="String">
    /// </field>
    /// <field name="isInitialized" type="Boolean">
    /// </field>
    /// <field name="_cookie" type="Open.Core.Cookie" static="true">
    /// </field>
    /// <field name="_cssSelector" type="String">
    /// </field>
    /// <field name="_resizeScript" type="String" static="true">
    /// </field>
    this._cssSelector = cssSelector;
    this._panel = $(cssSelector);
    this._cookieKey = cookieKey;
    if (Open.Core.UI.PanelResizerBase._cookie == null) {
        Open.Core.UI.PanelResizerBase._cookie = new Open.Core.Cookie('PanelResizeStore');
        Open.Core.UI.PanelResizerBase._cookie.set_expires(5000);
    }
    $(window).bind(Open.Core.Events.resize, ss.Delegate.create(this, function(e) {
        this.onWindowSizeChanged();
    }));
    this._loadSize();
}
Open.Core.UI.PanelResizerBase.prototype = {
    
    add_resized: function Open_Core_UI_PanelResizerBase$add_resized(value) {
        /// <summary>
        /// Fires during the resize operation.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__resized = ss.Delegate.combine(this.__resized, value);
    },
    remove_resized: function Open_Core_UI_PanelResizerBase$remove_resized(value) {
        /// <summary>
        /// Fires during the resize operation.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__resized = ss.Delegate.remove(this.__resized, value);
    },
    
    __resized: null,
    
    fireResized: function Open_Core_UI_PanelResizerBase$fireResized() {
        if (this.__resized != null) {
            this.__resized.invoke(this, new ss.EventArgs());
        }
    },
    
    add_resizeStarted: function Open_Core_UI_PanelResizerBase$add_resizeStarted(value) {
        /// <summary>
        /// Fires when the resize operation starts.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__resizeStarted = ss.Delegate.combine(this.__resizeStarted, value);
    },
    remove_resizeStarted: function Open_Core_UI_PanelResizerBase$remove_resizeStarted(value) {
        /// <summary>
        /// Fires when the resize operation starts.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__resizeStarted = ss.Delegate.remove(this.__resizeStarted, value);
    },
    
    __resizeStarted: null,
    
    _fireResizeStarted: function Open_Core_UI_PanelResizerBase$_fireResizeStarted() {
        if (this.__resizeStarted != null) {
            this.__resizeStarted.invoke(this, new ss.EventArgs());
        }
    },
    
    add_resizeStopped: function Open_Core_UI_PanelResizerBase$add_resizeStopped(value) {
        /// <summary>
        /// Fires when the resize operation stops.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__resizeStopped = ss.Delegate.combine(this.__resizeStopped, value);
    },
    remove_resizeStopped: function Open_Core_UI_PanelResizerBase$remove_resizeStopped(value) {
        /// <summary>
        /// Fires when the resize operation stops.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__resizeStopped = ss.Delegate.remove(this.__resizeStopped, value);
    },
    
    __resizeStopped: null,
    
    _fireResizeStopped: function Open_Core_UI_PanelResizerBase$_fireResizeStopped() {
        if (this.__resizeStopped != null) {
            this.__resizeStopped.invoke(this, new ss.EventArgs());
        }
    },
    
    _rootContainerId: null,
    _panel: null,
    _cookieKey: null,
    isInitialized: false,
    _cssSelector: null,
    
    get_rootContainerId: function Open_Core_UI_PanelResizerBase$get_rootContainerId() {
        /// <summary>
        /// Gets or sets the unique identifier of the root container of the panel(s).
        /// </summary>
        /// <value type="String"></value>
        return this._rootContainerId;
    },
    set_rootContainerId: function Open_Core_UI_PanelResizerBase$set_rootContainerId(value) {
        /// <summary>
        /// Gets or sets the unique identifier of the root container of the panel(s).
        /// </summary>
        /// <value type="String"></value>
        this._rootContainerId = Open.Core.Css.toId(value);
        return value;
    },
    
    get_hasRootContainer: function Open_Core_UI_PanelResizerBase$get_hasRootContainer() {
        /// <value type="Boolean"></value>
        return !String.isNullOrEmpty(this.get_rootContainerId());
    },
    
    get_isSaving: function Open_Core_UI_PanelResizerBase$get_isSaving() {
        /// <value type="Boolean"></value>
        return !String.isNullOrEmpty(this._cookieKey);
    },
    
    get_panel: function Open_Core_UI_PanelResizerBase$get_panel() {
        /// <value type="jQueryObject"></value>
        return this._panel;
    },
    
    initialize: function Open_Core_UI_PanelResizerBase$initialize() {
        /// <summary>
        /// Sets the panel up to be resizable.
        /// </summary>
        var eventCallback = ss.Delegate.create(this, function(eventName) {
            this._handleEvent(eventName);
        });
        var script = String.format(Open.Core.UI.PanelResizerBase._resizeScript, this._cssSelector, this.getHandles(), Open.Core.Helper.get_delegate().toEventCallbackString(eventCallback, Open.Core.UI.PanelResizerBase._eventStart), Open.Core.Helper.get_delegate().toEventCallbackString(eventCallback, Open.Core.UI.PanelResizerBase._eventStop), Open.Core.Helper.get_delegate().toEventCallbackString(eventCallback, Open.Core.UI.PanelResizerBase._eventResize));
        eval(script);
        this.onInitialize();
        this.isInitialized = true;
    },
    
    onInitialize: function Open_Core_UI_PanelResizerBase$onInitialize() {
    },
    
    onStarted: function Open_Core_UI_PanelResizerBase$onStarted() {
    },
    
    onResize: function Open_Core_UI_PanelResizerBase$onResize() {
    },
    
    onStopped: function Open_Core_UI_PanelResizerBase$onStopped() {
    },
    
    onWindowSizeChanged: function Open_Core_UI_PanelResizerBase$onWindowSizeChanged() {
    },
    
    getRootContainer: function Open_Core_UI_PanelResizerBase$getRootContainer() {
        /// <returns type="jQueryObject"></returns>
        return (this.get_hasRootContainer()) ? $(this.get_rootContainerId()) : null;
    },
    
    setResizeOption: function Open_Core_UI_PanelResizerBase$setResizeOption(option, value) {
        /// <param name="option" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        if (String.isNullOrEmpty(value)) {
            return;
        }
        var script = String.format('$(\'{0}\').resizable(\'option\', \'{1}\', {2});', this._cssSelector, option, value);
        eval(script);
    },
    
    shrinkIfOverflowing: function Open_Core_UI_PanelResizerBase$shrinkIfOverflowing(currentValue, minValue, maxValue, cssAttribute) {
        /// <param name="currentValue" type="Number">
        /// </param>
        /// <param name="minValue" type="Number">
        /// </param>
        /// <param name="maxValue" type="Number">
        /// </param>
        /// <param name="cssAttribute" type="String">
        /// </param>
        if (currentValue <= maxValue) {
            return;
        }
        if (maxValue < minValue) {
            return;
        }
        this.get_panel().css(cssAttribute, maxValue + Open.Core.Css.px);
        this.fireResized();
    },
    
    _handleEvent: function Open_Core_UI_PanelResizerBase$_handleEvent(eventName) {
        /// <param name="eventName" type="String">
        /// </param>
        if (eventName === Open.Core.UI.PanelResizerBase._eventStart) {
            this.onStarted();
            this._fireResizeStarted();
        }
        else if (eventName === Open.Core.UI.PanelResizerBase._eventResize) {
            this.onResize();
            this.fireResized();
        }
        else if (eventName === Open.Core.UI.PanelResizerBase._eventStop) {
            this.onStopped();
            this._saveSize();
            this._fireResizeStopped();
        }
    },
    
    _saveSize: function Open_Core_UI_PanelResizerBase$_saveSize() {
        if (!this.get_isSaving()) {
            return;
        }
        Open.Core.UI.PanelResizerBase._cookie.set(this._cookieKey, this.getCurrentSize());
        Open.Core.UI.PanelResizerBase._cookie.save();
    },
    
    _loadSize: function Open_Core_UI_PanelResizerBase$_loadSize() {
        if (!this.get_isSaving()) {
            return;
        }
        var size = Open.Core.UI.PanelResizerBase._cookie.get(this._cookieKey);
        if (ss.isNullOrUndefined(size)) {
            return;
        }
        this.setCurrentSize(size);
        this.fireResized();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.UI.VerticalPanelResizer

Open.Core.UI.VerticalPanelResizer = function Open_Core_UI_VerticalPanelResizer(cssSelector, cookieKey) {
    /// <summary>
    /// Controls the resizing of a panel on the Y plane.
    /// </summary>
    /// <param name="cssSelector" type="String">
    /// The CSS selector used to retrieve the panel being resized.
    /// </param>
    /// <param name="cookieKey" type="String">
    /// The unique key to store the panel size within (null if saving not required).
    /// </param>
    /// <field name="_minHeight$1" type="Number">
    /// </field>
    /// <field name="_maxHeightMargin$1" type="Number">
    /// </field>
    Open.Core.UI.VerticalPanelResizer.initializeBase(this, [ cssSelector, cookieKey ]);
}
Open.Core.UI.VerticalPanelResizer.prototype = {
    _minHeight$1: 0,
    _maxHeightMargin$1: 0,
    
    get_minHeight: function Open_Core_UI_VerticalPanelResizer$get_minHeight() {
        /// <summary>
        /// Gets or sets the minimum height the panel can be.
        /// </summary>
        /// <value type="Number"></value>
        return this._minHeight$1;
    },
    set_minHeight: function Open_Core_UI_VerticalPanelResizer$set_minHeight(value) {
        /// <summary>
        /// Gets or sets the minimum height the panel can be.
        /// </summary>
        /// <value type="Number"></value>
        if (value === this._minHeight$1) {
            return;
        }
        this._minHeight$1 = value;
        this._setMinHeight$1();
        return value;
    },
    
    get_maxHeightMargin: function Open_Core_UI_VerticalPanelResizer$get_maxHeightMargin() {
        /// <summary>
        /// Gets or sets the margin buffer used to calculate the max-height of the panel relative to the root container.
        /// </summary>
        /// <value type="Number"></value>
        return this._maxHeightMargin$1;
    },
    set_maxHeightMargin: function Open_Core_UI_VerticalPanelResizer$set_maxHeightMargin(value) {
        /// <summary>
        /// Gets or sets the margin buffer used to calculate the max-height of the panel relative to the root container.
        /// </summary>
        /// <value type="Number"></value>
        this._maxHeightMargin$1 = value;
        return value;
    },
    
    get__rootContainerHeight$1: function Open_Core_UI_VerticalPanelResizer$get__rootContainerHeight$1() {
        /// <value type="Number"></value>
        return (this.get_hasRootContainer()) ? this.getRootContainer().height() : -1;
    },
    
    get__maxHeight$1: function Open_Core_UI_VerticalPanelResizer$get__maxHeight$1() {
        /// <value type="Number"></value>
        return (this.get_hasRootContainer()) ? this.get__rootContainerHeight$1() - this.get_maxHeightMargin() : -1;
    },
    
    getHandles: function Open_Core_UI_VerticalPanelResizer$getHandles() {
        /// <returns type="String"></returns>
        return 'n';
    },
    
    onInitialize: function Open_Core_UI_VerticalPanelResizer$onInitialize() {
        this._setMinMaxHeight$1();
    },
    
    onStopped: function Open_Core_UI_VerticalPanelResizer$onStopped() {
        this.get_panel().css(Open.Core.Css.width, String.Empty);
        this.get_panel().css(Open.Core.Css.top, String.Empty);
    },
    
    onWindowSizeChanged: function Open_Core_UI_VerticalPanelResizer$onWindowSizeChanged() {
        if (!this.isInitialized) {
            return;
        }
        this._setMinMaxHeight$1();
        if (this.get_hasRootContainer()) {
            this.shrinkIfOverflowing(this.getCurrentSize(), this.get_minHeight(), this.get__maxHeight$1(), Open.Core.Css.height);
        }
    },
    
    getCurrentSize: function Open_Core_UI_VerticalPanelResizer$getCurrentSize() {
        /// <returns type="Number"></returns>
        return this.get_panel().height();
    },
    
    setCurrentSize: function Open_Core_UI_VerticalPanelResizer$setCurrentSize(size) {
        /// <param name="size" type="Number">
        /// </param>
        this.get_panel().css(Open.Core.Css.height, size + Open.Core.Css.px);
    },
    
    _setMinMaxHeight$1: function Open_Core_UI_VerticalPanelResizer$_setMinMaxHeight$1() {
        this._setMinHeight$1();
        this._setMaxHeight$1();
    },
    
    _setMinHeight$1: function Open_Core_UI_VerticalPanelResizer$_setMinHeight$1() {
        this.setResizeOption('minHeight', this.get_minHeight().toString());
    },
    
    _setMaxHeight$1: function Open_Core_UI_VerticalPanelResizer$_setMaxHeight$1() {
        var height = (this.get_hasRootContainer()) ? this.get__maxHeight$1().toString() : String.Empty;
        this.setResizeOption('maxHeight', height);
    }
}


Open.Core.ViewBase.registerClass('Open.Core.ViewBase', null, Open.Core.IView);
Open.Core.Css.registerClass('Open.Core.Css');
Open.Core.Events.registerClass('Open.Core.Events');
Open.Core.Cookie.registerClass('Open.Core.Cookie');
Open.Core.PropertyBag.registerClass('Open.Core.PropertyBag');
Open.Core.Size.registerClass('Open.Core.Size');
Open.Core.Helper.registerClass('Open.Core.Helper');
Open.Core.Helpers.JsonHelper.registerClass('Open.Core.Helpers.JsonHelper');
Open.Core.Helpers.ReflectionHelper.registerClass('Open.Core.Helpers.ReflectionHelper');
Open.Core.Helpers.JitScriptLoader.registerClass('Open.Core.Helpers.JitScriptLoader');
Open.Core.Helpers.ResourceLoader.registerClass('Open.Core.Helpers.ResourceLoader');
Open.Core.Helpers.ScriptLoader.registerClass('Open.Core.Helpers.ScriptLoader', Open.Core.Helpers.ResourceLoader);
Open.Core.Helpers.ScriptLoadHelper.registerClass('Open.Core.Helpers.ScriptLoadHelper');
Open.Core.Helpers.DelegateHelper.registerClass('Open.Core.Helpers.DelegateHelper');
Open.Core.UI.PanelResizerBase.registerClass('Open.Core.UI.PanelResizerBase');
Open.Core.UI.HorizontalPanelResizer.registerClass('Open.Core.UI.HorizontalPanelResizer', Open.Core.UI.PanelResizerBase);
Open.Core.UI.VerticalPanelResizer.registerClass('Open.Core.UI.VerticalPanelResizer', Open.Core.UI.PanelResizerBase);
Open.Core.Css.left = 'left';
Open.Core.Css.right = 'right';
Open.Core.Css.top = 'top';
Open.Core.Css.bottom = 'bottom';
Open.Core.Css.width = 'width';
Open.Core.Css.height = 'height';
Open.Core.Css.px = 'px';
Open.Core.Events.resize = 'resize';
Open.Core.Helper._delegateHelper = new Open.Core.Helpers.DelegateHelper();
Open.Core.Helper._jsonHelper = new Open.Core.Helpers.JsonHelper();
Open.Core.Helper._reflectionHelper = new Open.Core.Helpers.ReflectionHelper();
Open.Core.Helper._scriptLoadHelper = new Open.Core.Helpers.ScriptLoadHelper();
Open.Core.Helper._idCounter = 0;
Open.Core.Helpers.JitScriptLoader._jitFolder = 'Jit/';
Open.Core.UI.PanelResizerBase._eventStart = 'start';
Open.Core.UI.PanelResizerBase._eventStop = 'eventStop';
Open.Core.UI.PanelResizerBase._eventResize = 'eventResize';
Open.Core.UI.PanelResizerBase._cookie = null;
Open.Core.UI.PanelResizerBase._resizeScript = '\r\n$(\'{0}\').resizable({\r\n    handles: \'{1}\',\r\n    start: {2},\r\n    stop: {3},\r\n    resize: {4}\r\n    });\r\n';

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Script', [], executeScript);
})();
