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
    public partial class SamplesController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public SamplesController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected SamplesController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }


        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public SamplesController Actions { get { return MVC.Samples; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Samples";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Templates = "Templates";
            public readonly string ButtonTemplate = "ButtonTemplate";
            public readonly string JQuery = "JQuery";
            public readonly string Embed = "Embed";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string ButtonTemplate = "~/Views/Samples/ButtonTemplate.ascx";
            public readonly string Embed = "~/Views/Samples/Embed.aspx";
            public readonly string JQuery = "~/Views/Samples/JQuery.aspx";
            public readonly string Templates = "~/Views/Samples/Templates.aspx";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_SamplesController: Open.TestHarness.Web.Controllers.SamplesController {
        public T4MVC_SamplesController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Templates() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Templates);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ButtonTemplate() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ButtonTemplate);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult JQuery() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.JQuery);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Embed() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Embed);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591