<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_Monedero.aspx.cs"    Inherits="WF_Monedero" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
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
<%@ Register Assembly="Infragistics2.WebUI.WebResizingExtender.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI" TagPrefix="igui" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig1" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script language="javascript" type="text/javascript" src="JS_Navegador.js"></script>
    <script id="InfragisticsJS" type="text/javascript">
        var g_menuId;
        var g_itemId;
        var g_itemTag;
        var g_itemTagPadre;
        function Muestra(Dialogo, Mostrar) {
            var Control = "";
            if (Dialogo == "DlgAgregarFav")
                Control = '<%=DlgAgregarFav.ClientID%>';
            if (Dialogo == "DlgCompartirFav")
                Control = '<%=DlgCompartirFav.ClientID%>';

            if (Dialogo == "DlgConfirma")
                Control = '<%=DlgConfirma.ClientID%>';
            if (Mostrar == true)
                $find(Control).set_windowState($IG.DialogWindowState.Normal);
            else
                $find(Control).set_windowState($IG.DialogWindowState.Hidden);
        }
        function Menu_ItemClick(menuId, itemId) {
            g_menuId = menuId;
            g_itemId = itemId;
            var item = igmenu_getItemById(itemId);
            if (item.isTopLevelItem()) {
                // debugger;
                return;
            }
            var Tag = item.getTag();
            var TagPadre = null;
            if (item.getParent() != null) {
                TagPadre = item.getParent().getTag();
            }
            g_itemTag = Tag;
            g_itemTagPadre = TagPadre;
            var Confirmar = false;
            //        debugger;
            if (Tag == "SALDO_MONEDERO") {
                igmenu_getMenuById(menuId).CancelPostBack = true;
                return false;
            }
            if (Tag == "CONSUMO_EMPLEADO") {
                igmenu_getMenuById(menuId).CancelPostBack = true;
                return false;
            }
            if (Tag == "PERSONALIZADO") {
                igmenu_getMenuById(menuId).CancelPostBack = true;
                return false;
            }
            if (Tag == "MOV_MONEDERO") {
                igmenu_getMenuById(menuId).CancelPostBack = true;
                return false;
            }
            if (Tag == "MOV_MONEDERO_PROD") {
                igmenu_getMenuById(menuId).CancelPostBack = true;
                return false;
            }
            if (Tag == "FAVORITO_COMPARTIR") {
                igmenu_getMenuById(menuId).CancelPostBack = true;
                Muestra("DlgCompartirFav", true);
                return false;
            }
            if (Tag == "F_AGREGAR") {
                igmenu_getMenuById(menuId).CancelPostBack = true;
                Muestra("DlgAgregarFav", true);
                return false;
            } 
        }
        function Muestra(Dialogo, Mostrar) {
            var Control = "";
            if (Mostrar == true)
                $find(Control).set_windowState($IG.DialogWindowState.Normal);
            else
                $find(Control).set_windowState($IG.DialogWindowState.Hidden);
        }
        function Oculta(Dialogo) {
            Muestra(Dialogo, false);
        }
        function OcultayEjecuta(Dialogo, Evento) {
            Oculta(Dialogo);
            __doPostBack(Evento, '');
        }
        function BtnCompartirFavAceptar_Click() {
            debugger;
            Oculta('DlgCompartirFav');
            theForm.elements['ValorMenuTag'].value = g_itemTag;
            theForm.elements['ValorMenuTagPadre'].value = g_itemTagPadre;
            __doPostBack('BtnCompartirFavAceptar', '');
        }
        function BtnAceptar_Click() {
            Oculta('DlgConfirma');
            theForm.elements['ValorMenuTag'].value = g_itemTag;
            theForm.elements['ValorMenuTagPadre'].value = g_itemTagPadre;
            __doPostBack('MenuMenu', '');
        }

        var MostrarNoDias = false;
        function Btn_NoDias_Click(oButton, oEvent) {
            MostrarNoDias = !MostrarNoDias;
            var Dias;
            var Fecha;
            if (MostrarNoDias) {
                Dias = 'block';
                Fecha = 'none';
            }
            else {
                Dias = 'none';
                Fecha = 'block';
                debugger;
                var editor = $find('<%=Tbx_NoDias.ClientID%>');
                /*  var button = ig_getWebControlById("Tbx_NoDias");
                button.setText(button.getText()+' ');
                //__doPostBack('Btn_NoDias', '');*/
                __doPostBack('Tbx_NoDias', editor.get_value());
                return;
            }
            /* var button = ig_getWebControlById("Btn_NoDias");

            alert(button.getText());
            button.setText('123');*/
            document.getElementById('TD_NoDias').style.display = Dias;
            document.getElementById('TD_Tbx_NoDias').style.display = Dias;
            document.getElementById('TD_FechaFinal').style.display = Fecha;
        }
        function OnPageLoad() {
            SetGridSize();
        }
        function SetGridSize() {
            // debugger;
            //g_grdMediaAttachments_ClientID defined in the codeBehind
            var grid = igtbl_getGridById("Grid");
            if (grid == null)
                return;
            //        grid.resize("100%", "100px");
            alto = ObtenAltoVentana() - 5;
            ancho = ObtenAnchoVentana() - 5;
            alto = document.getElementById('TD_Grid').offsetHeight - 5;
            // document.getElementById('wrapper').width=ancho+"px";
            //ancho = document.getElementById('TD_Grid').offsetWidth-5;
            // return;
                    grid.resize(ancho, alto);
            // grid.MainGrid.style.height=alto+"px";
            // _igtbl_PagerRedraw(grid);
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 73px;
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
    <script type="text/javascript" id="igClientScript">
<!--

        function Grid_InitializeLayoutHandler(gridName) {

        }
// -->
    </script>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px;"
    onload="OnPageLoad();" onresize="SetGridSize();">
    <form id="Tabla" runat="server">
    <input name="ValorMenuTagPadre" id="ValorMenuTagPadre" type="hidden" value="" />
    <input name="ValorMenuTag" id="ValorMenuTag" type="hidden" value="" />
    <table id="wrapper" cellspacing="0" border="0">
        <tr style="height: 1px;">
            <td colspan="4" rowspan="1" align="left">
                <ignav:UltraWebMenu ID="Menu" runat="server" EnableAppStyling="True" OnMenuItemChecked="Menu_MenuItemChecked"
                    OnMenuItemClicked="Menu_MenuItemClicked" StyleSetName="Caribbean" Width="100%"
                    ItemPaddingSubMenus="0" Height="100%">
                    <Images>
                        <SubMenuImage Url="ig_menuTri.gif" />
                    </Images>
                    <MenuClientSideEvents ItemClick="Menu_ItemClick" />
                    <Items>
                        <ignav:Item TagString="SALDO_MONEDERO" Text="Saldo de Monedero" 
                            ToolTip="Reporte de Saldo de Monedero por Empleado" CheckBox="True" Checked="True">
                        </ignav:Item>
                        <ignav:Item TagString="CONSUMO_EMPLEADO" Text="Consumo por Empleado" 
                            ToolTip="Reporte de Monedero de Consumo por Empleado" CheckBox="True">
                        </ignav:Item>
                        <ignav:Item TagString="PERSONALIZADO" Text="Consumo por Campo Personalizado" 
                            ToolTip="Muestra el reporte personalizado" CheckBox="True">
                        </ignav:Item>
                        <ignav:Item TagString="MOV_MONEDERO" Text="Movimientos por Monedero" 
                            ToolTip="Muestra el reporte de cobro de Monedero" CheckBox="True">
                        </ignav:Item>   
                        <ignav:Item TagString="MOV_MONEDERO_PROD" Text="Monedero por Producto" 
                            ToolTip="Muestra el reporte de cobro por producto" CheckBox="True">
                        </ignav:Item>
                        <ignav:Item ImageUrl="./Imagenes/Iconos/Exportar16.png" TagString="EXPORTAR" Text="Exportar" >
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
                                <ignav:Item Separator="True" Text="-">
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
                                <ignav:Item Separator="True" Text="-">
                                </ignav:Item>
                                <ignav:Item CheckBox="True" TagString="FIRMA" Text="Agregar Firma">
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
        <tr style="height: 1px; width: 78px;">
            <td>
                <asp:Image ID="ImgEmpleado" runat="server" BorderStyle="Inset" Height="96px" Width="78px" />
            </td>
            <td style="height: 1px; width: 7800px;">
                <table cellspacing="2" style="width: 100%;">
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblNoEmpleadoSL" runat="server" Text="No. Empleado:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;" colspan="6">
                            <asp:Label ID="LblNoEmp" runat="server" Text="Label" Width="202px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblNombreSL" runat="server" Text="Nombre:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;" colspan="6">
                            <asp:Label ID="LblNombre" runat="server" Text="Label" Width="202px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblAgrupacionSL" runat="server" Text="Agrupación:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;" colspan="6">
                            <asp:Label ID="LblAgrupacion" runat="server" Text="Label" Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblTurnoSL" runat="server" Text="Turno:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;" colspan="6">
                            <asp:Label ID="LblTurno" runat="server" Text="Label" Width="202px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="style1">
                            <strong>
                                <asp:Label ID="LblAgrupacionSL0" runat="server" Text="Periodo:" Width="82px"></asp:Label></strong>
                        </td>
                        <td style="text-align: left; width: 143px;">
                            <igsch:WebDateChooser ID="FechaInicial" runat="server" Value="" Width="81px" EnableAppStyling="True"
                                OnValueChanged="FechaInicial_ValueChanged" StyleSetName="Caribbean" Height="22px">
                                <AutoPostBack ValueChanged="True" />
                            </igsch:WebDateChooser>
                        </td>
                        <td style="text-align: left; width: 143px" id="TD_FechaFinal">
                            <igsch:WebDateChooser ID="FechaFinal" runat="server" Width="81px" EnableAppStyling="True"
                                OnValueChanged="FechaFinal_ValueChanged" StyleSetName="Caribbean" Height="22px">
                                <AutoPostBack ValueChanged="True" />
                            </igsch:WebDateChooser>
                        </td>

                        <td style="text-align: left; width: 100px;display: none;"" id="TD_NoDias">
                        No de Días
                           <td style="text-align: left; width: 60px;display: none;"" id="TD_Tbx_NoDias">
                            <ig:WebNumericEditor ID="Tbx_NoDias" runat="server" DataMode="Int"
                                MaxDecimalPlaces="0" Width="60px" MaxValue="100" MinValue="1">
                                <Buttons SpinButtonsDisplay="OnRight">
                                </Buttons>
                            </ig:WebNumericEditor>
                        </td>

                                                <td style="text-align: left; width: 20;" >
                            <igtxt:WebImageButton ID="Btn_NoDias" runat="server" Height="20px" 
                                Width="20px" Text="&gt;" AutoSubmit="False" >
                                <ClientSideEvents Click="Btn_NoDias_Click()" />
                            </igtxt:WebImageButton>
                        </td>
                                                <td style="text-align: left; width: 100%;" >
                        </td>
                    </tr>
                </table>
            </td>
            <td style="height: 0px; width: 1px;">
                &nbsp;</td>
            <td style="height: 0px; width: 1px;">
                &nbsp;</td>
        </tr>
&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" rowspan="1" id="TD_Grid" style="vertical-align: top; text-align: left">
                <div style="height:300px" id="Div_Grid">
                    <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" OnInitializeDataSource="Grid_InitializeDataSource"
                        OnInitializeLayout="Grid_InitializeLayout" Style="left: 0px; top: 0px" Width="100%"
                        OnSortColumn="Grid_SortColumn" 
                        OnColumnMove="Grid_ColumnMove" OnGroupColumn="Grid_GroupColumn" 
                        OnUnGroupColumn="Grid_UnGroupColumn" Height="250px">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <RowEditTemplate>
                                    <br>
                                        <p align="center">
                                            <input id="igtbl_reOkBtn" onclick="igtbl_gRowEditButtonClick(event);" 
                                                style="width:50px;" type="button" value="OK">
                                                &nbsp;
                                                <input id="igtbl_reCancelBtn" onclick="igtbl_gRowEditButtonClick(event);" 
                                                    style="width:50px;" type="button" value="Cancel">
                                                </input>
                                            </input>
                                        </p>
                                    </br>
                                </RowEditTemplate>
                                <RowTemplateStyle BackColor="Window" BorderColor="Window" BorderStyle="Ridge">
                                    <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" 
                                        WidthTop="3px" />
                                </RowTemplateStyle>
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
                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" 
                                Width="100%" Height="250px">
                            </FrameStyle>
                            <ClientSideEvents InitializeLayoutHandler="Grid_InitializeLayoutHandler" />
                            <Pager MinimumPagesForDisplay="2">
                            </Pager>
                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                            </RowStyleDefault>
                            <FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
                                <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                            </FooterStyleDefault>
                            <ActivationObject BorderColor="" BorderWidth="">
                            </ActivationObject>
                        </DisplayLayout>
                    </igtbl:UltraWebGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4" rowspan="1" style="height: 1px; text-align: center">
            </td>
        </tr>
    </table>
    <igtbldocexp:UltraWebGridDocumentExporter ID="GridExporter" runat="server" OnBeginExport="GridExporter_BeginExport"
        OnRowExported="GridExporter_RowExported" 
        OnRowExporting="GridExporter_RowExporting" EnableViewState="False" 
        onendexport="GridExporter_EndExport">
    </igtbldocexp:UltraWebGridDocumentExporter>
    <igtblexp:UltraWebGridExcelExporter ID="GridExcel" runat="server">
    </igtblexp:UltraWebGridExcelExporter>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ig:WebDialogWindow ID="DlgConfirma" runat="server" Height="114px" InitialLocation="Centered"
        Width="342px" Modal="True" WindowState="Hidden">
        <Header CaptionText="Atenci&#243;n">
            <CloseBox Visible="False"></CloseBox>
        </Header>
        <ContentPane>
            <Template>
                <span style="font-size: 9pt">
                    <asp:Label ID="LblMensage" runat="server" Text="¿Esta seguro de que desea asignar el turno a los días y personas seleccionadas?"></asp:Label>
                    <br />
                    <br />
                    <igtxt:WebImageButton ID="BtnCancelar" runat="server" Text="Cancelar" ClickOnSpaceKey="False"
                        ClickOnEnterKey="False">
                        <ClientSideEvents MouseDown="BtnCancelar_Click" Click="BtnCancelar_Click" />
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;<igtxt:WebImageButton ID="BtnAceptar" runat="server" Text="Aceptar">
                        <ClientSideEvents Click="BtnAceptar_Click()" />
                    </igtxt:WebImageButton>
                </span>
            </Template>
        </ContentPane>
    </ig:WebDialogWindow>
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
                        ClickOnEnterKey="False" CausesValidation="False" OnClick="BtnCancelar0_Click"
                        AutoSubmit="False">
                        <ClientSideEvents MouseDown="Oculta('DlgAgregarFav');" />
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;<igtxt:WebImageButton ID="BtnNuevoFavAceptar" runat="server" Text="Aceptar"
                        AutoSubmit="False">
                        <ClientSideEvents Click="OcultayEjecuta('DlgAgregarFav','BtnNuevoFavAceptar');" />
                    </igtxt:WebImageButton>
                </span>
            </Template>
        </ContentPane>
    </ig:WebDialogWindow>
    <ig:WebDialogWindow ID="DlgCompartirFav" runat="server" Height="114px" InitialLocation="Centered"
        Width="404px" Modal="True" WindowState="Hidden">
        <Header CaptionText="Atenci&#243;n">
            <CloseBox Visible="False"></CloseBox>
        </Header>
        <ContentPane>
            <Template>
                <span style="font-size: 9pt">
                    <asp:Label ID="LblMensage1" runat="server" Text="¿Escriba el usuario al que desea compartir su favorito?"></asp:Label>
                    <br />
                    Nombre de usuario
                    <asp:TextBox ID="Tbx_NombreUsuario" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Se requiere nombre del usuario"
                        ControlToValidate="Tbx_NombreUsuario"></asp:RequiredFieldValidator>
                    <br />
                    <igtxt:WebImageButton ID="BtnCancelar1" runat="server" Text="Cancelar" ClickOnSpaceKey="False"
                        ClickOnEnterKey="False" CausesValidation="False" OnClick="BtnCancelar0_Click">
                        <ClientSideEvents MouseDown="Oculta('DlgCompartirFav');" />
                    </igtxt:WebImageButton>
                    &nbsp; &nbsp;<igtxt:WebImageButton ID="BtnCompartirFavAceptar" runat="server" Text="Aceptar"
                        AutoSubmit="False">
                        <ClientSideEvents Click="BtnCompartirFavAceptar_Click();" />
                    </igtxt:WebImageButton>
                </span>
            </Template>
        </ContentPane>
    </ig:WebDialogWindow>
    </form>
</body>
</html>
<script type="text/javascript">
    /*  // debugger;
    alto = document.getElementById('TD_Grid').offsetHeight;
    //    document.getElementById('Div_Grid').style.height = (alto-60) + 'px';
    //get the grid object from grid id
    var grid = igtbl_getGridById("Grid");
    // grid.MainGrid.style.height = alto + "px";
    //grid.resize("100%", alto-2);
    //grid.Height = (alto - 2) + "px";


    var gridHeight = (alto - 2) + "px";
    grid.MainGrid.style.height = gridHeight;
    tableId = "G_" + grid.Id;
    document.getElementById(tableId).style.height = 'auto';
    grid.alignStatMargins();*/
    //   document.getElementById('Grid_main').style.height = (alto - 60) + 'px';
</script>
