<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="testHarness">
    <% Html.RenderAction(MVC.TestHarness.ActionNames.Sidebar); %>
    <% Html.RenderAction(MVC.TestHarness.ActionNames.Main); %>
</div>

