<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Page language="c#"  MasterPageFile="~/MasterPage.master"  CodeFile="WF_DiaPersona.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_DiaPersona" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table2" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="1" cellPadding="1" align="center"
				border="0">
				<TR>
					<TD height="1" style="width: 933px"></TD>
				</TR>
				<TR>
					<TD style="width: 933px">
                        <igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                            stylesetname="PaneleClock">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Buscar Empleado">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
<BorderDetails ColorTop="158, 190, 245" WidthBottom="0px" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="1px" Left="4px" Bottom="1px"></Padding>
</Styles>
</ExpandedAppearance>

<HoverAppearance>
<Styles CssClass="igwpHeaderHoverBlue2k7"></Styles>
</HoverAppearance>

<CollapsedAppearance>
<Styles CssClass="igwpHeaderCollapsedBlue2k7"></Styles>
</CollapsedAppearance>

<ExpansionIndicator Height="0px" Width="0px"></ExpansionIndicator>
</Header>
<Template>
<BR /><TABLE style="WIDTH: 97%" id="Table5" height="100%" cellSpacing=1 cellPadding=1 border=0><TBODY><TR><TD style="WIDTH: 143px" align=left>No. de Empleado</TD><TD style="WIDTH: 168px" align=left><igtxt:webnumericedit id="TBTracve" runat="server" BorderColor="#7B9EBD" Font-Names="Tahoma" BorderWidth="1px" BorderStyle="Solid" Width="152px" Font-Size="8pt" NullText="0" MinValue="0" MaxValue="99999" DESIGNTIMEDRAGDROP="36"></igtxt:webnumericedit></TD><TD align=left><P style="TEXT-ALIGN: left" align=right>&nbsp;&nbsp;<igtxt:WebImageButton id="BBuscarEmpleado" onclick="BBuscarEmpleado_Click" runat="server" Text="Buscar Empleado" Width="133px" UseBrowserDefaults="False" Height="6px" ImageTextSpacing="4">
<RoundedCorners HoverImageUrl="ig_butXP2wh.gif" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
    <Alignments HorizontalAll="NotSet" VerticalAll="NotSet" VerticalImage="Bottom" />
    <Appearance>
        <Image Height="15px" Url="./Imagenes/Empleado.png" Width="18px" />
    </Appearance>
</igtxt:WebImageButton> </P></TD></TR><TR><TD style="WIDTH: 143px" align=left>Fecha de Consulta</TD><TD style="WIDTH: 168px" align=left><igsch:webdatechooser id="FechaInicial" runat="server" Text="Null" Width="158px" OnValueChanged="FechaInicial_ValueChanged">
										<AutoPostBack ValueChanged="True"></AutoPostBack>
										<CalendarLayout DayNameFormat="FirstLetter" ShowYearDropDown="False"
											PrevMonthImageUrl="igsch_left_arrow.gif" ShowMonthDropDown="False" NextMonthImageUrl="igsch_right_arrow.gif"
											ShowFooter="False">
											<SelectedDayStyle BorderWidth="2px" BorderColor="#BB5503" BorderStyle="Solid" ForeColor="Black" BackColor="Transparent"></SelectedDayStyle>
											<OtherMonthDayStyle ForeColor="#ACA899"></OtherMonthDayStyle>
											<CalendarStyle BorderColor="#7F9DB9" BorderStyle="Solid" BackColor="White"></CalendarStyle>
											<TodayDayStyle BackColor="#FBE694"></TodayDayStyle>
											<DayHeaderStyle>
												<BorderDetails StyleBottom="Solid" ColorBottom="172, 168, 153" WidthBottom="1px"></BorderDetails>
											</DayHeaderStyle>
											<TitleStyle BackColor="#9EBEF5"></TitleStyle>
										</CalendarLayout>
									</igsch:webdatechooser></TD><TD align=left></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
                        &nbsp;
					</TD>
				</TR>
				<TR>
					<TD style="width: 933px">
						<P align="center">
                            <igmisc:webpanel id="Webpanel1" runat="server" backcolor="White" bordercolor="SteelBlue"
                                borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                                stylesetname="PaneleClock">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Datos del Empleado">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
<BorderDetails ColorTop="158, 190, 245" WidthBottom="0px" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="1px" Left="4px" Bottom="1px"></Padding>
</Styles>
</ExpandedAppearance>

<HoverAppearance>
<Styles CssClass="igwpHeaderHoverBlue2k7"></Styles>
</HoverAppearance>

<CollapsedAppearance>
<Styles CssClass="igwpHeaderCollapsedBlue2k7"></Styles>
</CollapsedAppearance>

<ExpansionIndicator Height="0px" Width="0px"></ExpansionIndicator>
</Header>
<Template>
<TABLE style="WIDTH: 706px; HEIGHT: 443px" id="Table3" cellSpacing=1 cellPadding=1 width=706 border=0><TBODY><TR><TD><TABLE style="WIDTH: 456px; FONT-FAMILY: Arial; HEIGHT: 420px" id="Table4" cellSpacing=1 cellPadding=1 width=456 border=1 frame=below><TBODY><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 5px" align=left>No. Empleado</TD><TD align=left><asp:label id="LTracve" runat="server" Width="100%"></asp:label></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 5px" align=left>Nombre</TD><TD style="WIDTH: 210px; HEIGHT: 5px" align=left><FONT face="Arial"><asp:label id="LNombre" runat="server" Width="100%"></asp:label></FONT></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 12px" align=left>Area</TD><TD style="WIDTH: 210px; HEIGHT: 12px" align=left><FONT face="Arial"><asp:label id="LArea" runat="server" Width="100%"></asp:label></FONT></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 6px" align=left>Departamento</TD><TD style="WIDTH: 210px; HEIGHT: 6px" align=left><FONT face="Arial"><asp:label id="LDepto" runat="server" Width="100%"></asp:label></FONT></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 18px" align=left>Centro de Costos</TD><TD style="WIDTH: 210px; HEIGHT: 18px" align=left><FONT face="Arial"><asp:label id="LCC" runat="server" Width="100%"></asp:label></FONT></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left>Horario</TD><TD style="WIDTH: 210px; HEIGHT: 19px" align=left><FONT face="Arial"><asp:label id="LHorario" runat="server" Width="100%"></asp:label></FONT></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left>Hora de retardo</TD><TD style="WIDTH: 210px; HEIGHT: 19px" align=left><asp:label id="LHorarioRet" runat="server" Width="100%"></asp:label></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left><asp:Label id="LTipoTurno_Dex" runat="server">Tipo Turno</asp:Label></TD><TD style="FONT-SIZE: 11pt; WIDTH: 123px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left><P><asp:label id="LTipoturno" runat="server" Width="210px" Font-Size="Small"></asp:label> <asp:label id="LTipoTurno2" runat="server" Width="210px" Font-Size="Small"></asp:label></P></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left></TD><TD style="WIDTH: 210px; HEIGHT: 19px" align=left></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left>Entrada</TD><TD style="WIDTH: 210px; HEIGHT: 19px" align=left><asp:label id="LEntrada" runat="server" Width="100%"></asp:label></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left>Salida</TD><TD style="WIDTH: 210px; HEIGHT: 19px" align=left><asp:label id="LSalida" runat="server" Width="100%"></asp:label></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left><asp:Label id="LFlexTime_des" runat="server" Visible="False">Salida Horario Flexible</asp:Label></TD><TD style="WIDTH: 210px; HEIGHT: 19px" align=left><asp:label id="LFlexTime" runat="server" Width="100%" Visible="False"></asp:label></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left></TD><TD style="WIDTH: 210px; HEIGHT: 19px" align=left></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left>Incidencia Sistema</TD><TD style="WIDTH: 210px; HEIGHT: 19px" align=left><asp:label id="LIncidencia" runat="server" Width="100%"></asp:label></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left>Justificación</TD><TD style="WIDTH: 210px; HEIGHT: 19px" align=left><asp:label id="LJustifica" runat="server" Width="100%"></asp:label></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left>Motivo</TD><TD style="WIDTH: 210px; HEIGHT: 19px" align=left><asp:label id="LMotivo" runat="server" Width="100%"></asp:label></TD></TR><TR><TD style="FONT-SIZE: 11pt; WIDTH: 180px; FONT-FAMILY: Arial; HEIGHT: 19px" align=left>Por</TD><TD style="WIDTH: 210px; HEIGHT: 19px" align=left><asp:label id="LPor" runat="server" Width="100%"></asp:label></TD></TR><TR><TD style="WIDTH: 180px" align=left><FONT face="Arial"></FONT></TD><TD style="WIDTH: 210px" align=left><FONT face="Arial"></FONT></TD></TR></TBODY></TABLE></TD><TD><P><asp:image id="Foto" runat="server" ImageUrl="WF_Personas_ImaS.aspx" Width="259px" Height="277px"></asp:image></P><P align=center>&nbsp;</P></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
                            &nbsp;</P>
					</TD>
				</TR>
			</TABLE>
</asp:Content>