<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="Open.TestHarness" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TestHarness</title>

    <link rel="Stylesheet" href="~/Content/Css/Core.css" type="text/css" />
    <link rel="Stylesheet" href="~/Content/Css/TestHarness.css" type="text/css" />
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/Scripts/JQuery/jquery.cookie.js" type="text/javascript"></script>

    <script src="/Scripts/TestHarness/mscorlib.js" type="text/javascript"></script>
    <script src="/Scripts/TestHarness/Open.TestHarness.debug.js" type="text/javascript"></script>
    <script src="/Scripts/TestHarness/Open.Core.debug.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Open.TestHarness.Application.main();
        });
    </script>

</head>
<body>

    <div id="root">
        <div id="sidebar" class="panelBorder panelBorderRight">
            <div id="sidebarToolbar" class="panelBorder panelBorderBottom toolbar">
                <p class="toolbarReflection"></p>
            </div>
        </div>
        <div id="main">
            <div id="mainToolbar" class="panelBorder panelBorderBottom toolbar"></div>
            <%= Html.Action(MVC.TestHarness.Log()) %>
        </div>
    </div>

</body>
</html>
