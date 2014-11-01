<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_Inasistencias.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Inasistencias" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD width="100%" height="200">
                        <igtbl:UltraWebGrid ID="Grid" runat="server" Height="200px" Width="325px" OnInitializeLayout="Grid_InitializeLayout" OnInitializeDataSource = "Grid_InitializeDataSource" Browser="Xml">
                            <Bands>
                                <igtbl:UltraGridBand>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                HeaderClickActionDefault="SortMulti" Name="Grid" RowHeightDefault="20px"
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
                        </igtbl:UltraWebGrid>&nbsp;</TD>
				</TR>
				<TR>
					<TD style="height: 222px">
                        &nbsp;&nbsp;<igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                            stylesetname="PaneleClock">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left">
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
    <table bordercolor="#6699ff" bordercolordark="#0066ff" bordercolorlight="#99ccff"
        style="width: 149px; border-right: black thin solid; border-top: black thin solid; margin: 2px; border-left: black thin solid; border-bottom: black thin solid;">
        <tr>
            <td style="height: 168px">
<TABLE id="Table3" height=13 cellSpacing=0 cellPadding=0 align=center border=0><TBODY><TR><TD style="HEIGHT: 22px; text-align: center;" align=left colspan="2"><asp:Label id="LError" runat="server" ForeColor="Red"></asp:Label> <asp:Label id="LCorrecto" runat="server" ForeColor="Green"></asp:Label></TD></TR><TR><TD align=left><asp:Label id="LBIncidencia" runat="server" Font-Names="Arial Narrow">Incidencia</asp:Label></TD><TD style="WIDTH: 566px; text-align: left;"><igcmbo:webcombo id="TipoIncidenciaNombre" runat="server" BorderColor="LightGray" BackColor="White" Font-Names="Arial Narrow" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" Width="200px" Height="22px" DataSource="<%# dS_Inasistencias1 %>" Visible="True" DataTextField="TIPO_INCIDENCIA_NOMBRE" DataValueField="TIPO_INCIDENCIA_ID"   SelBackColor="17, 69, 158"   Version="3.00">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="TIPO_INCIDENCIA_ID" Key="TIPO_INCIDENCIA_ID" IsBound="True" Hidden="True"
												DataType="System.Decimal" BaseColumnName="TIPO_INCIDENCIA_ID">
												<Footer Key="TIPO_INCIDENCIA_ID"></Footer>
												<Header Key="TIPO_INCIDENCIA_ID" Caption="TIPO_INCIDENCIA_ID"></Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Incidencia" Key="TIPO_INCIDENCIA_NOMBRE" IsBound="True" BaseColumnName="TIPO_INCIDENCIA_NOMBRE">
												<Footer Key="TIPO_INCIDENCIA_NOMBRE">
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TIPO_INCIDENCIA_NOMBRE" Caption="Incidencia">
													<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Abreviatura" Key="TIPO_INCIDENCIA_ABR" IsBound="True" BaseColumnName="TIPO_INCIDENCIA_ABR">
												<Footer Key="TIPO_INCIDENCIA_ABR">
													<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TIPO_INCIDENCIA_ABR" Caption="Abreviatura">
													<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="TIPO_INCIDENCIA_BORRADO" Key="TIPO_INCIDENCIA_BORRADO" IsBound="True"
												Hidden="True" DataType="System.Decimal" BaseColumnName="TIPO_INCIDENCIA_BORRADO">
												<Footer Key="TIPO_INCIDENCIA_BORRADO">
													<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
												</Footer>
												<Header Key="TIPO_INCIDENCIA_BORRADO" Caption="TIPO_INCIDENCIA_BORRADO">
													<RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
												</Header>
											</igtbl:UltraGridColumn>
										</Columns>
										<DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" HeaderClickAction="Select">
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
									</igcmbo:webcombo></TD></TR><TR><TD align=left><asp:Label id="LBDescripcion" runat="server" Font-Names="Arial Narrow">Descripción(Motivo)</asp:Label></TD><TD style="WIDTH: 566px"><igtxt:webtextedit id="IncidenciaC" runat="server" BorderColor="#7F9DB9" Font-Names="Arial Narrow" BorderWidth="1px" BorderStyle="Solid" Width="307px" Height="55px" UseBrowserDefaults="False" CellSpacing="1" HideEnterKey="True">
										<ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow">
											<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
											<ButtonDisabledStyle BorderColor="#D7D7D7" ForeColor="#BEBEBE" BackColor="#E1E1DD"></ButtonDisabledStyle>
											<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" ForeColor="#506080"
												BackColor="#C5D5FC"></ButtonStyle>
											<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
										</ButtonsAppearance>
									</igtxt:webtextedit></TD></TR><TR><TD style="HEIGHT: 31px; text-align: center;" align=left colspan="2">
        <igtxt:webimagebutton id="BGuardarCambios" runat="server" Text="Justificar Inasistencias" Width="192px" Height="22px" UseBrowserDefaults="False" OnClick="BGuardarCambios_Click" ImageTextSpacing="4">
										<Alignments VerticalImage="Bottom" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>
										<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
											RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
										<Appearance>
											<Style Cursor="Default">
											</Style>
											<Image Url="./Imagenes/Empleado.png" Height="18px" Width="20px"></Image>
										</Appearance>
									</igtxt:webimagebutton>
</TD></TR></TBODY></TABLE>
            </td>
            <td valign="middle" style="border-right: black thin solid; border-top: black thin solid; margin-left: 2px; border-left: black thin solid; border-bottom: black thin solid; height: 168px">
                <igtxt:webimagebutton id="BJusAccesos" runat="server" Text="Justificar Acceso" Width="192px" Height="22px" UseBrowserDefaults="False" OnClick="BJusAccesos_Click" ImageTextSpacing="4">
										<Alignments VerticalImage="Bottom" VerticalAll="NotSet" HorizontalAll="NotSet"></Alignments>
										<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
											RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
										<Appearance>
											<Style Cursor="Default">
											</Style>
											<Image Url="./Imagenes/Horarios.png" Height="18px" Width="20px"></Image>
										</Appearance>
									</igtxt:webimagebutton>
            </td>
        </tr>
    </table>
</Template>
</igmisc:webpanel>
                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                        <igtxt:WebDateTimeEdit ID="txthora" runat="server" DisplayModeFormat="dd/MM/yy" EditModeFormat="dd/MM/yy"
                            Visible="False">
                        </igtxt:WebDateTimeEdit>
                    </TD>
				</TR>
			</TABLE>
</asp:Content>