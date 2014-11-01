<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RoleEdit.aspx.cs" Inherits="PAEEEM.RoleEdit" Title="SIP"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Label
        {
            width: 160px;
            color: Maroon;
        }
        .TextBox
        {
            width: 480px;
        }
        .Button
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
<div>
            <br>
            <div>
            <asp:Image runat="server" ImageUrl="../SupplierModule/images/t_edicion.png" />
            </div>
            </div>
            <br />

	<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
						<div id="divContainer">
										<div>
														<asp:Label runat="server" Text="Nombre del Rol" CssClass="Label" ForeColor="#333333">
														</asp:Label>
														&nbsp &nbsp &nbsp &nbsp
														<asp:TextBox ID="txtRoleName" runat="server" CssClass="TextBox">
														</asp:TextBox>
										</div>
										<br />
										<div align="center">
														<asp:Button ID="btnSaveRole" runat="server" Text="Guardar" OnClick="btnSaveRole_Click" CssClass="Button" OnClientClick="return confirm('Confirmar Guardar el Rol');" />&nbsp &nbsp &nbsp &nbsp
														<asp:Button ID="btnCancelRole" runat="server" Text="Salir" OnClick="btnCancelRole_Click" CssClass="Button" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" />
										</div>
						</div>
		</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>