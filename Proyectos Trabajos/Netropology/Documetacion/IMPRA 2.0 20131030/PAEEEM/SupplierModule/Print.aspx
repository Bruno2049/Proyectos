<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Print.aspx.cs"
    Inherits="PAEEEM.SupplierModule.Print" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Label
        {
            width: 230px;
            color: Maroon;
        }
        .TextBox
        {
            width: 250px;
        }
        .Button
        {
            width: 280px;
            height: 50px;
        }
        .Button1
        {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lblFecha" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtFecha" runat="server" Enabled="False"></asp:TextBox>
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
                <asp:Label ID="Label1" runat="server" Text="DOCUMENTACION" CssClass="Label"></asp:Label>
            </div>
            <br />
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDisplayCreditCheckList" runat="server" Text="Check List de Crédito"
                    CssClass="Button" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDisplayCreditContract" runat="server" Text="Contrato de Financiamiento"
                    CssClass="Button" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDisplayEquipmentAct" runat="server" Text="Acta Entrega - Recepción de Equipos"
                    CssClass="Button" OnClick="btnDisplayEquipmentAct_Click" />
            </div>
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDisplayCreditRequest" runat="server" Text="Solicitud y Autorización Consulta de Historial"
                    CssClass="Button" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDisplayPromissoryNote" runat="server" Text="Pagaré" CssClass="Button" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDisplayGuaranteeEndorsement" runat="server" Text="Endoso en Garantía"
                    CssClass="Button" />
            </div>
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDisplayQuota" runat="server" Text="Carta Presupuesto de Inversión"
                    CssClass="Button" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDisplayGuarantee" runat="server" Text="Carta Compromiso Aval"
                    CssClass="Button" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDisplayReceiptToSettle" runat="server" Text="Recibo de Caja Finiquito"
                    CssClass="Button" />
            </div>
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDisplayDisposalBonusReceipt" runat="server" Text="Recibo de Bono de Chatarrización"
                    CssClass="Button" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDisplayRepaymentSchedule" runat="server" Text="Tabla de Amortización"
                    CssClass="Button" />
            </div>
            <br />
            <div>
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button1" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSalir" runat="server" Text="Salir" CssClass="Button1" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
