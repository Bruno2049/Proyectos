<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddAndEditProduct.aspx.cs"
    Inherits="PAEEEM.CentralModule.AddAndEditProduct" Title="Agregar/Editar Producto" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />
    <link href="../Resources/Css/Table.css" type="text/css" rel="Stylesheet" />
    <link href="../Resources/Css/TablaNet.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />
    
    <script>
        function noBack() {
            window.history.forward();
        }
        noBack();
        window.onload = noBack;
        window.onpageshow = function(evt) { if (evt.persisted) noBack(); }
        window.onunload = function () { void (0); }

        
    </script>

    <style type="text/css">
        .Label {
            width: 100%;
            color: #333333;
            font-size: 16px;
        }

        .TextBox {
            width: 50%;
        }

        .Button {
            width: 120px;
        }

        .DropDownList {
            width: 50%;
        }
        .auto-style1
        {
            height: 26px;
        }
    </style>
    <script type="text/javascript">
        var pre = "ctl00_MainContent_";

        function lockScreen() {
            var lock = document.getElementById('lock');
            lock.style.width = '900px';
            lock.style.height = '500px';
            // lock.style.top = document.documentElement.clientHeight/2 - lock.style.height.replace('px','')/2 + 'px';
            lock.style.top =130;
                //document.body.offsetHeight / 1.5 - lock.style.height.replace('px', '') / 2 + 'px';
            lock.style.left = document.body.offsetWidth / 2 - lock.style.width.replace('px', '') / 2 + 'px';
            if (lock)
                lock.className = 'LockOn';
        }

        function confirmCallBackFn(arg) {
            if (arg == true) {
                var oButton = document.getElementById("ctl00_MainContent_" + "HiddenButton");
                var save = document.getElementById("ctl00_MainContent_" + "btnSave");
                var cancel = document.getElementById("ctl00_MainContent_" + "btnCancel");
                
                oButton.click();
                save.disabled = true;
                cancel.disabled = true;
            }
        }

        function confirmCallBackFn2(arg) {
            if (arg == true) {
                var oButton = document.getElementById("ctl00_MainContent_" + "HiddenButton2");
                oButton.click();
            }
        }


        function SumaModuloTransformador() {
            var precioSe = parseFloat(document.getElementById(pre + 'TxtPrecioSE').value);
            var precioFusibleSe;

            if (document.getElementById(pre + 'TxtPrecioSE').value == "")
                precioSe = 0.00;

            try {
                precioFusibleSe = parseFloat(document.getElementById(pre + 'TxtPrecioFusibleSE').value);

                if (document.getElementById(pre + 'TxtPrecioFusibleSE').value == "")
                    precioFusibleSe = 0.00; 
                
            } catch(e) {
                precioFusibleSe = 0.00;
            } 
                        

            var totalModulo = precioFusibleSe + precioSe;
            document.getElementById(pre + 'TxtPrecioToTalModTrans').value = totalModulo.toFixed(2);
            SumaTotalModulos();
        }

        function FormatoCantidad(control) {
            var valor = parseFloat(document.getElementById(control.id).value);
            document.getElementById(control.id).value = valor.toFixed(2);
        }

        function SumaModuloMediatension() {
            var precioConectores;
            var aisladores;
            var aisladoresSoporte;
            var cableGuia;
            var cableGuiaCorto;
            var cableGuiaTransformador;
            var apartarayo;
            var cortaCircuito;
            var fusible;
            var cableGuiaConexion;
            var cableConexionSubterraneo;
            var empalmes;
            var extremos;

            try {
                precioConectores = parseFloat(document.getElementById(pre + 'TxtPrecioConectores').value);
                aisladores = parseFloat(document.getElementById(pre + 'TxtAisladores').value);
                aisladoresSoporte = parseFloat(document.getElementById(pre + 'TxtAisladoresSoporte').value);
                cableGuia = parseFloat(document.getElementById(pre + 'TxtCableGuia').value);
                cableGuiaCorto = parseFloat(document.getElementById(pre + 'TxtCableGuiaCorto').value);
                cableGuiaTransformador = parseFloat(document.getElementById(pre + 'TxtCableGuiaTransformador').value);
                apartarayo = parseFloat(document.getElementById(pre + 'TxtApartarayo').value);
                cortaCircuito = parseFloat(document.getElementById(pre + 'TxtCortaCircuito').value);
                fusible = parseFloat(document.getElementById(pre + 'TxtFusible').value);
               
                if (document.getElementById(pre + 'TxtPrecioConectores').value == "")
                    precioConectores = 0.00;

                if (document.getElementById(pre + 'TxtAisladores').value == "")
                    aisladores = 0.00;

                if (document.getElementById(pre + 'TxtAisladoresSoporte').value == "")
                    aisladoresSoporte = 0.00;

                if (document.getElementById(pre + 'TxtCableGuia').value == "")
                    cableGuia = 0.00;

                if (document.getElementById(pre + 'TxtCableGuiaCorto').value == "")
                    cableGuiaCorto = 0.00;

                if (document.getElementById(pre + 'TxtCableGuiaTransformador').value == "")
                    cableGuiaTransformador = 0.00;

                if (document.getElementById(pre + 'TxtApartarayo').value == "")
                    apartarayo = 0.00;

                if (document.getElementById(pre + 'TxtCortaCircuito').value == "")
                    cortaCircuito = 0.00;

                if (document.getElementById(pre + 'TxtFusible').value == "")
                    fusible = 0.00;

            }
            catch (err) {
                precioConectores = 0.00;
                aisladores = 0.00;
                aisladoresSoporte = 0.00;
                cableGuia = 0.00;
                cableGuiaCorto = 0.00;
                cableGuiaTransformador = 0.00;
                apartarayo = 0.00;
                cortaCircuito = 0.00;
                fusible = 0.00;
                cableGuiaConexion = 0.00;
                cableConexionSubterraneo = 0.00;
            }

            try {

                var e = document.getElementById(pre + 'TxtEmpalmes');
                if (e != null) {
                    if (document.getElementById(pre + 'TxtEmpalmes').value == "")
                        empalmes = 0.00;
                    else
                        empalmes = parseFloat(document.getElementById(pre + 'TxtEmpalmes').value);

                    if (document.getElementById(pre + 'TxtExtremos').value == "")
                        extremos = 0.00;
                    else
                        extremos = parseFloat(document.getElementById(pre + 'TxtExtremos').value);
                }

                var e2 = document.getElementById(pre + 'TxtCableGuiaConexion');
                if (e2 != null) {
                    if (document.getElementById(pre + 'TxtCableGuiaConexion').value == "")
                        cableGuiaConexion = 0.00;
                    else
                        cableGuiaConexion = parseFloat(document.getElementById(pre + 'TxtCableGuiaConexion').value);

                    if (document.getElementById(pre + 'TxtCableConexionSubterraneo').value == "")
                        cableConexionSubterraneo = 0.00;
                    else
                        cableConexionSubterraneo = parseFloat(document.getElementById(pre + 'TxtCableConexionSubterraneo').value);
                }

                if (e == null) {
                    empalmes = 0.00;
                    extremos = 0.00;
                }

                if (e2 == null) {
                    cableGuiaConexion = 0.00;
                    cableConexionSubterraneo = 0.00;
                }

            }
            catch (err) {
                empalmes = 0.00;
                extremos = 0.00;
                cableGuiaConexion = 0.00;
                cableConexionSubterraneo = 0.00;
            }

            var totalModulo = precioConectores + aisladores + aisladoresSoporte + cableGuia + cableGuiaCorto + cableGuiaTransformador + apartarayo + cortaCircuito + fusible + cableGuiaConexion + cableConexionSubterraneo + empalmes + extremos;


            document.getElementById(pre + 'TxtPrecioTotalMediaTension').value = totalModulo.toFixed(2);
            SumaTotalModulos();
        }

//        function SumaModuloMediatension() {
//            var precioConectores;
//            var aisladores;
//            var aisladoresSoporte;
//            var cableGuia;
//            var cableGuiaCorto;
//            var cableGuiaTransformador;
//            var apartarayo;
//            var cortaCircuito;
//            var fusible;
//            var cableGuiaConexion;
//            var cableConexionSubterraneo;
//            var empalmes;
//            var extremos;

//            try
//			{
//				precioConectores = parseFloat(document.getElementById(pre + 'TxtPrecioConectores').value);
//				aisladores = parseFloat(document.getElementById(pre + 'TxtAisladores').value);
//				aisladoresSoporte = parseFloat(document.getElementById(pre + 'TxtAisladoresSoporte').value);
//				cableGuia = parseFloat(document.getElementById(pre + 'TxtCableGuia').value);
//				cableGuiaCorto = parseFloat(document.getElementById(pre + 'TxtCableGuiaCorto').value);
//				cableGuiaTransformador = parseFloat(document.getElementById(pre + 'TxtCableGuiaTransformador').value);
//				apartarayo = parseFloat(document.getElementById(pre + 'TxtApartarayo').value);
//				cortaCircuito = parseFloat(document.getElementById(pre + 'TxtCortaCircuito').value);
//				fusible = parseFloat(document.getElementById(pre + 'TxtFusible').value);
////				cableGuiaConexion = parseFloat(document.getElementById(pre + 'TxtCableGuiaConexion').value);
////				cableConexionSubterraneo = parseFloat(document.getElementById(pre + 'TxtCableConexionSubterraneo').value);
				
//				if (document.getElementById(pre + 'TxtPrecioConectores').value == "")
//                precioConectores = 0.00;

//				if (document.getElementById(pre + 'TxtAisladores').value == "")
//					aisladores = 0.00;

//				if (document.getElementById(pre + 'TxtAisladoresSoporte').value == "")
//					aisladoresSoporte = 0.00;

//				if (document.getElementById(pre + 'TxtCableGuia').value == "")
//					cableGuia = 0.00;

//				if (document.getElementById(pre + 'TxtCableGuiaCorto').value == "")
//					cableGuiaCorto = 0.00;

//				if (document.getElementById(pre + 'TxtCableGuiaTransformador').value == "")
//					cableGuiaTransformador = 0.00;

//				if (document.getElementById(pre + 'TxtApartarayo').value == "")
//					apartarayo = 0.00;

//				if (document.getElementById(pre + 'TxtCortaCircuito').value == "")
//					cortaCircuito = 0.00;

//				if (document.getElementById(pre + 'TxtFusible').value == "")
//					fusible = 0.00;

////				if (document.getElementById(pre + 'TxtCableGuiaConexion').value == "")
////					cableGuiaConexion = 0.00;

////				if (document.getElementById(pre + 'TxtCableConexionSubterraneo').value == "")
////					cableConexionSubterraneo = 0.00;
//			}
//			catch(err)
//			{
//				precioConectores = 0.00;
//				aisladores = 0.00;
//				aisladoresSoporte = 0.00;
//				cableGuia = 0.00;
//				cableGuiaCorto = 0.00;
//				cableGuiaTransformador = 0.00;
//				apartarayo = 0.00;
//				cortaCircuito = 0.00;
//				fusible = 0.00;
//				cableGuiaConexion = 0.00;
//				cableConexionSubterraneo = 0.00;
//			}
			
//			try
//			{
//				empalmes = parseFloat(document.getElementById(pre + 'TxtEmpalmes').value);
//				extremos = parseFloat(document.getElementById(pre + 'TxtExtremos').value);
//				cableGuiaConexion = parseFloat(document.getElementById(pre + 'TxtCableGuiaConexion').value);
//				cableConexionSubterraneo = parseFloat(document.getElementById(pre + 'TxtCableConexionSubterraneo').value);
				
//				if (document.getElementById(pre + 'TxtEmpalmes').value == "")
//					empalmes = 0.00;

//				if (document.getElementById(pre + 'TxtExtremos').value == "")
//				    extremos = 0.00;

//				if (document.getElementById(pre + 'TxtCableGuiaConexion').value == "")
//				    cableGuiaConexion = 0.00;

//				if (document.getElementById(pre + 'TxtCableConexionSubterraneo').value == "")
//				    cableConexionSubterraneo = 0.00;
//			}
//			catch(err)
//			{
//				empalmes = 0.00;
//				extremos = 0.00;
//				cableGuiaConexion = 0.00;
//			    cableConexionSubterraneo = 0.00;
//			}

//            var totalModulo = precioConectores + aisladores + aisladoresSoporte + cableGuia + cableGuiaCorto + cableGuiaTransformador + apartarayo + cortaCircuito + fusible + cableGuiaConexion + cableConexionSubterraneo + empalmes + extremos;


//            document.getElementById(pre + 'TxtPrecioTotalMediaTension').value = totalModulo.toFixed(2);
//            SumaTotalModulos();
//        }


        function SumaModuloAccProtecciones() {
            var herrajesInstalacion = parseFloat(document.getElementById(pre + 'TxtHerrajesInstalacion').value);
            var poste;
            var sistemTierra = parseFloat(document.getElementById(pre + 'TxtSistemTierra').value);
            var conexionTierra = parseFloat(document.getElementById(pre + 'TxtConexionTierra').value);


            if (document.getElementById(pre + 'TxtHerrajesInstalacion').value == "")
                herrajesInstalacion = 0.00;            

            if (document.getElementById(pre + 'TxtSistemTierra').value == "")
                sistemTierra = 0.00;

            if (document.getElementById(pre + 'TxtConexionTierra').value == "")
                conexionTierra = 0.00;

            try {
                poste = parseFloat(document.getElementById(pre + 'TxtPoste').value);

                if (document.getElementById(pre + 'TxtPoste').value == "")
                    poste = 0.00;   
                
            } catch(e) {
                poste = 0.00;
            } 
            
            var totalModulo = poste + herrajesInstalacion + sistemTierra + conexionTierra;


            document.getElementById(pre + 'TxtPrecioAccProtecciones').value = totalModulo.toFixed(2);
            SumaTotalModulos();
        }

        function SumaTotalModulos() {
            var precioToTalModTrans = parseFloat(document.getElementById(pre + 'TxtPrecioToTalModTrans').value);
            var precioTotalMediaTension = parseFloat(document.getElementById(pre + 'TxtPrecioTotalMediaTension').value);
            var cableConexionTr = parseFloat(document.getElementById(pre + 'TxtCableConexionTR').value);
            var precioAccProtecciones = parseFloat(document.getElementById(pre + 'TxtPrecioAccProtecciones').value);


            if (document.getElementById(pre + 'TxtPrecioToTalModTrans').value == "")
                precioToTalModTrans = 0.00;

            if (document.getElementById(pre + 'TxtPrecioTotalMediaTension').value == "")
                precioTotalMediaTension = 0.00;

            if (document.getElementById(pre + 'TxtCableConexionTR').value == "")
                cableConexionTr = 0.00;

            if (document.getElementById(pre + 'TxtPrecioAccProtecciones').value == "")
                precioAccProtecciones = 0.00;

            var totalModulos = precioToTalModTrans + precioTotalMediaTension + cableConexionTr + precioAccProtecciones;


            document.getElementById(pre + 'TxtPrecioTotalSE').value = totalModulos.toFixed(2);
        }

        function ValidaClaseSe() {
            var ddXClaseSe = document.getElementById(pre + 'DDXClaseSE');
            var ddXTipoSe = document.getElementById(pre + 'DDXTipoSE');

            if (ddXClaseSe.value == '') {
                ddXTipoSe.value = '';
                ddXClaseSe.focus();
                alert('Debe seleccionar la clase');
                return;
            } else {
                
                if (ddXClaseSe.value == 2)
                {

                    if (ddXTipoSe.value == 11)
                    {
                        document.getElementById(pre + 'LblPoste').style.display = 'none';
                        document.getElementById(pre + 'TxtPoste').style.display = 'none';
                    } else
                    {
                        document.getElementById(pre + 'LblPoste').style.display = '';
                        document.getElementById(pre + 'TxtPoste').style.display = '';
                    }

                    if (ddXTipoSe.value < 10) {
                        document.getElementById(pre + 'LabelCableGuia').style.display = 'none';
                        document.getElementById(pre + 'LableCableConexion').style.display = 'none';
                        document.getElementById(pre + 'TxtCableGuiaConexion').style.display = 'none';
                        document.getElementById(pre + 'TxtCableConexionSubterraneo').style.display = 'none';
                    }
                    else
                    {
                        document.getElementById(pre + 'LabelCableGuia').style.display = '';
                        document.getElementById(pre + 'LableCableConexion').style.display = '';
                        document.getElementById(pre + 'TxtCableGuiaConexion').style.display = '';
                        document.getElementById(pre + 'TxtCableConexionSubterraneo').style.display = '';
                    }

                    document.getElementById(pre + 'LblVerificacionUVIE').style.display = '';
                }
                else
                {
                    document.getElementById(pre + 'LabelCableGuia').style.display = 'none';
                    document.getElementById(pre + 'LableCableConexion').style.display = 'none';
                    document.getElementById(pre + 'TxtCableGuiaConexion').style.display = 'none';
                    document.getElementById(pre + 'TxtCableConexionSubterraneo').style.display = 'none';

                    if (ddXTipoSe.value == 4) {
                        document.getElementById(pre + 'LblVerificacionUVIE').style.display = 'none';
                    } else {
                        document.getElementById(pre + 'LblVerificacionUVIE').style.display = '';
                    }
                }
            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div id="lock" class="LockOff">
                    <img src="../images/progress.gif" alt="¡ En Proceso, Por favor Espere !" style="vertical-align: middle; position: relative;" />
                </div>
            <div>
                <br>
                <asp:Label ID="lblTitle" runat="server" Text="Agregar/Editar Producto" Font-Size="Larger"></asp:Label>
            </div>
            <div id="dtFecha" align="right">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                &nbsp;<div>
                    <asp:Label ID="lblFechaDate" runat="server"></asp:Label>
                </div>
            </div>
            <br />
            
            <table style="width: 80%">
                <tr class="tr1">
                    <td style="text-align: right">
                        Tecnología (*)
                            <%--<asp:Label ID="lblTechnology" runat="server" Text="Tecnología (*)" CssClass="Label"></asp:Label>--%>
                    </td>
                    <td style="text-align: center">
                        
                        <asp:DropDownList ID="drpTechnology" runat="server" CssClass="DropDownList" AutoPostBack="true"
                            OnSelectedIndexChanged="drpTechnology_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="drpTechnology" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                            ValidationGroup="Save" ID="Required1">
                        </asp:RequiredFieldValidator>
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br/>
                    </td>
                </tr>
            </table>

            <asp:Panel ID="PanelPlantillaComun" runat="server" Visible="True">
				<table style="width: 80%;">
					<tr>
					    <td colspan="2" class="trh">
					        Campos Base
					    </td>
					</tr>
                    <tr class="tr2">
						<td style="text-align: right">
							Fabricante (*)
						</td>
						<td style="text-align: center">
							<asp:DropDownList ID="DDXFabricante" runat="server" CssClass="DropDownList"></asp:DropDownList>
						    <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXFabricante" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXFabricante">
                            </asp:RequiredFieldValidator>
                        </td>
					</tr>
					<tr class="tr1">
						<td style="text-align: right">
							Tipo de Producto (*)
						</td>
						<td style="text-align: center">
							<asp:DropDownList ID="DDXTipoProducto" runat="server" CssClass="DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDXTipoProducto_SelectedIndexChanged"></asp:DropDownList>
						    <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXTipoProducto" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXTipoProducto">
                            </asp:RequiredFieldValidator>
                        </td>
					</tr>					
					<tr class="tr2" > <%--style="display: none;"--%>
						<td style="text-align: right">
							Nombre Producto (*)
						</td>
						<td style="text-align: center">
							<asp:TextBox ID="TxtNombreProducto" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtNombreProducto" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtNombreProducto">
                            </asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr class="tr1">
						<td style="text-align: right">
							Marca (*)
						</td>
						<td style="text-align: center">
							<asp:DropDownList ID="DDXMarcaComun" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXMarcaComun" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXMarcaComun">
                            </asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr class="tr2">
						<td style="text-align: right">
							Modelo (*)
						</td>
						<td style="text-align: center">
							<asp:TextBox ID="TxtModelo" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtModelo" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtModelo">
                            </asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr class="tr1">
						<td style="text-align: right">
							Precio Máximo (*)
						</td>
						<td style="text-align: center">
							<asp:TextBox ID="TxtPrecioMaximo" runat="server" CssClass="TextBox"></asp:TextBox>
						    <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtPrecioMaximo" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtPrecioMaximo">
                            </asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="FTETxtPrecioMaximo" 
                                runat="server" 
                                TargetControlID="TxtPrecioMaximo" 
                                FilterMode="ValidChars" 
                                ValidChars="0123456789." />
                        </td>
					</tr>
					<%--<tr class="tr2" style="display: none;">
						<td style="text-align: right">
							Eficiencia de Energía (*)
						</td>
						<td style="text-align: center">
							<asp:TextBox ID="TxtEficienciaEnergia" runat="server" CssClass="TextBox"></asp:TextBox>
						    <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtEficienciaEnergia" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtEficienciaEnergia">
                            </asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" 
                                runat="server" 
                                TargetControlID="TxtEficienciaEnergia" 
                                FilterMode="ValidChars" 
                                ValidChars="0123456789." />
                        </td>
					</tr>--%>
					<tr class="tr2">
						<td style="text-align: right">							
                            <asp:Label ID="LblCapacidadPlantillaComun" runat="server" Text="Capacidad (*)"></asp:Label>
						</td>
                        <td style="text-align: center; width: 50%">
                            <asp:DropDownList ID="DDLCapacidad" runat="server" CssClass="DropDownList"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDLCapacidad" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RequiredFieldValidator32">
                            </asp:RequiredFieldValidator>
                        </td>
						<%--<td style="text-align: center">
							<asp:TextBox ID="TxtUnidad" runat="server" CssClass="TextBox"></asp:TextBox>
						    <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtUnidad" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtUnidad">
                            </asp:RequiredFieldValidator>
                            
                        </td>--%>
					</tr>
                    <tr class="tr2">
						<td style="text-align: right">
                            <asp:Label ID="LblSistemaArreglo" runat="server" Text="Sistema / Arreglo (*)"></asp:Label>                           
						</td>
						<td style="text-align: center">
							<%--<asp:TextBox ID="TxtSistemaArreglo" runat="server" CssClass="TextBox"></asp:TextBox>
						    <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtSistemaArreglo" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtSistemaArreglo">
                            </asp:RequiredFieldValidator>--%>
                            <asp:DropDownList ID="ddlSistemaArreglo" runat="server" CssClass="DropDownList"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSistemaArreglo" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RequiredFieldValidator33">
                            </asp:RequiredFieldValidator>
                        </td>
					</tr>
				</table>
            </asp:Panel>
            
            <asp:Panel ID="PanelPlantillaBC" runat="server" Visible="True">
                
                <table style="width: 80%;">
                    <tr>
                        <td colspan ="2" class="trh">
                           Campos Base
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right; width: 50%">
                            Fabricante (*)
                        </td>
                        <td style="text-align: center; width: 50%">
                            <asp:DropDownList ID="DDXFabricanteBC" runat="server" CssClass="DropDownList" OnSelectedIndexChanged="DDXFabricanteBC_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXFabricanteBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXFabricanteBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Tipo de Producto (*)
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXTipoProductoBC" runat="server" CssClass="DropDownList" OnSelectedIndexChanged="DDXTipoProductoBC_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXTipoProductoBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXTipoProductoBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Marca (*)
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXMarcaBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXMarcaBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXMarcaBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Modelo (*)
                        </td>
                        <td style="text-align: center">
                            <asp:TextBox ID="TxtModeloBC" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtModeloBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtModeloBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Potencia (*)
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXPotenciaBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXPotenciaBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXPotenciaBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Precio Máximo (*)
                        </td>
                        <td style="text-align: center">
                            <asp:TextBox ID="TxtPrecioMaximoBC" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtPrecioMaximoBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtPrecioMaximoBC">
                            </asp:RequiredFieldValidator>
                            
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                runat="server" 
                                TargetControlID="TxtPrecioMaximoBC" 
                                FilterMode="ValidChars" 
                                ValidChars="0123456789." />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Tipo Encapsulado (*)
                        </td>
                        <td style="text-align: center">
                            <%--<asp:TextBox ID="TxtTipoEncapsuladoBC" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                            <asp:DropDownList ID="DDXTipoEncapsuladoBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXTipoEncapsuladoBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXTipoEncapsuladoBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Tipo de dieléctrico (*)
                        </td>
                        <td style="text-align: center">
                            <asp:TextBox ID="TxtTipoDielectrico" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtTipoDielectrico" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtTipoDielectrico">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Incluye Protección Interna (*)
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXProteccionInternaBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXProteccionInternaBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXProteccionInternaBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Tipo Protección Externa (*)
                        </td>
                        <td style="text-align: center">
                            <%--<asp:TextBox ID="TxtTipoProteccionExterna" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                            <asp:DropDownList ID="DDxTipoProteccionExBC" runat="server" CssClass="DropDownList" ></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDxTipoProteccionExBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDxTipoProteccionExBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Material de Cubierta (*)
                        </td>
                        <td style="text-align: center">
                            <%--<asp:TextBox ID="TxtMaterialCubiertaBC" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                            <asp:DropDownList ID="DDxMaterialCubiertaBC" runat="server" CssClass="DropDownList" >
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDxMaterialCubiertaBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDxMaterialCubiertaBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Pérdidas (*)
                        </td>
                        <td style="text-align: center">
                            <%--<asp:TextBox ID="TxtPerdidasBC" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                            <asp:DropDownList ID="DDXPerdidasBC" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXPerdidasBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXPerdidasBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Protección Interna de Sobre Corriente (*)
                        </td>
                        <td style="text-align: center">
                            <%--<asp:TextBox ID="TxtProteccionInternaBC" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                            <asp:DropDownList ID="DDxProteccionSCorrienteBC" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDxProteccionSCorrienteBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RequiredFieldValidator6">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Protección vs Fuego (*)
                        </td>
                        <td style="text-align: center">
                            <%--<asp:TextBox ID="TxtProteccionFuegoBC" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                            <asp:DropDownList ID="DDxProteccionVSFuegoBC" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDxProteccionVSFuegoBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtProteccionFuegoBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Protección vs Explosión (*)
                        </td>
                        <td style="text-align: center">
                            <%--<asp:TextBox ID="TxtProteccionExplosionBC" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                            <asp:DropDownList ID="DDxProteccionVSExplosionBC" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDxProteccionVSExplosionBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtDDxProteccionVSExplosionBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Anclaje (*)
                        </td>
                        <td style="text-align: center">
                            <%--<asp:TextBox ID="TxtAnclajeBC" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                            <asp:DropDownList ID="DDxAnclajeBC" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDxAnclajeBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDxAnclajeBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Términal de Tierra (*)
                        </td>
                        <td style="text-align: center">
                            <%--<asp:TextBox ID="TxtTerminalTierraBC" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                            <asp:DropDownList ID="DDxTerminalTierraBC" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDxTerminalTierraBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDxTerminalTierraBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PanelPlantillaBC2" runat="server">
                                <table style="width: 100%">
                                    <tr class="tr1">
                                        <td style="text-align: right; width: 50%">
                                            Tipo Controlador (*)
                                        </td>
                                        <td style="width: 50%; text-align: left">
                                            <asp:DropDownList ID="DDXTipoControladorBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXTipoControladorBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                                ValidationGroup="Save" ID="RFVDDXTipoControladorBC">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td style="text-align: right">
                                            Protección vs Sobre Corriente en controlador (*)
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="DDXPriteccionCorrienteBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXPriteccionCorrienteBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                                ValidationGroup="Save" ID="RFVDDXPriteccionCorrienteBC">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td style="text-align: right">
                                            Protección vs Temperatura (*)
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="DDXProteccionTemperaturaBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXProteccionTemperaturaBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                                ValidationGroup="Save" ID="RFVDDXProteccionTemperaturaBC">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td style="text-align: right">
                                            Protección vs Sobre distorsión en Voltaje (*)
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="DDXProteccionDistorsionBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXProteccionDistorsionBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                                ValidationGroup="Save" ID="RFVDDXProteccionDistorsionBC">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td style="text-align: right">
                                            Bloquéo de Programación en Display (*)
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="DDXBloqueoBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXBloqueoBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                                ValidationGroup="Save" ID="RFVDDXBloqueoBC">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td style="text-align: right">
                                            Tipo de Comunicación (*)
                                        </td>
                                        <td style="text-align: left">
                                            <%--<asp:TextBox ID="TxtTipoComunicacionBC" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                                            <asp:DropDownList ID="DDXTipoComunicaBC" runat="server" CssClass="DropDownList">
                                                </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXTipoComunicaBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                                ValidationGroup="Save" ID="RFVDDXTipoComunicaBC">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <%--<tr class="tr1">
                        <td style="text-align: right">
                            Tipo Controlador (*)
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXTipoControladorBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXTipoControladorBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXTipoControladorBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Protección vs Sobre Corriente en controlador (*)
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXPriteccionCorrienteBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXPriteccionCorrienteBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXPriteccionCorrienteBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Protección vs Temperatura (*)
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXProteccionTemperaturaBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXProteccionTemperaturaBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXProteccionTemperaturaBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Protección vs Sobre distorsión en Voltaje (*)
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXProteccionDistorsionBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXProteccionDistorsionBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXProteccionDistorsionBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Bloquéo de Programación en Display (*)
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXBloqueoBC" runat="server" CssClass="DropDownList"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXBloqueoBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXBloqueoBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Tipo de Comunicación (*)
                        </td>
                        <td style="text-align: center">
                            <asp:TextBox ID="TxtTipoComunicacionBC" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtTipoComunicacionBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVTxtTipoComunicacionBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>--%>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Protección del Gabinete (*)
                        </td>
                        <td style="text-align: center">
                            <%--<asp:TextBox ID="TxtProteccionGabineteBC" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                            <asp:DropDownList ID="DDXProteccionGabBC" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXProteccionGabBC" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="RFVDDXProteccionGabBC">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>

            </asp:Panel>

            <div id="containerSE" runat="server">
                <table style="width: 80%;">
                    <tr>
                        <td colspan="2" class="trh">
                            Campos Base
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PanelPrincipalSE" runat="server" Visible="True">
                                <table style="width: 100%;">
                                    <tr class="tr2">
                                        <td>
                                            Fabricante
                                        </td>
                                        <td colspan="4">
                                            <asp:DropDownList ID="DDXFabricanteSE" runat="server" Width="90%"></asp:DropDownList>
                                        </td>
                                        <td>
                                            Modelo:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtModeloSE" runat="server" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtModeloSE" 
                                                ErrorMessage="Modelo"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredDDXCombinacionTecnologias"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        
                                        <td>
                                            Clase:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXClaseSE" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDXClaseSE_SelectedIndexChanged" ></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="DDXClaseSE" 
                                                ErrorMessage="Clase"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator7"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Tipo:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXTipoSE" runat="server" 
                                                onselectedindexchanged="DDXTipoSE_SelectedIndexChanged" 
                                                AutoPostBack="True"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="DDXTipoSE" 
                                                ErrorMessage="Tipo"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator8"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Precio Total:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPrecioTotalSE" runat="server" ReadOnly="True" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="font-weight: bold; text-align: center; color: #000099;" colspan="2">
                            Modulo del Transformador
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PanelModuloTranformadorSE" runat="server" Visible="True">
                                <table style="width: 100%;">
                                    <tr class="tr1">
                                        <td>
                                            Característica
                                        </td>
                                        <td>
                                            Marca
                                        </td>
                                        <td>
                                            Capacidad
                                        </td>
                                        <td>
                                            Relación de Transformación
                                        </td>
                                        <td>
                                            Precio
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            Transformador
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXMarcaSE" runat="server" CssClass="DropDownList" Width="150px"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="DDXMarcaSE" 
                                                ErrorMessage="Marca Transformador"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator9"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXCapacidadSE" runat="server" 
                                                onselectedindexchanged="DDXCapacidadSE_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="DDXCapacidadSE" 
                                                ErrorMessage="Capacidad Transformador"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator10"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXRelTransSE" runat="server" 
                                                onselectedindexchanged="DDXRelTransSE_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="DDXRelTransSE" 
                                                ErrorMessage="Relación de Transformación"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator31"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPrecioSE" runat="server" onChange ="SumaModuloTransformador();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtPrecioSE" 
                                                ErrorMessage="Precio Transformador"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator11"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                                runat="server" 
                                                TargetControlID="TxtPrecioSE" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td class="auto-style1">
                                            <asp:Label ID="LblDDXMarcaFusibleSE" runat="server" Text="Fusible de Subestación en Media Tensión"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            
                                        </td>
                                        <%--<td class="auto-style1">
                                            <asp:DropDownList ID="DDXMarcaFusibleSE" runat="server" CssClass="DropDownList"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="DDXMarcaFusibleSE" 
                                                ErrorMessage="Marca Fusible de Subestación en Media Tensión"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator12"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="auto-style1">
                                            <asp:DropDownList ID="DDXCapacidadFusibleSE" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="DDXCapacidadFusibleSE" 
                                                ErrorMessage="Capacidad Fusible de Subestación en Media Tensión"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator13"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                        </td>--%>
                                        <td class="auto-style1">
                                            <asp:TextBox ID="TxtPrecioFusibleSE" runat="server" onChange ="SumaModuloTransformador();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtPrecioFusibleSE" 
                                                ErrorMessage="Precio Fusible de Subestación en Media Tensión"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator14"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                                runat="server" 
                                                TargetControlID="TxtPrecioFusibleSE" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            Precio Total Modulo de Transformador
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="TxtPrecioToTalModTrans" runat="server" onChange ="SumaTotalModulos()" ReadOnly="True" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="font-weight: bold; color: #000099; text-align: center" colspan="2">
                            Modulo de Media Tensión
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PanelModuloMediaTensionSE" runat="server" Visible="True">
                                <table style="width: 100%;">
                                    <tr class="tr2">
                                        <td style="width: 50%;">
                                            Característica
                                        </td>
                                        <td style="width: 50%;">
                                            Precio
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblPrecioConectores" runat="server" Text="Conectores “T”"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtPrecioConectores" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtPrecioConectores" 
                                                ErrorMessage="Precio Conectores T"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator15"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                                runat="server" 
                                                TargetControlID="TxtPrecioConectores" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LblAisladores" runat="server" Text="Aisladores Tensión"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtAisladores" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtAisladores" 
                                                ErrorMessage="Precio Aisladores Tensión"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator16"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                                runat="server" 
                                                TargetControlID="TxtAisladores" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblAisladoresSoporte" runat="server" Text="Aisladores Soporte"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtAisladoresSoporte" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtAisladoresSoporte" 
                                                ErrorMessage="Precio Aisladores Soporte"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator17"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
                                                runat="server" 
                                                TargetControlID="TxtAisladoresSoporte" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LblCableGuia" runat="server" Text="Cable Guía de Conexión de Línea a Apartarrayos"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCableGuia" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtCableGuia" 
                                                ErrorMessage="Precio Cable Guía de Conexión de Línea a Apartarrayos"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator18"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" 
                                                runat="server" 
                                                TargetControlID="TxtCableGuia" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblCableGuiaCorto" runat="server" Text="Cable Guía de Conexión de Línea a Corta circuito"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCableGuiaCorto" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtCableGuiaCorto" 
                                                ErrorMessage="Precio Cable Guía de Conexión de Línea a Corta circuito"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator19"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" 
                                                runat="server" 
                                                TargetControlID="TxtCableGuiaCorto" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LblCableGuiaTransformador" runat="server" Text="Cable Guía de Conexión de Línea a Transformador"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCableGuiaTransformador" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtCableGuiaTransformador" 
                                                ErrorMessage="Precio Cable Guía de Conexión de Línea a Transformador"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator20"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
                                                runat="server" 
                                                TargetControlID="TxtCableGuiaTransformador" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblApartarayo" runat="server" Text="Apartarrayo"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtApartarayo" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtApartarayo" 
                                                ErrorMessage="Precio Apartarrayo"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator21"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" 
                                                runat="server" 
                                                TargetControlID="TxtApartarayo" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LblCortaCircuito" runat="server" Text="Corta circuito"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCortaCircuito" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtCortaCircuito" 
                                                ErrorMessage="Precio Corta circuito"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator22"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" 
                                                runat="server" 
                                                TargetControlID="TxtCortaCircuito" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblFusible" runat="server" Text="Fusible"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtFusible" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtFusible" 
                                                ErrorMessage="Precio Fusible"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator23"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" 
                                                runat="server" 
                                                TargetControlID="TxtFusible" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LabelCableGuia" runat="server" Text="Cable Guía de Conexión de Línea a Terminales de Acometida" ></asp:Label> 
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCableGuiaConexion" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtCableGuiaConexion" 
                                                ErrorMessage="Precio Cable Guía de Conexión de Línea a Terminales de Acometida"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator24"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" 
                                                runat="server" 
                                                TargetControlID="TxtCableGuiaConexion" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LableCableConexion" runat="server" Text="Cable de conexión de media tensión subterráneo (acometida)" ></asp:Label> 
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCableConexionSubterraneo" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtCableConexionSubterraneo" 
                                                ErrorMessage="Precio Cable de conexión de media tensión subterráneo (acometida)"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator25"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" 
                                                runat="server" 
                                                TargetControlID="TxtCableConexionSubterraneo" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LblEmpalmes" runat="server" Text="Empalmes para cables de Media Tensión"></asp:Label> 
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtEmpalmes" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                           <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtEmpalmes" 
                                                ErrorMessage="Precio Empalmes para cables de Media Tensión"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator12"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" 
                                                runat="server" 
                                                TargetControlID="TxtEmpalmes" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblCableExtremos" runat="server" Text="Cable de Conexión Subterranéo para ambos extremos"></asp:Label> 
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtExtremos" runat="server" onChange ="SumaModuloMediatension();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                           <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtExtremos" 
                                                ErrorMessage="Cable de Conexión Subterranéo para ambos extremos"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator13"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" 
                                                runat="server" 
                                                TargetControlID="TxtExtremos" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            Precio Total Modulo de Media Tensión
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPrecioTotalMediaTension" runat="server" onChange ="SumaTotalModulos()" ReadOnly="True" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="font-weight: bold; color: #000099; text-align: center" colspan="2">
                            Modulo de Baja Tensión
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PanelModuloBajaTension" runat="server" Visible="True">
                                <table style="width: 100%;">
                                    <tr class="tr2">
                                        <td>
                                            Característica
                                        </td>
                                        <td>
                                            Precio
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            Cable de conexión de TR a Equipo de Medición (acometida) y primer medio de seccionamiento de la carga
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCableConexionTR" runat="server" onChange ="SumaTotalModulos();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtCableConexionTR" 
                                                ErrorMessage="Precio Cable de conexión de TR a Equipo de Medición (acometida) y primer medio de seccionamiento de la carga"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator26"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" 
                                                runat="server" 
                                                TargetControlID="TxtCableConexionTR" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="font-weight: bold; color: #000099; text-align: center" colspan="2">
                            Módulo Accesorios y Protecciones
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PanelModuloAccesorios" runat="server" Visible="True">
                                <table style="width: 100%;">
                                    <tr class="tr1">
                                        <td>
                                            Característica
                                        </td>
                                        <td>
                                            Precio
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            Herrajes para la Instalación
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtHerrajesInstalacion" runat="server" onChange ="SumaModuloAccProtecciones();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtHerrajesInstalacion" 
                                                ErrorMessage="Precio Herrajes para la Instalación"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator27"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" 
                                                runat="server" 
                                                TargetControlID="TxtHerrajesInstalacion" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblPoste" runat="server" Text="Poste"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtPoste" runat="server" onChange ="SumaModuloAccProtecciones();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtPoste" 
                                                ErrorMessage="Precio Poste"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator28"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" 
                                                runat="server" 
                                                TargetControlID="TxtPoste" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            Sistema(s) de Tierra
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtSistemTierra" runat="server" onChange ="SumaModuloAccProtecciones();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtSistemTierra" 
                                                ErrorMessage="Precio Sistema(s) de Tierra"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator29"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" 
                                                runat="server" 
                                                TargetControlID="TxtSistemTierra" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            Cable de conexión a sistema a tierra
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtConexionTierra" runat="server" onChange ="SumaModuloAccProtecciones();FormatoCantidad(this)" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" 
                                                ControlToValidate="TxtConexionTierra" 
                                                ErrorMessage="Precio Cable de conexión a sistema a tierra"
                                                ValidationGroup="SaveSE" 
                                                Display="Dynamic" 
                                                Text="*"
                                                EnableClientScript="true" 
                                                ID="RequiredFieldValidator30"
                                                InitialValue="">
                                             </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" 
                                                runat="server" 
                                                TargetControlID="TxtConexionTierra" 
                                                FilterMode="ValidChars" 
                                                ValidChars="0123456789." />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            Precio Total Módulo Accesorios y Protecciones
                                        </td>
                                        <td> 
                                            <asp:TextBox ID="TxtPrecioAccProtecciones" runat="server" onChange ="SumaTotalModulos()" ReadOnly="True" Enabled="False" onpaste="return false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="font-weight: bold; color: #000099; text-align: center; width: 90%;" colspan="2">
                            Módulo Conceptos Adicionales
                        </td>
                    </tr>
                    <tr class="tr2" style="width: 90%;">
                        <td>
                            Obra Civil (excavaciones, ductos para cable en media tensión)
                        </td>
                    </tr>
                    <tr class="tr1" style="width: 90%;">
                        <td>
                            Obra Civil(excavaciones, ductos para cable en baja tensión)
                        </td>
                    </tr>
                    <tr class="tr2" style="width: 90%;">
                        <td>
                            Mano de obra
                        </td>
                    </tr>
                    <tr class="tr1" style="width: 90%;">
                        <td>
                            Trámites ante CFE
                        </td>
                    </tr>
                    <tr class="tr2" style="width: 90%;">
                        <td>
                            Libranza
                        </td>
                    </tr>
                    <tr class="tr1" style="width: 90%;">
                        <td>
                            <asp:Label ID="LblVerificacionUVIE" runat="server" Text="Verificación UVIE"></asp:Label>
                        </td>
                    </tr>
                    <tr class="tr2" style="width: 90%;">
                        <td>
                            <asp:ValidationSummary ID="ProductoSEValidationSummary" runat="server" CssClass="failureNotification" 
                                 ValidationGroup="SaveSE" Font-Size="X-Small" HeaderText="Resumén Captura:"/>
                        </td>
                    </tr>
                </table>
                
            </div>
            
            <div>
                <asp:Panel ID="PanelCammposCustomizables" runat="server" Visible="True">
                    
                </asp:Panel>
            </div>

            <div runat="server" id="PlantillasAntiguas">
                <table width="100%">
                    
                    <tr>
                        <td width="20%">
                            <asp:Label ID="lblManufacture" runat="server" Text="Fabricante (*)" CssClass="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpManufacture" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="drpManufacture" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="Required">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                </table>
                <!-- RSA 2012-09-11 No aplican para SE Start -->
                <asp:Panel ID="PanelNotSE" runat="server">
                    <table width="100%">
                        <tr>
                            <td width="20%">
                                <asp:Label ID="lblTipoProduct" runat="server" Text="Tipo de Producto (*)" CssClass="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpTipoProduct" runat="server" CssClass="DropDownList">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="drpTipoProduct" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                    ValidationGroup="Save" ID="RequiredFieldValidator1">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblNameProduct" runat="server" Text="Nombre Producto (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="txtNameProduct" runat="server" CssClass="TextBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNameProduct" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredFieldValidator3">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <!-- RSA 2012-09-11 No aplican para SE End -->
                <table width="100%">
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblMarca" runat="server" Text="Marca (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpMarca" runat="server" CssClass="DropDownList">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="drpMarca" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                    ValidationGroup="Save" ID="RequiredFieldValidator2">
                                </asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblModel" runat="server" Text="Modelo Producto (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtModel" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtModel" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                    ValidationGroup="Save" ID="RequiredFieldValidator4">
                                </asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                </table>
                <!-- RSA 2012-09-11 Nuevos para SE Start -->
                <asp:Panel ID="PanelSE" runat="server" Visible="false" Enabled="false">
                    <table width="100%">
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_TIPO" runat="server" Text="Tipo (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_TIPO" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_TIPO" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_TIPO">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_TRANSFORMADOR" runat="server" Text="Transformador (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_TRANSFORMADOR" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <%--                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_TRANSFORMADOR" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_TRANSFORMADOR">
                                    </asp:RequiredFieldValidator>
                                    --%>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_TRANSFORM_FASE" runat="server" Text="Fase del Transformador (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_TRANSFORM_FASE" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_TRANSFORM_FASE" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_TRANSFORM_FASE">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_TRANSFORM_MARCA" runat="server" Text="Marca del Transformador (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_TRANSFORM_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_TRANSFORM_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_TRANSFORM_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_TRANSFORM_RELACION" runat="server" Text="Relación de Transformación (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_TRANSFORM_RELACION" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_TRANSFORM_RELACION" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_TRANSFORM_RELACION">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_APARTARRAYO" runat="server" Text="Apartarrayo (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_APARTARRAYO" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_APARTARRAYO" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_APARTARRAYO">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_APARTARRAYO_MARCA" runat="server" Text="Marca del Apartarrayo (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_APARTARRAYO_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_APARTARRAYO_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_APARTARRAYO_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_CORTACIRCUITO" runat="server" Text="Cortacircuito – Fusible (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_CORTACIRCUITO" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_CORTACIRCUITO" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_CORTACIRCUITO">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_CORTACIRC_MARCA" runat="server" Text="Marca Cortacircuito – Fusible (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_CORTACIRC_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_CORTACIRC_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_CORTACIRC_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_INTERRUPTOR" runat="server" Text="Interruptor Termomagnético (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_INTERRUPTOR" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_INTERRUPTOR" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_INTERRUPTOR">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_INTERRUPTOR_MARCA" runat="server" Text="Marca Interruptor Termomagnético (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_INTERRUPTOR_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_INTERRUPTOR_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_INTERRUPTOR_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_CONDUCTOR" runat="server" Text="Conductor de Tierra (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_CONDUCTOR" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_CONDUCTOR" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_CONDUCTOR">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_CONDUCTOR_MARCA" runat="server" Text="Marca Conductor de Tierra (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_CONDUCTOR_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_CONDUCTOR_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_CONDUCTOR_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_COND_CONEXION" runat="server" Text="Conductor de Conexión a Centro de Carga (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_COND_CONEXION" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_COND_CONEXION" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_COND_CONEXION">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_COND_CONEXION_MARCA" runat="server" Text="Marca Conductor de Conexión (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_COND_CONEXION_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_COND_CONEXION_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_COND_CONEXION_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table width="100%">
                    <!-- RSA 2012-09-11 Nuevos para SE End -->
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblMaxPrice" runat="server" Text="Precio Máximo (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtMaxPrice" runat="server" CssClass="TextBox" ToolTip="(2 Dígitos Decimales)"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(.[0-9]{2})?$"
                                    ControlToValidate="txtMaxPrice" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save" ID="RegularExpressionValidator5">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblEficience" runat="server" Text="Eficiencia de Energía (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtEficience" runat="server" CssClass="TextBox" ToolTip="(4 Dígitos Decimales)"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(.[0-9]{4})?$"
                                    ControlToValidate="txtEficience" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save" ID="RegularExpressionValidator1">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblConsumer" runat="server" Text="Max Consumo de 24h (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtConsumer" runat="server" CssClass="TextBox" ToolTip="(2 Dígitos Decimales)"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(.[0-9]{2})?$"
                                    ControlToValidate="txtConsumer" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save" ID="RegularExpressionValidator2">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblCapacidad" runat="server" Text="Capacidad (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpCapacidad" runat="server" CssClass="DropDownList">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="drpCapacidad" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                    ValidationGroup="Save" ID="RequiredFieldValidator5">
                                </asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblAhorroConsumo" runat="server" Text="Ahorro Consumo Kwh/Mes (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtAhorroConsumo" runat="server" CssClass="TextBox" ToolTip="(4 Dígitos Decimales)"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(.[0-9]{4})?$"
                                    ControlToValidate="txtAhorroConsumo" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save" ID="RegularExpressionValidator3">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="LblAhorroDemanda" runat="server" Text="Ahorro Demanda Kw (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtAhorroDemanda" runat="server" CssClass="TextBox" ToolTip="(2 Dígitos Decimales)"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(.[0-9]{2})?$"
                                    ControlToValidate="txtAhorroDemanda" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save" ID="RegularExpressionValidator4">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                          <asp:Label ID="lblMotivos" Text="Motivos" runat="server" CssClass="Label" Height="74px" Width="63px" Visible="False" />&nbsp
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMotivos" CssClass="TextBox" Height="71px" TextMode="MultiLine" ToolTip="Ingrese los motivos por los cuales se realizaran cambios al usuario" Width="546px" Visible="False" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            
            <div style="display: none">
                        <asp:Button ID="HiddenButton" BackColor="#FFFFFF" OnClick="HiddenButton_Click" runat="server" Width="0px" OnClientClick="lockScreen();" />
                        <asp:Button ID="HiddenButton2" BackColor="#FFFFFF" OnClick="HiddenButton2_Click" runat="server" Width="0px" />
                    </div>
            <!--NUEVAS PLANTILLAS: COMUN, SUBESTACIONES Y BANCO DE CAPACITORES -->
            
            
            <!--TERMINA NUEVAS PLANTILLAS -->

            <div>
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="Button" OnClick="btnSave_Click"
                    OnClientClick="lockScreen(); " />
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?')"
                    OnClick="btnCancel_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
