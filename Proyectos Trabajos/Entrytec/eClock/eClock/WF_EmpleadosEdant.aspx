<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="WF_EmpleadosEdant.aspx.cs" Inherits="WF_EmpleadosEdant" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGauge.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGauge" TagPrefix="igGauge" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc2" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>


<html xmlns="http://www.w3.org/1999/xhtml"  >
<head id="Head1" runat="server">
    <title>eClock</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" style="text-align: center">
        <igmisc:WebPanel ID="WebPanel2" runat="server" EnableAppStyling="True" StyleSetName="Caribbean">
            <Header Text="Datos del empleado">
            </Header>
            <Template>
<TABLE style="WIDTH: 475px; TEXT-ALIGN: center" height="100%" cellSpacing=0 cellPadding=0 align=center border=0><TBODY><TR><TD style="HEIGHT: 16px" align=center></TD><TD style="WIDTH: 100%; HEIGHT: 16px" align=center>&nbsp;<TABLE align=center><TBODY><TR><TD vAlign=top align=center><asp:Table id="Tabla" runat="server" Width="1px" HorizontalAlign="Center" Height="18px">
            <asp:TableRow runat="server" HorizontalAlign="Center">
            </asp:TableRow>
        </asp:Table> 
    <br />
    <igtxt:WebImageButton id="btnEditarTerminales" onclick="btnEditarTerminales_Click" runat="server" Text="Acceso a Terminales" Height="22px" UseBrowserDefaults="False" ImageTextSpacing="4">
                                    <Alignments VerticalImage="Middle" VerticalAll="Bottom"  />
                                    <Appearance>
                                        <Style Cursor="Default"></Style>
                                        <Image Url="./Imagenes/Search.png" Height="16px" Width="16px" />
                                    </Appearance>
                                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                        MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"  />
                                </igtxt:WebImageButton> <igtxt:WebImageButton id="BtnEditarHuellas" onclick="BtnEditarHuellas_Click" runat="server" Text="Enrolamiento" Height="22px" UseBrowserDefaults="False" ImageTextSpacing="4" Width="120px">
                                    <Appearance>
                                        <Style Cursor="Default"></Style>
                                        <Image Url="./Imagenes/Roll.png" Height="16px" Width="16px" />
                                    </Appearance>
                                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                        MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"  />
                                    <Alignments VerticalImage="Middle" VerticalAll="Bottom"  />
                                </igtxt:WebImageButton>
</TD><TD style="HEIGHT: 387px" align=center><asp:Image id="Image1" runat="server" ImageUrl="WF_Personas_FotoN.aspx" BorderStyle="None" Width="240px" Height="272px"></asp:Image><BR /><BR /><INPUT id="File1" type=file name="File1" runat="server" /><BR /><igtxt:WebImageButton id="WebImageButton1" onclick="WebImageButton1_Click" runat="server" Text="Subir Foto" Width="150px" Height="22px" UseBrowserDefaults="False" ImageTextSpacing="4">
                                    <Alignments VerticalImage="Middle" VerticalAll="Bottom"  />
                                    <Appearance>
                                        <Image Url="./Imagenes/panel-screenshot.png" Height="18px" Width="20px"  />
                                        <Style Cursor="Default"></Style>
                                    </Appearance>
                                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                        MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"  />
                                </igtxt:WebImageButton> 
    <br />
    <br />
    <igtxt:WebImageButton id="WebImageButton3" onclick="WebImageButton3_Click" runat="server" Text="Eliminar Foto" Width="150px" Height="22px" UseBrowserDefaults="False" ImageTextSpacing="4">
                                    <Alignments VerticalImage="Middle" VerticalAll="Bottom"  />
                                    <Appearance>
                                        <Image Url="./Imagenes/Cancel.png" Height="16px" Width="16px"  />
                                        <Style Cursor="Default"></Style>
                                    </Appearance>
                                    <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                                        MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"  />
                                </igtxt:WebImageButton> </TD></TR></TBODY></TABLE>
</TD></TR><TR><TD style="TEXT-ALIGN: center" align=center></TD><TD style="WIDTH: 475px; TEXT-ALIGN: center" align=center>&nbsp;<asp:Label id="LError" runat="server" Font-Names="Arial" ForeColor="#CC0033" Font-Size="Smaller"></asp:Label><asp:Label id="LCorrecto" runat="server" Font-Names="Arial" ForeColor="#00C000" Font-Size="Smaller"></asp:Label></TD></TR></TBODY></TABLE>
            </Template>
        </igmisc:WebPanel>
        <br />
                    <igtxt:webimagebutton id="BDeshacerCambios" runat="server" height="22px" onclick="BDeshacerCambios_Click"
                        text="Regresar" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Back.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>
                    <igtxt:webimagebutton id="BGuardarCambios" runat="server" height="22px" onclick="BGuardarCambios_Click"
                        text="Guardar Cambios" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>
                    <br />
    </form>
</body>

</html>