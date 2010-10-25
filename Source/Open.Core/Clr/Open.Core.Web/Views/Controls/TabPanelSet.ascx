<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script id="tmplTabPanelSet" type="text/html">
    <div class="c_tabPanelSet absoluteFill">
        <div class="toolbarPosition toolbarBackground panelBorder panelBorderBottom"></div>
        <div class="toolbarPosition overflowing">
            <img src="/Open.Assets/Icons/Silk/SilkControlFastforward.png" />
        </div>
        <div class="toolbarPosition toolbar noSelect"></div>
        <div class="container">
        </div>
    </div>
</script>

<script id="tmplTabPanelBtn_Bg" type="text/html">
    <div class="c_tabPanelButton background absoluteFill panelBorder panelBorderRight">
        <p class="toolbarReflection"></p>
    </div>
</script>

<script id="tmplTabPanelBtn_BgOver" type="text/html">
    <div class="c_tabPanelButton over absoluteFill"></div>
</script>

<script id="tmplTabPanelBtn_BgPressed" type="text/html">
    <div class="c_tabPanelButton pressed absoluteFill">
    </div>
</script>

<script id="tmplTabPanelBtn_Text" type="text/html">
    <div class="c_tabPanelButton text absoluteFill">
        ${buttonText}
    </div>
</script>