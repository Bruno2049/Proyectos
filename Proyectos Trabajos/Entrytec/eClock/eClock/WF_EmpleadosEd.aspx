<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_EmpleadosEd.aspx.cs" Inherits="WF_EmpleadosEd" %>

<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edicion de Personas</title>
    <style type="text/css">
        .style1
        {
            height: 24px;
        }
        .style2
        {
            width: 614px;
        }
        .style5
        {
            height: 24px;
            width: 153px;
        }
        .style6
        {
            width: 153px;
        }
        .style9
        {
            width: 3px;
            height: 25px;
        }
        .style10
        {
            width: 159px;
            height: 25px;
        }
        .style11
        {
            width: 169px;
            height: 24px;
        }
        .style14
        {
            height: 25px;
        }
        .style17
        {
            width: 169px;
            height: 25px;
        }
        .style22
        {
            text-align: center;
            height: 25px;
        }
        .style24
        {
            text-align: center;
            width: 3px;
            height: 25px;
        }
        .style25
        {
            text-align: center;
            width: 159px;
            height: 25px;
        }
        .style26
        {
            text-align: center;
            width: 169px;
            height: 25px;
        }
        .style27
        {
            width: 3px;
            height: 24px;
        }
        .style28
        {
            width: 159px;
            height: 24px;
        }
        .style40
        {
            width: 34px;
            height: 25px;
        }
        .style41
        {
            width: 62px;
            height: 25px;
        }
        .style43
        {
            width: 59px;
            height: 22px;
        }
        .style44
        {
            width: 120px;
            height: 22px;
        }
        .style51
        {
            width: 59px;
            height: 21px;
        }
        .style52
        {
            width: 120px;
            height: 21px;
        }
    </style>
    <script type="text/javascript" id="igClientScript">
<!--

<!--// Valida que solo se introduzcan números -->
function Wtx_PERSONA_S_HUELLA_CLAVE_KeyPress(oEdit, keyCode, oEvent){
    //Add code to handle your event here.
    if (keyCode < 48 || keyCode > 57)
        oEvent.cancel = true;
} 


function Wtx_PERSONA_LINK_ID_S_HUELLA_KeyPress(oEdit, keyCode, oEvent){
	//Add code to handle your event here.
    if (keyCode < 48 || keyCode > 57)
        oEvent.cancel = true;
} 
// -->
</script>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px;">
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="PanelMain" runat="server" Height="100%" Width="100%">
        <igtab:ultrawebtab id="UltraWebTab1" runat="server" bordercolor="Black" borderstyle="Solid"
            borderwidth="1px" Font-Size="X-Small" EnableAppStyling="True" 
                StyleSetName="Caribbean"><DefaultTabStyle 
                BackColor="PowderBlue"></DefaultTabStyle><Tabs><igtab:Tab 
                    Text="Datos Principales"><ContentTemplate><asp:Panel ID="PanelPrincipal1" 
                        runat="server" Font-Size="Small" Height="100%" Width="100%"><table 
                        style="vertical-align: top; text-align: left; height:auto; width:auto"><tr><td 
                                class="style27"><asp:Label ID="Lbl_PERSONA_LINK_ID" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="150px"> </asp:Label></td><td 
                                class="style28"><igtxt:WebTextEdit ID="Wtx_PERSONA_LINK_ID" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="150px">
                                    </igtxt:WebTextEdit></td><td 
                                class="style11"><asp:Label ID="Lbl_PERSONA_EMAIL" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                class="style28"><igtxt:WebTextEdit ID="Wtx_PERSONA_EMAIL" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                class="style27"><asp:Label ID="Lbl_NOMBRE" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="150px"></asp:Label></td><td 
                                class="style28"><igtxt:WebTextEdit ID="Wtx_NOMBRE" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td><td 
                                colspan="2" rowspan="8" style="text-align: center"><asp:Image ID="Image1" 
                                    runat="server" Height="184px" ImageUrl="WF_Personas_FotoN.aspx" Width="138px" /></td></tr><tr><td 
                                class="style27"><asp:Label ID="Lbl_APATERNO" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="150px"></asp:Label></td><td 
                                class="style28"><igtxt:WebTextEdit ID="Wtx_APATERNO" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                class="style27"><asp:Label ID="Lbl_AMATERNO" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="150px"></asp:Label></td><td 
                                class="style28"><igtxt:WebTextEdit ID="Wtx_AMATERNO" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                class="style9" valign="top"><asp:Label ID="Lbl_NOMBRE_COMPLETO" 
                                runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></asp:Label></td><td 
                                class="style10" valign="top"><igtxt:WebTextEdit ID="Wtx_NOMBRE_COMPLETO" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                class="style9" valign="top"><asp:Label ID="Lbl_CENTRO_DE_COSTOS" 
                                runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></asp:Label></td><td 
                                class="style10" valign="top"><igcmbo:WebCombo ID="Wco_CENTRO_DE_COSTOS" 
                                    runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                    BorderWidth="1px" Editable="True" ForeColor="Black" Height="18px" 
                                    oninitializelayout="Cmb_CentroCostos_InitializeLayout" SelBackColor="DarkBlue" 
                                    SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                            Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                        ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                        RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                            BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                            Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                            BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                            ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                            WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                            BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td></tr><tr><td 
                                class="style9" valign="top"><asp:Label ID="Lbl_AREA" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="150px"></asp:Label></td><td 
                                class="style10" valign="top"><igcmbo:WebCombo ID="Wco_AREA" runat="server" 
                                    BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                    Editable="True" ForeColor="Black" Height="18px" 
                                    oninitializelayout="Cmb_Area_InitializeLayout" SelBackColor="DarkBlue" 
                                    SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                            Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                        ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                        RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                            BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                            Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                            BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                            ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                            WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                            BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td></tr><tr><td 
                                class="style9" valign="top"><asp:Label ID="Lbl_DEPARTAMENTO" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="150px"></asp:Label></td><td 
                                class="style10" valign="top"><igcmbo:WebCombo ID="Wco_DEPARTAMENTO" 
                                    runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                    BorderWidth="1px" Editable="True" ForeColor="Black" Height="18px" 
                                    oninitializelayout="Cmb_Departamento_InitializeLayout" SelBackColor="DarkBlue" 
                                    SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                            Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                        ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                        RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                            BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                            Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                            BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                            ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                            WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                            BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td></tr><tr><td 
                                class="style9" valign="top"><asp:Label ID="Lbl_PUESTO" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="150px"></asp:Label></td><td 
                                class="style10" valign="top"><igcmbo:WebCombo ID="Wco_PUESTO" runat="server" 
                                    BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                    Editable="True" ForeColor="Black" Height="18px" 
                                    oninitializelayout="Cmb_Puesto_InitializeLayout" SelBackColor="DarkBlue" 
                                    SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                            Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                        ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                        RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                            BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                            Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                            BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                            ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                            WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                            BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td></tr><tr><td 
                                class="style9" valign="top"><asp:Label ID="Lbl_AGRUPACION_NOMBRE" 
                                runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></asp:Label></td><td 
                                class="style10" valign="top"><igcmbo:WebCombo ID="Wco_AGRUPACION_NOMBRE" 
                                    runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                    BorderWidth="1px" ForeColor="Black" Height="18px" 
                                    oninitializelayout="Cmb_Agrupacion_InitializeLayout" SelBackColor="DarkBlue" 
                                    SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                            Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                        ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                        RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                            BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                            Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                            BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                            ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                            WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                            BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td><td 
                                align="right" class="style14" colspan="2" valign="top"><input id="Fup_CONFIG_USUARIO_VALOR" runat="server" name="File1" style="width: 240px; height: 18px;" type="file" /></td></tr><tr><td 
                                class="style9" valign="top"><asp:Label ID="Lbl_NO_CREDENCIAL" 
                                runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></asp:Label></td><td 
                                class="style10" valign="top"><igtxt:WebTextEdit ID="Wtx_NO_CREDENCIAL" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td><td 
                                align="right" class="style17" valign="top"><igtxt:WebImageButton 
                                    ID="Btn_CONFIG_USUARIO_VALOR_subir" runat="server" Height="20px" 
                                    ImageTextSpacing="4" OnClick="WebImageButton1_Click" Text="Subir Foto" 
                                    UseBrowserDefaults="False" Width="100px"><Appearance><Image Height="18px" 
                                            Url="./Imagenes/panel-screenshot.png" Width="20px" /><ButtonStyle 
                                            Cursor="Default" Font-Size="X-Small"></ButtonStyle></Appearance><RoundedCorners 
                                        DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" 
                                        HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" 
                                        MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" /><PressedAppearance><ButtonStyle 
                                            Font-Size="Small"></ButtonStyle></PressedAppearance><Alignments 
                                        VerticalAll="Bottom" VerticalImage="Middle" /></igtxt:WebImageButton></td><td 
                                align="right" class="style10" valign="top"><igtxt:WebImageButton 
                                    ID="Btn_CONFIG_USUARIO_VALOR_eliminar" runat="server" Height="20px" 
                                    ImageTextSpacing="4" OnClick="WebImageButton3_Click" Text="Eliminar Foto" 
                                    UseBrowserDefaults="False" Width="104px"><Appearance><Image Height="16px" 
                                            Url="./Imagenes/Cancel.png" Width="16px" /><ButtonStyle Cursor="Default" 
                                            Font-Size="X-Small"></ButtonStyle></Appearance><RoundedCorners 
                                        DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" 
                                        HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" 
                                        MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" /><PressedAppearance><ButtonStyle 
                                            Font-Size="Small"></ButtonStyle></PressedAppearance><Alignments 
                                        VerticalAll="Bottom" VerticalImage="Middle" /></igtxt:WebImageButton></td></tr><tr><td 
                                class="style9" valign="top"><asp:Label ID="Lbl_TURNO_ID" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="150px"></asp:Label></td><td 
                                class="style10" valign="top"><igcmbo:WebCombo ID="Wco_TURNO_ID" runat="server" 
                                    BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                    ForeColor="Black" Height="16px" oninitializelayout="cmbTurno_InitializeLayout" 
                                    SelBackColor="DarkBlue" SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><header 
                                            caption="Column0"></header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                        ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                        RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                            BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                            Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                            BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                            ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                            BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                            WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                            BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td><td 
                                class="style17" valign="top"></td><td align="right" class="style10" 
                                valign="top"><asp:CheckBox ID="Chb_PERSONA_BORRADO" runat="server" 
                                    Font-Size="X-Small" /></td></tr><tr><td class="style24" valign="top">&nbsp;</td><td 
                                class="style22" colspan="2" valign="top"><asp:Label ID="Lbl_Correcto" 
                                runat="server" Font-Size="X-Small" ForeColor="Green"></asp:Label><asp:Label 
                                ID="Lbl_Error" runat="server" Font-Size="X-Small" ForeColor="Red"></asp:Label></td><td 
                                class="style25" valign="top">&nbsp;</td></tr><tr><td class="style24" 
                                valign="top">&nbsp;</td><td class="style25" valign="top"><igtxt:WebImageButton 
                                    ID="Btn_Cancelar" runat="server" AutoSubmit="True" Height="25px" 
                                    OnClick="btCancelar_Click" Text="Cancelar" UseBrowserDefaults="False" 
                                    Width="113px"><Appearance><Image Height="16px" Url="./Imagenes/Cancel.png" 
                                        Width="16px" /></Appearance><RoundedCorners 
                                    DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" 
                                    HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" 
                                    MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" /><Alignments 
                                    HorizontalAll="NotSet" VerticalAll="NotSet" VerticalImage="Middle" /></igtxt:WebImageButton></td><td 
                                class="style26" valign="top"><igtxt:WebImageButton ID="Btn_Guardar" 
                                    runat="server" Height="20px" OnClick="btGuardar_Click" Text="Guardar" 
                                    UseBrowserDefaults="False" Width="113px"><Appearance><Image 
                                            Url="./Imagenes/Save_As.png" /></Appearance><RoundedCorners 
                                        DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" 
                                        HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" 
                                        MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" /></igtxt:WebImageButton></td><td 
                                class="style25" valign="top">&nbsp;</td></tr></table></asp:Panel></ContentTemplate></igtab:Tab><igtab:Tab 
                    Text="Datos Personales"><ContentTemplate><asp:Panel ID="PanelPersonal" 
                            runat="server" Height="100%" Width="100%"><table 
                            style="width: auto; height: auto; vertical-align: top; text-align: left;"><tr><td 
                                    align="left" style="width: 80px; height: 1px" valign="top"><asp:Label 
                                    ID="Lbl_FECHA_NAC" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                    Width="120px"></asp:Label></td><td align="left" 
                                    style="width: 224px; height: 1px" valign="top"><igsch:WebDateChooser 
                                        ID="Wdc_FECHA_NAC" runat="server" Font-Size="X-Small" Height="8px" 
                                        Value="09/24/2009 13:27:53" Width="150px"></igsch:WebDateChooser></td><td 
                                    align="left" style="width: 224px; height: 1px" valign="top"><asp:Label 
                                        ID="Lbl_DP_PAIS" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="120px"></asp:Label></td><td align="left" 
                                    style="width: 224px; height: 1px" valign="top">&nbsp;<igtxt:WebTextEdit 
                                        ID="Wtx_DP_PAIS" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 3px"><asp:Label ID="Lbl_RFC" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 3px"><igtxt:WebTextEdit ID="Wtx_RFC" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td><td 
                                    align="left" style="width: 224px; height: 3px"><asp:Label ID="Lbl_NACIONALIDAD" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 3px">&nbsp;<igtxt:WebTextEdit 
                                        ID="Wtx_NACIONALIDAD" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 3px"><asp:Label ID="Lbl_CURP" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 3px"><igtxt:WebTextEdit ID="Wtx_CURP" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td><td 
                                    align="left" style="width: 224px; height: 3px"><asp:Label ID="Lbl_DP_TELEFONO1" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 3px">&nbsp;<igtxt:WebTextEdit 
                                        ID="Wtx_DP_TELEFONO1" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 5px"><asp:Label ID="Lbl_ESTUDIOS" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 5px"><igtxt:WebTextEdit 
                                        ID="Wtx_ESTUDIOS" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td><td align="left" 
                                    style="width: 224px; height: 5px"><asp:Label ID="Lbl_DP_TELEFONO2" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 5px">&nbsp;<igtxt:WebTextEdit 
                                        ID="Wtx_DP_TELEFONO2" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 5px"><asp:Label ID="Lbl_SEXO" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 5px"><igtxt:WebTextEdit ID="Wtx_SEXO" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td><td 
                                    align="left" style="width: 224px; height: 5px"><asp:Label ID="Lbl_DP_CELULAR1" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 5px">&nbsp;<igtxt:WebTextEdit 
                                        ID="Wtx_DP_CELULAR1" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 5px"><asp:Label ID="Lbl_DP_CP" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 5px"><igtxt:WebTextEdit 
                                        ID="Wtx_DP_CP" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td><td align="left" 
                                    style="width: 224px; height: 5px"><asp:Label ID="Lbl_DP_CELULAR2" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 5px">&nbsp;<igtxt:WebTextEdit 
                                        ID="Wtx_DP_CELULAR2" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 5px"><asp:Label ID="Lbl_DP_CALLE_NO" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 5px"><igtxt:WebTextEdit 
                                        ID="Wtx_DP_CALLE_NO" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td><td align="left" 
                                    style="width: 224px; height: 5px"></td><td align="left" 
                                    style="width: 224px; height: 5px">&nbsp;</td></tr><tr><td 
                                    style="width: 80px; height: 3px"><asp:Label ID="Lbl_DP_COLONIA" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 3px"><igtxt:WebTextEdit 
                                        ID="Wtx_DP_COLONIA" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td><td align="left" 
                                    style="width: 224px; height: 3px"></td><td align="left" 
                                    style="width: 224px; height: 3px">&nbsp;</td></tr><tr><td 
                                    style="width: 80px; height: 23px;"><asp:Label ID="Lbl_DP_DELEGACION" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    align="left" style="width: 224px; height: 23px;"><igtxt:WebTextEdit 
                                        ID="Wtx_DP_DELEGACION" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td><td align="left" 
                                    style="width: 224px; height: 23px;"></td><td align="left" 
                                    style="width: 224px; height: 23px;"></td></tr><tr><td style="width: 80px"><asp:Label 
                                    ID="Lbl_DP_CIUDAD" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                    Width="120px"></asp:Label></td><td align="left" style="width: 224px"><igtxt:WebTextEdit 
                                        ID="Wtx_DP_CIUDAD" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td><td align="left" 
                                    style="width: 224px"></td><td align="left" style="width: 224px"></td></tr><tr><td 
                                style="width: 80px"><asp:Label ID="Lbl_DP_ESTADO" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                align="left" style="width: 224px"><igtxt:WebTextEdit ID="Wtx_DP_ESTADO" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td><td 
                                align="left" style="width: 224px"></td><td align="left" 
                                style="width: 224px"></td></tr></table></asp:Panel></ContentTemplate></igtab:Tab><igtab:Tab 
                    Tag="" Text="Datos Laborales"><ContentTemplate><asp:Panel ID="PanelLaborales" 
                            runat="server" Height="100%" Width="100%"><table 
                            style="width: auto; height: auto; vertical-align: top; text-align: left;"><tr><td 
                                    style="width: 80px; height: 3px"><asp:Label ID="Lbl_COMPANIA" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    style="width: 224px; height: 3px"><igcmbo:WebCombo ID="Wco_COMPANIA" 
                                        runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                        BorderWidth="1px" Editable="True" ForeColor="Black" Height="18px" 
                                        oninitializelayout="Cmb_Compania_InitializeLayout" SelBackColor="DarkBlue" 
                                        SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                                Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                            ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                            RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                                BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                                Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                                BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                                ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                                BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                                WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                                BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td><td 
                                    style="width: 224px; height: 3px"><asp:Label ID="Lbl_DT_TELEFONO1" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    rowspan="1" style="width: 201px"><igtxt:WebTextEdit ID="Wtx_DT_TELEFONO1" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 23px"><asp:Label ID="Lbl_DIVISION" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    style="width: 224px; height: 23px"><igcmbo:WebCombo ID="Wco_DIVISION" 
                                        runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                        BorderWidth="1px" Editable="True" ForeColor="Black" Height="18px" 
                                        oninitializelayout="Cmb_Division_InitializeLayout" SelBackColor="DarkBlue" 
                                        SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                                Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                            ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                            RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                                BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                                Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                                BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                                ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                                BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                                WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                                BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td><td 
                                    style="width: 224px; height: 23px"><asp:Label ID="Lbl_DT_TELEFONO2" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    rowspan="1" style="width: 201px; height: 23px;"><igtxt:WebTextEdit 
                                        ID="Wtx_DT_TELEFONO2" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 3px"><asp:Label ID="Lbl_REGION" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    style="width: 224px; height: 3px"><igcmbo:WebCombo ID="Wco_REGION" 
                                        runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                        BorderWidth="1px" Editable="True" ForeColor="Black" Height="18px" 
                                        oninitializelayout="Cmb_Region_InitializeLayout" SelBackColor="DarkBlue" 
                                        SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                                Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                            ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                            RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                                BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                                Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                                BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                                ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                                BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                                WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                                BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td><td 
                                    style="width: 224px; height: 3px"><asp:Label ID="Lbl_DT_CELULAR1" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    rowspan="1" style="width: 201px"><igtxt:WebTextEdit ID="Wtx_DT_CELULAR1" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 3px"><asp:Label ID="Lbl_ZONA" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    style="width: 224px; height: 3px"><igcmbo:WebCombo ID="Wco_ZONA" runat="server" 
                                        BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                        Editable="True" ForeColor="Black" Height="18px" 
                                        oninitializelayout="Cmb_Zona_InitializeLayout" SelBackColor="DarkBlue" 
                                        SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                                Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                            ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                            RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                                BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                                Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                                BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                                ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                                BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                                WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                                BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td><td 
                                    style="width: 224px; height: 3px"><asp:Label ID="Lbl_DT_CELULAR2" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    rowspan="1" style="width: 201px"><igtxt:WebTextEdit ID="Wtx_DT_CELULAR2" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 3px"><asp:Label ID="Lbl_LINEA_PRODUCCION" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    style="width: 224px; height: 3px"><igcmbo:WebCombo ID="Wco_LINEA_PRODUCCION" 
                                        runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                        BorderWidth="1px" Editable="True" ForeColor="Black" Height="18px" 
                                        oninitializelayout="Cmb_LineaProduccion_InitializeLayout" 
                                        SelBackColor="DarkBlue" SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                                Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                            ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                            RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                                BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                                Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                                BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                                ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                                BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                                WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                                BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td><td 
                                    style="width: 224px; height: 3px"><asp:Label ID="Lbl_DT_DELEGACION" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    rowspan="1" style="width: 201px"><igtxt:WebTextEdit ID="Wtx_DT_DELEGACION" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 3px"><asp:Label ID="Lbl_GRUPO" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    style="width: 224px; height: 3px"><igcmbo:WebCombo ID="Wco_GRUPO" 
                                        runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                        BorderWidth="1px" Editable="True" ForeColor="Black" Height="18px" 
                                        oninitializelayout="Cmb_Grupo_InitializeLayout" SelBackColor="DarkBlue" 
                                        SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                                Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                            ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                            RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                                BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                                Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                                BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                                ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                                BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                                WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                                BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td><td 
                                    style="width: 224px; height: 3px"><asp:Label ID="Lbl_DT_CIUDAD" runat="server" 
                                        Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    rowspan="1" style="width: 201px"><igtxt:WebTextEdit ID="Wtx_DT_CIUDAD" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 3px"><asp:Label ID="Lbl_FECHA_INGRESO" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    style="width: 224px; height: 3px"><igsch:WebDateChooser ID="Wdc_FECHA_INGRESO" 
                                        runat="server" Font-Size="X-Small" Value="09/24/2009 13:27:53" Width="150px"></igsch:WebDateChooser></td><td 
                                    style="width: 224px; height: 3px"><asp:Label ID="Lbl_DT_ESTADO" runat="server" 
                                        Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    rowspan="1" style="width: 201px"><igtxt:WebTextEdit ID="Wtx_DT_ESTADO" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 26px"><asp:Label ID="Lbl_FECHA_BAJA" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    style="width: 224px; height: 26px"><igsch:WebDateChooser ID="Wdc_FECHA_BAJA" 
                                        runat="server" Font-Size="X-Small" Value="09/24/2009 13:27:53" Width="150px"></igsch:WebDateChooser></td><td 
                                    style="width: 224px; height: 26px"><asp:Label ID="Lbl_DT_PAIS" runat="server" 
                                        Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    rowspan="1" style="width: 201px; height: 26px"><igtxt:WebTextEdit 
                                        ID="Wtx_DT_PAIS" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 7px"><asp:Label ID="Lbl_CLAVE_EMPL" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    style="width: 224px; height: 7px"><igtxt:WebTextEdit ID="Wtx_CLAVE_EMPL" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td><td 
                                    style="width: 224px; height: 7px"><asp:Label ID="Lbl_DT_CP" runat="server" 
                                        Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    rowspan="1" style="width: 201px; height: 7px"><igtxt:WebTextEdit ID="Wtx_DT_CP" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                    style="width: 80px; height: 7px"><asp:Label ID="Lbl_DT_CALLE_NO" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    style="width: 224px; height: 7px"><igtxt:WebTextEdit ID="Wtx_DT_CALLE_NO" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td><td 
                                    style="width: 224px; height: 7px"></td><td rowspan="1" 
                                    style="width: 201px; height: 7px"></td></tr><tr><td 
                                    style="width: 80px; height: 7px"><asp:Label ID="Lbl_DT_COLONIA" 
                                    runat="server" Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    style="width: 224px; height: 7px"><igtxt:WebTextEdit ID="Wtx_DT_COLONIA" 
                                        runat="server" Font-Names="Arial" Font-Size="X-Small" Width="150px"></igtxt:WebTextEdit></td><td 
                                    style="width: 224px; height: 7px"></td><td rowspan="1" 
                                    style="width: 201px; height: 7px; vertical-align: top; text-align: left;"></td></tr></table></asp:Panel></ContentTemplate></igtab:Tab><igtab:Tab 
                    Text="Datos de Nomina"><ContentTemplate><asp:Panel ID="PanelNomina" runat="server" 
                            Height="100%" Width="100%"><table 
                            style="height:auto; width:auto; vertical-align: top; text-align: left;"><tr><td 
                                    class="style40"><asp:Label ID="Lbl_TIPO_NOMINA" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                    class="style41"><igcmbo:WebCombo ID="Wco_TIPO_NOMINA" runat="server" 
                                        BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                        Editable="True" ForeColor="Black" Height="18px" 
                                        oninitializelayout="Cmb_TipoNomina_InitializeLayout" SelBackColor="DarkBlue" 
                                        SelForeColor="White" Version="4.00" Width="150px"><Columns><igtbl:UltraGridColumn><Header 
                                                Caption="Column0"></Header></igtbl:UltraGridColumn></Columns><ExpandEffects 
                                            ShadowColor="LightGray" /><DropDownLayout BorderCollapse="Separate" 
                                            RowHeightDefault="20px" Version="4.00"><FrameStyle BackColor="Silver" 
                                                BorderStyle="Ridge" BorderWidth="2px" Cursor="Default" Font-Names="Verdana" 
                                                Font-Size="10pt" Height="130px" Width="325px"></FrameStyle><HeaderStyle 
                                                BackColor="LightGray" BorderStyle="Solid"><BorderDetails ColorLeft="White" 
                                                ColorTop="White" WidthLeft="1px" WidthTop="1px" /></HeaderStyle><RowStyle 
                                                BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"><BorderDetails 
                                                WidthLeft="0px" WidthTop="0px" /></RowStyle><SelectedRowStyle 
                                                BackColor="DarkBlue" ForeColor="White" /></DropDownLayout></igcmbo:WebCombo></td></tr><tr><td 
                                    class="style40"><asp:Label ID="Lbl_IMSS" runat="server" Font-Names="Arial" 
                                    Font-Size="X-Small" Width="120px"></asp:Label></td><td class="style41"><igtxt:WebTextEdit 
                                        ID="Wtx_IMSS" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td class="style40"><asp:Label 
                                    ID="Lbl_REGLA_VACA_ID" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                    Width="120px"></asp:Label></td><td class="style41"><igtxt:WebTextEdit 
                                        ID="Wtx_REGLA_VACA_ID" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td class="style40"><asp:Label 
                                    ID="Lbl_SUELDO_DIA" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                    Width="120px"></asp:Label></td><td class="style41"><igtxt:WebTextEdit 
                                        ID="Wtx_SUELDO_DIA" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td class="style40"><asp:Label 
                                    ID="LSueldoHora" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                    Text="Label" Width="120px"></asp:Label></td><td class="style41"><igtxt:WebTextEdit 
                                        ID="txtSueldoHora" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                        Width="150px"></igtxt:WebTextEdit></td></tr><tr><td class="style40"></td><td 
                                    class="style41">&nbsp;</td></tr><tr><td class="style40"></td><td class="style41">&nbsp;</td></tr><tr><td 
                                class="style40"></td><td class="style41">&nbsp;</td></tr><tr><td class="style40"></td><td 
                                class="style41">&nbsp;</td></tr><tr><td class="style40"></td><td class="style41">&nbsp;</td></tr></table></asp:Panel></ContentTemplate></igtab:Tab><igtab:Tab 
                    Text="Otros"><ContentTemplate><asp:Panel ID="PanelOtros" runat="server" 
                        Height="100%" Width="100%"><table 
                        style="vertical-align: top; text-align: left; height:auto; width:auto"><tr><td 
                                class="style51"><asp:Label ID="Lbl_CAMPO1" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                class="style52"><igtxt:WebTextEdit ID="Wtx_CAMPO1" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" OnValueChange="WebTextEdit1_ValueChange" 
                                    Width="150px"></igtxt:WebTextEdit></td></tr><tr><td class="style51"><asp:Label 
                                ID="Lbl_CAMPO2" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                Width="120px"></asp:Label></td><td class="style52"><igtxt:WebTextEdit 
                                    ID="Wtx_CAMPO2" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                    OnValueChange="WebTextEdit1_ValueChange" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                class="style51"><asp:Label ID="Lbl_CAMPO3" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                class="style52"><igtxt:WebTextEdit ID="Wtx_CAMPO3" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" OnValueChange="WebTextEdit1_ValueChange" 
                                    Width="150px"></igtxt:WebTextEdit></td></tr><tr><td class="style51"><asp:Label 
                                ID="Lbl_CAMPO4" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                Width="120px"></asp:Label></td><td class="style52"><igtxt:WebTextEdit 
                                    ID="Wtx_CAMPO4" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                                    OnValueChange="WebTextEdit1_ValueChange" Width="150px"></igtxt:WebTextEdit></td></tr><tr><td 
                                class="style43"><asp:Label ID="Lbl_CAMPO5" runat="server" 
                                Font-Names="Arial" Font-Size="X-Small" Width="120px"></asp:Label></td><td 
                                class="style44"><igtxt:WebTextEdit ID="Wtx_CAMPO5" runat="server" 
                                    Font-Names="Arial" Font-Size="X-Small" OnValueChange="WebTextEdit1_ValueChange" 
                                    Width="150px"></igtxt:WebTextEdit></td></tr><tr><td class="style43"></td><td 
                                class="style44">&nbsp;</td></tr><tr><td class="style43"></td><td class="style44">&nbsp;</td></tr><tr><td 
                            class="style43"></td><td class="style44">&nbsp;</td></tr><tr><td class="style43"></td><td 
                            class="style44">&nbsp;</td></tr><tr><td class="style43"></td><td class="style44">&nbsp;</td></tr></table></asp:Panel></ContentTemplate></igtab:Tab><igtab:Tab 
                    Text="Huellas"><ContentTemplate><table class="style2"><tr><td class="style6"><asp:CheckBox 
                        ID="Chb_EC_PERSONAS_S_HUELLA" runat="server" AutoPostBack="True" 
                        oncheckedchanged="Chb_SinHuella_CheckedChanged" Text="Permitir sin huella" /></td><td 
                        align="left" class="style6"><asp:Label ID="Lbl_Empleado" runat="server" 
                            Text="No. de Empleado" Visible="False"></asp:Label><igtxt:WebTextEdit 
                            ID="Wtx_PERSONA_LINK_ID_S_HUELLA" runat="server" Visible="False" 
                            Enabled="False"><ClientSideEvents 
                                KeyPress="Wtx_PERSONA_LINK_ID_S_HUELLA_KeyPress" /></igtxt:WebTextEdit></td><td 
                        align="left" class="style6"><asp:Label ID="Lbl_Clave" runat="server" 
                            Text="Clave opcional" Visible="False"></asp:Label><igtxt:WebTextEdit 
                            ID="Wtx_PERSONA_S_HUELLA_CLAVE" runat="server" Visible="False"><ClientSideEvents 
                                KeyPress="Wtx_PERSONA_S_HUELLA_CLAVE_KeyPress" /></igtxt:WebTextEdit></td><td 
                        align="left" class="style6"><igtxt:WebImageButton ID="WIBtn_Guardar" 
                            runat="server" Height="22px" OnClick="WIBtn_Guardar_Click" 
                            Text="Agregar y Guardar" UseBrowserDefaults="False" Visible="False" 
                            Width="150px"><alignments verticalall="Bottom" verticalimage="Middle" /><roundedcorners disabledimageurl="ig_butXP5wh.gif" 
                                        focusimageurl="ig_butXP3wh.gif" hoverimageurl="ig_butXP2wh.gif" 
                                        imageurl="ig_butXP1wh.gif" maxheight="80" maxwidth="400" 
                                        pressedimageurl="ig_butXP4wh.gif" renderingtype="FileImages" /><appearance><style 
                                    cursor="Default">


                                        </style><image height="16px" url="./Imagenes/Save_as.png" width="16px" /></appearance></igtxt:WebImageButton></td></tr><tr><td 
                            class="style5"><asp:Label ID="Lbl_Img_HuellaA1" runat="server" 
                            Text="Huella 1 Primera toma"></asp:Label><td class="style1"><asp:Label 
                                ID="Lbl_Img_HuellaA2" runat="server" Text="Huella 1 Segunda toma"></asp:Label>&nbsp;<td 
                                class="style1"><asp:Label ID="Lbl_Img_HuellaB1" runat="server" 
                                    Text="Huella 2 Primera toma"></asp:Label>&nbsp;<td class="style1"><asp:Label 
                                        ID="Lbl_Img_HuellaB2" runat="server" Text="Huella 2 Segunda toma"></asp:Label>&nbsp;</td></td></td></td></tr><tr><td 
                            class="style6"><asp:Image ID="Img_HuellaA1" runat="server" Height="149px" 
                            Width="128px" /></td><td><asp:Image ID="Img_HuellaA2" runat="server" 
                                Height="149px" Width="128px" /></td><td><asp:Image ID="Img_HuellaB1" 
                                runat="server" Height="149px" Width="128px" /></td><td><asp:Image 
                                ID="Img_HuellaB2" runat="server" Height="149px" Width="128px" /></td></tr><tr><td 
                            class="style6">&nbsp;</td><td colspan="2"><asp:Label ID="Lbl_Error_Huella" 
                                runat="server" ForeColor="Red"></asp:Label><asp:Label 
                                ID="Lbl_Correcto_Huella" runat="server" ForeColor="Green"></asp:Label></td><td></td></tr></table></ContentTemplate></igtab:Tab></Tabs><RoundedImage FillStyle="LeftMergedWithCenter"></RoundedImage><BorderDetails ColorLeft="White" ColorTop="White" /><borderdetails 
                colorleft="White" colortop="White" /></igtab:ultrawebtab>
        </asp:Panel>
    </div>
    </form>
</body>
</html>