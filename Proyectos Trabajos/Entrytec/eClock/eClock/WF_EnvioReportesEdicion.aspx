<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="WF_EnvioReportesEdicion.aspx.cs" Inherits="WF_EnvioReportesEdicion" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>

<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <script id="igClientScript" type="text/javascript">
        <!--
        function UnaVez(){
            document.getElementById('optUna').setAttribute('checked','checked');
	        document.getElementById('Una').style.display='block';
	        document.getElementById('Varias1').style.display='none';
            document.getElementById('Varias2').style.display='none';
            document.getElementById('Varias3').style.display='none';
            document.getElementById('_ctl0_ContentPlaceHolder1_WebPanel2_TE').setAttribute('value','1');
        }
        function Periodo(){
            document.getElementById('optPer').setAttribute('checked','checked');
	        document.getElementById('Una').style.display='none';
	        document.getElementById('Varias1').style.display='block';
            document.getElementById('Varias2').style.display='block';
            document.getElementById('Varias3').style.display='block';
            document.getElementById('_ctl0_ContentPlaceHolder1_WebPanel2_TE').setAttribute('value','0');
        }
        // -->
    </script>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
    

<igmisc:WebPanel ID="WebPanel2" runat="server" BackColor="White" BorderColor="SteelBlue"
                        BorderStyle="Outset" BorderWidth="2px" Font-Names="Arial" ForeColor="Black" Width="436px" StyleSetName="PaneleClock">
    <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Black">
<Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />

<BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
            ColorTop="0, 45, 150" />
</PanelStyle>
    <Header TextAlignment="Left">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderColor="Transparent" BorderStyle="Ridge"
                                    BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Black" Height="15px">
<Padding Bottom="1px" Left="4px" Top="1px" />

<BorderDetails ColorLeft="158, 190, 245" ColorRight="0, 45, 150" ColorTop="158, 190, 245"
                    WidthBottom="0px" />
</Styles>
</ExpandedAppearance>

<HoverAppearance>
<Styles CssClass="igwpHeaderHoverBlue2k7"></Styles>
</HoverAppearance>

<CollapsedAppearance>
<Styles CssClass="igwpHeaderCollapsedBlue2k7"></Styles>
</CollapsedAppearance>

<ExpansionIndicator Height="0px" Width="0px" />
</Header>
    <Template>
<table width="100%"><tr><td align="left" style="width: 127px;" valign="top"><asp:Label ID="Label1" runat="server" Text="Descripción de la regla:"></asp:Label></td><td align="left" style="width: 203px;"><igtxt:WebTextEdit ID="WT_DESCRIPCION" runat="server" Width="285px" HorizontalAlign="Left">
                    </igtxt:WebTextEdit> </td></tr><tr><td align="left" style="width: 127px;"><asp:Label ID="Label2" runat="server" Text="Usuario a Enviar:"></asp:Label></td><td align="left" style="width: 203px;"><igcmbo:webcombo id="WC_USUARIO" runat="server" backcolor="White" bordercolor="Silver"
                        borderstyle="Solid" borderwidth="1px" forecolor="Black" selbackcolor="DarkBlue"
                        selforecolor="White" version="4.00" width="175px" ><Columns>
<igtbl:UltraGridColumn HeaderText="Column0">
<header caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="4.00">
<FrameStyle Cursor="Default" BackColor="Silver" BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" Font-Size="10pt" Height="130px" Width="325px"></FrameStyle>

<HeaderStyle BackColor="LightGray" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</HeaderStyle>

<RowStyle BackColor="White" BorderColor="Gray" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
</RowStyle>

<SelectedRowStyle BackColor="DarkBlue" ForeColor="White"></SelectedRowStyle>
</DropDownLayout>
</igcmbo:webcombo> </td></tr><tr><td align="left" style="width: 127px;"><asp:Label ID="Label3" runat="server" Text="Reporte:"></asp:Label></td><td align="left" style="width: 203px;"><igcmbo:webcombo id="WC_REPORTE" runat="server" backcolor="White" bordercolor="Silver"
                        borderstyle="Solid" borderwidth="1px" forecolor="Black" selbackcolor="DarkBlue"
                        selforecolor="White" version="4.00" width="175px"><Columns>
<igtbl:UltraGridColumn HeaderText="Column0">
<header caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<ExpandEffects ShadowColor="LightGray"></ExpandEffects>

<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="4.00">
<FrameStyle Cursor="Default" BackColor="Silver" BorderWidth="2px" BorderStyle="Ridge" Font-Names="Verdana" Font-Size="10pt" Height="130px" Width="325px"></FrameStyle>

<HeaderStyle BackColor="LightGray" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</HeaderStyle>

<RowStyle BackColor="White" BorderColor="Gray" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
</RowStyle>

<SelectedRowStyle BackColor="DarkBlue" ForeColor="White"></SelectedRowStyle>
</DropDownLayout>
</igcmbo:webcombo> </td></tr></table>
<table width="100%">
    <tr>
        <td align="right" style="width: 50%;">
            <input name="ejecutar" id="optUna" type="radio" checked="checked" onclick="UnaVez();" />
            <asp:Label ID="Label6" runat="server" Text="Ejecutar Regla una Vez"></asp:Label>
        </td>
        <td align="left" style="width: 50%;">
            <input name="ejecutar" id="optPer" type="radio" onclick="Periodo();" />
            <asp:Label ID="Label7" runat="server" Text="Ejecutar Regla Periódicamente"></asp:Label>
        </td>
    </tr>
</table>
<asp:TextBox ID="TE" runat="server" BorderWidth="0px" Columns="1" Font-Names="Arial" 
Font-Size="3pt" ForeColor="White" Width="2px" >1</asp:TextBox> 
</Template>
</igmisc:WebPanel>
    <br />
    <table id="Una" style="display: block; width: 299px">
        <tr>
            <td align="right" style="width: 160px" valign="top">
                            <asp:Label ID="Label8" runat="server" Text="Fecha y Hora de Ejecución"></asp:Label></td>
            <td>
                <igsch:WebDateChooser ID="WDT_FECHAE" runat="server" Width="100px">
                    <CalendarLayout DayNameFormat="FirstLetter" ShowFooter="False" ShowTitle="False">
                        <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" />
                        <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                        <OtherMonthDayStyle ForeColor="#ACA899" />
                        <SelectedDayStyle BackColor="CornflowerBlue" />
                        <TitleStyle BackColor="#9EBEF5" />
                        <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                        <WeekendDayStyle BackColor="#E0E0E0" />
                    </CalendarLayout>
                </igsch:WebDateChooser>
            </td>
            <td valign="top">
                            <igtxt:WebDateTimeEdit ID="WDT_HORAE" runat="server" DisplayModeFormat="HH:mm"
                                EditModeFormat="HH:mm" Width="40px">
                            </igtxt:WebDateTimeEdit>
            </td>
        </tr>
    </table>
    <table id="Varias1" style="display: none; width: 300px">
        <tr>
            <td align="right" style="width: 160px" valign="top">
                            <asp:Label ID="Label9" runat="server" Text="Fecha y Hora de Inicio"></asp:Label></td>
            <td valign="top">
                <igsch:WebDateChooser ID="WDT_FECHAI" runat="server" Width="100px" 
                    ShowDropDown="True">
                    <CalendarLayout DayNameFormat="FirstLetter" ShowFooter="False" ShowTitle="False">
                        <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" />
                        <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                        <OtherMonthDayStyle ForeColor="#ACA899" />
                        <SelectedDayStyle BackColor="CornflowerBlue" />
                        <TitleStyle BackColor="#9EBEF5" />
                        <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                        <WeekendDayStyle BackColor="#E0E0E0" />
                    </CalendarLayout>
                </igsch:WebDateChooser>
            </td>
            <td valign="top">
                            <igtxt:WebDateTimeEdit ID="WDT_HORAI" runat="server" DisplayModeFormat="HH:mm" EditModeFormat="HH:mm"
                                Width="40px">
                            </igtxt:WebDateTimeEdit>
            </td>
        </tr>
    </table>
                <table id="tb2" >
                    <tr>
                        <td align="left" id="Varias2" style="display:none;">
                            <igmisc:WebPanel ID="WebPanel1" runat="server" BackColor="White" BorderColor="SteelBlue"
                                BorderStyle="Outset" BorderWidth="2px" Font-Names="Arial" ForeColor="Black" Width="205px" StyleSetName="PaneleClock">
                                <Template>
                                    <br />
                            <asp:RadioButton GroupName="periodo" ID="optCadaNDias" runat="server" Text="Cada " Checked="True" /><igtxt:WebNumericEdit
                            ID="WT_DiasEnvio" runat="server" MaxLength="3" ValueText="1" Width="20px" MaxValue="999" MinValue="1">
                            </igtxt:WebNumericEdit>
                                    &nbsp;
                                    <asp:Label ID="Label10" runat="server" Text="día(s)"></asp:Label><br />
                                    <br />
                            <asp:RadioButton GroupName="periodo" ID="optCadaQuincena" runat="server" Text="Cada quincena" /><br />
                                    <br />
                            <asp:RadioButton GroupName="periodo" ID="optCadaMes" runat="server" Text="Cada mes" /><br />
                                    <br />
                                </Template>
                                <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Black">
                                    <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                                    <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                        ColorTop="0, 45, 150" />
                                </PanelStyle>
                                <Header Text="Periodicidad de Envio" TextAlignment="Left">
                                    <ExpandedAppearance>
                                        <Styles BackgroundImage="./images/GridTitulo.gif" BorderColor="Transparent" BorderStyle="Ridge"
                                            BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Black" Height="15px">
                                            <Padding Bottom="1px" Left="4px" Top="1px" />
                                            <BorderDetails ColorLeft="158, 190, 245" ColorRight="0, 45, 150" ColorTop="158, 190, 245"
                                        WidthBottom="0px" />
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
                            <ExpansionIndicator Height="0px" Width="0px" />
                        </Header>
                    </igmisc:WebPanel>
                </td>
                <td align="left" valign="top">
                    <igmisc:WebPanel ID="WebPanel3" runat="server" BackColor="White" BorderColor="SteelBlue"
                        BorderStyle="Outset" BorderWidth="2px" Font-Names="Arial" ForeColor="Black" Width="205px" StyleSetName="PaneleClock">
                        <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Black">
                            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                ColorTop="0, 45, 150" />
                        </PanelStyle>
                        <Header Text="Intervalo para el Reporte" TextAlignment="Left">
                            <ExpandedAppearance>
                                <Styles BackgroundImage="./images/GridTitulo.gif" BorderColor="Transparent" BorderStyle="Ridge"
                                    BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Black" Height="15px">
                                    <Padding Bottom="1px" Left="4px" Top="1px" />
                                    <BorderDetails ColorLeft="158, 190, 245" ColorRight="0, 45, 150" ColorTop="158, 190, 245"
                                        WidthBottom="0px" />
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
                            <ExpansionIndicator Height="0px" Width="0px" />
                        </Header>
                        <Template>
                            <br />
                    <asp:RadioButton GroupName="intervalo" ID="optDias" runat="server" Text="Enviar último(s) " Checked="True" /><igtxt:WebNumericEdit ID="WT_UltimosDias" runat="server" MaxLength="3" ValueText="1"
                                Width="20px" MaxValue="999" MinValue="1">
                            </igtxt:WebNumericEdit>
                            &nbsp;
                            <asp:Label ID="Label11" runat="server" Text="día(s)"></asp:Label><br />
                            <br />
                    <asp:RadioButton GroupName="intervalo" ID="optQuincena" runat="server" Text="Enviar última quincena" /><br />
                            <br />
                    <asp:RadioButton GroupName="intervalo" ID="optMes" runat="server" Text="Enviar último mes" /><br />
                            <br />
                        </Template>
                    </igmisc:WebPanel>
                </td>
            </tr>
                    <tr>
                        <td id="Varias3" style="display:none;" align="center" colspan="2" valign="top">
                            <asp:Label ID="Label4" runat="server" Text="Última Ejecución:" Font-Bold="True"></asp:Label>
                            <asp:Label ID="lbl_UltEjecucion" runat="server" Font-Bold="True"></asp:Label><br />
                            <asp:Label ID="Label5" runat="server" Text="Siguiente Ejecución:" EnableTheming="False" Font-Bold="True"></asp:Label>
                            <asp:Label ID="lbl_SigEjecucion" runat="server" Font-Bold="True"></asp:Label></td>
                    </tr>
        </table>
    <asp:Label ID="LError" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller"
        ForeColor="#CC0033"></asp:Label>
    <asp:Label ID="LCorrecto" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller"
        ForeColor="#00C000"></asp:Label><br />
    <igtxt:WebImageButton ID="btn_Regresar" runat="server" Height="22px" Text="Regresar" Width="150px" OnClick="btn_Regresar_Click">
        <Appearance>
            <Image Height="16px" Url="./Imagenes/Back.png" Width="16px" />
        </Appearance>
        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
        <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
    </igtxt:WebImageButton>
    &nbsp;
    <igtxt:WebImageButton ID="btn_Deshacer" runat="server" Height="22px" Text="Deshacer Cambios" Width="150px" OnClick="btn_Deshacer_Click">
        <Appearance>
            <Image Height="16px" Url="./Imagenes/Undo.png" Width="16px" />
        </Appearance>
        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
        <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
    </igtxt:WebImageButton>
    &nbsp;
    <igtxt:WebImageButton ID="btn_Guardar" runat="server" Height="22px" Text="Guardar Cambios" Width="150px" OnClick="btn_Guardar_Click">
        <Appearance>
            <Image Height="16px" Url="./Imagenes/Save_as.png" Width="16px" />
        </Appearance>
        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
        <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
    </igtxt:WebImageButton>
    <br />
	 </div>

    </form>
</body>
</html>