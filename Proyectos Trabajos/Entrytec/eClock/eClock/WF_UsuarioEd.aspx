<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_UsuarioEd.aspx.cs" Inherits="WF_UsuarioEd" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.Misc" tagprefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebCombo" tagprefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style2
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
        }
        .style7
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: x-small;
        }
        .style1
        {
            height: 28px;
        }
        .style8
        {
        	            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            width: 300px;
        }
        .style10
        {
            height: 116px;
        }
        .style11
        {
            height: 148px;
        }
        .style12
        {
            height: 98px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="600" style="height: 380px">
				<TR>
					<TD style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat"
						align="center" class="style11">
						<DIV align="center">
						</DIV>
                        <igmisc:WebPanel ID="WebPanel3" runat="server" EnableAppStyling="True" 
                            Height="100px" StyleSetName="Caribbean" Width="605px">
                            <Header Text="Datos del Usuario">
                            </Header>
                            <Template>
                                <TABLE style="WIDTH: 607px; FONT-FAMILY: arial; HEIGHT: 6px" id="Table3" 
    cellSpacing=2 cellPadding=1 align=left border=0>
                                    <TBODY>
                                        <TR>
                                            <TD style="WIDTH: 124px; " align=left class="style2">
                                                Usuario</TD>
                                            <TD style="WIDTH: 166px" align=left>
                                                <igtxt:webtextedit id="Tbx_Usuario" runat="server" BorderColor="#7F9DB9" 
                    Font-Names="Arial Narrow" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
                    UseBrowserDefaults="False" CellSpacing="1" Enabled="False">
                                                    <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                        <ButtonPressedStyle BackColor="#83A6F4">
                                                        </ButtonPressedStyle>
                                                        <ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD">
                                                        </ButtonDisabledStyle>
                                                        <ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC">
                                                        </ButtonStyle>
                                                        <ButtonHoverStyle BackColor="#DCEDFD">
                                                        </ButtonHoverStyle>
                                                    </ButtonsAppearance>
                                                </igtxt:webtextedit>
                                            </TD>
                                            <TD align=left>
                                                &nbsp;</TD>
                                        </TR>
                                        <TR style="COLOR: #000000">
                                            <TD style="WIDTH: 124px; " align=left class="style2">
                                                Nombre</TD>
                                            <TD style="WIDTH: 166px" align=left>
                                                <igtxt:webtextedit id="Tbx_Nombre" runat="server" BorderColor="#7F9DB9" 
                    Font-Names="Arial Narrow" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
                    UseBrowserDefaults="False" CellSpacing="1" Enabled="False">
                                                    <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                        <ButtonPressedStyle BackColor="#83A6F4">
                                                        </ButtonPressedStyle>
                                                        <ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD">
                                                        </ButtonDisabledStyle>
                                                        <ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC">
                                                        </ButtonStyle>
                                                        <ButtonHoverStyle BackColor="#DCEDFD">
                                                        </ButtonHoverStyle>
                                                    </ButtonsAppearance>
                                                </igtxt:webtextedit>
                                            </TD>
                                            <TD align=left>
                                                &nbsp;</TD>
                                        </TR>
                                        <tr>
                                            <td align="left" style="width: 124px; " class="style2">
                                                Correo Electronico</td>
                                            <td align="left" style="width: 166px">
                                                <igtxt:webtextedit id="Tbx_Correo" runat="server" BorderColor="#7F9DB9" 
                Font-Names="Arial Narrow" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
                UseBrowserDefaults="False" CellSpacing="1">
                                                    <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                        <ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC">
                                                        </ButtonStyle>
                                                        <ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD">
                                                        </ButtonDisabledStyle>
                                                        <ButtonHoverStyle BackColor="#DCEDFD">
                                                        </ButtonHoverStyle>
                                                        <ButtonPressedStyle BackColor="#83A6F4">
                                                        </ButtonPressedStyle>
                                                    </ButtonsAppearance>
                                                </igtxt:WebTextEdit>
                                            </td>
                                            <td align="left">
                                                &nbsp;</td>
                                        </tr>
                                    </TBODY>
                                </TABLE>
                            </Template>
                        </igmisc:WebPanel>
					</TD>
				</TR>
				<TR>
					<TD style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat"
						align="center" class="style12">
                        <igmisc:WebPanel ID="WebPanel5" runat="server" EnableAppStyling="True" 
                            Height="102px" StyleSetName="Caribbean" Width="601px">
                            <Header Text="Cambio de Constraseña">
                            </Header>
                            <Template>
                                <TABLE style="WIDTH: 607px; FONT-FAMILY: arial; HEIGHT: 35px" id="Table5" 
    cellSpacing=2 cellPadding=1 align=left border=0>
                                    <TBODY>
                                        <tr>
                                            <td align="left" class="style2" style="width: 124px; ">
                                                Clave Actual</td>
                                            <td align="left" style="width: 166px">
                                                <igtxt:WebTextEdit ID="Tbx_ClaveAct" runat="server" BorderColor="#7F9DB9" 
                                                    BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" Font-Names="Arial Narrow" 
                                                    PasswordMode="True" UseBrowserDefaults="False" Width="200px">
                                                    <buttonsappearance custombuttondefaulttriangleimages="Arrow">
                                                        <buttonpressedstyle backcolor="#83A6F4">
                                                        </buttonpressedstyle>
                                                        <buttondisabledstyle backcolor="#E1E1DD" bordercolor="#D7D7D7" 
                                                            forecolor="#BEBEBE">
                                                        </buttondisabledstyle>
                                                        <buttonstyle backcolor="#C5D5FC" bordercolor="#ABC1F4" borderstyle="Solid" 
                                                            borderwidth="1px" forecolor="#506080" width="13px">
                                                        </buttonstyle>
                                                        <buttonhoverstyle backcolor="#DCEDFD">
                                                        </buttonhoverstyle>
                                                    </buttonsappearance>
                                                </igtxt:WebTextEdit>
                                            </td>
                                            <td align="left" class="style2" >
                                                *Solo si requiere cambiar la contraseña</td>
                                        </tr>
                                        <TR>
                                            <TD style="WIDTH: 124px; TEXT-ALIGN: left" 
            align=left class="style2">
                                                Nueva Clave</TD>
                                            <TD style="WIDTH: 166px" align=left>
                                                <igtxt:webtextedit id="Tbx_ClaveNueva" runat="server" BorderColor="#7F9DB9" 
                Font-Names="Arial Narrow" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
                UseBrowserDefaults="False" CellSpacing="1" PasswordMode="True">
                                                    <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                        <ButtonPressedStyle BackColor="#83A6F4">
                                                        </ButtonPressedStyle>
                                                        <ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD">
                                                        </ButtonDisabledStyle>
                                                        <ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC">
                                                        </ButtonStyle>
                                                        <ButtonHoverStyle BackColor="#DCEDFD">
                                                        </ButtonHoverStyle>
                                                    </ButtonsAppearance>
                                                </igtxt:webtextedit>
                                            </TD>
                                            <TD align=left>
                                                &nbsp;</TD>
                                        </TR>
                                        <TR>
                                            <TD style="WIDTH: 124px; HEIGHT: 26px; TEXT-ALIGN: left" 
                align=left class="style2">
                                                Confirma Clave</TD>
                                            <TD style="WIDTH: 166px; HEIGHT: 26px" align=left>
                                                <igtxt:webtextedit id="Tbx_ClaveConf" runat="server" BorderColor="#7F9DB9" 
                    Font-Names="Arial Narrow" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
                    UseBrowserDefaults="False" CellSpacing="1" PasswordMode="True">
                                                    <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
                                                        <ButtonPressedStyle BackColor="#83A6F4">
                                                        </ButtonPressedStyle>
                                                        <ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD">
                                                        </ButtonDisabledStyle>
                                                        <ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC">
                                                        </ButtonStyle>
                                                        <ButtonHoverStyle BackColor="#DCEDFD">
                                                        </ButtonHoverStyle>
                                                    </ButtonsAppearance>
                                                </igtxt:webtextedit>
                                            </TD>
                                            <TD style="HEIGHT: 26px" align=left>
                                                <asp:comparevalidator id="CompareValidator2" runat="server" 
                    Font-Names="Arial Narrow" Width="209px" Font-Size="X-Small" 
                    ErrorMessage="La clave de usario y la confirmacion no coinciden" 
                    ControlToValidate="Tbx_ClaveNueva" ControlToCompare="Tbx_ClaveConf" CssClass="style7"></asp:comparevalidator>
                                            </TD>
                                        </TR>
                                    </TBODY>
                                </TABLE>
                            </Template>
                        </igmisc:WebPanel>
					</TD>
				</TR>
				<TR>
					<TD style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat"
						align="center" class="style10">
                        <igmisc:WebPanel ID="WebPanel4" runat="server" EnableAppStyling="True" 
                            Height="96px" StyleSetName="Caribbean" Width="606px">
                            <Header Text="Alertas via Correo electrónico">
                            </Header>
                            <Template>
                                <TABLE style="WIDTH: 607px; FONT-FAMILY: arial; HEIGHT: 62px" id="Table4" 
    cellSpacing=2 cellPadding=1 align=left border=0>
                                    <TBODY>
                                        <TR>
                                            <TD align=left class="style8">
                                                <asp:CheckBox ID="Cbx_Retardos" runat="server" 
                                                    Text="Enviar Retardos" />
                                            </TD>
                                            <TD align=left class="style8">
                                                <asp:CheckBox ID="Cbx_Faltas" runat="server" 
                                                    Text="Enviar Faltas" />
                                            </TD>
                                        </TR>
                                        <TR style="COLOR: #000000">
                                            <TD align=left class="style8">
                                                <asp:CheckBox ID="Cbx_SolicitudPermisos" runat="server" 
                                                    Text="Enviar solicitud de Permisos o Justificaciones" />
                                            </TD>
                                            <TD align=left class="style8">
                                                <asp:CheckBox ID="Cbx_TerminalNoResponde" runat="server" 
                                                    Text="Enviar Terminal no Responde" />
                                            </TD>
                                        </TR>
                                        <TR>
                                            <TD align=left class="style8">
                                                &nbsp;</TD>
                                            <TD align=left>
                                            </TD>
                                        </TR>
                                    </TBODY>
                                </TABLE>
                            </Template>
                        </igmisc:WebPanel>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center"><asp:label id="LError" runat="server" Font-Names="Arial Narrow" ForeColor="#CC0033" Font-Size="Smaller"></asp:label><asp:label id="LCorrecto" runat="server" Font-Names="Arial Narrow" ForeColor="#00C000" Font-Size="Smaller"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" class="style1">&nbsp;&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BDeshacerCambios" runat="server" Height="22px" 
                            UseBrowserDefaults="False" Text="Deshacer Cambios" ImageTextSpacing="4" 
                            CausesValidation="False" onclick="BDeshacerCambios_Click">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"></Image>

<ButtonStyle Cursor="Default"></ButtonStyle>
							</Appearance>
                            <HoverAppearance>
<ButtonStyle BackColor="#F9DA9B"></ButtonStyle>

                                <InnerBorder ColorBottom="33, 27, 96" ColorLeft="33, 27, 96" ColorRight="33, 27, 96"
                                    ColorTop="33, 27, 96" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                    StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                <Style BackColor="#F9DA9B"></Style>
                            </HoverAppearance>
                            <FocusAppearance>
<ButtonStyle BackColor="#FCE6AB"></ButtonStyle>

                                <InnerBorder ColorBottom="33, 27, 96" ColorLeft="33, 27, 96" ColorRight="33, 27, 96"
                                    ColorTop="33, 27, 96" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                    StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                <Style BackColor="#FCE6AB"></Style>
                            </FocusAppearance>
                            <PressedAppearance>
<ButtonStyle BackColor="#F79646"></ButtonStyle>

                                <InnerBorder ColorBottom="33, 27, 96" ColorLeft="33, 27, 96" ColorRight="33, 27, 96"
                                    ColorTop="33, 27, 96" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid"
                                    StyleTop="Solid" WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                <Style BackColor="#F79646"></Style>
                            </PressedAppearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BGuardarCambios" runat="server" Height="22px" 
                            UseBrowserDefaults="False" Text="Guardar Cambios" ImageTextSpacing="4" 
                            onclick="BGuardarCambios_Click">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton></TD>
				</TR>
			</TABLE>
    <div>
    
    </div>
    </form>
</body>
</html>
