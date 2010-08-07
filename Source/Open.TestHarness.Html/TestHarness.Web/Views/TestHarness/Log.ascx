<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div id="outputLog" class="panelBorder panelBorderTop">
    <div id="outputLogTitlebar" class="panelBorder panelBorderBottom toolbar">
        <p class="toolbarReflection"></p>
        <p id="outputLogTitle"><%= Model.OutputTitle %></p>
    </div>

    <div class="outputLogContent">
    </div>


    <p class="dropShadow"></p>
</div>
