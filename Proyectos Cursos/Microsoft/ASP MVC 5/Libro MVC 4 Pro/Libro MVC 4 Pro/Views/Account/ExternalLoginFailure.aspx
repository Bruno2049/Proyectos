<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="externalLoginFailureTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Error de inicio de sesión
</asp:Content>

<asp:Content ID="externalLoginFailureContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Error de inicio de sesión.</h1>
        <h2>El inicio de sesión con el servicio no se ha llevado a cabo correctamente.</h2>
    </hgroup>
</asp:Content>
