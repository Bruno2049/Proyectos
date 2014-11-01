<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ProductMonitor.aspx.cs"
    Inherits="PAEEEM.CentralModule.ProductMonitor" Title="Monitor de Productos" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 120px;
            color: #333333;
            font-size: 16px;
            text-align: right;
        }
        .DropDownList
        {
            width: 200px;
        }
        .Button
        {
            width: 150px;
        }
        .style1
        {
            height: 44px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="container">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            
                <div>
                    <br />
        <asp:Image runat="server"  ImageUrl="../CentralModule/images/t_monitor_prod.png" />
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
                                <asp:Label ID="lblManufacture" Text="Fabricante" runat="server" Font-Size="11pt"
                                    CssClass="Label" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpManufacture" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpManufacture_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblTechnology" Text="Tecnología" Font-Size="11pt" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpTechnology" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpTechnology_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblTipoOfProduct" Text="Tipo de Producto" Font-Size="11pt" runat="server"
                                    CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpTipoProduct" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpTipoProduct_SelectedIndexChanged" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="lblMarca" Text="Marca" runat="server" Font-Size="11pt" CssClass="Label" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpMarca" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpMarca_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblStatus" Text="Estatus" Font-Size="11pt" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpStatus" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <br />
                <div>
                    <asp:GridView ID="grvProductMonitor" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        AllowPaging="True" PageSize="20" DataKeyNames="Cve_Producto,Cve_Estatus_Producto"
                        OnDataBound="grvProductMonitor_DataBound">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Dx_Producto_Code" HeaderText="Clave"></asp:BoundField>
                            <asp:BoundField DataField="Dx_Nombre_Producto" HeaderText="Nombre Producto" />
                            <asp:BoundField DataField="Dx_Nombre_Fabricante" HeaderText="Fabricante" />
                            <asp:BoundField DataField="Dx_Nombre_General" HeaderText="Tecnología" />
                            <asp:BoundField DataField="Dx_Tipo_Producto" HeaderText="Tipo de Producto" />
                            <asp:BoundField DataField="Dx_Marca" HeaderText="Marca" />
                            <asp:BoundField DataField="Dx_Modelo_Producto" HeaderText="Modelo" />
                            <asp:BoundField DataField="Dx_Estatus_Producto" HeaderText="Estatus" />
                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="btnEdit_Click" OnClientClick="return confirm('Confirmar Editar Producto');" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                </div>
    
    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="10"
        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
        UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
        FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
        CurrentPageButtonClass="cpb">
    </webdiyer:AspNetPager>
    <br />
    <table width="100%">
        <tr>
            <td width="10%">
            </td>
            <td width="20%" align="center">
                <asp:Button ID="btnAdd" runat="server" Text="Agregar Producto" OnClick="btnAdd_Click"
                    OnClientClick="return confirm('Confirmar Agregar un Nuevo Producto')" />
            </td>
            <td width="20%" align="center">
                <asp:Button ID="btnActive" runat="server" Text="Activar" OnClick="btnActive_Click"
                    OnClientClick="return confirm('Confirmar Activar los Productos Seleccionados')" />
            </td>
            <td width="20%" align="center">
                <asp:Button ID="btnDeActive" runat="server" Text="Inactivar" OnClick="btnDeActive_Click"
                    OnClientClick="return confirm('Confirmar Inactivar los Productos Seleccionados')" />
            </td>
            <td width="20%" align="center">
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click"
                    OnClientClick="return confirm('Confirmar Cancelar Definitivamente los Productos Seleccionados')" />
            </td>
            <td width="10%">
            </td>
        </tr>
    </table>
        </ContentTemplate>       
    </asp:UpdatePanel>
    </div>
</asp:Content>
