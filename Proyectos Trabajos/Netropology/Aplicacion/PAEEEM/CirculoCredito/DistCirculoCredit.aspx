<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DistCirculoCredit.aspx.cs" Inherits="PAEEEM.CirculoCredito.DistCirculoCredit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            var pre = "ctl00_MainContent_";

            function postackControl() {

                var oButton = document.getElementById(pre + "btnRefres");
                oButton.click();

            }

            function postackControl2() {

                var oButton2 = document.getElementById(pre + "Button1");
                oButton2.click();

            }


            function poponload(nocre, tipo, consulta) {
                var testwindow = window.open('VisorImagenes.aspx?Id=' + nocre + '&Tipo=' + tipo + '&Referencia=' + consulta + '', '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
                testwindow.moveTo(0, 0);
            }

            function OnClientValidationFailedUploadArchivo(sender, args) {

                alert("Ocurrió un problema al cargar el archivo 'Archivo no Permitido ó sobre paso de limite de peso 3MB. '");
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
    <style type="text/css">
        .auto-style1
        {
            width: 350px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel2" runat="server">
        <asp:Label ID="LabelEncabezado" runat="server" Text="MÓDULO CIRCULO DE CRÉDITO"
            Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
        <hr class="ruleNet" />
        <fieldset class="fieldset_Netro">

            <table align="center">
                <tr style="width: 350px;">
                    <td align="center">
                        <asp:Label ID="LblEstatus" runat="server" Text="Estatus:" Font-Size="Small"></asp:Label>
                        <telerik:RadComboBox ID="RadCbxEstatus" runat="server" Width="150px" OnSelectedIndexChanged="RadCbxEstatus_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                    </td>
                    <td style="align-items: stretch">
                        <asp:Label ID="NoCredit" runat="server" Text="No. Credito:" Font-Size="Small" Visible="false"></asp:Label>
                        <telerik:RadTextBox ID="RadTxtNoCredit" runat="server" EmptyMessage="No. Credito" Visible="false">
                        </telerik:RadTextBox>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblFolio" runat="server" Text="Folio Paquete:" Font-Size="Small" Visible="false"></asp:Label>
                        <telerik:RadComboBox ID="RadCbxFolio" runat="server" Width="150px" OnSelectedIndexChanged="RadCbxFolio_SelectedIndexChanged" AutoPostBack="true" Visible="false"></telerik:RadComboBox>

                    </td>
                    <td align="center">
                        <asp:Label ID="LblNoPaquete" runat="server" Text="No. Paquete:" Font-Size="Small" Visible="false"></asp:Label>
                        <telerik:RadComboBox ID="RadCbxNopack" runat="server" Width="150px" OnSelectedIndexChanged="RadCbxNopack_SelectedIndexChanged" AutoPostBack="true" Visible="false"></telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Enabled="False" Visible="false" />
                    </td>
                </tr>
            </table>
        </fieldset>


        <div style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td align="center">
                        <div id="GenePaquete" runat="server" visible="false">
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
                                                <telerik:RadGrid ID="rgPaquetePendiente" runat="server" AutoGenerateColumns="False"
                                                    GridLines="None" Skin="Office2010Silver" CellSpacing="0" AllowMultiRowSelection="true"
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
                                                                UniqueName="colSelect" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" >
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkOperacion" runat="server" OnCheckedChanged="chkOperacion_CheckedChanged" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox   ID="headerChkbox" runat="server" OnCheckedChanged="headerChkbox_CheckedChanged"
                                                                        AutoPostBack="True" Text="Seleccionar" TextAlign="Right" Checked="true" />
                                                                </HeaderTemplate>
                                                            </telerik:GridTemplateColumn>

                                                            <telerik:GridBoundColumn DataField="Nocredit" HeaderText="No. Credito" ReadOnly="true" Visible="true" UniqueName="Nocredit"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="folioConsulta" HeaderText="Folio Consulta" ReadOnly="true" Visible="true" UniqueName="folioConsulta"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fechaConsulta" HeaderText="Fecha Consulta" ReadOnly="true" Visible="true" UniqueName="fechaConsulta"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="statusPaquete" HeaderText="Estatus" ReadOnly="true" Visible="true" UniqueName="statusPaquete"></telerik:GridBoundColumn>

                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter colCarta column" ItemStyle-Width="10%"
                                                                ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                                                UniqueName="colCarta" HeaderText="Carta Autorización">
                                                                <ItemTemplate>
                                                                    <telerik:RadAsyncUpload runat="server" ID="UploadedCarta"
                                                                        MaxFileInputsCount="1"
                                                                        Style="width: 100%"
                                                                        MaxFileSize="3145728"
                                                                        Localization-Select="Examinar" Width="80px"
                                                                        OnClientFilesUploaded="postackControl"
                                                                        OnClientFileUploadFailed="OnClientValidationFailedUploadArchivo"
                                                                        OnClientValidationFailed="OnClientValidationFailedUploadArchivo"
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
                                                                        Style="width: 100%"
                                                                        MaxFileSize="3145728"
                                                                        Localization-Select="Examinar" Width="80px"
                                                                        OnClientFilesUploaded="postackControl"
                                                                        OnClientFileUploadFailed="OnClientValidationFailedUploadArchivo"
                                                                        OnClientValidationFailed="OnClientValidationFailedUploadArchivo"
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
                                                    <ClientSettings EnableRowHoverStyle="true">
                                                        <Selecting AllowRowSelect="True"></Selecting>
                                                    </ClientSettings>

                                                </telerik:RadGrid>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <br />
                                            <br />
                                            <telerik:RadButton ID="GenerarPaquete" runat="server" Text="Generar Paquete" OnClick="GenerarPaquete_Click" Enabled="false"></telerik:RadButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <telerik:RadButton ID="RadButton1" runat="server" Text="Salir" OnClick="btnSalir_Click"></telerik:RadButton>

                                        </td>
                                    </tr>
                                </table>

                                <div style="display: none">
                                    <asp:Button ID="btnRefres" runat="server" Text="Button"
                                        OnClick="btnRefres_Click" />
                                </div>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div id="ConPaquete" runat="server" visible="false">
                            <fieldset class="fieldset_Netro">
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label1" runat="server" Text="Consulta de Paquetes"
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
                                                    <MasterTableView ClientDataKeyNames="noCredit" NoMasterRecordsText="No se encontrarón Registros"
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
                                                            <%--<telerik:GridBoundColumn DataField="fechaRevision" HeaderText="Fecha Revisión" ReadOnly="true" Visible="true" UniqueName="fechaRevision"></telerik:GridBoundColumn>--%>

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
                                            <telerik:RadButton ID="btnSalir" runat="server" Text="Salir" OnClick="btnSalir_Click"></telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>

                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div id="GeneRechazado" runat="server" visible="false">
                            <fieldset class="fieldset_Netro">
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label2" runat="server" Text="Paquetes Rechazados"
                                                Font-Size="Medium" ForeColor="Silver" Font-Bold="True" Font-Overline="true"></asp:Label>
                                            <hr class="ruleNet" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div>
                                                <telerik:RadGrid ID="rgpaquetesRechazados" runat="server" AutoGenerateColumns="False"
                                                    GridLines="None" Skin="Office2010Silver" CellSpacing="0"
                                                    OnNeedDataSource="rgpaquetesRechazados_NeedDataSource"
                                                    OnDataBound="rgpaquetesRechazados_DataBound"
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
                                                                    <asp:CheckBox ID="chkOperacionRechaza" runat="server" OnCheckedChanged="chkOperacionRechaza_CheckedChanged" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>

                                                            <telerik:GridBoundColumn DataField="Nocredit" HeaderText="No. Credito" ReadOnly="true" Visible="true" UniqueName="Nocredit"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="folioConsulta" HeaderText="Folio Consulta" ReadOnly="true" Visible="true" UniqueName="folioConsulta"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fechaConsulta" HeaderText="Fecha Consulta" ReadOnly="true" Visible="true" UniqueName="fechaConsulta"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="statusPaquete" HeaderText="Estatus" ReadOnly="true" Visible="true" UniqueName="statusPaquete"></telerik:GridBoundColumn>

                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter colCarta column" ItemStyle-Width="10%"
                                                                ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                                                UniqueName="colCarta" HeaderText="Carta Autorización">
                                                                <ItemTemplate>
                                                                    <telerik:RadAsyncUpload runat="server" ID="UploadedCarta"
                                                                        MaxFileInputsCount="1"
                                                                        Style="width: 100%"
                                                                        MaxFileSize="3145728"
                                                                        Localization-Select="Examinar" Width="80px"
                                                                        OnClientFilesUploaded="postackControl2"
                                                                        OnClientFileUploadFailed="OnClientValidationFailedUploadArchivo"
                                                                        OnClientValidationFailed="OnClientValidationFailedUploadArchivo"
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
                                                                        Style="width: 100%"
                                                                        MaxFileSize="3145728"
                                                                        Localization-Select="Examinar" Width="80px"
                                                                        OnClientFilesUploaded="postackControl2"
                                                                        OnClientFileUploadFailed="OnClientValidationFailedUploadArchivo"
                                                                        OnClientValidationFailed="OnClientValidationFailedUploadArchivo"
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
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <br />
                                            <br />
                                            <telerik:RadButton ID="bntGeberarRechazados" runat="server" Text="Generar Paquete" OnClick="bntGeberarRechazados_Click" Enabled="false"></telerik:RadButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <telerik:RadButton ID="RadButton3" runat="server" Text="Salir" OnClick="btnSalir_Click"></telerik:RadButton>

                                        </td>
                                    </tr>
                                </table>

                                <div style="display: none">
                                    <asp:Button ID="Button1" runat="server" Text="Button"
                                        OnClick="btnRefres_Click" />
                                </div>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>
        </div>

    </telerik:RadAjaxPanel>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Office2010Silver">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Office2010Silver">
    </telerik:RadWindowManager>
</asp:Content>
