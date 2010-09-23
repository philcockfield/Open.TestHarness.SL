//! Open.Core.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.CssOverflow

Open.Core.CssOverflow = function() { 
    /// <field name="visible" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="hidden" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="scroll" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="auto" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="inherit" type="Number" integer="true" static="true">
    /// </field>
};
Open.Core.CssOverflow.prototype = {
    visible: 0, 
    hidden: 1, 
    scroll: 2, 
    auto: 3, 
    inherit: 4
}
Open.Core.CssOverflow.registerEnum('Open.Core.CssOverflow', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.CssTextAlign

Open.Core.CssTextAlign = function() { 
    /// <field name="left" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="right" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="center" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="justify" type="Number" integer="true" static="true">
    /// </field>
};
Open.Core.CssTextAlign.prototype = {
    left: 0, 
    right: 1, 
    center: 2, 
    justify: 3
}
Open.Core.CssTextAlign.registerEnum('Open.Core.CssTextAlign', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.LinkTarget

Open.Core.LinkTarget = function() { 
    /// <summary>
    /// The target of an HTML link.
    /// </summary>
    /// <field name="blank" type="Number" integer="true" static="true">
    /// Load in a new window.
    /// </field>
    /// <field name="self" type="Number" integer="true" static="true">
    /// Load in the same frame as it was clicked.
    /// </field>
    /// <field name="parent" type="Number" integer="true" static="true">
    /// Load in the parent frameset.
    /// </field>
    /// <field name="top" type="Number" integer="true" static="true">
    /// Load in the full body of the window.
    /// </field>
};
Open.Core.LinkTarget.prototype = {
    blank: 0, 
    self: 1, 
    parent: 2, 
    top: 2
}
Open.Core.LinkTarget.registerEnum('Open.Core.LinkTarget', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.HtmlListType

Open.Core.HtmlListType = function() { 
    /// <summary>
    /// Flags representing the various types of HTML list.
    /// </summary>
    /// <field name="unordered" type="Number" integer="true" static="true">
    /// An unordered list <ul></ul>.
    /// </field>
    /// <field name="ordered" type="Number" integer="true" static="true">
    /// An ordered list <ol></ol>.
    /// </field>
};
Open.Core.HtmlListType.prototype = {
    unordered: 0, 
    ordered: 1
}
Open.Core.HtmlListType.registerEnum('Open.Core.HtmlListType', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.IButton

Open.Core.IButton = function() { 
    /// <summary>
    /// A clickable button.
    /// </summary>
};
Open.Core.IButton.prototype = {
    add_click : null,
    remove_click : null,
    add_isPressedChanged : null,
    remove_isPressedChanged : null,
    get_isEnabled : null,
    set_isEnabled : null,
    get_canToggle : null,
    set_canToggle : null,
    get_state : null,
    get_isPressed : null,
    get_isMouseOver : null,
    get_isMouseDown : null,
    invokeClick : null
}
Open.Core.IButton.registerInterface('Open.Core.IButton');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.INotifyDisposed

Open.Core.INotifyDisposed = function() { 
    /// <summary>
    /// Provides notification of when an object is disposed.
    /// </summary>
};
Open.Core.INotifyDisposed.prototype = {
    add_disposed : null,
    remove_disposed : null,
    get_isDisposed : null
}
Open.Core.INotifyDisposed.registerInterface('Open.Core.INotifyDisposed');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Key

Open.Core.Key = function() { 
    /// <summary>
    /// Keyboard key codes.
    /// </summary>
    /// <field name="shift" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="ctrl" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="alt" type="Number" integer="true" static="true">
    /// </field>
};
Open.Core.Key.prototype = {
    shift: 16, 
    ctrl: 17, 
    alt: 18
}
Open.Core.Key.registerEnum('Open.Core.Key', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.HorizontalDirection

Open.Core.HorizontalDirection = function() { 
    /// <summary>
    /// A direction on the X plane.
    /// </summary>
    /// <field name="left" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="right" type="Number" integer="true" static="true">
    /// </field>
};
Open.Core.HorizontalDirection.prototype = {
    left: 0, 
    right: 1
}
Open.Core.HorizontalDirection.registerEnum('Open.Core.HorizontalDirection', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.VerticalDirection

Open.Core.VerticalDirection = function() { 
    /// <summary>
    /// A direction on the Y plane.
    /// </summary>
    /// <field name="up" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="down" type="Number" integer="true" static="true">
    /// </field>
};
Open.Core.VerticalDirection.prototype = {
    up: 0, 
    down: 1
}
Open.Core.VerticalDirection.registerEnum('Open.Core.VerticalDirection', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.SizeDimension

Open.Core.SizeDimension = function() { 
    /// <summary>
    /// Flags representing the width or height of an object.
    /// </summary>
    /// <field name="width" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="height" type="Number" integer="true" static="true">
    /// </field>
};
Open.Core.SizeDimension.prototype = {
    width: 0, 
    height: 1
}
Open.Core.SizeDimension.registerEnum('Open.Core.SizeDimension', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.ButtonState

Open.Core.ButtonState = function() { 
    /// <summary>
    /// The various kinds of mouse-related states a button can be in.
    /// </summary>
    /// <field name="normal" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="mouseOver" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="pressed" type="Number" integer="true" static="true">
    /// </field>
};
Open.Core.ButtonState.prototype = {
    normal: 0, 
    mouseOver: 1, 
    pressed: 2
}
Open.Core.ButtonState.registerEnum('Open.Core.ButtonState', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.InsertMode

Open.Core.InsertMode = function() { 
    /// <summary>
    /// Flags indicating the various strategies for inserting content.
    /// </summary>
    /// <field name="replace" type="Number" integer="true" static="true">
    /// The target element is replaced with the inserted content.
    /// </field>
};
Open.Core.InsertMode.prototype = {
    replace: 0
}
Open.Core.InsertMode.registerEnum('Open.Core.InsertMode', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.IHtmlFactory

Open.Core.IHtmlFactory = function() { 
    /// <summary>
    /// An object that can create HTML.
    /// </summary>
};
Open.Core.IHtmlFactory.prototype = {
    createHtml : null
}
Open.Core.IHtmlFactory.registerInterface('Open.Core.IHtmlFactory');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.LogSeverity

Open.Core.LogSeverity = function() { 
    /// <summary>
    /// Flags indicating the level of severity of a message being written to the log.
    /// </summary>
    /// <field name="info" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="debug" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="warning" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="error" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="success" type="Number" integer="true" static="true">
    /// </field>
};
Open.Core.LogSeverity.prototype = {
    info: 0, 
    debug: 1, 
    warning: 2, 
    error: 3, 
    success: 4
}
Open.Core.LogSeverity.registerEnum('Open.Core.LogSeverity', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.LogDivider

Open.Core.LogDivider = function() { 
    /// <summary>
    /// Flags representing the type of visual dividers that can be inserted into the log.
    /// </summary>
    /// <field name="lineBreak" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="section" type="Number" integer="true" static="true">
    /// </field>
};
Open.Core.LogDivider.prototype = {
    lineBreak: 0, 
    section: 1
}
Open.Core.LogDivider.registerEnum('Open.Core.LogDivider', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.ILog

Open.Core.ILog = function() { 
    /// <summary>
    /// An output log.
    /// </summary>
};
Open.Core.ILog.prototype = {
    get_view : null,
    set_view : null,
    title : null,
    info : null,
    debug : null,
    warning : null,
    error : null,
    success : null,
    write : null,
    lineBreak : null,
    newSection : null,
    clear : null
}
Open.Core.ILog.registerInterface('Open.Core.ILog');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.ILogView

Open.Core.ILogView = function() { 
    /// <summary>
    /// A visual representation of a log.
    /// </summary>
};
Open.Core.ILogView.prototype = {
    insert : null,
    divider : null,
    clear : null
}
Open.Core.ILogView.registerInterface('Open.Core.ILogView');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.IModel

Open.Core.IModel = function() { 
    /// <summary>
    /// A logical model.
    /// </summary>
};
Open.Core.IModel.prototype = {
    get_isDisposed : null,
    getPropertyRef : null,
    toJson : null
}
Open.Core.IModel.registerInterface('Open.Core.IModel');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.ITreeNode

Open.Core.ITreeNode = function() { 
    /// <summary>
    /// Represents a node within a tree data-structure.
    /// </summary>
};
Open.Core.ITreeNode.prototype = {
    add_selectionChanged : null,
    remove_selectionChanged : null,
    add_click : null,
    remove_click : null,
    add_childSelectionChanged : null,
    remove_childSelectionChanged : null,
    add_addingChild : null,
    remove_addingChild : null,
    add_addedChild : null,
    remove_addedChild : null,
    add_removingChild : null,
    remove_removingChild : null,
    add_removedChild : null,
    remove_removedChild : null,
    add_childrenChanged : null,
    remove_childrenChanged : null,
    get_parent : null,
    get_root : null,
    get_isRoot : null,
    get_isSelected : null,
    set_isSelected : null,
    get_children : null,
    get_childCount : null,
    addChild : null,
    insertChild : null,
    removeChild : null,
    clearChildren : null,
    contains : null,
    containsDescendent : null,
    childAt : null
}
Open.Core.ITreeNode.registerInterface('Open.Core.ITreeNode');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.INotifyPropertyChanged

Open.Core.INotifyPropertyChanged = function() { 
    /// <summary>
    /// Notifies clients that a property value has changed.
    /// </summary>
};
Open.Core.INotifyPropertyChanged.prototype = {
    add_propertyChanged : null,
    remove_propertyChanged : null
}
Open.Core.INotifyPropertyChanged.registerInterface('Open.Core.INotifyPropertyChanged');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.IViewFactory

Open.Core.IViewFactory = function() { 
    /// <summary>
    /// An object that is can create the view for itself.
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
    /// The logical controller for a view (visual UI control) contained with an HTML element.
    /// </summary>
};
Open.Core.IView.prototype = {
    add_isEnabledChanged : null,
    remove_isEnabledChanged : null,
    add_isVisibleChanged : null,
    remove_isVisibleChanged : null,
    add_sizeChanged : null,
    remove_sizeChanged : null,
    get_container : null,
    get_outerHtml : null,
    get_innerHtml : null,
    dispose : null,
    get_isEnabled : null,
    set_isEnabled : null,
    get_isVisible : null,
    set_isVisible : null,
    get_background : null,
    set_background : null,
    get_opacity : null,
    set_opacity : null,
    get_width : null,
    set_width : null,
    get_height : null,
    set_height : null,
    setSize : null,
    getCss : null,
    setCss : null,
    getAttribute : null,
    setAttribute : null,
    insert : null
}
Open.Core.IView.registerInterface('Open.Core.IView');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.ControllerBase

Open.Core.ControllerBase = function Open_Core_ControllerBase() {
    /// <summary>
    /// Base class for UI controllers.
    /// </summary>
    Open.Core.ControllerBase.initializeBase(this);
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.ModelBase

Open.Core.ModelBase = function Open_Core_ModelBase() {
    /// <summary>
    /// Base class for data models.
    /// </summary>
    /// <field name="__propertyChanged" type="Open.Core.PropertyChangedHandler">
    /// </field>
    /// <field name="__disposed" type="EventHandler">
    /// </field>
    /// <field name="_isDisposed" type="Boolean">
    /// </field>
    /// <field name="_propertyBag" type="Object">
    /// </field>
    /// <field name="_propertRefs" type="Array">
    /// </field>
}
Open.Core.ModelBase.prototype = {
    
    add_propertyChanged: function Open_Core_ModelBase$add_propertyChanged(value) {
        /// <param name="value" type="Function" />
        this.__propertyChanged = ss.Delegate.combine(this.__propertyChanged, value);
    },
    remove_propertyChanged: function Open_Core_ModelBase$remove_propertyChanged(value) {
        /// <param name="value" type="Function" />
        this.__propertyChanged = ss.Delegate.remove(this.__propertyChanged, value);
    },
    
    __propertyChanged: null,
    
    add_disposed: function Open_Core_ModelBase$add_disposed(value) {
        /// <param name="value" type="Function" />
        this.__disposed = ss.Delegate.combine(this.__disposed, value);
    },
    remove_disposed: function Open_Core_ModelBase$remove_disposed(value) {
        /// <param name="value" type="Function" />
        this.__disposed = ss.Delegate.remove(this.__disposed, value);
    },
    
    __disposed: null,
    
    _fireDisposed: function Open_Core_ModelBase$_fireDisposed() {
        if (this.__disposed != null) {
            this.__disposed.invoke(this, new ss.EventArgs());
        }
    },
    
    _isDisposed: false,
    _propertyBag: null,
    _propertRefs: null,
    
    get_isDisposed: function Open_Core_ModelBase$get_isDisposed() {
        /// <value type="Boolean"></value>
        return this._isDisposed;
    },
    
    get__propertyBag: function Open_Core_ModelBase$get__propertyBag() {
        /// <value type="Object"></value>
        return this._propertyBag || (this._propertyBag = {});
    },
    
    get__propertyRefs: function Open_Core_ModelBase$get__propertyRefs() {
        /// <value type="Array"></value>
        return this._propertRefs || (this._propertRefs = []);
    },
    
    dispose: function Open_Core_ModelBase$dispose() {
        /// <summary>
        /// Disposes of the object.
        /// </summary>
        if (this._isDisposed) {
            return;
        }
        Open.Core.Helper.get_collection().disposeAndClear(this.get__propertyRefs());
        this._fireDisposed();
        this.onDisposed();
        this._isDisposed = true;
    },
    
    toJson: function Open_Core_ModelBase$toJson() {
        /// <summary>
        /// Serializes the model to JSON.
        /// </summary>
        /// <returns type="String"></returns>
        return Open.Core.Helper.get_json().serialize(this);
    },
    
    onDisposed: function Open_Core_ModelBase$onDisposed() {
        /// <summary>
        /// Invoked when the model is disposed.
        /// </summary>
    },
    
    firePropertyChanged: function Open_Core_ModelBase$firePropertyChanged(propertyName) {
        /// <summary>
        /// Fires the 'PropertyChanged' event.
        /// </summary>
        /// <param name="propertyName" type="String">
        /// The name of the property that has changed.
        /// </param>
        if (this.__propertyChanged != null) {
            this.__propertyChanged.invoke(this, new Open.Core.PropertyChangedEventArgs(this.getPropertyRef(propertyName)));
        }
    },
    
    get: function Open_Core_ModelBase$get(propertyName, defaultValue) {
        /// <summary>
        /// Retrieves a property value from the backing store.
        /// </summary>
        /// <param name="propertyName" type="String">
        /// The name of the property.
        /// </param>
        /// <param name="defaultValue" type="Object">
        /// The default value to provide (if the value does not exist).
        /// </param>
        /// <returns type="Object"></returns>
        return (Object.keyExists(this.get__propertyBag(), propertyName)) ? this.get__propertyBag()[propertyName] : defaultValue;
    },
    
    set: function Open_Core_ModelBase$set(propertyName, value, defaultValue) {
        /// <summary>
        /// Stores the given value for the named property
        /// (firing the 'PropertyChanged' event if the value differs from the current value).
        /// </summary>
        /// <param name="propertyName" type="String">
        /// The name of the property.
        /// </param>
        /// <param name="value" type="Object">
        /// The value to set.
        /// </param>
        /// <param name="defaultValue" type="Object">
        /// The default value of the property.
        /// </param>
        /// <returns type="Boolean"></returns>
        var currentValue = this.get(propertyName, defaultValue);
        if (value === currentValue) {
            return false;
        }
        this.get__propertyBag()[propertyName] = value;
        this.firePropertyChanged(propertyName);
        return true;
    },
    
    getPropertyRef: function Open_Core_ModelBase$getPropertyRef(propertyName) {
        /// <summary>
        /// Retrieves a singleton instance to the handle to the named property.
        /// </summary>
        /// <param name="propertyName" type="String">
        /// The name of the property to retrieve.
        /// </param>
        /// <returns type="Open.Core.PropertyRef"></returns>
        var propertyRef = this._getPropertyRefFromList(propertyName);
        if (propertyRef != null) {
            return propertyRef;
        }
        if (!Open.Core.Helper.get_reflection().hasProperty(this, propertyName)) {
            return null;
        }
        propertyRef = new Open.Core.PropertyRef(this, propertyName);
        this.get__propertyRefs().add(propertyRef);
        return propertyRef;
    },
    
    _getPropertyRefFromList: function Open_Core_ModelBase$_getPropertyRefFromList(propertyName) {
        /// <param name="propertyName" type="String">
        /// </param>
        /// <returns type="Open.Core.PropertyRef"></returns>
        if (this._propertRefs == null) {
            return null;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(this.get__propertyRefs());
        while ($enum1.moveNext()) {
            var property = $enum1.get_current();
            if (property.get_name() === propertyName) {
                return property;
            }
        }
        return null;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.DiContainer

Open.Core.DiContainer = function Open_Core_DiContainer() {
    /// <summary>
    /// A simple DI container.
    /// </summary>
    /// <field name="_singletons" type="Array">
    /// </field>
    /// <field name="_defaultContainer" type="Open.Core.DiContainer" static="true">
    /// </field>
    this._singletons = [];
}
Open.Core.DiContainer.get_defaultContainer = function Open_Core_DiContainer$get_defaultContainer() {
    /// <summary>
    /// Gets the default DI container.
    /// </summary>
    /// <value type="Open.Core.DiContainer"></value>
    return Open.Core.DiContainer._defaultContainer || (Open.Core.DiContainer._defaultContainer = new Open.Core.DiContainer());
}
Open.Core.DiContainer.prototype = {
    
    dispose: function Open_Core_DiContainer$dispose() {
        /// <summary>
        /// Destructor.
        /// </summary>
        Open.Core.Helper.get_collection().disposeAndClear(this._singletons);
    },
    
    getSingleton: function Open_Core_DiContainer$getSingleton(key) {
        /// <summary>
        /// Retrieves the singleton that matches the given type.
        /// </summary>
        /// <param name="key" type="Type">
        /// The type-key (either the Type of the singleton, or an interface).
        /// </param>
        /// <returns type="Object"></returns>
        var wrapper = this._fromKey(key);
        return (wrapper == null) ? null : wrapper.get_instance();
    },
    
    getOrCreateSingleton: function Open_Core_DiContainer$getOrCreateSingleton(key, create) {
        /// <summary>
        /// Retrieves the singleton that matches the given type, and if not found creates and registers an instance using the given factory.
        /// </summary>
        /// <param name="key" type="Type">
        /// The type-key (either the Type of the singleton, or an interface).
        /// </param>
        /// <param name="create" type="Func">
        /// Factory used to create the new instance if the singleton has not yet been registered.
        /// </param>
        /// <returns type="Object"></returns>
        var instance = this.getSingleton(key);
        if (instance != null) {
            return instance;
        }
        instance = create.invoke();
        this.registerSingleton(key, instance);
        return instance;
    },
    
    registerSingleton: function Open_Core_DiContainer$registerSingleton(key, instance) {
        /// <summary>
        /// Registers the given object as a singleton within the container (replacing any existing instance).
        /// </summary>
        /// <param name="key" type="Type">
        /// The type-key (either the Type of the singleton, or an interface).
        /// </param>
        /// <param name="instance" type="Object">
        /// The instance.
        /// </param>
        if (key == null) {
            throw new Error('Singleton key cannot be null');
        }
        if (instance == null) {
            throw new Error('Singleton instance cannot be null');
        }
        this.unregisterSingleton(key);
        var wrapper = new Open.Core._diInstanceWrapper(key, instance);
        this._singletons.add(wrapper);
    },
    
    unregisterSingleton: function Open_Core_DiContainer$unregisterSingleton(key) {
        /// <summary>
        /// Removes the specified singleton from the container.
        /// </summary>
        /// <param name="key" type="Type">
        /// The type-key (either the Type of the singleton, or an interface).
        /// </param>
        /// <returns type="Boolean"></returns>
        var wrapper = this._fromKey(key);
        if (wrapper == null) {
            return false;
        }
        wrapper.dispose();
        this._singletons.remove(wrapper);
        return true;
    },
    
    containsSingleton: function Open_Core_DiContainer$containsSingleton(key) {
        /// <summary>
        /// Determines whether the a singleton with the given key exists within the container.
        /// </summary>
        /// <param name="key" type="Type">
        /// The type-key (either the Type of the singleton, or an interface).
        /// </param>
        /// <returns type="Boolean"></returns>
        return this._fromKey(key) != null;
    },
    
    _fromKey: function Open_Core_DiContainer$_fromKey(key) {
        /// <param name="key" type="Type">
        /// </param>
        /// <returns type="Open.Core._diInstanceWrapper"></returns>
        if (key == null) {
            return null;
        }
        return Type.safeCast(Open.Core.Helper.get_collection().first(this._singletons, ss.Delegate.create(this, function(o) {
            return (o).get_key() === key;
        })), Open.Core._diInstanceWrapper);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core._diInstanceWrapper

Open.Core._diInstanceWrapper = function Open_Core__diInstanceWrapper(key, instance) {
    /// <param name="key" type="Type">
    /// </param>
    /// <param name="instance" type="Object">
    /// </param>
    /// <field name="_key" type="Type">
    /// </field>
    /// <field name="_instance" type="Object">
    /// </field>
    this._key = key;
    this._instance = instance;
}
Open.Core._diInstanceWrapper.prototype = {
    _key: null,
    _instance: null,
    
    dispose: function Open_Core__diInstanceWrapper$dispose() {
        this._key = null;
        this._instance = null;
    },
    
    get_key: function Open_Core__diInstanceWrapper$get_key() {
        /// <value type="Type"></value>
        return this._key;
    },
    
    get_instance: function Open_Core__diInstanceWrapper$get_instance() {
        /// <value type="Object"></value>
        return this._instance;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Template

Open.Core.Template = function Open_Core_Template(selector) {
    /// <summary>
    /// A compiled jQuery template.
    /// </summary>
    /// <param name="selector" type="String">
    /// The CSS selector for the script block containing the template HTML.
    /// </param>
    /// <field name="_id" type="String">
    /// </field>
    /// <field name="_selector" type="String">
    /// </field>
    this._id = Open.Core.Helper.createId();
    this._selector = selector;
    $.template(this._id, this.get_templateHtml());
}
Open.Core.Template.prototype = {
    _id: null,
    _selector: null,
    
    get_selector: function Open_Core_Template$get_selector() {
        /// <summary>
        /// Gets the CSS selector for the script block containing the template HTML.
        /// </summary>
        /// <value type="String"></value>
        return this._selector;
    },
    
    get_templateHtml: function Open_Core_Template$get_templateHtml() {
        /// <summary>
        /// Gets the source template HTML.
        /// </summary>
        /// <value type="String"></value>
        var template = $(this._selector);
        return (template.length === 0) ? null : template.html();
    },
    
    toString: function Open_Core_Template$toString() {
        /// <returns type="String"></returns>
        return Open.Core.Helper.get_string().formatToString(this.get_templateHtml());
    },
    
    appendTo: function Open_Core_Template$appendTo(target, data) {
        /// <summary>
        /// Processes the template with the specified data and appends the result to the given target.
        /// </summary>
        /// <param name="target" type="jQueryObject">
        /// The target to append HTML to.
        /// </param>
        /// <param name="data" type="Object">
        /// The source data for the template to read from.
        /// </param>
        $.tmpl( this._id, data ).appendTo( target );
    },
    
    toHtml: function Open_Core_Template$toHtml(data) {
        /// <summary>
        /// Renders the template to HTML using the specified data.
        /// </summary>
        /// <param name="data" type="Object">
        /// The source data for the template to read from.
        /// </param>
        /// <returns type="String"></returns>
        var div = Open.Core.Html.createDiv();
        this.appendTo(div, data);
        return div.html();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Should

Open.Core.Should = function Open_Core_Should() {
    /// <summary>
    /// Testing assertions.
    /// </summary>
}
Open.Core.Should.equal = function Open_Core_Should$equal(subject, value) {
    /// <summary>
    /// Asserts that an object is equal to another object (uses != comparison).
    /// </summary>
    /// <param name="subject" type="Object">
    /// The value being compared.
    /// </param>
    /// <param name="value" type="Object">
    /// The value to compare to.
    /// </param>
    var isSame = subject == value;
    if (!isSame) {
        Open.Core.Should._throwError(String.format('The two values \'{0}\' and \'{1}\' are not equal.', Open.Core.Should._format(subject), Open.Core.Should._format(value)));
    }
}
Open.Core.Should.notEqual = function Open_Core_Should$notEqual(subject, value) {
    /// <summary>
    /// Asserts that an object is not equal to another object (uses != comparison).
    /// </summary>
    /// <param name="subject" type="Object">
    /// The value being compared.
    /// </param>
    /// <param name="value" type="Object">
    /// The value to compare to.
    /// </param>
    if (subject === value) {
        Open.Core.Should._throwError(String.format('The two values \'{0}\' and \'{1}\' should not be equal.', Open.Core.Should._format(subject), Open.Core.Should._format(value)));
    }
}
Open.Core.Should.notBeNull = function Open_Core_Should$notBeNull(subject) {
    /// <summary>
    /// Asserts that an object is not null.
    /// </summary>
    /// <param name="subject" type="Object">
    /// The value being examined.
    /// </param>
    if (subject == null) {
        Open.Core.Should._throwError('Value should not be null.');
    }
}
Open.Core.Should.beNull = function Open_Core_Should$beNull(subject) {
    /// <summary>
    /// Asserts that an object is not null.
    /// </summary>
    /// <param name="subject" type="Object">
    /// The value being examined.
    /// </param>
    if (subject != null) {
        Open.Core.Should._throwError(String.format('The value \'{0}\' should actually be null.', Open.Core.Should._format(subject)));
    }
}
Open.Core.Should.beTrue = function Open_Core_Should$beTrue(value) {
    /// <summary>
    /// Asserts that an value is True.
    /// </summary>
    /// <param name="value" type="Boolean">
    /// The value being examined.
    /// </param>
    if (!value) {
        Open.Core.Should._throwError('Value should be True.');
    }
}
Open.Core.Should.beFalse = function Open_Core_Should$beFalse(value) {
    /// <summary>
    /// Asserts that an value is False.
    /// </summary>
    /// <param name="value" type="Boolean">
    /// The value being examined.
    /// </param>
    if (value) {
        Open.Core.Should._throwError('Value should be False.');
    }
}
Open.Core.Should._throwError = function Open_Core_Should$_throwError(message) {
    /// <param name="message" type="String">
    /// </param>
    throw new Error(String.format('AssertionException: ' + message));
}
Open.Core.Should._format = function Open_Core_Should$_format(value) {
    /// <param name="value" type="Object">
    /// </param>
    /// <returns type="String"></returns>
    return Open.Core.Helper.get_string().formatToString(value);
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Keyboard

Open.Core.Keyboard = function Open_Core_Keyboard() {
    /// <summary>
    /// Global monitoring of keyboard state.
    /// </summary>
    /// <field name="_isShiftPressed" type="Boolean" static="true">
    /// </field>
    /// <field name="_isCtrlPressed" type="Boolean" static="true">
    /// </field>
    /// <field name="_isAltPressed" type="Boolean" static="true">
    /// </field>
}
Open.Core.Keyboard.get_isShiftPressed = function Open_Core_Keyboard$get_isShiftPressed() {
    /// <summary>
    /// Gets whether the SHIFT key is currently pressed.
    /// </summary>
    /// <value type="Boolean"></value>
    return Open.Core.Keyboard._isShiftPressed;
}
Open.Core.Keyboard.get_isCtrlPressed = function Open_Core_Keyboard$get_isCtrlPressed() {
    /// <summary>
    /// Gets whether the CTRL key is currently pressed.
    /// </summary>
    /// <value type="Boolean"></value>
    return Open.Core.Keyboard._isCtrlPressed;
}
Open.Core.Keyboard.get_isAltPressed = function Open_Core_Keyboard$get_isAltPressed() {
    /// <summary>
    /// Gets whether the ALT key is currently pressed.
    /// </summary>
    /// <value type="Boolean"></value>
    return Open.Core.Keyboard._isAltPressed;
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.TreeNode

Open.Core.TreeNode = function Open_Core_TreeNode() {
    /// <summary>
    /// Represents a node within a tree data-structure.
    /// </summary>
    /// <field name="__selectionChanged$1" type="EventHandler">
    /// </field>
    /// <field name="__click$1" type="EventHandler">
    /// </field>
    /// <field name="__childSelectionChanged$1" type="EventHandler">
    /// </field>
    /// <field name="__addingChild$1" type="Open.Core.TreeNodeHandler">
    /// </field>
    /// <field name="__addedChild$1" type="Open.Core.TreeNodeHandler">
    /// </field>
    /// <field name="__removedChild$1" type="Open.Core.TreeNodeHandler">
    /// </field>
    /// <field name="__removingChild$1" type="Open.Core.TreeNodeHandler">
    /// </field>
    /// <field name="__childrenChanged$1" type="EventHandler">
    /// </field>
    /// <field name="nullIndex" type="Number" integer="true" static="true">
    /// The index number of a node if it's not-known or is not applicable to the scenario.
    /// </field>
    /// <field name="propIsSelected" type="String" static="true">
    /// </field>
    /// <field name="propChildren" type="String" static="true">
    /// </field>
    /// <field name="_parent$1" type="Open.Core.ITreeNode">
    /// </field>
    /// <field name="_childList$1" type="Array">
    /// </field>
    Open.Core.TreeNode.initializeBase(this);
}
Open.Core.TreeNode.fromJson = function Open_Core_TreeNode$fromJson(json, factory) {
    /// <summary>
    /// Creates a new instance of the node from JSON.
    /// </summary>
    /// <param name="json" type="String">
    /// The JSON string to parse.
    /// </param>
    /// <param name="factory" type="Open.Core.TreeNodeFactory">
    /// The factory method for creating new nodes.
    /// </param>
    /// <returns type="Open.Core.TreeNode"></returns>
    return Open.Core.TreeNode._fromDictionary$1(Open.Core.Helper.get_json().parse(json), factory);
}
Open.Core.TreeNode._fromDictionary$1 = function Open_Core_TreeNode$_fromDictionary$1(dic, factory) {
    /// <param name="dic" type="Object">
    /// </param>
    /// <param name="factory" type="Open.Core.TreeNodeFactory">
    /// </param>
    /// <returns type="Open.Core.TreeNode"></returns>
    var node = factory.invoke(dic);
    var children = Type.safeCast(dic[Open.Core.TreeNode.propChildren], Array);
    if (children != null) {
        var $enum1 = ss.IEnumerator.getEnumerator(children);
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            var childNode = Open.Core.TreeNode._fromDictionary$1(child, factory);
            node.addChild(childNode);
        }
    }
    return node;
}
Open.Core.TreeNode._setParent$1 = function Open_Core_TreeNode$_setParent$1(node, value) {
    /// <param name="node" type="Open.Core.ITreeNode">
    /// </param>
    /// <param name="value" type="Open.Core.ITreeNode">
    /// </param>
    var concrete = Type.safeCast(node, Open.Core.TreeNode);
    if (concrete == null) {
        return;
    }
    concrete._parent$1 = value;
}
Open.Core.TreeNode._isDescendent$1 = function Open_Core_TreeNode$_isDescendent$1(parent, node) {
    /// <param name="parent" type="Open.Core.ITreeNode">
    /// </param>
    /// <param name="node" type="Open.Core.ITreeNode">
    /// </param>
    /// <returns type="Boolean"></returns>
    if (ss.isNullOrUndefined(node)) {
        return false;
    }
    if (parent.contains(node)) {
        return true;
    }
    var $enum1 = ss.IEnumerator.getEnumerator(parent.get_children());
    while ($enum1.moveNext()) {
        var child = $enum1.get_current();
        if (Open.Core.TreeNode._isDescendent$1(child, node)) {
            return true;
        }
    }
    return false;
}
Open.Core.TreeNode.prototype = {
    
    add_selectionChanged: function Open_Core_TreeNode$add_selectionChanged(value) {
        /// <param name="value" type="Function" />
        this.__selectionChanged$1 = ss.Delegate.combine(this.__selectionChanged$1, value);
    },
    remove_selectionChanged: function Open_Core_TreeNode$remove_selectionChanged(value) {
        /// <param name="value" type="Function" />
        this.__selectionChanged$1 = ss.Delegate.remove(this.__selectionChanged$1, value);
    },
    
    __selectionChanged$1: null,
    
    _fireSelectionChanged$1: function Open_Core_TreeNode$_fireSelectionChanged$1() {
        this.onIsSelectedChanged();
        if (this.__selectionChanged$1 != null) {
            this.__selectionChanged$1.invoke(this, new ss.EventArgs());
        }
    },
    
    add_click: function Open_Core_TreeNode$add_click(value) {
        /// <param name="value" type="Function" />
        this.__click$1 = ss.Delegate.combine(this.__click$1, value);
    },
    remove_click: function Open_Core_TreeNode$remove_click(value) {
        /// <param name="value" type="Function" />
        this.__click$1 = ss.Delegate.remove(this.__click$1, value);
    },
    
    __click$1: null,
    
    _fireClick: function Open_Core_TreeNode$_fireClick() {
        if (this.__click$1 != null) {
            this.__click$1.invoke(this, new ss.EventArgs());
        }
    },
    
    add_childSelectionChanged: function Open_Core_TreeNode$add_childSelectionChanged(value) {
        /// <param name="value" type="Function" />
        this.__childSelectionChanged$1 = ss.Delegate.combine(this.__childSelectionChanged$1, value);
    },
    remove_childSelectionChanged: function Open_Core_TreeNode$remove_childSelectionChanged(value) {
        /// <param name="value" type="Function" />
        this.__childSelectionChanged$1 = ss.Delegate.remove(this.__childSelectionChanged$1, value);
    },
    
    __childSelectionChanged$1: null,
    
    _fireChildSelectionChanged$1: function Open_Core_TreeNode$_fireChildSelectionChanged$1() {
        if (this.__childSelectionChanged$1 != null) {
            this.__childSelectionChanged$1.invoke(this, new ss.EventArgs());
        }
    },
    
    add_addingChild: function Open_Core_TreeNode$add_addingChild(value) {
        /// <param name="value" type="Function" />
        this.__addingChild$1 = ss.Delegate.combine(this.__addingChild$1, value);
    },
    remove_addingChild: function Open_Core_TreeNode$remove_addingChild(value) {
        /// <param name="value" type="Function" />
        this.__addingChild$1 = ss.Delegate.remove(this.__addingChild$1, value);
    },
    
    __addingChild$1: null,
    
    _fireAddingChild$1: function Open_Core_TreeNode$_fireAddingChild$1(e) {
        /// <param name="e" type="Open.Core.TreeNodeEventArgs">
        /// </param>
        if (this.__addingChild$1 != null) {
            this.__addingChild$1.invoke(this, e);
        }
    },
    
    add_addedChild: function Open_Core_TreeNode$add_addedChild(value) {
        /// <param name="value" type="Function" />
        this.__addedChild$1 = ss.Delegate.combine(this.__addedChild$1, value);
    },
    remove_addedChild: function Open_Core_TreeNode$remove_addedChild(value) {
        /// <param name="value" type="Function" />
        this.__addedChild$1 = ss.Delegate.remove(this.__addedChild$1, value);
    },
    
    __addedChild$1: null,
    
    _fireChildAdded$1: function Open_Core_TreeNode$_fireChildAdded$1(e) {
        /// <param name="e" type="Open.Core.TreeNodeEventArgs">
        /// </param>
        if (this.__addedChild$1 != null) {
            this.__addedChild$1.invoke(this, e);
        }
        this._fireChildrenChanged$1();
    },
    
    add_removedChild: function Open_Core_TreeNode$add_removedChild(value) {
        /// <param name="value" type="Function" />
        this.__removedChild$1 = ss.Delegate.combine(this.__removedChild$1, value);
    },
    remove_removedChild: function Open_Core_TreeNode$remove_removedChild(value) {
        /// <param name="value" type="Function" />
        this.__removedChild$1 = ss.Delegate.remove(this.__removedChild$1, value);
    },
    
    __removedChild$1: null,
    
    _fireChildRemoved$1: function Open_Core_TreeNode$_fireChildRemoved$1(e) {
        /// <param name="e" type="Open.Core.TreeNodeEventArgs">
        /// </param>
        if (this.__removedChild$1 != null) {
            this.__removedChild$1.invoke(this, e);
        }
        this._fireChildrenChanged$1();
    },
    
    add_removingChild: function Open_Core_TreeNode$add_removingChild(value) {
        /// <param name="value" type="Function" />
        this.__removingChild$1 = ss.Delegate.combine(this.__removingChild$1, value);
    },
    remove_removingChild: function Open_Core_TreeNode$remove_removingChild(value) {
        /// <param name="value" type="Function" />
        this.__removingChild$1 = ss.Delegate.remove(this.__removingChild$1, value);
    },
    
    __removingChild$1: null,
    
    _fireRemovingChild$1: function Open_Core_TreeNode$_fireRemovingChild$1(e) {
        /// <param name="e" type="Open.Core.TreeNodeEventArgs">
        /// </param>
        if (this.__removingChild$1 != null) {
            this.__removingChild$1.invoke(this, e);
        }
    },
    
    add_childrenChanged: function Open_Core_TreeNode$add_childrenChanged(value) {
        /// <param name="value" type="Function" />
        this.__childrenChanged$1 = ss.Delegate.combine(this.__childrenChanged$1, value);
    },
    remove_childrenChanged: function Open_Core_TreeNode$remove_childrenChanged(value) {
        /// <param name="value" type="Function" />
        this.__childrenChanged$1 = ss.Delegate.remove(this.__childrenChanged$1, value);
    },
    
    __childrenChanged$1: null,
    
    _fireChildrenChanged$1: function Open_Core_TreeNode$_fireChildrenChanged$1() {
        if (this.__childrenChanged$1 != null) {
            this.__childrenChanged$1.invoke(this, new ss.EventArgs());
        }
    },
    
    _parent$1: null,
    _childList$1: null,
    
    onDisposed: function Open_Core_TreeNode$onDisposed() {
        if (this._childList$1 != null) {
            var $enum1 = ss.IEnumerator.getEnumerator(this.get_children());
            while ($enum1.moveNext()) {
                var child = $enum1.get_current();
                child.dispose();
            }
        }
        Open.Core.TreeNode.callBaseMethod(this, 'onDisposed');
    },
    
    _onChildSelectionChanged$1: function Open_Core_TreeNode$_onChildSelectionChanged$1(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._fireChildSelectionChanged$1();
    },
    
    get_parent: function Open_Core_TreeNode$get_parent() {
        /// <value type="Open.Core.ITreeNode"></value>
        return this._parent$1;
    },
    
    get_root: function Open_Core_TreeNode$get_root() {
        /// <value type="Open.Core.ITreeNode"></value>
        return this._getRoot$1();
    },
    
    get_isRoot: function Open_Core_TreeNode$get_isRoot() {
        /// <value type="Boolean"></value>
        return this.get_parent() == null;
    },
    
    get_children: function Open_Core_TreeNode$get_children() {
        /// <value type="ss.IEnumerable"></value>
        return this.get__childList$1();
    },
    
    get_childCount: function Open_Core_TreeNode$get_childCount() {
        /// <value type="Number" integer="true"></value>
        return (this._childList$1 == null) ? 0 : this._childList$1.length;
    },
    
    get_isSelected: function Open_Core_TreeNode$get_isSelected() {
        /// <value type="Boolean"></value>
        return this.get(Open.Core.TreeNode.propIsSelected, false);
    },
    set_isSelected: function Open_Core_TreeNode$set_isSelected(value) {
        /// <value type="Boolean"></value>
        if (this.set(Open.Core.TreeNode.propIsSelected, value, false)) {
            this._fireSelectionChanged$1();
        }
        return value;
    },
    
    get__childList$1: function Open_Core_TreeNode$get__childList$1() {
        /// <value type="Array"></value>
        return this._childList$1 || (this._childList$1 = []);
    },
    
    toString: function Open_Core_TreeNode$toString() {
        /// <returns type="String"></returns>
        return String.format('[{0}({1})]', Type.getInstanceType(this).get_name(), this.get_childCount());
    },
    
    onIsSelectedChanged: function Open_Core_TreeNode$onIsSelectedChanged() {
        /// <summary>
        /// Invoked after the 'IsSelected' property changes.
        /// </summary>
    },
    
    toJson: function Open_Core_TreeNode$toJson() {
        /// <returns type="String"></returns>
        return Open.Core.Helper.get_json().serialize(this._toDictionary$1());
    },
    
    serializingJson: function Open_Core_TreeNode$serializingJson(node) {
        /// <summary>
        /// Allows deriving classes to suppliment the dictionary used for JSON serialization.
        /// </summary>
        /// <param name="node" type="Object">
        /// The dictionary representing the node to process.
        /// </param>
    },
    
    addChild: function Open_Core_TreeNode$addChild(node) {
        /// <param name="node" type="Open.Core.ITreeNode">
        /// </param>
        this.insertChild(Open.Core.TreeNode.nullIndex, node);
    },
    
    insertChild: function Open_Core_TreeNode$insertChild(index, node) {
        /// <param name="index" type="Number" integer="true">
        /// </param>
        /// <param name="node" type="Open.Core.ITreeNode">
        /// </param>
        if (node == null) {
            return;
        }
        if (this.contains(node)) {
            return;
        }
        if (index < 0) {
            index = this.get_childCount();
        }
        var args = new Open.Core.TreeNodeEventArgs(node, index);
        this._fireAddingChild$1(args);
        this.get__childList$1().insert(index, node);
        node.add_selectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$1));
        if (node.get_parent() !== this) {
            Open.Core.TreeNode._setParent$1(node, this);
        }
        this._fireChildAdded$1(args);
    },
    
    removeChild: function Open_Core_TreeNode$removeChild(node) {
        /// <param name="node" type="Open.Core.ITreeNode">
        /// </param>
        if (!this.contains(node)) {
            return;
        }
        var args = new Open.Core.TreeNodeEventArgs(node, Open.Core.TreeNode.nullIndex);
        this._fireRemovingChild$1(args);
        this.get__childList$1().remove(node);
        node.remove_selectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$1));
        if (node.get_parent() === this) {
            Open.Core.TreeNode._setParent$1(node, null);
        }
        this._fireChildRemoved$1(args);
    },
    
    clearChildren: function Open_Core_TreeNode$clearChildren() {
        var $enum1 = ss.IEnumerator.getEnumerator(this.get__childList$1().clone());
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            this.removeChild(child);
        }
    },
    
    childAt: function Open_Core_TreeNode$childAt(index) {
        /// <param name="index" type="Number" integer="true">
        /// </param>
        /// <returns type="Open.Core.ITreeNode"></returns>
        return (this._childList$1 == null) ? null : Type.safeCast(this.get__childList$1()[index], Open.Core.ITreeNode);
    },
    
    contains: function Open_Core_TreeNode$contains(node) {
        /// <param name="node" type="Open.Core.ITreeNode">
        /// </param>
        /// <returns type="Boolean"></returns>
        return this.get__childList$1().contains(node);
    },
    
    containsDescendent: function Open_Core_TreeNode$containsDescendent(node) {
        /// <param name="node" type="Open.Core.ITreeNode">
        /// </param>
        /// <returns type="Boolean"></returns>
        return Open.Core.TreeNode._isDescendent$1(this, node);
    },
    
    _toDictionary$1: function Open_Core_TreeNode$_toDictionary$1() {
        /// <returns type="Object"></returns>
        var json = {};
        this.serializingJson(json);
        var children = [];
        var $enum1 = ss.IEnumerator.getEnumerator(this.get_children());
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            children.add(child._toDictionary$1());
        }
        json[Open.Core.TreeNode.propChildren] = children;
        return json;
    },
    
    _getRoot$1: function Open_Core_TreeNode$_getRoot$1() {
        /// <returns type="Open.Core.ITreeNode"></returns>
        if (this.get_isRoot()) {
            return null;
        }
        var parentNode = this.get_parent();
        do {
            if (parentNode == null) {
                break;
            }
            if (parentNode.get_isRoot()) {
                return parentNode;
            }
            parentNode = parentNode.get_parent();
        } while (parentNode != null);
        return null;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.ViewBase

Open.Core.ViewBase = function Open_Core_ViewBase(container) {
    /// <summary>
    /// Base for classes that represent, manage and construct views ("UI").
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// The root HTML element of the control (if null a <DIV></DIV> is generated).
    /// </param>
    /// <field name="__isEnabledChanged$1" type="EventHandler">
    /// </field>
    /// <field name="__isVisibleChanged$1" type="EventHandler">
    /// </field>
    /// <field name="__sizeChanged$1" type="EventHandler">
    /// </field>
    /// <field name="propBackground" type="String" static="true">
    /// </field>
    /// <field name="propIsVisible" type="String" static="true">
    /// </field>
    /// <field name="propOpacity" type="String" static="true">
    /// </field>
    /// <field name="propWidth" type="String" static="true">
    /// </field>
    /// <field name="propHeight" type="String" static="true">
    /// </field>
    /// <field name="propIsEnabled" type="String" static="true">
    /// </field>
    /// <field name="_container$1" type="jQueryObject">
    /// </field>
    Open.Core.ViewBase.initializeBase(this);
    if (ss.isNullOrUndefined(container)) {
        container = Open.Core.Html.createDiv();
    }
    this._container$1 = container;
}
Open.Core.ViewBase.prototype = {
    
    add_isEnabledChanged: function Open_Core_ViewBase$add_isEnabledChanged(value) {
        /// <param name="value" type="Function" />
        this.__isEnabledChanged$1 = ss.Delegate.combine(this.__isEnabledChanged$1, value);
    },
    remove_isEnabledChanged: function Open_Core_ViewBase$remove_isEnabledChanged(value) {
        /// <param name="value" type="Function" />
        this.__isEnabledChanged$1 = ss.Delegate.remove(this.__isEnabledChanged$1, value);
    },
    
    __isEnabledChanged$1: null,
    
    fireIsEnabledChanged: function Open_Core_ViewBase$fireIsEnabledChanged() {
        this.onIsEnabledChanged();
        if (this.__isEnabledChanged$1 != null) {
            this.__isEnabledChanged$1.invoke(this, new ss.EventArgs());
        }
    },
    
    add_isVisibleChanged: function Open_Core_ViewBase$add_isVisibleChanged(value) {
        /// <param name="value" type="Function" />
        this.__isVisibleChanged$1 = ss.Delegate.combine(this.__isVisibleChanged$1, value);
    },
    remove_isVisibleChanged: function Open_Core_ViewBase$remove_isVisibleChanged(value) {
        /// <param name="value" type="Function" />
        this.__isVisibleChanged$1 = ss.Delegate.remove(this.__isVisibleChanged$1, value);
    },
    
    __isVisibleChanged$1: null,
    
    fireIsVisibleChanged: function Open_Core_ViewBase$fireIsVisibleChanged() {
        this.onIsVisibleChanged();
        if (this.__isVisibleChanged$1 != null) {
            this.__isVisibleChanged$1.invoke(this, new ss.EventArgs());
        }
    },
    
    add_sizeChanged: function Open_Core_ViewBase$add_sizeChanged(value) {
        /// <param name="value" type="Function" />
        this.__sizeChanged$1 = ss.Delegate.combine(this.__sizeChanged$1, value);
    },
    remove_sizeChanged: function Open_Core_ViewBase$remove_sizeChanged(value) {
        /// <param name="value" type="Function" />
        this.__sizeChanged$1 = ss.Delegate.remove(this.__sizeChanged$1, value);
    },
    
    __sizeChanged$1: null,
    
    fireSizeChanged: function Open_Core_ViewBase$fireSizeChanged() {
        this.onSizeChanged();
        if (this.__sizeChanged$1 != null) {
            this.__sizeChanged$1.invoke(this, new ss.EventArgs());
        }
    },
    
    _container$1: null,
    
    get_container: function Open_Core_ViewBase$get_container() {
        /// <value type="jQueryObject"></value>
        return this._container$1;
    },
    
    get_outerHtml: function Open_Core_ViewBase$get_outerHtml() {
        /// <value type="String"></value>
        return Open.Core.Html.toHtml(this.get_container());
    },
    
    get_innerHtml: function Open_Core_ViewBase$get_innerHtml() {
        /// <value type="String"></value>
        return this.get_container().html();
    },
    
    get_isEnabled: function Open_Core_ViewBase$get_isEnabled() {
        /// <value type="Boolean"></value>
        return this.get(Open.Core.ViewBase.propIsEnabled, true);
    },
    set_isEnabled: function Open_Core_ViewBase$set_isEnabled(value) {
        /// <value type="Boolean"></value>
        if (this.set(Open.Core.ViewBase.propIsEnabled, value, true)) {
            this.fireIsEnabledChanged();
        }
        return value;
    },
    
    get_isVisible: function Open_Core_ViewBase$get_isVisible() {
        /// <value type="Boolean"></value>
        return (this.get_container() == null) ? false : Open.Core.Css.isVisible(this.get_container());
    },
    set_isVisible: function Open_Core_ViewBase$set_isVisible(value) {
        /// <value type="Boolean"></value>
        if (value === this.get_isVisible()) {
            return;
        }
        this.setCss(Open.Core.Css.display, (value) ? Open.Core.Css.block : Open.Core.Css.none);
        this.fireIsVisibleChanged();
        this.firePropertyChanged(Open.Core.ViewBase.propIsVisible);
        return value;
    },
    
    get_background: function Open_Core_ViewBase$get_background() {
        /// <value type="String"></value>
        return this.getCss(Open.Core.Css.background);
    },
    set_background: function Open_Core_ViewBase$set_background(value) {
        /// <value type="String"></value>
        this.setCss(Open.Core.Css.background, value);
        this.firePropertyChanged(Open.Core.ViewBase.propBackground);
        return value;
    },
    
    get_opacity: function Open_Core_ViewBase$get_opacity() {
        /// <value type="Number"></value>
        return parseFloat(this.getCss(Open.Core.Css.opacity));
    },
    set_opacity: function Open_Core_ViewBase$set_opacity(value) {
        /// <value type="Number"></value>
        value = Open.Core.Helper.get_numberDouble().withinBounds(value, 0, 1);
        if (value === this.get_opacity()) {
            return;
        }
        Open.Core.Css.setOpacity(this.get_container(), value);
        this.firePropertyChanged(Open.Core.ViewBase.propOpacity);
        return value;
    },
    
    get_width: function Open_Core_ViewBase$get_width() {
        /// <value type="Number" integer="true"></value>
        return (this.get_container() == null) ? 0 : this.get_container().width();
    },
    set_width: function Open_Core_ViewBase$set_width(value) {
        /// <value type="Number" integer="true"></value>
        if (value === this.get_width()) {
            return;
        }
        this._setSizeInternal$1(value, Open.Core.SizeDimension.width, true);
        this.firePropertyChanged(Open.Core.ViewBase.propWidth);
        return value;
    },
    
    get_height: function Open_Core_ViewBase$get_height() {
        /// <value type="Number" integer="true"></value>
        return (this.get_container() == null) ? 0 : this.get_container().height();
    },
    set_height: function Open_Core_ViewBase$set_height(value) {
        /// <value type="Number" integer="true"></value>
        if (value === this.get_height()) {
            return;
        }
        this._setSizeInternal$1(value, Open.Core.SizeDimension.height, true);
        this.firePropertyChanged(Open.Core.ViewBase.propHeight);
        return value;
    },
    
    setSize: function Open_Core_ViewBase$setSize(width, height) {
        /// <param name="width" type="Number" integer="true">
        /// </param>
        /// <param name="height" type="Number" integer="true">
        /// </param>
        if (width === this.get_width() && height === this.get_height()) {
            return;
        }
        this._setSizeInternal$1(width, Open.Core.SizeDimension.width, false);
        this._setSizeInternal$1(height, Open.Core.SizeDimension.height, false);
        this.fireSizeChanged();
    },
    
    _setSizeInternal$1: function Open_Core_ViewBase$_setSizeInternal$1(value, dimension, withEvent) {
        /// <param name="value" type="Number" integer="true">
        /// </param>
        /// <param name="dimension" type="Open.Core.SizeDimension">
        /// </param>
        /// <param name="withEvent" type="Boolean">
        /// </param>
        if (value < 0) {
            value = 0;
        }
        this.setCss((dimension === Open.Core.SizeDimension.width) ? Open.Core.Css.width : Open.Core.Css.height, value + Open.Core.Css.px);
        if (withEvent) {
            this.fireSizeChanged();
        }
    },
    
    beforeInsertReplace: function Open_Core_ViewBase$beforeInsertReplace(replacedElement) {
        /// <summary>
        /// Invoked immediately before an Insert operation of mode 'Replace' is executed.
        /// </summary>
        /// <param name="replacedElement" type="jQueryObject">
        /// The element being replaced with this control.
        /// </param>
    },
    
    insert: function Open_Core_ViewBase$insert(cssSeletor, mode) {
        /// <param name="cssSeletor" type="String">
        /// </param>
        /// <param name="mode" type="Open.Core.InsertMode">
        /// </param>
        switch (mode) {
            case Open.Core.InsertMode.replace:
                var replaceElement = $(cssSeletor);
                if (replaceElement == null) {
                    throw this._getInsertException$1(cssSeletor, 'No such element exists');
                }
                this.beforeInsertReplace(replaceElement);
                Open.Core.Css.copyClasses(replaceElement, this.get_container());
                this.get_container().replaceAll(cssSeletor);
                break;
            default:
                throw this._getInsertException$1(cssSeletor, String.format('The insert mode \'{0}\' is not supported.', Open.Core.InsertMode.toString(mode)));
        }
    },
    
    _getInsertException$1: function Open_Core_ViewBase$_getInsertException$1(cssSeletor, message) {
        /// <param name="cssSeletor" type="String">
        /// </param>
        /// <param name="message" type="String">
        /// </param>
        /// <returns type="Error"></returns>
        return new Error(String.format('Failed to insert [{0}] at \'{1}\'. {2}', Type.getInstanceType(this).get_name(), cssSeletor, message));
    },
    
    getCss: function Open_Core_ViewBase$getCss(attribute) {
        /// <param name="attribute" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var value = this.get_container().css(attribute);
        return (String.isNullOrEmpty(value)) ? null : value;
    },
    
    setCss: function Open_Core_ViewBase$setCss(attribute, value) {
        /// <param name="attribute" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        this.get_container().css(attribute, value);
    },
    
    getAttribute: function Open_Core_ViewBase$getAttribute(attribute) {
        /// <param name="attribute" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var value = this.get_container().attr(attribute);
        return (String.isNullOrEmpty(value)) ? null : value;
    },
    
    setAttribute: function Open_Core_ViewBase$setAttribute(attribute, value) {
        /// <param name="attribute" type="String">
        /// </param>
        /// <param name="value" type="String">
        /// </param>
        this.get_container().attr(attribute, value);
    },
    
    onIsEnabledChanged: function Open_Core_ViewBase$onIsEnabledChanged() {
    },
    
    onIsVisibleChanged: function Open_Core_ViewBase$onIsVisibleChanged() {
    },
    
    onSizeChanged: function Open_Core_ViewBase$onSizeChanged() {
    },
    
    retrieveHtml: function Open_Core_ViewBase$retrieveHtml(url, onComplete) {
        /// <summary>
        /// Inserts the HTML from the specified URL.
        /// </summary>
        /// <param name="url" type="String">
        /// The URL of the HTML content to retrieve.
        /// </param>
        /// <param name="onComplete" type="Action">
        /// Action to invoke upon completion.
        /// </param>
        $.get(Open.Core.Helper.get_url().prependDomain(url), ss.Delegate.create(this, function(data) {
            this.get_container().html(data.toString());
            Open.Core.Helper.invokeOrDefault(onComplete);
        }));
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.TreeNodeEventArgs

Open.Core.TreeNodeEventArgs = function Open_Core_TreeNodeEventArgs(node, index) {
    /// <summary>
    /// Event arguments accompanying a 'TreeNode' operation.
    /// </summary>
    /// <param name="node" type="Open.Core.ITreeNode">
    /// The tree-node which is the subject of the event.
    /// </param>
    /// <param name="index" type="Number" integer="true">
    /// The index of the node within it's parent (-1 if not known or applicable).
    /// </param>
    /// <field name="_node$1" type="Open.Core.ITreeNode">
    /// </field>
    /// <field name="_index$1" type="Number" integer="true">
    /// </field>
    Open.Core.TreeNodeEventArgs.initializeBase(this);
    this._node$1 = node;
    this._index$1 = index;
}
Open.Core.TreeNodeEventArgs.prototype = {
    _node$1: null,
    _index$1: 0,
    
    get_node: function Open_Core_TreeNodeEventArgs$get_node() {
        /// <summary>
        /// Gets the tree-node which is the subject of the event.
        /// </summary>
        /// <value type="Open.Core.ITreeNode"></value>
        return this._node$1;
    },
    
    get_index: function Open_Core_TreeNodeEventArgs$get_index() {
        /// <summary>
        /// Gets the index of the node within it's parent (-1 if not known or applicable).
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._index$1;
    },
    
    get_hasIndex: function Open_Core_TreeNodeEventArgs$get_hasIndex() {
        /// <summary>
        /// Gets whether an index value exists.
        /// </summary>
        /// <value type="Boolean"></value>
        return this.get_index() !== Open.Core.TreeNode.nullIndex;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.PropertyChangedEventArgs

Open.Core.PropertyChangedEventArgs = function Open_Core_PropertyChangedEventArgs(property) {
    /// <summary>
    /// Event arguments accompanying the 'PropertyChanged' event.
    /// </summary>
    /// <param name="property" type="Open.Core.PropertyRef">
    /// The property that has changed.
    /// </param>
    /// <field name="_property$1" type="Open.Core.PropertyRef">
    /// </field>
    Open.Core.PropertyChangedEventArgs.initializeBase(this);
    this._property$1 = property;
}
Open.Core.PropertyChangedEventArgs.prototype = {
    _property$1: null,
    
    get_property: function Open_Core_PropertyChangedEventArgs$get_property() {
        /// <summary>
        /// Gets the reference to the property that has changed.
        /// </summary>
        /// <value type="Open.Core.PropertyRef"></value>
        return this._property$1;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.GlobalEvents

Open.Core.GlobalEvents = function Open_Core_GlobalEvents() {
    /// <summary>
    /// Handles and fires global events.
    /// </summary>
    /// <field name="__windowResize" type="EventHandler" static="true">
    /// </field>
    /// <field name="__windowResizeComplete" type="EventHandler" static="true">
    /// </field>
    /// <field name="__panelResized" type="EventHandler" static="true">
    /// </field>
    /// <field name="__panelResizeComplete" type="EventHandler" static="true">
    /// </field>
    /// <field name="__horizontalPanelResized" type="EventHandler" static="true">
    /// </field>
    /// <field name="__verticalPanelResized" type="EventHandler" static="true">
    /// </field>
    /// <field name="_resizeDelay" type="Number" static="true">
    /// </field>
    /// <field name="_windowResizeDelay" type="Open.Core.DelayedAction" static="true">
    /// </field>
    /// <field name="_panelResizeDelay" type="Open.Core.DelayedAction" static="true">
    /// </field>
}
Open.Core.GlobalEvents.add_windowResize = function Open_Core_GlobalEvents$add_windowResize(value) {
    /// <summary>
    /// Fires when the browser Window is resizing.
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__windowResize = ss.Delegate.combine(Open.Core.GlobalEvents.__windowResize, value);
}
Open.Core.GlobalEvents.remove_windowResize = function Open_Core_GlobalEvents$remove_windowResize(value) {
    /// <summary>
    /// Fires when the browser Window is resizing.
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__windowResize = ss.Delegate.remove(Open.Core.GlobalEvents.__windowResize, value);
}
Open.Core.GlobalEvents._fireWindowResize = function Open_Core_GlobalEvents$_fireWindowResize() {
    if (Open.Core.GlobalEvents.__windowResize != null) {
        Open.Core.GlobalEvents.__windowResize.invoke(Open.Core.GlobalEvents, new ss.EventArgs());
    }
}
Open.Core.GlobalEvents.add_windowResizeComplete = function Open_Core_GlobalEvents$add_windowResizeComplete(value) {
    /// <summary>
    /// Fires when the browser Window is completed resizing (uses an event-delay).
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__windowResizeComplete = ss.Delegate.combine(Open.Core.GlobalEvents.__windowResizeComplete, value);
}
Open.Core.GlobalEvents.remove_windowResizeComplete = function Open_Core_GlobalEvents$remove_windowResizeComplete(value) {
    /// <summary>
    /// Fires when the browser Window is completed resizing (uses an event-delay).
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__windowResizeComplete = ss.Delegate.remove(Open.Core.GlobalEvents.__windowResizeComplete, value);
}
Open.Core.GlobalEvents._fireWindowResizeComplete = function Open_Core_GlobalEvents$_fireWindowResizeComplete() {
    if (Open.Core.GlobalEvents.__windowResizeComplete != null) {
        Open.Core.GlobalEvents.__windowResizeComplete.invoke(Open.Core.GlobalEvents, new ss.EventArgs());
    }
}
Open.Core.GlobalEvents.add_panelResized = function Open_Core_GlobalEvents$add_panelResized(value) {
    /// <summary>
    /// Fires when any PanelResizer is resizing.
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__panelResized = ss.Delegate.combine(Open.Core.GlobalEvents.__panelResized, value);
}
Open.Core.GlobalEvents.remove_panelResized = function Open_Core_GlobalEvents$remove_panelResized(value) {
    /// <summary>
    /// Fires when any PanelResizer is resizing.
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__panelResized = ss.Delegate.remove(Open.Core.GlobalEvents.__panelResized, value);
}
Open.Core.GlobalEvents._firePanelResized = function Open_Core_GlobalEvents$_firePanelResized(sender) {
    /// <param name="sender" type="Object">
    /// </param>
    if (Open.Core.GlobalEvents.__panelResized != null) {
        Open.Core.GlobalEvents.__panelResized.invoke(sender, new ss.EventArgs());
    }
}
Open.Core.GlobalEvents.add_panelResizeComplete = function Open_Core_GlobalEvents$add_panelResizeComplete(value) {
    /// <summary>
    /// Fires when the PanelResizer has compled resizing (uses an event-delay).
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__panelResizeComplete = ss.Delegate.combine(Open.Core.GlobalEvents.__panelResizeComplete, value);
}
Open.Core.GlobalEvents.remove_panelResizeComplete = function Open_Core_GlobalEvents$remove_panelResizeComplete(value) {
    /// <summary>
    /// Fires when the PanelResizer has compled resizing (uses an event-delay).
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__panelResizeComplete = ss.Delegate.remove(Open.Core.GlobalEvents.__panelResizeComplete, value);
}
Open.Core.GlobalEvents._firePanelResizeComplete = function Open_Core_GlobalEvents$_firePanelResizeComplete() {
    if (Open.Core.GlobalEvents.__panelResizeComplete != null) {
        Open.Core.GlobalEvents.__panelResizeComplete.invoke(Open.Core.GlobalEvents, new ss.EventArgs());
    }
}
Open.Core.GlobalEvents.add_horizontalPanelResized = function Open_Core_GlobalEvents$add_horizontalPanelResized(value) {
    /// <summary>
    /// Fires when any HorizontalPanelResizer is resizing.
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__horizontalPanelResized = ss.Delegate.combine(Open.Core.GlobalEvents.__horizontalPanelResized, value);
}
Open.Core.GlobalEvents.remove_horizontalPanelResized = function Open_Core_GlobalEvents$remove_horizontalPanelResized(value) {
    /// <summary>
    /// Fires when any HorizontalPanelResizer is resizing.
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__horizontalPanelResized = ss.Delegate.remove(Open.Core.GlobalEvents.__horizontalPanelResized, value);
}
Open.Core.GlobalEvents._fireHorizontalPanelResized = function Open_Core_GlobalEvents$_fireHorizontalPanelResized(sender) {
    /// <param name="sender" type="Object">
    /// </param>
    if (Open.Core.GlobalEvents.__horizontalPanelResized != null) {
        Open.Core.GlobalEvents.__horizontalPanelResized.invoke(sender, new ss.EventArgs());
    }
}
Open.Core.GlobalEvents.add_verticalPanelResized = function Open_Core_GlobalEvents$add_verticalPanelResized(value) {
    /// <summary>
    /// Fires when any VerticalPanelResizer is resizing.
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__verticalPanelResized = ss.Delegate.combine(Open.Core.GlobalEvents.__verticalPanelResized, value);
}
Open.Core.GlobalEvents.remove_verticalPanelResized = function Open_Core_GlobalEvents$remove_verticalPanelResized(value) {
    /// <summary>
    /// Fires when any VerticalPanelResizer is resizing.
    /// </summary>
    /// <param name="value" type="Function" />
    Open.Core.GlobalEvents.__verticalPanelResized = ss.Delegate.remove(Open.Core.GlobalEvents.__verticalPanelResized, value);
}
Open.Core.GlobalEvents._fireVerticalPanelResized = function Open_Core_GlobalEvents$_fireVerticalPanelResized(sender) {
    /// <param name="sender" type="Object">
    /// </param>
    if (Open.Core.GlobalEvents.__verticalPanelResized != null) {
        Open.Core.GlobalEvents.__verticalPanelResized.invoke(sender, new ss.EventArgs());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Log

Open.Core.Log = function Open_Core_Log() {
    /// <summary>
    /// Static log writer.
    /// </summary>
    /// <field name="_writer" type="Open.Core.LogWriter" static="true">
    /// </field>
}
Open.Core.Log.get_isActive = function Open_Core_Log$get_isActive() {
    /// <summary>
    /// Gets or sets whether the log is active.  When False nothing is written to the log.
    /// </summary>
    /// <value type="Boolean"></value>
    return Open.Core.Log._writer.get_isActive();
}
Open.Core.Log.set_isActive = function Open_Core_Log$set_isActive(value) {
    /// <summary>
    /// Gets or sets whether the log is active.  When False nothing is written to the log.
    /// </summary>
    /// <value type="Boolean"></value>
    Open.Core.Log._writer.set_isActive(value);
    return value;
}
Open.Core.Log.get_writer = function Open_Core_Log$get_writer() {
    /// <summary>
    /// Gets the specific log-writer instance that the static methods write to.
    /// </summary>
    /// <value type="Open.Core.LogWriter"></value>
    return Open.Core.Log._writer || (Open.Core.Log._writer = new Open.Core.LogWriter());
}
Open.Core.Log.get_view = function Open_Core_Log$get_view() {
    /// <summary>
    /// Gets or sets the view-control to write to.
    /// </summary>
    /// <value type="Open.Core.ILogView"></value>
    return Open.Core.Log.get_writer().get_view();
}
Open.Core.Log.set_view = function Open_Core_Log$set_view(value) {
    /// <summary>
    /// Gets or sets the view-control to write to.
    /// </summary>
    /// <value type="Open.Core.ILogView"></value>
    Open.Core.Log.get_writer().set_view(value);
    return value;
}
Open.Core.Log.title = function Open_Core_Log$title(message) {
    /// <summary>
    /// Writes a informational message to the log (as a bold title).
    /// </summary>
    /// <param name="message" type="String">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.get_writer().title(message);
}
Open.Core.Log.info = function Open_Core_Log$info(message) {
    /// <summary>
    /// Writes a informational message to the log.
    /// </summary>
    /// <param name="message" type="Object">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.get_writer().info(message);
}
Open.Core.Log.debug = function Open_Core_Log$debug(message) {
    /// <summary>
    /// Writes a debug message to the log.
    /// </summary>
    /// <param name="message" type="Object">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.get_writer().debug(message);
}
Open.Core.Log.warning = function Open_Core_Log$warning(message) {
    /// <summary>
    /// Writes a warning to the log.
    /// </summary>
    /// <param name="message" type="Object">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.get_writer().warning(message);
}
Open.Core.Log.error = function Open_Core_Log$error(message) {
    /// <summary>
    /// Writes an error message to the log.
    /// </summary>
    /// <param name="message" type="Object">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.get_writer().error(message);
}
Open.Core.Log.success = function Open_Core_Log$success(message) {
    /// <summary>
    /// Writes a success message to the log.
    /// </summary>
    /// <param name="message" type="Object">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.get_writer().success(message);
}
Open.Core.Log.write = function Open_Core_Log$write(message, severity) {
    /// <summary>
    /// Writes a message to the log.
    /// </summary>
    /// <param name="message" type="Object">
    /// The message to write (HTML).
    /// </param>
    /// <param name="severity" type="Open.Core.LogSeverity">
    /// The severity of the message.
    /// </param>
    Open.Core.Log.get_writer().write(message, severity);
}
Open.Core.Log.lineBreak = function Open_Core_Log$lineBreak() {
    /// <summary>
    /// Inserts a line break to the log.
    /// </summary>
    Open.Core.Log.get_writer().lineBreak();
}
Open.Core.Log.newSection = function Open_Core_Log$newSection() {
    /// <summary>
    /// Inserts a new section divider.
    /// </summary>
    Open.Core.Log.get_writer().newSection();
}
Open.Core.Log.clear = function Open_Core_Log$clear() {
    /// <summary>
    /// Clears the log.
    /// </summary>
    Open.Core.Log.get_writer().clear();
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.LogWriter

Open.Core.LogWriter = function Open_Core_LogWriter() {
    /// <summary>
    /// An output log.
    /// </summary>
    /// <field name="_isActive$1" type="Boolean">
    /// </field>
    /// <field name="_canInsertSection$1" type="Boolean">
    /// </field>
    /// <field name="_view$1" type="Open.Core.ILogView">
    /// </field>
    Open.Core.LogWriter.initializeBase(this);
    Open.Core.Css.insertLink(Open.Core.LogCss.url);
}
Open.Core.LogWriter.prototype = {
    _isActive$1: true,
    _canInsertSection$1: true,
    _view$1: null,
    
    onDisposed: function Open_Core_LogWriter$onDisposed() {
        /// <summary>
        /// Destructor.
        /// </summary>
        Open.Core.Helper.dispose(this.get_view());
        Open.Core.LogWriter.callBaseMethod(this, 'onDisposed');
    },
    
    get_view: function Open_Core_LogWriter$get_view() {
        /// <summary>
        /// Gets or sets the view-control to write to.
        /// </summary>
        /// <value type="Open.Core.ILogView"></value>
        return this._view$1;
    },
    set_view: function Open_Core_LogWriter$set_view(value) {
        /// <summary>
        /// Gets or sets the view-control to write to.
        /// </summary>
        /// <value type="Open.Core.ILogView"></value>
        this._view$1 = value;
        return value;
    },
    
    get_isActive: function Open_Core_LogWriter$get_isActive() {
        /// <summary>
        /// Gets or sets whether the log is active.  When False nothing is written to the log.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isActive$1;
    },
    set_isActive: function Open_Core_LogWriter$set_isActive(value) {
        /// <summary>
        /// Gets or sets whether the log is active.  When False nothing is written to the log.
        /// </summary>
        /// <value type="Boolean"></value>
        this._isActive$1 = value;
        return value;
    },
    
    get__canWrite$1: function Open_Core_LogWriter$get__canWrite$1() {
        /// <value type="Boolean"></value>
        return this.get_isActive() && this.get_view() != null;
    },
    
    title: function Open_Core_LogWriter$title(message) {
        /// <summary>
        /// Writes a informational message to the log (as a bold title).
        /// </summary>
        /// <param name="message" type="String">
        /// The messge to write (HTML).
        /// </param>
        this.write(Open.Core.Html.toBold(message), Open.Core.LogSeverity.info);
    },
    
    info: function Open_Core_LogWriter$info(message) {
        /// <summary>
        /// Writes a informational message to the log.
        /// </summary>
        /// <param name="message" type="Object">
        /// The messge to write (HTML).
        /// </param>
        this.write(message, Open.Core.LogSeverity.info);
    },
    
    debug: function Open_Core_LogWriter$debug(message) {
        /// <summary>
        /// Writes a debug message to the log.
        /// </summary>
        /// <param name="message" type="Object">
        /// The messge to write (HTML).
        /// </param>
        this.write(message, Open.Core.LogSeverity.debug);
    },
    
    warning: function Open_Core_LogWriter$warning(message) {
        /// <summary>
        /// Writes a warning to the log.
        /// </summary>
        /// <param name="message" type="Object">
        /// The messge to write (HTML).
        /// </param>
        this.write(message, Open.Core.LogSeverity.warning);
    },
    
    error: function Open_Core_LogWriter$error(message) {
        /// <summary>
        /// Writes an error message to the log.
        /// </summary>
        /// <param name="message" type="Object">
        /// The messge to write (HTML).
        /// </param>
        this.write(message, Open.Core.LogSeverity.error);
    },
    
    success: function Open_Core_LogWriter$success(message) {
        /// <summary>
        /// Writes a success message to the log.
        /// </summary>
        /// <param name="message" type="Object">
        /// The messge to write (HTML).
        /// </param>
        this.write(message, Open.Core.LogSeverity.success);
    },
    
    write: function Open_Core_LogWriter$write(message, severity) {
        /// <param name="message" type="Object">
        /// </param>
        /// <param name="severity" type="Open.Core.LogSeverity">
        /// </param>
        if (!this.get__canWrite$1()) {
            return;
        }
        var css = Open.Core.LogCss.severityClass(severity);
        this.get_view().insert(message, css);
        this._canInsertSection$1 = true;
    },
    
    clear: function Open_Core_LogWriter$clear() {
        if (!this.get__canWrite$1()) {
            return;
        }
        this.get_view().clear();
    },
    
    lineBreak: function Open_Core_LogWriter$lineBreak() {
        /// <summary>
        /// Inserts a line break to the log.
        /// </summary>
        if (!this.get__canWrite$1()) {
            return;
        }
        this.get_view().divider(Open.Core.LogDivider.lineBreak);
    },
    
    newSection: function Open_Core_LogWriter$newSection() {
        /// <summary>
        /// Inserts a new section divider.
        /// </summary>
        if (!this.get__canWrite$1()) {
            return;
        }
        if (!this._canInsertSection$1) {
            return;
        }
        this.get_view().divider(Open.Core.LogDivider.section);
        this._canInsertSection$1 = false;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.LogCss

Open.Core.LogCss = function Open_Core_LogCss() {
    /// <summary>
    /// CSS constants for the log.
    /// </summary>
    /// <field name="url" type="String" static="true">
    /// </field>
    /// <field name="_rootClass" type="String" static="true">
    /// </field>
    /// <field name="listItemClass" type="String" static="true">
    /// </field>
    /// <field name="sectionBreak" type="String" static="true">
    /// </field>
    /// <field name="lineBreakClass" type="String" static="true">
    /// </field>
    /// <field name="counterClass" type="String" static="true">
    /// </field>
    /// <field name="messageClass" type="String" static="true">
    /// </field>
    /// <field name="list" type="String" static="true">
    /// </field>
    /// <field name="listItem" type="String" static="true">
    /// </field>
}
Open.Core.LogCss.severityClass = function Open_Core_LogCss$severityClass(severity) {
    /// <summary>
    /// Retrieves a CSS class for the given severity level.
    /// </summary>
    /// <param name="severity" type="Open.Core.LogSeverity">
    /// The severity of the log message.
    /// </param>
    /// <returns type="String"></returns>
    return String.format('{0}-{1}', Open.Core.LogCss._rootClass, Open.Core.LogSeverity.toString(severity));
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.PropertyBinding

Open.Core.PropertyBinding = function Open_Core_PropertyBinding(source, target) {
    /// <summary>
    /// Manages synchronization between a source property to a target property.
    /// </summary>
    /// <param name="source" type="Open.Core.PropertyRef">
    /// </param>
    /// <param name="target" type="Open.Core.PropertyRef">
    /// </param>
    /// <field name="_source" type="Open.Core.PropertyRef">
    /// </field>
    /// <field name="_target" type="Open.Core.PropertyRef">
    /// </field>
    this._source = source;
    this._target = target;
    source.add_changed(ss.Delegate.create(this, this._onSourceChanged));
    this._sync();
}
Open.Core.PropertyBinding.prototype = {
    _source: null,
    _target: null,
    
    _onSourceChanged: function Open_Core_PropertyBinding$_onSourceChanged(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._sync();
    },
    
    dispose: function Open_Core_PropertyBinding$dispose() {
        /// <summary>
        /// Disposes of the object.
        /// </summary>
        this._source.remove_changed(ss.Delegate.create(this, this._onSourceChanged));
    },
    
    _sync: function Open_Core_PropertyBinding$_sync() {
        this._target.set_value(this._source.get_value());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.PropertyDef

Open.Core.PropertyDef = function Open_Core_PropertyDef(declaringType, name) {
    /// <summary>
    /// Represents a definition of a property.
    /// </summary>
    /// <param name="declaringType" type="Type">
    /// The type of the object that exposes the property.
    /// </param>
    /// <param name="name" type="String">
    /// the name of the property.
    /// </param>
    /// <field name="_declaringType" type="Type">
    /// </field>
    /// <field name="_name" type="String">
    /// </field>
    /// <field name="_formattedName" type="String">
    /// </field>
    /// <field name="_singletons" type="Object" static="true">
    /// </field>
    this._declaringType = declaringType;
    this._name = name;
}
Open.Core.PropertyDef.get__singletons = function Open_Core_PropertyDef$get__singletons() {
    /// <value type="Object"></value>
    return Open.Core.PropertyDef._singletons || (Open.Core.PropertyDef._singletons = {});
}
Open.Core.PropertyDef.getSingletonDef = function Open_Core_PropertyDef$getSingletonDef(declaringType, name) {
    /// <summary>
    /// Gets a singleton version of the property definition.
    /// </summary>
    /// <param name="declaringType" type="Type">
    /// The type of the object that exposes the property.
    /// </param>
    /// <param name="name" type="String">
    /// the name of the property.
    /// </param>
    /// <returns type="Open.Core.PropertyDef"></returns>
    var key = String.format('{0}:{1}', declaringType.get_fullName(), name);
    if (Object.keyExists(Open.Core.PropertyDef.get__singletons(), key)) {
        return Type.safeCast(Open.Core.PropertyDef.get__singletons()[key], Open.Core.PropertyDef);
    }
    var def = new Open.Core.PropertyDef(declaringType, name);
    Open.Core.PropertyDef.get__singletons()[key] = def;
    return def;
}
Open.Core.PropertyDef.prototype = {
    _declaringType: null,
    _name: null,
    _formattedName: null,
    
    get_declaringType: function Open_Core_PropertyDef$get_declaringType() {
        /// <summary>
        /// Gets the type of the object that exposes the property.
        /// </summary>
        /// <value type="Type"></value>
        return this._declaringType;
    },
    
    get_name: function Open_Core_PropertyDef$get_name() {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value type="String"></value>
        return this._name;
    },
    
    get_javaScriptName: function Open_Core_PropertyDef$get_javaScriptName() {
        /// <summary>
        /// Gets the property name with the same casing that is using in the emitted JavaScript.
        /// </summary>
        /// <value type="String"></value>
        return this._formattedName || (this._formattedName = Open.Core.Helper.get_string().toCamelCase(this.get_name()));
    },
    
    get_fullName: function Open_Core_PropertyDef$get_fullName() {
        /// <summary>
        /// Gets the fully qualified name of the property..
        /// </summary>
        /// <value type="String"></value>
        return this.get_declaringType().get_fullName() + ':' + this.get_name();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.PropertyRef

Open.Core.PropertyRef = function Open_Core_PropertyRef(instance, name) {
    /// <summary>
    /// A reference to a property on an object.
    /// </summary>
    /// <param name="instance" type="Object">
    /// The instance of the object that exposes the property.
    /// </param>
    /// <param name="name" type="String">
    /// The name of the property.
    /// </param>
    /// <field name="__changed$1" type="EventHandler">
    /// </field>
    /// <field name="_instance$1" type="Object">
    /// </field>
    /// <field name="_observable$1" type="Open.Core.INotifyPropertyChanged">
    /// </field>
    /// <field name="_bindTo$1" type="Open.Core.PropertyRef">
    /// </field>
    /// <field name="_propertyBinding$1" type="Open.Core.PropertyBinding">
    /// </field>
    Open.Core.PropertyRef.initializeBase(this, [ Type.getInstanceType(instance), name ]);
    this._instance$1 = instance;
    this._observable$1 = Type.safeCast(instance, Open.Core.INotifyPropertyChanged);
    if (this._observable$1 != null) {
        this._observable$1.add_propertyChanged(ss.Delegate.create(this, this._onPropertyChanged$1));
    }
}
Open.Core.PropertyRef.getFromModel = function Open_Core_PropertyRef$getFromModel(obj, propertyName) {
    /// <summary>
    /// Retrieves the PropertyRef from an IModel.
    /// </summary>
    /// <param name="obj" type="Object">
    /// The model object.
    /// </param>
    /// <param name="propertyName" type="String">
    /// The name of the property.
    /// </param>
    /// <returns type="Open.Core.PropertyRef"></returns>
    var model = Type.safeCast(obj, Open.Core.IModel);
    return (model == null) ? null : model.getPropertyRef(propertyName);
}
Open.Core.PropertyRef.prototype = {
    
    add_changed: function Open_Core_PropertyRef$add_changed(value) {
        /// <summary>
        /// Fires when the property value changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__changed$1 = ss.Delegate.combine(this.__changed$1, value);
    },
    remove_changed: function Open_Core_PropertyRef$remove_changed(value) {
        /// <summary>
        /// Fires when the property value changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__changed$1 = ss.Delegate.remove(this.__changed$1, value);
    },
    
    __changed$1: null,
    
    _fireChanged$1: function Open_Core_PropertyRef$_fireChanged$1() {
        if (this.__changed$1 != null) {
            this.__changed$1.invoke(this, new ss.EventArgs());
        }
    },
    
    _instance$1: null,
    _observable$1: null,
    _bindTo$1: null,
    _propertyBinding$1: null,
    
    dispose: function Open_Core_PropertyRef$dispose() {
        /// <summary>
        /// Disposes of the object.
        /// </summary>
        if (this._observable$1 != null) {
            this._observable$1.remove_propertyChanged(ss.Delegate.create(this, this._onPropertyChanged$1));
        }
        if (this._propertyBinding$1 != null) {
            this._propertyBinding$1.dispose();
        }
    },
    
    _onPropertyChanged$1: function Open_Core_PropertyRef$_onPropertyChanged$1(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Core.PropertyChangedEventArgs">
        /// </param>
        if (e.get_property().get_name() !== this.get_name()) {
            return;
        }
        this._fireChanged$1();
    },
    
    get_instance: function Open_Core_PropertyRef$get_instance() {
        /// <summary>
        /// Gets the instance of the object that exposes the property.
        /// </summary>
        /// <value type="Object"></value>
        return this._instance$1;
    },
    
    get_value: function Open_Core_PropertyRef$get_value() {
        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        /// <value type="Object"></value>
        return this.get_instance()['get_' + this.get_javaScriptName()]();
    },
    set_value: function Open_Core_PropertyRef$set_value(value) {
        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        /// <value type="Object"></value>
        this.get_instance()['set_' + this.get_javaScriptName()](value);
        return value;
    },
    
    get_bindTo: function Open_Core_PropertyRef$get_bindTo() {
        /// <summary>
        /// Gets or sets the source property to bind this property to.
        /// </summary>
        /// <value type="Open.Core.PropertyRef"></value>
        return this._bindTo$1;
    },
    set_bindTo: function Open_Core_PropertyRef$set_bindTo(value) {
        /// <summary>
        /// Gets or sets the source property to bind this property to.
        /// </summary>
        /// <value type="Open.Core.PropertyRef"></value>
        if (value === this.get_bindTo()) {
            return;
        }
        this._bindTo$1 = value;
        this._propertyBinding$1 = new Open.Core.PropertyBinding(value, this);
        return value;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.DelayedAction

Open.Core.DelayedAction = function Open_Core_DelayedAction(delaySeconds, action) {
    /// <summary>
    /// Invokes an action after a delay times out (cancelling any previous actions that may be pending).
    /// </summary>
    /// <param name="delaySeconds" type="Number">
    /// The time delay in delay (in seconds).
    /// </param>
    /// <param name="action" type="Action">
    /// The action that is invoked after the delay.
    /// </param>
    /// <field name="__invoked" type="EventHandler">
    /// </field>
    /// <field name="_nullTimerId" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_delay" type="Number">
    /// </field>
    /// <field name="_action" type="Action">
    /// </field>
    /// <field name="_isAsyncronous" type="Boolean" static="true">
    /// </field>
    /// <field name="_timerId" type="Number" integer="true">
    /// </field>
    this._timerId = Open.Core.DelayedAction._nullTimerId;
    this.set_delay(delaySeconds);
    this.set_action(action);
}
Open.Core.DelayedAction.get_isAsyncronous = function Open_Core_DelayedAction$get_isAsyncronous() {
    /// <summary>
    /// Gets or sets whether the delayed action runs asynchronously (true) or synchronously (false) for testing purposes.
    /// </summary>
    /// <value type="Boolean"></value>
    return Open.Core.DelayedAction._isAsyncronous;
}
Open.Core.DelayedAction.set_isAsyncronous = function Open_Core_DelayedAction$set_isAsyncronous(value) {
    /// <summary>
    /// Gets or sets whether the delayed action runs asynchronously (true) or synchronously (false) for testing purposes.
    /// </summary>
    /// <value type="Boolean"></value>
    Open.Core.DelayedAction._isAsyncronous = value;
    return value;
}
Open.Core.DelayedAction.invoke = function Open_Core_DelayedAction$invoke(delay, action) {
    /// <summary>
    /// Invokes the given action after the specified delay.
    /// </summary>
    /// <param name="delay" type="Number">
    /// The delay (in seconds) before invoking the action.
    /// </param>
    /// <param name="action" type="Action">
    /// The action to invoke.
    /// </param>
    /// <returns type="Open.Core.DelayedAction"></returns>
    var delayedAction = new Open.Core.DelayedAction(delay, action);
    delayedAction.start();
    return delayedAction;
}
Open.Core.DelayedAction.prototype = {
    
    add_invoked: function Open_Core_DelayedAction$add_invoked(value) {
        /// <summary>
        /// Fires immediately after the action is invoked when the delay time elapses.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__invoked = ss.Delegate.combine(this.__invoked, value);
    },
    remove_invoked: function Open_Core_DelayedAction$remove_invoked(value) {
        /// <summary>
        /// Fires immediately after the action is invoked when the delay time elapses.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__invoked = ss.Delegate.remove(this.__invoked, value);
    },
    
    __invoked: null,
    
    _fireInvoked: function Open_Core_DelayedAction$_fireInvoked() {
        if (this.__invoked != null) {
            this.__invoked.invoke(this, new ss.EventArgs());
        }
    },
    
    _delay: 0,
    _action: null,
    
    dispose: function Open_Core_DelayedAction$dispose() {
        this.stop();
    },
    
    get_delay: function Open_Core_DelayedAction$get_delay() {
        /// <summary>
        /// Gets or sets the time delay in delay (in seconds).
        /// </summary>
        /// <value type="Number"></value>
        return this._delay;
    },
    set_delay: function Open_Core_DelayedAction$set_delay(value) {
        /// <summary>
        /// Gets or sets the time delay in delay (in seconds).
        /// </summary>
        /// <value type="Number"></value>
        if (value < 0) {
            value = 0;
        }
        this._delay = value;
        return value;
    },
    
    get_action: function Open_Core_DelayedAction$get_action() {
        /// <summary>
        /// Gets or sets the action that is invoked after the delay.
        /// </summary>
        /// <value type="Action"></value>
        return this._action;
    },
    set_action: function Open_Core_DelayedAction$set_action(value) {
        /// <summary>
        /// Gets or sets the action that is invoked after the delay.
        /// </summary>
        /// <value type="Action"></value>
        this._action = value;
        return value;
    },
    
    get_isRunning: function Open_Core_DelayedAction$get_isRunning() {
        /// <summary>
        /// Gets whether the timer is currently in progress.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._timerId !== -1;
    },
    
    start: function Open_Core_DelayedAction$start() {
        /// <summary>
        /// Starts the delay timer.  Subsequent calls to this method restart the timer.
        /// </summary>
        this.stop();
        if (Open.Core.DelayedAction.get_isAsyncronous()) {
            this._timerId = window.setTimeout(ss.Delegate.create(this, function() {
                this._invokeAction();
            }), Open.Core.Helper.get_time().toMsecs(this.get_delay()));
        }
        else {
            this._invokeAction();
        }
    },
    
    stop: function Open_Core_DelayedAction$stop() {
        /// <summary>
        /// Stops the timer.
        /// </summary>
        if (this.get_isRunning()) {
            window.clearTimeout(this._timerId);
        }
        this._timerId = Open.Core.DelayedAction._nullTimerId;
    },
    
    _invokeAction: function Open_Core_DelayedAction$_invokeAction() {
        if (ss.isNullOrUndefined(this.get_action())) {
            return;
        }
        this.get_action().invoke();
        this._fireInvoked();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Color

Open.Core.Color = function Open_Core_Color() {
    /// <summary>
    /// Class for working with colors.
    /// </summary>
    /// <field name="hotPink" type="String" static="true">
    /// </field>
}
Open.Core.Color.black = function Open_Core_Color$black(opacity) {
    /// <summary>
    /// Gets an RGBA value of black at the given opacity.
    /// </summary>
    /// <param name="opacity" type="Number">
    /// The opacity percentage (0..1).
    /// </param>
    /// <returns type="String"></returns>
    return String.format('rgba(0,0,0,{0})', Open.Core.Helper.get_numberDouble().withinBounds(opacity, 0, 1));
}
Open.Core.Color.white = function Open_Core_Color$white(opacity) {
    /// <summary>
    /// Gets an RGBA value of white at the given opacity.
    /// </summary>
    /// <param name="opacity" type="Number">
    /// The opacity percentage (0..1).
    /// </param>
    /// <returns type="String"></returns>
    return String.format('rgba(255,255,255,{0})', Open.Core.Helper.get_numberDouble().withinBounds(opacity, 0, 1));
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Url

Open.Core.Url = function Open_Core_Url() {
    /// <summary>
    /// Url utility.
    /// </summary>
    /// <field name="escAnd" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Html

Open.Core.Html = function Open_Core_Html() {
    /// <summary>
    /// HTML constants and DOM manipulation.
    /// </summary>
    /// <field name="head" type="String" static="true">
    /// </field>
    /// <field name="body" type="String" static="true">
    /// </field>
    /// <field name="div" type="String" static="true">
    /// </field>
    /// <field name="span" type="String" static="true">
    /// </field>
    /// <field name="img" type="String" static="true">
    /// </field>
    /// <field name="button" type="String" static="true">
    /// </field>
    /// <field name="anchor" type="String" static="true">
    /// </field>
    /// <field name="id" type="String" static="true">
    /// </field>
    /// <field name="href" type="String" static="true">
    /// </field>
    /// <field name="src" type="String" static="true">
    /// </field>
    /// <field name="type" type="String" static="true">
    /// </field>
    /// <field name="value" type="String" static="true">
    /// </field>
    /// <field name="disabled" type="String" static="true">
    /// </field>
    /// <field name="classAttr" type="String" static="true">
    /// </field>
    /// <field name="submit" type="String" static="true">
    /// </field>
    /// <field name="scrollTop" type="String" static="true">
    /// </field>
    /// <field name="scrollHeight" type="String" static="true">
    /// </field>
    /// <field name="click" type="String" static="true">
    /// </field>
}
Open.Core.Html.appendDiv = function Open_Core_Html$appendDiv(parent) {
    /// <summary>
    /// Creates and appends a DIV element within the given parent.
    /// </summary>
    /// <param name="parent" type="jQueryObject">
    /// The parent element to insert into
    /// </param>
    /// <returns type="jQueryObject"></returns>
    return Open.Core.Html.append(parent, Open.Core.Html.div);
}
Open.Core.Html.append = function Open_Core_Html$append(parent, tag) {
    /// <summary>
    /// Creates and appends a DIV element within the given parent.
    /// </summary>
    /// <param name="parent" type="jQueryObject">
    /// The parent element to insert into
    /// </param>
    /// <param name="tag" type="String">
    /// The tag name (NOT including angle brackets).
    /// </param>
    /// <returns type="jQueryObject"></returns>
    Open.Core.Html.createElement(tag).appendTo(parent);
    return parent.last().contents();
}
Open.Core.Html.createDiv = function Open_Core_Html$createDiv() {
    /// <summary>
    /// Creates a DIV element.
    /// </summary>
    /// <returns type="jQueryObject"></returns>
    return Open.Core.Html.createElement(Open.Core.Html.div);
}
Open.Core.Html.createSpan = function Open_Core_Html$createSpan() {
    /// <summary>
    /// Creates a SPAN element.
    /// </summary>
    /// <returns type="jQueryObject"></returns>
    return Open.Core.Html.createElement(Open.Core.Html.span);
}
Open.Core.Html.createImage = function Open_Core_Html$createImage(src, alt) {
    /// <summary>
    /// Creates an IMG element.
    /// </summary>
    /// <param name="src" type="String">
    /// The URL to the image.
    /// </param>
    /// <param name="alt" type="String">
    /// The alternative text for the image.
    /// </param>
    /// <returns type="jQueryObject"></returns>
    return $(String.format('<img src=\'{0}\' alt=\'{1}\' />', src, alt));
}
Open.Core.Html.createElement = function Open_Core_Html$createElement(tag) {
    /// <summary>
    /// Creates a new element with the given tag.
    /// </summary>
    /// <param name="tag" type="String">
    /// The HTML tag.
    /// </param>
    /// <returns type="jQueryObject"></returns>
    return $(String.format('<{0}></{0}>', tag));
}
Open.Core.Html.childAt = function Open_Core_Html$childAt(index, parent) {
    /// <summary>
    /// Retrieves the child at the specified index, otherwise Null.
    /// </summary>
    /// <param name="index" type="Number" integer="true">
    /// The index of the child (0-based).
    /// </param>
    /// <param name="parent" type="jQueryObject">
    /// The parent to look within.
    /// </param>
    /// <returns type="jQueryObject"></returns>
    var element = parent.children(String.format(':nth-child({0})', index + 1));
    return (element.length === 0) ? null : element;
}
Open.Core.Html.getOrCreateId = function Open_Core_Html$getOrCreateId(element) {
    /// <summary>
    /// Gets the elements ID, creating a unique ID of the element doesn't already have one.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to get the ID for.
    /// </param>
    /// <returns type="String"></returns>
    var id = element.attr(Open.Core.Html.id);
    if (String.isNullOrEmpty(id)) {
        id = Open.Core.Helper.createId();
        element.attr(Open.Core.Html.id, id);
    }
    return id;
}
Open.Core.Html.toHyperlink = function Open_Core_Html$toHyperlink(url, text, target) {
    /// <summary>
    /// Formats the URL as a hyperlink.
    /// </summary>
    /// <param name="url" type="String">
    /// The url to link to.
    /// </param>
    /// <param name="text" type="String">
    /// The display text of the link (null to use the URL).
    /// </param>
    /// <param name="target" type="Open.Core.LinkTarget">
    /// The target attribute.
    /// </param>
    /// <returns type="String"></returns>
    if (text == null) {
        text = url;
    }
    if (ss.isUndefined(target)) {
        target = Open.Core.LinkTarget.blank;
    }
    return String.format('<a href=\'{0}\' target=\'_{2}\'>{1}</a>', url, text, Open.Core.LinkTarget.toString(target));
}
Open.Core.Html.toBold = function Open_Core_Html$toBold(text) {
    /// <summary>
    /// Wraps the given text in <b></b> elements.
    /// </summary>
    /// <param name="text" type="String">
    /// The text to wrap.
    /// </param>
    /// <returns type="String"></returns>
    return String.format('<b>{0}</b>', text);
}
Open.Core.Html.spanIndent = function Open_Core_Html$spanIndent(pixels) {
    /// <summary>
    /// Creates a SPAN element with a magin-left set to the specified pixels (useful for indenting text).
    /// </summary>
    /// <param name="pixels" type="Number" integer="true">
    /// The number of pixels to indent.
    /// </param>
    /// <returns type="String"></returns>
    return String.format('<span style=\'margin-left:{0}px;\'></span>', pixels);
}
Open.Core.Html.width = function Open_Core_Html$width(cssSelector) {
    /// <summary>
    /// Retrieves the width of the specified element.
    /// </summary>
    /// <param name="cssSelector" type="String">
    /// The CSS selector of the element to measure.
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    return $(cssSelector).width();
}
Open.Core.Html.height = function Open_Core_Html$height(cssSelector) {
    /// <summary>
    /// Retrieves the height of the specified element.
    /// </summary>
    /// <param name="cssSelector" type="String">
    /// The CSS selector of the element to measure.
    /// </param>
    /// <returns type="Number" integer="true"></returns>
    return $(cssSelector).height();
}
Open.Core.Html.replaceWith = function Open_Core_Html$replaceWith(replaceSeletor, withReplacement, copyCssClasses) {
    /// <summary>
    /// Replaces an element with the given object.
    /// </summary>
    /// <param name="replaceSeletor" type="String">
    /// The CSS selector the element(s) to replace.
    /// </param>
    /// <param name="withReplacement" type="jQueryObject">
    /// The element to insert.
    /// </param>
    /// <param name="copyCssClasses" type="Boolean">
    /// Flag indicating if CSS classes should be copied from the old element to the new one.
    /// </param>
    if (copyCssClasses) {
        var replaceElement = $(replaceSeletor);
        Open.Core.Css.copyClasses(replaceElement, withReplacement);
    }
    withReplacement.replaceAll(replaceSeletor);
}
Open.Core.Html.toHtml = function Open_Core_Html$toHtml(element) {
    /// <summary>
    /// Retreives the OuterHtml of the given element.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to retrieve the HTML for.
    /// </param>
    /// <returns type="String"></returns>
    if (element == null) {
        return null;
    }
    return Open.Core.Html.createDiv().append(element.clone()).html();
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Css

Open.Core.Css = function Open_Core_Css() {
    /// <summary>
    /// CSS utility.
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
    /// <field name="background" type="String" static="true">
    /// </field>
    /// <field name="color" type="String" static="true">
    /// </field>
    /// <field name="display" type="String" static="true">
    /// </field>
    /// <field name="position" type="String" static="true">
    /// </field>
    /// <field name="padding" type="String" static="true">
    /// </field>
    /// <field name="margin" type="String" static="true">
    /// </field>
    /// <field name="overflow" type="String" static="true">
    /// </field>
    /// <field name="opacity" type="String" static="true">
    /// </field>
    /// <field name="fontSize" type="String" static="true">
    /// </field>
    /// <field name="textAlign" type="String" static="true">
    /// </field>
    /// <field name="block" type="String" static="true">
    /// </field>
    /// <field name="none" type="String" static="true">
    /// </field>
    /// <field name="relative" type="String" static="true">
    /// </field>
    /// <field name="absolute" type="String" static="true">
    /// </field>
    /// <field name="px" type="String" static="true">
    /// </field>
    /// <field name="classes" type="Open.Core.CoreCssClasses" static="true">
    /// </field>
    /// <field name="urls" type="Open.Core.CoreCssUrls" static="true">
    /// </field>
}
Open.Core.Css.isVisible = function Open_Core_Css$isVisible(element) {
    /// <summary>
    /// Determines whether the element is visible (has any display value other than 'None').
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to display.
    /// </param>
    /// <returns type="Boolean"></returns>
    return (ss.isNullOrUndefined(element)) ? false : element.css(Open.Core.Css.display).toLowerCase() !== Open.Core.Css.none;
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
Open.Core.Css.toId = function Open_Core_Css$toId(identifier) {
    /// <summary>
    /// Prepends the # to a CSS identifier if it's not present (eg: id='one' would be '#one').
    /// </summary>
    /// <param name="identifier" type="String">
    /// The ID value.
    /// </param>
    /// <returns type="String"></returns>
    return Open.Core.Css._prependSelectorPrefix(identifier, '#');
}
Open.Core.Css.toClass = function Open_Core_Css$toClass(cclass) {
    /// <summary>
    /// Prepends the period (.) to a CSS class name if it's not present (eg: id='one' would be '.one').
    /// </summary>
    /// <param name="cclass" type="String">
    /// </param>
    /// <returns type="String"></returns>
    return Open.Core.Css._prependSelectorPrefix(cclass, '.');
}
Open.Core.Css._prependSelectorPrefix = function Open_Core_Css$_prependSelectorPrefix(value, prefix) {
    /// <param name="value" type="String">
    /// </param>
    /// <param name="prefix" type="String">
    /// </param>
    /// <returns type="String"></returns>
    if (String.isNullOrEmpty(value)) {
        return value;
    }
    return (value.substr(0, 1) === prefix) ? value : prefix + value;
}
Open.Core.Css.addOrRemoveClass = function Open_Core_Css$addOrRemoveClass(element, cssClass, add) {
    /// <summary>
    /// Adds or removes a class from the given element.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to add or remove from.
    /// </param>
    /// <param name="cssClass" type="String">
    /// The CSS class name.
    /// </param>
    /// <param name="add" type="Boolean">
    /// Flag indicating whether the class should be added (true) or removed (false).
    /// </param>
    if (add) {
        element.addClass(cssClass);
    }
    else {
        element.removeClass(cssClass);
    }
}
Open.Core.Css.copyClasses = function Open_Core_Css$copyClasses(source, target) {
    /// <summary>
    /// Copies the CSS classes from one element to another.
    /// </summary>
    /// <param name="source" type="jQueryObject">
    /// The source element to copy from.
    /// </param>
    /// <param name="target" type="jQueryObject">
    /// The target element to copy to.
    /// </param>
    var classes = source.attr(Open.Core.Html.classAttr);
    if (String.isNullOrEmpty(classes)) {
        return;
    }
    Open.Core.Css.addClasses(target, classes);
}
Open.Core.Css.addClasses = function Open_Core_Css$addClasses(target, classValue) {
    /// <summary>
    /// Adds a single class, or multiple classs from a space seperated list.
    /// </summary>
    /// <param name="target" type="jQueryObject">
    /// The target element to add to.
    /// </param>
    /// <param name="classValue" type="String">
    /// The class attribute value to apply.
    /// </param>
    if (String.isNullOrEmpty(classValue)) {
        return;
    }
    var $enum1 = ss.IEnumerator.getEnumerator(classValue.split(' '));
    while ($enum1.moveNext()) {
        var className = $enum1.get_current();
        target.addClass(className);
    }
}
Open.Core.Css.insertLink = function Open_Core_Css$insertLink(url) {
    /// <summary>
    /// Inserts a CSS link within the document head (only if the CSS is not already present).
    /// </summary>
    /// <param name="url" type="String">
    /// The URL of the CSS to load.
    /// </param>
    /// <returns type="Boolean"></returns>
    if (Open.Core.Css.isLinked(url)) {
        return false;
    }
    $(Open.Core.Html.head).append(String.format('<link rel=\'Stylesheet\' href=\'{0}\' type=\'text/css\' />', url));
    return true;
}
Open.Core.Css.isLinked = function Open_Core_Css$isLinked(url) {
    /// <summary>
    /// Determines whether the specified URL has a link within the page.
    /// </summary>
    /// <param name="url" type="String">
    /// The path to the CSS document to match (not case-sensitive).
    /// </param>
    /// <returns type="Boolean"></returns>
    return Open.Core.Css.getLink(url) != null;
}
Open.Core.Css.getLink = function Open_Core_Css$getLink(url) {
    /// <summary>
    /// Retrieves the CSS <link type="text/css" /> with the given source (src) URL.
    /// </summary>
    /// <param name="url" type="String">
    /// The path to the CSS document to match (not case-sensitive).
    /// </param>
    /// <returns type="Object" domElement="true"></returns>
    url = url.toLowerCase();
    var $enum1 = ss.IEnumerator.getEnumerator($('link[type=\'text/css\']').get());
    while ($enum1.moveNext()) {
        var link = $enum1.get_current();
        var href = link.getAttributeNode(Open.Core.Html.href);
        if (ss.isNullOrUndefined(href)) {
            continue;
        }
        if (url === href.value.toLowerCase()) {
            return link;
        }
    }
    return null;
}
Open.Core.Css.absoluteFill = function Open_Core_Css$absoluteFill(element) {
    /// <summary>
    /// Sets the given element to absolute positioning and sets all edges to 0px.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to update.
    /// </param>
    element.css(Open.Core.Css.position, Open.Core.Css.absolute);
    element.css(Open.Core.Css.left, '0px');
    element.css(Open.Core.Css.top, '0px');
    element.css(Open.Core.Css.right, '0px');
    element.css(Open.Core.Css.bottom, '0px');
}
Open.Core.Css.setOverflow = function Open_Core_Css$setOverflow(element, value) {
    /// <summary>
    /// Sets the overflow style on the given element.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to update.
    /// </param>
    /// <param name="value" type="Open.Core.CssOverflow">
    /// The overflow value.
    /// </param>
    element.css(Open.Core.Css.overflow, Open.Core.CssOverflow.toString(value));
}
Open.Core.Css.applyDropshadow = function Open_Core_Css$applyDropshadow(element, opacity) {
    /// <summary>
    /// Applies a drop shadow.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to update.
    /// </param>
    /// <param name="opacity" type="String">
    /// Opacity of the drop-shadow (005, 010, 020, 030, 040, 050, 060).
    /// </param>
    element.css('background-image', String.format('url(/Open.Core/Images/DropShadow.12.{0}.png)', opacity));
    element.css('background-repeat', 'repeat-x');
    element.css('height', '12px');
}
Open.Core.Css.setVisible = function Open_Core_Css$setVisible(element, isVisible) {
    /// <summary>
    /// Shows or hides the given element.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to effect.
    /// </param>
    /// <param name="isVisible" type="Boolean">
    /// The desired visibility state.
    /// </param>
    element.css(Open.Core.Css.display, (isVisible) ? Open.Core.Css.block : Open.Core.Css.none);
}
Open.Core.Css.setSize = function Open_Core_Css$setSize(element, width, height) {
    /// <summary>
    /// Sets the size of the element.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to effect.
    /// </param>
    /// <param name="width" type="Number" integer="true">
    /// The pixel width of the element.
    /// </param>
    /// <param name="height" type="Number" integer="true">
    /// The pixel height of the element.
    /// </param>
    element.css(Open.Core.Css.width, width + Open.Core.Css.px);
    element.css(Open.Core.Css.height, height + Open.Core.Css.px);
}
Open.Core.Css.setOpacity = function Open_Core_Css$setOpacity(element, percent) {
    /// <summary>
    /// Applies opacity.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to effect.
    /// </param>
    /// <param name="percent" type="Number">
    /// The opacity percentage (0..1).
    /// </param>
    percent = Open.Core.Helper.get_numberDouble().withinBounds(percent, 0, 1);
    element.css(Open.Core.Css.opacity, percent.toString());
}
Open.Core.Css.center = function Open_Core_Css$center(element, within) {
    /// <summary>
    /// Sets the left and top position of an element so it is centered within another element.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to center.
    /// </param>
    /// <param name="within" type="jQueryObject">
    /// The element to center within.
    /// </param>
    Open.Core.Css.centerHorizontally(element, within);
    Open.Core.Css.centerVertically(element, within);
}
Open.Core.Css.centerHorizontally = function Open_Core_Css$centerHorizontally(element, within) {
    /// <summary>
    /// Sets the left position of an element so it is horizontally centered within another element.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to horizontally center.
    /// </param>
    /// <param name="within" type="jQueryObject">
    /// The element to center within.
    /// </param>
    var left = (within.width() / 2) - (element.width() / 2);
    element.css(Open.Core.Css.left, left + 'px');
}
Open.Core.Css.centerVertically = function Open_Core_Css$centerVertically(element, within) {
    /// <summary>
    /// Sets the top position of an element so it is vertically centered within another element.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The element to vertically center.
    /// </param>
    /// <param name="within" type="jQueryObject">
    /// The element to center within.
    /// </param>
    var top = (within.height() / 2) - (element.height() / 2);
    element.css(Open.Core.Css.top, top + 'px');
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.CoreCssClasses

Open.Core.CoreCssClasses = function Open_Core_CoreCssClasses() {
    /// <field name="titleFont" type="String">
    /// </field>
    /// <field name="absoluteFill" type="String">
    /// </field>
}
Open.Core.CoreCssClasses.prototype = {
    titleFont: 'titleFont',
    absoluteFill: 'absoluteFill'
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.CoreCssUrls

Open.Core.CoreCssUrls = function Open_Core_CoreCssUrls() {
    /// <field name="core" type="String">
    /// </field>
    /// <field name="coreLists" type="String">
    /// </field>
    /// <field name="coreControls" type="String">
    /// </field>
    /// <field name="jitHyperTree" type="String">
    /// </field>
    /// <field name="jQueryUi" type="String">
    /// </field>
}
Open.Core.CoreCssUrls.prototype = {
    core: '/Open.Core/Css/Core.css',
    coreLists: '/Open.Core/Css/Core.Lists.css',
    coreControls: '/Open.Core/Css/Core.Controls.css',
    jitHyperTree: '/Open.Core/Css/Jit.Hypertree.css',
    jQueryUi: 'http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css'
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.DomEvents

Open.Core.DomEvents = function Open_Core_DomEvents() {
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
    /// <field name="_propertyBag" type="Object">
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
        $.cookie(this.get_id(), Open.Core.Helper.get_json().serialize(this._propertyBag), { expires: this.get_expires() });
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
        this._propertyBag[key] = value;
    },
    
    get: function Open_Core_Cookie$get(key) {
        /// <summary>
        /// Retrieve the specified value.
        /// </summary>
        /// <param name="key" type="String">
        /// The unique identifier of the property.
        /// </param>
        /// <returns type="Object"></returns>
        return (this.hasValue(key)) ? this._propertyBag[key] : null;
    },
    
    hasValue: function Open_Core_Cookie$hasValue(key) {
        /// <summary>
        /// Determines whether there is a value for the given key.
        /// </summary>
        /// <param name="key" type="String">
        /// The unique identifier of the property.
        /// </param>
        /// <returns type="Boolean"></returns>
        return Object.keyExists(this._propertyBag, key);
    },
    
    _createPropertyBag: function Open_Core_Cookie$_createPropertyBag() {
        var json = this._readCookie();
        this._propertyBag = (String.isNullOrEmpty(json)) ? {} : Open.Core.Helper.get_json().parse(json);
    },
    
    _readCookie: function Open_Core_Cookie$_readCookie() {
        /// <returns type="String"></returns>
        return Type.safeCast($.cookie(this.get_id()), String);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Size

Open.Core.Size = function Open_Core_Size(width, height) {
    /// <summary>
    /// Represents a width and a height.
    /// </summary>
    /// <param name="width" type="Number" integer="true">
    /// The pixel width of the element.
    /// </param>
    /// <param name="height" type="Number" integer="true">
    /// The pixel height of the element.
    /// </param>
    /// <field name="_width" type="Number" integer="true">
    /// </field>
    /// <field name="_height" type="Number" integer="true">
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
        /// <value type="Number" integer="true"></value>
        return this._width;
    },
    
    get_height: function Open_Core_Size$get_height() {
        /// <summary>
        /// Gets or sets the pixel height of the element.
        /// </summary>
        /// <value type="Number" integer="true"></value>
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
    /// <field name="_collectionHelper" type="Open.Core.Helpers.CollectionHelper" static="true">
    /// </field>
    /// <field name="_stringHelper" type="Open.Core.Helpers.StringHelper" static="true">
    /// </field>
    /// <field name="_timeHelper" type="Open.Core.Helpers.TimeHelper" static="true">
    /// </field>
    /// <field name="_scrollHelper" type="Open.Core.Helpers.ScrollHelper" static="true">
    /// </field>
    /// <field name="_jQueryHelper" type="Open.Core.Helpers.JQueryHelper" static="true">
    /// </field>
    /// <field name="_treeHelper" type="Open.Core.Helpers.TreeHelper" static="true">
    /// </field>
    /// <field name="_eventHelper" type="Open.Core.Helpers.EventHelper" static="true">
    /// </field>
    /// <field name="_doubleHelper" type="Open.Core.Helpers.DoubleHelper" static="true">
    /// </field>
    /// <field name="_urlHelper" type="Open.Core.Helpers.UrlHelper" static="true">
    /// </field>
    /// <field name="_exceptionHelper" type="Open.Core.Helpers.ExceptionHelper" static="true">
    /// </field>
    /// <field name="_templateHelper" type="Open.Core.Helpers.TemplateHelper" static="true">
    /// </field>
    /// <field name="_idCounter" type="Number" integer="true" static="true">
    /// </field>
}
Open.Core.Helper.get_delegate = function Open_Core_Helper$get_delegate() {
    /// <summary>
    /// Gets the helper for working with Delegates.
    /// </summary>
    /// <value type="Open.Core.Helpers.DelegateHelper"></value>
    return Open.Core.Helper._delegateHelper || (Open.Core.Helper._delegateHelper = new Open.Core.Helpers.DelegateHelper());
}
Open.Core.Helper.get_json = function Open_Core_Helper$get_json() {
    /// <summary>
    /// Gets the helper for working with Delegates.
    /// </summary>
    /// <value type="Open.Core.Helpers.JsonHelper"></value>
    return Open.Core.Helper._jsonHelper || (Open.Core.Helper._jsonHelper = new Open.Core.Helpers.JsonHelper());
}
Open.Core.Helper.get_reflection = function Open_Core_Helper$get_reflection() {
    /// <summary>
    /// Gets the helper for working with reflection.
    /// </summary>
    /// <value type="Open.Core.Helpers.ReflectionHelper"></value>
    return Open.Core.Helper._reflectionHelper || (Open.Core.Helper._reflectionHelper = new Open.Core.Helpers.ReflectionHelper());
}
Open.Core.Helper.get_scriptLoader = function Open_Core_Helper$get_scriptLoader() {
    /// <summary>
    /// Gets the helper for downloading scripts.
    /// </summary>
    /// <value type="Open.Core.Helpers.ScriptLoadHelper"></value>
    return Open.Core.Helper._scriptLoadHelper || (Open.Core.Helper._scriptLoadHelper = new Open.Core.Helpers.ScriptLoadHelper());
}
Open.Core.Helper.get_collection = function Open_Core_Helper$get_collection() {
    /// <summary>
    /// Gets the helper for working with collections.
    /// </summary>
    /// <value type="Open.Core.Helpers.CollectionHelper"></value>
    return Open.Core.Helper._collectionHelper || (Open.Core.Helper._collectionHelper = new Open.Core.Helpers.CollectionHelper());
}
Open.Core.Helper.get_string = function Open_Core_Helper$get_string() {
    /// <summary>
    /// Gets the helper for working with strings.
    /// </summary>
    /// <value type="Open.Core.Helpers.StringHelper"></value>
    return Open.Core.Helper._stringHelper || (Open.Core.Helper._stringHelper = new Open.Core.Helpers.StringHelper());
}
Open.Core.Helper.get_time = function Open_Core_Helper$get_time() {
    /// <summary>
    /// Gets the helper for working with numbers.
    /// </summary>
    /// <value type="Open.Core.Helpers.TimeHelper"></value>
    return Open.Core.Helper._timeHelper || (Open.Core.Helper._timeHelper = new Open.Core.Helpers.TimeHelper());
}
Open.Core.Helper.get_scroll = function Open_Core_Helper$get_scroll() {
    /// <summary>
    /// Gets the helper for working with scrolling.
    /// </summary>
    /// <value type="Open.Core.Helpers.ScrollHelper"></value>
    return Open.Core.Helper._scrollHelper || (Open.Core.Helper._scrollHelper = new Open.Core.Helpers.ScrollHelper());
}
Open.Core.Helper.get_jQuery = function Open_Core_Helper$get_jQuery() {
    /// <summary>
    /// Gets the helper for working with JQuery.
    /// </summary>
    /// <value type="Open.Core.Helpers.JQueryHelper"></value>
    return Open.Core.Helper._jQueryHelper || (Open.Core.Helper._jQueryHelper = new Open.Core.Helpers.JQueryHelper());
}
Open.Core.Helper.get_tree = function Open_Core_Helper$get_tree() {
    /// <summary>
    /// Gets the helper for working with Tree data-structures.
    /// </summary>
    /// <value type="Open.Core.Helpers.TreeHelper"></value>
    return Open.Core.Helper._treeHelper || (Open.Core.Helper._treeHelper = new Open.Core.Helpers.TreeHelper());
}
Open.Core.Helper.get_event = function Open_Core_Helper$get_event() {
    /// <summary>
    /// Gets the helper for working with events.
    /// </summary>
    /// <value type="Open.Core.Helpers.EventHelper"></value>
    return Open.Core.Helper._eventHelper || (Open.Core.Helper._eventHelper = new Open.Core.Helpers.EventHelper());
}
Open.Core.Helper.get_numberDouble = function Open_Core_Helper$get_numberDouble() {
    /// <summary>
    /// Gets the helper for working with doubles.
    /// </summary>
    /// <value type="Open.Core.Helpers.DoubleHelper"></value>
    return Open.Core.Helper._doubleHelper || (Open.Core.Helper._doubleHelper = new Open.Core.Helpers.DoubleHelper());
}
Open.Core.Helper.get_url = function Open_Core_Helper$get_url() {
    /// <summary>
    /// Gets the helper for working with URLs.
    /// </summary>
    /// <value type="Open.Core.Helpers.UrlHelper"></value>
    return Open.Core.Helper._urlHelper || (Open.Core.Helper._urlHelper = new Open.Core.Helpers.UrlHelper());
}
Open.Core.Helper.get_exception = function Open_Core_Helper$get_exception() {
    /// <summary>
    /// Gets the helper for working with Exceptions.
    /// </summary>
    /// <value type="Open.Core.Helpers.ExceptionHelper"></value>
    return Open.Core.Helper._exceptionHelper || (Open.Core.Helper._exceptionHelper = new Open.Core.Helpers.ExceptionHelper());
}
Open.Core.Helper.get_template = function Open_Core_Helper$get_template() {
    /// <summary>
    /// Gets the helper for working with Templates.
    /// </summary>
    /// <value type="Open.Core.Helpers.TemplateHelper"></value>
    return Open.Core.Helper._templateHelper || (Open.Core.Helper._templateHelper = new Open.Core.Helpers.TemplateHelper());
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
    return String.format('gid{0}', Open.Core.Helper._idCounter);
}
Open.Core.Helper.dispose = function Open_Core_Helper$dispose(obj) {
    /// <summary>
    /// Disposes of the object (if it's not null and is an IDisposable).
    /// </summary>
    /// <param name="obj" type="Object">
    /// The object to dispose of.
    /// </param>
    if (ss.isNullOrUndefined(obj)) {
        return;
    }
    var disposable = Type.safeCast(obj, ss.IDisposable);
    if (disposable != null) {
        disposable.dispose();
    }
}


Type.registerNamespace('Open.Testing.Internal');

////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Internal.ITestHarnessEvents

Open.Testing.Internal.ITestHarnessEvents = function() { 
};
Open.Testing.Internal.ITestHarnessEvents.prototype = {
    add_testClassRegistered : null,
    remove_testClassRegistered : null,
    fireTestClassRegistered : null,
    add_controlAdded : null,
    remove_controlAdded : null,
    fireControlAdded : null,
    add_clearControls : null,
    remove_clearControls : null,
    fireClearControls : null,
    add_updateLayout : null,
    remove_updateLayout : null,
    fireUpdateLayout : null
}
Open.Testing.Internal.ITestHarnessEvents.registerInterface('Open.Testing.Internal.ITestHarnessEvents');


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Internal.TestClassEventArgs

Open.Testing.Internal.TestClassEventArgs = function Open_Testing_Internal_TestClassEventArgs() {
    /// <field name="testClass" type="Type">
    /// </field>
}
Open.Testing.Internal.TestClassEventArgs.prototype = {
    testClass: null
}


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.Internal.TestControlEventArgs

Open.Testing.Internal.TestControlEventArgs = function Open_Testing_Internal_TestControlEventArgs() {
    /// <field name="controlDisplayMode" type="Open.Testing.ControlDisplayMode">
    /// </field>
    /// <field name="htmlElement" type="jQueryObject">
    /// </field>
    /// <field name="control" type="Open.Core.IView">
    /// </field>
}
Open.Testing.Internal.TestControlEventArgs.prototype = {
    controlDisplayMode: 0,
    htmlElement: null,
    control: null
}


Type.registerNamespace('Open.Testing');

////////////////////////////////////////////////////////////////////////////////
// Open.Testing.ControlDisplayMode

Open.Testing.ControlDisplayMode = function() { 
    /// <summary>
    /// Flags representing the various different sizing strategies for a hosted control.
    /// </summary>
    /// <field name="none" type="Number" integer="true" static="true">
    /// No sizing or positioning is applied to the control.
    /// </field>
    /// <field name="center" type="Number" integer="true" static="true">
    /// The size is determined by the control, which is centered within the canvas.
    /// </field>
    /// <field name="fill" type="Number" integer="true" static="true">
    /// The control is sized to fill the host container.
    /// </field>
    /// <field name="fillWithMargin" type="Number" integer="true" static="true">
    /// The control is sized to fill the host container but is surrounded by some whitespace.
    /// </field>
};
Open.Testing.ControlDisplayMode.prototype = {
    none: 0, 
    center: 1, 
    fill: 2, 
    fillWithMargin: 3
}
Open.Testing.ControlDisplayMode.registerEnum('Open.Testing.ControlDisplayMode', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Testing.TestHarness

Open.Testing.TestHarness = function Open_Testing_TestHarness() {
    /// <summary>
    /// Shared functionality for working with the TestHarness
    /// (so that test assemblies don't have to reference to TestHarness project [and corresponding dependences]).
    /// </summary>
    /// <field name="_events" type="Open.Testing.Internal.ITestHarnessEvents" static="true">
    /// </field>
    /// <field name="_displayMode" type="Open.Testing.ControlDisplayMode" static="true">
    /// </field>
    /// <field name="_canScroll" type="Boolean" static="true">
    /// </field>
}
Open.Testing.TestHarness.get__events = function Open_Testing_TestHarness$get__events() {
    /// <value type="Open.Testing.Internal.ITestHarnessEvents"></value>
    return Open.Testing.TestHarness._events || (Open.Testing.TestHarness._events = Type.safeCast(Open.Core.DiContainer.get_defaultContainer().getSingleton(Open.Testing.Internal.ITestHarnessEvents), Open.Testing.Internal.ITestHarnessEvents));
}
Open.Testing.TestHarness.get_displayMode = function Open_Testing_TestHarness$get_displayMode() {
    /// <summary>
    /// Gets or sets the size strategy for displaying added controls/HTML.
    /// </summary>
    /// <value type="Open.Testing.ControlDisplayMode"></value>
    return Open.Testing.TestHarness._displayMode;
}
Open.Testing.TestHarness.set_displayMode = function Open_Testing_TestHarness$set_displayMode(value) {
    /// <summary>
    /// Gets or sets the size strategy for displaying added controls/HTML.
    /// </summary>
    /// <value type="Open.Testing.ControlDisplayMode"></value>
    Open.Testing.TestHarness._displayMode = value;
    return value;
}
Open.Testing.TestHarness.get_canScroll = function Open_Testing_TestHarness$get_canScroll() {
    /// <summary>
    /// Gets or sets whether the control host canvas can scroll.
    /// </summary>
    /// <value type="Boolean"></value>
    return Open.Testing.TestHarness._canScroll;
}
Open.Testing.TestHarness.set_canScroll = function Open_Testing_TestHarness$set_canScroll(value) {
    /// <summary>
    /// Gets or sets whether the control host canvas can scroll.
    /// </summary>
    /// <value type="Boolean"></value>
    Open.Testing.TestHarness._canScroll = value;
    Open.Testing.TestHarness.get__events().fireUpdateLayout();
    return value;
}
Open.Testing.TestHarness.registerClass = function Open_Testing_TestHarness$registerClass(testClass) {
    /// <summary>
    /// Registers a test-class with the harness.
    /// </summary>
    /// <param name="testClass" type="Type">
    /// The type of the test class.
    /// </param>
    if (ss.isNullOrUndefined(testClass)) {
        return;
    }
    if (Open.Testing.TestHarness.get__events() == null) {
        return;
    }
    var e = new Open.Testing.Internal.TestClassEventArgs();
    e.testClass = testClass;
    Open.Testing.TestHarness.get__events().fireTestClassRegistered(e);
}
Open.Testing.TestHarness.addControl = function Open_Testing_TestHarness$addControl(control) {
    /// <summary>
    /// Adds a visual control to the host canvas.
    /// </summary>
    /// <param name="control" type="Open.Core.IView">
    /// The control to add.
    /// </param>
    if (control == null) {
        throw new Error('A visual control was not specified.');
    }
    Open.Testing.TestHarness._fireControlAdded(control, control.get_container());
}
Open.Testing.TestHarness.addHtml = function Open_Testing_TestHarness$addHtml(element) {
    /// <summary>
    /// Adds an HTML element to the host canvas.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// The HTML content of the control.
    /// </param>
    if (element == null) {
        throw new Error('An HTML element was not specified.');
    }
    Open.Testing.TestHarness._fireControlAdded(null, element);
}
Open.Testing.TestHarness.reset = function Open_Testing_TestHarness$reset() {
    /// <summary>
    /// Clears the controls from the host canvas and resets to orginal state.
    /// </summary>
    Open.Testing.TestHarness._displayMode = Open.Testing.ControlDisplayMode.center;
    Open.Testing.TestHarness.get__events().fireClearControls();
    Open.Testing.TestHarness.set_canScroll(true);
}
Open.Testing.TestHarness.updateLayout = function Open_Testing_TestHarness$updateLayout() {
    /// <summary>
    /// Forces the display canvas to run it's layout routine.
    /// </summary>
    Open.Testing.TestHarness.get__events().fireUpdateLayout();
}
Open.Testing.TestHarness._fireControlAdded = function Open_Testing_TestHarness$_fireControlAdded(control, element) {
    /// <param name="control" type="Open.Core.IView">
    /// </param>
    /// <param name="element" type="jQueryObject">
    /// </param>
    var e = new Open.Testing.Internal.TestControlEventArgs();
    e.control = control;
    e.htmlElement = element;
    e.controlDisplayMode = Open.Testing.TestHarness.get_displayMode();
    Open.Testing.TestHarness.get__events().fireControlAdded(e);
}


Type.registerNamespace('Open.Core.Helpers');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.ScriptLibrary

Open.Core.Helpers.ScriptLibrary = function() { 
    /// <summary>
    /// The various Core script libraries.
    /// </summary>
    /// <field name="core" type="Number" integer="true" static="true">
    /// The root Core library.
    /// </field>
    /// <field name="controls" type="Number" integer="true" static="true">
    /// The general set of UI controls.
    /// </field>
    /// <field name="lists" type="Number" integer="true" static="true">
    /// The List controls.
    /// </field>
    /// <field name="jQuery" type="Number" integer="true" static="true">
    /// Core jQuery library
    /// </field>
    /// <field name="jQueryUi" type="Number" integer="true" static="true">
    /// The jQuery UI library.
    /// </field>
    /// <field name="jQueryCookie" type="Number" integer="true" static="true">
    /// The jQuery Cookie plugin.
    /// </field>
};
Open.Core.Helpers.ScriptLibrary.prototype = {
    core: 0, 
    controls: 1, 
    lists: 2, 
    jQuery: 3, 
    jQueryUi: 4, 
    jQueryCookie: 5
}
Open.Core.Helpers.ScriptLibrary.registerEnum('Open.Core.Helpers.ScriptLibrary', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.CollectionHelper

Open.Core.Helpers.CollectionHelper = function Open_Core_Helpers_CollectionHelper() {
    /// <summary>
    /// Utility methods for working with collections.
    /// </summary>
}
Open.Core.Helpers.CollectionHelper.prototype = {
    
    toArrayList: function Open_Core_Helpers_CollectionHelper$toArrayList(collection) {
        /// <summary>
        /// Converts an enumerable to an ArrayList.
        /// </summary>
        /// <param name="collection" type="ss.IEnumerable">
        /// The collection to convert.
        /// </param>
        /// <returns type="Array"></returns>
        var list = [];
        if (ss.isNullOrUndefined(collection)) {
            return list;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(collection);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            list.add(item);
        }
        return list;
    },
    
    filter: function Open_Core_Helpers_CollectionHelper$filter(collection, predicate) {
        /// <summary>
        /// Constructs a subset of the collection based on the response of an include-filter.
        /// </summary>
        /// <param name="collection" type="ss.IEnumerable">
        /// The collection to filter.
        /// </param>
        /// <param name="predicate" type="Open.Core.FuncBool">
        /// The predicate to match.
        /// </param>
        /// <returns type="Array"></returns>
        var list = [];
        var $enum1 = ss.IEnumerator.getEnumerator(collection);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            if (predicate.invoke(item)) {
                list.add(item);
            }
        }
        return list;
    },
    
    first: function Open_Core_Helpers_CollectionHelper$first(collection, predicate) {
        /// <summary>
        /// Retrieves the first item that matches the given filter (or null if there is no match).
        /// </summary>
        /// <param name="collection" type="ss.IEnumerable">
        /// The collection to examine.
        /// </param>
        /// <param name="predicate" type="Open.Core.FuncBool">
        /// The predicate to match.
        /// </param>
        /// <returns type="Object"></returns>
        var $enum1 = ss.IEnumerator.getEnumerator(collection);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            if (predicate.invoke(item)) {
                return item;
            }
        }
        return null;
    },
    
    total: function Open_Core_Helpers_CollectionHelper$total(collection, predicate) {
        /// <summary>
        /// Gets the total number of items that match the given predicate.
        /// </summary>
        /// <param name="collection" type="ss.IEnumerable">
        /// The collection to examine.
        /// </param>
        /// <param name="predicate" type="Open.Core.FuncBool">
        /// The predicate to match.
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        if (collection == null) {
            return 0;
        }
        var count = 0;
        var $enum1 = ss.IEnumerator.getEnumerator(collection);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            if (predicate.invoke(item)) {
                count++;
            }
        }
        return count;
    },
    
    disposeAndClear: function Open_Core_Helpers_CollectionHelper$disposeAndClear(collection) {
        /// <summary>
        /// Clears the collection, disposing of all disposable children.
        /// </summary>
        /// <param name="collection" type="Array">
        /// The collection to clear and dispose.
        /// </param>
        if (collection == null) {
            return;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(collection);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            var disposable = Type.safeCast(item, ss.IDisposable);
            if (disposable != null) {
                disposable.dispose();
            }
        }
        collection.clear();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.TemplateHelper

Open.Core.Helpers.TemplateHelper = function Open_Core_Helpers_TemplateHelper() {
    /// <summary>
    /// Utility methods for working with jQuery Templates.
    /// </summary>
    /// <field name="_downloadedUrls" type="Array">
    /// </field>
    /// <field name="_templates" type="Array">
    /// </field>
    this._downloadedUrls = [];
    this._templates = [];
}
Open.Core.Helpers.TemplateHelper.prototype = {
    
    get: function Open_Core_Helpers_TemplateHelper$get(url, selector, callback) {
        /// <summary>
        /// Retrieves a Template instance (only downloading it if required).
        /// </summary>
        /// <param name="url" type="String">
        /// The URL that contains the template.
        /// </param>
        /// <param name="selector" type="String">
        /// The CSS selector for the script block containing the template HTML.
        /// </param>
        /// <param name="callback" type="Open.Core.Helpers.TemplateCallback">
        /// Callback to return the template in.
        /// </param>
        if (callback == null) {
            return;
        }
        var template = Type.safeCast(Open.Core.Helper.get_collection().first(this._templates, ss.Delegate.create(this, function(o) {
            return (o).get_selector() === selector;
        })), Open.Core.Template);
        if (template != null) {
            callback.invoke(template);
            return;
        }
        this.download(url, ss.Delegate.create(this, function() {
            template = new Open.Core.Template(selector);
            this._templates.add(template);
            callback.invoke(template);
        }));
    },
    
    isDownloaded: function Open_Core_Helpers_TemplateHelper$isDownloaded(url) {
        /// <summary>
        /// Determines whether the specified URL has been downloaded.
        /// </summary>
        /// <param name="url" type="String">
        /// The URL of the template(s) to check.
        /// </param>
        /// <returns type="Boolean"></returns>
        return this._downloadedUrls.contains(url.toLowerCase());
    },
    
    download: function Open_Core_Helpers_TemplateHelper$download(url, onComplete) {
        /// <summary>
        /// Downloads the template(s) at the specified URL and appends them to the body.
        /// </summary>
        /// <param name="url" type="String">
        /// The URL of the template(s) to download.
        /// </param>
        /// <param name="onComplete" type="Action">
        /// Callback to invoke upon completion
        /// </param>
        if (this.isDownloaded(url)) {
            Open.Core.Helper.invokeOrDefault(onComplete);
            return;
        }
        this._downloadedUrls.add(url.toLowerCase());
        $.get(url, ss.Delegate.create(this, function(data) {
            $(Open.Core.Html.body).append(data.toString());
            Open.Core.Helper.invokeOrDefault(onComplete);
        }));
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.ExceptionHelper

Open.Core.Helpers.ExceptionHelper = function Open_Core_Helpers_ExceptionHelper() {
    /// <summary>
    /// Utility methods for working with Exceptions.
    /// </summary>
}
Open.Core.Helpers.ExceptionHelper.prototype = {
    
    notSupported: function Open_Core_Helpers_ExceptionHelper$notSupported(value) {
        /// <summary>
        /// Produces an exception for a value that is not supported.
        /// </summary>
        /// <param name="value" type="Object">
        /// The value that is not supported.
        /// </param>
        /// <returns type="Error"></returns>
        return new Error(String.format('Not supported [{0}].', Open.Core.Helper.get_string().formatToString(value)));
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.DoubleHelper

Open.Core.Helpers.DoubleHelper = function Open_Core_Helpers_DoubleHelper() {
    /// <summary>
    /// Utility methods for working with doubles.
    /// </summary>
}
Open.Core.Helpers.DoubleHelper.prototype = {
    
    withinBounds: function Open_Core_Helpers_DoubleHelper$withinBounds(value, min, max) {
        /// <summary>
        /// Ensures a value is within the given bounds.
        /// </summary>
        /// <param name="value" type="Number">
        /// The number to examine.
        /// </param>
        /// <param name="min" type="Number">
        /// The minimum value.
        /// </param>
        /// <param name="max" type="Number">
        /// The maximum value.
        /// </param>
        /// <returns type="Number"></returns>
        if (value < min) {
            value = min;
        }
        if (value > max) {
            value = max;
        }
        return value;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.EventHelper

Open.Core.Helpers.EventHelper = function Open_Core_Helpers_EventHelper() {
    /// <summary>
    /// Utility methods for working with events.
    /// </summary>
}
Open.Core.Helpers.EventHelper.prototype = {
    
    fireClick: function Open_Core_Helpers_EventHelper$fireClick(source) {
        /// <summary>
        /// Fires the click event from the source object (if it exposes a parameterless 'FireClick' method).
        /// </summary>
        /// <param name="source" type="Object">
        /// The source object to fire the event.
        /// </param>
        /// <returns type="Boolean"></returns>
        var obj = Type.safeCast(source, Object);
        if (obj == null) {
            return false;
        }
        var func = Open.Core.Helper.get_reflection().getFunction(source, 'FireClick');
        if (func == null) {
            return false;
        }
        func.call(source);
        return true;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.TreeHelper

Open.Core.Helpers.TreeHelper = function Open_Core_Helpers_TreeHelper() {
    /// <summary>
    /// Utility methods for working with Tree data-structures.
    /// </summary>
}
Open.Core.Helpers.TreeHelper.prototype = {
    
    firstRemainingParent: function Open_Core_Helpers_TreeHelper$firstRemainingParent(root, orphan) {
        /// <summary>
        /// Retrieves the first ancestor of a node that still exists as a descendent of the root node.
        /// </summary>
        /// <param name="root" type="Open.Core.ITreeNode">
        /// The root node.
        /// </param>
        /// <param name="orphan" type="Open.Core.ITreeNode">
        /// The orphaned node.
        /// </param>
        /// <returns type="Open.Core.ITreeNode"></returns>
        if (root == null || orphan == null) {
            return null;
        }
        var parent = orphan.get_parent();
        do {
            if (parent == null) {
                break;
            }
            if (parent === root || root.containsDescendent(parent)) {
                return parent;
            }
            parent = parent.get_parent();
        } while (parent != null);
        return null;
    },
    
    firstSelectedChild: function Open_Core_Helpers_TreeHelper$firstSelectedChild(parent) {
        /// <summary>
        /// Retrieves the first selected child node.
        /// </summary>
        /// <param name="parent" type="Open.Core.ITreeNode">
        /// The node to look within.
        /// </param>
        /// <returns type="Open.Core.ITreeNode"></returns>
        if (parent == null) {
            return null;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(parent.get_children());
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            if (child.get_isSelected()) {
                return child;
            }
        }
        return null;
    },
    
    hasSelectedChild: function Open_Core_Helpers_TreeHelper$hasSelectedChild(parent) {
        /// <summary>
        /// Determines whether at least one of the children of the given node are selected.
        /// </summary>
        /// <param name="parent" type="Open.Core.ITreeNode">
        /// The parent to examine.
        /// </param>
        /// <returns type="Boolean"></returns>
        return this.firstSelectedChild(parent) != null;
    },
    
    deselectChildren: function Open_Core_Helpers_TreeHelper$deselectChildren(parent) {
        /// <summary>
        /// Deselects all children of the given node.
        /// </summary>
        /// <param name="parent" type="Open.Core.ITreeNode">
        /// The node to deselect the children of.
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        if (parent == null) {
            return 0;
        }
        var total = 0;
        var $enum1 = ss.IEnumerator.getEnumerator(parent.get_children());
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            if (child.get_isSelected()) {
                child.set_isSelected(false);
                total++;
            }
        }
        return total;
    },
    
    firstDescendent: function Open_Core_Helpers_TreeHelper$firstDescendent(parent, predicate) {
        /// <summary>
        /// Gets the first descendent node that matches the given predicate.
        /// </summary>
        /// <param name="parent" type="Open.Core.ITreeNode">
        /// The parent to look within.
        /// </param>
        /// <param name="predicate" type="Open.Core.FuncBool">
        /// The predicate used to match.
        /// </param>
        /// <returns type="Open.Core.ITreeNode"></returns>
        if (parent == null || predicate == null) {
            return null;
        }
        var item = Open.Core.Helper.get_collection().first(parent.get_children(), predicate);
        if (item != null) {
            return Type.safeCast(item, Open.Core.ITreeNode);
        }
        var $enum1 = ss.IEnumerator.getEnumerator(parent.get_children());
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            var descendent = this.firstDescendent(child, predicate);
            if (descendent != null) {
                return descendent;
            }
        }
        return null;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.JQueryHelper

Open.Core.Helpers.JQueryHelper = function Open_Core_Helpers_JQueryHelper() {
    /// <summary>
    /// Utility methods for working with JQuery.
    /// </summary>
}
Open.Core.Helpers.JQueryHelper.prototype = {
    
    isKey: function Open_Core_Helpers_JQueryHelper$isKey(e, keyCode) {
        /// <summary>
        /// Determines whether the specified code matches the given event.
        /// </summary>
        /// <param name="e" type="jQueryEvent">
        /// The jQuery event.
        /// </param>
        /// <param name="keyCode" type="Open.Core.Key">
        /// The code to match.
        /// </param>
        /// <returns type="Boolean"></returns>
        return e.which == keyCode;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.ScrollHelper

Open.Core.Helpers.ScrollHelper = function Open_Core_Helpers_ScrollHelper() {
    /// <summary>
    /// Utility methods for scrolling.
    /// </summary>
}
Open.Core.Helpers.ScrollHelper.prototype = {
    
    toBottom: function Open_Core_Helpers_ScrollHelper$toBottom(container, duration, easing, onComplete) {
        /// <summary>
        /// Scrolls to the specified element.
        /// </summary>
        /// <param name="container" type="jQueryObject">
        /// The element to scroll to.
        /// </param>
        /// <param name="duration" type="Number">
        /// The duration of the scroll animation (in seconds).
        /// </param>
        /// <param name="easing" type="EffectEasing">
        /// The easing effect to apply.
        /// </param>
        /// <param name="onComplete" type="Action">
        /// Action to invoke on complete.
        /// </param>
        var props = {};
        props[Open.Core.Html.scrollTop] = container.attr(Open.Core.Html.scrollHeight);
        container.animate(props, Open.Core.Helper.get_time().toMsecs(duration), easing, ss.Delegate.create(this, function() {
            Open.Core.Helper.invokeOrDefault(onComplete);
        }));
    }
}


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
        return Type.safeCast($.toJSON( value ), String);
    },
    
    parse: function Open_Core_Helpers_JsonHelper$parse(json) {
        /// <summary>
        /// Parses the given JSON into an object.
        /// </summary>
        /// <param name="json" type="String">
        /// The JSON to parse.
        /// </param>
        /// <returns type="Object"></returns>
        return Type.safeCast(jQuery.parseJSON( json ), Object);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.TimeHelper

Open.Core.Helpers.TimeHelper = function Open_Core_Helpers_TimeHelper() {
    /// <summary>
    /// Utility methods for working with time.
    /// </summary>
}
Open.Core.Helpers.TimeHelper.prototype = {
    
    toMsecs: function Open_Core_Helpers_TimeHelper$toMsecs(secs) {
        /// <summary>
        /// Converts seconds to milliseconds.
        /// </summary>
        /// <param name="secs" type="Number">
        /// The value to convert.
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return Math.truncate((secs * 1000));
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
    },
    
    hasProperty: function Open_Core_Helpers_ReflectionHelper$hasProperty(instance, propertyName) {
        /// <summary>
        /// Determines whether the specified property exists on the object.
        /// </summary>
        /// <param name="instance" type="Object">
        /// The object to examine.
        /// </param>
        /// <param name="propertyName" type="String">
        /// The name of the property.
        /// </param>
        /// <returns type="Boolean"></returns>
        if (ss.isNullOrUndefined(instance)) {
            return false;
        }
        propertyName = 'get_' + Open.Core.Helper.get_string().toCamelCase(propertyName);
        var $dict1 = instance;
        for (var $key2 in $dict1) {
            var item = { key: $key2, value: $dict1[$key2] };
            if (item.key === propertyName) {
                return true;
            }
        }
        return false;
    },
    
    getFunction: function Open_Core_Helpers_ReflectionHelper$getFunction(source, name) {
        /// <summary>
        /// Retrieves the named function from the specified object.
        /// </summary>
        /// <param name="source" type="Object">
        /// The source object containing the function.
        /// </param>
        /// <param name="name" type="String">
        /// The name of the function.
        /// </param>
        /// <returns type="Function"></returns>
        var obj = Type.safeCast(source, Object);
        if (obj == null) {
            return null;
        }
        name = Open.Core.Helper.get_string().toCamelCase(name);
        var func = Type.safeCast(obj[name], Function);
        if (!ss.isNullOrUndefined(func)) {
            return func;
        }
        func = Type.safeCast(obj['_' + name], Function);
        if (!ss.isNullOrUndefined(func)) {
            return func;
        }
        return null;
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
// Open.Core.Helpers.StringHelper

Open.Core.Helpers.StringHelper = function Open_Core_Helpers_StringHelper() {
    /// <summary>
    /// Utility methods for working with strings.
    /// </summary>
}
Open.Core.Helpers.StringHelper.prototype = {
    
    toCamelCase: function Open_Core_Helpers_StringHelper$toCamelCase(value) {
        /// <summary>
        /// Converts the given string to camelCase.
        /// </summary>
        /// <param name="value" type="String">
        /// The value to convert.
        /// </param>
        /// <returns type="String"></returns>
        if (ss.isUndefined(value)) {
            return value;
        }
        if (String.isNullOrEmpty(value)) {
            return value;
        }
        return value.substr(0, 1).toLowerCase() + value.substring(1, value.length);
    },
    
    toSentenceCase: function Open_Core_Helpers_StringHelper$toSentenceCase(value) {
        /// <summary>
        /// Converts the given string to SentenceCase.
        /// </summary>
        /// <param name="value" type="String">
        /// The value to convert.
        /// </param>
        /// <returns type="String"></returns>
        if (ss.isUndefined(value)) {
            return value;
        }
        if (String.isNullOrEmpty(value)) {
            return value;
        }
        return value.substr(0, 1).toUpperCase() + value.substring(1, value.length);
    },
    
    removeStart: function Open_Core_Helpers_StringHelper$removeStart(text, remove) {
        /// <summary>
        /// Removes the specified text from the start of a string if it's present (not case sensitive).
        /// </summary>
        /// <param name="text" type="String">
        /// The string to effect.
        /// </param>
        /// <param name="remove" type="String">
        /// The text to remove.
        /// </param>
        /// <returns type="String"></returns>
        if (String.isNullOrEmpty(text) || String.isNullOrEmpty(remove)) {
            return text;
        }
        if (!text.toLowerCase().startsWith(remove.toLowerCase())) {
            return text;
        }
        return text.substr(remove.length, text.length - remove.length);
    },
    
    removeEnd: function Open_Core_Helpers_StringHelper$removeEnd(text, remove) {
        /// <summary>
        /// Removes the specified text from the end of a string if it's present (not case sensitive).
        /// </summary>
        /// <param name="text" type="String">
        /// The string to effect.
        /// </param>
        /// <param name="remove" type="String">
        /// The text to remove.
        /// </param>
        /// <returns type="String"></returns>
        if (String.isNullOrEmpty(text) || String.isNullOrEmpty(remove)) {
            return text;
        }
        if (!text.toLowerCase().endsWith(remove.toLowerCase())) {
            return text;
        }
        return text.substr(0, text.length - remove.length);
    },
    
    stripPath: function Open_Core_Helpers_StringHelper$stripPath(url) {
        /// <summary>
        /// Removes the preceeding path of a URL returning just the end segment.
        /// </summary>
        /// <param name="url" type="String">
        /// The URL to process.
        /// </param>
        /// <returns type="String"></returns>
        if (String.isNullOrEmpty(url)) {
            return url;
        }
        var parts = url.split('/');
        return (parts.length === 0) ? url : parts[parts.length - 1];
    },
    
    formatToString: function Open_Core_Helpers_StringHelper$formatToString(value, toString) {
        /// <summary>
        /// Converts the given object to a string, formatting appropirate null/undefined/empty-string
        /// versions if the variable is in any of those states.
        /// </summary>
        /// <param name="value" type="Object">
        /// The value to convert.
        /// </param>
        /// <param name="toString" type="Open.Core.ToString">
        /// Function that performs the conversion to a string.
        /// </param>
        /// <returns type="String"></returns>
        if (ss.isUndefined(value)) {
            return '<undefined>'.htmlEncode();
        }
        if (value == null || ss.isNull(value)) {
            return '<null>'.htmlEncode();
        }
        var text = (toString == null) ? value.toString() : toString.invoke(value);
        if (String.isNullOrEmpty(text)) {
            text = '<empty-string>';
        }
        return text.htmlEncode();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.UrlHelper

Open.Core.Helpers.UrlHelper = function Open_Core_Helpers_UrlHelper() {
    /// <summary>
    /// Utility methods for working with URL's.
    /// </summary>
}
Open.Core.Helpers.UrlHelper.prototype = {
    
    prependDomain: function Open_Core_Helpers_UrlHelper$prependDomain(urlPath) {
        /// <summary>
        /// Prepends the server name to the given url.
        /// </summary>
        /// <param name="urlPath" type="String">
        /// The URL path to prepend.
        /// </param>
        /// <returns type="String"></returns>
        if (ss.isNullOrUndefined(urlPath) || String.isNullOrEmpty(urlPath)) {
            return urlPath;
        }
        if (urlPath.startsWith('http')) {
            return urlPath;
        }
        urlPath = Open.Core.Helper.get_string().removeStart(urlPath, '/');
        var url = window.location;
        return String.format('{0}//{1}/{2}', url.protocol, url.host, urlPath);
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
    /// <field name="_listUrls" type="Array">
    /// </field>
    /// <field name="_listLoaders" type="Array">
    /// </field>
    this._listUrls = [];
    this._listLoaders = [];
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
        if (this._loadedCallbackTotal < this._listUrls.length) {
            return false;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(this._listLoaders);
        while ($enum1.moveNext()) {
            var loader = $enum1.get_current();
            if (!loader.get_isLoaded()) {
                return false;
            }
        }
        return true;
    },
    
    getEnumerator: function Open_Core_Helpers_ResourceLoader$getEnumerator() {
        /// <summary>
        /// Retrieves the enumerator for the collection of URL's.
        /// </summary>
        /// <returns type="ss.IEnumerator"></returns>
        return this._listUrls.getEnumerator();
    },
    
    addUrl: function Open_Core_Helpers_ResourceLoader$addUrl(urls, delimiter) {
        /// <summary>
        /// Splits a set of URL's from a string and adds each one to the list to download.
        /// </summary>
        /// <param name="urls" type="String">
        /// The string of delimited URLs.
        /// </param>
        /// <param name="delimiter" type="String">
        /// The delimiter.
        /// </param>
        if (String.isNullOrEmpty(urls)) {
            return;
        }
        if (ss.isNullOrUndefined(delimiter)) {
            this._listUrls.add(urls);
        }
        else {
            var $enum1 = ss.IEnumerator.getEnumerator(urls.split(delimiter));
            while ($enum1.moveNext()) {
                var item = $enum1.get_current();
                var url = item.trim();
                if (!String.isNullOrEmpty(url)) {
                    this._listUrls.add(url);
                }
            }
        }
    },
    
    addLoader: function Open_Core_Helpers_ResourceLoader$addLoader(loader) {
        /// <summary>
        /// Add a new loader.
        /// </summary>
        /// <param name="loader" type="Open.Core.Helpers.ResourceLoader">
        /// The loader to add.
        /// </param>
        this._listLoaders.add(loader);
    },
    
    start: function Open_Core_Helpers_ResourceLoader$start() {
        /// <summary>
        /// Start the download process.
        /// </summary>
        var $enum1 = ss.IEnumerator.getEnumerator(this._listUrls);
        while ($enum1.moveNext()) {
            var url = $enum1.get_current();
            this.loadResource(url, ss.Delegate.create(this, function() {
                this._loadedCallbackTotal++;
                this._onDownloaded();
            }));
        }
        var $enum2 = ss.IEnumerator.getEnumerator(this._listLoaders);
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
    /// <field name="propIsListsLoaded" type="String" static="true">
    /// </field>
    /// <field name="propIsViewsLoaded" type="String" static="true">
    /// </field>
    /// <field name="_rootScriptFolder$1" type="String">
    /// </field>
    /// <field name="_useDebug$1" type="Boolean">
    /// </field>
    /// <field name="_jit$1" type="Open.Core.Helpers.JitScriptLoader">
    /// </field>
    /// <field name="_loadedLibraries$1" type="Array">
    /// </field>
    /// <field name="_scripts$1" type="Open.Core.Helpers.ScriptNames">
    /// </field>
    this._loadedLibraries$1 = [];
    Open.Core.Helpers.ScriptLoadHelper.initializeBase(this);
}
Open.Core.Helpers.ScriptLoadHelper.prototype = {
    _rootScriptFolder$1: '/Open.Core/Scripts/',
    _useDebug$1: false,
    _jit$1: null,
    _scripts$1: null,
    
    get_useDebug: function Open_Core_Helpers_ScriptLoadHelper$get_useDebug() {
        /// <summary>
        /// Gets or sets whether the debug version of scripts should be used.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._useDebug$1;
    },
    set_useDebug: function Open_Core_Helpers_ScriptLoadHelper$set_useDebug(value) {
        /// <summary>
        /// Gets or sets whether the debug version of scripts should be used.
        /// </summary>
        /// <value type="Boolean"></value>
        this._useDebug$1 = value;
        return value;
    },
    
    get_rootScriptFolder: function Open_Core_Helpers_ScriptLoadHelper$get_rootScriptFolder() {
        /// <summary>
        /// Gets or sets the root folder where the script libraries are housed.
        /// </summary>
        /// <value type="String"></value>
        return this._rootScriptFolder$1;
    },
    set_rootScriptFolder: function Open_Core_Helpers_ScriptLoadHelper$set_rootScriptFolder(value) {
        /// <summary>
        /// Gets or sets the root folder where the script libraries are housed.
        /// </summary>
        /// <value type="String"></value>
        this._rootScriptFolder$1 = value;
        return value;
    },
    
    get_jit: function Open_Core_Helpers_ScriptLoadHelper$get_jit() {
        /// <summary>
        /// Gets the JIT (visualization library) loader.
        /// </summary>
        /// <value type="Open.Core.Helpers.JitScriptLoader"></value>
        return this._jit$1 || (this._jit$1 = new Open.Core.Helpers.JitScriptLoader());
    },
    
    get_scripts: function Open_Core_Helpers_ScriptLoadHelper$get_scripts() {
        /// <summary>
        /// An index of script names.
        /// </summary>
        /// <value type="Open.Core.Helpers.ScriptNames"></value>
        return this._scripts$1 || (this._scripts$1 = new Open.Core.Helpers.ScriptNames());
    },
    
    isLoaded: function Open_Core_Helpers_ScriptLoadHelper$isLoaded(library) {
        /// <summary>
        /// Determines whether the specified library has been loaded.
        /// </summary>
        /// <param name="library" type="Open.Core.Helpers.ScriptLibrary">
        /// The library to look for.
        /// </param>
        /// <returns type="Boolean"></returns>
        return this._loadedLibraries$1.contains(library);
    },
    
    loadLibrary: function Open_Core_Helpers_ScriptLoadHelper$loadLibrary(library, callback) {
        /// <summary>
        /// Loads the specified library.
        /// </summary>
        /// <param name="library" type="Open.Core.Helpers.ScriptLibrary">
        /// Flag indicating the library to load.
        /// </param>
        /// <param name="callback" type="Action">
        /// Callback to invoke upon completion.
        /// </param>
        if (this.isLoaded(library)) {
            Open.Core.Helper.invokeOrDefault(callback);
            return;
        }
        var loader = new Open.Core.Helpers.ScriptLoader();
        loader.add_loadComplete(ss.Delegate.create(this, function() {
            this._loadedLibraries$1.add(library);
            Open.Core.Helper.invokeOrDefault(callback);
        }));
        loader.addUrl(this.get_scripts().url(library));
        loader.start();
    },
    
    _url: function Open_Core_Helpers_ScriptLoadHelper$_url(path, fileName, debugVersion) {
        /// <summary>
        /// Retrieves the URL of a script.
        /// </summary>
        /// <param name="path" type="String">
        /// Sub path to the URL (null if in root Scripts folder).
        /// </param>
        /// <param name="fileName" type="String">
        /// The script file name.
        /// </param>
        /// <param name="debugVersion" type="Boolean">
        /// Flag indicating if the Debug version should be used.
        /// </param>
        /// <returns type="String"></returns>
        return String.format(this.get_rootScriptFolder() + path + this._fileName(fileName, debugVersion));
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
// Open.Core.Helpers.ScriptNames

Open.Core.Helpers.ScriptNames = function Open_Core_Helpers_ScriptNames() {
    /// <summary>
    /// Index of script names.
    /// </summary>
    /// <field name="core" type="String" static="true">
    /// </field>
    /// <field name="coreControls" type="String" static="true">
    /// </field>
    /// <field name="coreLists" type="String" static="true">
    /// </field>
    /// <field name="libraryJit" type="String" static="true">
    /// </field>
    /// <field name="jQuery" type="String" static="true">
    /// </field>
    /// <field name="jQueryUi" type="String" static="true">
    /// </field>
    /// <field name="jQueryCookie" type="String" static="true">
    /// </field>
}
Open.Core.Helpers.ScriptNames.prototype = {
    
    toName: function Open_Core_Helpers_ScriptNames$toName(library) {
        /// <summary>
        /// Retrives the of the file corresponding to the given library.
        /// </summary>
        /// <param name="library" type="Open.Core.Helpers.ScriptLibrary">
        /// Flag for the library to retreive.
        /// </param>
        /// <returns type="String"></returns>
        switch (library) {
            case Open.Core.Helpers.ScriptLibrary.core:
                return Open.Core.Helpers.ScriptNames.core;
            case Open.Core.Helpers.ScriptLibrary.controls:
                return Open.Core.Helpers.ScriptNames.coreControls;
            case Open.Core.Helpers.ScriptLibrary.lists:
                return Open.Core.Helpers.ScriptNames.coreLists;
            case Open.Core.Helpers.ScriptLibrary.jQuery:
                return Open.Core.Helpers.ScriptNames.jQuery;
            case Open.Core.Helpers.ScriptLibrary.jQueryUi:
                return Open.Core.Helpers.ScriptNames.jQueryUi;
            case Open.Core.Helpers.ScriptLibrary.jQueryCookie:
                return Open.Core.Helpers.ScriptNames.jQueryCookie;
            default:
                throw new Error(String.format('{0} not supported.', Open.Core.Helpers.ScriptLibrary.toString(library)));
        }
    },
    
    url: function Open_Core_Helpers_ScriptNames$url(library) {
        /// <summary>
        /// Get the URL for the given library.
        /// </summary>
        /// <param name="library" type="Open.Core.Helpers.ScriptLibrary">
        /// Flag for the library to retreive.
        /// </param>
        /// <returns type="String"></returns>
        var useDebug = Open.Core.Helper.get_scriptLoader().get_useDebug();
        var path = String.Empty;
        switch (library) {
            case Open.Core.Helpers.ScriptLibrary.jQuery:
            case Open.Core.Helpers.ScriptLibrary.jQueryUi:
            case Open.Core.Helpers.ScriptLibrary.jQueryCookie:
                path = 'JQuery/';
                useDebug = false;
                break;
        }
        return Open.Core.Helper.get_scriptLoader()._url(path, this.toName(library), useDebug);
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
    /// <field name="_minWidth$1" type="Number" integer="true">
    /// </field>
    /// <field name="_maxWidthMargin$1" type="Number" integer="true">
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
        /// <value type="Number" integer="true"></value>
        return this._minWidth$1;
    },
    set_minWidth: function Open_Core_UI_HorizontalPanelResizer$set_minWidth(value) {
        /// <summary>
        /// Gets or sets the minimum width the panel can be.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        if (value === this._minWidth$1) {
            return;
        }
        this._minWidth$1 = value;
        this._setMinWidth$1();
        return value;
    },
    
    get_maxWidth: function Open_Core_UI_HorizontalPanelResizer$get_maxWidth() {
        /// <summary>
        /// Gets the maximum width the panel can be.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return (this.get_hasRootContainer()) ? this.get__rootContainerWidth$1() - this.get_maxWidthMargin() : -1;
    },
    
    get_maxWidthMargin: function Open_Core_UI_HorizontalPanelResizer$get_maxWidthMargin() {
        /// <summary>
        /// Gets or sets the margin buffer used to calculate the max-width of the panel relative to the root container.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._maxWidthMargin$1;
    },
    set_maxWidthMargin: function Open_Core_UI_HorizontalPanelResizer$set_maxWidthMargin(value) {
        /// <summary>
        /// Gets or sets the margin buffer used to calculate the max-width of the panel relative to the root container.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        this._maxWidthMargin$1 = value;
        return value;
    },
    
    get__rootContainerWidth$1: function Open_Core_UI_HorizontalPanelResizer$get__rootContainerWidth$1() {
        /// <value type="Number" integer="true"></value>
        return (this.get_hasRootContainer()) ? this.getRootContainer().width() : -1;
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
    
    onWindowResize: function Open_Core_UI_HorizontalPanelResizer$onWindowResize() {
        if (!this.isInitialized) {
            return;
        }
        this._setMinMaxWidth$1();
        if (this.get_hasRootContainer()) {
            this.shrinkIfOverflowing(this.getCurrentSize(), this.get_minWidth(), this.get_maxWidth(), Open.Core.Css.width);
        }
    },
    
    getCurrentSize: function Open_Core_UI_HorizontalPanelResizer$getCurrentSize() {
        /// <returns type="Number" integer="true"></returns>
        return this.get_panel().width();
    },
    
    setCurrentSize: function Open_Core_UI_HorizontalPanelResizer$setCurrentSize(size) {
        /// <param name="size" type="Number" integer="true">
        /// </param>
        this.get_panel().css(Open.Core.Css.width, size + Open.Core.Css.px);
    },
    
    fireResized: function Open_Core_UI_HorizontalPanelResizer$fireResized() {
        Open.Core.UI.HorizontalPanelResizer.callBaseMethod(this, 'fireResized');
        Open.Core.GlobalEvents._fireHorizontalPanelResized(this);
    },
    
    _setMinMaxWidth$1: function Open_Core_UI_HorizontalPanelResizer$_setMinMaxWidth$1() {
        this._setMinWidth$1();
        this._setMaxWidth$1();
    },
    
    _setMinWidth$1: function Open_Core_UI_HorizontalPanelResizer$_setMinWidth$1() {
        this.setResizeOption('minWidth', this.get_minWidth().toString());
    },
    
    _setMaxWidth$1: function Open_Core_UI_HorizontalPanelResizer$_setMaxWidth$1() {
        var width = (this.get_hasRootContainer()) ? this.get_maxWidth().toString() : String.Empty;
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
    Open.Core.GlobalEvents.add_windowResize(ss.Delegate.create(this, function() {
        this.onWindowResize();
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
        Open.Core.GlobalEvents._firePanelResized(this);
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
    
    save: function Open_Core_UI_PanelResizerBase$save() {
        /// <summary>
        /// Persists the current size to storage.
        /// </summary>
        if (!this.get_isSaving()) {
            return;
        }
        Open.Core.UI.PanelResizerBase._cookie.set(this._cookieKey, this.getCurrentSize());
        Open.Core.UI.PanelResizerBase._cookie.save();
    },
    
    onInitialize: function Open_Core_UI_PanelResizerBase$onInitialize() {
    },
    
    onStarted: function Open_Core_UI_PanelResizerBase$onStarted() {
    },
    
    onResize: function Open_Core_UI_PanelResizerBase$onResize() {
    },
    
    onStopped: function Open_Core_UI_PanelResizerBase$onStopped() {
    },
    
    onWindowResize: function Open_Core_UI_PanelResizerBase$onWindowResize() {
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
        /// <param name="currentValue" type="Number" integer="true">
        /// </param>
        /// <param name="minValue" type="Number" integer="true">
        /// </param>
        /// <param name="maxValue" type="Number" integer="true">
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
            this.save();
            this._fireResizeStopped();
        }
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
    /// <field name="_minHeight$1" type="Number" integer="true">
    /// </field>
    /// <field name="_maxHeightMargin$1" type="Number" integer="true">
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
        /// <value type="Number" integer="true"></value>
        return this._minHeight$1;
    },
    set_minHeight: function Open_Core_UI_VerticalPanelResizer$set_minHeight(value) {
        /// <summary>
        /// Gets or sets the minimum height the panel can be.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        if (value === this._minHeight$1) {
            return;
        }
        this._minHeight$1 = value;
        this._setMinHeight$1();
        return value;
    },
    
    get_maxHeight: function Open_Core_UI_VerticalPanelResizer$get_maxHeight() {
        /// <summary>
        /// Gets the maximum height the panel can be.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return (this.get_hasRootContainer()) ? this.get__rootContainerHeight$1() - this.get_maxHeightMargin() : -1;
    },
    
    get_maxHeightMargin: function Open_Core_UI_VerticalPanelResizer$get_maxHeightMargin() {
        /// <summary>
        /// Gets or sets the margin buffer used to calculate the max-height of the panel relative to the root container.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._maxHeightMargin$1;
    },
    set_maxHeightMargin: function Open_Core_UI_VerticalPanelResizer$set_maxHeightMargin(value) {
        /// <summary>
        /// Gets or sets the margin buffer used to calculate the max-height of the panel relative to the root container.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        this._maxHeightMargin$1 = value;
        return value;
    },
    
    get__rootContainerHeight$1: function Open_Core_UI_VerticalPanelResizer$get__rootContainerHeight$1() {
        /// <value type="Number" integer="true"></value>
        return (this.get_hasRootContainer()) ? this.getRootContainer().height() : -1;
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
    
    onWindowResize: function Open_Core_UI_VerticalPanelResizer$onWindowResize() {
        if (!this.isInitialized) {
            return;
        }
        this._setMinMaxHeight$1();
        if (this.get_hasRootContainer()) {
            this.shrinkIfOverflowing(this.getCurrentSize(), this.get_minHeight(), this.get_maxHeight(), Open.Core.Css.height);
        }
    },
    
    getCurrentSize: function Open_Core_UI_VerticalPanelResizer$getCurrentSize() {
        /// <returns type="Number" integer="true"></returns>
        return this.get_panel().height();
    },
    
    setCurrentSize: function Open_Core_UI_VerticalPanelResizer$setCurrentSize(size) {
        /// <param name="size" type="Number" integer="true">
        /// </param>
        this.get_panel().css(Open.Core.Css.height, size + Open.Core.Css.px);
    },
    
    fireResized: function Open_Core_UI_VerticalPanelResizer$fireResized() {
        Open.Core.UI.VerticalPanelResizer.callBaseMethod(this, 'fireResized');
        Open.Core.GlobalEvents._fireVerticalPanelResized(this);
    },
    
    _setMinMaxHeight$1: function Open_Core_UI_VerticalPanelResizer$_setMinMaxHeight$1() {
        this._setMinHeight$1();
        this._setMaxHeight$1();
    },
    
    _setMinHeight$1: function Open_Core_UI_VerticalPanelResizer$_setMinHeight$1() {
        this.setResizeOption('minHeight', this.get_minHeight().toString());
    },
    
    _setMaxHeight$1: function Open_Core_UI_VerticalPanelResizer$_setMaxHeight$1() {
        var height = (this.get_hasRootContainer()) ? this.get_maxHeight().toString() : String.Empty;
        this.setResizeOption('maxHeight', height);
    }
}


Open.Core.ModelBase.registerClass('Open.Core.ModelBase', null, Open.Core.IModel, Open.Core.INotifyPropertyChanged, ss.IDisposable, Open.Core.INotifyDisposed);
Open.Core.ControllerBase.registerClass('Open.Core.ControllerBase', Open.Core.ModelBase);
Open.Core.DiContainer.registerClass('Open.Core.DiContainer', null, ss.IDisposable);
Open.Core._diInstanceWrapper.registerClass('Open.Core._diInstanceWrapper', null, ss.IDisposable);
Open.Core.Template.registerClass('Open.Core.Template');
Open.Core.Should.registerClass('Open.Core.Should');
Open.Core.Keyboard.registerClass('Open.Core.Keyboard');
Open.Core.TreeNode.registerClass('Open.Core.TreeNode', Open.Core.ModelBase, Open.Core.ITreeNode, ss.IDisposable);
Open.Core.ViewBase.registerClass('Open.Core.ViewBase', Open.Core.ModelBase, Open.Core.IView);
Open.Core.TreeNodeEventArgs.registerClass('Open.Core.TreeNodeEventArgs', ss.EventArgs);
Open.Core.PropertyChangedEventArgs.registerClass('Open.Core.PropertyChangedEventArgs', ss.EventArgs);
Open.Core.GlobalEvents.registerClass('Open.Core.GlobalEvents');
Open.Core.Log.registerClass('Open.Core.Log');
Open.Core.LogWriter.registerClass('Open.Core.LogWriter', Open.Core.ModelBase, Open.Core.ILog);
Open.Core.LogCss.registerClass('Open.Core.LogCss');
Open.Core.PropertyBinding.registerClass('Open.Core.PropertyBinding', null, ss.IDisposable);
Open.Core.PropertyDef.registerClass('Open.Core.PropertyDef');
Open.Core.PropertyRef.registerClass('Open.Core.PropertyRef', Open.Core.PropertyDef);
Open.Core.DelayedAction.registerClass('Open.Core.DelayedAction', null, ss.IDisposable);
Open.Core.Color.registerClass('Open.Core.Color');
Open.Core.Url.registerClass('Open.Core.Url');
Open.Core.Html.registerClass('Open.Core.Html');
Open.Core.Css.registerClass('Open.Core.Css');
Open.Core.CoreCssClasses.registerClass('Open.Core.CoreCssClasses');
Open.Core.CoreCssUrls.registerClass('Open.Core.CoreCssUrls');
Open.Core.DomEvents.registerClass('Open.Core.DomEvents');
Open.Core.Cookie.registerClass('Open.Core.Cookie');
Open.Core.Size.registerClass('Open.Core.Size');
Open.Core.Helper.registerClass('Open.Core.Helper');
Open.Testing.Internal.TestClassEventArgs.registerClass('Open.Testing.Internal.TestClassEventArgs');
Open.Testing.Internal.TestControlEventArgs.registerClass('Open.Testing.Internal.TestControlEventArgs');
Open.Testing.TestHarness.registerClass('Open.Testing.TestHarness');
Open.Core.Helpers.CollectionHelper.registerClass('Open.Core.Helpers.CollectionHelper');
Open.Core.Helpers.TemplateHelper.registerClass('Open.Core.Helpers.TemplateHelper');
Open.Core.Helpers.ExceptionHelper.registerClass('Open.Core.Helpers.ExceptionHelper');
Open.Core.Helpers.DoubleHelper.registerClass('Open.Core.Helpers.DoubleHelper');
Open.Core.Helpers.EventHelper.registerClass('Open.Core.Helpers.EventHelper');
Open.Core.Helpers.TreeHelper.registerClass('Open.Core.Helpers.TreeHelper');
Open.Core.Helpers.JQueryHelper.registerClass('Open.Core.Helpers.JQueryHelper');
Open.Core.Helpers.ScrollHelper.registerClass('Open.Core.Helpers.ScrollHelper');
Open.Core.Helpers.JsonHelper.registerClass('Open.Core.Helpers.JsonHelper');
Open.Core.Helpers.TimeHelper.registerClass('Open.Core.Helpers.TimeHelper');
Open.Core.Helpers.ReflectionHelper.registerClass('Open.Core.Helpers.ReflectionHelper');
Open.Core.Helpers.JitScriptLoader.registerClass('Open.Core.Helpers.JitScriptLoader');
Open.Core.Helpers.StringHelper.registerClass('Open.Core.Helpers.StringHelper');
Open.Core.Helpers.UrlHelper.registerClass('Open.Core.Helpers.UrlHelper');
Open.Core.Helpers.ResourceLoader.registerClass('Open.Core.Helpers.ResourceLoader', null, ss.IEnumerable);
Open.Core.Helpers.ScriptLoader.registerClass('Open.Core.Helpers.ScriptLoader', Open.Core.Helpers.ResourceLoader);
Open.Core.Helpers.ScriptLoadHelper.registerClass('Open.Core.Helpers.ScriptLoadHelper', Open.Core.ModelBase);
Open.Core.Helpers.ScriptNames.registerClass('Open.Core.Helpers.ScriptNames');
Open.Core.Helpers.DelegateHelper.registerClass('Open.Core.Helpers.DelegateHelper');
Open.Core.UI.PanelResizerBase.registerClass('Open.Core.UI.PanelResizerBase');
Open.Core.UI.HorizontalPanelResizer.registerClass('Open.Core.UI.HorizontalPanelResizer', Open.Core.UI.PanelResizerBase);
Open.Core.UI.VerticalPanelResizer.registerClass('Open.Core.UI.VerticalPanelResizer', Open.Core.UI.PanelResizerBase);
Open.Core.DiContainer._defaultContainer = null;
Open.Core.Keyboard._isShiftPressed = false;
Open.Core.Keyboard._isCtrlPressed = false;
Open.Core.Keyboard._isAltPressed = false;
(function () {
    var h = Open.Core.Helper.get_jQuery();
    $(document).keydown(function(e) {
        if (h.isKey(e, Open.Core.Key.shift)) {
            Open.Core.Keyboard._isShiftPressed = true;
        }
        if (h.isKey(e, Open.Core.Key.ctrl)) {
            Open.Core.Keyboard._isCtrlPressed = true;
        }
        if (h.isKey(e, Open.Core.Key.alt)) {
            Open.Core.Keyboard._isAltPressed = true;
        }
    });
    $(document).keyup(function(e) {
        if (h.isKey(e, Open.Core.Key.shift)) {
            Open.Core.Keyboard._isShiftPressed = false;
        }
        if (h.isKey(e, Open.Core.Key.ctrl)) {
            Open.Core.Keyboard._isCtrlPressed = false;
        }
        if (h.isKey(e, Open.Core.Key.alt)) {
            Open.Core.Keyboard._isAltPressed = false;
        }
    });
})();
Open.Core.TreeNode.nullIndex = -1;
Open.Core.TreeNode.propIsSelected = 'IsSelected';
Open.Core.TreeNode.propChildren = 'Children';
Open.Core.ViewBase.propBackground = 'Background';
Open.Core.ViewBase.propIsVisible = 'IsVisible';
Open.Core.ViewBase.propOpacity = 'Opacity';
Open.Core.ViewBase.propWidth = 'Width';
Open.Core.ViewBase.propHeight = 'Height';
Open.Core.ViewBase.propIsEnabled = 'IsEnabled';
Open.Core.GlobalEvents.__windowResize = null;
Open.Core.GlobalEvents.__windowResizeComplete = null;
Open.Core.GlobalEvents.__panelResized = null;
Open.Core.GlobalEvents.__panelResizeComplete = null;
Open.Core.GlobalEvents.__horizontalPanelResized = null;
Open.Core.GlobalEvents.__verticalPanelResized = null;
Open.Core.GlobalEvents._resizeDelay = 0.1;
Open.Core.GlobalEvents._windowResizeDelay = null;
Open.Core.GlobalEvents._panelResizeDelay = null;
(function () {
    Open.Core.GlobalEvents._windowResizeDelay = new Open.Core.DelayedAction(Open.Core.GlobalEvents._resizeDelay, Open.Core.GlobalEvents._fireWindowResizeComplete);
    Open.Core.GlobalEvents._panelResizeDelay = new Open.Core.DelayedAction(Open.Core.GlobalEvents._resizeDelay, Open.Core.GlobalEvents._firePanelResizeComplete);
    $(function() {
        $(window).bind(Open.Core.DomEvents.resize, function(e) {
            Open.Core.GlobalEvents._fireWindowResize();
        });
    });
    Open.Core.GlobalEvents.add_windowResize(function() {
        Open.Core.GlobalEvents._windowResizeDelay.start();
    });
    Open.Core.GlobalEvents.add_panelResized(function() {
        Open.Core.GlobalEvents._panelResizeDelay.start();
    });
})();
Open.Core.Log._writer = null;
Open.Core.LogCss.url = '/Open.Core/Css/Core.Controls.css';
Open.Core.LogCss._rootClass = 'c-log';
Open.Core.LogCss.listItemClass = 'c-log-listItem';
Open.Core.LogCss.sectionBreak = 'c-log-sectionBreak';
Open.Core.LogCss.lineBreakClass = 'c-log-lineBreak';
Open.Core.LogCss.counterClass = 'c-log-counter';
Open.Core.LogCss.messageClass = 'c-log-message';
Open.Core.LogCss.list = 'div.' + Open.Core.LogCss._rootClass + '-list';
Open.Core.LogCss.listItem = '.' + Open.Core.LogCss.listItemClass;
Open.Core.PropertyDef._singletons = null;
Open.Core.DelayedAction._nullTimerId = -1;
Open.Core.DelayedAction._isAsyncronous = true;
Open.Core.Color.hotPink = '#ff00f0';
Open.Core.Url.escAnd = '%26';
Open.Core.Html.head = 'head';
Open.Core.Html.body = 'body';
Open.Core.Html.div = 'div';
Open.Core.Html.span = 'span';
Open.Core.Html.img = 'img';
Open.Core.Html.button = 'button';
Open.Core.Html.anchor = 'a';
Open.Core.Html.id = 'id';
Open.Core.Html.href = 'href';
Open.Core.Html.src = 'src';
Open.Core.Html.type = 'type';
Open.Core.Html.value = 'value';
Open.Core.Html.disabled = 'disabled';
Open.Core.Html.classAttr = 'class';
Open.Core.Html.submit = 'submit';
Open.Core.Html.scrollTop = 'scrollTop';
Open.Core.Html.scrollHeight = 'scrollHeight';
Open.Core.Html.click = 'click';
Open.Core.Css.left = 'left';
Open.Core.Css.right = 'right';
Open.Core.Css.top = 'top';
Open.Core.Css.bottom = 'bottom';
Open.Core.Css.width = 'width';
Open.Core.Css.height = 'height';
Open.Core.Css.background = 'background';
Open.Core.Css.color = 'color';
Open.Core.Css.display = 'display';
Open.Core.Css.position = 'position';
Open.Core.Css.padding = 'padding';
Open.Core.Css.margin = 'margin';
Open.Core.Css.overflow = 'overflow';
Open.Core.Css.opacity = 'opacity';
Open.Core.Css.fontSize = 'font-size';
Open.Core.Css.textAlign = 'text-align';
Open.Core.Css.block = 'block';
Open.Core.Css.none = 'none';
Open.Core.Css.relative = 'relative';
Open.Core.Css.absolute = 'absolute';
Open.Core.Css.px = 'px';
Open.Core.Css.classes = new Open.Core.CoreCssClasses();
Open.Core.Css.urls = new Open.Core.CoreCssUrls();
Open.Core.DomEvents.resize = 'resize';
Open.Core.Helper._delegateHelper = null;
Open.Core.Helper._jsonHelper = null;
Open.Core.Helper._reflectionHelper = null;
Open.Core.Helper._scriptLoadHelper = null;
Open.Core.Helper._collectionHelper = null;
Open.Core.Helper._stringHelper = null;
Open.Core.Helper._timeHelper = null;
Open.Core.Helper._scrollHelper = null;
Open.Core.Helper._jQueryHelper = null;
Open.Core.Helper._treeHelper = null;
Open.Core.Helper._eventHelper = null;
Open.Core.Helper._doubleHelper = null;
Open.Core.Helper._urlHelper = null;
Open.Core.Helper._exceptionHelper = null;
Open.Core.Helper._templateHelper = null;
Open.Core.Helper._idCounter = 0;
Open.Testing.TestHarness._events = null;
Open.Testing.TestHarness._displayMode = Open.Testing.ControlDisplayMode.center;
Open.Testing.TestHarness._canScroll = true;
Open.Core.Helpers.JitScriptLoader._jitFolder = 'Jit/';
Open.Core.Helpers.ScriptLoadHelper.propIsListsLoaded = 'IsListsLoaded';
Open.Core.Helpers.ScriptLoadHelper.propIsViewsLoaded = 'IsViewsLoaded';
Open.Core.Helpers.ScriptNames.core = 'Open.Core';
Open.Core.Helpers.ScriptNames.coreControls = 'Open.Core.Controls';
Open.Core.Helpers.ScriptNames.coreLists = 'Open.Core.Lists';
Open.Core.Helpers.ScriptNames.libraryJit = 'Open.Library.Jit';
Open.Core.Helpers.ScriptNames.jQuery = 'jquery-1.4.2.min';
Open.Core.Helpers.ScriptNames.jQueryUi = 'jquery-ui-1.8.4.custom.min';
Open.Core.Helpers.ScriptNames.jQueryCookie = 'jquery.cookie';
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
