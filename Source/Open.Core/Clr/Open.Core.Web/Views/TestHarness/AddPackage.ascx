<%@ Import Namespace="Resources" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="th_addPackage noSelect">
    <div class="innerSlide">
        <div class="innerMargin">
            <div class="titleFont"><%= TestHarnessStrings.Add_Package_Title %></div>
            <div class="field_set">
                <div class="field_set_scriptUrl">
                    <p class="th_fieldLabel"><%= TestHarnessStrings.Add_Package_Label_ScriptUrl %></p>
                    <input class="textbox" type="text" />
                </div>

                <div class="fieldConnector"></div>

                <div class="field_set_initMethod">
                    <p class="th_fieldLabel"><%= TestHarnessStrings.Add_Package_Label_InitMethod %></p>
                    <input class="textbox" type="text"  />
                </div>
            </div>
            <div class="buttons">
                <button class="add button">Add</button>
                <button class="cancel button">Cancel</button>
            </div>
        </div>
    </div>
    <p class="dropShadow"></p>
</div>

