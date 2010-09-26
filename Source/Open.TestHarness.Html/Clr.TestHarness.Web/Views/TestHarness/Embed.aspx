<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% Html.RenderAction(MVC.TestHarness.ActionNames.GoogleAnalytics); %>
<% Html.RenderAction(MVC.TestHarness.ActionNames.Head); %>
<% Html.RenderAction(MVC.TestHarness.ActionNames.Body); %>

