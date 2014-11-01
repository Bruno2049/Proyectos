<%@ Page Language="C#" AutoEventWireup="true"   CodeFile="WF_SitiosE.aspx.cs" Inherits="WF_SitiosE" %>

<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <style type="text/css">
        .style15
        {
            width: 388px;
            height: auto;
        }
        .style24
        {
            width: 129px;
            height: 24px;
            text-align: center;
        }
        .style27
        {
            font-family: "Segoe UI";
            font-size: small;
            width: 128px;
        }
        .style28
        {
            width: 315px;
            height: 24px;
        }
        .style30
        {
            font-family: "Segoe UI";
            font-size: small;
            height: 24px;
            width: 128px;
        }
        .style31
        {
            font-family: "Segoe UI";
            font-size: small;
            height: 24px;
            width: 128px;
            text-align: center;
        }
    </style>

</head>
<body style="font-family: 'Segoe UI'; font-size: x-small; padding:1% 20% 1% 20%">
    <form id="form1" runat="server">
        <table id="Table2" align="center"class="style15">
            <tbody>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style27">
                        <asp:Label ID="Lbl_SITIO_ID" runat="server" Text="ID Sitio"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <igtxt:WebNumericEdit ID="Wne_SITIO_ID" runat="server" Enabled="False" 
                            Font-Size="Small">
                        </igtxt:WebNumericEdit>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style27">
                        <asp:Label ID="Lbl_SITIO_NOMBRE" runat="server" Text="Nombre del Sitio"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <font face="Arial">
                            <igtxt:WebTextEdit ID="Wtx_SITIO_NOMBRE" runat="server" 
                            HorizontalAlign="Right" Font-Size="Small">
                            </igtxt:WebTextEdit>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style27">
                        <asp:Label ID="SITIO_CONSULTA" runat="server" Text="Consulta del Sitio"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <font face="Arial">
                            <igtxt:WebTextEdit ID="Wtx_SITIO_CONSULTA" runat="server" 
                            HorizontalAlign="Right" Font-Size="Small">
                            </igtxt:WebTextEdit>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style30">
                        <asp:Label ID="Lbl_SITIO_HDESDE_SVEC" runat="server" Text="Sincroniza Desde"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <font face="Arial">
                            <igtxt:WebDateTimeEdit ID="Wde_SITIO_HDESDE_SVEC" runat="server" 
                            EditModeFormat="HH:mm" DisplayModeFormat="HH:mm" 
                            MinimumNumberOfValidFields="1" PromptChar=" " 
                            Font-Size="Small">
                            </igtxt:WebDateTimeEdit>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style27">
                        <asp:Label ID="Lbl_SITIO_HHASTA_SVEC" runat="server" Text="Sincroniza Hasta"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <igtxt:WebDateTimeEdit ID="Wde_SITIO_HHASTA_SVEC" runat="server" 
                            EditModeFormat="HH:mm" DisplayModeFormat="HH:mm" 
                            MinimumNumberOfValidFields="1" PromptChar=" " 
                            Font-Size="Small">
                        </igtxt:WebDateTimeEdit>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style27">
                        <asp:Label ID="Lbl_SITIO_SEGUNDOS_SYNC" runat="server" 
                            Text="Segundos entre Sincronizacion"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <igtxt:WebNumericEdit ID="Wne_SITIO_SEGUNDOS_SYNC" runat="server" 
                            Font-Size="Small">
                        </igtxt:WebNumericEdit>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style27">
                        <asp:Label ID="Lbl_SITIO_RESPONSABLES" runat="server" 
                            Text="Responsable(s) del Sitio"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <font face="Arial">
                            <igtxt:WebTextEdit ID="Wtx_SITIO_RESPONSABLES" runat="server" 
                            HorizontalAlign="Right" Font-Size="Small">
                            </igtxt:WebTextEdit>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style27">
                        <asp:Label ID="Lbl_SITIO_TELEFONOS" runat="server" Text="Teléfono"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <font face="Arial">
                            <igtxt:WebTextEdit ID="Wtx_SITIO_TELEFONOS" runat="server" 
                            HorizontalAlign="Right" Font-Size="Small">
                            </igtxt:WebTextEdit>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style27">
                        <asp:Label ID="Lbl_SITIO_DIRECCION_1" runat="server" Text="Dirección 1"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <font face="Arial">
                            <igtxt:WebTextEdit ID="Wtx_SITIO_DIRECCION_1" runat="server" 
                            HorizontalAlign="Right" Font-Size="Small">
                            </igtxt:WebTextEdit>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style27">
                        <asp:Label ID="Lbl_SITIO_DIRECCION_2" runat="server" Text="Dirección 2"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <font face="Arial">
                            <igtxt:WebTextEdit ID="Wtx_SITIO_DIRECCION_2" runat="server" 
                            HorizontalAlign="Right" Font-Size="Small">
                            </igtxt:WebTextEdit>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style27">
                        <asp:Label ID="Lbl_SITIO_REFERENCIAS" runat="server" 
                            Text="Referencias (como llegar)"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <font face="Arial">
                            <igtxt:WebTextEdit ID="Wtx_SITIO_REFERENCIAS" runat="server" 
                            HorizontalAlign="Right" Font-Size="Small">
                            </igtxt:WebTextEdit>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 11pt; font-family: Arial; " align="left" class="style27">
                        <asp:Label ID="Lbl_SITIO_COMENTARIOS" runat="server" Text="Comentarios"></asp:Label>
                    </td>
                    <td align="left" class="style28" colspan="2">
                        <font face="Arial">
                            <igtxt:WebTextEdit ID="Wtx_SITIO_COMENTARIOS" runat="server" 
                            HorizontalAlign="Right" Font-Size="Small">
                            </igtxt:WebTextEdit>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style30">
                        <font face="Arial">
                            <asp:Label ID="Lbl_SITIO_BORRADO" runat="server" ForeColor="Red" 
                            Font-Size="X-Small" Font-Names="Segoe UI">* Seleccione esta opción si lo que quiere es borrar el sitio</asp:Label></font></td>
                    <td align="left" class="style28" colspan="2">
                        &nbsp;
                        <asp:CheckBox ID="Chb_SITIO_BORRADO" runat="server" Font-Size="X-Small" Text="Borrar Sitio"
                            Font-Names="Segoe UI" Font-Strikeout="False"></asp:CheckBox><font face="Arial"></font></td>
                </tr>
                <tr>
                    <td align="left" class="style30">
                        &nbsp;</td>
                    <td align="left" class="style28" colspan="2">
                        <asp:Label ID="Lbl_Error" runat="server" Font-Names="Segoe UI" 
                            Font-Size="Small" ForeColor="Red"></asp:Label>
                        <asp:Label ID="Lbl_Correcto" runat="server" Font-Names="Segoe UI" 
                            Font-Size="Small" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style31">
                            <igtxt:WebImageButton ID="Btn_Deshacer" runat="server" Height="22px" 
                                ImageTextSpacing="4" onclick="Btn_Deshacer_Click" style="text-align: center" 
                                Text="Deshacer" UseBrowserDefaults="False">
                                <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                                <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                    FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                    ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                    PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                <Appearance>
                                    <style cursor="Default">
                                    </style>
                                    <Image Height="16px" Url="./Imagenes/Undo.png" Width="16px" />
                                </Appearance>
                            </igtxt:WebImageButton>
                    </td>
                    <td class="style24">
                        &nbsp;</td>
                    <td class="style24">
                        <igtxt:WebImageButton ID="Btn_Guardar" runat="server" ImageTextSpacing="4" 
                            OnClick="Btn_Guardar_Click" style="text-align: center; height: 26px;" Text="Guardar" 
                            UseBrowserDefaults="False">
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Appearance>
                                <style cursor="Default">
                                </style>
                                <Image Height="16px" Url="./Imagenes/Save_as.png" Width="16px" />
                            </Appearance>
                        </igtxt:WebImageButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
