<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SupplierMonitor.aspx.cs"
    Inherits="PAEEEM.SupplierMonitor" Title="Monitor de Proveedores" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 50px;
            color: #333333;
            font-size: 16px;
        }
        .DropDownList
        {
            width: 200px;
        }
        .Button
        {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="container">
                <div>
                    <br/>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/supplierMonitor.png" />
                </div>
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
                    <asp:Literal ID="lblFecha" runat='server' />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="lblZona" Text="Zona" runat="server" Font-Size="11pt" CssClass="Label" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpZona" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpZona_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblTipo" Text="Tipo" Font-Size="11pt" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpTipo" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpTipo_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="" Selected="True"/>
                                    <asp:ListItem Text="Proveedor Matriz" Value="M"/>
                                    <asp:ListItem Text="Proveedor Sucursal" Value="B"/>
                                    </asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblEstatus" Text="Estatus" Font-Size="11pt" runat="server" CssClass="Label" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpEstatus" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpEstatus_SelectedIndexChanged" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <div>
                    <asp:GridView ID="grvSupplierMonitor" runat="server" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" AllowPaging="True" PageSize="20" DataKeyNames="ID,Type" 
                        ondatabound="grvSupplierMonitor_DataBound">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Tipo">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("Type").ToString() == "Proveedor" ? "Matriz" : "Sucursal" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Dx_Razon_Social" HeaderText="Nombre o Razón Social" />
                            <asp:BoundField DataField="Dx_Nombre_Comercial" HeaderText="Nombre Comercial" />
                            <asp:BoundField DataField="Dx_Nombre_Zona" HeaderText="Zona" />
                            <asp:BoundField DataField="Dx_Estatus_Proveedor" HeaderText="Estatus" />
                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="btnEdit_Click" OnClientClick="return confirm('Confirmar Editar Proveedor seleccionado');"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asignar Productos" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnAssignProduct" runat="server" Text="Asignar Productos" 
                                        onclick="btnAssignProduct_Click" OnClientClick="return confirm('Confirmar Asignar Productos al Proveedor seleccionado');"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asignar CAyD" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnAssignDisposal" runat="server" Text="Asignar CAyD" 
                                        onclick="btnAssignDisposal_Click" OnClientClick="return confirm('Confirmar Asignar CAyD al Proveedor seleccionado');"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
                        FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                        CurrentPageButtonClass="cpb">
                    </webdiyer:AspNetPager>
                </div>
                <br />
                <div align="center">
                    <asp:Button ID="btnAdd" Text="Agregar Proveedor" runat="server" 
                        CssClass="Button" onclick="btnAdd_Click" OnClientClick="return confirm('Confirmar Agregar un Nuevo Proveedor');"/>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
