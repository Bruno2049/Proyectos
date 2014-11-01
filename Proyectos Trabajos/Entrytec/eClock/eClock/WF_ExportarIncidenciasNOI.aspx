<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_ExportarIncidenciasNOI.aspx.cs" Inherits="WF_ExportarIncidenciasNOI" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Exportar a NOI</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
    <igmisc:WebPanel ID="WebPanel2" runat="server" BackColor="White" BorderColor="SteelBlue"
        BorderStyle="Outset" BorderWidth="2px" Font-Bold="False" Font-Names="ARIAL" ForeColor="Black"
        StyleSetName="PaneleClock">
        <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Black">
            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                ColorTop="0, 45, 150" />
            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
        </PanelStyle>
        <Header TextAlignment="Left">
            <ExpandedAppearance>
                <Styles BackgroundImage="./images/GridTitulo.gif" BorderColor="Transparent" BorderStyle="Ridge"
                    BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Black"
                    Height="15px">
                    <BorderDetails ColorLeft="158, 190, 245" ColorRight="0, 45, 150" ColorTop="158, 190, 245"
                        WidthBottom="0px" />
                    <Padding Bottom="1px" Left="4px" Top="1px" />
                </Styles>
            </ExpandedAppearance>
            <HoverAppearance>
                <Styles CssClass="igwpHeaderHoverBlue2k7">
                </Styles>
            </HoverAppearance>
            <CollapsedAppearance>
                <Styles CssClass="igwpHeaderCollapsedBlue2k7">
                </Styles>
            </CollapsedAppearance>
            <ExpansionIndicator Height="10px" Width="10px" />
        </Header>
        <Template>
            <table style="width: 289px">
                <tr>
                    <td colspan="2">
                        Rango de Fechas</td>
                </tr>
                <tr>
                    <td style="width: 146px">
                        <igsch:WebCalendar ID="FechaI" runat="server">
                            <Layout DayNameFormat="FirstLetter" FooterFormat="" ShowFooter="False" ShowTitle="False">
                                <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                                <WeekendDayStyle BackColor="#E0E0E0" />
                                <SelectedDayStyle BackColor="CornflowerBlue" />
                                <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                                <OtherMonthDayStyle ForeColor="#ACA899" />
                                <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" />
                                <TitleStyle BackColor="#9EBEF5" />
                                <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False">
                                </CalendarStyle>
                            </Layout>
                        </igsch:WebCalendar>
                    </td>
                    <td>
                        <igsch:WebCalendar ID="FechaF" runat="server">
                            <Layout DayNameFormat="FirstLetter" FooterFormat="" ShowFooter="False" ShowTitle="False">
                                <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                                <WeekendDayStyle BackColor="#E0E0E0" />
                                <SelectedDayStyle BackColor="CornflowerBlue" />
                                <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                                <OtherMonthDayStyle ForeColor="#ACA899" />
                                <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" />
                                <TitleStyle BackColor="#9EBEF5" />
                                <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False">
                                </CalendarStyle>
                            </Layout>
                        </igsch:WebCalendar>
                    </td>
                </tr>
                <tr>
                    <td style="width: 146px">
                        </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 146px">
                        </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 146px; text-align: center; height: 23px;">
                        </td>
                    <td style="height: 23px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <igtxt:WebImageButton ID="BDeshacerCambios" runat="server" Height="22px" OnClick="BDeshacerCambios_Click"
                            Text="Regresar" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Appearance>
                                <Style Cursor="Default">
								</Style>
                                <Image Url="./Imagenes/Back.png" Height="16px" Width="16px" />
                            </Appearance>
                        </igtxt:WebImageButton>
                        &nbsp; &nbsp; &nbsp;
                        <igtxt:WebImageButton ID="BGuardarCambios" runat="server" Height="22px" OnClick="BGuardarCambios_Click"
                            Text="Generar Archivo" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                            <Appearance>
                                <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                                <Style Cursor="Default"></Style>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        </igtxt:WebImageButton>
                    </td>
                </tr>
            </table>
            <asp:Label ID="LError" runat="server" ForeColor="Red"></asp:Label>
        </Template>
    </igmisc:WebPanel>
</div>
    </form>
</body>
</html>
