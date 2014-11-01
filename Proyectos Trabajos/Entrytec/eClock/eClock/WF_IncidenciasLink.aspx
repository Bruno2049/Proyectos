<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_IncidenciasLink.aspx.cs" Inherits="WF_IncidenciasLink" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edicion de Incidencias</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
				<tr>
					<td style="HEIGHT: 0%" align="right">
                        </td>
				</tr>
				<tr>
					<td style="HEIGHT: 200px;" align="center">
                        &nbsp;<igtbl:UltraWebGrid ID="Grid" runat="server" Height="100%" Width="100%" OnInitializeLayout="Grid_InitializeLayout" style="left: 222px; top: 98px">
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
                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                            Width="100%">
                                        </FrameStyle>
                                        <Pager MinimumPagesForDisplay="2">
                                            <Style BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</Style>
                                        </Pager>
                                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                        </EditCellStyleDefault>
                                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                        </FooterStyleDefault>
                                        <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Left">
                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                        </HeaderStyleDefault>
                                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                            <Padding Left="3px" />
                                            <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                        </RowStyleDefault>
                                        <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                        </GroupByRowStyleDefault>
                                        <GroupByBox Hidden="True">
                                            <Style BackColor="ActiveBorder" BorderColor="Window"></Style>
                                        </GroupByBox>
                                        <AddNewBox Hidden="False">
                                            <Style BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</Style>
                                        </AddNewBox>
                                        <ActivationObject BorderColor="" BorderWidth="">
                                        </ActivationObject>
                                        <FilterOptionsDefault>
                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                Font-Size="11px" Height="300px" Width="200px">
                                                <Padding Left="2px" />
                                            </FilterDropDownStyle>
                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                            </FilterHighlightRowStyle>
                                            <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                Font-Size="11px">
                                                <Padding Left="2px" />
                                            </FilterOperandDropDownStyle>
                                        </FilterOptionsDefault>
                                    </DisplayLayout>
                                </igtbl:UltraWebGrid>&nbsp;
                    </td>
				</tr>
				<tr>
					<td style="HEIGHT: 0%" align="center"><asp:label id="LError" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Red"></asp:label><asp:label id="LCorrecto" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow" ForeColor="Green"></asp:label></td>
				</tr>
				<tr>
					<td style="HEIGHT: 0%" align="center">
                        <igtxt:WebImageButton ID="btn_regresar" runat="server" Height="22px" Text="Regresar"
                            Width="100px" OnClick="btn_regresar_Click">
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/Back.png" Width="16px" />
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" PressedImageUrl="ig_butXP4wh.gif"
                                RenderingType="FileImages" />
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
                        &nbsp;
					<igtxt:WebImageButton ID="btn_Guardar" runat="server" Height="22px" Text="Guardar"
                            Width="100px" OnClick="btn_Guardar_Click">
                        <Appearance>
                            <Image Height="16px" Url="./Imagenes/Save_as.png" Width="16px" />
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

