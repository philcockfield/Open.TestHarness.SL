//! Open.Core.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core');

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
    /// Represents a node within a tree structure.
    /// </summary>
};
Open.Core.ITreeNode.prototype = {
    add_selectionChanged : null,
    remove_selectionChanged : null,
    add_childSelectionChanged : null,
    remove_childSelectionChanged : null,
    get_parent : null,
    get_isRoot : null,
    get_isSelected : null,
    set_isSelected : null,
    get_children : null,
    get_totalChildren : null,
    add : null,
    remove : null,
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
// Open.Core.TreeNode

Open.Core.TreeNode = function Open_Core_TreeNode() {
    /// <field name="__selectionChanged$1" type="EventHandler">
    /// </field>
    /// <field name="__childSelectionChanged$1" type="EventHandler">
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
            node.add(childNode);
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
    
    get_isRoot: function Open_Core_TreeNode$get_isRoot() {
        /// <value type="Boolean"></value>
        return this.get_parent() == null;
    },
    
    get_children: function Open_Core_TreeNode$get_children() {
        /// <value type="ss.IEnumerable"></value>
        return this.get__childList$1();
    },
    
    get_totalChildren: function Open_Core_TreeNode$get_totalChildren() {
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
    
    add: function Open_Core_TreeNode$add(node) {
        /// <param name="node" type="Open.Core.ITreeNode">
        /// </param>
        if (this.contains(node)) {
            return;
        }
        this.get__childList$1().add(node);
        node.add_selectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$1));
        if (node.get_parent() !== this) {
            Open.Core.TreeNode._setParent$1(node, this);
        }
    },
    
    remove: function Open_Core_TreeNode$remove(node) {
        /// <param name="node" type="Open.Core.ITreeNode">
        /// </param>
        if (!this.contains(node)) {
            return;
        }
        this.get__childList$1().remove(node);
        node.remove_selectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$1));
        if (node.get_parent() === this) {
            Open.Core.TreeNode._setParent$1(node, null);
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
    /// <field name="_model$1" type="Object">
    /// </field>
    /// <field name="_container$1" type="jQueryObject">
    /// </field>
    Open.Core.ViewBase.initializeBase(this);
}
Open.Core.ViewBase.prototype = {
    _isInitialized$1: false,
    _model$1: null,
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
        this.onInitialize(container);
        this._isInitialized$1 = true;
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
    /// <field name="__changed" type="EventHandler">
    /// </field>
    /// <field name="_instance" type="Object">
    /// </field>
    /// <field name="_observable" type="Open.Core.INotifyPropertyChanged">
    /// </field>
    /// <field name="_name" type="String">
    /// </field>
    /// <field name="_formattedName" type="String">
    /// </field>
    /// <field name="_bindTo" type="Open.Core.PropertyRef">
    /// </field>
    /// <field name="_propertyBinding" type="Open.Core.PropertyBinding">
    /// </field>
    this._instance = instance;
    this._name = name;
    this._observable = Type.safeCast(instance, Open.Core.INotifyPropertyChanged);
    if (this._observable != null) {
        this._observable.add_propertyChanged(ss.Delegate.create(this, this._onPropertyChanged));
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
        this.__changed = ss.Delegate.combine(this.__changed, value);
    },
    remove_changed: function Open_Core_PropertyRef$remove_changed(value) {
        /// <summary>
        /// Fires when the property value changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__changed = ss.Delegate.remove(this.__changed, value);
    },
    
    __changed: null,
    
    _fireChanged: function Open_Core_PropertyRef$_fireChanged() {
        if (this.__changed != null) {
            this.__changed.invoke(this, new ss.EventArgs());
        }
    },
    
    _instance: null,
    _observable: null,
    _name: null,
    _formattedName: null,
    _bindTo: null,
    _propertyBinding: null,
    
    dispose: function Open_Core_PropertyRef$dispose() {
        /// <summary>
        /// Disposes of the object.
        /// </summary>
        if (this._observable != null) {
            this._observable.remove_propertyChanged(ss.Delegate.create(this, this._onPropertyChanged));
        }
        if (this._propertyBinding != null) {
            this._propertyBinding.dispose();
        }
    },
    
    _onPropertyChanged: function Open_Core_PropertyRef$_onPropertyChanged(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Core.PropertyChangedEventArgs">
        /// </param>
        if (e.get_property().get_name() !== this.get_name()) {
            return;
        }
        this._fireChanged();
    },
    
    get_instance: function Open_Core_PropertyRef$get_instance() {
        /// <summary>
        /// Gets the instance of the object that exposes the property.
        /// </summary>
        /// <value type="Object"></value>
        return this._instance;
    },
    
    get_name: function Open_Core_PropertyRef$get_name() {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value type="String"></value>
        return this._name;
    },
    
    get__formattedName: function Open_Core_PropertyRef$get__formattedName() {
        /// <value type="String"></value>
        return this._formattedName || (this._formattedName = Open.Core.Helper.get_string().toCamelCase(this.get_name()));
    },
    
    get_fullName: function Open_Core_PropertyRef$get_fullName() {
        /// <summary>
        /// Gets the fully qualified name of the property..
        /// </summary>
        /// <value type="String"></value>
        return Type.getInstanceType(this.get_instance()).get_fullName() + ':' + this.get_name();
    },
    
    get_value: function Open_Core_PropertyRef$get_value() {
        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        /// <value type="Object"></value>
        return this.get_instance()['get_' + this.get__formattedName()]();
    },
    set_value: function Open_Core_PropertyRef$set_value(value) {
        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        /// <value type="Object"></value>
        this.get_instance()['set_' + this.get__formattedName()](value);
        return value;
    },
    
    get_bindTo: function Open_Core_PropertyRef$get_bindTo() {
        /// <summary>
        /// Gets or sets the source property to bind this property to.
        /// </summary>
        /// <value type="Open.Core.PropertyRef"></value>
        return this._bindTo;
    },
    set_bindTo: function Open_Core_PropertyRef$set_bindTo(value) {
        /// <summary>
        /// Gets or sets the source property to bind this property to.
        /// </summary>
        /// <value type="Open.Core.PropertyRef"></value>
        if (value === this.get_bindTo()) {
            return;
        }
        this._bindTo = value;
        this._propertyBinding = new Open.Core.PropertyBinding(value, this);
        return value;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Html

Open.Core.Html = function Open_Core_Html() {
    /// <summary>
    /// HTML utility.
    /// </summary>
    /// <field name="head" type="String" static="true">
    /// </field>
    /// <field name="div" type="String" static="true">
    /// </field>
    /// <field name="span" type="String" static="true">
    /// </field>
    /// <field name="href" type="String" static="true">
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
    /// <field name="block" type="String" static="true">
    /// </field>
    /// <field name="none" type="String" static="true">
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
Open.Core.Css.insertLink = function Open_Core_Css$insertLink(url) {
    /// <summary>
    /// Inserts a CSS link witin the document head.
    /// </summary>
    /// <param name="url" type="String">
    /// The URL of the CSS to load.
    /// </param>
    $(Open.Core.Html.head).append(String.format('<link rel=\'Stylesheet\' href=\'{0}\' type=\'text/css\' />', url));
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
    element.css('position', 'absolute');
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


////////////////////////////////////////////////////////////////////////////////
// Open.Core.CoreCssClasses

Open.Core.CoreCssClasses = function Open_Core_CoreCssClasses() {
    /// <field name="titleFont" type="String">
    /// </field>
}
Open.Core.CoreCssClasses.prototype = {
    titleFont: 'titleFont'
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


Open.Core.ModelBase.registerClass('Open.Core.ModelBase', null, Open.Core.IModel, Open.Core.INotifyPropertyChanged, ss.IDisposable);
Open.Core.TreeNode.registerClass('Open.Core.TreeNode', Open.Core.ModelBase, Open.Core.ITreeNode, ss.IDisposable);
Open.Core.ViewBase.registerClass('Open.Core.ViewBase', Open.Core.ModelBase, Open.Core.IView);
Open.Core.PropertyChangedEventArgs.registerClass('Open.Core.PropertyChangedEventArgs', ss.EventArgs);
Open.Core.PropertyBinding.registerClass('Open.Core.PropertyBinding', null, ss.IDisposable);
Open.Core.PropertyRef.registerClass('Open.Core.PropertyRef');
Open.Core.Html.registerClass('Open.Core.Html');
Open.Core.Css.registerClass('Open.Core.Css');
Open.Core.CoreCssClasses.registerClass('Open.Core.CoreCssClasses');
Open.Core.DomEvents.registerClass('Open.Core.DomEvents');
Open.Core.Cookie.registerClass('Open.Core.Cookie');
Open.Core.Size.registerClass('Open.Core.Size');
Open.Core.Helper.registerClass('Open.Core.Helper');
Open.Core.Helpers.CollectionHelper.registerClass('Open.Core.Helpers.CollectionHelper');
Open.Core.Helpers.JsonHelper.registerClass('Open.Core.Helpers.JsonHelper');
Open.Core.Helpers.NumberHelper.registerClass('Open.Core.Helpers.NumberHelper');
Open.Core.Helpers.ReflectionHelper.registerClass('Open.Core.Helpers.ReflectionHelper');
Open.Core.Helpers.JitScriptLoader.registerClass('Open.Core.Helpers.JitScriptLoader');
Open.Core.Helpers.StringHelper.registerClass('Open.Core.Helpers.StringHelper');
Open.Core.Helpers.ResourceLoader.registerClass('Open.Core.Helpers.ResourceLoader');
Open.Core.Helpers.ScriptLoader.registerClass('Open.Core.Helpers.ScriptLoader', Open.Core.Helpers.ResourceLoader);
Open.Core.Helpers.ScriptLoadHelper.registerClass('Open.Core.Helpers.ScriptLoadHelper');
Open.Core.Helpers.DelegateHelper.registerClass('Open.Core.Helpers.DelegateHelper');
Open.Core.UI.PanelResizerBase.registerClass('Open.Core.UI.PanelResizerBase');
Open.Core.UI.HorizontalPanelResizer.registerClass('Open.Core.UI.HorizontalPanelResizer', Open.Core.UI.PanelResizerBase);
Open.Core.UI.VerticalPanelResizer.registerClass('Open.Core.UI.VerticalPanelResizer', Open.Core.UI.PanelResizerBase);
Open.Core.TreeNode.propIsSelected = 'IsSelected';
Open.Core.TreeNode.propChildren = 'Children';
Open.Core.Html.head = 'head';
Open.Core.Html.div = 'div';
Open.Core.Html.span = 'span';
Open.Core.Html.href = 'href';
Open.Core.Css.left = 'left';
Open.Core.Css.right = 'right';
Open.Core.Css.top = 'top';
Open.Core.Css.bottom = 'bottom';
Open.Core.Css.width = 'width';
Open.Core.Css.height = 'height';
Open.Core.Css.background = 'background';
Open.Core.Css.display = 'display';
Open.Core.Css.block = 'block';
Open.Core.Css.none = 'none';
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
