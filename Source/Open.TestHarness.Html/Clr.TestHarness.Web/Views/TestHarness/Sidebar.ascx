<%@ Import Namespace="Open.Core.Web" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<!-- Sidebar -->
<div id="testHarnessSidebar" class="panelBorder panelBorderRight noSelect">
    <!-- Toolbar -->
    <div class="th-toolbar panelBorder panelBorderBottom">
        <p class="toolbarReflection"></p>
        <div class="topHighlightLine"></div>        
        <img class="th-backMask" src="<%= Links.Content.Images.Sidebar_BackMask_png.PrependDomain() %>" />
    </div>
    <div class="th-content absoluteFill">
        <!-- Root List -->
        <div class="th-sidebarRootList absoluteFill"></div>

        <!-- Method List -->
        <% Html.RenderAction(MVC.TestHarness.ActionNames.MethodList); %>
    </div>
</div>

