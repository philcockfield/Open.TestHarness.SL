// Open.Core.Lists.js
(function(){function executeScript(){
Type.registerNamespace('Open.Core.Lists');Open.Core.Lists.List=function(element){Open.Core.Lists.List.initializeBase(this);this.initialize(element);}
Open.Core.Lists.List.prototype={$1_0:null,$1_1:null,$1_2:null,get_itemFactory:function(){return this.$1_0||(this.$1_1||(this.$1_1=new Open.Core.Lists.OpenCoreLists$0()));},set_itemFactory:function(value){this.$1_0=value;return value;},onInitialize:function(element){this.$1_2=element.append('<ul></ul>');},load:function(models){this.$1_2.empty();if(models==null){return;}var $0=this.get_itemFactory();var $enum1=ss.IEnumerator.getEnumerator(models);while($enum1.moveNext()){var $1=$enum1.get_current();var $2=$(String.format('<li></li>'));this.$1_2.append($2);var $3=$0.createView($2,$1);}}}
Open.Core.Lists.ListItem=function(liElement,model){Open.Core.Lists.ListItem.initializeBase(this);this.$1_0=model;this.initialize(liElement);}
Open.Core.Lists.ListItem.prototype={$1_0:null,onInitialize:function(liElement){var $0=this.$1_0.get_text();liElement.append(String.format('<span>{0}</span>',(ss.isNullOrUndefined($0))?'No Text':$0));}}
Open.Core.Lists.OpenCoreLists$0=function(){}
Open.Core.Lists.OpenCoreLists$0.prototype={createView:function($p0,$p1){return new Open.Core.Lists.ListItem($p0,$p1);}}
Open.Core.Lists.List.registerClass('Open.Core.Lists.List',Open.Core.ViewBase);Open.Core.Lists.ListItem.registerClass('Open.Core.Lists.ListItem',Open.Core.ViewBase);Open.Core.Lists.OpenCoreLists$0.registerClass('Open.Core.Lists.OpenCoreLists$0',null,Open.Core.IViewFactory);
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('Open.Core.Lists',['Open.Core.Script'],executeScript);})();