<%@ Import Namespace="Open.Core.Web" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="th_addPackage noSelect">
    <div class="innerContent">
        <div class="titleFont"><%= Model.Title %></div>

        <div class="field_set">
            <div class="field_set_scriptUrl">
                <p class="th_fieldLabel"><%= Model.ScriptUrlLabel %></p>
                <input type="text" value="/Content/Scripts/MyScript.debug.js" class="textBox">
                <img class="iconJavaScript" src="<%= Links.Content.Images.Icons.Icon_JavaScript_png.PrependDomain() %>" />
            </div>

            <img class="fieldConnector" src="<%= Links.Content.Images.AddPackage_FieldConnector_png.PrependDomain() %>" />

            <div class="field_set_initMethod">
                <p class="th_fieldLabel"><%= Model.InitMethodLabel%></p>
                <input type="text" value="Open.Core.Test.main()" class="textBox">
                <img class="iconMethod" src="<%= Links.Content.Images.Icons.Icon_Method_Small_png.PrependDomain() %>" />
            </div>


        </div>

    </DIV>
</div>

