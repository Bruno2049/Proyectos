<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RoleAssignment.aspx.cs" Inherits="PAEEEM.RoleAssignment" Title="Asignar Roles a Usuarios"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
            history.forward();
            function HiddenMenuPanel(IsHidden) {
                var menu = document.getElementById("menu");
                var visible = document.getElementById("imgVisible");
                var hidden = document.getElementById("imgInVisible");

                if (IsHidden == 1) {
                    visible.style.display = 'none';
                    hidden.style.display = 'inline';
                    menu.style.display = 'none';
                }
                else {
                    visible.style.display = 'inline';
                    hidden.style.display = 'none';
                    menu.style.display = 'inline';
                }
            }
    </script>
    <style type="text/css">
        .style5
        {
            width: 287px;
        }
        .style8
        {
            text-align: center;
            width: 275px;
        }
        .style9
        {
            text-align: center;
            width: 602px;
        }
    </style>
    <link href="../Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div>
            <br>
            <div>
            <asp:Image runat="server" ImageUrl="../SupplierModule/images/t_asignacion.png" />
            </div>
            </div>
            <br />
     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <table class="formTable" onmousemove="reset_intervalNew()">
                <tr>
                    <td class="style5">
                    </td>
                    <td class="style8">
                    <asp:Image runat="server" ImageUrl="../SupplierModule/images/usuarios.png" />
                        <asp:Label ID="Label1" runat="server" Text="Users" Visible="false"></asp:Label>
                    </td>
                    <td class="style9">
                     <asp:Image runat="server" ImageUrl="../SupplierModule/images/asignacion.png"/>                        <asp:Label ID="Label2" runat="server" Text="Asignación de Rol" Visible="false"></asp:Label>
                    </td>
                    <td class="style2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                    </td>
                    <td class="style8">
                        <asp:ListBox ID="listUser" runat="server" Height="466px" 
                            onselectedindexchanged="listUser_SelectedIndexChanged" AutoPostBack="true" 
                            Width="240px"></asp:ListBox>
                    </td>
                    <td class="style9" valign="top" align="left">
                        <asp:DropDownList ID="ddlRoleList" runat="server" Width="50%">
                        </asp:DropDownList>
                    </td>
                    <td class="style2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                    </td>
                    <td class="style8">
                        <asp:Button ID="btnSave" runat="server" Text="Guardar" OnClick="btnSave_Click" CssClass="btn1"/><%--<asp:Button
                            ID="btnCancel" runat="server" Text="Salir" />--%>
                    </td>
                    <td class="style2">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
