<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucPaquetePendiente.ascx.cs" Inherits="PAEEEM.CirculoCredito.wucPaquetePendiente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<fieldset class="fieldset_Netro">
    <table align="center">
        <tr>
            <td align="center">
                <asp:Label ID="titulo" runat="server" Text="Generación de Paquetes"
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
                        <telerik:RadGrid ID="rgPaquetePendiente" runat="server" AutoGenerateColumns="False"
                            GridLines="None" Skin="Office2010Silver" CellSpacing="0"
                            OnNeedDataSource="rgPaquetePendiente_NeedDataSource"
                            OnDataBound="rgPaquetePendiente_DataBound"
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

                                    <telerik:GridTemplateColumn FilterControlAltText="Filter colSelect column"
                                        UniqueName="colSelect" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkOperacion" runat="server" OnCheckedChanged="chkOperacion_CheckedChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn DataField="Nocredit" HeaderText="No. Credito" ReadOnly="true" Visible="true" UniqueName="Nocredit"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="folioConsulta" HeaderText="Folio Consulta" ReadOnly="true" Visible="true" UniqueName="folioConsulta"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fechaConsulta" HeaderText="Fecha Consulta" ReadOnly="true" Visible="true" UniqueName="fechaConsulta"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="statusPaquete" HeaderText="Estatus" ReadOnly="true" Visible="true" UniqueName="statusPaquete"></telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn FilterControlAltText="Filter colCarta column" ItemStyle-Width="10%"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        UniqueName="colCarta"  HeaderText="Carta Autorización">
                                        <ItemTemplate>
                                                        <telerik:RadAsyncUpload runat="server" ID="UploadedCarta" 
                                                            MaxFileInputsCount="1"
                                                            Style="margin-left: auto"
                                                            Localization-Select="Examinar" Width="80px"
                                                            OnClientFilesUploaded="postackControl"
                                                            OnFileUploaded="UploadedCarta_FileUploaded" Visible="true">
                                                            <FileFilters>
                                                                <telerik:FileFilter Description="PDFs(pdf)" Extensions="pdf" />
                                                            </FileFilters>
                                                            <Localization Select="Examinar" />
                                                        </telerik:RadAsyncUpload>
                                                        <asp:ImageButton runat="server" ID="verCarta"
                                                            ImageUrl="~/CirculoCredito/imagenes/Buscar.png" Visible="false" />
                                                        <asp:ImageButton runat="server" ID="EliminarCarta"
                                                            ImageUrl="~/CirculoCredito/imagenes/delete.gif" OnClick="EliminarCarta_Click" Visible="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn FilterControlAltText="Filter colActa column" ItemStyle-Width="10%"
                                        UniqueName="colActa" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="Acta Ministerio Público">
                                        <ItemTemplate>
                                                        <telerik:RadAsyncUpload runat="server" ID="UploadedActa"
                                                            MaxFileInputsCount="1"
                                                            Style="margin-left: auto"
                                                            Localization-Select="Examinar" Width="80px"
                                                            OnClientFilesUploaded="postackControl"
                                                            OnFileUploaded="UploadedActa_FileUploaded" Visible="true">
                                                            <FileFilters>
                                                                <telerik:FileFilter Description="Images(pdf)" Extensions="pdf" />
                                                            </FileFilters>
                                                            <Localization Select="Examinar" />
                                                        </telerik:RadAsyncUpload>
                                                        <asp:ImageButton runat="server" ID="verActa"
                                                            ImageUrl="~/CirculoCredito/imagenes/Buscar.png" Visible="false" />
                                                        <asp:ImageButton runat="server" ID="EliminarActa"
                                                            ImageUrl="~/CirculoCredito/imagenes/delete.gif" OnClick="EliminarActa_Click" Visible="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn DataField="comentarios" HeaderText="Comentarios" ReadOnly="true" Visible="true" UniqueName="comentarios"></telerik:GridBoundColumn>

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
                <telerik:RadButton ID="GenerarPaquete" runat="server" Text="Generar Paquete" OnClick="GenerarPaquete_Click"></telerik:RadButton>
            </td>
        </tr>
    </table>

    <div style="display: none">
        <asp:Button ID="btnRefres" runat="server" Text="Button"
            OnClick="btnRefres_Click" />
    </div>
</fieldset>
