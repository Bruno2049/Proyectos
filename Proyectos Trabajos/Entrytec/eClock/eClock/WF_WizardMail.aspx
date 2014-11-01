<%@ Page Language="C#"  MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="WF_WizardMail.aspx.cs" Inherits="WF_WizardMail" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table style="width: 619px">
            <tr>
                <td>
                    <igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
                        borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                        stylesetname="PaneleClock" width="100%">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="REPORTES AUTOMATICOS SEMANALES">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="White" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
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

<ExpansionIndicator Height="10px" Width="10px"></ExpansionIndicator>
</Header>
<Template>
<TABLE><TBODY><TR><TD style="WIDTH: 100%; TEXT-ALIGN: justify">Usted puede recibir el reporte de asistencias semanal automáticamente en su correo electrónico.</TD></TR></TBODY></TABLE><TABLE><TBODY><TR><TD style="HEIGHT: 26px; TEXT-ALIGN: left">Correo Electrónico</TD><TD style="HEIGHT: 26px; TEXT-ALIGN: left; width: 158px;"><igtxt:WebTextEdit id="txtCorreo" runat="server" Width="319px"></igtxt:WebTextEdit></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
                </td>
            </tr>
        </table>
        <br />
        <igmisc:webpanel id="Webpanel1" runat="server" backcolor="White" bordercolor="SteelBlue"
            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
            stylesetname="PaneleClock">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="REGISTRO GRATUITO">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="White" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
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

<ExpansionIndicator Height="10px" Width="10px"></ExpansionIndicator>
</Header>
<Template>
<TABLE><TBODY><TR><TD style="HEIGHT: 26px; TEXT-ALIGN: center" vAlign=middle><TABLE style="WIDTH: 428px"><TBODY><TR><TD style="WIDTH: 100%; TEXT-ALIGN: justify"><STRONG>Si desea registrarse&nbsp;para recibir soporte tecnico gratuito, de Click en el link</STRONG></TD></TR></TBODY></TABLE><BR /><asp:HyperLink id="HyperLink1" runat="server" Width="212px" ForeColor="DodgerBlue" NavigateUrl="http://www.EntryTec.com.mx/eClock/Soporte">http://www.EntryTec.com.mx/eClock/Soporte</asp:HyperLink></TD><TD style="HEIGHT: 26px; TEXT-ALIGN: left">&nbsp;<IMG src="Imagenes/WizardSoporte.png" style="width: 218px; height: 216px" /></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
        <br />
        <br />
        <br />
        <igtxt:webimagebutton id="BGuardarCambios" runat="server" height="22px" onclick="BGuardarCambios_Click"
            text="Siguiente" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
<Alignments VerticalAll="Bottom" VerticalImage="Middle"></Alignments>

<Appearance>
<Image Url="./Imagenes/Next.png" Height="16px" Width="16px"></Image>
</Appearance>

<RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif"></RoundedCorners>
</igtxt:webimagebutton>
        <br />
    
    </div>
     
</asp:Content>