<%@ Page Title="Validador Número Servicio PAEEEM" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Valida.aspx.cs"
    Inherits="PAEEEM.Captcha.Valida" EnableViewState="true" %>

<%@ Register src="MyCaptcha.ascx" tagname="MyCaptcha" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <asp:Label ID="lblRPU" runat="server" Text="<%$ Resources:DefaultResource, CaptchaRPU%>"></asp:Label>
        <asp:TextBox ID="txtServiceCode" runat="server" Width="200"></asp:TextBox>
        <br />
        <br />
        <uc1:MyCaptcha ID="MyCaptcha1" runat="server" />
        <br />
        <asp:Button ID="btnCheck" runat="server" onclick="btnCheck_Click" Text="<%$ Resources:DefaultResource, CaptchaCheck%>" />
        <br />
        <asp:Label ID="lblCheckResult" runat="server" Text="<%$ Resources:DefaultResource, CaptchaCheckResult%>"></asp:Label>   
        <br/><br/>
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True" 
            ForeColor="#3333FF" NavigateUrl="~/SupplierModule/CapturaAuxiliar.aspx" 
            Visible="False">Captura Auxiliar</asp:HyperLink>
</asp:Content>
