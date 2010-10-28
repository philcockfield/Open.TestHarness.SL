<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="Open.Core.Web" %>
<%@ Import Namespace="Open.Core.Web.Controllers" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<!-- Sidebar -->
<div id="testHarnessSidebar" class="panelBorder panelBorderRight noSelect">
    <!-- Toolbar -->
    <div class="th-toolbar panelBorder panelBorderBottom">
        <p class="toolbarReflection"></p>
        <div class="topHighlightLine"></div>        
        <img class="th-backMask" src="<%="/Open.Core/TestHarness/Images/Sidebar.BackMask.png".PrependDomain() %>" />
    </div>
    <div class="th-content absoluteFill">
        <!-- Root List -->
        <div class="th-sidebarRootList absoluteFill"></div>

        <!-- Method List -->
        <% Html.RenderAction(TestHarnessController.ActionMethodList); %>
    </div>
</div>

