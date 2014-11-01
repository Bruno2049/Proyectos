<%@ Register TagPrefix="flashmovie" Namespace="Osmosis.Web.UI.Controls" Assembly="FlashMovie" %>
<%@ Control Language="c#" AutoEventWireup="false" CodeFile="WC_Menu.ascx.cs" Inherits="eClock.WC_Menu"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<p>
</p>
<p>
</p>
<table border="0" cellpadding="0" cellspacing="0" class="FooterTable">
    <tr>

   <td style="background-image: url(skins/Menu_r1_c3.gif); text-align: left;"><img name="Menu_r1_c1" src="skins/Menu_r1_c1.gif" width="21" height="42" border="0" id="Menu_r1_c1" alt="" /></td>
           
        <td id="Td3" runat="server" class="FooterPane" style="background-image: url(skins/Menu_r1_c3.gif);
            vertical-align: middle; background-repeat: repeat-x; text-align: left" valign="top"
            visible="true" align="left">
            <ignav:UltraWebMenu ID="mnu_Main" runat="server" BorderWidth="0px"
                DisabledClass="" EnableViewState="False" FileUrl=""
                Font-Bold="False" Font-Names="Tahoma,Arial,Helvetica" Font-Size="16px" 
                ForeColor="SteelBlue" ItemPaddingSubMenus="0"
                ItemPaddingTop="1" ItemSpacingTop="0" TopSelectedClass="" Width="895px" Height="35px" 
                OnInit="mnu_Main_Init" SeparatorClass="" TopAligment="Center" 
                Font-Overline="False" MergeStyles="False">
                <Styles>
                    <ignav:Style BorderColor="#316AC5" BorderStyle="Outset" BorderWidth="1px" CssClass="TopHover4"
                        Cursor="Default" Font-Size="8pt" ForeColor="Black">
                        <BorderDetails WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                    </ignav:Style>
                    <ignav:Style BackgroundImage="None" BorderColor="#F0F0F0" BorderStyle="Solid" BorderWidth="1px"
                        CssClass="TopClass4" Cursor="Default" Font-Names="Arial" Font-Size="8pt" ForeColor="Black">
                        <BorderDetails WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                    </ignav:Style>
                    <ignav:Style CssClass="Style1">
                    </ignav:Style>
                </Styles>
                <Images>
                    <ScrollTopDisabledImage Url="ig_menu_scrollup_disabled.gif" />
                    <ScrollBottomImage Url="ig_menu_scrolldown.gif" />
                    <XPSpacerImage Width="0px" />
                    <ScrollBottomDisabledImage Url="ig_menu_scrolldown_disabled.gif" />
                    <ScrollTopImage Url="ig_menu_scrollup.gif" />
                    <SubMenuImage Url="ig_menutri.gif" />
                </Images>
                <IslandStyle BackColor="White" BorderColor="LightGray" BorderStyle="Outset" BorderWidth="1px"
                    Cursor="Default" ForeColor="#C1D2EE" Font-Bold="False" Font-Overline="False" Font-Size="9pt">
                </IslandStyle>
                <HoverItemStyle BackColor="#2F5788" Cursor="Default" ForeColor="White">
                    <Margin Bottom="5px" Top="5px" />
                    <BorderDetails StyleRight="None" />
                </HoverItemStyle>
                <Levels>
                    <ignav:Level CheckboxColumnName="" ColumnName="" ImageColumnName="" Index="0" LevelClass=""
                        LevelHiliteClass="" LevelHoverClass="" LevelImage="" LevelIslandClass="" LevelKeyField=""
                        RelationName="" TargetFrameName="" TargetUrlName="" />
                    <ignav:Level CheckboxColumnName="" ColumnName="" ImageColumnName="" Index="1" LevelClass=""
                        LevelHiliteClass="" LevelHoverClass="" LevelImage="" LevelIslandClass="" LevelKeyField=""
                        RelationName="" TargetFrameName="" TargetUrlName="" />
                    <ignav:Level CheckboxColumnName="" ColumnName="" ImageColumnName="" Index="2" LevelClass=""
                        LevelHiliteClass="" LevelHoverClass="" LevelImage="" LevelIslandClass="" LevelKeyField=""
                        RelationName="" TargetFrameName="" TargetUrlName="" />
                    <ignav:Level CheckboxColumnName="" ColumnName="" ImageColumnName="" Index="3" LevelClass=""
                        LevelHiliteClass="" LevelHoverClass="" LevelImage="" LevelIslandClass="" LevelKeyField=""
                        RelationName="" TargetFrameName="" TargetUrlName="" />
                </Levels>
                <DisabledStyle ForeColor="LightGray">
                </DisabledStyle>
                <ItemStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                    Font-Bold="False" Font-Italic="False" ForeColor="SteelBlue" >
                    <BorderDetails ColorRight="DarkGray" StyleRight="Solid" WidthLeft="1px" WidthRight="1px" StyleLeft="None" StyleTop="None" />
                    <Margin Bottom="5px" Top="5px" />
                </ItemStyle>
                <ExpandEffects Opacity="90" ShadowColor="DarkGray" Type="Slide" />
                <Items>
                    <ignav:Item TargetUrl="WF_EmpleadosN.aspx" Text="Empleados">
                        <Items>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_EmpleadosN.aspx" Text="Mostrar todos">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" Text="Nuevo" TargetUrl="WF_EmpleadosEd.aspx?Parametros=Asistencia">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_Importacion.aspx" Text="Pegar desde Excel">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_PersonasSHuella.aspx" Text="Solo Tarjeta o N&#250;mero">
                            </ignav:Item>
                        </Items>
                    </ignav:Item>
                    <ignav:Item Text="Turnos">
                        <Items>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_Turnos.aspx" Text="Mostrar todos">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" Text="Nuevo" TargetUrl="WF_TurnosEdicion.aspx?Parametros=Asistencia">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_CreacionTurnosExpress.aspx" Text="Captura R&#225;pida">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_TurnosAsignacionExpress.aspx" Text="Asignaci&#243;n Rapida">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_PersonasSemana.aspx?Parametros=Turnos"
                                Text="Asignaci&#243;n por d&#237;a">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_TurnosEmpleadosE.aspx" Text="Asignaci&#243;n Avanzada">
                            </ignav:Item>
                        </Items>
                    </ignav:Item>
                    <ignav:Item Text="Asistencia">
                        <Items>
                            <ignav:Item Text="Consultar Checadas" TargetFrame="top" TargetUrl="WF_Accesos.aspx">
                            </ignav:Item>
                            <ignav:Item Text="Asistencia semanal" TargetFrame="top">
                            </ignav:Item>
                            <ignav:Item Text="Consultar Asistencia" TargetFrame="top">
                            </ignav:Item>
                        </Items>
                    </ignav:Item>
                    <ignav:Item Text="Herramientas">
                        <Items>
                            <ignav:Item Text="Interface Noi">
                                <Items>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_EdicionExportacionNOI.aspx" Text="Configurar">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_ExportarIncidenciasNOI.aspx" Text="Exportar">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item Text="Interface Nomipaq">
                                <Items>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_ExportarIncidenciasNomipaq.aspx" Text="Exportar">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_EjecucionSQL.aspx" Text="Ejecutar sentencias SQL">
                            </ignav:Item>
                        </Items>
                    </ignav:Item>
                    <ignav:Item Text="Seguridad">
                        <Items>
                            <ignav:Item Text="Usuarios">
                                <Items>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_Usuarios.aspx" Text="Listado completo">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_UsuarioEmpleado.aspx" Text="Nuevo a partir de un Empleado">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_AsignarGrupoUsuarios.aspx?Grupo=1" Text="Asignar GRUPO1 a Usuarios">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_AsignarGrupoUsuarios.aspx?Grupo=2" Text="Asignar GRUPO2 a Usuarios">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_EnviarReporteUsuarios.aspx" Text="Enviar Reporte">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_Perfiles.aspx" Text="Perfiles">
                            </ignav:Item>
                        </Items>
                    </ignav:Item>
                    <ignav:Item Text="Parametros">
                        <Items>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_Sitios.aspx" Text="Sitios">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_Dias_Festivos.aspx" Text="D&#237;as festivos">
                            </ignav:Item>
                            <ignav:Item Text="Terminales">
                            </ignav:Item>
                            <ignav:Item Text="Avanzados">
                                <Items>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_Wizarda.aspx" Text="Asistente de configuraci&#243;n">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_Config.aspx" Text="General">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_Wizardb.aspx" Text="Campos">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_Wizarde.aspx" Text="Campos de Im&#225;gen">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_ModulosOpcionales.aspx" Text="M&#243;dulos Opcionales">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_VariablesConfiguracion.aspx" Text="Variables">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                        </Items>
                    </ignav:Item>
                    <ignav:Item CssClass="" TagString="" TargetFrame="" TargetUrl="" Text="Archivo" 
                        ToolTip="" Hidden="True">
                        <Items>
                            <ignav:Item CssClass="" TagString="" TargetFrame="" TargetUrl="" Text="Empleados"
                                 ToolTip="">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_EmpleadosN.aspx"
                                        Text="Listado Completo"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Personas_Diario.aspx?Menu=1"
                                        Text="Consultar Asistencia"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_PersonasSemana.aspx?Parametros=Asistencia"
                                        Text="Asistencia Semanal"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_PersonasAltasBajas.aspx"
                                        Text="Editar Estatus"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_IncidenciasE.aspx"
                                        Text="Justificaciones y/o Incidencias"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_PersonasSHuella.aspx"
                                        Text="Sin Huella"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_HuellasNAsig.aspx"
                                        Text="Huellas No Asignadas"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_AccesosNAsig.aspx"
                                        Text="Accesos No Asignados"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WF_Inasistencias.aspx" Text="Inasistencias" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WF_Importacion.aspx" Text="Captura Express" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_TiempoExtra.aspx" Text="Tiempo Extra">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_ImportaAccesos.aspx" Text="Importar Accesos">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl=""  ToolTip=""
                                Text="Turnos">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Turnos.aspx"
                                        Text="Edici&#243;n"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_TurnosEmpleadosE.aspx"
                                        Text="Asignaci&#243;n"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WF_CreacionTurnosExpress.aspx" Text="Captura Express">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WF_TurnosAsignacionExpress.aspx" Text="Asignaci&#243;n Express"
                                        ToolTip="Solo Menos de 100 Empleados">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_PersonasSemana.aspx?Parametros=Turnos"
                                        Text="Asignaci&#243;n Avanzada">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="" TargetUrl="" Text="Incidencias"
                                 ToolTip="">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Tipo_Incidencias.aspx"
                                        Text="Personalizadas"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Tipo_Inc_Sis.aspx"
                                        Text="Sistema"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Tipo_Inc_Comida_Sis.aspx"
                                        Text="Sistema (comida)"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_Incidencias_Rastreo.aspx" Text="Rastreo">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_EnviaIncidencias.aspx" Text="Envio y Recepci&#243;n">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WF_IncidenciasEX.aspx" Text="Extra" TargetFrame="top">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="" TargetUrl="" Text="Usuario del sistema"
                                 ToolTip="">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Usuarios.aspx"
                                        Text="Usuarios del sistema"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_UsuarioEmpleado.aspx"
                                        Text="Nuevo a partir de un Empleado"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_AsignarGrupoUsuarios.aspx?Grupo=1"
                                        Text="Asignar GRUPO1 a Usuarios"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_AsignarGrupoUsuarios.aspx?Grupo=2" Text="Asignar GRUPO2 a Usuarios">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_EnviarReporteUsuarios.aspx" Text="Enviar Reporte">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Dias_Festivos.aspx"
                                Text="D&#237;as Festivos"  ToolTip="">
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Terminales.aspx"
                                Text="Terminales"  ToolTip="">
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Perfiles.aspx"
                                Text="Perfiles"  ToolTip="">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_DiaPersona.aspx" Text="Vigilancia">
                            </ignav:Item>
                            <ignav:Item Text="Agrupaciones">
                                <Items>
                                    <ignav:Item TargetUrl="WF_Edicion_Contenido_Tablas.aspx?catalogo=1" Text="GRUPO1"
                                        TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WF_Edicion_Contenido_Tablas.aspx?catalogo=2" Text="GRUPO2"
                                        TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetFrame="top" TargetUrl="WF_Edicion_Contenido_Tablas.aspx?catalogo=3"
                                        Text="GRUPO3">
                                    </ignav:Item>
                                    <ignav:Item Text="Asignaci&#243;n">
                                        <Items>
                                            <ignav:Item TargetUrl="WF_Asignacion_Grupo_Persona.aspx?catalogo=1" Text="GRUPO1"
                                                TargetFrame="top">
                                            </ignav:Item>
                                            <ignav:Item TargetUrl="WF_Asignacion_Grupo_Persona.aspx?catalogo=2" Text="GRUPO2"
                                                TargetFrame="top">
                                            </ignav:Item>
                                            <ignav:Item TargetFrame="top" TargetUrl="WF_Asignacion_Grupo_Persona.aspx?catalogo=3"
                                                Text="GRUPO3">
                                            </ignav:Item>
                                        </Items>
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item Text="Monedero">
                                <Items>
                                    <ignav:Item TargetUrl="WF_Productos.aspx" Text="Productos">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WF_CargaDesayunosExpress.aspx" Text="Carga Manual de Monedero">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WF_SaldosContra.aspx" Text="Saldos en Contra">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item Text="Cobro de Comidas">
                                <Items>
                                    <ignav:Item TargetUrl="WF_CargaComidasExpress.aspx" Text="Carga Manual de Comidas">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WF_CostoComidas.aspx" Text="Costo Comidas">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item TargetUrl="WF_Sitios.aspx" Text="Sitios">
                            </ignav:Item>
                        </Items>
                    </ignav:Item>
                    <ignav:Item CssClass="" TagString="" TargetFrame="" TargetUrl="" Text="Reportes"
                         ToolTip="" Hidden="True">
                        <Items>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="" Text="Usuarios"
                                 ToolTip="">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Usuarios.aspx?Reporte=2"
                                        Text="PDF"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Usuarios.aspx?Reporte=1"
                                        Text="Excel"  ToolTip="">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="" Text="Terminales"
                                 ToolTip="">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_Terminales.aspx?Reporte=1"
                                        Text="PDF"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_Terminales.aspx?Reporte=2"
                                        Text="Excel"  ToolTip="">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="" Text="Perfiles"
                                 ToolTip="">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Perfiles.aspx?Reporte=2"
                                        Text="PDF"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Perfiles.aspx?Reporte=1"
                                        Text="Excel"  ToolTip="">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="" Text="D&#237;as Festivos"
                                 ToolTip="">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Dias_Festivos.aspx?Reporte=2"
                                        Text="PDF"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Dias_Festivos.aspx?Reporte=1"
                                        Text="Excel"  ToolTip="">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="" Text="Empleados"
                                 ToolTip="">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_Empleados.aspx?Reporte=1"
                                        Text="PDF"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_Empleados.aspx?Reporte=2"
                                        Text="Excel"  ToolTip="">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="" Text="Incidencias"
                                 ToolTip="">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="" Text="Sistema"
                                         ToolTip="">
                                        <Items>
                                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Tipo_Inc_Sis.aspx?Reporte=2"
                                                Text="PDF"  ToolTip="">
                                            </ignav:Item>
                                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Tipo_Inc_Sis.aspx?Reporte=1"
                                                Text="Excel"  ToolTip="">
                                            </ignav:Item>
                                        </Items>
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="" Text="Comida"
                                         ToolTip="">
                                        <Items>
                                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Tipo_Inc_Comida_Sis.aspx?Reporte=2"
                                                Text="PDF"  ToolTip="">
                                            </ignav:Item>
                                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Tipo_Inc_Comida_Sis.aspx?Reporte=1"
                                                Text="Excel"  ToolTip="">
                                            </ignav:Item>
                                        </Items>
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="" TargetUrl="" Text="Rastreo" 
                                        ToolTip="">
                                        <Items>
                                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Incidencias_Rastreo.aspx?Reporte=2"
                                                Text="PDF"  ToolTip="">
                                            </ignav:Item>
                                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Incidencias_Rastreo.aspx?Reporte=1"
                                                Text="Excel"  ToolTip="">
                                            </ignav:Item>
                                        </Items>
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="" Text="Tipo" 
                                        ToolTip="">
                                        <Items>
                                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Tipo_Incidencias.aspx?Reporte=2"
                                                Text="PDF"  ToolTip="">
                                            </ignav:Item>
                                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Tipo_Incidencias.aspx?Reporte=1"
                                                Text="Excel"  ToolTip="">
                                            </ignav:Item>
                                        </Items>
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="" Text="Turnos"
                                 ToolTip="">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Turnos.aspx?Reporte=2"
                                        Text="Listado en PDF"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Turnos.aspx?Reporte=1"
                                        Text="Listado en Excel"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_PersonasSemana.aspx?Reporte=2"
                                        Text="Siete d&#237;as"  ToolTip="">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="" TargetUrl="" Text="Asistencias"
                                 ToolTip="">
                                <Items>
                                    <ignav:Item Text="Detallado">
                                        <Items>
                                            <ignav:Item TargetUrl="WFR_ComidasTiempoDetallado.aspx?Orientacion=0" Text="Horizontal">
                                            </ignav:Item>
                                            <ignav:Item TargetUrl="WFR_ComidasTiempoDetallado.aspx?Orientacion=1" Text="Vertical">
                                            </ignav:Item>
                                            <ignav:Item TargetUrl="WFR_ComidasTiempoDetallado.aspx?Orientacion=2" Text="Con Tiempo Extra">
                                            </ignav:Item>
                                        </Items>
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_Asistencia.aspx?opc=1"
                                        Text="Por GRUPO1"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_AsistenciasD.aspx?opc=1"
                                        Text="Por D&#237;a"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_EmpleadosStatus.aspx?opc=1"
                                        Text="Mensual"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_EmpleadosStatus_B.aspx?opc=1"
                                        Text="Mensual por GRUPO1"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item Text="Resumido">
                                        <Items>
                                            <ignav:Item TargetFrame="top" TargetUrl="WFR_EmpleadoIncidencias.aspx?Grupo=1" Text="Por GRUPO1">
                                            </ignav:Item>
                                            <ignav:Item TargetFrame="top" TargetUrl="WFR_EmpleadoIncidencias.aspx?Grupo=2" Text="Por GRUPO2">
                                            </ignav:Item>
                                            <ignav:Item TargetFrame="top" TargetUrl="WFR_EmpleadoIncidencias.aspx?Grupo=3" Text="Por GRUPO3">
                                            </ignav:Item>
                                        </Items>
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_AsistenciasAux.aspx?opc=1" Text="Asistencia Express" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_ComidasTiempo.aspx" Text="Comidas" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item Text="General">
                                        <Items>
                                            <ignav:Item TargetUrl="WFR_GraficasGrupo.aspx?Grupo=1" Text="GRUPO1" TargetFrame="top">
                                            </ignav:Item>
                                            <ignav:Item TargetUrl="WFR_GraficasGrupo.aspx?Grupo=2" Text="GRUPO2" TargetFrame="top">
                                            </ignav:Item>
                                            <ignav:Item TargetUrl="WFR_GraficasGrupo.aspx?Grupo=3" Text="GRUPO3" TargetFrame="top">
                                            </ignav:Item>
                                            <ignav:Item TargetUrl="WFR_GraficosPersona.aspx" Text="Por Empleado" TargetFrame="top">
                                            </ignav:Item>
                                            <ignav:Item TargetUrl="WFR_GraficaBarrasGrupo.aspx?Grupo=1" Text="Barras GRUPO1" TargetFrame="top">
                                            </ignav:Item>
                                        </Items>
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item Text="Inasistencias">
                                <Items>
                                    <ignav:Item TargetUrl="WFR_Asistencia.aspx?opc=2" Text="Por GRUPO1" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_AsistenciasD.aspx?opc=2" Text="Por D&#237;a" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_EmpleadosStatus.aspx?opc=2" Text="Mensual" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_EmpleadosStatus_B.aspx?opc=2" Text="Mensual por GRUPO1"
                                        TargetFrame="top">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="" TargetUrl="" Text="Accesos" 
                                ToolTip="">
                                <Items>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_AccesoCC.aspx"
                                        Text="Por GRUPO1"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_AccesoT.aspx"
                                        Text="Por Terminales"  ToolTip="">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_AccesosTerminalDetallado.aspx" Text="Por D&#237;a" TargetFrame="top">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WFR_EmpleadosFaltas.aspx"
                                Text="Empleados Faltas"  ToolTip="">
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_EnviarReporteUsuarios.aspx"
                                Text="Enviar Reportes a Usuarios"  ToolTip="">
                            </ignav:Item>
                            <ignav:Item Text="Cobro de Comidas">
                                <Items>
                                    <ignav:Item TargetUrl="WFR_ComidasTotales.aspx" Text="Comidas Totales">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_ComidasPago.aspx" Text="Comidas por Pago">
                                    </ignav:Item>
                                    <ignav:Item Text="Comidas Por GRUPO1">
                                        <Items>
                                            <ignav:Item TargetUrl="WFR_ComidasPorPeriodo.aspx?Reporte=1" Text="PDF">
                                            </ignav:Item>
                                            <ignav:Item TargetUrl="WFR_ComidasPorPeriodo.aspx?Reporte=2" Text="Excel">
                                            </ignav:Item>
                                        </Items>
                                    </ignav:Item>
                                    <ignav:Item Text="Consumos Totales">
                                        <Items>
                                            <ignav:Item TargetUrl="WFR_ConsumosPorCentroCostos.aspx?Reporte=1" Text="PDF">
                                            </ignav:Item>
                                            <ignav:Item TargetUrl="WFR_ConsumosPorCentroCostos.aspx?Reporte=2" Text="Excel">
                                            </ignav:Item>
                                        </Items>
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_ComidasSimple.aspx" Text="Simple por Empleado">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item Text="Monedero">
                                <Items>
                                    <ignav:Item TargetUrl="WFR_DesayunosDetalle.aspx" Text="Detalles" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_DesayunosPago.aspx" Text="Pagos" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_DesayunosConsumos.aspx" Text="Consumos" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_Productos.aspx" Text="Productos" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item Text="Consumo y Abonos Por GRUPO1">
                                        <Items>
                                            <ignav:Item TargetUrl="WFR_MonederoPorCentroCostos.aspx?Reporte=1" Text="PDF" TargetFrame="top">
                                            </ignav:Item>
                                            <ignav:Item TargetUrl="WFR_MonederoPorCentroCostos.aspx?Reporte=2" Text="Excel" TargetFrame="top">
                                            </ignav:Item>
                                        </Items>
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_DesayunosSimple.aspx" Text="Simple Por Empleado" TargetFrame="top">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                            <ignav:Item Text="Horas Extras">
                                <Items>
                                    <ignav:Item TargetUrl="WFR_HorasExtrasSimple.aspx" Text="Simple" TargetFrame="top">
                                    </ignav:Item>
                                    <ignav:Item TargetUrl="WFR_HorasExtrasDetallado.aspx" Text="Detallado" TargetFrame="top">
                                    </ignav:Item>
                                </Items>
                            </ignav:Item>
                        </Items>
                    </ignav:Item>
                    <ignav:Item CssClass="" TagString="" TargetFrame="" TargetUrl="" Text="Configuraci&#243;n"
                         ToolTip="" Hidden="True">
                        <Items>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_Config.aspx"
                                Text="General"  ToolTip="">
                            </ignav:Item>
                            <ignav:Item CssClass="" TagString="" TargetFrame="top" TargetUrl="WF_VariablesConfiguracion.aspx"
                                Text="Variables"  ToolTip="">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_ModulosOpcionales.aspx" Text="M&#243;dulos Opcionales">
                            </ignav:Item>
                            <ignav:Item TargetFrame="top" TargetUrl="WF_EjecucionSQL.aspx" Text="Ejecutar Sentencias SQL">
                            </ignav:Item>
                            <ignav:Item TargetUrl="WF_EnvioReportes.aspx" Text="Reglas de Envio de Reportes"
                                TargetFrame="top">
                            </ignav:Item>
                            <ignav:Item TargetUrl="WF_Wizardb.aspx" Text="Campos" TargetFrame="top">
                            </ignav:Item>
                            <ignav:Item TargetUrl="WF_Wizarde.aspx" Text="Campos de Imagen" TargetFrame="top">
                            </ignav:Item>
                            <ignav:Item TargetUrl="WF_Wizarda.aspx" Text="Asistente" TargetFrame="top">
                            </ignav:Item>
                            <ignav:Item TargetUrl="WF_EnvioAutomaticoMails.aspx" Text="Envio Autom&#225;tico de Reporte de Asistencia"
                                TargetFrame="top">
                            </ignav:Item>
                            <ignav:Item TargetUrl="WF_EdicionExportacionNOI.aspx" Text="NOI" TargetFrame="top">
                            </ignav:Item>
                            <ignav:Item TargetUrl="WF_ExportarIncidenciasNOI.aspx" Text="Exportaci&#243;n NOI"
                                TargetFrame="top">
                            </ignav:Item>
                            <ignav:Item TargetUrl="WF_ExportarIncidenciasNomipaq.aspx" Text="Exportaci&#243;n Nomipaq"
                                TargetFrame="top">
                            </ignav:Item>
                        </Items>
                    </ignav:Item>
                    <ignav:Item Text="Ayuda">
                        <Items>
                            <ignav:Item TargetUrl="http://www.EntryTec.com.mx/eClock" Text="Acerca de eClock" TargetFrame=" ">
                            </ignav:Item>
                            <ignav:Item TargetUrl="http://www.EntryTec.com.mx/Ayuda/eClock" Text="Ayuda General"
                                TargetFrame=" ">
                            </ignav:Item>
                        </Items>
                    </ignav:Item>
                </Items>
                <HeaderStyle BackColor="Maroon">
                    <Margin Bottom="20px" />
                </HeaderStyle>
                <TopSelectedStyle BackgroundImage="./skins/navbar.on.jpg" ForeColor="White" Height="30px">
                    <Margin Bottom="0px" Top="0px" />
                </TopSelectedStyle>
                <TopLevelHoverItemStyle BackgroundImage="./skins/navbar.on.jpg" Height="30px">
                    <Margin Bottom="0px" Top="0px" />
                </TopLevelHoverItemStyle>
                <TopLevelLeafItemStyle Height="30px" BackColor="Transparent">
                </TopLevelLeafItemStyle>
                <TopLevelParentItemStyle Height="30px" BackColor="Transparent">
                    <Margin Left="0px" Top="0px" />
                </TopLevelParentItemStyle>
                <MenuClientSideEvents InitializeMenu="" ItemChecked="" ItemClick="" ItemHover=""
                    SubMenuDisplay="" />
            </ignav:UltraWebMenu>
        </td>
   <td style="background-image: url(skins/Menu_r1_c3.gif); text-align: right;"><img name="Menu_r1_c5" src="skins/Menu_r1_c5.gif" width="23" height="42" border="0" id="Menu_r1_c5" alt="" /></td>
   <td><img src="skins/spacer.gif" width="1" height="42" border="0" alt="" /></td>
    </tr> 
</table>
