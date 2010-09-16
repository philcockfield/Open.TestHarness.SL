//! Open.Core.Test.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.Core.Test');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.SampleView

Open.Core.Test.SampleView = function Open_Core_Test_SampleView() {
    /// <field name="lastPropertyChanged" type="Open.Core.PropertyRef">
    /// </field>
    Open.Core.Test.SampleView.initializeBase(this, [ Open.Core.Html.createDiv() ]);
    this.add_propertyChanged(ss.Delegate.create(this, function(sender, args) {
        this.lastPropertyChanged = args.get_property();
    }));
}
Open.Core.Test.SampleView.prototype = {
    lastPropertyChanged: null
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.SampleListItem

Open.Core.Test.SampleListItem = function Open_Core_Test_SampleListItem(text) {
    /// <param name="text" type="String">
    /// </param>
    Open.Core.Test.SampleListItem.initializeBase(this);
    this.set_text(text);
}
Open.Core.Test.SampleListItem.prototype = {
    
    toString: function Open_Core_Test_SampleListItem$toString() {
        /// <returns type="String"></returns>
        return Open.Core.Test.SampleListItem.callBaseMethod(this, 'toString');
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.SampleModel

Open.Core.Test.SampleModel = function Open_Core_Test_SampleModel() {
    Open.Core.Test.SampleModel.initializeBase(this);
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.Application

Open.Core.Test.Application = function Open_Core_Test_Application() {
}
Open.Core.Test.Application.main = function Open_Core_Test_Application$main(args) {
    /// <param name="args" type="Object">
    /// </param>
    Open.Testing.TestHarness.registerClass(Open.Core.Test.UnitTests.ScriptLoadHelperTest);
    Open.Testing.TestHarness.registerClass(Open.Core.Test.UnitTests.DiContainerTest);
    Open.Testing.TestHarness.registerClass(Open.Core.Test.UnitTests.ModelBaseUnitTest);
    Open.Testing.TestHarness.registerClass(Open.Core.Test.UnitTests.ViewBaseUnitTest);
    Open.Testing.TestHarness.registerClass(Open.Core.Test.ViewTests.Core.ViewBaseTest);
    Open.Testing.TestHarness.registerClass(Open.Core.Test.ViewTests.Lists.ListTreeTest);
    Open.Testing.TestHarness.registerClass(Open.Core.Test.ViewTests.Lists.ListItemViewTest);
    Open.Testing.TestHarness.registerClass(Open.Core.Test.ViewTests.Controls.LogTest);
    Open.Testing.TestHarness.registerClass(Open.Core.Test.ViewTests.Controls.Buttons.SystemButtonTest);
    Open.Testing.TestHarness.registerClass(Open.Core.Test.ViewTests.Controls.HtmlPrimitive.HtmlListTest);
}


Type.registerNamespace('Open.Core.Test.UnitTests');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.UnitTests.DiContainerTest

Open.Core.Test.UnitTests.DiContainerTest = function Open_Core_Test_UnitTests_DiContainerTest() {
    /// <field name="_container" type="Open.Core.DiContainer">
    /// </field>
}
Open.Core.Test.UnitTests.DiContainerTest.prototype = {
    _container: null,
    
    testInitialize: function Open_Core_Test_UnitTests_DiContainerTest$testInitialize() {
        this._container = new Open.Core.DiContainer();
    },
    
    testCleanup: function Open_Core_Test_UnitTests_DiContainerTest$testCleanup() {
        this._container.dispose();
    },
    
    shouldNotHaveSingleton: function Open_Core_Test_UnitTests_DiContainerTest$shouldNotHaveSingleton() {
        Open.Core.Should.beNull(this._container.getSingleton(Open.Core.Test.UnitTests.IMyInterface));
    },
    
    shouldHaveSingletonAfterRegistering: function Open_Core_Test_UnitTests_DiContainerTest$shouldHaveSingletonAfterRegistering() {
        var instance = new Open.Core.Test.UnitTests.MyClass();
        this._container.registerSingleton(Open.Core.Test.UnitTests.IMyInterface, instance);
        var retreived = this._container.getSingleton(Open.Core.Test.UnitTests.IMyInterface);
        Open.Core.Should.equal(retreived, instance);
    },
    
    shouldReplaceSingleton: function Open_Core_Test_UnitTests_DiContainerTest$shouldReplaceSingleton() {
        var instance1 = new Open.Core.Test.UnitTests.MyClass();
        var instance2 = new Open.Core.Test.UnitTests.MyClass();
        this._container.registerSingleton(Open.Core.Test.UnitTests.IMyInterface, instance1);
        this._container.registerSingleton(Open.Core.Test.UnitTests.IMyInterface, instance2);
        var retreived = this._container.getSingleton(Open.Core.Test.UnitTests.IMyInterface);
        Open.Core.Should.notEqual(retreived, instance1);
        Open.Core.Should.equal(retreived, instance2);
    },
    
    shouldReturnNullWhenNoKeySpecified: function Open_Core_Test_UnitTests_DiContainerTest$shouldReturnNullWhenNoKeySpecified() {
        Open.Core.Should.beNull(this._container.getSingleton(null));
    },
    
    shouldNotContainSingleton: function Open_Core_Test_UnitTests_DiContainerTest$shouldNotContainSingleton() {
        Open.Core.Should.beFalse(this._container.containsSingleton(Open.Core.Test.UnitTests.IMyInterface));
    },
    
    shouldContainSingleton: function Open_Core_Test_UnitTests_DiContainerTest$shouldContainSingleton() {
        this._container.registerSingleton(Open.Core.Test.UnitTests.IMyInterface, new Open.Core.Test.UnitTests.MyClass());
        Open.Core.Should.beTrue(this._container.containsSingleton(Open.Core.Test.UnitTests.IMyInterface));
    },
    
    shouldUnregisterSingleton: function Open_Core_Test_UnitTests_DiContainerTest$shouldUnregisterSingleton() {
        var instance1 = new Open.Core.Test.UnitTests.MyClass();
        this._container.registerSingleton(Open.Core.Test.UnitTests.IMyInterface, instance1);
        this._container.registerSingleton(Open.Core.Test.UnitTests.IMyInterface, instance1);
        Open.Core.Should.equal(this._container.getSingleton(Open.Core.Test.UnitTests.IMyInterface), instance1);
        this._container.unregisterSingleton(Open.Core.Test.UnitTests.IMyInterface);
        Open.Core.Should.equal(this._container.getSingleton(Open.Core.Test.UnitTests.IMyInterface), null);
    },
    
    shouldCreateSingletonInstance: function Open_Core_Test_UnitTests_DiContainerTest$shouldCreateSingletonInstance() {
        Open.Core.Should.beNull(this._container.getSingleton(Open.Core.Test.UnitTests.IMyInterface));
        var instance1 = new Open.Core.Test.UnitTests.MyClass();
        var retrieved = this._container.getOrCreateSingleton(Open.Core.Test.UnitTests.IMyInterface, ss.Delegate.create(this, function() {
            return instance1;
        }));
        Open.Core.Should.equal(retrieved, instance1);
        Open.Core.Should.equal(this._container.getSingleton(Open.Core.Test.UnitTests.IMyInterface), instance1);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.UnitTests.MyClass

Open.Core.Test.UnitTests.MyClass = function Open_Core_Test_UnitTests_MyClass() {
    Open.Core.Test.UnitTests.MyClass.initializeBase(this);
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.UnitTests.IMyInterface

Open.Core.Test.UnitTests.IMyInterface = function Open_Core_Test_UnitTests_IMyInterface() {
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.UnitTests.ModelBaseUnitTest

Open.Core.Test.UnitTests.ModelBaseUnitTest = function Open_Core_Test_UnitTests_ModelBaseUnitTest() {
    /// <field name="_model" type="Open.Core.Test.SampleModel">
    /// </field>
}
Open.Core.Test.UnitTests.ModelBaseUnitTest.prototype = {
    _model: null,
    
    classInitialize: function Open_Core_Test_UnitTests_ModelBaseUnitTest$classInitialize() {
    },
    
    classCleanup: function Open_Core_Test_UnitTests_ModelBaseUnitTest$classCleanup() {
    },
    
    testInitialize: function Open_Core_Test_UnitTests_ModelBaseUnitTest$testInitialize() {
        this._model = new Open.Core.Test.SampleModel();
    },
    
    testCleanup: function Open_Core_Test_UnitTests_ModelBaseUnitTest$testCleanup() {
        this._model.dispose();
    },
    
    shouldNotBeDisposed: function Open_Core_Test_UnitTests_ModelBaseUnitTest$shouldNotBeDisposed() {
        Open.Core.Should.beFalse(this._model.get_isDisposed());
    },
    
    shouldBeDisposed: function Open_Core_Test_UnitTests_ModelBaseUnitTest$shouldBeDisposed() {
        this._model.dispose();
        Open.Core.Should.beTrue(this._model.get_isDisposed());
    },
    
    shouldFireDisposedEventOnce: function Open_Core_Test_UnitTests_ModelBaseUnitTest$shouldFireDisposedEventOnce() {
        var count = 0;
        this._model.add_disposed(ss.Delegate.create(this, function() {
            count++;
        }));
        this._model.dispose();
        this._model.dispose();
        Open.Core.Should.equal(count, 1);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.UnitTests.ScriptLoadHelperTest

Open.Core.Test.UnitTests.ScriptLoadHelperTest = function Open_Core_Test_UnitTests_ScriptLoadHelperTest() {
}
Open.Core.Test.UnitTests.ScriptLoadHelperTest.prototype = {
    
    loadControls: function Open_Core_Test_UnitTests_ScriptLoadHelperTest$loadControls() {
        Open.Core.Log.info('Helper.ScriptLoader.IsLoaded: ' + Open.Core.Helper.get_scriptLoader().isLoaded(Open.Core.Helpers.ScriptLibrary.controls));
        Open.Core.Helper.get_scriptLoader().loadLibrary(Open.Core.Helpers.ScriptLibrary.controls, ss.Delegate.create(this, function() {
            Open.Core.Log.info('Callback - ' + Open.Core.Helper.get_scriptLoader().isLoaded(Open.Core.Helpers.ScriptLibrary.controls));
        }));
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.UnitTests.ViewBaseUnitTest

Open.Core.Test.UnitTests.ViewBaseUnitTest = function Open_Core_Test_UnitTests_ViewBaseUnitTest() {
    /// <field name="_view" type="Open.Core.Test.SampleView">
    /// </field>
}
Open.Core.Test.UnitTests.ViewBaseUnitTest.prototype = {
    _view: null,
    
    testInitialize: function Open_Core_Test_UnitTests_ViewBaseUnitTest$testInitialize() {
        this._view = new Open.Core.Test.SampleView();
        Open.Testing.TestHarness.addControl(this._view);
    },
    
    testCleanup: function Open_Core_Test_UnitTests_ViewBaseUnitTest$testCleanup() {
        this._view.dispose();
        Open.Testing.TestHarness.reset();
    },
    
    shouldNotHaveBackground: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldNotHaveBackground() {
        Open.Core.Should.beNull(this._view.get_background());
    },
    
    shouldSetCssValue: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldSetCssValue() {
        this._view.set_background('orange');
        Open.Core.Should.equal(this._view.get_background(), 'orange');
    },
    
    shouldResetCssValueToNull: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldResetCssValueToNull() {
        this._view.set_background('orange');
        this._view.set_background('');
        Open.Core.Should.beNull(this._view.get_background());
    },
    
    shouldFireBackgroundChangedEvent: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldFireBackgroundChangedEvent() {
        this._view.lastPropertyChanged = null;
        this._view.set_background('orange');
        Open.Core.Should.equal(this._view.lastPropertyChanged.get_name(), Open.Core.ViewBase.propBackground);
    },
    
    shouldBeVisibleByDefault: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldBeVisibleByDefault() {
        Open.Core.Should.beTrue(this._view.get_isVisible());
        Open.Core.Should.equal(this._view.getCss(Open.Core.Css.display), Open.Core.Css.block);
    },
    
    shouldSetDisplayToBlockWhenIsVisible: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldSetDisplayToBlockWhenIsVisible() {
        this._view.set_isVisible(false);
        this._view.set_isVisible(true);
        Open.Core.Should.equal(this._view.getCss(Open.Core.Css.display), Open.Core.Css.block);
    },
    
    shouldSetDisplayToNoneWhenNotIsVisible: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldSetDisplayToNoneWhenNotIsVisible() {
        this._view.set_isVisible(false);
        Open.Core.Should.equal(this._view.getCss(Open.Core.Css.display), Open.Core.Css.none);
    },
    
    shouldFireVisibilityChangedEventsOnce: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldFireVisibilityChangedEventsOnce() {
        var propChangedCount = 0;
        var visibilityChangedCount = 0;
        this._view.add_propertyChanged(ss.Delegate.create(this, function(sender, args) {
            if (args.get_property().get_name() === Open.Core.ViewBase.propIsVisible) {
                propChangedCount++;
            }
        }));
        this._view.add_isVisibleChanged(ss.Delegate.create(this, function() {
            visibilityChangedCount++;
        }));
        this._view.set_isVisible(false);
        this._view.set_isVisible(false);
        this._view.set_isVisible(false);
        Open.Core.Should.equal(propChangedCount, 1);
        Open.Core.Should.equal(visibilityChangedCount, 1);
    },
    
    shouldBeFullOpacityByDefault: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldBeFullOpacityByDefault() {
        Open.Core.Should.equal(this._view.get_opacity(), 1);
    },
    
    shouldBoundOpacityValue: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldBoundOpacityValue() {
        this._view.set_opacity(2);
        Open.Core.Should.equal(this._view.get_opacity(), 1);
        this._view.set_opacity(-1);
        Open.Core.Should.equal(this._view.get_opacity(), 0);
    },
    
    shouldSetOpacityCss: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldSetOpacityCss() {
        this._view.set_opacity(0.5);
        Open.Core.Should.equal(this._view.getCss('opacity'), '0.5');
    },
    
    shouldChangeWidth: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldChangeWidth() {
        var fireCount = 0;
        this._view.add_sizeChanged(ss.Delegate.create(this, function() {
            fireCount++;
        }));
        Open.Core.Should.equal(this._view.get_width(), 0);
        this._view.set_width(50);
        this._view.set_width(50);
        Open.Core.Should.equal(this._view.get_width(), 50);
        Open.Core.Should.equal(fireCount, 1);
    },
    
    shouldChangeHeight: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldChangeHeight() {
        var fireCount = 0;
        this._view.add_sizeChanged(ss.Delegate.create(this, function() {
            fireCount++;
        }));
        Open.Core.Should.equal(this._view.get_height(), 0);
        this._view.set_height(50);
        this._view.set_height(50);
        Open.Core.Should.equal(this._view.get_height(), 50);
        Open.Core.Should.equal(fireCount, 1);
    },
    
    shouldSetSize: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldSetSize() {
        this._view.setSize(80, 80);
        Open.Core.Should.equal(this._view.get_width(), 80);
        Open.Core.Should.equal(this._view.get_height(), 80);
    },
    
    shouldFireSizeChangedOnce: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldFireSizeChangedOnce() {
        var fireCount = 0;
        this._view.add_sizeChanged(ss.Delegate.create(this, function() {
            fireCount++;
        }));
        this._view.setSize(80, 80);
        this._view.setSize(80, 80);
        Open.Core.Should.equal(fireCount, 1);
    },
    
    shouldNowAllowNegativeSizing: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldNowAllowNegativeSizing() {
        this._view.setSize(-1, -1);
        Open.Core.Should.equal(this._view.get_width(), 0);
        Open.Core.Should.equal(this._view.get_height(), 0);
        this._view.set_width(-15);
        this._view.set_height(-5);
        Open.Core.Should.equal(this._view.get_width(), 0);
        Open.Core.Should.equal(this._view.get_height(), 0);
    },
    
    shouldBeEnabledByDefault: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldBeEnabledByDefault() {
        Open.Core.Should.beTrue(this._view.get_isEnabled());
    },
    
    shouldFireIsEnabledChanged: function Open_Core_Test_UnitTests_ViewBaseUnitTest$shouldFireIsEnabledChanged() {
        var isEnabledChangedCount = 0;
        var propChangedCount = 0;
        this._view.add_isEnabledChanged(ss.Delegate.create(this, function() {
            isEnabledChangedCount++;
        }));
        this._view.add_propertyChanged(ss.Delegate.create(this, function(sender, args) {
            if (args.get_property().get_name() === Open.Core.ViewBase.propIsEnabled) {
                propChangedCount++;
            }
        }));
        this._view.set_isEnabled(false);
        this._view.set_isEnabled(false);
        this._view.set_isEnabled(false);
        Open.Core.Should.beFalse(this._view.get_isEnabled());
        Open.Core.Should.equal(isEnabledChangedCount, 1);
        Open.Core.Should.equal(propChangedCount, 1);
        this._view.set_isEnabled(true);
        Open.Core.Should.beTrue(this._view.get_isEnabled());
        Open.Core.Should.equal(isEnabledChangedCount, 2);
        Open.Core.Should.equal(propChangedCount, 2);
    }
}


Type.registerNamespace('Open.Core.Test.ViewTests.Controls.Buttons');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.ViewTests.Controls.Buttons.LogButtons

Open.Core.Test.ViewTests.Controls.Buttons.LogButtons = function Open_Core_Test_ViewTests_Controls_Buttons_LogButtons() {
}
Open.Core.Test.ViewTests.Controls.Buttons.LogButtons.writeIButton = function Open_Core_Test_ViewTests_Controls_Buttons_LogButtons$writeIButton(button) {
    /// <param name="button" type="Open.Core.IButton">
    /// </param>
    Open.Core.Log.info('IsEnabled: ' + button.get_isEnabled());
    Open.Core.Log.info('CanToggle: ' + button.get_canToggle());
    Open.Core.Log.info('MouseState: ' + Open.Core.ButtonMouseState.toString(button.get_mouseState()));
    Open.Core.Log.info('IsPressed: ' + button.get_isPressed());
    Open.Core.Log.info('IsMouseOver: ' + button.get_isMouseOver());
    Open.Core.Log.info('IsMouseDown: ' + button.get_isMouseDown());
}
Open.Core.Test.ViewTests.Controls.Buttons.LogButtons.writeSystemButton = function Open_Core_Test_ViewTests_Controls_Buttons_LogButtons$writeSystemButton(button) {
    /// <param name="button" type="Open.Core.Controls.Buttons.SystemButton">
    /// </param>
    Open.Core.Log.info('Html: ' + button.get_html());
    Open.Core.Log.info('Type: ' + button.get_type());
    Open.Core.Log.info('Value: ' + button.get_value());
    Open.Core.Log.info('Padding: ' + button.get_padding());
    Open.Core.Log.info('FontSize: ' + button.get_fontSize());
    Open.Core.Test.ViewTests.Controls.Buttons.LogButtons.writeIButton(button);
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.ViewTests.Controls.Buttons.SystemButtonTest

Open.Core.Test.ViewTests.Controls.Buttons.SystemButtonTest = function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest() {
    /// <field name="_button" type="Open.Core.Controls.Buttons.SystemButton">
    /// </field>
}
Open.Core.Test.ViewTests.Controls.Buttons.SystemButtonTest.prototype = {
    _button: null,
    
    classInitialize: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$classInitialize() {
        this._button = new Open.Core.Controls.Buttons.SystemButton();
        Open.Testing.TestHarness.addControl(this._button);
        this._button.add_click(ss.Delegate.create(this, function() {
            Open.Core.Log.info('!! Click | IsPressed: ' + this._button.get_isPressed());
        }));
        this._button.add_isPressedChanged(ss.Delegate.create(this, function() {
            Open.Core.Log.info('!! IsPressedChanged | IsPressed: ' + this._button.get_isPressed());
        }));
        this._button.add_propertyChanged(ss.Delegate.create(this, function(sender, args) {
        }));
    },
    
    classCleanup: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$classCleanup() {
    },
    
    testInitialize: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$testInitialize() {
    },
    
    testCleanup: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$testCleanup() {
    },
    
    toggle_IsEnabled: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$toggle_IsEnabled() {
        this._button.set_isEnabled(!this._button.get_isEnabled());
    },
    
    change_Html: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$change_Html() {
        this._button.set_html((this._button.get_html() === Open.Core.Controls.Buttons.SystemButton.untitled) ? 'My <b>Button</b>' : Open.Core.Controls.Buttons.SystemButton.untitled);
    },
    
    change_Padding: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$change_Padding() {
        this._button.set_padding((this._button.get_padding() == null) ? Open.Core.Controls.Buttons.SystemButton.defaultPadding : null);
    },
    
    change_FontSize: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$change_FontSize() {
        this._button.set_fontSize((this._button.get_fontSize() === '40pt') ? '8pt' : '40pt');
    },
    
    invokeClick__Force: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$invokeClick__Force() {
        this._button.invokeClick(true);
    },
    
    invokeClick__Not_Forced: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$invokeClick__Not_Forced() {
        this._button.invokeClick(false);
    },
    
    canToggle_True__Error: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$canToggle_True__Error() {
        this._button.set_canToggle(true);
    },
    
    write_Properties: function Open_Core_Test_ViewTests_Controls_Buttons_SystemButtonTest$write_Properties() {
        Open.Core.Test.ViewTests.Controls.Buttons.LogButtons.writeSystemButton(this._button);
    }
}


Type.registerNamespace('Open.Core.Test.ViewTests.Controls');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.ViewTests.Controls.LogTest

Open.Core.Test.ViewTests.Controls.LogTest = function Open_Core_Test_ViewTests_Controls_LogTest() {
}
Open.Core.Test.ViewTests.Controls.LogTest.prototype = {
    
    classInitialize: function Open_Core_Test_ViewTests_Controls_LogTest$classInitialize() {
    },
    
    classCleanup: function Open_Core_Test_ViewTests_Controls_LogTest$classCleanup() {
    },
    
    testInitialize: function Open_Core_Test_ViewTests_Controls_LogTest$testInitialize() {
    },
    
    testCleanup: function Open_Core_Test_ViewTests_Controls_LogTest$testCleanup() {
    },
    
    log__Info: function Open_Core_Test_ViewTests_Controls_LogTest$log__Info() {
        Open.Core.Log.info('Info');
    },
    
    log__Debug: function Open_Core_Test_ViewTests_Controls_LogTest$log__Debug() {
        Open.Core.Log.debug('Debug');
    },
    
    log__Warning: function Open_Core_Test_ViewTests_Controls_LogTest$log__Warning() {
        Open.Core.Log.warning('Warning');
    },
    
    log__Error: function Open_Core_Test_ViewTests_Controls_LogTest$log__Error() {
        Open.Core.Log.error('Error');
    },
    
    log__Null: function Open_Core_Test_ViewTests_Controls_LogTest$log__Null() {
        Open.Core.Log.info(null);
    },
    
    log__Empty_String: function Open_Core_Test_ViewTests_Controls_LogTest$log__Empty_String() {
        Open.Core.Log.info('');
    },
    
    lineBreak: function Open_Core_Test_ViewTests_Controls_LogTest$lineBreak() {
        Open.Core.Log.lineBreak();
    },
    
    newSection: function Open_Core_Test_ViewTests_Controls_LogTest$newSection() {
        Open.Core.Log.newSection();
    },
    
    log_After_Delay: function Open_Core_Test_ViewTests_Controls_LogTest$log_After_Delay() {
        Open.Core.DelayedAction.invoke(0.5, ss.Delegate.create(this, function() {
            Open.Core.Log.info('Logged after delay');
        }));
    },
    
    clear: function Open_Core_Test_ViewTests_Controls_LogTest$clear() {
        Open.Core.Log.clear();
    },
    
    sample1: function Open_Core_Test_ViewTests_Controls_LogTest$sample1() {
        Open.Core.Log.info('This is a log entry');
        Open.Core.Log.info('Another entry within the same test.');
    },
    
    sample2: function Open_Core_Test_ViewTests_Controls_LogTest$sample2() {
        Open.Core.Log.info('This entry came from another test (Sample2).');
        Open.Core.Log.info('Note the section divider visually showing that these entries were emitted from different tests.');
    }
}


Type.registerNamespace('Open.Core.Test.ViewTests.Controls.HtmlPrimitive');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.ViewTests.Controls.HtmlPrimitive.HtmlListTest

Open.Core.Test.ViewTests.Controls.HtmlPrimitive.HtmlListTest = function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest() {
    /// <field name="_list" type="Open.Core.Controls.HtmlPrimitive.HtmlList">
    /// </field>
    /// <field name="_count" type="Number" integer="true">
    /// </field>
}
Open.Core.Test.ViewTests.Controls.HtmlPrimitive.HtmlListTest._itemToString = function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest$_itemToString(li) {
    /// <param name="li" type="jQueryObject">
    /// </param>
    /// <returns type="String"></returns>
    return Open.Core.Helper.get_string().formatToString(li, function(o) {
        return li.html();
    });
}
Open.Core.Test.ViewTests.Controls.HtmlPrimitive.HtmlListTest.prototype = {
    _list: null,
    _count: 0,
    
    classInitialize: function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest$classInitialize() {
        this._list = new Open.Core.Controls.HtmlPrimitive.HtmlList(Open.Core.HtmlListType.unordered, 'myListClass');
        this._list.get_container().css(Open.Core.Css.background, 'orange');
        this._list.get_container().width(300);
        Open.Testing.TestHarness.addControl(this._list);
        this.add();
        this.add();
    },
    
    classCleanup: function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest$classCleanup() {
    },
    
    testInitialize: function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest$testInitialize() {
    },
    
    testCleanup: function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest$testCleanup() {
    },
    
    add: function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest$add() {
        this._count++;
        var text = 'Item ' + this._count;
        var ulItem = this._list.add(text, 'myClass1 myClass2');
        Open.Core.Log.info('Inserted item: ' + Open.Core.Html.toHtml(ulItem).htmlEncode());
        this.write_Properties();
    },
    
    removeAt_Zero: function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest$removeAt_Zero() {
        this._list.remove(0);
        this.write_Properties();
    },
    
    removeLast: function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest$removeLast() {
        if (this._list.get_last() != null) {
            this._list.get_last().remove();
        }
        this.write_Properties();
    },
    
    clear: function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest$clear() {
        this._list.clear();
        this.write_Properties();
    },
    
    myERROR: function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest$myERROR() {
        Open.Core.Log.info('Throwing error now.');
        throw new Error('Hello');
    },
    
    write_Properties: function Open_Core_Test_ViewTests_Controls_HtmlPrimitive_HtmlListTest$write_Properties() {
        Open.Core.Log.info('Count: ' + this._list.get_count());
        Open.Core.Log.info('ListType: ' + Open.Core.HtmlListType.toString(this._list.get_listType()));
        Open.Core.Log.info('First: ' + Open.Core.Test.ViewTests.Controls.HtmlPrimitive.HtmlListTest._itemToString(this._list.get_first()));
        Open.Core.Log.info('Last: ' + Open.Core.Test.ViewTests.Controls.HtmlPrimitive.HtmlListTest._itemToString(this._list.get_last()));
        Open.Core.Log.info('InnerHtml: ' + this._list.get_innerHtml().htmlEncode());
        Open.Core.Log.info('OuterHtml: ' + this._list.get_outerHtml().htmlEncode());
    }
}


Type.registerNamespace('Open.Core.Test.ViewTests.Core');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.ViewTests.Core.ViewBaseTest

Open.Core.Test.ViewTests.Core.ViewBaseTest = function Open_Core_Test_ViewTests_Core_ViewBaseTest() {
    /// <field name="_view" type="Open.Core.Test.SampleView">
    /// </field>
}
Open.Core.Test.ViewTests.Core.ViewBaseTest.prototype = {
    _view: null,
    
    classInitialize: function Open_Core_Test_ViewTests_Core_ViewBaseTest$classInitialize() {
        this._view = new Open.Core.Test.SampleView();
        this._view.set_background('orange');
        this._view.setSize(150, 100);
        Open.Testing.TestHarness.addControl(this._view);
        this._view.add_sizeChanged(ss.Delegate.create(this, function() {
            Open.Core.Log.info('!! SizeChanged - Width: ' + this._view.get_width() + ', Height: ' + this._view.get_height());
        }));
    },
    
    classCleanup: function Open_Core_Test_ViewTests_Core_ViewBaseTest$classCleanup() {
        this._view.dispose();
    },
    
    testInitialize: function Open_Core_Test_ViewTests_Core_ViewBaseTest$testInitialize() {
    },
    
    testCleanup: function Open_Core_Test_ViewTests_Core_ViewBaseTest$testCleanup() {
    },
    
    toggle__IsVisible: function Open_Core_Test_ViewTests_Core_ViewBaseTest$toggle__IsVisible() {
        this._view.set_isVisible(!this._view.get_isVisible());
    },
    
    toggle__Background: function Open_Core_Test_ViewTests_Core_ViewBaseTest$toggle__Background() {
        this._view.set_background((this._view.get_background() === 'orange') ? 'red' : 'orange');
    },
    
    toggle__Opacity: function Open_Core_Test_ViewTests_Core_ViewBaseTest$toggle__Opacity() {
        this._view.set_opacity((this._view.get_opacity() === 1) ? 0.3 : 1);
        Open.Core.Log.info('Opacity: ' + this._view.get_opacity());
    },
    
    change__Size: function Open_Core_Test_ViewTests_Core_ViewBaseTest$change__Size() {
        if (this._view.get_width() === 150) {
            this._view.setSize(400, 300);
        }
        else {
            this._view.setSize(150, 100);
        }
    }
}


Type.registerNamespace('Open.Core.Test.ViewTests.Lists');

////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.ViewTests.Lists.ListItemViewTest

Open.Core.Test.ViewTests.Lists.ListItemViewTest = function Open_Core_Test_ViewTests_Lists_ListItemViewTest() {
    /// <field name="_model" type="Open.Core.Test.SampleListItem">
    /// </field>
    /// <field name="_view" type="Open.Core.Lists.ListItemView">
    /// </field>
}
Open.Core.Test.ViewTests.Lists.ListItemViewTest.prototype = {
    _model: null,
    _view: null,
    
    classInitialize: function Open_Core_Test_ViewTests_Lists_ListItemViewTest$classInitialize() {
        this._model = new Open.Core.Test.SampleListItem('My Item');
        this._view = new Open.Core.Lists.ListItemView(Open.Core.Html.createDiv(), this._model);
        this._view.set_width(300);
        Open.Testing.TestHarness.addControl(this._view);
    },
    
    classCleanup: function Open_Core_Test_ViewTests_Lists_ListItemViewTest$classCleanup() {
        this._view.dispose();
    },
    
    testInitialize: function Open_Core_Test_ViewTests_Lists_ListItemViewTest$testInitialize() {
    },
    
    testCleanup: function Open_Core_Test_ViewTests_Lists_ListItemViewTest$testCleanup() {
    },
    
    toggle__CanSelect: function Open_Core_Test_ViewTests_Lists_ListItemViewTest$toggle__CanSelect() {
        this._model.set_canSelect(!this._model.get_canSelect());
        this._writeProperties();
    },
    
    toggle__IsSelected: function Open_Core_Test_ViewTests_Lists_ListItemViewTest$toggle__IsSelected() {
        var newValue = !this._model.get_isSelected();
        if (!this._model.get_canSelect() && newValue) {
            Open.Core.Log.info('New value is true.  Because \'CanSelect\' == false, the new value will not stick.');
        }
        this._model.set_isSelected(newValue);
        this._writeProperties();
    },
    
    _writeProperties: function Open_Core_Test_ViewTests_Lists_ListItemViewTest$_writeProperties() {
        Open.Core.Log.info('IsSelected: ' + this._model.get_isSelected());
        Open.Core.Log.info('CanSelect: ' + this._model.get_canSelect());
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.Core.Test.ViewTests.Lists.ListTreeTest

Open.Core.Test.ViewTests.Lists.ListTreeTest = function Open_Core_Test_ViewTests_Lists_ListTreeTest() {
    /// <field name="_listTree" type="Open.Core.Lists.ListTreeView">
    /// </field>
}
Open.Core.Test.ViewTests.Lists.ListTreeTest.prototype = {
    _listTree: null,
    
    classInitialize: function Open_Core_Test_ViewTests_Lists_ListTreeTest$classInitialize() {
        this._listTree = new Open.Core.Lists.ListTreeView(Open.Core.Html.createDiv());
        this._listTree.setSize(250, 500);
        this._listTree.set_background('#f2f2f2');
        Open.Testing.TestHarness.addControl(this._listTree);
        this._listTree.add_selectedNodeChanged(ss.Delegate.create(this, function() {
            Open.Core.Log.info('!! SelectedNodeChanged | SelectedNode: ' + this._listTree.get_selectedNode());
        }));
        this._listTree.add_selectedParentChanged(ss.Delegate.create(this, function() {
            Open.Core.Log.info('!! SelectedParentChanged | SelectedParent: ' + this._listTree.get_selectedParent());
        }));
    },
    
    classCleanup: function Open_Core_Test_ViewTests_Lists_ListTreeTest$classCleanup() {
        this._listTree.dispose();
    },
    
    testInitialize: function Open_Core_Test_ViewTests_Lists_ListTreeTest$testInitialize() {
    },
    
    testCleanup: function Open_Core_Test_ViewTests_Lists_ListTreeTest$testCleanup() {
    },
    
    addNodes: function Open_Core_Test_ViewTests_Lists_ListTreeTest$addNodes() {
        this._addSampleNodes();
    },
    
    rootNode__Null: function Open_Core_Test_ViewTests_Lists_ListTreeTest$rootNode__Null() {
        this._listTree.set_rootNode(null);
    },
    
    back: function Open_Core_Test_ViewTests_Lists_ListTreeTest$back() {
        this._listTree.back();
    },
    
    home: function Open_Core_Test_ViewTests_Lists_ListTreeTest$home() {
        this._listTree.home();
    },
    
    _addSampleNodes: function Open_Core_Test_ViewTests_Lists_ListTreeTest$_addSampleNodes() {
        var rootNode = new Open.Core.Test.SampleListItem('Root');
        this._listTree.set_rootNode(rootNode);
        rootNode.addChild(new Open.Core.Test.SampleListItem('Child 1'));
        rootNode.addChild(new Open.Core.Test.SampleListItem('Child 2'));
        rootNode.addChild(new Open.Core.Test.SampleListItem('Child 3'));
        var recent1 = Type.safeCast(rootNode.childAt(0), Open.Core.Test.SampleListItem);
        recent1.addChild(new Open.Core.Test.SampleListItem('Recent Grandchild 1'));
        recent1.addChild(new Open.Core.Test.SampleListItem('Recent Grandchild 2'));
        recent1.addChild(new Open.Core.Test.SampleListItem('Recent Grandchild 3'));
    }
}


Open.Core.Test.SampleView.registerClass('Open.Core.Test.SampleView', Open.Core.ViewBase);
Open.Core.Test.SampleListItem.registerClass('Open.Core.Test.SampleListItem', Open.Core.Lists.ListItem);
Open.Core.Test.SampleModel.registerClass('Open.Core.Test.SampleModel', Open.Core.ModelBase);
Open.Core.Test.Application.registerClass('Open.Core.Test.Application');
Open.Core.Test.UnitTests.DiContainerTest.registerClass('Open.Core.Test.UnitTests.DiContainerTest');
Open.Core.Test.UnitTests.IMyInterface.registerClass('Open.Core.Test.UnitTests.IMyInterface');
Open.Core.Test.UnitTests.MyClass.registerClass('Open.Core.Test.UnitTests.MyClass', Open.Core.Test.UnitTests.IMyInterface);
Open.Core.Test.UnitTests.ModelBaseUnitTest.registerClass('Open.Core.Test.UnitTests.ModelBaseUnitTest');
Open.Core.Test.UnitTests.ScriptLoadHelperTest.registerClass('Open.Core.Test.UnitTests.ScriptLoadHelperTest');
Open.Core.Test.UnitTests.ViewBaseUnitTest.registerClass('Open.Core.Test.UnitTests.ViewBaseUnitTest');
Open.Core.Test.ViewTests.Controls.Buttons.LogButtons.registerClass('Open.Core.Test.ViewTests.Controls.Buttons.LogButtons');
Open.Core.Test.ViewTests.Controls.Buttons.SystemButtonTest.registerClass('Open.Core.Test.ViewTests.Controls.Buttons.SystemButtonTest');
Open.Core.Test.ViewTests.Controls.LogTest.registerClass('Open.Core.Test.ViewTests.Controls.LogTest');
Open.Core.Test.ViewTests.Controls.HtmlPrimitive.HtmlListTest.registerClass('Open.Core.Test.ViewTests.Controls.HtmlPrimitive.HtmlListTest');
Open.Core.Test.ViewTests.Core.ViewBaseTest.registerClass('Open.Core.Test.ViewTests.Core.ViewBaseTest');
Open.Core.Test.ViewTests.Lists.ListItemViewTest.registerClass('Open.Core.Test.ViewTests.Lists.ListItemViewTest');
Open.Core.Test.ViewTests.Lists.ListTreeTest.registerClass('Open.Core.Test.ViewTests.Lists.ListTreeTest');

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('Open.Core.Test', ['Open.Core.Script','Open.Core.Lists','Open.Core.Views'], executeScript);
})();
