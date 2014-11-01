<%@ Page Language="C#" AutoEventWireup="true"   CodeFile="WF_UsuariosEd.aspx.cs" Inherits="WF_UsuariosEd" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<!DOCTYPE html">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edicion de Incidencias</title>
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
        .style2
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
        }
        .style3
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            width: 124px;
        }
        .style4
        {
            font-family: Arial, Helvetica, sans-serif;
        }
        .style6
        {
            font-size: x-small;
        }
        .style7
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: x-small;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				<TR>
					<TD style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat"
						align="center">
						<DIV align="center">
						</DIV>
                        <igmisc:WebPanel ID="WebPanel3" runat="server" EnableAppStyling="True" 
                            Height="276px" StyleSetName="Caribbean" Width="606px">
                            <Header Text="Datos del Usuario">
                            </Header>
                            <Template>
                                <TABLE style="WIDTH: 607px; FONT-FAMILY: arial; HEIGHT: 183px" id="Table3" 
                                    cellSpacing=2 cellPadding=1 align=left border=0>
                                    <TBODY>
                                        <TR>
                                            <TD style="WIDTH: 124px; " align=left class="style2">
                                                Perfil</TD>
                                            <TD style="WIDTH: 166px" align=left>
                                                <igcmbo:webcombo id="Wco_EC_PERFILES" runat="server" BorderColor="Silver" 
                                                    BackColor="White" ForeColor="Black" BorderWidth="1px" 
                                                    BorderStyle="Solid" Version="4.00" 
                                                    SelBackColor="DarkBlue" SelForeColor="White">
                                                    <Columns>
                                                        <igtbl:UltraGridColumn>
                                                            <header caption="Column0">
                                                            </header>
                                                        </igtbl:UltraGridColumn>
                                                    </Columns>
                                                    <ExpandEffects ShadowColor="LightGray">
                                                    </ExpandEffects>
                                                    <dropdownlayout bordercollapse="Separate" rowheightdefault="20px" 
                                                        version="4.00">
                                                        <framestyle backcolor="Silver" borderstyle="Ridge" borderwidth="2px" 
                                                            cursor="Default" font-names="Verdana" font-size="10pt" height="130px" 
                                                            width="325px">
                                                        </framestyle>
                                                        <HeaderStyle BackColor="LightGray" BorderStyle="Solid">
                                                        <borderdetails colorleft="White" colortop="White" widthleft="1px" 
                                                            widthtop="1px" />
                                                        </HeaderStyle>
                                                        <RowStyle BackColor="White" BorderColor="Gray" BorderStyle="Solid" 
                                                            BorderWidth="1px">
                                                        <borderdetails widthleft="0px" widthtop="0px" />
                                                        </RowStyle>
                                                        <SelectedRowStyle BackColor="DarkBlue" ForeColor="White" />
                                                    </dropdownlayout>
                                                </igcmbo:webcombo>
                                            </TD>
                                            <TD align=left class="style7">
                                                <FONT class="style6">Seleccione Perfil de Usuario</FONT>
                                            </TD>
                                        </TR>
                                        <TR>
                                            <TD style="WIDTH: 124px; " align=left class="style2">
                                                Usuario</TD>
                                            <TD style="WIDTH: 166px" align=left>
                                                <igtxt:webtextedit id="Wtx_USUARIO_USUARIO" runat="server" BorderColor="#7F9DB9" 
                    Font-Names="Arial Narrow" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
                    UseBrowserDefaults="False" CellSpacing="1">
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
                                                <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" 
                    Font-Names="Arial Narrow" Font-Size="Smaller" 
                    ErrorMessage="El nombre de Usuario es obligatorio" 
                    ControlToValidate="Wtx_USUARIO_USUARIO" CssClass="style7"></asp:requiredfieldvalidator>
                                            </TD>
                                        </TR>
                                        <TR style="COLOR: #000000">
                                            <TD style="WIDTH: 124px; " align=left class="style2">
                                                Nombre</TD>
                                            <TD style="WIDTH: 166px" align=left>
                                                <igtxt:webtextedit id="Wtx_USUARIO_NOMBRE" runat="server" BorderColor="#7F9DB9" 
                    Font-Names="Arial Narrow" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
                    UseBrowserDefaults="False" CellSpacing="1">
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
                                                <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" 
                    Font-Names="Arial Narrow" Font-Size="Smaller" 
                    ErrorMessage="El nombre es un Dato obligatorio" 
                    ControlToValidate="Wtx_USUARIO_NOMBRE" CssClass="style7"></asp:requiredfieldvalidator>
                                            </TD>
                                        </TR>
                                        <TR>
                                            <TD align=left class="style3">
                                                Descripción&nbsp;</TD>
                                            <TD style="WIDTH: 166px" align=left>
                                                <igtxt:webtextedit id="Wtx_USUARIO_DESCRIPCION" runat="server" BorderColor="#7F9DB9" 
                    Font-Names="Arial Narrow" BorderWidth="1px" BorderStyle="Solid" Width="200px" 
                    UseBrowserDefaults="False" CellSpacing="1">
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
                                            </TD>
                                        </TR>
                                        <tr>
                                            <td align="left" style="width: 124px; " class="style2">
                                                Correo Electronico</td>
                                            <td align="left" style="width: 166px">
                                                <igtxt:webtextedit id="Wtx_USUARIO_EMAIL" runat="server" BorderColor="#7F9DB9" 
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
                                            </td>
                                        </tr>
                                        <TR>
                                            <TD style="WIDTH: 124px; TEXT-ALIGN: left" 
                align=left class="style2">
                                                Usuario Clave</TD>
                                            <TD style="WIDTH: 166px; " align=left>
                                                <igtxt:webtextedit id="Wtx_USUARIO_CLAVE" runat="server" BorderColor="#7F9DB9" 
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                    ControlToValidate="Wtx_USUARIO_CLAVE" CssClass="style7" 
                                                    ErrorMessage="Usuario Clave es un dato obligatorio" Font-Names="Arial Narrow" 
                                                    Font-Size="Smaller"></asp:RequiredFieldValidator>
                                            </TD>
                                        </TR>
                                        <tr>
                                            <td align="left" class="style2" 
                                                style="WIDTH: 124px; HEIGHT: 26px; TEXT-ALIGN: left">
                                                Confirma Clave</td>
                                            <td align="left" style="WIDTH: 166px; HEIGHT: 26px">
                                                <igtxt:WebTextEdit ID="Wtx_USUARIO_CLAVEC" runat="server" BorderColor="#7F9DB9" 
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
                                            <td align="left" style="HEIGHT: 26px">
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                    ControlToCompare="Wtx_USUARIO_CLAVEC" ControlToValidate="Wtx_USUARIO_CLAVE" 
                                                    CssClass="style7" 
                                                    ErrorMessage="La clave de usario y la confirmacion no coinciden" 
                                                    Font-Names="Arial Narrow" Font-Size="X-Small" Width="209px"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <TR>
                                            <TD style="WIDTH: 124px; height: 26px; text-align: left;" align=left 
                                                class="style2">
                                                &nbsp;</TD>
                                            <TD style="WIDTH: 166px; height: 26px;" align=left>
                                                <igcmbo:WebCombo ID="Wco_USUARIO_AGRUPACION" runat="server" BackColor="White" 
                                                    BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" 
                                                    SelBackColor="DarkBlue" SelForeColor="White" Version="4.00" Visible="False">
                                                    <Columns>
                                                        <igtbl:UltraGridColumn>
                                                            <header caption="Column0">
                                                            </header>
                                                        </igtbl:UltraGridColumn>
                                                    </Columns>
                                                    <expandeffects shadowcolor="LightGray" />
                                                    <dropdownlayout bordercollapse="Separate" rowheightdefault="20px" 
                                                        version="4.00">
                                                        <framestyle backcolor="Silver" borderstyle="Ridge" borderwidth="2px" 
                                                            cursor="Default" font-names="Verdana" font-size="10pt" height="130px" 
                                                            width="325px">
                                                        </framestyle>
                                                        <HeaderStyle BackColor="LightGray" BorderStyle="Solid">
                                                        <borderdetails colorleft="White" colortop="White" widthleft="1px" 
                                                            widthtop="1px" />
                                                        </HeaderStyle>
                                                        <RowStyle BackColor="White" BorderColor="Gray" BorderStyle="Solid" 
                                                            BorderWidth="1px">
                                                        <borderdetails widthleft="0px" widthtop="0px" />
                                                        </RowStyle>
                                                        <SelectedRowStyle BackColor="DarkBlue" ForeColor="White" />
                                                    </dropdownlayout>
                                                </igcmbo:WebCombo>
                                            </TD>
                                            <TD style="height: 26px;" align=left class="style7">
                                                &nbsp;</TD>
                                        </TR>
                                        <tr>
                                            <td align="left" style="WIDTH: 124px" class="style2">
                                                &nbsp;</td>
                                            <td align="left" style="WIDTH: 166px">
                                                &nbsp;</td>
                                            <td align="left" class="style7" style="COLOR: red; ">
                                                &nbsp;</td>
                                        </tr>
                                        <TR>
                                            <TD style="WIDTH: 124px; " 
                align=left>
                                            </TD>
                                            <TD style="WIDTH: 166px; " align=left>
                                                <asp:CheckBox ID="Chb_USUARIO_BORRADO" runat="server" Font-Names="Arial Narrow" 
                                                    Font-Size="Small" Text="Borrar Usuario" />
                                            </TD>
                                            <TD style="COLOR: red; font-family: Arial;" align=left class="style6">
                                                <asp:Label ID="LBorrar" runat="server" CssClass="style4" 
                                                    Font-Names="Arial Narrow">* Seleccione esta opción si lo que quiere es borrar el usuario</asp:Label>
                                            </TD>
                                        </TR>
                                        <tr>
                                            <td align="left" style="WIDTH: 124px; HEIGHT: 28px">
                                            </td>
                                            <td align="left" style="WIDTH: 166px; HEIGHT: 28px" valign="bottom">
                                                &nbsp;</td>
                                            <td align="left" class="style7" style="COLOR: black; HEIGHT: 28px" 
                                                valign="bottom">
                                                &nbsp;</td>
                                        </tr>
                                    </TBODY>
                                </TABLE>
                            </Template>
                        </igmisc:WebPanel>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center"><asp:label id="Lbl_Error" runat="server" Font-Names="Arial Narrow" ForeColor="#CC0033" Font-Size="Smaller"></asp:label><asp:label id="Lbl_Correcto" runat="server" Font-Names="Arial Narrow" ForeColor="#00C000" Font-Size="Smaller"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" class="style1">&nbsp;&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="WIBtn_Deshacer" runat="server" Height="22px" 
                            UseBrowserDefaults="False" Text="Deshacer Cambios" ImageTextSpacing="4" 
                            CausesValidation="False" onclick="WIBtn_Deshacer_Click">
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
						<igtxt:webimagebutton id="WIBtn_Guardar" runat="server" Height="22px" 
                            UseBrowserDefaults="False" Text="Guardar Cambios" ImageTextSpacing="4" 
                            onclick="WIBtn_Guardar_Click">
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
	 </div>
    </form>
</body>
</html>

