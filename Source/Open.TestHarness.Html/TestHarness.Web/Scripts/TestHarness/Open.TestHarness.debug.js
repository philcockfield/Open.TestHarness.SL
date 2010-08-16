//! Open.TestHarness.debug.js
//

(function() {
function executeScript() {

Type.registerNamespace('Open.TestHarness');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.CssSelectors

Open.TestHarness.CssSelectors = function Open_TestHarness_CssSelectors() {
    /// <summary>
    /// Constants for common CSS selectors.
    /// </summary>
    /// <field name="logTitlebar" type="String" static="true">
    /// </field>
    /// <field name="main" type="String" static="true">
    /// </field>
    /// <field name="sidebar" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Elements

Open.TestHarness.Elements = function Open_TestHarness_Elements() {
    /// <summary>
    /// Constants for element IDs.
    /// </summary>
    /// <field name="root" type="String" static="true">
    /// </field>
    /// <field name="outputLog" type="String" static="true">
    /// </field>
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Application

Open.TestHarness.Application = function Open_TestHarness_Application() {
    /// <field name="_resizeController" type="Open.TestHarness.Shell.PanelResizeController" static="true">
    /// </field>
}
Open.TestHarness.Application.main = function Open_TestHarness_Application$main(args) {
    /// <param name="args" type="Object">
    /// </param>
    Open.TestHarness.Application._resizeController = new Open.TestHarness.Shell.PanelResizeController();
}


Type.registerNamespace('Open.TestHarness.Shell');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Shell.PanelResizeController

Open.TestHarness.Shell.PanelResizeController = function Open_TestHarness_Shell_PanelResizeController() {
    /// <summary>
    /// Handles resizing of panels within the shell.
    /// </summary>
    /// <field name="_sidebarMinWidth" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_sidebarMaxWidthMargin" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_outputLogMaxHeightMargin" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_sideBarResizer" type="Open.Core.UI.HorizontalPanelResizer">
    /// </field>
    /// <field name="_outputResizer" type="Open.Core.UI.VerticalPanelResizer">
    /// </field>
    this._sideBarResizer = new Open.Core.UI.HorizontalPanelResizer(Open.TestHarness.CssSelectors.sidebar, 'TH_SB');
    this._sideBarResizer.add_resized(ss.Delegate.create(this, function() {
        Open.TestHarness.Shell.PanelResizeController._syncMainPanelWidth();
    }));
    this._sideBarResizer.set_minWidth(Open.TestHarness.Shell.PanelResizeController._sidebarMinWidth);
    this._sideBarResizer.set_maxWidthMargin(Open.TestHarness.Shell.PanelResizeController._sidebarMaxWidthMargin);
    Open.TestHarness.Shell.PanelResizeController._initializeResizer(this._sideBarResizer);
    Open.TestHarness.Shell.PanelResizeController._syncMainPanelWidth();
    this._outputResizer = new Open.Core.UI.VerticalPanelResizer(Open.Core.Css.toId(Open.TestHarness.Elements.outputLog), 'TH_OL');
    this._outputResizer.add_resized(ss.Delegate.create(this, function() {
    }));
    this._outputResizer.set_minHeight($(Open.TestHarness.CssSelectors.logTitlebar).height());
    this._outputResizer.set_maxHeightMargin(Open.TestHarness.Shell.PanelResizeController._outputLogMaxHeightMargin);
    Open.TestHarness.Shell.PanelResizeController._initializeResizer(this._outputResizer);
}
Open.TestHarness.Shell.PanelResizeController._initializeResizer = function Open_TestHarness_Shell_PanelResizeController$_initializeResizer(resizer) {
    /// <param name="resizer" type="Open.Core.UI.PanelResizerBase">
    /// </param>
    resizer.set_rootContainerId(Open.TestHarness.Elements.root);
    resizer.initialize();
}
Open.TestHarness.Shell.PanelResizeController._syncMainPanelWidth = function Open_TestHarness_Shell_PanelResizeController$_syncMainPanelWidth() {
    $(Open.TestHarness.CssSelectors.main).css(Open.Core.Css.left, $(Open.TestHarness.CssSelectors.sidebar).width() + 1 + Open.Core.Css.px);
}
Open.TestHarness.Shell.PanelResizeController.prototype = {
    _sideBarResizer: null,
    _outputResizer: null
}


Open.TestHarness.CssSelectors.registerClass('Open.TestHarness.CssSelectors');
Open.TestHarness.Elements.registerClass('Open.TestHarness.Elements');
Open.TestHarness.Application.registerClass('Open.TestHarness.Application');
Open.TestHarness.Shell.PanelResizeController.registerClass('Open.TestHarness.Shell.PanelResizeController');
Open.TestHarness.CssSelectors.logTitlebar = '#testHarnessLog .titlebar';
Open.TestHarness.CssSelectors.main = '#testHarness .main';
Open.TestHarness.CssSelectors.sidebar = '#testHarness .sidebar';
Open.TestHarness.Elements.root = 'testHarness';
Open.TestHarness.Elements.outputLog = 'testHarnessLog';
Open.TestHarness.Application._resizeController = null;
Open.TestHarness.Shell.PanelResizeController._sidebarMinWidth = 200;
Open.TestHarness.Shell.PanelResizeController._sidebarMaxWidthMargin = 80;
Open.TestHarness.Shell.PanelResizeController._outputLogMaxHeightMargin = 80;

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('TestHarness.Script3', ['Open.Core.Script'], executeScript);
})();
