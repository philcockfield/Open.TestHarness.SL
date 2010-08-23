// Open.Library.Jit.js
(function(){function executeScript(){
Type.registerNamespace('Open.Library.Jit');Open.Library.Jit.$create_HypertreeNode=function(id,name){var $o={};$o.id=id;$o.name=name;$o.children=[];return $o;}
Open.Library.Jit.CssSelectors=function(){}
Open.Library.Jit.Elements=function(){}
Open.Library.Jit.Hypertree=function(containerElement){if(containerElement==null){throw new Error('Container element not specified');}this.$1=containerElement;$(window).bind(Open.Core.DomEvents.resize,ss.Delegate.create(this,function($p1_0){
this.$3();}));}
Open.Library.Jit.Hypertree.prototype={$0:null,$1:null,$2:false,initialize:function(callback){Open.Core.Helper.get_scriptLoader().get_jit().loadHypertree(ss.Delegate.create(this,function(){
var $1_0=this.$1.attr('id');this.$0=insertTree($1_0);this.$3();this.$2=true;Open.Core.Helper.invokeOrDefault(callback);}));},load:function(rootNode){if(!this.$2){throw new Error('HyperTree not initialized');}this._hyperTree.loadJSON(rootNode);this.refresh();},refresh:function(){if(!this.$2){return;}this._hyperTree.refresh();this._hyperTree.controller.onAfterCompute();},$3:function(){if(!this.$2){return;}var $0=this.$4();this._hyperTree.canvas.resize($0.get_width(), $0.get_height());this.refresh();},$4:function(){return new Open.Core.Size(this.$1.width(),this.$1.height());}}
Open.Library.Jit.CssSelectors.registerClass('Open.Library.Jit.CssSelectors');Open.Library.Jit.Elements.registerClass('Open.Library.Jit.Elements');Open.Library.Jit.Hypertree.registerClass('Open.Library.Jit.Hypertree');
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('Open.Library.Jit',['Open.Core.Script'],executeScript);})();