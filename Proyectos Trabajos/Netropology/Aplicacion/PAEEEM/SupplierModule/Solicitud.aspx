<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Solicitud.aspx.cs" Inherits="PAEEEM.CentralModule.Solicitud" MasterPageFile="../Site.Master"%>

<%@ Register Src="~/SupplierModule/Controls/wucValidaPyme.ascx" TagPrefix="uc1" TagName="wucValidaPyme" %>
<%@ Register Src="~/SupplierModule/Controls/wucCapturaBasica.ascx" TagPrefix="uc1" TagName="wucCapturaBasica" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
         <%--      <div id="lock"  class="LockOff">
                <img src="../images/progress.gif" alt="¡ En Proceso, Por favor Espere !" style="vertical-align: middle; position: relative;" />
            </div>--%>
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
                    <div align="right">
                        <asp:Button ID="btnStartNext" runat="server" Text="Siguiente" CssClass="Button" CommandName="MoveNext"
                            OnClientClick="return confirm('¿ Desea Continuar con el Registro de la Solicitud de Crédito ?');" />
                        <asp:Button ID="btnSalir" Text="Salir" runat="server" CssClass="Button" OnClick="btnSalir_Click"
                            OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
                    </div>
                </StartNavigationTemplate>
                <WizardSteps>
                    <asp:WizardStep runat="server" ID="wizValidaPyME" StepType="Start">
                        <div id="divCreditRequest">
                            <br>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Image runat="server" ImageUrl="~/SupplierModule/images/t_alta.png" ID="imgAlta1" />
                                        <asp:Image runat="server" ImageUrl="~/SupplierModule/images/t_edicioncredito.png" ID="imgEdit1" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div align="left">
                                            <asp:Label ID="lblCredito1" Text="No. Crédito" runat="server" CssClass="Label" Width="165px" ForeColor="#333333" Visible="False" />
                                            <asp:TextBox ID="txtCredito1" runat="server" CssClass="TextBox" Width="250px" Enabled="false" Visible="False" />
                                        </div>
                                    </td>
                                    <td align="right">
                                        <asp:Label runat="server" Text="Fecha" CssClass="Label_2" ID="lblDate"></asp:Label>
                                        <asp:Label runat="server" ID="lbbNowdate"></asp:Label>
                                    </td>
                                </tr>
                                <%--<uc1:wucValidaPyme runat="server" ID="wucValidaPyme" />--%>
                            </table>
                            <uc1:wucCapturaBasica ID="wucCapturaBasica3" runat="server" />
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="wizPresupuestoInversion" StepType="Step">
                        <div>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/SupplierModule/images/t_presupuesto2.png" />
                            <asp:Label ID="Label3" runat="server" CssClass="Label_1"
                                Text="GENERACION DE PRESUPUESTO DE INVERSION" Visible="False"></asp:Label>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" ID="wizCapturaComplementaria" StepType="Step">
                        <uc1:wucCapturaBasica runat="server" ID="wucCapturaBasica" />
                    </asp:WizardStep>
                        <asp:WizardStep runat="server" ID="wizCapturaEquiposBaja" StepType="Step">
                        <uc1:wucCapturaBasica runat="server" ID="wucCapturaBasica1" />
                    </asp:WizardStep>
                        <asp:WizardStep runat="server" ID="wizCapturaEquiposAlta" StepType="Finish">
                        <uc1:wucCapturaBasica runat="server" ID="wucCapturaBasica2" />
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
