<%@ Page Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="WF_OtrosProductos.aspx.cs" Inherits="WF_OtrosProductos" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table style="width: 563px">
            <tr>
                <td colspan="2">
                    <span style="font-size: 14pt">COMPONENTES ADICIONALES</span></td>
            </tr>
            <tr>
                <td colspan="2">
                    <igmisc:WebPanel ID="Webpanel1" runat="server" BackColor="White" BorderColor="SteelBlue"
                        BorderStyle="Outset" BorderWidth="2px" Font-Bold="False" Font-Names="ARIAL" ForeColor="Black"
                        StyleSetName="PaneleClock" Width="100%">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="IsCard">
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
<TABLE style="WIDTH: 100%"><TBODY><TR><TD style="width: 246px; height: 31px">
    <img src="Imagenes/IsCard.jpg" /></TD><TD style="height: 31px; text-align: justify">
    <strong><span style="font-size: 10pt">Solución para diseño e impresion de credenciales local o vía Internet.<br />
        <br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</span><asp:HyperLink ID="HyperLink1"
            runat="server" ForeColor="DodgerBlue" NavigateUrl="http://www.EntryTec.com.mx/IsCard"
            Width="166px">http://www.EntryTec.com.mx/IsCard</asp:HyperLink></strong></TD></TR></TBODY></TABLE>
</Template>
</igmisc:WebPanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 227px">
                    <igmisc:WebPanel ID="Webpanel3" runat="server" BackColor="White" BorderColor="SteelBlue"
                        BorderStyle="Outset" BorderWidth="2px" Font-Bold="False" Font-Names="ARIAL" ForeColor="Black"
                        StyleSetName="PaneleClock" Width="100%">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="IsVision">
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
<TABLE style="WIDTH: 100%"><TBODY><TR><TD style="height: 16px">
    <img src="Imagenes/IsVision.jpg" /></TD><TD style="height: 16px; text-align: justify">
    <strong><span style="font-size: 10pt">Solucion de monitoreo local o remoto con opcion de almacenaje de fotografía
        o video.<br />
        <br />
        <br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span>
        <asp:HyperLink ID="HyperLink2" runat="server" ForeColor="DodgerBlue" NavigateUrl="http://www.EntryTec.com.mx/IsVision"
            Width="184px">http://www.EntryTec.com.mx/IsVision</asp:HyperLink></strong></TD></TR></TBODY></TABLE>
</Template>
</igmisc:WebPanel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <igtxt:webimagebutton id="BGuardarCambios" runat="server" onclick="BGuardarCambios_Click"
                        text="Finalizar" usebrowserdefaults="False" ImageTextSpacing="4" Height="22px">
<Alignments VerticalAll="Bottom" VerticalImage="Middle"></Alignments>

<Appearance>
<Image Url="./Imagenes/Accept.png" Height="16px" Width="16px"></Image>
</Appearance>

<RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif"></RoundedCorners>
</igtxt:webimagebutton>
                    &nbsp;&nbsp;
                    <igtxt:webimagebutton id="btn_BuscarTerminales" runat="server" onclick="btn_BuscarTerminales_Click"
                        text="Buscar Terminales" usebrowserdefaults="False" ImageTextSpacing="4" Height="22px">
                        <Appearance>
                            <Image Height="16px" Url="./Imagenes/Buscar_Terminal.png" Width="16px" />
                        </Appearance>
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                    </igtxt:WebImageButton>
                </td>
            </tr>
        </table>
    
    </div>

</asp:Content>