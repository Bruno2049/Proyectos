<%@ Register Assembly="Infragistics2.WebUI.WebScheduleDataProvider.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Data" TagPrefix="ig_scheduledata" %>
<%@ Register Assembly="Infragistics2.WebUI.WebSchedule.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="ig_sched" %>
<%@ Register TagPrefix="uc1" TagName="WC_Horario" Src="WC_Horario.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_HorarioPersona.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_HorarioPersona" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" style="FONT-FAMILY: Arial" height="97%" cellSpacing="1" cellPadding="1"
				width="99%" border="0">
				<TR>
					<TD>
						</TD>
				</TR>
				<TR>
					<TD height="100%" style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat">
						<TABLE id="Table3" style="WIDTH: 100%" cellSpacing="1" cellPadding="1" width="300" border="0">
							<TR>
								<TD align="left">
									<P align="right">
										<asp:Label id="Label1" runat="server">Semana</asp:Label></P>
								</TD>
								<TD align="left">
									<igcmbo:WebCombo id=CSemana runat="server" ForeColor="Black" Width="179px" Height="22px" BackColor="White" Version="3.00"   SelBackColor="17, 69, 158"   BorderStyle="Solid" BorderWidth="1px" BorderColor="LightGray" DataSource="<%# dS_HorarioPersona1 %>" DataMember="Semanas" DataTextField="DESCRIPCION" DataValueField="DIAS_TRABAJO" OnInitializeLayout="CSemana_InitializeLayout">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="DIAS_TRABAJO" Key="DIAS_TRABAJO" IsBound="True" Hidden="True" DataType="System.DateTime"
												BaseColumnName="DIAS_TRABAJO">
												<Footer Key="DIAS_TRABAJO"></Footer>
												<Header Key="DIAS_TRABAJO" Caption="DIAS_TRABAJO"></Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="DESCRIPCION" Key="DESCRIPCION" IsBound="True" BaseColumnName="DESCRIPCION">
												<Footer Key="DESCRIPCION">
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="DESCRIPCION" Caption="DESCRIPCION">
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
										</Columns>
										<DropDownLayout DropdownWidth="179px" BorderCollapse="Separate" RowHeightDefault="20px" HeaderClickAction="Select"
											DropdownHeight="350px" ColWidthDefault="179px">
											<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
												<Padding Left="3px"></Padding>
												<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
											</RowStyle>
											<SelectedRowStyle ForeColor="WhiteSmoke" BackColor="Sienna" ></SelectedRowStyle>
											<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
												ForeColor="White" BackColor="Navy" >
												<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
											</HeaderStyle>
											<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
											<FrameStyle Width="179px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
												BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="350px"></FrameStyle>
										</DropDownLayout>
										<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
									</igcmbo:WebCombo></TD>
								<TD style="WIDTH: 11px" align="left">
									<P align="right">
										<asp:Label id="Label2" runat="server">Año</asp:Label></P>
								</TD>
								<TD align="left">
									<igcmbo:WebCombo id=CAno runat="server" ForeColor="Black" Width="96px" Height="22px" BackColor="White" Version="3.00"   SelBackColor="17, 69, 158"   BorderStyle="Solid" BorderWidth="1px" BorderColor="LightGray" SelectedIndex="0" DataSource="<%# dS_HorarioPersona1 %>" DataMember="Anos" DataTextField="ANOS" DataValueField="ANOS" OnInitializeLayout="CAno_InitializeLayout">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="ANOS" Key="ANOS" IsBound="True" BaseColumnName="ANOS">
												<Footer Key="ANOS"></Footer>
												<Header Key="ANOS" Caption="ANOS"></Header>
											</igtbl:UltraGridColumn>
										</Columns>
										<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" HeaderClickAction="Select" ColWidthDefault="150px">
											<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
												<Padding Left="3px"></Padding>
												<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
											</RowStyle>
											<SelectedRowStyle ForeColor="WhiteSmoke" BackColor="Sienna" ></SelectedRowStyle>
											<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
												ForeColor="White" BackColor="Navy" >
												<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
											</HeaderStyle>
											<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
											<FrameStyle Width="325px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
												BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
										</DropDownLayout>
										<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
									</igcmbo:WebCombo></TD>
							</TR>
						</TABLE>
						<TABLE id="Table2" style="WIDTH: 100%; HEIGHT: 339px" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD style="WIDTH: 50%"></TD>
								<TD>
									<uc1:WC_Horario id="WC_Horario1" runat="server"></uc1:WC_Horario></TD>
								<TD>
									<uc1:WC_Horario id="WC_Horario2" runat="server"></uc1:WC_Horario></TD>
								<TD>
									<uc1:WC_Horario id="WC_Horario3" runat="server"></uc1:WC_Horario></TD>
								<TD>
									<uc1:WC_Horario id="WC_Horario4" runat="server"></uc1:WC_Horario></TD>
								<TD style="WIDTH: 50%"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD>
									<uc1:WC_Horario id="WC_Horario5" runat="server"></uc1:WC_Horario></TD>
								<TD>
									<uc1:WC_Horario id="WC_Horario6" runat="server"></uc1:WC_Horario></TD>
								<TD>
									<uc1:WC_Horario id="WC_Horario7" runat="server"></uc1:WC_Horario></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD height="100%">
						<P align="center"><asp:label id="LCorrecto" runat="server" ForeColor="Green"></asp:label><asp:label id="LError" runat="server" ForeColor="Red"></asp:label></P>
					</TD>
				</TR>
				<TR>
					<TD height="100%">
						<P align="center">&nbsp;&nbsp;&nbsp;&nbsp;
							<igtxt:WebImageButton id="Button2" runat="server" Height="22px" Text="Regresar" UseBrowserDefaults="False"
								Width="150px" ImageTextSpacing="4">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
									<Image Url="./Imagenes/Back.png" Height="16px" Width="16px"></Image>
								</Appearance>
							</igtxt:WebImageButton>&nbsp;&nbsp;
							<igtxt:WebImageButton id="Button1" runat="server" Height="22px" Text="Guardar Cambios" UseBrowserDefaults="False"
								Width="150px" ImageTextSpacing="4">
								<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
								<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
									RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
								<Appearance>
									<Style Cursor="Default">
									</Style>
									<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
								</Appearance>
							</igtxt:WebImageButton>
						</P>
					</TD>
				</TR>
</asp:Content>