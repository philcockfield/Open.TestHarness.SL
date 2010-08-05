﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TestHarness</title>

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
    <link rel="Stylesheet" href="~/Content/Common.css" type="text/css" />
    <link rel="Stylesheet" href="~/Content/TestHarness.css" type="text/css" />

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>

    <script src="/Scripts/TestHarness/mscorlib.js" type="text/javascript"></script>
    <script src="/Scripts/TestHarness/Open.TestHarness.Script.debug.js" type="text/javascript"></script>
    <script src="/Scripts/TestHarness/Open.Core.Script.debug.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
          Open.TestHarness.Application.main();
        });
    </script>

</head>
<body>

    <div id="root">
        <div id="sidebar" class="panelBorder panelBorderRight">Sidebar</div>
        <div id="main">
        
            Main
            <div id="outputLog" class="panelBorder panelBorderTop">Output</div>

        </div>
    </div>

</body>
</html>
