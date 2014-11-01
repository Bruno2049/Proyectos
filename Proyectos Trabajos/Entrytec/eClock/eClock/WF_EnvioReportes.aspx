<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_EnvioReportes.aspx.cs" Inherits="WF_EnvioReportes" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 27px;
        }
        .style2
        {
            height: 16px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				<tr>
					<td align="right" class="style1">
                        <asp:CheckBox ID="ReportesCheckBox1" runat="server" Text="Ver Borrados" OnCheckedChanged="ReportesCheckBox1_CheckedChanged" AutoPostBack="True" /></td>
				</tr>
				<tr>
					<td style="HEIGHT: 200px; text-align: left;" align="center">
                        <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" Height="288px" 
                            OnInitializeLayout="Grid_InitializeLayout" 
                            OnInitializeDataSource="Grid_InitializeDataSource" Width="100%">
                            <Bands>
                                <igtbl:UltraGridBand>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                AllowUpdateDefault="Yes" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortMulti"
                                LoadOnDemand="Xml" Name="Grid" RowHeightDefault="20px" RowSelectorsDefault="No"
                                SelectTypeRowDefault="Single" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" 
                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif" 
                                    Font-Size="8.25pt" Height="288px" Width="100%">
                                </FrameStyle>
                                <Pager MinimumPagesForDisplay="2">
                                </Pager>
                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                </EditCellStyleDefault>
                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                        WidthTop="1px" />
                                </FooterStyleDefault>
                                <HeaderStyleDefault BackColor="LightGray" 
                                    BackgroundImage="images/GridTitulo.gif" BorderStyle="Solid" 
                                    HorizontalAlign="Left">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                        WidthTop="1px" />
                                </HeaderStyleDefault>
                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" 
                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                    <Padding Left="3px" />
                                    <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                </RowStyleDefault>
                                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                </GroupByRowStyleDefault>
                                <GroupByBox Hidden="True">
                                    <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                    </BoxStyle>
                                </GroupByBox>
                                <AddNewBox>
                                    <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" 
                                        BorderWidth="1px">
                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                            WidthTop="1px" />
                                    </BoxStyle>
                                </AddNewBox>
                                <ActivationObject BorderColor="" BorderWidth="">
                                </ActivationObject>
                                <FilterOptionsDefault>
                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                        BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px">
                                        <Padding Left="2px" />
                                    </FilterOperandDropDownStyle>
                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px" Height="300px" Width="200px">
                                        <Padding Left="2px" />
                                    </FilterDropDownStyle>
                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                    </FilterHighlightRowStyle>
                                </FilterOptionsDefault>
                            </DisplayLayout>
                        </igtbl:UltraWebGrid></td>
				</tr>
				<tr>
					<td align="center" class="style2"><asp:label id="LError" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Red"></asp:label><asp:label id="LCorrecto" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Green"></asp:label></td>
				</tr>
				<tr>
					<td style="HEIGHT: 0%" align="center">&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp;
                        <igtxt:WebImageButton ID="btn_BorrarEnvio" runat="server" Height="22px" 
                            Text="Borrar" Width="100px" OnClick="btn_BorrarEnvio_Click">
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/Delete.png" Width="16px" />
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" PressedImageUrl="ig_butXP4wh.gif"
                                RenderingType="FileImages" />
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
                        &nbsp;&nbsp;<igtxt:WebImageButton ID="btn_NuevoEnvio" runat="server" Height="22px"
                            Text="Nuevo" Width="100px" OnClick="btn_NuevoEnvio_Click">
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/new.png" Width="16px" />
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" PressedImageUrl="ig_butXP4wh.gif"
                                RenderingType="FileImages" />
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
                        &nbsp;&nbsp;<igtxt:WebImageButton ID="btn_DuplicarEnvio" runat="server" Height="22px"
                            ImageTextSpacing="4" Text="Duplicar" UseBrowserDefaults="False" Width="100px" OnClick="btn_DuplicarEnvio_Click">
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/Duplicate.png" Width="16px" />
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" PressedImageUrl="ig_butXP4wh.gif"
                                RenderingType="FileImages" />
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
                        &nbsp;
                        <igtxt:WebImageButton ID="btn_EditarEnvio" runat="server" Height="22px" Text="Editar"
                            Width="100px" OnClick="btn_EditarEnvio_Click">
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/Edit.png" Width="16px" />
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" PressedImageUrl="ig_butXP4wh.gif"
                                RenderingType="FileImages" />
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
					</td>
				</tr>
			</table>
	 </div>

    </form>
</body>
</html>