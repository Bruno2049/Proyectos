<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisposalCenterMonitor.aspx.cs" 
Inherits="PAEEEM.RegionalModule.DisposalCenterMonitor" Title="Monitor de Centros de Acopio y Destrucción" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 160px;
            color: #333333;
            font-size: 16px;
        }
        .DropDownList
        {
            width: 250px;
        }
        .CenterButton
        {
            width: 150px;
            margin-left: 50%;
            margin-right: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="container">
                <div>
                    <br/>
                    <br />
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/RegionalModule/images/t_equipos3.png" />
                    <br />
                </div>
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="Label2" Text="Zona" runat="server" Font-Size="11pt" CssClass="Label"
                                    Width="70px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpZone" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList" onselectedindexchanged="drpZone_SelectedIndexChanged"/>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="Label3" Text="Tipo" Font-Size="11pt" runat="server" CssClass="Label"
                                    Width="70px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpType" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" onselectedindexchanged="drpType_SelectedIndexChanged">
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="Matriz" Value="M" />
                                        <asp:ListItem Text="Sucursal" Value="B" />
                                    </asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="Label4" Text="Estatus" Font-Size="11pt" runat="server" CssClass="Label"
                                    Width="70px" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpEstatus" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" onselectedindexchanged="drpEstatus_SelectedIndexChanged" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <div>
                    <asp:GridView ID="grdDisposalCenters" runat="server" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" AllowPaging="True" PageSize="20" 
                        OnDataBound="grdDisposalCenters_DataBound" 
                        onrowcommand="grdDisposalCenters_RowCommand">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Id_Centro_Disp" HeaderText="Clave"></asp:BoundField>
                            <asp:BoundField DataField="Dx_Razon_Social" HeaderText="Nombre o Razón Social" />
                            <asp:BoundField DataField="Dx_Nombre_Comercial" HeaderText="Nombre Comercial" />
                            <asp:BoundField DataField="Dx_Estatus_Centro_Disp" HeaderText="Estatus" />
                            <asp:BoundField DataField="Dx_Nombre_Zona" HeaderText="Zona" />
                            <asp:BoundField DataField="Tipo_Centro_Disp" HeaderText="Tipo" />
                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Editar" CommandName="edit" OnClientClick="return confirm('Confirmar Editar el Centro de Acopio y Destrucción');"/>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asignar Tecnología" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    
                                    <asp:Button ID="btnAssignTechnology" runat="server" Text="Asignar Tecnología" CommandName="assign" OnClientClick="return confirm('Confirmar Asignar Tecnología al CAyD');"/>
                                </ItemTemplate>
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
                    <asp:Button ID="btnAdd" Text="Agregar CAyD" runat="server"
                         CssClass="CenterButton" onclick="btnAdd_Click" OnClientClick="return confirm('Confirmar Agregar un Nuevo CAyD');"/>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
