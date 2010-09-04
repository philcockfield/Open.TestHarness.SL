// Open.Core.Lists.js
(function(){function executeScript(){
Type.registerNamespace('Open.Core.Lists');Open.Core.Lists.IListItemView=function(){};Open.Core.Lists.IListItemView.registerInterface('Open.Core.Lists.IListItemView');Open.Core.Lists.IListItem=function(){};Open.Core.Lists.IListItem.registerInterface('Open.Core.Lists.IListItem');Open.Core.Lists.ListSelectionMode=function(){};Open.Core.Lists.ListSelectionMode.prototype = {none:0,single:1}
Open.Core.Lists.ListSelectionMode.registerEnum('Open.Core.Lists.ListSelectionMode',false);Open.Core.Lists.ListHtml=function(){}
Open.Core.Lists.ListCss=function(){}
Open.Core.Lists.ListCss.insertCss=function(){if(Open.Core.Lists.ListCss.$0){return;}Open.Core.Css.insertLink('/Open.Core/Css/Core.Lists.css');Open.Core.Lists.ListCss.$0=true;}
Open.Core.Lists.ListClasses=function(){}
Open.Core.Lists.ListClasses.prototype={root:'c-list'}
Open.Core.Lists.ListItemClasses=function(){}
Open.Core.Lists.ListItemClasses.prototype={root:'c-listItem',defaultRoot:'c-listItem-default',selected:'c-listItem-selected',label:'c-listItem-label',iconRight:'c-listItem-iconRight'}
Open.Core.Lists.ListTreeBackController=function(listTree,backButton,backMask){Open.Core.Lists.ListTreeBackController.initializeBase(this);this.$2_0=listTree;this.$2_1=backButton;this.$2_2=backMask;listTree.add_propertyChanged(ss.Delegate.create(this,this.$2_3));backButton.click(ss.Delegate.create(this,this.$2_4));backMask.click(ss.Delegate.create(this,this.$2_4));}
Open.Core.Lists.ListTreeBackController.prototype={$2_0:null,$2_1:null,$2_2:null,onDisposed:function(){this.$2_0.remove_propertyChanged(ss.Delegate.create(this,this.$2_3));this.$2_1.unbind(Open.Core.Html.click,ss.Delegate.create(this,this.$2_4));this.$2_2.unbind(Open.Core.Html.click,ss.Delegate.create(this,this.$2_4));Open.Core.Lists.ListTreeBackController.callBaseMethod(this, 'onDisposed');},$2_3:function($p0,$p1){if($p1.get_property().get_name()==='SelectedParent'){this.$2_6();}},$2_4:function($p0){if(Open.Core.Keyboard.get_isAltPressed()){this.$2_0.home();}else{this.$2_0.back();}},get_listTree:function(){return this.$2_0;},get_backButton:function(){return this.$2_1;},get_backMask:function(){return this.$2_2;},get_$2_5:function(){var $0=this.$2_0.get_selectedParent();if($0==null){return false;}if($0.get_isRoot()){return false;}if($0.get_childCount()===0&&$0.get_parent().get_isRoot()){return false;}return true;},$2_6:function(){var $0=Open.Core.Helper.get_number().toMsecs(this.$2_0.get_slideDuration());var $1=Open.Core.Css.isVisible(this.$2_2);if(this.get_$2_5()){if(!$1){this.$2_2.fadeIn($0);}}else{if($1){this.$2_2.fadeOut($0);}}}}
Open.Core.Lists.ListItem=function(){Open.Core.Lists.ListItem.initializeBase(this);}
Open.Core.Lists.ListItem.create=function(text){var $0=new Open.Core.Lists.ListItem();$0.set_text(text);return $0;}
Open.Core.Lists.ListItem.prototype={get_text:function(){return this.get('Text',null);},set_text:function(value){this.set('Text',value,null);return value;},get_canSelect:function(){return this.get('CanSelect',true);},set_canSelect:function(value){this.set('CanSelect',value,true);return value;},get_rightIconSrc:function(){return this.get('RightIconSrc',null);},set_rightIconSrc:function(value){this.set('RightIconSrc',value,null);return value;},createHtml:function(){return null;},toString:function(){return String.format('{0} {1}',Open.Core.Lists.ListItem.callBaseMethod(this, 'toString'),this.get_text());}}
Open.Core.Lists._ListTreePanel=function(parentList,rootDiv,node){Open.Core.Lists._ListTreePanel.initializeBase(this);this.$2_3=parentList;this.$2_0=rootDiv;this.$2_2=node;Open.Core.GlobalEvents.add_horizontalPanelResized(ss.Delegate.create(this,this.$2_6));node.add_childSelectionChanged(ss.Delegate.create(this,this.$2_5));node.add_addedChild(ss.Delegate.create(this,this.$2_7));node.add_removedChild(ss.Delegate.create(this,this.$2_8));if(node.get_parent()!=null){node.get_parent().add_removingChild(ss.Delegate.create(this,this.$2_9));}}
Open.Core.Lists._ListTreePanel.prototype={$2_0:null,$2_1:null,$2_2:null,$2_3:null,$2_4:null,onDisposed:function(){this.$2_1.remove();Open.Core.GlobalEvents.remove_horizontalPanelResized(ss.Delegate.create(this,this.$2_6));this.$2_2.remove_childSelectionChanged(ss.Delegate.create(this,this.$2_5));this.$2_2.remove_addedChild(ss.Delegate.create(this,this.$2_7));this.$2_2.remove_removedChild(ss.Delegate.create(this,this.$2_8));if(this.$2_2.get_parent()!=null){this.$2_2.get_parent().remove_removingChild(ss.Delegate.create(this,this.$2_9));}Open.Core.Lists._ListTreePanel.callBaseMethod(this, 'onDisposed');},$2_5:function($p0,$p1){var $0=this.$2_14();if(!ss.isNullOrUndefined($0)){this.$2_3.set_selectedNode($0);}},$2_6:function($p0,$p1){this.$2_12();},$2_7:function($p0,$p1){this.$2_4.insert($p1.get_index(),$p1.get_node());},$2_8:function($p0,$p1){this.$2_4.remove($p1.get_node());},$2_9:function($p0,$p1){if($p1.get_node()!==this.$2_2){return;}if(this.$2_3.get_rootNode()!=null){var $0=Open.Core.Helper.get_tree().firstRemainingParent(this.$2_3.get_rootNode(),this.$2_2);this.$2_3.set_selectedParent($0||this.$2_3.get_rootNode());}this.dispose();},get_$2_A:function(){return this.$2_2;},get_$2_B:function(){return this.$2_1.css(Open.Core.Css.left)==='0px';},get_$2_C:function(){return this.$2_0.width();},get_$2_D:function(){return Open.Core.Helper.get_number().toMsecs(this.$2_3.get_slideDuration());},onInitialize:function($p0){this.$2_1=Open.Core.Html.appendDiv($p0);this.$2_1=$p0.children(Open.Core.Html.div).last();this.$2_13();Open.Core.Css.absoluteFill(this.$2_1);this.$2_4=new Open.Core.Lists.ListView(this.$2_1);this.$2_4.load(this.$2_2.get_children());this.$2_12();},$2_E:function($p0,$p1){if(!this.get_isInitialized()){return;}this.$2_10();var $0={};$0[Open.Core.Css.left]=($p0===0)?0-this.get_$2_C():this.get_$2_C();this.$2_1.animate($0,this.get_$2_D(),this.$2_3.get_slideEasing(),ss.Delegate.create(this,function(){
this.$2_13();Open.Core.Helper.invokeOrDefault($p1);}));},$2_F:function($p0,$p1){if(!this.get_isInitialized()){return;}this.$2_11($p0,true);var $0={};$0[Open.Core.Css.left]=0;this.$2_1.animate($0,this.get_$2_D(),this.$2_3.get_slideEasing(),ss.Delegate.create(this,function(){
Open.Core.Helper.invokeOrDefault($p1);}));},$2_10:function(){this.$2_1.css(Open.Core.Css.left,'0px');this.$2_1.css(Open.Core.Css.display,Open.Core.Css.block);this.$2_12();},$2_11:function($p0,$p1){var $0=($p0===1)?0-this.get_$2_C():this.get_$2_C();this.$2_1.css(Open.Core.Css.left,$0+Open.Core.Css.px);this.$2_1.css(Open.Core.Css.display,($p1)?Open.Core.Css.block:Open.Core.Css.none);this.$2_12();},$2_12:function(){this.$2_1.width(this.get_$2_C());},$2_13:function(){this.$2_1.css(Open.Core.Css.display,Open.Core.Css.none);},$2_14:function(){var $enum1=ss.IEnumerator.getEnumerator(this.$2_2.get_children());while($enum1.moveNext()){var $0=$enum1.get_current();if($0.get_isSelected()){return $0;}}return null;}}
Open.Core.Lists.ListTreeView=function(container){this.$2_6='swing';this.$2_7=[];Open.Core.Lists.ListTreeView.initializeBase(this);this.initialize(container);Open.Core.Lists.ListCss.insertCss();}
Open.Core.Lists.ListTreeView.$2_A=function($p0,$p1){if($p0==null){return 0;}return ($p0.containsDescendent($p1))?0:1;}
Open.Core.Lists.ListTreeView.$2_B=function($p0){var $enum1=ss.IEnumerator.getEnumerator($p0.get_children());while($enum1.moveNext()){var $0=$enum1.get_current();$0.set_isSelected(false);}}
Open.Core.Lists.ListTreeView.prototype={add_selectedNodeChanged:function(value){this.$2_0=ss.Delegate.combine(this.$2_0,value);},remove_selectedNodeChanged:function(value){this.$2_0=ss.Delegate.remove(this.$2_0,value);},$2_0:null,$2_1:function(){if(this.$2_0!=null){this.$2_0.invoke(this,new ss.EventArgs());}},add_selectedParentChanged:function(value){this.$2_2=ss.Delegate.combine(this.$2_2,value);},remove_selectedParentChanged:function(value){this.$2_2=ss.Delegate.remove(this.$2_2,value);},$2_2:null,$2_3:function(){if(this.$2_2!=null){this.$2_2.invoke(this,new ss.EventArgs());}},$2_4:null,$2_5:0.4,$2_8:null,onDisposed:function(){this.$2_F();Open.Core.Lists.ListTreeView.callBaseMethod(this, 'onDisposed');},get_rootNode:function(){return Type.safeCast(this.get('RootNode',null),Open.Core.ITreeNode);},set_rootNode:function(value){if(this.set('RootNode',value,null)){this.$2_F();this.set_selectedNode(value);}return value;},get_selectedNode:function(){return Type.safeCast(this.get('SelectedNode',null),Open.Core.ITreeNode);},set_selectedNode:function(value){if(this.set('SelectedNode',value,null)){if(value!=null&&(value.get_childCount()>0||value.get_isRoot())){this.set_selectedParent(value);}this.$2_1();}return value;},get_selectedParent:function(){return this.get('SelectedParent',null);},set_selectedParent:function(value){if(this.set('SelectedParent',value,null)){if(value!=null){Open.Core.Lists.ListTreeView.$2_B(value);if(this.$2_8==null){this.$2_C(value,true).$2_10();}else{this.$2_9(this.$2_8,value);}}this.$2_3();this.$2_8=value;}return value;},get_slideDuration:function(){return this.$2_5;},set_slideDuration:function(value){this.$2_5=value;return value;},get_slideEasing:function(){return this.$2_6;},set_slideEasing:function(value){this.$2_6=value;return value;},onInitialize:function(container){this.$2_4=Open.Core.Html.appendDiv(container);Open.Core.Css.absoluteFill(this.$2_4);Open.Core.Css.setOverflow(this.$2_4,1);},back:function(){if(this.get_selectedParent()==null||this.get_selectedParent().get_isRoot()){return;}this.set_selectedNode(this.get_selectedParent().get_parent());},home:function(){this.set_selectedNode(this.get_rootNode());},updateLayout:function(){var $enum1=ss.IEnumerator.getEnumerator(this.$2_7);while($enum1.moveNext()){var $0=$enum1.get_current();$0.$2_12();}},$2_9:function($p0,$p1){var $0=Open.Core.Lists.ListTreeView.$2_A($p0,$p1);if($p0!=null){var $2=this.$2_C($p0,true);$2.$2_E($0,null);}var $1=this.$2_C($p1,true);$1.$2_F($0,null);},$2_C:function($p0,$p1){var $0=this.$2_D($p0)||this.$2_E($p0);if($p1&&!$0.get_isInitialized()){$0.initialize(this.$2_4);}return $0;},$2_D:function($p0){var $enum1=ss.IEnumerator.getEnumerator(this.$2_7);while($enum1.moveNext()){var $0=$enum1.get_current();if($0.get_$2_A()===$p0){return $0;}}return null;},$2_E:function($p0){var $0=new Open.Core.Lists._ListTreePanel(this,this.$2_4,$p0);this.$2_7.add($0);return $0;},$2_F:function(){var $enum1=ss.IEnumerator.getEnumerator(this.$2_7);while($enum1.moveNext()){var $0=$enum1.get_current();$0.dispose();}}}
Open.Core.Lists.ListTemplates=function(){}
Open.Core.Lists.ListTemplates.defaultListItem=function(model){var $0=Type.safeCast(model,Open.Core.Lists.IListItem);var $1=Open.Core.Html.createDiv();var $2=Open.Core.Html.createSpan();$2.appendTo($1);var $3=($0==null)?'/Open.Core/Images/ListItem.ChildPointer.png':$0.get_rightIconSrc()||'/Open.Core/Images/ListItem.ChildPointer.png';var $4=Open.Core.Html.createImage($3,null);$4.addClass(Open.Core.Lists.ListCss.itemClasses.iconRight);$4.appendTo($1);$1.addClass(Open.Core.Lists.ListCss.itemClasses.defaultRoot);$2.addClass(Open.Core.Lists.ListCss.itemClasses.label);$2.addClass(Open.Core.Css.classes.titleFont);return $1;}
Open.Core.Lists.ListView=function(container){this.$2_1=1;this.$2_2=[];Open.Core.Lists.ListView.initializeBase(this);this.initialize(container);Open.Core.Lists.ListCss.insertCss();}
Open.Core.Lists.ListView.prototype={$2_0:null,onInitialize:function(container){container.addClass(Open.Core.Lists.ListCss.classes.root);Open.Core.Lists.ListView.callBaseMethod(this, 'onInitialize',[container]);},$2_3:function($p0,$p1){Open.Core.Helper.get_event().fireClick($p1.get_model());if(this.get_selectionMode()!==0){$p1.set_isSelected(true);}},$2_4:function($p0,$p1){if($p1.get_property().get_name()===Open.Core.TreeNode.propIsSelected){var $0=Type.safeCast($p0,Open.Core.Lists.IListItemView);if($0!=null&&$0.get_isSelected()){this.$2_8($0);}}},get_$2_5:function(){return this.$2_0||(this.$2_0=new Open.Core.Lists.OpenCoreLists$0());},get_selectionMode:function(){return this.$2_1;},set_selectionMode:function(value){this.$2_1=value;return value;},get_count:function(){return this.$2_2.length;},load:function(items){this.clear();this.insertRange(0,items);},insertRange:function(startingAt,models){if(ss.isNullOrUndefined(models)){return;}if(startingAt<0){startingAt=0;}var $enum1=ss.IEnumerator.getEnumerator(models);while($enum1.moveNext()){var $0=$enum1.get_current();this.insert(startingAt,$0);startingAt++;}},insert:function(index,model){var $0=this.$2_7(index);var $1=Open.Core.Html.createDiv();if($0==null){$1.appendTo(this.get_container());}else{$1.insertBefore($0);}var $2=this.get_$2_5().$0($1,model);var $3=Type.safeCast($2,Open.Core.Lists.IListItemView);this.$2_2.add($2);if($3!=null){$1.click(ss.Delegate.create(this,function($p1_0){
this.$2_3($p1_0,$3);}));}var $4=Type.safeCast($2,Open.Core.INotifyPropertyChanged);if($4!=null){$4.add_propertyChanged(ss.Delegate.create(this,this.$2_4));}},remove:function(model){if(model==null){return;}var $0=this.$2_B(model);this.$2_6(Type.safeCast($0,Open.Core.IView));},$2_6:function($p0){if($p0==null){return;}var $0=Type.safeCast($p0,Open.Core.INotifyPropertyChanged);if($0!=null){$0.remove_propertyChanged(ss.Delegate.create(this,this.$2_4));}$p0.dispose();this.$2_2.remove($p0);},clear:function(){var $enum1=ss.IEnumerator.getEnumerator(this.$2_2.clone());while($enum1.moveNext()){var $0=$enum1.get_current();this.$2_6($0);}},$2_7:function($p0){if($p0<0||this.get_count()===0){return null;}var $0=this.get_count()-1;if($p0>$0){return null;}return Open.Core.Html.childAt($p0,this.get_container());},$2_8:function($p0){if(ss.isNullOrUndefined($p0)){return;}this.$2_9($p0);$p0.set_isSelected(true);},$2_9:function($p0){var $enum1=ss.IEnumerator.getEnumerator(this.$2_A());while($enum1.moveNext()){var $0=$enum1.get_current();if(!ss.isNullOrUndefined($0)&&$0!==$p0){$0.set_isSelected(false);}}},$2_A:function(){return Open.Core.Helper.get_collection().filter(this.$2_2,ss.Delegate.create(this,function($p1_0){
return (Type.safeCast($p1_0,Open.Core.Lists.IListItemView))!=null;}));},$2_B:function($p0){return Type.safeCast(Open.Core.Helper.get_collection().first(this.$2_A(),ss.Delegate.create(this,function($p1_0){
return ($p1_0).get_model()===$p0;})),Open.Core.Lists.IListItemView);}}
Open.Core.Lists.ListItemView=function(container,model){Open.Core.Lists.ListItemView.initializeBase(this);this.$2_0=model;this.initialize(container);this.$2_4=Open.Core.PropertyRef.getFromModel(model,Open.Core.TreeNode.propIsSelected);if(this.$2_4!=null){this.$2_4.add_changed(ss.Delegate.create(this,this.$2_5));}if(this.get_$2_9()!=null){this.get_$2_9().add_childrenChanged(ss.Delegate.create(this,this.$2_6));}this.updateVisualState();}
Open.Core.Lists.ListItemView.$2_C=function($p0,$p1){return $p0.children(Open.Core.Css.toClass($p1)).first();}
Open.Core.Lists.ListItemView.prototype={$2_0:null,$2_1:null,$2_2:null,$2_3:null,$2_4:null,onDisposed:function(){if(this.$2_4!=null){this.$2_4.remove_changed(ss.Delegate.create(this,this.$2_5));}if(this.get_$2_9()!=null){this.get_$2_9().remove_childrenChanged(ss.Delegate.create(this,this.$2_6));}this.get_container().remove();Open.Core.Lists.ListItemView.callBaseMethod(this, 'onDisposed');},$2_5:function($p0,$p1){this.updateVisualState();this.firePropertyChanged(Open.Core.TreeNode.propIsSelected);},$2_6:function($p0,$p1){this.$2_B();},get_model:function(){return this.$2_0;},get_text:function(){return this.$2_3;},set_text:function(value){this.$2_3=value;if(this.$2_1!=null){this.$2_1.html(this.$2_3);}return value;},get_isSelected:function(){return (this.$2_4==null)?false:this.$2_4.get_value();},set_isSelected:function(value){if(!this.get_$2_A()){value=false;}if(value===this.get_isSelected()){return;}if(this.$2_4!=null){this.$2_4.set_value(value);}return value;},get_rightIconSrc:function(){return (this.$2_2==null)?null:this.$2_2.attr(Open.Core.Html.src);},set_rightIconSrc:function(value){value=value||'/Open.Core/Images/ListItem.ChildPointer.png';if(value===this.get_rightIconSrc()){return;}if(this.$2_2!=null){this.$2_2.attr(Open.Core.Html.src,value);}this.$2_B();return value;},get_$2_7:function(){return Type.safeCast(this.get_model(),Open.Core.IModel);},get_$2_8:function(){return Type.safeCast(this.get_model(),Open.Core.Lists.IListItem);},get_$2_9:function(){return Type.safeCast(this.get_model(),Open.Core.ITreeNode);},get_$2_A:function(){var $0=this.get_$2_8();return ($0==null)?true:$0.get_canSelect();},onInitialize:function(container){container.addClass(Open.Core.Lists.ListCss.itemClasses.root);var $0=this.$2_D();var $1=($0==null)?Open.Core.Lists.ListTemplates.defaultListItem(this.get_model()):$($0);$1.appendTo(container);this.$2_1=Open.Core.Lists.ListItemView.$2_C($1,Open.Core.Lists.ListCss.itemClasses.label);this.$2_2=Open.Core.Lists.ListItemView.$2_C($1,Open.Core.Lists.ListCss.itemClasses.iconRight);this.$2_2.load(ss.Delegate.create(this,function($p1_0){
this.$2_B();}));this.$2_E();this.updateVisualState();},updateVisualState:function(){Open.Core.Css.addOrRemoveClass(this.get_container(),Open.Core.Lists.ListCss.itemClasses.selected,this.get_isSelected());this.$2_B();},$2_B:function(){if(ss.isNullOrUndefined(this.$2_2)){return;}var $0=false;var $1=this.get_$2_9();if(this.get_$2_A()&&$1!=null&&$1.get_childCount()>0){$0=true;}Open.Core.Css.setVisible(this.$2_2,$0);if(!$0){return;}if(this.get_container().height()===0){return;}Open.Core.Html.centerVertically(this.$2_2,this.get_container());},$2_D:function(){var $0=Type.safeCast(this.get_model(),Open.Core.IHtmlFactory);return ($0==null)?null:$0.createHtml();},$2_E:function(){var $0=this.get_$2_7();if($0==null){return;}this.$2_F($0,'Text');this.$2_F($0,'RightIconSrc');},$2_F:function($p0,$p1){var $0=$p0.getPropertyRef($p1);if($0!=null){this.getPropertyRef($p1).set_bindTo($0);}}}
Open.Core.Lists.OpenCoreLists$0=function(){}
Open.Core.Lists.OpenCoreLists$0.prototype={$0:function($p0,$p1){return new Open.Core.Lists.ListItemView($p0,$p1);}}
Open.Core.Lists.ListHtml.registerClass('Open.Core.Lists.ListHtml');Open.Core.Lists.ListCss.registerClass('Open.Core.Lists.ListCss');Open.Core.Lists.ListClasses.registerClass('Open.Core.Lists.ListClasses');Open.Core.Lists.ListItemClasses.registerClass('Open.Core.Lists.ListItemClasses');Open.Core.Lists.ListTreeBackController.registerClass('Open.Core.Lists.ListTreeBackController',Open.Core.ControllerBase);Open.Core.Lists.ListItem.registerClass('Open.Core.Lists.ListItem',Open.Core.TreeNode,Open.Core.Lists.IListItem,Open.Core.IHtmlFactory);Open.Core.Lists._ListTreePanel.registerClass('Open.Core.Lists._ListTreePanel',Open.Core.ViewBase);Open.Core.Lists.ListTreeView.registerClass('Open.Core.Lists.ListTreeView',Open.Core.ViewBase);Open.Core.Lists.ListTemplates.registerClass('Open.Core.Lists.ListTemplates');Open.Core.Lists.ListView.registerClass('Open.Core.Lists.ListView',Open.Core.ViewBase);Open.Core.Lists.ListItemView.registerClass('Open.Core.Lists.ListItemView',Open.Core.ViewBase,Open.Core.Lists.IListItemView);Open.Core.Lists.OpenCoreLists$0.registerClass('Open.Core.Lists.OpenCoreLists$0');Open.Core.Lists.ListHtml.childPointerIcon='/Open.Core/Images/ListItem.ChildPointer.png';Open.Core.Lists.ListCss.url='/Open.Core/Css/Core.Lists.css';Open.Core.Lists.ListCss.$0=false;Open.Core.Lists.ListCss.itemClasses=new Open.Core.Lists.ListItemClasses();Open.Core.Lists.ListCss.classes=new Open.Core.Lists.ListClasses();Open.Core.Lists.ListItem.propText='Text';Open.Core.Lists.ListItem.propCanSelect='CanSelect';Open.Core.Lists.ListItem.propRightIconSrc='RightIconSrc';Open.Core.Lists.ListTreeView.propRootNode='RootNode';Open.Core.Lists.ListTreeView.propSelectedNode='SelectedNode';Open.Core.Lists.ListTreeView.propSelectedParent='SelectedParent';
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('Open.Core.Lists',['Open.Core.Script'],executeScript);})();