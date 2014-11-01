<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteDistribuidores.aspx.cs" Inherits="PAEEEM.CustomReports.ReporteDistribuidores" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">
        <div style="width: 100%">
            <asp:Label ID="LabelEncabezado" runat="server" Text="REPORTE DISTRIBUIDORES" 
                Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
            <hr class="ruleNet" />
            
            <fieldset class="fieldset_Netro" style="width: 1000px">
                <legend style="font-size: small;">Búsqueda</legend>
                <table style="width: 100%">
                    <tr>
                        <td colspan="7">
                            <br/>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 22%">
                            <asp:Label ID="Label1" Font-Size="Small" runat="server" Text="Matriz:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbMatriz" runat="server" 
                                onselectedindexchanged="RadCmbMatriz_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 3%">
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 22%">
                            <asp:Label ID="Label2" Font-Size="Small" runat="server" Text="Sucursal:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbSucursal" runat="server">
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 3%">
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 22%">
                            <asp:Label ID="Label3" Font-Size="Small" runat="server" Text="Estatus:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbEstatus" runat="server">
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 3%">
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 22%">
                            <asp:Label ID="Label4" Font-Size="Small" runat="server" Text="Region:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbRegion" runat="server" 
                                onselectedindexchanged="RadCmbRegion_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <br/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" Font-Size="Small" runat="server" Text="Zona:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbZona" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="RadCmbZona_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label6" Font-Size="Small" runat="server" Text="Estado:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbEstado" runat="server" 
                                onselectedindexchanged="RadCmbEstado_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label7" Font-Size="Small" runat="server" Text="Delegación/Municipio:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbMunicipio" runat="server">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label8" Font-Size="Small" runat="server" Text="Tecnología:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbTecnologia" runat="server">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <br/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" Font-Size="Small" runat="server" Text="PERIODO ALTA:"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label10" Font-Size="Small" runat="server" Text="Inicio:"></asp:Label>
                            <telerik:RadDatePicker ID="RadDateFechaInicio" runat="server">
                        </telerik:RadDatePicker>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>  
                        <td>
                            <asp:Label ID="Label11" Font-Size="Small" runat="server" Text="Fin:"></asp:Label>
                            <telerik:RadDatePicker ID="RadDateFechaFin" runat="server">
                        </telerik:RadDatePicker>
                        </td> 
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <br/>
                        </td>                     
                    </tr>
                    <tr>
                        <td colspan="7">
                            <br/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br/>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td colspan="3" style="text-align: center">
                            <telerik:RadButton ID="RadBtnBuscar" runat="server" Text="Buscar" 
                                onclick="RadBtnBuscar_Click">
                            </telerik:RadButton>
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="RadBtnLimpiar" runat="server" Text="Limpiar">
                            </telerik:RadButton>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <br/>
                        </td>
                    </tr>
                </table>
            </fieldset>
            
            <table style="width: 100%">
                <tr>
                    <td>
                        <telerik:RadGrid ID="rgDistribuidores" runat="server" AutoGenerateColumns="False" 
                            GridLines="None" Skin="Office2010Silver" CellSpacing="0" 
                            onneeddatasource="rgDistribuidores_NeedDataSource" 
                            AllowPaging="True"
                            PageSize="7">
                            <ClientSettings EnableRowHoverStyle="True">
                                <Selecting CellSelectionMode="None" />
                            </ClientSettings>
                            <ExportSettings HideStructureColumns="true">
                            </ExportSettings>
                            <MasterTableView ClientDataKeyNames="IdSucursal" NoMasterRecordsText="No se encontrarón Distribuidores"
                                CommandItemDisplay="Top" AllowAutomaticUpdates="False">
                                <CommandItemSettings ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportar a Excel" ShowExportToExcelButton="True" ShowExportToPdfButton="True" ShowRefreshButton="False" ShowAddNewRecordButton="False"></CommandItemSettings>

                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                    
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Tipo" HeaderText="Tipo" ReadOnly="true">
                                        </telerik:GridBoundColumn>
									    <telerik:GridBoundColumn DataField="IdMatriz" HeaderText="Id Matriz" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="IdSucursal" HeaderText="Id Sucursal" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="Rfc" HeaderText="Rfc Distribuidor" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="RazonSocial" HeaderText="Razon Social" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="NombreComercial" HeaderText="Nombre Comercial" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="IdIntelisis" HeaderText="Id Intelisis Distribudor" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="IvaDistribuidor" HeaderText="Iva Distribuidor" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="CpFiscal" HeaderText="CP" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="EstadoFiscal" HeaderText="Estado" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="MunicipioFiscal" HeaderText="Municipio" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="ColoniaFiscal" HeaderText="Colonia" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="CalleFiscal" HeaderText="Calle" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="NumeroFiscal" HeaderText="Numero" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="CpFisico" HeaderText="CP" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="EstadoFisico" HeaderText="Estado" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="MunicipioFisico" HeaderText="Municipio" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="ColoniaFisico" HeaderText="Colonia" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="CalleFisico" HeaderText="Calle" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="NumeroFisico" HeaderText="Número" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="RepresentanteLegal" HeaderText="Representante Legal" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="NombreContacto" HeaderText="Nombre" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="EmailContacto" HeaderText="Email" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="TelefonoContacto" HeaderText="Telefono" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Region" HeaderText="Región" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="Zona" HeaderText="Zona" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="FechaAlta" HeaderText="Fecha Alta" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="FechaUltimoEstatus" HeaderText="Fecha Ultimo Estatus" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										
										<%--<telerik:GridBoundColumn DataField="RefrigeracionComercial" HeaderText="RC" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="AireAcondicionado" HeaderText="AA" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="MotoresElectricos" HeaderText="ME" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="IluminacionLineal" HeaderText="IL" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="IluminacionLed" HeaderText="ILed" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="IluminacionInduccion" HeaderText="IIn" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="Subestaciones" HeaderText="SE" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="BancoCapaciores" HeaderText="BC" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="CamarasRefrigeracion" HeaderText="CR" ReadOnly="true">
                                        </telerik:GridBoundColumn>
										<telerik:GridBoundColumn DataField="CalentadoresSolares" HeaderText="CS" ReadOnly="true">
                                        </telerik:GridBoundColumn>--%>
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                        </telerik:GridTemplateColumn>
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
                    <td>
                        <telerik:RadChart ID="RadChartDistActivosRegion" runat="server" 
                            TempImagesFolder="Temp" 
                            Width="800px" Height="600px">
                            <Series>
                                <telerik:ChartSeries Name="Series 1">
                                </telerik:ChartSeries>
                                <telerik:ChartSeries Name="Series 2">
                                </telerik:ChartSeries>
                            </Series>
                            <Legend Visible="False">
                                <Appearance Visible="False">
                                </Appearance>
                            </Legend>
                            <PlotArea>
                                <XAxis LayoutMode="Normal">
                                    <Appearance>
                                        <LabelAppearance Position-AlignedPosition="Top">
                                        </LabelAppearance>
                                        <TextAppearance TextProperties-Color="Black" 
                                            TextProperties-Font="Verdana, 5.25pt">
                                        </TextAppearance>
                                    </Appearance>
                                    <AxisLabel Visible="True">
                                        <Appearance Visible="True">
                                        </Appearance>
                                    </AxisLabel>
                                </XAxis>
                                <Appearance Dimensions-Margins="18%, 10%, 22%, 10%">
                                </Appearance>
                            </PlotArea>
                            <ChartTitle>
                                <TextBlock Text="DISTRIBUIDORES ACTIVOS POR REGIÓN">
                                </TextBlock>
                            </ChartTitle>
                        </telerik:RadChart>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadChart ID="RadChartDistActivosZona" runat="server" 
                            TempImagesFolder="Temp"
                            Width="800px" Height="600px">
                            <Series>
                                <telerik:ChartSeries Name="Series 1">
                                </telerik:ChartSeries>
                                <telerik:ChartSeries Name="Series 2">
                                </telerik:ChartSeries>
                            </Series>
                            <Legend Visible="False">
                                <Appearance Visible="False">
                                </Appearance>
                            </Legend>
                            <PlotArea>
                                <XAxis LayoutMode="Normal">
                                    <Appearance>
                                        <LabelAppearance Position-AlignedPosition="Top">
                                        </LabelAppearance>
                                        <TextAppearance TextProperties-Color="Black" 
                                            TextProperties-Font="Verdana, 5.25pt">
                                        </TextAppearance>
                                    </Appearance>
                                    <AxisLabel Visible="True">
                                        <Appearance Visible="True">
                                        </Appearance>
                                    </AxisLabel>
                                </XAxis>
                                <Appearance Dimensions-Margins="18%, 10%, 22%, 10%">
                                </Appearance>
                            </PlotArea>
                            <ChartTitle>
                                <TextBlock Text="DISTRIBUIDORES ACTIVOS POR ZONA">
                                </TextBlock>
                            </ChartTitle>
                        </telerik:RadChart>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadChart ID="RadChartHistDistribuidores" runat="server" 
                            TempImagesFolder="Temp"
                            Width="800px" DefaultType="Point" Height="600px" >
                            <Series>
                                <telerik:ChartSeries Name="Series 1">
                                </telerik:ChartSeries>
                                <telerik:ChartSeries Name="Series 2">
                                </telerik:ChartSeries>
                            </Series>
                            <Legend Visible="False">
                                <Appearance Visible="False">
                                </Appearance>
                            </Legend>
                            <PlotArea>
                                <XAxis>
                                    <Appearance>
                                        <LabelAppearance Position-AlignedPosition="Top">
                                        </LabelAppearance>
                                        <TextAppearance TextProperties-Color="Black" 
                                            TextProperties-Font="Verdana, 5.25pt">
                                        </TextAppearance>
                                    </Appearance>
                                </XAxis>
                                <Appearance Dimensions-Margins="18%, 10%, 22%, 10%">
                                </Appearance>
                            </PlotArea>
                        </telerik:RadChart>
                    </td>
                </tr>
            </table>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
