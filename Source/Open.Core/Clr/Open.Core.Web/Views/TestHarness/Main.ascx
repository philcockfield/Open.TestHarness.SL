﻿<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="Open.Core.Web.Controllers" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<!-- Main -->
<div class="th-main">
    <!-- Title/Toolbar -->
    <div class="th-toolbar th-main-tb panelBorder panelBorderBottom">
        <div class="topHighlightLine"></div>
    </div>
    <div class="th-content absoluteFill">
        <!-- Control Host -->
        <div class="th-controlHost absoluteFill"></div>
        <% Html.RenderAction(TestHarnessController.ActionLog);%>
    </div>
</div>
