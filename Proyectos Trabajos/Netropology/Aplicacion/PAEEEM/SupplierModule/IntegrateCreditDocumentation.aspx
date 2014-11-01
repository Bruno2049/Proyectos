<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IntegrateCreditDocumentation.aspx.cs"
    Inherits="PAEEEM.SupplierModule.IntegrateCreditDocumentation" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Label
        {
            width: 200px;
            color: Maroon;
        }
        .Label1
        {
            width: 500px;
            color: Maroon;
        }
        .TextBox
        {
            width: 250px;
        }
        .DropDownList
        {
            width: 250px;
        }
        .Button
        {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    &nbsp;&nbsp;
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtFecha" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:Label ID="Label2" runat="server" Text="Nombre o razón social" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="TextBox" 
                    Enabled="False"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="Número de Crédito" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtCreditoNum" runat="server" CssClass="TextBox" 
                    Enabled="False"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:Label ID="Label4" runat="server" Text="PODER NOTARIAL DEL REPRESENTANTE LEGAL"
                    CssClass="Label1"></asp:Label>
            </div>
            <div>
                <asp:Label ID="Label5" runat="server" Text="Num. de Escritura" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtRepresentative_LegalDocumentNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label19" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtRepresentative_LegalDocumentFecha" runat="server" CssClass="TextBox"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label6" runat="server" Text="Nombre del Notario" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtRepresentative_NotariesProfessionName" runat="server" CssClass="TextBox"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label20" runat="server" Text="Num. de Notario" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtRepresentative_NotariesProfessionNumber" runat="server" CssClass="TextBox"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label7" runat="server" Text="Estado" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="drpRepresentative_Estado" runat="server" CssClass="DropDownList">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label21" runat="server" Text="Delegación o Municipio" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:DropDownList ID="drpRepresentative_OfficeorMunicipality" runat="server" CssClass="DropDownList">
                </asp:DropDownList>
            </div>
            <br />
            <div>
                <asp:Label ID="Label8" runat="server" Text="ACTA CONSTITUTIVA (PERSONA MORAL)" CssClass="Label1"></asp:Label>
            </div>
            <div>
                <asp:Label ID="Label9" runat="server" Text="Num. de Escritura" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtApplicant_LegalDocumentNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label23" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtApplicant_LegalDocumentFecha" runat="server" CssClass="TextBox"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label10" runat="server" Text="Nombre del Notario" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtApplicant_NotariesProfessionName" runat="server" CssClass="TextBox"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label24" runat="server" Text="Num. de Notario" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtApplicant_NotariesProfessionNumber" runat="server" CssClass="TextBox"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label11" runat="server" Text="Estado" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="drpApplicant_Estado" runat="server" CssClass="DropDownList">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label25" runat="server" Text="Delegación o Municipio" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:DropDownList ID="drpApplicant_OfficeOrMunicipality" runat="server" CssClass="DropDownList">
                </asp:DropDownList>
            </div>
            <br />
            <div>
                <asp:Label ID="Label12" runat="server" Text="ESCRITURA DEL INMUEBLE Y ACTA DE MATRIMONIO DEL AVAL"
                    CssClass="Label1"></asp:Label>
            </div>
            <div>
                <asp:Label ID="Label13" runat="server" Text="Num. de Escritura" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtGuarantee_LegalDocumentNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label27" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtGuarantee_LegalDocumentFecha" runat="server" CssClass="TextBox"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label14" runat="server" Text="Nombre del Notario" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtGuarantee_NotariesProfessionName" runat="server" CssClass="TextBox"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label28" runat="server" Text="Num. de Notario" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtGuarantee_NotariesProfessionNumber" runat="server" CssClass="TextBox"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label15" runat="server" Text="Estado" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="drpGuarantee_Estado" runat="server" CssClass="DropDownList">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label29" runat="server" Text="Delegación o Municipio" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:DropDownList ID="drpGuarantee_OfficeOrMunicipality" runat="server" CssClass="DropDownList">
                </asp:DropDownList>
            </div>
            <div>
                <asp:Label ID="Label16" runat="server" Text="Fecha Libertad de Gravamen" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtPropertyEncumbrancesFecha" runat="server" CssClass="TextBox"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label30" runat="server" Text="Emitido por" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtPropertyEncumbrancesName" runat="server" CssClass="TextBox"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label17" runat="server" Text="Num. Acta de Matrimonio" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtMarriageNumber" runat="server" CssClass="TextBox"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label31" runat="server" Text="Registro Civil" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtCitizenRegisterOffice" runat="server" CssClass="TextBox"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClientClick="return confirm('¿ Desea regresar a la pantalla previa ?')" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" OnClientClick="return confirm('¿ Desea continuar con la Integración del Expediente ?')"
                    OnClick="btnSiguiente_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSalir" runat="server" Text="Salir" OnClientClick="return confirm('¿ Desea salir de ésta pantalla ?')" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
