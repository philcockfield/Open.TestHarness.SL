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
// Open.Core.Lists.List

Open.Core.Lists.List = function Open_Core_Lists_List(container) {
    /// <summary>
    /// Renders a simple list.
    /// </summary>
    /// <param name="container" type="jQueryObject">
    /// The containing element.
    /// </param>
    /// <field name="__selectionChanged$1" type="EventHandler">
    /// </field>
    /// <field name="_itemFactory$1" type="Open.Core.IViewFactory">
    /// </field>
    /// <field name="_defaultItemFactory$1" type="Open.Core.IViewFactory">
    /// </field>
    /// <field name="_selectionMode$1" type="Open.Core.Lists.ListSelectionMode">
    /// </field>
    /// <field name="_views$1" type="Array">
    /// </field>
    this._selectionMode$1 = Open.Core.Lists.ListSelectionMode.single;
    this._views$1 = [];
    Open.Core.Lists.List.initializeBase(this);
    this.initialize(container);
    Open.Core.Lists.ListCss.insertCss();
}
Open.Core.Lists.List.prototype = {
    
    add_selectionChanged: function Open_Core_Lists_List$add_selectionChanged(value) {
        /// <summary>
        /// Fires when the item selection changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectionChanged$1 = ss.Delegate.combine(this.__selectionChanged$1, value);
    },
    remove_selectionChanged: function Open_Core_Lists_List$remove_selectionChanged(value) {
        /// <summary>
        /// Fires when the item selection changes.
        /// </summary>
        /// <param name="value" type="Function" />
        this.__selectionChanged$1 = ss.Delegate.remove(this.__selectionChanged$1, value);
    },
    
    __selectionChanged$1: null,
    
    _fireSelectionChanged$1: function Open_Core_Lists_List$_fireSelectionChanged$1() {
        if (this.__selectionChanged$1 != null) {
            this.__selectionChanged$1.invoke(this, new ss.EventArgs());
        }
    },
    
    _itemFactory$1: null,
    _defaultItemFactory$1: null,
    
    _onItemClick$1: function Open_Core_Lists_List$_onItemClick$1(e, view) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        /// <param name="view" type="Open.Core.IView">
        /// </param>
        if (this.get_selectionMode() === Open.Core.Lists.ListSelectionMode.none) {
            return;
        }
        this._selectItem$1(Type.safeCast(view, Open.Core.Lists.IListItemView));
    },
    
    get_itemFactory: function Open_Core_Lists_List$get_itemFactory() {
        /// <summary>
        /// Gets or sets the factory that creates each item in the list.
        /// </summary>
        /// <value type="Open.Core.IViewFactory"></value>
        return this._itemFactory$1 || (this._defaultItemFactory$1 || (this._defaultItemFactory$1 = new Open.Core.Lists._listItemFactory()));
    },
    set_itemFactory: function Open_Core_Lists_List$set_itemFactory(value) {
        /// <summary>
        /// Gets or sets the factory that creates each item in the list.
        /// </summary>
        /// <value type="Open.Core.IViewFactory"></value>
        this._itemFactory$1 = value;
        return value;
    },
    
    get_selectionMode: function Open_Core_Lists_List$get_selectionMode() {
        /// <summary>
        /// Gets or sets whether items within the list are selecable.
        /// </summary>
        /// <value type="Open.Core.Lists.ListSelectionMode"></value>
        return this._selectionMode$1;
    },
    set_selectionMode: function Open_Core_Lists_List$set_selectionMode(value) {
        /// <summary>
        /// Gets or sets whether items within the list are selecable.
        /// </summary>
        /// <value type="Open.Core.Lists.ListSelectionMode"></value>
        this._selectionMode$1 = value;
        return value;
    },
    
    get_count: function Open_Core_Lists_List$get_count() {
        /// <summary>
        /// Gets the number of items currently in the list.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this._views$1.length;
    },
    
    onInitialize: function Open_Core_Lists_List$onInitialize(container) {
        /// <param name="container" type="jQueryObject">
        /// </param>
    },
    
    load: function Open_Core_Lists_List$load(items) {
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
            this._views$1.add(view);
            div.click(ss.Delegate.create(this, function(e) {
                this._onItemClick$1(e, view);
            }));
        }));
    },
    
    clear: function Open_Core_Lists_List$clear() {
        /// <summary>
        /// Clears the list (disposing of all children).
        /// </summary>
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$1);
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            view.dispose();
        }
        this.get_container().empty();
        this._views$1.clear();
    },
    
    select: function Open_Core_Lists_List$select(model) {
        /// <summary>
        /// Selects the item corresponding to the given model.
        /// </summary>
        /// <param name="model" type="Object">
        /// The item's model.
        /// </param>
        if (ss.isNullOrUndefined(model)) {
            return;
        }
        this._selectItem$1(this._getListItem$1(model));
        this._fireSelectionChanged$1();
    },
    
    selectFirst: function Open_Core_Lists_List$selectFirst() {
        /// <summary>
        /// Selects the first item in the list.
        /// </summary>
        this.selectIndex(0);
    },
    
    selectLast: function Open_Core_Lists_List$selectLast() {
        /// <summary>
        /// Selects the first item in the list.
        /// </summary>
        if (this._views$1.length > 0) {
            this.selectIndex(this._views$1.length - 1);
        }
    },
    
    selectIndex: function Open_Core_Lists_List$selectIndex(index) {
        /// <summary>
        /// Selects the list item at the specified index.
        /// </summary>
        /// <param name="index" type="Number" integer="true">
        /// The index of the item to select (0-based).
        /// </param>
        if (this._views$1.length === 0 || index > this._views$1.length - 1 || index < 0) {
            return;
        }
        this._selectItem$1(Type.safeCast(this._views$1[index], Open.Core.Lists.IListItemView));
    },
    
    _selectItem$1: function Open_Core_Lists_List$_selectItem$1(item) {
        /// <param name="item" type="Open.Core.Lists.IListItemView">
        /// </param>
        if (ss.isNullOrUndefined(item)) {
            return;
        }
        this._clearSelection$1();
        item.set_isSelected(true);
    },
    
    _clearSelection$1: function Open_Core_Lists_List$_clearSelection$1() {
        var $enum1 = ss.IEnumerator.getEnumerator(this._getItems$1());
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            item.set_isSelected(false);
        }
    },
    
    _getItems$1: function Open_Core_Lists_List$_getItems$1() {
        /// <returns type="ss.IEnumerable"></returns>
        var list = new Array(this._views$1.length);
        var $enum1 = ss.IEnumerator.getEnumerator(this._views$1);
        while ($enum1.moveNext()) {
            var view = $enum1.get_current();
            var item = Type.safeCast(view, Open.Core.Lists.IListItemView);
            if (!ss.isNullOrUndefined(item)) {
                list.add(item);
            }
        }
        return list;
    },
    
    _getListItem$1: function Open_Core_Lists_List$_getListItem$1(model) {
        /// <param name="model" type="Object">
        /// </param>
        /// <returns type="Open.Core.Lists.IListItemView"></returns>
        var $enum1 = ss.IEnumerator.getEnumerator(this._getItems$1());
        while ($enum1.moveNext()) {
            var item = $enum1.get_current();
            if (item.get_model() === model) {
                return item;
            }
        }
        return null;
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
    /// <field name="_model$1" type="Object">
    /// </field>
    /// <field name="_isSelected$1" type="Boolean">
    /// </field>
    Open.Core.Lists.ListItemView.initializeBase(this);
    this._model$1 = model;
    this.initialize(liElement);
}
Open.Core.Lists.ListItemView.prototype = {
    _model$1: null,
    _isSelected$1: false,
    
    get_isSelected: function Open_Core_Lists_ListItemView$get_isSelected() {
        /// <summary>
        /// Gets or sets whether the item is currently selected.
        /// </summary>
        /// <value type="Boolean"></value>
        return this._isSelected$1;
    },
    set_isSelected: function Open_Core_Lists_ListItemView$set_isSelected(value) {
        /// <summary>
        /// Gets or sets whether the item is currently selected.
        /// </summary>
        /// <value type="Boolean"></value>
        if (value === this._isSelected$1) {
            return;
        }
        this._isSelected$1 = value;
        this._updateVisualState$1();
        return value;
    },
    
    get_model: function Open_Core_Lists_ListItemView$get_model() {
        /// <summary>
        /// Gets or sets the data model.
        /// </summary>
        /// <value type="Object"></value>
        return this._model$1;
    },
    
    onInitialize: function Open_Core_Lists_ListItemView$onInitialize(container) {
        /// <summary>
        /// Initializes the list-item.
        /// </summary>
        /// <param name="container" type="jQueryObject">
        /// The containing <li></li> element.
        /// </param>
        container.addClass(Open.Core.Lists.ListCss.classes.listItem);
        var text = this._model$1.get_text();
        var html = $(String.format('<span>{0}</span>', (ss.isNullOrUndefined(text)) ? 'No Text' : text));
        html.appendTo(container);
        html.addClass(Open.Core.Lists.ListCss.classes.itemLabel);
        html.addClass(Open.Core.Css.classes.titleFont);
    },
    
    _updateVisualState$1: function Open_Core_Lists_ListItemView$_updateVisualState$1() {
        if (this.get_isSelected()) {
            this.get_container().addClass(Open.Core.Lists.ListCss.classes.selectedListItem);
        }
        else {
            this.get_container().removeClass(Open.Core.Lists.ListCss.classes.selectedListItem);
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
Open.Core.Lists.List.registerClass('Open.Core.Lists.List', Open.Core.ViewBase);
Open.Core.Lists.ListItemView.registerClass('Open.Core.Lists.ListItemView', Open.Core.ViewBase, Open.Core.Lists.IListItemView);
Open.Core.Lists._listItemFactory.registerClass('Open.Core.Lists._listItemFactory', null, Open.Core.IViewFactory);
Open.Core.Lists.ListCss.url = '/Open.Core/Css/Core.Lists.css';
Open.Core.Lists.ListCss._isCssInserted = false;
Open.Core.Lists.ListCss.classes = new Open.Core.Lists.ListCssClasses();

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Lists', ['Open.Core.Script'], executeScript);
})();
