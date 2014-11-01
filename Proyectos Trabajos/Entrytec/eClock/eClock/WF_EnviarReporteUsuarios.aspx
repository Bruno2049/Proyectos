<%@ Page language="c#" MasterPageFile="~/MasterPage.master" CodeFile="WF_EnviarReporteUsuarios.aspx.cs" AutoEventWireup="True" Inherits="WF_EnviarReporteUsuarios" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" style="WIDTH: 100%; HEIGHT: 100%"
				cellSpacing="1" cellPadding="1" width="364" border="0">
				<TR>
					<TD style="HEIGHT: 0%">
                        <igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                            stylesetname="PaneleClock">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Reporte">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="White" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
<BorderDetails ColorTop="158, 190, 245" WidthBottom="0px" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="1px" Left="4px" Bottom="1px"></Padding>
</Styles>
</ExpandedAppearance>

<HoverAppearance>
<Styles CssClass="igwpHeaderHoverBlue2k7"></Styles>
</HoverAppearance>

<CollapsedAppearance>
<Styles CssClass="igwpHeaderCollapsedBlue2k7"></Styles>
</CollapsedAppearance>

<ExpansionIndicator Height="0px" Width="0px"></ExpansionIndicator>
</Header>
<Template>
<TABLE style="WIDTH: 100%; HEIGHT: 100%" id="Table3" cellSpacing=1 cellPadding=1 width=672 border=0><TBODY><TR><TD style="FONT-SIZE: 10pt; WIDTH: 222px; FONT-FAMILY: 'Arial Narrow'; HEIGHT: 26px" align=left>Seleccione&nbsp;tipo del Reporte</TD><TD style="WIDTH: 322px; HEIGHT: 26px" align=left><asp:dropdownlist id="ReporteDropDownList1" runat="server" Font-Names="Arial Narrow" Width="100%"></asp:dropdownlist></TD><TD style="WIDTH: 125px; HEIGHT: 26px" align=left>&nbsp;&nbsp; </TD><TD style="WIDTH: 146px; HEIGHT: 26px" align=left></TD></TR><TR><TD style="FONT-SIZE: 10pt; WIDTH: 222px; FONT-FAMILY: 'Arial Narrow'; HEIGHT: 22px" align=left>Desde </TD><TD style="WIDTH: 322px; HEIGHT: 22px" align=left><igsch:webdatechooser id="FechaIA" runat="server" Text="Null" Width="136px">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="" ShowNextPrevMonth="False" MaxDate=""
												ShowTitle="False" ShowFooter="False">
												<DayStyle Font-Size="9pt" Font-Names="Arial" BackColor="White"></DayStyle>
												<SelectedDayStyle BackColor="#0054E3"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DayHeaderStyle ForeColor="White" BackColor="#7A96DF"></DayHeaderStyle>
												<TitleStyle BackColor="White"></TitleStyle>
											</CalendarLayout>
										</igsch:webdatechooser></TD><TD style="FONT-SIZE: 10pt; WIDTH: 125px; FONT-FAMILY: 'Arial Narrow'; HEIGHT: 22px" align=left>Hasta </TD><TD style="WIDTH: 146px; HEIGHT: 22px" align=left><igsch:webdatechooser id="FechaFA" runat="server" Text="Null" Width="128px">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="" ShowNextPrevMonth="False" MaxDate=""
												ShowTitle="False" ShowFooter="False">
												<DayStyle Font-Size="9pt" Font-Names="Arial" BackColor="White"></DayStyle>
												<SelectedDayStyle BackColor="#0054E3"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DayHeaderStyle ForeColor="White" BackColor="#7A96DF"></DayHeaderStyle>
												<TitleStyle BackColor="White"></TitleStyle>
											</CalendarLayout>
										</igsch:webdatechooser></TD></TR><TR><TD style="FONT-SIZE: 10pt; FONT-FAMILY: 'Arial Narrow'; HEIGHT: 22px" align=right colSpan=4><asp:CheckBox id="DiasNoLaborablesCheckBox1" runat="server" Text="ver dias no laborables " Font-Size="Small"></asp:CheckBox></TD></TR><TR><TD style="FONT-SIZE: 10pt; FONT-FAMILY: 'Arial Narrow'; HEIGHT: 5px" colSpan=4>Seleccione&nbsp;los Usuario a los que desea enviar el reporte:</TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
                        <br />
                    </TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 100%" align="center"><asp:panel id="Panel1" runat="server" Width="100%">
							<TABLE id="Table2" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="1" cellPadding="1" width="672"
								border="0">
								<TR>
									<TD style="HEIGHT: 200px" vAlign="middle" align="center" colSpan="4">
                                        <igtbl:UltraWebGrid ID="Grid" runat="server" Height="200px" OnInitializeDataSource="Grid_InitializeDataSource"
                                            OnInitializeLayout="Grid_InitializeLayout" Width="325px" style="left: 176px; top: 184px">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                    </AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                HeaderClickActionDefault="SortMulti" Name="UltraWebGrid1" RowHeightDefault="20px"
                                                RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                <GroupByBox>
                                                    <Style BackColor="ActiveBorder" BorderColor="Window"></Style>
                                                </GroupByBox>
                                                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                                </GroupByRowStyleDefault>
                                                <ActivationObject BorderColor="" BorderWidth="">
                                                </ActivationObject>
                                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                </FooterStyleDefault>
                                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                                    <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                                    <Padding Left="3px" />
                                                </RowStyleDefault>
                                                <FilterOptionsDefault>
                                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                        BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                        Font-Size="11px">
                                                        <Padding Left="2px" />
                                                    </FilterOperandDropDownStyle>
                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                    </FilterHighlightRowStyle>
                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                        Font-Size="11px" Height="300px" Width="200px">
                                                        <Padding Left="2px" />
                                                    </FilterDropDownStyle>
                                                </FilterOptionsDefault>
                                                <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Left">
                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                </HeaderStyleDefault>
                                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                </EditCellStyleDefault>
                                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="200px"
                                                    Width="325px">
                                                </FrameStyle>
                                                <Pager MinimumPagesForDisplay="2">
                                                    <Style BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
                                                </Pager>
                                                <AddNewBox Hidden="False">
                                                    <Style BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
                                                </AddNewBox>
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 10pt; FONT-FAMILY: 'Arial Narrow'; height: 68px;" vAlign="middle" align="center"
										colSpan="4">
                                        <br />
                                        <igmisc:webpanel id="Webpanel1" runat="server" backcolor="White" bordercolor="SteelBlue"
                                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                                            stylesetname="PaneleClock">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Correo">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
<BorderDetails ColorTop="158, 190, 245" WidthBottom="0px" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="1px" Left="4px" Bottom="1px"></Padding>
</Styles>
</ExpandedAppearance>

<HoverAppearance>
<Styles CssClass="igwpHeaderHoverBlue2k7"></Styles>
</HoverAppearance>

<CollapsedAppearance>
<Styles CssClass="igwpHeaderCollapsedBlue2k7"></Styles>
</CollapsedAppearance>

<ExpansionIndicator Height="0px" Width="0px"></ExpansionIndicator>
</Header>
<Template>
Escriba el Asusnto del mail que desea enviar<BR /><TABLE style="WIDTH: 100%; HEIGHT: 100%" id="Table4" cellSpacing=1 cellPadding=1 width=672 border=0><TBODY><TR><TD style="FONT-SIZE: 10pt; FONT-FAMILY: 'Arial Narrow'" vAlign=middle align=left colSpan=4>Asunto : <asp:TextBox id="TxbAsunto" runat="server" Width="232px" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Solo se permiten caracteres a - z y numeros  0-9" ControlToValidate="TxbAsunto" Display="Dynamic" ValidationExpression="([.-@-a-zA-Z-0-9- ]*)"></asp:RegularExpressionValidator></TD></TR><TR><TD style="FONT-SIZE: 10pt; FONT-FAMILY: 'Arial Narrow'" vAlign=middle align=left colSpan=4>Escriba algun mensaje que desee anexar al mail&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TD></TR><TR><TD vAlign=middle align=left colSpan=4 style="height: 90px"><P align=center><asp:TextBox id="TxbMensage" runat="server" BorderColor="#0000C0" BackColor="#EEEEEE" Width="647px" Height="200px" TextMode="MultiLine"></asp:TextBox></P></TD></TR><TR><TD style="FONT-SIZE: 10pt; WIDTH: 100%; FONT-FAMILY: 'Arial Narrow'; HEIGHT: 0%" vAlign=middle align=center colSpan=4><asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" ErrorMessage="Solo se permiten caracteres a - z y numeros 0-9" ControlToValidate="TxbMensage" Display="Dynamic" ValidationExpression="([\n-.-a-zA-Z-0-9- ]*)"></asp:RegularExpressionValidator></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
                                        <br />
                                    </TD>
								</TR>
							</TABLE>
						</asp:panel></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center"><asp:label id="LError" runat="server" Font-Names="Arial Narrow" ForeColor="#CC0033" Font-Size="Smaller"></asp:label><asp:label id="LCorrecto" runat="server" Font-Names="Arial Narrow" ForeColor="#00C000" Font-Size="Smaller"></asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center"><igtxt:webimagebutton id="Enviar_Reporte" runat="server" Width="146px" Height="22px" Text="Enviar Reporte"
							UseBrowserDefaults="False" OnClick="Enviar_Reporte_Click" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Image Url="./Imagenes/Mail.png" Height="16px" Width="16px"></Image>
                                <Style>
<Padding Top="4px"></Padding>
</Style>
							</Appearance>
						</igtxt:webimagebutton></TD>
				</TR>
			</TABLE>
</asp:Content>