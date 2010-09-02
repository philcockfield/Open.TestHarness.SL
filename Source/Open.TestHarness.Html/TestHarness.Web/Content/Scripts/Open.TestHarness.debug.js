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
    /// <field name="sidebar" type="String" static="true">
    /// </field>
    /// <field name="sidebarList" type="String" static="true">
    /// </field>
    /// <field name="sidebarToolbar" type="String" static="true">
    /// </field>
    /// <field name="homeButton" type="String" static="true">
    /// </field>
    /// <field name="main" type="String" static="true">
    /// </field>
    /// <field name="logTitlebar" type="String" static="true">
    /// </field>
    /// <field name="log" type="String" static="true">
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
    /// <field name="_sidebarController" type="Open.TestHarness.Sidebar.SidebarController" static="true">
    /// </field>
}
Open.TestHarness.Application.main = function Open_TestHarness_Application$main(args) {
    /// <param name="args" type="Object">
    /// </param>
    Open.TestHarness.Application._resizeController = new Open.TestHarness.Shell.PanelResizeController();
    Open.TestHarness.Application._sidebarController = new Open.TestHarness.Sidebar.SidebarController();
    var logView = new Open.Core.Controls.LogView($(Open.TestHarness.CssSelectors.log).first());
    Open.Core.Log.registerView(logView);
}


Type.registerNamespace('Open.TestHarness.Shell');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Shell.PanelResizeController

Open.TestHarness.Shell.PanelResizeController = function Open_TestHarness_Shell_PanelResizeController() {
    /// <summary>
    /// Handles resizing of panels within the shell.
    /// </summary>
    /// <field name="_sidebarMinWidth$2" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_sidebarMaxWidthMargin$2" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_outputLogMaxHeightMargin$2" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="_sideBarResizer$2" type="Open.Core.UI.HorizontalPanelResizer">
    /// </field>
    /// <field name="_outputResizer$2" type="Open.Core.UI.VerticalPanelResizer">
    /// </field>
    Open.TestHarness.Shell.PanelResizeController.initializeBase(this);
    this._sideBarResizer$2 = new Open.Core.UI.HorizontalPanelResizer(Open.TestHarness.CssSelectors.sidebar, 'TH_SB');
    this._sideBarResizer$2.add_resized(ss.Delegate.create(this, function() {
        Open.TestHarness.Shell.PanelResizeController._syncMainPanelWidth$2();
    }));
    this._sideBarResizer$2.set_minWidth(Open.TestHarness.Shell.PanelResizeController._sidebarMinWidth$2);
    this._sideBarResizer$2.set_maxWidthMargin(Open.TestHarness.Shell.PanelResizeController._sidebarMaxWidthMargin$2);
    Open.TestHarness.Shell.PanelResizeController._initializeResizer$2(this._sideBarResizer$2);
    Open.TestHarness.Shell.PanelResizeController._syncMainPanelWidth$2();
    this._outputResizer$2 = new Open.Core.UI.VerticalPanelResizer(Open.Core.Css.toId(Open.TestHarness.Elements.outputLog), 'TH_OL');
    this._outputResizer$2.add_resized(ss.Delegate.create(this, function() {
    }));
    this._outputResizer$2.set_minHeight($(Open.TestHarness.CssSelectors.logTitlebar).height());
    this._outputResizer$2.set_maxHeightMargin(Open.TestHarness.Shell.PanelResizeController._outputLogMaxHeightMargin$2);
    Open.TestHarness.Shell.PanelResizeController._initializeResizer$2(this._outputResizer$2);
}
Open.TestHarness.Shell.PanelResizeController._initializeResizer$2 = function Open_TestHarness_Shell_PanelResizeController$_initializeResizer$2(resizer) {
    /// <param name="resizer" type="Open.Core.UI.PanelResizerBase">
    /// </param>
    resizer.set_rootContainerId(Open.TestHarness.Elements.root);
    resizer.initialize();
}
Open.TestHarness.Shell.PanelResizeController._syncMainPanelWidth$2 = function Open_TestHarness_Shell_PanelResizeController$_syncMainPanelWidth$2() {
    $(Open.TestHarness.CssSelectors.main).css(Open.Core.Css.left, $(Open.TestHarness.CssSelectors.sidebar).width() + 1 + Open.Core.Css.px);
}
Open.TestHarness.Shell.PanelResizeController.prototype = {
    _sideBarResizer$2: null,
    _outputResizer$2: null
}


Type.registerNamespace('Open.TestHarness.Sidebar');

////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Sidebar.SidebarController

Open.TestHarness.Sidebar.SidebarController = function Open_TestHarness_Sidebar_SidebarController() {
    /// <summary>
    /// Controller for the side-bar.
    /// </summary>
    /// <field name="_listTree$2" type="Open.Core.Lists.ListTreeView">
    /// </field>
    /// <field name="_backController$2" type="Open.Core.Lists.ListTreeBackController">
    /// </field>
    Open.TestHarness.Sidebar.SidebarController.initializeBase(this);
    this._listTree$2 = new Open.Core.Lists.ListTreeView($(Open.TestHarness.CssSelectors.sidebarList));
    this._listTree$2.set_slideDuration(0.2);
    this._backController$2 = new Open.Core.Lists.ListTreeBackController(this._listTree$2, $(Open.TestHarness.CssSelectors.sidebarToolbar), $(Open.TestHarness.CssSelectors.homeButton));
    this._TEMP$2();
}
Open.TestHarness.Sidebar.SidebarController.prototype = {
    _listTree$2: null,
    _backController$2: null,
    
    onDisposed: function Open_TestHarness_Sidebar_SidebarController$onDisposed() {
        this._backController$2.dispose();
        Open.TestHarness.Sidebar.SidebarController.callBaseMethod(this, 'onDisposed');
    },
    
    _TEMP$2: function Open_TestHarness_Sidebar_SidebarController$_TEMP$2() {
        var rootNode = new Open.TestHarness.Sidebar.MyNode('Root');
        rootNode.add(new Open.TestHarness.Sidebar.MyNode('Recent'));
        rootNode.add(new Open.TestHarness.Sidebar.MyNode('Child 2'));
        rootNode.add(new Open.TestHarness.Sidebar.MyNode('Child 3'));
        var child1 = Type.safeCast(rootNode.childAt(0), Open.TestHarness.Sidebar.MyNode);
        var child2 = (rootNode.childAt(1));
        var child3 = (rootNode.childAt(2));
        child1.add(new Open.TestHarness.Sidebar.MyNode('Recent Child 1'));
        child1.add(new Open.TestHarness.Sidebar.MyNode('Recent Child 2'));
        child1.add(new Open.TestHarness.Sidebar.MyNode('Recent Child 3'));
        var recent1 = Type.safeCast(child1.childAt(0), Open.TestHarness.Sidebar.MyNode);
        recent1.add(new Open.TestHarness.Sidebar.MyNode('Recent Grandchild 1'));
        recent1.add(new Open.TestHarness.Sidebar.MyNode('Recent Grandchild 2'));
        recent1.add(new Open.TestHarness.Sidebar.MyNode('Recent Grandchild 3'));
        child2.add(new Open.TestHarness.Sidebar.MyNode('Yo Child'));
        child3.add(new Open.TestHarness.Sidebar.MyNode('Yo Child'));
        this._listTree$2.set_rootNode(rootNode);
        child1.set_text('My Recent Foo');
        child2.set_canSelect(false);
        child2.set_isSelected(true);
        child3.set_rightIconSrc('http://www.feedicons.com/images/standard-icons.gif');
    }
}


////////////////////////////////////////////////////////////////////////////////
// Open.TestHarness.Sidebar.MyNode

Open.TestHarness.Sidebar.MyNode = function Open_TestHarness_Sidebar_MyNode(text) {
    /// <param name="text" type="String">
    /// </param>
    Open.TestHarness.Sidebar.MyNode.initializeBase(this);
    this.set_text(text);
}


Open.TestHarness.CssSelectors.registerClass('Open.TestHarness.CssSelectors');
Open.TestHarness.Elements.registerClass('Open.TestHarness.Elements');
Open.TestHarness.Application.registerClass('Open.TestHarness.Application');
Open.TestHarness.Shell.PanelResizeController.registerClass('Open.TestHarness.Shell.PanelResizeController', Open.Core.ControllerBase);
Open.TestHarness.Sidebar.SidebarController.registerClass('Open.TestHarness.Sidebar.SidebarController', Open.Core.ControllerBase);
Open.TestHarness.Sidebar.MyNode.registerClass('Open.TestHarness.Sidebar.MyNode', Open.Core.Lists.ListItem);
Open.TestHarness.CssSelectors.sidebar = '#testHarnessSidebar';
Open.TestHarness.CssSelectors.sidebarList = '#testHarnessSidebar .th-sidebarList';
Open.TestHarness.CssSelectors.sidebarToolbar = '#testHarnessSidebar div.th-toolbar';
Open.TestHarness.CssSelectors.homeButton = '#testHarnessSidebar img.th-home';
Open.TestHarness.CssSelectors.main = '#testHarness .th-main';
Open.TestHarness.CssSelectors.logTitlebar = '#testHarnessLog .th-log-titlebar';
Open.TestHarness.CssSelectors.log = '#testHarnessLog .coreLog';
Open.TestHarness.Elements.root = 'testHarness';
Open.TestHarness.Elements.outputLog = 'testHarnessLog';
Open.TestHarness.Application._resizeController = null;
Open.TestHarness.Application._sidebarController = null;
Open.TestHarness.Shell.PanelResizeController._sidebarMinWidth$2 = 200;
Open.TestHarness.Shell.PanelResizeController._sidebarMaxWidthMargin$2 = 80;
Open.TestHarness.Shell.PanelResizeController._outputLogMaxHeightMargin$2 = 80;

// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------

}
ss.loader.registerScript('TestHarness.Script3', ['Open.Core.Views','Open.Core.Script','Open.Core.Lists'], executeScript);
})();
