<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script id="btnSample_Background" type="text/x-jquery-tmpl">
    <div class="btn_sample btn_sample_bg absoluteFill">
    </div>
</script>

<script id="btnSample_Normal" type="text/x-jquery-tmpl">
    <div class="btn_sample btn_sample_normal absoluteFill">
        <p>${buttonText}</p>
    </div>
</script>

<script id="btnSample_Down" type="text/x-jquery-tmpl">
    <div class="btn_sample btn_sample_down absoluteFill">
        <p>Down</p>
    </div>
</script>

<script id="btn_Sample_Pressed" type="text/x-jquery-tmpl">
    <div class="btn_sample btn_sample_down absoluteFill">
        <p>Pressed</p>
    </div>
</script>

<script id="btnSample_Overlay" type="text/x-jquery-tmpl">
    <p class="btn_sample toolbarReflection"></p>
</script>
