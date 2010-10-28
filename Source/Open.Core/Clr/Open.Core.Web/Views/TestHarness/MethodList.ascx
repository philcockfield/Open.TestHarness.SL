<%@ Import Namespace="Resources" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<!-- Method List -->
<div class="th-testList panelBorder panelBorderTop">

    <!-- Title Bar -->
    <div class="th-testList-tb tb-height absoluteFill panelBorder panelBorderBottom">
        <div class="topHighlightLine opacity080"></div>        
        <div class="table tb-height">
            <div class="vAlignedCell tb-height">
                <p class="title opacity060"><%= TestHarnessStrings.MethodList_Title %></p>
            </div>
        </div>
        <div class="buttons">
            <button class="runTests button">Run</button>
            <div class="buttonDivider"></div>
            <button class="refresh button">Refresh</button>
            <div class="buttonDivider"></div>
        </div>
    </div>

    <!-- List -->
    <div class="th-testList-content absoluteFill"></div>
    <p class="dropShadow"></p>
</div>
