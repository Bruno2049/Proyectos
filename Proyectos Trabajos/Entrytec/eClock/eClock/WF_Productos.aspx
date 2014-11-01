<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="WF_Productos.aspx.cs" Inherits="WF_Productos" %>

<%@ Register Src="WC_Menu.ascx" TagName="WC_Menu" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%">
            <tr>
                <td valign="top" style="height: 20px" align="right">
                    <asp:CheckBox ID="CBBorrados" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="Small" Text="Ver Productos Borrados" /></td>
            </tr>
            <tr>
                <td valign="top" style="height: 300px">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp;<igtbl:UltraWebGrid ID="UWGProductos" runat="server"  OnInitializeDataSource="UWGProductos_InitializeDataSource" OnInitializeLayout="UWGProductos_InitializeLayout"
                        Width="100%" style="left: 3px; top: 243px">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                            HeaderClickActionDefault="SortMulti" Name="UWGProductos" RowHeightDefault="20px"
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
                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt"
                                Width="100%">
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
                    </igtbl:UltraWebGrid>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <igtxt:WebImageButton ID="btnborrar" runat="server" Height="22px" Text="Borrar" UseBrowserDefaults="False"
                        Width="100px" OnClick="btnborrar_Click" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Appearance>
                            <Image Url="./Imagenes/Delete.png" Height="16px" Width="16px" />
                            <Style>
<Padding Top="4px"></Padding>
</Style>
                        </Appearance>
                        <ClientSideEvents Click="BBorrar_Click" MouseDown="BBorrar_Click" />
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;&nbsp;
                    <igtxt:WebImageButton ID="btnnuevo" runat="server" Height="22px" OnClick="btnnuevo_Click"
                        Text="Nuevo" UseBrowserDefaults="False" Width="100px" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Appearance>
                            <Image Url="./Imagenes/New.png" Height="16px" Width="16px" />
                            <Style>
<Padding Top="4px"></Padding>
</Style>
                        </Appearance>
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;&nbsp;
                    <igtxt:WebImageButton ID="btneditar" runat="server" Height="22px" Text="Editar" UseBrowserDefaults="False"
                        Width="100px" OnClick="btneditar_Click" ImageTextSpacing="4">
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Appearance>
                            <Image Url="./Imagenes/Edit.png" Height="16px" Width="16px" />
                            <Style>
<Padding Top="4px"></Padding>
</Style>
                        </Appearance>
                        <ClientSideEvents Click="BEditar_Click" MouseDown="BEditar_Click" />
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                    </igtxt:WebImageButton>
                </td>
            </tr>
        </table>
</asp:Content>