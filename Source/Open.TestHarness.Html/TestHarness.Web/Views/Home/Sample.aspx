<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head>
  <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
  <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
  <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
  <style type="text/css">
    #resizable { width: 100px; height: 100px; background: silver; }
  </style>
  <script>
      $(document).ready(function () {
          $("#resizable").resizable();
      });
  </script>
</head>
<body style="font-size:62.5%;">
  
<div id="resizable"></div>

</body>
</html>