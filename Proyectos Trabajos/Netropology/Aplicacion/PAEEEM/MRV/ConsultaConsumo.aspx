<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaConsumo.aspx.cs" Inherits="PAEEEM.MRV.ConsultaConsumo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="wucEncabezadoMRV.ascx" TagName="wucEncabezadoMRV" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">--%>
        <div style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td colspan="5">
                        <uc1:wucEncabezadoMRV id="wucEncabezadoMRV1" runat="server"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadGrid ID="rgConsumos" runat="server" AutoGenerateColumns="False" 
                            GridLines="None" Skin="Office2010Silver" CellSpacing="0" 
                            onneeddatasource="rgConsumos_NeedDataSource" 
                            onitemcommand="rgConsumos_ItemCommand" >
                            <ClientSettings EnableRowHoverStyle="True">
                                <Selecting CellSelectionMode="None" />
                            </ClientSettings>
                            <ExportSettings HideStructureColumns="true">
                            </ExportSettings>
                            <MasterTableView ClientDataKeyNames="IdConsultaConsumo" NoMasterRecordsText="No se encontrarón Consumos"
                                CommandItemDisplay="Top" AllowAutomaticUpdates="False">
                                <CommandItemSettings ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportar a Excel" ShowExportToExcelButton="True" ShowExportToPdfButton="True" ShowRefreshButton="False" ShowAddNewRecordButton="False"></CommandItemSettings>

                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="IdConsultaConsumo" HeaderText="ID Consumo" ReadOnly="true" Visible="False">
                                        </telerik:GridBoundColumn>
									    <telerik:GridBoundColumn DataField="FechaConsumo" HeaderText="Fecha Consulta Consumos" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="View" Text="Ver detalle..." UniqueName="colView" HeaderText="Consultar"
                                        ButtonType="LinkButton" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-ForeColor="Blue">
                                    </telerik:GridButtonColumn>
                                    </Columns>

                                <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                                </EditFormSettings>
                            </MasterTableView>

                            <FilterMenu EnableImageSprites="False"></FilterMenu>
                        </telerik:RadGrid>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                    <td colspan="2">
                        <telerik:RadGrid ID="rgHistoricoConsumos" runat="server" AutoGenerateColumns="False" 
                            GridLines="None" Skin="Office2010Silver" CellSpacing="0" 
                            onneeddatasource="rgHistoricoConsumos_NeedDataSource" 
                            onitemcommand="rgHistoricoConsumos_ItemCommand" >
                            <ClientSettings EnableRowHoverStyle="True">
                                <Selecting CellSelectionMode="None" />
                            </ClientSettings>
                            <ExportSettings HideStructureColumns="true">
                            </ExportSettings>
                            <MasterTableView ClientDataKeyNames="IdHistorico" NoMasterRecordsText="No se encontrarón Consumos"
                                CommandItemDisplay="Top" AllowAutomaticUpdates="False">
                                <CommandItemSettings ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportar a Excel" ShowExportToExcelButton="True" ShowExportToPdfButton="True" ShowRefreshButton="False" ShowAddNewRecordButton="False"></CommandItemSettings>

                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="IdHistorico" HeaderText="ID Consumo" ReadOnly="true">
                                        </telerik:GridBoundColumn>
									    <telerik:GridBoundColumn DataField="FechaPeriodo" HeaderText="Fecha Periodo" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Consumo" HeaderText="Consumo" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="Demanda" HeaderText="Demanda" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="FactorPotencia" HeaderText="Factor de Potencia" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                    </Columns>

                                <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                                </EditFormSettings>
                            </MasterTableView>

                            <FilterMenu EnableImageSprites="False"></FilterMenu>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="5">                        
                        <telerik:RadButton ID="RadBTnSalir" runat="server" Text="Salir" 
                            onclick="RadBTnSalir_Click">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </div>
    <%--</telerik:RadAjaxPanel>--%>
</asp:Content>
