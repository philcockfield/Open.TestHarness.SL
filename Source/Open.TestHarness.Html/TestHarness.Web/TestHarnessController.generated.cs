// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Open.TestHarness.Web.Controllers {
    public partial class TestHarnessController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public TestHarnessController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected TestHarnessController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }


        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public TestHarnessController Actions { get { return MVC.TestHarness; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "TestHarness";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string Log = "Log";
            public readonly string TEMP = "TEMP";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string Index = "~/Views/TestHarness/Index.aspx";
            public readonly string Log = "~/Views/TestHarness/Log.ascx";
            public readonly string TEMP = "~/Views/TestHarness/TEMP.aspx";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_TestHarnessController: Open.TestHarness.Web.Controllers.TestHarnessController {
        public T4MVC_TestHarnessController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Index() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Log() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Log);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult TEMP() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.TEMP);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
