<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CapturaMRV.aspx.cs" Inherits="PAEEEM.MRV.CapturaMRV" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="wucEncabezadoMRV.ascx" TagName="wucEncabezadoMRV" TagPrefix="uc1" %>
<%@ Register Src="wucMediciones.ascx" TagName="wucMediciones" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var pre = "ctl00_MainContent_wucMediciones1_";

        function poponload() {
            var idMedicion = document.getElementById(pre + "HidIdMedicion").value;
            var testwindow = window.open('VisorImagenes.aspx?Id=' + idMedicion + '&IdT=2', '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
            testwindow.moveTo(0, 0);
        }

        function confirmCallBackFn(arg) {
            if (arg == true) {
                var oButton = document.getElementById(pre + "hidBtnFinaliza");
                oButton.click();
            }
        }

                
    </script>
    
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            var pre = "ctl00_MainContent_wucMediciones1_";

            function postackControl() {
                var oButton = document.getElementById(pre + "btnRefresh2");
                oButton.click();
            }

            function OnClientValidationFailedUploadArchivo(sender, args) {
                alert("Ocurrió un problema al cargar el archivo");
            }
            function OnClientClicking(sender, args) {
                var event = args.get_domEvent();
                if (event.keyCode == 13) {
                    args.set_cancel(true);
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">
        <div style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:wucEncabezadoMRV id="wucEncabezadoMRV1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc2:wucMediciones id="wucMediciones1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </telerik:RadAjaxPanel>
    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" 
        Skin="Office2010Silver">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="rwmVentana" runat="server" Skin="Office2010Silver">
    </telerik:RadWindowManager>
</asp:Content>
