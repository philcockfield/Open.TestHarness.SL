//! Open.Core.Lists.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core.Lists');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.IListItemView

Open.Core.Lists.IListItemView = function() { 
    /// <summary>
    /// Represents a single item within a list.
    /// </summary>
};
Open.Core.Lists.IListItemView.prototype = {
    get_isSelected : null,
    set_isSelected : null,
    get_model : null
}
Open.Core.Lists.IListItemView.registerInterface('Open.Core.Lists.IListItemView');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.IListItem

Open.Core.Lists.IListItem = function() { 
    /// <summary>
    /// An item that resides within a List.
    /// </summary>
};
Open.Core.Lists.IListItem.prototype = {
    get_text : null,
    set_text : null,
    get_canSelect : null,
    set_canSelect : null,
    get_rightIconSrc : null,
    set_rightIconSrc : null
}
Open.Core.Lists.IListItem.registerInterface('Open.Core.Lists.IListItem');


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListSelectionMode

Open.Core.Lists.ListSelectionMode = function() { 
    /// <summary>
    /// Flags indicating how items within a list are selected.
    /// </summary>
    /// <field name="none" type="Number" integer="true" static="true">
    /// Items are not selectable.
    /// </field>
    /// <field name="single" type="Number" integer="true" static="true">
    /// Only one item at a time can be selected.
    /// </field>
};
Open.Core.Lists.ListSelectionMode.prototype = {
    none: 0, 
    single: 1
}
Open.Core.Lists.ListSelectionMode.registerEnum('Open.Core.Lists.ListSelectionMode', false);


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListHtml

Open.Core.Lists.ListHtml = function Open_Core_Lists_ListHtml() {
    /// <summary>
    /// HTML constants.
    /// </summary>
    /// <field name="childPointerIcon" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListCss

Open.Core.Lists.ListCss = function Open_Core_Lists_ListCss() {
    /// <summary>
    /// CSS declarations for lists.
    /// </summary>
    /// <field name="url" type="String" static="true">
    /// </field>
    /// <field name="_isCssInserted" type="Boolean" static="true">
    /// </field>
    /// <field name="itemClasses" type="Open.Core.Lists.ListItemClasses" static="true">
    /// </field>
    /// <field name="classes" type="Open.Core.Lists.ListClasses" static="true">
    /// </field>
}
Open.Core.Lists.ListCss.insertCss = function Open_Core_Lists_ListCss$insertCss() {
    /// <summary>
    /// Inserts the CSS for the 'Open.Core.Lists' library.
    /// </summary>
    if (Open.Core.Lists.ListCss._isCssInserted) {
        return;
    }
    Open.Core.Css.insertLink(Open.Core.Lists.ListCss.url);
    Open.Core.Lists.ListCss._isCssInserted = true;
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListClasses

Open.Core.Lists.ListClasses = function Open_Core_Lists_ListClasses() {
    /// <field name="root" type="String">
    /// </field>
}
Open.Core.Lists.ListClasses.prototype = {
    root: 'c-list'
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListItemClasses

Open.Core.Lists.ListItemClasses = function Open_Core_Lists_ListItemClasses() {
    /// <field name="root" type="String">
    /// </field>
    /// <field name="defaultRoot" type="String">
    /// </field>
    /// <field name="selected" type="String">
    /// </field>
    /// <field name="label" type="String">
    /// </field>
    /// <field name="iconRight" type="String">
    /// </field>
}
Open.Core.Lists.ListItemClasses.prototype = {
    root: 'c-listItem',
    defaultRoot: 'c-listItem-default',
    selected: 'c-listItem-selected',
    label: 'c-listItem-label',
    iconRight: 'c-listItem-iconRight'
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListTreeBackController

Open.Core.Lists.ListTreeBackController = function Open_Core_Lists_ListTreeBackController(listTree, backButton, backMask) {
    /// <summary>
    /// A controller for attaching a 'Back' and 'Home' button to a ListTree.
    /// </summary>
    /// <param name="listTree" type="Open.Core.Lists.ListTreeView">
    /// The list tree under control.
    /// </param>
    /// <param name="backButton" type="jQueryObject">
    /// The back button.
    /// </param>
    /// <param name="backMask" type="jQueryObject">
    /// The mask which causes the back-button to look like a back button.
    /// </param>
    /// <field name="_listTree$2" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_backButton$2" type="jQueryObject">
    /// </field>
    /// <field name="_backMask$2" type="jQueryObject">
    /// </field>
    Open.Core.Lists.ListTreeBackController.initializeBase(this);
    this._listTree$2 = listTree;
    this._backButton$2 = backButton;
    this._backMask$2 = backMask;
    listTree.add_propertyChanged(ss.Delegate.create(this, this._onPropertyChanged$2));
    backButton.click(ss.Delegate.create(this, this._onBackClick$2));
    backMask.click(ss.Delegate.create(this, this._onBackClick$2));
}
Open.Core.Lists.ListTreeBackController.prototype = {
    _listTree$2: null,
    _backButton$2: null,
    _backMask$2: null,
    
    onDisposed: function Open_Core_Lists_ListTreeBackController$onDisposed() {
        this._listTree$2.remove_propertyChanged(ss.Delegate.create(this, this._onPropertyChanged$2));
        this._backButton$2.unbind(Open.Core.Html.click, ss.Delegate.create(this, this._onBackClick$2));
        this._backMask$2.unbind(Open.Core.Html.click, ss.Delegate.create(this, this._onBackClick$2));
        Open.Core.Lists.ListTreeBackController.callBaseMethod(this, 'onDisposed');
    },
    
    _onPropertyChanged$2: function Open_Core_Lists_ListTreeBackController$_onPropertyChanged$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Core.PropertyChangedEventArgs">
        /// </param>
        if (e.get_property().get_name() === Open.Core.Lists.ListTreeView.propSelectedParent) {
            this._fadeBackMask$2();
        }
    },
    
    _onBackClick$2: function Open_Core_Lists_ListTreeBackController$_onBackClick$2(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (Open.Core.Keyboard.get_isAltPressed()) {
            this._listTree$2.home();
        }
        else {
            this._listTree$2.back();
        }
    },
    
    get_listTree: function Open_Core_Lists_ListTreeBackController$get_listTree() {
        /// <summary>
        /// Gets the list-tree which is under control.
        /// </summary>
        /// <value type="Open.Core.Lists.ListTreeView"></value>
        return this._listTree$2;
    },
    
    get_backButton: function Open_Core_Lists_ListTreeBackController$get_backButton() {
        /// <summary>
        /// Gets the 'Back' button.
        /// </summary>
        /// <value type="jQueryObject"></value>
        return this._backButton$2;
    },
    
    get_backMask: function Open_Core_Lists_ListTreeBackController$get_backMask() {
        /// <summary>
        /// Gets the 'Home' button.
        /// </summary>
        /// <value type="jQueryObject"></value>
        return this._backMask$2;
    },
    
    get__showBackMask$2: function Open_Core_Lists_ListTreeBackController$get__showBackMask$2() {
        /// <value type="Boolean"></value>
        var node = this._listTree$2.get_selectedParent();
        if (node == null) {
            return false;
        }
        if (node.get_isRoot()) {
            return false;
        }
        if (node.get_childCount() === 0 && node.get_parent().get_isRoot()) {
            return false;
        }
        return true;
    },
    
    _fadeBackMask$2: function Open_Core_Lists_ListTreeBackController$_fadeBackMask$2() {
        var duration = Open.Core.Helper.get_number().toMsecs(this._listTree$2.get_slideDuration());
        var isVisible = Open.Core.Css.isVisible(this._backMask$2);
        if (this.get__showBackMask$2()) {
            if (!isVisible) {
                this._backMask$2.fadeIn(duration);
            }
        }
        else {
            if (isVisible) {
                this._backMask$2.fadeOut(duration);
            }
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListItem

Open.Core.Lists.ListItem = function Open_Core_Lists_ListItem() {
    /// <summary>
    /// An implementation of a TreeNode for inclusion within the ListTree control.
    /// </summary>
    /// <field name="propText" type="String" static="true">
    /// </field>
    /// <field name="propCanSelect" type="String" static="true">
    /// </field>
    /// <field name="propRightIconSrc" type="String" static="true">
    /// </field>
    Open.Core.Lists.ListItem.initializeBase(this);
}
Open.Core.Lists.ListItem.create = function Open_Core_Lists_ListItem$create(text) {
    /// <summary>
    /// Creates a new tree node (factory).
    /// </summary>
    /// <param name="text" type="String">
    /// The value of the 'Text' property.
    /// </param>
    /// <returns type="Open.Core.Lists.ListItem"></returns>
    var node = new Open.Core.Lists.ListItem();
    node.set_text(text);
    return node;
}
Open.Core.Lists.ListItem.prototype = {
    
    get_text: function Open_Core_Lists_ListItem$get_text() {
        /// <value type="String"></value>
        return this.get(Open.Core.Lists.ListItem.propText, null);
    },
    set_text: function Open_Core_Lists_ListItem$set_text(value) {
        /// <value type="String"></value>
        this.set(Open.Core.Lists.ListItem.propText, value, null);
        return value;
    },
    
    get_canSelect: function Open_Core_Lists_ListItem$get_canSelect() {
        /// <value type="Boolean"></value>
        return this.get(Open.Core.Lists.ListItem.propCanSelect, true);
    },
    set_canSelect: function Open_Core_Lists_ListItem$set_canSelect(value) {
        /// <value type="Boolean"></value>
        this.set(Open.Core.Lists.ListItem.propCanSelect, value, true);
        return value;
    },
    
    get_rightIconSrc: function Open_Core_Lists_ListItem$get_rightIconSrc() {
        /// <value type="String"></value>
        return this.get(Open.Core.Lists.ListItem.propRightIconSrc, null);
    },
    set_rightIconSrc: function Open_Core_Lists_ListItem$set_rightIconSrc(value) {
        /// <value type="String"></value>
        this.set(Open.Core.Lists.ListItem.propRightIconSrc, value, null);
        return value;
    },
    
    createHtml: function Open_Core_Lists_ListItem$createHtml() {
        /// <summary>
        /// Allows deriving classes to provide custom HTML for the item.
        /// </summary>
        /// <returns type="String"></returns>
        return null;
    },
    
    toString: function Open_Core_Lists_ListItem$toString() {
        /// <returns type="String"></returns>
        return String.format('{0} {1}', Open.Core.Lists.ListItem.callBaseMethod(this, 'toString'), this.get_text());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists._listTreePanel

Open.Core.Lists._listTreePanel = function Open_Core_Lists__listTreePanel(parentList, rootDiv, node) {
    /// <summary>
    /// Renders a single list within a tree-of lists.
    /// </summary>
    /// <param name="parentList" type="Open.Core.Lists.ListTreeView">
    /// </param>
    /// <param name="rootDiv" type="jQueryObject">
    /// </param>
    /// <param name="node" type="Open.Core.ITreeNode">
    /// </param>
    /// <field name="_rootDiv$2" type="jQueryObject">
    /// </field>
    /// <field name="_div$2" type="jQueryObject">
    /// </field>
    /// <field name="_node$2" type="Open.Core.ITreeNode">
    /// </field>
    /// <field name="_parentList$2" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_listView$2" type="Open.Core.Lists.ListView">
    /// </field>
    Open.Core.Lists._listTreePanel.initializeBase(this);
    this._parentList$2 = parentList;
    this._rootDiv$2 = rootDiv;
    this._node$2 = node;
    Open.Core.GlobalEvents.add_horizontalPanelResized(ss.Delegate.create(this, this._onHorizontalPanelResized$2));
    node.add_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$2));
    node.add_addedChild(ss.Delegate.create(this, this._onAddedChild$2));
    node.add_removedChild(ss.Delegate.create(this, this._onRemovedChild$2));
    if (node.get_parent() != null) {
        node.get_parent().add_removingChild(ss.Delegate.create(this, this._onParentRemovingChild$2));
    }
}
Open.Core.Lists._listTreePanel.prototype = {
    _rootDiv$2: null,
    _div$2: null,
    _node$2: null,
    _parentList$2: null,
    _listView$2: null,
    
    onDisposed: function Open_Core_Lists__listTreePanel$onDisposed() {
        this._div$2.remove();
        Open.Core.GlobalEvents.remove_horizontalPanelResized(ss.Delegate.create(this, this._onHorizontalPanelResized$2));
        this._node$2.remove_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$2));
        this._node$2.remove_addedChild(ss.Delegate.create(this, this._onAddedChild$2));
        this._node$2.remove_removedChild(ss.Delegate.create(this, this._onRemovedChild$2));
        if (this._node$2.get_parent() != null) {
            this._node$2.get_parent().remove_removingChild(ss.Delegate.create(this, this._onParentRemovingChild$2));
        }
        Open.Core.Lists._listTreePanel.callBaseMethod(this, 'onDisposed');
    },
    
    _onChildSelectionChanged$2: function Open_Core_Lists__listTreePanel$_onChildSelectionChanged$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        var selectedNode = this._getSelectedChild$2();
        if (!ss.isNullOrUndefined(selectedNode)) {
            this._parentList$2.set_selectedNode(selectedNode);
        }
    },
    
    _onHorizontalPanelResized$2: function Open_Core_Lists__listTreePanel$_onHorizontalPanelResized$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._syncWidth$2();
    },
    
    _onAddedChild$2: function Open_Core_Lists__listTreePanel$_onAddedChild$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Core.TreeNodeEventArgs">
        /// </param>
        this._listView$2.insert(e.get_index(), e.get_node());
    },
    
    _onRemovedChild$2: function Open_Core_Lists__listTreePanel$_onRemovedChild$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Core.TreeNodeEventArgs">
        /// </param>
        this._listView$2.remove(e.get_node());
    },
    
    _onParentRemovingChild$2: function Open_Core_Lists__listTreePanel$_onParentRemovingChild$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Core.TreeNodeEventArgs">
        /// </param>
        if (e.get_node() !== this._node$2) {
            return;
        }
        if (this._parentList$2.get_rootNode() != null) {
            var ancestor = Open.Core.Helper.get_tree().firstRemainingParent(this._parentList$2.get_rootNode(), this._node$2);
            this._parentList$2.set_selectedParent(ancestor || this._parentList$2.get_rootNode());
        }
        this.dispose();
    },
    
    get_node: function Open_Core_Lists__listTreePanel$get_node() {
        /// <value type="Open.Core.ITreeNode"></value>
        return this._node$2;
    },
    
    get_isCenterStage: function Open_Core_Lists__listTreePanel$get_isCenterStage() {
        /// <value type="Boolean"></value>
        return this._div$2.css(Open.Core.Css.left) === '0px';
    },
    
    get__width$2: function Open_Core_Lists__listTreePanel$get__width$2() {
        /// <value type="Number" integer="true"></value>
        return this._rootDiv$2.width();
    },
    
    get__slideDuration$2: function Open_Core_Lists__listTreePanel$get__slideDuration$2() {
        /// <value type="Number" integer="true"></value>
        return Open.Core.Helper.get_number().toMsecs(this._parentList$2.get_slideDuration());
    },
    
    onInitialize: function Open_Core_Lists__listTreePanel$onInitialize(container) {
        /// <param name="container" type="jQueryObject">
        /// </param>
        this._div$2 = Open.Core.Html.appendDiv(container);
        this._div$2 = container.children(Open.Core.Html.div).last();
        this._hide$2();
        Open.Core.Css.absoluteFill(this._div$2);
        this._listView$2 = new Open.Core.Lists.ListView(this._div$2);
        this._listView$2.load(this._node$2.get_children());
        this._syncWidth$2();
    },
    
    slideOff: function Open_Core_Lists__listTreePanel$slideOff(direction, onComplete) {
        /// <param name="direction" type="Open.Core.HorizontalDirection">
        /// </param>
        /// <param name="onComplete" type="Action">
        /// </param>
        if (!this.get_isInitialized()) {
            return;
        }
        this.centerStage();
        var properties = {};
        properties[Open.Core.Css.left] = (direction === Open.Core.HorizontalDirection.left) ? 0 - this.get__width$2() : this.get__width$2();
        this._div$2.animate(properties, this.get__slideDuration$2(), this._parentList$2.get_slideEasing(), ss.Delegate.create(this, function() {
            this._hide$2();
            Open.Core.Helper.invokeOrDefault(onComplete);
        }));
    },
    
    slideOn: function Open_Core_Lists__listTreePanel$slideOn(direction, onComplete) {
        /// <param name="direction" type="Open.Core.HorizontalDirection">
        /// </param>
        /// <param name="onComplete" type="Action">
        /// </param>
        if (!this.get_isInitialized()) {
            return;
        }
        this.setPosition(direction, true);
        var properties = {};
        properties[Open.Core.Css.left] = 0;
        this._div$2.animate(properties, this.get__slideDuration$2(), this._parentList$2.get_slideEasing(), ss.Delegate.create(this, function() {
            Open.Core.Helper.invokeOrDefault(onComplete);
        }));
    },
    
    centerStage: function Open_Core_Lists__listTreePanel$centerStage() {
        this._div$2.css(Open.Core.Css.left, '0px');
        this._div$2.css(Open.Core.Css.display, Open.Core.Css.block);
        this._syncWidth$2();
    },
    
    setPosition: function Open_Core_Lists__listTreePanel$setPosition(direction, isVisible) {
        /// <param name="direction" type="Open.Core.HorizontalDirection">
        /// </param>
        /// <param name="isVisible" type="Boolean">
        /// </param>
        var startLeft = (direction === Open.Core.HorizontalDirection.right) ? 0 - this.get__width$2() : this.get__width$2();
        this._div$2.css(Open.Core.Css.left, startLeft + Open.Core.Css.px);
        this._div$2.css(Open.Core.Css.display, (isVisible) ? Open.Core.Css.block : Open.Core.Css.none);
        this._syncWidth$2();
    },
    
    _hide$2: function Open_Core_Lists__listTreePanel$_hide$2() {
        this._div$2.css(Open.Core.Css.display, Open.Core.Css.none);
    },
    
    _syncWidth$2: function Open_Core_Lists__listTreePanel$_syncWidth$2() {
        this._div$2.width(this.get__width$2());
    },
    
    _getSelectedChild$2: function Open_Core_Lists__listTreePanel$_getSelectedChild$2() {
        /// <returns type="Open.Core.ITreeNode"></returns>
        var $enum1 = ss.IEnumerator.getEnumerator(this._node$2.get_children());
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            if (item.get_isSelected()) {
                return item;
            }
        }
        return null;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListTreeView

Open.Core.Lists.ListTreeView = function Open_Core_Lists_ListTreeView(container) {
    /// <summary>
    /// Represents a tree structure of lists.
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// The containing element.
    /// </param>
    /// <field name="__selectedNodeChanged$2" type="EventHandler">
    /// </field>
    /// <field name="__selectedParentChanged$2" type="EventHandler">
    /// </field>
    /// <field name="propRootNode" type="String" static="true">
    /// </field>
    /// <field name="propSelectedNode" type="String" static="true">
    /// </field>
    /// <field name="propSelectedParent" type="String" static="true">
    /// </field>
    /// <field name="_div$2" type="jQueryObject">
    /// </field>
    /// <field name="_slideDuration$2" type="Number">
    /// </field>
    /// <field name="_slideEasing$2" type="EffectEasing">
    /// </field>
    /// <field name="_panels$2" type="Array">
    /// </field>
    /// <field name="_previousNode$2" type="Open.Core.ITreeNode">
    /// </field>
    this._slideEasing$2 = 'swing';
    this._panels$2 = [];
    Open.Core.Lists.ListTreeView.initializeBase(this);
    this.initialize(container);
    Open.Core.Lists.ListCss.insertCss();
}
Open.Core.Lists.ListTreeView._getSlideDirection$2 = function Open_Core_Lists_ListTreeView$_getSlideDirection$2(previousNode, newNode) {
    /// <param name="previousNode" type="Open.Core.ITreeNode">
    /// </param>
    /// <param name="newNode" type="Open.Core.ITreeNode">
    /// </param>
    /// <returns type="Open.Core.HorizontalDirection"></returns>
    if (previousNode == null) {
        return Open.Core.HorizontalDirection.left;
    }
    return (previousNode.containsDescendent(newNode)) ? Open.Core.HorizontalDirection.left : Open.Core.HorizontalDirection.right;
}
Open.Core.Lists.ListTreeView._deselectChildren$2 = function Open_Core_Lists_ListTreeView$_deselectChildren$2(node) {
    /// <param name="node" type="Open.Core.ITreeNode">
    /// </param>
    var $enum1 = ss.IEnumerator.getEnumerator(node.get_children());
    while ($enum1.moveNext()) {
        var child = $enum1.get_current();
        child.set_isSelected(false);
    }
}
Open.Core.Lists.ListTreeView.prototype = {
    
    add_selectedNodeChanged: function Open_Core_Lists_ListTreeView$add_selectedNodeChanged(value) {
        /// <summary>
        /// Fires when the selected-node property changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectedNodeChanged$2 = ss.Delegate.combine(this.__selectedNodeChanged$2, value);
    },
    remove_selectedNodeChanged: function Open_Core_Lists_ListTreeView$remove_selectedNodeChanged(value) {
        /// <summary>
        /// Fires when the selected-node property changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectedNodeChanged$2 = ss.Delegate.remove(this.__selectedNodeChanged$2, value);
    },
    
    __selectedNodeChanged$2: null,
    
    _fireSelectedNodeChanged$2: function Open_Core_Lists_ListTreeView$_fireSelectedNodeChanged$2() {
        if (this.__selectedNodeChanged$2 != null) {
            this.__selectedNodeChanged$2.invoke(this, new ss.EventArgs());
        }
    },
    
    add_selectedParentChanged: function Open_Core_Lists_ListTreeView$add_selectedParentChanged(value) {
        /// <summary>
        /// Fires when the selected-parent property changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectedParentChanged$2 = ss.Delegate.combine(this.__selectedParentChanged$2, value);
    },
    remove_selectedParentChanged: function Open_Core_Lists_ListTreeView$remove_selectedParentChanged(value) {
        /// <summary>
        /// Fires when the selected-parent property changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectedParentChanged$2 = ss.Delegate.remove(this.__selectedParentChanged$2, value);
    },
    
    __selectedParentChanged$2: null,
    
    _fireSelectedParentChanged$2: function Open_Core_Lists_ListTreeView$_fireSelectedParentChanged$2() {
        if (this.__selectedParentChanged$2 != null) {
            this.__selectedParentChanged$2.invoke(this, new ss.EventArgs());
        }
    },
    
    _div$2: null,
    _slideDuration$2: 0.4,
    _previousNode$2: null,
    
    onDisposed: function Open_Core_Lists_ListTreeView$onDisposed() {
        this._reset$2();
        Open.Core.Lists.ListTreeView.callBaseMethod(this, 'onDisposed');
    },
    
    get_rootNode: function Open_Core_Lists_ListTreeView$get_rootNode() {
        /// <summary>
        /// Gets or sets the root node that the tree of lists built from.
        /// </summary>
        /// <value type="Open.Core.ITreeNode"></value>
        return Type.safeCast(this.get(Open.Core.Lists.ListTreeView.propRootNode, null), Open.Core.ITreeNode);
    },
    set_rootNode: function Open_Core_Lists_ListTreeView$set_rootNode(value) {
        /// <summary>
        /// Gets or sets the root node that the tree of lists built from.
        /// </summary>
        /// <value type="Open.Core.ITreeNode"></value>
        if (this.set(Open.Core.Lists.ListTreeView.propRootNode, value, null)) {
            this._reset$2();
            this.set_selectedNode(value);
        }
        return value;
    },
    
    get_selectedNode: function Open_Core_Lists_ListTreeView$get_selectedNode() {
        /// <summary>
        /// Gets or sets the currently selected node (it's Children are what is displayed in the visible list).
        /// </summary>
        /// <value type="Open.Core.ITreeNode"></value>
        return Type.safeCast(this.get(Open.Core.Lists.ListTreeView.propSelectedNode, null), Open.Core.ITreeNode);
    },
    set_selectedNode: function Open_Core_Lists_ListTreeView$set_selectedNode(value) {
        /// <summary>
        /// Gets or sets the currently selected node (it's Children are what is displayed in the visible list).
        /// </summary>
        /// <value type="Open.Core.ITreeNode"></value>
        if (this.set(Open.Core.Lists.ListTreeView.propSelectedNode, value, null)) {
            if (value != null && (value.get_childCount() > 0 || value.get_isRoot())) {
                this.set_selectedParent(value);
            }
            this._fireSelectedNodeChanged$2();
        }
        return value;
    },
    
    get_selectedParent: function Open_Core_Lists_ListTreeView$get_selectedParent() {
        /// <summary>
        /// Gets or sets the node which is the root of the current list (may be the same as SelectedNode).
        /// </summary>
        /// <value type="Open.Core.ITreeNode"></value>
        return this.get(Open.Core.Lists.ListTreeView.propSelectedParent, null);
    },
    set_selectedParent: function Open_Core_Lists_ListTreeView$set_selectedParent(value) {
        /// <summary>
        /// Gets or sets the node which is the root of the current list (may be the same as SelectedNode).
        /// </summary>
        /// <value type="Open.Core.ITreeNode"></value>
        if (this.set(Open.Core.Lists.ListTreeView.propSelectedParent, value, null)) {
            if (value != null) {
                Open.Core.Lists.ListTreeView._deselectChildren$2(value);
                if (this._previousNode$2 == null) {
                    this._getOrCreatePanel$2(value, true).centerStage();
                }
                else {
                    this._slidePanels$2(this._previousNode$2, value);
                }
            }
            this._fireSelectedParentChanged$2();
            this._previousNode$2 = value;
        }
        return value;
    },
    
    get_slideDuration: function Open_Core_Lists_ListTreeView$get_slideDuration() {
        /// <summary>
        /// Gets or sets the slide duration (in seconds).
        /// </summary>
        /// <value type="Number"></value>
        return this._slideDuration$2;
    },
    set_slideDuration: function Open_Core_Lists_ListTreeView$set_slideDuration(value) {
        /// <summary>
        /// Gets or sets the slide duration (in seconds).
        /// </summary>
        /// <value type="Number"></value>
        this._slideDuration$2 = value;
        return value;
    },
    
    get_slideEasing: function Open_Core_Lists_ListTreeView$get_slideEasing() {
        /// <summary>
        /// Gets or sets the easing effect applied to the slide.
        /// </summary>
        /// <value type="EffectEasing"></value>
        return this._slideEasing$2;
    },
    set_slideEasing: function Open_Core_Lists_ListTreeView$set_slideEasing(value) {
        /// <summary>
        /// Gets or sets the easing effect applied to the slide.
        /// </summary>
        /// <value type="EffectEasing"></value>
        this._slideEasing$2 = value;
        return value;
    },
    
    onInitialize: function Open_Core_Lists_ListTreeView$onInitialize(container) {
        /// <param name="container" type="jQueryObject">
        /// </param>
        this._div$2 = Open.Core.Html.appendDiv(container);
        Open.Core.Css.absoluteFill(this._div$2);
        Open.Core.Css.setOverflow(this._div$2, Open.Core.CssOverflow.hidden);
    },
    
    back: function Open_Core_Lists_ListTreeView$back() {
        /// <summary>
        /// Moves the selected node to the parent of the current node.
        /// </summary>
        if (this.get_selectedParent() == null || this.get_selectedParent().get_isRoot()) {
            return;
        }
        this.set_selectedNode(this.get_selectedParent().get_parent());
    },
    
    home: function Open_Core_Lists_ListTreeView$home() {
        /// <summary>
        /// Moves the selected node to the root node.
        /// </summary>
        this.set_selectedNode(this.get_rootNode());
    },
    
    _slidePanels$2: function Open_Core_Lists_ListTreeView$_slidePanels$2(previousNode, newNode) {
        /// <param name="previousNode" type="Open.Core.ITreeNode">
        /// </param>
        /// <param name="newNode" type="Open.Core.ITreeNode">
        /// </param>
        var direction = Open.Core.Lists.ListTreeView._getSlideDirection$2(previousNode, newNode);
        if (previousNode != null) {
            var oldPanel = this._getOrCreatePanel$2(previousNode, true);
            oldPanel.slideOff(direction, null);
        }
        var panel = this._getOrCreatePanel$2(newNode, true);
        panel.slideOn(direction, null);
    },
    
    _getOrCreatePanel$2: function Open_Core_Lists_ListTreeView$_getOrCreatePanel$2(node, initialize) {
        /// <param name="node" type="Open.Core.ITreeNode">
        /// </param>
        /// <param name="initialize" type="Boolean">
        /// </param>
        /// <returns type="Open.Core.Lists._listTreePanel"></returns>
        var panel = this._getPanel$2(node) || this._createPanel$2(node);
        if (initialize && !panel.get_isInitialized()) {
            panel.initialize(this._div$2);
        }
        return panel;
    },
    
    _getPanel$2: function Open_Core_Lists_ListTreeView$_getPanel$2(node) {
        /// <param name="node" type="Open.Core.ITreeNode">
        /// </param>
        /// <returns type="Open.Core.Lists._listTreePanel"></returns>
        var $enum1 = ss.IEnumerator.getEnumerator(this._panels$2);
        while ($enum1.moveNext()) {
            var panel = $enum1.get_current();
            if (panel.get_node() === node) {
                return panel;
            }
        }
        return null;
    },
    
    _createPanel$2: function Open_Core_Lists_ListTreeView$_createPanel$2(node) {
        /// <param name="node" type="Open.Core.ITreeNode">
        /// </param>
        /// <returns type="Open.Core.Lists._listTreePanel"></returns>
        var panel = new Open.Core.Lists._listTreePanel(this, this._div$2, node);
        this._panels$2.add(panel);
        return panel;
    },
    
    _reset$2: function Open_Core_Lists_ListTreeView$_reset$2() {
        var $enum1 = ss.IEnumerator.getEnumerator(this._panels$2);
        while ($enum1.moveNext()) {
            var panel = $enum1.get_current();
            panel.dispose();
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListTemplates

Open.Core.Lists.ListTemplates = function Open_Core_Lists_ListTemplates() {
    /// <summary>
    /// HTML templates for lists and list-items.
    /// </summary>
}
Open.Core.Lists.ListTemplates.defaultListItem = function Open_Core_Lists_ListTemplates$defaultListItem(model) {
    /// <summary>
    /// Constructs the default HTML for an item in a list.
    /// </summary>
    /// <param name="model" type="Object">
    /// </param>
    /// <returns type="jQueryObject"></returns>
    var listItem = Type.safeCast(model, Open.Core.Lists.IListItem);
    var divRoot = Open.Core.Html.createDiv();
    var spanLabel = Open.Core.Html.createSpan();
    spanLabel.appendTo(divRoot);
    var src = (listItem == null) ? Open.Core.Lists.ListHtml.childPointerIcon : listItem.get_rightIconSrc() || Open.Core.Lists.ListHtml.childPointerIcon;
    var img = Open.Core.Html.createImage(src, null);
    img.addClass(Open.Core.Lists.ListCss.itemClasses.iconRight);
    img.appendTo(divRoot);
    divRoot.addClass(Open.Core.Lists.ListCss.itemClasses.defaultRoot);
    spanLabel.addClass(Open.Core.Lists.ListCss.itemClasses.label);
    spanLabel.addClass(Open.Core.Css.classes.titleFont);
    return divRoot;
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListView

Open.Core.Lists.ListView = function Open_Core_Lists_ListView(container) {
    /// <summary>
    /// Renders a simple list.
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// The containing element.
    /// </param>
    /// <field name="_itemFactory$2" type="Open.Core.Lists._listItemFactory">
    /// </field>
    /// <field name="_selectionMode$2" type="Open.Core.Lists.ListSelectionMode">
    /// </field>
    /// <field name="_views$2" type="Array">
    /// </field>
    this._selectionMode$2 = Open.Core.Lists.ListSelectionMode.single;
    this._views$2 = [];
    Open.Core.Lists.ListView.initializeBase(this);
    this.initialize(container);
    Open.Core.Lists.ListCss.insertCss();
}
Open.Core.Lists.ListView.prototype = {
    _itemFactory$2: null,
    
    onInitialize: function Open_Core_Lists_ListView$onInitialize(container) {
        /// <param name="container" type="jQueryObject">
        /// </param>
        container.addClass(Open.Core.Lists.ListCss.classes.root);
        Open.Core.Lists.ListView.callBaseMethod(this, 'onInitialize', [ container ]);
    },
    
    _onItemClick$2: function Open_Core_Lists_ListView$_onItemClick$2(e, view) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        /// <param name="view" type="Open.Core.Lists.IListItemView">
        /// </param>
        if (this.get_selectionMode() === Open.Core.Lists.ListSelectionMode.none) {
            return;
        }
        view.set_isSelected(true);
    },
    
    _onViewPropertyChanged$2: function Open_Core_Lists_ListView$_onViewPropertyChanged$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Core.PropertyChangedEventArgs">
        /// </param>
        if (e.get_property().get_name() === Open.Core.TreeNode.propIsSelected) {
            var view = Type.safeCast(sender, Open.Core.Lists.IListItemView);
            if (view != null && view.get_isSelected()) {
                this._selectItem$2(view);
            }
        }
    },
    
    get__itemFactory$2: function Open_Core_Lists_ListView$get__itemFactory$2() {
        /// <summary>
        /// Gets or sets the factory that creates each item in the list.
        /// </summary>
        /// <value type="Open.Core.Lists._listItemFactory"></value>
        return this._itemFactory$2 || (this._itemFactory$2 = new Open.Core.Lists._listItemFactory());
    },
    
    get_selectionMode: function Open_Core_Lists_ListView$get_selectionMode() {
        /// <summary>
        /// Gets or sets whether items within the list are selecable.
        /// </summary>
        /// <value type="Open.Core.Lists.ListSelectionMode"></value>
        return this._selectionMode$2;
    },
    set_selectionMode: function Open_Core_Lists_ListView$set_selectionMode(value) {
        /// <summary>
        /// Gets or sets whether items within the list are selecable.
        /// </summary>
        /// <value type="Open.Core.Lists.ListSelectionMode"></value>
        this._selectionMode$2 = value;
        return value;
    },
    
    get_count: function Open_Core_Lists_ListView$get_count() {
        /// <summary>
        /// Gets the number of items currently in the list.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._views$2.length;
    },
    
    load: function Open_Core_Lists_ListView$load(items) {
        /// <summary>
        /// Loads the collection of models into the list.
        /// </summary>
        /// <param name="items" type="ss.IEnumerable">
        /// A collection models.
        /// </param>
        this.clear();
        this.insertRange(0, items);
    },
    
    insertRange: function Open_Core_Lists_ListView$insertRange(startingAt, models) {
        /// <summary>
        /// Loads the collection of models into the list.
        /// </summary>
        /// <param name="startingAt" type="Number" integer="true">
        /// Index to start inserting at (0-based).
        /// </param>
        /// <param name="models" type="ss.IEnumerable">
        /// A collection models.
        /// </param>
        if (ss.isNullOrUndefined(models)) {
            return;
        }
        if (startingAt < 0) {
            startingAt = 0;
        }
        var $enum1 = ss.IEnumerator.getEnumerator(models);
        while ($enum1.moveNext()) {
            var model = $enum1.get_current();
            this.insert(startingAt, model);
            startingAt++;
        }
    },
    
    insert: function Open_Core_Lists_ListView$insert(index, model) {
        /// <summary>
        /// Inserts a list-item for the given model at the specified index.
        /// </summary>
        /// <param name="index" type="Number" integer="true">
        /// The index to insert at (0-based).
        /// </param>
        /// <param name="model" type="Object">
        /// The data-model for the item.
        /// </param>
        var insertBefore = this._insertBefore$2(index);
        var div = Open.Core.Html.createDiv();
        if (insertBefore == null) {
            div.appendTo(this.get_container());
        }
        else {
            div.insertBefore(insertBefore);
        }
        var view = this.get__itemFactory$2().createView(div, model);
        var listItemView = Type.safeCast(view, Open.Core.Lists.IListItemView);
        this._views$2.add(view);
        if (listItemView != null) {
            div.click(ss.Delegate.create(this, function(e) {
                this._onItemClick$2(e, listItemView);
            }));
        }
        var observableView = Type.safeCast(view, Open.Core.INotifyPropertyChanged);
        if (observableView != null) {
            observableView.add_propertyChanged(ss.Delegate.create(this, this._onViewPropertyChanged$2));
        }
    },
    
    remove: function Open_Core_Lists_ListView$remove(model) {
        /// <summary>
        /// Removes the list item with the specified model.
        /// </summary>
        /// <param name="model" type="Object">
        /// The model of the item to remove.
        /// </param>
        if (model == null) {
            return;
        }
        var view = this._getView$2(model);
        this._removeView$2(Type.safeCast(view, Open.Core.IView));
    },
    
    _removeView$2: function Open_Core_Lists_ListView$_removeView$2(view) {
        /// <param name="view" type="Open.Core.IView">
        /// </param>
        if (view == null) {
            return;
        }
        var observableView = Type.safeCast(view, Open.Core.INotifyPropertyChanged);
        if (observableView != null) {
            observableView.remove_propertyChanged(ss.Delegate.create(this, this._onViewPropertyChanged$2));
        }
        view.dispose();
        this._views$2.remove(view);
    },
    
    clear: function Open_Core_Lists_ListView$clear() {
        /// <summary>
        /// Clears the list (disposing of all children).
        /// </summary>
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$2.clone());
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            this._removeView$2(view);
        }
    },
    
    _insertBefore$2: function Open_Core_Lists_ListView$_insertBefore$2(insertAt) {
        /// <param name="insertAt" type="Number" integer="true">
        /// </param>
        /// <returns type="jQueryObject"></returns>
        if (insertAt < 0 || this.get_count() === 0) {
            return null;
        }
        var lastItem = this.get_count() - 1;
        if (insertAt > lastItem) {
            return null;
        }
        return Open.Core.Html.childAt(insertAt, this.get_container());
    },
    
    _selectItem$2: function Open_Core_Lists_ListView$_selectItem$2(item) {
        /// <param name="item" type="Open.Core.Lists.IListItemView">
        /// </param>
        if (ss.isNullOrUndefined(item)) {
            return;
        }
        this._clearSelection$2(item);
        item.set_isSelected(true);
    },
    
    _clearSelection$2: function Open_Core_Lists_ListView$_clearSelection$2(exclude) {
        /// <param name="exclude" type="Open.Core.Lists.IListItemView">
        /// </param>
        var $enum1 = ss.IEnumerator.getEnumerator(this._getListItemViews$2());
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            if (!ss.isNullOrUndefined(view) && view !== exclude) {
                view.set_isSelected(false);
            }
        }
    },
    
    _getListItemViews$2: function Open_Core_Lists_ListView$_getListItemViews$2() {
        /// <returns type="ss.IEnumerable"></returns>
        return Open.Core.Helper.get_collection().filter(this._views$2, ss.Delegate.create(this, function(o) {
            return (Type.safeCast(o, Open.Core.Lists.IListItemView)) != null;
        }));
    },
    
    _getView$2: function Open_Core_Lists_ListView$_getView$2(model) {
        /// <param name="model" type="Object">
        /// </param>
        /// <returns type="Open.Core.Lists.IListItemView"></returns>
        return Type.safeCast(Open.Core.Helper.get_collection().first(this._getListItemViews$2(), ss.Delegate.create(this, function(o) {
            return (o).get_model() === model;
        })), Open.Core.Lists.IListItemView);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListItemView

Open.Core.Lists.ListItemView = function Open_Core_Lists_ListItemView(container, model) {
    /// <summary>
    /// Represents a single item within a list.
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// The containing element.
    /// </param>
    /// <param name="model" type="Object">
    /// The data model for the list item.
    /// </param>
    /// <field name="_model$2" type="Object">
    /// </field>
    /// <field name="_htmLabel$2" type="jQueryObject">
    /// </field>
    /// <field name="_imgRightIcon$2" type="jQueryObject">
    /// </field>
    /// <field name="_text$2" type="String">
    /// </field>
    /// <field name="_isSelectedRef$2" type="Open.Core.PropertyRef">
    /// </field>
    Open.Core.Lists.ListItemView.initializeBase(this);
    this._model$2 = model;
    this.initialize(container);
    this._isSelectedRef$2 = Open.Core.PropertyRef.getFromModel(model, Open.Core.TreeNode.propIsSelected);
    if (this._isSelectedRef$2 != null) {
        this._isSelectedRef$2.add_changed(ss.Delegate.create(this, this._onIsSelectedChanged$2));
    }
    if (this.get__modelAsTreeNode$2() != null) {
        this.get__modelAsTreeNode$2().add_childrenChanged(ss.Delegate.create(this, this._onTreeNodeChildrenChanged$2));
    }
    this.updateVisualState();
}
Open.Core.Lists.ListItemView._getChild$2 = function Open_Core_Lists_ListItemView$_getChild$2(parent, cssClass) {
    /// <param name="parent" type="jQueryObject">
    /// </param>
    /// <param name="cssClass" type="String">
    /// </param>
    /// <returns type="jQueryObject"></returns>
    return parent.children(Open.Core.Css.toClass(cssClass)).first();
}
Open.Core.Lists.ListItemView.prototype = {
    _model$2: null,
    _htmLabel$2: null,
    _imgRightIcon$2: null,
    _text$2: null,
    _isSelectedRef$2: null,
    
    onDisposed: function Open_Core_Lists_ListItemView$onDisposed() {
        if (this._isSelectedRef$2 != null) {
            this._isSelectedRef$2.remove_changed(ss.Delegate.create(this, this._onIsSelectedChanged$2));
        }
        if (this.get__modelAsTreeNode$2() != null) {
            this.get__modelAsTreeNode$2().remove_childrenChanged(ss.Delegate.create(this, this._onTreeNodeChildrenChanged$2));
        }
        this.get_container().remove();
        Open.Core.Lists.ListItemView.callBaseMethod(this, 'onDisposed');
    },
    
    _onIsSelectedChanged$2: function Open_Core_Lists_ListItemView$_onIsSelectedChanged$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this.updateVisualState();
        this.firePropertyChanged(Open.Core.TreeNode.propIsSelected);
    },
    
    _onTreeNodeChildrenChanged$2: function Open_Core_Lists_ListItemView$_onTreeNodeChildrenChanged$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._updateRightIcon$2();
    },
    
    get_model: function Open_Core_Lists_ListItemView$get_model() {
        /// <summary>
        /// Gets or sets the data model.
        /// </summary>
        /// <value type="Object"></value>
        return this._model$2;
    },
    
    get_text: function Open_Core_Lists_ListItemView$get_text() {
        /// <value type="String"></value>
        return this._text$2;
    },
    set_text: function Open_Core_Lists_ListItemView$set_text(value) {
        /// <value type="String"></value>
        this._text$2 = value;
        if (this._htmLabel$2 != null) {
            this._htmLabel$2.html(this._text$2);
        }
        return value;
    },
    
    get_isSelected: function Open_Core_Lists_ListItemView$get_isSelected() {
        /// <value type="Boolean"></value>
        return (this._isSelectedRef$2 == null) ? false : this._isSelectedRef$2.get_value();
    },
    set_isSelected: function Open_Core_Lists_ListItemView$set_isSelected(value) {
        /// <value type="Boolean"></value>
        if (!this.get__canSelect$2()) {
            value = false;
        }
        if (value === this.get_isSelected()) {
            return;
        }
        if (this._isSelectedRef$2 != null) {
            this._isSelectedRef$2.set_value(value);
        }
        return value;
    },
    
    get_rightIconSrc: function Open_Core_Lists_ListItemView$get_rightIconSrc() {
        /// <value type="String"></value>
        return (this._imgRightIcon$2 == null) ? null : this._imgRightIcon$2.attr(Open.Core.Html.src);
    },
    set_rightIconSrc: function Open_Core_Lists_ListItemView$set_rightIconSrc(value) {
        /// <value type="String"></value>
        value = value || Open.Core.Lists.ListHtml.childPointerIcon;
        if (value === this.get_rightIconSrc()) {
            return;
        }
        if (this._imgRightIcon$2 != null) {
            this._imgRightIcon$2.attr(Open.Core.Html.src, value);
        }
        this._updateRightIcon$2();
        return value;
    },
    
    get__modelAsBindable$2: function Open_Core_Lists_ListItemView$get__modelAsBindable$2() {
        /// <value type="Open.Core.IModel"></value>
        return Type.safeCast(this.get_model(), Open.Core.IModel);
    },
    
    get__modelAsListItem$2: function Open_Core_Lists_ListItemView$get__modelAsListItem$2() {
        /// <value type="Open.Core.Lists.IListItem"></value>
        return Type.safeCast(this.get_model(), Open.Core.Lists.IListItem);
    },
    
    get__modelAsTreeNode$2: function Open_Core_Lists_ListItemView$get__modelAsTreeNode$2() {
        /// <value type="Open.Core.ITreeNode"></value>
        return Type.safeCast(this.get_model(), Open.Core.ITreeNode);
    },
    
    get__canSelect$2: function Open_Core_Lists_ListItemView$get__canSelect$2() {
        /// <value type="Boolean"></value>
        var item = this.get__modelAsListItem$2();
        return (item == null) ? true : item.get_canSelect();
    },
    
    onInitialize: function Open_Core_Lists_ListItemView$onInitialize(container) {
        /// <summary>
        /// Initializes the list-item.
        /// </summary>
        /// <param name="container" type="jQueryObject">
        /// The containing <li></li> element.
        /// </param>
        container.addClass(Open.Core.Lists.ListCss.itemClasses.root);
        var customHtml = this._getFactoryHtml$2();
        var content = (customHtml == null) ? Open.Core.Lists.ListTemplates.defaultListItem(this.get_model()) : $(customHtml);
        content.appendTo(container);
        this._htmLabel$2 = Open.Core.Lists.ListItemView._getChild$2(content, Open.Core.Lists.ListCss.itemClasses.label);
        this._imgRightIcon$2 = Open.Core.Lists.ListItemView._getChild$2(content, Open.Core.Lists.ListCss.itemClasses.iconRight);
        this._imgRightIcon$2.load(ss.Delegate.create(this, function(eevent) {
            this._updateRightIcon$2();
        }));
        this._setupBindings$2();
        this.updateVisualState();
    },
    
    updateVisualState: function Open_Core_Lists_ListItemView$updateVisualState() {
        /// <summary>
        /// Refrehses the visual state of the item.
        /// </summary>
        Open.Core.Css.addOrRemoveClass(this.get_container(), Open.Core.Lists.ListCss.itemClasses.selected, this.get_isSelected());
        this._updateRightIcon$2();
    },
    
    _updateRightIcon$2: function Open_Core_Lists_ListItemView$_updateRightIcon$2() {
        if (ss.isNullOrUndefined(this._imgRightIcon$2)) {
            return;
        }
        var isVisible = false;
        var treeNode = this.get__modelAsTreeNode$2();
        if (this.get__canSelect$2() && treeNode != null && treeNode.get_childCount() > 0) {
            isVisible = true;
        }
        Open.Core.Css.setVisible(this._imgRightIcon$2, isVisible);
        if (!isVisible) {
            return;
        }
        if (this.get_container().height() === 0) {
            return;
        }
        Open.Core.Html.centerVertically(this._imgRightIcon$2, this.get_container());
    },
    
    _getFactoryHtml$2: function Open_Core_Lists_ListItemView$_getFactoryHtml$2() {
        /// <returns type="String"></returns>
        var factory = Type.safeCast(this.get_model(), Open.Core.IHtmlFactory);
        return (factory == null) ? null : factory.createHtml();
    },
    
    _setupBindings$2: function Open_Core_Lists_ListItemView$_setupBindings$2() {
        var bindable = this.get__modelAsBindable$2();
        if (bindable == null) {
            return;
        }
        this._setBinding$2(bindable, Open.Core.Lists.ListItem.propText);
        this._setBinding$2(bindable, Open.Core.Lists.ListItem.propRightIconSrc);
    },
    
    _setBinding$2: function Open_Core_Lists_ListItemView$_setBinding$2(bindable, propertyName) {
        /// <param name="bindable" type="Open.Core.IModel">
        /// </param>
        /// <param name="propertyName" type="String">
        /// </param>
        var sourceProperty = bindable.getPropertyRef(propertyName);
        if (sourceProperty != null) {
            this.getPropertyRef(propertyName).set_bindTo(sourceProperty);
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists._listItemFactory

Open.Core.Lists._listItemFactory = function Open_Core_Lists__listItemFactory() {
    /// <summary>
    /// The default view-factory for a list.
    /// </summary>
}
Open.Core.Lists._listItemFactory.prototype = {
    
    createView: function Open_Core_Lists__listItemFactory$createView(liElement, model) {
        /// <param name="liElement" type="jQueryObject">
        /// </param>
        /// <param name="model" type="Object">
        /// </param>
        /// <returns type="Open.Core.IView"></returns>
        return new Open.Core.Lists.ListItemView(liElement, model);
    }
}


Open.Core.Lists.ListHtml.registerClass('Open.Core.Lists.ListHtml');
Open.Core.Lists.ListCss.registerClass('Open.Core.Lists.ListCss');
Open.Core.Lists.ListClasses.registerClass('Open.Core.Lists.ListClasses');
Open.Core.Lists.ListItemClasses.registerClass('Open.Core.Lists.ListItemClasses');
Open.Core.Lists.ListTreeBackController.registerClass('Open.Core.Lists.ListTreeBackController', Open.Core.ControllerBase);
Open.Core.Lists.ListItem.registerClass('Open.Core.Lists.ListItem', Open.Core.TreeNode, Open.Core.Lists.IListItem, Open.Core.IHtmlFactory);
Open.Core.Lists._listTreePanel.registerClass('Open.Core.Lists._listTreePanel', Open.Core.ViewBase);
Open.Core.Lists.ListTreeView.registerClass('Open.Core.Lists.ListTreeView', Open.Core.ViewBase);
Open.Core.Lists.ListTemplates.registerClass('Open.Core.Lists.ListTemplates');
Open.Core.Lists.ListView.registerClass('Open.Core.Lists.ListView', Open.Core.ViewBase);
Open.Core.Lists.ListItemView.registerClass('Open.Core.Lists.ListItemView', Open.Core.ViewBase, Open.Core.Lists.IListItemView);
Open.Core.Lists._listItemFactory.registerClass('Open.Core.Lists._listItemFactory');
Open.Core.Lists.ListHtml.childPointerIcon = '/Open.Core/Images/ListItem.ChildPointer.png';
Open.Core.Lists.ListCss.url = '/Open.Core/Css/Core.Lists.css';
Open.Core.Lists.ListCss._isCssInserted = false;
Open.Core.Lists.ListCss.itemClasses = new Open.Core.Lists.ListItemClasses();
Open.Core.Lists.ListCss.classes = new Open.Core.Lists.ListClasses();
Open.Core.Lists.ListItem.propText = 'Text';
Open.Core.Lists.ListItem.propCanSelect = 'CanSelect';
Open.Core.Lists.ListItem.propRightIconSrc = 'RightIconSrc';
Open.Core.Lists.ListTreeView.propRootNode = 'RootNode';
Open.Core.Lists.ListTreeView.propSelectedNode = 'SelectedNode';
Open.Core.Lists.ListTreeView.propSelectedParent = 'SelectedParent';

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Lists', ['Open.Core.Script'], executeScript);
})();
