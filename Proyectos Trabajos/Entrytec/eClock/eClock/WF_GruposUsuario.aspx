<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="WF_GruposUsuario.aspx.cs" Inherits="WF_GruposUsuario" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table width="100%" >
            <tr>
                <td style="font-size: 12px; font-style: italic; font-family: arial" align="left">
                    Asignando Grupos al Usuario: &nbsp;<asp:Label ID="lnombre" runat="server" Width="416px"></asp:Label></td>
            </tr>
            <tr>
                <td height="200" valign="top">
                    <igtbl:ultrawebgrid id="UWGGrupo" runat="server" browser="Xml" height="200px"
                        width="100%" OnInitializeDataSource = "UWGGrupo_InitializeDataSource"  OnInitializeLayout="UWGGrupo_InitializeLayout" OnUpdateRowBatch="UWGGrupo_UpdateRowBatch"><Bands>
<igtbl:UltraGridBand>
<AddNewRow View="NotSet" Visible="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
</Bands>

<DisplayLayout ViewType="OutlookGroupBy" Version="4.00" AllowSortingDefault="OnClient" StationaryMargins="Header" AllowColSizingDefault="Free" AllowUpdateDefault="Yes" LoadOnDemand="Xml" StationaryMarginsOutlookGroupBy="True" HeaderClickActionDefault="SortMulti" Name="UWGGrupo" BorderCollapseDefault="Separate" AllowDeleteDefault="Yes" RowSelectorsDefault="No" TableLayout="Fixed" RowHeightDefault="20px" AllowColumnMovingDefault="OnServer" SelectTypeRowDefault="Extended">
<GroupByBox>
<Style BorderColor="Window" BackColor="ActiveBorder"></Style>
</GroupByBox>

<GroupByRowStyleDefault BorderColor="Window" BackColor="Control"></GroupByRowStyleDefault>

<ActivationObject BorderWidth="" BorderColor=""></ActivationObject>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</FooterStyleDefault>

<RowStyleDefault BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="8.25pt" Font-Names="Microsoft Sans Serif" BackColor="Window">
<BorderDetails ColorTop="Window" ColorLeft="Window"></BorderDetails>

<Padding Left="3px"></Padding>
</RowStyleDefault>

<FilterOptionsDefault>
<FilterOperandDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" CustomRules="overflow:auto;">
<Padding Left="2px"></Padding>
</FilterOperandDropDownStyle>

<FilterHighlightRowStyle ForeColor="White" BackColor="#151C55"></FilterHighlightRowStyle>

<FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" Width="200px" Height="300px" CustomRules="overflow:auto;">
<Padding Left="2px"></Padding>
</FilterDropDownStyle>
</FilterOptionsDefault>

<HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" HorizontalAlign="Left" BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</HeaderStyleDefault>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>

<FrameStyle BorderWidth="1px" BorderColor="InactiveCaption" BorderStyle="Solid" Font-Size="8.25pt" Font-Names="Microsoft Sans Serif" BackColor="Window" Width="100%" Height="200px"></FrameStyle>

<Pager MinimumPagesForDisplay="2">
<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
</Pager>

<AddNewBox Hidden="False">
<Style BorderWidth="1px" BorderColor="InactiveCaption" BorderStyle="Solid" BackColor="Window">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
</AddNewBox>
</DisplayLayout>
</igtbl:ultrawebgrid>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <igtxt:webimagebutton id="btnregresar" runat="server" height="22px" onclick="btnregresar_Click"
                        text="Regresar" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom"  />
                        <Appearance>
                            <Image Url="./Imagenes/Back.png" Height="16px" Width="16px"  />
                            <Style Cursor="Default"></Style>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"  />
                    </igtxt:webimagebutton>
                    &nbsp; &nbsp; &nbsp; &nbsp;
                    <igtxt:webimagebutton id="btnguardar" runat="server" height="22px" onclick="btnguardar_Click"
                        text="Guardar Cambios" usebrowserdefaults="False" width="150px" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom"  />
                        <Appearance>
                            <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px"  />
                            <Style Cursor="Default"></Style>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"  />
                    </igtxt:webimagebutton>
                </td>
            </tr>
        </table>
    
</asp:Content>