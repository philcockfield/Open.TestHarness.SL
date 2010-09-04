<%@ Import Namespace="Open.Core.Web" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div id="testHarnessLog" class="panelBorder panelBorderTop">
    <div class="th-log-tb tb-height th-toolbar panelBorder panelBorderBottom">
        <p class="toolbarReflection"></p>
        <div class="table tb-height">
            <div class="vAlignedCell">
               <p class="title titleFont"><%= Model.OutputTitle %></p>
            </div>
        </div>
    </div>
    <% Html.InsertLog(); %>
    <p class="dropShadow"></p>
</div>
