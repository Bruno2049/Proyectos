<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eClock.aspx.cs" Inherits="eClock.eClock" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebListbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebListbar" TagPrefix="iglbar" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebResizingExtender.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI" TagPrefix="igui" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eClock</title>
    <script language="javascript" type="text/javascript" src="Scripts/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="JS_Navegador.js"></script>
    <link rel="shortcut icon" href="favicon.ico" />
    <script id="InfragisticseClock" type="text/javascript">
<!--
        function habilitarPagina() {
            document.getElementById('capa_cargando').style.visibility = 'hidden';
        }
        var sessionTimeout = "<%= Session.Timeout %>";
        function LimpiaTimeOut() {
            sessionTimeout = "<%= Session.Timeout %>";
            MuestraTimeOut();
        }
        function LimpiaTimeOut2() {
            //alert("Limpiando TimeOut2");
            LimpiaTimeOut();
        }
        //assigning minutes left to session timeout to Label
        function MuestraTimeOut() {
            document.getElementById("<%= Lbl_TimeOut.ClientID %>").innerText = "Restan " + sessionTimeout + " minutos";
        }
        var SesionAlertada = -1;
        function DisplaySessionTimeout() {

            MuestraTimeOut();

            sessionTimeout = sessionTimeout - 1;

            //if session is not less than 0
            if (sessionTimeout >= 0)
            //call the function again after 1 minute delay
            //        window.setTimeout("DisplaySessionTimeout()", 60000);
                window.setTimeout("DisplaySessionTimeout()", 60000);
            else {
                if (SesionAlertada == 0) {
                    //show message box
                    alert("Su sesión ha caducado.");
                    SesionAlertada = 1;
                    location = "WF_LogIn.aspx"
                }
            }
        }

        function AsignacionTurnos() {
            MuestraTurno();
            MuestraTab("<%=TabTurnos.ClientID%>", 4);

        }
        function EditaUsuario() {
            //alert("EditaUsuario");
            MuestraPagina("WF_UsuarioEd.aspx");
        }
        function VerPendientes() {
            MuestraEmpleados();
            MuestraTab("<%=TabEmpleados.ClientID%>", 0);
            //    OcultaFrame(false);
            //    self.frames[0].location = "WF_Termin.aspx?";
        }
        function MuestraPagina(Pagina) {
            LimpiaTimeOut();
            OcultaFrame(false);
            self.frames[0].location = Pagina;
            self.frames["FrameMain"].location = Pagina;
            document.getElementById("FrameMain").location = Pagina;
        }

        function OcultaFrame(Ocultar) {
            if (Ocultar == true) {
                document.getElementById("FrameMain").style.display = 'none';

            }
            else {
                document.getElementById("FrameMain").style.display = 'block';
                OcultaTab("<%=TabEmpleados.ClientID%>", true);
                OcultaTab("<%=TabAgrupacion.ClientID%>", true);
                OcultaTab("<%=TabEmpleado.ClientID%>", true);
                OcultaTab("<%=TabTurnos.ClientID%>", true);
                OcultaTab("<%=TabTurno.ClientID%>", true);
                OcultaTab("<%=TabConfiguracion.ClientID%>", true);
                OcultaTab("<%=TabConfiguracionAdv.ClientID%>", true);
            }
        }
        var UtlimoNodoEmpleadosClick;
        function OcultaTab(tab, Ocultar) {

            LimpiaTimeOut();
            //    debugger;
            if (Ocultar == true) {
                //          document.getElementById("igtab"+ tab).style.display = 'none';
                //  debugger;

                try {
                    document.getElementById("igtab" + tab + ".i").style.display = 'none';
                } catch (err) { }
                try {
                    document.getElementById("igtab" + tab).style.display = 'none';
                }
                catch (err) { }
            }
            else {
                OcultaFrame(true);
                //        document.getElementById("igtab"+ tab).style.display = 'block';
                try {
                    document.getElementById("igtab" + tab + ".i").style.display = 'block';
                } catch (err) { }
                try {
                    document.getElementById("igtab" + tab).style.display = 'block';
                } catch (err) { }

                ActualizaTab0(tab);
            }

        }
        function CambiaPropiedadTabs(Valor) {
            eval("document.getElementById('<%=TabEmpleados.ClientID%>_cp').style." + Valor);
            eval("document.getElementById('<%=TabAgrupacion.ClientID%>_cp').style." + Valor);
            eval("document.getElementById('<%=TabEmpleado.ClientID%>_cp').style." + Valor);
            eval("document.getElementById('<%=TabTurnos.ClientID%>_cp').style." + Valor);
            eval("document.getElementById('<%=TabTurno.ClientID%>_cp').style." + Valor);
            eval("document.getElementById('<%=TabConfiguracion.ClientID%>_cp').style." + Valor);
            eval("document.getElementById('<%=TabConfiguracionAdv.ClientID%>_cp').style." + Valor);
        }
        function CambiaPropiedadTree(Valor) {

            try {
                var Tree = igtree_getTreeById('<%= TreeEmpleados.ClientID %>');
                eval("Tree.Element.style." + Valor);
            } catch (err) { }
            try {
                var Tree = igtree_getTreeById('<%= TreeTurnos.ClientID %>');
                eval("Tree.Element.style." + Valor);
            } catch (err) { }
            try {
                var Tree = igtree_getTreeById('<%= TreeUtilerias.ClientID %>');
                eval("Tree.Element.style." + Valor);
            } catch (err) { }
            try {
                var Tree = igtree_getTreeById('<%= TreeConfiguracion.ClientID %>');
                eval("Tree.Element.style." + Valor);
            } catch (err) { }

        }

        function MuestraEmpleados() {
            try {

                OcultaTab("<%=TabEmpleados.ClientID%>", false);
                OcultaTab("<%=TabAgrupacion.ClientID%>", true);
                OcultaTab("<%=TabEmpleado.ClientID%>", true);
                OcultaTab("<%=TabTurnos.ClientID%>", true);
                OcultaTab("<%=TabTurno.ClientID%>", true);
                OcultaTab("<%=TabConfiguracion.ClientID%>", true);
                OcultaTab("<%=TabConfiguracionAdv.ClientID%>", true);
                // loadSite();
            }
            catch (err) {
                alert(err.description);
            }
        }
        function MuestraEmpleado() {
            try {
                OcultaTab("<%=TabEmpleados.ClientID%>", true);
                OcultaTab("<%=TabAgrupacion.ClientID%>", true);
                OcultaTab("<%=TabEmpleado.ClientID%>", false);
                OcultaTab("<%=TabTurnos.ClientID%>", true);
                OcultaTab("<%=TabTurno.ClientID%>", true);
                OcultaTab("<%=TabConfiguracion.ClientID%>", true);
                OcultaTab("<%=TabConfiguracionAdv.ClientID%>", true);
            }
            catch (err) {
                alert(err.description);
            }
        }
        function MuestraAgrupacion() {
            try {
                OcultaTab("<%=TabEmpleados.ClientID%>", true);
                OcultaTab("<%=TabAgrupacion.ClientID%>", false);
                OcultaTab("<%=TabEmpleado.ClientID%>", true);
                OcultaTab("<%=TabTurnos.ClientID%>", true);
                OcultaTab("<%=TabTurno.ClientID%>", true);
                OcultaTab("<%=TabConfiguracion.ClientID%>", true);
                OcultaTab("<%=TabConfiguracionAdv.ClientID%>", true);
            }
            catch (err) {
                alert(err.description);
            }
        }
        function MuestraTurnos() {
            try {
                OcultaTab("<%=TabEmpleados.ClientID%>", true);
                OcultaTab("<%=TabAgrupacion.ClientID%>", true);
                OcultaTab("<%=TabEmpleado.ClientID%>", true);
                OcultaTab("<%=TabTurnos.ClientID%>", false);
                OcultaTab("<%=TabTurno.ClientID%>", true);
                OcultaTab("<%=TabConfiguracion.ClientID%>", true);
                OcultaTab("<%=TabConfiguracionAdv.ClientID%>", true);
            }
            catch (err) {
                alert(err.description);
            }
        }
        function MuestraTurno() {
            try {
                OcultaTab("<%=TabEmpleados.ClientID%>", true);
                OcultaTab("<%=TabAgrupacion.ClientID%>", true);
                OcultaTab("<%=TabEmpleado.ClientID%>", true);
                OcultaTab("<%=TabTurnos.ClientID%>", true);
                OcultaTab("<%=TabTurno.ClientID%>", false);
                OcultaTab("<%=TabConfiguracion.ClientID%>", true);
                OcultaTab("<%=TabConfiguracionAdv.ClientID%>", true);
            }
            catch (err) {
                alert(err.description);
            }
        }
        function MuestraConfiguracion() {
            try {
                OcultaTab("<%=TabEmpleados.ClientID%>", true);
                OcultaTab("<%=TabAgrupacion.ClientID%>", true);
                OcultaTab("<%=TabEmpleado.ClientID%>", true);
                OcultaTab("<%=TabTurnos.ClientID%>", true);
                OcultaTab("<%=TabTurno.ClientID%>", true);
                OcultaTab("<%=TabConfiguracion.ClientID%>", false);
                OcultaTab("<%=TabConfiguracionAdv.ClientID%>", true);
            }
            catch (err) {
                alert(err.description);
            }
        }
        function MuestraConfiguracionAdv() {
            try {
                OcultaTab("<%=TabEmpleados.ClientID%>", true);
                OcultaTab("<%=TabAgrupacion.ClientID%>", true);
                OcultaTab("<%=TabEmpleado.ClientID%>", true);
                OcultaTab("<%=TabTurnos.ClientID%>", true);
                OcultaTab("<%=TabTurno.ClientID%>", true);
                OcultaTab("<%=TabConfiguracion.ClientID%>", true);
                OcultaTab("<%=TabConfiguracionAdv.ClientID%>", false);

            }
            catch (err) {
                alert(err.description);
            }
        }

        function ObtenRandom() {
            return "&Rnd=" + Math.floor(Math.random() * 101);
        }
        function TreeEmpleados_AfterNodeSelectionChange(treeId, nodeId) {
            try {

                var nod = igtree_getNodeById(nodeId);
                var Tag = nod.getTag();
                //        alert(Tag);
                if (Tag.charAt(0) == 'M') {
                    MuestraEmpleados();
                    self.frames[0].location = "eClockAct.aspx?Parametros=PERSONAS|" + ObtenRandom();
                    //           MuestraAgrupacion();

                }
                else {
                    self.frames[0].location = "eClockAct.aspx?Parametros=PERSONAS" + Tag + "|" + ObtenRandom();
                    /*    document.frames.Ifrm.location = "eClockAct.aspx?Parametros=PERSONAS" + Tag;*/
                    if (Tag.charAt(0) == '|' || Tag.charAt(0) == 'S') {
                        MuestraAgrupacion();
                    }
                    else {
                        MuestraEmpleado();
                    }
                }
            }
            catch (err) {
                alert(err.description);
            }

        }
        function MuestraTab(UltraTab, SubTab) {
            LimpiaTimeOut();
            //    debugger;
            var ultraTab = igtab_getTabById(UltraTab);
            ultraTab.setSelectedIndex(SubTab);
            var tabItem = ultraTab.Tabs[ultraTab.getSelectedIndex()];
            tabItem.setTargetUrl(ActualizaNombre(tabItem.getTargetUrl()));
        }
        function ActualizaTab0(UltraTab) {
            try {
                //  debugger;
                LimpiaTimeOut();
                if (UltraTab == "<%=TabConfiguracion.ClientID%>")
                    return;

                var ultraTab = igtab_getTabById(UltraTab);
                var tabItem = ultraTab.Tabs[ultraTab.getSelectedIndex()];
                tabItem.setTargetUrl(ActualizaNombre(tabItem.getTargetUrl()));
            }
            catch (err) {
                alert(err.description);
            }
        }
        function ActualizaTab0Pagina(UltraTab, Pagina) {
            LimpiaTimeOut();
            var ultraTab = igtab_getTabById(UltraTab);
            var tabItem = ultraTab.Tabs[0];
            tabItem.setTargetUrl("eClockAct.aspx?UrlDestino=" + Pagina);
        }
        function TreeEmpleados_InitializeTree(treeId) {
            //Add code to handle your event here.

        }

        function TreeEmpleados_BeforeNodeSelectionChange(treeId, oldNodeId, newNodeId) {


        }
        function ActualizaNombre(Nombre) {
//            debugger;
            LimpiaTimeOut();
            var randomnumber = Math.floor(Math.random() * 10)
            if (Nombre.indexOf(".aspx", 12) <= 12)
                Nombre += ".aspx";
            else
                if (Nombre.indexOf("&") <= 0)
                    Nombre += "&RND=1";
                else
                    Nombre += randomnumber;
            // alert(Nombre);
            return Nombre;
        }
        function TabEmpleados_BeforeSelectedTabChange(oWebTab, oTab, oEvent) {
            var str = oTab.getTargetUrl();
            str = ActualizaNombre(str);
            oTab.setTargetUrl(str);
            //alert(str);
        }

        function TreeTurnos_AfterNodeSelectionChange(treeId, nodeId) {
//            debugger;
            var nod = igtree_getNodeById(nodeId);
            var Tag = nod.getTag();
            if (Tag.charAt(0) == 'M')
                MuestraTurnos();
            else {
                self.frames[0].location = "eClockAct.aspx?Parametros=TURNO" + Tag;
                MuestraTurno();
            }
        }

        function TreeTurnos_InitializeTree(treeId) {
            //Add code to handle your event here.

        }

        function TreeEmpleados_NodeClick(treeId, nodeId, button) {

            if (button == 2) {
                var nod = igtree_getNodeById(nodeId);
                /*            nod.setSelected(true);   
                var Tag = nod.getTag();*/
                UtlimoNodoEmpleadosClick = nod;
                var treeA = igtree_getTreeById(treeId);
                igmenu_showMenu('MenuContextual', treeA.event);
                return true;

            }
            else {
                var nod = igtree_getNodeById(nodeId);
                if (nod.getSelected())
                    TreeEmpleados_AfterNodeSelectionChange(treeId, nodeId);
            }
        }

        function TreeTurnos_NodeClick(treeId, nodeId, button) {
            TreeTurnos_AfterNodeSelectionChange(treeId, nodeId);
        }

        function Mostrar() {
            //debugger;
            eval('window.<%=TabEmpleados.ClientID%>_frame0.frameElement.setAttribute("style","VISIBILITY: visible");');
            //        window.TabEmpleados_frame0.frameElement.setAttribute("style","VISIBILITY: visible");
            //debugger;
        }
        function loadSite() {


            window.TabEmpleados_frame0.frameElement.setAttribute("style", "display:none;");
//            debugger;
            // set up onload listeners differently for IE
            // and other browsers
            if (window.TabEmpleados_frame0.readyState) { // IE

                window.TabEmpleados_frame0.onreadystatechange = function () {
                    if (window.TabEmpleados_frame0.readyState == "complete") {
                        window.TabEmpleados_frame0.onreadystatechange = null;
                        Mostrar();
                    }
                }
            }
            else {
                window.TabEmpleados_frame0.onload = Mostrar;
            }

            // load the site
            //window.TabEmpleados_frame0.src = currentURL;
            //DisplaySessionTimeout();
        }



        function TabConfiguracion_InitializeTabs(oWebTab) {
            //Add code to handle your event here.
            try {
                //debugger;
                //document.getElementById("TabEmpleados_frame0").onload = FrameCargado();
                if (SesionAlertada == -1) {
                    DisplaySessionTimeout();
                    SesionAlertada = 0;
                }
                MuestraEmpleados();
                resetTamano();
                /*  
     
                var ListBar = iglbar_getListbarById("ListBar");    
                ListBar.Groups[0].setSelected(true);	*/

            }
            catch (err) {
                alert(err.description);
            }
        }
        function TreeConfiguracion_NodeClick(treeId, nodeId, button) {
            TreeConfiguracion_AfterNodeSelectionChange(treeId, nodeId);
        }
        function TreeConfiguracion_AfterNodeSelectionChange(treeId, nodeId) {
            var nod = igtree_getNodeById(nodeId);
            var Tag = nod.getTag();
            if (Tag == 'WF_ConfigSuscripcion.aspx') {
                MuestraConfiguracionAdv();
                ActualizaTab0("<%=TabConfiguracionAdv.ClientID%>");
            }
            else {
                MuestraConfiguracion();
                var nod = igtree_getNodeById(nodeId);
                var Tag = nod.getTag();

                ActualizaTab0Pagina("<%=TabConfiguracion.ClientID%>", Tag);
            }
        }
        var NodoCortar;
        var NodoPegar;
        function AddChildNodesRecursive(childNode, parentNode) {
            var newChild = parentNode.addChild(childNode.getText(), childNode.getClass());
            var Tag = childNode.getTag();
            var Ruta = "";
            if (Tag.charAt(0) == 'M')
                return;
            if (Tag.charAt(0) == '|')
                Ruta = parentNode.getTag() + "|";
            /*debugger;/*
            var node = $('#' + newChild.toString() + ' img');
            if(Tag.charAt(0)!= 'M' && Tag.charAt(0) != '|' )
            {
            node.each(function() {
            this.src = 'Imagenes/Iconos/Empleados16b.png';             
            }); 
            }
            else
            {
            node.each(function() {
            this.src = 'Imagenes/Iconos/Agrupacion16b.png';             
            }); 
            }
            */

            newChild.setTag(Ruta + childNode.getText());

            /*        newChild.setHtml(childNode.Element.innerHTML);*/
            /*newChild.setHtml(childNode.getElement());*/
            if (childNode.hasChildren()) {
                var children = childNode.getChildNodes();
                for (c in children) {
                    var childSubNode = children[c];
                    AddChildNodesRecursive(childSubNode, newChild);
                }
            }
        }

        function Mueve(Origen, Destino) {

            var Tag = Destino.getTag();
            if (Tag.charAt(0) != 'M' && Tag.charAt(0) != '|') {
                Destino = Destino.getParent();
                Tag = Destino.getTag();
            }
            if (Tag.charAt(0) == 'M')
                Tag = "|";
            self.frames[0].location = "eClockAct.aspx?Parametros=MUEVE" + Origen.getTag() + "/" + Destino.getTag();
            AddChildNodesRecursive(NodoCortar, Destino);
            /*        NodoPegar.addChild(NodoCortar.getText(),NodoCortar.getClass());
            NodoPegar.setTag(NodoCortar.getTag())
            NodoPegar.setExpanded(true); */
            Origen.remove();
        }

        function MenuContextual_ItemClick(menuId, itemId) {
            var item = igmenu_getItemById(itemId);
            var tag = item.getTag();
            if (tag == "ELIMINAR") {
                self.frames[0].location = "eClockAct.aspx?Parametros=ELIMINAR" + UtlimoNodoEmpleadosClick.getTag();
                UtlimoNodoEmpleadosClick.remove();
            }
            if (tag == "CORTAR") {
                NodoCortar = UtlimoNodoEmpleadosClick;
            }
            if (tag == "PEGAR") {

                NodoPegar = UtlimoNodoEmpleadosClick;
                Mueve(NodoCortar, NodoPegar);

            }
            if (tag == "RENOMBRAR") {
                igedit_getById("<%=TbxAgrupacion.ClientID%>").setText(UtlimoNodoEmpleadosClick.getText());
                ig_getWebControlById("<%=BtnCambiarNombre.ClientID%>").setVisible(true);
                ig_getWebControlById("<%=BtnAgrupacionAgregar.ClientID%>").setVisible(false);
                //UtlimoNodoEmpleadosClick.setSelected(true); 
                $find("<%=DlgAgrupacion.ClientID%>").set_windowState($IG.DialogWindowState.Normal);
            }
            if (tag == "NUEVO") {
                igedit_getById("<%=TbxAgrupacion.ClientID%>").setText("");
                ig_getWebControlById("<%=BtnCambiarNombre.ClientID%>").setVisible(false);
                ig_getWebControlById("<%=BtnAgrupacionAgregar.ClientID%>").setVisible(true);
                //UtlimoNodoEmpleadosClick.setSelected(true); 
                $find("<%=DlgAgrupacion.ClientID%>").set_windowState($IG.DialogWindowState.Normal);
            }
            return false;

        }


        function BtnAgrupacionAgregar_MouseDown(oButton, oEvent) {
            try {
                var Texto = igedit_getById("<%=TbxAgrupacion.ClientID%>").getText();
                var Tag = UtlimoNodoEmpleadosClick.getTag();
                var NuevoTag = '';
                if (Tag.charAt(0) == 'M') {
                    NuevoTag = "|" + Texto;
                }
                else
                    if (Tag.charAt(0) == '|') {
                        NuevoTag = UtlimoNodoEmpleadosClick.getTag() + "|" + Texto;
                    }
                    else {
                        NuevoTag = UtlimoNodoEmpleadosClick.getParent().getTag() + "|" + Texto;


                    }
                self.frames[0].location = "eClockAct.aspx?Parametros=AGREGA" + NuevoTag;
                var Nodo = UtlimoNodoEmpleadosClick.insertChild(0, Texto, '');
                Nodo.setTag(NuevoTag);
                Nodo.setExpanded(true);
                //"~/Imagenes/Iconos/Agrupacion16.png"
                $find('<%=DlgAgrupacion.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
            }
            catch (err) {
                alert(err.description);
            }
        }

        function BtnCambiarNombre_MouseDown(oButton, oEvent) {
            try {
                var Texto = igedit_getById("<%=TbxAgrupacion.ClientID%>").getText();

                self.frames[0].location = "eClockAct.aspx?Parametros=RENOMBRA" + UtlimoNodoEmpleadosClick.getTag() + "@" + Texto;
                UtlimoNodoEmpleadosClick.setText(Texto);
                $find('"<%=DlgAgrupacion.ClientID%>"').set_windowState($IG.DialogWindowState.Hidden);
            }
            catch (err) {
                alert(err.description);
            }
        }

        function TreeEmpleados_DragStart(oTree, oNode, oDataTransfer, oEvent) {
            /* oDataTransfer.dataTransfer.setData("Tag", oNode.getTag());
            oDataTransfer.dataTransfer.effectAllowed = "move";*/
            NodoCortar = oNode;
        }

        function TreeEmpleados_Drop(oTree, oNode, oDataTransfer, oEvent) {
            var sourceNode = oDataTransfer.sourceObject;
            if (!oNode.isChildOf(sourceNode)) {
                Mueve(NodoCortar, oNode);
            }
            return false;

            //Mueve(NodoCortar,oNode);
            return true;
        }
        function FrameCargado() {

        }

        function Btn_Principal_Click(oButton, oEvent) {
            //Add code to handle your event here.
            VerPendientes();
        }

// -->
    </script>
    <script type="text/javascript">
        function setTamano() {


            //alto = document.getElementById('TD_WebSplitter').offsetHeight;
            //ancho = document.getElementById('TD_WebSplitter').offsetWidth;
            AreaLibreAlto = ObtenAltoVentana() - 140;
            AreaLibreAncho = ObtenAnchoVentana() - 40;
            //AreaLibreAncho = ObtenAnchoVentana()-20;
            //Obtiene el objeto Splitter
            var splitter = $util.findControl('<%= WebSplitter1.ClientID %>');
            //debugger;
            if (BrowserDetect.browser == "Explorer" && (BrowserDetect.version == "6" || BrowserDetect.version == "7" || BrowserDetect.version == "8")) {
                document.getElementById('content').style.height = AreaLibreAlto + 'px';
                document.getElementById('content').style.width = AreaLibreAncho + 'px';
                AreaLibreAlto += 4;
                document.getElementById('<%= WebSplitter1.ClientID %>').style.height = (AreaLibreAlto) + 'px';
                document.getElementById('<%= WebSplitter1.ClientID %>').style.width = AreaLibreAncho + 'px';
                document.getElementById('DivListBar').style.height = (AreaLibreAlto - 2) + "px";
                AltoTab = AreaLibreAlto - 27;
                CambiaPropiedadTabs("height = '" + AltoTab + "px'");
                AltoTree = AreaLibreAlto - 108;
                document.getElementById('<%= ListBar.ClientID %>').style.height = (AreaLibreAlto - 30) + "px";

                document.getElementById('<%= ListBar.ClientID %>_Items_0').style.height = AltoTree + "px";
                document.getElementById('<%= ListBar.ClientID %>_Items_1').style.height = AltoTree + "px";
                document.getElementById('<%= ListBar.ClientID %>_Items_2').style.height = AltoTree + "px";
                document.getElementById('<%= ListBar.ClientID %>_Items_3').style.height = AltoTree + "px";

                CambiaPropiedadTree("height = '" + AltoTree + "px'");
                if (splitter)
                    CambiaPropiedadTree("width = '" + (splitter.get_panes()[0].get_size() - 2) + "px'");


            }

            if (BrowserDetect.browser == "Explorer" && BrowserDetect.version == "9") {
                AreaLibreAncho -= 20;
                AreaLibreAlto += 10;
                document.getElementById('content').style.height = AreaLibreAlto + 'px';
                document.getElementById('content').style.width = AreaLibreAncho + 'px';
                document.getElementById('<%= WebSplitter1.ClientID %>').style.height = '100%';
                //document.getElementById('<%= WebSplitter1.ClientID %>').style.width = AreaLibreAncho + 'px';
                document.getElementById('<%= WebSplitter1.ClientID %>').style.width = '100%';
                document.getElementById('DivListBar').style.height = (AreaLibreAlto) + "px";
                ListBarAlto = AreaLibreAlto - 106;
                document.getElementById('<%= ListBar.ClientID %>').style.height = ListBarAlto + "px";

                AltoTab = AreaLibreAlto - 27;
                CambiaPropiedadTabs("height = '" + AltoTab + "px'");
                AltoTree = AreaLibreAlto - 108;
                CambiaPropiedadTree("height = '" + AltoTree + "px'");
                if (splitter)
                    CambiaPropiedadTree("width = '" + (splitter.get_panes()[0].get_size() - 2) + "px'");



            }

            if (BrowserDetect.browser == "Firefox" || BrowserDetect.browser == "Chrome" || BrowserDetect.browser == "Safari") {
                AreaLibreAncho -= 20;
                AreaLibreAlto += 10;
                document.getElementById('content').style.height = AreaLibreAlto + 'px';
                document.getElementById('content').style.width = AreaLibreAncho + 'px';
                document.getElementById('<%= WebSplitter1.ClientID %>').style.height = AreaLibreAlto + 'px';
                document.getElementById('<%= WebSplitter1.ClientID %>').style.width = AreaLibreAncho + 'px';
                document.getElementById('DivListBar').style.height = (AreaLibreAlto) + "px";

                AltoTab = AreaLibreAlto - 27;
                CambiaPropiedadTabs("height = '" + AltoTab + "px'");
                AltoTree = AreaLibreAlto - 108;
                if (BrowserDetect.browser == "Chrome" || BrowserDetect.browser == "Safari")
                    AltoTree = AreaLibreAlto - 125;
                CambiaPropiedadTree("height = '" + AltoTree + "px'");
                if (splitter) {
                    CambiaPropiedadTabs("width = '" + (splitter.get_panes()[1].get_size() - 10) + "px'");
                    if (BrowserDetect.browser == "Chrome" || BrowserDetect.browser == "Safari")
                        CambiaPropiedadTree("width = '" + (splitter.get_panes()[0].get_size() - 2) + "px'");
                    else
                        CambiaPropiedadTree("width = '" + (splitter.get_panes()[0].get_size() - 2) + "px'");
                }
                document.getElementById('<%= ListBar.ClientID %>').style.height = AreaLibreAlto + "px";
            }


            // Force splitter to redraw itself (necessary in some browsers)

            if (splitter)
                splitter.layout();

        }
        function resetTamano() {

            //document.getElementById('<%= WebSplitter1.ClientID %>').style.height = "auto";
            setTamano()
        }
        function Cargado() {
            setTamano();
            habilitarPagina();
        }
        window.onload = Cargado;
        window.onresize = resetTamano;
    </script>
    <style type="text/css">
        .clase_cargando
        {
            position: absolute;
            top: 25px;
            left: 0px;
            width: 100%;
            height: 20%;
            z-index: 10000;
            vertical-align: middle;
            text-align: center;
        }
        .BoxTable
        {
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            border: none;            
        }
        
        #FooterTable
        {
            width: 100%;
        }
        
        .Fuente
        {
            font-size: x-small;
            color: #FFFFFF;
            font-weight: bold;
        }
        .Splitter
        {
            height: 100%;
            width: 100%;
        }
        *
        {
            margin: 0;
            padding: 0;
        }
        #fondo
        {
            position: absolute;
            z-index: 0;
            left: 0px;
            top: 0px;
        }
        #encima
        {
            margin: 10px;
            position: absolute;
            z-index: 10;
            height: 98%;
            width: 98%;
            left: 0px;
            top: 0px;
        }
        #content
        {
            height: 300px /* here you can adjust size */;
            width: 500px;
        }
        
        .split
        {
            border: 0; /* optionnal */
        }
        .Ahem
        {
            font: 100%/1 Ahem, monospace;
        }
        .Peque
        {
            line-height: 0.0;
            padding-top: 0px;
            font: 0px/0px Arial;
            display: inline-block;
        }
    </style>
    <script type="text/javascript" id="igClientScript1">
<!--



        function WebSplitter1_SplitterBarPositionChanged(sender, eventArgs) {
            ///<summary>
            ///
            ///</summary>
            ///<param name="sender" type="Infragistics.Web.UI.WebSplitter"></param>
            ///<param name="eventArgs" type="Infragistics.Web.UI.SplitterBarPositionEventArgs"></param>
            if (BrowserDetect.browser == "Explorer" && (BrowserDetect.version == "6" || BrowserDetect.version == "7" || BrowserDetect.version == "8")) {
                document.getElementById('<%= ListBar.ClientID %>').style.width = eventArgs.get_prevPaneNewSize() + "px";
                CambiaPropiedadTree("width = '" + (eventArgs.get_prevPaneNewSize() - 2) + "px'");
            }
            if (BrowserDetect.browser == "Explorer" && BrowserDetect.version == "9") {
                document.getElementById('<%= ListBar.ClientID %>').style.width = (eventArgs.get_prevPaneNewSize() - 1) + "px";
                CambiaPropiedadTree("width = '" + (eventArgs.get_prevPaneNewSize() - 2) + "px'");
            }
            if (BrowserDetect.browser == "Firefox" || BrowserDetect.browser == "Chrome" || BrowserDetect.browser == "Safari") {
                document.getElementById('<%= ListBar.ClientID %>').style.width = eventArgs.get_prevPaneNewSize() + "px";
                AnchoTree = eventArgs.get_prevPaneNewSize() - 2;
                CambiaPropiedadTabs("width = '" + (eventArgs.get_nextPaneNewSize() - 10) + "px'");
                CambiaPropiedadTree("width = '" + AnchoTree + "px'");
            }

            //Add code to handle your event here.
        }// -->
    </script>
    <script type="text/javascript" id="igClientScript">
<!--

        function Btn_Ayuda_Click(oButton, oEvent) {
            var src = oEvent.event.srcElement;
            if (!src)
                src = oEvent.event.target;
            if (src && src.nodeName == 'A')
                return;
            window.open('http://www.eclock.com.mx/Recursos/Ayuda');
        }
// -->
    </script>
</head>
<body style="font-size: xx-small; font-family: tahoma; text-align: center; background-image: url(skins/page.bg.colorb.jpg);
    background-repeat: repeat-x; background-color: #1560bd; overflow:hidden;">
    <form id="form1" runat="server" class="BoxTable">
    <div id="capa_cargando" class="clase_cargando" onload="Cagando()">
        <div style="margin: auto; width: 200px; height: 20px; border: 1px solid #FF6666;
            background-color: #666666; color: #FFFFFF; text-align: center; vertical-align: middle">
            <b>Cargando...</b>
        </div>
    </div>
    <div id="fondo">
        <img src="skins/page.bg.color.jpg" /></div>
    <div id="encima">
        <table border="0" cellpadding="0" cellspacing="0" class="BoxTable" style="font-size: xx-small">
            <tr>
                <td style="background-image: url(skins/boxed_002.gif); width: 10px; height: 10px;
                    background-repeat: no-repeat;" bgcolor="White">
                </td>
                <td style="height: 10px; background-color: white;">
                </td>
                <td style="background-image: url(skins/boxed.TR.gif); width: 10px; height: 10px;
                    background-repeat: no-repeat;" bgcolor="White">
                </td>
            </tr>
            <tr style="height: 1px; line-height: 10px;">
                <td class="BoxL" style="width: 10px; background-repeat: repeat-y; background-color: white;
                    height: 1px;">
                </td>
                <td class="BoxM" valign="top" style="background-color: white; height: 1px;">
                    <table border="0" cellpadding="0" cellspacing="0" id="FooterTable" style="font-size: xx-small">
                        <tr>
                            <td style="background-image: url(skins/Titulo_r1_c1.jpg); background-repeat: repeat-x">
                            </td>
                            <td style="background-image: url(skins/Titulo_r1_c2.jpg); vertical-align: bottom;
                                text-align: left; background-repeat: repeat-x;" height="10">
                            </td>
                            <td style="background-image: url(skins/Titulo_r1_c2b.jpg); vertical-align: bottom;
                                text-align: left; background-repeat: repeat-x;" height="10">
                            </td>
                            <td style="background-image: url(skins/Titulo_r1_c3.jpg); background-repeat: repeat-x">
                            </td>
                        </tr>
                        <tr>
                            <td style="background-image: url(skins/Titulo_r2_c1.jpg);">
                            </td>
                            <td style="background-image: url(skins/Titulo_r2_c2a.jpg); vertical-align: top; text-align: left;
                                width: 550px;">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="text-align: left;" rowspan="2">
                                            <asp:Image ID="Image2" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                ImageUrl="Imagenes/imgencabezado.png" />
                                        </td>
                                        <td class="LoginTD" nowrap="nowrap" valign="top" style="text-align: right; height: 42px;
                                            vertical-align: bottom;" colspan="2">
                                        </td>
                                    </tr>
                                    <tr class="Fuente">
                                        <td nowrap="nowrap" valign="top" style="text-align: center; width: 200px; height: 35px;
                                            vertical-align: bottom;">
                                        </td>
                                        <td nowrap="nowrap" valign="top" style="text-align: right; height: 35px; vertical-align: bottom;">
                                            <asp:Label ID="Lbl_TimeOut" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background-image: url(skins/Titulo_r2_c2b.jpg); vertical-align: top; text-align: left;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr nowrap="nowrap" valign="top" style="text-align: right; height: 42px; vertical-align: bottom;">
                                        <td>
                                        </td>
                                        <td width="1px">
                                            <igtxt:WebImageButton ID="Btn_Ayuda" runat="server" EnableTheming="True" Text="Ayuda"
                                                ToolTip="Redirige a la ayuda en linea" AutoSubmit="False">
                                                <ClientSideEvents Click="Btn_Ayuda_Click" />
                                                <Alignments HorizontalAll="Left" VerticalImage="Top" />
                                                <Appearance>
                                                    <Image Url="./skins/ico-help.gif" />
                                                    <ButtonStyle BackColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Bold="True"
                                                        Font-Names="Tahoma" Font-Size="10px" ForeColor="#939393">
                                                        <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                    </ButtonStyle>
                                                    <InnerBorder StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                </Appearance>
                                                <HoverAppearance>
                                                    <Image Url="./skins/ico-help-over.gif" />
                                                    <ButtonStyle ForeColor="WhiteSmoke" Cursor="Hand">
                                                    </ButtonStyle>
                                                </HoverAppearance>
                                                <RoundedCorners RenderingType="Disabled" />
                                                <FocusAppearance>
                                                    <ButtonStyle Cursor="Hand">
                                                    </ButtonStyle>
                                                </FocusAppearance>
                                            </igtxt:WebImageButton>
                                        </td>
                                        <td width="1px">
                                            <igtxt:WebImageButton ID="Btn_Principal" runat="server" EnableTheming="True" OnClick="Btn_Principal_Click"
                                                Text="Inicio" ToolTip="Pagina Principal">
                                                <Alignments HorizontalAll="Left" VerticalImage="Top" />
                                                <Appearance>
                                                    <Image Url="./skins/ico-home.gif" />
                                                    <ButtonStyle BackColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Bold="True"
                                                        Font-Names="Tahoma" Font-Size="10px" ForeColor="#939393">
                                                        <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                    </ButtonStyle>
                                                    <InnerBorder StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                </Appearance>
                                                <HoverAppearance>
                                                    <Image Url="./skins/ico-home-over.gif" />
                                                    <ButtonStyle ForeColor="WhiteSmoke" Cursor="Hand">
                                                    </ButtonStyle>
                                                </HoverAppearance>
                                                <RoundedCorners RenderingType="Disabled" />
                                                <FocusAppearance>
                                                    <ButtonStyle Cursor="Hand">
                                                    </ButtonStyle>
                                                </FocusAppearance>
                                            </igtxt:WebImageButton>
                                        </td>
                                        <td width="1px">
                                            <igtxt:WebImageButton ID="Btn_Usuario" runat="server" EnableTheming="True" Text="Nombre de Usuario"
                                                ToolTip="Editar Cuenta de Usuario" AutoSubmit="False">
                                                <Alignments VerticalImage="Top" />
                                                <Appearance>
                                                    <Image Url="./skins/ico-register.gif" />
                                                    <ButtonStyle BackColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Bold="True"
                                                        Font-Names="Tahoma" Font-Size="10px" ForeColor="#939393">
                                                        <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                    </ButtonStyle>
                                                    <InnerBorder StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                </Appearance>
                                                <ClientSideEvents Click="EditaUsuario()" />
                                                <HoverAppearance>
                                                    <Image Url="./skins/ico-register-over.gif" />
                                                    <ButtonStyle ForeColor="WhiteSmoke" Cursor="Hand">
                                                    </ButtonStyle>
                                                </HoverAppearance>
                                                <RoundedCorners RenderingType="Disabled" />
                                            </igtxt:WebImageButton>
                                        </td>
                                        <td width="1px">
                                            <igtxt:WebImageButton ID="Btn_Salir" runat="server" EnableTheming="True" OnClick="Btn_Salir_Click"
                                                Text="Salir" ToolTip="Salir del Sistema">
                                                <Alignments HorizontalAll="Left" VerticalImage="Top" />
                                                <Appearance>
                                                    <Image Url="./skins/ico-login.gif" />
                                                    <ButtonStyle BackColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Bold="True"
                                                        Font-Names="Tahoma" Font-Size="10px" ForeColor="#939393">
                                                        <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                    </ButtonStyle>
                                                    <InnerBorder StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                </Appearance>
                                                <HoverAppearance>
                                                    <Image Url="./skins/ico-login-over.gif" />
                                                    <ButtonStyle ForeColor="WhiteSmoke" Cursor="Hand">
                                                    </ButtonStyle>
                                                </HoverAppearance>
                                                <RoundedCorners RenderingType="Disabled" />
                                            </igtxt:WebImageButton>
                                        </td>
                                    </tr>
                                    <tr class="Fuente">
                                        <td nowrap="nowrap" valign="top" style="text-align: right; height: 35px; vertical-align: bottom;"
                                            colspan="5">
                                            <span style="vertical-align: middle"><span style="vertical-align: top; color: #FFFFFF;">
                                                <igtxt:WebTextEdit ID="Tbx_Busqueda" runat="server" ToolTip="Texto a buscar" OnEnterKeyPress="Tbx_Busqueda_EnterKeyPress">
                                                </igtxt:WebTextEdit>
                                                &nbsp;<asp:LinkButton ID="Lbtn_Buscar" runat="server" Text="Buscar" OnClick="Lbtn_Buscar_Click"
                                                    ForeColor="White"></asp:LinkButton></span></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background-image: url(skins/Titulo_r2_c3.jpg);">
                            </td>
                        </tr>
                        <tr>
                            <td width="10px" style="background-image: url(skins/Titulo_r3_c1.jpg); background-repeat: repeat-x">
                            </td>
                            <td style="background-image: url(skins/Titulo_r3_c2.jpg); background-repeat: repeat-x">
                            </td>
                            <td style="background-image: url(skins/Titulo_r3_c2.jpg); background-repeat: repeat-x">
                            </td>
                            <td width="10px" style="background-image: url(skins/Titulo_r3_c3.jpg); background-repeat: repeat-x">
                                <img src="skins/dummy.gif" border="0" alt="" height="10px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="BoxR" style="width: 10px; background-repeat: repeat-y; background-color: white;
                    height: 1px;">
                </td>
            </tr>
            <tr>
                <td class="BoxL" style="width: 10px; background-repeat: repeat-y; background-color: white;">
                    <img height="10" src="skins/dummy.gif" width="10" />
                </td>
                <td id="TD_WebSplitter" class="BoxM" valign="top" style="vertical-align: top; text-align: left;">
                    <div id="content">
                        <ig:WebSplitter ID="WebSplitter1" runat="server" CssClass="split" BorderStyle="None">
                            <ClientEvents SplitterBarPositionChanged="WebSplitter1_SplitterBarPositionChanged">
                            </ClientEvents>
                            <Panes>
                                <ig:SplitterPane runat="server" CollapsedDirection="PreviousPane" Size="230" Collapsed="False"
                                    EnableRelativeLayout="True">
                                    <Template>
                                        <div id="DivListBar" style="width: 100%; height: 50px">
                                            <iglbar:UltraWebListbar ID="ListBar" runat="server" BarWidth="100%" BorderStyle="Solid"
                                                BorderWidth="1px" BrowserTarget="UpLevel" EnableAppStyling="True" GroupSpacing="0px"
                                                Height="100%" ItemIconStyle="SmallWithInsetText" Style="left: 0px; top: 0px"
                                                StyleSetName="Caribbean" UseClientID="True" Width="100%" KeepInView="AutoScroll"
                                                BorderColor="#3698D4">
                                                <DefaultItemHoverStyle Font-Bold="True">
                                                </DefaultItemHoverStyle>
                                                <Groups>
                                                    <iglbar:Group Key="Empleados" Text="Empleados" TextAlign="Left">
                                                        <Labels Collapsed="" Expanded="" Selected="" />
                                                        <Template>
                                                            <ignav:UltraWebTree ID="TreeEmpleados" runat="server" AllowDrag="True" AllowDrop="True"
                                                                BackColor="Transparent" DefaultImage="" EnableAppStyling="True" FullNodeSelect="True"
                                                                HoverClass="" Indentation="20" StyleSetName="Caribbean" WebTreeTarget="ClassicTree"
                                                                HiliteClass="" Height="100px" Width="10px">
                                                                <ClientSideEvents AfterNodeSelectionChange="TreeEmpleados_AfterNodeSelectionChange"
                                                                    BeforeNodeSelectionChange="TreeEmpleados_BeforeNodeSelectionChange" DragStart="TreeEmpleados_DragStart"
                                                                    Drop="TreeEmpleados_Drop" NodeClick="TreeEmpleados_NodeClick" />
                                                                <Images>
                                                                    <ParentNodeImage Url="./Imagenes/Iconos/Agrupacion16.png" />
                                                                    <LeafNodeImage Url="./Imagenes/Iconos/Empleado16.png" />
                                                                </Images>
                                                            </ignav:UltraWebTree>
                                                        </Template>
                                                    </iglbar:Group>
                                                    <iglbar:Group Key="Turnos" Text="Turnos" TextAlign="Left">
                                                        <Labels Collapsed="" Expanded="" Selected="" />
                                                        <Template>
                                                            <ignav:UltraWebTree ID="TreeTurnos" runat="server" AllowDrag="True" AllowDrop="True"
                                                                DefaultImage="" EnableAppStyling="True" FullNodeSelect="True" HiliteClass=""
                                                                HoverClass="" Indentation="20" StyleSetName="Caribbean" Height="100px" Width="10px">
                                                                <ClientSideEvents AfterNodeSelectionChange="TreeTurnos_AfterNodeSelectionChange"
                                                                    NodeClick="TreeTurnos_NodeClick" />
                                                                <Images>
                                                                    <ParentNodeImage Url="./Imagenes/Iconos/CarpetaTurnos16.png" />
                                                                    <LeafNodeImage Url="./Imagenes/Iconos/Turnos16.png" />
                                                                </Images>
                                                            </ignav:UltraWebTree>
                                                        </Template>
                                                    </iglbar:Group>
                                                    <iglbar:Group Key="Utilerias" Text="Utilerias" TextAlign="Left">
                                                        <Labels Collapsed="" Expanded="" Selected="" />
                                                        <Template>
                                                            <ignav:UltraWebTree ID="TreeUtilerias" runat="server" AllowDrag="True" AllowDrop="True"
                                                                DefaultImage="" EnableAppStyling="True" ExpandOnClick="True" FullNodeSelect="True"
                                                                HoverClass="" Indentation="20" StyleSetName="Caribbean" Height="100px" Width="10px">
                                                                <ClientSideEvents AfterNodeSelectionChange="TreeConfiguracion_AfterNodeSelectionChange" 
                                                                NodeClick="TreeConfiguracion_NodeClick" />
                                                                <Levels>
                                                                    <ignav:Level Index="0" />
                                                                </Levels>
                                                                <Nodes>
                                                                    <ignav:Node TagString="WF_Importacion.aspx" Text="Importar Empleados">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_ImportaAccesos.aspx" Text="Importar Accesos">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_ImportarInc.aspx" Text="Importar Incidencias">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_IncCargaMensual.aspx" Text="Importar Incidencias Mensual">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_ExportarIncidenciasNOI.aspx" Text="Exportar a Noi">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_ExportarIncidenciasNomipaq.aspx" Text="Exportar a Nomipaq">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_ExportarIncidenciasAdam.aspx" Text="Exportar a Adam">
                                                                    </ignav:Node>
                                                                    <%--<ignav:Node TagString="WF_ExportarTurnosAdam.aspx" Text="Exportar Turnos a Adam">
                                                                    </ignav:Node>--%>
                                                                    <ignav:Node TagString="WF_ExportarIncidenciasOracleBS.aspx" Text="Exportar a OracleBS">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_ExportarIncidenciasSicoss.aspx" Text="Exportar a Sicoss">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_ExportacionAv.aspx" Text="Exportar Avanzado">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_Log.aspx" Text="Log de auditoria">
                                                                    </ignav:Node>
                                                                </Nodes>
                                                            </ignav:UltraWebTree>
                                                        </Template>
                                                    </iglbar:Group>
                                                    <iglbar:Group Key="Configuracion" Text="Configuración" TextAlign="Left">
                                                        <Labels Collapsed="" Expanded="" Selected="" />
                                                        <Template>
                                                            <ignav:UltraWebTree ID="TreeConfiguracion" runat="server" AllowDrag="True" AllowDrop="True"
                                                                DefaultImage="" EnableAppStyling="True" ExpandOnClick="True" FullNodeSelect="True"
                                                                HoverClass="" Indentation="20" StyleSetName="Caribbean" Height="100px" Width="10px">
                                                                <ClientSideEvents AfterNodeSelectionChange="TreeConfiguracion_AfterNodeSelectionChange"
                                                                NodeClick="TreeConfiguracion_NodeClick" />
                                                                <Levels>
                                                                    <ignav:Level Index="0" />
                                                                </Levels>
                                                                <Nodes>
                                                                    <ignav:Node TagString="WF_Tipo_Incidencias.aspx" Text="Tipos de Incidencias">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_Dias_Festivos.aspx" Text="Dias Festivos">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_ReorganizarEmpleados.aspx" Text="Reorganizar Empleados">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_Sitios.aspx" Text="Sitios">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_Terminales.aspx" Text="Terminales">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_Usuarios.aspx" Text="Usuarios">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_Suscripciones.aspx" Text="Suscripciones">
                                                                    </ignav:Node>
                                                                    <ignav:Node TagString="WF_ConfigSuscripcion.aspx" Text="Otros">
                                                                    </ignav:Node>
                                                                </Nodes>
                                                            </ignav:UltraWebTree>
                                                        </Template>
                                                    </iglbar:Group>
                                                </Groups>
                                                <DefaultItemStyle Cursor="Default" Font-Names="Verdana" Font-Size="8pt" ForeColor="MediumBlue">
                                                    <Margin Left="1px" />
                                                </DefaultItemStyle>
                                                <DefaultGroupStyle BackColor="AliceBlue" Cursor="Default">
                                                </DefaultGroupStyle>
                                                <DefaultItemSelectedStyle BorderStyle="Inset" BorderWidth="1px" Cursor="Default">
                                                </DefaultItemSelectedStyle>
                                            </iglbar:UltraWebListbar>
                                        </div>
                                    </Template>
                                </ig:SplitterPane>
                                <ig:SplitterPane runat="server">
                                    <Template>
                                        <igtab:UltraWebTab ID="TabEmpleados" runat="server" BorderStyle="NotSet" AsyncMode="Off"
                                            EnableAppStyling="True" StyleSetName="Caribbean" Height="92%" Width="100%" SelectedTab="0"
                                            SpaceOnRight="0">
                                            <Tabs>
                                                <igtab:Tab DefaultImage="./Imagenes/Iconos/MostrarPendientes16.png" ImageAlign="AbsMiddle"
                                                    Text="Tareas Pendientes" Key="TabEmpleados_TareasPendientes">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_TareasPendientes">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Asistencia" DefaultImage="./Imagenes/Iconos/MostrarAsistencias16.png"
                                                    ImageAlign="AbsMiddle" Key="TabEmpleados_Asistencia">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_AsistenciasGrupo">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Horas Extras" DefaultImage="./Imagenes/Iconos/MostrarAsistencias16.png"
                                                    ImageAlign="AbsMiddle" Key="TabEmpleados_HorasExtras">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_HorasExtrasGrupo">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Empleados" DefaultImage="./Imagenes/Iconos/Agrupacion16.png" ImageAlign="AbsMiddle"
                                                    Key="TabEmpleados_Empleados">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_EmpleadosN">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Nuevo" DefaultImage="./Imagenes/Iconos/EmpleadoNuevo16.png" ImageAlign="AbsMiddle"
                                                    Key="TabEmpleados_Nuevo">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_EmpleadoNuevo">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Herramientas" DefaultImage="./Imagenes/Iconos/EmpleadoEditar16.png"
                                                    ImageAlign="AbsMiddle" Key="TabEmpleados_Herramientas">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_EmpleadosHe">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Importar" DefaultImage="./Imagenes/Iconos/Importar16.png" ImageAlign="AbsMiddle"
                                                    Key="TabEmpleados_Importar">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_Importacion">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Comida" DefaultImage="./Imagenes/Iconos/MostrarPendientes16.png" ImageAlign="AbsMiddle"
                                                    Key="TabEmpleados_Comida">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_ComidaGrupo">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Monedero" DefaultImage="./Imagenes/Iconos/MostrarPendientes16.png" ImageAlign="AbsMiddle"
                                                    Key="TabEmpleados_Monedero">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_MonederoGrupo">
                                                    </ContentPane>
                                                </igtab:Tab>
                                            </Tabs>
                                            <RoundedImage FillStyle="RightMergedWithCenter" LeftSideWidth="0" RightSideWidth="0" />
                                            <ScrollButtons AllowScrollToLastTab="True">
                                            </ScrollButtons>
                                            <AsyncOptions EnableLoadOnDemand="True" RequestContext="IncludeFormData" ResponseContext="IncludeSelectedTab" />
                                            <ClientSideEvents BeforeSelectedTabChange="TabEmpleados_BeforeSelectedTabChange" />
                                        </igtab:UltraWebTab>
                                        <igtab:UltraWebTab ID="TabAgrupacion" runat="server" BorderStyle="NotSet" AsyncMode="Off"
                                            EnableAppStyling="True" StyleSetName="Caribbean" Height="92%" Width="100%" SelectedTab="0"
                                            SpaceOnRight="0">
                                            <Tabs>
                                                <igtab:Tab Text="Asistencia" DefaultImage="./Imagenes/Iconos/MostrarAsistencias16.png"
                                                    ImageAlign="AbsMiddle" Key="TabAgrupacion_Asistencia">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_AsistenciasGrupo">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Horas Extras" DefaultImage="./Imagenes/Iconos/MostrarAsistencias16.png"
                                                    ImageAlign="AbsMiddle" Key="TabAgrupacion_HorasExtras">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_HorasExtrasGrupo">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Empleados" DefaultImage="./Imagenes/Iconos/Agrupacion16.png" ImageAlign="AbsMiddle"
                                                    Key="TabAgrupacion_Empleados">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_Tabla.aspx?Parametros=EC_PERSONAS">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Nuevo" DefaultImage="./Imagenes/Iconos/EmpleadoNuevo16.png" ImageAlign="AbsMiddle"
                                                    Key="TabAgrupacion_Nuevo">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_EmpleadoNuevo">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab DefaultImage="./Imagenes/Iconos/Agrupacion16.png" ImageAlign="AbsMiddle"
                                                    Text="Supervisores" Key="TabAgrupacion_Supervisores">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_AgrupacionPermisos">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Importar" DefaultImage="./Imagenes/Iconos/Importar16.png" ImageAlign="AbsMiddle"
                                                    Key="TabAgrupacion_Importar">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_Importacion">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Comida" DefaultImage="./Imagenes/Iconos/MostrarPendientes16.png" ImageAlign="AbsMiddle"
                                                    Key="TabEmpleados_Comida">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_ComidaGrupo">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Monedero" DefaultImage="./Imagenes/Iconos/MostrarPendientes16.png" ImageAlign="AbsMiddle"
                                                    Key="TabEmpleados_Monedero">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_MonederoGrupo">
                                                    </ContentPane>
                                                </igtab:Tab>
                                            </Tabs>
                                            <RoundedImage FillStyle="LeftMergedWithCenter" />
                                            <AsyncOptions EnableLoadOnDemand="True" RequestContext="IncludeFormData" ResponseContext="IncludeSelectedTab" />
                                            <ClientSideEvents BeforeSelectedTabChange="TabEmpleados_BeforeSelectedTabChange" />
                                        </igtab:UltraWebTab>
                                        <igtab:UltraWebTab ID="TabEmpleado" runat="server" BorderStyle="NotSet" EnableAppStyling="True"
                                            StyleSetName="Caribbean" Height="92%" Width="100%" SpaceOnRight="0"
                                            AsyncMode="On">
                                            <Tabs>
                                                <igtab:Tab Text="Asistencia" DefaultImage="./Imagenes/Iconos/MostrarAsistencias16.png"
                                                    ImageAlign="AbsMiddle" Key="TabEmpleado_Asistencia">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_AsistenciasEmp">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Horas Extras" DefaultImage="./Imagenes/Iconos/MostrarAsistencias16.png"
                                                    ImageAlign="AbsMiddle" Key="TabEmpleado_HorasExtras">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_HorasExtras">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Asistencia Anual" DefaultImage="./Imagenes/Iconos/AsistenciaAnual16.png"
                                                    ImageAlign="AbsMiddle" Key="TabEmpleado_AsistenciaAnual">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_Personas_Diario">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Edicion" DefaultImage="./Imagenes/Iconos/EmpleadoEditar16.png" ImageAlign="AbsMiddle"
                                                    Key="TabEmpleado_Edicion">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_EmpleadosEd">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Permisos de Acceso" DefaultImage="./Imagenes/Iconos/PermisosAcceso16.png"
                                                    ImageAlign="AbsMiddle" Key="TabEmpleado_PermisosAcceso">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_PersonasTerminales">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Comida" DefaultImage="./Imagenes/Iconos/MostrarPendientes16.png" ImageAlign="AbsMiddle"
                                                    Key="TabEmpleados_Comida">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_Comida">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Monedero" DefaultImage="./Imagenes/Iconos/MostrarPendientes16.png" ImageAlign="AbsMiddle"
                                                    Key="TabEmpleados_Monedero">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_Monedero">
                                                    </ContentPane>
                                                </igtab:Tab>
                                            </Tabs>
                                            <RoundedImage FillStyle="LeftMergedWithCenter" />
                                            <AsyncOptions EnableLoadOnDemand="True" RequestContext="IncludeFormData" ResponseContext="IncludeSelectedTab"
                                                Triggers="SelectedTabChanged" />
                                            <ClientSideEvents BeforeSelectedTabChange="TabEmpleados_BeforeSelectedTabChange" />
                                        </igtab:UltraWebTab>
                                        <igtab:UltraWebTab ID="TabTurnos" runat="server" BorderStyle="NotSet" AsyncMode="On"
                                            EnableAppStyling="True" StyleSetName="Caribbean" Height="92%" Width="100%" SelectedTab="0"
                                            SpaceOnRight="0">
                                            <ClientSideEvents BeforeSelectedTabChange="TabEmpleados_BeforeSelectedTabChange" />
                                            <Tabs>
                                                <igtab:Tab Text="Lista de turnos" Key="TabTurnos_Lista">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_TurnosN">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <%--<igtab:Tab Text="Creaci&#243;n Express" Key="TabTurnos_CreacionExpress">
                                                    <ContentPane TargetUrl="eClockAct.aspx?UrlDestino=WF_CreacionTurnosExpress" BorderStyle="None">
                                                    </ContentPane>
                                                </igtab:Tab>--%>
                                                <igtab:Tab Text="Nuevo Turno" Key="TabTurnos_NuevoTurno">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_TurnoNuevo">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <%--<igtab:Tab Text="Asignaci&#243;n Express">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_TurnosAsignacionExpress">
                                                    </ContentPane>
                                                </igtab:Tab>--%>
                                                <%--<igtab:Tab Text="Asignaci&#243;n Semanal">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_PersonasSemana">
                                                    </ContentPane>
                                                </igtab:Tab>--%>
                                                <%--<igtab:Tab Text="Asignaci&#243;n Avanzada" Visible="False">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_TurnoAnual">
                                                    </ContentPane>
                                                </igtab:Tab>--%>
                                                <igtab:Tab Text="Asignaci&#243;n por Importación" Visible="True">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_TurnoImportaAsignacion">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Asignaci&#243;n Por Fechas" Visible="True">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_TurnoAsignacionFechas.aspx">
                                                    </ContentPane>
                                                </igtab:Tab>
                                            </Tabs>
                                            <RoundedImage FillStyle="LeftMergedWithCenter" />
                                            <AsyncOptions EnableLoadOnDemand="True" RequestContext="IncludeFormData" ResponseContext="IncludeSelectedTab"
                                                Triggers="SelectedTabChanged" />
                                        </igtab:UltraWebTab>
                                        <igtab:UltraWebTab ID="TabTurno" runat="server" BorderStyle="NotSet" AsyncMode="On"
                                            EnableAppStyling="True" StyleSetName="Caribbean" Height="92%" Width="100%" SelectedTab="0"
                                            SpaceOnRight="0">
                                            <ClientSideEvents BeforeSelectedTabChange="TabEmpleados_BeforeSelectedTabChange" />
                                            <Tabs>
                                                <igtab:Tab Text="Empleados Asignados">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_TurnoAsignacion">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Edicion" Key="TabTurno_Edicion">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_TurnosEdicion">
                                                    </ContentPane>
                                                </igtab:Tab>
                                            </Tabs>
                                            <RoundedImage FillStyle="LeftMergedWithCenter" />
                                            <AsyncOptions EnableLoadOnDemand="True" RequestContext="IncludeFormData" ResponseContext="IncludeSelectedTab"
                                                Triggers="SelectedTabChanged" />
                                        </igtab:UltraWebTab>
                                        <igtab:UltraWebTab ID="TabConfiguracion" runat="server" BorderStyle="NotSet" AsyncMode="On"
                                            EnableAppStyling="True" StyleSetName="Caribbean" Height="92%" Width="100%" SelectedTab="0"
                                            SpaceOnRight="0">
                                            <ClientSideEvents InitializeTabs="TabConfiguracion_InitializeTabs" />
                                            <Tabs>
                                                <igtab:Tab Text="Configuracion">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?">
                                                    </ContentPane>
                                                </igtab:Tab>
                                            </Tabs>
                                            <RoundedImage FillStyle="LeftMergedWithCenter" />
                                            <AsyncOptions EnableLoadOnDemand="True" RequestContext="IncludeFormData" ResponseContext="IncludeSelectedTab"
                                                Triggers="SelectedTabChanged" />
                                        </igtab:UltraWebTab>
                                        <igtab:UltraWebTab ID="TabConfiguracionAdv" runat="server" BorderStyle="NotSet" AsyncMode="On"
                                            EnableAppStyling="True" StyleSetName="Caribbean" Height="92%" Width="100%" SelectedTab="0"
                                            SpaceOnRight="0">
                                            <ClientSideEvents InitializeTabs="TabConfiguracion_InitializeTabs" BeforeSelectedTabChange="TabEmpleados_BeforeSelectedTabChange" />
                                            <Tabs>
                                                <igtab:Tab Text="Alertas Mail">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_ConfigAlertas">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Envio automatico">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_EnvioReportes">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="SMTP">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_ConfigSMTP">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Empresa">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_DatosEmpresa">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <%--<igtab:Tab Text="Variables">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_ConfigVariables">
                                                    </ContentPane>
                                                </igtab:Tab>--%>
                                                <igtab:Tab Text="Variables">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_ConfigUsuarioE">
                                                    </ContentPane>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Avanzada">
                                                    <ContentPane BorderStyle="None" TargetUrl="eClockAct.aspx?UrlDestino=WF_Config">
                                                    </ContentPane>
                                                </igtab:Tab>
                                            </Tabs>
                                            <RoundedImage FillStyle="LeftMergedWithCenter" />
                                            <AsyncOptions EnableLoadOnDemand="True" RequestContext="IncludeFormData" ResponseContext="IncludeSelectedTab"
                                                Triggers="SelectedTabChanged" />
                                        </igtab:UltraWebTab>
                                        <iframe id="FrameMain" height="450px" width="733px" frameborder="0"></iframe>
                                    </Template>
                                </ig:SplitterPane>
                            </Panes>
                        </ig:WebSplitter>
                    </div>
                </td>
                <td class="BoxR" style="width: 10px; background-repeat: repeat-y; background-color: white;">
                    <img height="10" src="skins/dummy.gif" width="10" style="background-color: white" />
                </td>
            </tr>
            <tr style="height: 10; vertical-align: top;">
                <td class="BoxBL">
                    <img height="10" src="skins/boxed.gif" width="10" />
                </td>
                <td class="BoxB" style="background-repeat: repeat-x; background-image: url(skins/boxed.CE.gif);"
                    height="10">
                            <table class="Fuente" width="100%">
                    <tr>
                <td style="text-align: center">
                    <div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <ignav:UltraWebMenu ID="MenuContextual" runat="server" WebMenuTarget="PopupMenu"
                            EnableAppStyling="True" StyleSetName="Caribbean" TargetFrame="">
                            <IslandStyle BackColor="LightGray" BorderStyle="Outset" BorderWidth="1px" Cursor="Default">
                            </IslandStyle>
                            <HoverItemStyle BackColor="DarkBlue" Cursor="Default" ForeColor="White">
                            </HoverItemStyle>
                            <Images>
                                <SubMenuImage Url="ig_menuTri.gif" />
                            </Images>
                            <ItemStyle Cursor="Default" />
                            <Items>
                                <ignav:Item ImageUrl="./Imagenes/Iconos/Explorar.png" TagString="EXPLORAR" Text="Explorar">
                                    <Images>
                                        <DefaultImage Url="./Imagenes/Iconos/Explorar.png" />
                                    </Images>
                                </ignav:Item>
                                <ignav:Item Separator="True" Text="">
                                </ignav:Item>
                                <ignav:Item Text="Cortar" ImageUrl="./Imagenes/Iconos/Cortar16.png" TagString="CORTAR">
                                    <Images>
                                        <DefaultImage Url="./Imagenes/Iconos/Cortar16.png" />
                                    </Images>
                                </ignav:Item>
                                <ignav:Item Text="Pegar" ImageUrl="./Imagenes/Iconos/Pegar16.png" TagString="PEGAR">
                                    <Images>
                                        <DefaultImage Url="./Imagenes/Iconos/Pegar16.png" />
                                    </Images>
                                </ignav:Item>
                                <ignav:Item Separator="True" Text="">
                                </ignav:Item>
                                <ignav:Item TagString="ELIMINAR" Text="Eliminar">
                                </ignav:Item>
                                <ignav:Item ImageUrl="./Imagenes/Iconos/Renombrar16.png" TagString="RENOMBRAR" Text="Renombrar">
                                    <Images>
                                        <DefaultImage Url="./Imagenes/Iconos/Renombrar16.png" />
                                    </Images>
                                </ignav:Item>
                                <ignav:Item Separator="True" Text="">
                                </ignav:Item>
                                <ignav:Item Text="Nueva Agrupaci&#243;n" ImageUrl="./Imagenes/Iconos/AgrupacionNueva16.png"
                                    TagString="NUEVO">
                                    <Images>
                                        <DefaultImage Url="./Imagenes/Iconos/AgrupacionNueva16.png" />
                                    </Images>
                                </ignav:Item>
                            </Items>
                            <DisabledStyle Font-Names="MS Sans Serif" Font-Size="8pt" ForeColor="Gray">
                            </DisabledStyle>
                            <Levels>
                                <ignav:Level Index="0" />
                                <ignav:Level Index="1" />
                            </Levels>
                            <SeparatorStyle BackgroundImage="ig_menuSep.gif" CssClass="SeparatorClass" CustomRules="background-repeat:repeat-x; " />
                            <ExpandEffects ShadowColor="LightGray" />
                            <MenuClientSideEvents ItemClick="MenuContextual_ItemClick" />
                        </ignav:UltraWebMenu>
                        <ig:WebDialogWindow ID="DlgAgrupacion" runat="server" Height="105px" InitialLocation="Centered"
                            Modal="True" Width="363px" StyleSetName="Caribbean" WindowState="Hidden">
                            <ContentPane>
                                <Template>
                                    <strong><span style="font-size: 1.3em">Nombre:</span></strong>
                                    <igtxt:WebTextEdit ID="TbxAgrupacion" runat="server" Width="107px">
                                    </igtxt:WebTextEdit>
                                    <br />
                                    <br />
                                    <br />
                                    <igtxt:WebImageButton ID="WebImageButton2" runat="server" Text="Cancelar">
                                        <DisabledAppearance>
                                            <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False">
                                            </ButtonStyle>
                                        </DisabledAppearance>
                                        <Appearance>
                                            <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False">
                                            </ButtonStyle>
                                        </Appearance>
                                        <FocusAppearance>
                                            <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False">
                                            </ButtonStyle>
                                        </FocusAppearance>
                                        <PressedAppearance>
                                            <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False">
                                            </ButtonStyle>
                                        </PressedAppearance>
                                        <HoverAppearance>
                                            <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False">
                                            </ButtonStyle>
                                        </HoverAppearance>
                                        <ClientSideEvents MouseDown="$find('&lt;%=DlgAgrupacion.ClientID%&gt;').set_windowState($IG.DialogWindowState.Hidden);" />
                                    </igtxt:WebImageButton>
                                    &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<igtxt:WebImageButton ID="BtnAgrupacionAgregar"
                                        runat="server" Text="Agregar" AutoSubmit="False">
                                        <ClientSideEvents MouseDown="BtnAgrupacionAgregar_MouseDown" />
                                    </igtxt:WebImageButton>
                                    <igtxt:WebImageButton ID="BtnCambiarNombre" runat="server" Text="Cambiar Nombre"
                                        AutoSubmit="False">
                                        <ClientSideEvents MouseDown="BtnCambiarNombre_MouseDown" />
                                    </igtxt:WebImageButton>
                                </Template>
                            </ContentPane>
                            <Resizer Enabled="True" />
                        </ig:WebDialogWindow>
                        <ig:WebDialogWindow ID="DlgModal" runat="server" Height="105px" InitialLocation="Centered"
                            Modal="True" Width="363px" StyleSetName="Caribbean" WindowState="Hidden">
                            <ContentPane>
                                <Template>
                                    <strong><span style="font-size: 1.3em">Nombre:</span></strong>
                                    <igtxt:WebTextEdit ID="TbxAgrupacion0" runat="server" Width="107px">
                                    </igtxt:WebTextEdit>
                                    <br />
                                    <br />
                                    <br />
                                    <igtxt:WebImageButton ID="WebImageButton3" runat="server" Text="Cancelar">
                                        <DisabledAppearance>
                                            <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False">
                                            </ButtonStyle>
                                        </DisabledAppearance>
                                        <Appearance>
                                            <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False">
                                            </ButtonStyle>
                                        </Appearance>
                                        <FocusAppearance>
                                            <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False">
                                            </ButtonStyle>
                                        </FocusAppearance>
                                        <PressedAppearance>
                                            <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False">
                                            </ButtonStyle>
                                        </PressedAppearance>
                                        <HoverAppearance>
                                            <ButtonStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False">
                                            </ButtonStyle>
                                        </HoverAppearance>
                                        <ClientSideEvents MouseDown="$find('&lt;%=DlgAgrupacion.ClientID%&gt;').set_windowState($IG.DialogWindowState.Hidden);" />
                                    </igtxt:WebImageButton>
                                    &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<igtxt:WebImageButton ID="BtnAgrupacionAgregar0"
                                        runat="server" Text="Agregar" AutoSubmit="False">
                                        <ClientSideEvents MouseDown="BtnAgrupacionAgregar_MouseDown" />
                                    </igtxt:WebImageButton>
                                    <igtxt:WebImageButton ID="BtnCambiarNombre0" runat="server" Text="Cambiar Nombre"
                                        AutoSubmit="False">
                                        <ClientSideEvents MouseDown="BtnCambiarNombre_MouseDown" />
                                    </igtxt:WebImageButton>
                                </Template>
                            </ContentPane>
                            <Resizer Enabled="True" />
                        </ig:WebDialogWindow>
                        <iframe id="Ifrm" frameborder="0" height="1px" width="1px"></iframe>
                    </div>
                </td>
            </tr>
        </table>
                </td>
                <td class="BoxBR">
                    <img height="10" src="skins/boxed.BR.gif" width="10" />
                </td>
            </tr>
        </table>

    </div>
    </form>
</body>
</html>
<script>
    //  debugger;
    /*alto = document.getElementById('TD_WebSplitter').offsetHeight;
    document.getElementById('WebSplitter1').style.height = alto + 'px';*/
    
</script>
