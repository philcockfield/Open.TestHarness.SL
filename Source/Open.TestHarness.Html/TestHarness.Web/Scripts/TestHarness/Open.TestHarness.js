// Open.TestHarness.js
(function(){function executeScript(){
Type.registerNamespace('Open.TestHarness');Open.TestHarness.CssSelectors=function(){}
Open.TestHarness.Elements=function(){}
Open.TestHarness.Application=function(){}
Open.TestHarness.Application.main=function(args){Open.TestHarness.Application.$1=new Open.TestHarness.Shell.PanelResizeController();Open.TestHarness.Application.$0=new Open.TestHarness.Log.LogView($(Open.TestHarness.CssSelectors.logList).first());for(var $0=0;$0<3;$0++){Open.TestHarness.Application.$0.write('Hello '+$0);}}
Type.registerNamespace('Open.TestHarness.Log');Open.TestHarness.Log.LogView=function(divLogList){Open.TestHarness.Log.LogView.initializeBase(this);this.initialize(divLogList);}
Open.TestHarness.Log.LogView.prototype={write:function(message){var $0=Open.Core.Html.createElement(Open.Core.Html.div);$0.addClass('th-log-listItem');$0.append(message);$0.appendTo(this.get_container());}}
Type.registerNamespace('Open.TestHarness.Shell');Open.TestHarness.Shell.PanelResizeController=function(){this.$3=new Open.Core.UI.HorizontalPanelResizer(Open.TestHarness.CssSelectors.sidebar,'TH_SB');this.$3.add_resized(ss.Delegate.create(this,function(){
Open.TestHarness.Shell.PanelResizeController.$6();}));this.$3.set_minWidth(200);this.$3.set_maxWidthMargin(80);Open.TestHarness.Shell.PanelResizeController.$5(this.$3);Open.TestHarness.Shell.PanelResizeController.$6();this.$4=new Open.Core.UI.VerticalPanelResizer(Open.Core.Css.toId('testHarnessLog'),'TH_OL');this.$4.add_resized(ss.Delegate.create(this,function(){
}));this.$4.set_minHeight($(Open.TestHarness.CssSelectors.logTitlebar).height());this.$4.set_maxHeightMargin(80);Open.TestHarness.Shell.PanelResizeController.$5(this.$4);}
Open.TestHarness.Shell.PanelResizeController.$5=function($p0){$p0.set_rootContainerId('testHarness');$p0.initialize();}
Open.TestHarness.Shell.PanelResizeController.$6=function(){$(Open.TestHarness.CssSelectors.main).css(Open.Core.Css.left,$(Open.TestHarness.CssSelectors.sidebar).width()+1+Open.Core.Css.px);}
Open.TestHarness.Shell.PanelResizeController.prototype={$3:null,$4:null}
Open.TestHarness.CssSelectors.registerClass('Open.TestHarness.CssSelectors');Open.TestHarness.Elements.registerClass('Open.TestHarness.Elements');Open.TestHarness.Application.registerClass('Open.TestHarness.Application');Open.TestHarness.Log.LogView.registerClass('Open.TestHarness.Log.LogView',Open.Core.ViewBase);Open.TestHarness.Shell.PanelResizeController.registerClass('Open.TestHarness.Shell.PanelResizeController');Open.TestHarness.CssSelectors.classLogListItem='th-log-listItem';Open.TestHarness.CssSelectors.main='#testHarness .th-main';Open.TestHarness.CssSelectors.sidebar='#testHarness .th-sidebar';Open.TestHarness.CssSelectors.logTitlebar='#testHarnessLog .th-log-titlebar';Open.TestHarness.CssSelectors.logList='#testHarnessLog div.th-log-list';Open.TestHarness.CssSelectors.logListItem='#testHarnessLog div.'+'th-log-listItem';Open.TestHarness.Elements.root='testHarness';Open.TestHarness.Elements.outputLog='testHarnessLog';Open.TestHarness.Application.$0=null;Open.TestHarness.Application.$1=null;
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('TestHarness.Script3',['Open.Core.Script'],executeScript);})();