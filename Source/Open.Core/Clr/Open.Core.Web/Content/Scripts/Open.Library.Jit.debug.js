//! Open.Library.Jit.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Library.Jit');

////////////////////////////////////////////////////////////////////////////////
// Open.Library.Jit.HypertreeNode

Open.Library.Jit.$create_HypertreeNode = function Open_Library_Jit_HypertreeNode(id, name) {
    var $o = { };
    $o.id = id;
    $o.name = name;
    $o.children = [];
    return $o;
}


////////////////////////////////////////////////////////////////////////////////
// Open.Library.Jit.JitCss

Open.Library.Jit.JitCss = function Open_Library_Jit_JitCss() {
    /// <summary>
    /// Constants for common CSS selectors.
    /// </summary>
    /// <field name="hypertreeUrl" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.Library.Jit.Elements

Open.Library.Jit.Elements = function Open_Library_Jit_Elements() {
    /// <summary>
    /// Constants for element IDs.
    /// </summary>
}


////////////////////////////////////////////////////////////////////////////////
// Open.Library.Jit.Hypertree

Open.Library.Jit.Hypertree = function Open_Library_Jit_Hypertree(containerElement) {
    /// <summary>
    /// Inserts and manages a hypertree.
    /// </summary>
    /// <param name="containerElement" type="jQueryObject">
    /// The the element to inject the tree into.
    /// </param>
    /// <field name="__selectedNodeChanged" type="EventHandler">
    /// </field>
    /// <field name="_hyperTree" type="Object">
    /// </field>
    /// <field name="_containerElement" type="jQueryObject">
    /// </field>
    /// <field name="_isInitialized" type="Boolean">
    /// </field>
    /// <field name="_selectedNode" type="Open.Library.Jit.HypertreeNode">
    /// </field>
    if (containerElement == null) {
        throw new Error('Container element not specified');
    }
    this._containerElement = containerElement;
    if (!Open.Core.Css.isLinked(Open.Library.Jit.JitCss.hypertreeUrl)) {
        Open.Core.Css.insertLink(Open.Library.Jit.JitCss.hypertreeUrl);
    }
    $(window).bind(Open.Core.DomEvents.resize, ss.Delegate.create(this, function(e) {
        this._updateSize();
    }));
}
Open.Library.Jit.Hypertree.prototype = {
    
    add_selectedNodeChanged: function Open_Library_Jit_Hypertree$add_selectedNodeChanged(value) {
        /// <summary>
        /// Fires when the currently selected node changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectedNodeChanged = ss.Delegate.combine(this.__selectedNodeChanged, value);
    },
    remove_selectedNodeChanged: function Open_Library_Jit_Hypertree$remove_selectedNodeChanged(value) {
        /// <summary>
        /// Fires when the currently selected node changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectedNodeChanged = ss.Delegate.remove(this.__selectedNodeChanged, value);
    },
    
    __selectedNodeChanged: null,
    
    _fireSelectedNodeChanged: function Open_Library_Jit_Hypertree$_fireSelectedNodeChanged() {
        if (this.__selectedNodeChanged != null) {
            this.__selectedNodeChanged.invoke(this, new ss.EventArgs());
        }
    },
    
    _hyperTree: null,
    _containerElement: null,
    _isInitialized: false,
    _selectedNode: null,
    
    onNodeClick: function Open_Library_Jit_Hypertree$onNodeClick(node) {
        /// <param name="node" type="Open.Library.Jit.HypertreeNode">
        /// </param>
        this.set_selectedNode(node);
    },
    
    get_selectedNode: function Open_Library_Jit_Hypertree$get_selectedNode() {
        /// <summary>
        /// Gets the currently selected node.
        /// </summary>
        /// <value type="Open.Library.Jit.HypertreeNode"></value>
        return this._selectedNode;
    },
    set_selectedNode: function Open_Library_Jit_Hypertree$set_selectedNode(value) {
        /// <summary>
        /// Gets the currently selected node.
        /// </summary>
        /// <value type="Open.Library.Jit.HypertreeNode"></value>
        if (value === this.get_selectedNode()) {
            return;
        }
        this._selectedNode = value;
        this._fireSelectedNodeChanged();
        return value;
    },
    
    initialize: function Open_Library_Jit_Hypertree$initialize(callback) {
        /// <summary>
        /// Inserts the hyper-tree into the DOM, initializes it and then loads the given node.
        /// </summary>
        /// <param name="callback" type="Action">
        /// Action to invoke upon completion.
        /// </param>
        Open.Core.Helper.get_scriptLoader().get_jit().loadHypertree(ss.Delegate.create(this, function() {
            var containerId = this._containerElement.attr('id');
            this._hyperTree = insertTree(this, containerId);
            this._updateSize();
            this._isInitialized = true;
            Open.Core.Helper.invokeOrDefault(callback);
        }));
    },
    
    load: function Open_Library_Jit_Hypertree$load(rootNode) {
        /// <summary>
        /// Loads the specified root node into the tree.
        /// </summary>
        /// <param name="rootNode" type="Open.Library.Jit.HypertreeNode">
        /// The root node to load.
        /// </param>
        if (!this._isInitialized) {
            throw new Error('HyperTree not initialized');
        }
        this._hyperTree.loadJSON(rootNode);
        this.set_selectedNode(rootNode);
        this.refresh();
    },
    
    refresh: function Open_Library_Jit_Hypertree$refresh() {
        /// <summary>
        /// Refreshes the tree causing all points to be recalculated.
        /// </summary>
        if (!this._isInitialized) {
            return;
        }
        this._hyperTree.refresh();
        this._hyperTree.controller.onAfterCompute();
    },
    
    _updateSize: function Open_Library_Jit_Hypertree$_updateSize() {
        if (!this._isInitialized) {
            return;
        }
        var size = this._getSize();
        this._hyperTree.canvas.resize(size.get_width(), size.get_height());
        this.refresh();
        if (this.get_selectedNode() != null) {
            this._hyperTree.onClick(this.get_selectedNode().id);
        }
    },
    
    _getSize: function Open_Library_Jit_Hypertree$_getSize() {
        /// <returns type="Open.Core.Size"></returns>
        return new Open.Core.Size(this._containerElement.width(), this._containerElement.height());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Library.Jit.HypertreeNodeFactory

Open.Library.Jit.HypertreeNodeFactory = function Open_Library_Jit_HypertreeNodeFactory() {
    /// <field name="propId" type="String" static="true">
    /// </field>
    /// <field name="propName" type="String" static="true">
    /// </field>
    /// <field name="propChildren" type="String" static="true">
    /// </field>
    /// <field name="propData" type="String" static="true">
    /// </field>
}
Open.Library.Jit.HypertreeNodeFactory.create = function Open_Library_Jit_HypertreeNodeFactory$create(json) {
    /// <summary>
    /// Constructs the node from a JSON object.
    /// </summary>
    /// <param name="json" type="Object">
    /// The JSON to construct from.
    /// </param>
    /// <returns type="Open.Library.Jit.HypertreeNode"></returns>
    var node = Open.Library.Jit.$create_HypertreeNode(json[Open.Library.Jit.HypertreeNodeFactory.propId], Type.safeCast(json[Open.Library.Jit.HypertreeNodeFactory.propName], String));
    node.data = json[Open.Library.Jit.HypertreeNodeFactory.propData];
    var children = Type.safeCast(json[Open.Library.Jit.HypertreeNodeFactory.propChildren], Array);
    if (!ss.isNullOrUndefined(children)) {
        var $enum1 = ss.IEnumerator.getEnumerator(children);
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            node.children.add(Open.Library.Jit.HypertreeNodeFactory.create(child));
        }
    }
    return node;
}


Open.Library.Jit.JitCss.registerClass('Open.Library.Jit.JitCss');
Open.Library.Jit.Elements.registerClass('Open.Library.Jit.Elements');
Open.Library.Jit.Hypertree.registerClass('Open.Library.Jit.Hypertree');
Open.Library.Jit.HypertreeNodeFactory.registerClass('Open.Library.Jit.HypertreeNodeFactory');
Open.Library.Jit.JitCss.hypertreeUrl = '/Open.Core/Css/Jit.Hypertree.css';
Open.Library.Jit.HypertreeNodeFactory.propId = 'Id';
Open.Library.Jit.HypertreeNodeFactory.propName = 'Name';
Open.Library.Jit.HypertreeNodeFactory.propChildren = 'Children';
Open.Library.Jit.HypertreeNodeFactory.propData = 'Data';

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Library.Jit', ['Open.Core.Script'], executeScript);
})();
