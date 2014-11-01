<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PermissionTreeManage.aspx.cs"
    Inherits="PAEEEM.PermissionTreeManage"  Title="Administrador de Permisos de Navegación"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style type="text/css">
        #divContainer
        {
            margin-left: auto;
            margin-right: auto;
        }
        #divTreeView
        {
            float: left;
        }
        #divDetailInfo
        {
            float: right;
        }
        .Label
        {
            width: 160px;
            color: Maroon;
        }
        .TextBox
        {
            width: 480px;
        }
        .DropDownList
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
            <asp:Image runat="server" ImageUrl="../SupplierModule/images/t_admon.png" />
            </div>
            </div>
            <br />

	<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
						<div id="divContainer">
						</div>
						<div id="divTreeView">
										<asp:Image runat="server" ImageUrl="../SupplierModule/images/t_navegacion.png" id="Image1" />
										<br>
										<br />
										<asp:Panel ID="Panel1" runat="server" Width="300px" Height="300px" ScrollBars="Auto">
														<asp:TreeView ID="tvPemissionTree" runat="server" ShowExpandCollapse="true" ExpandDepth="1" ShowLines="true" OnSelectedNodeChanged="tvPemissionTree_SelectedNodeChanged">
																		<HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
																		<NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
																		<ParentNodeStyle Font-Bold="False" />
																		<SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
														</asp:TreeView>
										</asp:Panel>
										<br />
										<asp:Button ID="btnRefresh" runat="server" Text="Refrescar Arbol" OnClick="btnRefresh_Click" />
										</br></div>
						<div id="divDetailInfo">
										<h6 align="center">
										<asp:Image runat="server" ImageUrl="../SupplierModule/images/t_nodo.png" />
										</h6>
										<br />
										<div>
														<asp:Label ID="Label3" runat="server" Text="ID Nodo (Automático)" CssClass="Label" ForeColor="#333333">
														</asp:Label>
														&nbsp; &nbsp; &nbsp; &nbsp;
														<asp:TextBox ID="txtNodeNo" runat="server" CssClass="TextBox">
														</asp:TextBox>
										</div>
										<div>
														<asp:Label ID="Label4" runat="server" Text="Nombre del Nodo" CssClass="Label" ForeColor="#333333">
														</asp:Label>
														&nbsp; &nbsp; &nbsp; &nbsp;
														<asp:TextBox ID="txtNodeName" runat="server" CssClass="TextBox">
														</asp:TextBox>
										</div>
										<div>
														<asp:Label ID="Label5" runat="server" Text="Tipo de Página" CssClass="Label" ForeColor="#333333">
														</asp:Label>
														&nbsp; &nbsp; &nbsp; &nbsp;
														<asp:DropDownList ID="ddlNodeType" runat="server" OnSelectedIndexChanged="ddlNodeType_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownList">
																		<asp:ListItem Selected="True" Text="Página" Value="P">
																		</asp:ListItem>
																		<%-- <asp:ListItem Text="Operación" Value="O"></asp:ListItem>--%>
														</asp:DropDownList>
										</div>
										<div>
														<asp:Label ID="lblNodeInfo" runat="server" Text="URL de la Página" CssClass="Label" ForeColor="#333333">
														</asp:Label>
														&nbsp; &nbsp; &nbsp; &nbsp;
														<asp:TextBox ID="txtNodeInfo" runat="server" CssClass="TextBox">
														</asp:TextBox>
										</div>
										<div>
														<asp:Label ID="lblParentNode" runat="server" Text="Padre del Nodo" CssClass="Label" ForeColor="#333333">
														</asp:Label>
														&nbsp; &nbsp; &nbsp; &nbsp;
														<asp:DropDownList ID="ddlParentNode" runat="server" CssClass="DropDownList">
														</asp:DropDownList>
										</div>
										<div>
														<asp:Label ID="Label6" runat="server" Text="Orden Secuencial" CssClass="Label" ForeColor="#333333">
														</asp:Label>
														&nbsp; &nbsp; &nbsp; &nbsp;
														<asp:TextBox ID="txtNodeSequence" runat="server" CssClass="TextBox">
														</asp:TextBox>
										</div>
										<br />
										<br />
										<div align="center">
														&nbsp;<asp:Button ID="btnSave" runat="server" Text="Guardar" OnClick="btnSave_Click" CssClass="Button" OnClientClick="return confirm('Confirmar Guardar Nodo');" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text="Agregar" OnClick="btnAdd_Click" CssClass="Button" OnClientClick="return confirm('Confirmar Agregar Nodo');" />&nbsp; 
								&nbsp;&nbsp;&nbsp;
														<asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="btnEdit_Click" CssClass="Button" OnClientClick="return confirm('Confirmar Editar Nodo');" />&nbsp; &nbsp;&nbsp;&nbsp;
														<asp:Button ID="btnDelete" runat="server" Text="Borrar" OnClick="btnDelete_Click" OnClientClick="return confirm('Confirmar Borrar Nodo');" CssClass="Button" />
										</div>
										<br />
										<div>
										</div>
						</div>
						</div>
		</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
