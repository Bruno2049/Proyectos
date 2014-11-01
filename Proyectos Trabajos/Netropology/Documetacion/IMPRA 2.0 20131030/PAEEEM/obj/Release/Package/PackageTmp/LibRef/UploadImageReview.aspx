<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UploadImageReview.aspx.cs"
 Inherits="PAEEEM.DisposalModule.UploadImageReview" Title="Imagen del Producto Viejo Subir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />

    <script src="../Resources/Script/Calendar/WdatePicker.js" type="text/javascript"></script>

    <link href="../Resources/Script/Calendar/skin/default/datepicker.css" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 150px;
            color: #333333;
            font-size: 16px;
        }       
        .Button
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div>  
               <asp:Label ID="lblSelect" runat="server" CssClass="Label" Text="Seleccionar Archivo"></asp:Label>
                <asp:FileUpload ID="fldSelect" runat="server" Width="416px"  />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnUpload" runat ="server" Text="Cargar" 
                    OnClientClick="return confirm('  Confirmar Cargar Foto asociada al Equipo seleccionado')" 
                    onclick="btnUpload_Click" />     
                    &nbsp;&nbsp;&nbsp;     
                    <asp:Button ID="btnCancel" runat="server" Text="Salir" 
                    onclick="btnCancel_Click" />          
            </div>
        <div>
            <asp:Image ID="imgReview" runat="server" Visible="false" />
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>