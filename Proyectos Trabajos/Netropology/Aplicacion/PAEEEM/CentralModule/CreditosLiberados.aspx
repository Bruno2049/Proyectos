<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreditosLiberados.aspx.cs" Inherits="PAEEEM.CentralModule.CreditosLiberados" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .classImage {
            background: url(img/categories.png);
            background-position: 0 0;
            width: 150px;
            height: 94px;
        }

        .classHoveredImage {
            background-position: 0 -100px;
        }

        .classPressedImage {
            background-position: 0 -200px;
        }
    </style>
    <style type="text/css">
        .Label {
            width: 160px;
            color: #333333;
            font-size: 16px;
        }

        .Label_1 {
            width: 100px;
            color: #333333;
            font-size: 16px;
        }

        .DropDownList {
            width: 330px;
        }

        .Button {
            width: 120px;
        }

        .CenterButton {
            width: 120px;
            margin-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <br />
            <asp:Label runat="server" ID="LBL_Titulo" Text="Reporte de Creditos Liberados" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <br />
            <br />
            <fieldset class="legend_info">
                <table class="table.formTable" align="center">
                    <tr>
                        <td>
                            <asp:Label ID="Lbl_Tipo_Distribuidor" CssClass="td_label" Text="Tipo de Distribuidor: " runat="server" Width="150px" />
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RCB_TipoDistribuidor" Width="160px">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="TODOS" Value="0" />
                                </Items>
                            </telerik:RadComboBox>

                        </td>
                        <td></td>
                        <td>
                            <asp:Label runat="server" CssClass="td_label" ID="LBL_Estado" Text="Estado: " Width="70px"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RCB_Estado" Width="160px" OnSelectedIndexChanged="RCB_Estado_OnSelectedIndexChanged" AutoPostBack="True">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="TODOS" Value="0" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label runat="server" CssClass="td_label" ID="LBL_Region" Text="Región: " Width="70px"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RCB_Region" Width="160px" OnSelectedIndexChanged="RCB_Region_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="TODOS" Value="0" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" CssClass="td_label" ID="LBL_NoCredito" Text="No_Credito: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="TXB_NoCredito" MaxLength="16" Width="160px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label runat="server" CssClass="td_label" ID="LBL_Municipio" Text="Municipio: "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RCB_Municipio" Width="160px">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="TODOS" Value="0" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label runat="server" CssClass="td_label" ID="LBL_Zona" Text="Zona: "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RCB_Zona" Width="160px"></telerik:RadComboBox>
                        </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <asp:Label runat="server" ID="LBLPeriodo_Colocacion" Text="Periodo de Colocacion:" CssClass="td_label"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" CssClass="td_label" ID="LBL_Desde" Text="Desde "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="RDPKR_Desda" runat="server" Width="150px" AutoPostBack="true"
                                DateInput-EmptyMessage="Elige Fecha" MinDate="01/01/2014" MaxDate="<%# DateTime.Today %>" Culture="es-MX">
                                <Calendar>
                                    <SpecialDays>
                                        <telerik:RadCalendarDay />
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label runat="server" CssClass="td_label" ID="LBL_Hasta" Text="Hasta "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="RDPKR_Hasta" runat="server" Width="150px" AutoPostBack="true"
                                DateInput-EmptyMessage="Elige Fecha" MinDate="01/01/2014" MaxDate='<%# DateTime.Today %>' Culture="es-MX">
                                <Calendar>
                                    <SpecialDays>
                                        <telerik:RadCalendarDay />
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <telerik:RadButton runat="server" ID="RBTN_GeneraReporte" Text="Generar Reporte" OnClick="RBTN_GeneraReporte_OnClick"></telerik:RadButton>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <br />
            <div style="width: 100%" align="center" runat="server">
                <telerik:RadGrid ID="DGV_CL" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowSorting="True"
                    AllowPaging="True" GridLines="None" OnPageSizeChanged="DGV_CreditosLiberados_OnPageSizeChanged" OnPageIndexChanged="DGV_CreditosLiberados_OnPageIndexChanged">

                    <PagerStyle Mode="NextPrevNumericAndAdvanced" Position="Bottom" AlwaysVisible="False"></PagerStyle>
                    <ClientSettings>
                        <Selecting CellSelectionMode="None" />
                    </ClientSettings>
                    <MasterTableView NoDetailRecordsText="No hay ninguna solicitud para validar" DataKeyNames="No_Credito">
                        <Columns>
                            <telerik:GridBoundColumn DataField="No_Credito" UniqueName="No_Credito" HeaderText="No_Credito" />
                            <telerik:GridBoundColumn DataField="FechaIngreso" UniqueName="FechaIngreso" HeaderText="Fecha de Ingreso" />
                            <telerik:GridBoundColumn DataField="FechaAutorizado" UniqueName="FechaAutorizado" HeaderText="Fecha de Autorizacion" />
                            <telerik:GridBoundColumn DataField="FechaLiberado" UniqueName="FechaLiberado" HeaderText="Fecha de Liberacion" />
                            <telerik:GridBoundColumn DataField="Tarifa" UniqueName="Tarifa" HeaderText="Tarifa" />
                            <telerik:GridBoundColumn DataField="TarifaOrigen" UniqueName="TarifaOrigen" HeaderText="TarifaOrigen" />
                            <telerik:GridBoundColumn DataField="intelisis" UniqueName="intelisis" HeaderText="Intelisis" />
                            <telerik:GridBoundColumn DataField="RazonSocial" UniqueName="RazonSocial" HeaderText="Razon Social Cliente" />
                            <telerik:GridBoundColumn DataField="NombreComercial" UniqueName="NombreComercial" HeaderText="Nombre Comercial Cliente" />
                            <telerik:GridBoundColumn DataField="RFC" UniqueName="RFC" HeaderText="RFC" />
                            <telerik:GridBoundColumn DataField="GiroComercial" UniqueName="GiroComercial" HeaderText="Giro Comercial" />
                            <telerik:GridBoundColumn DataField="Estado" UniqueName="Estado" HeaderText="Estado" />
                            <telerik:GridBoundColumn DataField="Municipio" UniqueName="Municipio" HeaderText="Municipio" />
                            <telerik:GridBoundColumn DataField="MontoFinanciado" UniqueName="MontoFinanciado" HeaderText="Monto Financiado" />
                            <telerik:GridBoundColumn DataField="GastosInstalacion" UniqueName="GastosInstalacion" HeaderText="Gastos de Instalacion" />
                            <telerik:GridBoundColumn DataField="Incentivo" UniqueName="Incentivo" HeaderText="Incentivo" />
                            <telerik:GridBoundColumn DataField="PAmortizacion" UniqueName="PAmortizacion" HeaderText="Fecha de Amortizacion" />
                            <telerik:GridBoundColumn DataField="Amortizacion" UniqueName="Amortizacion" HeaderText="Amortizacion" />
                            <telerik:GridBoundColumn DataField="Chatarrizacion" UniqueName="Chatarrizacion" HeaderText="Chatarrizacion" />
                            <telerik:GridBoundColumn DataField="kwhAhorro" UniqueName="kwhAhorro" HeaderText="kwh Ahorro" />
                            <telerik:GridBoundColumn DataField="kwAhorro" UniqueName="kwAhorro" HeaderText="kwA horro" />
                            <telerik:GridBoundColumn DataField="FactorPotencia" UniqueName="FactorPotencia" HeaderText="Factor de Potencia" />
                            <telerik:GridBoundColumn DataField="kwhPromedio" UniqueName="kwhPromedio" HeaderText="kwh Promedio" />
                            <telerik:GridBoundColumn DataField="kwPromedio" UniqueName="kwPromedio" HeaderText="kw Promedio" />
                            <telerik:GridBoundColumn DataField="FechaPagoDist" UniqueName="FechaPagoDist" HeaderText="Fecha de Pago a Distribuidor" />
                            <telerik:GridBoundColumn DataField="MontoPagoDist" UniqueName="MontoPagoDist" HeaderText="Monto de Pago a Distribuidor" />
                            <telerik:GridBoundColumn DataField="RazonSocialDist" UniqueName="RazonSocialDist" HeaderText="Razon Social de Distribuidor" />
                            <telerik:GridBoundColumn DataField="NombreComercialDist" UniqueName="NombreComercialDist" HeaderText="Nombre Comercial de Distribuidor" />
                            <telerik:GridBoundColumn DataField="TipoSucuralDist" UniqueName="TipoSucuralDist" HeaderText="Tipo de Sucural" />
                            <telerik:GridBoundColumn DataField="Region" UniqueName="Region" HeaderText="Region" />
                            <telerik:GridBoundColumn DataField="Zona" UniqueName="Zona" HeaderText="Zona" />
                            <telerik:GridBoundColumn DataField="NO_EA_RC" UniqueName="NO_EA_RC" HeaderText="EA-RC" />
                            <telerik:GridBoundColumn DataField="NO_EA_AA" UniqueName="NO_EA_AA" HeaderText="EA-AA" />
                            <telerik:GridBoundColumn DataField="NO_EA_IL" UniqueName="NO_EA_IL" HeaderText="EA-IL" />
                            <telerik:GridBoundColumn DataField="NO_EA_ME" UniqueName="NO_EA_ME" HeaderText="EA-ME" />
                            <telerik:GridBoundColumn DataField="NO_EA_SE" UniqueName="NO_EA_SE" HeaderText="EA-SE" />
                            <telerik:GridBoundColumn DataField="NO_EA_ILED" UniqueName="NO_EA_ILED" HeaderText="EA-ILED" />
                            <telerik:GridBoundColumn DataField="NO_EA_BC" UniqueName="NO_EA_BC" HeaderText="EA-BC" />
                            <telerik:GridBoundColumn DataField="NO_EA_II" UniqueName="NO_EA_II" HeaderText="EA-II" />
                            <telerik:GridBoundColumn DataField="NO_EA_CR" UniqueName="NO_EA_CR" HeaderText="EA-CR" />
                            <telerik:GridBoundColumn DataField="NO_EB_RC" UniqueName="NO_EB_RC" HeaderText="EB-RC" />
                            <telerik:GridBoundColumn DataField="NO_EB_AA" UniqueName="NO_EB_AA" HeaderText="EB-AA" />
                            <telerik:GridBoundColumn DataField="NO_EB_ME" UniqueName="NO_EB_ME" HeaderText="EB-ME" />
                            <telerik:GridBoundColumn DataField="NO_EB_CR" UniqueName="NO_EB_CR" HeaderText="EB-CR" />
                        </Columns>
                        <NoRecordsTemplate>
                            <table width="100%" border="0" cellpadding="20" cellspacing="20">
                                <tr>
                                    <td align="center">
                                        <h5 style="color: Black">No hay ninguna solicitud para validar</h5>
                                    </td>
                                </tr>
                            </table>
                        </NoRecordsTemplate>
                    </MasterTableView>

                </telerik:RadGrid>
                <%--<telerik:RadWindow ID="RadWindow1" runat="server" Width="550px" Height="550px" Modal="true" OnClientClose="CerrarRadWinMot">
                            <ContentTemplate>
                                <div style="padding: 10px; text-align: center; align-content: center; width: 100%; height: 100%;">
                                </div>
                            </ContentTemplate>
                        </telerik:RadWindow>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
