<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PruebaUC.aspx.cs" Inherits="PAEEEM.MRV.PruebaUC" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="wucMediciones.ascx" TagName="wucMediciones" TagPrefix="uc1" %>
<%@ Register Src="wucCuestionario.ascx" TagName="wucCuestionario" TagPrefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var pre = "wucCuestionario1_";

        function CalculaTotalHorasTrabajadasAltaEquipo() {
            var total = 0; //total semanal de horas trabajadas

            if (document.getElementById(pre + 'RadTxtLunes').value != "" &&
                document.getElementById(pre + 'RadCmbLunes').value != "Seleccione" &&
                document.getElementById(pre + 'ChkLunes').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtLunes').value);

            if (document.getElementById(pre + 'RadTxtMartes').value != "" &&
                document.getElementById(pre + 'RadCmbMartes').value != "Seleccione" &&
                document.getElementById(pre + 'ChkMartes').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtMartes').value);

            if (document.getElementById(pre + 'RadTxtMiercoles').value != "" &&
                document.getElementById(pre + 'RadCmbMiercoles').value != "Seleccione" &&
                document.getElementById(pre + 'ChkMiercoles').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtMiercoles').value);

            if (document.getElementById(pre + 'RadTxtJueves').value != "" &&
                document.getElementById(pre + 'RadCmbJueves').value != "Seleccione" &&
                document.getElementById(pre + 'ChkJueves').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtJueves').value);

            if (document.getElementById(pre + 'RadTxtViernes').value != "" &&
                document.getElementById(pre + 'RadCmbViernes').value != "Seleccione" &&
                document.getElementById(pre + 'ChkViernes').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtViernes').value);

            if (document.getElementById(pre + 'RadTxtSabado').value != "" &&
                document.getElementById(pre + 'RadCmbSabado').value != "Seleccione" &&
                document.getElementById(pre + 'ChkSabado').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtSabado').value);

            if (document.getElementById(pre + 'RadTxtDomingo').value != "" &&
                document.getElementById(pre + 'RadCmbDomingo').value != "Seleccione" &&
                document.getElementById(pre + 'ChkDomingo').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtDomingo').value);

            document.getElementById(pre + 'TxtHorasSemana').value = total.toFixed(1);
            CalculaHorasAnioAltaEquipo();
        }

        function CalculaHorasAnioAltaEquipo() {

            var horasSemana = parseFloat(document.getElementById(pre + 'TxtHorasSemana').value);
            var semanasAnio = parseFloat(document.getElementById(pre + 'RadTxtSemanasAnio').value);

            if (document.getElementById(pre + 'TxtHorasSemana').value == "")
                horasSemana = 0.00;

            if (document.getElementById(pre + 'RadTxtSemanasAnio').value == "")
                semanasAnio = 0.00;

            var horasAnio = horasSemana * semanasAnio;

            document.getElementById(pre + 'TxtHorasAnio').value = horasAnio.toFixed(1);
        }

        function CalculaTotalHorasTrabajadasNegocio() {
            var total = 0; //total semanal de horas trabajadas

            if (document.getElementById(pre + 'RadTxtLunesNeg').value != "" &&
                document.getElementById(pre + 'RadCmbLunesNeg').value != "Seleccione" &&
                document.getElementById(pre + 'ChkLunesNeg').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtLunesNeg').value);

            if (document.getElementById(pre + 'RadTxtMartesNeg').value != "" &&
                document.getElementById(pre + 'RadCmbMartesNeg').value != "Seleccione" &&
                document.getElementById(pre + 'ChkMartesNeg').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtMartesNeg').value);

            if (document.getElementById(pre + 'RadTxtMiercolesNeg').value != "" &&
                document.getElementById(pre + 'RadCmbMiercolesNeg').value != "Seleccione" &&
                document.getElementById(pre + 'ChkMiercolesNeg').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtMiercolesNeg').value);

            if (document.getElementById(pre + 'RadTxtJuevesNeg').value != "" &&
                document.getElementById(pre + 'RadCmbJuevesNeg').value != "Seleccione" &&
                document.getElementById(pre + 'ChkJuevesNeg').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtJuevesNeg').value);

            if (document.getElementById(pre + 'RadTxtViernesNeg').value != "" &&
                document.getElementById(pre + 'RadCmbViernesNeg').value != "Seleccione" &&
                document.getElementById(pre + 'ChkViernesNeg').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtViernesNeg').value);

            if (document.getElementById(pre + 'RadTxtSabadoNeg').value != "" &&
                document.getElementById(pre + 'RadCmbSabadoNeg').value != "Seleccione" &&
                document.getElementById(pre + 'ChkSabadoNeg').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtSabadoNeg').value);

            if (document.getElementById(pre + 'RadTxtDomingoNeg').value != "" &&
                document.getElementById(pre + 'RadCmbDomingoNeg').value != "Seleccione" &&
                document.getElementById(pre + 'ChkDomingoNeg').checked)
                total = total + parseFloat(document.getElementById(pre + 'RadTxtDomingoNeg').value);

            document.getElementById(pre + 'TxtHorasSemanaNeg').value = total.toFixed(1);
            CalculaHorasAnioAltaEquipo();
        }

        function CalculaHorasAnioNegocio() {

            var horasSemana = parseFloat(document.getElementById(pre + 'TxtHorasSemanaNeg').value);
            var semanasAnio = parseFloat(document.getElementById(pre + 'RadTxtSemanasAnioNeg').value);

            if (document.getElementById(pre + 'TxtHorasSemanaNeg').value == "")
                horasSemana = 0.00;

            if (document.getElementById(pre + 'RadTxtSemanasAnioNeg').value == "")
                semanasAnio = 0.00;

            var horasAnio = horasSemana * semanasAnio;

            document.getElementById(pre + 'TxtHorasAnioNeg').value = horasAnio.toFixed(1);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="clientScriptManager" runat="server">
                        </asp:ScriptManager>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">
        <div>
            <%--<uc1:wucMediciones id="wucMediciones1" runat="server"  />--%>
            <uc2:wucCuestionario id="wucCuestionario1" runat="server"  />
        </div>
    </telerik:RadAjaxPanel>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" 
        Skin="Office2010Silver">
    </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>
