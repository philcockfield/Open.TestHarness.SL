// Open.TestHarness.js
(function(){function executeScript(){
Type.registerNamespace('Open.TestHarness');Open.TestHarness.CssSelectors=function(){}
Open.TestHarness.Elements=function(){}
Open.TestHarness.Application=function(){}
Open.TestHarness.Application.main=function(args){Open.TestHarness.Application.$0=new Open.TestHarness.Shell.PanelResizeController();Open.TestHarness.Application.$1=new Open.TestHarness.Sidebar.SidebarController();var $0=new Open.Core.Controls.LogView($(Open.TestHarness.CssSelectors.log).first());Open.Core.Log.registerView($0);}
Type.registerNamespace('Open.TestHarness.Shell');Open.TestHarness.Shell.PanelResizeController=function(){Open.TestHarness.Shell.PanelResizeController.initializeBase(this);this.$2_3=new Open.Core.UI.HorizontalPanelResizer(Open.TestHarness.CssSelectors.sidebar,'TH_SB');this.$2_3.add_resized(ss.Delegate.create(this,function(){
Open.TestHarness.Shell.PanelResizeController.$2_6();}));this.$2_3.set_minWidth(200);this.$2_3.set_maxWidthMargin(80);Open.TestHarness.Shell.PanelResizeController.$2_5(this.$2_3);Open.TestHarness.Shell.PanelResizeController.$2_6();this.$2_4=new Open.Core.UI.VerticalPanelResizer(Open.Core.Css.toId('testHarnessLog'),'TH_OL');this.$2_4.add_resized(ss.Delegate.create(this,function(){
}));this.$2_4.set_minHeight($(Open.TestHarness.CssSelectors.logTitlebar).height());this.$2_4.set_maxHeightMargin(80);Open.TestHarness.Shell.PanelResizeController.$2_5(this.$2_4);}
Open.TestHarness.Shell.PanelResizeController.$2_5=function($p0){$p0.set_rootContainerId('testHarness');$p0.initialize();}
Open.TestHarness.Shell.PanelResizeController.$2_6=function(){$(Open.TestHarness.CssSelectors.main).css(Open.Core.Css.left,$(Open.TestHarness.CssSelectors.sidebar).width()+1+Open.Core.Css.px);}
Open.TestHarness.Shell.PanelResizeController.prototype={$2_3:null,$2_4:null}
Type.registerNamespace('Open.TestHarness.Sidebar');Open.TestHarness.Sidebar.SidebarController=function(){Open.TestHarness.Sidebar.SidebarController.initializeBase(this);this.$2_0=new Open.Core.Lists.ListTreeView($(Open.TestHarness.CssSelectors.sidebarList));this.$2_0.set_slideDuration(0.2);this.$2_1=new Open.Core.Lists.ListTreeBackController(this.$2_0,$(Open.TestHarness.CssSelectors.sidebarToolbar),$(Open.TestHarness.CssSelectors.homeButton));this.$2_2();}
Open.TestHarness.Sidebar.SidebarController.prototype={$2_0:null,$2_1:null,onDisposed:function(){this.$2_1.dispose();Open.TestHarness.Sidebar.SidebarController.callBaseMethod(this, 'onDisposed');},$2_2:function(){var $0=new Open.TestHarness.Sidebar.MyNode('Root');$0.add(new Open.TestHarness.Sidebar.MyNode('Recent'));$0.add(new Open.TestHarness.Sidebar.MyNode('Child 2'));$0.add(new Open.TestHarness.Sidebar.MyNode('Child 3'));var $1=Type.safeCast($0.childAt(0),Open.TestHarness.Sidebar.MyNode);var $2=($0.childAt(1));var $3=($0.childAt(2));$1.add(new Open.TestHarness.Sidebar.MyNode('Recent Child 1'));$1.add(new Open.TestHarness.Sidebar.MyNode('Recent Child 2'));$1.add(new Open.TestHarness.Sidebar.MyNode('Recent Child 3'));var $4=Type.safeCast($1.childAt(0),Open.TestHarness.Sidebar.MyNode);$4.add(new Open.TestHarness.Sidebar.MyNode('Recent Grandchild 1'));$4.add(new Open.TestHarness.Sidebar.MyNode('Recent Grandchild 2'));$4.add(new Open.TestHarness.Sidebar.MyNode('Recent Grandchild 3'));$2.add(new Open.TestHarness.Sidebar.MyNode('Yo Child'));$3.add(new Open.TestHarness.Sidebar.MyNode('Yo Child'));this.$2_0.set_rootNode($0);$1.set_text('My Recent Foo');$2.set_canSelect(false);$2.set_isSelected(true);$3.set_rightIconSrc('http://www.feedicons.com/images/standard-icons.gif');}}
Open.TestHarness.Sidebar.MyNode=function(text){Open.TestHarness.Sidebar.MyNode.initializeBase(this);this.set_text(text);}
Open.TestHarness.CssSelectors.registerClass('Open.TestHarness.CssSelectors');Open.TestHarness.Elements.registerClass('Open.TestHarness.Elements');Open.TestHarness.Application.registerClass('Open.TestHarness.Application');Open.TestHarness.Shell.PanelResizeController.registerClass('Open.TestHarness.Shell.PanelResizeController',Open.Core.ControllerBase);Open.TestHarness.Sidebar.SidebarController.registerClass('Open.TestHarness.Sidebar.SidebarController',Open.Core.ControllerBase);Open.TestHarness.Sidebar.MyNode.registerClass('Open.TestHarness.Sidebar.MyNode',Open.Core.Lists.ListItem);Open.TestHarness.CssSelectors.sidebar='#testHarnessSidebar';Open.TestHarness.CssSelectors.sidebarList='#testHarnessSidebar .th-sidebarList';Open.TestHarness.CssSelectors.sidebarToolbar='#testHarnessSidebar div.th-toolbar';Open.TestHarness.CssSelectors.homeButton='#testHarnessSidebar img.th-home';Open.TestHarness.CssSelectors.main='#testHarness .th-main';Open.TestHarness.CssSelectors.logTitlebar='#testHarnessLog .th-log-titlebar';Open.TestHarness.CssSelectors.log='#testHarnessLog .coreLog';Open.TestHarness.Elements.root='testHarness';Open.TestHarness.Elements.outputLog='testHarnessLog';Open.TestHarness.Application.$0=null;Open.TestHarness.Application.$1=null;
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('TestHarness.Script3',['Open.Core.Views','Open.Core.Script','Open.Core.Lists'],executeScript);})();