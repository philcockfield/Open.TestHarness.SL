<%@ Import Namespace="Open.Core.Web" %>
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= Model.AppTitle %></title>

    <!-- CSS -->
    <link rel="Stylesheet" href="/Open.Core/Css/Core.css" type="text/css" />
    <link rel="Stylesheet" href="/Open.Core/Css/Core.Controls.css" type="text/css" />
    <link rel="Stylesheet" href="/Open.Core/Css/Core.Lists.css" type="text/css" />
    <link rel="Stylesheet" href="/Content/Css/Open.TestHarness.css" type="text/css" />

    <!-- TypeKit -->
    <script type="text/javascript" src="http://use.typekit.com/pdx3yro.js"></script>
    <script type="text/javascript">try { Typekit.load(); } catch (e) { }</script>   
    
    <% Html.InsertCoreJQuery();%>
    <% Html.InsertCoreScripts();%>
    <script src="/Open.Core/Scripts/Open.Core.Controls.debug.js" type="text/javascript"></script>
    <script src="/Open.Core/Scripts/Open.Core.Lists.debug.js" type="text/javascript"></script>

    <!-- TestHarness -->
    <script src="/Content/Scripts/Open.TestHarness.debug.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Open.TestHarness.Application.main();
        });
    </script>

</head>
<body>

    <div id="testHarness">
         <% Html.RenderAction(MVC.TestHarness.ActionNames.Sidebar); %>
        <div class="th-main">
            <div class="th-mainToolbar th-toolbar panelBorder panelBorderBottom"></div>
            <div class="th-mainContent">
                <% Html.RenderAction(MVC.TestHarness.ActionNames.Log);%>
            </div>
    </div>

</body>
</html>
