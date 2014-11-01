<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WF_EnvioAutomaticoMails.aspx.cs" Inherits="WF_EnvioAutomaticoMails" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebSchedule.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="ig_sched" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <igmisc:WebPanel ID="WebPanel2" runat="server" BackColor="White" BorderColor="SteelBlue"
            BorderStyle="Outset" BorderWidth="2px" Font-Bold="False" Font-Names="ARIAL" ForeColor="Black"
            StyleSetName="PaneleClock">
            <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Black">
                <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                    ColorTop="0, 45, 150" />
                <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
            </PanelStyle>
            <Header Text="Configuracion" TextAlignment="Left">
                <ExpandedAppearance>
                    <Styles BackgroundImage="./images/GridTitulo.gif" BorderColor="Transparent" BorderStyle="Ridge"
                        BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="White"
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
                <table id="Table3" border="0" cellpadding="1" cellspacing="1" style="width: 100%;
                    height: 100%" width="672">
                    <tr>
                        <td align="left" colspan="2" style="height: 26px">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:CheckBox ID="CBActivar" runat="server" Text="Activar Envio Automatico de Mails"
                                Width="199px" /></td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 26px; text-align: left; width: 70px;">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            Desde</td>
                        <td align="left" style="width: 146px; height: 26px">
                            <igsch:webcalendar id="FECHA" runat="server">
<Layout ShowTitle="False" DayNameFormat="FirstLetter" ShowFooter="False" FooterFormat="">
<TodayDayStyle BorderColor="CornflowerBlue" BackColor="Transparent"></TodayDayStyle>

<WeekendDayStyle BackColor="#E0E0E0"></WeekendDayStyle>

<SelectedDayStyle BackColor="CornflowerBlue"></SelectedDayStyle>

<DayStyle BackColor="White" Font-Size="9pt" Font-Names="Arial"></DayStyle>

<OtherMonthDayStyle ForeColor="#ACA899"></OtherMonthDayStyle>

<DayHeaderStyle ForeColor="White" BackColor="#7A96DF" Font-Size="9pt" Font-Names="Tahoma"></DayHeaderStyle>

<TitleStyle BackColor="#9EBEF5"></TitleStyle>

<CalendarStyle Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Font-Overline="False" Font-Bold="False"></CalendarStyle>
</Layout>
</igsch:webcalendar>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 26px; text-align: left; width: 70px;">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Cada</td>
                        <td align="left" style="height: 26px">
                            <igtxt:webnumericedit id="TxtDias" runat="server" datamode="Int" minvalue="1" width="31px"></igtxt:webnumericedit>
                        dias</td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="height: 26px; text-align: center;">
                            <igtxt:webimagebutton id="BDeshacerCambios" runat="server" height="22px" onclick="BDeshacerCambios_Click"
                                text="Deshacer Cambios" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"  />
                            <Appearance>
                                <Image Url="./Imagenes/Copia de deshacer.png" Height="18px" Width="20px"  />
                                <Style Cursor="Default"></Style>
                            </Appearance>
                            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"  />
                        </igtxt:webimagebutton>
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <igtxt:webimagebutton id="BGuardarCambios" runat="server" height="22px" onclick="BGuardarCambios_Click"
                                text="Guardar Cambios" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom"  />
                            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"  />
                            <Appearance>
                                <Style Cursor="Default">
								</Style>
                                <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"  />
                            </Appearance>
                        </igtxt:webimagebutton>
                        </td>
                    </tr>
                </table>
            </Template>
        </igmisc:WebPanel>
        &nbsp;</div>
</asp:Content>