<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_Log.aspx.cs" Inherits="WF_Log" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.DocumentExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid.DocumentExport" TagPrefix="igtbldocexp" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Log</title>
    <style type="text/css">
        .style1
        {
            height: 33px;
        }
        .style3
        {
            height: 242px;
        }
        .style4
        {
            height: 15px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="100%" style="height: 400px">
			
				<TR>
					<TD style="text-align: center;" align="right" class="style4">
                        <table style="width: 258px" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 110px; height: 24px; text-align"><strong> Periodo:</strong></td>
     
                                <td style="height: 24px">
                                    <igsch:WebDateChooser ID="FechaInicial" runat="server" Value="" Width="81px" EnableAppStyling="True"
                                        OnValueChanged="FechaInicial_ValueChanged" StyleSetName="Caribbean" Height="22px">
                                        <AutoPostBack ValueChanged="True" />
                                    </igsch:WebDateChooser>
                                </td>
                                <td style="height: 24px">
                                    <igsch:WebDateChooser ID="FechaFinal" runat="server" Width="81px" EnableAppStyling="True"
                                        OnValueChanged="FechaFinal_ValueChanged" StyleSetName="Caribbean" 
                                        Height="22px">
                                        <AutoPostBack ValueChanged="True" />
                                    </igsch:WebDateChooser>
                                </td>
                            </tr>
                        </table>
                                </TD>
				</TR>
				<TR>
					<TD align="center" class="style3">
                        <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" OnInitializeDataSource="Grid_InitializeDataSource"
                            OnInitializeLayout="Grid_InitializeLayout" Style="left: 7px; top: -233px" 
                            Width="100%" OnInitializeRow="Grid_InitializeRow">
                            <Bands>
                                <igtbl:UltraGridBand>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowRowNumberingDefault="Continuous"
                                AllowSortingDefault="OnClient" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                                HeaderClickActionDefault="SortMulti" LoadOnDemand="Xml" Name="Grid" RowHeightDefault="20px"
                                RowSelectorsDefault="No" RowsRange="30" ScrollBar="Always" SelectTypeRowDefault="Extended"
                                StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                Version="4.00" ViewType="OutlookGroupBy" XmlLoadOnDemandType="Accumulative">
                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" 
                                    Width="100%">
                                </FrameStyle>
                                <Pager MinimumPagesForDisplay="2">
                                </Pager>
                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                </EditCellStyleDefault>
                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </FooterStyleDefault>
                                <HeaderStyleDefault BackColor="LightGray" BackgroundImage="images/GridTitulo.gif"
                                    BorderStyle="Solid" HorizontalAlign="Left">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </HeaderStyleDefault>
                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                    <Padding Left="3px" />
                                    <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                </RowStyleDefault>
                                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                </GroupByRowStyleDefault>
                                <AddNewBox Hidden="False">
                                </AddNewBox>
                                <ActivationObject BorderColor="" BorderWidth="">
                                </ActivationObject>
                                <FilterOptionsDefault FilterRowView="Top" FilterUIType="FilterRow" 
                                    ShowAllCondition="No" AllowRowFiltering="OnServer">
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
                        </igtbl:UltraWebGrid></TD>
				</TR>
				<TR>
					<TD align="center" class="style1">
                        <igtxt:webimagebutton id="btImprimir" runat="server" Height="22px" Text="Imprimir" UseBrowserDefaults="False"
							Width="100px" OnClick="btImprimir_Click" ImageTextSpacing="4">
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/printer-inkjet.png" Width="16px" />
                                <ButtonStyle Cursor="Default">
                                </ButtonStyle>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        </igtxt:WebImageButton>
					</TD>
				</TR>
			</TABLE>
 </div>
        <igtbldocexp:ultrawebgriddocumentexporter id="GridExporter" runat="server" OnBeginExport="GridExporter_BeginExport">
        </igtbldocexp:ultrawebgriddocumentexporter>
    </form>
</body>
</html>

