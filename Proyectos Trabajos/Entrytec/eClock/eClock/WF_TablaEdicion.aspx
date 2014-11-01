<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_TablaEdicion.aspx.cs" Inherits="WF_TablaEdicion" %>


<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-size: 13px; font-family: tahoma; margin: 0px;">
    <form id="form1" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td colspan="2">
                    <asp:Table ID="Tabla" runat="server" Width="100%">
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Size="" Font-Names=""></asp:Label>
						<asp:Label id="LCorrecto" runat="server" ForeColor="#00C000" Font-Size="" Font-Names=""></asp:Label></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td style="text-align: right">
                    <igtxt:WebImageButton ID="Btn_Guardar" runat="server" Text="Guardar" 
                        UseBrowserDefaults="False" onclick="Btn_Guardar_Click">
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                            FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                            ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                            PressedImageUrl="ig_butXP4wh.gif" RenderingType="Disabled" />
                        <Appearance>
                            <Image Url="./Imagenes/Iconos/Guardar16.png" />
                        </Appearance>
                        <FocusAppearance>
                            <ButtonStyle BackColor="#DEEAF3">
                            </ButtonStyle>
                            <InnerBorder ColorBottom="37, 108, 180" ColorLeft="37, 108, 180" 
                                ColorRight="37, 108, 180" ColorTop="37, 108, 180" StyleBottom="Solid" 
                                StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid" WidthBottom="1px" 
                                WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                        </FocusAppearance>
                        <HoverAppearance ContentShift="UpLeft">
                            <ButtonStyle BackColor="#BCD7F1">
                            </ButtonStyle>
                            <InnerBorder ColorBottom="37, 108, 180" ColorLeft="37, 108, 180" 
                                ColorRight="37, 108, 180" ColorTop="37, 108, 180" StyleBottom="Solid" 
                                StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid" WidthBottom="1px" 
                                WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                        </HoverAppearance>
                        <PressedAppearance ContentShift="DownRight">
                            <ButtonStyle BackColor="#92B8E7">
                            </ButtonStyle>
                            <InnerBorder ColorBottom="37, 108, 180" ColorLeft="37, 108, 180" 
                                ColorRight="37, 108, 180" ColorTop="37, 108, 180" StyleBottom="Solid" 
                                StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid" WidthBottom="1px" 
                                WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                        </PressedAppearance>
                    </igtxt:WebImageButton>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
