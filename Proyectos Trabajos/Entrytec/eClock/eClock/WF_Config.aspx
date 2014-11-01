<%@ Page language="c#"  CodeFile="WF_Config.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Config" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<%@ Register Assembly="Infragistics2.WebUI.WebNavBar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebNavBar" TagPrefix="ignavbar" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebListbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebListbar" TagPrefix="iglbar" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Configuracion</title>
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				<TR>
					<TD align="center" style="BACKGROUND-POSITION: right center; BACKGROUND-IMAGE: url(./Imagenes/fondoeClock3.jpg); BACKGROUND-REPEAT: no-repeat">
                        <br />
                        <igmisc:WebPanel ID="WebPanel2" runat="server" BackColor="White" BorderColor="SteelBlue"
                            BorderStyle="Outset" BorderWidth="2px" Font-Bold="False" 
                            Font-Names="ARIAL" ForeColor="Black"
                            StyleSetName="Caribbean" EnableAppStyling="True" Width="653px">
                            <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Black">
                                <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                                <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                    ColorTop="0, 45, 150" />
                            </PanelStyle>
                            <Header TextAlignment="Left" Text="Sincronizacion de Huellas">
                            </Header>
                            <Template>
                                <TABLE id="Table2" style="WIDTH: 633px; FONT-FAMILY: 'Arial Narrow'; HEIGHT: 2px" cellSpacing="1"
								cellPadding="3" width="633" border="0">
                                    <tr>
									<TD align="left">
										</TD>
									<TD align="left" style="WIDTH: 210px;">
										<igtxt:WebNumericEdit id="TSincTime" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="200px"
											Height="21px" BorderColor="#7B9EBD" Font-Names="Tahoma" Font-Size="8pt" Visible="False"></igtxt:WebNumericEdit></TD>
									<TD style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Arial;" align="left">
										<asp:Label id="LBorrar" runat="server" Font-Names="Arial Narrow" Visible="False">Los minutos entre cada sincronización es un dato requerido</asp:Label></TD>
                                    </tr>
                                    <tr>
									<TD style="HEIGHT: 25px" align="left">Sincronizacion de Huellas</TD>
									<TD style="WIDTH: 210px; HEIGHT: 25px" align="left">
										<igtxt:WebDateTimeEdit id="TEInicio" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="120px"
											BorderColor="#7F9DB9" UseBrowserDefaults="False" CellSpacing="1" DisplayModeFormat="t" EditModeFormat="t">
											<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
												<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
												<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
												<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC"></ButtonStyle>
												<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
											</ButtonsAppearance>
											<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px"></SpinButtons>
										</igtxt:WebDateTimeEdit></TD>
									<TD style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Arial; HEIGHT: 25px" align="left">*Hora de 
										inicio</TD>
                                    </tr>
                                    <tr>
									<TD style="HEIGHT: 25px" align="left">Sincronizacion de Huellas</TD>
									<TD style="WIDTH: 210px; HEIGHT: 25px" align="left">
										<igtxt:WebDateTimeEdit id="TEFin" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="120px" BorderColor="#7F9DB9"
											UseBrowserDefaults="False" CellSpacing="1" DisplayModeFormat="t" EditModeFormat="t">
											<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
												<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
												<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
												<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
													BackColor="#C5D5FC"></ButtonStyle>
												<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
											</ButtonsAppearance>
											<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px"></SpinButtons>
										</igtxt:WebDateTimeEdit></TD>
									<TD style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Arial; HEIGHT: 25px" align="left">*Hora de 
										fin</TD>
                                    </tr>
                                    <tr>
									<TD style="HEIGHT: 25px" align="left">
										<asp:RadioButton id="RD1" runat="server" Width="176px" Text="Grupo 1" GroupName="N1"></asp:RadioButton></TD>
									<TD style="WIDTH: 210px; HEIGHT: 25px" align="left">
										<asp:TextBox id="TextBox1" runat="server" Width="200px"></asp:TextBox></TD>
									<TD style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Arial; HEIGHT: 25px" align="left">
										<igcmbo:WebCombo id="WCGpo1" runat="server" BorderStyle="Solid" BorderWidth="1px" BackColor="White"
											Width="192px" ForeColor="Black" Height="22px" BorderColor="LightGray" Version="3.00" Font-Names="Arial Narrow"
											Font-Size="9pt"  
											SelBackColor="17, 69, 158"   >
											<Rows>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.1"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.2"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.3"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.4"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
											</Rows>
											<Columns>
												<igtbl:UltraGridColumn HeaderText="TERMINAL_ID" Key="TERMINAL_ID" IsBound="True" DataType="System.Decimal"
													BaseColumnName="TERMINAL_ID">
													<Footer Key="TERMINAL_ID"></Footer>
													<Header Key="TERMINAL_ID" Caption="TERMINAL_ID"></Header>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="TERMINAL_NOMBRE" Key="TERMINAL_NOMBRE" IsBound="True" BaseColumnName="TERMINAL_NOMBRE">
													<Footer Key="TERMINAL_NOMBRE">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Footer>
													<Header Key="TERMINAL_NOMBRE" Caption="TERMINAL_NOMBRE">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Header>
												</igtbl:UltraGridColumn>
											</Columns>
											<DropDownLayout DropdownWidth="300px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
												HeaderClickAction="Select" GridLines="None" ColWidthDefault="200px">
												<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="White" BackColor="Sienna" ></SelectedRowStyle>
												<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
													ForeColor="White" BackColor="Navy" >
													<BorderDetails ColorTop="173, 197, 235" WidthLeft="1px" StyleBottom="Solid" ColorBottom="DarkGray"
														WidthTop="1px" StyleTop="Outset" WidthBottom="2px" ColorLeft="173, 197, 235"></BorderDetails>
												</HeaderStyle>
												<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
												<FrameStyle Width="300px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
													BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:WebCombo></TD>
                                    </tr>
                                    <tr>
									<TD style="HEIGHT: 32px" align="left">
										<asp:RadioButton id="RD2" runat="server" Width="176px" Text="Grupo 2" GroupName="N1"></asp:RadioButton></TD>
									<TD style="WIDTH: 210px; HEIGHT: 32px" align="left">
										<asp:TextBox id="TextBox2" runat="server" Width="200px"></asp:TextBox></TD>
									<TD style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Arial; HEIGHT: 32px" align="left">
										<igcmbo:WebCombo id="WCGpo2" runat="server" BorderStyle="Solid" BorderWidth="1px" BackColor="White"
											Width="192px" ForeColor="Black" Height="22px" BorderColor="LightGray" Version="3.00" Font-Names="Arial Narrow"
											Font-Size="9pt"  
											SelBackColor="17, 69, 158"   >
											<Rows>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.1"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.2"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.3"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.4"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
											</Rows>
											<Columns>
												<igtbl:UltraGridColumn HeaderText="TERMINAL_ID" Key="TERMINAL_ID" IsBound="True" DataType="System.Decimal"
													BaseColumnName="TERMINAL_ID">
													<Footer Key="TERMINAL_ID"></Footer>
													<Header Key="TERMINAL_ID" Caption="TERMINAL_ID"></Header>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="TERMINAL_NOMBRE" Key="TERMINAL_NOMBRE" IsBound="True" BaseColumnName="TERMINAL_NOMBRE">
													<Footer Key="TERMINAL_NOMBRE">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Footer>
													<Header Key="TERMINAL_NOMBRE" Caption="TERMINAL_NOMBRE">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Header>
												</igtbl:UltraGridColumn>
											</Columns>
											<DropDownLayout DropdownWidth="300px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
												HeaderClickAction="Select" GridLines="None" ColWidthDefault="200px">
												<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="White" BackColor="Sienna" ></SelectedRowStyle>
												<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
													ForeColor="White" BackColor="Navy" >
													<BorderDetails ColorTop="173, 197, 235" WidthLeft="1px" StyleBottom="Solid" ColorBottom="DarkGray"
														WidthTop="1px" StyleTop="Outset" WidthBottom="2px" ColorLeft="173, 197, 235"></BorderDetails>
												</HeaderStyle>
												<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
												<FrameStyle Width="300px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
													BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:WebCombo></TD>
                                    </tr>
                                    <tr>
									<TD style="HEIGHT: 25px" align="left">
										<asp:RadioButton id="RD3" runat="server" Width="176px" Text="Grupo 3" GroupName="N1"></asp:RadioButton></TD>
									<TD style="WIDTH: 210px; HEIGHT: 25px" align="left">
										<asp:TextBox id="TextBox3" runat="server" Width="200px"></asp:TextBox></TD>
									<TD style="FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: Arial; HEIGHT: 25px" align="left">
										<igcmbo:WebCombo id="WCGpo3" runat="server" BorderStyle="Solid" BorderWidth="1px" BackColor="White"
											Width="192px" ForeColor="Black" Height="22px" BorderColor="LightGray" Version="3.00" Font-Names="Arial Narrow"
											Font-Size="9pt"  
											SelBackColor="17, 69, 158"   >
											<Rows>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.1"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.2"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.3"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.4"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
											</Rows>
											<Columns>
												<igtbl:UltraGridColumn HeaderText="TERMINAL_ID" Key="TERMINAL_ID" IsBound="True" DataType="System.Decimal"
													BaseColumnName="TERMINAL_ID">
													<Footer Key="TERMINAL_ID"></Footer>
													<Header Key="TERMINAL_ID" Caption="TERMINAL_ID"></Header>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="TERMINAL_NOMBRE" Key="TERMINAL_NOMBRE" IsBound="True" BaseColumnName="TERMINAL_NOMBRE">
													<Footer Key="TERMINAL_NOMBRE">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Footer>
													<Header Key="TERMINAL_NOMBRE" Caption="TERMINAL_NOMBRE">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Header>
												</igtbl:UltraGridColumn>
											</Columns>
											<DropDownLayout DropdownWidth="300px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
												HeaderClickAction="Select" GridLines="None" ColWidthDefault="200px">
												<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="White" BackColor="Sienna" ></SelectedRowStyle>
												<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
													ForeColor="White" BackColor="Navy" >
													<BorderDetails ColorTop="173, 197, 235" WidthLeft="1px" StyleBottom="Solid" ColorBottom="DarkGray"
														WidthTop="1px" StyleTop="Outset" WidthBottom="2px" ColorLeft="173, 197, 235"></BorderDetails>
												</HeaderStyle>
												<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
												<FrameStyle Width="300px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
													BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:WebCombo></TD>
                                    </tr>
                                </table>
                            </Template>
                        </igmisc:WebPanel>
                        <br />
                        <igmisc:WebPanel ID="WebPanel1" runat="server" BackColor="White" BorderColor="SteelBlue"
                            BorderStyle="Outset" BorderWidth="2px" Font-Bold="False" 
                            Font-Names="ARIAL" ForeColor="Black"
                            StyleSetName="Caribbean" Width="648px" EnableAppStyling="True">
                            <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Black">
                                <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                    ColorTop="0, 45, 150" />
                                <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                            </PanelStyle>
                            <Header TextAlignment="Left" Text="Asistencias">

                            </Header>
                            <Template>
                                <TABLE id="Table7" style="WIDTH: 633px; FONT-FAMILY: 'Arial Narrow'; HEIGHT: 2px" cellSpacing="1"
								cellPadding="3" width="633" border="0">
                                    <tr>
									<TD style="WIDTH: 132px; HEIGHT: 0%" align="right">
										<asp:CheckBox id="CBRecalcular" runat="server" Text="Recalcular Asistencias" TextAlign="Left" Font-Size="Small"></asp:CheckBox></TD>
									<TD style="HEIGHT: 0%" vAlign="middle" colSpan="2" align="left">
										<TABLE id="Table6" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="1" cellPadding="1" width="360"
											border="0">
											<TR>
												<TD style="WIDTH: 32px">Desde</TD>
												<TD>
													<igsch:WebDateChooser id="FInicial" runat="server">
														<CalendarLayout DayNameFormat="FirstLetter" ShowFooter="False" ShowTitle="False">
                                                            <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" />
                                                            <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                                                            <OtherMonthDayStyle ForeColor="#ACA899" />
                                                            <SelectedDayStyle BackColor="CornflowerBlue" />
                                                            <TitleStyle BackColor="#9EBEF5" />
                                                            <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                                                            <WeekendDayStyle BackColor="#E0E0E0" />
                                                        </CalendarLayout>
													</igsch:WebDateChooser></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 32px">Hasta</TD>
												<TD width="100%">
													<igsch:WebDateChooser id="FFinal" runat="server">
														<CalendarLayout DayNameFormat="FirstLetter" ShowFooter="False" ShowTitle="False">
                                                            <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" />
                                                            <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                                                            <OtherMonthDayStyle ForeColor="#ACA899" />
                                                            <SelectedDayStyle BackColor="CornflowerBlue" />
                                                            <TitleStyle BackColor="#9EBEF5" />
                                                            <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                                                            <WeekendDayStyle BackColor="#E0E0E0" />
                                                        </CalendarLayout>
													</igsch:WebDateChooser></TD>
											</TR>
										</TABLE>
									</TD>
                                    </tr>
                                    <tr>
									<TD style="WIDTH: 132px;" align="right">
										<asp:Label id="Label1" runat="server" Font-Size="Small">Incidencia de Vacaciones</asp:Label></TD>
									<TD vAlign="middle" colSpan="2" align="left">
										<igcmbo:WebCombo id="WebCombo4" runat="server" BorderStyle="Solid" BorderWidth="1px" BackColor="White"
											Width="200px" ForeColor="Black" Height="22px" BorderColor="LightGray" Version="3.00" Font-Names="Arial Narrow"
											Font-Size="9pt"  
											SelBackColor="17, 69, 158"   OnInitializeLayout="WebCombo4_InitializeLayout">
											<Rows>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.1"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.2"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.3"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
												<igtbl:UltraGridRow Height="">
													<Cells>
														<igtbl:UltraGridCell Key="TERMINAL_ID" Text="0.4"></igtbl:UltraGridCell>
														<igtbl:UltraGridCell Key="TERMINAL_NOMBRE" Text="abc"></igtbl:UltraGridCell>
													</Cells>
												</igtbl:UltraGridRow>
											</Rows>
											<Columns>
												<igtbl:UltraGridColumn HeaderText="TERMINAL_ID" Key="TERMINAL_ID" IsBound="True" DataType="System.Decimal"
													BaseColumnName="TERMINAL_ID">
													<Footer Key="TERMINAL_ID"></Footer>
													<Header Key="TERMINAL_ID" Caption="TERMINAL_ID"></Header>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="TERMINAL_NOMBRE" Key="TERMINAL_NOMBRE" IsBound="True" BaseColumnName="TERMINAL_NOMBRE">
													<Footer Key="TERMINAL_NOMBRE">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Footer>
													<Header Key="TERMINAL_NOMBRE" Caption="TERMINAL_NOMBRE">
														<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
													</Header>
												</igtbl:UltraGridColumn>
											</Columns>
											<DropDownLayout DropdownWidth="300px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
												HeaderClickAction="Select" GridLines="None" ColWidthDefault="200px">
												<RowStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="White" BackColor="CornflowerBlue">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="White" BackColor="Sienna" ></SelectedRowStyle>
												<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black" BorderStyle="Solid"
													ForeColor="White" BackColor="Navy" >
													<BorderDetails ColorTop="173, 197, 235" WidthLeft="1px" StyleBottom="Solid" ColorBottom="DarkGray"
														WidthTop="1px" StyleTop="Outset" WidthBottom="2px" ColorLeft="173, 197, 235"></BorderDetails>
												</HeaderStyle>
												<RowAlternateStyle ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyle>
												<FrameStyle Width="300px" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
													BorderColor="Black" BorderStyle="Solid" ForeColor="#759AFD" Height="130px"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:WebCombo></TD>
                                    </tr>
                                </table>
                            </Template>
                        </igmisc:WebPanel>
                        <br />
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Names="Arial" Font-Size="Smaller"></asp:Label>
						<asp:Label id="LCorrecto" runat="server" ForeColor="#00C000" Font-Names="Arial" Font-Size="Smaller"></asp:Label></TD>
				</TR>
				<TR>
					<TD align="center" class="style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<igtxt:WebImageButton id="BDeshacerCambios" runat="server" Height="22px" 
                            UseBrowserDefaults="False" Text="Deshacer Cambios"
							Width="150px" ImageTextSpacing="4" onclick="BDeshacerCambios_Click">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:WebImageButton>&nbsp;&nbsp;&nbsp;
						<igtxt:WebImageButton id="BGuardarCambios" runat="server" Height="22px" 
                            UseBrowserDefaults="False" Text="Guardar Cambios"
							Width="150px" ImageTextSpacing="4" onclick="BGuardarCambios_Click">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:WebImageButton></TD>
				</TR>
			</TABLE>
</div>
    </form>
</body>
</html>
