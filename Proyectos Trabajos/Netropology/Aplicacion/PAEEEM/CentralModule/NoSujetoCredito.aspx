<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NoSujetoCredito.aspx.cs" Inherits="PAEEEM.CentralModule.NoSujetoCredito" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function Validarfiltros() {
            var rpu = document.getElementById('<%= txtRPU.ClientID %>');

            if (rpu.value.length > 0) {

                document.getElementById('<%= btnBuscar.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%= btnBuscar.ClientID %>').disabled = true;
            }
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2010Silver"></telerik:RadAjaxLoadingPanel>--%>
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" Width="100%"  runat="server" Visible="true">--%>
    
    <asp:Label ID="LabelEncabezado" runat="server" Text="Consultar Motivos No Sujeto de Crédito"
        Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
    <hr class="ruleNet" />

    <div align="center">
        <fieldset class="fieldset_Netro">
            <br />
            <table align="center">
                <tr style="width: 350px;">
                    <td>
                        <asp:Label runat="server" Text="Numero de Servicio" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                <telerik:RadTextBox ID="txtRPU" runat="server" EmptyMessage="RPU"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <telerik:RadButton ID="btnBuscar" runat="server" Text="BUSCAR" OnClick="btnBuscar_Click"></telerik:RadButton>
                        <%--<asp:Button ID="btnBuscar1" runat="server" Text="BUSCAR" Enabled="False"></asp:Button>--%>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <%--    </telerik:RadAjaxPanel>--%>
    <br />
    <div>
        <table id="Table6" width="100%" runat="server">
            <tr>
                <td class="tbright" width="90%"></td>
                <td class="auto-style11">
                    <asp:ImageButton ID="imgExportaExcel" Visible="False" ImageUrl="~/CentralModule/images/ImgExcel.gif" runat="server" OnClick="imgExportaExcel_OnClick" Height="35px" ToolTip="Exportar a Excel" />
                </td>
                <td class="auto-style11" align="right">
                    <asp:ImageButton ID="imgExportaPDF" Visible="False" ImageUrl="~/CentralModule/images/Pdf.png" runat="server" OnClick="imgExportaPDF_Click" Height="20px" ToolTip="Exportar a PDF" />&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
        <telerik:RadGrid ID="RadGrid1" Visible="true" Culture="es-MX" runat="server" AutoGenerateColumns="False" PagerStyle-Mode="NumericPages" PageSize="3" CellSpacing="0" GridLines="None"
            OnItemCommand="RadGrid1_OnItemCommand">
            <ExportSettings HideStructureColumns="true">
            </ExportSettings>
            <MasterTableView
                CommandItemDisplay="Top" NoMasterRecordsText="No se encontrarón registros">
                <CommandItemSettings ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportar a Excel" ShowExportToExcelButton="True" ShowExportToPdfButton="false" ShowRefreshButton="False" ShowAddNewRecordButton="False"></CommandItemSettings>
                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="NoIntentos" HeaderText="No Intentos"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="NombreRazonSocial" HeaderText="Nombre/Razon Social"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Causa" HeaderText="Causa"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Motivo" HeaderText="Motivo"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Datos" HeaderText="Datos"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Zona" HeaderText="Zona"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Region" HeaderText="Region"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Distribuidor" HeaderText="Distribuidor"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="IdTrama" HeaderText="IDTrama"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <%--        </telerik:RadAjaxPanel>--%>
    </div>
    <br />
    <div>

        <%--        <telerik:RadAjaxPanel ID="RadAjaxPanel2" LoadingPanelID="RadAjaxLoadingPanel1" runat="server" Visible="true">--%>
        <table visible="True" >
            <tr>
                <td style="vertical-align: top">
                    <asp:Label ID="tit1" runat="server" Width="100%" Visible="False" Text="EQUIPOS DE BAJA EFICIENCIA" ForeColor="deepskyblue"></asp:Label>
                    <telerik:RadGrid ID="RadGrid2" Visible="true" runat="server" AutoGenerateColumns="False" PagerStyle-Mode="NumericPages" PageSize="3" CellSpacing="0" GridLines="None">

                        <MasterTableView NoMasterRecordsText="Sin registos de equipos de baja">

                            <Columns>
                                <telerik:GridBoundColumn DataField="NoIntentos" HeaderText="No Intentos"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Dx_Tecnologia" HeaderText="Tecnologia"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Dx_Grupo" HeaderText="Grupo"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Dx_Tipo_Producto" HeaderText="Producto"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Dx_Consumo" HeaderText="Capacidad"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cantidad" HeaderText="Cantidad"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
                <td style="vertical-align: top">
                    <asp:Label ID="tit2" runat="server" Width="100%" Visible="False" Text="EQUIPOS DE ALTA EFICIENCIA" ForeColor="deepskyblue"></asp:Label>
                    <telerik:RadGrid ID="RadGrid3" Visible="true" runat="server" AutoGenerateColumns="False" PagerStyle-Mode="NumericPages" PageSize="3" CellSpacing="0" GridLines="None">

                        <MasterTableView NoMasterRecordsText="Sin registos de equipos de alta">
                            <Columns>
                                <telerik:GridBoundColumn DataField="NoIntentos" HeaderText="No Intentos"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Producto" HeaderText="Producto"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Dx_Marca" HeaderText="Marca"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Dx_Modelo" HeaderText="Modelo"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Dx_Sistema" HeaderText="Capacidad"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cantidad" HeaderText="Cantidad"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Importe_Total_Sin_IVA" HeaderText="Precio Unitario Sin Iva"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Gasto_Instalacion" HeaderText="Gastos de Instalacion"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
        <%--        </telerik:RadAjaxPanel>--%>
    </div>
        <%--</telerik:RadAjaxPanel>--%>
</asp:Content>
