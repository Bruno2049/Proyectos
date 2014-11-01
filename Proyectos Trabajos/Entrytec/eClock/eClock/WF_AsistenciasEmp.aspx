<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_AsistenciasEmp.aspx.cs"
    Inherits="WF_AsistenciasEmp" %>

<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.DocumentExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid.DocumentExport" TagPrefix="igtbldocexp" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.DisplayControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGauge.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGauge" TagPrefix="igGauge" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGauge.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraGauge.Resources" TagPrefix="igGaugeProp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
<%@ Register Assembly="Infragistics2.WebUI.WebResizingExtender.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI" TagPrefix="igui" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig1" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script language="javascript" type="text/javascript" src="JS_Navegador.js"></script>
    <script id="InfragisticsJS" type="text/javascript">
    var g_menuId;
    var g_itemId;
    var g_itemTag;
    var g_itemTagPadre;
    function Menu_ItemClick(menuId, itemId){
    g_menuId = menuId;
    g_itemId = itemId;
	var item = igmenu_getItemById(itemId);
	if(item.isTopLevelItem())
	{
     
	   // debugger;
	    return ;
	}

	var Tag = item.getTag();
    var TagPadre = null;
    if(item.getParent() != null)
        TagPadre = item.getParent().getTag();
    g_itemTag = Tag;
    g_itemTagPadre = TagPadre;
    var Confirmar = false;
    if (Confirmar && (TagPadre == "TURNOS" || TagPadre == "TURNOSPRED" || TagPadre == "JUSTIFICACIONES"))
    {
        igmenu_getMenuById(menuId).CancelPostBack=true;
        var Texto = "";
        if(TagPadre == "TURNOS")
            Texto = "¿Esta seguro de asignar el horario a los DÍAS y Personas seleccionadas?";
        if(TagPadre == "TURNOSPRED")
            Texto = "¿Esta seguro de asignar el horario predeterminado (" + item.getText() + ") para todos las personas del listado?";
        if(TagPadre == "JUSTIFICACIONES")
            Texto = "¿Esta seguro de justificar las faltas seleccionadas?";
        // debugger;
        
        <%=LblMensage.ClientID%>.innerText = Texto;
        //DlgConfirma_tmpl_LblMensage.innerText = Texto;
        Muestra("DlgConfirma",true);
        return false;
    }
//    debugger;
    if(Tag == "FAVORITO_COMPARTIR")
    {
        igmenu_getMenuById(menuId).CancelPostBack=true;
        Muestra("DlgCompartirFav",true);
        return false;
    }
      if(Tag == "F_AGREGAR")
    {
        igmenu_getMenuById(menuId).CancelPostBack=true;
        Muestra("DlgAgregarFav",true);
        return false;
    }  
}
function BtnCancelar_Click(oButton, oEvent){
    
	$find('<%=DlgConfirma.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
}
function Muestra(Dialogo, Mostrar)
{
    var Control = "";
    if (Dialogo == "DlgAgregarFav")
        Control = '<%=DlgAgregarFav.ClientID%>';
    if (Dialogo == "DlgCompartirFav")
        Control = '<%=DlgCompartirFav.ClientID%>';
        
    if (Dialogo == "DlgConfirma")
        Control = '<%=DlgConfirma.ClientID%>';
    if(Mostrar == true)
        $find(Control).set_windowState($IG.DialogWindowState.Normal);
    else
        $find(Control).set_windowState($IG.DialogWindowState.Hidden);
}
function Oculta(Dialogo) {
    Muestra(Dialogo,false);
}
function OcultayEjecuta(Dialogo, Evento) {
    Oculta(Dialogo);
    __doPostBack(Evento, '');
}
function BtnCompartirFavAceptar_Click() {
//debugger;
    Oculta('DlgCompartirFav');
	theForm.elements['ValorMenuTag'].value = g_itemTag;
	theForm.elements['ValorMenuTagPadre'].value = g_itemTagPadre;    
    __doPostBack('BtnCompartirFavAceptar', '');
}
function BtnAceptar_Click(){
	Oculta('DlgConfirma');
	theForm.elements['ValorMenuTag'].value = g_itemTag;
	theForm.elements['ValorMenuTagPadre'].value = g_itemTagPadre;
	__doPostBack('MenuMenu', '');
}


function BtnAceptar1_Click(oButton, oEvent){
    $find('<%=DlgCompartirFav.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
    //	debugger;
    theForm.elements['ValorMenuTag'].value = g_itemTag;
    theForm.elements['ValorMenuTagPadre'].value = g_itemTagPadre;
    __doPostBack('__Page', "BtnAceptar1_Click");
}
var MostrarNoDias = false;
function Btn_NoDias_Click(oButton, oEvent){
    MostrarNoDias = !MostrarNoDias;
    var Dias;
    var Fecha;
    if(MostrarNoDias)
    {
        Dias = 'block';
        Fecha = 'none';
    }
    else
    {
        Dias = 'none';
        Fecha = 'block';
//        debugger;
        var editor = $find('<%=Tbx_NoDias.ClientID%>');
      /*  var button = ig_getWebControlById("Tbx_NoDias");
        button.setText(button.getText()+' ');
        //__doPostBack('Btn_NoDias', '');*/
        __doPostBack('Tbx_NoDias', editor.get_value());
        return;
    }
         /* var button = ig_getWebControlById("Btn_NoDias");

            alert(button.getText());
            button.setText('123');*/
    document.getElementById('TD_NoDias').style.display = Dias;
    document.getElementById('TD_Tbx_NoDias').style.display = Dias;
    document.getElementById('TD_FechaFinal').style.display = Fecha;
}
function OnPageLoad() {
SetGridSize();
}
function SetGridSize() {
   // debugger;
    //g_grdMediaAttachments_ClientID defined in the codeBehind
    var grid = igtbl_getGridById("Grid");
    if(grid == null)
        return ;
//    grid.resize("100%", "100px");
            alto = ObtenAltoVentana()-5;
            ancho = ObtenAnchoVentana()-5 ;
    alto = document.getElementById('TD_Grid').offsetHeight-5;
   // document.getElementById('wrapper').width=ancho+"px";
    //ancho = document.getElementById('TD_Grid').offsetWidth-5;
   // return;
    grid.resize(ancho, alto);
   // grid.MainGrid.style.height=alto+"px";
   // _igtbl_PagerRedraw(grid);
}
    </script>
    <style type="text/css">
        .style1
        {
            width: 73px;
        }
        html, body, #wrapper, #Tabla
        {
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            border: none;
            text-align: center;
            overflow: hidden;
        }
    </style>
    <script type="text/javascript" id="igClientScript">
<!--

        function Grid_InitializeLayoutHandler(gridName) {

        }
// -->
    </script>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px;"
    onload="OnPageLoad();" onresize="SetGridSize();">
    <form id="Tabla" runat="server">
    <input name="ValorMenuTagPadre" id="ValorMenuTagPadre" type="hidden" value="" />
    <input name="ValorMenuTag" id="ValorMenuTag" type="hidden" value="" />
    <table id="wrapper" cellspacing="0" border="0">
        <tr style="height: 1px;">
            <td colspan="4" rowspan="1" align="left">
                <ignav:UltraWebMenu ID="Menu" runat="server" EnableAppStyling="True" OnMenuItemChecked="Menu_MenuItemChecked"
                    OnMenuItemClicked="Menu_MenuItemClicked" StyleSetName="Caribbean" Width="100%"
                    ItemPaddingSubMenus="0" Height="100%">
                    <Images>
                        <SubMenuImage Url="ig_menuTri.gif" />
                    </Images>
                    <MenuClientSideEvents ItemClick="Menu_ItemClick" />
                    <Items>
                        <ignav:Item TagString="FAVORITOS" Text="+" ToolTip="Favoritos (Diseños favoritos)">
                            <Items>
                                <ignav:Item TagString="F_AGREGAR" Text="Agregar la consulta" ToolTip="Agrega la consulta actual como favorita">
                                </ignav:Item>
                                <ignav:Item Separator="True" Text="">
                                </ignav:Item>
                                <ignav:Item TagString="F_SOLO_RETARDOS" Text="Solo Retardos" ToolTip="Muestra la consulta de solo retardos">
                                </ignav:Item>
                                <ignav:Item TagString="F_SOLO_FALTAS" Text="Solo Faltas" ToolTip="Muestra la consulta de solo faltas">
                                </ignav:Item>
                                <ignav:Item Separator="True" Text="">
                                </ignav:Item>
                            </Items>
                        </ignav:Item>
                        <ignav:Item CheckBox="True" ImageUrl="./Imagenes/Iconos/GraficarAsistencias16.png"
                            TagString="AsistenciaGraficar" Text="Graficar" ToolTip="Muestra totales en formato grafico">
                            <Items>
                                <ignav:Item CheckBox="True" TagString="AS_G_AGRUPACION" Text="Mostrar Agrupacion">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="AS_G_EMPLEADO" Text="Mostrar Empleado">
                                </ignav:Item>
                            </Items>
                            <Images>
                                <DefaultImage Url="./Imagenes/Iconos/GraficarAsistencias16.png" />
                            </Images>
                        </ignav:Item>
                        <ignav:Item CheckBox="True" ImageUrl="./Imagenes/Iconos/MostrarAsistencias16.png"
                            TagString="AsistenciaMostrar" Text="Asistencias">
                            <Items>
                                <ignav:Item CheckBox="True" Checked="True" TagString="AS_PREDETERMINADO" Text="Formato Predeterminado">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="AS_7_DIAS" Text="Formato 7 días">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="AS_31_DIAS" Text="Formato Horizontal">
                                </ignav:Item>
                                <ignav:Item Separator="True" Text="-">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" Checked="True" TagString="AS_ENTRADA_SALIDA" Text="Mostrar Entrada y Salida">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="AS_COMIDA" Text="Mostrar Comida">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="AS_HORAS_EXTRAS" Text="Mostrar Horas Extras">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" Checked="True" TagString="AS_TOTALES" Text="Mostrar Totales">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" Checked="True" TagString="AS_INCIDENCIA" Text="Mostrar Incidencia">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" Checked="True" TagString="AS_TURNO" Text="Mostrar Turno">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" Checked="True" TagString="AS_EMPLEADO" Text="Mostrar Empleado">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="AS_AGRUPACION" Text="Mostrar Agrupacion">
                                </ignav:Item>
                                <ignav:Item TagString="FILTRAR_POR" Text="Filtrar por" ToolTip="Indica que solo mostrará determinadas incidencias">
                                    <Items>
                                        <ignav:Item CheckBox="True" TagString="AS_SOLO_FALTAS" Text="Solo Faltas">
                                        </ignav:Item>
                                        <ignav:Item CheckBox="True" TagString="AS_SOLO_RETARDOS" Text="Solo Retardos">
                                        </ignav:Item>
                                        <ignav:Item CheckBox="True" TagString="AS_SOLO_JUSTIFICACIONES" Text="Todas las justificaciones">
                                        </ignav:Item>
                                        <ignav:Item Separator="True" Text="">
                                        </ignav:Item>
                                        <ignav:Item CheckBox="False" TagString="FILTRAR_POR_SISTEMA" Text="Incidencias automáticas">
                                        </ignav:Item>
                                        <ignav:Item Separator="True" Text="">
                                        </ignav:Item>
                                    </Items>
                                </ignav:Item>
                            </Items>
                            <Images>
                                <DefaultImage Url="./Imagenes/Iconos/MostrarAsistencias16.png" />
                            </Images>
                        </ignav:Item>
                        <ignav:Item CheckBox="True" ImageUrl="./Imagenes/Iconos/MostrarAsistenciasES16.png"
                            TagString="AsistenciaMostrarES" Text="Ent/Sal">
                            <Images>
                                <DefaultImage Url="./Imagenes/Iconos/MostrarAsistenciasES16.png" />
                            </Images>
                        </ignav:Item>
                        <ignav:Item CheckBox="True" ImageUrl="./Imagenes/Iconos/GraficarAsistencias16.png"
                            TagString="AsistenciaTotales" Text="Totales" ToolTip="Muestra totales">
                            <Items>
                                <ignav:Item CheckBox="True" TagString="AS_T_AGRUPACION" Text="Mostrar Agrupacion">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="AS_T_EMPLEADO" Text="Mostrar Empleado">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" Checked="True" TagString="AS_T_TOTALES" Text="Totales">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="AS_T_HISTORIAL" Text="Historial">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="AS_T_SALDO" Text="Saldo">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="AS_T_FALTAS" Text="Faltas">
                                </ignav:Item>
                            </Items>
                            <Images>
                                <DefaultImage Url="./Imagenes/Iconos/GraficarAsistencias16.png" />
                            </Images>
                        </ignav:Item>
                        <ignav:Item CheckBox="True" ImageUrl="./Imagenes/Iconos/MostarAccesos16.png" TagString="AsistenciaMostrarAccesos"
                            Text="Accesos" ToolTip="Muestra los Accesos">
                            <Images>
                                <DefaultImage Url="./Imagenes/Iconos/MostarAccesos16.png" />
                            </Images>
                            <Items>
                                <ignav:Item TagString="MostrarTodasTerminales" Text="Mostrar Todas las Terminales" 
                                ToolTip="Muestra Entrada y Salida por Terminal">
                                </ignav:Item>
                                <ignav:Item Separator="True" Text="">
                                </ignav:Item>
                            </Items>
                        </ignav:Item>
                        <ignav:Item Hidden="True" TagString="BORRAR_INVENTARIO" 
                            Text="Borrar Inventario" 
                            ToolTip="Revierte un movimiento en el inventario de incidencias">
                        </ignav:Item>
                        <ignav:Item ImageUrl="./Imagenes/Iconos/TurnoEmpleado16.png" TagString="TURNOS" Text="Asignar Turno Día"
                            ToolTip="Asigna un turno solo para los días seleccionados">
                            <Items>
                                <ignav:Item TagString="TURNOSPRED" Text="Definir Turno Predeterminado" ToolTip="Asigna el turno que se usará para los días posteriores a la fecha de hoy, siempre y cuando no tengan un turno temporal asignado,no modificará ninguna asistencia calculada">
                                </ignav:Item>
                            </Items>
                            <Images>
                                <DefaultImage Url="./Imagenes/Iconos/TurnoEmpleado16.png" />
                            </Images>
                        </ignav:Item>
                        <ignav:Item ImageUrl="./Imagenes/Iconos/Justificacion16.png" TagString="JUSTIFICACIONES"
                            Text="Justificar" ToolTip="Permite justificar o asignar una incidencia a un día o empleado">
                            <Images>
                                <DefaultImage Url="./Imagenes/Iconos/Justificacion16.png" />
                            </Images>
                        </ignav:Item>
                        <ignav:Item ImageUrl="./Imagenes/Iconos/TurnoEmpleado16.png" TagString="AGREGAR_INCIDENCIAS"
                            Text="Agregar Incidencias" ToolTip="Permite justificar o asignar varias incidencias">
                            <Images>
                                <DefaultImage Url="./Imagenes/Iconos/TurnoEmpleado16.png" />
                            </Images>
                        </ignav:Item>
                        <ignav:Item ImageUrl="./Imagenes/Iconos/Exportar16.png" TagString="EXPORTAR" Text="Exportar">
                            <Items>
                                <ignav:Item TagString="EXCEL" Text="Excel">
                                </ignav:Item>
                                <%--<ignav:Item TagString="DBF" Text="DBF">
                                </ignav:Item>--%>
                                <ignav:Item TagString="TEXTO" Text="Texto">
                                    <Items>
                                        <ignav:Item TagString="CSV" Text="CSV">
                                        </ignav:Item>
                                        <ignav:Item TagString="TXT" Text="TXT">
                                        </ignav:Item>
                                    </Items>
                                </ignav:Item>
                                <ignav:Item Separator="True" Text="-">
                                </ignav:Item>
                                <ignav:Item TagString="PDF" Text="PDF">
                                    <Items>
                                        <ignav:Item TagString="PDF_VERTICAL" Text="Vertical">
                                        </ignav:Item>
                                        <ignav:Item TagString="PDF_HORIZONTAL" Text="Horizontal">
                                        </ignav:Item>
                                    </Items>
                                </ignav:Item>
                                <ignav:Item TagString="XPS" Text="XPS">
                                    <Items>
                                        <ignav:Item TagString="XPS_VERTICAL" Text="Vertical">
                                        </ignav:Item>
                                        <ignav:Item TagString="XPS_HORIZONTAL" Text="Horizontal">
                                        </ignav:Item>
                                    </Items>
                                </ignav:Item>
                                <ignav:Item Separator="True" Text="-">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="FIRMA" Text="Agregar Firma">
                                </ignav:Item>
                            </Items>
                            <Images>
                                <DefaultImage Url="./Imagenes/Iconos/Exportar16.png" />
                            </Images>
                        </ignav:Item>
                    </Items>
                    <DisabledStyle Font-Names="MS Sans Serif" Font-Size="8pt" ForeColor="Gray">
                    </DisabledStyle>
                    <Levels>
                        <ignav:Level Index="0" />
                        <ignav:Level Index="1" />
                        <ignav:Level Index="2" />
                    </Levels>
                    <SeparatorStyle BackgroundImage="ig_menuSep.gif" CssClass="SeparatorClass" CustomRules="background-repeat:repeat-x; " />
                    <ExpandEffects ShadowColor="LightGray" />
                </ignav:UltraWebMenu>
            </td>
        </tr>
        <tr style="height: 1px; width: 78px;">
            <td>
                <asp:Image ID="ImgEmpleado" runat="server" BorderStyle="Inset" Height="96px" Width="78px" />
            </td>
            <td style="height: 1px; width: 7800px;">
                <table cellspacing="2" style="width: 100%;">
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblNoEmpleadoSL" runat="server" Text="No. Empleado:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;" colspan="6">
                            <asp:Label ID="LblNoEmp" runat="server" Text="Label" Width="202px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblNombreSL" runat="server" Text="Nombre:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;" colspan="6">
                            <asp:Label ID="LblNombre" runat="server" Text="Label" Width="202px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblAgrupacionSL" runat="server" Text="Agrupación:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;" colspan="6">
                            <asp:Label ID="LblAgrupacion" runat="server" Text="Label" Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblTurnoSL" runat="server" Text="Turno:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;" colspan="6">
                            <asp:Label ID="LblTurno" runat="server" Text="Label" Width="202px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblAgrupacionSL0" runat="server" Text="Periodo:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;">
                            <igsch:WebDateChooser ID="FechaInicial" runat="server" Value="" Width="81px" EnableAppStyling="True"
                                OnValueChanged="FechaInicial_ValueChanged" StyleSetName="Caribbean" Height="22px">
                                <AutoPostBack ValueChanged="True" />
                            </igsch:WebDateChooser>
                        </td>
                        <td style="text-align: left; width: 143px" id="TD_FechaFinal">
                            <igsch:WebDateChooser ID="FechaFinal" runat="server" Width="81px" EnableAppStyling="True"
                                OnValueChanged="FechaFinal_ValueChanged" StyleSetName="Caribbean" Height="22px">
                                <AutoPostBack ValueChanged="True" />
                            </igsch:WebDateChooser>
                        </td>

                        <td style="text-align: left; width: 100px;display: none;"" id="TD_NoDias">
                        No de Días
                        </td>
                        <td style="text-align: left; width: 60px;display: none;"" id="TD_Tbx_NoDias">
                            <ig:WebNumericEditor ID="Tbx_NoDias" runat="server" DataMode="Int"
                                MaxDecimalPlaces="0" Width="60px" MaxValue="100" MinValue="1">
                                <Buttons SpinButtonsDisplay="OnRight">
                                </Buttons>
                            </ig:WebNumericEditor>
                        </td>

                                                <td style="text-align: left; width: 20;" >
                            <igtxt:WebImageButton ID="Btn_NoDias" runat="server" Height="20px" 
                                Width="20px" Text="&gt;" AutoSubmit="False" >
                                <ClientSideEvents Click="Btn_NoDias_Click()" />
                            </igtxt:WebImageButton>
                        </td>
                                                <td style="text-align: left; width: 100%;" >
                        </td>
                    </tr>
                </table>
            </td>
            <td style="height: 0px; width: 1px;">
                <igmisc:WebGroupBox ID="WebGroupBox1" runat="server" EnableAppStyling="True" StyleSetName="Caribbean"
                    Text="Tiempos" Width="160px" Height="98px">
                    <Template>
                        <table cellspacing="2" style="width: 100%; height: 80px">
                            <tr>
                                <td style="text-align: right; width: 113px;">
                                    <strong>X Trabajar</strong>:
                                </td>
                                <td style="text-align: left; width: 134px;">
                                    <asp:Label ID="LblHXTrabajar" runat="server" Height="21px" Text="" Width="120px"></asp:Label>
                                </td>
                            </tr>
                            <tr style="font-weight: bold">
                                <td style="text-align: right; width: 113px;">
                                    Trabajado:
                                </td>
                                <td style="text-align: left; width: 134px;">
                                    <asp:Label ID="LblHTrabajadas" runat="server" Font-Bold="False" Height="21px" Text=""
                                        Width="120px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 113px; text-align: right">
                                    <strong>Comida:</strong>
                                </td>
                                <td style="width: 134px; text-align: left">
                                    <asp:Label ID="LblHComida" runat="server" Font-Bold="False" Height="21px" Text=""
                                        Width="120px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 113px;">
                                    <strong>Retardo:</strong>
                                </td>
                                <td style="text-align: left; width: 134px;">
                                    <asp:Label ID="LblHRetardo" runat="server" Height="21px" Text="" Width="120px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 113px;">
                                    <strong>Extra:</strong>
                                </td>
                                <td style="text-align: left; width: 134px;">
                                    <asp:Label ID="LblExtra" runat="server" Height="21px" Text="" Width="120px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </Template>
                </igmisc:WebGroupBox>
            </td>
            <td style="height: 0px; width: 1px;">
                <igchart:UltraChart ID="Chart" runat="server" BackColor="" Height="98px" Width="176px"
                    BackgroundImageFileName="" EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource"
                    SmoothingMode="None" TextRenderingHint="ClearTypeGridFit" ChartType="BarChart"
                    Version="11.1">
                    <Legend BackgroundColor="Transparent" BorderThickness="0" Font="Calibri, 9.75pt, style=Bold"
                        FontColor="89, 89, 89"></Legend>
                    <ColorModel AlphaLevel="150" ColorBegin="Pink" ModelStyle="CustomLinear" ColorEnd="DarkRed">
                        <Skin ApplyRowWise="False">
                            <PEs>
                                <igchartprop:PaintElement ElementType="Gradient" Fill="47, 123, 214" FillGradientStyle="Vertical"
                                    FillStopColor="29, 82, 145" Stroke="29, 82, 145" StrokeWidth="0" />
                            </PEs>
                        </Skin>
                    </ColorModel>
                    <CompositeChart>
                        <Series>
                            <igchartprop:NumericSeries Key="Asistencias" Label="Asistencias">
                                <points>
                                            <igchartprop:NumericDataPoint Value="100">
                                                <pe elementtype="None" />
<PE ElementType="None"></PE>
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                <pes>
                                            <igchartprop:PaintElement ElementType="Gradient" Fill="MediumBlue" 
                                                FillGradientStyle="Vertical" FillStopColor="Navy" />
                                        </pes>
                            </igchartprop:NumericSeries>
                            <igchartprop:NumericSeries Key="Retardos" Label="Retardos">
                                <points>
                                            <igchartprop:NumericDataPoint Value="5">
                                                <pe elementtype="None" />
<PE ElementType="None"></PE>
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                <pes>
                                            <igchartprop:PaintElement ElementType="Gradient" Fill="Yellow" 
                                                FillGradientStyle="Vertical" FillStopColor="Orange" />
                                        </pes>
                            </igchartprop:NumericSeries>
                            <igchartprop:NumericSeries Key="Faltas" Label="Faltas">
                                <points>
                                            <igchartprop:NumericDataPoint Value="50">
                                                <pe elementtype="None" />
<PE ElementType="None"></PE>
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                <pes>
                                            <igchartprop:PaintElement ElementType="Gradient" Fill="Red" 
                                                FillGradientStyle="Vertical" FillStopColor="Maroon" />
                                        </pes>
                            </igchartprop:NumericSeries>
                            <igchartprop:NumericSeries Key="Justificaciones" Label="Justificaciones">
                                <points>
                                    <igchartprop:NumericDataPoint Value="15">
                                        <pe elementtype="None" />
                                        <PE ElementType="None"></PE>
                                    </igchartprop:NumericDataPoint>
                                </points>
                                <pes>
                                    <igchartprop:PaintElement ElementType="Gradient" Fill="Green" 
                                        FillStopColor="DarkGreen" />
                                </pes>
                            </igchartprop:NumericSeries>
                        </Series>
                    </CompositeChart>
                    <Axis>
                        <PE Fill="233, 237, 244" StrokeWidth="0" />
                        <X Extent="12" LineColor="White" LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart"
                            Visible="True">
                            <Margin>
                                <Near Value="1.4388489208633095" />
                                <Far Value="-2.4096385542168677" />
                            </Margin>
                            <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                Visible="False" />
                            <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                Visible="False" />
                            <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" HorizontalAlign="Far"
                                    Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </SeriesLabels>
                                <Layout Behavior="Auto">
                                </Layout>
                            </Labels>
                        </X>
                        <Y Extent="58" LineColor="White" LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart"
                            Visible="True">
                            <Margin>
                                <Far Value="-3.9215686274509802" />
                            </Margin>
                            <MajorGridLines AlphaLevel="255" Color="White" DrawStyle="Dot" Thickness="1" Visible="True" />
                            <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                Visible="False" />
                            <Labels Font="Verdana, 7pt" HorizontalAlign="Far" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                Orientation="Horizontal" VerticalAlign="Far" FontColor="DimGray">
                                <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" Orientation="Horizontal"
                                    VerticalAlign="Far">
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </SeriesLabels>
                                <Layout Behavior="Auto">
                                </Layout>
                            </Labels>
                        </Y>
                        <Y2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                            <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                Visible="True" />
                            <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                Visible="False" />
                            <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                Orientation="Horizontal" VerticalAlign="Center">
                                <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Center" Orientation="VerticalLeftFacing"
                                    VerticalAlign="Center">
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </SeriesLabels>
                                <Layout Behavior="Auto">
                                </Layout>
                            </Labels>
                        </Y2>
                        <X2 LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="False">
                            <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                Visible="True" />
                            <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                Visible="False" />
                            <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" HorizontalAlign="Far"
                                    Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </SeriesLabels>
                                <Layout Behavior="Auto">
                                </Layout>
                            </Labels>
                        </X2>
                        <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                            <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                Visible="True" />
                            <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                Visible="False" />
                            <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString=""
                                Orientation="Horizontal" VerticalAlign="Center">
                                <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                    VerticalAlign="Center">
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </SeriesLabels>
                                <Layout Behavior="Auto">
                                </Layout>
                            </Labels>
                        </Z>
                        <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                            <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                Visible="True" />
                            <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                Visible="False" />
                            <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                                Orientation="Horizontal" VerticalAlign="Center">
                                <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="VerticalLeftFacing"
                                    VerticalAlign="Center">
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </SeriesLabels>
                                <Layout Behavior="Auto">
                                </Layout>
                            </Labels>
                        </Z2>
                    </Axis>
                    <TitleLeft FontColor="0, 0, 192" Extent="33" Visible="True" Location="Left">
                    </TitleLeft>
                    <Effects>
                        <Effects>
                            <igchartprop:GradientEffect />
                        </Effects>
                    </Effects>
                    <TitleRight FontColor="0, 0, 192" Extent="33" Visible="True" Location="Right">
                    </TitleRight>
                    <TitleTop Font="Lucida Console, 10.2pt" FontColor="0, 0, 192">
                    </TitleTop>
                    <TitleBottom Font="Lucida Console, 10.2pt" FontColor="0, 0, 192" Visible="False"
                        Extent="33" Location="Bottom">
                    </TitleBottom>
                    <Border Color="134, 134, 134" />
                    <Tooltips BackColor="192, 192, 255" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" FontColor="Navy" />
                </igchart:UltraChart>
            </td>
        </tr>
        <tr>
            <td colspan="4" rowspan="1" id="TD_Grid" style="vertical-align: top; text-align: left">
                <div style="height:300px" id="Div_Grid">
                    <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" OnInitializeDataSource="Grid_InitializeDataSource"
                        OnInitializeLayout="Grid_InitializeLayout" OnInitializeRow="Grid_InitializeRow" Style="left: 0px; top: 0px" Width="100%"
                        OnSortColumn="Grid_SortColumn" 
                        OnColumnMove="Grid_ColumnMove" OnGroupColumn="Grid_GroupColumn" 
                        OnUnGroupColumn="Grid_UnGroupColumn">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowRowNumberingDefault="Continuous"
                            BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect" HeaderClickActionDefault="SortMulti"
                            LoadOnDemand="Xml" Name="Grid" RowHeightDefault="20px" RowSelectorsDefault="No"
                            RowsRange="30" ScrollBar="Always" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                            XmlLoadOnDemandType="Accumulative" AllowSortingDefault="Yes"
                            AllowUpdateDefault="Yes">
                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" 
                                Width="100%">
                            </FrameStyle>
                            <ClientSideEvents InitializeLayoutHandler="Grid_InitializeLayoutHandler" />
                            <Pager MinimumPagesForDisplay="2">
                            </Pager>
                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                            </RowStyleDefault>
                            <FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
                                <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                            </FooterStyleDefault>
                            <ActivationObject BorderColor="" BorderWidth="">
                            </ActivationObject>
                        </DisplayLayout>
                    </igtbl:UltraWebGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4" rowspan="1" style="height: 1px; text-align: center">
            </td>
        </tr>
    </table>
    <igtbldocexp:UltraWebGridDocumentExporter ID="GridExporter" runat="server" OnBeginExport="GridExporter_BeginExport"
        OnRowExported="GridExporter_RowExported" 
        OnRowExporting="GridExporter_RowExporting" EnableViewState="False" 
        onendexport="GridExporter_EndExport">
    </igtbldocexp:UltraWebGridDocumentExporter>
    <igtblexp:UltraWebGridExcelExporter ID="GridExcel" runat="server">
    </igtblexp:UltraWebGridExcelExporter>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ig:WebDialogWindow ID="DlgConfirma" runat="server" Height="114px" InitialLocation="Centered"
        Width="342px" Modal="True" WindowState="Hidden">
        <Header CaptionText="Atenci&#243;n">
            <CloseBox Visible="False"></CloseBox>
        </Header>
        <ContentPane>
            <Template>
                <span style="font-size: 9pt">
                    <asp:Label ID="LblMensage" runat="server" Text="¿Esta seguro de que desea asignar el turno a los días y personas seleccionadas?"></asp:Label>
                    <br />
                    <br />
                    <igtxt:WebImageButton ID="BtnCancelar" runat="server" Text="Cancelar" ClickOnSpaceKey="False"
                        ClickOnEnterKey="False">
                        <ClientSideEvents MouseDown="BtnCancelar_Click" Click="BtnCancelar_Click" />
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;<igtxt:WebImageButton ID="BtnAceptar" runat="server" Text="Aceptar">
                        <ClientSideEvents Click="BtnAceptar_Click()" />
                    </igtxt:WebImageButton>
                </span>
            </Template>
        </ContentPane>
    </ig:WebDialogWindow>
    <ig:WebDialogWindow ID="DlgAgregarFav" runat="server" Height="114px" InitialLocation="Centered"
        Width="342px" Modal="True" WindowState="Hidden">
        <Header CaptionText="Atenci&#243;n">
            <CloseBox Visible="False"></CloseBox>
        </Header>
        <ContentPane>
            <Template>
                <span style="font-size: 9pt">
                    <asp:Label ID="LblMensage0" runat="server" Text="¿Desea agregar la consulta actual a favoritos?"></asp:Label>
                    <br />
                    Nombre
                    <asp:TextBox ID="Tbx_NombreFav" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Se requiere nombre del favorito"
                        ControlToValidate="Tbx_NombreFav"></asp:RequiredFieldValidator>
                    <br />
                    <igtxt:WebImageButton ID="BtnCancelar0" runat="server" Text="Cancelar" ClickOnSpaceKey="False"
                        ClickOnEnterKey="False" CausesValidation="False" OnClick="BtnCancelar0_Click"
                        AutoSubmit="False">
                        <ClientSideEvents MouseDown="Oculta('DlgAgregarFav');" />
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;<igtxt:WebImageButton ID="BtnNuevoFavAceptar" runat="server" Text="Aceptar"
                        AutoSubmit="False">
                        <ClientSideEvents Click="OcultayEjecuta('DlgAgregarFav','BtnNuevoFavAceptar');" />
                    </igtxt:WebImageButton>
                </span>
            </Template>
        </ContentPane>
    </ig:WebDialogWindow>
    <ig:WebDialogWindow ID="DlgCompartirFav" runat="server" Height="114px" InitialLocation="Centered"
        Width="404px" Modal="True" WindowState="Hidden">
        <Header CaptionText="Atenci&#243;n">
            <CloseBox Visible="False"></CloseBox>
        </Header>
        <ContentPane>
            <Template>
                <span style="font-size: 9pt">
                    <asp:Label ID="LblMensage1" runat="server" Text="¿Escriba el usuario al que desea compartir su favorito?"></asp:Label>
                    <br />
                    Nombre de usuario
                    <asp:TextBox ID="Tbx_NombreUsuario" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Se requiere nombre del usuario"
                        ControlToValidate="Tbx_NombreUsuario"></asp:RequiredFieldValidator>
                    <br />
                    <igtxt:WebImageButton ID="BtnCancelar1" runat="server" Text="Cancelar" ClickOnSpaceKey="False"
                        ClickOnEnterKey="False" CausesValidation="False" OnClick="BtnCancelar0_Click">
                        <ClientSideEvents MouseDown="Oculta('DlgCompartirFav');" />
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;<igtxt:WebImageButton ID="BtnCompartirFavAceptar" runat="server" Text="Aceptar"
                        AutoSubmit="False">
                        <ClientSideEvents Click="BtnCompartirFavAceptar_Click();" />
                    </igtxt:WebImageButton>
                </span>
            </Template>
        </ContentPane>
    </ig:WebDialogWindow>
    </form>
</body>
</html>
<script type="text/javascript">
    /*  // debugger;
    alto = document.getElementById('TD_Grid').offsetHeight;
    //    document.getElementById('Div_Grid').style.height = (alto-60) + 'px';
    //get the grid object from grid id
    var grid = igtbl_getGridById("Grid");
    // grid.MainGrid.style.height = alto + "px";
    //grid.resize("100%", alto-2);
    //grid.Height = (alto - 2) + "px";


    var gridHeight = (alto - 2) + "px";
    grid.MainGrid.style.height = gridHeight;
    tableId = "G_" + grid.Id;
    document.getElementById(tableId).style.height = 'auto';
    grid.alignStatMargins();*/
    //   document.getElementById('Grid_main').style.height = (alto - 60) + 'px';
</script>
