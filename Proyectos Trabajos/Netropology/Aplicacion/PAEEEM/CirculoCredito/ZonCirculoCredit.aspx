<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZonCirculoCredit.aspx.cs" Inherits="PAEEEM.CirculoCredito.ZonCirculoCredit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            var pre = "ctl00_MainContent_";

            function poponload(nocre, tipo, consulta) {
                var testwindow = window.open('VisorImagenes.aspx?Id=' + nocre + '&Tipo=' + tipo + '&Referencia=' + consulta + '', '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
                testwindow.moveTo(0, 0);
            }

            function ValidarComentarios(textareaControl, maxlength) {
                var obser = document.getElementById('<%=TexComen.ClientID%>');
                if (obser.value.length > maxlength) {
                    textareaControl.value = textareaControl.value.substring(0, maxlength);
                }
                if (obser.value.length > 0) {
                    document.getElementById('<%=btnFinalizar.ClientID%>').disabled = false;
                }
            }

            function Validarfiltros() {
                var cre = document.getElementById('<%= RadTxtNoCredit.ClientID %>');
                if (cre.value.length > 0) {

                    document.getElementById('<%= btnBuscar.ClientID %>').disabled = false;
                    }
                    else {
                        document.getElementById('<%= btnBuscar.ClientID %>').disabled = true;
                    }

                }

        </script>
    </telerik:RadScriptBlock>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel2" runat="server">
        <asp:Label ID="LabelEncabezado" runat="server" Text="MÓDULO CIRCULO DE CRÉDITO"
            Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
        <hr class="ruleNet" />
        <div style="width: 100%">
            <fieldset class="fieldset_Netro">

                <table align="center">
                    <tr style="width: 350px;">
                        <td align="center">
                            <asp:Label ID="LblEstatus" runat="server" Text="Estatus:" Font-Size="Small"></asp:Label>
                            <telerik:RadComboBox ID="RadCbxEstatus" runat="server" Width="150px" OnSelectedIndexChanged="RadCbxEstatus_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                        </td>
                        <td style="align-items: stretch">
                            <asp:Label ID="NoCredit" runat="server" Text="No. Credito:" Font-Size="Small" Visible="false"></asp:Label>
                            <telerik:RadTextBox ID="RadTxtNoCredit" runat="server" EmptyMessage="No. Credito">
                            </telerik:RadTextBox>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblFolio" runat="server" Text="Folio Paquete:" Font-Size="Small" Visible="false"></asp:Label>
                            <telerik:RadComboBox ID="RadCbxfolio" runat="server" Width="150px" OnSelectedIndexChanged="RadCbxfolio_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                        </td>
                        <td align="center">
                            <asp:Label ID="LblNoPaquete" runat="server" Text="No. Paquete:" Font-Size="Small" Visible="false"></asp:Label>
                            <telerik:RadComboBox ID="RadCbxPaquete" runat="server" Width="150px" OnSelectedIndexChanged="RadCbxPaquete_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                        </td>
                        <td align="center">
                            <asp:Label ID="lbldist" runat="server" Text="Distribuidor:" Font-Size="Small" Visible="false"></asp:Label>
                            <telerik:RadComboBox ID="RadCbxDist" runat="server" Width="150px" OnSelectedIndexChanged="RadCbxDist_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Visible="false" Enabled="false" />

                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>

        <div style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td align="center">
                        <div id="RevisiPaquete" runat="server" visible="false">
                            <fieldset class="fieldset_Netro">
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="titulo" runat="server" Text="Revisión de Paquetes"
                                                Font-Size="Medium" ForeColor="Silver" Font-Bold="True" Font-Overline="true"></asp:Label>
                                            <hr class="ruleNet" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div>
                                                <telerik:RadGrid ID="rgPaqueteRevision" runat="server" AutoGenerateColumns="False"
                                                    GridLines="None" Skin="Office2010Silver" CellSpacing="0"
                                                    OnNeedDataSource="rgPaqueteRevision_NeedDataSource"
                                                    OnDataBound="rgPaqueteRevision_DataBound"
                                                    AllowPaging="True">
                                                    <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom" />
                                                    <ClientSettings EnableRowHoverStyle="True">
                                                        <Selecting CellSelectionMode="None" />
                                                    </ClientSettings>
                                                    <MasterTableView ClientDataKeyNames="noCredit" NoMasterRecordsText="No se encontrarón Registros"
                                                        AllowAutomaticUpdates="False">

                                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>

                                                        <Columns>

                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter colSelect column"
                                                                UniqueName="colSelect" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="Seleccionar">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkOperacion" runat="server" OnCheckedChanged="chkOperacion_CheckedChanged" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="status" HeaderText="Estatus" ReadOnly="true" Visible="true" UniqueName="status"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="distribuidor" HeaderText="Distribuidor" ReadOnly="true" Visible="true" UniqueName="distribuidor"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="noCredit" HeaderText="No. Credito" ReadOnly="true" Visible="true" UniqueName="noCredit"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="folioConsulta" HeaderText="Folio Consulta" ReadOnly="true" Visible="true" UniqueName="folioConsulta"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fechaConsulta" HeaderText="Fecha Consulta" ReadOnly="true" Visible="true" UniqueName="fechaConsulta"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="noPakete" HeaderText="Paquete" ReadOnly="true" Visible="true" UniqueName="noPakete"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="folioPakete" HeaderText="Folio Paquete" ReadOnly="true" Visible="true" UniqueName="folioPakete"></telerik:GridBoundColumn>
                                                            <%-- <telerik:GridBoundColumn DataField="fechaRevision" HeaderText="Fecha Revisión" ReadOnly="true" Visible="true" UniqueName="fechaRevision"></telerik:GridBoundColumn>--%>

                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter colFechaRevi" ItemStyle-Width="10%"
                                                                ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                                                UniqueName="colFechaRevicion" HeaderText="Fecha Revisión">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConFecha" runat="server" Text='<%# Convert.ToDateTime(Eval("fechaRevision")).ToShortDateString()%>' />
                                                                    <asp:Label ID="lblSinFecha" runat="server" Text='Pendiente' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter colCarta column" ItemStyle-Width="10%"
                                                                ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                                                UniqueName="colCarta" HeaderText="Carta Autorización">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="verCarta" ImageUrl="~/CirculoCredito/imagenes/Buscar.png" />
                                                                    <asp:Label ID="lblCarta" runat="server" Text="NA" Font-Size="Small" ForeColor="Silver"></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>

                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter colActa column" ItemStyle-Width="10%"
                                                                UniqueName="colActa" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="Acta Ministerio Público">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="verActa" ImageUrl="~/CirculoCredito/imagenes/Buscar.png" />
                                                                    <asp:Label ID="lblActa" runat="server" Text="NA" Font-Size="Small" ForeColor="Silver"></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>

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
                                            <telerik:RadButton ID="btnAceptar" runat="server" Text="Aceptar Paquete" OnClick="btnAceptar_Click" Enabled="false"></telerik:RadButton>
                                            <telerik:RadButton ID="btnRechazar" runat="server" Text="Rechazar Paquete" Enabled="false"></telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <br />
                                            <br />
                                            <telerik:RadButton ID="RadButton2" runat="server" Text="Salir" OnClick="btnSalir_Click"></telerik:RadButton>

                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="AceptadosPaquete" runat="server" visible="false">
                            <fieldset class="fieldset_Netro">
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label1" runat="server" Text="Consulta de Paquetes Aceptados"
                                                Font-Size="Medium" ForeColor="Silver" Font-Bold="True" Font-Overline="true"></asp:Label>
                                            <hr class="ruleNet" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div>
                                                <telerik:RadGrid ID="rgPaqueteAceptado" runat="server" AutoGenerateColumns="False"
                                                    GridLines="None" Skin="Office2010Silver" CellSpacing="0"
                                                    OnNeedDataSource="rgPaqueteAceptado_NeedDataSource"
                                                    OnDataBound="rgPaqueteAceptado_DataBound"
                                                    AllowPaging="True">
                                                    <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom" />
                                                    <ClientSettings EnableRowHoverStyle="True">
                                                        <Selecting CellSelectionMode="None" />
                                                    </ClientSettings>
                                                    <MasterTableView ClientDataKeyNames="noCredit" NoMasterRecordsText="No se encontraron coincidencias con los criterios ingresados"
                                                        AllowAutomaticUpdates="False">

                                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>

                                                        <Columns>

                                                            <telerik:GridBoundColumn DataField="status" HeaderText="Estatus" ReadOnly="true" Visible="true" UniqueName="status"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="distribuidor" HeaderText="Distribuidor" ReadOnly="true" Visible="true" UniqueName="distribuidor"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="noCredit" HeaderText="No. Credito" ReadOnly="true" Visible="true" UniqueName="noCredit"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="folioConsulta" HeaderText="Folio Consulta" ReadOnly="true" Visible="true" UniqueName="folioConsulta"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fechaConsulta" HeaderText="Fecha Consulta" ReadOnly="true" Visible="true" UniqueName="fechaConsulta"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="noPakete" HeaderText="Paquete" ReadOnly="true" Visible="true" UniqueName="noPakete"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="folioPakete" HeaderText="Folio Paquete" ReadOnly="true" Visible="true" UniqueName="folioPakete"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fechaRevision" HeaderText="Fecha Revisión" ReadOnly="true" Visible="true" UniqueName="fechaRevision"></telerik:GridBoundColumn>

                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter colCarta column" ItemStyle-Width="10%"
                                                                ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                                                UniqueName="colCarta" HeaderText="Carta Autorización">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="verCarta" ImageUrl="~/CirculoCredito/imagenes/Buscar.png" />
                                                                    <asp:Label ID="lblCarta" runat="server" Text="NA" Font-Size="Small" ForeColor="Silver"></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>

                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter colActa column" ItemStyle-Width="10%"
                                                                UniqueName="colActa" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="Acta Ministerio Público">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="verActa" ImageUrl="~/CirculoCredito/imagenes/Buscar.png" />
                                                                    <asp:Label ID="lblActa" runat="server" Text="NA" Font-Size="Small" ForeColor="Silver"></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>

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
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="PendientePaquete" runat="server" visible="false">
                            <fieldset class="fieldset_Netro">
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label2" runat="server" Text="Consulta de Paquetes Pendientes"
                                                Font-Size="Medium" ForeColor="Silver" Font-Bold="True" Font-Overline="true"></asp:Label>
                                            <hr class="ruleNet" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div>
                                                <telerik:RadGrid ID="rgPaquetePendiente" runat="server" AutoGenerateColumns="False"
                                                    GridLines="None" Skin="Office2010Silver" CellSpacing="0"
                                                    OnNeedDataSource="rgPaquetePendiente_NeedDataSource"
                                                    OnDataBound="rgPaquetePendiente_DataBound"
                                                    AllowPaging="True">
                                                    <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom" />
                                                    <ClientSettings EnableRowHoverStyle="True">
                                                        <Selecting CellSelectionMode="None" />
                                                    </ClientSettings>
                                                    <MasterTableView ClientDataKeyNames="noCredit" NoMasterRecordsText="No se encontraron coincidencias con los criterios ingresados"
                                                        AllowAutomaticUpdates="False">

                                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>

                                                        <Columns>

                                                            <telerik:GridBoundColumn DataField="status" HeaderText="Estatus" ReadOnly="true" Visible="true" UniqueName="status" HeaderStyle-Width="150"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="distribuidor" HeaderText="Distribuidor" ReadOnly="true" Visible="true" UniqueName="distribuidor" HeaderStyle-Width="150"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="noCredit" HeaderText="No. Credito" ReadOnly="true" Visible="true" UniqueName="noCredit" HeaderStyle-Width="150"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="folioConsulta" HeaderText="Folio Consulta" ReadOnly="true" Visible="true" UniqueName="folioConsulta" HeaderStyle-Width="150"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fechaConsulta" HeaderText="Fecha Consulta" ReadOnly="true" Visible="true" UniqueName="fechaConsulta" HeaderStyle-Width="150"></telerik:GridBoundColumn>

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
                                            <telerik:RadButton ID="RadButton1" runat="server" Text="Salir" OnClick="btnSalir_Click"></telerik:RadButton>

                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="ConsultPaquetes" runat="server" visible="false">
                            <fieldset class="fieldset_Netro">
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label3" runat="server" Text="Consulta de Paquetes"
                                                Font-Size="Medium" ForeColor="Silver" Font-Bold="True" Font-Overline="true"></asp:Label>
                                            <hr class="ruleNet" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div>
                                                <telerik:RadGrid ID="rgConsultaPaquete" runat="server" AutoGenerateColumns="False"
                                                    GridLines="None" Skin="Office2010Silver" CellSpacing="0"
                                                    OnNeedDataSource="rgConsultaPaquete_NeedDataSource"
                                                    OnDataBound="rgConsultaPaquete_DataBound"
                                                    AllowPaging="True">
                                                    <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom" />
                                                    <ClientSettings EnableRowHoverStyle="True">
                                                        <Selecting CellSelectionMode="None" />
                                                    </ClientSettings>
                                                    <MasterTableView ClientDataKeyNames="noCredit" NoMasterRecordsText="No se encontraron coincidencias con los criterios ingresados"
                                                        AllowAutomaticUpdates="False">

                                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>

                                                        <Columns>

                                                            <telerik:GridBoundColumn DataField="status" HeaderText="Estatus" ReadOnly="true" Visible="true" UniqueName="status"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="distribuidor" HeaderText="Distribuidor" ReadOnly="true" Visible="true" UniqueName="distribuidor"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="noCredit" HeaderText="No. Credito" ReadOnly="true" Visible="true" UniqueName="noCredit"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="folioConsulta" HeaderText="Folio Consulta" ReadOnly="true" Visible="true" UniqueName="folioConsulta"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fechaConsulta" HeaderText="Fecha Consulta" ReadOnly="true" Visible="true" UniqueName="fechaConsulta"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="noPakete" HeaderText="Paquete" ReadOnly="true" Visible="true" UniqueName="noPakete"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="folioPakete" HeaderText="Folio Paquete" ReadOnly="true" Visible="true" UniqueName="folioPakete"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fechaRevision" HeaderText="Fecha Revisión" ReadOnly="true" Visible="true" UniqueName="fechaRevision"></telerik:GridBoundColumn>

                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter colCarta column" ItemStyle-Width="10%"
                                                                ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                                                UniqueName="colCarta" HeaderText="Carta Autorización">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="verCarta" ImageUrl="~/CirculoCredito/imagenes/Buscar.png" />
                                                                    <asp:Label ID="lblCarta" runat="server" Text="NA" Font-Size="Small" ForeColor="Silver"></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>

                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter colActa column" ItemStyle-Width="10%"
                                                                UniqueName="colActa" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="Acta Ministerio Público">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="verActa" ImageUrl="~/CirculoCredito/imagenes/Buscar.png" />
                                                                    <asp:Label ID="lblActa" runat="server" Text="NA" Font-Size="Small" ForeColor="Silver"></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>

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
                                            <telerik:RadButton ID="RadButton3" runat="server" Text="Salir" OnClick="btnSalir_Click"></telerik:RadButton>

                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <telerik:RadWindow ID="modalPopup" runat="server" Width="600px" Height="300px" Modal="true">
            <ContentTemplate>
                <div style="padding: 10px; text-align: center;">
                    <table>
                        <tr>
                            <td>
                                <p style="text-align: center;">
                                    Por favor ingrese la causa del rechazo y seleccione Finalizar
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Label ID="comentarios" runat="server" Text="COMENTARIOS:" />
                                <br />
                                <asp:TextBox ID="TexComen" runat="server" Height="80px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" Enabled="false" OnClick="btnFinalizar_Click" />
                                <asp:Button ID="bntCancelar" runat="server" Text="Cancelar" OnClick="bntCancelar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>



    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Office2010Silver">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Office2010Silver">
    </telerik:RadWindowManager>
</asp:Content>
