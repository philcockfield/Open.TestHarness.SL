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
// Open.Core.Lists.ListCss

Open.Core.Lists.ListCss = function Open_Core_Lists_ListCss() {
    /// <summary>
    /// CSS declarations for lists.
    /// </summary>
    /// <field name="url" type="String" static="true">
    /// </field>
    /// <field name="_isCssInserted" type="Boolean" static="true">
    /// </field>
    /// <field name="classes" type="Open.Core.Lists.ListCssClasses" static="true">
    /// </field>
}
Open.Core.Lists.ListCss.insertCss = function Open_Core_Lists_ListCss$insertCss() {
    /// <summary>
    /// Inserts the CSS for the 'Open.Core.Lists' library.
    /// </summary>
    if (Open.Core.Lists.ListCss._isCssInserted) {
        return;
    }
    if (!Open.Core.Css.isLinked(Open.Core.Lists.ListCss.url)) {
        Open.Core.Css.insertLink(Open.Core.Lists.ListCss.url);
    }
    Open.Core.Lists.ListCss._isCssInserted = true;
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListCssClasses

Open.Core.Lists.ListCssClasses = function Open_Core_Lists_ListCssClasses() {
    /// <field name="listItem" type="String">
    /// </field>
    /// <field name="selectedListItem" type="String">
    /// </field>
    /// <field name="itemLabel" type="String">
    /// </field>
}
Open.Core.Lists.ListCssClasses.prototype = {
    listItem: 'coreListItem',
    selectedListItem: 'selectedListItem',
    itemLabel: 'itemLabel'
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists._listTreePanel

Open.Core.Lists._listTreePanel = function Open_Core_Lists__listTreePanel(listTreeView, rootDiv, rootNode) {
    /// <param name="listTreeView" type="Open.Core.Lists.ListTreeView">
    /// </param>
    /// <param name="rootDiv" type="jQueryObject">
    /// </param>
    /// <param name="rootNode" type="Open.Core.ITreeNode">
    /// </param>
    /// <field name="_div$2" type="jQueryObject">
    /// </field>
    /// <field name="_listTreeView$2" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_listView$2" type="Open.Core.Lists.ListView">
    /// </field>
    /// <field name="_rootDiv$2" type="jQueryObject">
    /// </field>
    /// <field name="_rootNode$2" type="Open.Core.ITreeNode">
    /// </field>
    Open.Core.Lists._listTreePanel.initializeBase(this);
    this._listTreeView$2 = listTreeView;
    this._rootDiv$2 = rootDiv;
    this._rootNode$2 = rootNode;
    rootNode.add_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$2));
}
Open.Core.Lists._listTreePanel.prototype = {
    _div$2: null,
    _listTreeView$2: null,
    _listView$2: null,
    _rootDiv$2: null,
    _rootNode$2: null,
    
    onDisposed: function Open_Core_Lists__listTreePanel$onDisposed() {
        this._div$2.empty();
        this._rootNode$2.remove_childSelectionChanged(ss.Delegate.create(this, this._onChildSelectionChanged$2));
        Open.Core.Lists._listTreePanel.callBaseMethod(this, 'onDisposed');
    },
    
    _onChildSelectionChanged$2: function Open_Core_Lists__listTreePanel$_onChildSelectionChanged$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        var selectedNode = this._getSelectedChild$2();
        if (!ss.isNullOrUndefined(selectedNode)) {
            this._listTreeView$2.set_selectedNode(selectedNode);
        }
    },
    
    get_rootNode: function Open_Core_Lists__listTreePanel$get_rootNode() {
        /// <value type="Open.Core.ITreeNode"></value>
        return this._rootNode$2;
    },
    
    get__width$2: function Open_Core_Lists__listTreePanel$get__width$2() {
        /// <value type="Number" integer="true"></value>
        return this._rootDiv$2.width();
    },
    
    get__slideDuration$2: function Open_Core_Lists__listTreePanel$get__slideDuration$2() {
        /// <value type="Number" integer="true"></value>
        return Open.Core.Helper.get_number().toMsecs(this._listTreeView$2.get_slideDuration());
    },
    
    onInitialize: function Open_Core_Lists__listTreePanel$onInitialize(container) {
        /// <param name="container" type="jQueryObject">
        /// </param>
        this._div$2 = Open.Core.Html.appendDiv(container);
        this._div$2 = container.children(Open.Core.Html.div).last();
        this._hide$2();
        Open.Core.Css.absoluteFill(this._div$2);
        this._listView$2 = new Open.Core.Lists.ListView(this._div$2);
        this._listView$2.load(this._rootNode$2.get_children());
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
        this._div$2.animate(properties, this.get__slideDuration$2(), this._listTreeView$2.get_slideEasing(), ss.Delegate.create(this, function() {
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
        this._div$2.animate(properties, this.get__slideDuration$2(), this._listTreeView$2.get_slideEasing(), ss.Delegate.create(this, function() {
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
        this._div$2.css(Open.Core.Css.left, startLeft + 'px');
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
        var $enum1 = ss.IEnumerator.getEnumerator(this._rootNode$2.get_children());
        while ($enum1.moveNext()) {
            var node = $enum1.get_current();
            if (node.get_isSelected()) {
                return node;
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
    /// <field name="__selectionChanged$2" type="EventHandler">
    /// </field>
    /// <field name="propRootNode" type="String" static="true">
    /// </field>
    /// <field name="propSelectedNode" type="String" static="true">
    /// </field>
    /// <field name="propCurrentListRoot" type="String" static="true">
    /// </field>
    /// <field name="_div$2" type="jQueryObject">
    /// </field>
    /// <field name="_slideDuration$2" type="Number">
    /// </field>
    /// <field name="_slideEasing$2" type="EffectEasing">
    /// </field>
    /// <field name="_panels$2" type="Array">
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
Open.Core.Lists.ListTreeView.prototype = {
    
    add_selectionChanged: function Open_Core_Lists_ListTreeView$add_selectionChanged(value) {
        /// <summary>
        /// Fires when the selected node changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectionChanged$2 = ss.Delegate.combine(this.__selectionChanged$2, value);
    },
    remove_selectionChanged: function Open_Core_Lists_ListTreeView$remove_selectionChanged(value) {
        /// <summary>
        /// Fires when the selected node changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectionChanged$2 = ss.Delegate.remove(this.__selectionChanged$2, value);
    },
    
    __selectionChanged$2: null,
    
    _fireSelectionChanged$2: function Open_Core_Lists_ListTreeView$_fireSelectionChanged$2() {
        if (this.__selectionChanged$2 != null) {
            this.__selectionChanged$2.invoke(this, new ss.EventArgs());
        }
    },
    
    _div$2: null,
    _slideDuration$2: 0.4,
    
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
            if (value != null && value.get_totalChildren() > 0) {
                this.set_currentListRoot(value);
            }
            this._fireSelectionChanged$2();
        }
        return value;
    },
    
    get_currentListRoot: function Open_Core_Lists_ListTreeView$get_currentListRoot() {
        /// <summary>
        /// Gets or sets the node which is the root of the current list (may be the same as SelectedNode).
        /// </summary>
        /// <value type="Open.Core.ITreeNode"></value>
        return this.get(Open.Core.Lists.ListTreeView.propCurrentListRoot, null);
    },
    set_currentListRoot: function Open_Core_Lists_ListTreeView$set_currentListRoot(value) {
        /// <summary>
        /// Gets or sets the node which is the root of the current list (may be the same as SelectedNode).
        /// </summary>
        /// <value type="Open.Core.ITreeNode"></value>
        var previousNode = this.get_currentListRoot();
        if (this.set(Open.Core.Lists.ListTreeView.propCurrentListRoot, value, null)) {
            if (value != null) {
                this._deselectChildren$2(value);
                if (value.get_totalChildren() > 0) {
                    if (previousNode == null) {
                        this._getOrCreatePanel$2(value, true).centerStage();
                    }
                    else {
                        this._slidePanels$2(previousNode, value);
                    }
                }
            }
            this.firePropertyChanged(Open.Core.Lists.ListTreeView.propCurrentListRoot);
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
        if (this.get_currentListRoot() == null || this.get_currentListRoot().get_isRoot()) {
            return;
        }
        this.set_selectedNode(this.get_currentListRoot().get_parent());
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
    
    _deselectChildren$2: function Open_Core_Lists_ListTreeView$_deselectChildren$2(node) {
        /// <param name="node" type="Open.Core.ITreeNode">
        /// </param>
        var $enum1 = ss.IEnumerator.getEnumerator(node.get_children());
        while ($enum1.moveNext()) {
            var child = $enum1.get_current();
            child.set_isSelected(false);
        }
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
            if (panel.get_rootNode() === node) {
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
// Open.Core.Lists.ListView

Open.Core.Lists.ListView = function Open_Core_Lists_ListView(container) {
    /// <summary>
    /// Renders a simple list.
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// The containing element.
    /// </param>
    /// <field name="__selectionChanged$2" type="EventHandler">
    /// </field>
    /// <field name="_itemFactory$2" type="Open.Core.IViewFactory">
    /// </field>
    /// <field name="_defaultItemFactory$2" type="Open.Core.IViewFactory">
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
    
    add_selectionChanged: function Open_Core_Lists_ListView$add_selectionChanged(value) {
        /// <summary>
        /// Fires when the item selection changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectionChanged$2 = ss.Delegate.combine(this.__selectionChanged$2, value);
    },
    remove_selectionChanged: function Open_Core_Lists_ListView$remove_selectionChanged(value) {
        /// <summary>
        /// Fires when the item selection changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectionChanged$2 = ss.Delegate.remove(this.__selectionChanged$2, value);
    },
    
    __selectionChanged$2: null,
    
    _fireSelectionChanged$2: function Open_Core_Lists_ListView$_fireSelectionChanged$2() {
        if (this.__selectionChanged$2 != null) {
            this.__selectionChanged$2.invoke(this, new ss.EventArgs());
        }
    },
    
    _itemFactory$2: null,
    _defaultItemFactory$2: null,
    
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
        if (e.get_property().get_name() === Open.Core.Lists.ListItemView.propIsSelected) {
            var view = Type.safeCast(sender, Open.Core.Lists.IListItemView);
            if (view != null && view.get_isSelected()) {
                this._selectItem$2(view);
            }
        }
    },
    
    get_itemFactory: function Open_Core_Lists_ListView$get_itemFactory() {
        /// <summary>
        /// Gets or sets the factory that creates each item in the list.
        /// </summary>
        /// <value type="Open.Core.IViewFactory"></value>
        return this._itemFactory$2 || (this._defaultItemFactory$2 || (this._defaultItemFactory$2 = new Open.Core.Lists._listItemFactory()));
    },
    set_itemFactory: function Open_Core_Lists_ListView$set_itemFactory(value) {
        /// <summary>
        /// Gets or sets the factory that creates each item in the list.
        /// </summary>
        /// <value type="Open.Core.IViewFactory"></value>
        this._itemFactory$2 = value;
        return value;
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
    
    onInitialize: function Open_Core_Lists_ListView$onInitialize(container) {
        /// <param name="container" type="jQueryObject">
        /// </param>
    },
    
    load: function Open_Core_Lists_ListView$load(items) {
        /// <summary>
        /// Loads the collection of models into the list.
        /// </summary>
        /// <param name="items" type="ss.IEnumerable">
        /// A collection models.
        /// </param>
        this.clear();
        if (ss.isNullOrUndefined(items)) {
            return;
        }
        var models = Open.Core.Helper.get_collection().toArrayList(items);
        for (var i = 0; i < models.length; i++) {
            var div = Open.Core.Html.appendDiv(this.get_container());
            div.appendTo(this.get_container());
        }
        var factory = this.get_itemFactory();
        this.get_container().children(Open.Core.Html.div).each(ss.Delegate.create(this, function(index, element) {
            var div = $(element);
            var model = models[index];
            var view = factory.createView(div, model);
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
        }));
    },
    
    clear: function Open_Core_Lists_ListView$clear() {
        /// <summary>
        /// Clears the list (disposing of all children).
        /// </summary>
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$2);
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            var observableView = Type.safeCast(view, Open.Core.INotifyPropertyChanged);
            if (observableView != null) {
                observableView.remove_propertyChanged(ss.Delegate.create(this, this._onViewPropertyChanged$2));
            }
            view.dispose();
        }
        this.get_container().empty();
        this._views$2.clear();
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
        var $enum1 = ss.IEnumerator.getEnumerator(this._getItems$2());
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            if (!ss.isNullOrUndefined(item) && item !== exclude) {
                item.set_isSelected(false);
            }
        }
    },
    
    _getItems$2: function Open_Core_Lists_ListView$_getItems$2() {
        /// <returns type="ss.IEnumerable"></returns>
        var list = new Array(this._views$2.length);
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$2);
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            var item = Type.safeCast(view, Open.Core.Lists.IListItemView);
            if (!ss.isNullOrUndefined(item)) {
                list.add(item);
            }
        }
        return list;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListItemView

Open.Core.Lists.ListItemView = function Open_Core_Lists_ListItemView(liElement, model) {
    /// <summary>
    /// Represents a single item within a list.
    /// </summary>
    /// <param name="liElement" type="jQueryObject">
    /// The containing <li></li> element.
    /// </param>
    /// <param name="model" type="Object">
    /// The data model for the list item.
    /// </param>
    /// <field name="propText" type="String" static="true">
    /// </field>
    /// <field name="propIsSelected" type="String" static="true">
    /// </field>
    /// <field name="_model$2" type="Object">
    /// </field>
    /// <field name="_spanLabel$2" type="jQueryObject">
    /// </field>
    /// <field name="_text$2" type="String">
    /// </field>
    /// <field name="_isSelectedPropertyRef$2" type="Open.Core.PropertyRef">
    /// </field>
    Open.Core.Lists.ListItemView.initializeBase(this);
    this._model$2 = model;
    this.initialize(liElement);
    this._isSelectedPropertyRef$2 = Open.Core.PropertyRef.getFromModel(model, Open.Core.Lists.ListItemView.propIsSelected);
    if (this._isSelectedPropertyRef$2 != null) {
        this._isSelectedPropertyRef$2.add_changed(ss.Delegate.create(this, this._onIsSelectedChanged$2));
    }
    this._updateVisualState$2();
}
Open.Core.Lists.ListItemView.prototype = {
    _model$2: null,
    _spanLabel$2: null,
    _text$2: null,
    _isSelectedPropertyRef$2: null,
    
    onDisposed: function Open_Core_Lists_ListItemView$onDisposed() {
        if (this._isSelectedPropertyRef$2 != null) {
            this._isSelectedPropertyRef$2.remove_changed(ss.Delegate.create(this, this._onIsSelectedChanged$2));
        }
        Open.Core.Lists.ListItemView.callBaseMethod(this, 'onDisposed');
    },
    
    _onIsSelectedChanged$2: function Open_Core_Lists_ListItemView$_onIsSelectedChanged$2(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._updateVisualState$2();
        this.firePropertyChanged(Open.Core.Lists.ListItemView.propIsSelected);
    },
    
    get_isSelected: function Open_Core_Lists_ListItemView$get_isSelected() {
        /// <summary>
        /// Gets or sets whether the item is currently selected.
        /// </summary>
        /// <value type="Boolean"></value>
        return (this._isSelectedPropertyRef$2 == null) ? false : this._isSelectedPropertyRef$2.get_value();
    },
    set_isSelected: function Open_Core_Lists_ListItemView$set_isSelected(value) {
        /// <summary>
        /// Gets or sets whether the item is currently selected.
        /// </summary>
        /// <value type="Boolean"></value>
        if (value === this.get_isSelected()) {
            return;
        }
        if (this._isSelectedPropertyRef$2 != null) {
            this._isSelectedPropertyRef$2.set_value(value);
        }
        return value;
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
        if (this._spanLabel$2 != null) {
            this._spanLabel$2.html(this._text$2);
        }
        return value;
    },
    
    onInitialize: function Open_Core_Lists_ListItemView$onInitialize(container) {
        /// <summary>
        /// Initializes the list-item.
        /// </summary>
        /// <param name="container" type="jQueryObject">
        /// The containing <li></li> element.
        /// </param>
        this._spanLabel$2 = Open.Core.Html.createElement(Open.Core.Html.span);
        this._spanLabel$2.appendTo(container);
        container.addClass(Open.Core.Lists.ListCss.classes.listItem);
        this._spanLabel$2.addClass(Open.Core.Lists.ListCss.classes.itemLabel);
        this._spanLabel$2.addClass(Open.Core.Css.classes.titleFont);
        this._setupBindings$2(Type.safeCast(this.get_model(), Open.Core.IModel));
    },
    
    _updateVisualState$2: function Open_Core_Lists_ListItemView$_updateVisualState$2() {
        if (this.get_isSelected()) {
            this.get_container().addClass(Open.Core.Lists.ListCss.classes.selectedListItem);
        }
        else {
            this.get_container().removeClass(Open.Core.Lists.ListCss.classes.selectedListItem);
        }
    },
    
    _setupBindings$2: function Open_Core_Lists_ListItemView$_setupBindings$2(bindable) {
        /// <param name="bindable" type="Open.Core.IModel">
        /// </param>
        if (bindable == null) {
            return;
        }
        this._setBinding$2(bindable, Open.Core.Lists.ListItemView.propText);
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


Open.Core.Lists.ListCss.registerClass('Open.Core.Lists.ListCss');
Open.Core.Lists.ListCssClasses.registerClass('Open.Core.Lists.ListCssClasses');
Open.Core.Lists._listTreePanel.registerClass('Open.Core.Lists._listTreePanel', Open.Core.ViewBase);
Open.Core.Lists.ListTreeView.registerClass('Open.Core.Lists.ListTreeView', Open.Core.ViewBase);
Open.Core.Lists.ListView.registerClass('Open.Core.Lists.ListView', Open.Core.ViewBase);
Open.Core.Lists.ListItemView.registerClass('Open.Core.Lists.ListItemView', Open.Core.ViewBase, Open.Core.Lists.IListItemView);
Open.Core.Lists._listItemFactory.registerClass('Open.Core.Lists._listItemFactory', null, Open.Core.IViewFactory);
Open.Core.Lists.ListCss.url = '/Open.Core/Css/Core.Lists.css';
Open.Core.Lists.ListCss._isCssInserted = false;
Open.Core.Lists.ListCss.classes = new Open.Core.Lists.ListCssClasses();
Open.Core.Lists.ListTreeView.propRootNode = 'RootNode';
Open.Core.Lists.ListTreeView.propSelectedNode = 'SelectedNode';
Open.Core.Lists.ListTreeView.propCurrentListRoot = 'CurrentListRoot';
Open.Core.Lists.ListItemView.propText = 'Text';
Open.Core.Lists.ListItemView.propIsSelected = 'IsSelected';

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Lists', ['Open.Core.Script'], executeScript);
})();
