<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_ConfigCampos.aspx.cs" Inherits="WF_ConfigCampos" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edicion de Incidencias</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
       <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" Direction="LeftToRight"
            Height="100%" HorizontalAlign="Center" Width="100%">
        <table cellpadding="0" cellspacing="0" height="100%" width="100%">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td height="100%">
                    &nbsp;&nbsp;&nbsp;
                    <igtbl:UltraWebGrid ID="Grid" runat="server" Height="100%" OnInitializeLayout="Grid_InitializeLayout"
                        Width="100%" Browser="UpLevel" OnInitializeDataSource = "Grid_DataSource" style="top: 1px">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer"
                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                            HeaderClickActionDefault="SortMulti" Name="Grid" RowHeightDefault="20px"
                            RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" CellClickActionDefault="Edit">
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
                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                Width="100%">
                            </FrameStyle>
                            <Pager MinimumPagesForDisplay="2">
                                <Style BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
                            </Pager>
                            <AddNewBox>
                                <Style BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
                            </AddNewBox>
                        </DisplayLayout>
                    </igtbl:UltraWebGrid>
                    </td>
            </tr>
            <tr>
                <td style="text-align: center;" height="1">
                    <asp:Label ID="LError" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller"
                        ForeColor="#CC0033" meta:resourcekey="LErrorResource1"></asp:Label><br />
                    <asp:Label ID="LCorrecto" runat="server" Font-Names="Arial Narrow"
                            Font-Size="Smaller" ForeColor="#00C000" meta:resourcekey="LCorrectoResource1"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: center; height: 1px;">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<igtxt:WebImageButton ID="BDeshacerCambios"
                        runat="server" Height="22px" OnClick="BDeshacerCambios_Click" Text="Deshacer Cambios"
                        UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        <Appearance>
                            <Image Url="./Imagenes/Deshacer.png" Height="18px" Width="20px" />
                            <Style Cursor="Default"></Style>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;&nbsp;
                    <igtxt:WebImageButton ID="BGuardarCambios" runat="server" Height="22px" OnClick="BGuardarCambios_Click"
                        Text="Guardar Cambios" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" HorizontalAll="NotSet" VerticalAll="Bottom" />
                        <Appearance>
                            <Image Url="./Imagenes/Save.png" Height="16px" Width="16px" />
                            <Style Cursor="Default"></Style>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                    </igtxt:WebImageButton>
                </td>
            </tr>
        </table>
            &nbsp;&nbsp;
        </igmisc:WebAsyncRefreshPanel>
</div>
    </form>
</body>
</html>
