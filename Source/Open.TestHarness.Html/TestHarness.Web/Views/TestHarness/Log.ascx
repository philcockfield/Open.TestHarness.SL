<%@ Import Namespace="Open.Core.Web" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div id="testHarnessLog" class="panelBorder panelBorderTop">
    <div class="th-log-titlebar th-toolbar panelBorder panelBorderBottom">
        <p class="toolbarReflection"></p>
        <div class="table">
           <p class="title titleFont"><%= Model.OutputTitle %></p>
        </div>
    </div>
    <% Html.InsertLog(); %>
    <p class="dropShadow"></p>
</div>
