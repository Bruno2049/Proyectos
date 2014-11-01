<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RespaldoCapturaCompPruebas.aspx.cs" Inherits="PAEEEM.SupplierModule.RespaldoCapturaCompPruebas" MasterPageFile="~/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="wuAltaBajaEquipos.ascx" TagName="wuAltaBajaEquipos" TagPrefix="uc2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript">

        var pre = "ctl00_MainContent_wizardPages_";

        function CalculateDateDiff(dateFrom, dateTo) {
            var difference = (dateTo - dateFrom);

            var years = Math.floor(difference / (1000 * 60 * 60 * 24 * 365));
            difference -= years * (1000 * 60 * 60 * 24 * 365);
            var months = Math.floor(difference / (1000 * 60 * 60 * 24 * 30.4375));
            var daysBetween = Math.floor(difference / (1000 * 60 * 60 * 24));

            var dif = '';
            if (years > 0)
                dif = years;

            //if (months > 0) {
            //    if (years > 0) dif += ' y ';
            //    dif += months;
            //}

            //if (daysBetween > 0) {
            //    dif += daysBetween;
            //}

            return dif;
        }

        function muestra_calendarioValidEdad(v) {
            var cfecha = showModalDialog("images/calendario.htm", 0, "dialogWidth:272px;dialogHeight:235px;center:yes");
            if (cfecha == -1 || cfecha == null || cfecha == "")
                document.getElementById(pre + v).value = "";
            else {
                var annio = cfecha.substring(6, 10);
                var mes = cfecha.substring(3, 5);
                var dia = cfecha.substring(0, 2);

                var fechaActual = new Date();
                var fechaTexto = annio + "-" + mes + "-" + dia;

                var ms = Date.parse(fechaTexto);

                var fechaCapturada = new Date(ms);

                var dif = CalculateDateDiff(fechaCapturada, fechaActual);


                if (dif < 66) {
                    if (fechaCapturada > fechaActual)
                        alert("La fecha seleccionada debe ser menor a la fecha actual");
                    else {
                        var fechaFormato = cfecha.substring(6, 10) + "-" + cfecha.substring(3, 5) + "-" + cfecha.substring(0, 2);
                        document.getElementById(pre + v).value = fechaFormato;
                    }
                } else {
                    alert("La edad del cliente no cumple con los requisitos.");
                }
            }
        }


        function muestra_calendario(v) {
            var cfecha = showModalDialog("images/calendario.htm", 0, "dialogWidth:272px;dialogHeight:235px;center:yes");
            if (cfecha == -1 || cfecha == null || cfecha == "")
                document.getElementById(pre + v).value = "";
            else {
                var annio = cfecha.substring(6, 10);
                var mes = cfecha.substring(3, 5);
                var dia = cfecha.substring(0, 2);

                var fechaActual = new Date();
                var fechaTexto = annio + "-" + mes + "-" + dia;

                var ms = Date.parse(fechaTexto);

                var fechaCapturada = new Date(ms);

                if (fechaCapturada > fechaActual)
                    alert("La fecha seleccionada debe ser menor a la fecha actual");
                else {
                    var fechaFormato = cfecha.substring(6, 10) + "-" + cfecha.substring(3, 5) + "-" + cfecha.substring(0, 2);
                    document.getElementById(pre + v).value = fechaFormato;
                }
            }
        }

        function soloLetras(e) {
            var key = e.keyCode || e.which;
            var tecla = String.fromCharCode(key).toLowerCase();
            var letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";

            if (letras.indexOf(tecla) == -1) {
                return false;
            }
            return true;
        }

        function lockScreen() {
            var lock = document.getElementById('lock');
            lock.style.width = '300px';
            lock.style.height = '30px';
            lock.style.top = document.body.offsetHeight / 1.5 - lock.style.height.replace('px', '') / 2 + 'px';
            lock.style.left = document.body.offsetWidth / 2 - lock.style.width.replace('px', '') / 2 + 'px';
            if (lock)
                lock.className = 'LockOn';
        }
        function lockScreen1() {
            var lock = document.getElementById('lock');
            lock.style.width = '300px';
            lock.style.height = '30px';
            lock.style.top = document.body.offsetHeight / 5 * 4 + 'px';
            lock.style.left = document.body.offsetWidth / 2 - lock.style.width.replace('px', '') / 2 + 'px';
            if (lock)
                lock.className = 'LockOn';
        }
        function CapitalASCII(obj) {
            var valor = obj.value.toUpperCase();
            valor = valor.replace("Á", "A", "gmi");
            valor = valor.replace("É", "E", "gmi");
            valor = valor.replace("Í", "I", "gmi");
            valor = valor.replace("Ó", "O", "gmi");
            valor = valor.replace("Ú", "U", "gmi");
            if (obj.value != valor)
                obj.value = valor;
        }

        function CalculaTotalHorasTrabajadas() {
            var total = 0; //total semanal de horas trabajadas

            if (document.getElementById(pre + 'hlabor1Negocio').value != "" &&
                document.getElementById(pre + 'DDXInicioLunes').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor1Negocio').value);
            if (document.getElementById(pre + 'hlabor2Negocio').value != "" &&
                document.getElementById(pre + 'DDXInicioMartes').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor2Negocio').value);
            if (document.getElementById(pre + 'hlabor3Negocio').value != "" &&
                document.getElementById(pre + 'DDXInicioMiercoles').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor3Negocio').value);
            if (document.getElementById(pre + 'hlabor4Negocio').value != "" &&
                document.getElementById(pre + 'DDXInicioJueves').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor4Negocio').value);
            if (document.getElementById(pre + 'hlabor5Negocio').value != "" &&
                document.getElementById(pre + 'DDXInicioViernes').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor5Negocio').value);
            if (document.getElementById(pre + 'hlabor6Negocio').value != "" &&
                document.getElementById(pre + 'DDXInicioSabado').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor6Negocio').value);
            if (document.getElementById(pre + 'hlabor7Negocio').value != "" &&
                document.getElementById(pre + 'DDXInicioDomingo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor7Negocio').value);

            document.getElementById(pre + 'TxtHorasSemana').value = total;
            CalculaHorasAnio();
        }
        function CalculaHorasAnio() {
            var horasSemana = parseFloat(document.getElementById(pre + 'TxtHorasSemana').value);
            var semanasAnio = parseFloat(document.getElementById(pre + 'noSemanasNegocio').value);

            if (document.getElementById(pre + 'TxtHorasSemana').value == "")
                horasSemana = 0;

            if (document.getElementById(pre + 'noSemanasNegocio').value == "")
                semanasAnio = 0;

            var horasAnio = horasSemana * semanasAnio;

            document.getElementById(pre + 'TxtHorasAnio').value = horasAnio.toFixed(1);
        }
        function HabilitaHorariosBajaEquipo(inicio, fin, check1) {
            var casilla = document.getElementById(pre + check1);

            if (casilla.checked == true) {
                document.getElementById(pre + inicio).disabled = false;
                document.getElementById(pre + fin).disabled = false;
            }
            else {
                document.getElementById(pre + inicio).disabled = true;
                document.getElementById(pre + fin).disabled = true;
            }

        }


        /*CAPTURA BAJA DE EQUIPOS*/
        function CalculaHorasTrabajadasDiaBajaEquipo(entrada, salida) {
            var horasTrabajadas;

            if ((entrada == 0) || (salida == 0)) {
                horasTrabajadas = 0;
            }
            else {
                if (entrada > salida) {
                    var horasEntrada = 24 - entrada;
                    var horasSalida = 0 + salida;

                    horasTrabajadas = horasEntrada + horasSalida;
                }
                else {
                    horasTrabajadas = salida - entrada;
                }
            }
            return horasTrabajadas;
        }
        function CalculaTotalHorasTrabajadasBajaEquipo() {
            var total = 0; //total semanal de horas trabajadas

            if (document.getElementById(pre + 'hlabor1BajaEquipo').value != "" &&
                document.getElementById(pre + 'DDXInicioLunesBajaEquipo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor1BajaEquipo').value);
            if (document.getElementById(pre + 'hlabor2BajaEquipo').value != "" &&
                document.getElementById(pre + 'DDXInicioMartesBajaEquipo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor2BajaEquipo').value);
            if (document.getElementById(pre + 'hlabor3BajaEquipo').value != "" &&
                document.getElementById(pre + 'DDXInicioMiercolesBajaEquipo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor3BajaEquipo').value);
            if (document.getElementById(pre + 'hlabor4BajaEquipo').value != "" &&
                document.getElementById(pre + 'DDXInicioJuevesBajaEquipo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor4BajaEquipo').value);
            if (document.getElementById(pre + 'hlabor5BajaEquipo').value != "" &&
                document.getElementById(pre + 'DDXInicioViernesBajaEquipo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor5BajaEquipo').value);
            if (document.getElementById(pre + 'hlabor6BajaEquipo').value != "" &&
                document.getElementById(pre + 'DDXInicioSabadoBajaEquipo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor6BajaEquipo').value);
            if (document.getElementById(pre + 'hlabor7BajaEquipo').value != "" &&
                document.getElementById(pre + 'DDXInicioDomingoBajaEquipo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor7BajaEquipo').value);

            document.getElementById(pre + 'TxtHorasSemanaBajaEquipo').value = total;
            CalculaHorasAnioBajaEquipo();
        }
        function CalculaHorasAnioBajaEquipo() {
            var horasSemana = parseFloat(document.getElementById(pre + 'TxtHorasSemanaBajaEquipo').value);
            var semanasAnio = parseFloat(document.getElementById(pre + 'noSemanasBajaEquipo').value);

            if (document.getElementById(pre + 'TxtHorasSemanaBajaEquipo').value == "")
                horasSemana = 0;

            if (document.getElementById(pre + 'noSemanasBajaEquipo').value == "")
                semanasAnio = 0;

            var horasAnio = horasSemana * semanasAnio;

            document.getElementById(pre + 'TxtHorasAnioBajaEquipo').value = horasAnio.toFixed(1);
        }
        function HabilitaHorariosBajaEquipo(inicio, fin, check1) {
            var casilla = document.getElementById(pre + check1);

            if (casilla.checked == true) {
                document.getElementById(pre + inicio).disabled = false;
                document.getElementById(pre + fin).disabled = false;
            }
            else {
                document.getElementById(pre + inicio).disabled = true;
                document.getElementById(pre + fin).disabled = true;
            }

        }
        
        function CalculaHorasTrabajadasDiaAltaEquipoViejo() {
            var total = 0; //total semanal de horas trabajadas

            if (document.getElementById(pre + 'hlabor1').value != "" &&
                document.getElementById(pre + 'DDXInicioLunesAltaEquipoViejo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor1').value);
            if (document.getElementById(pre + 'hlabor2').value != "" &&
                document.getElementById(pre + 'DDXInicioMartesAltaEquipoViejo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor2').value);
            if (document.getElementById(pre + 'hlabor3').value != "" &&
                document.getElementById(pre + 'DDXInicioMiercolesAltaEquipoViejo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor3').value);
            if (document.getElementById(pre + 'hlabor4').value != "" &&
                document.getElementById(pre + 'DDXInicioJuevesAltaEquipoViejo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor4').value);
            if (document.getElementById(pre + 'hlabor5').value != "" &&
                document.getElementById(pre + 'DDXInicioViernesAltaEquipoViejo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor5').value);
            if (document.getElementById(pre + 'hlabor6').value != "" &&
                document.getElementById(pre + 'DDXInicioSabadoAltaEquipoViejo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor6').value);
            if (document.getElementById(pre + 'hlabor7').value != "" &&
                document.getElementById(pre + 'DDXInicioDomingoAltaEquipoViejo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlabor7').value);

            //total = parseInt(total);

            document.getElementById(pre + 'TxtHorasSemanaAltaEquipoViejo').value = total;
            CalculaHorasAnioAltaEquipoViejo();
        }
        function CalculaHorasAnioAltaEquipoViejo() {

            var horasSemana = parseFloat(document.getElementById(pre + 'TxtHorasSemanaAltaEquipoViejo').value);
            var semanasAnio = parseFloat(document.getElementById(pre + 'noSemanasAltaEquipoViejo').value);

            if (document.getElementById(pre + 'TxtHorasSemanaAltaEquipoViejo').value == "")
                horasSemana = 0.00;

            if (document.getElementById(pre + 'noSemanasAltaEquipoViejo').value == "")
                semanasAnio = 0.00;

            var horasAnio = horasSemana * semanasAnio;

            document.getElementById(pre + 'TxtHorasAnioAltaEquipoViejo').value = horasAnio.toFixed(1);
        }
        function CalculaTotalHorasTrabajadasAltaEquipoNuevo() {
            var total = 0.00;

            if (document.getElementById(pre + 'hlaborNuevo1').value != "" &&
                document.getElementById(pre + 'DDXInicioLunesAltaEquipoNuevo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlaborNuevo1').value);
            if (document.getElementById(pre + 'hlaborNuevo2').value != "" &&
                document.getElementById(pre + 'DDXInicioMartesAltaEquipoNuevo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlaborNuevo2').value);
            if (document.getElementById(pre + 'hlaborNuevo3').value != "" &&
                document.getElementById(pre + 'DDXInicioMiercolesAltaEquipoNuevo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlaborNuevo3').value);
            if (document.getElementById(pre + 'hlaborNuevo4').value != "" &&
                document.getElementById(pre + 'DDXInicioJuevesAltaEquipoNuevo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlaborNuevo4').value);
            if (document.getElementById(pre + 'hlaborNuevo5').value != "" &&
                document.getElementById(pre + 'DDXInicioViernesAltaEquipoNuevo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlaborNuevo5').value);
            if (document.getElementById(pre + 'hlaborNuevo6').value != "" &&
                document.getElementById(pre + 'DDXInicioSabadoAltaEquipoNuevo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlaborNuevo6').value);
            if (document.getElementById(pre + 'hlaborNuevo7').value != "" &&
                document.getElementById(pre + 'DDXInicioDomingoAltaEquipoNuevo').value != "Seleccione")
                total = total + parseFloat(document.getElementById(pre + 'hlaborNuevo7').value);

            document.getElementById(pre + 'TxtHorasSemanaAltaEquipoNuevo').value = total.toFixed(1); // .toFixed(1);
            CalculaHorasAnioAltaEquipoNuevo();
        }
        function CalculaHorasAnioAltaEquipoNuevo() {

            var horasSemana = parseFloat(document.getElementById(pre + 'TxtHorasSemanaAltaEquipoNuevo').value);
            var semanasAnio = parseFloat(document.getElementById(pre + 'TxtSemanasAnioAltaEquipoNuevo').value);

            if (document.getElementById(pre + 'TxtHorasSemanaAltaEquipoNuevo').value == "")
                horasSemana = 0.00;

            if (document.getElementById(pre + 'TxtSemanasAnioAltaEquipoNuevo').value == "")
                semanasAnio = 0.00;

            var horasAnio = horasSemana * semanasAnio;

            document.getElementById(pre + 'TxtHorasAnioAltaEquipoNuevo').value = horasAnio.toFixed(1);
        }
        function Cancel_OnKeyPress(sender, args) {
            //Add JavaScript handler code here
            var charCode = (args._keycode) ? args.which : args._keyCode;
            if (charCode == 13 || charCode == 13) {

                args._domEvent.preventDefault();

            }
        }

        function confirmCallBackFn(arg) {
            if (arg == true) {
                var oButton = document.getElementById("ctl00_MainContent_" + "HiddenButton");
                oButton.click();
            }
        }
        
    </script>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            
            function OnClientFilesUploadedFachada(sender, args)
            {
                __doPostBack('<% = this.UploadFachada.ClientID%>', '');
                var hiden = document.getElementById(pre + "HidActualiza2Ok");
                hiden.value = "Fachada";
                var hidenState = document.getElementById(pre + "HidstateLoad2");
                hidenState.value = "ok";
                var divok = $get("<%=btnRefresh2.ClientID%>");
                divok.click();
            }
            function OnClientFilesUploadedEquipoViejo(sender, args) {
                __doPostBack('<% = this.UploadEquipoViejo.ClientID%>', '');
                var hiden = document.getElementById(pre + "HidActualiza2Ok");
                hiden.value = "Viejo";

                var hidenState = document.getElementById(pre + "HidstateLoad2");
                hidenState.value = "ok";

                var divok = $get("<%=btnRefresh2.ClientID%>");
                divok.click();
            }
            function OnClientFilesUploadEquipoNuevo(sender, args)
            {
                __doPostBack('<% = this.UploadEquipoNuevo.ClientID%>', '');
                var hiden = document.getElementById(pre + "HidActualiza2Ok");
                hiden.value = "Nuevo";
                var hidenState = document.getElementById(pre + "HidstateLoad2");
                hidenState.value = "ok";
                var divok = $get("<%=btnRefresh2.ClientID%>");
                divok.click();
            }
            
            function OnClientFilesFailUploadEquipoNuevo(sender, args) {
                var hiden = document.getElementById(pre + "HidActualiza2Ok");
                hiden.value = "Nuevo";
                var hidenState = document.getElementById(pre + "HidstateLoad2");
                hidenState.value = "ko";
                var divok = $get("<%=btnRefresh2.ClientID%>");
                divok.click();
            }
            
            function OnClientFilesFailUploadedEquipoViejo(sender, args)
            {
                var hiden = document.getElementById(pre + "HidActualiza2Ok");
                hiden.value = "Viejo";
                var hidenState = document.getElementById(pre + "HidstateLoad2");
                hidenState.value = "ko";
                var divok = $get("<%=btnRefresh2.ClientID%>");
                divok.click();
            }
            
            function OnClientFilesFailUploadedFachada(sender, args) {
                var hiden = document.getElementById(pre + "HidActualiza2Ok");
                hiden.value = "Fachada";
                var hidenState = document.getElementById(pre + "HidstateLoad2");
                hidenState.value = "ko";
                var divok = $get("<%=btnRefresh2.ClientID%>");
                divok.click();
            }
           
            function OnClientFilesUploadedBajaEquipoViejo(sender, args) {
                __doPostBack('<% = this.UploadedBajaEquipoViejo.ClientID%>', '');
                var hiden = document.getElementById(pre + "HidActualizaOk");
                hiden.value = "Viejo";
                var hidenState = document.getElementById(pre + "HidstateLoad");
                hidenState.value = "ok";
                var divok = $get("<%=btnRefresh.ClientID%>");
                divok.click();

            }

            function OnClientFilesFailUploadedBajaEquipoViejo(sender, args) {
                var hiden = document.getElementById(pre + "HidActualizaOk");
                hiden.value = "Viejo";
                var hidenState = document.getElementById(pre + "HidstateLoad");
                hidenState.value = "ko";
                var divok = $get("<%=btnRefresh.ClientID%>");
                divok.click();
            }

            function OnClientValidationFailUploadedFachada(sender, args) {
                alert("El Formato de Imagen no es válido. (Intente con emf, wmf, jpg, jpeg, jpe, png, bmp, tif)");
            }

            function OnClientValidationFailedUploadedBajaEquipoViejo(sender, args) {
                alert("El Formato de Imagen no es válido. (Intente con emf, wmf, jpg, jpeg, jpe, png, bmp, tif)");
            }

            function OnClientValidationFailedUploadEquipoNuevo(sender, args) {
                alert("El Formato de Imagen no es válido. (Intente con emf, wmf, jpg, jpeg, jpe, png, bmp, tif)");
            }

        </script>
        </telerik:RadScriptBlock>
    <style type="text/css">
        #busyIndicator {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: #FFFFFF;
            opacity: 0.8;
            -moz-opacity: 0.8;
            filter: alpha(opacity=80);
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=80)";
            z-index: 10000;
        }

        #busyIndicator img {
            position: absolute;
            margin: auto;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
        }

         .auto-style1 {
             width: 291px;
         }

        .auto-style2 {
            width: 283px;
        }

        .TextBoxMayusculas {
            width: 25%;
            text-transform: uppercase;
        }

        .auto-style3 {
            width: 101px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <%--skgjsdg--%>
            <%--      <div id="lock"  class="LockOff">
                <img src="../images/progress.gif" alt="¡ En Proceso, Por favor Espere !" style="vertical-align: middle; position: relative;" />
            </div>--%>
            <%--        <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Image runat="server" ImageUrl="~/SupplierModule/images/t_alta.png" ID="imgAlta1" />
                                        <asp:Image runat="server" ImageUrl="~/SupplierModule/images/t_edicioncredito.png" ID="imgEdit1" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label ID="lblCredito1" Text="No. Crédito" runat="server" CssClass="Label" Width="165px" ForeColor="#333333" Visible="False" />
                                            <asp:TextBox ID="txtCredito1" runat="server" CssClass="TextBox" Width="250px" Enabled="false" Visible="False" />
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" Text="Fecha" CssClass="Label_2" ID="lblDate"></asp:Label>
                                        <asp:Label runat="server" ID="lbbNowdate"></asp:Label>
                                    </td>
                                </tr>
                            </table>--%>
            <asp:Wizard ID="wizardPages" runat="server" DisplaySideBar="false" Style="width: auto"
                StartNextButtonText="Siguiente" OnNextButtonClick="wizardPages_NextButtonClick"
                ActiveStepIndex="0" StepNextButtonText="Siguiente" StepPreviousButtonText="Regresar"
                FinishCompleteButtonText="Guardar" FinishPreviousButtonText="Regresar"
                OnPreviousButtonClick="btnStepPre_Click" OnFinishButtonClick="wizardPages_FinishButtonClick">
                <FinishNavigationTemplate>
                    <asp:Button ID="btnFinishPre" runat="server" CssClass="Button" Text="Regresar" CommandName="MovePrevious"
                        OnClientClick="return confirm('¿ Desea Regresar a la Pantalla Previa ?');" />
                    <asp:Button ID="btnValidarIntegrar" runat="server" CssClass="Button" Text="Validar e Integrar" OnClick="btnValidarIntegrar_Click"
                       />   <%--OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"--%>
                </FinishNavigationTemplate>
                <StartNavigationTemplate>
                    <div>
                        <asp:Button ID="btnStartNext" runat="server" Text="Siguiente" CssClass="Button" CommandName="MoveNext"
                            OnClientClick="return confirm('Se evaluaran los datos ingresados para validar si la empresa es una PyME ¿Estás seguro de que la información capturada es correcta?');" />
                        <asp:Button ID="btnSalir" Text="Salir" runat="server" CssClass="Button" OnClick="btnSalir_Click"
                            OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
                    </div>
                </StartNavigationTemplate>
                <WizardSteps>
                    <asp:WizardStep runat="server" ID="wizValidaPyME" StepType="Start">
                        <div>
                            <asp:Image runat="server" ImageUrl="images/t_alta.png" ID="imgAlta1" />
                            <br/>
                            <br/>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="3">Número de Servicio RPU:
                                            <asp:TextBox ID="TxtNoRPU" runat="server" Width="164px" ReadOnly="True" Enabled="False" />
                                    </td>
                                    <td align="right">
                                        <asp:Label runat="server" Text="Fecha" CssClass="Label_2" ID="lblDate" />
                                        <asp:Label runat="server" ID="lbbNowdate" />
                                    </td>
                                </tr>
                            </table>
                            <fieldset style="width: 90%">
                                <legend style="font-size: 14px; align-content: initial">
                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/SupplierModule/images/t1.png" />
                                </legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="3" style="color: #0099FF; font-weight: bold;">DATOS DEL NEGOCIO
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table style="width: 100%">
                                    <tr>
                                        <td class="auto-style2" style="vertical-align: top;">
                                            <asp:Label ID="Label26" Width="250px" CssClass="td_label" Text="Tipo de Persona: " runat="server" />
                                            <asp:DropDownList ID="DDXTipoPersona" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DDXTipoPersona_SelectedIndexChanged" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXTipoPersona"
                                                ErrorMessage="Se debe seleccionar el Tipo de persona"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator93"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td colspan="2" class="auto-style2" style="vertical-align: top;" />
                                    </tr>
                                </table>
                                <div runat="server" id="divRFCFisica">
                                    <table style="width: 100%">
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="Label55" Width="250px" CssClass="td_label" Text="Nombre " runat="server" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="Label56" Width="250px" CssClass="td_label" Text="Apellido Paterno: " runat="server" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="Label57" Width="250px" CssClass="td_label" Text="Apellido Materno: " runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:TextBox ID="TxtNombrePFisicaVPyME" CssClass="TextBoxMayusculas" runat="server" MaxLength="100" Width="250px" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator88" runat="server" ControlToValidate="TxtNombrePFisicaVPyME" Display="Dynamic" ErrorMessage="Se debe capturar el Nombre" Text="*" ValidationGroup="Basica" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender51" runat="server" Enabled="True" TargetControlID="TxtNombrePFisicaVPyME" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />

                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:TextBox ID="TxtApellidoPaternoVPyME" CssClass="TextBoxMayusculas" runat="server" MaxLength="50" Width="250px" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator89" runat="server" ControlToValidate="TxtApellidoPaternoVPyME" Display="Dynamic" ErrorMessage="Se debe capturar el Apellido Paterno" Text="*" ValidationGroup="Basica" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender52" runat="server" Enabled="True" TargetControlID="TxtApellidoPaternoVPyME" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:TextBox ID="TxtApellidoMaternoVPyME" runat="server" CssClass="TextBoxMayusculas" MaxLength="50" Width="250px" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender53" runat="server" Enabled="True" TargetControlID="TxtApellidoMaternoVPyME" ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="Label58" Width="250px" CssClass="td_label" Text="Fecha Nacimiento: " runat="server" />
                                                <asp:TextBox ID="TxtFechaNacimientoVPyME" runat="server" Width="230px"></asp:TextBox>
                                                <img id="Img2" onclick="muestra_calendarioValidEdad('TxtFechaNacimientoVPyME')" runat="server" src="~/CentralModule/images/Calendario.bmp" style="width: 17px; height: 17px" alt="" />
                                                &nbsp;</img>&nbsp;</img>&nbsp;</img><asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtFechaNacimientoVPyME"
                                                    ErrorMessage="Se debe capturar la Fecha Nacimiento"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator90"
                                                    InitialValue="" />
                                                <asp:RegularExpressionValidator runat="server"
                                                    ControlToValidate="TxtFechaNacimientoVPyME"
                                                    ErrorMessage="*"
                                                    ValidationGroup="Basica"
                                                    ID="RegularExpressionValidator4"
                                                    ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">Formato de Fecha AAAA-MM-DD</asp:RegularExpressionValidator>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender54"
                                                    runat="server"
                                                    TargetControlID="TxtFechaNacimientoVPyME"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789-" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="lblRFCPErsonaFisica" Width="250px" CssClass="td_label" Text="RFC: " runat="server" />
                                                <br />
                                                <asp:TextBox ID="TxtRFCFisicaVPyME" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="15" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtRFCFisicaVPyME"
                                                    ErrorMessage="Se debe capturar el RFC"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator20"
                                                    InitialValue="" />
                                                <asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator21"
                                                    runat="server"
                                                    ControlToValidate="TxtRFCFisicaVPyME"
                                                    ErrorMessage="Formato Incorrecto de RFC (AAAA999999XXX)"
                                                    Display="Dynamic"
                                                    ValidationGroup="Basica"
                                                    ValidationExpression="^(([A-Z]|[a-z]){4})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))">Formato Incorrecto de RFC (AAAA999999XXX) </asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div id="divRFCMoral" runat="server" visible="False">
                                    <table style="width: 100%">
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="Label59" Width="250px" CssClass="td_label" Text="Razón Social: " runat="server" />
                                                <asp:TextBox ID="TxtRazonSocialVPyME" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="255" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtRazonSocialVPyME"
                                                    ErrorMessage="Se debe capturar la Razón Social"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator91"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender55"
                                                    runat="server"
                                                    TargetControlID="TxtRazonSocialVPyME"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="Label60" Width="250px" CssClass="td_label" Text="Fecha de Registro: " runat="server" />
                                                <asp:TextBox ID="TxtFechaConstitucionVPyME" runat="server" Width="230px" MaxLength="10" />
                                                <img id="Img3" onclick="muestra_calendario('TxtFechaConstitucionVPyME')" runat="server" src="~/CentralModule/images/Calendario.bmp" style="width: 17px; height: 17px" alt="" />
                                                &nbsp;</img>&nbsp;</img>&nbsp;</img><asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtFechaConstitucionVPyME"
                                                    ErrorMessage="Se debe capturar la Fecha de Registro"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator92"
                                                    InitialValue="" />
                                                <asp:RegularExpressionValidator runat="server"
                                                    ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px"
                                                    ControlToValidate="TxtFechaConstitucionVPyME"
                                                    ErrorMessage="*"
                                                    ValidationGroup="Basica"
                                                    ID="RegularExpressionValidator20">
                                                    Formato de Fecha AAAA-MM-DD 
                                                </asp:RegularExpressionValidator>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender56"
                                                    runat="server"
                                                    TargetControlID="TxtFechaConstitucionVPyME"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789-" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="lblRFCPErsonaMoral" Width="250px" CssClass="td_label" Text="RFC: " runat="server" Style="margin-left: 0" />
                                                <br />
                                                <asp:TextBox ID="TxtRFCMoralVPyME" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="12" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtRFCMoralVPyME"
                                                    ErrorMessage="Se debe capturar el RFC"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator155"
                                                    InitialValue="" />
                                                <asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator3"
                                                    runat="server"
                                                    ControlToValidate="TxtRFCMoralVPyME"
                                                    ErrorMessage="Formato Incorrecto de RFC (AAA999999XXX)"
                                                    Display="Dynamic"
                                                    ValidationGroup="Basica"
                                                    ValidationExpression="^(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))">
                                                    Formato Incorrecto de RFC (AAA999999XXX)
                                                </asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table style="width: 100%">
                                    <tr>
                                        <td class="auto-style2" style="vertical-align: top;" colspan="3">
                                            <asp:Label ID="lblTipoTarifa" Width="250px" CssClass="td_label" Text="Giro de la Empresa: " runat="server" />
                                            <asp:DropDownList ID="DDXGiroEmpresa" runat="server" Width="750px"  />
                                            <asp:RequiredFieldValidator
                                                runat="server"
                                                ControlToValidate="DDXGiroEmpresa"
                                                ErrorMessage="Se debe seleccionar el Giro de la Empresa"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFVDDXGiroEmpresa"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td />
                                        <td />
                                    </tr>
                                        <tr>
                                            <td colspan="3" style="height: 4px"><br/></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="lblNom" runat="server" CssClass="td_label" Text="Nombre Comercial: " Width="250px"></asp:Label>
                                                <asp:TextBox ID="TxtNombreComercial" runat="server" CssClass="TextBoxMayusculas" Width="250px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFVTxtNombreComercial" runat="server" ControlToValidate="TxtNombreComercial" Display="Dynamic" ErrorMessage="Se debe capturar el Nombre Comercial" Text="*" ValidationGroup="Basica"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="Label12" runat="server" CssClass="td_label" Text="Sector: " Width="250px"></asp:Label>
                                                <asp:DropDownList ID="DDXSector" runat="server"  Width="250px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVDDXSector" runat="server" ControlToValidate="DDXSector" Display="Dynamic" ErrorMessage="Se debe seleccionar el Sector Ecónomico" InitialValue="Seleccione" Text="*" ValidationGroup="Basica"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="Label13" runat="server" CssClass="td_label" Text="Número de Empleados: " Width="250px"></asp:Label>
                                                <asp:TextBox ID="TxtNoEmpleados" runat="server" MaxLength="4" Width="250px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFVTxtNoEmpleados" runat="server" ControlToValidate="TxtNoEmpleados" Display="Dynamic" ErrorMessage="Se debe capturar el Número de Empleados" Text="*" ValidationGroup="Basica"></asp:RequiredFieldValidator>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True" TargetControlID="TxtNoEmpleados" ValidChars="0123456789">
                                                </asp:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 4px"><br/></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="Label14" runat="server" CssClass="td_label" Text="Total de Gastos Mensuales: " Width="250px"></asp:Label>
                                                <asp:TextBox ID="TxtGastosMensuales" runat="server" Width="250px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFVTxtGastosMensuales" runat="server" ControlToValidate="TxtGastosMensuales" Display="Dynamic" ErrorMessage="Se debe capturar los Gastos Mensuales" Text="*" ValidationGroup="Basica"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtGastosMensuales" Display="Dynamic" ErrorMessage="Solo Números" ValidationExpression="^[0-9]{1,18}(\.[0-9]{0,2})?$" ValidationGroup="Basica"></asp:RegularExpressionValidator>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender155" runat="server" Enabled="True" TargetControlID="TxtGastosMensuales" ValidChars="0123456789.">
                                                </asp:FilteredTextBoxExtender>
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">
                                                <asp:Label ID="Label15" runat="server" CssClass="td_label" Text="Promedio de Ventas Mensuales: " Width="250px"></asp:Label>
                                                <asp:TextBox ID="TxtVentasAnuales" runat="server" Width="250px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFVTxtVentasAnuales" runat="server" ControlToValidate="TxtVentasAnuales" Display="Dynamic" ErrorMessage="Se deben capturar las Ventas Anuales" Text="*" ValidationGroup="Basica"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtVentasAnuales" Display="Dynamic" ErrorMessage="Solo Números" ValidationExpression="^[0-9]{1,18}(\.[0-9]{0,2})?$" ValidationGroup="Basica"></asp:RegularExpressionValidator>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender145" runat="server" Enabled="True" TargetControlID="TxtVentasAnuales" ValidChars="0123456789.">
                                                </asp:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                </table>
                                <br />
                                <br />
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="3" style="color: #0099FF; font-weight: bold;">DATOS DEL DOMICILIO DEL NEGOCIO
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 4px"><br/></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2" style="vertical-align: top;">
                                            <asp:Label ID="Label16" Width="250px" CssClass="td_label" Text="Código Postal: " runat="server" />
                                            <asp:TextBox ID="TxtCP" runat="server" Width="250px" AutoPostBack="True" OnTextChanged="TxtCP_TextChanged" MaxLength="5" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtCP"
                                                ErrorMessage="Se deben capturar el Código Postal"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFVTxtCP"
                                                InitialValue="" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender35"
                                                runat="server"
                                                TargetControlID="TxtCP"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789" />
                                        </td>
                                        <td class="auto-style2" style="vertical-align: top;">
                                            <asp:Label ID="Label17" Width="250px" CssClass="td_label" Text="Estado: " runat="server" />
                                            <asp:DropDownList ID="DDXEstado" runat="server" AutoPostBack="True" Width="250px" OnSelectedIndexChanged="DDXEstado_SelectedIndexChanged" Font-Size="Small" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXEstado"
                                                ErrorMessage="Se deben seleccionar el Estado"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFVDDXEstado"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td class="auto-style2" style="vertical-align: top;">
                                            <asp:Label ID="Label18" Width="250px" CssClass="td_label" Text="Delegación o Municipio: " runat="server" />
                                            <asp:DropDownList ID="DDXMunicipio" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDXMunicipio_SelectedIndexChanged" Font-Size="Small" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXMunicipio"
                                                ErrorMessage="Se deben seleccionar el Municipio o Delegación"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFVDDXMunicipio"
                                                InitialValue="Seleccione" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 4px"><br/></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2" style="vertical-align: top;">
                                            <asp:Label ID="Label19" Width="250px" CssClass="td_label" Text="Colonia: " runat="server" />
                                            <asp:DropDownList ID="DDXColonia" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDXColonia_SelectedIndexChanged" Font-Size="Small" />
                                            <asp:DropDownList ID="DDXColoniaHidden" runat="server" Visible="False" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXColonia"
                                                ErrorMessage="Se debe seleccionar la Colonia"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFVDDXColonia"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td>
                                            <br />
                                        </td>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:ValidationSummary ID="Pyme" runat="server" CssClass="failureNotification"
                                                ValidationGroup="Basica" Font-Size="Small" HeaderText="Resumén Captura:" />
                                        </td>
                                    </tr>
                                </table>

                                <div id="divEditar" runat="server">
                                    <table align="center" runat="server">
                                        <tr>
                                            <td>
                                                <asp:Button runat="server" ID="btnEditarValidacionPyme" Text="Editar" OnClientClick="return confirm('Los datos de la generación de presupuesto se borrarán si se modifica la dirección actual. ¿Esta seguro de Editar los datos?');" OnClick="btnEditarValidacionPyme_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </asp:WizardStep>

                    <asp:WizardStep runat="server" ID="wizPresupuestoInversion" StepType="Step">
                        <div>
                            <fieldset style="width: 95%">
                                <legend style="font-size: 14px; align-content: initial">
                                    <asp:Image ID="imgPresupuesto" runat="server" ImageUrl="~/SupplierModule/images/t_presupuesto2.png" />
                                </legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="3">
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="3">Número de Servicio RPU:
                                            <asp:TextBox ID="TxtRPUPresupuesto" runat="server" Width="164px" ReadOnly="True" Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="width: 90%">
                                                <uc2:wuAltaBajaEquipos ID="wuAltaBajaEquipos1" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                        </div>
                    </asp:WizardStep>


                    <asp:WizardStep runat="server" ID="wizCapturaBasica" StepType="Step">
                        <fieldset style="width: 99%">
                            <legend style="font-size: 14px; align-content: initial">
                                <asp:Image runat="server" ImageUrl="images/t_alta.png" ID="Image9" /></legend>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="3">Número de Servicio RPU:
                                            <asp:TextBox ID="TxtNumeroServicioRPUBasica" runat="server" Width="164px" ReadOnly="True" Enabled="False" />
                                    </td>
                                    <td colspan="3">Número Crédito:
                                            <asp:TextBox ID="txtNumeroCreditoBasica" runat="server" Width="164px" ReadOnly="True" Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </fieldset>
                        <div runat="server" id="PanelPersonaFisica" visible="True">
                            <fieldset style="width: 100%">
                                <legend style="font-size: 14px; align-content: initial">DATOS DEL CLIENTE</legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="vertical-align: top;" class="auto-style1">
                                            <asp:Label ID="Label7" CssClass="td_label" Text="Tipo de Persona: " runat="server" />
                                            <asp:Label ID="lblFisica" CssClass="td_label" Text="   FÍSICA" runat="server" />
                                        </td>
                                        <td style="vertical-align: top;" class="auto-style1" />
                                        <td style="vertical-align: top;" class="auto-style1" />
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" class="auto-style1">
                                            <asp:Label ID="Label8" Width="250px" CssClass="td_label" Text="Nombre: " runat="server" />
                                        </td>
                                        <td style="vertical-align: top;" class="auto-style2">
                                            <asp:Label ID="Label9" Width="250px" CssClass="td_label" Text="Apellido Paterno: " runat="server" />
                                        </td>
                                        <td style="vertical-align: top;" class="auto-style2">
                                            <asp:Label ID="Label10" Width="250px" CssClass="td_label" Text="Apellido Materno: " runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" class="auto-style2">
                                            <asp:TextBox CssClass="TextBoxMayusculas" ID="TxtNombrePFisicaCBas" runat="server" Width="250px" MaxLength="100" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtNombrePFisicaCBas"
                                                ErrorMessage="Se debe capturar el Nombre"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFV1"
                                                InitialValue="" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6"
                                                runat="server"
                                                TargetControlID="TxtNombrePFisicaCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtApellidoPaternoCBas" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="50" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtApellidoPaternoCBas"
                                                ErrorMessage="Se debe capturar el Apellido Paterno"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFV2"
                                                InitialValue="" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7"
                                                runat="server"
                                                TargetControlID="TxtApellidoPaternoCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtApellidoMaternoCBas" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="50" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8"
                                                runat="server"
                                                TargetControlID="TxtApellidoMaternoCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label20" Width="250px" CssClass="td_label" Text="Sexo: " runat="server" />
                                            <asp:DropDownList ID="DDXSexoCBas" runat="server" Width="250px" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXSexoCBas"
                                                ErrorMessage="Se debe seleccionar el Sexo"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFV3"
                                                InitialValue="Seleccione" />

                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaNcimiento" Width="250px" CssClass="td_label" Text="Fecha Nacimiento: " runat="server" />
                                            <asp:TextBox ID="TxtFechaNacimietoCBas" runat="server" Width="230px" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtFechaNacimietoCBas"
                                                ErrorMessage="Se debe capturar la Fecha de Nacimiento"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator21"
                                                InitialValue="" />
                                            <asp:RegularExpressionValidator runat="server"
                                                ControlToValidate="TxtFechaNacimietoCBas"
                                                ErrorMessage="*"
                                                ValidationGroup="Basica"
                                                ID="RegularExpressionValidator6"
                                                ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                                                Formato de Fecha AAAA-MM-DD
                                            </asp:RegularExpressionValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                                                runat="server"
                                                TargetControlID="TxtFechaNacimietoCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789-" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label21" Width="250px" CssClass="td_label" Text="RFC: " runat="server" />
                                            <asp:TextBox ID="TxtRFCFisicaCBas" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="15" Enabled="False" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtRFCFisicaCBas"
                                                ErrorMessage="Se debe capturar el RFC"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="rqrtxtFisicarfcbasica"
                                                InitialValue="" />
                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator5"
                                                runat="server"
                                                ControlToValidate="TxtRFCFisicaCBas"
                                                ErrorMessage="Formato Incorrecto de RFC (AAAA999999XXX)"
                                                Display="Dynamic"
                                                ValidationGroup="Basica"
                                                ValidationExpression="^(([A-Z]|[a-z]){4})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))">
                                                Formato Incorrecto de RFC (AAAA999999XXX) 
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label22" Width="250px" CssClass="td_label" Text="CURP " runat="server" />
                                            <asp:TextBox ID="TxtCURPCBas" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="18" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtCURPCBas"
                                                ErrorMessage="Se debe capturar la CURP"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFV6"
                                                InitialValue="" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9"
                                                runat="server"
                                                TargetControlID="TxtCURPCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ0123456789" />
                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator7"
                                                runat="server"
                                                ControlToValidate="TxtCURPCBas"
                                                ErrorMessage="Formato Incorrecto de CURP"
                                                Display="Dynamic"
                                                ValidationGroup="Basica"
                                                ValidationExpression="^[a-zA-Z]{4}\d{6}[a-zA-Z]{6}\d{2}$">
                                                Formato Incorrecto de CURP(AAAA999999AAAAAA99)
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label23" Width="250px" CssClass="td_label" Text="Estado Civil: " runat="server" />
                                            <asp:DropDownList ID="RBEstadoCivilCBas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RBEstadoCivilCBas_SelectedIndexChanged" Height="16px" Width="250px" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label24" Width="250px" Text="Regimen Matrimonial: " runat="server" />
                                            <asp:DropDownList ID="DDXRegimenMatrimonialCBas" runat="server" Width="250px" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXRegimenMatrimonialCBas"
                                                ErrorMessage="Se debe seleccionar el Regimen Matrimonial"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFV7"
                                                InitialValue="Seleccione" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label25" Width="250px" CssClass="td_label" Text="Acredita Ocupación del Negocio: " runat="server" />
                                            <asp:DropDownList ID="DDXOcupacionCBas" runat="server" Width="250px" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXOcupacionCBas"
                                                ErrorMessage="Se debe seleccionar si acredita Ocupación del Negocio"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFV8"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label27" Width="250px" CssClass="td_label" Text="Tipo de Identificación: " runat="server" />
                                            <asp:DropDownList ID="DDXTipoIdentificaFisicaCBas" runat="server" Width="250px" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXTipoIdentificaFisicaCBas"
                                                ErrorMessage="Se debe seleccionar el Tipo de Identificación"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFV9"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label28" Width="250px" CssClass="td_label" Text="Número de Identificación: " runat="server" />
                                            <asp:TextBox ID="TxtNroIdentificacionCBas" runat="server" Width="250px" MaxLength="20" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtNroIdentificacionCBas"
                                                ErrorMessage="Se debe capturar el Número de Identificación"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFV10"
                                                InitialValue="" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10"
                                                runat="server"
                                                TargetControlID="TxtNroIdentificacionCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label29" Width="250px" CssClass="td_label" Text="Email: " runat="server" />
                                            <asp:TextBox ID="TxtEmailCBas" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="50" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtEmailCBas"
                                                ErrorMessage="Se debe capturar el E-mail"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RFV11"
                                                InitialValue="" />
                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator8"
                                                runat="server"
                                                ControlToValidate="TxtEmailCBas"
                                                ErrorMessage="Formato Incorrecto de E-mail XXX@XXX.XXX"
                                                Display="Dynamic"
                                                ValidationGroup="Basica"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                Formato Incorrecto de E-mail (XXX@XXX.XXX)
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <br />
                                        </td>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>

                        <div runat="server" id="PanelPersonaMoral" visible="False">
                            <fieldset style="width: 100%">
                                <legend style="font-size: 14px; align-content: initial">DATOS DEL CLIENTE</legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="vertical-align: top;" class="auto-style1">
                                            <asp:Label ID="LblTipoPersonaF" CssClass="td_label" Text="Tipo de Persona: " runat="server" />
                                            <asp:Label ID="lblMoral" CssClass="td_label" Text="  MORAL " runat="server" />
                                        </td>
                                        <td style="width: 290px; vertical-align: top;" />
                                        <td style="width: 290px; vertical-align: top;" />
                                    </tr>
                                    <tr>
                                        <td style="width: 290px; vertical-align: top;">
                                            <asp:Label ID="Label30" Width="250px" CssClass="td_label" Text="Razón Social: " runat="server" />
                                            <asp:TextBox ID="TxtRazonSocialCBas" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="255" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtRazonSocialCBas"
                                                ErrorMessage="Se debe capturar la Razón Social"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="rqrtxtrazonsolicalcbas"
                                                InitialValue="" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11"
                                                runat="server"
                                                TargetControlID="TxtRazonSocialCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " />
                                        </td>
                                        <td style="width: 250px; vertical-align: top;">
                                            <asp:Label ID="Label453" Width="250px" CssClass="td_label" Text="Fecha de Registro: " runat="server" />
                                            <asp:TextBox ID="TxtFechaConstitucionCBas" runat="server" Width="250px" MaxLength="12" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtFechaConstitucionCBas"
                                                ErrorMessage="Se debe capturar la Fecha de Registro"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator4"
                                                InitialValue="" />
                                            <asp:RegularExpressionValidator runat="server"
                                                ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px"
                                                ControlToValidate="TxtFechaConstitucionCBas" ErrorMessage="*" ValidationGroup="Basica"
                                                ID="revTxtFechaCons">
                                                Formato de Fecha AAAA-MM-DD
                                            </asp:RegularExpressionValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5"
                                                runat="server"
                                                TargetControlID="TxtFechaConstitucionCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789-" />
                                        </td>
                                        <td style="width: 290px; vertical-align: top;">
                                            <asp:Label ID="Label31" Width="250px" CssClass="td_label" Text="RFC: " runat="server" />
                                            <asp:TextBox ID="TxtRFCMoralCBas" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="14" Enabled="False" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                                                runat="server"
                                                TargetControlID="TxtRFCMoralCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZÑ0123456789" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtRFCMoralCBas"
                                                ErrorMessage="Se debe capturar el RFC"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator5"
                                                InitialValue="" />
                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator9"
                                                runat="server"
                                                ControlToValidate="TxtRFCMoralCBas"
                                                ErrorMessage="Formato Incorrecto de RFC (AAAA999999XXX)"
                                                Display="Dynamic"
                                                ValidationGroup="Basica"
                                                ValidationExpression="^(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))">
                                                Formato Incorrecto de RFC (AAAA999999XXX) 
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label32" Width="250px" CssClass="td_label" Text="Tipo Identificación: " runat="server" />
                                            <asp:DropDownList ID="DDXTipoIdentificaMoralCBas" runat="server" Width="250px" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXTipoIdentificaMoralCBas"
                                                ErrorMessage="Se debe seleccionar el Tipo de Identificación"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator6"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label33" Width="250px" CssClass="td_label" Text="Número Identificación: " runat="server" />
                                            <asp:TextBox ID="TxtNumeroIdentMoralCBas" runat="server" Width="250px" MaxLength="20" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtNumeroIdentMoralCBas"
                                                ErrorMessage="Se debe capturar el Número de Identificación"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator7"
                                                InitialValue="" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12"
                                                runat="server"
                                                TargetControlID="TxtNumeroIdentMoralCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label34" Width="250px" CssClass="td_label" Text="E-mail: " runat="server" />
                                            <asp:TextBox ID="TxtEmailMoralCBas" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="50" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtEmailMoralCBas"
                                                ErrorMessage="Se debe capturar el E-mail"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator8"
                                                InitialValue="" />
                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator10"
                                                runat="server"
                                                ControlToValidate="TxtEmailMoralCBas"
                                                ErrorMessage="Formato Incorrecto de E-mail"
                                                Display="Dynamic"
                                                ValidationGroup="Basica"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                Formato Incorrecto de E-mail (XXX@XXX.XXX)
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label35" Width="250px" CssClass="td_label" Text="Acredita Ocupación del negocio: " runat="server" />
                                            <asp:DropDownList ID="DDXAcreditaCBas" runat="server" Width="250px" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXAcreditaCBas"
                                                ErrorMessage="Se debe seleccionar si Acredita Ocupación del Negocio"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator9"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td>
                                            <br />
                                        </td>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <br />
                        <fieldset style="width: 100%">
                            <legend style="font-size: 14px; align-content: initial">DATOS DEL NEGOCIO</legend>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 290px; vertical-align: top;">
                                        <asp:Label ID="Label36" Width="250px" CssClass="td_label" Text="Código Postal: " runat="server" />
                                        <asp:TextBox ID="TxtCPCBas" runat="server" Width="250px" Enabled="False" MaxLength="5" />
                                    </td>
                                    <td style="width: 290px; vertical-align: top;">
                                        <asp:Label ID="Label37" Width="250px" CssClass="td_label" Text="Estado: " runat="server" />
                                        <asp:DropDownList ID="DDXEstadoCBas" runat="server" Width="250px" Enabled="False" />
                                    </td>
                                    <td style="width: 290px; vertical-align: top;">
                                        <asp:Label ID="Label38" Width="250px" CssClass="td_label" Text="Delegación o Municipio: " runat="server" />
                                        <asp:DropDownList ID="DDXMunicipioCBas" runat="server" Width="250px" Enabled="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label39" Width="250px" CssClass="td_label" Text="Colonia: " runat="server" />
                                        <asp:DropDownList ID="DDXColoniaCBas" runat="server" Width="250px" Enabled="False" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label40" Width="250px" CssClass="td_label" Text="Calle: " runat="server" />
                                        <asp:TextBox ID="TxtCalleCBas" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="50" />
                                        <asp:RequiredFieldValidator runat="server"
                                            ControlToValidate="TxtCalleCBas"
                                            ErrorMessage="Se debe capturar la Calle del Domicilio del Negocio"
                                            ValidationGroup="Basica"
                                            Display="Dynamic"
                                            Text="*"
                                            EnableClientScript="true"
                                            ID="RequiredFieldValidator10"
                                            InitialValue="" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label41" Width="250px" CssClass="td_label" Text="Teléfono: " runat="server" />
                                        <asp:TextBox ID="TxtTelefonoCBas" runat="server" Width="250px" MaxLength="10" />
                                        <asp:RequiredFieldValidator runat="server"
                                            ControlToValidate="TxtTelefonoCBas"
                                            ErrorMessage="Se debe capturar el Teléfono del Domicilio del Negocio"
                                            ValidationGroup="Basica"
                                            Display="Dynamic"
                                            Text="*"
                                            EnableClientScript="true"
                                            ID="RequiredFieldValidator11"
                                            InitialValue="" />
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                            runat="server"
                                            TargetControlID="TxtTelefonoCBas"
                                            FilterMode="ValidChars"
                                            ValidChars="0123456789" />
                                        <asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator160"
                                            runat="server"
                                            ControlToValidate="TxtTelefonoCBas"
                                            ErrorMessage="Número de Teléfono de 10 dígitos"
                                            Display="Dynamic"
                                            ValidationGroup="Basica"
                                            ValidationExpression="[0-9]{10}">
                                            Número de Teléfono de 10 dígitos    
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label42" Width="250px" CssClass="td_label" Text="Número Exterior: " runat="server" />
                                        <asp:TextBox ID="TxtNumeroExteriorCBas" runat="server" Width="250px" MaxLength="50" />
                                        <asp:RequiredFieldValidator runat="server"
                                            ControlToValidate="TxtNumeroExteriorCBas"
                                            ErrorMessage="Se debe capturar el Número Exterior del Domicilio del Negocio"
                                            ValidationGroup="Basica"
                                            Display="Dynamic"
                                            Text="*"
                                            EnableClientScript="true"
                                            ID="RequiredFieldValidator12"
                                            InitialValue="" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblnumInterior" Width="250px" CssClass="td_label" Text="Número Interior: " runat="server" />
                                        <asp:TextBox ID="TxtNumeroInteriorCBas" runat="server" Width="250px" MaxLength="50" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label43" Width="250px" CssClass="td_label" Text="Tipo de Propiedad: " runat="server" />
                                        <asp:DropDownList ID="DDXTipoPropiedadCBas" runat="server" Width="250px" />
                                        <asp:RequiredFieldValidator runat="server"
                                            ControlToValidate="DDXTipoPropiedadCBas"
                                            ErrorMessage="Se debe seleccionar el Tipo de Propiedad del Domicilio del Negocio"
                                            ValidationGroup="Basica"
                                            Display="Dynamic"
                                            Text="*"
                                            EnableClientScript="true"
                                            ID="RequiredFieldValidator13"
                                            InitialValue="Seleccione" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label63" Width="250px" CssClass="td_label" Text="Referencia del Domicilio: " runat="server" />
                                        <asp:TextBox ID="txtReferenciaDomNegocioMoral" runat="server" Width="354px" MaxLength="250" TextMode="MultiLine" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset id="FieldsetDatosFiscal" runat="server" style="width: 100%">
                            <legend style="font-size: 14px; align-content: initial">DOMICILIO FISCAL</legend>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <asp:Label ID="Label44" Width="417px" CssClass="td_label" Text="El Domicilio Fiscal es el mismo que el Domicilio del Negocio " runat="server" />
                                        <asp:DropDownList runat="server" ID="ddxMismoDom_Fiscal_Negocio" AutoPostBack="True" OnSelectedIndexChanged="ddxMismoDom_Fiscal_Negocio_SelectedIndexChanged" />
                                        <asp:RequiredFieldValidator runat="server"
                                            ControlToValidate="ddxMismoDom_Fiscal_Negocio"
                                            ErrorMessage="Se debe seleccionar El Domicilio Fiscal es el mismo que el Domicilio del Negocio"
                                            ValidationGroup="Basica"
                                            Display="Dynamic"
                                            Text="*"
                                            EnableClientScript="true"
                                            ID="RequiredFieldValidator157"
                                            InitialValue="Seleccione" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Panel ID="panelMismoDom_Fiscal_Negocio" runat="server">
                                <table>
                                    <tr>
                                        <td style="width: 290px; vertical-align: top;">
                                            <asp:Label ID="Label45" Width="250px" CssClass="td_label" Text="Código Postal: " runat="server" />
                                            <asp:TextBox ID="TxtCPFiscalCBas" runat="server" Width="250px" MaxLength="5" AutoPostBack="True" OnTextChanged="TxtCPFiscalCBas_TextChanged" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtCPFiscalCBas"
                                                ErrorMessage="Se debe capturar el Código Postal del Domicilio Fiscal"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator14"
                                                InitialValue="" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14"
                                                runat="server"
                                                TargetControlID="TxtCPFiscalCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789" />
                                        </td>
                                        <td style="width: 290px; vertical-align: top;">
                                            <asp:Label ID="Label46" Width="250px" CssClass="td_label" Text="Estado: " runat="server" />
                                            <asp:DropDownList ID="DDXEstadoFiscalCBas" runat="server" Width="250px" OnSelectedIndexChanged="DDXEstadoFiscalCBas_SelectedIndexChanged" AutoPostBack="True" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXEstadoFiscalCBas"
                                                ErrorMessage="Se debe seleccionar el Estado del Domicilio Fiscal"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator16"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td style="width: 290px; vertical-align: top;">
                                            <asp:Label ID="Label47" Width="250px" CssClass="td_label" Text="Delegación o Municipio: " runat="server" />
                                            <asp:DropDownList ID="DDXMunicipioFiscalCBas" Width="250px" runat="server" OnSelectedIndexChanged="DDXMunicipioFiscalCBas_SelectedIndexChanged" AutoPostBack="True" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXMunicipioFiscalCBas"
                                                ErrorMessage="Se debe seleccionar el Municipio o Delegación del Domicilio Fiscal"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator17"
                                                InitialValue="Seleccione" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label48" Width="250px" CssClass="td_label" Text="Colonia: " runat="server" />
                                            <asp:DropDownList ID="DDXColoniaFiscalCBas" Width="250px" runat="server" OnSelectedIndexChanged="DDXColoniaFiscalCBas_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            <asp:DropDownList ID="DDXColoniaFiscalHiddenCBas" Width="250px" runat="server" Visible="False" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXColoniaFiscalCBas"
                                                ErrorMessage="Se debe seleccionar la Colonia del Domicilio Fiscal"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator18"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label49" Width="250px" CssClass="td_label" Text="Calle: " runat="server" />
                                            <asp:TextBox ID="TxtCalleFiscalCBas" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="50" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtCalleFiscalCBas"
                                                ErrorMessage="Se debe capturar la Calle del Domicilio Fiscal"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator19"
                                                InitialValue="" />

                                        </td>
                                        <td>
                                            <asp:Label ID="Label50" Width="250px" CssClass="td_label" Text="Teléfono: " runat="server" />
                                            <asp:TextBox ID="TxttelefonoFiscalCBas" runat="server" Width="250px" MaxLength="10" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxttelefonoFiscalCBas"
                                                ErrorMessage="Se debe capturar el Teléfono del Domicilio Fiscal"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator22"
                                                InitialValue="" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15"
                                                runat="server"
                                                TargetControlID="TxttelefonoFiscalCBas"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789" />
                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator22"
                                                runat="server"
                                                ControlToValidate="TxtTelefonoCBas"
                                                ErrorMessage="Formato Incorrecto Teléfono del Domicilio Fiscal"
                                                Display="Dynamic"
                                                ValidationGroup="Basica"
                                                ValidationExpression="[0-9]{10}">
                                                Número de Teléfono de 10 dígitos 
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label51" Width="250px" CssClass="td_label" Text="Número Exterior: " runat="server" />
                                            <asp:TextBox ID="TxtExteriorFiscalCBas" runat="server" Width="250px" MaxLength="50" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtExteriorFiscalCBas"
                                                ErrorMessage="Se debe capturar el Número Exterior del Domicilio Fiscal"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator23"
                                                InitialValue="" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label11" Width="250px" CssClass="td_label" Text="Número Interior: " runat="server" />
                                            <asp:TextBox ID="TxtInteriorFiscalCBas" runat="server" Width="250px" MaxLength="50" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label52" Width="250px" CssClass="td_label" Text="Tipo de Propiedad: " runat="server" />
                                            <asp:DropDownList ID="DDXTipoPropiedadFiscalCBas" runat="server" Width="250px" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXTipoPropiedadFiscalCBas"
                                                ErrorMessage="Se debe seleccionar el Tipo de Propiedad del Domicilio Fiscal"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator24"
                                                InitialValue="Seleccione" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblReferencia" Width="250px" CssClass="td_label" Text="Referencia del Domicilio: " runat="server" />
                                            <asp:TextBox ID="txtReferenciaDomFiscalMoral" runat="server" Width="400px" MaxLength="250" TextMode="MultiLine" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </fieldset>
                        <p>
                            <asp:ValidationSummary ID="Basica" runat="server" CssClass="failureNotification"
                                ValidationGroup="Basica" Font-Size="Small" HeaderText="Resumén Captura:" />
                            <p style="text-align: center; width: 873px">
                                <asp:Button ID="BtnGuardaTermporal" runat="server" Text="Guardar" OnClick="BtnGuardaTermporal_Click" />
                            </p>
                    </asp:WizardStep>

                    <asp:WizardStep runat="server" ID="wizValidaHistorialCrediticio" StepType="Step">
                        <div id="divForm" runat="server" style="align-content: center">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                       <%-- <asp:Image ID="imghist" runat="server" ImageUrl="~/SupplierModule/images/t_validar_servicio.png" />--%>
                                        <asp:Image ID="imghist" runat="server" ImageUrl="~/SupplierModule/images/t_alta.png"/>
                                    </td>
                                    <td  align="right">
                                        <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label" ForeColor="#333333" />
                                        <asp:TextBox ID="txtFecha" runat="server" Enabled="false" BorderWidth="0" Font-Size="11pt" />
                                    </td>
                                </tr>
                            </table>
                            <br/>
                            <br/>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 180px; height: 25px">
                                        <asp:Label ID="lblra" runat="server" Text="Nombre o Razón Social" CssClass="Label"
                                            Width="165px" ForeColor="#666666" Font-Size="11pt" />

                                    </td>
                                    <td style="width: 200px; height: 25px;">
                                        <asp:TextBox ID="txtRazonSocial" CssClass="TextBoxMayusculas" runat="server" Font-Size="11px"
                                            Enabled="False" Width="200px" />
                                    </td>
                                    <td style="width: 115px; height: 25px;">
                                        <div>
                                        </div>
                                    </td>
                                    <td style="width: 150px; height: 25px;">
                                        <asp:Label ID="Label1" runat="server" Text="Número Crédito" CssClass="Label" Width="150px"
                                            ForeColor="#666666" Font-Size="11pt" />
                                    </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtCreditoNumHistorialCred" runat="server" Font-Size="11px" CssClass="TextBox"
                                            Enabled="False" Width="200px" />

                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px" />
                                    <td style="width: 200px" />
                                    <td style="width: 115px" />
                                    <td style="width: 150px" />
                                    <td></td>
                                </tr>
                            </table>
                            <br/>
                            <br/>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 670px">
                                        <asp:Image ID="imgs" runat="server" ImageUrl="~/SupplierModule/images/t6.png" />
                                    </td>
                                    <td>
                                        <img id="Img1" alt="" runat="server" src="~/SupplierModule/images/t12.png" width="166" height="21" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Monto a Financiar (M.N.)" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label4" />
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtRequestAmount" />
                                    </td>
                                    <td style="width: 100px" />
                                    <td>
                                        <asp:Button runat="server" Text="Formato Autorización" CssClass="Button" Width="200px"
                                            ID="btnDisplayCreditRequest" OnClick="btnDisplayCreditRequest_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 180px">
                                        <asp:Label runat="server" Text="Número de Pagos" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="lblpagos" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtCreditYearsNumber" />
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" Text="Tabla de Amortización" CssClass="Button" Width="200px"
                                            ID="btnDisplayPaymentSchedule" OnClick="btnDisplayPaymentSchedule_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Periodicidad de Pago" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label6" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtPaymentPeriod" />
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" Text="Carta Presupuesto" CssClass="Button" Width="200px"
                                            ID="btnDisplayQuota" OnClick="btnDisplayQuota_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="Div1" runat="server" style="display: none">
                                            <asp:Label runat="server" Text="Importe Contrato" CssClass="Label" Font-Size="11pt"
                                                ForeColor="#666666" Width="150px" ID="lblImporte" />
                                        </div>
                                    </td>
                                    <td>
                                        <div id="Div2" runat="server" style="display: none">
                                            <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtContratoImporte" />
                                        </div>
                                    </td>
                                    <td style="width: 267px">
                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]*[1-9][0-9]*$"
                                            ControlToValidate="txtContratoImporte" ErrorMessage="(*) Campo Obligatorio" ID="revContratoImporte" />

                                    </td>
                                   <%-- <td>
                                        <asp:Button runat="server" Text="Solicitud" CssClass="Button" Width="200px" ID="btnDisplayRequest"
                                            OnClick="btnDisplayRequest_Click" />
                                    </td>--%>
                                </tr>
                            </table>
                            <asp:HiddenField runat="server" Value="mop" ID="hiddenfield" />
                            <table style="width: 100%">
                                <tr>
                                    <td width="150px">
                                        <asp:Button runat="server" Text="Consulta Crediticia" Width="150px" ID="btnConsultaCrediticia"
                                            OnClick="btnConsultaCrediticia_Click" OnClientClick="if (confirm('Confirma realizar Consulta Crediticia. NO podrá editar el crédito después de hacer la consulta crediticia')) {this.style.display ='none'; } else { return false; }" /><br />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMopInvalido" runat="server" Text='<%$ Resources:DefaultResource, MOPErrorInvalid %>' Visible="false"
                                            Style="font-size: x-large; color: #FF0000; font-weight: 700; background-color: #FFFF00" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:WizardStep>

                    <asp:WizardStep runat="server" ID="wizCapturaComplementaria" StepType="Step">
                        <div id="div3" style="align-content: center">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                      <asp:Image ID="Image2" runat="server" ImageUrl="~/SupplierModule/images/t_alta.png"/>
                                    </td>
                                </tr>
                                </table>
                            <fieldset class="login" style="border: thin solid #6699FF; width: 873px; border-color: #6699FF;">
                                <legend style="border-color: #3333FF; color: #0066FF">REGISTRO DE HORARIOS DE OPERACIÓN DEL NEGOCIO</legend>
                                <table style="width: 100%;">
                                    <tr class="trh">
                                        <td>Horarios de Operación</td>
                                        <td>Lunes</td>
                                        <td>Martes</td>
                                        <td>Miércoles</td>
                                        <td>Jueves</td>
                                        <td>Viernes</td>
                                        <td>Sábado</td>
                                        <td>Domingo</td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>Hora de inicio
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioLunes" runat="server" Width="7em" onChange="CalculaTotalHorasTrabajadas();" Font-Size="Small" />

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioMartes" runat="server" Width="7em" onChange="CalculaTotalHorasTrabajadas();" Font-Size="Small" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioMiercoles" runat="server" Width="7em" onChange="CalculaTotalHorasTrabajadas();" Font-Size="Small" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioJueves" runat="server" Width="7em" onChange="CalculaTotalHorasTrabajadas();" Font-Size="Small" />

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioViernes" runat="server" Width="7em" onChange="CalculaTotalHorasTrabajadas();" Font-Size="Small" />

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioSabado" runat="server" Width="7em" onChange="CalculaTotalHorasTrabajadas();" Font-Size="Small" />

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioDomingo" runat="server" Width="7em" onChange="CalculaTotalHorasTrabajadas();" Font-Size="Small" />

                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>Horas laborables
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor1Negocio" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadas();">
                                                <NumberFormat DecimalDigits="1" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor2Negocio" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadas();">
                                                <NumberFormat DecimalDigits="1" />
                                            </telerik:RadNumericTextBox>

                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor3Negocio" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadas();">
                                                <NumberFormat DecimalDigits="1" />
                                            </telerik:RadNumericTextBox>

                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor4Negocio" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadas();">
                                                <NumberFormat DecimalDigits="1" />
                                            </telerik:RadNumericTextBox>

                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor5Negocio" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadas();">
                                                <NumberFormat DecimalDigits="1" />
                                            </telerik:RadNumericTextBox>

                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor6Negocio" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadas();">
                                                <NumberFormat DecimalDigits="1" />
                                            </telerik:RadNumericTextBox>

                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor7Negocio" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadas();">
                                                <NumberFormat DecimalDigits="1" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="ok" runat="server" />
                                            Horas a la semana:
                                            <asp:TextBox ID="TxtHorasSemana" Width="110px" runat="server" Enabled="False" />
                                        </td>
                                        <td>Semanas al año:
                                            <telerik:RadNumericTextBox ID="noSemanasNegocio" Width="110px" runat="server" MaxLength="2" MaxValue="52" MinValue="1" Value="52" onChange="CalculaHorasAnio();">
                                                <NumberFormat DecimalDigits="0"/>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>Horas al año:
                                            <asp:TextBox ID="TxtHorasAnio" Width="110px" runat="server" Enabled="False" Text="0.0"/>
                                            <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtHorasAnio"
                                                    ErrorMessage="Se debe registrar Horarios de Operación"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="ReqHOras"
                                                    InitialValue="0.0" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
                            <fieldset class="login" style="border: thin solid #6699FF; width: 873px;">
                                <legend style="color: #0066FF">DATOS DEL REPRESENTANTE LEGAL</legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="3" style="text-align: center">El Representante Legal es el Mismo que el Cliente
                                            <asp:DropDownList runat="server" ID="ddxRepLegal" AutoPostBack="True" OnSelectedIndexChanged="ddxRepLegal_SelectedIndexChanged" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel runat="server" ID="PanelRepLegal">
                                    <table>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 290px; vertical-align: top;">Nombre(s):<br />
                                                <asp:TextBox ID="TxtNombreRepLegal" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNombreRepLegal"
                                                    ErrorMessage="Se debe capturar el Nombre (Representante Legal)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator25"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender17"
                                                    runat="server"
                                                    TargetControlID="TxtNombreRepLegal"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " />
                                            </td>
                                            <td>Apellido Paterno:<br />
                                                <asp:TextBox ID="TxtApPaternoRepLegal" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtApPaternoRepLegal"
                                                    ErrorMessage="Se debe capturar el Apellido Patermo (Representante Legal)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator43"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender27"
                                                    runat="server"
                                                    TargetControlID="TxtApPaternoRepLegal"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " />
                                            </td>
                                            <td>Apellido Materno:<br />
                                                <asp:TextBox ID="TxtApMaternoRepLegal" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtApMaternoRepLegal"
                                                    ErrorMessage="Se debe capturar el Apellido Matermo (Representante Legal)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator44"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender28"
                                                    runat="server"
                                                    TargetControlID="TxtApMaternoRepLegal"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 290px; vertical-align: top;">E-mail:
                                        <asp:TextBox ID="TxtEmailRepLegal" CssClass="TextBoxMayusculas" runat="server" Width="250px"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtEmailRepLegal"
                                                    ErrorMessage="Se debe capturar el E-mail (Representante Legal)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator26"
                                                    InitialValue="" />
                                                <br />
                                                <asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator11"
                                                    runat="server"
                                                    ControlToValidate="TxtEmailRepLegal"
                                                    ErrorMessage="Formato Incorrecto de E-mail"
                                                    Display="Dynamic"
                                                    ValidationGroup="Basica"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                    Formato Incorrecto de E-mail XXXX@XXX.XXX</asp:RegularExpressionValidator>

                                            </td>
                                            <td style="width: 290px; vertical-align: top;">Teléfono:
                                            <asp:TextBox ID="TxtTelefonoRepLegal" runat="server" Width="250px" MaxLength="10" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtTelefonoRepLegal"
                                                    ErrorMessage="Se debe capturar el Teléfono (Representante Legal)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator40"
                                                    InitialValue="">
                                                </asp:RequiredFieldValidator>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender18"
                                                    runat="server"
                                                    TargetControlID="TxtTelefonoRepLegal"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                <asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator23"
                                                    runat="server"
                                                    ControlToValidate="TxtTelefonoRepLegal"
                                                    ErrorMessage="Número de Teléfono de 10 dígitos"
                                                    Display="Dynamic"
                                                    ValidationGroup="Basica"
                                                    ValidationExpression="[0-9]{10}">Número de Teléfono de 10 dígitos</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </fieldset>
                            <br />                            
                            <asp:Panel runat="server" ID="PanelPoderRepLegal">
                            <fieldset class="login" style="border: thin solid #6699FF; width: 873px; border-color: #6699FF;">
                                <legend style="border-color: #3333FF; color: #0066FF">PODER NOTARIAL DEL REPRESENTANTE LEGAL</legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 290px; vertical-align: top;">Número de Escritura:
                                            <asp:TextBox ID="TxtNumeroEscrituraPNCliente" runat="server" Width="250px" MaxLength="10" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtNumeroEscrituraPNCliente"
                                                ErrorMessage="Se debe capturar el Número de Escritura (Poder Notarial - Rep. Legal)"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator59"
                                                InitialValue="" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender39"
                                                runat="server"
                                                TargetControlID="TxtNumeroEscrituraPNCliente"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789" />
                                        </td>
                                        <td style="width: 290px; vertical-align: top;">Fecha:<br />
                                            <asp:TextBox ID="TxtPNCliente" runat="server" Width="250px" />
                                            <img id="Img8" onclick="muestra_calendario('TxtPNCliente')" runat="server" src="~/CentralModule/images/Calendario.bmp" style="width: 17px; height: 17px" alt="" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtPNCliente"
                                                ErrorMessage="Se debe capturar la Fecha de Poder Notarial del Representante Legal"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator60"
                                                InitialValue="" />
                                            <asp:RegularExpressionValidator runat="server"
                                                ControlToValidate="TxtPNCliente"
                                                ErrorMessage="*"
                                                ValidationGroup="Basica"
                                                ID="RegularExpressionValidator18"
                                                ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                                                Formato de Fecha AAAA-MM-DD
                                            </asp:RegularExpressionValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender40"
                                                runat="server"
                                                TargetControlID="TxtPNCliente"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789-" />
                                        </td>
                                        <td style="width: 290px; vertical-align: top;">Nombre Completo del Notario:
                                            <asp:TextBox ID="TxtNomNotarioPNCliente" onkeypress="return soloLetras(event)" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="255" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtNomNotarioPNCliente"
                                                ErrorMessage="Se debe capturar el Nombre del Notario (Poder Notarial Rep. Legal)"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator61"
                                                InitialValue="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 290px; vertical-align: top;">Estado:
                                            <asp:DropDownList ID="DDXEstadoPNRL" runat="server" Width="252px" AutoPostBack="True" OnSelectedIndexChanged="DDXEstadoPNRL_SelectedIndexChanged" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXEstadoPNRL"
                                                ErrorMessage="Se debe seleccionar el Estado (Poder Notarial - Rep. Legal)"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator62"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td style="width: 290px; vertical-align: top;">Delegación o Municipio:
                                            <asp:DropDownList ID="DDXMunicipioPNRL" Width="252px" runat="server" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="DDXMunicipioPNRL"
                                                ErrorMessage="Se debe seleccionar el Municipio o Delegación (Poder Notarial - Rep. Legal)"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator63"
                                                InitialValue="Seleccione" />
                                        </td>
                                        <td style="width: 290px; vertical-align: top;">Número de Notaría:
                                            <asp:TextBox ID="TxtNotariaPNRL" runat="server" Width="250px" MaxLength="10" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TxtNotariaPNRL"
                                                ErrorMessage="Se debe capturar el Número de Notaria (Poder Notarial - Rep. Legal)"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator64"
                                                InitialValue="" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender42"
                                                runat="server"
                                                TargetControlID="TxtNotariaPNRL"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            </asp:Panel>
                            <br />
                            <asp:Panel runat="server" ID="PanelActaConstitutiva" Visible="False">
                                <fieldset class="login" style="border: thin solid #6699FF; width: 100%; border-color: #6699FF;">
                                    <legend style="border-color: #3333FF; color: #0066FF">ACTA CONSTITUTIVA</legend>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 290px; vertical-align: top;">Número de Escritura:
                                                <asp:TextBox ID="TxtNoEscrituraClienteAC" runat="server" Width="250px" MaxLength="10" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNoEscrituraClienteAC"
                                                    ErrorMessage="Se debe capturar el Número de Escritura (Acta Constitutiva)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator65"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender43"
                                                    runat="server"
                                                    TargetControlID="TxtNoEscrituraClienteAC"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                            </td>
                                            <td style="width: 290px; vertical-align: top;">Fecha:<br />
                                                <asp:TextBox ID="TxtFechaClienteAC" runat="server" Width="250px" />
                                                <img id="Img4" onclick="muestra_calendario('TxtFechaClienteAC')" runat="server" src="~/CentralModule/images/Calendario.bmp" style="width: 17px; height: 17px" alt="" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtFechaClienteAC"
                                                    ErrorMessage="Se debe capturar la Fecha del Acta Constitutiva"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator66"
                                                    InitialValue="" />
                                                <asp:RegularExpressionValidator runat="server"
                                                    ControlToValidate="TxtFechaClienteAC"
                                                    ErrorMessage="*"
                                                    ValidationGroup="Basica"
                                                    ID="RegularExpressionValidator19"
                                                    ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                                                    Formato de Fecha AAAA-MM-DD</asp:RegularExpressionValidator>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender44"
                                                    runat="server"
                                                    TargetControlID="TxtFechaClienteAC"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789-" />
                                            </td>
                                            <td style="width: 290px; vertical-align: top;">Nombre Completo del Notario:
                                                <asp:TextBox ID="TxtNomNotarioClienteAC" runat="server" Width="250px" onkeypress="return soloLetras(event)" CssClass="TextBoxMayusculas" MaxLength="255" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNomNotarioClienteAC"
                                                    ErrorMessage="Se debe capturar el Nombre del Notario (Acta Constitutiva)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator67"
                                                    InitialValue="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 290px; vertical-align: top;">Estado:
                                                <asp:DropDownList ID="DDXEstadoClienteAC" runat="server" Width="252px" AutoPostBack="True" OnSelectedIndexChanged="DDXEstadoClienteAC_SelectedIndexChanged" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="DDXEstadoClienteAC"
                                                    ErrorMessage="Se debe seleccionar el Estado (Acta Constitutiva)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator68"
                                                    InitialValue="Seleccione" />
                                            </td>
                                            <td style="width: 290px; vertical-align: top;">Delegación o Municipio:
                                                <asp:DropDownList ID="DDXMunicipipClienteAC" Width="252px" runat="server" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="DDXMunicipipClienteAC"
                                                    ErrorMessage="Se debe seleccionar el Municipio o Delegación Acta Constitutiva"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator69"
                                                    InitialValue="Seleccione" />
                                            </td>
                                            <td style="width: 290px; vertical-align: top;">Número de Notaría:
                                                <asp:TextBox ID="TxtNotariaClienteAC" runat="server" Width="250px" MaxLength="10" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNotariaClienteAC"
                                                    ErrorMessage="Se debe capturar el Número de Notaría (Acta Constitutiva)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator70"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender46"
                                                    runat="server"
                                                    TargetControlID="TxtNotariaClienteAC"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <br />
                            </asp:Panel>
                            <asp:Panel runat="server" ID="PanelActaMatrimonio">
                                <fieldset class="login" style="border: thin solid #6699FF; width: 873px; border-color: #6699FF;">
                                    <legend style="border-color: #3333FF; color: #0066FF">ACTA DE MATRIMONIO DEL CLIENTE</legend>
                                    <table style="width: 873px;">
                                        <tr>
                                            <td style="width: 290px; vertical-align: top;">Nombre Completo (Conyuge):
                                            <asp:TextBox runat="server" ID="txtNombreConyuge" onkeypress="return soloLetras(event)" CssClass="TextBoxMayusculas" Width="250" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="txtNombreConyuge"
                                                    ErrorMessage="Se debe capturar el Nombre del Conyuge"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator71"
                                                    InitialValue="" />
                                            </td>
                                            <td style="width: 290px; vertical-align: top;">Número de Acta de Matrimonio:
                                            <asp:TextBox ID="TxtNumeroActaMat" runat="server" Width="250px" MaxLength="20" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNumeroActaMat"
                                                    ErrorMessage="Se debe capturar el Número de Acta de Matrimonio"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator72"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender48"
                                                    runat="server"
                                                    TargetControlID="TxtNumeroActaMat"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                            </td>
                                            <td style="width: 290px; vertical-align: top;">Número de Registro Civil:
                                            <asp:TextBox ID="TxtRegistroCivil" runat="server" Width="250px" MaxLength="50" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtRegistroCivil"
                                                    ErrorMessage="Se debe capturar el Número de Registro Civil"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator73"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender49"
                                                    runat="server"
                                                    TargetControlID="TxtRegistroCivil"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <br />
                            </asp:Panel>
                            <fieldset class="login" style="border: thin solid #6699FF; width: 873px; border-color: #6699FF;">
                                <legend style="border-color: #3333FF; color: #0066FF">DATOS DEL OBLIGADO SOLIDARIO</legend>
                                <table style="width: 873px;">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:RadioButtonList ID="RBListTipoPersona" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RBListTipoPersona_SelectedIndexChanged">
                                                <asp:ListItem Value="1">Persona Física</asp:ListItem>
                                                <asp:ListItem Value="2">Persona Moral</asp:ListItem>
                                            </asp:RadioButtonList>
                                             <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="RBListTipoPersona"
                                                    ErrorMessage="Seleccione el Tipo de Persona del Obligado Solidario"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator76"
                                                    InitialValue="" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="PanelPersonaFisicaCComp" runat="server" Visible="False">
                                    <table style="width: 873px;">
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">Nombre:
                                                <asp:TextBox ID="TxtNombrePFOS" runat="server" CssClass="TextBoxMayusculas" Width="250px" MaxLength="100" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNombrePFOS"
                                                    ErrorMessage="Se debe capturar el Nombre (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator27"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender19"
                                                    runat="server"
                                                    TargetControlID="TxtNombrePFOS"
                                                    FilterMode="ValidChars"
                                                    ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ " />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Apellido Paterno:
                                                <asp:TextBox ID="TxtApPaternoOS" runat="server" CssClass="TextBoxMayusculas" Width="250px" MaxLength="50" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtApPaternoOS"
                                                    ErrorMessage="Se debe capturar el Apellido Paterno (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator28"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender20"
                                                    runat="server"
                                                    TargetControlID="TxtApPaternoOS"
                                                    FilterMode="ValidChars"
                                                    ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZÑ" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Apellido Materno:
                                                <asp:TextBox ID="TxtApMaternoOS" CssClass="TextBoxMayusculas" runat="server" Width="250px" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender22"
                                                    runat="server"
                                                    TargetControlID="TxtApMaternoOS"
                                                    FilterMode="ValidChars"
                                                    ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZÑ" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">Sexo:<br />
                                                <asp:DropDownList ID="DDXSexoCComp" runat="server" Width="250px" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="DDXSexoCComp"
                                                    ErrorMessage="Se debe seleccionar el Sexo (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator29"
                                                    InitialValue="Seleccione" /></td>
                                            <td>Fecha de Nacimiento:
                                                <asp:TextBox ID="TxtFechaNacimietoOS" runat="server" Width="250px" />
                                                <img id="Img5" onclick="muestra_calendarioValidEdad('TxtFechaNacimietoOS')" runat="server" src="~/CentralModule/images/Calendario.bmp" style="width: 17px; height: 17px" alt="" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtFechaNacimietoOS"
                                                    ErrorMessage="Se debe capturar la Fecha de Nacimiento del Obligado Solidario"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator30"
                                                    InitialValue="" />
                                                <asp:RegularExpressionValidator runat="server"
                                                    ControlToValidate="TxtFechaNacimietoOS"
                                                    ErrorMessage="*"
                                                    ValidationGroup="Basica"
                                                    ID="RegularExpressionValidator12"
                                                    ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                                                    Formato de Fecha AAAA-MM-DD</asp:RegularExpressionValidator>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender21"
                                                    runat="server"
                                                    TargetControlID="TxtFechaNacimietoOS"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789-" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">RFC:<br />
                                                <asp:TextBox ID="TxtRFCOS" runat="server" CssClass="TextBoxMayusculas" Width="250px" MaxLength="15" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtRFCOS"
                                                    ErrorMessage="Se debe capturar el RFC (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator31"
                                                    InitialValue="" />
                                                <asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator13"
                                                    runat="server"
                                                    ControlToValidate="TxtRFCOS"
                                                    ErrorMessage="Formato Incorrecto de RFC (AAAA999999)"
                                                    Display="Dynamic"
                                                    ValidationGroup="Basica"
                                                    ValidationExpression="^(([A-Z]|[a-z]){4})([0-9]{6})([a-zA-Z0-9]{3})|^(([A-Z]|[a-z]){4})([0-9]{6})">
                                                    Formato Incorrecto de RFC (AAAA999999XXX)</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">CURP:
                                                <asp:TextBox ID="TxtCURPOS" runat="server" CssClass="TextBoxMayusculas" Width="250px" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtCURPOS"
                                                    ErrorMessage="Se debe capturar la CURP (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator32"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender23"
                                                    runat="server"
                                                    TargetControlID="TxtCURPOS"
                                                    FilterMode="ValidChars"
                                                    ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZÑ0123456789" />
                                                <asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator14"
                                                    runat="server"
                                                    ControlToValidate="TxtCURPOS"
                                                    ErrorMessage="Formato Incorrecto CURP"
                                                    Display="Dynamic"
                                                    ValidationGroup="Basica"
                                                    ValidationExpression="^[a-zA-Z]{4}\d{6}[a-zA-Z]{6}\d{2}$">
                                                    Formato Incorrecto CURP (AAAA999999AAAAAA99)</asp:RegularExpressionValidator>
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Teléfono:
                                                <asp:TextBox ID="TxtTelefonoOS" runat="server" Width="250px" MaxLength="10" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtTelefonoOS"
                                                    ErrorMessage="Se debe capturar el Telefono (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator33"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender24"
                                                    runat="server"
                                                    TargetControlID="TxtTelefonoOS"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                <asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator24"
                                                    runat="server"
                                                    ControlToValidate="TxtTelefonoOS"
                                                    ErrorMessage="Número de Teléfono de 10 dígitos"
                                                    Display="Dynamic"
                                                    ValidationGroup="Basica"
                                                    ValidationExpression="[0-9]{10}">
                                                    Número de Teléfono de 10 dígitos</asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">Código Postal:
                                                <asp:TextBox ID="TxtCPOS" runat="server" Width="250px" MaxLength="5" OnTextChanged="TxtCPOS_TextChanged" AutoPostBack="True" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtCPOS"
                                                    ErrorMessage="Se debe capturar el Código Postal (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator34"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender25"
                                                    runat="server"
                                                    TargetControlID="TxtCPOS"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Estado:
                                                <asp:DropDownList ID="DDXEstadoOS" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DDXEstadoOS_SelectedIndexChanged" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="DDXEstadoOS"
                                                    ErrorMessage="Se debe seleccionar el Estado (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator35"
                                                    InitialValue="Seleccione" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Delegación o Municipio:
                                                <asp:DropDownList ID="DDXMunicipioOS" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDXMunicipioOS_SelectedIndexChanged" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="DDXMunicipioOS"
                                                    ErrorMessage="Se debe seleccionar el Municipio o Delegación (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator36"
                                                    InitialValue="Seleccione" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">Colonia:
                                                <asp:DropDownList ID="DDXColoniaOS" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDXColoniaOS_SelectedIndexChanged" />
                                                <asp:DropDownList ID="DDXColoniaOSHidden" Width="250px" runat="server" Visible="False" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="DDXColoniaOS"
                                                    ErrorMessage="Se debe seleccionar la Colonia del Domicilio (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator37"
                                                    InitialValue="Seleccione" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top; width: 250px;">
                                                <asp:Label runat="server" Text=" Calle:" Width="250px" />
                                                <asp:TextBox ID="TxtCalleOS" runat="server" CssClass="TextBoxMayusculas" Width="250px" MaxLength="50" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtCalleOS"
                                                    ErrorMessage="Se debe capturar la Calle (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator38"
                                                    InitialValue="" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Número Exterior e Interior:
                                                <asp:TextBox ID="TxtNoExteriorOS" runat="server" Width="250px" MaxLength="50" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNoExteriorOS"
                                                    ErrorMessage="Se debe capturar el Número Exterior (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator39"
                                                    InitialValue="" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="PanelPersonaMoralCComp" runat="server" Visible="False">
                                    <table style="width: 873px;">
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">Razón Social
                                                <asp:TextBox ID="TxtRazonSocialOS" CssClass="TextBoxMayusculas" runat="server" Width="40%" MaxLength="255" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtRazonSocialOS"
                                                    ErrorMessage="Se debe capturar la Razón Social (Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator41"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender26"
                                                    runat="server"
                                                    TargetControlID="TxtRazonSocialOS"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 873px;">
                                        <tr>
                                            <td colspan="3" style="color: #0066FF">DATOS DEL REPRESENTANTE LEGAL DEL OBLIGADO SOLIDARIO
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">Nombre(s):<br />
                                                <asp:TextBox ID="TxtNombreRepLegalOS" runat="server" CssClass="TextBoxMayusculas" Width="250px" MaxLength="255" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNombreRepLegal"
                                                    ErrorMessage="Se debe capturar el Nombre (Representante Legal Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator42"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender29"
                                                    runat="server"
                                                    TargetControlID="TxtNombreRepLegal"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789 ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Apellido Paterno:<br />
                                                <asp:TextBox ID="TxtApPaternoRepLegalOS" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="255" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtApPaternoRepLegal"
                                                    ErrorMessage="Se debe capturar el Apellido Patermo (Representante Legal Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator45"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender30"
                                                    runat="server"
                                                    TargetControlID="TxtApPaternoRepLegal"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Apellido Materno:<br />
                                                <asp:TextBox ID="TxtApMaternoRepLegalOS" runat="server" CssClass="TextBoxMayusculas" Width="250px" MaxLength="255" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtApMaternoRepLegal"
                                                    ErrorMessage="Se debe capturar el Apellido Matermo (Representante Legal Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator46"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender31"
                                                    runat="server"
                                                    TargetControlID="TxtApMaternoRepLegal"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">E-mail:
                                                <asp:TextBox ID="TxtEmailRepLegalOS" runat="server" CssClass="TextBoxMayusculas" Width="250px" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtEmailRepLegalOS"
                                                    ErrorMessage="Se debe capturar el Email (Representante legal- Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator47"
                                                    InitialValue="" />
                                                <asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator15"
                                                    runat="server"
                                                    ControlToValidate="TxtEmailRepLegalOS"
                                                    ErrorMessage="Formato Incorrecto de E-mail"
                                                    Display="Dynamic"
                                                    ValidationGroup="Basica"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                    Formato Incorrecto de E-mail (XXXX@XXX.XX)
                                                </asp:RegularExpressionValidator>
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Teléfono:
                                                <asp:TextBox ID="TxtTelefonoRLOS" runat="server" Width="250px" MaxLength="10" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtTelefonoRLOS"
                                                    ErrorMessage="Se debe capturar el Telefono (Representante legal- Obligado Solidario)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator48"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender32"
                                                    runat="server"
                                                    TargetControlID="TxtTelefonoRLOS"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                <asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator25"
                                                    runat="server"
                                                    ControlToValidate="TxtTelefonoRLOS"
                                                    ErrorMessage="Número de Teléfono de 10 dígitos"
                                                    Display="Dynamic"
                                                    ValidationGroup="Basica"
                                                    ValidationExpression="[0-9]{10}">
                                                    Número de Teléfono de 10 dígitos</asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="color: #0066FF">PODER NOTARIAL DEL REPRESENTANTE LEGAL DEL OBLIGADO SOLIDARIO
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">Número de Escritura:
                                                <asp:TextBox ID="TxtNoEscrituraPN" runat="server" Width="250px" MaxLength="10" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNoEscrituraPN"
                                                    ErrorMessage="Se debe capturar el Número de Escritura (Poder Notarial)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator49"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender33"
                                                    runat="server"
                                                    TargetControlID="TxtNoEscrituraPN"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Fecha:<br />
                                                <asp:TextBox ID="TxtFechaEscrituraPN" runat="server" Width="250px" />
                                                <img id="Img6" onclick="muestra_calendario('TxtFechaEscrituraPN')" runat="server" src="~/CentralModule/images/Calendario.bmp" style="width: 17px; height: 17px" alt="" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtFechaEscrituraPN"
                                                    ErrorMessage="Se debe capturar la Fecha"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator50"
                                                    InitialValue="" />
                                                <asp:RegularExpressionValidator runat="server"
                                                    ControlToValidate="TxtFechaEscrituraPN"
                                                    ErrorMessage="*"
                                                    ValidationGroup="Basica"
                                                    ID="RegularExpressionValidator16"
                                                    ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                                                    Formato de Fecha AAAA-MM-DD
                                                </asp:RegularExpressionValidator>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender34"
                                                    runat="server"
                                                    TargetControlID="TxtFechaEscrituraPN"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789-" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Nombre Completo del Notario:
                                                <asp:TextBox ID="TxtNombreNotarioPN" onkeypress="return soloLetras(event)" CssClass="TextBoxMayusculas" runat="server" Width="250px" MaxLength="255" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNombreNotarioPN"
                                                    ErrorMessage="Se debe capturar el Nombre del Notario (Poder Notarial)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator51"
                                                    InitialValue="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">Estado:
                                                <asp:DropDownList ID="DDXEstadoPN" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DDXEstadoPN_SelectedIndexChanged" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="DDXEstadoPN"
                                                    ErrorMessage="Se debe seleccionar el Estado (Poder Notarial)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator52"
                                                    InitialValue="Seleccione" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Delegación o Municipio:
                                                <asp:DropDownList ID="DDXMunicipioPN" Width="250px" runat="server" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="DDXMunicipioPN"
                                                    ErrorMessage="Se debe seleccionar el Municipio o Delegación (Poder Notarial)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator53"
                                                    InitialValue="Seleccione" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Número de Notaría:
                                                <asp:TextBox ID="TxtNotariaPN" runat="server" Width="250px" MaxLength="10" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNotariaPN"
                                                    ErrorMessage="Se debe capturar el Número de Notaria (Poder Notarial)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator54"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender36"
                                                    runat="server"
                                                    TargetControlID="TxtNotariaPN"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="color: #0066FF">ACTA CONSTITUTIVA DEL OBLIGADO SOLIDARIO
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">Número de Escritura:
                                                <asp:TextBox ID="TxtNumeroEscrituraAC" runat="server" Width="250px" MaxLength="10" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNumeroEscrituraAC"
                                                    ErrorMessage="Se debe capturar el Número de Escritura del Acta Constitutiva"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator55"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender37"
                                                    runat="server"
                                                    TargetControlID="TxtNumeroEscrituraAC"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Fecha:<br />
                                                <asp:TextBox ID="TxtFechaAC" runat="server" Width="230px" />
                                                <img id="Img7" onclick="muestra_calendario('TxtFechaAC')" runat="server" src="~/CentralModule/images/Calendario.bmp" style="width: 17px; height: 17px" alt="" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtFechaAC"
                                                    ErrorMessage="Se debe capturar la Fecha del Acta Constitutiva"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator56"
                                                    InitialValue="" />
                                                <asp:RegularExpressionValidator runat="server"
                                                    ControlToValidate="TxtFechaAC"
                                                    ErrorMessage="*"
                                                    ValidationGroup="Basica"
                                                    ID="RegularExpressionValidator17"
                                                    ValidationExpression="^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$" Height="0px">
                                                    Formato de Fecha AAAA-MM-DD
                                                </asp:RegularExpressionValidator>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender38"
                                                    runat="server"
                                                    TargetControlID="TxtFechaAC"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789-" />
                                            </td>
                                            <td class="auto-style2" style="vertical-align: top;">Nombre Completo del Notario:
                                                <asp:TextBox ID="TxtNombreNotarioAC" runat="server" onkeypress="return soloLetras(event)" CssClass="TextBoxMayusculas" Width="250px" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNombreNotarioAC"
                                                    ErrorMessage="Se debe capturar el Nombre del Notario (Acta Constitutiva)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator231"
                                                    InitialValue="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2" style="vertical-align: top;">Estado:
                                                <asp:DropDownList ID="DDXEstadoAC" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DDXEstadoAC_SelectedIndexChanged" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="DDXEstadoAC"
                                                    ErrorMessage="Se debe seleccionar el Estado (Acta Constitutiva)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator57"
                                                    InitialValue="Seleccione" />
                                            </td>
                                            <td style="width: 250px; vertical-align: top;">Delegación o Municipio:
                                                <asp:DropDownList ID="DDXMunicipioAC" Width="250px" runat="server" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="DDXMunicipioAC"
                                                    ErrorMessage="Se debe seleccionar el Municipio o Delegación del Domicilio (Acta Constitutiva)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator58"
                                                    InitialValue="Seleccione" />
                                            </td>
                                            <td style="width: 250px; vertical-align: top;">Número de Notaría:
                                                <asp:TextBox ID="TxtNotariaAC" runat="server" Width="250px" MaxLength="10" />
                                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtNotariaAC"
                                                    ErrorMessage="Se debe capturar el Número de Notaría (Acta Constitutiva)"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator284"
                                                    InitialValue="" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender196"
                                                    runat="server"
                                                    TargetControlID="TxtNotariaAC"
                                                    FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </fieldset>
                            <p>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification"
                                    ValidationGroup="Basica" Font-Size="Small" HeaderText="Resumén Captura:" />
                                <p>
                                    <asp:Button ID="BtnGuardarComplementaria" runat="server" OnClick="BtnGuardarComplementaria_Click" Text="Guardar" />
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                            </p>
                        </div>
                    </asp:WizardStep>

                    <asp:WizardStep runat="server" ID="wizBajaEquiposCapturaComp" StepType="Step">
                        <div style="display: none">
                            <asp:Button ID="btnRefresh" runat="server" Text="Button" OnClick="btnRefresh_Click" />
                            <asp:HiddenField ID="HidActualizaOk" runat="server" />
                            <asp:HiddenField ID="HidstateLoad" runat="server" />
                        </div>
                        <br/>
                        <asp:Image ID="imgBajaEquipo" runat="server" ImageUrl="~/SupplierModule/images/t_alta.png"/>
                        <br />
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 180px; height: 25px">
                                    <asp:Label ID="lblNomCom" runat="server" CssClass="Label,etiqueta" Text="Nombre comercial" />
                                </td>
                                <td style="width: 200px; height: 25px;">
                                    <asp:TextBox ID="txtRazonSocialEquiposBaja" runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="200px" />
                                </td>
                                <td style="width: 150px; height: 25px;">
                                    <asp:Label ID="lblNumcre" runat="server" CssClass="Label,etiqueta" Text="Número Crédito" />
                                </td>
                                <td style="height: 25px">
                                    <asp:TextBox ID="txtCreditoNumEquiposBaja" runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="200px" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Image ID="imgeus" runat="server" ImageUrl="~/SupplierModule/images/t10.png" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div  style="padding: 10px">
                            <asp:Label ID="lblCAyD" runat="server" Text="CAyD: " CssClass="Label,etiqueta" />
                             <asp:DropDownList ID="drpCAyD" runat="server" Class="DropDownList" Font-Size="11px" Width="207px" Height="16px" />
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="drpCAyD"
                                                ErrorMessage="Se debe de capturar el CAyD"
                                                ValidationGroup="ValidaCAyD"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="True"
                                                ID="ValidaDrp"
                                                InitialValue="Seleccione" />
                        </div>
                        <div id="divGridEquiposBaja" style="align-content: center;" align="center" runat="server">
                            <asp:GridView Width="100%" runat="server" ID="grdEquiposBaja" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                AllowPaging="True" PageSize="10" DataKeyNames="Id_Credito_Sustitucion" 
                                EmptyDataText="Recuerda que para las tecnologías de adquisición (Sub Estaciones Eléctricas y Banco de Capacitores)  no se registran equipos de baja eficiencia favor de continuar."
                                OnRowDataBound="grdEquiposBaja_RowDataBound">
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <EditRowStyle CssClass="GridViewEditStyle" />
                                <Columns>
                                    <asp:BoundField DataField="No_Credito" ItemStyle-CssClass="PanelNoVisible" HeaderStyle-CssClass="PanelNoVisible" >
                                    <HeaderStyle CssClass="PanelNoVisible" />
                                    <ItemStyle CssClass="PanelNoVisible" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_Credito_Sustitucion" ItemStyle-CssClass="PanelNoVisible" HeaderStyle-CssClass="PanelNoVisible">
                                    <HeaderStyle CssClass="PanelNoVisible" />
                                    <ItemStyle CssClass="PanelNoVisible" />
                                    </asp:BoundField>
                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="Información Complementaria" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="BtnImgEditar" runat="server" ImageUrl="~/CentralModule/images/editar-icono.png" OnClick="BtnImgEditar_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Con/Sin Información" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckbSelect" Enabled="False" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Tecnologia" HeaderText="Tecnología" ItemStyle-Width="15%">
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Producto" HeaderText="Tipo de Producto" ItemStyle-Width="25%">
                                        <ItemStyle Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Grupo" HeaderText="Grupo" ItemStyle-Width="15%">
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Unidades" HeaderText="Unidades" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                    </asp:BoundField>
                                  <asp:BoundField DataField="Cve_Tecnologia" ItemStyle-CssClass="PanelNoVisible" HeaderStyle-CssClass="PanelNoVisible" >

                                    <HeaderStyle CssClass="PanelNoVisible" />
                                    <ItemStyle CssClass="PanelNoVisible" />
                                    </asp:BoundField>

                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
                        <asp:Panel runat="server" ID="datosComplementarios" CssClass="PanelNoVisible" Visible="True">
                            <fieldset id="Fieldset1" runat="server">
                                <legend style="color: #0066FF">
                                    <asp:Label ID="lblInformacionEB" runat="server" />
                                </legend>
                                <asp:HiddenField ID="hiddenRowIndexEquiboBaja" runat="server" />
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="4">
                                            <asp:Image ID="Image34" runat="server" Height="16" ImageUrl="~/SupplierModule/images/arrow.png" />
                                            <asp:Label runat="server" Text="Información Equipo Baja Eficiencia a Disponer" CssClass="Label1" ForeColor="#333333" ID="lblequi" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                             
                                        </td>
                                        <td>
                                           
                                        </td>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">
                                            <asp:Label ID="lblColor" runat="server" CssClass="Label,etiqueta" Text="Color: " />
                                        </td>
                                        <td class="auto-style2">
                                            <telerik:RadTextBox ID="txtColor" runat="server" Width="127px" CssClass="TextBoxMayusculas">
                                             <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="txtColor"
                                                ErrorMessage="Se debe capturar el Color"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator1"
                                                InitialValue="" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMarca" runat="server" CssClass="Label,etiqueta" Text="Marca: " />
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtMarca" runat="server" Width="183px" CssClass="TextBoxMayusculas" >
                                              <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="txtMarca"
                                                ErrorMessage="Se debe capturar la Marca"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="RequiredFieldValidator2"
                                                InitialValue="">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>    
                                        <td>
                                            <asp:Label ID="lblModelo" runat="server" CssClass="Label,etiqueta" Text="Modelo: " />
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtModelo" runat="server" CssClass="TextBoxMayusculas" Width="149px">
                                                <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="txtModelo"
                                                ErrorMessage="Se debe capturar el Modelo"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="reqModelo"
                                                InitialValue="" />

                                        </td>
                                        <td>
                                            <asp:Label ID="lbAntiguedad" runat="server" CssClass="Label,etiqueta" Text="Antigüedad: " />
                                        </td>
                                        <td>
                                             <telerik:RadNumericTextBox ID="txtAntiguedad" MaxLength="3" name="txtAntiguedad" runat="server" Width="7em" Type="Number" AutoPostBack="True"  OnTextChanged="txtAntiguedad_TextChanged" >
                                                 <NumberFormat ZeroPattern="n" />
                                                 <ClientEvents OnKeyPress="Cancel_OnKeyPress" ></ClientEvents>
                                                 
                                            </telerik:RadNumericTextBox>
                                             <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="txtAntiguedad"
                                                ErrorMessage="Se debe capturar la Antigüedad"
                                                ValidationGroup="Basica"
                                                Display="Dynamic"
                                                Text="*"
                                                EnableClientScript="true"
                                                ID="reqModelo1"
                                                InitialValue="" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblFoto" runat="server" CssClass="Label,etiqueta" Text="Fotografía Equipo Baja Eficiencia: " />
                                        </td>
                                        <td colspan="2" style="width: 280px">
                                           <telerik:RadAsyncUpload runat="server" id="UploadedBajaEquipoViejo" 
                                                OnFileUploaded="UploadedBajaEquipoViejo_FileUploaded"
                                                OnClientFilesUploaded="OnClientFilesUploadedBajaEquipoViejo"
                                                OnClientFileUploadFailed="OnClientFilesFailUploadedBajaEquipoViejo"
                                                OnClientValidationFailed="OnClientValidationFailedUploadedBajaEquipoViejo"
                                                MaxFileSize="10000000"
                                                Style="margin-left: 34px"
                                                Localization-Select="Examinar" Width="80px">
                                               <FileFilters>
                                                    <telerik:FileFilter Description="Images(emf;wmf;jpg;jpeg;jpe;png;bmp;tif)" Extensions="emf,wmf,jpg,jpeg,jpe,png,bmp,tif" />
                                                </FileFilters>
                                               <Localization Select="Examinar" />
                                            </telerik:RadAsyncUpload>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgOkEquipoViejo" runat="server" ImageUrl="~/CentralModule/images/icono_correcto.png" Height="25px" Width="27px" Visible="False" />
                                            <asp:ImageButton runat="server" ID="verEquipoViejo" ImageUrl="~/CentralModule/images/visualizar.png" OnClick="verEquipoViejo_Click" />

                                        </td>
                                    </tr>
                                </table>

                                <br />
                                <table>
                                    <tr>
                                        <td colspan="5">
                                            <asp:Image ID="Image4" runat="server" Height="16" ImageUrl="~/SupplierModule/images/arrow.png" />
                                            <asp:Label ID="lblregistara" runat="server" CssClass="Label1" ForeColor="#333333" Text="Registrar Horarios de Equipos" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%;">
                                    <tr class="trh">
                                        <td>Horarios de Operación
                                        </td>
                                        <td>
                                        Lunes<td>Martes</td>
                                        <td>Miércoles</td>
                                        <td>Jueves</td>
                                        <td>Viernes</td>
                                        <td>Sábado</td>
                                        <td>Domingo</td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>Hora de inicio
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioLunesBajaEquipo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasBajaEquipo();" Font-Size="Small"/>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioMartesBajaEquipo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasBajaEquipo();" Font-Size="Small"/>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioMiercolesBajaEquipo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasBajaEquipo();" Font-Size="Small"/>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioJuevesBajaEquipo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasBajaEquipo();" Font-Size="Small"/>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioViernesBajaEquipo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasBajaEquipo();" Font-Size="Small"/>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioSabadoBajaEquipo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasBajaEquipo();" Font-Size="Small"/>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXInicioDomingoBajaEquipo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasBajaEquipo();" Font-Size="Small"/>

                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>Horas laborables
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor1BajaEquipo" MaxLength="3" MaxValue="24" MinValue="1" name="hlabor1BajaEquipo" runat="server" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasBajaEquipo();">
                                                <NumberFormat DecimalDigits="1" />
                                                <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor2BajaEquipo" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1BajaEquipo" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasBajaEquipo();">
                                                <NumberFormat DecimalDigits="1" />
                                                <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor3BajaEquipo" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1BajaEquipo" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasBajaEquipo();">
                                                <NumberFormat DecimalDigits="1" />
                                                <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor4BajaEquipo" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1BajaEquipo" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasBajaEquipo();">
                                                <NumberFormat DecimalDigits="1" />
                                                <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor5BajaEquipo" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1BajaEquipo" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasBajaEquipo();">
                                                <NumberFormat DecimalDigits="1" />
                                                <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                            </telerik:RadNumericTextBox>
                                            </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor6BajaEquipo" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1BajaEquipo" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasBajaEquipo();">
                                                <NumberFormat DecimalDigits="1"  />
                                                <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="hlabor7BajaEquipo" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1BajaEquipo" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasBajaEquipo();">
                                                <NumberFormat DecimalDigits="1" />
                                                <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            Horas a la semana &nbsp;
                                <asp:TextBox ID="TxtHorasSemanaBajaEquipo" runat="server" Enabled="False" />
                                        </td>
                                        <td colspan="3">Semanas al año &nbsp;
                               <telerik:RadNumericTextBox ID="noSemanasBajaEquipo" runat="server" MaxLength="2" MaxValue="52" MinValue="1" Value="52" onChange="CalculaTotalHorasTrabajadasBajaEquipo();">
                                   <NumberFormat DecimalDigits="0" />
                                   <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                               </telerik:RadNumericTextBox>
                                        </td>
                                        <td colspan="2">Horas al año &nbsp;
                                <asp:TextBox ID="TxtHorasAnioBajaEquipo" runat="server" Enabled="False" Text="0" />
                                             <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtHorasAnioBajaEquipo"
                                                    ErrorMessage="Se debe registrar Horarios de Operación"
                                                    ValidationGroup="ValidaDatosEquipobaja"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator15"
                                                    InitialValue="0" />

                                        </td>
                                    </tr>
                                </table>
                                <asp:ValidationSummary runat="server" ID="ValidaDatosEquipobaja" CssClass="failureNotification" ValidationGroup="Basica" Font-Size="Small" HeaderText="Resumen:" />
                                <p style="text-align: center; width: 873px">
                                    <asp:Button ID="btnGuardarDatosCompEquipoBaja" runat="server" OnClick="btnGuardarDatosCompEquipoBaja_Click" Text="Guardar Datos" />
                                </p>
                            </fieldset>
                        </asp:Panel>
                        <p>
                            <asp:HiddenField ID="hidIdCreditoSustitucion" runat="server" />
                            <asp:HiddenField ID="hidCveTecnologia" runat="server" />
                            <asp:HiddenField ID="HidIdTecnologia" runat="server" />
                            <asp:HiddenField ID="HidRequiereCAyD" runat="server" />
                    </asp:WizardStep>
                
                    <asp:WizardStep runat="server" ID="wizardAltaBajaEquipos" StepType="Finish">
                      <div style="display: none">
                            <asp:Button ID="btnRefresh2" runat="server" Text="Button" OnClick="btnRefresh2_Click" />
                            <asp:HiddenField ID="HidActualiza2Ok" runat="server" />
                            <asp:HiddenField ID="HidstateLoad2" runat="server" />
                      </div>
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/SupplierModule/images/t_alta.png"/>
                        <br />
                        <br/>
                        <asp:Image ID="Image6" runat="server" ImageUrl="~/SupplierModule/images/t11.png" />
                        <br />
                        <br/>
                         <table style="width: 100%">
                <tr>
                    <td style="width: 180px; height: 25px">
                        <asp:Label ID="Label2" runat="server" CssClass="Label,etiqueta" Text="Nombre comercial" />
                    </td>
                    <td style="width: 200px; height: 25px;">
                        <asp:TextBox ID="txtRazonSocialAltaEquipos" runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="200px" />
                    </td>
                    <td style="width: 150px; height: 25px;">
                        <asp:Label ID="Label3" runat="server" CssClass="Label,etiqueta" Text="Número Crédito" />
                    </td>
                    <td style="height: 25px">
                        <asp:TextBox ID="txtCreditoNumAltaEquipos" runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="200px" />
                    </td>
                </tr>
            </table>
                        <br />
                         <table id="Table1" align="center" runat="server">
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Check List de Crédito" ID="btnDisplayCreditCheckList" Width="267px"
                            OnClick="btnDisplayCreditCheckList_Click" />
                    </td>
                    <td id="Td1" class="auto-style6" runat="server"></td>
                    <td>
                        <asp:Button runat="server" Text="Contrato de Financiamiento" Width="267px"
                            ID="btnDisplayCreditContract" OnClick="btnDisplayCreditContract_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Acta Entrega - Recepción de Equipos" Width="267px"
                            ID="btnDisplayEquipmentAct" OnClick="btnDisplayEquipmentAct_Click" />
                    </td>
                    <td id="Td2"  runat="server"></td>
                    <td >
                        <asp:Button runat="server" Text="Solicitud Crédito" ID="btnDisplayCreditRequest1" Width="267px"
                            OnClick="btnDisplayCreditRequest1_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Pagaré" ID="btnDisplayPromissoryNote" Width="267px"
                            OnClick="btnDisplayPromissoryNote_Click" />
                    </td>
                    <td id="Td3" runat="server"></td>
                    <td >
                        <asp:Button runat="server" Text="Endoso en Garantía" ID="btnDisplayGuaranteeEndorsement" Width="267px"
                            OnClick="btnDisplayGuaranteeEndorsement_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Carta Presupuesto de Inversión" Width="267px"
                            ID="btnDisplayQuota1" OnClick="btnDisplayQuota1_Click" />
                    </td>
                    <td id="Td4"  runat="server"></td>
                    <td>
                        <asp:Button runat="server" Text="Carta Compromiso Obligado Solidario" Width="267px"
                            ID="btnDisplayGuarantee" OnClick="btnDisplayGuarantee_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Tabla de Amortización" ID="btnDisplayRepaymentSchedule" Width="267px"
                            OnClick="btnDisplayRepaymentSchedule_Click" />
                    </td>
                    <td></td>
                    <td>
                        <asp:Button runat="server" Text="Recibo de Incentivo Energético (Descuento)" Enabled="True" Width="267px"
                            ID="btnDisplayDisposalBonusReceipt" OnClick="btnDisplayDisposalBonusReceipt_Click" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Pre-Boleta CAyD" ID="btnDisplayReceiptToSettle" Enabled="True" Width="267px"
                            OnClick="btnDisplayReceiptToSettle_Click" />
                    </td>
                    <td id="Td5" runat="server"></td>
                    <td >
                        <asp:Button runat="server" Text="Tabla de Amortización - Pagaré" Width="267px"
                            ID="BtnAmortPag" OnClick="btnAmortPag_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnBoletaBajaEficiencia" runat="server" Enabled="false" Width="267px"
                            OnClick="btnBoletaBajaEficiencia_Click" Text="Boleta Ingreso Equipo" Visible="False" />
                    </td>
                    <td id="Td7" class="auto-style6" runat="server"></td>
                    <td id="Td8" class="auto-style6" runat="server"></td>
                </tr>
            </table>
                        <br />
                        <br />
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/SupplierModule/images/t8.png" />
                        <table>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label5" runat="server" CssClass="Label,etiqueta" Text="Fotografía Fachada del Negocio/Local: " />
                    </td>
                    <td style="width:285px">
                        <telerik:RadAsyncUpload runat="server" id="UploadFachada" 
                                                OnFileUploaded="UploadFachada_FileUploaded"
                                                OnClientFilesUploaded="OnClientFilesUploadedFachada"
                                                OnClientFileUploadFailed="OnClientFilesFailUploadedFachada"
                                                OnClientValidationFailed="OnClientValidationFailUploadedFachada"
                                                MaxFileSize="10000000"
                                                Style="margin-left: 34px"
                            Localization-Select="Examinar" Width="80px">
                            <Localization Select="Examinar" />
                            <FileFilters>
                                <telerik:FileFilter Description="Images(emf;wmf;jpg;jpeg;jpe;png;bmp;tif)" Extensions="emf,wmf,jpg,jpeg,jpe,png,bmp,tif" />
                            </FileFilters>
                        </telerik:RadAsyncUpload>
                      </td>
                    <td>
                        <asp:Image ID="imgOKLoaderFachada" runat="server" ImageUrl="~/CentralModule/images/icono_correcto.png" Height="25px" Width="25px" Visible="False" />
                            <asp:ImageButton runat="server" ID="imgbtnVer" ImageUrl="~/CentralModule/images/visualizar.png" OnClick="imgbtnVer_Click" />
                         <asp:Label ID="lblMesgFachada" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
                        <br />
                        <div id="div4" style="align-content: center;" align="center" runat="server">
                         <asp:GridView Width="100%" runat="server" ID="grdEquiposAlta" AutoGenerateColumns="False" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="6" DataKeyNames="Id_Credito_Producto,idConsecutivo" OnRowDataBound="grdEquiposAlta_RowDataBound">
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EditRowStyle CssClass="GridViewEditStyle" />
                    <Columns>
                        <asp:BoundField DataField="Id_Credito_Producto" Visible="False" />
                        <asp:BoundField DataField="idConsecutivo" Visible="False" />
                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="Información Complementaria" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="BtnImgEditar" runat="server" ImageUrl="~/CentralModule/images/editar-icono.png" OnClick="BtnImgEditarAltaEquipo_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Con/Sin Información" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbSelect" Enabled="False" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Tecnologia" HeaderText="Tecnología" ItemStyle-Width="10%" >
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-Width="10%" >
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-Width="10%" >
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Producto" HeaderText="Producto" ItemStyle-Width="15%">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right" >
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right" >
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="Precio Distribuidor" HeaderText="Cantidad" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right" />--%>
                        <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right" >
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Gastos_Instalacion" HeaderText="Gastos Instalacion" DataFormatString="{0:C}"  ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right" >
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ImporteTotalSinIva" HeaderText="Importe Total s/IVA" DataFormatString="{0:C}" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right" >
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Cve_Tecnologia" Visible="False" />
                    </Columns>
                </asp:GridView>
                        </div>
                        <br />
                        <asp:Panel runat="server" ID="datosComplementariosAlta" Visible="True" CssClass="PanelVisible">
                    <asp:HiddenField ID="hiddenRowIndexEquiboAlta" runat="server" />
                    <table style="width: 100%">
                        <tr>
                            <td colspan="8">
                                <asp:Image ID="Image3" runat="server" Height="16" ImageUrl="~/SupplierModule/images/arrow.png" />
                                <asp:Label runat="server" Text="Información Equipo Alta" CssClass="Label1" ForeColor="#333333" ID="Label53" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoMov" runat="server" CssClass="Label,etiqueta" />
                                <br />
                                <asp:Label ID="lblInformacionEA" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="EquipoViejo" runat="server">
                <fieldset class="legend_info">
                    <legend style="font-size: 14px; align-content: initial">Equipo Viejo</legend>
                    <table>
                        <tr>
                            <td colspan="2" class="auto-style10">
                                <asp:Label ID="Label54" runat="server" CssClass="Label,etiqueta" Text="Fotografía Equipo Viejo: " />
                            </td>
                            <td colspan="2" style="width: 280px">
                            <telerik:RadAsyncUpload runat="server" id="UploadEquipoViejo" 
                                                OnFileUploaded="UploadEquipoViejo_FileUploaded"
                                                OnClientFilesUploaded="OnClientFilesUploadedEquipoViejo"
                                                OnClientFileUploadFailed="OnClientFilesFailUploadedEquipoViejo"
                                                OnClientValidationFailed="OnClientValidationFailedUploadedBajaEquipoViejo"
                                                MaxFileSize="10000000"
                                                Style="margin-left: 34px"
                                                Localization-Select="Examinar" Width="80px">
                                
                                <Localization Select="Examinar" />
                                <FileFilters>
                                    <telerik:FileFilter Description="Images(emf;wmf;jpg;jpeg;jpe;png;bmp;tif)" Extensions="emf,wmf,jpg,jpeg,jpe,png,bmp,tif" />
                                </FileFilters>
                                </telerik:RadAsyncUpload>

                            </td>
                            <td>
                                <asp:Image ID="imgOKLoaderEquipoViejo" runat="server" ImageUrl="~/CentralModule/images/icono_correcto.png" Height="25px" Width="27px" Visible="False" />
                                <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/CentralModule/images/visualizar.png" OnClick="verEquipoViejoAlta_Click" />
                               
                            </td>
                        </tr>
                    </table>
                    <table id="tbl" runat="server" style="width: 100%;">
                        <tr class="trh">
                            <td>Horarios de Operación
                            </td>
                            <td>Lunes
                    &nbsp;
                   </td>
                            <td>Martes
                    &nbsp;
                            </td>
                            <td>Miercoles
                    &nbsp;
                            </td>
                            <td>Jueves
                    &nbsp;
                            </td>
                            <td>Viernes
                    &nbsp;
                            </td>
                            <td>Sabado
                    &nbsp;
                            </td>
                            <td>Domingo
                    &nbsp;
                            </td>
                        </tr>
                        <tr class="tr2">
                            <td>Inicio
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioLunesAltaEquipoViejo" runat="server"  Width="7em" Font-Size="Small" Height="17px"></asp:DropDownList>

                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioMartesAltaEquipoViejo" runat="server"  Width="7em" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioMiercolesAltaEquipoViejo" runat="server"  Width="7em" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();"></asp:DropDownList>

                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioJuevesAltaEquipoViejo" runat="server"  Width="7em" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" ></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioViernesAltaEquipoViejo" runat="server"  Width="7em" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();"></asp:DropDownList>

                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioSabadoAltaEquipoViejo" runat="server"  Width="7em" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioDomingoAltaEquipoViejo" runat="server"  Width="7em" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tr1">
                            <td>Horas de Operación
                            </td>
                            <td>
                                 <telerik:RadNumericTextBox ID="hlabor1" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" runat="server" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="1" />
                                     <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>
                                
                              

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor2" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor3" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor4" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor5" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox> 

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor6" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor7" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="HiddenField4" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldLunesAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldMartesAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldMiercolesAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldJuevesAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldViernesAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldSabadoAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldDomingoAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">Horas a la semana
                    &nbsp;
                    <asp:TextBox ID="TxtHorasSemanaAltaEquipoViejo" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                            <td colspan="3">Semanas al año
                    &nbsp;
                    <telerik:RadNumericTextBox ID="noSemanasAltaEquipoViejo" runat="server" MaxLength="2" MaxValue="52" MinValue="1" Value="52" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();">
                                    <NumberFormat DecimalDigits="0" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="2">Horas al año
                    &nbsp;
                                <asp:TextBox ID="TxtHorasAnioAltaEquipoViejo" runat="server" Enabled="False" Text="0.0"/>
                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtHorasAnioAltaEquipoViejo"
                                                    ErrorMessage="Se debe registrar Horarios de Operación"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator74"
                                                    InitialValue="0.0" />
                            </td>
                        </tr>
                    </table>

                </fieldset>
            </asp:Panel>
                    <asp:Panel ID="EquipoNuevo" runat="server">
                <fieldset class="legend_info">
                    <legend style="font-size: 14px; align-content: initial">Equipo Nuevo </legend>
                    <table>
                        <tr>
                            <td colspan="2" class="auto-style10">
                                <asp:Label ID="Label61" runat="server" CssClass="Label,etiqueta" Text="Fotografía Equipo Nuevo: " />
                            </td>
                            <td colspan="2" style="width: 280px">
                              <telerik:RadAsyncUpload runat="server" id="UploadEquipoNuevo" 
                                                OnFileUploaded="UploadEquipoNuevo_FileUploaded"
                                                OnClientFilesUploaded="OnClientFilesUploadEquipoNuevo"
                                                OnClientFileUploadFailed="OnClientFilesFailUploadEquipoNuevo"
                                                OnClientValidationFailed="OnClientValidationFailedUploadEquipoNuevo"
                                                MaxFileSize="10000000"
                                                Style="margin-left: 34px"
                                                Localization-Select="Examinar" Width="80px">

                                  <Localization Select="Examinar" />
                                    <FileFilters>
                                        <telerik:FileFilter Description="Images(emf;wmf;jpg;jpeg;jpe;png;bmp;tif)" Extensions="emf,wmf,jpg,jpeg,jpe,png,bmp,tif" />
                                    </FileFilters>
                                </telerik:RadAsyncUpload>

                            </td>
                            <td>
                                <asp:Image ID="imgOkEquipoNuevo" runat="server" ImageUrl="~/CentralModule/images/icono_correcto.png" Height="25px" Width="27px" Visible="False" />
                                <asp:ImageButton runat="server" ID="verEquipoNuevo" ImageUrl="~/CentralModule/images/visualizar.png" OnClick="verEquipoNuevo_Click" />
                               
                            </td>
                        </tr>
                    </table>

                    <table style="width: 100%;">
                        <tr class="trh">
                            <td>Horarios de Operación
                            </td>
                            <td>Lunes
                    &nbsp;
                                </td>
                            <td>Martes
                    &nbsp;
                            </td>
                            <td>Miercoles
                    &nbsp;
                            </td>
                            <td>Jueves
                    &nbsp;
                            </td>
                            <td>Viernes
                    &nbsp;
                            </td>
                            <td>Sabado
                    &nbsp;
                            </td>
                            <td>Domingo
                    &nbsp;
                            </td>
                        </tr>
                        <tr class="tr2">
                            <td>Inicio
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioLunesAltaEquipoNuevo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioMartesAltaEquipoNuevo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioMiercolesAltaEquipoNuevo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioJuevesAltaEquipoNuevo" runat="server" Width="7em" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioViernesAltaEquipoNuevo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioSabadoAltaEquipoNuevo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioDomingoAltaEquipoNuevo" runat="server"  Width="7em" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr class="tr1">
                            <td>Horas laborables
                            </td>
                            <td>
                                 <telerik:RadNumericTextBox ID="hlaborNuevo1" MaxLength="4" MaxValue="24" MinValue="1" name="hlaborNuevo1" runat="server" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="1" />
                                     <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>
                                
                              

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo2" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo3" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo4" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo5" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox> 

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo6" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo7" runat="server" MaxLength="4" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="1" />
                                    <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                                </telerik:RadNumericTextBox>

                            </td>
                        </tr>
                       
                         <tr>
                            <td>
                                <asp:HiddenField ID="HiddenField5" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldLunesAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldMartesAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldMiercolesAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldJuevesAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldViernesAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldSabadoAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldDomingoAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">Horas a la semana
                    &nbsp;
                    <asp:TextBox ID="TxtHorasSemanaAltaEquipoNuevo" runat="server" Enabled="False"  Text="0.0"></asp:TextBox>
                            </td>
                            <td colspan="3">Semanas al año
                    &nbsp;
                     <telerik:RadNumericTextBox ID="TxtSemanasAnioAltaEquipoNuevo" runat="server" MaxLength="2" MaxValue="52" MinValue="1" Value="52" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();">
                       <NumberFormat DecimalDigits="0" />
                         <ClientEvents OnKeyPress="Cancel_OnKeyPress"></ClientEvents>
                     </telerik:RadNumericTextBox>
                      <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="TxtSemanasAnioAltaEquipoNuevo"
                                    ErrorMessage="Se debe capturar las Semanas al año"
                                    ValidationGroup="Basica"
                                    Display="Dynamic"
                                    Text="*"
                                    EnableClientScript="true"
                                    ID="RequiredFieldValidator3"
                                    InitialValue="" />
                            </td>
                            <td colspan="2">Horas al año
                    &nbsp;
                                <asp:TextBox ID="TxtHorasAnioAltaEquipoNuevo" runat="server" Enabled="False" Text="0.0"/>
                                <asp:RequiredFieldValidator runat="server"
                                                    ControlToValidate="TxtHorasAnioAltaEquipoNuevo"
                                                    ErrorMessage="Se debe registrar Horarios de Operación"
                                                    ValidationGroup="Basica"
                                                    Display="Dynamic"
                                                    Text="*"
                                                    EnableClientScript="true"
                                                    ID="RequiredFieldValidator75"
                                                    InitialValue="0.0" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
                    <asp:ValidationSummary runat="server" ID="ValidationSummary2" CssClass="failureNotification" ValidationGroup="Basica" Font-Size="Small" HeaderText="Resumen:" />
                    <p style="text-align: center; width: 873px">
                    <asp:Button ID="Button3" runat="server" OnClick="btnGuardarDatosCompEquipoAlta_Click" Text="Guardar Datos" />
            </p>
                     <asp:HiddenField ID="hidDataKey0" runat="server" />
                     <asp:HiddenField ID="hidDataKey" runat="server" />
                     <asp:HiddenField ID="HidTipoMovimiento" runat="server" />
            </asp:Panel>
                    </asp:WizardStep>
                    
                </WizardSteps>
                <StepNavigationTemplate>
                    <asp:Button ID="btnStepPre" runat="server" Text="Regresar" CssClass="Button" CommandName="MovePrevious"
                        OnClientClick="return confirm('¿ Desea Regresar a la Pantalla Previa ?');" />
                    <asp:Button ID="btnStepNext" runat="server" Text="Siguiente" CssClass="Button" CommandName="MoveNext"
                        OnClientClick="return confirm('¿ Desea Continuar con el Registro de la Solicitud de Crédito ?');" />
                    <asp:Button ID="BtnCacel" runat="server" Text="Salir" CssClass="Button" OnClick="BtnCacel_Click"
                        OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
                </StepNavigationTemplate>
            </asp:Wizard>
            <div style="display: none">
                <asp:Button ID="HiddenButton" BackColor="#FFFFFF" OnClick="HiddenButton_Click" runat="server" Width="0px" />
            </div>
            
        </ContentTemplate>
   </asp:UpdatePanel>
   <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="panel">
        <ProgressTemplate>
            <div id="busyIndicator"> 
                <img src="../images/progress.gif" alt="¡ En Proceso, Por favor Espere !" />
            </div> 
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        function InitializeRequest(sender, args) {
            var doc = document.documentElement;
            var top = (window.pageYOffset || doc.scrollTop) - (doc.clientTop || 0);
            var busyIndicator = document.getElementById('busyIndicator');
            busyIndicator.style.top = top + 'px';
            document.body.style.overflow = "hidden";
        }
        function EndRequest(sender, args) {
            var busyIndicator = document.getElementById('busyIndicator');
            busyIndicator.style.top = top + 'px';
            document.body.style.overflow = "auto";
        }
    </script>--%>
</asp:Content>
