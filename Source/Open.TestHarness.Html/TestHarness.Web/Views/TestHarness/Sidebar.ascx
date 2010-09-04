<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="testHarnessSidebar" class="panelBorder panelBorderRight">
    <div class="th-toolbar panelBorder panelBorderBottom">
        <p class="toolbarReflection"></p>
        <div class="topHighlightLine"></div>        
        <img class="th-backMask" src="<%= Links.Content.Images.Sidebar_BackMask_png %>" />
    </div>
    <div class="th-content absoluteFill">
        <div class="th-sidebarRootList absoluteFill"></div>
        <% Html.RenderAction(MVC.TestHarness.ActionNames.TestList); %>
    </div>
</div>

