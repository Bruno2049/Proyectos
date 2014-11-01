<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidateCrediticialHistroy.aspx.cs"
    Inherits="PAEEEM.ValidateCrediticialHistroy" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Resources/Script/Calendar/WdatePicker.js" type="text/javascript"></script>

    <link href="../Resources/Script/Calendar/skin/default/datepicker.css" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 20%;
            color: Maroon;
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
            <div>
                <asp:Label ID="lblTitle" runat="server" Text="VALIDAR HISTORIAL CREDITICIO" CssClass="Title"></asp:Label>
                <br />
                <div align="right">
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtFecha" runat="server" Enabled="false" BorderWidth="0"></asp:TextBox></div>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Nombre o razón social" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="TextBox" Enabled="False"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="Número de Crédito" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtCreditoNum" runat="server" CssClass="TextBox" Enabled="False"></asp:TextBox>
                <br />
                <br />
            </div>
            <div>
                <asp:Wizard ID="wizardPages" runat="server" Style="width: 100%;" DisplaySideBar="false"
                    ActiveStepIndex="0" OnNextButtonClick="wizardPages_NextButtonClick" 
                    OnPreviousButtonClick="wizardPages_PreviousButtonClick">
                    <FinishCompleteButtonStyle CssClass="Disvisible" />
                    <StartNavigationTemplate>
                        <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Siguiente"
                            OnClientClick="return confirm('¿ Desea continuar con la Integración del Expediente ?')" />
                    </StartNavigationTemplate>
                    <StepNavigationTemplate>
                        <asp:Button ID="StepPreviousButton" runat="server" CommandName="MovePrevious" Text="Regresar"
                            OnClientClick="return confirm('¿ Desea regresar a la pantalla previa ?')" />
                        <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Siguiente"
                            OnClientClick="return confirm('¿ Desea continuar con la Integración del Expediente ?')" />
                    </StepNavigationTemplate>
                    <FinishNavigationTemplate>
                        <asp:Button ID="FinishPreviousButton" runat="server" CommandName="MovePrevious" Text="Regresar"
                            OnClientClick="return confirm('¿ Desea regresar a la pantalla previa ?')" />
                    </FinishNavigationTemplate>
                    <WizardSteps>
                        <asp:WizardStep ID="Validate" runat="server" Title="Validate Crediticial Histroy"
                            StepType="Start">
                            <%--Update by Tina 2011/08/03--%>
                            <asp:Label ID="Label1" runat="server" Text="DATOS DEL FINANCIAMIENTO" CssClass="Label1"></asp:Label><br />
                            <asp:Label ID="Label4" runat="server" Text="Monto a Financiar (MXP)" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtRequestAmount" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDisplayCreditRequest" runat="server" Text="Formato Autorización"
                                CssClass="Button" OnClick="btnDisplayCreditRequest_Click" />
                            <asp:Button ID="btnDisplayPaymentSchedule" runat="server" Text="Tabla de Amortización"
                                CssClass="Button" OnClick="btnDisplayPaymentSchedule_Click" />
                            <br />
                            <asp:Label ID="Label5" runat="server" Text="Plazo del Crédito" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtCreditYearsNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDisplayQuota" runat="server" Text="Carta Presupuesto" CssClass="Button"
                                OnClick="btnDisplayQuota_Click" />


                            <asp:Button ID="btnDisplayRequest" runat="server" CssClass="Button" 
                                OnClick="btnDisplayRequest_Click" Text="Solicitud" />

                            <br />
                            <asp:Label ID="Label6" runat="server" Text="Periodicidad de Pago" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPaymentPeriod" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            <br />
                            <br />
                            <asp:Label ID="lblImporte" runat="server" Text="Importe Contrato" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtContratoImporte" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;
                            <asp:RegularExpressionValidator ID="revContratoImporte" runat="server" ErrorMessage="*"
                                ControlToValidate="txtContratoImporte" 
                                ValidationExpression="^[0-9]*[1-9][0-9]*$"></asp:RegularExpressionValidator>
                            <asp:HiddenField ID="hiddenfield" runat="server" Value="mop" />
                            <br />
                            <asp:Button ID="btnSalirValidate" runat="server" Text="Salir" CssClass="Button" OnClientClick="return confirm('¿ Desea salir de ésta pantalla ?');"
                                OnClick="btnSalirValidate_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnConsultaCrediticia" runat="server" Text="Consulta Crediticia"
                                OnClick="btnConsultaCrediticia_Click" />
                        </asp:WizardStep>
                        <asp:WizardStep ID="Integrate" runat="server" Title="Integrate Credit Documentation"
                            StepType="Step">
                            <asp:Label ID="Label10" runat="server" Text="PODER NOTARIAL DEL REPRESENTANTE LEGAL"
                                CssClass="Label1"></asp:Label>
                            <br />
                            <asp:Label ID="Label11" runat="server" Text="Num. de Escritura" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtRepresentative_LegalDocumentNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label19" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtRepresentative_LegalDocumentFecha" runat="server" CssClass="Wdate"
                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label12" runat="server" Text="Nombre del Notario" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtRepresentative_NotariesProfessionName" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label20" runat="server" Text="Num. de Notario" CssClass="Label"></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtRepresentative_NotariesProfessionNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label13" runat="server" Text="Estado" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="drpRepresentative_Estado" runat="server" 
                                CssClass="DropDownList" AutoPostBack="True" 
                                OnSelectedIndexChanged="drpRepresentative_Estado_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label21" runat="server" Text="Delegación o Municipio" CssClass="Label"></asp:Label>
                            &nbsp;
                            <asp:DropDownList ID="drpRepresentative_OfficeorMunicipality" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Label ID="Label14" runat="server" Text="ACTA CONSTITUTIVA (PERSONA MORAL)" CssClass="Label1"></asp:Label>
                            <br />
                            <asp:Label ID="Label15" runat="server" Text="Num. de Escritura" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtApplicant_LegalDocumentNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label23" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtApplicant_LegalDocumentFecha" runat="server" CssClass="Wdate"
                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label16" runat="server" Text="Nombre del Notario" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtApplicant_NotariesProfessionName" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label24" runat="server" Text="Num. de Notario" CssClass="Label"></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtApplicant_NotariesProfessionNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label17" runat="server" Text="Estado" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="drpApplicant_Estado" runat="server" CssClass="DropDownList"
                                AutoPostBack="True" OnSelectedIndexChanged="drpApplicant_Estado_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label25" runat="server" Text="Delegación o Municipio" CssClass="Label"></asp:Label>
                            &nbsp;
                            <asp:DropDownList ID="drpApplicant_OfficeOrMunicipality" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Label ID="Label18" runat="server" Text="ESCRITURA DEL INMUEBLE Y ACTA DE MATRIMONIO DEL AVAL"
                                CssClass="Label1"></asp:Label>
                            <br />
                            <asp:Label ID="Label22" runat="server" Text="Num. de Escritura" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtGuarantee_LegalDocumentNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label27" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtGuarantee_LegalDocumentFecha" runat="server" CssClass="Wdate"
                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label26" runat="server" Text="Nombre del Notario" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtGuarantee_NotariesProfessionName" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label28" runat="server" Text="Num. de Notario" CssClass="Label"></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtGuarantee_NotariesProfessionNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label29" runat="server" Text="Estado" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="drpGuarantee_Estado" runat="server" CssClass="DropDownList"
                                AutoPostBack="True" OnSelectedIndexChanged="drpGuarantee_Estado_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label30" runat="server" Text="Delegación o Municipio" CssClass="Label"></asp:Label>
                            &nbsp;
                            <asp:DropDownList ID="drpGuarantee_OfficeOrMunicipality" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="Label31" runat="server" Text="Fecha Libertad de Gravamen" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPropertyEncumbrancesFecha" runat="server" CssClass="Wdate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label32" runat="server" Text="Emitido por" CssClass="Label"></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtPropertyEncumbrancesName" runat="server" CssClass="TextBox"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label33" runat="server" Text="Num. Acta de Matrimonio" CssClass="Label"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtMarriageNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label34" runat="server" Text="Registro Civil" CssClass="Label"></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtCitizenRegisterOffice" runat="server" CssClass="TextBox"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Button ID="btnSalirIntegrate" runat="server" Text="Salir" OnClientClick="return confirm('¿ Desea salir de ésta pantalla ?');"
                                CssClass="Button" OnClick="btnSalirIntegrate_Click" />
                        </asp:WizardStep>
                        <asp:WizardStep ID="Replacement" runat="server" Title="Acquisition or Replacement"
                            StepType="Step">
                            <asp:Label ID="Label36" runat="server" Text="DATOS DEL O LOS EQUIPOS A DISPONER"
                                CssClass="Label1"></asp:Label>
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="radAcquisition" runat="server" OnCheckedChanged="radAcquisition_CheckedChanged"
                                CssClass="RadioButton" GroupName="gpReplacement" AutoPostBack="True" Text="Adquisición" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radReplacement" Checked="true"
                                runat="server" OnCheckedChanged="radReplacement_CheckedChanged" CssClass="RadioButton"
                                GroupName="gpReplacement" AutoPostBack="True" Text="Sustitución" />
                            <br />
                            <br />
                            <asp:Label ID="Label37" runat="server" Text="Información Disposición Equipo" CssClass="Label1"></asp:Label>
                            <br />
                            <br />
                            <asp:GridView ID="gdvReplacement" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnDataBound="gdvReplacement_DataBound" BorderStyle="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="Tecnología">
                                        <ItemTemplate>
                                            <%--Update by Tina 2011/08/03--%>
                                            <asp:DropDownList ID="drpTechnology" runat="server" Width="100%" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpTechnology_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <%--End--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="35%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Número de Unidades">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUnidades" runat="server" Width="40%" Style="border-bottom-width: 0;
                                                border-left-width: 0; border-top-width: 0; border-right-width: thin"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="revUnidades" runat="server" ControlToValidate="txtUnidades"
                                                ErrorMessage="No es un valor válido." ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Centro de Disposición">
                                        <ItemTemplate>
                                            <%--Update by Tina 2011/08/03--%>
                                            <asp:DropDownList ID="drpDisposalCenter" runat="server" Width="100%" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <%--End--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="35%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnAddRecord" runat="server" Text="Agregar Tecnología" OnClick="btnAddRecord_Click"
                                CssClass="Button" />
                            <br />
                            <br />
                            <asp:Button ID="btnSalirReplacement" runat="server" Text="Salir" OnClientClick="return confirm('¿ Desea salir de ésta pantalla ?');"
                                CssClass="Button" OnClick="btnSalirReplacement_Click" />
                        </asp:WizardStep>
                        <asp:WizardStep ID="Print" runat="server" Title="Print Documentation" StepType="Finish">
                            <asp:Label ID="Label44" runat="server" Text="DOCUMENTACION" CssClass="Label"></asp:Label>
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDisplayCreditCheckList" runat="server" Text="Check List de Crédito"
                                CssClass="Button_1" OnClick="btnDisplayCreditCheckList_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDisplayCreditContract" runat="server" Text="Contrato de Financiamiento"
                                CssClass="Button_1" OnClick="btnDisplayCreditContract_Click" />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDisplayEquipmentAct" runat="server" Text="Acta Entrega - Recepción de Equipos"
                                CssClass="Button_1" OnClick="btnDisplayEquipmentAct_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDisplayCreditRequest1" runat="server" Text="Solicitud"
                                CssClass="Button_1" OnClick="btnDisplayCreditRequest1_Click" />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDisplayPromissoryNote" runat="server" Text="Pagaré" CssClass="Button_1"
                                OnClick="btnDisplayPromissoryNote_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDisplayGuaranteeEndorsement" runat="server" Text="Endoso en Garantía"
                                CssClass="Button_1" OnClick="btnDisplayGuaranteeEndorsement_Click" />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDisplayQuota1" runat="server" Text="Carta Presupuesto de Inversión"
                                CssClass="Button_1" OnClick="btnDisplayQuota1_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDisplayGuarantee" runat="server" Text="Carta Compromiso Aval"
                                CssClass="Button_1" OnClick="btnDisplayGuarantee_Click" />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDisplayRepaymentSchedule" runat="server" Text="Tabla de Amortización"
                                CssClass="Button_1" OnClick="btnDisplayRepaymentSchedule_Click" />                            
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDisplayDisposalBonusReceipt" runat="server" Text="Recibo de Bono de Chatarrización"
                                CssClass="Button_1" OnClick="btnDisplayDisposalBonusReceipt_Click" />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="btnDisplayReceiptToSettle" runat="server" Text="Equipo Baja Eficiencia"
                                CssClass="Button_1" OnClick="btnDisplayReceiptToSettle_Click" 
                                Visible="True" />

                            <br />
                            <br />
                            <asp:Button ID="btnSalirPrint" runat="server" Text="Salir" CssClass="Button" OnClientClick="return confirm('¿ Desea salir de ésta pantalla ?');"
                                OnClick="btnSalirPrint_Click" />
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
