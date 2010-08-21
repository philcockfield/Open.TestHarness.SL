//! Open.Core.Lists.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core.Lists');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.List

Open.Core.Lists.List = function Open_Core_Lists_List(element) {
    /// <summary>
    /// Renders a simple list.
    /// </summary>
    /// <param name="element" type="jQueryObject">
    /// </param>
    /// <field name="_itemFactory$1" type="Open.Core.IViewFactory">
    /// </field>
    /// <field name="_defaultItemFactory$1" type="Open.Core.IViewFactory">
    /// </field>
    /// <field name="_listElement$1" type="jQueryObject">
    /// </field>
    Open.Core.Lists.List.initializeBase(this);
    this.initialize(element);
}
Open.Core.Lists.List.prototype = {
    _itemFactory$1: null,
    _defaultItemFactory$1: null,
    _listElement$1: null,
    
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
    
    onInitialize: function Open_Core_Lists_List$onInitialize(element) {
        /// <param name="element" type="jQueryObject">
        /// </param>
        this._listElement$1 = element.append('<ul></ul>');
    },
    
    load: function Open_Core_Lists_List$load(models) {
        /// <summary>
        /// Loads the collection of models into the list.
        /// </summary>
        /// <param name="models" type="ss.IEnumerable">
        /// A collection models.
        /// </param>
        this._listElement$1.empty();
        if (models == null) {
            return;
        }
        var factory = this.get_itemFactory();
        var $enum1 = ss.IEnumerator.getEnumerator(models);
        while ($enum1.moveNext()) {
            var model = $enum1.get_current();
            var li = $(String.format('<li></li>'));
            this._listElement$1.append(li);
            var view = factory.createView(li, model);
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Lists.ListItem

Open.Core.Lists.ListItem = function Open_Core_Lists_ListItem(liElement, model) {
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
    Open.Core.Lists.ListItem.initializeBase(this);
    this._model$1 = model;
    this.initialize(liElement);
}
Open.Core.Lists.ListItem.prototype = {
    _model$1: null,
    
    onInitialize: function Open_Core_Lists_ListItem$onInitialize(liElement) {
        /// <summary>
        /// Initializes the list-item.
        /// </summary>
        /// <param name="liElement" type="jQueryObject">
        /// The containing <li></li> element.
        /// </param>
        var text = this._model$1.get_text();
        liElement.append(String.format('<span>{0}</span>', (ss.isNullOrUndefined(text)) ? 'No Text' : text));
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
        return new Open.Core.Lists.ListItem(liElement, model);
    }
}


Open.Core.Lists.List.registerClass('Open.Core.Lists.List', Open.Core.ViewBase);
Open.Core.Lists.ListItem.registerClass('Open.Core.Lists.ListItem', Open.Core.ViewBase);
Open.Core.Lists._listItemFactory.registerClass('Open.Core.Lists._listItemFactory', null, Open.Core.IViewFactory);

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Lists', ['Open.Core.Script'], executeScript);
})();
