<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="ValidateCrediticialHistroy.aspx.cs"
    Inherits="PAEEEM.ValidateCrediticialHistroy" Title="Validación de Historial de Crédito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Resources/Script/Calendar/WdatePicker.js" type="text/javascript"></script>

    <link href="../Resources/Script/Calendar/skin/default/datepicker.css" type="text/css" />
   <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .Label
        {
            width: 20%;
            color: Maroon;
        }
        .titulo
        {
            color: #FFFFFF;
            font-size: 16px;
            background-image: url( '../Resources/Images/tec2.png' );
            background-repeat: no-repeat;
            background-position: center;
            border: 5px;
            border-color: #CCCCCC;
            width: 400px;
            height: 26px;
        }
        .Label1
        {
            width: 50%;
            color: Maroon;
        }
        .Label_1
        {
            color: Maroon;
            width: 13%;
        }
        .Title
        {
            text-align: center;
            font-weight: bold;
            font-size: 1em;
            color: #666666;
            font-variant: small-caps;
            text-transform: none;
            margin-bottom: 0px;
        }
        .tabla
        {
            text-align: left;
            color: #FFFFFF;
        }
        .TextBox
        {
            width: 22%;
        }
        .TextBox_1
        {
            width: 18.5%;
        }
        .DropDownList
        {
            width: 22%;
        }
        .DropDownList1
        {
            width: 12%;
        }
        .DropDownList_1
        {
            width: 30%;
        }
        .Button
        {
            width: 15%;
        }
        .Button_1
        {
            width: 28%;
        }
        .RadioButton
        {
            width: 15%;
        }
        .Disvisible
        {
            visibility: hidden;
        }
        </style>
    <%--<script type="text/javascript">
    function dismop()
    {
    var value; 
    value = window.prompt('Please Enter the Mop value to continue','');
    if(value!=null)
    {
        document.getElementById("<%=hiddenfield.ClientID%>").innerText=value;
    }
    else
    {
        document.getElementById("<%=hiddenfield.ClientID%>").innerText='';
    }
    }
    </script>--%>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <br>
                            <asp:Image runat="server" ImageUrl="images/t_validar_servicio.png" />
                            <asp:Label ID="lblTitle" runat="server" Text="VALIDAR HISTORIAL CREDITICIO" CssClass="Title"
                                Visible="false">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div align="right">
                            <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label" ForeColor="#333333">
                            </asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtFecha" runat="server" Enabled="false" BorderWidth="0" Font-Size="11pt">
                            </asp:TextBox>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
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
                        <asp:Label ID="Label3" runat="server" Text="Número Crédito" CssClass="Label" Width="150px"
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
                    <td width="180px">
                        &nbsp;
                    </td>
                    <td style="width: 200px">
                        &nbsp;
                    </td>
                    <td style="width: 115px">
                        &nbsp;
                    </td>
                    <td style="width: 150px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div>
                <asp:Wizard ID="wizardPages" runat="server" Style="width: 100%;" DisplaySideBar="false"
                    ActiveStepIndex="0" OnNextButtonClick="wizardPages_NextButtonClick" OnPreviousButtonClick="wizardPages_PreviousButtonClick"
                    Width="200px">
                    <FinishCompleteButtonStyle CssClass="Disvisible" />
                    <StartNavigationTemplate>
                        <div>
                            <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Siguiente"
                                OnClientClick="return confirm('¿ Desea Continuar con la Integración del Expediente ?')" />
                        </div>
                    </StartNavigationTemplate>
                    <StepNavigationTemplate>
                        <div>
                            <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Siguiente"
                                OnClientClick="return confirm('¿ Desea Continuar con la Integración del Expediente ?')" />
                        </div>
                    </StepNavigationTemplate>
                    <WizardSteps>
                        <asp:WizardStep runat="server" ID="Validate" StepType="Start" Title="Validate Crediticial Histroy">
                            <table width="100%">
                                <tr>
                                    <td style="width: 670px">
                                        <asp:Image runat="server" ImageUrl="images/t6.png" />
                                    </td>
                                    <td>
                                        <img alt="" src="images/t12.png" width="166" height="21"></img>
                                    </td>
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
                                    <td style="width: 100px">
                                    </td>
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
                                    <td>
                                    </td>
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
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" Text="Carta Presupuesto" CssClass="Button" Width="200px"
                                            ID="btnDisplayQuota" OnClick="btnDisplayQuota_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="display:none">
                                        <asp:Label runat="server" Text="Importe Contrato" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="lblImporte"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div style="display:none">
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
                                            style="font-size: x-large; color: #FF0000; font-weight: 700; background-color: #FFFF00"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:WizardStep>
                        <asp:WizardStep runat="server" ID="Integrate" StepType="Step" Title="Integrate Credit Documentation">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Image runat="server" ImageUrl="images/arrow.png" />
                                        <asp:Label runat="server" Text="PODER NOTARIAL DEL REPRESENTANTE LEGAL" CssClass="Label1"
                                            ForeColor="#333333" Width="600px" ID="Label10"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: small; color: #0499B6">
                                        <img alt="" src="images/notice.png" width="22" height="20" />Sólo sí el Beneficiario es una
                                        Persona Física y cuenta con un Representante Legal, completar la siguiente
                                        información
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td width="180px">
                                        <asp:Label runat="server" Text="Número Escritura" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label11"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtRepresentative_LegalDocumentNumber"></asp:TextBox>
                                    </td>
                                    <td style="width: 115px">
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" Text="Fecha" CssClass="Label" Font-Size="11pt" ForeColor="#666666"
                                            Width="150px" ID="Label19"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="Wdate" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtRepresentative_LegalDocumentFecha"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">
                                        <asp:Label runat="server" Text="Nombre Completo del Notario" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="160px" ID="Label12"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtRepresentative_NotariesProfessionName"></asp:TextBox>
                                    </td>
                                    <td style="width: 115px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" Text="Número Notaría" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label20"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtRepresentative_NotariesProfessionNumber"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">
                                        <asp:Label runat="server" Text="Estado" CssClass="Label" Font-Size="11pt" ForeColor="#666666"
                                            Width="150px" ID="Label13"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:DropDownList runat="server" AutoPostBack="True" ValidationGroup="Siguiente"
                                            CssClass="DropDownList" Font-Size="11px" ToolTip="(*) Campo Obligatorio" Width="200px"
                                            ID="drpRepresentative_Estado" OnSelectedIndexChanged="drpRepresentative_Estado_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 115px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" Text="Delegación o Municipio" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label21"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ValidationGroup="Siguiente" CssClass="DropDownList"
                                            Font-Size="11px" ToolTip="(*) Campo Obligatorio" Width="200px" ID="drpRepresentative_OfficeorMunicipality">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Image runat="server" ImageUrl="images/arrow.png" />
                                        <asp:Label runat="server" Text="ACTA CONSTITUTIVA (PERSONA MORAL)" CssClass="Label1"
                                            ForeColor="#333333" ID="Label14"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td width="180px">
                                        <asp:Label runat="server" Text="Número Escritura" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label15"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtApplicant_LegalDocumentNumber"></asp:TextBox>
                                    </td>
                                    <td style="width: 115px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" Text="Fecha" CssClass="Label" Font-Size="11pt" ForeColor="#666666"
                                            Width="150px" ID="Label23"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="Wdate" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtApplicant_LegalDocumentFecha"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">
                                        <asp:Label runat="server" Text="Nombre Completo del Notario" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="160px" ID="Label16"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtApplicant_NotariesProfessionName"></asp:TextBox>
                                    </td>
                                    <td style="width: 115px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" Text="Número Notaría" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label24"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtApplicant_NotariesProfessionNumber"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">
                                        <asp:Label runat="server" Text="Estado" CssClass="Label" Font-Size="11pt" ForeColor="#666666"
                                            Width="150px" ID="Label17"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:DropDownList runat="server" AutoPostBack="True" ValidationGroup="Siguiente"
                                            CssClass="DropDownList" Font-Size="11px" ToolTip="(*) Campo Obligatorio" Width="200px"
                                            ID="drpApplicant_Estado" OnSelectedIndexChanged="drpApplicant_Estado_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 115px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" Text="Delegación o Municipio" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label25"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ValidationGroup="Siguiente" CssClass="DropDownList"
                                            Font-Size="11px" ToolTip="(*) Campo Obligatorio" Width="200px" ID="drpApplicant_OfficeOrMunicipality">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Image runat="server" ImageUrl="images/arrow.png" />
                                        <asp:Label runat="server" Text="ESCRITURA DEL INMUEBLE Y ACTA DE MATRIMONIO DEL OBLIGADO SOLIDARIO"
                                            CssClass="Label1" ForeColor="#333333" Width="600px" ID="Label18"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td width="180px">
                                        <asp:Label runat="server" Text="Número Escritura" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label22"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtGuarantee_LegalDocumentNumber"></asp:TextBox>
                                    </td>
                                    <td style="width: 115px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" Text="Fecha" CssClass="Label" Font-Size="11pt" ForeColor="#666666"
                                            Width="150px" ID="Label27"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="Wdate" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtGuarantee_LegalDocumentFecha"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">
                                        <asp:Label runat="server" Text="Nombre Completo del Notario" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="160px" ID="Label26"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtGuarantee_NotariesProfessionName"></asp:TextBox>
                                    </td>
                                    <td style="width: 115px">
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" Text="Número Notaría" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label28"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ValidationGroup="Siguiente" CssClass="TextBox" Font-Size="11px"
                                            ToolTip="(*) Campo Obligatorio" Width="200px" ID="txtGuarantee_NotariesProfessionNumber"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">
                                        <asp:Label runat="server" Text="Estado" CssClass="Label" Font-Size="11pt" ForeColor="#666666"
                                            Width="150px" ID="Label29"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:DropDownList runat="server" AutoPostBack="True" ValidationGroup="Siguiente"
                                            CssClass="DropDownList" Font-Size="11px" ToolTip="(*) Campo Obligatorio" Width="200px"
                                            ID="drpGuarantee_Estado" OnSelectedIndexChanged="drpGuarantee_Estado_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 115px">
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" Text="Delegación o Municipio" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label30"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ValidationGroup="Siguiente" CssClass="DropDownList"
                                            Font-Size="11px" ToolTip="(*) Campo Obligatorio" Width="200px" ID="drpGuarantee_OfficeOrMunicipality">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">
                                        <asp:Label runat="server" Text="Fecha Liberación Gravamen" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label31"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox runat="server" CssClass="Wdate" Font-Size="11px" Width="200px" ID="txtPropertyEncumbrancesFecha"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                                    </td>
                                    <td style="width: 115px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" Text="Emitido por" CssClass="Label" Font-Size="11pt" ForeColor="#666666"
                                            Width="150px" ID="Label32"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtPropertyEncumbrancesName"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">
                                        <asp:Label runat="server" Text="Num. Acta de Matrimonio" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label33"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtMarriageNumber"></asp:TextBox>
                                    </td>
                                    <td style="width: 115px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Label runat="server" Text="Número Registro Civil" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label34"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtCitizenRegisterOffice"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div align="left">
                                <asp:Button runat="server" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"
                                    Text="Salir" CssClass="Button" ID="btnSalirIntegrate" OnClick="btnSalirIntegrate_Click" /></div>
                            <br />
                        </asp:WizardStep>
                        <asp:WizardStep runat="server" ID="Replacement" StepType="Step" Title="Adquisición o Sustitución">
                            <asp:Image runat="server" ImageUrl="images/t10.png" />
                            <asp:Label runat="server" CssClass="Label1" ID="Label36"></asp:Label>
                            <div align="left">
                                <br>
                                    <asp:RadioButton runat="server" GroupName="gpReplacement" AutoPostBack="True" Text="Adquisición de Equipo"
                                        CssClass="RadioButton" ID="radAcquisition" Visible="False" OnCheckedChanged="radAcquisition_CheckedChanged" />
                                    <asp:RadioButton runat="server" GroupName="gpReplacement" AutoPostBack="True" Checked="True"
                                        Text="Sustitución de Equipo" CssClass="RadioButton" ID="radReplacement" OnCheckedChanged="radReplacement_CheckedChanged" />
                            </div>
                            <br />
                            <img alt="" src="images/arrow.png" width="16" height="16">
                                <asp:Label runat="server" Text="Información Equipo Baja Eficiencia a Disponer" CssClass="Label1"
                                    ForeColor="#333333" ID="Label37"></asp:Label>
                                <br />
                                <br />
                                <div>
                                    <asp:GridView runat="server" AutoGenerateColumns="False" DataKeyNames="KeyNumber"  CssClass="GridViewStyle"
                                        BorderStyle="None" Width="100%" ID="gdvReplacement" OnDataBound="gdvReplacement_DataBound">
                                         <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Tecnología">
                                                <ItemTemplate>
                                                    <%--Update by Tina 2011/08/03--%><asp:DropDownList ID="drpTechnology" runat="server"
                                                        Width="100%" AutoPostBack="True" OnSelectedIndexChanged="drpTechnology_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--End--%>
                                                </ItemTemplate>
                                                 <ItemStyle Width="13%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CAyD">
                                                <ItemTemplate>
                                                    <%--Update by Tina 2011/08/03--%><asp:DropDownList ID="drpDisposalCenter" runat="server" AutoPostBack="true"
                                                        Width="100%" OnSelectedIndexChanged="drpDisposalCenter_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--End--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="14%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Tipo Producto">
                                                <ItemTemplate>
                                                    <%--Update by Tina 2012/04/12--%>
                                                    <asp:DropDownList ID="drpProductType" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drpProductType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--End--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="12%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Modelo">
                                                <ItemTemplate>
                                                    <%-- edit by coco 2012-07-17--%>
<%--                                                    <asp:DropDownList ID="drpModeloold" runat="server" Width="100%">
                                                    </asp:DropDownList>--%>
                                                    <asp:TextBox ID="TextModelo" runat="server" Width="100%"></asp:TextBox>
                                                    <%-- end edit--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Marca">
                                                <ItemTemplate>
                                                    <%-- edit by coco 2012-07-17--%>
                                                    <%--<asp:DropDownList ID="drpMarcaold" runat="server" Width="100%">
                                                    </asp:DropDownList>--%>
                                                    <asp:TextBox ID="TextMarca" runat="server"  Width="100%"></asp:TextBox>
                                                    <%-- end edit--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtColor" runat="server" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Peso">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPeso" runat="server" Width="50%">
                                                    </asp:TextBox><asp:Label ID="lblPesoUnit" runat="server" Text="Kgs."> <%--added by tina 2012/04/12--%>
                                                    </asp:Label><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                                        ControlToValidate="txtPeso" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <ItemStyle Width="11%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Capacidad">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpCapacidad" runat="server"
                                                        Width="100%">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Antiguedad">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAntiguedad" runat="server" Width="40%"></asp:TextBox>&nbsp;Años
                                                </ItemTemplate>
                                                <ItemStyle Width="8%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td width="150px">
                                            <br />
                                        </td>
                                        <td width="30px">
                                            <asp:Button runat="server" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"
                                                Text="Salir" CssClass="Button" Width="50px" ID="btnSalirReplacement" OnClick="btnSalirReplacement_Click" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </img></asp:WizardStep>
                        <asp:WizardStep runat="server" ID="Print" StepType="Finish" Title="Impresión de Expediente">
                            <img alt="" src="images/arrow.png" width="16" height="16">
                                <asp:Label runat="server" Text="DOCUMENTACION" CssClass="Label" ForeColor="#333333"
                                    ID="Label44"></asp:Label>
                                <br />
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Check List de Crédito" CssClass="Button_1" ID="btnDisplayCreditCheckList"
                                    OnClick="btnDisplayCreditCheckList_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Contrato de Financiamiento" CssClass="Button_1"
                                    ID="btnDisplayCreditContract" OnClick="btnDisplayCreditContract_Click" /><br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Acta Entrega - Recepción de Equipos" CssClass="Button_1"
                                    ID="btnDisplayEquipmentAct" OnClick="btnDisplayEquipmentAct_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Solicitud Crédito" CssClass="Button_1" ID="btnDisplayCreditRequest1"
                                    OnClick="btnDisplayCreditRequest1_Click" /><br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Pagaré" CssClass="Button_1" ID="btnDisplayPromissoryNote"
                                    OnClick="btnDisplayPromissoryNote_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Endoso en Garantía" CssClass="Button_1" ID="btnDisplayGuaranteeEndorsement"
                                    OnClick="btnDisplayGuaranteeEndorsement_Click" /><br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Carta Presupuesto de Inversión" CssClass="Button_1"
                                    ID="btnDisplayQuota1" OnClick="btnDisplayQuota1_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Carta Compromiso Obligado Solidario" CssClass="Button_1"
                                    ID="btnDisplayGuarantee" OnClick="btnDisplayGuarantee_Click" /><br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Tabla de Amortización" CssClass="Button_1" ID="btnDisplayRepaymentSchedule"
                                    OnClick="btnDisplayRepaymentSchedule_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Recibo de Incentivo Energético (Descuento)" CssClass="Button_1" Enabled="false"
                                    ID="btnDisplayDisposalBonusReceipt" OnClick="btnDisplayDisposalBonusReceipt_Click" /><br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Pre-Boleta CAyD" CssClass="Button_1" ID="btnDisplayReceiptToSettle" Enabled="false" 
                                    OnClick="btnDisplayReceiptToSettle_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" Text="Tabla de Amortización - Pagaré" CssClass="Button_1"
                                    ID="BtnAmortPag" OnClick="btnAmortPag_Click" /><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnPrint" runat="server" CssClass="Button_1" Enabled="false"
                                OnClick="btnPrint_Click" Text="Boleta Ingreso Equipo" Visible="False" />
                                <br />
                                <asp:Button runat="server" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"
                                    Text="Salir" CssClass="Button" ID="btnSalirPrint" OnClick="btnSalirPrint_Click" /></img></asp:WizardStep>
                    </WizardSteps>
                    <StepNavigationTemplate>
                        <asp:Button ID="StepPreviousButton" runat="server" CommandName="MovePrevious" Text="Regresar"
                            OnClientClick="return confirm('¿ Desea Regresar a la Pantalla Previa ?')" Visible="false" />
                        <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Siguiente"
                            OnClientClick="var res = confirm('¿ Desea Continuar con la Integración del Expediente ?'); if (res) { this.style.display = 'none'; } else { return false; }" />
                    </StepNavigationTemplate>
                    <FinishNavigationTemplate>
                        <asp:Button ID="FinishPreviousButton" runat="server" CommandName="MovePrevious" Text="Regresar"
                            OnClientClick="return confirm('¿ Desea Regresar a la Pantalla Previa ?')" Visible="false" />
                    </FinishNavigationTemplate>
                </asp:Wizard>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
