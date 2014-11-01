<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Page language="c#" MasterPageFile="~/MasterPage.master" CodeFile="WF_Perfiles.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_Perfiles" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

			<TABLE id="Table1" style="WIDTH: 100%; FONT-FAMILY: Arial; HEIGHT: 100%" cellSpacing="1"
				cellPadding="1" width="300" border="0">
				<TR>
					<TD style="HEIGHT: 0%">
						</TD>
				</TR>
				<TR>
					<TD align="center">
						<igtbl:ultrawebgrid id=Grid runat="server" DataMember="EC_PERFILES" DataSource="<%# dS_Perfiles1 %>" Height="100%" Width="100%" OnInitializeLayout="Grid_InitializeLayout" DataKeyField="PERFIL_ID">
							<DisplayLayout ColWidthDefault="" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00"
								GridLinesDefault="Horizontal" SelectTypeRowDefault="Extended" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="Grid" TableLayout="Fixed" CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes">
								<AddNewBox>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
								</AddNewBox>
								<Pager ChangeLinksColor="True" QuickPages="5" PageSize="200" PagerAppearance="Both" AllowCustomPaging="True"
									StyleMode="CustomLabels" Alignment="Center">
									<Style BorderWidth="1px" Font-Names="Arial Narrow" BorderStyle="Solid" ForeColor="Blue"
										BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
								</Pager>
								<HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" BorderColor="Black"
									HorizontalAlign="Left" ForeColor="White" BackColor="Navy" >
									<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
								</HeaderStyleDefault>
								<RowSelectorStyleDefault BackColor= "   "></RowSelectorStyleDefault>
								<FrameStyle Width="100%" Font-Names="Arial Narrow" BorderColor="Black" ForeColor="#759AFD" Height="100%"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<ActivationObject BorderStyle="Dotted" BorderColor="1, 68, 208" BorderWidth=""></ActivationObject>
								<GroupByBox Prompt="Arrastre la columna que desea agrupar...">
									<Style BorderColor="Window" ForeColor="Navy" BackColor="LightSteelBlue">
									</Style>
								</GroupByBox>
								<SelectedRowStyleDefault BackgroundImage="images/Office2003SelRow.png" ForeColor="DodgerBlue" BackColor="Sienna" >
									<BorderDetails WidthLeft="0px" StyleBottom="Solid" ColorBottom="Black" WidthTop="0px" WidthRight="0px"
										StyleTop="None" StyleRight="None" WidthBottom="1px" StyleLeft="None"></BorderDetails>
								</SelectedRowStyleDefault>
								<RowAlternateStyleDefault ForeColor="Black" BackColor="WhiteSmoke"></RowAlternateStyleDefault>
								<RowStyleDefault BorderColor="Black" ForeColor="White" BackColor="CornflowerBlue" Font-Names="Verdana" Font-Size="8pt">
									<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
								</RowStyleDefault>
                                <Images>
                                    <CollapseImage Url="ig_tblcrm_rowarrow_down.gif" />
                                    <ExpandImage Url="ig_tblcrm_rowarrow_right.gif" />
                                </Images>
							</DisplayLayout>
							<Bands>
                                <igtbl:UltraGridBand AddButtonCaption="EC_PERFILES" BaseTableName="EC_PERFILES"
                                    DataKeyField="PERFIL_ID" Key="EC_PERFILES">
                                   
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                    <Columns>
                                        <igtbl:UltraGridColumn BaseColumnName="PERFIL_NOMBRE" HeaderText="Nombre del Perfil"
                                            IsBound="True" Key="PERFIL_NOMBRE">
                                            <Header Caption="Nombre del Perfil">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PERFIL_ID" DataType="System.Decimal" HeaderText="PERFIL_ID"
                                            Hidden="True" IsBound="True" Key="PERFIL_ID">
                                            <Header Caption="PERFIL_ID">
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn BaseColumnName="PERFIL_BORRADO" DataType="System.Decimal"
                                            HeaderText="PERFIL_BORRADO" Hidden="True" IsBound="True" Key="PERFIL_BORRADO">
                                            <Header Caption="PERFIL_BORRADO">
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                </igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
                <tr>
                    <td align="center">
                        <asp:Label ID="LCorrecto" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller"
                            ForeColor="Green"></asp:Label>
                        <asp:Label ID="LError" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller"
                            ForeColor="Red"></asp:Label></td>
                </tr>
				<TR>
					<TD style="HEIGHT: 0%" align="center">
                        <igtxt:WebImageButton ID="BBorrarTerminal" runat="server" Height="22px" OnClick="BBorrarPerfil_Click"
                            Text="Borrar" UseBrowserDefaults="False" Width="100px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Appearance>
                                <Style Cursor="Default">
								</Style>
                                <Image Url="./Imagenes/Delete.png" Height="16px" Width="16px" />
                            </Appearance>
                        </igtxt:WebImageButton>
                        &nbsp; &nbsp; &nbsp;
                        <igtxt:WebImageButton ID="BAgregarTerminal" runat="server" Height="22px" OnClick="BAgregarPerfil_Click"
                            Text="Nuevo" UseBrowserDefaults="False" Width="100px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                            <Appearance>
                                <Image Url="./Imagenes/New.png" Height="16px" Width="16px" />
                                <Style Cursor="Default"></Style>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        </igtxt:WebImageButton>
                        &nbsp; &nbsp; &nbsp;&nbsp;<igtxt:WebImageButton ID="BEditarTerminal" runat="server"
                            Height="22px" OnClick="BEditarPerfil_Click" Text="Editar" UseBrowserDefaults="False"
                            Width="100px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Appearance>
                                <Style Cursor="Default">
								</Style>
                                <Image Url="./Imagenes/Edit.png" Height="16px" Width="16px" />
                            </Appearance>
                        </igtxt:WebImageButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</TD>
				</TR>
			</TABLE>
</asp:Content>