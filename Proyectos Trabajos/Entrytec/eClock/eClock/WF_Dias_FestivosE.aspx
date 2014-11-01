<%@ Page language="c#"  CodeFile="WF_Dias_FestivosE.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Dias_FestivosE" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register TagPrefix="uc1" TagName="WC_Mes" Src="WC_Mes.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Días Festivos</title>
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
    </style>
    </head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
    		
<TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD style="HEIGHT: 0%">
						</TD>
				</TR>
				<TR>
					<TD align="center" style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat">
                        &nbsp;<igmisc:WebPanel ID="WebPanel3" runat="server" Height="286px"
            EnableAppStyling="True" StyleSetName="Caribbean" ExpandEffect="None">
            <Header Text="Detalles del día festivo" >
            </Header>
            <Template>
                                        
                <table id="Table3" border="0" cellpadding="1" cellspacing="1" 
                    style="WIDTH: 633px; HEIGHT: 261px" width="633">
                    <tbody>
                        <tr>
                            <td align="left" 
                                style="FONT-SIZE: 11pt; WIDTH: 138px; FONT-FAMILY: Arial; HEIGHT: 10px">
                                <font face="Arial Narrow">Id. Dia Festivo</font>
                            </td>
                            <td align="left" style="WIDTH: 210px; HEIGHT: 10px">
                                <igtxt:WebNumericEdit ID="DiaFestivoId" runat="server" BorderColor="#7F9DB9" 
                                    BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Enabled="False" 
                                    Font-Names="Arial Narrow" UseBrowserDefaults="False" Width="200px">
                                    <buttonsappearance custombuttondefaulttriangleimages="Arrow">
                                        <buttonpressedstyle backcolor="#83A6F4">
                                        </buttonpressedstyle>
                                        <buttondisabledstyle backcolor="#E1E1DD" bordercolor="#D7D7D7" 
                                            forecolor="#BEBEBE">
                                        </buttondisabledstyle>
                                        <buttonstyle backcolor="#C5D5FC" bordercolor="#ABC1F4" borderstyle="Solid" 
                                            borderwidth="1px" forecolor="#506080" width="13px">
                                        </buttonstyle>
                                        <buttonhoverstyle backcolor="#DCEDFD">
                                        </buttonhoverstyle>
                                    </buttonsappearance>
                                    <spinbuttons defaulttriangleimages="ArrowSmall" width="15px" />
                                </igtxt:WebNumericEdit>
                            </td>
                            <td align="left" style="HEIGHT: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" 
                                style="FONT-SIZE: 11pt; WIDTH: 138px; FONT-FAMILY: Arial; HEIGHT: 1px">
                                <font face="Arial Narrow">Fecha</font>
                            </td>
                            <td align="left" style="WIDTH: 210px; HEIGHT: 1px">
                                <igsch:WebCalendar ID="Fecha" runat="server">
                                    <layout daynameformat="FirstLetter" footerformat="" showfooter="False" 
                                        showtitle="False">
                                        <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                                        <WeekendDayStyle BackColor="#C0C0FF" />
                                        <SelectedDayStyle BackColor="CornflowerBlue" />
                                        <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                                        <OtherMonthDayStyle ForeColor="#ACA899" />
                                        <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" 
                                            ForeColor="White" />
                                        <TitleStyle BackColor="#9EBEF5" />
                                        <calendarstyle font-bold="False" font-italic="False" font-overline="False" 
                                            font-strikeout="False" font-underline="False">
                                        </calendarstyle>
                                    </layout>
                                </igsch:WebCalendar>
                            </td>
                            <td align="left" style="HEIGHT: 1px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" 
                                style="FONT-SIZE: 11pt; WIDTH: 138px; FONT-FAMILY: Arial; HEIGHT: 5px">
                                <font face="Arial Narrow">Nombre</font>
                            </td>
                            <td align="left" style="WIDTH: 210px; HEIGHT: 5px">
                                <igtxt:WebTextEdit ID="DiaFestivoNombre" runat="server" BorderColor="#7F9DB9" 
                                    BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Font-Names="Arial Narrow" 
                                    MaxLength="45" UseBrowserDefaults="False" Width="200px">
                                    <buttonsappearance custombuttondefaulttriangleimages="Arrow">
                                        <buttonpressedstyle backcolor="#83A6F4">
                                        </buttonpressedstyle>
                                        <buttondisabledstyle backcolor="#E1E1DD" bordercolor="#D7D7D7" 
                                            forecolor="#BEBEBE">
                                        </buttondisabledstyle>
                                        <buttonstyle backcolor="#C5D5FC" bordercolor="#ABC1F4" borderstyle="Solid" 
                                            borderwidth="1px" forecolor="#506080" width="13px">
                                        </buttonstyle>
                                        <buttonhoverstyle backcolor="#DCEDFD">
                                        </buttonhoverstyle>
                                    </buttonsappearance>
                                </igtxt:WebTextEdit>
                            </td>
                            <td align="left" style="HEIGHT: 5px">
                                <asp:RequiredFieldValidator ID="RVDiaFestivoNombre" runat="server" 
                                    ControlToValidate="DiaFestivoNombre" 
                                    ErrorMessage="El nombre del dia festivo es obligatorio" 
                                    Font-Names="Arial Narrow"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="WIDTH: 138px">
                            </td>
                            <td align="left" style="WIDTH: 210px">
                                <asp:CheckBox ID="CBBorrar" runat="server" Font-Names="Arial Narrow" 
                                    Font-Size="Small" Text="Borrar Dia Festivo" />
                            </td>
                            <td align="left" style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Arial">
                                <asp:Label ID="LBorrar" runat="server" Font-Names="Arial Narrow">* Seleccione 
                                esta opción si lo que quiere es borrar el Dia Festivo</asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
                                        
            </Template>
            <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial">
                <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                    ColorTop="0, 45, 150" />
            </PanelStyle>
        </igmisc:WebPanel>
                    </TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label>
						<asp:Label id="LCorrecto" runat="server" ForeColor="#00C000" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label>
                    </TD>
				</TR>
				<TR>
					<TD align="center" class="style1">&nbsp;&nbsp;&nbsp;&nbsp;
						<igtxt:WebImageButton id="BDeshacerCambios" runat="server" 
                            UseBrowserDefaults="False" Text="Deshacer Cambios"
							Height="22px" Width="150px" ImageTextSpacing="4" onclick="BDeshacerCambios_Click">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:WebImageButton>&nbsp;&nbsp;&nbsp;
						<igtxt:WebImageButton id="BGuardarCambios" runat="server" 
                            UseBrowserDefaults="False" Text="Guardar Cambios"
							Height="22px" Width="150px" ImageTextSpacing="4" onclick="BGuardarCambios_Click">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:WebImageButton></TD>
				</TR>
			</TABLE>
 </div>
    </form>
</body>
</html>