<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdmonMRV.aspx.cs" Inherits="PAEEEM.MRV.AdmonMRV" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="wucEncabezadoMRV.ascx" TagName="wucEncabezadoMRV" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">
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
                <td style="text-align: right; width: 31%">
                    <telerik:RadButton ID="RadBtnAgregaMedicion" runat="server" 
                        Text="Agregar Medición" onclick="RadBtnAgregaMedicion_Click">
                    </telerik:RadButton>
                </td>
                <td style="width: 3%">
                    &nbsp;
                </td>
                <td style="text-align: center; width: 32%">
                    <telerik:RadButton ID="RadBtnConsultaConsumo" runat="server" 
                        Text="Consulta Consumo" onclick="RadBtnConsultaConsumo_Click">
                    </telerik:RadButton>
                </td>
                <td style="width: 3%">
                    &nbsp;
                </td>
                <td style="text-align: left; width: 31%">
                    <telerik:RadButton ID="RadBtnAgregaCuestionario" runat="server" 
                        Text="Agregar Cuestionario" onclick="RadBtnAgregaCuestionario_Click">
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td colspan="3">
                    <br/>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <br/>
                </td>
            </tr>
            <tr>
                <td style="width: 35%">
                    <telerik:RadGrid ID="rgMediciones" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" Skin="Office2010Silver" CellSpacing="0" 
                        onneeddatasource="rgMediciones_NeedDataSource" 
                        onitemdatabound="rgMediciones_ItemDataBound" 
                        onitemcommand="rgMediciones_ItemCommand">
                        <ClientSettings EnableRowHoverStyle="True">
                            <Selecting CellSelectionMode="None" />
                        </ClientSettings>

                        <MasterTableView ClientDataKeyNames="IdMedicion" NoMasterRecordsText="No se encontrarón Mediciones"
                            AllowAutomaticUpdates="False">
                            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                            <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                            <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="IdMedicion" HeaderText="ID Medición" ReadOnly="true" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DescripcionMedicion" HeaderText="Medición" ReadOnly="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn CommandName="View" Text="Consultar" UniqueName="colView" HeaderText="Consultar"
                                        ButtonType="ImageButton" ImageUrl="~/images/lupa.png" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn CommandName="Edit" Text="Editar" UniqueName="colEdit" HeaderText="Editar" 
                                        ButtonType="ImageButton" ImageUrl="~/images/Edit.gif" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    </telerik:GridButtonColumn>
                                </Columns>

                            <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                            </EditFormSettings>
                        </MasterTableView>

                        <FilterMenu EnableImageSprites="False"></FilterMenu>
                    </telerik:RadGrid>
                </td>
                <td style="width: 15%">
                    <br/>
                </td>
                <td style="width: 50%">
                    <telerik:RadGrid ID="rgCuestionarios" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" Skin="Office2010Silver" CellSpacing="0" 
                        onneeddatasource="rgCuestionarios_NeedDataSource" 
                        onitemdatabound="rgCuestionarios_ItemDataBound" 
                        onitemcommand="rgCuestionarios_ItemCommand">
                        <ClientSettings EnableRowHoverStyle="True">
                            <Selecting CellSelectionMode="None" />
                        </ClientSettings>

                        <MasterTableView ClientDataKeyNames="IdCuestionario" NoMasterRecordsText="No se encontrarón Cuestionarios"
                            AllowAutomaticUpdates="False">
                            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                            <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                            <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="IdCuestionario" HeaderText="ID Cuestionario" ReadOnly="true" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DescripcionCuestionario" HeaderText="Cuestionario de Seguimiento" ReadOnly="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn CommandName="View" Text="Consultar" UniqueName="colView" HeaderText="Consultar"
                                        ButtonType="ImageButton" ImageUrl="~/images/lupa.png" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn CommandName="Edit" Text="Editar" UniqueName="colEdit" HeaderText="Editar" 
                                        ButtonType="ImageButton" ImageUrl="~/images/Edit.gif" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    </telerik:GridButtonColumn>
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
                <td colspan="3">
                    <br/>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center">
                    <telerik:RadButton ID="RadBtnSalirAdmon" runat="server" Text="Salir" 
                        onclick="RadBtnSalirAdmon_Click">
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
