<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministradorPlantillasProd.aspx.cs" Inherits="PAEEEM.CentralModule.AdministradorPlantillasProd" MasterPageFile="../Site.Master" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/TablaNet.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .DropDownList
        {
            width: 200px;
        }
        .DropDownList2
        {
            width: 150px;
        }
        .TextBox
        {
            width: 200px;
        }
        .TextBox2
        {
            width: 150px;
        }
        /*.tr1
        {
            background-color: #E0E0E0;
            text-align: center;
        }
        .tr2
        {
            background-color: #FFFFFF;
            text-align: center;
        }*/
        .tablaSE {
            width: 100%;
        }
        .tablaComun {
            width: 80%;
        }
    </style>

    <script type="text/javascript">
        var pre = "ctl00_MainContent_";



        function ValidaClaseSe() {
            var ddXClaseSe = document.getElementById(pre + 'DDXClaseSE');
            var ddXTipoSe = document.getElementById(pre + 'DDXTipoSE');

            if (ddXClaseSe.value == '') {
                ddXTipoSe.value = '';
                ddXClaseSe.focus();
                alert('Debe seleccionar la clase');
                return;
            } else {

                if (ddXClaseSe.value == 2) {

                    if (ddXTipoSe.value == 11) {
                        document.getElementById(pre + 'LblPoste').style.display = 'none';
                        document.getElementById(pre + 'TxtPoste').style.display = 'none';
                    } else {
                        document.getElementById(pre + 'LblPoste').style.display = '';
                        document.getElementById(pre + 'TxtPoste').style.display = '';
                    }

                    if (ddXTipoSe.value < 10) {
                        document.getElementById(pre + 'LabelCableGuia').style.display = 'none';
                        document.getElementById(pre + 'LableCableConexion').style.display = 'none';
                        document.getElementById(pre + 'TxtCableGuiaConexion').style.display = 'none';
                        document.getElementById(pre + 'TxtCableConexionSubterraneo').style.display = 'none';
                        document.getElementById(pre + 'ChkTxtCableGuiaConexion').style.display = 'none';
                        document.getElementById(pre + 'ChkTxtCableConexionSubterraneo').style.display = 'none';
                    }
                    else {
                        document.getElementById(pre + 'LabelCableGuia').style.display = '';
                        document.getElementById(pre + 'LableCableConexion').style.display = '';
                        document.getElementById(pre + 'TxtCableGuiaConexion').style.display = '';
                        document.getElementById(pre + 'TxtCableConexionSubterraneo').style.display = '';
                        document.getElementById(pre + 'ChkTxtCableGuiaConexion').style.display = '';
                        document.getElementById(pre + 'ChkTxtCableConexionSubterraneo').style.display = '';
                    }

                    document.getElementById(pre + 'LblVerificacionUVIE').style.display = '';
                }
                else {
                    document.getElementById(pre + 'LabelCableGuia').style.display = 'none';
                    document.getElementById(pre + 'LableCableConexion').style.display = 'none';
                    document.getElementById(pre + 'TxtCableGuiaConexion').style.display = 'none';
                    document.getElementById(pre + 'TxtCableConexionSubterraneo').style.display = 'none';
                    document.getElementById(pre + 'ChkTxtCableGuiaConexion').style.display = 'none';
                    document.getElementById(pre + 'ChkTxtCableConexionSubterraneo').style.display = 'none';

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
            <div>
                <br>
               <h2> <asp:Label ID="lblTitle" runat="server" Text="ADMINISTRACION DE PLANTILLAS DE PRODUCTO" Font-Size="Large" Font-Bold="True"></asp:Label>
                <%--&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="ImgBtnHome" runat="server" ImageUrl="~/CentralModule/images/home-icon.jpg" OnClick="ImgBtnHome_Click" Visible="False" />--%>
                <asp:LinkButton ID="ImgBtnHome" runat="server" Font-Bold="True" Font-Size="Large" Visible="False" OnClick="link1_Click">ADMINISTRACION DE PLANTILLAS DE PRODUCTO</asp:LinkButton></h2>
                <hr class="rule" />
                <br /><br />
            </div>
            
            <asp:Panel ID="PanelPlantillasTecnologia" runat="server" Visible="True" Width="90%">
                

                <div ID="containerLiteral" runat="server" style="width: 90%">
                    <asp:Button ID="BtnAgregarNuevaPlantilla" runat="server" Text="Agregar Nueva Plantilla" 
                    OnClientClick="return confirm('Confirmar Agregar Nueva Plantilla');" OnClick="BtnAgregarNuevaPlantilla_Click"/>
                    &nbsp;&nbsp;
                <asp:Button ID="BtnEditarPlantilla" runat="server" Text="Editar Plantilla" 
                    OnClientClick="return confirm('Confirmar Editar Plantilla');" OnClick="BtnEditarPlantilla_Click"/>
                    <br/><br/>
                    <asp:Literal ID="LiteralTablaPlantillas" runat="server"></asp:Literal>
                </div>
                
                <div ID="contaiterEdicionPlantilla" runat="server">
                    <table style="width: 40%">
                        <tr>
                            <td class="trh" colspan="2">
                                Catalogo de Plantillas
                            </td>
                        </tr>
                        <tr class="tr1">
                           <td>
                              <br/>
                           </td>
                            <td>
                                <asp:RadioButtonList ID="RadioBtnLstPlantillas" runat="server"></asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BtnCancelarEdicion" runat="server" Text="Cancelar" OnClick="BtnCancelarEdicion_Click" />
                                &nbsp;
                                <asp:Button ID="BtnIrEdicionPlantilla" runat="server" Text="Aceptar" OnClick="BtnIrEdicionPlantilla_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div ID="containerNP" runat="server">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="LblNuevaPlantilla" runat="server" Text="Nombre Plantilla: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtNuevaPlantilla" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtNuevaPlantilla" 
                                    ErrorMessage="El Nombre de la Plantilla es requerido"
                                    ValidationGroup="Save" ID="Required1">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>                           
                            <td>
                                <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" />
                            </td>
                            <td>
                                <asp:Button ID="BtnCrearNuevaPlantilla" runat="server" Text="Crear Plantilla" OnClick="BtnCrearNuevaPlantilla_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                

            </asp:Panel>
            
            <asp:Panel ID="PanelPlantillaComun" runat="server" Visible="True">
                <table style="width: 80%;">
                    <tr>
                        <td colspan ="2" align="center" class="trh">
                            Editar Plantilla - 
                            <asp:Label ID="LblNomPlantilla" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="2" align="center" class="trh">
                           Campos Base
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <table style="border: 1px solid #000000; clip: rect(1px, 1px, 1px, 1px); width: 100%;">
                                <tr>
                                    <td colspan="2" class="trh">
                                        Campo
                                    </td>
                                    <td class="trh">
                                        &nbsp;Imprimir en Reporte
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr class="tr1">
                                    <td align="right">
                                        Tecnología
                                    </td>
                                    <td align="center">
                                        <asp:DropDownList ID="DDXTecnologia" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox ID="ChkImprimirTeconologia" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tr2">
                                    <td align="right">
                                        Fabricante
                                    </td>
                                    <td align="center">
                                        <asp:DropDownList ID="DDXFabricante" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox ID="ChkImprimirFabricante" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tr1">
                                    <td align="right">
                                        Tipo de Producto
                                    </td>
                                    <td align="center">
                                        <asp:DropDownList ID="DDXTipoProducto" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox ID="ChkImprimirTipoProducto" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tr2" runat="server">
                                    <td align="right">
                                        Sistema / Arreglo
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="TxtSistemaArreglo" runat="server" Enabled="False" CssClass="TextBox2"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox ID="ChkImprimirSistemaArreglo" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tr1">
                                    <td align="right">
                                        Nombre Producto
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="TxtNombreProducto" runat="server" Enabled="False" CssClass="TextBox2"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox ID="ChkImprimirNombreProducto" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tr2">
                                    <td align="right">
                                        Marca
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="TxtMarca" runat="server" Enabled="False" CssClass="TextBox2"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox ID="ChkImprimirMarca" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tr1">
                                    <td align="right">
                                        Modelo
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="TxtModelo" runat="server" Enabled="False" CssClass="TextBox2"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox ID="ChkImprimirModelo" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tr2">
                                    <td align="right">
                                        Precio Máximo
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="TxtPrecioMaximo" runat="server" Enabled="False" CssClass="TextBox2"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox ID="ChkImprimirPrecioMaximo" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tr1">
                                    <td align="right">
                                        Capacidad de Energía
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="TxtEficienciaEnergia" runat="server" Enabled="False" CssClass="TextBox2"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox ID="ChkImprimirEficiencia" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tr2">
                                    <td align="right">
                                        Unidad
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="TxtUnidad" runat="server" Enabled="False" CssClass="TextBox2"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox ID="ChkImprimirUnidad" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                  
                </table>
            </asp:Panel>
            
            <asp:Panel ID="PanelPlantillaBC" runat="server" Visible="False">
                
                <table width="80%">
                    <tr>
                        <td colspan ="3" class="trh">
                            Editar Plantilla - Banco de Capacitores
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="3">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="3" class="trh">
                           Campos Base
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td colspan="2" style="font-weight: bold;">
                            Campo
                        </td>
                        <td style="font-weight: bold;">
                            Imprimir en Reporte
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right; width: 40%" >
                            Tecnología
                        </td>
                        <td style="text-align: center; width: 30%">
                            <asp:DropDownList ID="DDXTecnologiaBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                        </td>
                        <td style="text-align: center; width: 30%">
                            <asp:CheckBox ID="CnkDDXTecnologiaBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Fabricante
                        </td>
                        <td style="text-align: center"">
                            <asp:DropDownList ID="DDXFabricanteBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkDDXFabricanteBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Tipo de Producto
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXTipoProductoBC" runat="server" CssClass="DropDownList2" AutoPostBack="True" OnSelectedIndexChanged="DDXTipoProductoBC_SelectedIndexChanged"></asp:DropDownList>
                            <br/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDXTipoProductoBC" ErrorMessage="(*) Seleccione el Tipo de Producto"
                                ValidationGroup="Save" ID="RFVDDXTipoProductoBC" Font-Size="X-Small"></asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkDDXTipoProductoBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Marca
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXMarcaBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkDDXMarcaBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Modelo
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXModeloBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkDDXModeloBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Potencia
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXPotenciaBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkDDXPotenciaBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Precio Máximo
                        </td>
                        <td style="text-align: center">
                            <asp:TextBox ID="TxtPrecioMaximoBC" runat="server" ReadOnly="True" CssClass="TextBox2"></asp:TextBox>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkTxtPrecioMaximoBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Tipo Encapsulado
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXTipoEncapsuladoBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkDDXTipoEncapsuladoBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Tipo de dieléctrico
                        </td>
                        <td style="text-align: center">
                            <asp:TextBox ID="TxtTipoDielectrico" runat="server" ReadOnly="True" CssClass="TextBox2"></asp:TextBox>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkTxtTipoDielectrico" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Incluye Protección Interna
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXProteccionInternaBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkDDXProteccionInternaBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Tipo Protección Externa
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDxTipoProteccionExBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkDDxTipoProteccionExBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Material de Cubierta
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDxMaterialCubiertaBC" runat="server" CssClass="DropDownList2" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkTxtMaterialCubiertaBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Pérdidas
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXPerdidasBC" runat="server" CssClass="DropDownList2" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkTxtPerdidasBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Protección Interna de Sobre Corriente
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDxProteccionSCorrienteBC" runat="server" CssClass="DropDownList2" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkTxtProteccionInternaBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Protección vs Fuego
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDxProteccionVSFuegoBC" runat="server" CssClass="DropDownList2" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkTxtProteccionFuegoBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Protección vs Explosión
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDxProteccionVSExplosionBC" runat="server" CssClass="DropDownList2" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkTxtProteccionExplosionBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Anclaje
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDxAnclajeBC" runat="server" CssClass="DropDownList2" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkTxtAnclajeBC" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="text-align: right">
                            Términal de Tierra
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDxTerminalTierraBC" runat="server" CssClass="DropDownList2" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkTxtTerminalTierraBC" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="PanelPlantillaBC2" runat="server">
                                <table style="width: 100%">                                                                   
                                        <tr class="tr1">
                                            <td style="text-align: right; width: 40%">
                                                Tipo Controlador
                                            </td>
                                            <td style="text-align: center; width: 30%">
                                                <asp:DropDownList ID="DDXTipoControladorBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; width: 30%">
                                                <asp:CheckBox ID="ChkDDXTipoControladorBC" runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="tr2">
                                            <td style="text-align: right">
                                                Protección vs Sobre Corriente en controlador
                                            </td>
                                            <td style="text-align: center">
                                                <asp:DropDownList ID="DDXPriteccionCorrienteBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:CheckBox ID="ChkDDXPriteccionCorrienteBC" runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="tr1">
                                            <td style="text-align: right">
                                                Protección vs Sobre Temperatura
                                            </td>
                                            <td style="text-align: center">
                                                <asp:DropDownList ID="DDXProteccionTemperaturaBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:CheckBox ID="ChkDDXProteccionTemperaturaBC" runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="tr2">
                                            <td style="text-align: right">
                                                Protección vs Sobre distorsión en Voltaje
                                            </td>
                                            <td style="text-align: center">
                                                <asp:DropDownList ID="DDXProteccionDistorsionBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:CheckBox ID="ChkDDXProteccionDistorsionBC" runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="tr1">
                                            <td style="text-align: right">
                                                Bloqueo de Programación en Display
                                            </td>
                                            <td style="text-align: center">
                                                <asp:DropDownList ID="DDXBloqueoBC" runat="server" CssClass="DropDownList2" Enabled="False"></asp:DropDownList>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:CheckBox ID="ChkDDXBloqueoBC" runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="tr2">
                                            <td style="text-align: right">
                                                Tipo de Comunicación
                                            </td>
                                            <td style="text-align: center">
                                                <asp:DropDownList ID="DDXTipoComunicaBC" runat="server" CssClass="DropDownList2" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:CheckBox ID="ChkTxtTipoComunicacionBC" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="text-align: right">
                            Protección del Gabinete
                        </td>
                        <td style="text-align: center">
                            <asp:DropDownList ID="DDXProteccionGabBC" runat="server" CssClass="DropDownList2" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="ChkTxtProteccionGabineteBC" runat="server" />
                        </td>
                    </tr>
                </table>

            </asp:Panel>
            
            <div id="containerSE" runat="server">
                <%--<p style="text-align: right; color: #FF0000; font-weight: bold;" style="width: 90%">
                    
                </p>--%>
                <table style="width: 90%">
                    <tr>
                        <td colspan="2" style="text-align: right; color: #FF0000; font-weight: bold;">
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                            Marque la Casilla en cada campo para Imprimir el campo en el Reporte
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="trh">
                            Plantilla SE
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PanelPrincipalSE" runat="server" Visible="True">
                                <table style="width: 100%;">
                                    <tr class="tr1">
                                        <td>
                                            Fabricante
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXFabricanteSE" runat="server"></asp:DropDownList>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkDDXFabricanteSE" runat="server" />
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Modelo:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtModeloSE" runat="server" ReadOnly="True"></asp:TextBox>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkTxtModeloSE" runat="server" />
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Clase:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXClaseSE" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDXClaseSE_SelectedIndexChanged"></asp:DropDownList>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkDDXClaseSE" runat="server" />
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            Tipo:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXTipoSE" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDXTipoSE_SelectedIndexChanged"></asp:DropDownList>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkDDXTipoSE" runat="server" />
                                        </td>
                                        <td>
                                            Precio Total:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPrecioTotalSE" runat="server" ReadOnly="True"></asp:TextBox>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkTxtPrecioTotalSE" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="font-weight: bold; text-align: center" colspan="2">
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
                                            Tranformador
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXMarcaSE" runat="server" CssClass="DropDownList"></asp:DropDownList>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkDDXMarcaSE" runat="server" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXCapacidadSE" runat="server" CssClass="DropDownList"></asp:DropDownList>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkDDXCapacidadSE" runat="server" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDXRelTransSE" runat="server" CssClass="DropDownList"></asp:DropDownList>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkDDXRelTransSE" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPrecioSE" runat="server" ReadOnly="True"></asp:TextBox>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkTxtPrecioSE" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblDDXMarcaFusibleSE" runat="server" Text="Fusible de Subestación en Media Tensión"></asp:Label>
                                            
                                        </td>
                                        <td colspan="3">
                                            
                                        </td>
                                        <%--<td>
                                            
                                            </td>
                                        <td>
                                            
                                            </td>--%>
                                        <td>
                                            <asp:TextBox ID="TxtPrecioFusibleSE" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtPrecioFusibleSE" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            Precio Total Modulo de Tranformador
                                        </td>
                                        <td colspan="4">
                                            <asp:Label ID="LblPrecioModuloTransformador" runat="server" Font-Underline="True" ForeColor="#CC0000" Font-Bold="True">Precio Total Modulo Transformador</asp:Label>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkLblPrecioModuloTransformador" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="font-weight: bold; text-align: center" colspan="2">
                            Modulo de Media Tensión
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PanelModuloMediaTensionSE" runat="server" Visible="True">
                                <table style="width: 100%; empty-cells: hide;">
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
                                           <asp:TextBox ID="TxtPrecioConectores" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtPrecioConectores" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LblAisladores" runat="server" Text="Aisladores Tensión"></asp:Label>                                        
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtAisladores" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtAisladores" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblAisladoresSoporte" runat="server" Text="Aisladores Soporte"></asp:Label>
                                            
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtAisladoresSoporte" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtAisladoresSoporte" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LblCableGuia" runat="server" Text="Cable Guía de Conexión de Línea a Apartarrayos"></asp:Label>                                          
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCableGuia" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtCableGuia" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                           <asp:Label ID="LblCableGuiaCorto" runat="server" Text="Cable Guía de Conexión de Línea a Corta circuito"></asp:Label>
                                             
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCableGuiaCorto" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtCableGuiaCorto" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LblCableGuiaTransformador" runat="server" Text="Cable Guía de Conexión de Línea a Transformador"></asp:Label>
                                            
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCableGuiaTransformador" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtCableGuiaTransformador" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblApartarayo" runat="server" Text="Apartarrayo"></asp:Label>
                                            
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtApartarayo" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtApartarayo" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LblCortaCircuito" runat="server" Text="Corta circuito"></asp:Label>
                                            
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCortaCircuito" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtCortaCircuito" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblFusible" runat="server" Text="Fusible"></asp:Label>
                                            
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtFusible" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtFusible" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LabelCableGuia" runat="server" Text="Cable Guía de Conexión de Línea a Terminales de Acometida"></asp:Label> 
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCableGuiaConexion" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtCableGuiaConexion" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LableCableConexion" runat="server" Text="Cable de conexión de media tensión subterráneo (acometida)"></asp:Label> 
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtCableConexionSubterraneo" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtCableConexionSubterraneo" runat="server" />
                                        </td>
                                    </tr>
                                    
                                    <tr class="tr2">
                                        <td>
                                            <asp:Label ID="LblEmpalmes" runat="server" Text="Empalmes para cables de Media Tensión"></asp:Label> 
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtEmpalmes" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtEmpalmes" runat="server" />
                                        </td>
                                    </tr>
                                    
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblCableExtremos" runat="server" Text="Cable de Conexión Subterranéo para ambos extremos"></asp:Label> 
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtExtremos" runat="server" ReadOnly="True"></asp:TextBox>
                                            <asp:CheckBox ID="ChkTxtExtremos" runat="server" />
                                        </td>
                                    </tr>
                                    
                                    <tr class="tr2">
                                        <td>
                                            Precio Total Modulo de Media Tensión
                                        </td>
                                        <td>
                                            <asp:Label ID="LblPrecioModuloMediaTension" runat="server" Font-Underline="True" ForeColor="#CC0000" Font-Bold="True">Precio Total Modulo Media Tensión</asp:Label>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkLblPrecioModuloMediaTension" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="font-weight: bold; text-align: center" colspan="2">
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
                                            <asp:TextBox ID="TxtCableConexionTR" runat="server" ReadOnly="True"></asp:TextBox>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkTxtCableConexionTR" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td style="font-weight: bold; text-align: center" colspan="2">
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
                                           <asp:TextBox ID="TxtHerrajesInstalacion" runat="server" ReadOnly="True"></asp:TextBox>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkTxtHerrajesInstalacion" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            <asp:Label ID="LblPoste" runat="server" Text="Poste"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtPoste" runat="server" ReadOnly="True"></asp:TextBox>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkTxtPoste" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            Sistema(s) de Tierra
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtSistemTierra" runat="server" ReadOnly="True"></asp:TextBox>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkTxtSistemTierra" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr1">
                                        <td>
                                            Cable de conexión a sistema a tierra
                                        </td>
                                        <td>
                                           <asp:TextBox ID="TxtConexionTierra" runat="server" ReadOnly="True"></asp:TextBox>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkTxtConexionTierra" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tr2">
                                        <td>
                                            Precio Total Módulo Accesorios y Protecciones
                                        </td>
                                        <td>
                                            <asp:Label ID="LblPrecioModuloAccesorios" runat="server" Font-Underline="True" ForeColor="#CC0000" Font-Bold="True">Precio Total Modulo Accesorios</asp:Label>
                                            &nbsp;
                                            <asp:CheckBox ID="ChkLblPrecioModuloAccesorios" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td style="font-weight: bold; text-align: center; width: 90%;" colspan="2">
                            Módulo Conceptos Adicionales</td>
                    </tr>
                    <tr class="tr2" style="width: 90%;">
                        <td>
                            Obra Civil (excavaciones, ductos para cable en media tensión)
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox2" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr1" style="width: 90%;">
                        <td>
                            Obra Civil(excavaciones, ductos para cable en baja tensión)
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox3" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr2" style="width: 90%;">
                        <td>
                            Mano de obra
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox4" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr1" style="width: 90%;">
                        <td>
                            Trámites ante CFE
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox5" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr2" style="width: 90%;">
                        <td>
                            Libranza
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox6" runat="server" />
                        </td>
                    </tr>
                    <tr class="tr1" style="width: 90%;">
                        <td>
                            <asp:Label ID="LblVerificacionUVIE" runat="server" Text="Verificación UVIE"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox7" runat="server" />
                        </td>
                    </tr>
                </table>
                
            </div>

            <asp:Panel ID="PanelGridCustomizables" runat="server" Visible="False">
                <table style="width: 80%;" id="tablaCustomizablesPlantilla" runat="server">
                    <tr>
                        <td colspan ="2" class="trh">
                           Campos Personalizados 
                        </td>
                    </tr>
                    <tr style="text-align: right">
                        <td>
                            <asp:Button ID="BtnAgregaCampoGrid" runat="server" Text="Agregar" OnClick="BtnAgregaCampoGrid_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="2">
                            <asp:GridView ID="grdCamposCustomizables" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                AllowPaging="True" PageSize="10" DataKeyNames="CveCampo, CveAgregarReporte" OnRowCreated="grdCamposCustomizables_RowCreated">
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                                <PagerSettings Visible="False" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="CveCampo" Visible="False"></asp:BoundField>
                                    <asp:BoundField DataField="DxNombreCampo" HeaderText="Campo"></asp:BoundField>
                                    <asp:BoundField DataField="DxDescripcionCampo" HeaderText="Descripción"></asp:BoundField>
                                    <asp:BoundField DataField="DxTipo" HeaderText="Tipo"></asp:BoundField>
                                    <asp:BoundField DataField="CveAgregarReporte" Visible="False"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Imprimir en Reporte" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckbSelect" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="BtnImgEliminar" runat="server" ImageUrl="~/CentralModule/images/eliminar-icono.png" OnClick="BtnImgEliminar_Click" 
                                                OnClientClick="return confirm('Confirmar que Desea Eliminar el registro');"/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <webdiyer:AspNetPager ID="AspNetPager2" CssClass="pagerDRUPAL" runat="server" PageSize="10"
                                AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                                PageIndexBoxType="DropDownList" CustomInfoHTML="Página:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                                UrlPaging="false"
                                FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                                CurrentPageButtonClass="cpb" OnPageChanged="AspNetPager2_PageChanged">
                            </webdiyer:AspNetPager>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    
                </table>
            </asp:Panel>
            
            <table style="width: 80%;" runat="server">
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="BtnGuardarPlantilla" runat="server" Text="Guardar" OnClick="BtnGuardarPlantilla_Click" Visible="False" />
                    </td>
                </tr>
            </table>

            <asp:Panel ID="PanelCamposCustomizables" runat="server" Visible="False">
                <table>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>

                    <tr>                        
                        <td>
                            <asp:Button ID="BtnBuscarCampo" runat="server" Text="Campo Existente" OnClick="BtnBuscarCampo_Click" />                            
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RBlstTipoCampo" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="Text">Texto</asp:ListItem>
                                <asp:ListItem Value="Cat">Catalogo</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:Button ID="BtnCreaNuevo" runat="server" Text="Crear Nuevo" OnClick="BtnCreaNuevo_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="Button4" runat="server" Text="Regresar" OnClick="Button4_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
            <asp:Panel ID="PanelBusquedaCamposCustomizables" runat="server" Visible="False">
                <div id="container">
                    <fieldset class="login" style="border: thin solid #6699FF; border-color: #6699FF;">
                        <legend style="font-size: 14px;">Elige el criterio de Búsqueda</legend>      
                        <table>
                            <tr>
                                <td>
                                    Tipo:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDXTipoCampo" runat="server" CssClass="DropDownList" Width="200px">
                                        <asp:ListItem Selected="True">Seleccione...</asp:ListItem>
                                        <asp:ListItem Value="Text">Texto</asp:ListItem>
                                        <asp:ListItem Value="Cat">Catalogo</asp:ListItem>
                                        <asp:ListItem Value="Tod">Todos</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    Nombre Campo:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtNombreCampo" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="BtnBusquedaCampos" runat="server" Text="Buscar" OnClick="BtnBusquedaCampos_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <asp:GridView runat="server" ID="grdBuscaCamposCustomizables" AutoGenerateColumns="False" CssClass="GridViewStyle"
                            AllowPaging="True" PageSize="10" DataKeyNames="CveCampo" >
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                                    <PagerSettings Visible="False" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />

                        <Columns>
                            <asp:BoundField DataField="CveCampo" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="DxNombreCampo" HeaderText="Campo"></asp:BoundField>
                            <asp:BoundField DataField="DxDescripcionCampo" HeaderText="Descripción"></asp:BoundField>
                            <asp:BoundField DataField="DxTipo" HeaderText="Tipo"></asp:BoundField>
                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbSelect" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="10"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Página:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="false"
                        FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                        CurrentPageButtonClass="cpb" OnPageChanged="AspNetPager_PageChanged">
                    </webdiyer:AspNetPager>

                    <table width="100%">
                        <tr>
                            <td width="100%" align="right">
                                <asp:Button ID="BtnSelectCustom" runat="server" Text="Detalles" OnClick="BtnSelectCustom_Click" />
                            </td>
                        </tr>
                         <tr>
                            <td align="center">
                                <asp:Button ID="Button3" runat="server" Text="Regresar" OnClick="Button3_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="PanelCampoCatalogo" runat="server" Visible="False" Width="50%">
                <div style="border-style: solid; border-width: 1px">
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center" class="trh">
                            Crear / Editar Catalogo
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Clave Catalogo:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtCveCatalogo" runat="server" ReadOnly="True" Enabled="False" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Nombre Catalogo:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtNomCatalogo" runat="server" CssClass="TextBox" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNomCatalogo" 
                                ErrorMessage="Se debe capturar el Nombre del Catalogo"
                                ValidationGroup="Catalogo" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator1"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="FTETxtMontoChatarrizacion" 
                                runat="server" 
                                TargetControlID="TxtNomCatalogo" 
                                FilterType="Custom" 
                                FilterMode="ValidChars" 
                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789. " />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Descripción:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtDescCatalogo" runat="server" CssClass="TextBox" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtDescCatalogo" 
                                ErrorMessage="Se debe capturar la Descripcion del Catalogo"
                                ValidationGroup="Catalogo" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator2"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                runat="server" 
                                TargetControlID="TxtDescCatalogo" 
                                FilterType="Custom" 
                                FilterMode="ValidChars" 
                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789. " />
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Mensaje Emergente:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtToolTip" runat="server" CssClass="TextBox" MaxLength="50"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtToolTip" 
                                ErrorMessage="Se debe capturar el Tooltip del Catalogo"
                                ValidationGroup="Catalogo" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator3"
                                InitialValue="">
                             </asp:RequiredFieldValidator>--%>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                runat="server" 
                                TargetControlID="TxtToolTip" 
                                FilterType="Custom" 
                                FilterMode="ValidChars" 
                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789. " />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Obligatorio:
                        </td>
                        <td style="text-align: center">
                            <asp:RadioButtonList ID="RbLstObligatorio" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" class="trh">
                            Valores Catalogo
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr id="trValores" runat="server">
                        <td>
                            Descripción Valor:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtValorCatalogo" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="BtnAgregarValor" runat="server" Text="Agregar" OnClick="BtnAgregarValor_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" class="trh">
                            Valores
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="2">
                            <asp:BulletedList ID="BLstValoresCatalogo" runat="server" BackColor="#CCCCFF"></asp:BulletedList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="RadioListValoresCat" runat="server">
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImgEliminar" runat="server" ImageUrl="~/SupplierModule/images/delete.gif" OnClick="ImgEliminar_Click" Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TxtNuevoNombreCat" runat="server" Visible="False"></asp:TextBox>
                                        <asp:Button ID="BtnActualizaCat" runat="server" Text="Cambiar" OnClick="BtnActualizaCat_Click" Visible="False" />
                                    </td>
                                </tr>
                            </table>                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="Catalogo" runat="server" CssClass="failureNotification" 
                                 ValidationGroup="Catalogo" Font-Size="Small" HeaderText="Resumén Captura:"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="BtnGuardarCatalogo" runat="server" Text="Guardar" OnClick="BtnGuardarCatalogo_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="BtnAgregarAPlantilla" runat="server" Text="Agregar a Plantilla" Visible="False" OnClick="BtnAgregarAPlantilla_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br/>
                        </td>
                        
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="Button2" runat="server" Text="Regresar" OnClick="Button2_Click" />
                        </td>
                    </tr>
                </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="PanelCampoTexto" runat="server" Visible="False" Width="40%">
                <div style="border-style: solid; border-width: 1px">
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center" class="trh">
                            Crear / Editar Campo Texto
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Clave Campo:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtClaveCampoTexto" runat="server" ReadOnly="True" Enabled="False" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Nombre Campo:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtNombreCampoTexto" runat="server" CssClass="TextBox" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNombreCampoTexto" 
                                ErrorMessage="Se debe capturar el Nombre del Campo Texto"
                                ValidationGroup="Texto" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator4"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                runat="server" 
                                TargetControlID="TxtNombreCampoTexto" 
                                FilterType="Custom" 
                                FilterMode="ValidChars" 
                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789. " />
                            
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Descripción:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtDescripcionCampoTexto" runat="server" CssClass="TextBox" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtDescripcionCampoTexto" 
                                ErrorMessage="Se debe capturar la Descripción del Campo Texto"
                                ValidationGroup="Texto" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator5"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                runat="server" 
                                TargetControlID="TxtDescripcionCampoTexto" 
                                FilterType="Custom" 
                                FilterMode="ValidChars" 
                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789. " />
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Mensaje Emergente:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtToolTipTexto" runat="server" CssClass="TextBox" MaxLength="50"></asp:TextBox>
                           <%-- <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtToolTipTexto" 
                                ErrorMessage="Se debe capturar el Tooltip del Campo Texto"
                                ValidationGroup="Texto" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator6"
                                InitialValue="">
                             </asp:RequiredFieldValidator>--%>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                runat="server" 
                                TargetControlID="TxtToolTipTexto" 
                                FilterType="Custom" 
                                FilterMode="ValidChars" 
                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789. " />
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Obligatorio:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RBLObligatorioTexto" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification" 
                                 ValidationGroup="Texto" Font-Size="Small" HeaderText="Resumén Captura:"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="BtnGuardarCampoTexto" runat="server" Text="Guardar" OnClick="BtnGuardarCampoTexto_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="BtnAgregarAPlantilla2" runat="server" Text="Agregar a Plantilla" Visible="False" OnClick="BtnAgregarAPlantilla2_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br/>
                        </td>
                        
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="Button1" runat="server" Text="Regresar" OnClick="Button1_Click" />
                        </td>
                    </tr>
                </table>
                </div>
            </asp:Panel>

        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
