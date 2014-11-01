<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_PersonasAltasBajas.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_PersonasAltasBajas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

			<TABLE id="Table1" cellSpacing="1"
				cellPadding="1" width="100%" border="0">
				<TR>
					<TD style="HEIGHT: 0%" align="right"><asp:CheckBox id="CBListado" runat="server" Text="Aplicar a todo el Listado" Font-Size="Small"
							Font-Names="Arial Narrow" AutoPostBack = "false"></asp:CheckBox>
                        <asp:CheckBox id="PersonasCheckBox1" runat="server" Text="Ver Personas Borradas" Font-Size="Small"
							Font-Names="Arial Narrow" AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 100%; height: 200px;" align="center">
                        &nbsp;<igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" OnInitializeLayout="UltraWebGrid1_InitializeLayout" OnInitializeDataSource ="UltraWebGrid1_InitializeDataSource" Browser="Xml">
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
                                StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" LoadOnDemand="Xml" RowsRange="30">
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
					<TD style="HEIGHT: 0%" align="center">
						<asp:Label id="LCorrecto" runat="server" ForeColor="Green" Font-Names="Arial" Font-Size="Smaller"></asp:Label>
						<asp:Label id="LError" runat="server" ForeColor="#CC0033" Font-Names="Arial" Font-Size="Smaller"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
						<igtxt:WebImageButton id="AltaPersonas" runat="server" Height="22px" Text="Reactivar" UseBrowserDefaults="False"
							Width="150px" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Accept.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:WebImageButton>&nbsp;&nbsp;&nbsp;
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
						</igtxt:WebImageButton>&nbsp;&nbsp;&nbsp;
						<igtxt:WebImageButton id="BajaPersonas" runat="server" Height="22px" Text="Dar baja" UseBrowserDefaults="False"
							Width="150px" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Down.png" Height="16px" Width="16px"></Image>
							</Appearance>
						</igtxt:WebImageButton></TD>
				</TR>
			</TABLE>
		</asp:Content>