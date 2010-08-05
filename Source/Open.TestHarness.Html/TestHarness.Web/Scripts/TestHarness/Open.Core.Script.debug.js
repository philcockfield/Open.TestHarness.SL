//! Open.Core.Script.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Css

Open.Core.Css = function Open_Core_Css() {
    /// <summary>
    /// CSS constants.
    /// </summary>
    /// <field name="top" type="String" static="true">
    /// </field>
    /// <field name="bottom" type="String" static="true">
    /// </field>
    /// <field name="width" type="String" static="true">
    /// </field>
    /// <field name="height" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Events

Open.Core.Events = function Open_Core_Events() {
    /// <field name="resize" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.DelegateUtil

Open.Core.DelegateUtil = function Open_Core_DelegateUtil() {
}
Open.Core.DelegateUtil.toCallbackString = function Open_Core_DelegateUtil$toCallbackString(callback) {
    /// <summary>
    /// Formats a callback function to a JavaScript function name.
    /// </summary>
    /// <param name="callback" type="ss.Delegate">
    /// The callback delegate.
    /// </param>
    /// <returns type="String"></returns>
    return 'ss.Delegate.' + ss.Delegate.createExport(callback, true);
}
Open.Core.DelegateUtil.toEventCallbackString = function Open_Core_DelegateUtil$toEventCallbackString(callback, eventIdentifier) {
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
    var func = String.format('{0}(\'{1}\');', Open.Core.DelegateUtil.toCallbackString(callback), eventIdentifier);
    return 'function(e,ui){ ' + func + ' }';
}


Type.registerNamespace('Open.Core.UI');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.UI.HorizontalPanelResizer

Open.Core.UI.HorizontalPanelResizer = function Open_Core_UI_HorizontalPanelResizer(panelId) {
    /// <summary>
    /// Controls the resizing of a panel on the X plane.
    /// </summary>
    /// <param name="panelId" type="String">
    /// The unique identifier of the panel being resized.
    /// </param>
    /// <field name="_minWidth$1" type="Number">
    /// </field>
    /// <field name="_maxWidthMargin$1" type="Number">
    /// </field>
    Open.Core.UI.HorizontalPanelResizer.initializeBase(this, [ panelId ]);
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
    
    getHandles: function Open_Core_UI_HorizontalPanelResizer$getHandles() {
        /// <returns type="String"></returns>
        return 'e';
    },
    
    onInitialize: function Open_Core_UI_HorizontalPanelResizer$onInitialize() {
        this._setMinMaxWidth$1();
    },
    
    onStopped: function Open_Core_UI_HorizontalPanelResizer$onStopped() {
        var panel = this.getPanel();
        panel.css(Open.Core.Css.height, String.Empty);
    },
    
    onWindowSizeChanged: function Open_Core_UI_HorizontalPanelResizer$onWindowSizeChanged() {
        if (this.isInitialized) {
            this._setMinMaxWidth$1();
        }
    },
    
    _setMinMaxWidth$1: function Open_Core_UI_HorizontalPanelResizer$_setMinMaxWidth$1() {
        this._setMinWidth$1();
        this._setMaxWidth$1();
    },
    
    _setMinWidth$1: function Open_Core_UI_HorizontalPanelResizer$_setMinWidth$1() {
        this.setResizeOption('minWidth', this.get_minWidth());
    },
    
    _setMaxWidth$1: function Open_Core_UI_HorizontalPanelResizer$_setMaxWidth$1() {
        var width = (this.get_hasRootContainer()) ? (this.getRootContainer().width() - this.get_maxWidthMargin()).toString() : String.Empty;
        this.setResizeOption('maxWidth', width);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.UI.PanelResizerBase

Open.Core.UI.PanelResizerBase = function Open_Core_UI_PanelResizerBase(panelId) {
    /// <summary>
    /// Base class for resizing panels.
    /// </summary>
    /// <param name="panelId" type="String">
    /// The unique identifier of the panel being resized.
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
    /// <field name="panelId" type="String">
    /// </field>
    /// <field name="isInitialized" type="Boolean">
    /// </field>
    /// <field name="_resizeScript" type="String" static="true">
    /// </field>
    this.panelId = panelId;
    $(window).bind(Open.Core.Events.resize, ss.Delegate.create(this, function(e) {
        this.onWindowSizeChanged();
    }));
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
    
    _fireResized: function Open_Core_UI_PanelResizerBase$_fireResized() {
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
    panelId: null,
    isInitialized: false,
    
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
        this._rootContainerId = value;
        return value;
    },
    
    get_hasRootContainer: function Open_Core_UI_PanelResizerBase$get_hasRootContainer() {
        /// <value type="Boolean"></value>
        return !String.isNullOrEmpty(this.get_rootContainerId());
    },
    
    initialize: function Open_Core_UI_PanelResizerBase$initialize() {
        /// <summary>
        /// Sets the panel up to be resizable.
        /// </summary>
        var eventCallback = ss.Delegate.create(this, function(eventName) {
            this._handleEvent(eventName);
        });
        var script = String.format(Open.Core.UI.PanelResizerBase._resizeScript, this.panelId, this.getHandles(), Open.Core.DelegateUtil.toEventCallbackString(eventCallback, Open.Core.UI.PanelResizerBase._eventStart), Open.Core.DelegateUtil.toEventCallbackString(eventCallback, Open.Core.UI.PanelResizerBase._eventStop), Open.Core.DelegateUtil.toEventCallbackString(eventCallback, Open.Core.UI.PanelResizerBase._eventResize));
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
    
    getPanel: function Open_Core_UI_PanelResizerBase$getPanel() {
        /// <returns type="jQueryObject"></returns>
        return $(this.panelId);
    },
    
    getRootContainer: function Open_Core_UI_PanelResizerBase$getRootContainer() {
        /// <returns type="jQueryObject"></returns>
        return (this.get_hasRootContainer()) ? $(this.get_rootContainerId()) : null;
    },
    
    setResizeOption: function Open_Core_UI_PanelResizerBase$setResizeOption(option, value) {
        /// <param name="option" type="String">
        /// </param>
        /// <param name="value" type="Object">
        /// </param>
        var script = String.format('$(\'{0}\').resizable(\'option\', \'{1}\', {2});', this.panelId, option, value);
        eval(script);
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
            this._fireResized();
        }
        else if (eventName === Open.Core.UI.PanelResizerBase._eventStop) {
            this.onStopped();
            this._fireResizeStopped();
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.UI.VerticalPanelResizer

Open.Core.UI.VerticalPanelResizer = function Open_Core_UI_VerticalPanelResizer(panelId) {
    /// <summary>
    /// Controls the resizing of a panel on the Y plane.
    /// </summary>
    /// <param name="panelId" type="String">
    /// The unique identifier of the panel being resized.
    /// </param>
    /// <field name="_minHeight$1" type="Number">
    /// </field>
    /// <field name="_maxHeightMargin$1" type="Number">
    /// </field>
    Open.Core.UI.VerticalPanelResizer.initializeBase(this, [ panelId ]);
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
    
    getHandles: function Open_Core_UI_VerticalPanelResizer$getHandles() {
        /// <returns type="String"></returns>
        return 'n';
    },
    
    onInitialize: function Open_Core_UI_VerticalPanelResizer$onInitialize() {
        this._setMinMaxHeight$1();
    },
    
    onStopped: function Open_Core_UI_VerticalPanelResizer$onStopped() {
        var panel = this.getPanel();
        panel.css(Open.Core.Css.width, String.Empty);
        panel.css(Open.Core.Css.top, String.Empty);
    },
    
    onWindowSizeChanged: function Open_Core_UI_VerticalPanelResizer$onWindowSizeChanged() {
        if (this.isInitialized) {
            this._setMinMaxHeight$1();
        }
    },
    
    _setMinMaxHeight$1: function Open_Core_UI_VerticalPanelResizer$_setMinMaxHeight$1() {
        this._setMinHeight$1();
        this._setMaxHeight$1();
    },
    
    _setMinHeight$1: function Open_Core_UI_VerticalPanelResizer$_setMinHeight$1() {
        this.setResizeOption('minHeight', this.get_minHeight());
    },
    
    _setMaxHeight$1: function Open_Core_UI_VerticalPanelResizer$_setMaxHeight$1() {
        var width = (this.get_hasRootContainer()) ? (this.getRootContainer().height() - this.get_maxHeightMargin()).toString() : String.Empty;
        this.setResizeOption('maxHeight', width);
    }
}


Open.Core.Css.registerClass('Open.Core.Css');
Open.Core.Events.registerClass('Open.Core.Events');
Open.Core.DelegateUtil.registerClass('Open.Core.DelegateUtil');
Open.Core.UI.PanelResizerBase.registerClass('Open.Core.UI.PanelResizerBase');
Open.Core.UI.HorizontalPanelResizer.registerClass('Open.Core.UI.HorizontalPanelResizer', Open.Core.UI.PanelResizerBase);
Open.Core.UI.VerticalPanelResizer.registerClass('Open.Core.UI.VerticalPanelResizer', Open.Core.UI.PanelResizerBase);
Open.Core.Css.top = 'top';
Open.Core.Css.bottom = 'bottom';
Open.Core.Css.width = 'width';
Open.Core.Css.height = 'height';
Open.Core.Events.resize = 'resize';
Open.Core.UI.PanelResizerBase._eventStart = 'start';
Open.Core.UI.PanelResizerBase._eventStop = 'eventStop';
Open.Core.UI.PanelResizerBase._eventResize = 'eventResize';
Open.Core.UI.PanelResizerBase._resizeScript = '\r\n$(\'{0}\').resizable({\r\n    handles: \'{1}\',\r\n    start: {2},\r\n    stop: {3},\r\n    resize: {4}\r\n    });\r\n';

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Script', [], executeScript);
})();
