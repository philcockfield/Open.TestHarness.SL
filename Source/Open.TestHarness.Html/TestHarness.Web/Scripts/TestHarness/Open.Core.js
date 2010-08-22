// Open.Core.js
(function(){function executeScript(){
Type.registerNamespace('Open.Core');Open.Core.IViewFactory=function(){};Open.Core.IViewFactory.registerInterface('Open.Core.IViewFactory');Open.Core.IView=function(){};Open.Core.IView.registerInterface('Open.Core.IView');Open.Core.ViewBase=function(){}
Open.Core.ViewBase.prototype={$0:false,$1:false,$2:null,$3:null,dispose:function(){if(this.$0){return;}this.onDispose();this.$0=true;},onDispose:function(){},get_isDisposed:function(){return this.$0;},get_isInitialized:function(){return this.$1;},get_element:function(){return this.$3;},initialize:function(element){if(this.get_isInitialized()){throw new Error('View is already initialized.');}this.$3=element;this.onInitialize(element);this.$1=true;}}
Open.Core.Html=function(){}
Open.Core.Css=function(){}
Open.Core.Css.toId=function(identifier){if(String.isNullOrEmpty(identifier)){return identifier;}return (identifier.substr(0,1)==='#')?identifier:'#'+identifier;}
Open.Core.Css.selectFromId=function(identifier){return $(Open.Core.Css.toId(identifier));}
Open.Core.Css.insertLink=function(url){$('head').append(String.format('<link rel=\'Stylesheet\' href=\'{0}\' type=\'text/css\' />',url));}
Open.Core.Css.isLinked=function(url){return Open.Core.Css.getLink(url)!=null;}
Open.Core.Css.getLink=function(url){url=url.toLowerCase();var $enum1=ss.IEnumerator.getEnumerator($('link[type=\'text/css\']').get());while($enum1.moveNext()){var $0=$enum1.get_current();var $1=$0.getAttributeNode('href');if(ss.isNullOrUndefined($1)){continue;}if(url===$1.value.toLowerCase()){return $0;}}return null;}
Open.Core.Events=function(){}
Open.Core.Cookie=function(cookieId){this.id=cookieId;this.$2();}
Open.Core.Cookie.prototype={id:null,$0:0,$1:null,get_id:function(){return this.id;},set_id:function(value){this.id=value;return value;},get_expires:function(){return this.$0;},set_expires:function(value){if(value<0){value=0;}this.$0=value;return value;},save:function(){$.cookie(this.get_id(), this.$1.toJson(), { expires: this.get_expires() });},clear:function(){$.cookie(this.get_id(), null);this.$2();},set:function(key,value){this.$1.set(key,value);},get:function(key){return this.$1.get(key);},hasValue:function(key){return this.$1.hasValue(key);},$2:function(){var $0=this.$3();this.$1=(String.isNullOrEmpty($0))?Open.Core.PropertyBag.create():Open.Core.PropertyBag.fromJson($0);},$3:function(){return Type.safeCast($.cookie(this.get_id()),String);}}
Open.Core.PropertyBag=function(json){if(ss.isNullOrUndefined(json)){this.$0= {};}else{this.$0=Open.Core.Helper.get_json().parse(json);}}
Open.Core.PropertyBag.create=function(){return new Open.Core.PropertyBag(null);}
Open.Core.PropertyBag.fromJson=function(json){return new Open.Core.PropertyBag(json);}
Open.Core.PropertyBag.prototype={$0:null,get_data:function(){return this.$0;},get:function(key){var $0=String.format('this._backingObject.{0}',key);return eval($0);},set:function(key,value){if(Open.Core.Helper.get_reflection().isString(value)){value=String.format('\'{0}\'',value);}var $0=String.format('this._backingObject.{0} = {1}',key,value);eval($0);},hasValue:function(key){return !ss.isNullOrUndefined(this.get(key));},toJson:function(){return Open.Core.Helper.get_json().serialize(this.$0);}}
Open.Core.Size=function(width,height){this.$0=width;this.$1=height;}
Open.Core.Size.prototype={$0:0,$1:0,get_width:function(){return this.$0;},get_height:function(){return this.$1;}}
Open.Core.Helper=function(){}
Open.Core.Helper.get_delegate=function(){return Open.Core.Helper.$0;}
Open.Core.Helper.get_json=function(){return Open.Core.Helper.$1;}
Open.Core.Helper.get_reflection=function(){return Open.Core.Helper.$2;}
Open.Core.Helper.get_scriptLoader=function(){return Open.Core.Helper.$3;}
Open.Core.Helper.get_collection=function(){return Open.Core.Helper.$4;}
Open.Core.Helper.invokeOrDefault=function(action){if(!ss.isNullOrUndefined(action)){action.invoke();}}
Open.Core.Helper.createId=function(){Open.Core.Helper.$5++;return String.format('g.{0}',Open.Core.Helper.$5);}
Type.registerNamespace('Open.Core.Helpers');Open.Core.Helpers.CollectionHelper=function(){}
Open.Core.Helpers.CollectionHelper.prototype={toArrayList:function(collection){var $0=[];if(ss.isNullOrUndefined(collection)){return $0;}var $enum1=ss.IEnumerator.getEnumerator(collection);while($enum1.moveNext()){var $1=$enum1.get_current();$0.add($1);}return $0;}}
Open.Core.Helpers.JsonHelper=function(){}
Open.Core.Helpers.JsonHelper.prototype={serialize:function(value){return Type.safeCast(JSON.stringify( value ),String);},parse:function(json){return JSON.parse( json );}}
Open.Core.Helpers.ReflectionHelper=function(){}
Open.Core.Helpers.ReflectionHelper.prototype={isString:function(value){return Type.getInstanceType(value).get_name()==='String';}}
Open.Core.Helpers.JitScriptLoader=function(){}
Open.Core.Helpers.JitScriptLoader.prototype={$1:false,$2:false,get_isBaseLoaded:function(){return this.$1;},get_isHypertreeLoaded:function(){return this.$2;},loadBase:function(callback){if(this.get_isBaseLoaded()){Open.Core.Helper.invokeOrDefault(callback);return;}var $0=this.$3();$0.add_loadComplete(ss.Delegate.create(this,function(){
Open.Core.Helper.invokeOrDefault(callback);}));$0.start();},$3:function(){var $0=new Open.Core.Helpers.ScriptLoader();$0.add_loadComplete(ss.Delegate.create(this,function(){
this.$1=true;}));$0.addUrl(Open.Core.Helper.get_scriptLoader().$4(String.Empty,'Open.Library.Jit',true));$0.addUrl(Open.Core.Helper.get_scriptLoader().$4('Jit/','excanvas',false));$0.addUrl(Open.Core.Helper.get_scriptLoader().$4('Jit/','Jit.Initialize',false));return $0;},loadHypertree:function(callback){if(this.get_isHypertreeLoaded()){Open.Core.Helper.invokeOrDefault(callback);return;}var $0=new Open.Core.Helpers.ScriptLoader();$0.add_loadComplete(ss.Delegate.create(this,function(){
this.$2=true;Open.Core.Helper.invokeOrDefault(callback);}));$0.addLoader(this.$3());$0.addUrl(Open.Core.Helper.get_scriptLoader().$4('Jit/','HyperTree',true));$0.addUrl(Open.Core.Helper.get_scriptLoader().$4('Jit/','HyperTree.Initialize',false));$0.start();}}
Open.Core.Helpers.ResourceLoader=function(){this.$3=[];this.$4=[];}
Open.Core.Helpers.ResourceLoader.prototype={add_loadComplete:function(value){this.$0=ss.Delegate.combine(this.$0,value);},remove_loadComplete:function(value){this.$0=ss.Delegate.remove(this.$0,value);},$0:null,$1:function(){if(this.$0!=null){this.$0.invoke(this,new ss.EventArgs());}},$2:0,get_isLoaded:function(){if(this.$2<this.$3.length){return false;}var $enum1=ss.IEnumerator.getEnumerator(this.$4);while($enum1.moveNext()){var $0=$enum1.get_current();if(!$0.get_isLoaded()){return false;}}return true;},addUrl:function(url){this.$3.add(url);},addLoader:function(loader){this.$4.add(loader);},start:function(){var $enum1=ss.IEnumerator.getEnumerator(this.$3);while($enum1.moveNext()){var $0=$enum1.get_current();this.loadResource($0,ss.Delegate.create(this,function(){
this.$2++;this.$5();}));}var $enum2=ss.IEnumerator.getEnumerator(this.$4);while($enum2.moveNext()){var $1=$enum2.get_current();if($1.get_isLoaded()){continue;}$1.add_loadComplete(ss.Delegate.create(this,function(){
this.$5();}));$1.start();}},$5:function(){if(this.get_isLoaded()){this.$1();}}}
Open.Core.Helpers.ScriptLoader=function(){Open.Core.Helpers.ScriptLoader.initializeBase(this);}
Open.Core.Helpers.ScriptLoader.prototype={loadResource:function(url,onDownloaded){$.getScript(url,ss.Delegate.create(this,function($p1_0){
onDownloaded.invoke();}));}}
Open.Core.Helpers.ScriptLoadHelper=function(){}
Open.Core.Helpers.ScriptLoadHelper.prototype={$0:'/Open.Core/Scripts/',$1:false,$2:null,$3:false,get_useDebug:function(){return this.$1;},set_useDebug:function(value){this.$1=value;return value;},get_rootScriptFolder:function(){return this.$0;},set_rootScriptFolder:function(value){this.$0=value;return value;},get_jit:function(){return this.$2||(this.$2=new Open.Core.Helpers.JitScriptLoader());},get_isListsLoaded:function(){return this.$3;},loadLists:function(callback){if(this.get_isListsLoaded()){Open.Core.Helper.invokeOrDefault(callback);return;}var $0=new Open.Core.Helpers.ScriptLoader();$0.add_loadComplete(ss.Delegate.create(this,function(){
this.$3=true;Open.Core.Helper.invokeOrDefault(callback);}));$0.addUrl(Open.Core.Helper.get_scriptLoader().$4(String.Empty,'Open.Core.Lists',true));$0.start();},$4:function($p0,$p1,$p2){return String.format(this.get_rootScriptFolder()+$p0+this.$5($p1,$p2));},$5:function($p0,$p1){var $0=($p1&&this.get_useDebug())?'.debug':null;return String.format('{0}{1}.js',$p0,$0);}}
Open.Core.Helpers.DelegateHelper=function(){}
Open.Core.Helpers.DelegateHelper.prototype={toCallbackString:function(callback){return 'ss.Delegate.'+ss.Delegate.createExport(callback,true);},toEventCallbackString:function(callback,eventIdentifier){var $0=String.format('{0}(\'{1}\');',this.toCallbackString(callback),eventIdentifier);return 'function(e,ui){ '+$0+' }';}}
Type.registerNamespace('Open.Core.UI');Open.Core.UI.HorizontalPanelResizer=function(cssSelector,cookieKey){Open.Core.UI.HorizontalPanelResizer.initializeBase(this,[cssSelector,cookieKey]);}
Open.Core.UI.HorizontalPanelResizer.prototype={$11:0,$12:0,get_minWidth:function(){return this.$11;},set_minWidth:function(value){if(value===this.$11){return;}this.$11=value;this.$16();return value;},get_maxWidthMargin:function(){return this.$12;},set_maxWidthMargin:function(value){this.$12=value;return value;},get_$13:function(){return (this.get_hasRootContainer())?this.getRootContainer().width():-1;},get_$14:function(){return (this.get_hasRootContainer())?this.get_$13()-this.get_maxWidthMargin():-1;},getHandles:function(){return 'e';},onInitialize:function(){this.$15();},onStopped:function(){this.get_panel().css('height',String.Empty);},onWindowSizeChanged:function(){if(!this.isInitialized){return;}this.$15();if(this.get_hasRootContainer()){this.shrinkIfOverflowing(this.getCurrentSize(),this.get_minWidth(),this.get_$14(),'width');}},getCurrentSize:function(){return this.get_panel().width();},setCurrentSize:function(size){this.get_panel().css('width',size+'px');},$15:function(){this.$16();this.$17();},$16:function(){this.setResizeOption('minWidth',this.get_minWidth().toString());},$17:function(){var $0=(this.get_hasRootContainer())?this.get_$14().toString():String.Empty;this.setResizeOption('maxWidth',$0);}}
Open.Core.UI.PanelResizerBase=function(cssSelector,cookieKey){this.$C=cssSelector;this.$9=$(cssSelector);this.$A=cookieKey;if(Open.Core.UI.PanelResizerBase.$B==null){Open.Core.UI.PanelResizerBase.$B=new Open.Core.Cookie('PanelResizeStore');Open.Core.UI.PanelResizerBase.$B.set_expires(5000);}$(window).bind('resize',ss.Delegate.create(this,function($p1_0){
this.onWindowSizeChanged();}));this.$F();}
Open.Core.UI.PanelResizerBase.prototype={add_resized:function(value){this.$0=ss.Delegate.combine(this.$0,value);},remove_resized:function(value){this.$0=ss.Delegate.remove(this.$0,value);},$0:null,fireResized:function(){if(this.$0!=null){this.$0.invoke(this,new ss.EventArgs());}},add_resizeStarted:function(value){this.$1=ss.Delegate.combine(this.$1,value);},remove_resizeStarted:function(value){this.$1=ss.Delegate.remove(this.$1,value);},$1:null,$2:function(){if(this.$1!=null){this.$1.invoke(this,new ss.EventArgs());}},add_resizeStopped:function(value){this.$3=ss.Delegate.combine(this.$3,value);},remove_resizeStopped:function(value){this.$3=ss.Delegate.remove(this.$3,value);},$3:null,$4:function(){if(this.$3!=null){this.$3.invoke(this,new ss.EventArgs());}},$8:null,$9:null,$A:null,isInitialized:false,$C:null,get_rootContainerId:function(){return this.$8;},set_rootContainerId:function(value){this.$8=Open.Core.Css.toId(value);return value;},get_hasRootContainer:function(){return !String.isNullOrEmpty(this.get_rootContainerId());},get_isSaving:function(){return !String.isNullOrEmpty(this.$A);},get_panel:function(){return this.$9;},initialize:function(){var $0=ss.Delegate.create(this,function($p1_0){
this.$D($p1_0);});var $1=String.format('\r\n$(\'{0}\').resizable({\r\n    handles: \'{1}\',\r\n    start: {2},\r\n    stop: {3},\r\n    resize: {4}\r\n    });\r\n',this.$C,this.getHandles(),Open.Core.Helper.get_delegate().toEventCallbackString($0,'start'),Open.Core.Helper.get_delegate().toEventCallbackString($0,'eventStop'),Open.Core.Helper.get_delegate().toEventCallbackString($0,'eventResize'));eval($1);this.onInitialize();this.isInitialized=true;},onInitialize:function(){},onStarted:function(){},onResize:function(){},onStopped:function(){},onWindowSizeChanged:function(){},getRootContainer:function(){return (this.get_hasRootContainer())?$(this.get_rootContainerId()):null;},setResizeOption:function(option,value){if(String.isNullOrEmpty(value)){return;}var $0=String.format('$(\'{0}\').resizable(\'option\', \'{1}\', {2});',this.$C,option,value);eval($0);},shrinkIfOverflowing:function(currentValue,minValue,maxValue,cssAttribute){if(currentValue<=maxValue){return;}if(maxValue<minValue){return;}this.get_panel().css(cssAttribute,maxValue+'px');this.fireResized();},$D:function($p0){if($p0==='start'){this.onStarted();this.$2();}else if($p0==='eventResize'){this.onResize();this.fireResized();}else if($p0==='eventStop'){this.onStopped();this.$E();this.$4();}},$E:function(){if(!this.get_isSaving()){return;}Open.Core.UI.PanelResizerBase.$B.set(this.$A,this.getCurrentSize());Open.Core.UI.PanelResizerBase.$B.save();},$F:function(){if(!this.get_isSaving()){return;}var $0=Open.Core.UI.PanelResizerBase.$B.get(this.$A);if(ss.isNullOrUndefined($0)){return;}this.setCurrentSize($0);this.fireResized();}}
Open.Core.UI.VerticalPanelResizer=function(cssSelector,cookieKey){Open.Core.UI.VerticalPanelResizer.initializeBase(this,[cssSelector,cookieKey]);}
Open.Core.UI.VerticalPanelResizer.prototype={$11:0,$12:0,get_minHeight:function(){return this.$11;},set_minHeight:function(value){if(value===this.$11){return;}this.$11=value;this.$16();return value;},get_maxHeightMargin:function(){return this.$12;},set_maxHeightMargin:function(value){this.$12=value;return value;},get_$13:function(){return (this.get_hasRootContainer())?this.getRootContainer().height():-1;},get_$14:function(){return (this.get_hasRootContainer())?this.get_$13()-this.get_maxHeightMargin():-1;},getHandles:function(){return 'n';},onInitialize:function(){this.$15();},onStopped:function(){this.get_panel().css('width',String.Empty);this.get_panel().css('top',String.Empty);},onWindowSizeChanged:function(){if(!this.isInitialized){return;}this.$15();if(this.get_hasRootContainer()){this.shrinkIfOverflowing(this.getCurrentSize(),this.get_minHeight(),this.get_$14(),'height');}},getCurrentSize:function(){return this.get_panel().height();},setCurrentSize:function(size){this.get_panel().css('height',size+'px');},$15:function(){this.$16();this.$17();},$16:function(){this.setResizeOption('minHeight',this.get_minHeight().toString());},$17:function(){var $0=(this.get_hasRootContainer())?this.get_$14().toString():String.Empty;this.setResizeOption('maxHeight',$0);}}
Open.Core.ViewBase.registerClass('Open.Core.ViewBase',null,Open.Core.IView);Open.Core.Html.registerClass('Open.Core.Html');Open.Core.Css.registerClass('Open.Core.Css');Open.Core.Events.registerClass('Open.Core.Events');Open.Core.Cookie.registerClass('Open.Core.Cookie');Open.Core.PropertyBag.registerClass('Open.Core.PropertyBag');Open.Core.Size.registerClass('Open.Core.Size');Open.Core.Helper.registerClass('Open.Core.Helper');Open.Core.Helpers.CollectionHelper.registerClass('Open.Core.Helpers.CollectionHelper');Open.Core.Helpers.JsonHelper.registerClass('Open.Core.Helpers.JsonHelper');Open.Core.Helpers.ReflectionHelper.registerClass('Open.Core.Helpers.ReflectionHelper');Open.Core.Helpers.JitScriptLoader.registerClass('Open.Core.Helpers.JitScriptLoader');Open.Core.Helpers.ResourceLoader.registerClass('Open.Core.Helpers.ResourceLoader');Open.Core.Helpers.ScriptLoader.registerClass('Open.Core.Helpers.ScriptLoader',Open.Core.Helpers.ResourceLoader);Open.Core.Helpers.ScriptLoadHelper.registerClass('Open.Core.Helpers.ScriptLoadHelper');Open.Core.Helpers.DelegateHelper.registerClass('Open.Core.Helpers.DelegateHelper');Open.Core.UI.PanelResizerBase.registerClass('Open.Core.UI.PanelResizerBase');Open.Core.UI.HorizontalPanelResizer.registerClass('Open.Core.UI.HorizontalPanelResizer',Open.Core.UI.PanelResizerBase);Open.Core.UI.VerticalPanelResizer.registerClass('Open.Core.UI.VerticalPanelResizer',Open.Core.UI.PanelResizerBase);Open.Core.Html.head='head';Open.Core.Html.href='href';Open.Core.Css.left='left';Open.Core.Css.right='right';Open.Core.Css.top='top';Open.Core.Css.bottom='bottom';Open.Core.Css.width='width';Open.Core.Css.height='height';Open.Core.Css.px='px';Open.Core.Events.resize='resize';Open.Core.Helper.$0=new Open.Core.Helpers.DelegateHelper();Open.Core.Helper.$1=new Open.Core.Helpers.JsonHelper();Open.Core.Helper.$2=new Open.Core.Helpers.ReflectionHelper();Open.Core.Helper.$3=new Open.Core.Helpers.ScriptLoadHelper();Open.Core.Helper.$4=new Open.Core.Helpers.CollectionHelper();Open.Core.Helper.$5=0;Open.Core.UI.PanelResizerBase.$B=null;
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('Open.Core.Script',[],executeScript);})();