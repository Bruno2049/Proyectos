<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_Importacion.aspx.cs" Inherits="WF_Importacion" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Importar Empleados</title>
    <style type="text/css">
        .style1
        {
            width: 233px;
        }
    </style>
</head>
<body style="font-size: small; font-family: 'Segoe UI'; text-align: center; margin: 0px;">
    <form id="form1" runat="server">
    
    <table style="width: 100%;">
        <tr>
            <td colspan="2">
                <table style="width: 100%; height: 41px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height: 10px; width: 10px;">
                            <img name="Titulo_r1_c1" src="skins/Titulo_r1_c1.jpg" width="10" height="10" border="0"
                                id="Titulo_r1_c1" alt="" />
                        </td>
                        <td style="width: 1122px; height: 10px; background-image: url('skins/Titulo_r1_c2.jpg');">
                            &nbsp;</td>
                        <td style="height: 10px; width: 14px;">
                            <img name="Titulo_r1_c3" src="skins/Titulo_r1_c3.jpg" width="10" height="10" border="0"
                                id="Titulo_r1_c3" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px; background-image: url(skins/Titulo_r2_c1.jpg); width: 10px;">
                        </td>
                        <td style="width: 1122px; height: 5px; background-image: url(skins/Titulo_r2_c2.jpg);"
                            align="left">
                            &nbsp;<asp:Label ID="Lbl_Instrucciones" 
                                runat="server" Font-Bold="True" Font-Italic="False"
                                Font-Names="Arial" Font-Size="X-Small" ForeColor="White" 
                                
                                Text="Copie y pegue los datos (Ctrl + C , Ctrl + V) a importar desde una tabla de EXCEL o ACCESS, los encabezados muestran en que campo se guardarán los datos, si el registro existe, se actualizará, en caso contrario,se creará un nuevo registro con los datos especificados. El Nombre es OBLIGATORIO, en caso de encontrar un nombre vacío, se interrumpirá el proceso, los errores se listarán al pie de la página. Usted puede establecer el estado de su empleado, si desea que este inactivo asigne 1 de lo contrario asigne 0."></asp:Label>
                        </td>
                        <td style="height: 5px; background-image: url(skins/Titulo_r2_c3.jpg); width: 14px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 11px; width: 10px;" align="right" valign="top">
                            <img name="Titulo_r3_c1" src="skins/Titulo_r3_c1.jpg" width="10" height="10" border="0"
                                id="Titulo_r3_c1" alt="" />
                        </td>
                        <td style="height: 11px; width: 1122px; background-image: url(skins/Titulo_r3_c2.jpg);" 
                            align="left" valign="top">

                        </td>
                        <td style="height: 11px; width: 10px;" align="left" valign="top">
                            <img name="Titulo_r3_c3" src="skins/Titulo_r3_c3.jpg" width="10" height="9" border="0"
                         
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <igmisc:WebPanel ID="Wpn_Importacion" runat="server" Width="601px" 
                    Height="54px" EnableAppStyling="True"
                    StyleSetName="Caribbean" ExpandEffect="None">
                    <Template>
                        <table style="width: 357px">
                            <tr>
                                <td valign="top">
                                    <asp:RadioButtonList ID="Rbn_Ignorar" runat="server" OnSelectedIndexChanged="RBIgnorar_SelectedIndexChanged"
                                        AutoPostBack="True" Width="331px" RepeatDirection="Horizontal" 
                                        Height="1px">
                                        <asp:ListItem Selected="True">Ignorar Registros en Blanco</asp:ListItem>
                                        <asp:ListItem>Aplicar Registros en Blanco</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 3px">
                                    <asp:Label ID="Lbl_Advertencia" runat="server" ForeColor="DarkRed" Text="Advertencia: Al aplicar los registros en blanco se borraran los datos existentes del Empleado y se guardaran en blanco"
                                        Width="594px" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </Template>
                    <Header Text="Opciones Avanzadas">
                    </Header>
                </igmisc:WebPanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <igtbl:UltraWebGrid Style="left: 0px; float: left; top: 0px" ID="Uwg_Importacion"
                    runat="server" Browser="Xml" Height="220" Width="100%" 
                    StyleSetName="Caribbean">
                    <Bands>
                        <igtbl:UltraGridBand>
                            <AddNewRow View="NotSet" Visible="NotSet">
                            </AddNewRow>
                        </igtbl:UltraGridBand>
                    </Bands>
                    <DisplayLayout Version="4.00" SelectTypeCellDefault="Extended" AllowColSizingDefault="Free"
                        AllowUpdateDefault="Yes" Name="Uwg_Importacion" BorderCollapseDefault="Separate"
                        AllowDeleteDefault="Yes" TableLayout="Fixed" AllowRowNumberingDefault="Continuous"
                        RowHeightDefault="20px" AllowColumnMovingDefault="OnServer" SelectTypeColDefault="Single"
                        SelectTypeRowDefault="Extended" HeaderTitleModeDefault="Always" StationaryMargins="Header">
                        <GroupByBox>
                            <BandLabelStyle ForeColor="White" BackColor="#6372D4">
                            </BandLabelStyle>
                            <BoxStyle CssClass="igwgGrpBoxBlack2k7">
                            </BoxStyle>
                        </GroupByBox>
                        <GroupByRowStyleDefault BackColor="#F4FBFE">
                        </GroupByRowStyleDefault>
                        <ActivationObject BorderWidth="1px" BorderStyle="Solid" BorderColor="204, 237, 252">
                            <BorderDetails WidthRight="0px" WidthLeft="0px"></BorderDetails>
                        </ActivationObject>
                        <FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                            </BorderDetails>
                        </FooterStyleDefault>
                        <RowStyleDefault BackColor="Window" Height="10px">
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
                        <RowSelectorStyleDefault BackgroundImage="none" BackColor="White" Width="40px">
                        </RowSelectorStyleDefault>
                        <SelectedRowStyleDefault BackgroundImage="images/Office2003SelRow.png" BorderWidth="0px"
                            BorderStyle="None" BackColor="#E0F1F9" CustomRules="background-repeat: repeat-x;">
                            <Padding Left="7px"></Padding>
                        </SelectedRowStyleDefault>
                        <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" ForeColor="#555555" HorizontalAlign="Center"
                            BorderStyle="None" Font-Size="X-Small" Font-Names="Trebuchet MS,Verdana,Arial,sans-serif"
                            Font-Bold="True" Height="23px" Cursor="Hand" BackColor="LightBlue" BorderColor="CadetBlue">
                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                            </BorderDetails>
                            <Padding Left="5px"></Padding>
                        </HeaderStyleDefault>
                        <Images>
                            <CollapseImage Url="Themes/Aero/ig_treeArrowMinus.png"></CollapseImage>
                            <FixedHeaderOnImage Url="Themes/Aero/ig_tblFixedOn.gif"></FixedHeaderOnImage>
                            <ExpandImage Url="Themes/Aero/ig_treeArrowPlus.png"></ExpandImage>
                            <CurrentRowImage Url="Themes/Aero/ig_CurrentRow.gif"></CurrentRowImage>
                            <FixedHeaderOffImage Url="Themes/Aero/ig_tblFixedOff.gif"></FixedHeaderOffImage>
                        </Images>
                        <EditCellStyleDefault BorderStyle="None" CssClass="EditStyle" Font-Size="9pt" Font-Names="Trebuchet MS,Verdana,Arial,sans-serif"
                            BackColor="White" Height="19px">
                        </EditCellStyleDefault>
                        <FrameStyle BorderWidth="1px" BorderColor="SlateGray" BorderStyle="Solid" Font-Size="XX-Small"
                            Font-Names="Trebuchet MS,Verdana,Arial,sans-serif" BackColor="White" Width="100%"
                            Height="220px">
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
            </td>
        </tr>
        <tr>
            <td colspan="2">
                    <asp:label id="Lbl_Correcto" runat="server" ForeColor="Green"></asp:label>
                <asp:Label ID="Lbl_Error" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="2">
                            <igtxt:WebImageButton ID="WIBtn_Guardar" 
                                OnClick="WIBtn_GuardarCambios_Click" runat="server"
                                Height="22px" Width="210px" UseBrowserDefaults="False" Text="Guardar y Agregar Mas"
                                ImageTextSpacing="4">
                                <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                                <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                    HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                    PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                                <Appearance>
                                    <style cursor="Default">
                                        
                                    </style>
                                    <Image Url="./Imagenes/Save_as.png" Height="16px" Width="16px" />
                                </Appearance>
                            </igtxt:WebImageButton>
                            
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                            &nbsp;</td>
            <td style="text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Lbl_Importar" runat="server" 
                                            Text="Importación por archivo TXT separado por tabulador:"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:FileUpload ID="Fup_Importar" runat="server" />
                                    </td>
                                    <td style="text-align: left">
                <igtxt:WebImageButton ID="WIBtn_Importar" runat="server" Height="22px" OnClick="Btn_Importar_Click"
                                Text="Importar" ImageTextSpacing="4">
                                <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                                <Appearance>
                                    <Image Url="./Imagenes/Iconos/Importar16.png" Height="16px" Width="16px" />
                                    <ButtonStyle Cursor="Default">
                                    </ButtonStyle>
                                </Appearance>
                                <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                    HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                    PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            </igtxt:WebImageButton>
                                    </td>
                                </tr>
                            </table>
                            
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
