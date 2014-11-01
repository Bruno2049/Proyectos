<%@ Page Language="C#"   AutoEventWireup="true" CodeFile="WF_EdicionExportacionNOI.aspx.cs" Inherits="WF_EdicionExportacionNOI"  EnableEventValidation = "false" %>

<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>


<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Exportacion a NOI</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
    <br />
    &nbsp;
    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" Height=""
        Width="100%">
        <table style="width: 100%; height: 125px">
            <tr>
                <td style="width: 50%">
    Incidencias del Sistema<br />
    <igtbl:UltraWebGrid ID="GridSis" runat="server"  OnInitializeDataSource="GridSis_InitializeDataSource" Height="202px" Style="left: 0px;
        top: -135px" Width="325px" OnInitializeLayout="GridSis_InitializeLayout">
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
                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="202px"
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
    </igtbl:UltraWebGrid></td>
                <td>
                    <igtxt:WebImageButton ID="BtnAsignarSis" runat="server" Height="20px" OnClick="BtnAsignarSis_Click"
                            Text="Asignar codigo" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        <Appearance>
                            <Style Cursor="Default"></Style>
                            <Image Height="16px" Url="./Imagenes/Assign.png" Width="16px" />
                        </Appearance>
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                    </igtxt:WebImageButton>
                    <igtxt:WebTextEdit ID="txtAsignarSis" runat="server" Width="60px">
                    </igtxt:WebTextEdit>
                    <br />
                    <asp:Label ID="LErrorSis" runat="server" ForeColor="Red"></asp:Label></td>
                <td>
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    Incidencias Personalizadas<br />
    <igtbl:UltraWebGrid ID="GridPer" runat="server" Height="200px" Width="325px" OnInitializeDataSource="GridPer_InitializeDataSource" OnInitializeLayout="GridPer_InitializeLayout" style="left: 0px; top: 111px">
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
               
            </Pager>
            <AddNewBox Hidden="False">
                <Style BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
            </AddNewBox>
        </DisplayLayout>
    </igtbl:UltraWebGrid></td>
                <td>
                    <igtxt:WebImageButton ID="BtnAsignarPer" runat="server" Height="20px" OnClick="BtnAsignarPer_Click"
                            Text="Asignar codigo" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        <Appearance>
                            <Style Cursor="Default"></Style>
                            <Image Height="16px" Url="./Imagenes/Assign.png" Width="16px" />
                        </Appearance>
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                    </igtxt:WebImageButton>
                    <igtxt:WebTextEdit ID="txtAsignarPer" runat="server" Width="60px">
                    </igtxt:WebTextEdit>
                    <br />
                    <asp:Label ID="LErrorPer" runat="server" ForeColor="Red"></asp:Label></td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <igcmbo:WebCombo ID="WC_Campos" runat="server" BackColor="White" BorderColor="Silver"
                        BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" OnSelectedRowChanged="WC_Campos_SelectedRowChanged"
                        SelBackColor="DarkBlue" SelForeColor="White" Version="4.00">
                        <Columns>
                            <igtbl:UltraGridColumn HeaderText="Column0">
                                <header caption="Column0"></header>
                            </igtbl:UltraGridColumn>
                        </Columns>
                        <ExpandEffects ShadowColor="LightGray" />
                        <DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" Version="4.00">
                            <HeaderStyle BackColor="LightGray" BorderStyle="Solid">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </HeaderStyle>
                            <FrameStyle BackColor="Silver" BorderStyle="Ridge" BorderWidth="2px" Cursor="Default"
                                Font-Names="Verdana" Font-Size="10pt" Height="130px" Width="325px">
                            </FrameStyle>
                            <RowStyle BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                                <BorderDetails WidthLeft="0px" WidthTop="0px" />
                            </RowStyle>
                            <SelectedRowStyle BackColor="DarkBlue" ForeColor="White" />
                        </DropDownLayout>
                    </igcmbo:WebCombo>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </igmisc:WebAsyncRefreshPanel>
    &nbsp;<br />
    &nbsp;<igtxt:WebImageButton ID="BDeshacerCambios" runat="server" Height="22px" OnClick="BDeshacerCambios_Click"
                            Text="Regresar" UseBrowserDefaults="False" Width="150px" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Appearance>
                                <Style Cursor="Default">
								</Style>
                                <Image Url="./Imagenes/Back.png" Height="16px" Width="16px" />
                            </Appearance>
                        </igtxt:WebImageButton>
 </div>
    </form>
</body>
</html>

