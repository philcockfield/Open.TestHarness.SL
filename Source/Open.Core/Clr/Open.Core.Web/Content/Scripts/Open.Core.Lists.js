// Open.Core.Lists.js
(function(){function executeScript(){
Type.registerNamespace('Open.Core.Lists');Open.Core.Lists.IListItemView=function(){};Open.Core.Lists.IListItemView.registerInterface('Open.Core.Lists.IListItemView');Open.Core.Lists.ListSelectionMode=function(){};Open.Core.Lists.ListSelectionMode.prototype = {none:0,single:1}
Open.Core.Lists.ListSelectionMode.registerEnum('Open.Core.Lists.ListSelectionMode',false);Open.Core.Lists.ListCss=function(){}
Open.Core.Lists.ListCss.insertCss=function(){if(Open.Core.Lists.ListCss.$0){return;}Open.Core.Css.insertLink('/Open.Core/Css/Core.Lists.css');Open.Core.Lists.ListCss.$0=true;}
Open.Core.Lists.ListCssClasses=function(){}
Open.Core.Lists.ListCssClasses.prototype={listItem:'coreListItem',selectedListItem:'selectedListItem',itemLabel:'itemLabel'}
Open.Core.Lists._ListTreePanel=function(listTreeView,rootDiv,rootNode){Open.Core.Lists._ListTreePanel.initializeBase(this);this.$2_1=listTreeView;this.$2_3=rootDiv;this.$2_4=rootNode;rootNode.add_childSelectionChanged(ss.Delegate.create(this,this.$2_5));}
Open.Core.Lists._ListTreePanel.prototype={$2_0:null,$2_1:null,$2_2:null,$2_3:null,$2_4:null,onDisposed:function(){this.$2_0.empty();this.$2_4.remove_childSelectionChanged(ss.Delegate.create(this,this.$2_5));Open.Core.Lists._ListTreePanel.callBaseMethod(this, 'onDisposed');},$2_5:function($p0,$p1){var $0=this.$2_F();if(!ss.isNullOrUndefined($0)){this.$2_1.set_selectedNode($0);}},get_$2_6:function(){return this.$2_4;},get_$2_7:function(){return this.$2_3.width();},get_$2_8:function(){return Open.Core.Helper.get_number().toMsecs(this.$2_1.get_slideDuration());},onInitialize:function($p0){this.$2_0=Open.Core.Html.appendDiv($p0);this.$2_0=$p0.children(Open.Core.Html.div).last();this.$2_D();Open.Core.Css.absoluteFill(this.$2_0);this.$2_2=new Open.Core.Lists.ListView(this.$2_0);this.$2_2.load(this.$2_4.get_children());this.$2_E();},$2_9:function($p0,$p1){if(!this.get_isInitialized()){return;}this.$2_B();var $0={};$0[Open.Core.Css.left]=($p0===0)?0-this.get_$2_7():this.get_$2_7();this.$2_0.animate($0,this.get_$2_8(),this.$2_1.get_slideEasing(),ss.Delegate.create(this,function(){
this.$2_D();Open.Core.Helper.invokeOrDefault($p1);}));},$2_A:function($p0,$p1){if(!this.get_isInitialized()){return;}this.$2_C($p0,true);var $0={};$0[Open.Core.Css.left]=0;this.$2_0.animate($0,this.get_$2_8(),this.$2_1.get_slideEasing(),ss.Delegate.create(this,function(){
Open.Core.Helper.invokeOrDefault($p1);}));},$2_B:function(){this.$2_0.css(Open.Core.Css.left,'0px');this.$2_0.css(Open.Core.Css.display,Open.Core.Css.block);this.$2_E();},$2_C:function($p0,$p1){var $0=($p0===1)?0-this.get_$2_7():this.get_$2_7();this.$2_0.css(Open.Core.Css.left,$0+'px');this.$2_0.css(Open.Core.Css.display,($p1)?Open.Core.Css.block:Open.Core.Css.none);this.$2_E();},$2_D:function(){this.$2_0.css(Open.Core.Css.display,Open.Core.Css.none);},$2_E:function(){this.$2_0.width(this.get_$2_7());},$2_F:function(){var $enum1=ss.IEnumerator.getEnumerator(this.$2_4.get_children());while($enum1.moveNext()){var $0=$enum1.get_current();if($0.get_isSelected()){return $0;}}return null;}}
Open.Core.Lists.ListTreeView=function(container){this.$2_4='swing';this.$2_5=[];Open.Core.Lists.ListTreeView.initializeBase(this);this.initialize(container);Open.Core.Lists.ListCss.insertCss();}
Open.Core.Lists.ListTreeView.$2_7=function($p0,$p1){if($p0==null){return 0;}return ($p0.containsDescendent($p1))?0:1;}
Open.Core.Lists.ListTreeView.prototype={add_selectionChanged:function(value){this.$2_0=ss.Delegate.combine(this.$2_0,value);},remove_selectionChanged:function(value){this.$2_0=ss.Delegate.remove(this.$2_0,value);},$2_0:null,$2_1:function(){if(this.$2_0!=null){this.$2_0.invoke(this,new ss.EventArgs());}},$2_2:null,$2_3:0.4,onDisposed:function(){this.$2_C();Open.Core.Lists.ListTreeView.callBaseMethod(this, 'onDisposed');},get_rootNode:function(){return Type.safeCast(this.get('RootNode',null),Open.Core.ITreeNode);},set_rootNode:function(value){if(this.set('RootNode',value,null)){this.$2_C();this.set_selectedNode(value);}return value;},get_selectedNode:function(){return Type.safeCast(this.get('SelectedNode',null),Open.Core.ITreeNode);},set_selectedNode:function(value){if(this.set('SelectedNode',value,null)){if(value!=null&&value.get_totalChildren()>0){this.set_currentListRoot(value);}this.$2_1();}return value;},get_currentListRoot:function(){return this.get('CurrentListRoot',null);},set_currentListRoot:function(value){var $0=this.get_currentListRoot();if(this.set('CurrentListRoot',value,null)){if(value!=null){this.$2_8(value);if(value.get_totalChildren()>0){if($0==null){this.$2_9(value,true).$2_B();}else{this.$2_6($0,value);}}}this.firePropertyChanged('CurrentListRoot');}return value;},get_slideDuration:function(){return this.$2_3;},set_slideDuration:function(value){this.$2_3=value;return value;},get_slideEasing:function(){return this.$2_4;},set_slideEasing:function(value){this.$2_4=value;return value;},onInitialize:function(container){this.$2_2=Open.Core.Html.appendDiv(container);Open.Core.Css.absoluteFill(this.$2_2);Open.Core.Css.setOverflow(this.$2_2,1);},back:function(){if(this.get_currentListRoot()==null||this.get_currentListRoot().get_isRoot()){return;}this.set_selectedNode(this.get_currentListRoot().get_parent());},$2_6:function($p0,$p1){var $0=Open.Core.Lists.ListTreeView.$2_7($p0,$p1);if($p0!=null){var $2=this.$2_9($p0,true);$2.$2_9($0,null);}var $1=this.$2_9($p1,true);$1.$2_A($0,null);},$2_8:function($p0){var $enum1=ss.IEnumerator.getEnumerator($p0.get_children());while($enum1.moveNext()){var $0=$enum1.get_current();$0.set_isSelected(false);}},$2_9:function($p0,$p1){var $0=this.$2_A($p0)||this.$2_B($p0);if($p1&&!$0.get_isInitialized()){$0.initialize(this.$2_2);}return $0;},$2_A:function($p0){var $enum1=ss.IEnumerator.getEnumerator(this.$2_5);while($enum1.moveNext()){var $0=$enum1.get_current();if($0.get_$2_6()===$p0){return $0;}}return null;},$2_B:function($p0){var $0=new Open.Core.Lists._ListTreePanel(this,this.$2_2,$p0);this.$2_5.add($0);return $0;},$2_C:function(){var $enum1=ss.IEnumerator.getEnumerator(this.$2_5);while($enum1.moveNext()){var $0=$enum1.get_current();$0.dispose();}}}
Open.Core.Lists.ListView=function(container){this.$2_1=1;this.$2_2=[];Open.Core.Lists.ListView.initializeBase(this);this.initialize(container);Open.Core.Lists.ListCss.insertCss();}
Open.Core.Lists.ListView.prototype={$2_0:null,$2_3:function($p0,$p1){if(this.get_selectionMode()===0){return;}$p1.set_isSelected(true);},$2_4:function($p0,$p1){if($p1.get_property().get_name()==='IsSelected'){var $0=Type.safeCast($p0,Open.Core.Lists.IListItemView);if($0!=null&&$0.get_isSelected()){this.$2_6($0);}}},get_$2_5:function(){return this.$2_0||(this.$2_0=new Open.Core.Lists.OpenCoreLists$0());},get_selectionMode:function(){return this.$2_1;},set_selectionMode:function(value){this.$2_1=value;return value;},get_count:function(){return this.$2_2.length;},onInitialize:function(container){},load:function(items){this.clear();if(ss.isNullOrUndefined(items)){return;}var $0=Open.Core.Helper.get_collection().toArrayList(items);for(var $1=0;$1<$0.length;$1++){var $2=Open.Core.Html.appendDiv(this.get_container());$2.appendTo(this.get_container());}this.get_container().children(Open.Core.Html.div).each(ss.Delegate.create(this,function($p1_0,$p1_1){
var $1_0=$($p1_1);var $1_1=$0[$p1_0];var $1_2=this.get_$2_5().$0($1_0,$1_1);var $1_3=Type.safeCast($1_2,Open.Core.Lists.IListItemView);this.$2_2.add($1_2);if($1_3!=null){$1_0.click(ss.Delegate.create(this,function($p2_0){
this.$2_3($p2_0,$1_3);}));}var $1_4=Type.safeCast($1_2,Open.Core.INotifyPropertyChanged);if($1_4!=null){$1_4.add_propertyChanged(ss.Delegate.create(this,this.$2_4));}}));},clear:function(){var $enum1=ss.IEnumerator.getEnumerator(this.$2_2);while($enum1.moveNext()){var $0=$enum1.get_current();var $1=Type.safeCast($0,Open.Core.INotifyPropertyChanged);if($1!=null){$1.remove_propertyChanged(ss.Delegate.create(this,this.$2_4));}$0.dispose();}this.get_container().empty();this.$2_2.clear();},$2_6:function($p0){if(ss.isNullOrUndefined($p0)){return;}this.$2_7($p0);$p0.set_isSelected(true);},$2_7:function($p0){var $enum1=ss.IEnumerator.getEnumerator(this.$2_8());while($enum1.moveNext()){var $0=$enum1.get_current();if(!ss.isNullOrUndefined($0)&&$0!==$p0){$0.set_isSelected(false);}}},$2_8:function(){var $0=new Array(this.$2_2.length);var $enum1=ss.IEnumerator.getEnumerator(this.$2_2);while($enum1.moveNext()){var $1=$enum1.get_current();var $2=Type.safeCast($1,Open.Core.Lists.IListItemView);if(!ss.isNullOrUndefined($2)){$0.add($2);}}return $0;}}
Open.Core.Lists.ListItemView=function(liElement,model){Open.Core.Lists.ListItemView.initializeBase(this);this.$2_0=model;this.initialize(liElement);this.$2_3=Open.Core.PropertyRef.getFromModel(model,'IsSelected');if(this.$2_3!=null){this.$2_3.add_changed(ss.Delegate.create(this,this.$2_4));}this.$2_5();}
Open.Core.Lists.ListItemView.prototype={$2_0:null,$2_1:null,$2_2:null,$2_3:null,onDisposed:function(){if(this.$2_3!=null){this.$2_3.remove_changed(ss.Delegate.create(this,this.$2_4));}Open.Core.Lists.ListItemView.callBaseMethod(this, 'onDisposed');},$2_4:function($p0,$p1){this.$2_5();this.firePropertyChanged('IsSelected');},get_isSelected:function(){return (this.$2_3==null)?false:this.$2_3.get_value();},set_isSelected:function(value){if(value===this.get_isSelected()){return;}if(this.$2_3!=null){this.$2_3.set_value(value);}return value;},get_model:function(){return this.$2_0;},get_text:function(){return this.$2_2;},set_text:function(value){this.$2_2=value;if(this.$2_1!=null){this.$2_1.html(this.$2_2);}return value;},onInitialize:function(container){this.$2_1=Open.Core.Html.createElement(Open.Core.Html.span);this.$2_1.appendTo(container);container.addClass(Open.Core.Lists.ListCss.classes.listItem);this.$2_1.addClass(Open.Core.Lists.ListCss.classes.itemLabel);this.$2_1.addClass(Open.Core.Css.classes.titleFont);this.$2_6(Type.safeCast(this.get_model(),Open.Core.IModel));},$2_5:function(){if(this.get_isSelected()){this.get_container().addClass(Open.Core.Lists.ListCss.classes.selectedListItem);}else{this.get_container().removeClass(Open.Core.Lists.ListCss.classes.selectedListItem);}},$2_6:function($p0){if($p0==null){return;}this.$2_7($p0,'Text');},$2_7:function($p0,$p1){var $0=$p0.getPropertyRef($p1);if($0!=null){this.getPropertyRef($p1).set_bindTo($0);}}}
Open.Core.Lists.OpenCoreLists$0=function(){}
Open.Core.Lists.OpenCoreLists$0.prototype={$0:function($p0,$p1){return new Open.Core.Lists.ListItemView($p0,$p1);}}
Open.Core.Lists.ListCss.registerClass('Open.Core.Lists.ListCss');Open.Core.Lists.ListCssClasses.registerClass('Open.Core.Lists.ListCssClasses');Open.Core.Lists._ListTreePanel.registerClass('Open.Core.Lists._ListTreePanel',Open.Core.ViewBase);Open.Core.Lists.ListTreeView.registerClass('Open.Core.Lists.ListTreeView',Open.Core.ViewBase);Open.Core.Lists.ListView.registerClass('Open.Core.Lists.ListView',Open.Core.ViewBase);Open.Core.Lists.ListItemView.registerClass('Open.Core.Lists.ListItemView',Open.Core.ViewBase,Open.Core.Lists.IListItemView);Open.Core.Lists.OpenCoreLists$0.registerClass('Open.Core.Lists.OpenCoreLists$0');Open.Core.Lists.ListCss.url='/Open.Core/Css/Core.Lists.css';Open.Core.Lists.ListCss.$0=false;Open.Core.Lists.ListCss.classes=new Open.Core.Lists.ListCssClasses();Open.Core.Lists.ListTreeView.propRootNode='RootNode';Open.Core.Lists.ListTreeView.propSelectedNode='SelectedNode';Open.Core.Lists.ListTreeView.propCurrentListRoot='CurrentListRoot';Open.Core.Lists.ListItemView.propText='Text';Open.Core.Lists.ListItemView.propIsSelected='IsSelected';
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('Open.Core.Lists',['Open.Core.Script'],executeScript);})();