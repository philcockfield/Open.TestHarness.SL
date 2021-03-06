﻿<%@ Import Namespace="Open.Core.Web" %>
<%@ Import Namespace="Open.TestHarness.Web" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%= WebConstants.Script.TypeKit("pdx3yro") %>

<!-- CSS -->
<%= WebConstants.Css[CssFile.Core] %>
<%= WebConstants.Css[CssFile.CoreControls] %>
<%= WebConstants.Css[CssFile.CoreLists] %>
<%= TestHarnessConstants.Css[TestHarnessCssFile.TestHarness] %>
<%= TestHarnessConstants.Css[TestHarnessCssFile.TestHarnessIe] %>

<!-- Script -->
<% Html.InsertCoreJQuery(); %>
<% Html.InsertCoreScripts(); %>
<%= WebConstants.Script[ScriptFile.CoreControls] %>
<%= WebConstants.Script[ScriptFile.CoreLists] %>
    
<!-- TestHarness -->
<%= TestHarnessConstants.Script.Application %>

