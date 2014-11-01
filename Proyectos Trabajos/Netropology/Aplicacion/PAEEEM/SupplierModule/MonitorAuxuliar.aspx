<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonitorAuxuliar.aspx.cs" Inherits="PAEEEM.SupplierModule.MonitorAuxuliar" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">
        <div style="width: 100%">
            <asp:Label ID="LabelEncabezado" runat="server" Text="BUSQUEDA DE BASE AUXILIAR RPU´S" 
                Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
            <hr class="ruleNet" />
            
            <table style="width: 100%">
                <tr>
                    <td style="width: 15%">
                        <br/>
                    </td>
                    
                    <td style="width: 70%">
                        <fieldset class="fieldset_Netro" style="width: 100%">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <br/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <br/>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="ESTATUS:"></asp:Label>
                                        <br/>
                                        <telerik:RadComboBox ID="RadCmbEstatus" runat="server">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="RPU:"></asp:Label>
                                        <br/>
                                        <telerik:RadTextBox ID="RadTxtRPU" runat="server">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="TARIFA:"></asp:Label>
                                        <br/>
                                        <telerik:RadComboBox ID="RadCmbTarifa" runat="server">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="width: 10%">
                                        <br/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <br/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7" style="text-align: center">
                                        <telerik:RadButton ID="RadBtnBuscar" runat="server" Text="Buscar" 
                                            onclick="RadBtnBuscar_Click">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    
                    <td style="width: 15%">
                        <br/>
                    </td>
                </tr>
            </table>

            <telerik:RadGrid ID="rgAuxuliar" runat="server" AutoGenerateColumns="False" 
                GridLines="None" Skin="Office2010Silver" CellSpacing="0" 
                onneeddatasource="rgAuxuliar_NeedDataSource" >
                <ClientSettings EnableRowHoverStyle="True">
                    <Selecting CellSelectionMode="None" />
                </ClientSettings>

                <MasterTableView ClientDataKeyNames="Rpu" NoMasterRecordsText="No se encontro Información"
                    AllowAutomaticUpdates="False">
                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                    <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                    <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Rpu" HeaderText="Rpu" ReadOnly="true" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Cuenta" HeaderText="Cuenta" ReadOnly="true">
                            </telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="Tarifa" HeaderText="Tarifa" ReadOnly="true">
                            </telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="PeriodoPago" HeaderText="Periodo" ReadOnly="true">
                            </telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre del Cliente" ReadOnly="true">
                            </telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="FechaAdicion" HeaderText="Fecha de Ingreso" ReadOnly="true">
                            </telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="Vigencia" HeaderText="Vigencia" ReadOnly="true">
                            </telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus" ReadOnly="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Acciones" HeaderText="Acciones">
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="RadCmbAcciones" runat="server" Enabled="False">
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkSeleccionar" runat="server" 
                                        oncheckedchanged="ChkSeleccionar_CheckedChanged" AutoPostBack="True" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>

                    <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                    </EditFormSettings>
                </MasterTableView>

                <FilterMenu EnableImageSprites="False"></FilterMenu>
            </telerik:RadGrid>
            <br/>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <telerik:RadButton ID="RadBtnAceptar" runat="server" Text="Aceptar" 
                            onclick="RadBtnAceptar_Click">
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
