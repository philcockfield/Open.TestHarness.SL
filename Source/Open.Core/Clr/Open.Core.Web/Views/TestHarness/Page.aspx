<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="Resources" %>
<%@ Import Namespace="Open.Core.Web.Controllers" %>
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= TestHarnessStrings.App_Title %></title>
    <% Html.RenderAction(TestHarnessController.ActionGoogleAnalytics); %>
    <% Html.RenderAction(TestHarnessController.ActionHead); %>
</head>
<body>
    <% Html.RenderAction(TestHarnessController.ActionBody); %>
</body>
</html>
