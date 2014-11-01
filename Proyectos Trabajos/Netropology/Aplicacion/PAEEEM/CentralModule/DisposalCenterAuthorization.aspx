<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DisposalCenterAuthorization.aspx.cs"
    Inherits="PAEEEM.CentralModule.DisposalCenterAuthorization" Title="Autorización de Centros de Acopio y Destrucción" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="container">
                 <div>
         			<br>
            		<asp:Image runat="server" ImageUrl="../images/t_autorizacion_centros_acopioydestruccion.png" />
                          
                </div>
             
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />
                    <asp:Literal ID="lblFecha" runat='server' />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="lblRegional" Text="Regional" runat="server" Font-Size="11pt" CssClass="Label" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpRegional" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList" onselectedindexchanged="drpRegional_SelectedIndexChanged" 
                                    />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblZona" Text="Zona" runat="server" Font-Size="11pt" CssClass="Label" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpZona" runat="server" Font-Size="11px" AutoPostBack="true"
                                    CssClass="DropDownList" 
                                    onselectedindexchanged="drpZona_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblTipo" Text="Tipo" Font-Size="11pt" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpTipo" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" 
                                    onselectedindexchanged="drpTipo_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="M" Text="CAyD Matriz"></asp:ListItem>
                                    <asp:ListItem Value="B" Text="CAyD Sucursal"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblEstatus" Text="Estatus" Font-Size="11pt" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpEstatus" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" 
                                    onselectedindexchanged="drpEstatus_SelectedIndexChanged" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="lblCAyD" Text="CAyD" Font-Size="11pt" runat="server" CssClass="Label" /></div>
                        </td>
                        <td colspan="3">
                            <div>
                                <asp:DropDownList ID="drpCAyD" AutoPostBack="true" Font-Size="11px" runat="server"
                                    onselectedindexchanged="drpCAyD_SelectedIndexChanged" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <div>
                    <asp:GridView ID="grvDisposalCenter" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        AllowPaging="True" PageSize="10" DataKeyNames="Id_Centro_Disp,Cve_Estatus_Centro_Disp" 
                        ondatabound="grvDisposalCenter_DataBound">
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
                            <asp:BoundField DataField="Dx_Nombre_Repre" HeaderText="Responsable CAyD" />
                            <asp:BoundField DataField="Tipo_Centro_Disp" HeaderText="Tipo" />
                            <asp:BoundField DataField="Dx_Nombre_Region" HeaderText="Region" />
                            <asp:BoundField DataField="Dx_Nombre_Zona" HeaderText="Zona" />
                            <asp:BoundField DataField="Dx_Estatus_Centro_Disp" HeaderText="Estatus CAyD" />
                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="10"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
                        FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                        CurrentPageButtonClass="cpb">
                    </webdiyer:AspNetPager>
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td width="10%">
                        </td>
                        <td width="20%" align="center">
                            <asp:Button ID="btnActive" runat="server" Text="Activar" 
                                OnClientClick="return confirm('Confirmar Activar los CAyD´s Seleccionados')" 
                                onclick="btnActive_Click" />
                        </td>
                        <td width="20%" align="center">
                            <asp:Button ID="btnDeActive" runat="server" Text="Desactivar" 
                                OnClientClick="return confirm(' Confirmar Desactivar los CAyD´s Seleccionados')" 
                                onclick="btnDeActive_Click" />
                        </td>
                         <td width="20%" align="center">
                            <asp:Button ID="btnReActive" runat="server" Text="Reactivar" 
                                OnClientClick="return confirm('Confirmar Reactivar los CAyD´s  Seleccionados')" 
                                onclick="btnReActive_Click" />
                        </td>
                        <td width="20%" align="center">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" 
                                OnClientClick="return confirm(' Confirmar Cancelar los CAyD´s  Seleccionados')" 
                                onclick="btnCancel_Click" style="height: 26px" />
                        </td>
                        <td width="10%">
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
