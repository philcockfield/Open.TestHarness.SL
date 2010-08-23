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
// Open.Library.Jit.CssSelectors

Open.Library.Jit.CssSelectors = function Open_Library_Jit_CssSelectors() {
    /// <summary>
    /// Constants for common CSS selectors.
    /// </summary>
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
    /// <field name="_hyperTree" type="Object">
    /// </field>
    /// <field name="_containerElement" type="jQueryObject">
    /// </field>
    /// <field name="_isInitialized" type="Boolean">
    /// </field>
    if (containerElement == null) {
        throw new Error('Container element not specified');
    }
    this._containerElement = containerElement;
    $(window).bind(Open.Core.DomEvents.resize, ss.Delegate.create(this, function(e) {
        this._updateSize();
    }));
}
Open.Library.Jit.Hypertree.prototype = {
    _hyperTree: null,
    _containerElement: null,
    _isInitialized: false,
    
    initialize: function Open_Library_Jit_Hypertree$initialize(callback) {
        /// <summary>
        /// Inserts the hyper-tree into the DOM, initializes it and then loads the given node.
        /// </summary>
        /// <param name="callback" type="Action">
        /// Action to invoke upon completion.
        /// </param>
        Open.Core.Helper.get_scriptLoader().get_jit().loadHypertree(ss.Delegate.create(this, function() {
            var containerId = this._containerElement.attr('id');
            this._hyperTree = insertTree(containerId);
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
    },
    
    _getSize: function Open_Library_Jit_Hypertree$_getSize() {
        /// <returns type="Open.Core.Size"></returns>
        return new Open.Core.Size(this._containerElement.width(), this._containerElement.height());
    }
}


Open.Library.Jit.CssSelectors.registerClass('Open.Library.Jit.CssSelectors');
Open.Library.Jit.Elements.registerClass('Open.Library.Jit.Elements');
Open.Library.Jit.Hypertree.registerClass('Open.Library.Jit.Hypertree');

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Library.Jit', ['Open.Core.Script'], executeScript);
})();
