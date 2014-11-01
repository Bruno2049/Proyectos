<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Page language="c#" CodeFile="WF_IncidenciasE.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_IncidenciasE" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
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
					<TD style="HEIGHT: 0%; text-align: right;">
                        <asp:CheckBox ID="CBListado" runat="server" AutoPostBack="false" Font-Names="Arial Narrow"
                            Font-Size="Smaller" Text="Aplicar a todo el Listado" /></TD>
				</TR>
				<TR>
					<TD align="center">
						<TABLE id="Table2" style="WIDTH: 100%; HEIGHT: 100%" cellSpacing="1" cellPadding="1" width="633"
							border="0">
							<TR>
								<TD colSpan="3" height="1"></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="3" height="200">
                                    <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" OnInitializeDataSource="Grid_DataSource"
                                        OnInitializeLayout="Grid_InitializeLayout" style="left: 0px">
                                        <Bands>
                                            <igtbl:UltraGridBand>
                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                </AddNewRow>
                                            </igtbl:UltraGridBand>
                                        </Bands>
                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                            HeaderClickActionDefault="SortMulti" Name="Grid"
                                            RowHeightDefault="20px" RowSelectorsDefault="No" RowsRange="30" SelectTypeRowDefault="Extended"
                                            StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                            Version="4.00" ViewType="OutlookGroupBy">
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
                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                            </FrameStyle>
                                            <Pager MinimumPagesForDisplay="2">
                                                <Style BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">

</Style>
                                            </Pager>
                                            <AddNewBox Hidden="False">
                                                <Style BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">

</Style>
                                            </AddNewBox>
                                        </DisplayLayout>
                                    </igtbl:UltraWebGrid>
                                &nbsp;</TR>
						</TABLE>
					</TD>
				</TR>
                <tr>
                    <td align="center" valign="top">
                        <igmisc:webasyncrefreshpanel id="WebAsyncRefreshPanel1" runat="server" Width="" Height="">
                            <igmisc:WebPanel ID="WebPanel2" runat="server" BackColor="White" BorderColor="SteelBlue"
                                BorderStyle="Outset" BorderWidth="2px" Font-Bold="False" Font-Names="ARIAL" ForeColor="Black"
                                StyleSetName="PaneleClock">
                                <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Black">
                                    <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                        ColorTop="0, 45, 150" />
                                    <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                                </PanelStyle>
                                <Header Text="Opciones" TextAlignment="Left">
                                    <ExpandedAppearance>
                                        <Styles BackgroundImage="./images/GridTitulo.gif" BorderColor="Transparent" BorderStyle="Ridge"
                                            BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Black"
                                            Height="15px">
                                            <BorderDetails ColorLeft="158, 190, 245" ColorRight="0, 45, 150" ColorTop="158, 190, 245"
                                                WidthBottom="0px" />
                                            <Padding Bottom="1px" Left="4px" Top="1px" />
                                        </Styles>
                                    </ExpandedAppearance>
                                    <HoverAppearance>
                                        <Styles CssClass="igwpHeaderHoverBlue2k7">
                                        </Styles>
                                    </HoverAppearance>
                                    <CollapsedAppearance>
                                        <Styles CssClass="igwpHeaderCollapsedBlue2k7">
                                        </Styles>
                                    </CollapsedAppearance>
                                    <ExpansionIndicator Height="0px" Width="0px" />
                                </Header>
                                <Template>
                                    <TABLE id="Table3" cellSpacing=3 cellPadding=1 border=0><TBODY>
    <tr>
        <td align="left" style="font-size: 11pt; font-family: Arial; height: 21px">
        </td>
        <td align="left" style="height: 21px">
            <asp:CheckBox id="CBBorrarI" runat="server" Text="Borrar Incidencias" Font-Names="Arial Narrow" AutoPostBack="True" OnCheckedChanged="CBBorrarI_CheckedChanged" Font-Size="Small"></asp:CheckBox></td>
        <td align="left" style="height: 21px">
        </td>
        <td align="left" style="width: 207px; height: 21px">
        </td>
    </tr>
    <TR><TD style="FONT-SIZE: 11pt; FONT-FAMILY: Arial; HEIGHT: 21px" align="left"><FONT><asp:label id="Label1" runat="server" Width="72px" Font-Size="Small">Fecha Inicio</asp:label></FONT></TD><TD style="HEIGHT: 21px" align="left"><igsch:webdatechooser id="FechaI" runat="server" Width="200px" Text="Null" Font-Names="Arial Narrow" MinDate="1900-01-01">
										<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="" ShowNextPrevMonth="False" MaxDate=""
											ShowTitle="False" ShowFooter="False">
											<DayStyle Font-Size="9pt" Font-Names="Arial" BackColor="White"></DayStyle>
											<SelectedDayStyle BackColor="#0054E3"></SelectedDayStyle>
											<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
											<DayHeaderStyle ForeColor="White" BackColor="#7A96DF"></DayHeaderStyle>
											<TitleStyle BackColor="White"></TitleStyle>
										</CalendarLayout>
									</igsch:webdatechooser></TD>
        <td align="left" style="height: 21px">
            <asp:label id="Label2" runat="server" Width="61px">Fecha Fin</asp:label></td>
        <td align="left" style="width: 207px; height: 21px">
            <igsch:webdatechooser id="FechaF" runat="server" Width="200px" Text="Null" Font-Names="Arial Narrow" MinDate="1900-01-01">
										<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="" ShowNextPrevMonth="False" MaxDate=""
											ShowTitle="False" ShowFooter="False">
											<DayStyle Font-Size="9pt" Font-Names="Arial" BackColor="White"></DayStyle>
											<SelectedDayStyle BackColor="#0054E3"></SelectedDayStyle>
											<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
											<DayHeaderStyle ForeColor="White" BackColor="#7A96DF"></DayHeaderStyle>
											<TitleStyle BackColor="White"></TitleStyle>
										</CalendarLayout>
									</igsch:webdatechooser>
        </td>
    </TR><TR><TD style="FONT-SIZE: 11pt; FONT-FAMILY: Arial; HEIGHT: 5px" align="left"><P><FONT><asp:label id="Label3" runat="server" Width="104px" Font-Size="Small">Tipo de Incidencia</asp:label></FONT></P></TD><TD style="HEIGHT: 5px" align="left"><igcmbo:webcombo id="TipoIncidenciaNombre" runat="server" Height="22px" Width="200px" OnInitializeLayout="TipoIncidenciaNombre_InitializeLayout" Font-Names="Arial Narrow" BorderColor="LightGray" BorderWidth="1px" BorderStyle="Solid" BackColor="White" ForeColor="Black" Version="3.00"   SelBackColor="17, 69, 158"   DataTextField="TIPO_INCIDENCIA_NOMBRE" DataValueField="TIPO_INCIDENCIA_ID" DataMember="EC_TIPO_INCIDENCIAS" DataSource="<%# dS_Incidencias1 %>">
										<Columns>
                                            <igtbl:UltraGridColumn HeaderText="Column0">
                                                <header caption="Column0"></header>
                                            </igtbl:UltraGridColumn>
										</Columns>
										<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="3.00">
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
									</igcmbo:webcombo></TD>
                                        <td align="left" style="height: 5px">
                                        </td>
                                        <td align="left" style="width: 207px; height: 5px">
                                        </td>
                                    </TR><TR><TD style="FONT-SIZE: 11pt; FONT-FAMILY: Arial; HEIGHT: 5px" align="left"><P><FONT><asp:label id="Comentariolabel" runat="server" Width="104px" Font-Size="Small">Comentario</asp:label></FONT></P><P>&nbsp;</P></TD>
                                        <td align="left" colspan="3" style="height: 5px">
                                            <igtxt:webtextedit id="IncidenciaC" runat="server" Height="44px" Width="433px" Font-Names="Arial Narrow" BorderColor="#7F9DB9" BorderWidth="1px" BorderStyle="Solid" UseBrowserDefaults="False" CellSpacing="1" HideEnterKey="True">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:webtextedit>&nbsp; 
                                        </td>
                                    </TR></TBODY></TABLE>
                                </Template>
                            </igmisc:WebPanel>
                        </igmisc:webasyncrefreshpanel>
                    </td>
                </tr>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
                        <asp:label id="LError" runat="server" Font-Names="Arial Narrow" ForeColor="#CC0033" Font-Size="Smaller"></asp:label><asp:label id="LCorrecto" runat="server" Font-Names="Arial Narrow" ForeColor="#00C000" Font-Size="Smaller"></asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
                        &nbsp;<igtxt:webimagebutton id="BDeshacerCambios" runat="server" Height="22px" Width="150px" Text="Deshacer Cambios"
							UseBrowserDefaults="False" OnClick="BDeshacerCambios_Click" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:webimagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
						<igtxt:webimagebutton id="BGuardarCambios" runat="server" Height="22px" Width="140px" Text="Guardar Cambios"
							UseBrowserDefaults="False" OnClick="BGuardarCambios_Click" ImageTextSpacing="4">
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
			</table>
   </div>
    </form>
</body>
</html>