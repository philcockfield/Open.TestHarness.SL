<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div id="testHarnessLog" class="panelBorder panelBorderTop">
    <div class="titlebar panelBorder panelBorderBottom toolbar">
        <p class="toolbarReflection"></p>
        <div class="table">
           <p class="title titleFont"><%= Model.OutputTitle %></p>
        </div>
    </div>
    <div class="outputLogContent"></div>
    <p class="dropShadow"></p>
</div>
