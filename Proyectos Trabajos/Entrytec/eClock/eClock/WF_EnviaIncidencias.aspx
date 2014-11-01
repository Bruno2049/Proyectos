
<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="WF_EnviaIncidencias.aspx.cs" Inherits="WF_EnviaIncidencias" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                    <td colspan="2" style="vertical-align: middle; height: 83px">
                        Empleados:
                        <asp:TextBox ID="tbx_SQLEmpleados" runat="server" Height="75px" TextMode="MultiLine"
                            Width="399px">SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0</asp:TextBox></td>
                </tr>
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
                        &nbsp;&nbsp; <igtxt:WebImageButton ID="btn_Recibir" runat="server" Height="22px" OnClick="btn_Recibir_Click"
                            Text="Recibir" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                            <Appearance>
                                <Image Url="./Imagenes/Get.png" Height="16px" Width="16px" />
                                <Style Cursor="Default"></Style>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        </igtxt:WebImageButton>
                        &nbsp; &nbsp;
                        <igtxt:WebImageButton ID="btn_Enviar" runat="server" Height="22px" OnClick="btn_Enviar_Click"
                            Text="Enviar" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                            <Appearance>
                                <Image Url="./Imagenes/Send.png" Height="16px" Width="16px" />
                                <Style Cursor="Default"></Style>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        </igtxt:WebImageButton>
                    </td>
                </tr>
            </table>
            <asp:Label ID="LError" runat="server" ForeColor="Red" Font-Names="arial narrow" Font-Size="Smaller"></asp:Label><asp:Label
                ID="LCorrecto" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller" ForeColor="Green"></asp:Label>
        </Template>
    </igmisc:WebPanel>
</asp:Content>