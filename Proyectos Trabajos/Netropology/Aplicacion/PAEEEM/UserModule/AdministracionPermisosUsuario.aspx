<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministracionPermisosUsuario.aspx.cs" Inherits="PAEEEM.UserModule.AdministracionPermisosUsuario" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">
        <div style="width: 100%">
            <table width="100%">
                <tr>
                    <td style="width: 30%">
                        <asp:Label ID="Label1" runat="server" Text="ROL:"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <telerik:RadComboBox ID="RadCmbRoles" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="RadCmbRoles_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td style="width: 10%">
                        <br/>
                    </td>
                    <td style="width: 30%">
                        <asp:Image ID="Image1" runat="server" ImageUrl="../SupplierModule/images/t_arbol.png" />
						<br/>
                        <asp:CheckBox ID="cbSelectAll" runat="server" Text="Seleccionar Todo el Menú" AutoPostBack="true" />
                    </td>
                    <td style="width: 10%">
                        <br/>
                    </td>
                    <td style="width: 20%">
                        ACCIONES
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <br/>
                    </td>
                </tr>
                <tr style="vertical-align: top">
                    <td>
                        <telerik:RadListBox ID="RadListUsuarios" runat="server" 
                            EmptyMessage="No se encontrarón Usuarios" Height="500px" Width="100%" 
                            Skin="Office2010Silver" AutoPostBack="True" 
                            onselectedindexchanged="RadListUsuarios_SelectedIndexChanged">
                            <ButtonSettings TransferButtons="All" />
                            <EmptyMessageTemplate>
                                No se encontrarón Usuarios
                            </EmptyMessageTemplate>
                        </telerik:RadListBox>
                    </td>
                    
                    <td>
                        <br/>
                    </td>
                    <td>                        
                        <telerik:RadTreeView ID="RadTreeViewPermisos" Runat="server" CheckBoxes="True" 
                            onnodecheck="RadTreeViewPermisos_NodeCheck" Skin="Office2010Silver" 
                            onnodeclick="RadTreeViewPermisos_NodeClick">
                        </telerik:RadTreeView>                       
                    </td>
                    <td>
                        <br/>
                    </td>
                    <td>
                        <telerik:RadListBox ID="RadLstAcciones" Width="100%" runat="server" CheckBoxes="True" 
                            Skin="Office2010Silver" AutoPostBack="True" 
                            onitemcheck="RadLstAcciones_ItemCheck" 
                            onselectedindexchanged="RadLstAcciones_SelectedIndexChanged" Visible="False">
                        </telerik:RadListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <br/>                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="5">
                        <telerik:RadButton ID="RadBtnGuardar" runat="server" Text="Guardar" 
                            onclick="RadBtnGuardar_Click">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </div>       
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" 
        Skin="Office2010Silver">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="rwmVentana" runat="server" Skin="Office2010Silver">                                  
    </telerik:RadWindowManager>
</asp:Content>
