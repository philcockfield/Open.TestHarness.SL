<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="testHarnessSidebar" class="panelBorder panelBorderRight">
    <div class="th-sidebarToolbar th-toolbar panelBorder panelBorderBottom">
        <img class="th-backMask" src="<%= Links.Content.Images.Sidebar_BackMask_png %>" />
        <div class="th-topHighlight th-sidebarToolbar-highlight"></div>        
    </div>
    <div class="th-sidebarList absoluteFill"></div>
</div>

