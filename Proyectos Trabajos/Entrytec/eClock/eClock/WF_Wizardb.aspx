<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master"  AutoEventWireup="true" CodeFile="WF_Wizardb.aspx.cs" Inherits="WF_Wizardb" EnableEventValidation = "false" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp; &nbsp;
    <br />
    <table>
        <tr>
            <td style="width: 100%; text-align: left">
        <igtxt:WebImageButton ID="BDeshacerCambios" runat="server" Height="22px" OnClick="BDeshacerCambios_Click"
            Text="Datos Por Default" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
            <Appearance>
               
                <Image Url="./Imagenes/Undo.png" Height="16px" Width="16px" />
            </Appearance>
        </igtxt:WebImageButton>
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <igtxt:WebImageButton ID="BGuardarCambios" runat="server" Height="22px" OnClick="BGuardarCambios_Click"
            Text="Siguiente" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
            <Appearance>
            
                <Image Url="./Imagenes/Next.png" Height="16px" Width="16px" />
            </Appearance>
        </igtxt:WebImageButton>
            </td>
        </tr>
    </table>
        <br />
        <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" Height="" Width="100%">
    <div style="text-align: left">
       <igmisc:WebPanel ID="WebPanel1" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="Tahoma" forecolor="Black"
                            stylesetname="PaneleClock" Font-Size="Small">
      <PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>
        <Header Text="Clave" TextAlignment="Left">
            <ExpandedAppearance>
                <Styles BorderColor="#002D96" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" BackColor="SteelBlue">
                    <Padding Bottom="4px" Left="4px" Right="4px" Top="4px" />
                </Styles>
            </ExpandedAppearance>
            <HoverAppearance>
                <Styles CssClass="igwpHeaderHoverBlack2k7">
                </Styles>
            </HoverAppearance>
            <CollapsedAppearance>
                <Styles CssClass="igwpHeaderCollapsedBlack2k7">
                </Styles>
            </CollapsedAppearance>
            <ExpansionIndicator Height="0px" Width="0px" />
        </Header>
        <Template>
            <table>
                <tr>
                    <td valign="middle">
                        <igmisc:WebGroupBox ID="WebGroupBox1" runat="server" BorderColor="Transparent" Width="500px">
                            <Template>
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left">
                                            <igtxt:WebTextEdit ID="TxtTRACVE" runat="server" Width="228px">
                                            </igtxt:WebTextEdit>
                                        </td>
                                        <td align="left">
                                            [<asp:Label ID="LTRACVEDato" runat="server" Text="PERSONA_LINK_ID" Width="139px"></asp:Label>]</td>
                                        <td align="left">
                                            <asp:Label ID="LTRACVETipo" runat="server" Width="161px"></asp:Label></td>
                                        <td align="left">
                                            <asp:Label ID="LTRACVE" runat="server" Text="No Empleado" Visible="False"></asp:Label></td>
                                    </tr>
                                </table>
                            </Template>
                        </igmisc:WebGroupBox>
                    </td>
                </tr>
            </table>
        </Template>
    </igmisc:WebPanel>
        <br />
        <igmisc:webasyncrefreshpanel id="Webasyncrefreshpanel2" runat="server" height=""
            width="600px">
            <igmisc:WebPanel ID="WebPanel6" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="Tahoma" forecolor="Black"
                            stylesetname="PaneleClock" Font-Size="Small">
      <PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>
                <Header Text="Nombre" TextAlignment="Left">
                    <ExpandedAppearance>
                        <Styles BorderColor="#002D96" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" BackColor="SteelBlue">
                            <Padding Bottom="4px" Left="4px" Right="4px" Top="4px" />
                        </Styles>
                    </ExpandedAppearance>
                    <HoverAppearance>
                        <Styles CssClass="igwpHeaderHoverBlack2k7">
                        </Styles>
                    </HoverAppearance>
                    <CollapsedAppearance>
                        <Styles CssClass="igwpHeaderCollapsedBlack2k7">
                        </Styles>
                    </CollapsedAppearance>
                    <ExpansionIndicator Height="0px" Width="0px" />
                </Header>
                <Template>
                    <table height="100">
                        <tr>
                            <td valign="top">
                                <asp:RadioButtonList ID="RBLNombre" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="RBLNombre_SelectedIndexChanged" Width="168px">
                                    <asp:ListItem>Nombre Completo</asp:ListItem>
                                    <asp:ListItem>Nombre Segmentado</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td valign="top" style="width: 736px">
                                <igmisc:WebGroupBox ID="WGBNombreCompleto" runat="server" Height="7px" BorderColor="Transparent" Width="0px">
                                    <Template>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 100px; height: 26px" align="left">
                                                    <igtxt:WebTextEdit ID="TxtNombreCompleto" runat="server" Width="228px">
                                                    </igtxt:WebTextEdit>
                                                </td>
                                                <td style="height: 26px" align="left">
                                                    [<asp:Label ID="LNombreCompletoDato" runat="server" Text="NOMBRE_COMPLETO" Width="139px"></asp:Label>]</td>
                                                <td style="width: 100px; height: 26px" align="left">
                                                    <asp:Label ID="LNombreCompletoTipo" runat="server" Width="161px"></asp:Label></td>
                                                <td style="height: 26px" align="left">
                                                    <asp:Label ID="LNombreCompleto" runat="server" Text="Nombre Completo" Width="166px" Visible="False"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </Template>
                                </igmisc:WebGroupBox>
                                <igmisc:WebGroupBox ID="WGBNombreSeparado" runat="server" BorderColor="Transparent">
                                    <Template>
                                        <table align="left">
                                            <tr>
                                                <td style="width: 100px">
                                                    <igtxt:WebTextEdit ID="TxtNombre" runat="server" Width="229px">
                                                    </igtxt:WebTextEdit>
                                                </td>
                                                <td>
                                                    [<asp:Label ID="LNombreDato" runat="server" Text="NOMBRE" Width="139px"></asp:Label>]</td>
                                                <td style="width: 100px">
                                                    <asp:Label ID="LNombreTipo" runat="server" Width="161px"></asp:Label></td>
                                                <td style="width: 200px">
                                                    <asp:Label ID="LNombre" runat="server" Text="Nombre(s)" Width="165px" Visible="False"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px">
                                                    <igtxt:WebTextEdit ID="TxtAPaterno" runat="server" Width="229px">
                                                    </igtxt:WebTextEdit>
                                                </td>
                                                <td style="width: 93px">
                                                    [<asp:Label ID="LAPaternoDato" runat="server" Text="APATERNO" Width="139px"></asp:Label>]</td>
                                                <td style="width: 100px">
                                                    <asp:Label ID="LAPaternoTipo" runat="server" Width="161px"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="LAPaterno" runat="server" Text="A. Paterno" Width="165px" Visible="False"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px">
                                                    <igtxt:WebTextEdit ID="TxtAMaterno" runat="server" Width="229px">
                                                    </igtxt:WebTextEdit>
                                                </td>
                                                <td>
                                                    [<asp:Label ID="LAMaternoDato" runat="server" Text="AMATERNO" Width="139px"></asp:Label>]</td>
                                                <td>
                                                    <asp:Label ID="LAMaternoTipo" runat="server" Width="161px"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="LAMaterno" runat="server" Text="A. Materno" Width="165px" Visible="False"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </Template>
                                </igmisc:WebGroupBox>
                            </td>
                        </tr>
                    </table>
                </Template>
            </igmisc:WebPanel>
        </igmisc:webasyncrefreshpanel>
    
    </div>
        <igmisc:webasyncrefreshpanel id="Webasyncrefreshpanel3" runat="server" Height="" Width="100%">
            <table style="width: 100%">
                <tr>
                    <td width="10" style="text-align: left" align="left">
            <asp:CheckBox ID="CBDatosPersonales" runat="server" AutoPostBack="True" OnCheckedChanged="CBDatosPersonales_CheckedChanged" OnLoad="CBDatosPersonales_CheckedChanged" Width="3px" /></td>
                    <td width="470" align="left">
           <igmisc:WebPanel ID="WPDatosPersonales" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="Tahoma" forecolor="Black"
                            stylesetname="PaneleClock" width="100%" Expanded="False" Font-Size="Small">
      <PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>
                            <Header Text="Datos Personales" TextAlignment="Left">
                                <ExpandedAppearance>
                                    <Styles CssClass="igwpHeaderExpandedBlack2k7" Width="680px" BackColor="SteelBlue" Font-Bold="True" ForeColor="White">
                                        <Padding Left="5px" />
                                    </Styles>
                                </ExpandedAppearance>
                                <HoverAppearance>
                                    <Styles CssClass="igwpHeaderHoverBlack2k7" Width="680px">
                                    </Styles>
                                </HoverAppearance>
                                <CollapsedAppearance>
                                    <Styles CssClass="igwpHeaderCollapsedBlack2k7" Width="680px">
                                    </Styles>
                                </CollapsedAppearance>
                            </Header>
             
                <Template>
                    <igmisc:WebGroupBox ID="WGBDatosPersonales" runat="server" Width="470px" BorderColor="Transparent" Height="280px">
                        <Template>
                            <table style="width: 470px" align="left">
                                <tr>
                                    <td style="text-align: left" align="left">
                                        <asp:CheckBox ID="CBFecNac" runat="server" AutoPostBack="false" type="checkbox" Width="45px" /></td>
                                    <td style="width: 200px;" align="left"><igtxt:WebTextEdit ID="TxtFechaNac" runat="server" Width="229px">
                                    </igtxt:WebTextEdit>
                                    </td>
                                    <td style="width: 150px; text-align: left;" nowrap="noWrap" align="left">
                                        [<asp:Label ID="LFechaNacDato" runat="server" Text="FECHA_NAC" Width="161px"></asp:Label>]</td>
                                    <td style="width: 150px">
                                        <asp:Label ID="LFecNacTipo" runat="server" Width="161px"></asp:Label></td>
                                    <td style="width: 200px;">
                                        <asp:Label ID="LFechaNac" runat="server" Text="Fecha De Nacimiento" Width="143px" Visible="False"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" align="left">
                                        <asp:CheckBox ID="CBRFC" runat="server" AutoPostBack="false" type="checkbox" Width="70px" /></td>
                                    <td style="width: 200px" align="left"><igtxt:WebTextEdit ID="TxtRFC" runat="server" Width="229px">
                                    </igtxt:WebTextEdit>
                                    </td>
                                    <td style="width: 150px" nowrap="noWrap" align="left">
                                        [<asp:Label ID="LRFCDato" runat="server" Text="RFC" Width="161px"></asp:Label>]</td>
                                    <td style="width: 150px">
                                        <asp:Label ID="LRFCTipo" runat="server" Width="161px"></asp:Label></td>
                                    <td style="width: 200px">
                                        <asp:Label ID="LRFC" runat="server" Text="R.F.C." Width="138px" Visible="False"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" align="left">
                                        <asp:CheckBox ID="CBCURP" runat="server" AutoPostBack="false" type="checkbox" Width="23px" /></td>
                                    <td style="width: 200px;" align="left">
                                        <igtxt:WebTextEdit ID="TxtCURP" runat="server" Width="229px">
                                        </igtxt:WebTextEdit>
                                    </td>
                                    <td style="width: 150px;" nowrap="noWrap" align="left">
                                        [<asp:Label ID="LCURPDato" runat="server" Text="CURP" Width="161px"></asp:Label>]</td>
                                    <td style="width: 150px">
                                        <asp:Label ID="LCURPTipo" runat="server" Width="161px"></asp:Label></td>
                                    <td style="width: 200px;">
                                        <asp:Label ID="LCURP" runat="server" Text="CURP" Width="138px" Visible="False"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" align="left">
                                        <asp:CheckBox ID="CBIMSS" runat="server" AutoPostBack="false" type="checkbox" Width="35px" /></td>
                                    <td style="width: 200px;" align="left">
                                        <igtxt:WebTextEdit ID="TxtIMSS" runat="server" Width="229px">
                                        </igtxt:WebTextEdit>
                                    </td>
                                    <td style="width: 150px;" nowrap="noWrap" align="left">
                                        [<asp:Label ID="LIMSSDato" runat="server" Text="IMSS" Width="161px"></asp:Label>]</td>
                                    <td style="width: 150px">
                                        <asp:Label ID="LIMSSTipo" runat="server" Width="161px"></asp:Label></td>
                                    <td style="width: 200px;">
                                        <asp:Label ID="LIMSS" runat="server" Text="IMSS" Width="138px" Visible="False"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" align="left">
                                        <asp:CheckBox ID="CBEstudios" runat="server" AutoPostBack="false" type="checkbox" Width="5px" /></td>
                                    <td style="width: 200px; text-align: left;" align="left">
                                        <igtxt:WebTextEdit ID="TxtEstudios" runat="server" Width="229px">
                                        </igtxt:WebTextEdit>
                                    </td>
                                    <td style="width: 150px;" nowrap="noWrap" align="left">
                                        [<asp:Label ID="LEstudiosDato" runat="server" Text="ESTUDIOS" Width="161px"></asp:Label>]</td>
                                    <td style="width: 150px">
                                        <asp:Label ID="LEstudiosTipo" runat="server" Width="161px"></asp:Label></td>
                                    <td style="width: 200px;">
                                        <asp:Label ID="LEstudios" runat="server" Text="Estudios" Width="138px" Visible="False"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" align="left">
                                        <asp:CheckBox ID="CBSexo" runat="server" AutoPostBack="false" type="checkbox" Width="33px" /></td>
                                    <td style="width: 200px; text-align: left;" align="left">
                                        <igtxt:WebTextEdit ID="TxtSexo" runat="server" Width="229px">
                                        </igtxt:WebTextEdit>
                                    </td>
                                    <td style="width: 150px;" nowrap="noWrap" align="left">
                                        [<asp:Label ID="LSexoDato" runat="server" Text="SEXO" Width="161px"></asp:Label>]</td>
                                    <td style="width: 150px">
                                        <asp:Label ID="LSexoTipo" runat="server" Width="161px"></asp:Label></td>
                                    <td style="width: 200px;">
                                        <asp:Label ID="LSexo" runat="server" Text="Sexo" Width="138px" Visible="False"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" align="left">
                                        <asp:CheckBox ID="CBNacionalidad" runat="server" AutoPostBack="false" type="checkbox" Width="1px" /></td>
                                    <td style="width: 200px; text-align: left;" align="left">
                                        <igtxt:WebTextEdit ID="TxtNacionalidad" runat="server" Width="229px">
                                        </igtxt:WebTextEdit>
                                    </td>
                                    <td style="width: 150px;" nowrap="noWrap" align="left">
                                        [<asp:Label ID="LNacionalidadDato" runat="server" Text="NACIONALIDAD" Width="161px"></asp:Label>]</td>
                                    <td style="width: 150px">
                                        <asp:Label ID="LNacionalidadTipo" runat="server" Width="161px"></asp:Label></td>
                                    <td style="width: 200px;">
                                        <asp:Label ID="LNacionalidad" runat="server" Text="Nacionalidad" Width="138px" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" align="left">
                                        <asp:CheckBox ID="CBFecIngreso" runat="server" AutoPostBack="false" type="checkbox" Width="29px" /></td>
                                    <td style="width: 200px; text-align: left;" align="left">
                                        <igtxt:WebTextEdit ID="TxtFecIngreso" runat="server" Width="229px">
                                        </igtxt:WebTextEdit>
                                    </td>
                                    <td style="width: 150px;" nowrap="noWrap" align="left">
                                        [<asp:Label ID="LFIngresoDato" runat="server" Text="FECHA_INGRESO" Width="161px"></asp:Label>]</td>
                                    <td style="width: 150px">
                                        <asp:Label ID="LFechaIngresoTipo" runat="server" Width="161px"></asp:Label></td>
                                    <td style="width: 200px;">
                                        <asp:Label ID="LFIngreso" runat="server" Text="Fecha de Ingreso" Width="138px" Visible="False"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="height: 26px; text-align: left;" align="left">
                                        <asp:CheckBox ID="CBFecBaja" runat="server" AutoPostBack="false" type="checkbox" Width="22px" /></td>
                                    <td style="width: 200px; text-align: left;" align="left">
                                        <igtxt:WebTextEdit ID="TxtFecBaja" runat="server" Width="229px">
                                        </igtxt:WebTextEdit>
                                    </td>
                                    <td style="width: 150px;" nowrap="noWrap" align="left">
                                        [<asp:Label ID="LFBajaDato" runat="server" Text="FECHA_BAJA" Width="161px"></asp:Label>]</td>
                                    <td style="width: 150px">
                                        <asp:Label ID="LFechaBajaTipo" runat="server" Width="161px"></asp:Label></td>
                                    <td style="width: 200px;">
                                        <asp:Label ID="LFBaja" runat="server" Text="Fecha de Baja" Width="138px" Visible="False"></asp:Label></td>
                                </tr>
                            </table>
                        </Template>
                    </igmisc:WebGroupBox>
                </Template>
            </igmisc:WebPanel>
                    </td>
                </tr>
                <tr>
                    <td width="1" align="left">
                        <asp:CheckBox ID="CBDirPersonal" runat="server" AutoPostBack="True"
                            OnCheckedChanged="CBDirPersonal_CheckedChanged" Width="5px" OnLoad="CBDirPersonal_CheckedChanged" /></td>
                    <td style="width: 470px;" align="left">
                      <igmisc:WebPanel ID="WPDirPer" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="Tahoma" forecolor="Black"
                            stylesetname="PaneleClock" width="100%" Expanded="False" Font-Size="Small">
      <PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>
                            <Header Text="Direccion Personal" TextAlignment="Left">
                                <ExpandedAppearance>
                                    <Styles CssClass="igwpHeaderExpandedBlack2k7" Width="680px" BackColor="SteelBlue" Font-Bold="True" ForeColor="White">
                                        <Padding Left="5px" />
                                    </Styles>
                                </ExpandedAppearance>
                                <HoverAppearance>
                                    <Styles CssClass="igwpHeaderHoverBlack2k7" Width="680px">
                                    </Styles>
                                </HoverAppearance>
                                <CollapsedAppearance>
                                    <Styles CssClass="igwpHeaderCollapsedBlack2k7" Width="680px">
                                    </Styles>
                                </CollapsedAppearance>
                            </Header>
                            <Template>
                                <table style="width: 470px">
                                    <tr>
                                        <td valign="top">
                                            <igmisc:WebGroupBox ID="WGBDirPer" runat="server" BackColor="Transparent" BorderColor="Transparent" Width="470px">
                                                <Template>
                                                    <table align="left" width="470">
                                                        <tr>
                                                            <td style="width: 22px;" align="left">
                                                                <asp:CheckBox ID="CBDPCalleyNum" runat="server" type="checkbox" Checked="True" /></td>
                                                            <td style="width: 200px;" align="left">
                                                                <igtxt:WebTextEdit ID="TxtDPCalleyNum" runat="server" Width="229px">
                                                                </igtxt:WebTextEdit>
                                                            </td>
                                                            <td style="width: 150px;" nowrap="noWrap" align="left">
                                                                [<asp:Label ID="LDPCalleyNumDato" runat="server" Text="DP_CALLE_NO" Width="181px"></asp:Label>]</td>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="LDPCalleyNumTipo" runat="server" Width="161px"></asp:Label></td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="LDPCalleyNum" runat="server" Text="Calle y Num" Width="121px" Visible="False"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 22px" align="left">
                                                                <asp:CheckBox ID="CBDPColonia" runat="server" AutoPostBack="false" type="checkbox" Checked="True" /></td>
                                                            <td style="width: 200px;" align="left">
                                                                <igtxt:WebTextEdit ID="TxtDPColonia" runat="server" Width="229px">
                                                                </igtxt:WebTextEdit>
                                                            </td>
                                                            <td style="width: 150px;" nowrap="noWrap" align="left">
                                                                [<asp:Label ID="LDPColoniaDato" runat="server" Text="DP_COLONIA" Width="181px"></asp:Label>]</td>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="LDPColoniaTipo" runat="server" Width="161px"></asp:Label></td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="LDPColonia" runat="server" Text="Colonia" Width="121px" Visible="False"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 22px;" align="left">
                                                                <asp:CheckBox ID="CBDPDelegacion" runat="server" AutoPostBack="false" type="checkbox" Checked="True" /></td>
                                                            <td style="width: 200px;" align="left">
                                                                <igtxt:WebTextEdit ID="TxtDPDelegacion" runat="server" Width="229px">
                                                                </igtxt:WebTextEdit>
                                                            </td>
                                                            <td style="width: 150px;" nowrap="noWrap" align="left">
                                                                [<asp:Label ID="LDPDelegacionDato" runat="server" Text="DP_DELEGACION" Width="181px"></asp:Label>]</td>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="LDPDelegacionTipo" runat="server" Width="161px"></asp:Label></td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="LDPDelegacion" runat="server" Text="Delegacion" Width="121px" Visible="False"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 22px;" align="left">
                                                                <asp:CheckBox ID="CBDPCiudad" runat="server" AutoPostBack="false" type="checkbox" Checked="True" /></td>
                                                            <td style="width: 200px;" align="left">
                                                                <igtxt:WebTextEdit ID="TxtDPCiudad" runat="server" Width="229px">
                                                                </igtxt:WebTextEdit>
                                                            </td>
                                                            <td style="width: 150px;" nowrap="noWrap" align="left">
                                                                [<asp:Label ID="LDPCiudadDato" runat="server" Text="DP_CIUDAD" Width="181px"></asp:Label>]</td>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="LDPCiudadTipo" runat="server" Width="161px"></asp:Label></td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="LDPCiudad" runat="server" Text="Ciudad" Width="121px" Visible="False"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 22px;" align="left">
                                                                <asp:CheckBox ID="CBDPEstado" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                            <td style="width: 200px;" align="left">
                                                                <igtxt:WebTextEdit ID="TxtDPEstado" runat="server" Width="229px">
                                                                </igtxt:WebTextEdit>
                                                            </td>
                                                            <td style="width: 150px;" nowrap="noWrap" align="left">
                                                                [<asp:Label ID="LDPEstadoDato" runat="server" Text="DP_ESTADO" Width="181px"></asp:Label>]</td>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="LDPEstadoTipo" runat="server" Width="161px"></asp:Label></td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="LDPEstado" runat="server" Text="Estado" Width="121px" Visible="False"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 22px;" align="left">
                                                                <asp:CheckBox ID="CBDPPais" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                            <td style="width: 200px;" align="left">
                                                                <igtxt:WebTextEdit ID="TxtDPPais" runat="server" Width="229px">
                                                                </igtxt:WebTextEdit>
                                                            </td>
                                                            <td style="width: 150px;" nowrap="noWrap" align="left">
                                                                [<asp:Label ID="LDPPaisDato" runat="server" Text="DP_PAIS" Width="181px"></asp:Label>]</td>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="LDPPaisTipo" runat="server" Width="161px"></asp:Label></td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="LDPPais" runat="server" Text="Pais" Width="121px" Visible="False"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 22px;" align="left">
                                                                <asp:CheckBox ID="CBDPCP" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                            <td style="width: 200px;" align="left">
                                                                <igtxt:WebTextEdit ID="TxtDPCP" runat="server" Width="229px">
                                                                </igtxt:WebTextEdit>
                                                            </td>
                                                            <td style="width: 150px;" nowrap="noWrap" align="left">
                                                                [<asp:Label ID="LDPCPDato" runat="server" Text="DP_CP" Width="181px"></asp:Label>]</td>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="LDPCPTipo" runat="server" Width="161px"></asp:Label></td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="LDPCP" runat="server" Text="CP" Width="121px" Visible="False"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </Template>
                                            </igmisc:WebGroupBox>
                                        </td>
                                    </tr>
                                </table>
                            </Template>
                        </igmisc:WebPanel>
                    </td>
                </tr>
                <tr>
                    <td width="1" align="left">
            <asp:CheckBox ID="CBTelPersonal" runat="server" AutoPostBack="True"
                            OnCheckedChanged="CBTelPersonal_CheckedChanged" Width="86px" OnLoad="CBTelPersonal_CheckedChanged" /></td>
                    <td style="width: 470px" align="left">
            <igmisc:WebPanel ID="WPTelPer" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="Tahoma" forecolor="Black"
                            stylesetname="PaneleClock" width="100%" Expanded="False" Font-Size="Small">
      <PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>
                            <Header Text="Telefono Personal" TextAlignment="Left">
                                <ExpandedAppearance>
                                    <Styles CssClass="igwpHeaderExpandedBlack2k7" BackColor="SteelBlue" Font-Bold="True" ForeColor="White">
                                        <Padding Left="5px" />
                                    </Styles>
                                </ExpandedAppearance>
                                <HoverAppearance>
                                    <Styles CssClass="igwpHeaderHoverBlack2k7">
                                    </Styles>
                                </HoverAppearance>
                                <CollapsedAppearance>
                                    <Styles CssClass="igwpHeaderCollapsedBlack2k7">
                                    </Styles>
                                </CollapsedAppearance>
                            </Header>
                            <Template>
                                <table style="width: 470px" >
                                    <tr>
                                        <td  valign="top" align="left">
                                            <igmisc:WebGroupBox ID="WGBTelPer" runat="server" BorderColor="Transparent" Width="470px">
                                                <Template>
                                                    <table width="470" >
                                                        <tr>
                                                            <td style="height: 26px" align="left">
                                                                <asp:CheckBox ID="CBDPTel1" runat="server" AutoPostBack="false" type="checkbox" Checked="True" /></td>
                                                            <td style="width: 200px;">
                                                                <igtxt:WebTextEdit ID="TxtDPTel1" runat="server" Width="229px">
                                                                </igtxt:WebTextEdit>
                                                            </td>
                                                            <td style="width: 150px;" nowrap="noWrap">
                                                                [<asp:Label ID="LDPTel1Dato" runat="server" Text="DP_TELEFONO1" Width="163px"></asp:Label>]</td>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="LDPTel1Tipo" runat="server" Width="161px"></asp:Label></td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="LDPTel1" runat="server" Text="Telefono1" Width="109px" Visible="False"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 26px; width: 22px;" align="left">
                                                                <asp:CheckBox ID="CBDPTel2" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                            <td style="width: 200px;">
                                                                <igtxt:WebTextEdit ID="TxtDPTel2" runat="server" Width="229px">
                                                                </igtxt:WebTextEdit>
                                                            </td>
                                                            <td style="width: 150px;" nowrap="noWrap">
                                                                [<asp:Label ID="LDPTel2Dato" runat="server" Text="DP_TELEFONO2" Width="163px"></asp:Label>]</td>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="LDPTel2Tipo" runat="server" Width="161px"></asp:Label></td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="LDPTel2" runat="server" Text="Telefono2" Width="109px" Visible="False"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 26px" align="left">
                                                                <asp:CheckBox ID="CBDPCel1" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                            <td style="width: 200px;">
                                                                <igtxt:WebTextEdit ID="TxtDPCel1" runat="server" Width="229px">
                                                                </igtxt:WebTextEdit>
                                                            </td>
                                                            <td style="width: 150px;" nowrap="noWrap">
                                                                [<asp:Label ID="LDPCel1Dato" runat="server" Text="DP_CELULAR1" Width="163px"></asp:Label>]</td>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="LDPCel1Tipo" runat="server" Width="161px"></asp:Label></td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="LDPCel1" runat="server" Text="Celular1" Width="109px" Visible="False"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 26px" align="left">
                                                                <asp:CheckBox ID="CBDPCel2" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                            <td style="width: 200px;">
                                                                <igtxt:WebTextEdit ID="TxtDPCel2" runat="server" Width="229px">
                                                                </igtxt:WebTextEdit>
                                                            </td>
                                                            <td style="width: 150px;" nowrap="noWrap">
                                                                [<asp:Label ID="LDPCel2Dato" runat="server" Text="DP_CELULAR2" Width="163px"></asp:Label>]</td>
                                                            <td style="width: 150px">
                                                                <asp:Label ID="LDPCel2Tipo" runat="server" Width="161px"></asp:Label></td>
                                                            <td style="width: 200px;">
                                                                <asp:Label ID="LDPCel2" runat="server" Text="Celular2" Width="109px" Visible="False"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </Template>
                                            </igmisc:WebGroupBox>
                                        </td>
                                    </tr>
                                </table>
                            </Template>
                        </igmisc:WebPanel>
                    </td>
                </tr>
                <tr>
                    <td width="1" align="left">
            <asp:CheckBox ID="CBDirLaboral" runat="server" AutoPostBack="True"
                OnCheckedChanged="CBDirLaboral_CheckedChanged" Width="1px" OnLoad="CBDirLaboral_CheckedChanged" /></td>
                    <td style="width: 470px" align="left">
                        <igmisc:WebPanel ID="WPDirLab" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="Tahoma" forecolor="Black"
                            stylesetname="PaneleClock" width="100%" Expanded="False" Font-Size="Small">
      <PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>
                    
                    <Header Text="Direccion Laboral" TextAlignment="Left">
                        <ExpandedAppearance>
                            <Styles CssClass="igwpHeaderExpandedBlack2k7" BackColor="SteelBlue" Font-Bold="True" ForeColor="White">
                                <Padding Left="5px" />
                            </Styles>
                        </ExpandedAppearance>
                        <HoverAppearance>
                            <Styles CssClass="igwpHeaderHoverBlack2k7">
                            </Styles>
                        </HoverAppearance>
                        <CollapsedAppearance>
                            <Styles CssClass="igwpHeaderCollapsedBlack2k7">
                            </Styles>
                        </CollapsedAppearance>
                    </Header>
                    <Template>
                        <table style="width: 470px">
                            <tr>
                                <td style="width: 655px; height: 162px" valign="top" align="left">
                                    <igmisc:WebGroupBox ID="WGBDirLab" runat="server" BackColor="Transparent" BorderColor="Transparent" Width="470px">
                                        <Template>
                                            <table style="width: 470px" align="left">
                                                <tr>
                                                    <td style="height: 26px">
                                                        <asp:CheckBox ID="CBDTCalleyNum" runat="server" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDTCalleyNum" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDTCalleyNumDato" runat="server" Text="DT_CALLE_NO" Width="183px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDTCalleyNumTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDTCalleyNum" runat="server" Text="Calle y Num" Width="119px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 22px; height: 26px">
                                                        <asp:CheckBox ID="CBDTColonia" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px">
                                                        <igtxt:WebTextEdit ID="TxtDTColonia" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px" nowrap="noWrap">
                                                        [<asp:Label ID="LDTColoniaDato" runat="server" Text="DT_COLONIA" Width="183px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDTColoniaTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px">
                                                        <asp:Label ID="LDTColonia" runat="server" Text="Colonia" Width="119px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px">
                                                        <asp:CheckBox ID="CBDTDelegacion" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDTDelegacion" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDTDelegacionDato" runat="server" Text="DT_DELEGACION" Width="183px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDTDelegacionTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDTDelegacion" runat="server" Text="Delegacion" Width="119px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px">
                                                        <asp:CheckBox ID="CBDTCiudad" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDTCiudad" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDTCiudadDato" runat="server" Text="DT_CIUDAD" Width="183px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDTCiudadTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDTCiudad" runat="server" Text="Ciudad" Width="119px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px">
                                                        <asp:CheckBox ID="CBDTEstado" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDTEstado" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDTEstadoDato" runat="server" Text="DT_ESTADO" Width="183px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDTEstadoTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDTEstado" runat="server" Text="Estado" Width="119px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px">
                                                        <asp:CheckBox ID="CBDTPais" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDTPais" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDTPaisDato" runat="server" Text="DT_PAIS" Width="183px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDTPaisTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDTPais" runat="server" Text="Pais" Width="119px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px">
                                                        <asp:CheckBox ID="CBDTCP" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDTCP" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDTCPDato" runat="server" Text="DT_CP" Width="183px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDTCPTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDTCP" runat="server" Text="CP" Width="119px" Visible="False"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </Template>
                                    </igmisc:WebGroupBox>
                                </td>
                            </tr>
                        </table>
                    </Template>
                </igmisc:WebPanel>
                    </td>
                </tr>
                <tr>
                    <td width="1" align="left">
            <asp:CheckBox ID="CBTelLaboral" runat="server" AutoPostBack="True"
                OnCheckedChanged="CBTelLaboral_CheckedChanged" Width="86px" OnLoad="CBTelLaboral_CheckedChanged" /></td>
                    <td style="width: 470px" align="left">
                      <igmisc:WebPanel ID="WPTelLab" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="Tahoma" forecolor="Black"
                            stylesetname="PaneleClock" width="100%" Expanded="False" Font-Size="Small">
      <PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>
                    <Header Text="Telefono Laboral" TextAlignment="Left">
                        <ExpandedAppearance>
                            <Styles CssClass="igwpHeaderExpandedBlack2k7" BackColor="SteelBlue" Font-Bold="True" ForeColor="White">
                                <Padding Left="5px" />
                            </Styles>
                        </ExpandedAppearance>
                        <HoverAppearance>
                            <Styles CssClass="igwpHeaderHoverBlack2k7">
                            </Styles>
                        </HoverAppearance>
                        <CollapsedAppearance>
                            <Styles CssClass="igwpHeaderCollapsedBlack2k7">
                            </Styles>
                        </CollapsedAppearance>
                    </Header>
                    <Template>
                        <table style="width: 470px">
                            <tr>
                                <td valign="top" align="left">
                                    <igmisc:WebGroupBox ID="WGBTelLab" runat="server" BorderColor="Transparent" Width="470px">
                                        <Template>
                                            <table style="width: 470px">
                                                <tr>
                                                    <td style="height: 26px">
                                                        <asp:CheckBox ID="CBDTTel1" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDTTel1" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDTTel1Dato" runat="server" Text="DT_TELEFONO1" Width="203px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDTTel1Tipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDTTel1" runat="server" Text="Telefono1" Width="105px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px">
                                                        <asp:CheckBox ID="CBDTTel2" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDTTel2" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDTTel2Dato" runat="server" Text="DT_TELEFONO2" Width="203px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDTTel2Tipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDTTel2" runat="server" Text="Telefono2" Width="105px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px">
                                                        <asp:CheckBox ID="CBDTCel1" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDTCel1" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDTCel1Dato" runat="server" Text="DT_CELULAR1" Width="203px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDTCel1Tipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDTCel1" runat="server" Text="Celular1" Width="105px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px">
                                                        <asp:CheckBox ID="CBDTCel2" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDTCel2" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDTCel2Dato" runat="server" Text="DT_CELULAR2" Width="203px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDTCel2Tipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDTCel2" runat="server" Text="Celular2" Width="105px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                    </td>
                                                    <td style="width: 200px;">
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                    </td>
                                                    <td style="width: 150px">
                                                    </td>
                                                    <td style="width: 200px;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </Template>
                                    </igmisc:WebGroupBox>
                                </td>
                            </tr>
                        </table>
                    </Template>
                </igmisc:WebPanel>
                    </td>
                </tr>
                <tr>
                    <td width="1" align="left">
            <asp:CheckBox ID="CBDatosEmp" runat="server" AutoPostBack="True"
                OnCheckedChanged="CBDatosEmp_CheckedChanged" Width="75px" OnLoad="CBDatosEmp_CheckedChanged" /></td>
                    <td style="width: 470px" align="left">
                      <igmisc:WebPanel ID="WPDatosEmp" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="Tahoma" forecolor="Black"
                            stylesetname="PaneleClock" width="100%" Font-Size="Small" Expanded="False">
      <PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>
                    <Header Text="Datos Empresariales" TextAlignment="Left">
                        <ExpandedAppearance>
                            <Styles CssClass="igwpHeaderExpandedBlack2k7" BackColor="SteelBlue" Font-Bold="True" ForeColor="White">
                                <Padding Left="5px" />
                            </Styles>
                        </ExpandedAppearance>
                        <HoverAppearance>
                            <Styles CssClass="igwpHeaderHoverBlack2k7">
                            </Styles>
                        </HoverAppearance>
                        <CollapsedAppearance>
                            <Styles CssClass="igwpHeaderCollapsedBlack2k7">
                            </Styles>
                        </CollapsedAppearance>
                    </Header>
                    <Template>
                        <table style="width: 470px">
                            <tr>
                                <td style="width: 655px; height: 177px" valign="top" align="left">
                                    <igmisc:WebGroupBox ID="WGBDatosEmp" runat="server" BorderColor="Transparent" Width="470px">
                                        <Template>
                                            <table style="width: 470px" align="left">
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                        <asp:CheckBox ID="CBCentroCostos" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtCentroCostos" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LCentroCostosDato" runat="server" Text="CENTRO_DE_COSTOS" Width="203px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LCentroCostosTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LCentroCostos" runat="server" Text="Centro de Costos" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                        <asp:CheckBox ID="CBArea" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtArea" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LAreaDato" runat="server" Text="AREA" Width="203px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LAreaTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LArea" runat="server" Text="Area" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;"><asp:CheckBox ID="CBDepartamento" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDepartamento" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDepartamentoDato" runat="server" Text="DEPARTAMENTO" Width="203px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDepartamentoTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDepartamento" runat="server" Text="Departamento" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                        <asp:CheckBox ID="CBPuesto" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtPuesto" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LPuestoDato" runat="server" Text="PUESTO" Width="203px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LPuestoTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LPuesto" runat="server" Text="Puesto" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                        <asp:CheckBox ID="CBGrupo" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtGrupo" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LGrupoDato" runat="server" Text="GRUPO" Width="204px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LGrupoTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LGrupo" runat="server" Text="Grupo" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 22px; height: 26px">
                                                        <asp:CheckBox ID="CBNoCredencial" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtNoCredencial" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LNoCredencialDato" runat="server" Text="NO_CREDENCIAL" Width="204px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LNoCredencialTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LNoCredencial" runat="server" Text="No. de Credencial" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                        <asp:CheckBox ID="CBLineaProduccion" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtLineaProduccion" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LLineaProduccionDato" runat="server" Text="LINEA_PRODUCCION" Width="204px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LLineaProduccionTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LLineaProduccion" runat="server" Text="Linea de Produccion" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                        <asp:CheckBox ID="CBClaveEmp" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtClaveEmpleado" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LClaveEmpDato" runat="server" Text="CLAVE_EMPL" Width="204px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LClaveEmplTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LClaveEmp" runat="server" Text="Clave de Empleado" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                        <asp:CheckBox ID="CBCompania" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtCompania" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LCompaniaDato" runat="server" Text="COMPANIA" Width="204px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LCompaniaTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LCompania" runat="server" Text="Compañia" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                        <asp:CheckBox ID="CBRegion" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtRegion" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LRegionDato" runat="server" Text="REGION" Width="204px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LRegionTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LRegion" runat="server" Text="Region" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                        <asp:CheckBox ID="CBDivision" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtDivision" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LDivisionDato" runat="server" Text="DIVISION" Width="204px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LDivisionTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LDivision" runat="server" Text="Division" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                        <asp:CheckBox ID="CBTipoNomina" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtTipoNomina" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LTipoNominaDato" runat="server" Text="TIPO_NOMINA" Width="204px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LTipoNominaTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LTipoNomina" runat="server" Text="Tipo de Nomina" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 26px; width: 22px;">
                                                        <asp:CheckBox ID="CBZona" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                    <td style="width: 200px;">
                                                        <igtxt:WebTextEdit ID="TxtZona" runat="server" Width="229px">
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td style="width: 150px;" nowrap="noWrap">
                                                        [<asp:Label ID="LZonaDato" runat="server" Text="ZONA" Width="204px"></asp:Label>]</td>
                                                    <td style="width: 150px">
                                                        <asp:Label ID="LZonaTipo" runat="server" Width="161px"></asp:Label></td>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="LZona" runat="server" Text="Zona" Width="204px" Visible="False"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </Template>
                                    </igmisc:WebGroupBox>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </Template>
                </igmisc:WebPanel>
                    </td>
                </tr>
                <tr>
                    <td width="1" align="left">
            <asp:CheckBox ID="CBCamposPersonalizados" runat="server" AutoPostBack="True"
                OnCheckedChanged="CBCamposPersonalizados_CheckedChanged" OnLoad="CBCamposPersonalizados_CheckedChanged" Width="1px" /></td>
                    <td style="width: 470px;" align="left">
                    <igmisc:WebPanel ID="WPCamposPersonalizados" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="Tahoma" forecolor="Black"
                            stylesetname="PaneleClock" width="100%" Expanded="False" Font-Size="Small">
      <PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>
                <Header Text="Campos Personalizados" TextAlignment="Left">
                    <ExpandedAppearance>
                        <Styles CssClass="igwpHeaderExpandedBlack2k7" BackColor="SteelBlue" Font-Bold="True" ForeColor="White">
                            <Padding Left="5px" />
                        </Styles>
                    </ExpandedAppearance>
                    <HoverAppearance>
                        <Styles CssClass="igwpHeaderHoverBlack2k7">
                        </Styles>
                    </HoverAppearance>
                    <CollapsedAppearance>
                        <Styles CssClass="igwpHeaderCollapsedBlack2k7">
                        </Styles>
                    </CollapsedAppearance>
                </Header>
                <Template>
                    <table style="width: 470px">
                        <tr>
                            <td style="width: 602px; height: 177px" valign="top" align="left">
                                <igmisc:WebGroupBox ID="WGBCamposPersonalizados" runat="server" BackColor="Transparent" BorderColor="Transparent" Width="470px">
                                    <Template>
                                        <table style="width: 470px" align="left">
                                            <tr>
                                                <td style="height: 26px">
                                                    <asp:CheckBox ID="CBCampo1" runat="server" type="checkbox" /></td>
                                                <td style="height: 26px" width="200">
                                                    <igtxt:WebTextEdit ID="TxtCampo1" runat="server" Width="229px">
                                                    </igtxt:WebTextEdit>
                                                </td>
                                                <td style="height: 26px" width="150" nowrap="noWrap">
                                                    [<asp:Label ID="LCampo1Dato" runat="server" Text="CAMPO1" Width="113px"></asp:Label>]</td>
                                                <td style="height: 26px" width="150">
                                                    <asp:Label ID="LCampo1Tipo" runat="server" Width="161px"></asp:Label></td>
                                                <td style="height: 26px; width: 150px;">
                                                    <asp:Label ID="LCampo1" runat="server" Text="Campo 1" Width="98px" Visible="False"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CBCampo2" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                <td width="200">
                                                    <igtxt:WebTextEdit ID="TxtCampo2" runat="server" Width="229px">
                                                    </igtxt:WebTextEdit>
                                                </td>
                                                <td width="150" nowrap="noWrap">
                                                    [<asp:Label ID="LCampo2Dato" runat="server" Text="CAMPO2" Width="113px"></asp:Label>]</td>
                                                <td width="150">
                                                    <asp:Label ID="LCampo2Tipo" runat="server" Width="161px"></asp:Label></td>
                                                <td style="width: 150px">
                                                    <asp:Label ID="LCampo2" runat="server" Text="Campo 2" Width="98px" Visible="False"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 26px">
                                                    <asp:CheckBox ID="CBCampo3" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                <td style="height: 26px" width="200">
                                                    <igtxt:WebTextEdit ID="TxtCampo3" runat="server" Width="229px">
                                                    </igtxt:WebTextEdit>
                                                </td>
                                                <td style="height: 26px" width="150" nowrap="noWrap">
                                                    [<asp:Label ID="LCampo3Dato" runat="server" Text="CAMPO3" Width="113px"></asp:Label>]</td>
                                                <td style="height: 26px" width="150">
                                                    <asp:Label ID="LCampo3Tipo" runat="server" Width="161px"></asp:Label></td>
                                                <td style="height: 26px; width: 150px;">
                                                    <asp:Label ID="LCampo3" runat="server" Text="Campo 3" Width="98px" Visible="False"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 26px">
                                                    <asp:CheckBox ID="CBCampo4" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                <td style="height: 26px" width="200">
                                                    <igtxt:WebTextEdit ID="TxtCampo4" runat="server" Width="229px">
                                                    </igtxt:WebTextEdit>
                                                </td>
                                                <td style="height: 26px" width="150" nowrap="noWrap">
                                                    [<asp:Label ID="LCampo4Dato" runat="server" Text="CAMPO4" Width="113px"></asp:Label>]</td>
                                                <td style="height: 26px" width="150">
                                                    <asp:Label ID="LCampo4Tipo" runat="server" Width="161px"></asp:Label></td>
                                                <td style="height: 26px; width: 150px;">
                                                    <asp:Label ID="LCampo4" runat="server" Text="Campo 4" Width="98px" Visible="False"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 26px; width: 22px;">
                                                    <asp:CheckBox ID="CBCampo5" runat="server" AutoPostBack="false" type="checkbox" /></td>
                                                <td style="height: 26px" width="200">
                                                    <igtxt:WebTextEdit ID="TxtCampo5" runat="server" Width="229px">
                                                    </igtxt:WebTextEdit>
                                                </td>
                                                <td style="height: 26px" width="150" nowrap="noWrap">
                                                    [<asp:Label ID="LCampo5Dato" runat="server" Text="CAMPO5" Width="113px"></asp:Label>]</td>
                                                <td style="height: 26px" width="150">
                                                    <asp:Label ID="LCampo5Tipo" runat="server" Width="161px"></asp:Label></td>
                                                <td style="height: 26px; width: 150px;">
                                                    <asp:Label ID="LCampo5" runat="server" Text="Campo 5" Width="98px" Visible="False"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </Template>
                                </igmisc:WebGroupBox>
                            </td>
                        </tr>
                    </table>
                </Template>
            </igmisc:WebPanel>
                    </td>
                </tr>
            </table>
            <br />
            <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel4" runat="server" Width="820px">
            <asp:Label ID="LError" runat="server" Font-Names="Arial" Font-Size="Small" ForeColor="Red" Width="817px" Height="21px"></asp:Label><br />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; 
            </igmisc:WebAsyncRefreshPanel>
        </igmisc:webasyncrefreshpanel>
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
        <br />
        <br />
        </igmisc:WebAsyncRefreshPanel>
    
</asp:Content>