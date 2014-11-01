<%@ Page Language="C#" MasterPageFile="~/Site.Master" EnableViewState="true" AutoEventWireup="true"
    CodeBehind="DynamicPageRender.aspx.cs" Title="Generar Páginas Dinámicas" Inherits="PAEEEM.CentralModule.DynamicPageRender" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content runat='server' ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server" ID="dynamicPanel">
        <ContentTemplate>
            <asp:Panel runat="server">
                <asp:Panel runat="server" ID="ContainerHolder">
                </asp:Panel>
                <br />
            </asp:Panel>
            <div align="center">
                <asp:Button runat="server" ID="btnSave" Text="Guardar" OnClick="btnSave_Click" OnClientClick="return confirm('Confirmar Guardar');"/>&nbsp;&nbsp;&nbsp;&nbsp
                <asp:Button runat="server" ID="btnCancel" Text="Salir" OnClick="btnCancel_Click"
                    OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
