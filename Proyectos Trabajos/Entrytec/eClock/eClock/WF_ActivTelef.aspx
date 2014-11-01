<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="WF_ActivTelef.aspx.cs" Inherits="WF_ActivTelef" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Activación Telefónica de eClock</title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div style="text-align: center">
        &nbsp;</div>
        <igmisc:webpanel id="WebPanel1" runat="server" bordercolor="SteelBlue" borderstyle="Outset"
            cssclass="igwpMainBlack2k7" font-names="Tahoma" font-size="Medium">
<PanelStyle CssClass="igwpPanelBlack2k7">
<Padding Left="15px" Right="15px"></Padding>
</PanelStyle>

<Header Text="ACTIVACI&#211;N TELEF&#211;NICA">
<ExpandedAppearance>
<Styles ForeColor="White" CssClass="igwpHeaderExpandedBlack2k7" BackColor="SteelBlue"></Styles>
</ExpandedAppearance>

<HoverAppearance>
<Styles CssClass="igwpHeaderHoverBlack2k7"></Styles>
</HoverAppearance>

<CollapsedAppearance>
<Styles CssClass="igwpHeaderCollapsedBlack2k7"></Styles>
</CollapsedAppearance>

<ExpansionIndicator Height="0px" Width="0px"></ExpansionIndicator>
</Header>
<Template>
<TABLE style="WIDTH: 899px"><TBODY><TR><TD style="TEXT-ALIGN: center" colSpan=2><BR /><IMG src="Imagenes/imgencabezado.jpg" /><BR /></TD></TR><TR><TD><IMG src="Imagenes/Padlock%20User%20Control.JPG" /></TD><TD><TABLE><TBODY><TR><TD style="HEIGHT: 26px; TEXT-ALIGN: left" colSpan=3><SPAN style="FONT-SIZE: 10pt; FONT-FAMILY: Arial">Por favor para la activación telefónica&nbsp; marque cualquiera de los números siguientes <BR />(55)2454-4360<BR />(55)2454-4361<BR />ó Pongase en contacto con su vendedor<BR /><BR /></SPAN>&nbsp; </TD></TR><TR><TD style="TEXT-ALIGN: left" colSpan=2><SPAN style="FONT-SIZE: 10pt; FONT-FAMILY: Arial">Llave de producto : <BR /></SPAN></TD><TD style="TEXT-ALIGN: left" colSpan=1><asp:Label id="LProducto" runat="server" Font-Size="10pt" Font-Names="Arial" ForeColor="#0066FF" Font-Bold="True"></asp:Label></TD></TR><TR style="COLOR: #000000"><TD style="HEIGHT: 16px; TEXT-ALIGN: left" colSpan=2><SPAN style="FONT-SIZE: 10pt; FONT-FAMILY: Arial">Llave Telefonica:</SPAN></TD><TD style="HEIGHT: 16px; TEXT-ALIGN: left" colSpan=1><asp:Label id="LTelefono" runat="server" Font-Size="10pt" Font-Names="Arial" ForeColor="#0066CC" Font-Bold="True"></asp:Label></TD></TR><TR style="FONT-SIZE: 12pt; COLOR: #000000"><TD style="HEIGHT: 16px; TEXT-ALIGN: left" colSpan=2><SPAN style="FONT-SIZE: 10pt"><SPAN style="FONT-FAMILY: Arial"><SPAN>Activacion Telefonica</SPAN>:</SPAN></SPAN></TD><TD style="HEIGHT: 16px; TEXT-ALIGN: left" colSpan=1><igtxt:WebMaskEdit id="txtActivTelef" runat="server" Font-Size="10pt" Font-Names="Arial" InputMask="CCCC-CCCC-CCCC-CCCC">
                    </igtxt:WebMaskEdit></TD></TR><TR><TD style="HEIGHT: 18px; TEXT-ALIGN: center" colSpan=3><igtxt:webimagebutton id="BGuardarCambios" onclick="BGuardarCambios_Click" runat="server" width="150px" usebrowserdefaults="False" text="Siguiente" height="22px">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom"      />
                            <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"      />
                            <Appearance>
				
                                <Image Url="./Imagenes/Next.png"      />
                            </Appearance>
                        </igtxt:webimagebutton> <BR /><asp:Label id="LError" runat="server" Font-Size="10pt" Font-Names="Arial" ForeColor="Red"></asp:Label></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE><BR /><BR /><BR />
</Template>
</igmisc:webpanel>
 </form>
</body>
</html>
