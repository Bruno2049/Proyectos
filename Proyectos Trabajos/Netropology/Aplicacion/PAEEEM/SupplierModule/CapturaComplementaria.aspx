<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapturaComplementaria.aspx.cs" Inherits="PAEEEM.SupplierModule.CapturaComplementaria" MasterPageFile="../Site.Master" %>

<%@ Register Src="~/SupplierModule/Controls/WebUserControl1.ascx" TagPrefix="uc1" TagName="WebUserControl1" %>
<%@ Register Src="~/SupplierModule/Controls/wucCapturaBasica.ascx" TagPrefix="uc1" TagName="wucCapturaBasica" %>
<%@ Register Src="~/SupplierModule/Controls/wucValidaPyme.ascx" TagPrefix="uc1" TagName="wucValidaPyme" %>
<%@ Register Src="~/SupplierModule/Controls/wucInformacionComplementaria.ascx" TagPrefix="uc1" TagName="wucInformacionComplementaria" %>






<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        var infoComp = "ctl00_MainContent_wizardPages_wucInformacionComplementaria_";
        var CapturaBasica = "ctl00_MainContent_wizardPages_wucCapturaBasica_";
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

        function HabilitaHorarios(inicio, fin, check1) {
            var casilla = document.getElementById(infoComp + check1);

            if (casilla.checked == true) {
                document.getElementById(infoComp + inicio).disabled = false;
                document.getElementById(infoComp + fin).disabled = false;
            } else {
                document.getElementById(infoComp + inicio).disabled = true;
                document.getElementById(infoComp + fin).disabled = true;
            }
        }

        function CalculaHorasTrabajadasDia(entrada, salida) {
            var horasTrabajadas;

            if ((entrada == 0.00) || (salida == 0.00)) {
                horasTrabajadas = 0.00;
            }
            else {
                if (entrada > salida) {
                    var horasEntrada = 24 - entrada;
                    var horasSalida = 0.00 + salida;

                    horasTrabajadas = horasEntrada + horasSalida;
                }

                else {
                    horasTrabajadas = salida - entrada;
                }
            }


            return horasTrabajadas;
        }

        function CalculaTotalHorasTrabajadas() {
            var totalHorasTrabajadas = 0.00;
            var entrada;
            var salida;
            var lunes = document.getElementById(infoComp + 'ChkLunes');
            var martes = document.getElementById(infoComp + 'ChkMartes');
            var miercoles = document.getElementById(infoComp + 'ChkMiercoles');
            var jueves = document.getElementById(infoComp + 'ChkJueves');
            var viernes = document.getElementById(infoComp + 'ChkViernes');
            var sabado = document.getElementById(infoComp + 'ChkSabado');
            var domingo = document.getElementById(infoComp + 'ChkDomingo');

            if (lunes.checked == true) {

                entrada = parseFloat(document.getElementById(infoComp + 'DDXInicioLunes').value);
                salida = parseFloat(document.getElementById(infoComp + 'DDXFinLunes').value);

                if (document.getElementById(infoComp + 'DDXInicioLunes').value == "")
                    entrada = 0.00;
                if (document.getElementById(infoComp + 'DDXFinLunes').value == "")
                    salida = 0.00;

                var horasDia = CalculaHorasTrabajadasDia(entrada, salida);
                totalHorasTrabajadas = totalHorasTrabajadas + horasDia;
                document.getElementById(infoComp + 'HiddenFieldLunes').value = horasDia;
            }

            if (martes.checked == true) {

                entrada = parseFloat(document.getElementById(infoComp + 'DDXInicioMartes').value);
                salida = parseFloat(document.getElementById(infoComp + 'DDXFinMartes').value);

                if (document.getElementById(infoComp + 'DDXInicioMartes').value == "")
                    entrada = 0.00;
                if (document.getElementById(infoComp + 'DDXFinMartes').value == "")
                    salida = 0.00;

                var horasDia2 = CalculaHorasTrabajadasDia(entrada, salida);
                totalHorasTrabajadas = totalHorasTrabajadas + horasDia2;
                document.getElementById(infoComp + 'HiddenFieldMartes').value = horasDia2;
            }

            if (miercoles.checked == true) {

                entrada = parseFloat(document.getElementById(infoComp + 'DDXInicioMiercoles').value);
                salida = parseFloat(document.getElementById(infoComp + 'DDXFinMiercoles').value);

                if (document.getElementById(infoComp + 'DDXInicioMiercoles').value == "")
                    entrada = 0.00;
                if (document.getElementById(infoComp + 'DDXFinMiercoles').value == "")
                    salida = 0.00;

                var horasDia3 = CalculaHorasTrabajadasDia(entrada, salida);
                totalHorasTrabajadas = totalHorasTrabajadas + horasDia3;
                document.getElementById(infoComp + 'HiddenFieldMiercoles').value = horasDia3;
            }

            if (jueves.checked == true) {

                entrada = parseFloat(document.getElementById(infoComp + 'DDXInicioJueves').value);
                salida = parseFloat(document.getElementById(infoComp + 'DDXFinJueves').value);

                if (document.getElementById(infoComp + 'DDXInicioJueves').value == "")
                    entrada = 0.00;
                if (document.getElementById(infoComp + 'DDXFinJueves').value == "")
                    salida = 0.00;

                var horasDia4 = CalculaHorasTrabajadasDia(entrada, salida);
                totalHorasTrabajadas = totalHorasTrabajadas + horasDia4;
                document.getElementById(infoComp + 'HiddenFieldJueves').value = horasDia4;
            }

            if (viernes.checked == true) {

                entrada = parseFloat(document.getElementById(infoComp + 'DDXInicioViernes').value);
                salida = parseFloat(document.getElementById(infoComp + 'DDXFinViernes').value);

                if (document.getElementById(infoComp + 'DDXInicioViernes').value == "")
                    entrada = 0.00;
                if (document.getElementById(infoComp + 'DDXFinViernes').value == "")
                    salida = 0.00;

                var horasDia5 = CalculaHorasTrabajadasDia(entrada, salida);
                totalHorasTrabajadas = totalHorasTrabajadas + horasDia5;
                document.getElementById(infoComp + 'HiddenFieldViernes').value = horasDia5;
            }

            if (sabado.checked == true) {

                entrada = parseFloat(document.getElementById(infoComp + 'DDXInicioSabado').value);
                salida = parseFloat(document.getElementById(infoComp + 'DDXFinSabado').value);

                if (document.getElementById(infoComp + 'DDXInicioSabado').value == "")
                    entrada = 0.00;
                if (document.getElementById(infoComp + 'DDXFinSabado').value == "")
                    salida = 0.00;

                var horasDia6 = CalculaHorasTrabajadasDia(entrada, salida);
                totalHorasTrabajadas = totalHorasTrabajadas + horasDia6;
                document.getElementById(infoComp + 'HiddenFieldSabado').value = horasDia6;
            }

            if (domingo.checked == true) {

                entrada = parseFloat(document.getElementById(infoComp + 'DDXInicioDomingo').value);
                salida = parseFloat(document.getElementById(infoComp + 'DDXFinDomingo').value);

                if (document.getElementById(infoComp + 'DDXInicioDomingo').value == "")
                    entrada = 0.00;
                if (document.getElementById(infoComp + 'DDXFinDomingo').value == "")
                    salida = 0.00;

                var horasDia7 = CalculaHorasTrabajadasDia(entrada, salida);
                totalHorasTrabajadas = totalHorasTrabajadas + horasDia7;
                document.getElementById(infoComp + 'HiddenFieldDomingo').value = horasDia7;
            }

            document.getElementById(infoComp + 'TxtHorasSemana').value = totalHorasTrabajadas.toFixed(1);
            CalculaHorasAnio();
        }

        function CalculaHorasAnio() {

            var horasSemana = parseFloat(document.getElementById(infoComp + 'TxtHorasSemana').value);
            var semanasAnio = parseFloat(document.getElementById(infoComp + 'TxtSemanasAnio').value);

            if (document.getElementById(infoComp + 'TxtHorasSemana').value == "")
                horasSemana = 0.00;

            if (document.getElementById(infoComp + 'TxtSemanasAnio').value == "")
                semanasAnio = 0.00;

            var horasAnio = horasSemana * semanasAnio;

            document.getElementById(infoComp + 'TxtHorasAnio').value = horasAnio.toFixed(1);
        }

        function HabilitaHorarios(inicio, fin, check1) {
            var casilla = document.getElementById(infoComp + check1);

            if (casilla.checked == true) {
                document.getElementById(infoComp + inicio).disabled = false;
                document.getElementById(infoComp + fin).disabled = false;
            } else {
                document.getElementById(infoComp + inicio).disabled = true;
                document.getElementById(infoComp + fin).disabled = true;
            }
        }


        function popupCalendar(v) {
            var dateField = document.getElementById(CapturaBasica + v);

            // toggle the div
            if (dateField.style.display == 'none')
                dateField.style.display = 'block';
            else
                dateField.style.display = 'none';
        }
        function popupCalendar2() {
            var dateField = document.getElementById(CapturaBasica + 'dateField2');

            // toggle the div
            if (dateField.style.display == 'none')
                dateField.style.display = 'block';
            else
                dateField.style.display = 'none';
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
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
                OnFinishButtonClick="wizardPages_FinishButtonClick">
                <FinishNavigationTemplate>
                    <asp:Button ID="btnFinishPre" runat="server" CssClass="Button" Text="Regresar" CommandName="MovePrevious"
                        OnClientClick="return confirm('¿ Desea Regresar a la Pantalla Previa ?');" />
                    <asp:Button ID="btnFinishCom" runat="server" CssClass="Button" Text="Guardar" CommandName="MoveComplete"
                        OnClientClick="this.style.display='none'; return true;" />
                    <asp:Button ID="btnCancel3" runat="server" CssClass="Button" Text="Salir" OnClick="btnCancel3_Click"
                        OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
                </FinishNavigationTemplate>
                <StartNavigationTemplate>
                    <div>
                        <asp:Button ID="btnStartNext" runat="server" Text="Siguiente" CssClass="Button" CommandName="MoveNext"
                            OnClientClick="return confirm('¿ Desea Continuar con el Registro de la Solicitud de Crédito ?');" />
                        <asp:Button ID="btnSalir" Text="Salir" runat="server" CssClass="Button" OnClick="btnSalir_Click"
                            OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
                    </div>
                </StartNavigationTemplate>
                <WizardSteps>
                    <asp:WizardStep runat="server" ID="wizValidaPyME" StepType="Start">
                        <uc1:wucValidaPyme runat="server" id="wucValidaPyme" />
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="wiz" StepType="Step">
                        <div>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/SupplierModule/images/t_presupuesto2.png" />
                            <asp:Label ID="Label3" runat="server" CssClass="Label_1"
                                Text="GENERACION DE PRESUPUESTO DE INVERSION" Visible="False"></asp:Label>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="wizCapturaBasica" StepType="Step">
                        <uc1:wucCapturaBasica runat="server" ID="wucCapturaBasica" />
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="wizValidaHistorialCrediticio" StepType="Step">
                       <%--   <uc1:wucValidacionCrediticia runat="server" ID="wucValidacionCrediticia" />--%>
                        <div id="divForm" runat="server" style="align-content: center">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/SupplierModule/images/t_validar_servicio.png" />
                                        <asp:Label ID="lblTitle" runat="server" Text="VALIDAR HISTORIAL CREDITICIO" CssClass="Title"
                                            Visible="false">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label" ForeColor="#333333">
                                            </asp:Label>
                                            &nbsp;
                            <asp:TextBox ID="txtFecha" runat="server" Enabled="false" BorderWidth="0" Font-Size="11pt">
                            </asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td width="180px" style="height: 25px">
                                        <asp:Label ID="Label2" runat="server" Text="Nombre o Razón Social" CssClass="Label"
                                            Width="165px" ForeColor="#666666" Font-Size="11pt">
                                        </asp:Label>
                                    </td>
                                    <td style="width: 200px; height: 25px;">
                                        <asp:TextBox ID="txtRazonSocial" runat="server" Font-Size="11px" CssClass="TextBox"
                                            Enabled="False" Width="200px">
                                        </asp:TextBox>
                                    </td>
                                    <td style="width: 115px; height: 25px;">
                                        <div>
                                        </div>
                                    </td>
                                    <td style="width: 150px; height: 25px;">
                                        <asp:Label ID="Label1" runat="server" Text="Número Crédito" CssClass="Label" Width="150px"
                                            ForeColor="#666666" Font-Size="11pt">
                                        </asp:Label>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtCreditoNum" runat="server" Font-Size="11px" CssClass="TextBox"
                                            Enabled="False" Width="200px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">&nbsp;
                                    </td>
                                    <td style="width: 200px">&nbsp;
                                    </td>
                                    <td style="width: 115px">&nbsp;
                                    </td>
                                    <td style="width: 150px">&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td style="width: 670px">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/SupplierModule/images/t6.png" />
                                    </td>
                                    <td>
                                        <img id="Img1" alt="" runat="server" src="~/SupplierModule/images/t12.png" width="166" height="21" />
                                        &nbsp;</img>&nbsp;</img>&nbsp;</img>&nbsp;</img></td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Monto a Financiar (MXP)" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label4"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtRequestAmount"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px"></td>
                                    <td>
                                         <asp:Button runat="server" Text="Formato Autorización" CssClass="Button" Width="200px"
                                            ID="btnDisplayCreditRequest" OnClick="btnDisplayCreditRequest_Click" />
                                           </td>
                                </tr>
                                <tr>
                                    <td style="width: 180px">
                                        <asp:Label runat="server" Text="Número de Pagos" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtCreditYearsNumber"></asp:TextBox>
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
                                            ForeColor="#666666" Width="150px" ID="Label6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtPaymentPeriod"></asp:TextBox>
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
                                                ForeColor="#666666" Width="150px" ID="lblImporte"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="Div2" runat="server" style="display: none">
                                            <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtContratoImporte"></asp:TextBox>
                                        </div>
                                    </td>
                                      <td style="width: 267px">
                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]*[1-9][0-9]*$"
                                            ControlToValidate="txtContratoImporte" ErrorMessage="(*) Campo Obligatorio" ID="revContratoImporte">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                      <asp:Button runat="server" Text="Solicitud" CssClass="Button" Width="200px" ID="btnDisplayRequest"
                                            OnClick="btnDisplayRequest_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField runat="server" Value="mop" ID="hiddenfield" />
                            <table width="100%">
                                <tr>
                                    <td class="style1" style="width: 70px">
                                          <asp:Button runat="server" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"
                                            Text="Salir" CssClass="Button" Width="70px" ID="btnSalirValidate" OnClick="btnSalirValidate_Click" /><br />
                                          </td>
                                    <td width="150px">
                                         <asp:Button runat="server" Text="Consulta Crediticia" Width="150px" ID="btnConsultaCrediticia"
                                            OnClick="btnConsultaCrediticia_Click" /><br />
                                          </td>
                                    <td>
                                        <asp:Label ID="lblMopInvalido" runat="server" Text='<%$ Resources:DefaultResource, MOPErrorInvalid %>' Visible="false"
                                            Style="font-size: x-large; color: #FF0000; font-weight: 700; background-color: #FFFF00"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:WizardStep>

                       <asp:WizardStep runat="server" ID="wizCapturaComplementaria" StepType="Step">
                           <uc1:wucInformacionComplementaria runat="server" ID="wucInformacionComplementaria" />
                    </asp:WizardStep>
                  <asp:WizardStep runat="server" ID="wizBajaEquipos" StepType="Step">
                      <uc1:WebUserControl1 runat="server" ID="WebUserControl1" />
                    </asp:WizardStep>
                </WizardSteps>
                <StepNavigationTemplate>
                    <asp:Button ID="btnStepPre" runat="server" Text="Regresar" CssClass="Button" CommandName="MovePrevious"
                        OnClientClick="return confirm('¿ Desea Regresar a la Pantalla Previa ?');" />
                    <asp:Button ID="btnStepNext" runat="server" Text="Siguiente" CssClass="Button" CommandName="MoveNext"
                        OnClientClick="return confirm('¿ Desea Continuar con la Generación del Presupuesto de Inversión correspondiente a ésta Solicitud de Crédito ?');" />
                    <asp:Button ID="BtnCacel" runat="server" Text="Salir" CssClass="Button" OnClick="BtnCacel_Click"
                        OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
                </StepNavigationTemplate>
            </asp:Wizard>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
