<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_PersonasTerminales.aspx.cs" Inherits="WF_PersonasTerminales" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Importar Empleados</title>
    <style type="text/css">
        .style2
        {
            height: 18px;
        }
        .style3
        {
            height: 25px;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="100%" style="height: 380px">
            <tr>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="font-size: 12px; font-style: italic; font-family: arial">
                    Asignando Terminales a: &nbsp;<asp:Label ID="lnombre" runat="server" 
                        Width="416px" style="font-weight: 700"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="height: 280px">
                    <igtbl:UltraWebGrid ID="UWGTerminales" runat="server" Height="298px" 
                        Width="100%" OnInitializeLayout="UWGTerminales_InitializeLayout" 
                        OnUpdateRowBatch="UWGTerminales_UpdateRowBatch" Browser="Xml" 
                        style="left: 1px; top: 0px">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                  
<DisplayLayout Name="UWGTerminales" AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" 
                            AllowDeleteDefault="Yes" AllowSortingDefault="OnClient" 
                            AllowUpdateDefault="Yes" BorderCollapseDefault="Separate" 
                            HeaderClickActionDefault="SortMulti" RowHeightDefault="20px" 
                            RowSelectorsDefault="No" SelectTypeRowDefault="Extended" 
                            StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" 
                            TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif" 
        Font-Size="8.25pt" Height="298px" Width="100%">
    </FrameStyle>
    <Pager MinimumPagesForDisplay="2">
        <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
            WidthTop="1px" />
        </PagerStyle>
    </Pager>
    <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
    </EditCellStyleDefault>
    <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
            WidthTop="1px" />
    </FooterStyleDefault>
    <HeaderStyleDefault BackColor="LightGray" BorderStyle="Solid" 
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
    <GroupByBox>
        <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
        </BoxStyle>
    </GroupByBox>
    <AddNewBox Hidden="False">
        <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" 
            BorderWidth="1px">
            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                WidthTop="1px" />
        </BoxStyle>
    </AddNewBox>
<ActivationObject BorderColor="" BorderWidth=""></ActivationObject>
    <FilterOptionsDefault>
        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
            BorderWidth="1px" CustomRules="overflow:auto;" 
            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px" 
            Width="200px">
            <Padding Left="2px" />
        </FilterDropDownStyle>
        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
        </FilterHighlightRowStyle>
        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" 
            BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" 
            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
            <Padding Left="2px" />
        </FilterOperandDropDownStyle>
    </FilterOptionsDefault>
</DisplayLayout>
                  
                    </igtbl:UltraWebGrid></td>
            </tr>
            <tr>
                <td align="center" class="style2">
                    <asp:Label ID="Lbl_Correcto" runat="server" ForeColor="Green"></asp:Label>
                    <asp:Label ID="Lbl_Error" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="style3">
                    &nbsp;
                    <igtxt:WebImageButton ID="WIBtn_Guardar" runat="server" Height="22px" OnClick="WIBtn_Guardar_Click"
                        Text="Guardar Cambios" UseBrowserDefaults="False" Width="150px" 
                        ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        <Appearance>
                            <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                            <Style Cursor="Default"></Style>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                    </igtxt:WebImageButton>
                </td>
            </tr>
    </table>
    </div>
    </form>
</body>
</html>
