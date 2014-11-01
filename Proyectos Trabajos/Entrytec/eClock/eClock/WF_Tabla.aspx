<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_Tabla.aspx.cs" Inherits="WF_Tabla" %>

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
<%@ Register Assembly="Infragistics2.WebUI.WebResizingExtender.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI" TagPrefix="igui" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script language="javascript" type="text/javascript" src="JS_Navegador.js"></script>
    <script type='text/javascript' src='Scripts/jquery.js'></script>
    <script type='text/javascript' src='Scripts/jquery.simplemodal.js'></script>
    <script type='text/javascript' src='Scripts/confirm.js'></script>
    <!-- Confirm CSS files -->
    <link type='text/css' href='Scripts/confirm.css' rel='stylesheet' media='screen' />
    <style type="text/css">
        html, body, #wrapper, #Form1
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
    <script type="text/javascript" id="Script1">
<!--

        // ensure that menus are not corrupted and transparent. IE7 has a well known bug with the z-indexes and reseting the stacking index whenever there is a
        // an element with position relative or absolute in the control's dom hierarchy
        function IE7ZIndexAdjust() {


            if ($util.IsIE7) {
                var TD_Menu = $get('TD_Menu');
                var Div_Grid = $get('Div_Grid');
                Div_Grid.style.zIndex = '10';
                Div_Grid.style.position = 'relative';
                TD_Menu.style.zIndex = '1000';
                TD_Menu.style.position = 'relative';

            }

        }

        function OnPageLoad() {
            SetGridSize();
        }
        function SetGridSize() {

            //g_grdMediaAttachments_ClientID defined in the codeBehind

            var grid = document.getElementById('Grid');
            alto = ObtenAltoVentana() - 40;
            ancho = ObtenAnchoVentana() - 5;
            if (alto < 0)
                alto = 0;
            if (ancho < 0)
                ancho = 0;
            document.getElementById('wrapper').style.width = ancho + "px";
            document.getElementById('wrapper').style.height = ObtenAltoVentana() + "px";

            document.getElementById('Div_Grid').style.width = ancho + "px";
            document.getElementById('Div_Grid').style.height = alto + "px";


        }
        window.onload = OnPageLoad;
        window.onresize = SetGridSize;

        function Menu_ItemSelecting(sender, eventArgs) {
            ///<summary>
            ///
            ///</summary>
            ///<param name="sender" type="Infragistics.Web.UI.WebDataMenu"></param>
            ///<param name="eventArgs" type="Infragistics.Web.UI.DataMenuItemCancelEventArgs"></param>


        }
        var iframe;
        function initDialog(dialog) {
            var niframe = dialog.get_contentPane().get_iframe();
            if (niframe) {
                iframe = niframe;
                iframe.frameBorder = 'no';
                iframe.style.border = '0px';
                iframe.style.width = '99.5%';
                iframe.style.height = '99.5%';
                //etc.
            }
        }

        function NoFilasSeleccionadas() {
            var grid = $find("Grid");
            var selection = grid.get_behaviors().get_selection();
            var rows = selection.get_selectedRows();
            return rows.get_length();
        }
        function ObtenFilasSeleccionadas() {
            var grid = $find("Grid");
            var selection = grid.get_behaviors().get_selection();
            var rows = selection.get_selectedRows();
            var FilasSeleccionadas = "";
            var Cont = 0;
            // debugger;
            for (Cont = 0; Cont < rows.get_length(); Cont++) {
                var resultVar = rows.getItem(Cont).get_dataKey();
                var i = 0;
                for (i = 0; i < resultVar.length; i++) {
                    FilasSeleccionadas += resultVar[i] + "|";
                }
                FilasSeleccionadas += "~";
            }
            return FilasSeleccionadas;
        }
        function gup(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.href);
            if (results == null)
                return "";
            else
                return results[1];
        }
        function ObtenUrl(Valor) {
            var URL = "";
            //debugger;
            if (Valor.indexOf("URL(") == 0) {

                URL = Valor.substring(4, Valor.length - 1)
                var Filas = ObtenFilasSeleccionadas();
                if (Filas == "")
                    Filas = -1;
                URL = URL.replace("<IDS></IDS>", Filas);
            }
            return URL;
        }
        function Menu_ItemClick(sender, eventArgs) {
            ///<summary>
            ///
            ///</summary>
            ///<param name="sender" type="Infragistics.Web.UI.WebDataMenu"></param>
            ///<param name="eventArgs" type="Infragistics.Web.UI.DataMenuItemCancelEventArgs"></param>
            //debugger;


            Key = eventArgs.getItem().get_key();

            if (Key == "Borrar") {
                eventArgs.set_cancel(true);
                NoFilas = NoFilasSeleccionadas();
                if (NoFilas <= 0) {
                    Alerta("Se debe seleccionar minimo un registro");
                    return;
                }
                if (NoFilas == 1)
                    Confirmacion("Desea borrar el registro seleccionado", function () { __doPostBack('Menu', 'Borrar<~>' + ObtenFilasSeleccionadas()); });
                if (NoFilas > 1)
                    Confirmacion("Desea borrar los " + NoFilas + " registros seleccionados", function () { __doPostBack('Menu', 'Borrar<~>' + ObtenFilasSeleccionadas()); });

            }
            if (Key == "Nuevo" || Key == "Editar") {
                eventArgs.set_cancel(true);
                NoFilas = NoFilasSeleccionadas();
                if (Key == "Editar" && NoFilas <= 0) {
                    Alerta("Se debe seleccionar un registro");
                    return;
                }
                if (Key == "Editar" && NoFilas > 1) {
                    Alerta("Solo se debe seleccionar un registro");
                    return;
                }
                var Codigo = eventArgs.getItem().get_valueString();
                if (Codigo != "")
                    Muestra('Dlg_Edicion', true, ObtenUrl(Codigo), Key);
                else
                    if (Key == "Nuevo")
                        Muestra('Dlg_Edicion', true, "WF_TablaEdicion.aspx?Parametros=" + gup("Parametros")+",-9999999", Key);
                    else
                        Muestra('Dlg_Edicion', true, "WF_TablaEdicion.aspx?Parametros=" + gup("Parametros") + "," + ObtenFilasSeleccionadas(), Key);

            }
            //debugger;
            // return false;
        }
        function Muestra(Dialogo, Mostrar, Titulo) {
            Muestra(Dialogo, Mostrar, "", Titulo);
        }
        function Muestra(Dialogo, Mostrar, Pagina, Titulo) {
            var Control = "";
            if (Control == "")
                Control = Dialogo;
            var webDialogWindow = $find(Control);
            if (Mostrar == true) {

                if (Pagina != "")
                    iframe.src = Pagina;
                webDialogWindow.set_windowState($IG.DialogWindowState.Normal);
                webDialogWindow._header.setCaptionText(Titulo);
            }
            else
                webDialogWindow.set_windowState($IG.DialogWindowState.Hidden);
        }
        // -->
    </script>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px;">
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="wrapper" cellspacing="0" border="0">
        <tr style="height: 1px;">
            <td rowspan="1" id="TD_Menu">
                <ig:WebDataMenu ID="Menu" runat="server" StyleSetName="Caribbean" Width="100%" OnItemClick="Menu_ItemClick">
                    <GroupSettings Orientation="Horizontal" />
                    <ClientEvents Initialize="IE7ZIndexAdjust" ItemSelecting="Menu_ItemSelecting" ItemClick="Menu_ItemClick" />
                    <Items>
                        <ig:DataMenuItem ImageUrl="~/Imagenes/Iconos/Nuevo16.png" Key="Nuevo" Text="Nuevo"
                            ToolTip="Crea un nuevo registro">
                        </ig:DataMenuItem>
                        <ig:DataMenuItem ImageUrl="~/Imagenes/Iconos/Editar16.png" Key="Editar" Text="Editar"
                            ToolTip="Permite editar el registro seleccionado">
                        </ig:DataMenuItem>
                        <ig:DataMenuItem ImageUrl="~/Imagenes/Iconos/Borrar16.png" Key="Borrar" Text="Borrar"
                            ToolTip="Borra el registro seleccionado">
                        </ig:DataMenuItem>
                        <ig:DataMenuItem Key="Mostrar Borrados" Text="Mostrar Borrados" 
                            ToolTip="Muestra los registros que estan marcados como borrados">

                        </ig:DataMenuItem>
                        <ig:DataMenuItem ImageUrl="~/Imagenes/Iconos/Exportar16.png" Key="Exportar" Text="Exportar"
                            ToolTip="Permite extraer la información del sistema">
                            <Items>
                                <ig:DataMenuItem Text="Excel" Key="Excel">
                                </ig:DataMenuItem>
                                <ig:DataMenuItem Text="Texto" Key="Texto">
                                    <Items>
                                        <ig:DataMenuItem Text="CSV" Key="CSV">
                                        </ig:DataMenuItem>
                                        <ig:DataMenuItem Text="TXT" Key="TXT">
                                        </ig:DataMenuItem>
                                    </Items>
                                </ig:DataMenuItem>
                                <ig:DataMenuItem Text="PDF" Key="PDF">
                                    <Items>
                                        <ig:DataMenuItem Text="Vertical" Key="PDF_Vertical">
                                        </ig:DataMenuItem>
                                        <ig:DataMenuItem Text="Horizontal" Key="PDF_Horizontal">
                                        </ig:DataMenuItem>
                                    </Items>
                                </ig:DataMenuItem>
                                <ig:DataMenuItem Text="XPS" Key="XPS">
                                    <Items>
                                        <ig:DataMenuItem Text="Vertical" Key="XPS_Vertical">
                                        </ig:DataMenuItem>
                                        <ig:DataMenuItem Text="Horizontal" Key="XPS_Horizontal">
                                        </ig:DataMenuItem>
                                    </Items>
                                </ig:DataMenuItem>
                            </Items>
                        </ig:DataMenuItem>
                    </Items>

                </ig:WebDataMenu>
            </td>
        </tr>
        <tr>
            <td rowspan="1" id="TD_Grid" style="vertical-align: top; text-align: left">
                <div style="height: 300px; width: 300px;" id="Div_Grid">
                    <ig:WebDataGrid ID="Grid" runat="server" DefaultColumnWidth="74px" StyleSetName="Caribbean"
                        Width="100%" Height="100%" OnRowSelectionChanged="Grid_RowSelectionChanged" EnableDataViewState="True">
                        <Behaviors>
                            <ig:ColumnFixing>
                            </ig:ColumnFixing>
                            <ig:ColumnMoving>
                            </ig:ColumnMoving>
                            <ig:ColumnResizing>
                            </ig:ColumnResizing>
                            <ig:Filtering>
                            </ig:Filtering>
                            <ig:Selection CellClickAction="Row" RowSelectType="Multiple">
                            </ig:Selection>
                            <ig:RowSelectors>
                            </ig:RowSelectors>
                            <ig:Sorting SortingMode="Multi">
                            </ig:Sorting>
                            <ig:VirtualScrolling>
                            </ig:VirtualScrolling>
                        </Behaviors>
                    </ig:WebDataGrid>
                    <ig:WebDocumentExporter ID="GridExporter" runat="server">
                    </ig:WebDocumentExporter>
                    <ig:WebExcelExporter ID="GridExcel" runat="server">
                    </ig:WebExcelExporter>
                </div>
            </td>
        </tr>
        <tr style="height: 1px; visibility: hidden;">
            <td rowspan="1" style="height: 1px; text-align: center;">
                <igtxt:WebImageButton ID="WebImageButton1" runat="server" Height="1px" Width="1px">
                </igtxt:WebImageButton>
            </td>
        </tr>
    </table>
    <ig:WebDialogWindow ID="Dlg_Edicion" runat="server" Height="420px" InitialLocation="Centered"
        Modal="True" StyleSetName="Caribbean" Width="640px" WindowState="Hidden">
        <ContentPane ContentUrl="WF_Vacia.aspx">
        </ContentPane>
        <Header>
            <MaximizeBox Visible="True" />
            <MinimizeBox Visible="True" />
            <MaximizeBox Visible="True"></MaximizeBox>
            <MinimizeBox Visible="True"></MinimizeBox>
        </Header>
        <ClientEvents Initialize="initDialog" />
        <Resizer Enabled="True" />
    </ig:WebDialogWindow>
    <div id='Confirmacion'>
        <div class='Confirmacion_header'>
            <span>Atención</span></div>
        <div class='Confirmacion_message'>
        </div>
        <div class='Confirmacion_buttons'>
            <div class='no simplemodal-close'>
                No</div>
            <div class='yes'>
                Si</div>
        </div>
    </div>
    <div id='Alerta'>
        <div class='Alerta_header'>
            <span>Atención</span></div>
        <div class='Alerta_message'>
        </div>
        <div class='Alerta_buttons'>
            <div class='no simplemodal-close'>
                Cerrar</div>
        </div>
    </div>
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
