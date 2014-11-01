<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleRelation.aspx.cs" MasterPageFile="~/Site.Master" Inherits="PAEEEM.RoleRelation" Title="Administrador de Relación de Roles"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="../Styles/Table.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        history.forward();
    </script>     
    <style type="text/css">
        .style7
        {
            text-align: left;
            }
        .style10
        {
            width: 756px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div>
            <br>
            <div>
            <asp:Image runat="server" ImageUrl="../SupplierModule/images/t_relacion.png" />
            </div>
            </div>
            <br />

 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <table style="width: 100%;" onmousemove="reset_intervalNew()">
                <tr>
                     <td class="td_lable_1">
                        <asp:Label ID="Label3" runat="server" Text="Nombre del Rol"></asp:Label>
                    </td>
                    <td class="tbleft">
                        <asp:DropDownList ID="ddlRoleList" runat="server"  
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlRoleList_SelectedIndexChanged" Width="50%" >
                        </asp:DropDownList>
                    </td>                   
                </tr>  
                <tr><td colspan=2></td></tr>             
                <tr>
                    <td class="td_lable_1">                      
                        <asp:Label ID="Label1" runat="server" Text="Relación del Rol"></asp:Label>                        
                    </td>
                    <td class="style7" rowspan="2" style="border:1px;width:2000px;">
                        <asp:CheckBoxList ID="cblRelation" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" RepeatLayout="Table"   >
                        </asp:CheckBoxList>
                    </td>
                </tr>               
                <tr>
                    <td class="td_lable_1" valign="top" style="border:1px;width:100px;">
                        &nbsp;</td>
                    <td class="tbleft" valign="top" style="border:1px">
                        
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10"> </td>
                    <td align="left">
                       <asp:Button ID="btnSave" runat="server" Text="Guardar" onclick="btnSave_Click"/>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>