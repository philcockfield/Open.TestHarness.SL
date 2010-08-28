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
    /// <field name="_rootNode" type="Open.Library.Jit.HypertreeNode">
    /// </field>
    /// <field name="_selectedNode" type="Open.Library.Jit.HypertreeNode">
    /// </field>
    /// <field name="_resizeDelay" type="Open.Core.DelayedAction">
    /// </field>
    /// <field name="_clickedNode" type="Open.Library.Jit.HypertreeNode">
    /// </field>
    /// <field name="_nodeInserter" type="Open.Library.Jit._hypertreeInserter">
    /// </field>
    if (containerElement == null) {
        throw new Error('Container element not specified');
    }
    this._containerElement = containerElement;
    if (!Open.Core.Css.isLinked(Open.Library.Jit.JitCss.hypertreeUrl)) {
        Open.Core.Css.insertLink(Open.Library.Jit.JitCss.hypertreeUrl);
    }
    $(window).bind(Open.Core.DomEvents.resize, ss.Delegate.create(this, function(e) {
        this._onWindowResized();
    }));
}
Open.Library.Jit.Hypertree.findWithin = function Open_Library_Jit_Hypertree$findWithin(id, node) {
    /// <summary>
    /// Looks for a macthing node within the specified node.
    /// </summary>
    /// <param name="id" type="Object">
    /// The id of the node to match.
    /// </param>
    /// <param name="node" type="Open.Library.Jit.HypertreeNode">
    /// The node to look within.
    /// </param>
    /// <returns type="Open.Library.Jit.HypertreeNode"></returns>
    if (node == null || ss.isNullOrUndefined(id)) {
        return null;
    }
    if (id === node.id) {
        return node;
    }
    var $enum1 = ss.IEnumerator.getEnumerator(node.children);
    while ($enum1.moveNext()) {
        var child = $enum1.get_current();
        if (child.id === id) {
            return child;
        }
    }
    var $enum2 = ss.IEnumerator.getEnumerator(node.children);
    while ($enum2.moveNext()) {
        var child = $enum2.get_current();
        var desendent = Open.Library.Jit.Hypertree.findWithin(id, child);
        if (desendent != null) {
            return desendent;
        }
    }
    return null;
}
Open.Library.Jit.Hypertree.mergeChildrenInto = function Open_Library_Jit_Hypertree$mergeChildrenInto(source, target) {
    /// <summary>
    /// Adds child nodes that doen't already exist within the tree.
    /// </summary>
    /// <param name="source" type="Open.Library.Jit.HypertreeNode">
    /// The source node to insert.
    /// </param>
    /// <param name="target" type="Open.Library.Jit.HypertreeNode">
    /// The target node to target node to merge into
    /// </param>
    if (source.id !== target.id) {
        throw new Error('The source and target nodes are not the same.');
    }
    var $enum1 = ss.IEnumerator.getEnumerator(source.children);
    while ($enum1.moveNext()) {
        var sourceChild = $enum1.get_current();
        var targetChild = Open.Library.Jit.Hypertree.getChild(target, sourceChild.id);
        if (targetChild == null) {
            target.children.add(sourceChild);
        }
        else {
            Open.Library.Jit.Hypertree.mergeChildrenInto(sourceChild, targetChild);
        }
    }
}
Open.Library.Jit.Hypertree.getChild = function Open_Library_Jit_Hypertree$getChild(parent, childId) {
    /// <summary>
    /// Determines whether the specified exists within the Children collection.
    /// </summary>
    /// <param name="parent" type="Open.Library.Jit.HypertreeNode">
    /// The parent node to look at.
    /// </param>
    /// <param name="childId" type="Object">
    /// The ID of the child to retrieve
    /// </param>
    /// <returns type="Open.Library.Jit.HypertreeNode"></returns>
    if (ss.isNull(parent)) {
        return null;
    }
    if (ss.isNullOrUndefined(parent.children)) {
        return null;
    }
    var $enum1 = ss.IEnumerator.getEnumerator(parent.children);
    while ($enum1.moveNext()) {
        var child = $enum1.get_current();
        if (child.id === childId) {
            return child;
        }
    }
    return null;
}
Open.Library.Jit.Hypertree.containsChild = function Open_Library_Jit_Hypertree$containsChild(parent, child) {
    /// <summary>
    /// Determines whether the child exists directly within the parent.
    /// </summary>
    /// <param name="parent" type="Open.Library.Jit.HypertreeNode">
    /// The parent node to look at.
    /// </param>
    /// <param name="child" type="Open.Library.Jit.HypertreeNode">
    /// The child to look for.
    /// </param>
    /// <returns type="Boolean"></returns>
    if (ss.isNull(parent) || ss.isNull(child)) {
        return false;
    }
    return Open.Library.Jit.Hypertree.getChild(parent, child.id) != null;
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
    _rootNode: null,
    _selectedNode: null,
    _resizeDelay: null,
    _clickedNode: null,
    _nodeInserter: null,
    
    _onWindowResized: function Open_Library_Jit_Hypertree$_onWindowResized() {
        if (this._resizeDelay == null) {
            this._resizeDelay = new Open.Core.DelayedAction(0.2, ss.Delegate.create(this, function() {
                this._updateSize();
            }));
        }
        this._resizeDelay.start();
    },
    
    onNodeClick: function Open_Library_Jit_Hypertree$onNodeClick(node) {
        /// <param name="node" type="Open.Library.Jit.HypertreeNode">
        /// </param>
        this._clickedNode = node;
    },
    
    onBeforeCompute: function Open_Library_Jit_Hypertree$onBeforeCompute(node) {
        /// <param name="node" type="Open.Library.Jit.HypertreeNode">
        /// </param>
    },
    
    onAfterCompute: function Open_Library_Jit_Hypertree$onAfterCompute() {
        this.set_selectedNode(this._clickedNode);
    },
    
    onAddComplete: function Open_Library_Jit_Hypertree$onAddComplete() {
    },
    
    get_rootNode: function Open_Library_Jit_Hypertree$get_rootNode() {
        /// <summary>
        /// Gets the root node within the tree.
        /// </summary>
        /// <value type="Open.Library.Jit.HypertreeNode"></value>
        return this._rootNode;
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
    
    get__nodeInserter: function Open_Library_Jit_Hypertree$get__nodeInserter() {
        /// <value type="Open.Library.Jit._hypertreeInserter"></value>
        return this._nodeInserter || (this._nodeInserter = new Open.Library.Jit._hypertreeInserter(this));
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
            this._hyperTree = insertHyperTree(this, containerId);
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
        this._rootNode = rootNode;
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
    
    insertChild: function Open_Library_Jit_Hypertree$insertChild(parent, child) {
        /// <summary>
        /// Inserts a node within the tree (see also 'CompleteInsertion').
        /// </summary>
        /// <param name="parent" type="Open.Library.Jit.HypertreeNode">
        /// The parent of the node to add.
        /// </param>
        /// <param name="child" type="Open.Library.Jit.HypertreeNode">
        /// The node to add
        /// </param>
        if (!Open.Library.Jit.Hypertree.containsChild(parent, child)) {
            if (ss.isNullOrUndefined(parent.children)) {
                parent.children = [];
            }
            parent.children.add(child);
        }
        this.get__nodeInserter().add(parent, child);
    },
    
    completeInsertion: function Open_Library_Jit_Hypertree$completeInsertion() {
        /// <summary>
        /// Visually updates the tree after a series of child nodes have been added.
        /// </summary>
        this.get__nodeInserter().updateTree();
        this._nodeInserter = null;
    },
    
    _executeNodeInsertion: function Open_Library_Jit_Hypertree$_executeNodeInsertion(data) {
        /// <param name="data" type="Object">
        /// </param>
        var parameters = {};
        parameters['type'] = 'fade:con';
        parameters['duration'] = 1000;
        parameters['data'] = data;
        addHyperTreeNodes(this, this._hyperTree, parameters);
    },
    
    find: function Open_Library_Jit_Hypertree$find(id) {
        /// <summary>
        /// Looks for a macthing node within the RootNode.
        /// </summary>
        /// <param name="id" type="Object">
        /// The id of the node to match.
        /// </param>
        /// <returns type="Open.Library.Jit.HypertreeNode"></returns>
        return Open.Library.Jit.Hypertree.findWithin(id, this.get_rootNode());
    },
    
    select: function Open_Library_Jit_Hypertree$select(node) {
        /// <summary>
        /// Selects the specified node, centering it on the tree.
        /// </summary>
        /// <param name="node" type="Open.Library.Jit.HypertreeNode">
        /// The node to select.
        /// </param>
        if (node != null) {
            this._hyperTree.onClick(node.id);
        }
        this.set_selectedNode(node);
    },
    
    _updateSize: function Open_Library_Jit_Hypertree$_updateSize() {
        if (!this._isInitialized) {
            return;
        }
        var size = this._getSize();
        this._hyperTree.canvas.resize(size.get_width(), size.get_height());
        this.refresh();
        this.select(this.get_selectedNode());
    },
    
    _getSize: function Open_Library_Jit_Hypertree$_getSize() {
        /// <returns type="Open.Core.Size"></returns>
        return new Open.Core.Size(this._containerElement.width(), this._containerElement.height());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Library.Jit._hypertreeChildInserter

Open.Library.Jit._hypertreeChildInserter = function Open_Library_Jit__hypertreeChildInserter(control, parent) {
    /// <param name="control" type="Open.Library.Jit.Hypertree">
    /// </param>
    /// <param name="parent" type="Open.Library.Jit.HypertreeNode">
    /// </param>
    /// <field name="_control" type="Open.Library.Jit.Hypertree">
    /// </field>
    /// <field name="_parent" type="Open.Library.Jit.HypertreeNode">
    /// </field>
    /// <field name="_children" type="Array">
    /// </field>
    this._children = [];
    if (control == null) {
        throw new Error('Null control');
    }
    if (parent == null) {
        throw new Error('Null parent');
    }
    this._control = control;
    this._parent = parent;
}
Open.Library.Jit._hypertreeChildInserter.prototype = {
    _control: null,
    _parent: null,
    
    get_parent: function Open_Library_Jit__hypertreeChildInserter$get_parent() {
        /// <value type="Open.Library.Jit.HypertreeNode"></value>
        return this._parent;
    },
    
    add: function Open_Library_Jit__hypertreeChildInserter$add(child) {
        /// <param name="child" type="Open.Library.Jit.HypertreeNode">
        /// </param>
        this._children.add(child);
    },
    
    updateTree: function Open_Library_Jit__hypertreeChildInserter$updateTree() {
        this._control._executeNodeInsertion(this.toData());
    },
    
    toData: function Open_Library_Jit__hypertreeChildInserter$toData() {
        /// <returns type="Array"></returns>
        var adjacentIds = [];
        var $enum1 = ss.IEnumerator.getEnumerator(this._children);
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            adjacentIds.add(child.id);
        }
        var dicAdjacencies = {};
        dicAdjacencies['id'] = this.get_parent().id;
        dicAdjacencies['adjacencies'] = adjacentIds;
        var data = [];
        data.add(dicAdjacencies);
        var $enum2 = ss.IEnumerator.getEnumerator(this._children);
        while ($enum2.moveNext()) {
            var child = $enum2.get_current();
            data.add(child);
        }
        return data;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Library.Jit._hypertreeInserter

Open.Library.Jit._hypertreeInserter = function Open_Library_Jit__hypertreeInserter(control) {
    /// <param name="control" type="Open.Library.Jit.Hypertree">
    /// </param>
    /// <field name="inserters" type="Array">
    /// </field>
    /// <field name="_control" type="Open.Library.Jit.Hypertree">
    /// </field>
    this.inserters = [];
    this._control = control;
}
Open.Library.Jit._hypertreeInserter.prototype = {
    _control: null,
    
    add: function Open_Library_Jit__hypertreeInserter$add(parent, child) {
        /// <param name="parent" type="Open.Library.Jit.HypertreeNode">
        /// </param>
        /// <param name="child" type="Open.Library.Jit.HypertreeNode">
        /// </param>
        var relationship = this._getRelationship(parent);
        relationship.add(child);
    },
    
    updateTree: function Open_Library_Jit__hypertreeInserter$updateTree() {
        var $enum1 = ss.IEnumerator.getEnumerator(this.inserters);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            item.updateTree();
        }
    },
    
    _getRelationship: function Open_Library_Jit__hypertreeInserter$_getRelationship(parent) {
        /// <param name="parent" type="Open.Library.Jit.HypertreeNode">
        /// </param>
        /// <returns type="Open.Library.Jit._hypertreeChildInserter"></returns>
        var $enum1 = ss.IEnumerator.getEnumerator(this.inserters);
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            if (item.get_parent().id === parent.id) {
                return item;
            }
        }
        var inserter = new Open.Library.Jit._hypertreeChildInserter(this._control, parent);
        this.inserters.add(inserter);
        return inserter;
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
    if (ss.isNullOrUndefined(json)) {
        throw new Error('[Null] Cannot create from factory. JSON object not provided.');
    }
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
Open.Library.Jit._hypertreeChildInserter.registerClass('Open.Library.Jit._hypertreeChildInserter');
Open.Library.Jit._hypertreeInserter.registerClass('Open.Library.Jit._hypertreeInserter');
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
