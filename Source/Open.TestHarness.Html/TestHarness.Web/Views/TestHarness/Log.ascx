<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div id="testHarnessLog" class="panelBorder panelBorderTop">
    <div class="th-log-titlebar th-toolbar panelBorder panelBorderBottom">
        <p class="toolbarReflection"></p>
        <div class="table">
           <p class="title titleFont"><%= Model.OutputTitle %></p>
        </div>
    </div>
    <div class="th-log-content absoluteFill">
        <div class="th-log-list absoluteFill"></div>
        <div class="th-log-margin absoluteFill"></div>
    </div>
    <p class="dropShadow"></p>
</div>
