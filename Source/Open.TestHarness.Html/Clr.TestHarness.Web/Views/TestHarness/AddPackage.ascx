<%@ Import Namespace="Open.Core.Web" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="th_addPackage noSelect">
    <div class="innerSlide">
        <div class="innerMargin">
            <div class="titleFont"><%= Model.Title %></div>
            <div class="field_set">
                <div class="field_set_scriptUrl">
                    <p class="th_fieldLabel"><%= Model.ScriptUrlLabel %></p>
                    <input class="textbox" type="text" value="/Content/Scripts/MyScript.debug.js" />
                </div>

                <div class="fieldConnector"></div>

                <div class="field_set_initMethod">
                    <p class="th_fieldLabel"><%= Model.InitMethodLabel%></p>
                    <input class="textbox" type="text" value="Open.Core.Test.main()" />
                </div>
            </div>
        </div>
    </div>
    <p class="dropShadow"></p>
</div>

