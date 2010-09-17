//! Open.Core.Controls.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core.Controls.Buttons');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Controls.Buttons.ButtonBase

Open.Core.Controls.Buttons.ButtonBase = function Open_Core_Controls_Buttons_ButtonBase(html) {
    /// <summary>
    /// Base class for buttons.
    /// </summary>
    /// <param name="html" type="jQueryObject">
    /// The HTML of the button.
    /// </param>
    /// <field name="__click$2" type="EventHandler">
    /// </field>
    /// <field name="__isPressedChanged$2" type="EventHandler">
    /// </field>
    /// <field name="propCanToggle" type="String" static="true">
    /// </field>
    /// <field name="propMouseState" type="String" static="true">
    /// </field>
    /// <field name="propIsPressed" type="String" static="true">
    /// </field>
    /// <field name="propIsMouseOver" type="String" static="true">
    /// </field>
    /// <field name="propIsMouseDown" type="String" static="true">
    /// </field>
    Open.Core.Controls.Buttons.ButtonBase.initializeBase(this, [ html ]);
    html.mouseover(ss.Delegate.create(this, this._onMouseOver$2));
    html.mouseout(ss.Delegate.create(this, this._onMouseOut$2));
    html.mousedown(ss.Delegate.create(this, this._onMouseDown$2));
    html.mouseup(ss.Delegate.create(this, this._onMouseUp$2));
}
Open.Core.Controls.Buttons.ButtonBase.prototype = {
    
    add_click: function Open_Core_Controls_Buttons_ButtonBase$add_click(value) {
        /// <param name="value" type="Function" />
        this.__click$2 = ss.Delegate.combine(this.__click$2, value);
    },
    remove_click: function Open_Core_Controls_Buttons_ButtonBase$remove_click(value) {
        /// <param name="value" type="Function" />
        this.__click$2 = ss.Delegate.remove(this.__click$2, value);
    },
    
    __click$2: null,
    
    _fireClick$2: function Open_Core_Controls_Buttons_ButtonBase$_fireClick$2() {
        if (this.__click$2 != null) {
            this.__click$2.invoke(this, new ss.EventArgs());
        }
    },
    
    add_isPressedChanged: function Open_Core_Controls_Buttons_ButtonBase$add_isPressedChanged(value) {
        /// <param name="value" type="Function" />
        this.__isPressedChanged$2 = ss.Delegate.combine(this.__isPressedChanged$2, value);
    },
    remove_isPressedChanged: function Open_Core_Controls_Buttons_ButtonBase$remove_isPressedChanged(value) {
        /// <param name="value" type="Function" />
        this.__isPressedChanged$2 = ss.Delegate.remove(this.__isPressedChanged$2, value);
    },
    
    __isPressedChanged$2: null,
    
    _fireIsPressedChanged$2: function Open_Core_Controls_Buttons_ButtonBase$_fireIsPressedChanged$2() {
        if (this.__isPressedChanged$2 != null) {
            this.__isPressedChanged$2.invoke(this, new ss.EventArgs());
        }
    },
    
    _onMouseOver$2: function Open_Core_Controls_Buttons_ButtonBase$_onMouseOver$2(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this.set_isMouseOver(true);
        this._updateMouseState$2();
    },
    
    _onMouseOut$2: function Open_Core_Controls_Buttons_ButtonBase$_onMouseOut$2(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this.set_isMouseOver(false);
        this._updateMouseState$2();
    },
    
    _onMouseDown$2: function Open_Core_Controls_Buttons_ButtonBase$_onMouseDown$2(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this.set_isMouseDown(true);
        this._updateMouseState$2();
    },
    
    _onMouseUp$2: function Open_Core_Controls_Buttons_ButtonBase$_onMouseUp$2(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        var wasMouseDown = this.get_isMouseDown();
        this.set_isMouseDown(false);
        this._updateMouseState$2();
        if (this.get_isEnabled() && this.get_isMouseOver() && wasMouseDown) {
            this.invokeClick(true);
        }
    },
    
    get_canToggle: function Open_Core_Controls_Buttons_ButtonBase$get_canToggle() {
        /// <value type="Boolean"></value>
        return this.get(Open.Core.Controls.Buttons.ButtonBase.propCanToggle, false);
    },
    set_canToggle: function Open_Core_Controls_Buttons_ButtonBase$set_canToggle(value) {
        /// <value type="Boolean"></value>
        this.set(Open.Core.Controls.Buttons.ButtonBase.propCanToggle, value, false);
        return value;
    },
    
    get_mouseState: function Open_Core_Controls_Buttons_ButtonBase$get_mouseState() {
        /// <value type="Open.Core.ButtonMouseState"></value>
        return this.get(Open.Core.Controls.Buttons.ButtonBase.propMouseState, Open.Core.ButtonMouseState.normal);
    },
    set_mouseState: function Open_Core_Controls_Buttons_ButtonBase$set_mouseState(value) {
        /// <value type="Open.Core.ButtonMouseState"></value>
        this.set(Open.Core.Controls.Buttons.ButtonBase.propMouseState, value, Open.Core.ButtonMouseState.normal);
        return value;
    },
    
    get_isPressed: function Open_Core_Controls_Buttons_ButtonBase$get_isPressed() {
        /// <value type="Boolean"></value>
        return this.get(Open.Core.Controls.Buttons.ButtonBase.propIsPressed, false);
    },
    set_isPressed: function Open_Core_Controls_Buttons_ButtonBase$set_isPressed(value) {
        /// <value type="Boolean"></value>
        if (this.set(Open.Core.Controls.Buttons.ButtonBase.propIsPressed, value, false)) {
            this._fireIsPressedChanged$2();
        }
        return value;
    },
    
    get_isMouseOver: function Open_Core_Controls_Buttons_ButtonBase$get_isMouseOver() {
        /// <value type="Boolean"></value>
        return this.get(Open.Core.Controls.Buttons.ButtonBase.propIsMouseOver, false);
    },
    set_isMouseOver: function Open_Core_Controls_Buttons_ButtonBase$set_isMouseOver(value) {
        /// <value type="Boolean"></value>
        this.set(Open.Core.Controls.Buttons.ButtonBase.propIsMouseOver, value, false);
        return value;
    },
    
    get_isMouseDown: function Open_Core_Controls_Buttons_ButtonBase$get_isMouseDown() {
        /// <value type="Boolean"></value>
        return this.get(Open.Core.Controls.Buttons.ButtonBase.propIsMouseDown, false);
    },
    set_isMouseDown: function Open_Core_Controls_Buttons_ButtonBase$set_isMouseDown(value) {
        /// <value type="Boolean"></value>
        this.set(Open.Core.Controls.Buttons.ButtonBase.propIsMouseDown, value, false);
        return value;
    },
    
    invokeClick: function Open_Core_Controls_Buttons_ButtonBase$invokeClick(force) {
        /// <param name="force" type="Boolean">
        /// </param>
        if (!this.get_isEnabled() && !force) {
            return;
        }
        if (this.get_canToggle()) {
            this.set_isPressed(!this.get_isPressed());
        }
        this._fireClick$2();
    },
    
    _updateMouseState$2: function Open_Core_Controls_Buttons_ButtonBase$_updateMouseState$2() {
        if (!this.get_isEnabled()) {
            this.set_mouseState(Open.Core.ButtonMouseState.normal);
        }
        else if (this.get_isMouseOver() && this.get_isMouseDown()) {
            this.set_mouseState(Open.Core.ButtonMouseState.pressed);
        }
        else if (this.get_isMouseOver()) {
            this.set_mouseState(Open.Core.ButtonMouseState.mouseOver);
        }
        else {
            this.set_mouseState(Open.Core.ButtonMouseState.normal);
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Controls.Buttons.SystemButton

Open.Core.Controls.Buttons.SystemButton = function Open_Core_Controls_Buttons_SystemButton() {
    /// <summary>
    /// A simple button that renders with like the native Browser/OS.
    /// </summary>
    /// <field name="untitled" type="String" static="true">
    /// </field>
    /// <field name="defaultPadding" type="String" static="true">
    /// </field>
    /// <field name="propHtml" type="String" static="true">
    /// </field>
    /// <field name="propType" type="String" static="true">
    /// </field>
    /// <field name="propValue" type="String" static="true">
    /// </field>
    /// <field name="propPadding" type="String" static="true">
    /// </field>
    /// <field name="propFontSize" type="String" static="true">
    /// </field>
    /// <field name="_htmButton$3" type="jQueryObject">
    /// </field>
    Open.Core.Controls.Buttons.SystemButton.initializeBase(this, [ Open.Core.Controls.Buttons.SystemButton._initHtml$3() ]);
    this._htmButton$3 = this.get_container();
    this._syncHtml$3();
    this._syncType$3();
    this._syncValue$3();
    this._syncPadding$3();
    this._syncFontSize$3();
    this.add_propertyChanged(ss.Delegate.create(this, this._onPropertyChanged$3));
}
Open.Core.Controls.Buttons.SystemButton._initHtml$3 = function Open_Core_Controls_Buttons_SystemButton$_initHtml$3() {
    /// <returns type="jQueryObject"></returns>
    var htmButton = Open.Core.Html.createElement(Open.Core.Html.button);
    htmButton.attr(Open.Core.Html.type, Open.Core.Html.submit);
    return htmButton;
}
Open.Core.Controls.Buttons.SystemButton.prototype = {
    _htmButton$3: null,
    
    _onPropertyChanged$3: function Open_Core_Controls_Buttons_SystemButton$_onPropertyChanged$3(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Open.Core.PropertyChangedEventArgs">
        /// </param>
        var name = e.get_property().get_name();
        if (name === Open.Core.Controls.Buttons.ButtonBase.propCanToggle && this.get_canToggle()) {
            throw new Error(String.format('[{0}] cannot be made to toggle.', Type.getInstanceType(this).get_name()));
        }
    },
    
    onIsEnabledChanged: function Open_Core_Controls_Buttons_SystemButton$onIsEnabledChanged() {
        if (this.get_isEnabled()) {
            this.get_container().removeAttr(Open.Core.Html.disabled);
        }
        else {
            this.get_container().attr(Open.Core.Html.disabled, true.toString());
        }
        Open.Core.Controls.Buttons.SystemButton.callBaseMethod(this, 'onIsEnabledChanged');
    },
    
    get_html: function Open_Core_Controls_Buttons_SystemButton$get_html() {
        /// <summary>
        /// Gets or sets the HTML content of the button.
        /// </summary>
        /// <value type="String"></value>
        return this.get(Open.Core.Controls.Buttons.SystemButton.propHtml, Open.Core.Controls.Buttons.SystemButton.untitled);
    },
    set_html: function Open_Core_Controls_Buttons_SystemButton$set_html(value) {
        /// <summary>
        /// Gets or sets the HTML content of the button.
        /// </summary>
        /// <value type="String"></value>
        if (this.set(Open.Core.Controls.Buttons.SystemButton.propHtml, value, Open.Core.Controls.Buttons.SystemButton.untitled)) {
            this._syncHtml$3();
        }
        return value;
    },
    
    get_type: function Open_Core_Controls_Buttons_SystemButton$get_type() {
        /// <summary>
        /// Gets or sets the button type (HTML attribute).
        /// </summary>
        /// <value type="String"></value>
        return this.get(Open.Core.Controls.Buttons.SystemButton.propType, Open.Core.Html.submit);
    },
    set_type: function Open_Core_Controls_Buttons_SystemButton$set_type(value) {
        /// <summary>
        /// Gets or sets the button type (HTML attribute).
        /// </summary>
        /// <value type="String"></value>
        if (this.set(Open.Core.Controls.Buttons.SystemButton.propType, value, Open.Core.Html.submit)) {
            this._syncType$3();
        }
        return value;
    },
    
    get_value: function Open_Core_Controls_Buttons_SystemButton$get_value() {
        /// <summary>
        /// Gets or sets the underlying value of a button.
        /// </summary>
        /// <value type="String"></value>
        return this.get(Open.Core.Controls.Buttons.SystemButton.propValue, null);
    },
    set_value: function Open_Core_Controls_Buttons_SystemButton$set_value(value) {
        /// <summary>
        /// Gets or sets the underlying value of a button.
        /// </summary>
        /// <value type="String"></value>
        if (this.set(Open.Core.Controls.Buttons.SystemButton.propValue, value, null)) {
            this._syncValue$3();
        }
        return value;
    },
    
    get_padding: function Open_Core_Controls_Buttons_SystemButton$get_padding() {
        /// <value type="String"></value>
        return this.get(Open.Core.Controls.Buttons.SystemButton.propPadding, Open.Core.Controls.Buttons.SystemButton.defaultPadding);
    },
    set_padding: function Open_Core_Controls_Buttons_SystemButton$set_padding(value) {
        /// <value type="String"></value>
        if (this.set(Open.Core.Controls.Buttons.SystemButton.propPadding, value, Open.Core.Controls.Buttons.SystemButton.defaultPadding)) {
            this._syncPadding$3();
        }
        return value;
    },
    
    get_fontSize: function Open_Core_Controls_Buttons_SystemButton$get_fontSize() {
        /// <value type="String"></value>
        return this.get(Open.Core.Controls.Buttons.SystemButton.propFontSize, null);
    },
    set_fontSize: function Open_Core_Controls_Buttons_SystemButton$set_fontSize(value) {
        /// <value type="String"></value>
        if (this.set(Open.Core.Controls.Buttons.SystemButton.propFontSize, value, null)) {
            this._syncFontSize$3();
        }
        return value;
    },
    
    beforeInsertReplace: function Open_Core_Controls_Buttons_SystemButton$beforeInsertReplace(e) {
        /// <param name="e" type="jQueryObject">
        /// </param>
        this.set_value(e.attr(Open.Core.Html.value));
        this.set_type(e.attr(Open.Core.Html.type));
        this.set_html(e.html());
        Open.Core.Controls.Buttons.SystemButton.callBaseMethod(this, 'beforeInsertReplace', [ e ]);
    },
    
    _syncHtml$3: function Open_Core_Controls_Buttons_SystemButton$_syncHtml$3() {
        this._htmButton$3.html(this.get_html());
        this.fireSizeChanged();
    },
    
    _syncType$3: function Open_Core_Controls_Buttons_SystemButton$_syncType$3() {
        this._htmButton$3.attr(Open.Core.Html.type, this.get_type());
    },
    
    _syncValue$3: function Open_Core_Controls_Buttons_SystemButton$_syncValue$3() {
        this._htmButton$3.attr(Open.Core.Html.value, this.get_value());
    },
    
    _syncPadding$3: function Open_Core_Controls_Buttons_SystemButton$_syncPadding$3() {
        this._htmButton$3.css(Open.Core.Css.padding, this.get_padding());
        this.fireSizeChanged();
    },
    
    _syncFontSize$3: function Open_Core_Controls_Buttons_SystemButton$_syncFontSize$3() {
        this._htmButton$3.css(Open.Core.Css.fontSize, this.get_fontSize());
        this.fireSizeChanged();
    }
}


Type.registerNamespace('Open.Core.Controls.HtmlPrimitive');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Controls.HtmlPrimitive.HtmlList

Open.Core.Controls.HtmlPrimitive.HtmlList = function Open_Core_Controls_HtmlPrimitive_HtmlList(listType, cssClass) {
    /// <summary>
    /// Renders an <UL></UL> or <OL></OL>.
    /// </summary>
    /// <param name="listType" type="Open.Core.HtmlListType">
    /// The type of list to construct.
    /// </param>
    /// <param name="cssClass" type="String">
    /// The CSS class attribute to add to the root list element (can be multiple classes).
    /// </param>
    /// <field name="_listType$2" type="Open.Core.HtmlListType">
    /// </field>
    /// <field name="_items$2" type="Array">
    /// </field>
    this._items$2 = [];
    Open.Core.Controls.HtmlPrimitive.HtmlList.initializeBase(this, [ Open.Core.Html.createElement((listType === Open.Core.HtmlListType.unordered) ? 'ul' : 'ol') ]);
    this._listType$2 = listType;
    Open.Core.Css.addClasses(this.get_container(), cssClass);
}
Open.Core.Controls.HtmlPrimitive.HtmlList.prototype = {
    _listType$2: 0,
    
    get_listType: function Open_Core_Controls_HtmlPrimitive_HtmlList$get_listType() {
        /// <summary>
        /// Gets the type of list to construct.
        /// </summary>
        /// <value type="Open.Core.HtmlListType"></value>
        return this._listType$2;
    },
    
    get_count: function Open_Core_Controls_HtmlPrimitive_HtmlList$get_count() {
        /// <summary>
        /// Gets the number of items within the list.
        /// </summary>
        /// <value type="Number" integer="true"></value>
        return this.get_container().children().length;
    },
    
    get_isEmpty: function Open_Core_Controls_HtmlPrimitive_HtmlList$get_isEmpty() {
        /// <summary>
        /// Gets whether the list is empty.
        /// </summary>
        /// <value type="Boolean"></value>
        return this.get_count() === 0;
    },
    
    get_first: function Open_Core_Controls_HtmlPrimitive_HtmlList$get_first() {
        /// <value type="jQueryObject"></value>
        return this.get_item(0);
    },
    
    get_last: function Open_Core_Controls_HtmlPrimitive_HtmlList$get_last() {
        /// <value type="jQueryObject"></value>
        return this.get_item(this.get_count() - 1);
    },
    
    add: function Open_Core_Controls_HtmlPrimitive_HtmlList$add(text, cssClass) {
        /// <summary>
        /// Adds a new list item <li></li>.
        /// </summary>
        /// <param name="text" type="String">
        /// The text to insert within the element.
        /// </param>
        /// <param name="cssClass" type="String">
        /// The class(es) to apply to the LI.
        /// </param>
        /// <returns type="jQueryObject"></returns>
        var li = Open.Core.Html.createElement('li');
        if (!String.isNullOrEmpty(text)) {
            li.append(String.format('<p>{0}</p>', text));
        }
        Open.Core.Css.addClasses(li, cssClass);
        li.appendTo(this.get_container());
        this.fireSizeChanged();
        return li;
    },
    
    remove: function Open_Core_Controls_HtmlPrimitive_HtmlList$remove(index) {
        /// <summary>
        /// Removes the item at the given index.
        /// </summary>
        /// <param name="index" type="Number" integer="true">
        /// The index to remove.
        /// </param>
        var item = this.get_item(index);
        if (item == null) {
            return;
        }
        item.remove();
        this.fireSizeChanged();
    },
    
    clear: function Open_Core_Controls_HtmlPrimitive_HtmlList$clear() {
        /// <summary>
        /// Removes all child LI items.
        /// </summary>
        if (this.get_isEmpty()) {
            return;
        }
        do {
            this.remove(0);
        } while (!this.get_isEmpty());
    },
    get_item: function Open_Core_Controls_HtmlPrimitive_HtmlList$get_item(index) {
        /// <summary>
        /// Gets the list-item at the given index.
        /// </summary>
        /// <param name="index" type="Number" integer="true">
        /// </param>
        /// <param name="value" type="jQueryObject">
        /// </param>
        /// <returns type="jQueryObject"></returns>
        if (index < 0) {
            index = 0;
        }
        if (this.get_isEmpty() || index >= this.get_count()) {
            return null;
        }
        return $(this.get_container().children().get(index));
    }
}


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
    Open.Core.Controls.LogView.initializeBase(this, [ container ]);
    this._scrollDelay$2 = new Open.Core.DelayedAction(0.05, ss.Delegate.create(this, this._onScrollDelayElapsed$2));
    this._divList$2 = container.children(Open.Core.LogCss.list).first();
    Open.Core.GlobalEvents.add_windowResize(ss.Delegate.create(this, function() {
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
        this._divRow$2 = this._createRowDiv$2(cssClass);
        var spanCounter = Open.Core.Html.createSpan();
        spanCounter.addClass(Open.Core.LogCss.counterClass);
        spanCounter.append(this._counter$2.toString());
        var divMessage = Open.Core.Html.createDiv();
        divMessage.addClass(Open.Core.LogCss.messageClass);
        divMessage.append(message);
        spanCounter.appendTo(this._divRow$2);
        divMessage.appendTo(this._divRow$2);
        this._insertRow$2(this._divRow$2);
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
        this._divList$2.width(this.get_container().width());
    },
    
    divider: function Open_Core_Controls_LogView$divider(type) {
        /// <summary>
        /// Inserts a visual divider into the log.
        /// </summary>
        /// <param name="type" type="Open.Core.LogDivider">
        /// The type of divider to insert.
        /// </param>
        switch (type) {
            case Open.Core.LogDivider.lineBreak:
                this._lineBreak$2();
                break;
            case Open.Core.LogDivider.section:
                this._insertSectionDivider$2();
                break;
            default:
                throw new Error('Not supporred: ' + Open.Core.LogDivider.toString(type));
        }
    },
    
    _lineBreak$2: function Open_Core_Controls_LogView$_lineBreak$2() {
        if (this._divRow$2 == null) {
            return;
        }
        this._divRow$2.addClass(Open.Core.LogCss.lineBreakClass);
    },
    
    _insertSectionDivider$2: function Open_Core_Controls_LogView$_insertSectionDivider$2() {
        this._insertRow$2(this._createRowDiv$2(Open.Core.LogCss.sectionBreak));
    },
    
    _createRowDiv$2: function Open_Core_Controls_LogView$_createRowDiv$2(cssClass) {
        /// <param name="cssClass" type="String">
        /// </param>
        /// <returns type="jQueryObject"></returns>
        var div = Open.Core.Html.createDiv();
        div.addClass(cssClass);
        div.addClass(Open.Core.LogCss.listItemClass);
        return div;
    },
    
    _insertRow$2: function Open_Core_Controls_LogView$_insertRow$2(div) {
        /// <param name="div" type="jQueryObject">
        /// </param>
        div.appendTo(this._divList$2);
        this._scrollDelay$2.start();
    }
}


Open.Core.Controls.Buttons.ButtonBase.registerClass('Open.Core.Controls.Buttons.ButtonBase', Open.Core.ViewBase, Open.Core.IButton);
Open.Core.Controls.Buttons.SystemButton.registerClass('Open.Core.Controls.Buttons.SystemButton', Open.Core.Controls.Buttons.ButtonBase);
Open.Core.Controls.HtmlPrimitive.HtmlList.registerClass('Open.Core.Controls.HtmlPrimitive.HtmlList', Open.Core.ViewBase);
Open.Core.Controls.LogView.registerClass('Open.Core.Controls.LogView', Open.Core.ViewBase, Open.Core.ILogView);
Open.Core.Controls.Buttons.ButtonBase.propCanToggle = 'CanToggle';
Open.Core.Controls.Buttons.ButtonBase.propMouseState = 'MouseState';
Open.Core.Controls.Buttons.ButtonBase.propIsPressed = 'IsPressed';
Open.Core.Controls.Buttons.ButtonBase.propIsMouseOver = 'IsMouseOver';
Open.Core.Controls.Buttons.ButtonBase.propIsMouseDown = 'IsMouseDown';
Open.Core.Controls.Buttons.SystemButton.untitled = 'Untitled';
Open.Core.Controls.Buttons.SystemButton.defaultPadding = '5px 30px';
Open.Core.Controls.Buttons.SystemButton.propHtml = 'Html';
Open.Core.Controls.Buttons.SystemButton.propType = 'Type';
Open.Core.Controls.Buttons.SystemButton.propValue = 'Value';
Open.Core.Controls.Buttons.SystemButton.propPadding = 'Padding';
Open.Core.Controls.Buttons.SystemButton.propFontSize = 'FontSize';

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Views', ['Open.Core.Script'], executeScript);
})();