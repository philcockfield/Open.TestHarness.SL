// Open.TestHarness.js
(function(){function executeScript(){
Type.registerNamespace('Open.TestHarness');Open.TestHarness.CssSelectors=function(){}
Open.TestHarness.Elements=function(){}
Open.TestHarness.Application=function(){}
Open.TestHarness.Application.get_shell=function(){return Open.TestHarness.Application.$0;}
Open.TestHarness.Application.main=function(args){var $0=new Open.Core.Controls.LogView($(Open.TestHarness.CssSelectors.log).first());Open.Core.Log.registerView($0);Open.TestHarness.Application.$0=new Open.TestHarness.Views.ShellView($(Open.TestHarness.CssSelectors.root));Open.TestHarness.Application.$1=new Open.TestHarness.Controllers.PanelResizeController();Open.TestHarness.Application.$2=new Open.TestHarness.Controllers.SidebarController();var $1='/Content/Scripts/Test.debug.js';var $2='Test.Application.main';var $3=Open.TestHarness.Models.TestPackageInfo.singletonFromUrl($1,$2);Open.TestHarness.Application.$2.addPackage($3);}
Type.registerNamespace('Open.TestHarness.Models');Open.TestHarness.Models.TestMethodListItem=function(testMethod){Open.TestHarness.Models.TestMethodListItem.initializeBase(this);this.$3_0=testMethod;this.set_text(testMethod.get_displayName());}
Open.TestHarness.Models.TestMethodListItem.prototype={$3_0:null,get_testMethod:function(){return this.$3_0;}}
Open.TestHarness.Models.TestClassListItem=function(testClass){Open.TestHarness.Models.TestClassListItem.initializeBase(this);this.$3_0=testClass;this.set_text(testClass.get_classType().get_name());}
Open.TestHarness.Models.TestClassListItem.prototype={$3_0:null,get_testClass:function(){return this.$3_0;}}
Open.TestHarness.Models.TestMethodInfo=function(testClass,name){Open.TestHarness.Models.TestMethodInfo.initializeBase(this);this.$1_0=testClass;this.$1_1=name;this.$1_2=Open.TestHarness.Models.TestMethodInfo.formatName(name);}
Open.TestHarness.Models.TestMethodInfo.isTestMethod=function(item){var $0=item.key;if(typeof(item.value)!=='function'){return false;}if($0.startsWith('_')){return false;}if($0.startsWith('get_')){return false;}if($0.startsWith('set_')){return false;}if($0==='constructor'){return false;}return true;}
Open.TestHarness.Models.TestMethodInfo.formatName=function(name){name=name.replaceAll('__',': ');name=name.replaceAll('_',' ');name=Open.Core.Helper.get_string().toSentenceCase(name);return name;}
Open.TestHarness.Models.TestMethodInfo.prototype={$1_0:null,$1_1:null,$1_2:null,get_testClass:function(){return this.$1_0;},get_name:function(){return this.$1_1;},get_displayName:function(){return this.$1_2;}}
Open.TestHarness.Models.TestPackageInfo=function(scriptUrl,initMethod){this.$1_1=[];Open.TestHarness.Models.TestPackageInfo.initializeBase(this);if(String.isNullOrEmpty(scriptUrl)){throw new Error('A URL to the test-package script must be specified.');}if(String.isNullOrEmpty(initMethod)){throw new Error('An entry point method must be specified.');}this.$1_3=Open.TestHarness.Models.TestPackageInfo.$1_4(scriptUrl);this.$1_2=new Open.TestHarness.Models.TestPackageLoader(this,scriptUrl.toLowerCase(),initMethod);}
Open.TestHarness.Models.TestPackageInfo.get_singletons=function(){return Open.TestHarness.Models.TestPackageInfo.$1_0;}
Open.TestHarness.Models.TestPackageInfo.singletonFromUrl=function(scriptUrl,initMethod){var $0=Type.safeCast(Open.Core.Helper.get_collection().first(Open.TestHarness.Models.TestPackageInfo.$1_0,function($p1_0){
return ($p1_0).get_id()===scriptUrl.toLowerCase();}),Open.TestHarness.Models.TestPackageInfo);if($0==null){$0=new Open.TestHarness.Models.TestPackageInfo(scriptUrl,initMethod);Open.TestHarness.Models.TestPackageInfo.$1_0.add($0);}return $0;}
Open.TestHarness.Models.TestPackageInfo.$1_4=function($p0){var $0=Open.Core.Helper.get_string();$p0=$0.removeEnd($p0,'.js');$p0=$0.removeEnd($p0,'.debug');$p0=$0.stripPath($p0);if(String.isNullOrEmpty($p0.trim())){$p0='<Untitled>'.htmlEncode();}return $p0;}
Open.TestHarness.Models.TestPackageInfo.prototype={$1_2:null,$1_3:null,get_id:function(){return this.get_loader().get_scriptUrl();},get_name:function(){return this.$1_3;},get_loader:function(){return this.$1_2;},get_isLoaded:function(){return this.get_loader().get_isLoaded();},get_count:function(){return this.$1_1.length;},getEnumerator:function(){return this.$1_1.getEnumerator();},addClass:function(testClass){if(testClass==null){return;}if(this.contains(testClass)){return;}this.$1_1.add(Open.TestHarness.Models.TestClassInfo.getSingleton(testClass));},contains:function(testClass){return this.getTestClassDef(testClass)!=null;},getTestClassDef:function(testClass){if(testClass==null){return null;}var $0=testClass.get_fullName();var $enum1=ss.IEnumerator.getEnumerator(this.$1_1);while($enum1.moveNext()){var $1=$enum1.get_current();if($1.get_classType().get_fullName()===$0){return $1;}}return null;}}
Open.TestHarness.Models.TestClassInfo=function(classType){this.$1_3=[];Open.TestHarness.Models.TestClassInfo.initializeBase(this);this.$1_1=classType;this.$1_4();}
Open.TestHarness.Models.TestClassInfo.getSingleton=function(testClass){if(Open.TestHarness.Models.TestClassInfo.$1_0==null){Open.TestHarness.Models.TestClassInfo.$1_0={};}var $0=testClass.get_fullName();if(Object.keyExists(Open.TestHarness.Models.TestClassInfo.$1_0,$0)){return Type.safeCast(Open.TestHarness.Models.TestClassInfo.$1_0[$0],Open.TestHarness.Models.TestClassInfo);}var $1=new Open.TestHarness.Models.TestClassInfo(testClass);Open.TestHarness.Models.TestClassInfo.$1_0[$0]=$1;return $1;}
Open.TestHarness.Models.TestClassInfo.prototype={$1_1:null,$1_2:null,get_classType:function(){return this.$1_1;},get_instance:function(){return this.$1_2||(this.$1_2=Type.safeCast(new this.$1_1(),Object));},get_count:function(){return this.$1_3.length;},reset:function(){this.$1_2=null;},getEnumerator:function(){return this.$1_3.getEnumerator();},toString:function(){return String.format('[{0}:{1}]',Type.getInstanceType(this).get_name(),this.get_classType().get_name());},$1_4:function(){if(this.get_instance()==null){return;}var $dict1=this.get_instance();for(var $key2 in $dict1){var $0={key:$key2,value:$dict1[$key2]};if(!Open.TestHarness.Models.TestMethodInfo.isTestMethod($0)){continue;}this.$1_3.add(new Open.TestHarness.Models.TestMethodInfo(this,$0.key));}}}
Open.TestHarness.Models.TestPackageListItem=function(testPackage){Open.TestHarness.Models.TestPackageListItem.initializeBase(this);this.$3_0=testPackage;this.set_text(testPackage.get_name());}
Open.TestHarness.Models.TestPackageListItem.prototype={$3_0:null,get_testPackage:function(){return this.$3_0;}}
Open.TestHarness.Models.TestPackageLoader=function(parent,scriptUrl,initMethod){this.$0=parent;this.$1=scriptUrl;this.$2=initMethod;Open.TestHarness.TestHarnessEvents.add_testClassRegistered(ss.Delegate.create(this,this.$6));}
Open.TestHarness.Models.TestPackageLoader.prototype={$0:null,$1:null,$2:null,$3:false,$4:null,$5:false,dispose:function(){Open.TestHarness.TestHarnessEvents.remove_testClassRegistered(ss.Delegate.create(this,this.$6));},$6:function($p0,$p1){if(!this.$5){return;}this.$0.addClass($p1.testClass);},get_scriptUrl:function(){return this.$1;},get_initMethod:function(){return this.$2;},get_isLoaded:function(){return this.$3;},get_error:function(){return this.$4;},get_succeeded:function(){return this.get_error()==null;},load:function(onComplete){if(this.get_isLoaded()){Open.Core.Helper.invokeOrDefault(onComplete);return;}$.getScript(this.$1,ss.Delegate.create(this,function($p1_0){
try{this.$5=true;eval(this.$2+'();');}catch($1_0){Open.Core.Log.error(String.format('Failed to initialize the script-file at \'{0}\' with the entry method \'{1}()\'.<br/>Message: {2}',this.$1,this.$2,$1_0.message));this.$4=$1_0;}finally{this.$5=false;}this.$3=this.get_succeeded();Open.Core.Helper.invokeOrDefault(onComplete);}));}}
Type.registerNamespace('Open.TestHarness.Controllers');Open.TestHarness.Controllers.PanelResizeController=function(){Open.TestHarness.Controllers.PanelResizeController.initializeBase(this);this.$2_3=new Open.Core.UI.HorizontalPanelResizer(Open.TestHarness.CssSelectors.sidebar,'TH_SB');this.$2_3.add_resized(ss.Delegate.create(this,function(){
Open.TestHarness.Controllers.PanelResizeController.$2_6();}));this.$2_3.set_minWidth(200);this.$2_3.set_maxWidthMargin(80);Open.TestHarness.Controllers.PanelResizeController.$2_5(this.$2_3);Open.TestHarness.Controllers.PanelResizeController.$2_6();this.$2_4=new Open.Core.UI.VerticalPanelResizer(Open.Core.Css.toId('testHarnessLog'),'TH_OL');this.$2_4.add_resized(ss.Delegate.create(this,function(){
}));this.$2_4.set_minHeight($(Open.TestHarness.CssSelectors.logTitlebar).height());this.$2_4.set_maxHeightMargin(80);Open.TestHarness.Controllers.PanelResizeController.$2_5(this.$2_4);}
Open.TestHarness.Controllers.PanelResizeController.$2_5=function($p0){$p0.set_rootContainerId('testHarness');$p0.initialize();}
Open.TestHarness.Controllers.PanelResizeController.$2_6=function(){$(Open.TestHarness.CssSelectors.main).css(Open.Core.Css.left,$(Open.TestHarness.CssSelectors.sidebar).width()+1+Open.Core.Css.px);}
Open.TestHarness.Controllers.PanelResizeController.prototype={$2_3:null,$2_4:null}
Open.TestHarness.Controllers.SidebarController=function(){this.$2_0=[];Open.TestHarness.Controllers.SidebarController.initializeBase(this);this.$2_1=Open.TestHarness.Application.get_shell().get_sidebar();this.$2_2();}
Open.TestHarness.Controllers.SidebarController.prototype={$2_1:null,onDisposed:function(){this.$2_1.dispose();var $enum1=ss.IEnumerator.getEnumerator(this.$2_0);while($enum1.moveNext()){var $0=$enum1.get_current();$0.dispose();}Open.TestHarness.Controllers.SidebarController.callBaseMethod(this, 'onDisposed');},$2_2:function(){var $0=new Open.TestHarness.Controllers.MyNode('Root');this.$2_1.get_rootList().set_rootNode($0);$0.addChild(new Open.TestHarness.Controllers.MyNode('Recent'));var $1=Type.safeCast($0.childAt(0),Open.TestHarness.Controllers.MyNode);var $2=($0.childAt(1));var $3=($0.childAt(2));$1.addChild(new Open.TestHarness.Controllers.MyNode('Recent Child 1'));$1.addChild(new Open.TestHarness.Controllers.MyNode('Recent Child 2'));$1.addChild(new Open.TestHarness.Controllers.MyNode('Recent Child 3'));var $4=Type.safeCast($1.childAt(0),Open.TestHarness.Controllers.MyNode);$4.addChild(new Open.TestHarness.Controllers.MyNode('Recent Grandchild 1'));$4.addChild(new Open.TestHarness.Controllers.MyNode('Recent Grandchild 2'));$4.addChild(new Open.TestHarness.Controllers.MyNode('Recent Grandchild 3'));},addPackage:function(testPackage){if(testPackage==null){return;}var $0=new Open.TestHarness.Models.TestPackageListItem(testPackage);this.$2_1.get_rootList().get_rootNode().addChild($0);var $1=new Open.TestHarness.Controllers.TestPackageController($0);this.$2_0.add($1);$1.add_loaded(ss.Delegate.create(this,function(){
this.$2_1.get_rootList().set_selectedParent($1.get_rootNode());}));},removePackage:function(testPackage){if(testPackage==null){return;}var $0=this.$2_3(testPackage);if($0==null){return;}this.$2_1.get_rootList().get_rootNode().removeChild($0.get_rootNode());Open.Core.Log.info(String.format('Test package unloaded: {0}',Open.Core.Html.toHyperlink(testPackage.get_id(),null,0)));Open.Core.Log.lineBreak();},$2_3:function($p0){return Type.safeCast(Open.Core.Helper.get_collection().first(this.$2_0,ss.Delegate.create(this,function($p1_0){
return ($p1_0).get_testPackage()===$p0;})),Open.TestHarness.Controllers.TestPackageController);}}
Open.TestHarness.Controllers.MyNode=function(text){Open.TestHarness.Controllers.MyNode.initializeBase(this);this.set_text(text);}
Open.TestHarness.Controllers.MyNode.prototype={toString:function(){return Open.TestHarness.Controllers.MyNode.callBaseMethod(this, 'toString');}}
Open.TestHarness.Controllers.TestPackageController=function(rootNode){Open.TestHarness.Controllers.TestPackageController.initializeBase(this);this.$2_3=rootNode;rootNode.add_selectionChanged(ss.Delegate.create(this,this.$2_4));rootNode.add_childSelectionChanged(ss.Delegate.create(this,this.$2_5));}
Open.TestHarness.Controllers.TestPackageController.prototype={add_loaded:function(value){this.$2_0=ss.Delegate.combine(this.$2_0,value);},remove_loaded:function(value){this.$2_0=ss.Delegate.remove(this.$2_0,value);},$2_0:null,$2_1:function(){if(this.$2_0!=null){this.$2_0.invoke(this,new ss.EventArgs());}},$2_3:null,onDisposed:function(){this.$2_3.remove_selectionChanged(ss.Delegate.create(this,this.$2_4));this.$2_3.remove_childSelectionChanged(ss.Delegate.create(this,this.$2_5));Open.TestHarness.Controllers.TestPackageController.callBaseMethod(this, 'onDisposed');},$2_4:function($p0,$p1){if(this.get_rootNode().get_isSelected()){this.$2_6();}},$2_5:function($p0,$p1){var $0=Type.safeCast(Open.Core.Helper.get_tree().firstSelectedChild(this.get_rootNode()),Open.TestHarness.Models.TestClassListItem);this.set_selectedTestClass(($0==null)?null:$0.get_testClass());},get_testPackage:function(){return this.$2_3.get_testPackage();},get_rootNode:function(){return this.$2_3;},get_selectedTestClass:function(){return this.get('SelectedTestClass',null);},set_selectedTestClass:function(value){if(this.set('SelectedTestClass',value,null)){Open.TestHarness.Application.get_shell().get_sidebar().get_testList().set_testClass(value);}return value;},$2_6:function(){if(this.get_testPackage().get_isLoaded()){return;}var $0=this.get_testPackage().get_loader();var $1=Open.Core.Html.toHyperlink($0.get_scriptUrl(),null,0);var $2=new Open.Core.DelayedAction(10,ss.Delegate.create(this,function(){
Open.Core.Log.error(String.format('Failed to download the test-package at \'{0}\'.  Please ensure the file exists.',$1));Open.Core.Log.lineBreak();}));Open.Core.Log.info(String.format('Downloading test-package: {0} ...',$1));$0.load(ss.Delegate.create(this,function(){
$2.stop();if($0.get_succeeded()){Open.Core.Log.success('Test-package loaded successfully.');this.$2_7();this.$2_1();}Open.Core.Log.lineBreak();}));$2.start();},$2_7:function(){var $enum1=ss.IEnumerator.getEnumerator(this.get_testPackage());while($enum1.moveNext()){var $0=$enum1.get_current();var $1=new Open.TestHarness.Models.TestClassListItem($0);this.get_rootNode().addChild($1);}}}
Type.registerNamespace('Open.TestHarness.Views');Open.TestHarness.Views.ShellView=function(container){Open.TestHarness.Views.ShellView.initializeBase(this);this.initialize(container);this.$2_0=new Open.TestHarness.Views.SidebarView($(Open.TestHarness.CssSelectors.sidebar));}
Open.TestHarness.Views.ShellView.prototype={$2_0:null,get_sidebar:function(){return this.$2_0;}}
Open.TestHarness.Views.SidebarView=function(container){Open.TestHarness.Views.SidebarView.initializeBase(this);this.initialize(container);this.$2_0=new Open.Core.Lists.ListTreeView($(Open.TestHarness.CssSelectors.sidebarRootList));this.$2_0.set_slideDuration(0.2);this.$2_2=new Open.TestHarness.Views.TestListView($(Open.TestHarness.CssSelectors.testList));this.$2_1=new Open.Core.Lists.ListTreeBackController(this.$2_0,$(Open.TestHarness.CssSelectors.sidebarToolbar),$(Open.TestHarness.CssSelectors.backMask));this.$2_0.add_selectedParentChanged(ss.Delegate.create(this,function(){
this.$2_6();}));this.updateVisualState();this.get_rootList().get_container().click(ss.Delegate.create(this,function($p1_0){
}));}
Open.TestHarness.Views.SidebarView.$2_4=function($p0,$p1,$p2){$p0.animate($p1,Open.Core.Helper.get_number().toMsecs(0.2),'swing',function(){
Open.Core.Helper.invokeOrDefault($p2);});}
Open.TestHarness.Views.SidebarView.prototype={$2_0:null,$2_1:null,$2_2:null,onDisposed:function(){this.$2_1.dispose();this.$2_0.dispose();Open.TestHarness.Views.SidebarView.callBaseMethod(this, 'onDisposed');},get_rootList:function(){return this.$2_0;},get_testList:function(){return this.$2_2;},get_isTestListVisible:function(){return this.get('IsTestListVisible',false);},set_isTestListVisible:function(value){if(this.set('IsTestListVisible',value,false)){if(value){this.showTestList(null);}else{this.hideTestList(null);}}return value;},updateVisualState:function(){this.$2_5();},showTestList:function(onComplete){this.set_isTestListVisible(true);var $0=this.$2_7();this.$2_3($0,onComplete);},hideTestList:function(onComplete){this.set_isTestListVisible(false);this.$2_3(0,onComplete);},$2_3:function($p0,$p1){var $0={};$0[Open.Core.Css.height]=$p0;var $1={};$1[Open.Core.Css.bottom]=$p0;Open.TestHarness.Views.SidebarView.$2_4(this.get_testList().get_container(),$0,null);Open.TestHarness.Views.SidebarView.$2_4(this.get_rootList().get_container(),$1,$p1);},$2_5:function(){this.get_rootList().get_container().css(Open.Core.Css.bottom,this.get_testList().get_container().height()+Open.Core.Css.px);},$2_6:function(){var $0=this.$2_0.get_selectedParent();this.set_isTestListVisible($0!=null&&(Type.canCast($0,Open.TestHarness.Models.TestPackageListItem)));},$2_7:function(){return 250;}}
Open.TestHarness.Views.TestListView=function(container){Open.TestHarness.Views.TestListView.initializeBase(this);this.initialize(container);this.$2_0=new Open.Core.Lists.ListTreeView($(Open.TestHarness.CssSelectors.testListContent));this.$2_0.set_slideDuration(0.2);this.$2_1=new Open.Core.Lists.ListItem();this.$2_0.set_rootNode(this.$2_1);}
Open.TestHarness.Views.TestListView.prototype={$2_0:null,$2_1:null,get_testClass:function(){return this.get('TestClass',null);},set_testClass:function(value){if(this.set('TestClass',value,null)){this.$2_2(value);}return value;},$2_2:function($p0){this.$2_1.clearChildren();if($p0==null){return;}var $enum1=ss.IEnumerator.getEnumerator($p0);while($enum1.moveNext()){var $0=$enum1.get_current();this.$2_1.addChild(new Open.TestHarness.Models.TestMethodListItem($0));}}}
Open.TestHarness.CssSelectors.registerClass('Open.TestHarness.CssSelectors');Open.TestHarness.Elements.registerClass('Open.TestHarness.Elements');Open.TestHarness.Application.registerClass('Open.TestHarness.Application');Open.TestHarness.Models.TestMethodListItem.registerClass('Open.TestHarness.Models.TestMethodListItem',Open.Core.Lists.ListItem);Open.TestHarness.Models.TestClassListItem.registerClass('Open.TestHarness.Models.TestClassListItem',Open.Core.Lists.ListItem);Open.TestHarness.Models.TestMethodInfo.registerClass('Open.TestHarness.Models.TestMethodInfo',Open.Core.ModelBase);Open.TestHarness.Models.TestPackageInfo.registerClass('Open.TestHarness.Models.TestPackageInfo',Open.Core.ModelBase,ss.IEnumerable);Open.TestHarness.Models.TestClassInfo.registerClass('Open.TestHarness.Models.TestClassInfo',Open.Core.ModelBase,ss.IEnumerable);Open.TestHarness.Models.TestPackageListItem.registerClass('Open.TestHarness.Models.TestPackageListItem',Open.Core.Lists.ListItem);Open.TestHarness.Models.TestPackageLoader.registerClass('Open.TestHarness.Models.TestPackageLoader',null,ss.IDisposable);Open.TestHarness.Controllers.PanelResizeController.registerClass('Open.TestHarness.Controllers.PanelResizeController',Open.Core.ControllerBase);Open.TestHarness.Controllers.SidebarController.registerClass('Open.TestHarness.Controllers.SidebarController',Open.Core.ControllerBase);Open.TestHarness.Controllers.MyNode.registerClass('Open.TestHarness.Controllers.MyNode',Open.Core.Lists.ListItem);Open.TestHarness.Controllers.TestPackageController.registerClass('Open.TestHarness.Controllers.TestPackageController',Open.Core.ControllerBase);Open.TestHarness.Views.ShellView.registerClass('Open.TestHarness.Views.ShellView',Open.Core.ViewBase);Open.TestHarness.Views.SidebarView.registerClass('Open.TestHarness.Views.SidebarView',Open.Core.ViewBase);Open.TestHarness.Views.TestListView.registerClass('Open.TestHarness.Views.TestListView',Open.Core.ViewBase);Open.TestHarness.CssSelectors.root='#testHarness';Open.TestHarness.CssSelectors.sidebar='#testHarnessSidebar';Open.TestHarness.CssSelectors.sidebarRootList='#testHarnessSidebar .th-sidebarRootList';Open.TestHarness.CssSelectors.sidebarToolbar='#testHarnessSidebar div.th-toolbar';Open.TestHarness.CssSelectors.backMask='#testHarnessSidebar img.th-backMask';Open.TestHarness.CssSelectors.testList='#testHarnessSidebar .th-testList';Open.TestHarness.CssSelectors.testListContent='#testHarnessSidebar .th-testList-content';Open.TestHarness.CssSelectors.main='#testHarness .th-main';Open.TestHarness.CssSelectors.logTitlebar='#testHarnessLog .th-log-tb';Open.TestHarness.CssSelectors.log='#testHarnessLog .coreLog';Open.TestHarness.Elements.root='testHarness';Open.TestHarness.Elements.outputLog='testHarnessLog';Open.TestHarness.Application.$0=null;Open.TestHarness.Application.$1=null;Open.TestHarness.Application.$2=null;Open.TestHarness.Models.TestMethodInfo.keyConstructor='constructor';Open.TestHarness.Models.TestMethodInfo.keyGetter='get_';Open.TestHarness.Models.TestMethodInfo.keySetter='set_';Open.TestHarness.Models.TestMethodInfo.keyField='_';Open.TestHarness.Models.TestMethodInfo.keyFunction='function';Open.TestHarness.Models.TestPackageInfo.$1_0=[];Open.TestHarness.Models.TestClassInfo.$1_0=null;Open.TestHarness.Controllers.TestPackageController.propSelectedTestClass='SelectedTestClass';Open.TestHarness.Views.SidebarView.slideDuration=0.2;Open.TestHarness.Views.SidebarView.propIsTestListVisible='IsTestListVisible';Open.TestHarness.Views.TestListView.propTestClass='TestClass';
// ---- Do not remove this footer ----
// This script was generated using Script# v0.6.0.0 (http://projects.nikhilk.net/ScriptSharp)
// -----------------------------------
}
ss.loader.registerScript('TestHarness.Script3',['Open.Core.Views','Open.Core.Script','Open.Core.Lists'],executeScript);})();