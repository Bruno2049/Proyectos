<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Equipos.aspx.cs" Inherits="PAEEEM.SolicitudCredito.Equipos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />
    <script src="~/Funciones.js" type="text/javascript"></script>

    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="Stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .table {
            border: 2px solid rgb(0, 149, 159);
            border-spacing: 0 0;
            text-align: center;
            font-family: Arial, Geneva, sans-serif;
        }

        .Label {
            color: #333333;
            font-size: 16px;
        }

        .Label_1 {
            color: Maroon;
            font-size: 14pt;
        }

        .DropDownList {
            font-size: 18pt;
            margin-left: 37px;
        }

        .Button {
            width: 160px;
            margin-left: 400%;
        }

        #dtFecha {
            float: right;
        }

        #DropDownGroup {
            float: left;
        }

        </style>
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
        .auto-style2 {
            width: 210px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:updatepanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
           
            <div style="width:100%">
                <h2>
                    <asp:Label ID="Label1" runat="server" Font-Size="Large" Font-Bold="True" Text="INFORMACIÓN DE EQUIPOS" />
                </h2>
                <hr class="rule" />
                <fieldset class="legend_info">
                    <table align="center">
                        <tr>
                            <td>
                                <asp:HyperLink runat="server" ID="HPL_RegInfol" Text="Regresar a información de solicitud" NavigateUrl="#"></asp:HyperLink>
                                
                            </td>
                            <td>|                            
                            </td>
                            <td>
                                <asp:HyperLink runat="server" ID="HPL_HisMod" Text="Historial de Modificaciones" NavigateUrl="#"></asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </fieldset>

                <table>
                    <tr>
                        <td style="font-style: inherit; text-align: center;  border: solid;"><asp:Label runat="server" Font-Bold="True" Text="EQUIPOS DE BAJA EFICIENCIA"/> 
                        </td>
                        <td class="auto-style2"></td>
                        <td>
                            <asp:Label Text="Foto Fachada del Negocio"  runat="server"/> 
                            </td>
                         <td></td>
                        <td>
                            <asp:ImageButton runat="server" ID="verEquipoNuevo" ImageUrl="~/CentralModule/images/visualizar.png" OnClick="verFotoFachada_Click" />
                        </td>
                    </tr>
                </table>
                <br />

                <asp:GridView ID="DGV_InfEquBajaEfic" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="20" DataKeyNames="No_Credito,IdCreditoSustitucion,idTipoFoto,IdConsecutivo" OnSelectedIndexChanged="DGV_InfEquBajaEfic_SelectedIndexChanged">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EditRowStyle CssClass="GridViewEditStyle" />
                    <Columns>
                        <asp:BoundField HeaderText="Grupo" DataField="Dx_Grupo" />
                        <asp:BoundField HeaderText="Tecnología" DataField="Dx_Tecnologia" />
                        <asp:BoundField HeaderText="Producto" DataField="Dx_Tipo_Producto" />
                        <asp:BoundField HeaderText="Capacidad/Sistema" DataField="CapSis" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Unidad" DataField="Dx_Unidad" />
                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-HorizontalAlign="Right" Visible="False"/>
                        <asp:BoundField HeaderText="Marca" DataField="Marca" />
                        <asp:BoundField HeaderText="Modelo" DataField="Modelo" />
                        <asp:BoundField HeaderText="Color" DataField="Color" />
                        <asp:BoundField HeaderText="Años de Antigüedad" DataField="Antiguedad" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Pre-Folio" DataField="PreFolio" />
                        <asp:BoundField HeaderText="Folio" DataField="Folio" />
                        <asp:BoundField HeaderText="Fecha de Ingreso" DataField="FechaIngr" />
                        <asp:BoundField HeaderText="CAyD Comercial" DataField="CAyD" />
                        <asp:BoundField HeaderText="Razón Social" DataField="RazonSocial" />
                        <asp:BoundField HeaderText="Zona" DataField="Zona" />
                        <asp:BoundField HeaderText="Región" DataField="Region" />
                         <asp:BoundField HeaderText="IdCreditoSustitucion" DataField="IdCreditoSustitucion" Visible="False"/>
                         <asp:BoundField HeaderText="idTipoFoto" DataField="idTipoFoto" Visible="False"/>
                         <asp:BoundField HeaderText="IdConsecutivo" DataField="IdConsecutivo" Visible="False"/>
                        <asp:TemplateField ItemStyle-Width="25%" HeaderText="Imagen" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:ImageButton runat="server" ID="verEquipoViejo" ImageUrl="~/CentralModule/images/visualizar.png" OnClick="verFotoEqBajaEfc_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <br />
                <p />
                <table>
                    <tr>
                        <td style="text-align: center; ">
                            <b>  <asp:Label ID="LBL_Nota1" Text="Nota: En caso de aparecer la columna del folio en blanco significa que no hay ingreso al CAyD" runat="server"/></b>
                        </td>
                    </tr>
                </table>
                <br />

                <table>
                    <tr>
                        <td style="font-style: inherit; text-align: center;  border: solid;">
                            <asp:Label runat="server" Font-Bold="True" Text="EQUIPOS DE ALTA EFICIENCIA"/>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="DGV_InfEquAltaEfic" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="20" DataKeyNames="No_Credito,IdCreditoProducto,idTipoFoto,IdConsecutivo"  OnSelectedIndexChanged="DGV_InfEquAltaEfic_SelectedIndexChanged">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" HorizontalAlign="Right" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:BoundField HeaderText="Grupo" DataField="Dx_Grupo" />
                        <asp:BoundField HeaderText="Tecnología" DataField="Dx_Tecnologia" />
                        <asp:BoundField HeaderText="Fabricante" DataField="Fabricante" />
                        <asp:BoundField HeaderText="Marca" DataField="Dx_Marca" />
                        <asp:BoundField HeaderText="Modelo" DataField="Dx_Modelo" />
                        <asp:BoundField HeaderText="Capacidad" DataField="No_Capacidad" />
                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />                        
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Precio Distribuidor" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="LBL_PrecioDistribuidor" runat="server" Text='<%# String.Format("{0:C2}",DataBinder.Eval(Container.DataItem,"Precio_Distribuidor"))%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Precio Unitario" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="LBL_PrecioUnitario" runat="server" Text='<%# String.Format("{0:C2}",DataBinder.Eval(Container.DataItem,"Precio_Unitario"))%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Importe sin IVA" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="LBL_Importe_Toal_Sin_Iva" runat="server" Text='<%# String.Format("{0:C2}",DataBinder.Eval(Container.DataItem,"Importe_Total_Sin_IVA"))%>' />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Gastos de Instalación" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="LBL_Gasto_Instalacion" runat="server" Text='<%# String.Format("{0:C2}",DataBinder.Eval(Container.DataItem,"Gasto_Instalacion"))%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField HeaderText="Costo Acopio y Destrucion" DataField="MontoChatarrizacion" />--%>
                        
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Monto Incentivo" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="LBL_MontoIncentivo" runat="server" Text='<%# String.Format("{0:C2}",DataBinder.Eval(Container.DataItem,"MontoIncentivo"))%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Tarifa Origen"  DataField="TarifaOrigen" />
                        <asp:BoundField HeaderText="Tarifa Futura" DataField="TarifaFutura" />
                        
                         <asp:BoundField HeaderText="IdCreditoProducto" DataField="IdCreditoProducto" Visible="False" />
                         <asp:BoundField HeaderText="idTipoFoto" DataField="idTipoFoto" Visible="False"/>
                         <asp:BoundField HeaderText="IdConsecutivo" DataField="IdConsecutivo" Visible="False"/>
                        <asp:TemplateField ItemStyle-Width="1%" HeaderText="Imagen" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                    <asp:ImageButton runat="server" ID="verEquipoViejo" ImageUrl="~/CentralModule/images/visualizar.png" OnClick="verFotoEqAltaEfc_Click" />
 
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <br />
            </div>
            <div style="text-align: center;">
            <table   style="align-content:center; text-align: center;" align="center">
                <tr>
                    <td style="font-style: inherit; text-align: center;  border: solid;">
                        <asp:Label runat="server" Font-Bold="True" Text="Presupuestos"/>
                    </td>
                    <td style="width:50px;"></td>
                    <td style="font-style: inherit; text-align: center; border: solid;">
                        <asp:Label runat="server" Font-Bold="True" Text="Datos Energéticos"/>
                    </td>
                    <td style="width:50px;"></td>
                    <td style="font-style: inherit; text-align: center; border: solid;">
                        <asp:Label runat="server" Font-Bold="True" Text="Concepto"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="border: solid; ">
                            <tr>
                                <td style="border: 1px solid; text-align: center" ><asp:Label runat="server" Font-Bold="True" Text="Concepto"/>    
                                </td>
                                <td style="border: 1px  solid; text-align: center"><asp:Label runat="server" Font-Bold="True" Text="Precio"/>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px  solid;"><asp:Label runat="server" Text="Costo Equipos"/>
                                </td>
                                <td style=" text-align: right; border: 1px solid; ">
                                    <asp:Label ID="LBL_CostoEquipo" Text="" runat="server"/>
                                </td> 
                            </tr>
                            <tr>
                                <td  style="border: 1px solid;"><asp:Label runat="server"  Text="Gastos de instalación"/>
                                </td>
                                <td style=" text-align: right;border: 1px solid;  ">
                                     <asp:Label ID="LBL_GastosInstalacion" Text="" runat="server" style=" text-align: right;"/>
                                </td>
                            </tr>
                            <tr>
                                <td  style="border: 1px;border: 1px solid; "><asp:Label runat="server"  Text="IVA"/>
                                </td>
                                <td style=" text-align: right; border: 1px solid;">
                                    <asp:Label ID="LBL_IVA" Text="" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td  style="border: 1px solid;"><asp:Label runat="server"  Text="SubTotal"/>
                                </td>
                                <td style=" text-align: right; border: 1px solid;">
                                    <asp:Label ID="LBL_SubTotal" Text="" runat="server" align="right"/>
                                </td>
                            </tr>
                            <tr>
                                <td  style="border: 1px solid;"><asp:Label runat="server"  Text="Incentivo"/>
                                </td>
                                <td style=" text-align: right; border: 1px solid; ">
                                    <asp:Label ID="LBL_Incentivo" Text="" runat="server" align="right"/>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid;"><asp:Label runat="server"  Text="Costo Acopio y Destrucción"/>
                                </td>
                                <td style=" text-align: right; border: 1px solid; ">
                                    <asp:Label ID="LBL_CostAcopDes" Text="" runat="server"/>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid;"><asp:Label runat="server"  Text="Descuento"/>
                                </td>
                                <td style=" text-align: right; border: 1px solid;">
                                    <asp:Label ID="LBL_Descuento" Text="" runat="server" align="right"/>
                                </td>
                            </tr>
                            <tr>
                        <td style="border: 1px solid;"><asp:Label runat="server"  Text="Total"/>
                                </td>
                                <td style=" text-align: right; border: 1px solid;">
                                    <asp:Label ID="LBL_TOTAL" Text="" runat="server" align="right"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                    <td style="vertical-align: top;">
                        <table style="border: solid; ">
                            <tr>
                                <td style="border: 1px solid;"><asp:Label runat="server"  Text="Consumo Promedio"/></td>
                                <td style=" text-align: right; border: 1px solid;">
                                    <asp:Label runat="server" ID="LBL_ConsumoPromedio"/></td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid;"><asp:Label runat="server"  Text="Demanda Promedio"/></td>
                                <td style=" text-align: right;border: 1px solid">
                                    <asp:Label runat="server" ID="LBL_DemandaPromedio" align="right"/></td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid;"><asp:Label runat="server" Text="Factor Potencia Promedio"/></td>
                                <td style=" text-align: right; border: 1px solid">
                                    <asp:Label runat="server" ID="LBL_FacPotProm" align="right"/></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                    <td style="vertical-align: top;" id="Conceptos">
                        <table style="border: solid;">
                            <tr>
                                <td style="border: 1px solid;"><asp:Label runat="server"  Text="Gastos Mensuales"/></td>
                                <td style=" text-align: right; border: 1px solid;">
                                    <asp:Label ID="LBL_GastosMen" Text="" runat="server" align="right"/></td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid;"><asp:Label runat="server"  Text="Ventas Mensuales"/></td>
                                <td style=" text-align: right; border: 1px solid;">
                                    <asp:Label ID="LBL_VentasMen" Text="" runat="server" align="right"/></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </div>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>


