<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CirculoDeCredito.aspx.cs" Inherits="PAEEEM.SupplierModule.CirculoDeCredito" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="Stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../Resources/Css/TablaNet.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table style="width: 100%; height: 71px;">
            <tr>
                <td align="center" colspan="3" style="color: #0099FF; font-weight: bold; font-size: large">MÓDULO CIRCULO DE CRÉDITO
                <br />
                </td>
            </tr>
            <tr>
                <td align="center" style="font-weight: bold;">
                    <asp:Label runat="server" ID="lbGeneraPaquetes">Generación de Paquetes</asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="width: 100%; background: #99CCFF; height: 71px;">
            <tr>
                <td colspan="3" align="center">ESTATUS:
                    <asp:DropDownList ID="DDLEstatus" runat="server" Width="140px" AutoPostBack="True" />
                </td>
                <td colspan="3" align="center">
                    <asp:Label runat="server" ID="Label1" Visible="false">NO.CREDITO:</asp:Label>
                    <asp:TextBox ID="TxtNoCredito" runat="server" Width="150px" Visible="false" />
                </td>
                <td colspan="3" align="center">
                    <asp:Label runat="server" ID="Label2" Visible="false">NO.PAQUETE</asp:Label>
                    <asp:TextBox ID="TxtCliente" runat="server" Width="170px" Visible="false" />
                </td>
                <td colspan="3" align="center">
                    <asp:Label runat="server" ID="Label3" Visible="false">FOLIO PAQUETE:</asp:Label>
                    <asp:TextBox ID="TxtNomComercial" runat="server" Width="170px" Visible="false" />
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Enabled="False" /></td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <div id="div_GeneraPaquetes" style="width: 100%" align="center" runat="server">
                        <asp:UpdatePanel ID="UpdatePaneGeneraPaque" runat="server">
                            <ContentTemplate>
                                <asp:GridView runat="server" ID="gvGeneraPaque" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                    AllowPaging="True">
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                                    <PagerSettings Visible="False" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />

                                    <Columns>
                                        <asp:TemplateField ShowHeader="True" HeaderText="Seleccionar">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:CheckBox ID="ckbSelect" runat="server" AutoPostBack="true"></asp:CheckBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NoCredito" HeaderText="No de Credito"></asp:BoundField>
                                        <asp:BoundField DataField="FoliConsul" HeaderText="Folio de Consulta"></asp:BoundField>
                                        <asp:BoundField DataField="NomComercial" HeaderText="Nombre Comercial"></asp:BoundField>
                                        <asp:BoundField DataField="FechConsulta" HeaderText="Fecha de Consulta"></asp:BoundField>
                                        <asp:BoundField DataField="Status" HeaderText="Estatus"></asp:BoundField>
                                        <asp:TemplateField ShowHeader="True" HeaderText="Carta Autorización">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button ID="btnExaminarCarta" runat="server" Text="Examinar" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="True" HeaderText="Acta Ministarió Público">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button ID="btnExaminarActa" runat="server" Text="Examinar" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="True" HeaderText="Comentarios">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:TextBox ID="TexComentarios" runat="server" Height="20px" Width="100px" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="10"
                                    AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                                    PageIndexBoxType="DropDownList" CustomInfoHTML="Página:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                                    UrlPaging="false"
                                    FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                                    CurrentPageButtonClass="cpb" >
                                </webdiyer:AspNetPager>

                                <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false" />
                                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
                             <%--   <div style="display: none">
                                    <asp:Button ID="HiddenButton" BackColor="#FFFFFF" OnClick="HiddenButton_Click" runat="server" Width="0px" />
                                </div>--%>
                                <br />
                                <br />
                                <table style="width: 100%">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnGenerarPaque" runat="server" Text="Generar Paquete" Enabled="False" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnSalir" runat="server" Text="Salir" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>

    </div>
</asp:Content>
