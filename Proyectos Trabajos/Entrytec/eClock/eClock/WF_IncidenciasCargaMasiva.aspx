<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_IncidenciasCargaMasiva.aspx.cs" Inherits="WF_IncidenciasCargaMasiva" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>

<%@ Register assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.Misc" tagprefix="igmisc" %>

<%@ Register assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>

<%@ Register src="WUC__IncidenciaEd.ascx" tagname="WUC__IncidenciaEd" tagprefix="uc1" %>

<%@ Register assembly="Infragistics2.WebUI.WebNavBar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebNavBar" tagprefix="ignavbar" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebToolbar" tagprefix="igtbar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding:0px 0px 0px 50px; margin-left: 20px;">
    <igmisc:WebPanel ID="Wpn_AGREGAR_INCIDENCIAS_" runat="server" BackColor="White" BorderColor="SteelBlue"
                            BorderStyle="Outset" BorderWidth="2px" Font-Bold="False" 
                            Font-Names="ARIAL" ForeColor="Black"
                            StyleSetName="Caribbean" EnableAppStyling="True" 
                            Width="865px" Height="100%">
        <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ForeColor="Black">
            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                ColorTop="0, 45, 150" />
            <Padding Top="5px" Left="5px" Right="5px" Bottom="5px"></Padding>

            <BorderDetails ColorLeft="158, 190, 245" ColorTop="0, 45, 150" ColorRight="0, 45, 150" ColorBottom="0, 45, 150"></BorderDetails>
        </PanelStyle>
        <Header TextAlignment="Left" Text="Carga Masiva de Incidencias">
        </Header>
        <Template>
        <asp:Table ID="Table1" runat="server" Height="100%" Width="100%">
        <asp:TableHeaderRow>
        <asp:TableCell>
            <asp:Label ID="Lbl_Instrucciones" runat="server"></asp:Label>
            <br />
            <br />
            <igtxt:WebImageButton ID="WIBtn_GuardarContinuar" Text="Guardar y Agregar Mas" runat="server" 
                OnClick="WIBtn_GuardarContinuar_Click" UseBrowserDefaults="False">
                <Alignments VerticalImage="Middle" />
                <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                    HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                    PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                <Appearance>
                    <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                </Appearance>
            </igtxt:WebImageButton>
            <igtxt:WebImageButton ID="WIBtn_Salir" Text="Salir" runat="server" 
                OnClick="WIBtn_Salir_Click" UseBrowserDefaults="False" >
                <Alignments VerticalImage="Middle" />
                <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                    HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                    PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                <Appearance>
                    <Image Url="./Imagenes/Next.png" Height="16px" Width="16px" />
                </Appearance>
            </igtxt:WebImageButton>
            <br />
            <br />
            <asp:Label ID="Lbl_Mensaje_Correcto" runat="server" ForeColor="Green"></asp:Label>
            <asp:Label ID="Lbl_Mensaje_Error" runat="server" ForeColor="Red"></asp:Label>
            <uc1:WUC__IncidenciaEd ID="CamposIncidencia" runat="server" />
        </asp:TableCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell>
            <igtbl:UltraWebGrid Style="left: 360px; float: left; top: 0px" ID="UWG_ASIGNACION_INCIDENCIAS"
                runat="server" Browser="Xml" Height="100%" Width="100%">
                <Bands>
                    <igtbl:UltraGridBand>
                        <AddNewRow View="NotSet" Visible="NotSet">
                        </AddNewRow>
                    </igtbl:UltraGridBand>
                </Bands>
                <DisplayLayout Version="4.00" SelectTypeCellDefault="Extended" AllowColSizingDefault="Free"
                    AllowUpdateDefault="Yes" Name="UWG_ASIGNACION_INCIDENCIAS" BorderCollapseDefault="Separate"
                    AllowDeleteDefault="Yes" TableLayout="Fixed" AllowRowNumberingDefault="Continuous"
                    RowHeightDefault="20px" AllowColumnMovingDefault="OnServer" SelectTypeColDefault="Single"
                    SelectTypeRowDefault="Extended" HeaderTitleModeDefault="Always" StationaryMargins = "Header">
                    <GroupByRowStyleDefault BackColor="Gray">
                    </GroupByRowStyleDefault>
                    <ActivationObject BorderWidth="1px" BorderStyle="Solid" BorderColor="Gray">
                        <BorderDetails WidthRight="0px" WidthLeft="0px"></BorderDetails>
                    </ActivationObject>
                    <RowStyleDefault BackColor="Window" Height="19px">
                        <BorderDetails StyleLeft="Solid" ColorTop="Black" WidthLeft="1px" WidthTop="1px"
                            ColorLeft="Gray" StyleTop="Solid"></BorderDetails>
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
                    <RowSelectorStyleDefault BackgroundImage="none" BackColor="White" Width="40px">
                    </RowSelectorStyleDefault>
                    <ClientSideEvents ClipboardError="Grid_OnClipboardError">
                    </ClientSideEvents>
                    <SelectedRowStyleDefault BackgroundImage="images/Office2003SelRow.png" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid"
                        BackColor="#ADD8E6" CustomRules="background-repeat: repeat-x;">
                        <Padding Left="7px"></Padding>
                    </SelectedRowStyleDefault>
                    <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" ForeColor="#555555" HorizontalAlign="Center" BorderStyle="None"
                            Font-Size="X-Small" Font-Names="Trebuchet MS,Verdana,Arial,sans-serif"
                        Font-Bold="True" Height="23px" Cursor="Hand" BackColor="LightBlue" BorderColor="CadetBlue">
                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                        <Padding Left="5px"></Padding>
                    </HeaderStyleDefault>
                    
                    <EditCellStyleDefault BorderStyle="None" CssClass="EditStyle" Font-Size="9pt" Font-Names="Trebuchet MS,Verdana,Arial,sans-serif"
                        BackColor="White" Height="19px">
                    </EditCellStyleDefault>
                    <FrameStyle BorderWidth="1px" BorderColor="SlateGray" BorderStyle="Solid" Font-Size="X-Small"
                        Font-Names="Trebuchet MS,Verdana,Arial,sans-serif" BackColor="White" Width="100%"
                        Height="100%">
                    </FrameStyle>
                    <Pager>
                        <PagerStyle CssClass="igwgPgrBlack2k7" />
                    </Pager>
                    <AddNewBox Hidden="False">
                        <BoxStyle CssClass="igwgAddNewBoxBlack2k7">
                        </BoxStyle>
                    </AddNewBox>
                </DisplayLayout>
            </igtbl:UltraWebGrid>
            </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        </Template>
    </igmisc:WebPanel>
    </div>
    </form>
</body>
</html>
