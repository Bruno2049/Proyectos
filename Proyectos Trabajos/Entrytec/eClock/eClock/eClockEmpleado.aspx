<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eClockEmpleado.aspx.cs" Inherits="eClockEmpleado" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebListbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebListbar" TagPrefix="iglbar" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="WC_LinksPiePagina.ascx" TagName="WC_LinksPiePagina" TagPrefix="uc3" %>
<%@ Register Src="WCBotonesEncabezado.ascx" TagName="WCBotonesEncabezado" TagPrefix="uc2" %>
<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="flashmovie" Namespace="Osmosis.Web.UI.Controls" Assembly="FlashMovie" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eClock Portal del empleado</title>

    <style type="text/css">
.ig_CaribbeanControl
{
	background-color:White;
	font-size:xx-small;
	font-family: verdana;
	cursor:default;
	color:Black;
}


.ig_CaribbeanControl
{
	background-color:White;
	font-size:xx-small;
	font-family: verdana;
	cursor:default;
	color:Black;
}


    </style>
    <script id="InfragisticseClock" type="text/javascript">
<!--
var sessionTimeout = "<%= Session.Timeout %>";
function LimpiaTimeOut()
{
    sessionTimeout = "<%= Session.Timeout %>";
    MuestraTimeOut();
}
//assigning minutes left to session timeout to Label
function MuestraTimeOut()
{
document.getElementById("<%= Lbl_TimeOut.ClientID %>").innerText = "Restan " + sessionTimeout + " minutos";
}
var SesionAlertada = 0;
function DisplaySessionTimeout()
{
    
    MuestraTimeOut();
    
    sessionTimeout = sessionTimeout - 1;
    
    //if session is not less than 0
    if (sessionTimeout >= 0)
        //call the function again after 1 minute delay
        window.setTimeout("DisplaySessionTimeout()", 60000);
    else
    {
        if(SesionAlertada == 0)
        {
            //show message box
            alert("Su sesión ha caducado.");
            SesionAlertada = 1; 
            location = "WF_LogIn.aspx"
        }
    }
}

function AsignacionTurnos()
{
    MuestraTurno();
    MuestraTab("TabTurnos",4);
    
}
function EditaUsuario()
{
    //alert("Atencion");
    MuestraPagina("WF_UsuarioEd.aspx");
}
function VerPendientes()
{
    MuestraEmpleados();
    MuestraTab("TabEmpleados",0);
//    OcultaFrame(false);
//    self.frames[0].location = "WF_Termin.aspx?";
}
function MuestraPagina(Pagina)
{
    LimpiaTimeOut();
    OcultaFrame(false);
    self.frames[0].location = Pagina;
    self.frames["FrameMain"].location = Pagina;
    document.getElementById("FrameMain").location = Pagina;
}

function OcultaFrame(Ocultar)
{
    if(Ocultar == true)
    {
        document.getElementById("FrameMain").style.display = 'none';

    } 
    else
    {
        document.getElementById("FrameMain").style.display = 'block';
                OcultaTab("TabEmpleados",true);
        OcultaTab("TabAgrupacion",true);
        OcultaTab("TabEmpleado",true);
        OcultaTab("TabTurnos",true);
        OcultaTab("TabTurno",true);
        OcultaTab("TabConfiguracion",true);
        OcultaTab("TabConfiguracionAdv",true);
    }
}
var UtlimoNodoEmpleadosClick;
function OcultaTab(tab, Ocultar)
{
    LimpiaTimeOut();
//debugger;
    if(Ocultar == true)
    {
          document.getElementById("igtab"+ tab).style.display = 'none';
//        document.getElementById("igtab"+ tab).setAttribute("style","display:none;");
        
    }
    else
    {
        OcultaFrame(true);   
        document.getElementById("igtab"+ tab).style.display = 'block';
        //document.getElementById("igtab"+ tab).setAttribute("style","DISPLAY: block; VISIBILITY: visible");
        ActualizaTab0(tab); 
    }
}
function MuestraEmpleados()
{
	try
    {   
        OcultaTab("TabEmpleados",false);
        OcultaTab("TabAgrupacion",true);
        OcultaTab("TabEmpleado",true);
        OcultaTab("TabTurnos",true);
        OcultaTab("TabTurno",true);
        OcultaTab("TabConfiguracion",true);
        OcultaTab("TabConfiguracionAdv",true);
       // loadSite();
    }
	catch(err)
	{
	    //alert(err.description);
	}	
}

function ObtenRandom()
{
    return "&Rnd="+Math.floor(Math.random()*101);
}
function TreeEmpleados_AfterNodeSelectionChange(treeId, nodeId){		
	try
    {           
    
        var nod = igtree_getNodeById(nodeId);
        var Tag = nod.getTag();
//        alert(Tag);
        if(Tag.charAt(0)== 'M')
        {
            MuestraEmpleados();
            self.frames[0].location = "eClockAct.aspx?Parametros=PERSONAS|"+ObtenRandom();
 //           MuestraAgrupacion();
          
        }
        else
        {        
            self.frames[0].location = "eClockAct.aspx?Parametros=PERSONAS" + Tag+"|"+ObtenRandom();
         /*    document.frames.Ifrm.location = "eClockAct.aspx?Parametros=PERSONAS" + Tag;*/
            if(Tag.charAt(0)== '|' || Tag.charAt(0)== 'S')
            {
                MuestraAgrupacion();
            }
            else
            {            
                MuestraEmpleado();                
            }
        }
	}
	catch(err)
	{
	//alert(err.description);
	}	
	
}
function MuestraTab(UltraTab,SubTab)
{
    LimpiaTimeOut();
   var ultraTab = igtab_getTabById(UltraTab);
   ultraTab.setSelectedIndex(SubTab);
    var tabItem = ultraTab.Tabs[ultraTab.getSelectedIndex()];
    tabItem.setTargetUrl(ActualizaNombre(tabItem.getTargetUrl()) );
}
function ActualizaTab0(UltraTab)
{
    LimpiaTimeOut();
    if(UltraTab == "TabConfiguracion")
        return;
        debugger;
    var ultraTab = igtab_getTabById(UltraTab);
    var tabItem = ultraTab.Tabs[ultraTab.getSelectedIndex()];
    tabItem.setTargetUrl(ActualizaNombre(tabItem.getTargetUrl()) );
}
function ActualizaTab0Pagina(UltraTab, Pagina)
{
    LimpiaTimeOut();
    var ultraTab = igtab_getTabById(UltraTab);
    var tabItem = ultraTab.Tabs[0];
    tabItem.setTargetUrl("eClockAct.aspx?UrlDestino="+ Pagina);
}

function ActualizaNombre(Nombre)
{
    LimpiaTimeOut();
    var randomnumber=Math.floor(Math.random()*10)
    if(Nombre.indexOf(".aspx",12) <= 12)
        Nombre += ".aspx";
    else
    if(Nombre.indexOf("&") <= 0)
        Nombre += "&RND=1";
    else
        Nombre += randomnumber;
       // alert(Nombre);
    return Nombre;
}
function TabEmpleados_BeforeSelectedTabChange(oWebTab, oTab, oEvent){
   var str=oTab.getTargetUrl();
    str = ActualizaNombre(str);
	oTab.setTargetUrl(str );
	//alert(str);
}


function Mostrar()
{
//debugger;
        window.TabEmpleados_frame0.frameElement.setAttribute("style","VISIBILITY: visible");
//debugger;
}
function loadSite() {


                window.TabEmpleados_frame0.frameElement.setAttribute("style","display:none;");
debugger;
				// set up onload listeners differently for IE
				// and other browsers
				if (window.TabEmpleados_frame0.readyState) { // IE

					window.TabEmpleados_frame0.onreadystatechange = function() {
						if (window.TabEmpleados_frame0.readyState == "complete") {
							window.TabEmpleados_frame0.onreadystatechange = null;
							Mostrar();
						}
					}
				}
				else {
					window.TabEmpleados_frame0.onload = Mostrar;
				}

				// load the site
				//window.TabEmpleados_frame0.src = currentURL;
				//DisplaySessionTimeout();
			}

  
function FrameCargado()
{

}

function TabEmpleados_InitializeTabs(oWebTab){
    DisplaySessionTimeout();
    MuestraEmpleados();
}
// -->
</script>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 10px;
    background-image: url(skins/page.bg.colorb.jpg); background-repeat: no-repeat;
    background-color: #1560bd;">
    <form id="form1" runat="server" style="text-align: center">
    <table id="Table1" height="100%" cellspacing="1" cellpadding="1" width="100%" align="center"
        border="0">
        <tr>
            <td align="center" width="100%" height="100%">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                &nbsp;&nbsp;<table border="0" cellpadding="0" cellspacing="0" class="BoxTable">
                    <tr>
                        <td class="BoxTL" style="width: 11px; height: 11px;">
                            <img height="11" src="skins/boxed_002.gif" width="11" />
                        </td>
                        <td class="BoxT" style="background-repeat: repeat-x; height: 11px; background-color: white;">
                            <img height="11" src="skins/dummy.gif" width="11" />
                        </td>
                        <td class="BoxTR" style="width: 11px; background-repeat: repeat-y; height: 11px;
                            background-color: white;">
                            <img height="11" src="skins/boxed.TR.gif" width="11" />
                        </td>
                    </tr>
                    <tr>
                        <td class="BoxL" style="width: 11px; background-repeat: repeat-y; background-color: white;">
                            <img height="11" src="skins/dummy.gif" width="11" />
                        </td>
                        <td class="BoxM" valign="top" style="background-color: white">
                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="LogoRow" colspan="2" style="width: 937px">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MenuHeight" colspan="2" style="width: 937px; height: 49px">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="FooterTable">
                                            <!-- fwtable fwsrc="Titulo.png" fwpage="Titulo" fwbase="Titulo.gif" fwstyle="Dreamweaver" fwdocid = "1726667265" fwnested="0" -->
                                            <tr>
                                                <td>
                                                    <img src="skins/spacer.gif" width="11" height="1" border="0" alt="" />
                                                </td>
                                                <td>
                                                    <img src="skins/spacer.gif" width="914" height="1" border="0" alt="" />
                                                </td>
                                                <td>
                                                    <img src="skins/spacer.gif" width="13" height="1" border="0" alt="" />
                                                </td>
                                                <td>
                                                    <img src="skins/spacer.gif" width="1" height="1" border="0" alt="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img name="Titulo_r1_c1" src="skins/Titulo_r1_c1.jpg" width="11" height="10" border="0"
                                                        id="Titulo_r1_c1" alt="" />
                                                </td>
                                                <td style="background-image: url(skins/Titulo_r1_c2.jpg); vertical-align: top; text-align: left">
                                                    <img name="Titulo_r1_c2" src="skins/Titulo_r1_c2.jpg" width="914" height="10" border="0"
                                                        id="Titulo_r1_c2" alt="" />
                                                </td>
                                                <td>
                                                    <img name="Titulo_r1_c3" src="skins/Titulo_r1_c3.jpg" width="13" height="10" border="0"
                                                        id="Titulo_r1_c3" alt="" />
                                                </td>
                                                <td>
                                                    <img src="skins/spacer.gif" width="1" height="10" border="0" alt="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-image: url(skins/Titulo_r2_c1.jpg);">
                                                </td>
                                                <td style="background-image: url(skins/Titulo_r2_c2.jpg); vertical-align: top; text-align: left;">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td class="LogoTD" width="550" style="text-align: left; height: 77px;" rowspan="2">
                                                                <asp:Image ID="Image1" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                    ImageUrl="Imagenes/imgencabezado.png" />
                                                            </td>
                                                            <td class="LoginTD" nowrap="nowrap" valign="top" style="text-align: right; height: 42px;
                                                                vertical-align: bottom;" colspan="2">
                                                                <div>
                                                                    <igtxt:webimagebutton ID="Btn_Principal" runat="server" EnableTheming="True" OnClick="Btn_Principal_Click"
                                                                        Text="Inicio" ToolTip="Pagina Principal">
                                                                        <Alignments HorizontalAll="Left" VerticalImage="Top" />
                                                                        <Appearance>
                                                                            <Image Url="./skins/ico-home.gif" />
                                                                            <ButtonStyle BackColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Bold="True"
                                                                                Font-Names="Tahoma" Font-Size="11px" ForeColor="#939393">
                                                                                <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                                            </ButtonStyle>
                                                                            <InnerBorder StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                                        </Appearance>
                                                                        <HoverAppearance>
                                                                            <Image Url="./skins/ico-home-over.gif" />
                                                                            <ButtonStyle ForeColor="WhiteSmoke">
                                                                            </ButtonStyle>
                                                                        </HoverAppearance>
                                                                        <RoundedCorners RenderingType="Disabled" />
                                                                        <FocusAppearance>
                                                                            <ButtonStyle Cursor="Hand">
                                                                            </ButtonStyle>
                                                                        </FocusAppearance>
                                                                    </igtxt:webimagebutton>
                                                                    &nbsp;&nbsp;
                                                                    <igtxt:webimagebutton ID="Btn_Usuario" runat="server" EnableTheming="True" Text="Nombre de Usuario"
                                                                        ToolTip="Editar Cuenta de Usuario" AutoSubmit="False" 
                                                                        >
                                                                        <Alignments VerticalImage="Top" />
                                                                        <Appearance>
                                                                            <Image Url="./skins/ico-register.gif" />
                                                                            <ButtonStyle BackColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Bold="True"
                                                                                Font-Names="Tahoma" Font-Size="11px" ForeColor="#939393">
                                                                                <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                                            </ButtonStyle>
                                                                            <InnerBorder StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                                        </Appearance>
                                                                        <ClientSideEvents Click="EditaUsuario()" />
                                                                        <HoverAppearance>
                                                                            <Image Url="./skins/ico-register-over.gif" />
                                                                            <ButtonStyle ForeColor="WhiteSmoke">
                                                                            </ButtonStyle>
                                                                        </HoverAppearance>
                                                                        <RoundedCorners RenderingType="Disabled" />
                                                                    </igtxt:webimagebutton>
                                                                    &nbsp;&nbsp;
                                                                    <igtxt:webimagebutton ID="Btn_Salir" runat="server" EnableTheming="True" OnClick="Btn_Salir_Click"
                                                                        Text="Salir" ToolTip="Salir del Sistema">
                                                                        <Alignments HorizontalAll="Left" VerticalImage="Top" />
                                                                        <Appearance>
                                                                            <Image Url="./skins/ico-login.gif" />
                                                                            <ButtonStyle BackColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Bold="True"
                                                                                Font-Names="Tahoma" Font-Size="11px" ForeColor="#939393">
                                                                                <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                                            </ButtonStyle>
                                                                            <InnerBorder StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                                        </Appearance>
                                                                        <HoverAppearance>
                                                                            <Image Url="./skins/ico-login-over.gif" />
                                                                            <ButtonStyle ForeColor="WhiteSmoke">
                                                                            </ButtonStyle>
                                                                        </HoverAppearance>
                                                                        <RoundedCorners RenderingType="Disabled" />
                                                                    </igtxt:webimagebutton>
                                                                    &nbsp;</div>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style2" nowrap="nowrap" valign="top" style="text-align: center; width: 1000px;
                                                                height: 35px; vertical-align: bottom;">
                                                                <asp:Label ID="Lbl_TimeOut" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="LoginTD" nowrap="nowrap" valign="top" style="text-align: right; height: 35px;
                                                                vertical-align: bottom;">
                                                                <span class="style1" style="vertical-align: middle">
                                                                <span class="style1" style="vertical-align: top;
                                                                    color: #FFFFFF;">
                                                                    &nbsp;</span></span></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="background-image: url(skins/Titulo_r2_c3.jpg);">
                                                </td>
                                                <td>
                                                    <img src="skins/spacer.gif" width="1" height="18" border="0" alt="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img name="Titulo_r3_c1" src="skins/Titulo_r3_c1.jpg" width="11" height="9" border="0"
                                                        id="Titulo_r3_c1" alt="" />
                                                </td>
                                                <td style="background-image: url(skins/Titulo_r3_c2.jpg); background-repeat: repeat-x">
                                                    <img name="Titulo_r3_c2" src="skins/Titulo_r3_c2.jpg" width="914" height="9" border="0"
                                                        id="Titulo_r3_c2" alt="" />
                                                </td>
                                                <td>
                                                    <img name="Titulo_r3_c3" src="skins/Titulo_r3_c3.jpg" width="13" height="9" border="0"
                                                        id="Titulo_r3_c3" alt="" />
                                                </td>
                                                <td>
                                                    <img src="skins/spacer.gif" width="1" height="9" border="0" alt="" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MenuHeight" colspan="2" style="text-align: left;" align="center">
                                        <table width="100%">
                                            <tr>
                                                <td style="vertical-align: baseline;">
                                                    <igtab:UltraWebTab ID="TabEmpleados" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                        BorderWidth="1px" AsyncMode="Off" EnableAppStyling="True" StyleSetName="Caribbean"
                                                        Height="450px" Width="981px">
                                                        <Tabs>
                                                            <igtab:Tab DefaultImage="./Imagenes/Iconos/MostrarPendientes16.png" ImageAlign="AbsMiddle"
                                                                Text="Tareas Pendientes">
                                                                <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_TareasPendientes">
                                                                </ContentPane>
                                                            </igtab:Tab>
                                                            <igtab:Tab Text="Asistencia" DefaultImage="./Imagenes/Iconos/MostrarAsistencias16.png"
                                                                ImageAlign="AbsMiddle">
                                                                <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_AsistenciasEmp">
                                                                </ContentPane>
                                                            </igtab:Tab>
                                                            <igtab:Tab Text="Horas Extras" DefaultImage="./Imagenes/Iconos/MostrarAsistencias16.png"
                                                                ImageAlign="AbsMiddle">
                                                                <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_HorasExtrasGrupo">
                                                                </ContentPane>
                                                            </igtab:Tab>
                                                            <igtab:Tab Text="Mis Datos" DefaultImage="./Imagenes/Iconos/Agrupacion16.png" ImageAlign="AbsMiddle"
                                                                Key="TabEmpleados_Empleados">
                                                                <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_EmpleadosN">
                                                                </ContentPane>
                                                            </igtab:Tab>
                                                        </Tabs>
                                                        <RoundedImage FillStyle="LeftMergedWithCenter" />
                                                        <BorderDetails ColorLeft="White" ColorTop="White" />
                                                        <DefaultTabStyle BackColor="PowderBlue">
                                                        </DefaultTabStyle>
                                                        <AsyncOptions EnableLoadOnDemand="True" RequestContext="IncludeFormData" ResponseContext="IncludeSelectedTab" />
                                                        <ClientSideEvents BeforeSelectedTabChange="TabEmpleados_BeforeSelectedTabChange" 
                                                            InitializeTabs="TabEmpleados_InitializeTabs" />
                                                    </igtab:UltraWebTab>
                                                    </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="MenuHeight" colspan="2">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td class="BoxR" style="width: 11px; background-repeat: repeat-y; background-color: white;">
                            <img height="11" src="skins/dummy.gif" width="11" style="background-color: white" />
                        </td>
                    </tr>
                    <tr>
                        <td class="BoxBL" style="width: 11px">
                            <img height="11" src="skins/boxed.gif" width="11" />
                        </td>
                        <td class="BoxB" style="background-repeat: repeat-x; background-color: white;">
                            <img height="11" src="skins/dummy.gif" width="11" />
                        </td>
                        <td class="BoxBR" style="width: 11px; background-repeat: repeat-y; background-color: white;">
                            <img height="11" src="skins/boxed.BR.gif" width="11" />
                        </td>
                    </tr>
                </table>
                &nbsp;&nbsp;
                <table>
                    <tr>
                        <td>
                            <span id="Span1" class="FOOTER_objects">Copyright © 2013, EntryTec </span>todos
                            los derechos reservados
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <span class="FooterArrow">
                                <asp:HyperLink ID="HyperLink4" runat="server" ForeColor="White" NavigateUrl="http://www.entrytec.com.mx/eClock">Acerca de</asp:HyperLink>|
                                <a id="dnn_dnnTERMS_hypTerms" class="FOOTER_objects">
                                    <asp:HyperLink ID="HyperLink5" runat="server" ForeColor="White" NavigateUrl="http://www.entrytec.com.mx/eClock/Soporte">Soporte</asp:HyperLink></a>
                            </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>