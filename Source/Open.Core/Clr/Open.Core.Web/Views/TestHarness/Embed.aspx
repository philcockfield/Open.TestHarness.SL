<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="Open.Core.Web.Controllers" %>
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<% Html.RenderAction(TestHarnessController.ActionGoogleAnalytics); %>
<% Html.RenderAction(TestHarnessController.ActionHead); %>
<% Html.RenderAction(TestHarnessController.ActionBody); %>

