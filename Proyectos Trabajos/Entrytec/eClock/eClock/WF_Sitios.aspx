<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="WF_Sitios.aspx.cs" Inherits="WF_Sitios" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
        .style2
        {
            height: 296px;
        }
    </style>
</head>
<body style="font-family: 'Segoe UI'; font-size: small">
    <%--<form id="form1" runat="server">
        <table id="Table1" border="0" cellpadding="1" cellspacing="1" style="width: 100%;
            font-family: Arial; height: 319px;" width="300">
            <tr>
                <td align="right" style="height: 0%">
                    <asp:CheckBox ID="Chb_EC_SITIOS" runat="server" AutoPostBack="True" Font-Names="Segoe UI"
                        Font-Size="Small" OnCheckedChanged="CBBorrados_CheckedChanged" 
                        Text="Ver Sitios Borrados" /></td>
            </tr>
            <tr>
                <td align="center" class="style2">
                    <igtbl:ultrawebgrid id="Uwg_EC_SITIOS" runat="server" browser="Xml" 
                        height="280px" oninitializedatasource="Grid_InitializeDataSource"
                        oninitializelayout="Grid_InitializeLayout" width="100%">
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
                                <GroupByBox>
                                    <Style BackColor="ActiveBorder" BorderColor="Window"></Style>
<BoxStyle BackColor="ActiveBorder" BorderColor="Window"></BoxStyle>
                                </GroupByBox>
                                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                </GroupByRowStyleDefault>
                                <ActivationObject BorderColor="" BorderWidth="">
                                </ActivationObject>
                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"  />
                                </FooterStyleDefault>
                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                    <BorderDetails ColorLeft="Window" ColorTop="Window"  />
                                    <Padding Left="3px"  />
                                </RowStyleDefault>
                                <FilterOptionsDefault>
                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                        BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px">
                                        <Padding Left="2px"  />
                                    </FilterOperandDropDownStyle>
                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                    </FilterHighlightRowStyle>
                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px" Height="300px" Width="200px">
                                        <Padding Left="2px"  />
                                    </FilterDropDownStyle>
                                </FilterOptionsDefault>
                                <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Left">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"  />
                                </HeaderStyleDefault>
                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                </EditCellStyleDefault>
                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="280px"
                                    Width="100%">
                                </FrameStyle>
                                <Pager MinimumPagesForDisplay="2">
                                    
                                </Pager>
                                <AddNewBox>
                                    <Style BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
<BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</BoxStyle>
                                </AddNewBox>
                            </DisplayLayout>
                        </igtbl:ultrawebgrid>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 0%">
                    <asp:Label ID="Lbl_Error" runat="server" Font-Names="Arial Narrow" Font-Size="Smaller"
                        ForeColor="Red"></asp:Label>
                    <asp:Label ID="Lbl_Correcto" runat="server" Font-Names="Arial Narrow"
                            Font-Size="Smaller" ForeColor="Green"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" class="style1">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <igtxt:webimagebutton id="WIBtn_Borrar" runat="server" height="22px" onclick="BBorrar_Click"
                        text="Borrar" usebrowserdefaults="False" width="100px" 
                        ImageTextSpacing="4">
							<Alignments VerticalImage="Middle"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Delete.png" Height="16px" Width="16px"></Image>

<ButtonStyle Cursor="Default" Font-Names="Segoe UI"></ButtonStyle>
							</Appearance>
						</igtxt:webimagebutton>
                    &nbsp; &nbsp;
                    <igtxt:webimagebutton id="WIBtn_Nuevo" runat="server" height="22px" onclick="BAgregar_Click"
                        text="Nueva" usebrowserdefaults="False" width="100px" ImageTextSpacing="4">
<Alignments VerticalImage="Middle"></Alignments>

<Appearance>
<Image Url="./Imagenes/New.png" Height="16px" Width="16px"></Image>

<Style Cursor="Default"></Style>

<ButtonStyle Cursor="Default" Font-Names="Segoe UI"></ButtonStyle>
</Appearance>

<RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" MaxWidth="400" MaxHeight="80" ImageUrl="ig_butXP1wh.gif"></RoundedCorners>
</igtxt:webimagebutton>
                    &nbsp; &nbsp;
                    <igtxt:webimagebutton id="WIBtn_Editar" runat="server" height="22px" onclick="BEditar_Click"
                        text="Editar" usebrowserdefaults="False" width="100px" 
                        ImageTextSpacing="4">
							<Alignments VerticalImage="Middle"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Edit.png" Height="16px" Width="16px"></Image>

<ButtonStyle Cursor="Default" Font-Names="Segoe UI"></ButtonStyle>
							</Appearance>
						</igtxt:webimagebutton>
                    &nbsp; &nbsp;
                </td>
            </tr>
        </table>
    --%>
    </form>
</body>
</html>