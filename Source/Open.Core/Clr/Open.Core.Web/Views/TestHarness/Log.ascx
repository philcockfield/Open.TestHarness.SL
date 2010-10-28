<%@ Import Namespace="Open.Core.Web" %>
<%@ Import Namespace="Resources" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<!-- Log -->
<div id="testHarnessLog" class="panelBorder panelBorderTop">
    <!-- Log Title/Toolbar-->
    <div class="th-log-tb tb-height th-toolbar panelBorder panelBorderBottom noSelect">
        <p class="toolbarReflection"></p>
        <div class="table tb-height">
            <div class="vAlignedCell">
               <p class="title titleFont"><%= TestHarnessStrings.Output_Title %></p>
            </div>
        </div>
        <div class="buttons">
            <button class="clear button">Clear</button>
            <div class="buttonDivider"></div>
        </div>
    </div>
    <!-- Log Content -->
    <% Html.InsertLog(); %>
    <p class="dropShadow"></p>
</div>
