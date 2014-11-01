<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistoricoProveedor.aspx.cs" Inherits="PAEEEM.CentralModule.HostoricoProveedor" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function EnableDisable() {


            var NC = document.getElementById('<%= RadTxtDistNC.ClientID %>');
            var RS = document.getElementById('<%= RadTxtDistRS.ClientID %>');
            var reg = document.getElementById('<%= RadCbxRegion.ClientID %>');
            var zon = document.getElementById('<%= RadCbxZona.ClientID %>');
            var sta = document.getElementById('<%= RadCbxStatus.ClientID %>');
            var btnBus = $find("<%= btnBuscar.ClientID %>");
            var btnLim = $find("<%= btnLimpiar.ClientID %>");




            if (NC.value.length == 0 && RS.value.length == 0 && reg.value == "Seleccione" && (zon.value == "Seleccione región" || zon.value == "Seleccione") && sta.value == "Seleccione") {
                btnBus.set_enabled(false);
                btnLim.set_enabled(false);
            } else {
                btnBus.set_enabled(true);
                btnLim.set_enabled(true);
            }



        }


        ////////pruebas ////
        function validarCoboxs(sender, eventarqs) {

            alert("You typed " + sender.get_text());

            var combo = $find("<%= RadCbxZona.ClientID %>");
            var rrr = $find("<%= btnLimpiar.ClientID %>");
            combo.disable();

            rrr.set_enabled(true);
        }
        ////////////////////////

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Office2010Silver">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Office2010Silver">
    </telerik:RadWindowManager>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel2" runat="server">
        <asp:Label ID="LabelEncabezado" runat="server" Text="HISTÓRICO DE PROVEEDORES "
            Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
        <hr class="ruleNet" />
        <fieldset class="fieldset_Netro">

            <table align="center">
                <tr style="width: 350px;">
                    <td align="center">
                        <asp:Label ID="LblDistNC" runat="server" Text="Distribuidor NC:" Font-Size="Small"></asp:Label>
                        <telerik:RadTextBox ID="RadTxtDistNC" runat="server" EmptyMessage="Distribuidor NC" Width="200px"></telerik:RadTextBox>
                    </td>
                    <td style="align-items: stretch">
                        <asp:Label ID="LblDistRS" runat="server" Text="Distribuidor RS:" Font-Size="Small"></asp:Label>
                        <telerik:RadTextBox ID="RadTxtDistRS" runat="server" EmptyMessage="Distribuidor RS" Width="200px"></telerik:RadTextBox>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblReg" runat="server" Text="Región:" Font-Size="Small"></asp:Label>
                        <telerik:RadComboBox ID="RadCbxRegion" runat="server" Width="150px" OnSelectedIndexChanged="RadCbxRegion_SelectedIndexChanged" AutoPostBack="true" OnClientSelectedIndexChanged="EnableDisable"></telerik:RadComboBox>

                    </td>
                    <td align="center">
                        <asp:Label ID="LblZona" runat="server" Text="Zona:" Font-Size="Small"></asp:Label>
                        <telerik:RadComboBox ID="RadCbxZona" runat="server" Width="150px" OnClientSelectedIndexChanged="EnableDisable"></telerik:RadComboBox>
                    </td>
                    <td align="center">
                        <asp:Label ID="Lbstatus" runat="server" Text="Estatus:" Font-Size="Small"></asp:Label>
                        <telerik:RadComboBox ID="RadCbxStatus" runat="server" Width="150px" OnClientSelectedIndexChanged="EnableDisable"></telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </fieldset>
    </telerik:RadAjaxPanel>
    <table style="width: 100%">
        <tr>
            <td align="center">
                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" Enabled="False" OnClick="btnBuscar_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <telerik:RadButton ID="btnLimpiar" runat="server" Text="Limpiar" Enabled="False" OnClick="btnLimpiar_Click" />
            </td>
        </tr>
    </table>
    <table id="Table6" width="100%" runat="server">
        <tr>
            <td class="tbright" width="90%"></td>
            <td class="auto-style11" align="right">
                <asp:ImageButton ID="imgExportaPDF" ImageUrl="~/CentralModule/images/Pdf.png" runat="server" OnClick="imgExportaPDF_Click" Height="20px" ToolTip="Exportar a PDF" />&nbsp;&nbsp;&nbsp;
            </td>
            <td class="auto-style11">
                <asp:ImageButton ID="imgExportaExcel"  ImageUrl="~/CentralModule/images/ImgExcel.gif" runat="server" OnClick="imgExportaExcel_Click" Height="35px" ToolTip="Exportar a Excel" />
            </td>
        </tr>
    </table>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" LoadingPanelID="RadAjaxLoadingPanel2" runat="server">
        <div id="ww" runat="server">
            <fieldset class="fieldset_Netro">
                <table align="center">
                    <tr>
                        <td align="center">
                            <div>
                                <telerik:RadGrid ID="rgHistoricoProveedor" runat="server" AutoGenerateColumns="False"
                                    GridLines="None" Skin="Office2010Silver" CellSpacing="0" AllowMultiRowSelection="true"
                                    OnNeedDataSource="rgHistoricoProveedor_NeedDataSource"
                                    AllowPaging="True">
                                    <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom" />

                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting CellSelectionMode="None" />
                                    </ClientSettings>
                                    <MasterTableView ClientDataKeyNames="idProveedor" NoMasterRecordsText="No se encontraron registros con los criterios ingresados" AllowAutomaticUpdates="False" >

                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="region" HeaderText="REGIÓN" ReadOnly="true" Visible="true" UniqueName="region"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="zona" HeaderText="ZONA" ReadOnly="true" Visible="true" UniqueName="zona"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="idProveedor" HeaderText="ID DISTRIBUIDOR" ReadOnly="true" Visible="true" UniqueName="idProveedor"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="tipo" HeaderText="TIPO" ReadOnly="true" Visible="true" UniqueName="tipo"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NomNC" HeaderText="DISTRIBUIDOR NC" ReadOnly="true" Visible="true" UniqueName="NomNC"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NomRS" HeaderText="DISTRIBUIDOR RS" ReadOnly="true" Visible="true" UniqueName="NomRS"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Status" HeaderText="ESTATUS" ReadOnly="true" Visible="true" UniqueName="Status"></telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn FilterControlAltText="Filter colFechaRevi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" UniqueName="fechaEstatus" HeaderText="FECHA ESTATUS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConFecha" runat="server" Text='<%# Convert.ToDateTime(Eval("fechaEstatus")).ToShortDateString()%>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="motivo" HeaderText="MOTIVO" ReadOnly="true" Visible="true" UniqueName="motivo"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="usuario" HeaderText="USUARIO" ReadOnly="true" Visible="true" UniqueName="usuario"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>

                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <br />
                            <telerik:RadButton ID="btnSalir" runat="server" Text="Salir" OnClick="btnSalir_Click"></telerik:RadButton>
                        </td>
                    </tr>

                </table>

            </fieldset>
        </div>
    </telerik:RadAjaxPanel>

</asp:Content>
