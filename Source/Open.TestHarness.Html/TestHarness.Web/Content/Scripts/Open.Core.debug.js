//! Open.Core.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core');

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
// Open.Core.ILog

Open.Core.ILog = function() { 
    /// <summary>
    /// An output log.
    /// </summary>
};
Open.Core.ILog.prototype = {
    write : null,
    lineBreak : null,
    clear : null,
    registerView : null
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
    lineBreak : null,
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
    /// The logical controller for a view (visual UI) contained with an HTML element.
    /// </summary>
};
Open.Core.IView.prototype = {
    get_isDisposed : null,
    get_isInitialized : null,
    initialize : null,
    get_container : null,
    dispose : null
}
Open.Core.IView.registerInterface('Open.Core.IView');


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
    /// <field name="_isDisposed" type="Boolean">
    /// </field>
    /// <field name="__propertyChanged" type="Open.Core.PropertyChangedHandler">
    /// </field>
    /// <field name="_propertyBag" type="Object">
    /// </field>
    /// <field name="_propertRefs" type="Array">
    /// </field>
}
Open.Core.ModelBase.prototype = {
    _isDisposed: false,
    
    add_propertyChanged: function Open_Core_ModelBase$add_propertyChanged(value) {
        /// <param name="value" type="Function" />
        this.__propertyChanged = ss.Delegate.combine(this.__propertyChanged, value);
    },
    remove_propertyChanged: function Open_Core_ModelBase$remove_propertyChanged(value) {
        /// <param name="value" type="Function" />
        this.__propertyChanged = ss.Delegate.remove(this.__propertyChanged, value);
    },
    
    __propertyChanged: null,
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
        if (this._propertRefs != null) {
            var $enum1 = ss.IEnumerator.getEnumerator(this.get__propertyRefs());
            while ($enum1.moveNext()) {
                var propertyRef = $enum1.get_current();
                propertyRef.dispose();
            }
        }
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

Open.Core.ViewBase = function Open_Core_ViewBase() {
    /// <summary>
    /// Base for classes that represent, manage and construct views ("UI").
    /// </summary>
    /// <field name="_isInitialized$1" type="Boolean">
    /// </field>
    /// <field name="_container$1" type="jQueryObject">
    /// </field>
    Open.Core.ViewBase.initializeBase(this);
}
Open.Core.ViewBase.prototype = {
    _isInitialized$1: false,
    _container$1: null,
    
    get_isInitialized: function Open_Core_ViewBase$get_isInitialized() {
        /// <value type="Boolean"></value>
        return this._isInitialized$1;
    },
    
    get_container: function Open_Core_ViewBase$get_container() {
        /// <summary>
        /// Gets the element that the view is contained within.
        /// </summary>
        /// <value type="jQueryObject"></value>
        return this._container$1;
    },
    
    initialize: function Open_Core_ViewBase$initialize(container) {
        /// <param name="container" type="jQueryObject">
        /// </param>
        if (this.get_isInitialized()) {
            throw new Error('View is already initialized.');
        }
        this._container$1 = container;
        this.onInitialize(this._container$1);
        this._isInitialized$1 = true;
    },
    
    onInitialize: function Open_Core_ViewBase$onInitialize(container) {
        /// <summary>
        /// Deriving implementation of Initialize.
        /// </summary>
        /// <param name="container" type="jQueryObject">
        /// </param>
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
    /// <field name="__panelResized" type="EventHandler" static="true">
    /// </field>
    /// <field name="__horizontalPanelResized" type="EventHandler" static="true">
    /// </field>
    /// <field name="__verticalPanelResized" type="EventHandler" static="true">
    /// </field>
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
Open.Core.Log.get__writer = function Open_Core_Log$get__writer() {
    /// <summary>
    /// Gets the specific log-writer instance that the static methods write to.
    /// </summary>
    /// <value type="Open.Core.LogWriter"></value>
    return Open.Core.Log._writer || (Open.Core.Log._writer = new Open.Core.LogWriter());
}
Open.Core.Log.title = function Open_Core_Log$title(message) {
    /// <summary>
    /// Writes a informational message to the log (as a bold title).
    /// </summary>
    /// <param name="message" type="String">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.write(Open.Core.Html.toBold(message), Open.Core.LogSeverity.info);
}
Open.Core.Log.info = function Open_Core_Log$info(message) {
    /// <summary>
    /// Writes a informational message to the log.
    /// </summary>
    /// <param name="message" type="String">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.write(message, Open.Core.LogSeverity.info);
}
Open.Core.Log.debug = function Open_Core_Log$debug(message) {
    /// <summary>
    /// Writes a debug message to the log.
    /// </summary>
    /// <param name="message" type="String">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.write(message, Open.Core.LogSeverity.debug);
}
Open.Core.Log.warning = function Open_Core_Log$warning(message) {
    /// <summary>
    /// Writes a warning to the log.
    /// </summary>
    /// <param name="message" type="String">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.write(message, Open.Core.LogSeverity.warning);
}
Open.Core.Log.error = function Open_Core_Log$error(message) {
    /// <summary>
    /// Writes an error message to the log.
    /// </summary>
    /// <param name="message" type="String">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.write(message, Open.Core.LogSeverity.error);
}
Open.Core.Log.success = function Open_Core_Log$success(message) {
    /// <summary>
    /// Writes a success message to the log.
    /// </summary>
    /// <param name="message" type="String">
    /// The messge to write (HTML).
    /// </param>
    Open.Core.Log.write(message, Open.Core.LogSeverity.success);
}
Open.Core.Log.write = function Open_Core_Log$write(message, severity) {
    /// <summary>
    /// Writes a message to the log.
    /// </summary>
    /// <param name="message" type="String">
    /// The message to write (HTML).
    /// </param>
    /// <param name="severity" type="Open.Core.LogSeverity">
    /// The severity of the message.
    /// </param>
    Open.Core.Log.get__writer().write(message, severity);
}
Open.Core.Log.clear = function Open_Core_Log$clear() {
    /// <summary>
    /// Clears the log.
    /// </summary>
    Open.Core.Log.get__writer().clear();
}
Open.Core.Log.lineBreak = function Open_Core_Log$lineBreak() {
    /// <summary>
    /// Inserts a line break to the log.
    /// </summary>
    Open.Core.Log.get__writer().lineBreak();
}
Open.Core.Log.registerView = function Open_Core_Log$registerView(view) {
    /// <summary>
    /// Registers a log viewer to emit output to (multiple views can be associated with the log).
    /// </summary>
    /// <param name="view" type="Open.Core.ILogView">
    /// The log view to emit to.
    /// </param>
    Open.Core.Log.get__writer().registerView(view);
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.LogWriter

Open.Core.LogWriter = function Open_Core_LogWriter() {
    /// <summary>
    /// An output log.
    /// </summary>
    /// <field name="_views$1" type="Array">
    /// </field>
    this._views$1 = [];
    Open.Core.LogWriter.initializeBase(this);
    Open.Core.Css.insertLink(Open.Core.LogCss.url);
}
Open.Core.LogWriter.prototype = {
    
    onDisposed: function Open_Core_LogWriter$onDisposed() {
        /// <summary>
        /// Destructor.
        /// </summary>
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$1);
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            var disposable = Type.safeCast(view, ss.IDisposable);
            if (disposable != null) {
                disposable.dispose();
            }
        }
        Open.Core.LogWriter.callBaseMethod(this, 'onDisposed');
    },
    
    write: function Open_Core_LogWriter$write(message, severity) {
        /// <param name="message" type="String">
        /// </param>
        /// <param name="severity" type="Open.Core.LogSeverity">
        /// </param>
        var css = Open.Core.LogCss.severityClass(severity);
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$1);
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            view.insert(message, css);
        }
    },
    
    lineBreak: function Open_Core_LogWriter$lineBreak() {
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$1);
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            view.lineBreak();
        }
    },
    
    clear: function Open_Core_LogWriter$clear() {
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$1);
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            view.clear();
        }
    },
    
    registerView: function Open_Core_LogWriter$registerView(view) {
        /// <param name="view" type="Open.Core.ILogView">
        /// </param>
        if (view != null) {
            this._views$1.add(view);
        }
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
            }), Open.Core.Helper.get_number().toMsecs(this.get_delay()));
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
    /// <field name="div" type="String" static="true">
    /// </field>
    /// <field name="span" type="String" static="true">
    /// </field>
    /// <field name="img" type="String" static="true">
    /// </field>
    /// <field name="id" type="String" static="true">
    /// </field>
    /// <field name="href" type="String" static="true">
    /// </field>
    /// <field name="src" type="String" static="true">
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
Open.Core.Html.centerVertically = function Open_Core_Html$centerVertically(element, within) {
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
    /// Formats the an hyperlink.
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
    /// <field name="display" type="String" static="true">
    /// </field>
    /// <field name="position" type="String" static="true">
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
    element.css('overflow', Open.Core.CssOverflow.toString(value));
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
    /// <field name="_collectionHelper" type="Open.Core.Helpers.CollectionHelper" static="true">
    /// </field>
    /// <field name="_stringHelper" type="Open.Core.Helpers.StringHelper" static="true">
    /// </field>
    /// <field name="_numberHelper" type="Open.Core.Helpers.NumberHelper" static="true">
    /// </field>
    /// <field name="_scrollHelper" type="Open.Core.Helpers.ScrollHelper" static="true">
    /// </field>
    /// <field name="_jQueryHelper" type="Open.Core.Helpers.JQueryHelper" static="true">
    /// </field>
    /// <field name="_treeHelper" type="Open.Core.Helpers.TreeHelper" static="true">
    /// </field>
    /// <field name="_eventHelper" type="Open.Core.Helpers.EventHelper" static="true">
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
Open.Core.Helper.get_number = function Open_Core_Helper$get_number() {
    /// <summary>
    /// Gets the helper for working with numbers.
    /// </summary>
    /// <value type="Open.Core.Helpers.NumberHelper"></value>
    return Open.Core.Helper._numberHelper || (Open.Core.Helper._numberHelper = new Open.Core.Helpers.NumberHelper());
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


Type.registerNamespace('Open.Core.Helpers');

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
        /// <param name="predicate" type="Open.Core.IsMatch">
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
        /// <param name="predicate" type="Open.Core.IsMatch">
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
    
    firstSelectedChild: function Open_Core_Helpers_TreeHelper$firstSelectedChild(node) {
        /// <summary>
        /// Retrieves the first selected child node.
        /// </summary>
        /// <param name="node" type="Open.Core.ITreeNode">
        /// The node to look within.
        /// </param>
        /// <returns type="Open.Core.ITreeNode"></returns>
        if (node == null) {
            return null;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(node.get_children());
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            if (child.get_isSelected()) {
                return child;
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
        container.animate(props, Open.Core.Helper.get_number().toMsecs(duration), easing, ss.Delegate.create(this, function() {
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
        return Type.safeCast(JSON.parse( json ), Object);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Helpers.NumberHelper

Open.Core.Helpers.NumberHelper = function Open_Core_Helpers_NumberHelper() {
    /// <summary>
    /// Utility methods for working with numbers.
    /// </summary>
}
Open.Core.Helpers.NumberHelper.prototype = {
    
    toMsecs: function Open_Core_Helpers_NumberHelper$toMsecs(secs) {
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
    /// <field name="_isListsLoaded$1" type="Boolean">
    /// </field>
    /// <field name="_isViewsLoaded$1" type="Boolean">
    /// </field>
    Open.Core.Helpers.ScriptLoadHelper.initializeBase(this);
}
Open.Core.Helpers.ScriptLoadHelper._load$1 = function Open_Core_Helpers_ScriptLoadHelper$_load$1(scriptName, isLoadedProperty, callback) {
    /// <param name="scriptName" type="String">
    /// </param>
    /// <param name="isLoadedProperty" type="Open.Core.PropertyRef">
    /// </param>
    /// <param name="callback" type="Action">
    /// </param>
    if (isLoadedProperty.get_value()) {
        Open.Core.Helper.invokeOrDefault(callback);
        return;
    }
    var loader = new Open.Core.Helpers.ScriptLoader();
    loader.add_loadComplete(function() {
        isLoadedProperty.set_value(true);
        Open.Core.Helper.invokeOrDefault(callback);
    });
    loader.addUrl(Open.Core.Helper.get_scriptLoader()._url(String.Empty, scriptName, true));
    loader.start();
}
Open.Core.Helpers.ScriptLoadHelper.prototype = {
    _rootScriptFolder$1: '/Open.Core/Scripts/',
    _useDebug$1: false,
    _jit$1: null,
    _isListsLoaded$1: false,
    _isViewsLoaded$1: false,
    
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
    
    get_isListsLoaded: function Open_Core_Helpers_ScriptLoadHelper$get_isListsLoaded() {
        /// <summary>
        /// Gets whether the Lists library has been loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isListsLoaded$1;
    },
    set_isListsLoaded: function Open_Core_Helpers_ScriptLoadHelper$set_isListsLoaded(value) {
        /// <summary>
        /// Gets whether the Lists library has been loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        this._isListsLoaded$1 = value;
        return value;
    },
    
    get_isViewsLoaded: function Open_Core_Helpers_ScriptLoadHelper$get_isViewsLoaded() {
        /// <summary>
        /// Gets whether the Views library has been loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isViewsLoaded$1;
    },
    set_isViewsLoaded: function Open_Core_Helpers_ScriptLoadHelper$set_isViewsLoaded(value) {
        /// <summary>
        /// Gets whether the Views library has been loaded.
        /// </summary>
        /// <value type="Boolean"></value>
        this._isViewsLoaded$1 = value;
        return value;
    },
    
    loadViews: function Open_Core_Helpers_ScriptLoadHelper$loadViews(callback) {
        /// <summary>
        /// Loads the Views library.
        /// </summary>
        /// <param name="callback" type="Action">
        /// Callback to invoke upon completion.
        /// </param>
        Open.Core.Helpers.ScriptLoadHelper._load$1('Open.Core.Views', this.getPropertyRef(Open.Core.Helpers.ScriptLoadHelper.propIsViewsLoaded), callback);
    },
    
    loadLists: function Open_Core_Helpers_ScriptLoadHelper$loadLists(callback) {
        /// <summary>
        /// Loads the Lists library.
        /// </summary>
        /// <param name="callback" type="Action">
        /// Callback to invoke upon completion.
        /// </param>
        Open.Core.Helpers.ScriptLoadHelper._load$1('Open.Core.Lists', this.getPropertyRef(Open.Core.Helpers.ScriptLoadHelper.propIsListsLoaded), callback);
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
    $(window).bind(Open.Core.DomEvents.resize, ss.Delegate.create(this, function(e) {
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
        var height = (this.get_hasRootContainer()) ? this.get__maxHeight$1().toString() : String.Empty;
        this.setResizeOption('maxHeight', height);
    }
}


Type.registerNamespace('Open');

////////////////////////////////////////////////////////////////////////////////
// Open.Testing

Open.Testing = function Open_Testing() {
    /// <summary>
    /// Shared functionality for working with the TestHarness
    /// (so that test assemblies don't have to reference to TestHarness project [and corresponding dependences]).
    /// </summary>
}
Open.Testing.registerClass = function Open_Testing$registerClass(testClass) {
    /// <summary>
    /// Registers a test-class with the harness.
    /// </summary>
    /// <param name="testClass" type="Type">
    /// The type of the test class.
    /// </param>
    if (ss.isNullOrUndefined(testClass)) {
        return;
    }
    var e = new Open.TestHarness.TestClassEventArgs();
    e.testClass = testClass;
    Open.TestHarness.TestHarnessEvents._fireTestClassRegistered(e);
}


Type.registerNamespace('Open.TestHarness');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.TestClassEventArgs

Open.TestHarness.TestClassEventArgs = function Open_TestHarness_TestClassEventArgs() {
    /// <field name="testClass" type="Type">
    /// </field>
}
Open.TestHarness.TestClassEventArgs.prototype = {
    testClass: null
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.TestHarnessEvents

Open.TestHarness.TestHarnessEvents = function Open_TestHarness_TestHarnessEvents() {
    /// <field name="__testClassRegistered" type="Open.TestHarness.TestClassHandler" static="true">
    /// </field>
}
Open.TestHarness.TestHarnessEvents.add_testClassRegistered = function Open_TestHarness_TestHarnessEvents$add_testClassRegistered(value) {
    /// <summary>
    /// Fires when a test class is registered.
    /// </summary>
    /// <param name="value" type="Function" />
    Open.TestHarness.TestHarnessEvents.__testClassRegistered = ss.Delegate.combine(Open.TestHarness.TestHarnessEvents.__testClassRegistered, value);
}
Open.TestHarness.TestHarnessEvents.remove_testClassRegistered = function Open_TestHarness_TestHarnessEvents$remove_testClassRegistered(value) {
    /// <summary>
    /// Fires when a test class is registered.
    /// </summary>
    /// <param name="value" type="Function" />
    Open.TestHarness.TestHarnessEvents.__testClassRegistered = ss.Delegate.remove(Open.TestHarness.TestHarnessEvents.__testClassRegistered, value);
}
Open.TestHarness.TestHarnessEvents._fireTestClassRegistered = function Open_TestHarness_TestHarnessEvents$_fireTestClassRegistered(e) {
    /// <param name="e" type="Open.TestHarness.TestClassEventArgs">
    /// </param>
    if (Open.TestHarness.TestHarnessEvents.__testClassRegistered != null) {
        Open.TestHarness.TestHarnessEvents.__testClassRegistered.invoke(Open.TestHarness.TestHarnessEvents, e);
    }
}


Open.Core.ModelBase.registerClass('Open.Core.ModelBase', null, Open.Core.IModel, Open.Core.INotifyPropertyChanged, ss.IDisposable);
Open.Core.ControllerBase.registerClass('Open.Core.ControllerBase', Open.Core.ModelBase);
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
Open.Core.Url.registerClass('Open.Core.Url');
Open.Core.Html.registerClass('Open.Core.Html');
Open.Core.Css.registerClass('Open.Core.Css');
Open.Core.CoreCssClasses.registerClass('Open.Core.CoreCssClasses');
Open.Core.DomEvents.registerClass('Open.Core.DomEvents');
Open.Core.Cookie.registerClass('Open.Core.Cookie');
Open.Core.Size.registerClass('Open.Core.Size');
Open.Core.Helper.registerClass('Open.Core.Helper');
Open.Core.Helpers.CollectionHelper.registerClass('Open.Core.Helpers.CollectionHelper');
Open.Core.Helpers.EventHelper.registerClass('Open.Core.Helpers.EventHelper');
Open.Core.Helpers.TreeHelper.registerClass('Open.Core.Helpers.TreeHelper');
Open.Core.Helpers.JQueryHelper.registerClass('Open.Core.Helpers.JQueryHelper');
Open.Core.Helpers.ScrollHelper.registerClass('Open.Core.Helpers.ScrollHelper');
Open.Core.Helpers.JsonHelper.registerClass('Open.Core.Helpers.JsonHelper');
Open.Core.Helpers.NumberHelper.registerClass('Open.Core.Helpers.NumberHelper');
Open.Core.Helpers.ReflectionHelper.registerClass('Open.Core.Helpers.ReflectionHelper');
Open.Core.Helpers.JitScriptLoader.registerClass('Open.Core.Helpers.JitScriptLoader');
Open.Core.Helpers.StringHelper.registerClass('Open.Core.Helpers.StringHelper');
Open.Core.Helpers.ResourceLoader.registerClass('Open.Core.Helpers.ResourceLoader');
Open.Core.Helpers.ScriptLoader.registerClass('Open.Core.Helpers.ScriptLoader', Open.Core.Helpers.ResourceLoader);
Open.Core.Helpers.ScriptLoadHelper.registerClass('Open.Core.Helpers.ScriptLoadHelper', Open.Core.ModelBase);
Open.Core.Helpers.DelegateHelper.registerClass('Open.Core.Helpers.DelegateHelper');
Open.Core.UI.PanelResizerBase.registerClass('Open.Core.UI.PanelResizerBase');
Open.Core.UI.HorizontalPanelResizer.registerClass('Open.Core.UI.HorizontalPanelResizer', Open.Core.UI.PanelResizerBase);
Open.Core.UI.VerticalPanelResizer.registerClass('Open.Core.UI.VerticalPanelResizer', Open.Core.UI.PanelResizerBase);
Open.Testing.registerClass('Open.Testing');
Open.TestHarness.TestClassEventArgs.registerClass('Open.TestHarness.TestClassEventArgs');
Open.TestHarness.TestHarnessEvents.registerClass('Open.TestHarness.TestHarnessEvents');
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
Open.Core.GlobalEvents.__panelResized = null;
Open.Core.GlobalEvents.__horizontalPanelResized = null;
Open.Core.GlobalEvents.__verticalPanelResized = null;
Open.Core.Log._writer = null;
Open.Core.LogCss.url = '/Open.Core/Css/Core.Controls.css';
Open.Core.LogCss._rootClass = 'c-log';
Open.Core.LogCss.listItemClass = 'c-log-listItem';
Open.Core.LogCss.lineBreakClass = 'c-log-lineBreak';
Open.Core.LogCss.counterClass = 'c-log-counter';
Open.Core.LogCss.messageClass = 'c-log-message';
Open.Core.LogCss.list = 'div.' + Open.Core.LogCss._rootClass + '-list';
Open.Core.LogCss.listItem = '.' + Open.Core.LogCss.listItemClass;
Open.Core.PropertyDef._singletons = null;
Open.Core.DelayedAction._nullTimerId = -1;
Open.Core.DelayedAction._isAsyncronous = true;
Open.Core.Url.escAnd = '%26';
Open.Core.Html.head = 'head';
Open.Core.Html.div = 'div';
Open.Core.Html.span = 'span';
Open.Core.Html.img = 'img';
Open.Core.Html.id = 'id';
Open.Core.Html.href = 'href';
Open.Core.Html.src = 'src';
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
Open.Core.Css.display = 'display';
Open.Core.Css.position = 'position';
Open.Core.Css.block = 'block';
Open.Core.Css.none = 'none';
Open.Core.Css.relative = 'relative';
Open.Core.Css.absolute = 'absolute';
Open.Core.Css.px = 'px';
Open.Core.Css.classes = new Open.Core.CoreCssClasses();
Open.Core.DomEvents.resize = 'resize';
Open.Core.Helper._delegateHelper = null;
Open.Core.Helper._jsonHelper = null;
Open.Core.Helper._reflectionHelper = null;
Open.Core.Helper._scriptLoadHelper = null;
Open.Core.Helper._collectionHelper = null;
Open.Core.Helper._stringHelper = null;
Open.Core.Helper._numberHelper = null;
Open.Core.Helper._scrollHelper = null;
Open.Core.Helper._jQueryHelper = null;
Open.Core.Helper._treeHelper = null;
Open.Core.Helper._eventHelper = null;
Open.Core.Helper._idCounter = 0;
Open.Core.Helpers.JitScriptLoader._jitFolder = 'Jit/';
Open.Core.Helpers.ScriptLoadHelper.propIsListsLoaded = 'IsListsLoaded';
Open.Core.Helpers.ScriptLoadHelper.propIsViewsLoaded = 'IsViewsLoaded';
Open.Core.UI.PanelResizerBase._eventStart = 'start';
Open.Core.UI.PanelResizerBase._eventStop = 'eventStop';
Open.Core.UI.PanelResizerBase._eventResize = 'eventResize';
Open.Core.UI.PanelResizerBase._cookie = null;
Open.Core.UI.PanelResizerBase._resizeScript = '\r\n$(\'{0}\').resizable({\r\n    handles: \'{1}\',\r\n    start: {2},\r\n    stop: {3},\r\n    resize: {4}\r\n    });\r\n';
Open.TestHarness.TestHarnessEvents.__testClassRegistered = null;

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Script', [], executeScript);
})();
