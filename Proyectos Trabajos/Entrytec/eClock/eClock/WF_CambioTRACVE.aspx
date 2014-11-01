<%@ Page language="c#" MasterPageFile="~/MasterPage.master" CodeFile="WF_CambioTRACVE.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_CambioTRACVE" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1">
				<TR>
					<TD style="WIDTH: 0%; HEIGHT: 0%">
						</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; HEIGHT: 100%" align="center">
						<asp:Panel id="Panel1" runat="server">
							
								<TABLE id="Table3" style="WIDTH: 455px; HEIGHT: 300px" cellSpacing="1" cellPadding="1"
									width="455" border="0">
									<TR>
										<TD style="WIDTH: 219px; HEIGHT: 58px" align="center">
											<asp:Label id="Label1" runat="server" Width="192px">No. Empleado (TRACVE) Actual</asp:Label></TD>
										<TD style="HEIGHT: 58px" align="center">
											<asp:Label id="Label2" runat="server" Width="192px">No. Empleado (TRACVE)  Nuevo</asp:Label></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 219px; HEIGHT: 207px" align="center">
											<asp:Label id="Label3" runat="server"></asp:Label></TD>
										<TD style="HEIGHT: 207px" align="center">
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
											<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Debe de poner un TRACVE Válido"
												Font-Size="Smaller" Font-Names="Arial" ControlToValidate="TextBox1"></asp:RequiredFieldValidator></TD>
									</TR>
									<TR>
										<TD align="center"></TD>
										<TD align="center">
											<igtxt:webimagebutton id="Enviar_Reporte" runat="server" Width="146px" Height="26px" UseBrowserDefaults="False"
												Text="Guardar Cambio" ImageTextSpacing="4">
												<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
												<RoundedCorners MaxHeight="40" ImageUrl="ig_butCRM1.gif" MaxWidth="400" HoverImageUrl="ig_butCRM2.gif"
													RenderingType="FileImages" HeightOfBottomEdge="2" PressedImageUrl="ig_butCRM2.gif" WidthOfRightEdge="2"></RoundedCorners>
												<Appearance>
													<Image Url="./Imagenes/Save.png" Height="16px" Width="16px"></Image>
												</Appearance>
											</igtxt:webimagebutton></TD>
									</TR>
								</TABLE>
							
						</asp:Panel></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 0%; HEIGHT: 0%" align="center">
						<asp:label id="LCorrecto" runat="server" Font-Size="Smaller" Font-Names="Arial" ForeColor="#00C000"></asp:label>
						<asp:label id="LError" runat="server" Font-Size="Smaller" Font-Names="Arial" ForeColor="#CC0033"></asp:label></TD>
				</TR>
			</TABLE>
	
</asp:Content>