<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="th-main">
    <div class="th-toolbar th-main-tb panelBorder panelBorderBottom">
        <div class="topHighlightLine"></div>
    </div>
    <div class="th-content absoluteFill">
        <div class="th-controlHost absoluteFill"></div>
        <% Html.RenderAction(MVC.TestHarness.ActionNames.Log);%>
    </div>
</div>
