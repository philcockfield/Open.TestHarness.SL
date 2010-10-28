<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="Open.Core.Web.Controllers" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="testHarness">
    <% Html.RenderAction(TestHarnessController.ActionSidebar); %>
    <% Html.RenderAction(TestHarnessController.ActionMain); %>
</div>

