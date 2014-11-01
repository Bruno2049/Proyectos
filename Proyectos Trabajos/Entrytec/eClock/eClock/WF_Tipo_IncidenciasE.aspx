<%@ Page language="c#"  CodeFile="WF_Tipo_IncidenciasE.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Tipo_IncidenciasE" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edicion de Incidencias</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				<TR>
					<TD style="HEIGHT: 0%">
						</TD>
				</TR>
				<TR>
					<TD align="center" style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat">
						<DIV align="center">
							<TABLE id="Table2" style="WIDTH: 592px; HEIGHT: 86px" cellSpacing="1" cellPadding="1" width="592"
								border="0" align="center">
								<TR>
									<TD style="FONT-SIZE: 11pt; WIDTH: 138px; FONT-FAMILY: Arial" align="left"><FONT face="Arial Narrow">Id. 
											Incidencia</FONT></TD>
									<TD style="WIDTH: 210px" align="left">
										<igtxt:WebNumericEdit id="TipoIncidenciaId" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="200px"
											BorderColor="#7F9DB9" UseBrowserDefaults="False" CellSpacing="1" Enabled="False" Font-Names="Arial Narrow">
											<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
												<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
												<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
												<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC"></ButtonStyle>
												<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
											</ButtonsAppearance>
											<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px"></SpinButtons>
										</igtxt:WebNumericEdit><FONT face="Arial Narrow"></FONT></TD>
									<TD align="left">
										<asp:RequiredFieldValidator id="RVTipoIncidenciaId" runat="server" ErrorMessage="Este dato es obligatorio" ControlToValidate="TipoIncidenciaId"
											Font-Names="Arial Narrow" Font-Size="Smaller"></asp:RequiredFieldValidator><FONT face="Arial Narrow"></FONT></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; WIDTH: 138px; FONT-FAMILY: Arial" align="left"><FONT face="Arial Narrow">Nombre</FONT></TD>
									<TD style="WIDTH: 210px" align="left">
										<igtxt:WebTextEdit id="TipoIncidenciaNombre" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="200px"
											BorderColor="#7F9DB9" UseBrowserDefaults="False" CellSpacing="1" Font-Names="Arial Narrow">
											<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
												<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
												<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
												<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC"></ButtonStyle>
												<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
											</ButtonsAppearance>
										</igtxt:WebTextEdit><FONT face="Arial Narrow"></FONT></TD>
									<TD align="left">
										<asp:RequiredFieldValidator id="RVTipoIncidenciaNombre" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow"
											ErrorMessage="Este dato es obligatorio" ControlToValidate="TipoIncidenciaNombre"></asp:RequiredFieldValidator><FONT face="Arial Narrow"></FONT></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 11pt; WIDTH: 138px; FONT-FAMILY: Arial" align="left"><FONT face="Arial Narrow">Abreviatura</FONT></TD>
									<TD style="WIDTH: 210px" align="left">
										<igtxt:WebTextEdit id="IncidenciaAbreviatura" runat="server" CellSpacing="1" UseBrowserDefaults="False"
											BorderColor="#7F9DB9" Width="200px" BorderWidth="1px" BorderStyle="Solid" MaxLength="2" Font-Names="Arial Narrow">
											<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
												<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
												<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
												<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC"></ButtonStyle>
												<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
											</ButtonsAppearance>
										</igtxt:WebTextEdit><FONT face="Arial Narrow"></FONT></TD>
									<TD align="left">
										<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="IncidenciaAbreviatura"
											ErrorMessage="Este dato es obligatorio" Font-Names="Arial Narrow" Font-Size="Smaller"></asp:RequiredFieldValidator><FONT face="Arial Narrow"></FONT></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 138px" align="left"></TD>
									<TD style="WIDTH: 210px" align="left">
										<asp:CheckBox id="CBBorrar" runat="server" Font-Size="Small" Font-Names="Arial Narrow" Text="Borrar Incidencia"></asp:CheckBox></TD>
									<TD style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Arial" align="left">
										<asp:Label id="LBorrar" runat="server" Font-Names="Arial Narrow">* Seleccione esta opción si lo que quiere es borrar la incidencia</asp:Label></TD>
								</TR>
							</TABLE>
						</DIV>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Size="Smaller" Font-Names="Arial Narrow"></asp:Label>
						<asp:Label id="LCorrecto" runat="server" ForeColor="#00C000" Font-Size="Smaller" Font-Names="Arial Narrow"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">&nbsp;&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BDeshacerCambios" runat="server" UseBrowserDefaults="False" Text="Deshacer Cambios"
							Height="22px" Width="150px" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BGuardarCambios" runat="server" UseBrowserDefaults="False" Text="Guardar Cambios"
							Height="22px" Width="150px" ImageTextSpacing="4">
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

