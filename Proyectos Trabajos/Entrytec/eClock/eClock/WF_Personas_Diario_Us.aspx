
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_Personas_Diario_Us.aspx.cs" AutoEventWireup="True" Inherits="WF_Personas_Diario_Us" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register TagPrefix="uc1" TagName="WC_Mes" Src="WC_Mes.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD style="HEIGHT: 0%">
						</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%"><FONT face="Arial">
							<asp:Label id="LNombrePersona" runat="server" ForeColor="Navy"></asp:Label>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:LinkButton ID="LBAnterior" runat="server" OnClick="LBAnterior_Click"><<</asp:LinkButton>
                        &nbsp; &nbsp;<asp:LinkButton ID="LBTemp" runat="server" Enabled="False" ForeColor="DarkGray" CausesValidation="False"></asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="LBSiguiente" runat="server" OnClick="LBSiguiente_Click">>></asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="LBActual" runat="server" OnClick="LBActual_Click" Width="48px">Actual</asp:LinkButton></FONT></TD>
				</TR>
				<TR>
					<TD align="center" style="height: 50px">
						<TABLE id="Table3" style="WIDTH: 897px; HEIGHT: 280px; border-right: #3399ff solid; border-top: #3399ff solid; border-left: #3399ff solid; border-bottom: #3399ff solid;" borderColor="#ff9933" cellSpacing="0"
							borderColorDark="#996600" cellPadding="1" width="897" borderColorLight="#ffcc00" border="3">
							<TR>
								<TD style="height: 199px">
                                    <asp:Table ID="Tabla" runat="server">
                                    </asp:Table>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 44%" align="center">&nbsp;&nbsp;&nbsp;<br />
                        <table style="width: 100%; height: 100%">
                            <tr>
                                <td valign="top" height="200" style="width: 464px">
                                    <span style="font-family: Arial"></span>
                                    <igmisc:webpanel id="Webpanel1" runat="server" backcolor="White" bordercolor="SteelBlue"
                                        borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                                        stylesetname="PaneleClock" Width="100%">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Abrebiaturas de Incidencias">
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
    <table height="200" width="100%">
        <tr>
            <td style="width: 100%; height: 200px">
<igtbl:UltraWebGrid style="LEFT: 0px; TOP: 0px" id="Grid" runat="server" Width="100%" OnLoad="Grid_Load" OnInitializeLayout="Grid_InitializeLayout" Height="200px" DataSourceID="DataSource">
                                        <Bands>
                                            <igtbl:UltraGridBand>
                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                </AddNewRow>
                                                <Columns>
                                                    <igtbl:UltraGridColumn BaseColumnName="TIPO_INCIDENCIA_NOMBRE" HeaderText="TIPO_INCIDENCIA_NOMBRE"
                                                        IsBound="True" Key="TIPO_INCIDENCIA_NOMBRE" Width="300px">
                                                        <Header Caption="TIPO_INCIDENCIA_NOMBRE">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="TIPO_INCIDENCIA_ABR" HeaderText="TIPO_INCIDENCIA_ABR"
                                                        IsBound="True" Key="TIPO_INCIDENCIA_ABR">
                                                        <Header Caption="TIPO_INCIDENCIA_ABR">
                                                            <RowLayoutColumnInfo OriginX="1"  />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="1"  />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                </Columns>
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
                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"  />
                                            </FooterStyleDefault>
                                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                                <BorderDetails ColorLeft="Window" ColorTop="Window"  />
                                                <Padding Left="3px"  />
                                            </RowStyleDefault>
                                            <FilterOptionsDefault>
                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                    Font-Size="11px">
                                                    <Padding Left="2px"  />
                                                </FilterOperandDropDownStyle>
                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                </FilterHighlightRowStyle>
                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                    Font-Size="11px" Height="300px" Width="200px">
                                                    <Padding Left="2px"  />
                                                </FilterDropDownStyle>
                                            </FilterOptionsDefault>
                                            <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Left">
                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"  />
                                            </HeaderStyleDefault>
                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                            </EditCellStyleDefault>
                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="200px"
                                                Width="100%">
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
                                    </igtbl:UltraWebGrid></td>
        </tr>
    </table>
</Template>
</igmisc:webpanel>
                                    <asp:ObjectDataSource ID="DataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="GetData" TypeName="DS_IncidenciasFillTableAdapters.EC_TIPO_INCIDENCIASTableAdapter">
                                    </asp:ObjectDataSource>
                                </td>
                                <td valign="top" height="200">
                                    <span style="font-family: Arial">
                                    </span>
                                    <igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
                                        borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                                        stylesetname="PaneleClock" Width="100%">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left" Text="Abrebiaturas de Incidencias del Sistema">
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
    <table height="200" width="100%">
        <tr>
            <td style="width: 100%; height: 200px">
<igtbl:UltraWebGrid style="LEFT: 12px; TOP: 214px" id="Grid2" runat="server" Width="100%" OnLoad="Grid2_Load" OnInitializeLayout="Grid2_InitializeLayout" Height="200px" DataSourceID="DataSource2">
                                        <Bands>
                                            <igtbl:UltraGridBand>
                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                </AddNewRow>
                                                <Columns>
                                                    <igtbl:UltraGridColumn BaseColumnName="TIPO_INC_SIS_NOMBRE" HeaderText="TIPO_INC_SIS_NOMBRE"
                                                        IsBound="True" Key="TIPO_INC_SIS_NOMBRE" Width="300px">
                                                        <Header Caption="TIPO_INC_SIS_NOMBRE">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="TIPO_INC_SIS_ABR" HeaderText="TIPO_INC_SIS_ABR"
                                                        IsBound="True" Key="TIPO_INC_SIS_ABR">
                                                        <Header Caption="TIPO_INC_SIS_ABR">
                                                            <RowLayoutColumnInfo OriginX="1"  />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="1"  />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                </Columns>
                                            </igtbl:UltraGridBand>
                                        </Bands>
                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                            HeaderClickActionDefault="SortMulti" Name="Grid2" RowHeightDefault="20px"
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
                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"  />
                                            </FooterStyleDefault>
                                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                                <BorderDetails ColorLeft="Window" ColorTop="Window"  />
                                                <Padding Left="3px"  />
                                            </RowStyleDefault>
                                            <FilterOptionsDefault>
                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                    Font-Size="11px">
                                                    <Padding Left="2px"  />
                                                </FilterOperandDropDownStyle>
                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                </FilterHighlightRowStyle>
                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                    Font-Size="11px" Height="300px" Width="200px">
                                                    <Padding Left="2px"  />
                                                </FilterDropDownStyle>
                                            </FilterOptionsDefault>
                                            <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Left">
                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"  />
                                            </HeaderStyleDefault>
                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                            </EditCellStyleDefault>
                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="200px"
                                                Width="100%">
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
                                    </igtbl:UltraWebGrid></td>
        </tr>
    </table>
</Template>
</igmisc:webpanel>
                                    <br />
                                    <span style="font-family: Arial"><asp:ObjectDataSource ID="DataSource2"
                                        runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                                        TypeName="DS_IncidenciasFillTableAdapters.EC_TIPO_INC_SISTableAdapter"></asp:ObjectDataSource>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        &nbsp; &nbsp;</TD>
				</TR>
			</TABLE>
</asp:Content>