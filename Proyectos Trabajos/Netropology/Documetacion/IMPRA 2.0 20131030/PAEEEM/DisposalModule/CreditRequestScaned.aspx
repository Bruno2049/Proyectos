<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreditRequestScaned.aspx.cs"
    Inherits="PAEEEM.DisposalModule.CreditRequestScaned" Title="Entrega-Recepción de Equipo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">
           function ContinueOrCancel() {
               if (confirm('¡ Ya ha sido Realizado el Ingreso del (los) Equipo(s) incluidos en el Crédito Ingresado !')) {
                   window.document.getElementById("<%=hidConfirm.ClientID%>").click();
               }
               else {
                   var barcode;
                   barcode = window.document.getElementById("<%=txtBarCode.ClientID %>");
                   barcode.innerText = "";
                   barcode.focus();
               }
           }  
    </script>
    
    <style type="text/css">
        .Label
        {
            width: 260px;
            color: #333333;
            font-size: 16px;
        }
        .TextBox
        {
            width: 40%;
        }
        .Button
        {
            width: 120px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <br>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/RECEPCION_EQUIPOS.png" />
            </div>
            <div id="dtFecha" align="right">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                &nbsp;<asp:TextBox ID="txtFecha" runat="server" Enabled="False" BorderWidth="0" /></div>
            <br />
            <div>
                <asp:Label ID="lblBarCode" runat="server" Font-Size="11pt" Text="Capturar/Escanear el Número de Crédito"
                    CssClass="Label"></asp:Label>
                &nbsp;<asp:TextBox ID="txtBarCode" runat="server" Font-Size="11pt" CssClass="TextBox"></asp:TextBox>
            </div>
            <br />
            <div align="center">
                <asp:Button ID="btnValidate" runat="server" Text="Siguiente" CssClass="Button" OnClick="btnValidate_Click" />
                 <asp:Button ID="hidConfirm" runat="server" OnClick="hidConfirm_Click" Style="display: none;" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
