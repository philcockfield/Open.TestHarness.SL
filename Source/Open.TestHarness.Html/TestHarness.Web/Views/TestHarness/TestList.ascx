<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="th-testList panelBorder panelBorderTop">

    <!-- Title Bar -->
    <div class="th-testList-tb tb-height absoluteFill panelBorder panelBorderBottom">
        <div class="topHighlightLine opacity080"></div>        
        <div class="table tb-height">
            <div class="vAlignedCell tb-height">
                <p class="title opacity060"><%= Model.TestListTitle %></p>
            </div>
        </div>
        <button class="runTests" value="foo"></button>
    </div>

    <!-- List -->
    <div class="th-testList-content absoluteFill"></div>
    <p class="dropShadow"></p>
</div>
