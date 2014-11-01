<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WF_CargaComidasExpress.aspx.cs" Inherits="WF_CargaComidasExpress" %>
<%@ Register Assembly="Infragistics2.WebUI.WebSchedule.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="ig_sched" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    &nbsp;
<table style="width: 760px">
        <tr>
            <td style="height: 15px;">
                &nbsp;<igsch:webcalendar id="FIL_FECHA" runat="server">
                <Layout FirstDayOfWeek="Sunday" ShowTitle="False" DayNameFormat="FirstLetter" ShowFooter="False" FooterFormat="" >
                <TodayDayStyle BorderColor="CornflowerBlue" BackColor="Transparent" ></TodayDayStyle>
                <WeekendDayStyle BackColor="#E0E0E0" ></WeekendDayStyle>
                <SelectedDayStyle BackColor="CornflowerBlue" ></SelectedDayStyle>
                <DayStyle BackColor="White" Font-Size="9pt" Font-Names="Arial" ></DayStyle>
                <OtherMonthDayStyle ForeColor="#ACA899"></OtherMonthDayStyle>
                <DayHeaderStyle ForeColor="White" BackColor="#7A96DF" Font-Size="9pt" Font-Names="Tahoma"></DayHeaderStyle>
                <TitleStyle BackColor="#9EBEF5" ></TitleStyle>
                <CalendarStyle Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Font-Overline="False" Font-Bold="False" ></CalendarStyle>
                </Layout>
                </igsch:webcalendar>
                &nbsp; &nbsp;
            </td>
            <td style="height: 15px" width="450">
    <igtbl:UltraWebGrid Style="left: 360px; float: left; top: 0px" ID="Uwg_CopyPaste"
                    runat="server" Browser="Xml" Height="180px" Width="100%">
                    <Bands>
                        <igtbl:UltraGridBand>
                            <AddNewRow View="NotSet" Visible="NotSet">
                            </AddNewRow>
                        </igtbl:UltraGridBand>
                    </Bands>
                    <DisplayLayout Version="4.00" SelectTypeCellDefault="Extended" AllowColSizingDefault="Free"
                        AllowUpdateDefault="Yes" Name="Uwg_CopyPaste" BorderCollapseDefault="Separate"
                        AllowDeleteDefault="Yes" TableLayout="Fixed" AllowRowNumberingDefault="Continuous"
                        RowHeightDefault="20px" AllowColumnMovingDefault="OnServer" SelectTypeColDefault="Single"
                        SelectTypeRowDefault="Extended" HeaderTitleModeDefault="Always" StationaryMargins = "Header">
                        <GroupByBox>
                            <BandLabelStyle ForeColor="White" BackColor="#6372D4">
                            </BandLabelStyle>
                            <Style CssClass="igwgGrpBoxBlack2k7"></Style>
                        </GroupByBox>
                        <GroupByRowStyleDefault BackColor="#F4FBFE">
                        </GroupByRowStyleDefault>
                        <ActivationObject BorderWidth="1px" BorderStyle="Solid" BorderColor="204, 237, 252">
                            <BorderDetails WidthRight="0px" WidthLeft="0px"></BorderDetails>
                        </ActivationObject>
                        <FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                        </FooterStyleDefault>
                        <RowStyleDefault BackColor="Window" Height="19px">
                            <BorderDetails StyleLeft="Solid" ColorTop="241, 241, 241" WidthLeft="1px" WidthTop="1px"
                                ColorLeft="230, 230, 230" StyleTop="Solid"></BorderDetails>
                            <Padding Left="6px"></Padding>
                        </RowStyleDefault>
                        <FilterOptionsDefault>
                            <FilterHighlightRowStyle ForeColor="White" BackColor="#151C55">
                            </FilterHighlightRowStyle>
                            <FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px"
                                Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" Width="200px"
                                CustomRules="overflow:auto;">
                                <Padding Left="2px"></Padding>
                            </FilterDropDownStyle>
                        </FilterOptionsDefault>
                        <RowSelectorStyleDefault BackgroundImage="None" BackColor="White" Width="40px">
                        </RowSelectorStyleDefault>
                        <ClientSideEvents ClipboardError="Grid_OnClipboardError">
                        </ClientSideEvents>
                        <SelectedRowStyleDefault BackgroundImage="images/Office2003SelRow.png" BorderWidth="0px" BorderStyle="None" 
                            BackColor="#E0F1F9" CustomRules="background-repeat: repeat-x;">
                            <Padding Left="7px"></Padding>
                        </SelectedRowStyleDefault>
                        <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" ForeColor="#555555" HorizontalAlign="Center" BorderStyle="None"
                             Font-Size="X-Small" Font-Names="Trebuchet MS,Verdana,Arial,sans-serif"
                            Font-Bold="True" Height="23px" Cursor="Hand" BackColor="LightBlue" BorderColor="CadetBlue">
                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                            <Padding Left="5px"></Padding>
                        </HeaderStyleDefault>
                        <Images>
                            <CollapseImage Url="Themes/Aero/ig_treeArrowMinus.png"></CollapseImage>
                            <FixedHeaderOnImage Url="Themes/Aero/ig_tblFixedOn.gif"></FixedHeaderOnImage>
                            <ExpandImage Url="Themes/Aero/ig_treeArrowPlus.png"></ExpandImage>
                            <CurrentRowImage Url="Themes/Aero/ig_CurrentRow.gif"></CurrentRowImage>
                            <FixedHeaderOffImage Url="Themes/Aero/ig_tblFixedOff.gif"></FixedHeaderOffImage>
                        </Images>
                        <SelectedHeaderStyleDefault BackgroundImage="~/Themes/Aero/grid_header_selected_bg.jpg">
                        </SelectedHeaderStyleDefault>
                        <EditCellStyleDefault BorderStyle="None" CssClass="EditStyle" Font-Size="9pt" Font-Names="Trebuchet MS,Verdana,Arial,sans-serif"
                            BackColor="White" Height="19px">
                        </EditCellStyleDefault>
                        <FrameStyle BorderWidth="1px" BorderColor="SlateGray" BorderStyle="Solid" Font-Size="X-Small"
                            Font-Names="Trebuchet MS,Verdana,Arial,sans-serif" BackColor="White" Width="100%"
                            Height="180px">
                        </FrameStyle>
                        <Pager>
                            <Style CssClass="igwgPgrBlack2k7"></Style>
                        </Pager>
                        <AddNewBox Hidden="False">
                            <Style CssClass="igwgAddNewBoxBlack2k7"></Style>
                        </AddNewBox>
                    </DisplayLayout>
                </igtbl:UltraWebGrid><br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <igtxt:WebImageButton ID="WIBtn_Guardar" runat="server" Height="22px" OnClick="WIBtn_Guardar_Click"
                    Text="Guardar y Agregar Mas" UseBrowserDefaults="False" Width="210px" 
                    ImageTextSpacing="4">
                    <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                        PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                    <Appearance>

                        <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                    </Appearance>
                </igtxt:WebImageButton>
                &nbsp; &nbsp; &nbsp; &nbsp;
                <igtxt:WebImageButton ID="WIBtn_Continuar" runat="server" Height="22px" OnClick="WIBtn_Continuar_Click"
                    Text="Guardar y Continuar" UseBrowserDefaults="False" Width="210px" 
                    ImageTextSpacing="4">
                    <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                    <Appearance>
                        <Image Url="./Imagenes/Next.png" Height="16px" Width="16px" />
                        <Style Cursor="Default"></Style>
                    </Appearance>
                    <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                        HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                        PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                </igtxt:WebImageButton>
            </td>
        </tr>
    </table>
    </asp:Content>