<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= Model.AppTitle %></title>
    <% Html.RenderAction(MVC.TestHarness.ActionNames.GoogleAnalytics); %>
    <% Html.RenderAction(MVC.TestHarness.ActionNames.Head); %>
</head>
<body>
    <div id="testHarness">
        <% Html.RenderAction(MVC.TestHarness.ActionNames.Sidebar); %>
        <% Html.RenderAction(MVC.TestHarness.ActionNames.Main); %>
    </div>
</body>
</html>
