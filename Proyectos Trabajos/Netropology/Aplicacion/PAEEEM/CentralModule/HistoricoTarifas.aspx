<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistoricoTarifas.aspx.cs" Inherits="PAEEEM.CentralModule.HistoricoTarifas" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.3.2.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="Stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style7 {
            width: 226px;
        }

        .auto-style8 {
            width: 184px;
        }

        .auto-style9 {
            width: 145px;
        }

        .auto-style10 {
            width: 161px;
        }

        .auto-style11 {
            width: 42px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label runat="server" Font-Size="Large" Font-Bold="True" Text="HISTÓRICO TARIFAS" /></h2>
    <hr class="rule" />
    <fieldset class="legend_info">
        <legend style="font-size: 14px;">Elije el criterio de búsqueda</legend>
        <table class="table.formTable">
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="lblTipoTarifa" CssClass="td_label" Text="Tipo Tarifa " runat="server" />
                    <asp:DropDownList ID="drpTipoTarifa" runat="server" Font-Size="11px" Class="DropDownList"
                        AutoPostBack="true" Width="93px" OnSelectedIndexChanged="drpTipoTarifa_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="auto-style9">
                    <asp:Label ID="blAnio" CssClass="td_label" Text="Año " runat="server" />
                    <asp:DropDownList ID="drpAnio" runat="server" Font-Size="11px" Class="DropDownList"
                        Width="93px" OnSelectedIndexChanged="drpAnio_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="auto-style10">
                    <asp:Label ID="lblMes" CssClass="td_label" Text="Mes " runat="server" />
                    <asp:DropDownList ID="drpMes" runat="server" Font-Size="11px" Class="DropDownList"
                        Width="112px" OnSelectedIndexChanged="drpMes_SelectedIndexChanged" Height="20px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style7">
                    <asp:Label ID="lblRegion" CssClass="td_label" Text="Región " runat="server" Visible="False" />
                    <asp:DropDownList ID="drpRegiones" runat="server" Font-Size="11px" Class="DropDownList"
                        Width="147px" Visible="False" OnSelectedIndexChanged="drpRegiones_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" /></td>
            </tr>
        </table>
    </fieldset>
    <asp:Label runat="server" ID="lblMensaje"></asp:Label>

    <br />
    <div id="div_Tarifa_OM" style="width: 100%" align="center" runat="server">
        <table>
            <tr>
                <td colspan="3" style="text-align: center; font-size: medium; font-weight: bold;">Tarifa OM
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvTarifa_OM" runat="server" Width="100%" AutoGenerateColumns="false" Font-Names="Arial"
            Font-Size="11pt" AllowPaging="true" PageSize="20" CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyleNet" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EditRowStyle CssClass="GridViewEditStyle" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="5%" HeaderText="idTarifa" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblIdTarifaOM" runat="server" Text='<%# Eval("IdTarifaOm")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="5%" HeaderText="IdRegion" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblIdRegionOm" runat="server" Text='<%# Eval("IdRegion")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Región Tarifaria">
                    <ItemTemplate>
                        <asp:Label ID="lblDESCRIPCION" runat="server" Text='<%# Eval("Descripcion")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Cargo por demanda máxima" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblcargoKWDemandaMax" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoKwDemanda"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Cargo por consumo" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCargoKWhDemandaConsumida" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoKwhConsumo"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="15%" HeaderText="Última modificación">
                    <ItemTemplate>
                        <asp:Label ID="lblUser" runat="server" Text='<%# Eval("Usuario")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="5%" HeaderText="Hora" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblHora" runat="server" Text='<%# string.Concat(Eval("Hora"), ":", Eval("Minutos"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="5%" HeaderText="Año" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblAnio" runat="server" Text='<%# Eval("Año")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="5%" HeaderText="Mes">
                    <ItemTemplate>
                        <asp:Label ID="lblMes" runat="server" Text='<%# Eval("Mes")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="5%" HeaderText="Día" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblDia" runat="server" Text='<%# Eval("Dia")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPagerOM" CssClass="pagerDRUPAL" runat="server" PageSize="20"
            AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
            PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
            UrlPaging="false" OnPageChanged="AspNetPagerOM_PageChanged" OnPageChanging="AspNetPager_PageChanging"
            FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
            CurrentPageButtonClass="cpb">
        </webdiyer:AspNetPager>
    </div>

    <div id="div_Tarifa_HM" style="width: 100%" align="center" runat="server">
        <table>
            <tr>
                <td colspan="3" style="text-align: center; font-size: medium; font-weight: bold;">Tarifa HM
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvTarifa_HM" runat="server" Width="100%" AutoGenerateColumns="false" Font-Names="Arial"
            Font-Size="11pt" AllowPaging="true" PageSize="20" CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyleNet" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EditRowStyle CssClass="GridViewEditStyle" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="5%" HeaderText="IdTarifa" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblIdTarifaHm" runat="server" Text='<%# Eval("IdTarifaHm")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="5%" HeaderText="ID_Region" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblIdRegionHM" runat="server" Text='<%# Eval("IdRegion")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Región Tarifaria">
                    <ItemTemplate>
                        <asp:Label ID="lblDESCRIPCION" runat="server" Text='<%# Eval("Descripcion")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Cargo por demanda" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblcargoKWDemandaFac" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoDemanda"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Consumo base" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCargoKWhEnergiaBase" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoBase"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Cargo por consumo intermedio" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCargoKWhEnergiaIntermedia" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoIntermedia"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Cargo por consumo punta" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCargoKWhEnergiaPunta" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoPunta"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Promedio Tarifa" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblPromedio" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"PROMEDIO"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Última modificación">
                    <ItemTemplate>
                        <asp:Label ID="lblUser" runat="server" Text='<%# Eval("Usuario")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="2%" HeaderText="Hora" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblHora" runat="server" Text='<%# string.Concat(Eval("Hora"), ":", Eval("Minutos"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="2%" HeaderText="Año" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblAnio" runat="server" Text='<%# Eval("Año")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="5%" HeaderText="Mes">
                    <ItemTemplate>
                        <asp:Label ID="lblMes" runat="server" Text='<%# Eval("Mes")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="2%" HeaderText="Día" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblDia" runat="server" Text='<%# Eval("Dia")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPagerHM" CssClass="pagerDRUPAL" runat="server" PageSize="20"
            AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
            PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
            UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
            FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
            CurrentPageButtonClass="cpb">
        </webdiyer:AspNetPager>

    </div>

    <div id="div_Tarifa_02" style="width: 100%" align="center" runat="server">
        <table>
            <tr>
                <td colspan="3" style="text-align: center; font-size: medium; font-weight: bold;">Tarifa 02
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvTarifa_02" runat="server" Width="100%" AutoGenerateColumns="false" Font-Names="Arial"
            Font-Size="11pt" AllowPaging="true" PageSize="20" CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyleNet" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EditRowStyle CssClass="GridViewEditStyle" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="10px" HeaderText="ID_TARIFA_02" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblID_TARIFA_02" runat="server" Text='<%# Eval("IdTarifa02")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Cargo Fijo" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCargoFijo" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCostoKwhFijo"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Consumo básico" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCargoFirst50KWh" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCostoKwhBasico"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Consumo intermedio" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCargoMayor50KWh" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCostoKwhIntermedio"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Consumo excedente" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCargoKWhAdicional" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCostoKwhExcedente"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Última modificación">
                    <ItemTemplate>
                        <asp:Label ID="lblUser" runat="server" Text='<%# Eval("Usuario")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="60px" HeaderText="Hora" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblHora" runat="server" Text='<%# string.Concat(Eval("Hora"), ":", Eval("Minutos"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="40px" HeaderText="Año" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblAnio" runat="server" Text='<%# Eval("Año")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Mes" >
                    <ItemTemplate>
                        <asp:Label ID="lblMes" runat="server" Text='<%# Eval("Mes")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="40px" HeaderText="Día" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblDia" runat="server" Text='<%# Eval("Dia")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager02" CssClass="pagerDRUPAL" runat="server" PageSize="20"
            AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
            PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
            UrlPaging="false" OnPageChanged="AspNetPager02_PageChanged" OnPageChanging="AspNetPager_PageChanging"
            FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
            CurrentPageButtonClass="cpb">
        </webdiyer:AspNetPager>
    </div>

    <div id="div_Tarifa_03" style="width: 100%" align="center" runat="server">
        <table>
            <tr>
                <td colspan="3" style="text-align: center; font-size: medium; font-weight: bold;">Tarifa 03
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvTarifa_03" runat="server" Width="100%" AutoGenerateColumns="false" Font-Names="Arial"
            Font-Size="11pt" AllowPaging="true" CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyleNet" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EditRowStyle CssClass="GridViewEditStyle" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="ID_TARIFA_03" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblID_TARIFA_03" runat="server" Text='<%# Eval("IdTarifa03")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Cargo por demanda máxima" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblCargoDemandaMax" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoDemandaMax"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="22%" HeaderText="Cargo adicional por la energía consumida" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblCargoAdicional" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoAdicional"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="15%" HeaderText="Última modificación">
                    <ItemTemplate>
                        <asp:Label ID="lblUser" runat="server" Text='<%# Eval("Usuario")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="8%" HeaderText="Hora" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblHora" runat="server" Text='<%# string.Concat(Eval("Hora"), ":",Eval("Minutos"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="8%" HeaderText="Año" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblAnio" runat="server" Text='<%# Eval("Año")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Mes">
                    <ItemTemplate>
                        <asp:Label ID="lblMes" runat="server" Text='<%# Eval("Mes")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="8%" HeaderText="Día" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="lblDia" runat="server" Text='<%# Eval("Dia")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager03" CssClass="pagerDRUPAL" runat="server" PageSize="20"
            AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
            PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
            UrlPaging="false" OnPageChanged="AspNetPager03_PageChanged" OnPageChanging="AspNetPager_PageChanging"
            FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
            CurrentPageButtonClass="cpb">
        </webdiyer:AspNetPager>
    </div>

    <table width="100%" id="tbButton" runat="server">
        <tr>
            <td class="tbright" style="font-size: 14px; color: #2F2F2F; font-weight: bold; font-family: Arial, Tahoma;">&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" Text="Exportar a: "></asp:Label>
            </td>
            <td class="auto-style11">
                <asp:ImageButton ID="imgExportaExcel" ImageUrl="~/CentralModule/images/ImgExcel.gif" runat="server" OnClick="imgExportaExcel_Click" Height="35px" ToolTip="Exportar a Excel" />
            </td>
            <td class="auto-style11">
                <asp:ImageButton ID="imgExportaPDF" ImageUrl="~/CentralModule/images/Pdf.png" runat="server" OnClick="imgExportaPDF_Click" Height="35px" ToolTip="Exportar a PDF" />
            </td>
        </tr>
    </table>

    <div id="div_Salir" style="width: 100%" align="center" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Button Text="Salir" CssClass="Button" runat="server" Width="81px" ID="btnSalir" OnClick="btnSalir_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
