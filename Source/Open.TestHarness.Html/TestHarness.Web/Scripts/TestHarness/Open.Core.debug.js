//! Open.Core.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core');

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
        this._backingObject = JSON.parse( json );
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
        return Type.safeCast(JSON.stringify( this._backingObject ), String);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.DelegateUtil

Open.Core.DelegateUtil = function Open_Core_DelegateUtil() {
}
Open.Core.DelegateUtil.toCallbackString = function Open_Core_DelegateUtil$toCallbackString(callback) {
    /// <summary>
    /// Formats a callback function to a JavaScript function name.
    /// </summary>
    /// <param name="callback" type="ss.Delegate">
    /// The callback delegate.
    /// </param>
    /// <returns type="String"></returns>
    return 'ss.Delegate.' + ss.Delegate.createExport(callback, true);
}
Open.Core.DelegateUtil.toEventCallbackString = function Open_Core_DelegateUtil$toEventCallbackString(callback, eventIdentifier) {
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
    var func = String.format('{0}(\'{1}\');', Open.Core.DelegateUtil.toCallbackString(callback), eventIdentifier);
    return 'function(e,ui){ ' + func + ' }';
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
        var script = String.format(Open.Core.UI.PanelResizerBase._resizeScript, this._cssSelector, this.getHandles(), Open.Core.DelegateUtil.toEventCallbackString(eventCallback, Open.Core.UI.PanelResizerBase._eventStart), Open.Core.DelegateUtil.toEventCallbackString(eventCallback, Open.Core.UI.PanelResizerBase._eventStop), Open.Core.DelegateUtil.toEventCallbackString(eventCallback, Open.Core.UI.PanelResizerBase._eventResize));
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


Open.Core.Css.registerClass('Open.Core.Css');
Open.Core.Events.registerClass('Open.Core.Events');
Open.Core.Cookie.registerClass('Open.Core.Cookie');
Open.Core.PropertyBag.registerClass('Open.Core.PropertyBag');
Open.Core.DelegateUtil.registerClass('Open.Core.DelegateUtil');
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
