<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_ReorganizarEmpleados.aspx.cs" Inherits="WF_ReorganizarEmpleados" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.Misc" tagprefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 330px;
            text-align: center;
        }
        .style2
        {
            width: 288px;
            font-size: small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        <table style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style1">
                    <igmisc:WebPanel ID="WebPanel1" runat="server" EnableAppStyling="True" 
                        Height="325px" style="text-align: left" StyleSetName="Caribbean" 
                        Width="633px">
                        <Header Text="Reagrupar Empleados">
                        </Header>
                        <Template>
                            &nbsp;<br />
                            <table style="width:100%;">
                                <tr>
                                    <td class="style2">
                                        1. Elija los campos por los que desea agrupar</td>
                                    <td>
                                        <asp:DropDownList ID="Cmb_Campos" runat="server" Height="21px" Width="173px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <igtxt:WebImageButton ID="Btn_Agregar" runat="server" Height="22px" 
                                            ImageTextSpacing="4" onclick="Btn_Agregar_Click" Text="Agregar a la lista" 
                                            UseBrowserDefaults="False" Width="150px">
                                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                            <Appearance>
                                                <Image Height="16px" Url="./Imagenes/Dar_baja.png" Width="16px" />
                                                <ButtonStyle Cursor="Default">
                                                </ButtonStyle>
                                            </Appearance>
                                        </igtxt:WebImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2" rowspan="2">
                                        2. Edite el orden que se usará para generar las nuevas agrupaciones</td>
                                    <td rowspan="5">
                                        <asp:ListBox ID="Lbx_Campos" runat="server" Height="194px" Width="173px">
                                        </asp:ListBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <igtxt:WebImageButton ID="Btn_Subir" runat="server" Height="22px" 
                                            ImageTextSpacing="4" onclick="Btn_Subir_Click" Text="Subir posición" 
                                            UseBrowserDefaults="False" Visible="False" Width="150px">
                                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                            <Appearance>
                                                <Image Height="16px" Url="./images/igdtc_arrowup.gif" Width="16px" />
                                                <ButtonStyle Cursor="Default">
                                                </ButtonStyle>
                                            </Appearance>
                                        </igtxt:WebImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        &nbsp;</td>
                                    <td>
                                        <igtxt:WebImageButton ID="Btn_Bajar" runat="server" Height="22px" 
                                            ImageTextSpacing="4" onclick="BGuardarCambios1_Click" Text="Bajar posicion" 
                                            UseBrowserDefaults="False" Visible="False" Width="150px">
                                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                            <Appearance>
                                                <Image Height="16px" Url="./images/Igdtc_arrowdown.gif" Width="16px" />
                                                <ButtonStyle Cursor="Default">
                                                </ButtonStyle>
                                            </Appearance>
                                        </igtxt:WebImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        &nbsp;</td>
                                    <td>
                                        <igtxt:WebImageButton ID="Btn_Quitar" runat="server" Height="22px" 
                                            ImageTextSpacing="4" onclick="Btn_Quitar_Click" Text="Quitar de la lista" 
                                            UseBrowserDefaults="False" Width="150px">
                                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                            <Appearance>
                                                <Image Height="16px" Url="./Imagenes/Remover.png" Width="16px" />
                                                <ButtonStyle Cursor="Default">
                                                </ButtonStyle>
                                            </Appearance>
                                        </igtxt:WebImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        3. Regenere las agrupaciones</td>
                                    <td style="text-align: center">
                                        <igtxt:WebImageButton ID="BGuardarCambios" runat="server" Height="22px" 
                                            ImageTextSpacing="4" onclick="BGuardarCambios_Click" Text="Aplicar Cambios" 
                                            UseBrowserDefaults="False" Width="150px">
                                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                            <Appearance>
                                                <style cursor="Default">
                                                </style>
                                                <Image Height="16px" Url="./Imagenes/Save_as.png" Width="16px" />
                                                <ButtonStyle Cursor="Default">
                                                </ButtonStyle>
                                            </Appearance>
                                        </igtxt:WebImageButton>
                                    </td>
                                    <td style="font-size: x-small">
                                        *Todas las agrupaciones anteriores se perderán</td>
                                </tr>
                            </table>
                        </Template>
                    </igmisc:WebPanel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style1">
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Names="Arial" Font-Size="Smaller"></asp:Label>
						<asp:Label id="LCorrecto" runat="server" ForeColor="#00C000" Font-Names="Arial" Font-Size="Smaller"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style1">
						&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
