<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wuAltaBajaEquipos.ascx.cs" Inherits="WebProyectoEquipos.wuAltaBajaEquipos" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

    <style type="text/css">              
        #datosCliente {
            width: 100%;
            height: auto;
        }

        #tecnologias {
            width: 100%;
            height: auto;
            margin-top: 10px;
        }
      

        fieldset {
            border: 1px solid lightblue;
            padding: 6px;
           border-radius: 5px;
           -moz-border-radius: 5px;
            -o-border-radius: 5px;
           -webkit-border-radius: 5px;
           -ms-border-radius: 5px;                      
        }
        
        legend {
            color: #4682b4;
            font-size: 14px;
        }
        
        
        #Resumen {
           border: 1px solid lightblue;
           padding: 4px;
           border-radius: 5px;
           -moz-border-radius: 5px;
            -o-border-radius: 5px;
           -webkit-border-radius: 5px;
           -ms-border-radius: 5px;  
        }
           
        #contenedor {
            display: table;
        }   
        
        
        #contenidos {
            display: table-row;            
        }
                        
        
        #columna1, columna2, columna3 {
            display: table-cell;            
        } 
        
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }              
           
    </style>



    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">
            <div>
                <fieldset id="Fieldset1" runat="server">
                    <legend>Parseo de Trama</legend>
                    <table style="width: 80%">
                        <tr>
                            <td>                               
                                <telerik:RadTextBox ID="RadTextBoxTRama" runat="server" TextMode="MultiLine" 
                                    Height="100px" style="top: 0px; left: 0px; height: 57px; width: 180px" 
                                    Width="800px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="horiz-align: center">                                
                                <telerik:RadButton ID="RadButton2" runat="server" Text="Parseo" 
                                    onclick="RadButton2_Click">
                                </telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br/>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>

            <div id="datosCliente">
            <table>
                <tr>
                    <td class="columnaUno">
                        <asp:Label runat="server" Text="Razón Social:" Width="140px" ID="lblDX_NOMBRE_COMERCIAL"></asp:Label>
                    </td>
                    <td>                        
                        <telerik:RadTextBox ID="txtDX_NOMBRE_COMERCIAL" runat="server" Enabled="False" 
                            Width="200px" Skin="Office2010Silver">
                        </telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" Text="Giro de la Empresa:" Width="155px" ID="lblDX_TIPO_INDUSTRIA"></asp:Label>
                    </td>
                    <td>                       
                        <telerik:RadTextBox ID="txtDX_TIPO_INDUSTRIA" runat="server" Enabled="False" 
                            Width="200px" Skin="Office2010Silver">
                        </telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" Text="RPU:" Width="50px" ID="lblServiceCode"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtServiceCode" Runat="server" Enabled="False" 
                            Width="150px" Skin="Office2010Silver"> </telerik:RadTextBox>                      
                    </td>
                </tr>
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td class="columnaUno">
                        <asp:Label runat="server" Text="Domicilio:" Width="140px" ID="lblDX_DOMICILIO_FISC_CP"></asp:Label>
                    </td>
                    <td>                      
                        <telerik:RadTextBox ID="txtDX_DOMICILIO_FISC" runat="server" Enabled="False" 
                            Width="200px" Skin="Office2010Silver">
                        </telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" Text="Codigo Postal:" Width="155px" ID="lblCodigoPostal"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtDX_CP_FISC" runat="server" Enabled="False" 
                            Width="200px" Skin="Office2010Silver">
                        </telerik:RadTextBox>                        
                    </td>
                    <td></td>
                    <td></td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td >
                        <asp:Label runat="server" Text="Tecnologia:" ID="Label2" Width="140px"></asp:Label>
                    </td>
                    <td>                       
                        <telerik:RadComboBox ID="cboTecnologias" runat="server"
                            OnSelectedIndexChanged="cboTecnologias_SelectedIndexChanged" Width="200px" 
                            Skin="Office2010Silver" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblSistema" runat="server" Text="Tarifa Destino:" Width="155px"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cboTarifaDestino" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="cboTecnologias_SelectedIndexChanged" Width="200px" 
                            Skin="Office2010Silver" Visible="False">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Selected="True" Text="Seleccione..." />
                                <telerik:RadComboBoxItem runat="server" Text="OM" Value="3" />
                                <telerik:RadComboBoxItem runat="server" Text="HM" Value="4" />
                            </Items>
                        </telerik:RadComboBox>                        
                    </td>
                    <td></td>
                    <td>                     
                        <telerik:RadButton ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" 
                            Text="Agregar" Skin="Office2010Silver" style="top: 0px; left: 0px" 
                            Width="100px">
                        </telerik:RadButton>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="8"></td>
                </tr>
            </table>
        </div>
        <div>
            <fieldset id="fielSetEB" runat="server">
                <legend>Equipos de Baja Eficiencia</legend>
                <table>
                    <tr>
                        <td>                            
                            <telerik:RadButton ID="btnAgrupar" runat="server" Text="Agrupar" 
                                OnClick="btnAgrupar_Click" Skin="Office2010Silver" Width="100px" >
                            </telerik:RadButton>
                            <telerik:RadButton ID="btnDesagrupar" runat="server" Text="Desagrupar" 
                                OnClick="btnDesagrupar_Click" Skin="Office2010Silver" Width="100px">
                            </telerik:RadButton>
                        </td>
                    </tr>
                 </table>  
                 <div>            
                   <telerik:RadGrid ID="rgEquiposBaja" runat="server" 
                        AutoGenerateColumns="False" CellSpacing="0"                             
                        OnUpdateCommand="rgEquiposBaja_UpdateCommand"
                        OnItemCommand="rgEquiposBaja_ItemCommand"
                        GridLines="None" Skin="Office2010Silver" 
                        OnNeedDataSource="rgEquiposBaja_NeedDataSource" 
                        OnDeleteCommand="rgEquiposBaja_DeleteCommand" 
                        OnItemDataBound="rgEquiposBaja_ItemDataBound" Culture="es-MX" 
                        OnSelectedIndexChanged="rgEquiposBaja_SelectedIndexChanged" >
                                <ClientSettings EnableRowHoverStyle="true">
                                   <Selecting CellSelectionMode="None" />
                                </ClientSettings>
                                <MasterTableView DataKeyNames="ID" EditMode="InPlace" >
                                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="false">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="false">
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>                                        
                                        <telerik:GridButtonColumn CommandName="Seleccionar" FilterControlAltText="Filter column column" Text="Seleccionar" UniqueName="colSeleccionar">
                                        </telerik:GridButtonColumn>
                                         <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="colEditar" FilterControlAltText="Filter EditCommandColumn column">
                                            <HeaderStyle Width="40" />
                                        </telerik:GridEditCommandColumn>                                     
                                        <telerik:GridButtonColumn ConfirmText="¿Desea eliminar el equipo de baja" Text="Eliminar"
                                            ConfirmDialogType="RadWindow" ConfirmTitle="Equipos de baja eficiencia" ButtonType="ImageButton"
                                            CommandName="Delete" ConfirmDialogHeight="100px" ConfirmDialogWidth="280px">
                                            <HeaderStyle Width="40" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="ID" FilterControlAltText="Filter colID column" HeaderText="ID" ReadOnly="True" UniqueName="ID" Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ID_BAJA" FilterControlAltText="Filter colID column" HeaderText="ID BAJA" ReadOnly="True" UniqueName="ID_BAJA" Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn FilterControlAltText="Filter colAgrupar column" HeaderText="Agrupar" UniqueName="colAgrupar">                                           
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupar" runat="server" OnCheckedChanged="chkAgrupar_CheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Dx_Grupo" FilterControlAltText="Filter colGrupo column" HeaderText="Grupo" UniqueName="Dx_Grupo" ReadOnly="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn  DataField="Cve_Tecnologia" FilterControlAltText="Filter colCve_Tecnologia column" HeaderText="Cve_Tecnologia" UniqueName="Cve_Tecnologia" Visible="False" ReadOnly="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Dx_Tecnologia" FilterControlAltText="Filter colTecnologia column" HeaderText="Tecnologia" UniqueName="Dx_Tecnologia" ReadOnly="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="Ft_Tipo_Producto" FilterControlAltText="Filter colFt_Tipo_Producto column" HeaderText="Tipo de Producto" UniqueName="Ft_Tipo_Producto">
                                            <EditItemTemplate>
                                                <telerik:RadComboBox ID="cboFt_Tipo_Producto" runat="server" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="cboFt_Tipo_Producto_SelectedIndexChanged1">                                                    
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="rfvTipoProducto" runat="server" ClientIDMode="Static"
                                                    ControlToValidate="cboFt_Tipo_Producto" ErrorMessage="*" InitialValue="Seleccione"
                                                    ForeColor="red" ValidationGroup="equipoBaja">                                                    
                                                </asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Dx_Tipo_Producto")%>                                                
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Cve_Consumo" FilterControlAltText="Filter colCve_Capacidad column" HeaderText="Capacidad / Sistema" UniqueName="Cve_Consumo">
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtCve_Capacidad" runat="server" Width="80px" 
                                                 Visible="false" MinValue="1" MaxValue="10000" NumberFormat-DecimalDigits="2" MaxLength="5" >
                                                </telerik:RadNumericTextBox>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static"
                                                    ControlToValidate="txtCve_Capacidad" ErrorMessage="*" InitialValue=""
                                                    ForeColor="red" ValidationGroup="equipoBaja">                                                   
                                                </asp:RequiredFieldValidator>--%>                                                                                                                                           
                                                <telerik:RadComboBox ID="cboCve_Capacidad" runat="server" DataTextField="Elemento"
                                                    DataValueField="IdElemento" Visible="false">                                                   
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="rfvCboCapacidad" runat="server" ClientIDMode="Static"
                                                    ControlToValidate="cboCve_Capacidad" ErrorMessage="*" InitialValue="Seleccione"
                                                    ForeColor="red" ValidationGroup="equipoBaja">                                                    
                                                </asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Dx_Consumo")%>                                              
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Dx_Unidad" FilterControlAltText="Filter colUnidad column" HeaderText="Unidad" 
                                                            UniqueName="Dx_Unidad" ReadOnly="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="Cantidad" FilterControlAltText="Filter colCantidad column" HeaderText="Cantidad" UniqueName="Cantidad">
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Width="80px" Text='<%# Bind("Cantidad") %>' 
                                                MaxLength="3" MaxValue="999" MinValue="1">                                                
                                                </telerik:RadNumericTextBox>
                                                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ClientIDMode="Static" 
                                                    ControlToValidate="txtCantidad" ErrorMessage="*" InitialValue="0"
                                                    ForeColor="red" ValidationGroup="equipoBaja">
                                                </asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Cantidad")%>                                               
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                   <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Dx_Tecnologia" FieldName="Dx_Tecnologia" FormatString="" HeaderText="Tecnología" />
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldAlias="Dx_Tecnologia" FieldName="Dx_Tecnologia" FormatString="" HeaderText="Tecnología" />
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Dx_Grupo" FieldName="Dx_Grupo" 
                                                    HeaderText="Grupo" />
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldAlias="Dx_Grupo" FieldName="Dx_Grupo" 
                                                    HeaderText="Grupo" />
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Dx_Grupo" FieldName="Dx_Grupo" FormatString="" HeaderText="Grupo" />
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldAlias="Dx_Grupo" FieldName="Dx_Grupo" FormatString="" HeaderText="Grupo" />
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <EditFormSettings>
                                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                        </EditColumn>
                                    </EditFormSettings>
                                </MasterTableView>                               
                        <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                        <ValidationSettings EnableValidation="true" ValidationGroup="equipoBaja"></ValidationSettings>
                        <SelectedItemStyle CssClass="SelectedItem"/>
                        <FilterMenu EnableImageSprites="False">
                        </FilterMenu>
                    </telerik:RadGrid>                                                          
                 </div>  
            </fieldset>
            <br/><br/>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <telerik:RadComboBox ID="CboGrupos" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="CboGrupos_SelectedIndexChanged" Visible="False">
                    </telerik:RadComboBox>
                    </td>                   
                </tr>
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
            </table>
            <fieldset id="fSEA" runat="server">
                <legend id="legEquiposAlta" runat="server">Equipos de Alta Eficiencia</legend>                                                                
                <div>
                   <telerik:RadGrid ID="rgEquiposAlta" runat="server" 
                        AutoGenerateColumns="False" CellSpacing="0" GridLines="None"                                                       
                        OnUpdateCommand="rgEquiposAlta_UpdateCommand" Skin="Office2010Silver" 
                        OnItemDataBound="rgEquiposAlta_ItemDataBound" 
                        OnNeedDataSource="rgEquiposAlta_NeedDataSource" 
                        OnDeleteCommand="rgEquiposAlta_DeleteCommand" 
                        OnInsertCommand="rgEquiposAlta_InsertCommand" Culture="es-MX" 
                        OnItemCommand="rgEquiposAlta_ItemCommand">
                                <ClientSettings EnableRowHoverStyle="true">
                                   <Selecting CellSelectionMode="None" />
                                </ClientSettings>
                                <MasterTableView DataKeyNames="ID" EditMode="InPlace" NoMasterRecordsText="No hay equipos de alta eficiencia"
                                    CommandItemDisplay="Top" AllowAutomaticUpdates="False">
                                    <CommandItemSettings ExportToPdfText="Export to PDF" 
                                        AddNewRecordText="Agregar equipo de alta eficiencia" />
                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                    </ExpandCollapseColumn>
                                    <Columns>                                        
                                         <telerik:GridEditCommandColumn ButtonType="ImageButton" FilterControlAltText="Filter EditCommandColumn column">
                                            <HeaderStyle Width="40" />
                                        </telerik:GridEditCommandColumn>
                                         <telerik:GridButtonColumn ConfirmText="¿Desea eliminar el equipo de alta"
                                            ConfirmDialogType="RadWindow" ConfirmTitle="Equipos de alta eficiencia" ButtonType="ImageButton"
                                            CommandName="Delete" ConfirmDialogHeight="100px" ConfirmDialogWidth="280px" Text="Eliminar">
                                            <HeaderStyle Width="40" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridTemplateColumn DataField="ID_Baja" FilterControlAltText="Filter colID_Baja column" HeaderText="ID_Baja" UniqueName="ID_Baja" Visible="False">
                                            <EditItemTemplate>
                                                <asp:Label ID="lbIdBaja" runat="server" Text='<%# Eval("ID_Baja") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "ID_Baja")%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="ID" FilterControlAltText="Filter colID column" HeaderText="ID" ReadOnly="True" UniqueName="ID" Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="Cve_Marca" FilterControlAltText="Filter colGrupo column" HeaderText="Marca" UniqueName="Cve_Marca">
                                            <EditItemTemplate>
                                                <telerik:RadComboBox ID="cboCve_Marca" AutoPostBack="True"  runat="server" DataTextField="Elemento" DataValueField="IdElemento" Skin="Office2010Silver" OnSelectedIndexChanged="cboCve_Marca_SelectedIndexChanged" Height="300px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccione" Value="-1"  />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="rfvCboCveMarca" runat="server" ErrorMessage="*" ValidationGroup="equipoAlta" ControlToValidate="cboCve_Marca" ></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Dx_Marca")%>                                                
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Cve_Modelo" FilterControlAltText="Filter colCve_Modelo column" HeaderText="Modelo" UniqueName="Cve_Modelo">
                                            <EditItemTemplate>
                                                <telerik:RadComboBox ID="cboCve_Modelo" runat="server" DataTextField="Elemento" DataValueField="IdElemento" 
                                                AutoPostBack="true" Height="300px" OnSelectedIndexChanged="cboCve_Modelo_SelectedIndexChanged" Skin="Office2010Silver">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccione" Value="-1"  />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="rfvcboCveModelo" runat="server" ErrorMessage="*" ValidationGroup="equipoAlta" ControlToValidate="cboCve_Modelo"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Dx_Modelo")%>                                                
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="No_Capacidad" FilterControlAltText="Filter colPrecio_Distribuidor column" HeaderText="Capacidad" UniqueName="No_Capacidad">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblNo_Capacidad" runat="server" Width="100px" ReadOnly="true" Text='<%# Bind("No_Capacidad") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo_Capacidadr" runat="server" Text='<%# Eval("No_Capacidad") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Cantidad" FilterControlAltText="Filter colCantidad column" HeaderText="Cantidad" UniqueName="Cantidad">
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtCantidad" Width="80px" runat="server" Text='<%# Bind("Cantidad") %>' AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged" MinValue="1" MaxValue="999" MaxLength="3">
                                                </telerik:RadNumericTextBox>  
                                                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ErrorMessage="*" ValidationGroup="equipoAlta" ControlToValidate="txtCantidad"></asp:RequiredFieldValidator>                                         
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Cantidad")%>                                                
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Precio_Distribuidor" FilterControlAltText="Filter colPrecio_Distribuidor column" HeaderText="Precio Distribuidor " UniqueName="Precio_Distribuidor">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblPrecio_DistribuidorEdicion" runat="server" Width="100px" ReadOnly="true" Text='<%# Bind("Precio_Distribuidor") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrecio_Distribuidor" runat="server" Text='<%# Eval("Precio_Distribuidor", "{0:C2}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Precio_Unitario" FilterControlAltText="Filter colPrecio_Unitario column" HeaderText="Precio Unitario" UniqueName="Precio_Unitario">
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtPrecio_Unitario" runat="server" Width="100px" Text='<%# Bind("Precio_Unitario") %>' AutoPostBack="true" OnTextChanged="txtPrecio_Unitario_TextChanged" MinValue="0"> 
                                                </telerik:RadNumericTextBox>  
                                                <asp:RequiredFieldValidator ID="rfvPrecioUnitario" runat="server" ErrorMessage="*" ValidationGroup="equipoAlta" ControlToValidate="txtPrecio_Unitario"></asp:RequiredFieldValidator>                                                                                         
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Precio_Unitario", "{0:C2}")%>                                                
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Importe_Total_Sin_IVA" FilterControlAltText="Filter colImporte_Total_Sin_IVA column" HeaderText="Importe Total Sin IVA" UniqueName="Importe_Total_Sin_IVA" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblImporte_Total_Sin_IVAEdicion" ReadOnly="true" Width="100px" runat="server" Text='<%# Bind("Importe_Total_Sin_IVA") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblImporte_Total_Sin_IVA" runat="server" Text='<%# Eval("Importe_Total_Sin_IVA", "{0:C2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Gasto_Instalacion" FilterControlAltText="Filter colGasto_Instalacion column" HeaderText="Gasto Instalación" UniqueName="Gasto_Instalacion" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>                                               
                                                <telerik:RadNumericTextBox ID="txtGasto_Instalacion" runat="server" 
                                                    Width="100px" Text='<%# Bind("Gasto_Instalacion") %>' MinValue="0" 
                                                    AutoPostBack="True" ontextchanged="txtGasto_Instalacion_TextChanged">
                                                </telerik:RadNumericTextBox>   
                                                <asp:RequiredFieldValidator ID="rfvGastoInstalacion" runat="server" ErrorMessage="*" ValidationGroup="equipoAlta" ControlToValidate="txtGasto_Instalacion"></asp:RequiredFieldValidator>                                            
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGasto_Instalacion" runat="server" Text='<%# Eval("Gasto_Instalacion", "{0:C2}") %>'></asp:Label>                                                                                        
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                        </EditColumn>
                                    </EditFormSettings>
                                </MasterTableView>
                                <ValidationSettings EnableValidation="true" ValidationGroup="equipoAlta"></ValidationSettings>
                                <FilterMenu EnableImageSprites="False">
                                </FilterMenu>
                            </telerik:RadGrid>             
                </div>
            </fieldset>
        </div>
        <div style="width:100%">
             <div style="float:right;width:40%;margin:0px">
                 <fieldset>
                     <legend>Presupuesto</legend>
                     <telerik:RadGrid ID="rgPresupuesto" runat="server" CellSpacing="0" 
                         GridLines="None" Skin="Office2010Silver" Culture="es-MX" 
                         AutoGenerateColumns="False" ShowHeader="False" >
                        <ClientSettings EnableRowHoverStyle="True">
                            <Selecting CellSelectionMode="None" />
                        </ClientSettings>
                        <MasterTableView DataKeyNames="IdPresupuesto">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                                Visible="True">
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                                Visible="True">
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>                              
                                <telerik:GridBoundColumn DataField="Nombre" HeaderStyle-Width="50%"
                                    FilterControlAltText="Filter column column" UniqueName="Nombre">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Resultado" DataFormatString="{0:C2}" EmptyDataText="$0"
                                    FilterControlAltText="Filter column1 column" UniqueName="Resultado" ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu EnableImageSprites="False">
                        </FilterMenu>
                    </telerik:RadGrid>
                 </fieldset>                                                    
            </div>            
            <div style="width:30%;margin:0px">
                <telerik:RadButton ID="RadButton1" runat="server" Text="Reliza Calculos" 
                    onclick="RadButton1_Click">
                </telerik:RadButton>
                <br/>
                <telerik:RadButton ID="RadButton3" runat="server" Text="Ver Resumen">
                </telerik:RadButton>
                <br/>
                <telerik:RadButton ID="RadButton5" runat="server" Text="Generar Solicitud" 
                    onclick="RadButton5_Click">
                </telerik:RadButton>
            </div>           
            <div style="clear:both"></div>
        </div>
        
        <div>
            <table style="width: 100%">
                <tr>
                    <td style="background-color: #28a628;">
                        <asp:Label ID="Label1" runat="server" Text="SECCION INFORMATIVA UNICAMENTE PARA PRUEBAS DE USUARIO" ForeColor="White" Font-Bold="True"></asp:Label>
                    </td>                    
                </tr>
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        Tipo Tarifa:
                    </td>
                    <td>
                        <asp:Label ID="LblTarifa" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Periodo Facturación:
                    </td>
                    <td>
                        <asp:Label ID="LblPeridoFacturacion" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Consumo Promedio: &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="LblConsumoPromedio" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Demanda Máxima: 
                    </td>
                    <td>
                        <asp:Label ID="LblDemandaMaxima" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Factor de Potencia: 
                    </td>
                    <td>
                        <asp:Label ID="LblFactorPotencia" runat="server" Text=""></asp:Label>
                    </td>
                </tr>  
                <tr>
                    <td>
                        Valor Amortización: 
                    </td>
                    <td>
                        <asp:Label ID="LblAmortizacion" runat="server" Text=""></asp:Label>
                    </td>
                </tr> 
                <tr>
                    <td>
                        Total Ahorro Kw: 
                    </td>
                    <td>
                        <asp:Label ID="lblAhorroKw" runat="server" Text=""></asp:Label>
                    </td>
                </tr>  
                <tr>
                    <td>
                        Total Ahorro KwH: 
                    </td>
                    <td>
                        <asp:Label ID="LblAhorroKwH" runat="server" Text=""></asp:Label>
                    </td>
                </tr>                       
            </table>
        </div>

        <div>
            <table style="width: 100%">
                <tr>
                    <td>
                        <fieldset>
                        <legend>Facturación Sin Ahorro</legend>
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server">
                                        <Columns>
                                            <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                                            <asp:BoundField DataField="CPromedioODemMax" HeaderText="Consumo" />
                                            <asp:BoundField DataField="CargoAdicional" HeaderText="Costo Mensual" />
                                            <asp:BoundField DataField="Facturacion" HeaderText="Facturación" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SubTotal
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadSubTotal" runat="server">
                                    </telerik:RadNumericTextBox>                       
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Iva
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadIva" runat="server">
                                    </telerik:RadNumericTextBox>                       
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadTotal" runat="server">
                                    </telerik:RadNumericTextBox>                       
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pago Facturación (Bimestral o Mensual)
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadFacturacion" runat="server">
                                    </telerik:RadNumericTextBox>                       
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Monto Máximo a Facturar (Regla 20%)
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadMontoMaximo" runat="server">
                                    </telerik:RadNumericTextBox>                       
                                </td>
                            </tr>
                        </table>
                        </fieldset>
                    </td>
                    
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>

                    <td>
                        <fieldset>
                        <legend>Facturación Con Ahorro</legend>
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridView2" AutoGenerateColumns="False" runat="server">
                                        <Columns>
                                            <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                                            <asp:BoundField DataField="CPromedioODemMax" HeaderText="Consumo" />
                                            <asp:BoundField DataField="CargoAdicional" HeaderText="Costo Mensual" />
                                            <asp:BoundField DataField="Facturacion" HeaderText="Facturación" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SubTotal
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadSubTotalAhorro" runat="server">
                                    </telerik:RadNumericTextBox>                       
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Iva
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadIvaAhorro" runat="server">
                                    </telerik:RadNumericTextBox>                       
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadTotalAhorro" runat="server">
                                    </telerik:RadNumericTextBox>                       
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pago Facturación (Bimestral o Mensual)
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadFacturacionMensual" runat="server">
                                    </telerik:RadNumericTextBox>                       
                                </td>
                            </tr>
                        </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="GridView3" AutoGenerateColumns="True" runat="server">
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    Validación 20%
                                </td>
                                <td>
                                    <asp:Label ID="LblValidacion20" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    PSR
                                </td>
                                <td>
                                    <asp:Label ID="lblValidacionPsr" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Capacidad de Pago
                                </td>
                                <td>
                                    <asp:Label ID="LblValidacionCapacidad" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Nueva Facturación Negativa&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="LblValidaFacturacionNegativa" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Consumo Negativo
                                </td>
                                <td>
                                    <asp:Label ID="LblValidacionNegativa" runat="server" ></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        
        <div>
            <table>
                <tr>
                <td>
                    <fieldset>
                        <legend>Periodo Simple de Recuperación</legend>
                        <table>
                            <tr>
                                <td>
                                    Monto Financiamiento
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadCapMonto" runat="server">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Ahorro Económico
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadCapAhorro" runat="server">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Periodo
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadCapPeriodo" runat="server">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    P.S.R
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadCapPSR" runat="server">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <fieldset>
                        <legend>Capacidad de Pago</legend>
                        <table>
                            <tr>
                                <td>
                                    Ventas
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadVentas" runat="server">
                                    </telerik:RadNumericTextBox>
                                 </td>
                            </tr>
                            <tr>
                                <td>
                                    Gastos
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadGastos" runat="server">
                                    </telerik:RadNumericTextBox>
                                 </td>
                            </tr>
                            <tr>
                                <td>
                                    Ahorro
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadAhorro" runat="server">
                                    </telerik:RadNumericTextBox>
                                 </td>
                            </tr>
                            <tr>
                                <td>
                                    Flujo
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadFlujo" runat="server">
                                    </telerik:RadNumericTextBox>
                                 </td>
                            </tr>
                            <tr>
                                <td>
                                    Capacidad
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadCapacidad" runat="server">
                                    </telerik:RadNumericTextBox>
                                 </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
                </tr>
            </table>
        </div>
        
        <div>
            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="PanelResumen" TargetControlID="RadButton3"
                CancelControlID="RadButton4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelResumen" runat="server">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <br/>
                <telerik:RadButton ID="RadButton4" runat="server" Text="Cerrar">
                </telerik:RadButton>
            </asp:Panel>
        </div>       
    </telerik:RadAjaxPanel>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" 
        Skin="Office2010Silver">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="rwmVentana" runat="server" Skin="Office2010Silver">                                  
    </telerik:RadWindowManager>        

