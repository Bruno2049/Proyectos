<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UploadImageForProduct.aspx.cs"
    Inherits="PAEEEM.DisposalModule.UploadImageForProduct" Title="Cargar Fotografía" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Button
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    &nbsp;<div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br>
        <asp:Image ID="Image1" runat="server" ImageUrl="" />
    </div>
    <div align="right">
        <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
        <asp:Literal ID="literalFecha" runat='server' />
    </div>
    <br />
    <br />
    <div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblImage1" Text="Image1" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:FileUpload ID="fldImage1" runat="server" Width="60%" />
    </div>
    <div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblImage2" Text="Image2" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:FileUpload ID="fldImage2" runat="server" Width="60%" />
    </div>
    <div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblImage3" Text="Image3" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:FileUpload ID="fldImage3" runat="server" Width="60%" />
    </div>
    <div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="Button" OnClick="btnUpload_Click" OnClientClick="return confirm('¿estás seguro de subir?')"/>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="Button" OnClick="btnCancel_Click" OnClientClick="return confirm('¿estás seguro de que existe?')"/>
    </div>
</asp:Content>
