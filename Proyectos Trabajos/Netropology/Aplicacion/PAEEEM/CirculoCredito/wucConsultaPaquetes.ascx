<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucConsultaPaquetes.ascx.cs" Inherits="PAEEEM.CirculoCredito.wucConsultaPaquetes" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<fieldset class="fieldset_Netro">
    <table align="center">
        <tr>
            <td align="center">
                <asp:Label ID="titulo" runat="server" Text="Consulta de Paquetes Aceptados"
                    Font-Size="Medium" ForeColor="Silver" Font-Bold="True" Font-Overline="true"></asp:Label>
                <hr class="ruleNet" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <div>
                    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Office2010Silver">
                    </telerik:RadWindowManager>
                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
                    </telerik:RadAjaxLoadingPanel>
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                        <telerik:RadGrid ID="rgConsultaPaquete" runat="server" AutoGenerateColumns="False"
                            GridLines="None" Skin="Office2010Silver" CellSpacing="0"
                            OnNeedDataSource="rgConsultaPaquete_NeedDataSource"
                            OnDataBound="rgConsultaPaquete_DataBound"
                            AllowPaging="True">
                            <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom" />
                            <ClientSettings EnableRowHoverStyle="True">
                                <Selecting CellSelectionMode="None" />
                            </ClientSettings>
                            <MasterTableView ClientDataKeyNames="Nocredit" NoMasterRecordsText="No se encontrarón Registros"
                                AllowAutomaticUpdates="False">

                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>

                                <Columns>
                                    <telerik:GridBoundColumn DataField="status" HeaderText="Estatus" ReadOnly="true" Visible="true" UniqueName="status"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="noCredit" HeaderText="Número de Credito" ReadOnly="true" Visible="true" UniqueName="noCredit"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="folioConsulta" HeaderText="Folio de Consulta" ReadOnly="true" Visible="true" UniqueName="folioConsulta"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fechaConsulta" HeaderText="Fecha de Consulta" ReadOnly="true" Visible="true" UniqueName="fechaConsulta"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="noPakete" HeaderText="Paquete" ReadOnly="true" Visible="true" UniqueName="noPakete"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="folioPakete" HeaderText="Folio Paquete" ReadOnly="true" Visible="true" UniqueName="folioPakete"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fechaRevision" HeaderText="Fecha Revisión" ReadOnly="true" Visible="true" UniqueName="fechaRevision"></telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn FilterControlAltText="Filter colCarta column" ItemStyle-Width="10%"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        UniqueName="colCarta" HeaderText="Carta Autorización">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="verCarta" ImageUrl="~/CirculoCredito/imagenes/Buscar.png" Visible="false" />
                                            <asp:Label ID="lblCarta" runat="server" Text="NA" Font-Size="Small" ForeColor="Silver"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn FilterControlAltText="Filter colActa column" ItemStyle-Width="10%"
                                        UniqueName="colActa" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="Acta Ministerio Público">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="verActa" ImageUrl="~/CirculoCredito/imagenes/Buscar.png" Visible="false" />
                                            <asp:Label ID="lblActa" runat="server" Text="NA" Font-Size="Small" ForeColor="Silver"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadAjaxPanel>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
                <br />
                <br />
                <telerik:RadButton ID="btnSalir" runat="server" Text="Salir"></telerik:RadButton>
            </td>
        </tr>
    </table>

</fieldset>
