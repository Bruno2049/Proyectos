<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CapturaAuxiliar.aspx.cs" UICulture="Auto" Inherits="PAEEEM.SupplierModule.CapturaAuxiliar" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     function OpenWindow() {

         $find('<%= rgColonias.ClientID%>').get_masterTableView();
         $find('<%= rgColonias.ClientID %>').show();
     }
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">
         <div style="width: 100%">
            <asp:Label ID="LabelEncabezado" runat="server" Text="CAPTURA PARA BASE AUXILIAR RPU´S" 
                Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
            <hr class="ruleNet" />
            
            <fieldset class="fieldset_Netro">
                <asp:Panel ID="PanelCaptura" runat="server">               
                <table style="width: 100%">
                    <tr>
                        <td colspan="7">
                            <br/>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 22%">
                            <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="RPU:"></asp:Label>
                            <br/>
                            <telerik:RadTextBox ID="RadTxtRPU" runat="server">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadTxtRPU"
                                ErrorMessage="Se debe capturar el RPU"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RFV1"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 4%">
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 22%">
                            <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="Nombre(s):"></asp:Label>
                            <br/>
                            <telerik:RadTextBox ID="RadTxtNombres" runat="server">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadTxtNombres"
                                ErrorMessage="Se debe capturar el Nombre"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator1"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 4%">
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 22%">
                            <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="Apellido Paterno:"></asp:Label>
                            <br/>
                            <telerik:RadTextBox ID="RadTxtApPaterno" runat="server">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadTxtApPaterno"
                                ErrorMessage="Se debe capturar el Apellido Paterno"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator2"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 4%">
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 22%">
                            <asp:Label ID="Label4" runat="server" Font-Size="Small" Text="Apellido Materno:"></asp:Label>
                            <br/>
                            <telerik:RadTextBox ID="RadTxtApMaterno" runat="server">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <br/>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 22%">
                            <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="Codigo Postal:"></asp:Label>
                            <br/>
                            <telerik:RadTextBox ID="RadTxtCP" runat="server" AutoPostBack="True" 
                                ontextchanged="RadTxtCP_TextChanged" >
                            </telerik:RadTextBox>
                            <asp:ImageButton ID="ImbBtnBuscarCP" runat="server" 
                                ImageUrl="~/SupplierModule/images/buscar.png"  />
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadTxtCP"
                                ErrorMessage="Se debe capturar el Codigo Postal"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator3"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        
                        <td style="width: 4%">
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 22%">
                            <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Estado:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbEstado" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="RadCmbEstado_SelectedIndexChanged">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadCmbEstado"
                                ErrorMessage="Se debe capturar el Estado"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator4"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 4%">
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 22%">
                            <asp:Label ID="Label7" runat="server" Font-Size="Small" Text="Delegación/Municipio:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbMunicipio" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="RadCmbMunicipio_SelectedIndexChanged">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadCmbMunicipio"
                                ErrorMessage="Se debe capturar el Municipio"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator5"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 4%">
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 22%">
                            <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Colonia:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbColonia" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="RadCmbColonia_SelectedIndexChanged">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadCmbColonia"
                                ErrorMessage="Se debe capturar la Colonia"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator6"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                            <telerik:RadComboBox ID="RadCmbColoniaHidden" runat="server" Visible="False" 
                                Width="0px">
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
                            <asp:Label ID="Label9" runat="server" Font-Size="Small" Text="Calle:"></asp:Label>
                            <br/>
                            <telerik:RadTextBox ID="RadTxtCalle" runat="server">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadTxtCalle"
                                ErrorMessage="Se debe capturar la Calle"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator7"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Font-Size="Small" Text="Número:"></asp:Label>
                            <br/>
                            <telerik:RadTextBox ID="RadTxtNumero" runat="server">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadTxtNumero"
                                ErrorMessage="Se debe capturar el Número"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator8"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <br/>
                        </td>
                        <td>
                            <br/>
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
                            <asp:Label ID="Label11" runat="server" Font-Size="Small" Text="Cuenta:"></asp:Label>
                            <br/>
                            <telerik:RadTextBox ID="RadTxtCuenta" runat="server">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadTxtCuenta"
                                ErrorMessage="Se debe capturar la cuenta"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator9"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td> 
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label12" runat="server" Font-Size="Small" Text="Tarifa:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbtarifa" runat="server">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadCmbtarifa"
                                ErrorMessage="Se debe capturar la Tarifa"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator10"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label13" runat="server" Font-Size="Small" Text="Periodo de Facturación:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbPeriodo" runat="server">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadCmbPeriodo"
                                ErrorMessage="Se debe capturar el Periodo"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator11"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>  
                        <td>
                            <asp:Label ID="Label14" runat="server" Font-Size="Small" Text="Total Periodos:"></asp:Label>
                            <br/>
                            <telerik:RadNumericTextBox ID="RadTxtTotalPeriodos" runat="server" 
                                DataType="System.Int32" MinValue="0">
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadTxtTotalPeriodos"
                                ErrorMessage="Se debe capturar el total de periodos"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator12"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>                 
                    </tr>
                    <tr>
                        <td colspan="7">
                            <br/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Font-Size="Small" Text="Inicio:"></asp:Label>
                            <br/>
                            <telerik:RadDatePicker ID="RadDateInicio" runat="server">
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadDateInicio"
                                ErrorMessage="Se debe capturar la Fecha de Inicio"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator13"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td> 
                        <td>
                            <asp:Label ID="Label16" runat="server" Font-Size="Small" Text="Fin:"></asp:Label>
                            <br/>
                            <telerik:RadDatePicker ID="RadDateFin" runat="server">
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadDateFin"
                                ErrorMessage="Se debe capturar la Fecha Final"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator14"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <br/>
                        </td>
                        <td>
                            <br/>
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
                        <td colspan="3" style="text-align: center">
                            <asp:Label ID="Label21" runat="server" Font-Size="Small" Text="REGION Y ZONA CFE"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td colspan="3" style="text-align: center">
                            <asp:Label ID="Label22" runat="server" Font-Size="Small" Text="REGION Y ZONA FIDE"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label17" runat="server" Font-Size="Small" Text="Región Tarifaria:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbRegionCFE" runat="server">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadCmbRegionCFE"
                                ErrorMessage="Se debe capturar la Región CFE"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator15"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label18" runat="server" Font-Size="Small" Text="Zona:"></asp:Label>
                            <br/>
                            <telerik:RadTextBox ID="RadTxtZonaCFE" runat="server" MaxLength="4">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadTxtZonaCFE"
                                ErrorMessage="Se debe capturar la Zona CFE"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator16"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label19" runat="server" Font-Size="Small" Text="Región:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbRegionFide" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="RadCmbRegionFide_SelectedIndexChanged">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadCmbRegionFide"
                                ErrorMessage="Se debe capturar la Región Fide"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator17"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label20" runat="server" Font-Size="Small" Text="Zona:"></asp:Label>
                            <br/>
                            <telerik:RadComboBox ID="RadCmbZonaFide" runat="server">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="RadCmbZonaFide"
                                ErrorMessage="Se debe capturar la Zona Fide"
                                ValidationGroup="Captura"
                                Display="Dynamic"
                                Text="*"
                                EnableClientScript="true"
                                ID="RequiredFieldValidator18"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="text-align: center">
                            <telerik:RadButton ID="RadBtnGuardar" runat="server" Text="Guardar" 
                                onclick="RadBtnGuardar_Click">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <br/>
                        </td>
                    </tr>
                </table>                
                </asp:Panel>
            </fieldset>
            <p style="text-align: left">
                    &nbsp;<asp:ValidationSummary  ID="Basica" runat="server" CssClass="failureNotification"
                        ValidationGroup="Captura" Font-Size="Small" HeaderText="Resumén Captura:" />
                </p>
            <br/>
            
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center" colspan="3">
                        <asp:Label ID="Label23" runat="server" Font-Size="Small" Text="HISTORIAL DE ENERGÍA"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                        <br/>
                    </td>
                    <td style="width: 80%">
                        <telerik:RadGrid ID="rgHistoricoConsumo" runat="server" AllowPaging="True" 
                            CellSpacing="0" Culture="es-MX" GridLines="None"  Width="100%" 
                             Skin="Office2010Silver" oninsertcommand="rgHistoricoConsumo_InsertCommand" 
                             onneeddatasource="rgHistoricoConsumo_NeedDataSource" 
                             ondeletecommand="rgHistoricoConsumo_DeleteCommand" 
                             onitemdatabound="rgHistoricoConsumo_ItemDataBound" 
                             onupdatecommand="rgHistoricoConsumo_UpdateCommand" PageSize="12">
                
                            <ClientSettings>
				                <Selecting CellSelectionMode="None"></Selecting>
				            </ClientSettings>
                
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="IdHistorial" CommandItemDisplay="Top" NoMasterRecordsText="No hay equipos de Medición">
					            <CommandItemSettings ExportToPdfText="Export to PDF" AddNewRecordText="Agregar Registro Historico"></CommandItemSettings>
                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
					            </RowIndicatorColumn>

					            <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
					            </ExpandCollapseColumn>
                    
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar"  
                                        FilterControlAltText="Filter EditCommandColumn column" InsertText="Insertar" UpdateText="Actualizar" CancelText="Cancelar">
                                    </telerik:GridEditCommandColumn>
                        
                                    <telerik:GridButtonColumn ConfirmText="¿Desea eliminar este registro?" ConfirmDialogType="RadWindow"
							            ConfirmTitle="Historico" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
							            ConfirmDialogWidth="280px" Text="Eliminar">
							            <HeaderStyle Width="40" />
						            </telerik:GridButtonColumn>
                        
                                    <telerik:GridTemplateColumn DataField="IdHistorial" FilterControlWidth="" AutoPostBackOnFilter="true"
											            FilterControlAltText="Filter IdHistorial column" HeaderText="ID" 
											            UniqueName="IdHistorial" ShowFilterIcon="False">
                                        <ItemTemplate>
                                            <asp:Label ID="LblIDHistorial" runat="server" Text='<%# Eval("IdHistorial") %>'></asp:Label>
                                        </ItemTemplate>

                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="FECHA_PERIODO" FilterControlWidth="" AutoPostBackOnFilter="true"
											            FilterControlAltText="Filter FECHA_PERIODO column" HeaderText="Fecha Periodo" 
											            UniqueName="FECHA_PERIODO" ShowFilterIcon="False">
                        
                                        <EditItemTemplate>
                                            <telerik:RadDatePicker ID="RadDateFechaPeriodo" runat="server">
                                            </telerik:RadDatePicker>
                                        </EditItemTemplate>
                            
                                        <ItemTemplate>
                                            <asp:Label ID="LblfechaPeriodo" runat="server" Text='<%# Eval("FECHA_PERIODO","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>

                                    </telerik:GridTemplateColumn>
                        
                                    <telerik:GridTemplateColumn DataField="CONSUMO" FilterControlWidth="" AutoPostBackOnFilter="true"
											            FilterControlAltText="Filter CONSUMO column" HeaderText="Consumo" 
											            UniqueName="CONSUMO" ShowFilterIcon="False">
                            
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="RadTxtConsumo" runat="server" 
                                                DataType="System.Decimal" MinValue="0">
                                                <NumberFormat AllowRounding="False" DecimalDigits="4" ZeroPattern="n" />
                                            </telerik:RadNumericTextBox>
                                        </EditItemTemplate>
                            
                                        <ItemTemplate>
                                            <asp:Label ID="LblConsumo" runat="server" Text='<%# Eval("CONSUMO") %>'></asp:Label>
                                        </ItemTemplate>

                                    </telerik:GridTemplateColumn>
                        
                                    <telerik:GridTemplateColumn DataField="DEMANDA" FilterControlWidth="" AutoPostBackOnFilter="true"
											            FilterControlAltText="Filter DEMANDA column" HeaderText="Demanda" 
											            UniqueName="DEMANDA" ShowFilterIcon="False">
                            
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="RadTxtDEMANDA" runat="server" 
                                                DataType="System.Int32" MinValue="0">
                                            </telerik:RadNumericTextBox>
                                        </EditItemTemplate>
                            
                                        <ItemTemplate>
                                            <asp:Label ID="LblDemanda" runat="server" Text='<%# Eval("DEMANDA") %>'></asp:Label>
                                        </ItemTemplate>

                                    </telerik:GridTemplateColumn>
                        
                                    <telerik:GridTemplateColumn DataField="FACTORPOTENCIA" FilterControlWidth="" AutoPostBackOnFilter="true"
											            FilterControlAltText="Filter FACTORPOTENCIA column" HeaderText="Factor de Potencia" 
											            UniqueName="FACTORPOTENCIA" ShowFilterIcon="False">
                            
                                        <EditItemTemplate>
                                            <telerik:RadNumericTextBox ID="RadTxtFACTORPOTENCIA" runat="server" 
                                                MaxValue="2" MinValue="0">
                                                <NumberFormat AllowRounding="False" DecimalDigits="4" ZeroPattern="n" />
                                            </telerik:RadNumericTextBox>
                                        </EditItemTemplate>
                            
                                        <ItemTemplate>
                                            <asp:Label ID="LblFactorPotencia" runat="server" Text='<%# Eval("FACTORPOTENCIA") %>'></asp:Label>
                                        </ItemTemplate>

                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" 
                                        UniqueName="EditCommandColumn1">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                        </telerik:RadGrid>
                    </td>
                    <td style="width: 10%">
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <telerik:RadButton ID="RadBtnValida" runat="server" Text="Validar" 
                            onclick="RadBtnValida_Click">
                        </telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br/>                        
                    </td>
                </tr>
            </table>            
         </div>
         
                <telerik:RadWindow ID="rwGrid" runat="server" Modal="True" 
                    Skin="Office2010Silver" Title="Asentamientos Encontrados" Width="600px">
                    <ContentTemplate>
					    <telerik:RadGrid ID="rgColonias" runat="server" AutoGenerateColumns="False" 
                                GridLines="None" Skin="Office2010Silver" CellSpacing="0" 
                                OnNeedDataSource="rgColonias_NeedDataSource"
                                OnItemCommand="rgColonias_ItemCommand" >
                                <ClientSettings EnableRowHoverStyle="True">
                                    <Selecting CellSelectionMode="None" />
                                </ClientSettings>

                            <MasterTableView ClientDataKeyNames="CveCp" NoMasterRecordsText="No se encontrarón registros"
                                AllowAutomaticUpdates="False">
                                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="CveCp" HeaderText="Clave" ReadOnly="true" Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CodigoPostal" HeaderText="C.P." ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DxColonia" HeaderText="Asentamiento" ReadOnly="true">
                                        </telerik:GridBoundColumn>
									    <telerik:GridBoundColumn DataField="DxTipoColonia" HeaderText="Tipo Asentamiento" ReadOnly="true">
                                        </telerik:GridBoundColumn>
									    <telerik:GridBoundColumn DataField="DxDelegacionMunicipio" HeaderText="Delegación/Municipio" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CveDelegMunicipio" ReadOnly="true" Visible="False">
                                        </telerik:GridBoundColumn>
									    <telerik:GridBoundColumn DataField="DxEstado" HeaderText="Estado" ReadOnly="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CveEstado" ReadOnly="true" Visible="False">
                                        </telerik:GridBoundColumn>
									    <telerik:GridButtonColumn CommandName="Seleccionar" Text="Seleccionar" UniqueName="colEdit" HeaderText="" 
                                            ButtonType="LinkButton" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        </telerik:GridButtonColumn>
                                    </Columns>

                                <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                                </EditFormSettings>
                            </MasterTableView>

                            <FilterMenu EnableImageSprites="False"></FilterMenu>
                        </telerik:RadGrid>
                    </ContentTemplate>
                </telerik:RadWindow>

                  
     </telerik:RadAjaxPanel>
     <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" 
        Skin="Office2010Silver">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="rwmVentana" runat="server" Skin="Office2010Silver">                                  
    </telerik:RadWindowManager>

    <%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="RadTxtCP">
			<UpdatedControls>
				<telerik:AjaxUpdatedControl ControlID="rgColonias" LoadingPanelID="RadAjaxLoadingPanel1"/>
			</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>--%>

</asp:Content>
