<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucPresupuestpInversion.ascx.cs" Inherits="PAEEEM.SupplierModule.ControlesUsuario.wucPresupuestpInversion" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

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
                        
        
        #columnaUno, columna2, columna3 {
            display: table-cell;            
        }               
           
    </style>
    
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
            </table>
            <table>
                <tr>
                    <td >
                        <asp:Label runat="server" Text="Tecnologia:" ID="Label2" Width="140px"></asp:Label>
                    </td>
                    <td>                       
                        <telerik:RadComboBox ID="cboTecnologias" runat="server"
                            OnSelectedIndexChanged="cboTecnologias_SelectedIndexChanged" Width="200px" 
                            Skin="Office2010Silver">
                        </telerik:RadComboBox>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblSistema" runat="server" Text="Sistema:" Visible="False" Width="155px"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cboSistema" runat="server" AutoPostBack="True" Visible="false"
                            OnSelectedIndexChanged="cboTecnologias_SelectedIndexChanged" Width="200px" 
                            Skin="Office2010Silver">
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
                                onNeeddatasource="rgEquiposBaja_NeedDataSource" 
                                ondeletecommand="rgEquiposBaja_DeleteCommand" 
                                onitemdatabound="rgEquiposBaja_ItemDataBound" Culture="es-MX" >
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
                                                    Skin="Office2010Silver" AutoPostBack="True" 
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
                                                <telerik:RadNumericTextBox ID="txtCve_Capacidad" runat="server" Width="80px" Text='<%# Bind("Dx_Consumo") %>'
                                                 Visible="false" MinValue="0.0" MaxValue="10000">
                                                </telerik:RadNumericTextBox>                                                                                                                                           
                                                <telerik:RadComboBox ID="cboCve_Capacidad" runat="server" DataTextField="Elemento"
                                                    DataValueField="IdElemento" Visible="false" Skin="Office2010Silver">                                                   
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
                                                MaxLength="3" MaxValue="999" MinValue="0">                                                
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
                                <FilterMenu EnableImageSprites="False">
                                </FilterMenu>
                            </telerik:RadGrid>                                                          
                 </div>  
            </fieldset>
            <br/><br/>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        Elija el Grupo para registrar los equipos de Alata Eficiencia:<br/>
                        <telerik:RadComboBox ID="CboGrupos" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="CboGrupos_SelectedIndexChanged">
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
                                onItemdatabound="rgEquiposAlta_ItemDataBound" 
                                onNeeddatasource="rgEquiposAlta_NeedDataSource" 
                                onDeletecommand="rgEquiposAlta_DeleteCommand" 
                                OnInsertCommand="rgEquiposAlta_InsertCommand" Culture="es-MX" 
                        onitemcommand="rgEquiposAlta_ItemCommand">
                                <ClientSettings EnableRowHoverStyle="true">
                                   <Selecting CellSelectionMode="None" />
                                </ClientSettings>
                                <MasterTableView DataKeyNames="ID" EditMode="InPlace" NoMasterRecordsText="No hay equipos de alta eficiencia"
                                    CommandItemDisplay="Top">
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
                                        <telerik:GridTemplateColumn DataField="Cantidad" FilterControlAltText="Filter colCantidad column" HeaderText="Cantidad" UniqueName="Cantidad">
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtCantidad" Width="80px" runat="server" Text='<%# Bind("Cantidad") %>' AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged" MinValue="0">
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
                                                <asp:Label ID="lblPrecio_Distribuidor" runat="server" Text='<%# Eval("Precio_Distribuidor") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Precio_Unitario" FilterControlAltText="Filter colPrecio_Unitario column" HeaderText="Precio Unitario" UniqueName="Precio_Unitario">
                                            <EditItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtPrecio_Unitario" runat="server" Width="100px" Text='<%# Bind("Precio_Unitario") %>' AutoPostBack="true" OnTextChanged="txtPrecio_Unitario_TextChanged" MinValue="0"> 
                                                </telerik:RadNumericTextBox>  
                                                <asp:RequiredFieldValidator ID="rfvPrecioUnitario" runat="server" ErrorMessage="*" ValidationGroup="equipoAlta" ControlToValidate="txtPrecio_Unitario"></asp:RequiredFieldValidator>                                                                                         
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Precio_Unitario")%>                                                
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Importe_Total_Sin_IVA" FilterControlAltText="Filter colImporte_Total_Sin_IVA column" HeaderText="Importe Total Sin IVA" UniqueName="Importe_Total_Sin_IVA">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblImporte_Total_Sin_IVAEdicion" ReadOnly="true" Width="100px" runat="server" Text='<%# Bind("Importe_Total_Sin_IVA") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblImporte_Total_Sin_IVA" runat="server" Text='<%# Eval("Importe_Total_Sin_IVA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="Gasto_Instalacion" FilterControlAltText="Filter colGasto_Instalacion column" HeaderText="Gasto Instalación" UniqueName="Gasto_Instalacion">
                                            <EditItemTemplate>                                               
                                                <telerik:RadNumericTextBox ID="txtGasto_Instalacion" runat="server" Width="100px" Text='<%# Bind("Gasto_Instalacion") %>' MinValue="0">
                                                </telerik:RadNumericTextBox>   
                                                <asp:RequiredFieldValidator ID="rfvGastoInstalacion" runat="server" ErrorMessage="*" ValidationGroup="equipoAlta" ControlToValidate="txtGasto_Instalacion"></asp:RequiredFieldValidator>                                            
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGasto_Instalacion" runat="server" Text='<%# Eval("Gasto_Instalacion") %>'></asp:Label>                                                                                        
                                            </ItemTemplate>
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
                                    FilterControlAltText="Filter column1 column" UniqueName="Resultado">
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
                <telerik:RadButton ID="RadButton1" runat="server" Text="RadButton" 
                    onclick="RadButton1_Click">
                </telerik:RadButton>
            </div>           
            <div style="clear:both"></div>
        </div>
        <telerik:RadWindowManager ID="rwmVentana" runat="server" Skin="Office2010Silver">                                  
        </telerik:RadWindowManager>       
