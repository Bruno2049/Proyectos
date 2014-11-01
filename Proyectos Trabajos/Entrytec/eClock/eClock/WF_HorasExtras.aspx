<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_HorasExtras.aspx.cs" Inherits="WF_HorasExtras" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.DocumentExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid.DocumentExport" TagPrefix="igtbldocexp" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.DisplayControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGauge.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGauge" TagPrefix="igGauge" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGauge.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraGauge.Resources" TagPrefix="igGaugeProp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script language="javascript" type="text/javascript" src="JS_Navegador.js"></script>
    <script id="InfragisticsJS" type="text/javascript">
        function OnPageLoad() {
            SetGridSize();
        }
        function SetGridSize() {
            var grid = igtbl_getGridById("Grid");
            alto = ObtenAltoVentana() - 5;
            ancho = ObtenAnchoVentana() - 5;
            alto = document.getElementById('TD_Grid').offsetHeight - 5;
            grid.resize(ancho, alto);
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 73px;
        }
        .style4
        {
            width: 150px;
        }
        .style5
        {
            height: 87px;
        }
        .style6
        {
            width: 67px;
        }
        html, body, #wrapper, #Tabla
        {
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            border: none;
            text-align: center;
            overflow: hidden;
        }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px;"
    onload="OnPageLoad();" onresize="SetGridSize();">
    <form id="Tabla" runat="server">
    <table id="wrapper" cellspacing="0" border="0">
        <tr style="height: 1px;">
            <td>
                <ignav:UltraWebMenu ID="Menu" runat="server" EnableAppStyling="True" OnMenuItemChecked="Menu_MenuItemChecked"
                    OnMenuItemClicked="Menu_MenuItemClicked" StyleSetName="Caribbean" Width="100%"
                    ItemPaddingSubMenus="0" Height="100%">
                    <Images>
                        <SubMenuImage Url="ig_menuTri.gif" />
                    </Images>
                    <Items>
                        <ignav:Item Text="+" ToolTip="Favoritos (Diseños favoritos)" TagString="FAVORITOS">
                            <Items>
                                <ignav:Item TagString="F_AGREGAR" Text="Agregar la consulta" ToolTip="Agrega la consulta actual como favorita">
                                </ignav:Item>
                                <ignav:Item Separator="True" Text="">
                                </ignav:Item>
                                <ignav:Item TagString="F_SIMPLE" Text="Listado Simple" ToolTip="Muestra la consulta de horas extras con los menos campos posibles">
                                </ignav:Item>
                                <ignav:Item TagString="F_DETALLADO" Text="Listado Detallado" ToolTip="Muestra la consulta de horas extras con los mayores campos posibles">
                                </ignav:Item>
                                <ignav:Item Separator="True" Text="">
                                </ignav:Item>
                            </Items>
                        </ignav:Item>
                        <ignav:Item Text="Mostrar..." CheckBox="True" TagString="AsistenciaMostrar" ImageUrl="./Imagenes/Iconos/MostrarAsistencias16.png">
                            <Items>
                                <ignav:Item CheckBox="True" TagString="ASHE_AGRUPACION" Text="Mostrar Agrupacion">
                                </ignav:Item>
                                <ignav:Item TagString="ASHE_EMPLEADO" Text="Mostrar Empleado">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" Checked="True" TagString="ASHE_ENTRADA_SALIDA" Text="Mostrar Entrada y Salida">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="ASHE_COMIDA" Text="Mostrar Comida">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" Checked="True" TagString="ASHE_TOTALES" Text="Mostrar Totales">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" Checked="True" TagString="ASHE_INCIDENCIA" Text="Mostrar Incidencia">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" Checked="True" TagString="ASHE_TURNO" Text="Mostrar Turno">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="ASHE_CEROS" Text="Mostrar Horas en Ceros">
                                </ignav:Item>
                            </Items>
                            <Images>
                                <DefaultImage Url="./Imagenes/Iconos/MostrarAsistencias16.png" />
                            </Images>
                        </ignav:Item>
                        <ignav:Item TagString="AceptarHoras" Text="Aceptar Horas" ToolTip="Usa las horas calculadas por el sistema y las aplica">
                        </ignav:Item>
                        <ignav:Item TagString="UsarMisHoras" Text="Usar Mis Horas" ToolTip="Aplica las horas capturadas por el usuario">
                        </ignav:Item>
                        <ignav:Item TagString="CrearHoras" Text="Crear Horas" ToolTip="Crea horas extra para empleados"
                            Hidden="True">
                        </ignav:Item>
                        <ignav:Item TagString="QuitarHoras" Text="Quitar Horas" ToolTip="Quita o asigna 0 Horas extras a los elementos seleccionados">
                        </ignav:Item>
                        <ignav:Item TagString="UsarComo" Text="Usar Como" ToolTip="Indica sobre que tipo de incidencia se usarán dichas horas">
                        </ignav:Item>
                        <ignav:Item Text="Exportar" TagString="EXPORTAR" ImageUrl="./Imagenes/Iconos/Exportar16.png">
                            <Items>
                                <ignav:Item TagString="EXCEL" Text="Excel">
                                </ignav:Item>
                                <ignav:Item TagString="TEXTO" Text="Texto">
                                    <Items>
                                        <ignav:Item TagString="CSV" Text="CSV">
                                        </ignav:Item>
                                        <ignav:Item TagString="TXT" Text="TXT">
                                        </ignav:Item>
                                    </Items>
                                </ignav:Item>
                                <ignav:Item Text="-" Separator="True">
                                </ignav:Item>
                                <ignav:Item TagString="PDF" Text="PDF">
                                    <Items>
                                        <ignav:Item TagString="PDF_VERTICAL" Text="Vertical">
                                        </ignav:Item>
                                        <ignav:Item TagString="PDF_HORIZONTAL" Text="Horizontal">
                                        </ignav:Item>
                                    </Items>
                                </ignav:Item>
                                <ignav:Item TagString="XPS" Text="XPS">
                                    <Items>
                                        <ignav:Item TagString="XPS_VERTICAL" Text="Vertical">
                                        </ignav:Item>
                                        <ignav:Item TagString="XPS_HORIZONTAL" Text="Horizontal">
                                        </ignav:Item>
                                    </Items>
                                </ignav:Item>
                            </Items>
                            <Images>
                                <DefaultImage Url="./Imagenes/Iconos/Exportar16.png" />
                            </Images>
                        </ignav:Item>
                    </Items>
                    <DisabledStyle Font-Names="MS Sans Serif" Font-Size="8pt" ForeColor="Gray">
                    </DisabledStyle>
                    <Levels>
                        <ignav:Level Index="0" />
                        <ignav:Level Index="1" />
                        <ignav:Level Index="2" />
                    </Levels>
                    <SeparatorStyle BackgroundImage="ig_menuSep.gif" CssClass="SeparatorClass" CustomRules="background-repeat:repeat-x; " />
                    <ExpandEffects ShadowColor="LightGray" />
                </ignav:UltraWebMenu>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <table cellspacing="2" style="width: 100%;">
                    <tr>
                        <td style="text-align: right;" class="style1" rowspan="5">
                            <asp:Image ID="ImgEmpleado" runat="server" BorderStyle="Inset" Height="96px" Width="78px" />
                        </td>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblNoEmpleadoSL" runat="server" Text="No. Empleado:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 14300px;">
                            <asp:Label ID="LblNoEmp" runat="server" Text="Label" Width="202px"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 143px;">
                            &nbsp;
                        </td>
                        <td style="text-align: left; width: 143px;" rowspan="5">
                            <igmisc:WebGroupBox ID="WebGroupBox1" runat="server" EnableAppStyling="True" StyleSetName="Caribbean"
                                Text="Tiempos" Width="322px">
                                <Template>
                                    <table cellspacing="2" style="height: 100px; width: 300px;">
                                        <tr>
                                            <td style="text-align: right;" class="style4">
                                                <strong>X Trabajar:</strong>
                                            </td>
                                            <td style="text-align: left;" class="style6">
                                                <asp:Label ID="LblHXTrabajar" runat="server" Height="21px" Text="Label" Width="120px"></asp:Label>
                                            </td>
                                            <td style="text-align: right;" class="style4">
                                                <strong>Trabajado:</strong>
                                            </td>
                                            <td style="text-align: left;" class="style6">
                                                <asp:Label ID="LblHTrabajadas" runat="server" Font-Bold="False" Height="21px" Text="Label"
                                                    Width="120px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right" class="style4">
                                                <strong>Extra:</strong>
                                            </td>
                                            <td style="text-align: left" class="style6">
                                                <asp:Label ID="LblExtra" runat="server" Font-Bold="False" Height="21px" Text="Label"
                                                    Width="120px"></asp:Label>
                                            </td>
                                            <td style="text-align: right;" class="style4">
                                                <strong>Extra Simple:</strong>
                                            </td>
                                            <td style="text-align: left;" class="style6">
                                                <asp:Label ID="LblExtraSimple" runat="server" Font-Bold="False" Height="21px" Text="Label"
                                                    Width="120px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="style4">
                                                <strong>Extra Calculado:</strong>
                                            </td>
                                            <td style="text-align: left;" class="style6">
                                                <asp:Label ID="LblExtraCalculado" runat="server" Height="21px" Text="Label" Width="120px"></asp:Label>
                                            </td>
                                            <td style="text-align: right;" class="style4">
                                                <strong>Extra Doble:</strong>
                                            </td>
                                            <td style="text-align: left;" class="style6">
                                                <asp:Label ID="LblExtraDoble" runat="server" Font-Bold="False" Height="21px" Text="Label"
                                                    Width="120px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="style4">
                                                <strong>Extra Aplicado:</strong>
                                            </td>
                                            <td style="text-align: left;" class="style6">
                                                <asp:Label ID="LblExtraAplicado" runat="server" Height="21px" Text="Label" Width="120px"></asp:Label>
                                            </td>
                                            <td style="text-align: right;" class="style4">
                                                <strong>Extra Triple:</strong>
                                            </td>
                                            <td style="text-align: left;" class="style6">
                                                <asp:Label ID="LblExtraTriple" runat="server" Font-Bold="False" Height="21px" Text="Label"
                                                    Width="120px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </Template>
                            </igmisc:WebGroupBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblNombreSL" runat="server" Text="Nombre:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;">
                            <asp:Label ID="LblNombre" runat="server" Text="Label" Width="202px"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 143px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblAgrupacionSL" runat="server" Text="Agrupación:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;">
                            <asp:Label ID="LblAgrupacion" runat="server" Text="Label" Width="200px"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 143px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblTurnoSL" runat="server" Text="Turno:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;">
                            <asp:Label ID="LblTurno" runat="server" Text="Label" Width="202px"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 143px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">
                            <table style="width: 294px" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 82px; height: 24px; text-align: right;">
                                        <strong style="text-align: center">Periodo:</strong>
                                    </td>
                                    <td style="height: 24px" width="24px">
                                        <igsch:WebDateChooser ID="FechaInicial" runat="server" Value="" Width="81px" EnableAppStyling="True"
                                            OnValueChanged="FechaInicial_ValueChanged" StyleSetName="Caribbean" Height="22px">
                                            <AutoPostBack ValueChanged="True" />
                                        </igsch:WebDateChooser>
                                    </td>
                                    <td style="height: 24px">
                                        <igsch:WebDateChooser ID="FechaFinal" runat="server" Width="81px" EnableAppStyling="True"
                                            OnValueChanged="FechaFinal_ValueChanged" StyleSetName="Caribbean" Height="22px">
                                            <AutoPostBack ValueChanged="True" />
                                        </igsch:WebDateChooser>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="TD_Grid" style="vertical-align: top; text-align: left">
                <div style="height: 330px" id="Div_Grid">
                    <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" OnInitializeDataSource="Grid_InitializeDataSource"
                        OnInitializeLayout="Grid_InitializeLayout" Style="left: 0px; top: 0px" Width="100%"
                        OnInitializeRow="Grid_InitializeRow" Height="100px" 
                        onupdaterowbatch="Grid_UpdateRowBatch">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowRowNumberingDefault="Continuous"
                            BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect" HeaderClickActionDefault="SortMulti"
                            LoadOnDemand="Xml" Name="Grid" RowHeightDefault="20px" RowSelectorsDefault="No"
                            RowsRange="30" ScrollBar="Always" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                            XmlLoadOnDemandType="Accumulative" AllowSortingDefault="Yes">
                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Width="100%"
                                Height="100px">
                            </FrameStyle>
                            <Pager MinimumPagesForDisplay="2">
                            </Pager>
                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                            </RowStyleDefault>
                            <ActivationObject BorderColor="" BorderWidth="">
                            </ActivationObject>
                        </DisplayLayout>
                    </igtbl:UltraWebGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LError" runat="server" Font-Size="Smaller" Font-Names="Arial Narrow"
                    ForeColor="Red"></asp:Label><asp:Label ID="LCorrecto" runat="server" Font-Size="Smaller"
                        Font-Names="Arial Narrow" ForeColor="Green"></asp:Label>
            </td>
        </tr>
    </table>
    <igtbldocexp:UltraWebGridDocumentExporter ID="GridExporter" runat="server" OnBeginExport="GridExporter_BeginExport"
        OnFooterRowExported="GridExporter_FooterRowExported" OnInitializeRow="GridExporter_InitializeRow"
        OnRowExported="GridExporter_RowExported" OnRowExporting="GridExporter_RowExporting">
    </igtbldocexp:UltraWebGridDocumentExporter>
    <igtblexp:UltraWebGridExcelExporter ID="GridExcel" runat="server">
    </igtblexp:UltraWebGridExcelExporter>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ig:WebDialogWindow ID="DlgAgregarFav" runat="server" Height="114px" InitialLocation="Centered"
        Width="342px" Modal="True" WindowState="Hidden">
        <Header CaptionText="Atenci&#243;n">
            <CloseBox Visible="False"></CloseBox>
        </Header>
        <ContentPane>
            <Template>
                <span style="font-size: 9pt">
                    <asp:Label ID="LblMensage0" runat="server" Text="¿Desea agregar la consulta actual a favoritos?"></asp:Label>
                    <br />
                    Nombre
                    <asp:TextBox ID="Tbx_NombreFav" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Se requiere nombre del favorito"
                        ControlToValidate="Tbx_NombreFav"></asp:RequiredFieldValidator>
                    <br />
                    <igtxt:WebImageButton ID="BtnCancelar0" runat="server" Text="Cancelar" ClickOnSpaceKey="False"
                        ClickOnEnterKey="False" CausesValidation="False" OnClick="BtnCancelar0_Click">
                        <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                            HeightOfBottomEdge="2" HoverImageUrl="ig_butCRM2.gif" ImageUrl="ig_butCRM1.gif"
                            MaxHeight="40" MaxWidth="400" PressedImageUrl="ig_butCRM2.gif" WidthOfRightEdge="2" />
                        <ClientSideEvents MouseDown="BtnCancelar_Click" Click="BtnCancelar_Click" />
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;<igtxt:WebImageButton ID="BtnAceptar0" runat="server" Text="Aceptar"
                        OnClick="BtnAceptar0_Click">
                        <ClientSideEvents Click="BtnAceptar_Click" />
                    </igtxt:WebImageButton>
                </span>
            </Template>
        </ContentPane>
    </ig:WebDialogWindow>
    </form>
</body>
</html>
