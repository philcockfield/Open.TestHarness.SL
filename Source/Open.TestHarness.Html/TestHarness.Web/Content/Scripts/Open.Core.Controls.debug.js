//! Open.Core.Controls.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core.Controls');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Controls.LogView

Open.Core.Controls.LogView = function Open_Core_Controls_LogView(container) {
    /// <param name="container" type="jQueryObject">
    /// The container of the log
    /// </param>
    /// <field name="_divList$2" type="jQueryObject">
    /// </field>
    /// <field name="_divRow$2" type="jQueryObject">
    /// </field>
    /// <field name="_counter$2" type="Number" integer="true">
    /// </field>
    /// <field name="_scrollDuration$2" type="Number">
    /// </field>
    /// <field name="_scrollDelay$2" type="Open.Core.DelayedAction">
    /// </field>
    Open.Core.Controls.LogView.initializeBase(this);
    this.initialize(container);
    this._scrollDelay$2 = new Open.Core.DelayedAction(0.05, ss.Delegate.create(this, this._onScrollDelayElapsed$2));
    $(window).bind(Open.Core.DomEvents.resize, ss.Delegate.create(this, function(e) {
        this.updateLayout();
    }));
    Open.Core.GlobalEvents.add_horizontalPanelResized(ss.Delegate.create(this, function() {
        this.updateLayout();
    }));
    Open.Core.GlobalEvents.add_verticalPanelResized(ss.Delegate.create(this, function() {
        this._scrollDelay$2.start();
    }));
    this.updateLayout();
}
Open.Core.Controls.LogView.prototype = {
    _divList$2: null,
    _divRow$2: null,
    _counter$2: 0,
    _scrollDuration$2: 0,
    _scrollDelay$2: null,
    
    onDisposed: function Open_Core_Controls_LogView$onDisposed() {
        this.clear();
        this._scrollDelay$2.dispose();
        Open.Core.Controls.LogView.callBaseMethod(this, 'onDisposed');
    },
    
    _onScrollDelayElapsed$2: function Open_Core_Controls_LogView$_onScrollDelayElapsed$2() {
        if (this._divRow$2 == null) {
            return;
        }
        this.updateLayout();
        Open.Core.Helper.get_scroll().toBottom(this._divList$2, this.get_scrollDuration(), 'swing', null);
    },
    
    get_scrollDuration: function Open_Core_Controls_LogView$get_scrollDuration() {
        /// <summary>
        /// Gets or sets the scroll duration (in seconds) used when scrolling to the bottom.
        /// </summary>
        /// <value type="Number"></value>
        return this._scrollDuration$2;
    },
    set_scrollDuration: function Open_Core_Controls_LogView$set_scrollDuration(value) {
        /// <summary>
        /// Gets or sets the scroll duration (in seconds) used when scrolling to the bottom.
        /// </summary>
        /// <value type="Number"></value>
        this._scrollDuration$2 = value;
        return value;
    },
    
    onInitialize: function Open_Core_Controls_LogView$onInitialize(container) {
        /// <param name="container" type="jQueryObject">
        /// </param>
        this._divList$2 = container.children(Open.Core.LogCss.list).first();
        Open.Core.Controls.LogView.callBaseMethod(this, 'onInitialize', [ container ]);
    },
    
    insert: function Open_Core_Controls_LogView$insert(message, cssClass) {
        /// <param name="message" type="String">
        /// </param>
        /// <param name="cssClass" type="String">
        /// </param>
        this._counter$2++;
        if (message == null) {
            message = '<null>'.htmlEncode();
        }
        if (message === String.Empty) {
            message = '<empty-string>'.htmlEncode();
        }
        if (String.isNullOrEmpty(message.trim())) {
            message = '<whitespace>'.htmlEncode();
        }
        this._divRow$2 = Open.Core.Html.createDiv();
        this._divRow$2.addClass(cssClass);
        this._divRow$2.addClass(Open.Core.LogCss.listItemClass);
        this._divRow$2.attr(Open.Core.Html.id, Open.Core.Helper.createId());
        var spanCounter = Open.Core.Html.createSpan();
        spanCounter.addClass(Open.Core.LogCss.counterClass);
        spanCounter.append(this._counter$2.toString());
        var divMessage = Open.Core.Html.createDiv();
        divMessage.addClass(Open.Core.LogCss.messageClass);
        divMessage.append(message);
        spanCounter.appendTo(this._divRow$2);
        divMessage.appendTo(this._divRow$2);
        this._divRow$2.appendTo(this._divList$2);
        this._scrollDelay$2.start();
    },
    
    lineBreak: function Open_Core_Controls_LogView$lineBreak() {
        if (this._divRow$2 == null) {
            return;
        }
        this._divRow$2.addClass(Open.Core.LogCss.lineBreakClass);
    },
    
    clear: function Open_Core_Controls_LogView$clear() {
        this._scrollDelay$2.stop();
        this._counter$2 = 0;
        this._divRow$2 = null;
        this._divList$2.html(String.Empty);
    },
    
    updateLayout: function Open_Core_Controls_LogView$updateLayout() {
        /// <summary>
        /// Updates the visual layout.
        /// </summary>
        if (!this.get_isInitialized()) {
            return;
        }
        this._divList$2.width(this.get_container().width());
    }
}


Open.Core.Controls.LogView.registerClass('Open.Core.Controls.LogView', Open.Core.ViewBase, Open.Core.ILogView);

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Views', ['Open.Core.Script'], executeScript);
})();
