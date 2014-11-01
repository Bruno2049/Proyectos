<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrediticialHistroyReview.aspx.cs"
    Inherits="PAEEEM.CrediticialHistroyReview" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center">
        <h6>
            &nbsp;INFORMACIÓN DE LA SOLICITUD DE CRÉDITO</h6>
        <br />
    </div>
    <div align="right">
        <asp:Label ID="lblFecha" Text="Fecha" runat="server" CssClass="Label" />
        <asp:TextBox ID="txtFecha" runat="server" BorderWidth="0" Enabled="false"></asp:TextBox>
    </div>
    <br />
    <asp:Panel ID="panelValidate" runat="server" GroupingText="DATOS DEL FINANCIAMIENTO">
        <asp:Label ID="Label1" runat="server" Text="DATOS DEL FINANCIAMIENTO" CssClass="Label1"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Monto a Financiar (MXP)" CssClass="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtRequestAmount" runat="server" CssClass="TextBox"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayCreditRequest" runat="server" Text="Solicitud de Crédito"
            CssClass="Button" OnClick="btnDisplayCreditRequest_Click" />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Plazo del Crédito" CssClass="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtCreditYearsNumber" runat="server" CssClass="TextBox"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayQuota" runat="server" Text="Carta Presupuesto" CssClass="Button"
            OnClick="btnDisplayQuota_Click" />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Periodicidad de Pago" CssClass="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtPaymentPeriod" runat="server" CssClass="TextBox"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayPaymentSchedule" runat="server" Text="Tabla de Amortización"
            CssClass="Button" OnClick="btnDisplayPaymentSchedule_Click" />
    </asp:Panel>
    <asp:Panel ID="panelIntegrate" runat="server" GroupingText="EXPEDIENTE DEL CREDITO">
        <asp:Label ID="Label10" runat="server" Text="PODER NOTARIAL DEL REPRESENTANTE LEGAL"
            CssClass="Label1"></asp:Label>
        <br />
        <asp:Label ID="Label11" runat="server" Text="Num. de Escritura" CssClass="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtRepresentative_LegalDocumentNumber" runat="server" CssClass="TextBox"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label19" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
        &nbsp;
        <asp:TextBox ID="txtRepresentative_LegalDocumentFecha" runat="server" CssClass="TextBox"></asp:TextBox>
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
        <asp:DropDownList ID="drpRepresentative_Estado" runat="server" CssClass="DropDownList"
            >
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
        <asp:TextBox ID="txtApplicant_LegalDocumentFecha" runat="server" CssClass="TextBox"></asp:TextBox>
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
            >
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
        <asp:TextBox ID="txtGuarantee_LegalDocumentFecha" runat="server" CssClass="TextBox"></asp:TextBox>
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
            >
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label30" runat="server" Text="Delegación o Municipio" CssClass="Label"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="drpGuarantee_OfficeOrMunicipality" runat="server" CssClass="DropDownList">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label31" runat="server" Text="Fecha Libertad de Gravamen" CssClass="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtPropertyEncumbrancesFecha" runat="server" CssClass="TextBox"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
    </asp:Panel>
    <asp:Panel ID="panelReplacement" runat="server" GroupingText="DATOS DEL O LOS EQUIPOS A DISPONER">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="radAcquisition" runat="server" 
            CssClass="RadioButton" GroupName="gpReplacement" AutoPostBack="True" Text="Adquisición" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radReplacement" Checked="true"
            runat="server" CssClass="RadioButton"
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
                        <asp:DropDownList ID="drpTechnology" runat="server" Width="100%">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle Width="40%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Número de Unidades">
                    <ItemTemplate>
                        <asp:TextBox ID="txtUnidades" runat="server" Width="100%" Style="border-bottom-width: 0;
                            border-left-width: 0; border-top-width: 0; border-right-width: 0; text-align:center;"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Centro de Disposición">
                    <ItemTemplate>
                        <asp:DropDownList ID="drpDisposalCenter" runat="server" Width="100%">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle Width="40%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="panelPrint" runat="server" GroupingText="IMPRIMIR EXPEDIENTE">
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
        <asp:Button ID="btnDisplayCreditRequest1" runat="server" Text="Solicitud y Autorización Consulta de Historial"
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
        <asp:Button ID="btnDisplayReceiptToSettle" runat="server" Text="Recibo de Caja Finiquito"
            CssClass="Button_1" OnClick="btnDisplayReceiptToSettle_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayDisposalBonusReceipt" runat="server" Text="Recibo de Bono de Chatarrización"
            CssClass="Button_1" OnClick="btnDisplayDisposalBonusReceipt_Click" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayRepaymentSchedule" runat="server" Text="Tabla de Amortización"
            CssClass="Button_1" OnClick="btnDisplayRepaymentSchedule_Click" />
        <br />
    </asp:Panel>
    <br />
    <div>
        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="Button" 
            OnClientClick="return confirm('¿Está seguro de cancelación?')" 
            onclick="btnCancel_Click" />
            &nbsp &nbsp<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button" 
            OnClientClick="return confirm('¿ Desea regresar a la pantalla previa ?')" onclick="btnRegresar_Click"/>
    </div>
</asp:Content>
