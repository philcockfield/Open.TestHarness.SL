<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="testHarnessSidebar" class="panelBorder panelBorderRight">
    <div class="th-toolbar panelBorder panelBorderBottom">
        <p class="toolbarReflection"></p>
        <img class="th-home" src="<%= Links.Content.Images.Sidebar_Home_png %>" />
    </div>
    <div class="th-sidebarList absoluteFill"></div>
</div>

