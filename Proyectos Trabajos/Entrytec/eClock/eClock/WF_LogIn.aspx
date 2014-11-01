<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Page language="c#" CodeFile="WF_LogIn.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_LogIn" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Sistema de Asistencia</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="shortcut icon" href="favicon.ico" />
		    <script id="Login" type="text/javascript">
<!--
    function OnLoad()
    {
        igedit_getById("UsuarioUsuario").focus(); 
        try
        {
        //debugger;
            if(parent.location.href.indexOf("WF_LogIn.aspx",0)<=1) 
                parent.location.href = "WF_LogIn.aspx";
        }
	    catch(err)
	    {
    	    
	    }	
    }
    function OnClickTerminal()
    {
        __doPostBack('OnClickTerminal', 'OnClickTerminal');
        //__doPostBack();
    }
// -->
</script>
	</HEAD>
<body onload="OnLoad()" style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px;
    background-image: url(skins/page.bg.colorb.jpg); background-repeat: repeat-x;
    background-color: #1560bd;">
    <form id="form1" runat="server" style="text-align: center; background-image: url('skins/page.bg.color.jpg'); background-repeat: no-repeat;" >
			<TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%" align="center"
				border="0">
				<TR>
    					<TD align="center" width="100%" height="100%">
    					        <table border="0" cellpadding="0" cellspacing="0" class="BoxTable">
            <tr>
                <td class="BoxTL" style="width: 11px; height: 11px;">
                    <img height="11" src="skins/boxed_002.gif" width="11" /></td>
                <td class="BoxT" style="background-repeat: repeat-x; height: 11px; background-color: white;">
                    <img height="11" src="skins/dummy.gif" width="11" /></td>
                <td class="BoxTR" style="width: 11px; background-repeat: repeat-y; height: 11px;
                    background-color: white;">
                    <img height="11" src="skins/boxed.TR.gif" width="11" /></td>
            </tr>
            <tr>
                <td class="BoxL" style="width: 11px; background-repeat: repeat-y; background-color: white;">
                    <img height="11" src="skins/dummy.gif" width="11" /></td>
                <td class="BoxM" valign="top" style="background-color: white">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="LogoRow" colspan="2" style="width: 937px">
                            </td>
                        </tr>
                        <tr>
                            <td class="MenuHeight" colspan="2" style="width: 937px; height: 49px">
                                <img alt="" src="skins/LogInTitulo.jpg" /></td>
                        </tr>
                        <tr>
                            <td class="MenuHeight" colspan="2" style="text-align: left;" align="center">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 203px; vertical-align: baseline; height: 347px;">
                                            <table style="width: 935px">
                                                <tr>
                                                    <td colspan="3" style="font-family: Tahoma">
                                                        <strong><span style="font-size: 1.4em">Inicio de sesión</span></strong></td>
                                                    <td rowspan="1" style="width: 108px; font-family: Tahoma">
                                                    </td>
                                                    <td rowspan="1" style="font-family: Tahoma; text-align: right">
                                                    </td>
                                                    <td rowspan="1" style="font-family: Tahoma">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="font-family: Tahoma">
                                                        <asp:Panel ID="pnl_Estado" runat="server" BorderColor="#00C000" BorderStyle="Solid">
                                                            <table style="width: 544px">
                                                                <tr>
                                                                    <td rowspan="3" style="width: 55px">
                                                                        <asp:Image ID="img_Error" runat="server" ImageUrl="Imagenes/okshield.png" /></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_Estado" runat="server" Text="Para continuar, debes iniciar una sesión con tu nombre de usuario."></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td rowspan="1" style="width: 108px; font-family: Tahoma">
                                                    </td>
                                                    <td rowspan="1" style="text-align: right; cursor: hand;">
                                                    </td>
                                                    <td rowspan="1" style="cursor: hand; text-align: right;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 160px; background-color: #dde6f7; height: 31px;">
                                                        <span style="font-size: 0.8em; font-family: Arial"><strong>
                                                        Nombre de usuario:</strong></span></td>
                                                    <td rowspan="6" style="font-family: Tahoma; background-color: #dde6f7; text-align: center">
                                                        <img alt="candado" src="./Imagenes/candadosinfondo.png" /></td>
                                                    <td style="font-family: Tahoma; background-color: #dde6f7; height: 31px;">
                                                    </td>
                                                    <td rowspan="2" style="width: 108px; font-family: Tahoma">
                                                    </td>
                                                    <td rowspan="2" style="text-align: right; cursor: hand;" onclick="__doPostBack('OnClickTerminal', 'OnClickTerminal');">
                                                        <img alt="Terminal" src="Imagenes/eclock300.jpg" 
                                                            style="width: 64px; height: 60px" /></td>
                                                    <td rowspan="2" style="cursor: hand; text-align: center;"  onclick="__doPostBack('OnClickTerminal', 'OnClickTerminal');">
                                                        <span style="font-size: 0.8em">
                                                        Adquiere tu <strong>terminal 
                                                            <br />
                                                            biomética</strong></span></td>
                                                </tr>
                                                <tr style="font-family: Tahoma">
                                                    <td style="width: 160px; height: 28px; background-color: #dde6f7; text-align: center">
                                                        &nbsp;<igtxt:WebTextEdit id="UsuarioUsuario" runat="server" Width="100px" BorderStyle="Solid" UseBrowserDefaults="False"
										BorderWidth="1px" BorderColor="#7F9DB9" CellSpacing="1">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:WebTextEdit>
							<asp:RequiredFieldValidator id="RVUsuario" runat="server" ErrorMessage="*" ControlToValidate="UsuarioUsuario"></asp:RequiredFieldValidator></td>
                                                    <td style="height: 28px; background-color: #dde6f7">
                                                        <ul>
                                                            <li><strong><span style="font-size: 0.8em">Seguro </span></strong></li>
                                                        </ul>
                                                    </td>
                                                </tr>
                                                <tr style="font-family: Tahoma">
                                                    <td style="width: 160px; background-color: #dde6f7">
                                                        <span style="font-size: 0.8em; font-family: Arial"><strong>
                                                        Contraseña:</strong></span></td>
                                                    <td style="background-color: #dde6f7">
                                                        <ul>
                                                            <li><strong><span style="font-size: 0.8em">Fácil y rapido </span></strong></li>
                                                        </ul>
                                                    </td>
                                                    <td rowspan="2" style="width: 108px">
                                                    </td>
                                                    <td rowspan="2" style="text-align: right; cursor: hand;" onclick="__doPostBack('OnClickSitio', 'OnClickSitio');">
                                                        <img alt="Logo" src="./Imagenes/solo.png" style="width: 64px; height: 64px" /></td>
                                                    <td rowspan="2" style="cursor: hand; text-align: center" onclick="__doPostBack('OnClickSitio', 'OnClickSitio');">
                                                        <span style="font-size: 0.8em">Conoce mas sobre
                                                            <br />
                                                            <strong>eClock</strong></span></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 160px; height: 27px; background-color: #dde6f7; text-align: center" colspan="">
									<igtxt:WebTextEdit id="UsuarioClave" runat="server" Width="100px" BorderStyle="Solid" UseBrowserDefaults="False"
										BorderWidth="1px" BorderColor="#7F9DB9" CellSpacing="1" PasswordMode="True">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:WebTextEdit>
                                                        &nbsp;&nbsp;</td>
                                                    <td style="height: 27px; background-color: #dde6f7">
                                                        <ul>
                                                            <li><strong><span style="font-size: 0.8em">Confiable</span></strong></li>
                                                        </ul>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 160px; height: 33px; background-color: #dde6f7; text-align: center">
									<igtxt:WebImageButton id="BEntrar" runat="server" Text="Entrar" UseBrowserDefaults="False" Height="24px"
										Width="89px" ImageTextSpacing="4" OnClick="BEntrar_Click">
										<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
										<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
											RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
										<Appearance>
											<Image Url="./Imagenes/Usuarios.png" Height="24px" Width="24px"></Image>
                                            <ButtonStyle Cursor="Default">
                                            </ButtonStyle>
										</Appearance>
									</igtxt:WebImageButton>
                                                    </td>
                                                    <td style="height: 33px; background-color: #dde6f7">
                                                        <ul>
                                                            <li><strong><span style="font-size: 0.8em">Automatizado</span></strong></li>
                                                        </ul>
                                                    </td>
                                                    <td rowspan="2" style="width: 108px">
                                                    </td>
                                                    <td rowspan="2" style="text-align: right; cursor: hand;" onclick="__doPostBack('OnClickComentarios', 'OnClickComentarios');">
                                                        <img alt="comentarios" src="./Imagenes/comentarios.png" style="width: 64px; height: 64px;" /></td>
                                                    <td rowspan="2" style="cursor: hand; text-align: center" onclick="__doPostBack('OnClickComentarios', 'OnClickComentarios');">
                                                        <span style="font-size: 0.8em">
                                                        Regalanos tus 
                                                            <br />
                                                            <strong>comentarios </strong></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 160px; background-color: #dde6f7; height: 20px; text-align: right;">
                                                        <span style="font-size: 0.8em">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClick="LinkButton1_Click"
                                                                ToolTip="Da click aqui para recuperar tu contraseña">¿Olvidaste tu contraseña?</asp:LinkButton><br />
                                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" OnClick="LinkButton2_Click"
                                                                ToolTip="Da click aqui para registrarte">¿Aun no eres usuario?</asp:LinkButton></span></td>
                                                    <td style="background-color: #dde6f7; border-left-color: #dde6f7; border-bottom-color: #dde6f7; border-top-style: none; border-top-color: #dde6f7; border-right-style: none; border-left-style: none; height: 20px; border-right-color: #dde6f7; border-bottom-style: none;" rowspan="">
                                                    </td>
                                                </tr>
                                            </table>

                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="MenuHeight" colspan="2" style="height: 55px">
                                <span style="font-size: 9pt">Copyright © 2010, EntryTec todos
                                            los derechos reservados</span></td>
                        </tr>
                    </table>
                </td>
                <td class="BoxR" style="width: 11px; background-repeat: repeat-y; background-color: white;">
                    <img height="11" src="skins/dummy.gif" width="11" style="background-color: white" /></td>
            </tr>
            <tr>
                <td class="BoxBL" style="width: 11px">
                    <img height="11" src="skins/boxed.gif" width="11" /></td>
                <td class="BoxB" style="background-repeat: repeat-x; background-color: white;">
                    <img height="11" src="skins/dummy.gif" width="11" /></td>
                <td class="BoxBR" style="width: 11px; background-repeat: repeat-y; background-color: white;">
                    <img height="11" src="skins/boxed.BR.gif" width="11" /></td>
            </tr>
        </table>
        
						<P>
                            &nbsp;</P>

					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
