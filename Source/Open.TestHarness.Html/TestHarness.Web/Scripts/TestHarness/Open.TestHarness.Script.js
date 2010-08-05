// Open.TestHarness.Script.js
(function(){function executeScript(){
Type.registerNamespace('Open.TestHarness');Open.TestHarness.Elements=function(){}
Open.TestHarness.Application=function(){}
Open.TestHarness.Application.main=function(args){Open.TestHarness.Application.$0=new Open.TestHarness.Shell.PanelResizeController();}
Type.registerNamespace('Open.TestHarness.Shell');Open.TestHarness.Shell.PanelResizeController=function(){this.$4=new Open.Core.UI.HorizontalPanelResizer('#sidebar');this.$4.add_resized(ss.Delegate.create(this,function(){
Open.TestHarness.Shell.PanelResizeController.$7();}));this.$4.set_minWidth(200);this.$4.set_maxWidthMargin(80);Open.TestHarness.Shell.PanelResizeController.$6(this.$4);this.$5=new Open.Core.UI.VerticalPanelResizer('#outputLog');this.$5.add_resized(ss.Delegate.create(this,function(){
}));this.$5.set_minHeight(30);this.$5.set_maxHeightMargin(80);Open.TestHarness.Shell.PanelResizeController.$6(this.$5);}
Open.TestHarness.Shell.PanelResizeController.$6=function($p0){$p0.set_rootContainerId('#root');$p0.initialize();}
Open.TestHarness.Shell.PanelResizeController.$7=function(){$('#main').css('left',$('#sidebar').width()+1+'px');}
Open.TestHarness.Shell.PanelResizeController.prototype={$4:null,$5:null}
Open.TestHarness.Elements.registerClass('Open.TestHarness.Elements');Open.TestHarness.Application.registerClass('Open.TestHarness.Application');Open.TestHarness.Shell.PanelResizeController.registerClass('Open.TestHarness.Shell.PanelResizeController');Open.TestHarness.Elements.root='#root';Open.TestHarness.Elements.sideBar='#sidebar';Open.TestHarness.Elements.main='#main';Open.TestHarness.Elements.outputLog='#outputLog';Open.TestHarness.Application.$0=null;
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('TestHarness.Script3',['Open.Core.Script'],executeScript);})();