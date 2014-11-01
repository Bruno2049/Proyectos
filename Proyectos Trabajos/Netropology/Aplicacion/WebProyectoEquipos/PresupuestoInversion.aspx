<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PresupuestoInversion.aspx.cs" Inherits="WebProyectoEquipos.PresupuestoInversion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div style="width: 100%">
        Contraseña:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
         <asp:Button ID="Button2" runat="server" Text="Desencriptar" 
            onclick="Button2_Click" />
            &nbsp;
         <asp:Button ID="Button3" runat="server" Text="Encriptar" 
            onclick="Button3_Click" />
        <br/>
        <asp:Label ID="Label1" runat="server"></asp:Label>
         <br/><br/>                        
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    </div>
    
</asp:Content>
