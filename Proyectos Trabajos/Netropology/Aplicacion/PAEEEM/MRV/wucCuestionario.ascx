<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCuestionario.ascx.cs" Inherits="PAEEEM.MRV.wucCuestionario" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="width: 100%">
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <br/>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <asp:Label ID="LblCuest1" runat="server" Font-Size="Small" 
                    Text="¿Medición a la que pertenece el Cuestionario?" Visible="False"></asp:Label>
                &nbsp;
                <telerik:RadComboBox ID="RadCmbMedicionesDisponibles"  
                    runat="server" AutoPostBack="True" Width="200px" Visible="False">
                </telerik:RadComboBox>
            </td>            
            <td style="width: 50%">
                <br/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br/>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <asp:Label ID="LblCuestionarioSeguimiento" runat="server" Font-Size="Small" Text="¿Se realizó cuestionario de seguimiento?"></asp:Label>
                &nbsp;
                <telerik:RadComboBox ID="RadCmbCuestionarioSegumiento" Width="80px"  
                    runat="server" AutoPostBack="True" 
                    onselectedindexchanged="RadCmbCuestionarioSegumiento_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>            
            <td style="width: 50%">
                <br/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br/>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <telerik:RadTextBox ID="RadTxtComentarios" runat="server" Height="200px" 
                    TextMode="MultiLine" Width="55%" Visible="False">
                </telerik:RadTextBox>
            </td>
           
            <td style="width: 50%">
                <br/>
            </td>
        </tr>
        
    </table>
    <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="False">
        <table style="width: 100%">
            <tr>
                <td colspan="2">
                    <br/>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <telerik:RadGrid ID="rgEquipos" runat="server" AutoGenerateColumns="False" 
                            GridLines="None" Skin="Office2010Silver" CellSpacing="0" 
                        onneeddatasource="rgEquipos_NeedDataSource" 
                        onitemdatabound="rgEquipos_ItemDataBound">
                        <ClientSettings EnableRowHoverStyle="True">
                            <Selecting CellSelectionMode="None" />
                        </ClientSettings>
                        <MasterTableView ClientDataKeyNames="IdEquipo" NoMasterRecordsText="No se encontrarón Mediciones"
                                AllowAutomaticUpdates="False">
                        
                            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                            <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                            <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                        
                            <Columns>
                                <telerik:GridBoundColumn DataField="IdEquipo" HeaderText="ID Equipo" ReadOnly="true" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NombreEquipo" HeaderText="Equipo de Alta Eficiencia" ReadOnly="true" ItemStyle-Width="70%">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn FilterControlAltText="Filter colOperacion column" 
                                    UniqueName="colOperacion" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">                                           
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkOperacion" runat="server" OnCheckedChanged="ToggleRowSelection" AutoPostBack="true" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                      <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState"
                                        AutoPostBack="True" Text="Marcar si continúa en operación" TextAlign="Left" />
                                    </HeaderTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>

                            <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                            </EditFormSettings>
                        </MasterTableView>  
                    
                    </telerik:RadGrid>
                </td>
                <td style="width: 50%; text-align: left; vertical-align: bottom">
                    <telerik:RadButton ID="RadBtnListo" runat="server" Text="Listo" 
                        onclick="RadBtnListo_Click">
                    </telerik:RadButton>
                </td>   
            </tr>
            <tr>
                <td colspan="2">
                    <br/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br/>
                </td>
            </tr>            
        </table>
        
    </asp:Panel>

    <asp:Panel ID="Panel2" runat="server" Visible="True">
        <table style="width: 100%">
            <tr>                
                <td colspan="2">
                    <asp:Label ID="lblHorariosOperacion" runat="server" Font-Size="Small" 
                        Text="Horarios de operación de los equipos que continúan en operación">               
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <telerik:RadComboBox ID="RadCmbEquipos" runat="server" Width="550px" 
                        AutoPostBack="True" onselectedindexchanged="RadCmbEquipos_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
                <td style="width: 50%">
                    <br/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%; border:solid #a4abb2;border-width:0 0 1px 1px;">
                        <tr style="background:#d6e5f3 0 -2300px repeat-x url('../images/sprite.gif')">
                            <td>
                                <asp:Label ID="LblHorario" runat="server" Font-Size="Small" Text="Horario"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkLunes" runat="server" Font-Size="Small" Text="Lunes" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkMartes" runat="server" Font-Size="Small" Text="Martes" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkMiercoles" runat="server" Font-Size="Small" Text="Miércoles" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkJueves" runat="server" Font-Size="Small" Text="Jueves" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkViernes" runat="server" Font-Size="Small" Text="Viernes" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkSabado" runat="server" Font-Size="Small" Text="Sábado" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkDomingo" runat="server" Font-Size="Small" Text="Domingo" 
                                    TextAlign="Left" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblInicio" runat="server" Font-Size="Small" Text="Inicio"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbLunes" Width="100px" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbMartes" Width="100px" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbMiercoles" Width="100px" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbJueves" Width="100px" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbViernes" Width="100px" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbSabado" Width="100px" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbDomingo" Width="100px" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblHorasLaborales" runat="server" Font-Size="Small" Text="Horas Laborales"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtLunes" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtMartes" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtMiercoles" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtJueves" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtViernes" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtSabado" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtDomingo" onChange="CalculaTotalHorasTrabajadasAltaEquipo();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>                   
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="LblHorasSemana" runat="server" Font-Size="Small" Text="Horas a la semana"></asp:Label>
                                <asp:TextBox ID="TxtHorasSemana" runat="server" Width="60px" Enabled="False" />
                            </td>
                            <td>
                                <br/>
                            </td>
                            <td>
                                <asp:Label ID="LblSemanasAnio" runat="server" Font-Size="Small" Text="Semanas al año"></asp:Label>
                                <telerik:RadNumericTextBox ID="RadTxtSemanasAnio" onChange="CalculaHorasAnioAltaEquipo();" Width="60px" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <br/>
                            </td>
                            <td>
                                <asp:Label ID="LblHorasAnio" runat="server" Font-Size="Small" Text="Horas al año"></asp:Label>
                                <asp:TextBox ID="TxtHorasAnio" runat="server" Width="60px" Enabled="False" />
                            </td>
                            <td>
                                <br/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="text-align: right">
                                <telerik:RadButton ID="RadBtnGuardaHorariosEquipos" runat="server" 
                                    Text="Guardar" onclick="RadBtnGuardaHorariosEquipos_Click">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <br/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br/>
                </td>
            </tr>
            <tr>                
                <td colspan="2">
                    <asp:Label ID="LblEncabezadoNegocio" runat="server" Font-Size="Small" 
                        Text="Horarios de operación del Negocio">               
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%; border:solid #a4abb2;border-width:0 0 1px 1px;">
                        <tr style="background:#d6e5f3 0 -2300px repeat-x url('../images/sprite.gif')">
                            <td>
                                <asp:Label ID="LblHorarioNeg" runat="server" Font-Size="Small" Text="Horario"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkLunesNeg" runat="server" Font-Size="Small" Text="Lunes" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkMartesNeg" runat="server" Font-Size="Small" Text="Martes" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkMiercolesNeg" runat="server" Font-Size="Small" Text="Miércoles" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkJuevesNeg" runat="server" Font-Size="Small" Text="Jueves" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkViernesNeg" runat="server" Font-Size="Small" Text="Viernes" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkSabadoNeg" runat="server" Font-Size="Small" Text="Sábado" 
                                    TextAlign="Left" />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="ChkDomingoNeg" runat="server" Font-Size="Small" Text="Domingo" 
                                    TextAlign="Left" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblInicioNeg" runat="server" Font-Size="Small" Text="Inicio"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbLunesNeg" Width="100px" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbMartesNeg" Width="100px" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbMiercolesNeg" Width="100px" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbJuevesNeg" Width="100px" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbViernesNeg" Width="100px" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbSabadoNeg" Width="100px" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadComboBox ID="RadCmbDomingoNeg" Width="100px" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblHorasLaboralesNeg" runat="server" Font-Size="Small" Text="Horas Laborales"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtLunesNeg" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtMartesNeg" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtMiercolesNeg" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtJuevesNeg" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtViernesNeg" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtSabadoNeg" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="text-align: center">
                                <telerik:RadNumericTextBox ID="RadTxtDomingoNeg" onChange="CalculaTotalHorasTrabajadasNegocio();" runat="server" MinValue="0" 
                                    Width="100px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="LblHorasSemanaNeg" runat="server" Font-Size="Small" Text="Horas a la semana"></asp:Label>
                                <asp:TextBox ID="TxtHorasSemanaNeg" runat="server" Width="60px" Enabled="False" />
                            </td>
                            <td>
                                <br/>
                            </td>
                            <td>
                                <asp:Label ID="LblSemanasAnioNeg" runat="server" Font-Size="Small" Text="Semanas al año"></asp:Label>
                                <telerik:RadNumericTextBox ID="RadTxtSemanasAnioNeg" onChange="CalculaHorasAnioNegocio();" Width="60px" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <br/>
                            </td>
                            <td>
                                <asp:Label ID="LblHorasAnioNeg" runat="server" Font-Size="Small" Text="Horas al año"></asp:Label>
                                <asp:TextBox ID="TxtHorasAnioNeg" runat="server" Width="60px" Enabled="False" />
                            </td>
                            <td>
                                <br/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
        <br/><br/>
        <table>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    &nbsp;</td>
                <td>
                
                </td>
                <td rowspan="3">
                    <table>
                        <tr>
                            <td colspan="2" style="width: 280px">
                                <telerik:RadAsyncUpload runat="server" id="UploadedArchivoMedicionCuest" 
                                    MaxFileSize="10000000"
                                    Style="margin-left: 34px"
                                    Localization-Select="Examinar" Width="80px" 
                                    onfileuploaded="UploadedArchivoMedicionCuest_FileUploaded" 
                                    OnClientFilesUploaded="postackControl" 
                                    onclientfileuploadfailed="OnClientValidationFailedUploadArchivo" 
                                    onclientvalidationfailed="OnClientValidationFailedUploadArchivo">
                                    <FileFilters>
                                        <telerik:FileFilter Description="Images(emf;wmf;jpg;jpeg;jpe;png;bmp;tif)" Extensions="emf,wmf,jpg,jpeg,jpe,png,bmp,tif" />
                                    </FileFilters>
                                    <Localization Select="Examinar" />
                                </telerik:RadAsyncUpload>
                            </td>
                            <td>
                                <asp:Image ID="imgOkCuestionario" runat="server" ImageUrl="~/CentralModule/images/icono_correcto.png" Height="25px" Width="27px" Visible="False" />
                                <asp:ImageButton runat="server" ID="verEquipoViejo" 
                                    ImageUrl="~/CentralModule/images/visualizar.png" 
                                    onclientclick="OnClientClicking;poponload();" />

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        
            <tr>
                <td>
                    <asp:Label ID="LblComentGrales" runat="server" Text="Comentarios Generales" Font-Size="Small" />
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;
                </td>
                <td colspan="2">
                    <telerik:RadTextBox ID="RadTxtCometGeneralesCuest" runat="server" Height="120px" 
                        TextMode="MultiLine" Width="350px">
                    </telerik:RadTextBox>
                </td>            
            </tr>
        </table>
        <br/>
        <table style="width: 100%">
            <tr>
                <td style="width: 33%; text-align: right">
                    <telerik:RadButton ID="RadBtnSalir" runat="server" Text="Salir" 
                        onclick="RadBtnSalir_Click" onclientclicking="OnClientClicking">
                    </telerik:RadButton>
                </td>
                <td style="width: 33%; text-align: center">
                    <telerik:RadButton ID="RadBtnGuardar" runat="server" Text="Guardar" 
                        onclick="RadBtnGuardar_Click" onclientclicking="OnClientClicking">
                    </telerik:RadButton>
                </td>
                <td style="width: 33%; text-align: left">
                    <telerik:RadButton ID="RadBtnFinalizar" runat="server" Text="Finalizar" 
                        onclick="RadBtnFinalizar_Click" onclientclicking="OnClientClicking">
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td style="text-align: center">
                <telerik:RadButton ID="RadBtnSalir2" Visible="True" runat="server" 
                    onclick="RadBtnSalir_Click" Text="Salir" 
                    onclientclicking="OnClientClicking">
                </telerik:RadButton>
            </td>
        </tr>       
    </table>
</div>
<div style="display: none">
    <asp:Button ID="btnRefresh2" runat="server" Text="Button" 
        onclick="btnRefresh2_Click" />
    <asp:Button ID="hidBtnFinalizar" runat="server" Text="Button" 
        onclick="hidBtnFinalizar_Click" />
    <asp:HiddenField ID="HidActualiza2Ok" runat="server" />
    <asp:HiddenField ID="HidstateLoad2" runat="server" />
    <asp:HiddenField ID="HidIdCuestionario" runat="server" />
</div>
<telerik:RadWindowManager ID="rwmVentana" runat="server" Skin="Office2010Silver">                                  
</telerik:RadWindowManager>  

