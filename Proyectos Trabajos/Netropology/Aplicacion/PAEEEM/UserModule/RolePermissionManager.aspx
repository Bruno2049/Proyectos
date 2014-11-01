<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RolePermissionManager.aspx.cs"
    Inherits="PAEEEM.RolePermissionManager" Title="Administrador de Permisos"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="../Styles/Table.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        history.forward();
    </script>     
    <style type="text/css">
        .style6
        {
            text-align: center;
        }
        .style7
        {
            text-align: center;
        }
        .style8
        {
            text-align: left;
            width: 169px;
        }
        .style9
        {
            text-align: right;
            width: 135px;
        }
        .style10
        {
            width: 42px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <br>
        <div>
            <asp:Image runat="server" ImageUrl="../SupplierModule/images/t_permisos.png" />
        </div>
    </div>
    <br />

 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
						<table class="formTable">
										<tr>
														<td class="style10" rowspan="3">
														</td>
														<td style="width: 20px">
														</td>
														<td style="border: 1px" height="30px" align="left">
														<asp:Image runat="server" ImageUrl="../SupplierModule/images/rol.png" ImageAlign="Middle" />
														<asp:Label ID="Label2" runat="server" Text="Nombre del Rol" Visible="false">
														</asp:Label>
														</td>
														<td class="style6" style="border: 1px">&nbsp;</td>
														<td class="style2" style="border: 1px; width: 500px;" align="left" rowspan="3">
														<asp:Image runat="server" ImageUrl="../SupplierModule/images/t_arbol.png" />
														<asp:Label ID="Label1" runat="server" Text="Permisos de Navegación" Visible="false">
														</asp:Label>
														<br>
														<asp:CheckBox ID="cbSelectAll" runat="server" Text="Seleccionar Todo el Menú" AutoPostBack="true" OnCheckedChanged="cbSelectAll_CheckedChanged" />
														    <br></br>
														</br></td>
										</tr>
										<tr>
														<td colspan="2" style="width: 160px">&nbsp;&nbsp;&nbsp;&nbsp;
														<asp:Label ID="Label3" runat="server" Text="Nombre del Rol">
														</asp:Label>
														</td>
														<td class="style6" style="border: 1px">
														<asp:DropDownList ID="ddlRoleList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleList_SelectedIndexChanged" CssClass="td_ddList">
														</asp:DropDownList>
														</td>
										</tr>
										<tr>
														<td class="style6" colspan="2" style="width: 110px" valign="baseline">&nbsp;
														<asp:Label ID="Label4" runat="server" Text="Tipo de Permiso" Visible="false">
														</asp:Label>
														</td>
														<td class="style6" style="border: 1px">
														<asp:DropDownList ID="ddlPermissionType" runat="server" CssClass="td_ddList" OnSelectedIndexChanged="ddlPermissionType_SelectedIndexChanged" AutoPostBack="true" Visible="false">
														</asp:DropDownList>
														</td>
										</tr>
										<tr>
														<td class="style10" rowspan="2" style="border: 1px" colspan="2">
														</td>
														<td class="style9" style="border: 1px">&nbsp;</td>
														<td class="style8">&nbsp;</td>
														<td class="style7" rowspan="2" style="border: 1px; width: 300px;vertical-align:top;text-align:left">
														<%--     <asp:CheckBoxList ID="cblOperator" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" RepeatLayout="Table"   >
                       </asp:CheckBoxList>--%><asp:Panel ID="Panel1" runat="server" Width="300px" Height="300px" ScrollBars="Auto">
																		<asp:TreeView ID="tvPemissionTree" runat="server" ShowCheckBoxes="All" ShowExpandCollapse="true" ExpandDepth="1" ShowLines="true">
																						<HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
																						<NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
																						<ParentNodeStyle Font-Bold="False" />
																						<SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
																		</asp:TreeView>
														</asp:Panel>
														</td>
										</tr>
										<tr>
														<td class="style9" valign="top" style="border-style: none; border-color: inherit; border-width: 1px;">&nbsp;</td>
														<td class="style8" valign="top" style="border: 1px">&nbsp;</td>
										</tr>
										<tr>
														<td class="style10" colspan="2">
														</td>
														<td class="style7" colspan="2">
														<asp:Button ID="btnSave" runat="server" Text="Guardar" OnClick="btnSave_Click" CssClass="btn1" />
														<%--<asp:Button
                            ID="btnCancel" runat="server" Text="Salir" />--%>&nbsp;
														</td>
										</tr>
						</table>
		</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
