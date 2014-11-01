<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_TurnosN.aspx.cs" Inherits="WF_TurnosN" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.DocumentExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid.DocumentExport" TagPrefix="igtbldocexp" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>eClock</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">

    <form id="form1" runat="server">
    <div>
        <table style="height: 400px" width="710">
            <tr>
                <td align="right" style="height: 0%">
                    <asp:CheckBox ID="TurnosCheckBox1" runat="server" AutoPostBack="True" Font-Names="Arial Narrow"
                        Font-Size="Small" OnCheckedChanged="TurnosCheckBox1_CheckedChanged" Text="Ver Turnos Borrados" /></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td align="center" style="height: 200px">
                    <igtbl:UltraWebGrid ID="Grid" runat="server" Height="200px"
                        OnInitializeLayout="Grid_InitializeLayout" Style="left: 7px; top: -233px" Width="100%">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowRowNumberingDefault="Continuous"
                            AllowSortingDefault="OnClient" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                            HeaderClickActionDefault="SortMulti" Name="Grid" RowHeightDefault="20px"
                            RowSelectorsDefault="No" RowsRange="30" ScrollBar="Always" SelectTypeRowDefault="Extended"
                            StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                            Version="4.00" ViewType="OutlookGroupBy">
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
                            <FilterOptionsDefault AllowRowFiltering="OnServer" FilterRowView="Top" FilterUIType="FilterRow"
                                ShowAllCondition="No">
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
                            <HeaderStyleDefault BackColor="LightGray" BackgroundImage="images/GridTitulo.gif"
                                BorderStyle="Solid" HorizontalAlign="Left">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </HeaderStyleDefault>
                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                            </EditCellStyleDefault>
                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="200px"
                                Width="100%">
                            </FrameStyle>
                            <Pager MinimumPagesForDisplay="2">
                            </Pager>
                            <AddNewBox Hidden="False">
                            </AddNewBox>
                            <GroupByBox Hidden="True">
                            </GroupByBox>
                        </DisplayLayout>
                    </igtbl:UltraWebGrid></td>
            </tr>
            <tr>
                <td align="center" style="height: 0%">
                    <asp:Label ID="LError" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller"
                        ForeColor="Red"></asp:Label>
                    <asp:Label ID="LCorrecto" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller"
                        ForeColor="Green"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" style="height: 0%">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <igtxt:WebImageButton ID="BBorrarTurno" runat="server" Height="22px" ImageTextSpacing="4"
                        Text="Borrar" UseBrowserDefaults="False" Width="100px" OnClick="BBorrarTurno_Click">
                        <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        <Appearance>
                            <style cursor="Default">
								</style>
                            <Image Height="16px" Url="./Imagenes/Delete.png" Width="16px" />
                        </Appearance>
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;
                    <igtxt:WebImageButton ID="BAgregarTurno" runat="server" Height="22px" ImageTextSpacing="4"
                        OnClick="BAgregarTurno_Click1" Text="Nuevo" UseBrowserDefaults="False" Width="100px">
                        <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        <Appearance>
                            <style cursor="Default">
								</style>
                            <Image Height="16px" Url="./Imagenes/New.png" Width="16px" />
                        </Appearance>
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp; &nbsp;<igtxt:WebImageButton ID="BtnDuplicar" runat="server" Height="22px"
                        ImageTextSpacing="4" OnClick="BtnDuplicar_Click" Text="Duplicar" UseBrowserDefaults="False"
                        Width="100px">
                        <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        <Appearance>
                            <Image Height="16px" Url="./Imagenes/Duplicate.png" Width="16px" />
                            <ButtonStyle Cursor="Default">
                            </ButtonStyle>
                        </Appearance>
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;
                    <igtxt:WebImageButton ID="BEditarTurno" runat="server" Height="22px" ImageTextSpacing="4"
                        OnClick="BEditarTurno_Click1" Text="Editar" UseBrowserDefaults="False" Width="100px">
                        <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        <Appearance>
                            <ButtonStyle Cursor="Default">
                            </ButtonStyle>
                            <Image Height="16px" Url="./Imagenes/Edit.png" Width="16px" />
                        </Appearance>
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;&nbsp;
                    <igtxt:WebImageButton ID="btImprimir" runat="server" Height="22px" ImageTextSpacing="4"
                        OnClick="btImprimir_Click" Text="Imprimir" UseBrowserDefaults="False" Width="100px">
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
                </td>
            </tr>
        </table>
    </div>
        <igtbldocexp:UltraWebGridDocumentExporter ID="GridExporter" runat="server" OnBeginExport="GridExporter_BeginExport">
        </igtbldocexp:UltraWebGridDocumentExporter>
    </form>
</body>
</html>
