<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_SolicitudNueva.aspx.cs" Inherits="WF_SolicitudNueva" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <style type="text/css">
        .style1
        {        	
        	font-size: 9pt;
            font-family: Arial; 
        }

        .style5
        {
            width: 492px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%; height: 331px">
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;<igmisc:WebPanel ID="WebPanel2" runat="server"
                        
                         Width="496px" EnableAppStyling="True" StyleSetName="Caribbean">

                        <Header TextAlignment="Left" Text="Solicitud de incidencia">
                        </Header>
                        <Template>
                            <table style="width: 480px; height: 100%" id="Table2" cellspacing="1" cellpadding="1"
                                align="center" border="0">
                                <tbody>
                                    <tr>
                                        <td align="left" class="style1">
                                            Tipo Incidencia</td>
                                        <td class="style1">
                                            <asp:Label ID="Lbl_TipoIncidencia" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style1">
                                            Fechas</td>
                                        <td align="left" class="style1">
                                            <asp:Label ID="Lbl_PerFechas" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style1">
                                            Comentario</td>
                                        <td style="width: 250px; height: 12px" align="left">
                                            <font face="Arial">
                                                <igtxt:WebTextEdit ID="Tbx_Comentario" runat="server" 
                                                HorizontalAlign="Left" Width="303px" MaxLength="255">
                                                </igtxt:WebTextEdit>
                                            </font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style1" colspan="2">
                                            <asp:Label ID="Lbl_Html" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style1" colspan="2">
                                            <asp:Label ID="LCorrecto" runat="server" ForeColor="#009933"></asp:Label>
                                            <asp:Label ID="LError" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </Template>
                    </igmisc:WebPanel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
                <td align="center" class="style5">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<igtxt:WebImageButton
                        ID="BDeshacerCambios" runat="server" Height="22px" Text="Regresar" UseBrowserDefaults="False"
                        Width="150px" ImageTextSpacing="4" onclick="BDeshacerCambios_Click">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80"
                            MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif"
                            FocusImageUrl="ig_butXP3wh.gif" />
                        <Appearance>
                            <Style Cursor="Default">
								</Style>
                            <Image Url="./Imagenes/Undo.png" Height="16px" Width="16px" />

<ButtonStyle Cursor="Default"></ButtonStyle>
                        </Appearance>
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;&nbsp;
                    <igtxt:WebImageButton ID="BGuardarCambios" runat="server" Height="22px" OnClick="BGuardarCambios_Click"
                        Text="Enviar Solicitud" UseBrowserDefaults="False" Width="150px" 
                        ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80"
                            MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif"
                            FocusImageUrl="ig_butXP3wh.gif" />
                        <Appearance>
                            <Style Cursor="Default">
								</Style>
                            <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />

<ButtonStyle Cursor="Default"></ButtonStyle>
                        </Appearance>
                    </igtxt:WebImageButton>
                </td>
                <td align="center">
                    &nbsp;</td>
            </tr>
        </table>

        <p>
            &nbsp;</p>

    </form>
</body>
</html>
