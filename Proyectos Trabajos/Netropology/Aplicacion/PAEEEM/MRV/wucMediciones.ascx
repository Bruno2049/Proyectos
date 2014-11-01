<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucMediciones.ascx.cs" Inherits="PAEEEM.MRV.wucMediciones" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>--%>

<link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
<link href="../Resources/Css/TablaNet.css" rel="stylesheet" type="text/css" />

<div>
    <table>
        <tr>
            <td>
                <br/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblGruposBaja" runat="server" Text="Grupo de Equipo(s) de Baja Eficiencia" Font-Size="Small" />
                &nbsp;
            </td>
            <td>
                <telerik:RadComboBox ID="RadGrupoSolicitud" runat="server"
                            OnSelectedIndexChanged="RadGrupoSolicitud_SelectedIndexChanged" Width="50px" 
                            Skin="Office2010Silver" AutoPostBack="True">
                        </telerik:RadComboBox>
            </td>
            <td>
                <asp:BulletedList ID="BulletedListEquiposBaja" runat="server" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Size="X-Small" Width="200px">
                </asp:BulletedList>
            </td>
            <td>
               
                &nbsp;&nbsp;
            </td>
            <td>
                <asp:Label ID="lblGruposAlta" runat="server" Text="VS Grupo de Equipo(s) de Alta Eficiencia" Font-Size="Small"></asp:Label>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:BulletedList ID="BulletedListEquiposAlta" runat="server" 
                    BorderStyle="Solid" BorderWidth="1px" Font-Size="X-Small" Width="200px">
                </asp:BulletedList>
            </td>            
        </tr>
        <tr>
            <td colspan="7">
                <br/>
            </td>
        </tr>
        <tr>
            <td colspan="7" style="text-align: center">
                <asp:CheckBox ID="ChkTrifasico" runat="server" Text="Trifásico" 
                    AutoPostBack="True" oncheckedchanged="ChkTrifasico_CheckedChanged" 
                    Font-Size="Small" />               
            </td>
        </tr>
        <tr>
            <td colspan="7">
                <br/>
            </td>
        </tr>
    </table>
</div>

<div id="divMedicionDetalle" runat="server">
    <asp:Panel ID="PnlMedicionDetalle" runat="server" Enabled="False">
        <table style="width: 85%">
        <tr>
            <td colspan="4"></td>
            <td colspan="2" class="trh">Ahorros Eq. Ineficiente vs Eq. Eficiente</td>
            <td></td>
        </tr>
        
        <tr class="trh">
            <td style="width: 17%">
                Descripción     
            </td>
            <td style="width: 13%">
                Equipo Ineficiente     
            </td>
            <td style="width: 13%">
                Equipo Eficiente     
            </td>
            <td style="width: 8%">
                Unidades     
            </td>
            <td style="width: 13%">
                Valor     
            </td>
            <td style="width: 13%">
                %     
            </td>
            <td style="width: 19%">
                Observaciones     
            </td>
        </tr>
        
        <tr class="tr11">
            <td style="width: 17%">
                <asp:Label ID="Label1" runat="server" Text="Demanda Máxima" Font-Size="Small" />
            </td>
            <td style="width: 13%; text-align: center">
                <telerik:RadNumericTextBox ID="RadDemandMaxEqIneficiente" runat="server" 
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="width: 13%; text-align: center">
                <telerik:RadNumericTextBox ID="RadDemandMaxEqEficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="width: 8%; text-align: center">
                <asp:Label ID="Label21" runat="server" Text="kW" Font-Size="Small"></asp:Label>
            </td>
            <td style="width: 13%; text-align: center">
                <telerik:RadNumericTextBox ID="RadDemandMaxValor" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="width: 13%; text-align: center">
                <telerik:RadNumericTextBox ID="RadDemandMaxPorcent" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="width: 19%; text-align: center">
                <telerik:RadTextBox ID="RadDemandMaxObs" runat="server"
                    Width="85%" DataType="System.Decimal">
                </telerik:RadTextBox>
            </td>
        </tr>
        
        <tr class="tr11">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Demanda Media" Font-Size="Small" />                
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadDemandMedEqIneficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadDemandMedEqEficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label22" runat="server" Text="kW" Font-Size="Small"></asp:Label>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadDemandMedValor" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadDemandMedPorcent" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadTextBox ID="RadDemandMedObs" runat="server"
                    Width="85%">
                </telerik:RadTextBox>
            </td>
        </tr>
        
        <tr class="tr11">
            <td>
                <asp:Label ID="Label3" runat="server" Text="Consumo medido (24 hr)" Font-Size="Small" />
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadConsumoDiaEqIneficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadConsumoDiaEqEficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label23" runat="server" Text="kWh" Font-Size="Small"></asp:Label>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadConsumoDiaValor" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadConsumoDiaPorcent" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center"|>
                <telerik:RadTextBox ID="RadConsumoDiaObs" runat="server"
                    Width="85%">
                </telerik:RadTextBox>
            </td>
        </tr>
        
        <tr class="tr11">
            <td>
                <asp:Label ID="Label4" runat="server" Text="Consumo anual" Font-Size="Small" />
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadConsumoAnualEqIneficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadConsumoAnualEqEficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label24" runat="server" Text="kWh" Font-Size="Small"></asp:Label>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadConsumoAnualValor" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadCosumoAnualPorcent" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadTextBox ID="RadConsumoAnualObs" runat="server"
                    Width="85%">
                </telerik:RadTextBox>
            </td>
        </tr>
        
        <tr class="tr11">
            <td>
                <asp:Label ID="Label5" runat="server" Text="Factor de Potencia" Font-Size="Small" />
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadFactPotenciaEqIneficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadFactPotenciaEqEficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadFactPotenciaValor" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadFactPotenciaPorcent" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadTextBox ID="RadFactPotenciaObs" runat="server"
                    Width="85%">
                </telerik:RadTextBox>
            </td>
        </tr>
        
        <tr class="tr11">
            <td>
                <asp:Label ID="Label6" runat="server" Text="Tensión de Línea(VL)" Font-Size="Small" />
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadTensionLineaEqIneficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadTensionLineaEqEficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label25" runat="server" Text="Volts" Font-Size="Small"></asp:Label>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadTensionLineaValor" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadTensionLineaPorcent" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadTextBox ID="RadTensionLineaObs" runat="server"
                    Width="85%">
                </telerik:RadTextBox>
            </td>
        </tr>
        
        <tr class="tr11">
            <td>
                <asp:Label ID="Label7" runat="server" Text="Corriente de Línea(IL)" Font-Size="Small" />
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadCorrienteLineaEqIneficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadCorrienteLineaEqEficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label26" runat="server" Text="Amperes" Font-Size="Small"></asp:Label>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadCorrienteLineaValor" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadCorrienteLineaPorcent" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadTextBox ID="RadCorrienteLineaObs" runat="server"
                    Width="85%">
                </telerik:RadTextBox>
            </td>
        </tr>
        
        <tr class="tr11">
            <td>
                <asp:Label ID="Label8" runat="server" Text="Temperatura" Font-Size="Small" />
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadTemperaturaEqIneficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadTemperaturaEqEficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label27" runat="server" Text="C°" Font-Size="Small"></asp:Label>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadTemperaturaValor" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadTemperaturaPorcent" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadTextBox ID="RadTemperaturaObs" runat="server"
                    Width="85%">
                </telerik:RadTextBox>
            </td>
        </tr>
        
        <%--<tr class="tr11">
            <td>
                <asp:Label ID="Label9" runat="server" Text="Voltaje de Línea(VL)" Font-Size="Small" />
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadVoltajeLineaEqIneficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadVoltajeLineaEqEficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label28" runat="server" Text="Volts" Font-Size="Small"></asp:Label>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadVoltajeLineaValor" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadVoltajeLineaPorcent" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadTextBox ID="RadVoltajeLineaObs" runat="server"
                    Width="85%">
                </telerik:RadTextBox>
            </td>
        </tr>--%>
        
        <tr class="tr11">
            <td>
                <asp:Label ID="Label10" runat="server" Text="Flujo Luminoso" Font-Size="Small" />
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadFlujoLuminosoEqIneficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadFlujoLuminosoEqEficiente" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <asp:Label ID="Label29" runat="server" Text="Lm" Font-Size="Small"></asp:Label>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadFlujoLuminosoValor" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadNumericTextBox ID="RadFlujoLuminosoPorcent" runat="server"
                    Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                </telerik:RadNumericTextBox>
            </td>
            <td style="text-align: center">
                <telerik:RadTextBox ID="RadFlujoLuminosoObs" runat="server"
                    Width="85%">
                </telerik:RadTextBox>
            </td>
        </tr>
    </table>
    </asp:Panel>

    <asp:Panel ID="PnlTrifasico" runat="server" Visible="False">
        <table style="width: 85%">
            <tr class="tr11">
                <td style="width: 17%">
                    <asp:Label ID="Label11" runat="server" Text="Tensión La" Font-Size="Small" />
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLaEqIneficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLaEqEficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 8%; text-align: center">
                    <asp:Label ID="Label30" runat="server" Text="Volts" Font-Size="Small"></asp:Label>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLaValor" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLaPorcent" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 19%">
                    <telerik:RadTextBox ID="RadTensionLaObs" runat="server"
                        Width="85%">
                    </telerik:RadTextBox>
                </td>
            </tr>
        
            <tr class="tr11">
                <td style="width: 17%">
                    <asp:Label ID="Label12" runat="server" Text="Tensión Lb" Font-Size="Small" />
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLbEqIneficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLbEqEficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 8%; text-align: center">
                    <asp:Label ID="Label31" runat="server" Text="Volts" Font-Size="Small"></asp:Label>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLbValor" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLbPorcent" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 19%">
                    <telerik:RadTextBox ID="RadTensionLbObs" runat="server"
                        Width="85%">
                    </telerik:RadTextBox>
                </td>
            </tr>
            
            <tr class="tr11">
                <td style="width: 17%">
                    <asp:Label ID="Label13" runat="server" Text="Tensión Lc" Font-Size="Small" />
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLcEqIneficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLcEqEficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 8%; text-align: center">
                    <asp:Label ID="Label32" runat="server" Text="Volts" Font-Size="Small"></asp:Label>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLcValor" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadTensionLcPorcent" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 19%">
                    <telerik:RadTextBox ID="RadTensionLcObs" runat="server"
                        Width="85%">
                    </telerik:RadTextBox>
                </td>
            </tr>
            
            <tr class="tr11">
                <td style="width: 17%">
                    <asp:Label ID="Label14" runat="server" Text="Corriente La" Font-Size="Small" />
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLaEqIneficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLaEqEficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 8%; text-align: center">
                    <asp:Label ID="Label33" runat="server" Text="Amperes" Font-Size="Small"></asp:Label>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLaValor" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLaPorcent" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 19%">
                    <telerik:RadTextBox ID="RadCorrienteLaObs" runat="server"
                        Width="85%">
                    </telerik:RadTextBox>
                </td>
            </tr>
            
            <tr class="tr11">
                <td style="width: 17%">
                    <asp:Label ID="Label15" runat="server" Text="Corriente Lb" Font-Size="Small" />
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLbEqIneficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLbEqEficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 8%; text-align: center">
                    <asp:Label ID="Label34" runat="server" Text="Amperes" Font-Size="Small"></asp:Label>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLbValor" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLbPorcent" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 19%">
                    <telerik:RadTextBox ID="RadCorrienteLbObs" runat="server"
                        Width="85%">
                    </telerik:RadTextBox>
                </td>
            </tr>
            
            <tr class="tr11">
                <td style="width: 17%">
                    <asp:Label ID="Label16" runat="server" Text="Corriente Lc" Font-Size="Small" />
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLcEqIneficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLcEqEficiente" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="width: 8%; text-align: center">
                    <asp:Label ID="Label35" runat="server" Text="Amperes" Font-Size="Small"></asp:Label>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLcValor" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 13%">
                    <telerik:RadNumericTextBox ID="RadCorrienteLcPorcent" runat="server"
                        Width="55%" DataType="System.Decimal">
                <NumberFormat ZeroPattern="n" decimaldigits="4"></NumberFormat>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align: center; width: 15%">
                    <telerik:RadTextBox ID="RadCorrienteLcObs" runat="server"
                        Width="85%">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
<br/><br/>
<div style="width: 100%">
    <telerik:RadGrid ID="rgEquiposMedicion" runat="server" 
                        AutoGenerateColumns="False" 
                        CellSpacing="0" 
                        GridLines="None"                                                       
                        Skin="Office2010Silver" 
						Culture="es-MX" Width="80%" 
        oninsertcommand="rgEquiposMedicion_InsertCommand" 
        onneeddatasource="rgEquiposMedicion_NeedDataSource" Font-Size="Small" 
        ondeletecommand="rgEquiposMedicion_DeleteCommand" 
        onitemdatabound="rgEquiposMedicion_ItemDataBound">
        <ClientSettings EnableRowHoverStyle="true">
            <Selecting CellSelectionMode="None" />
        </ClientSettings>
        <MasterTableView DataKeyNames="IdEquipoMedicion" EditMode="InPlace" NoMasterRecordsText="No hay equipos de Medición"
            CommandItemDisplay="Top" AllowAutomaticUpdates="False">
            <CommandItemSettings ExportToPdfText="Export to PDF" 
                AddNewRecordText="Agregar equipo de Medición" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
            </ExpandCollapseColumn>
            <Columns>                                        
                <telerik:GridEditCommandColumn ButtonType="ImageButton" FilterControlAltText="Filter EditCommandColumn column">
                    <HeaderStyle Width="40" />
                </telerik:GridEditCommandColumn>
                <telerik:GridButtonColumn ConfirmText="¿Desea eliminar el equipo de Medición"
                    ConfirmDialogType="RadWindow" ConfirmTitle="Equipos de Medición" ButtonType="ImageButton"
                    CommandName="Delete" ConfirmDialogHeight="100px" ConfirmDialogWidth="280px" Text="Eliminar">
                    <HeaderStyle Width="40" />
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn DataField="IdEquipoMedicion" FilterControlAltText="Filter colIdEquipoMedicion column" HeaderText="IdEquipoMedicion" UniqueName="IdEquipoMedicion" Visible="False">
                    <EditItemTemplate>
                        <asp:Label ID="lbIdMedicion" runat="server" Text='<%# Eval("IdEquipoMedicion") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "IdEquipoMedicion")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn DataField="EquipoMedicion" FilterControlAltText="Filter colEquipoMedicion column" HeaderText="Equipo de Medición" UniqueName="EquipoMedicion">
                    <EditItemTemplate>
                        <telerik:RadTextBox ID="txtEquipoMedicion" Width="80px" runat="server" Text='<%# Bind("EquipoMedicion") %>' >
                        </telerik:RadTextBox>  
                        <asp:RequiredFieldValidator ID="rfvtxtEquipoMedicion" runat="server" ErrorMessage="*" ValidationGroup="equipoAlta" ControlToValidate="txtEquipoMedicion"></asp:RequiredFieldValidator>                                         
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "EquipoMedicion")%>                                                
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn DataField="Modelo" FilterControlAltText="Filter colModelo column" HeaderText="Modelo" UniqueName="Modelo">
                    <EditItemTemplate>
                        <telerik:RadTextBox ID="txtModelo" runat="server" Width="100px" Text='<%# Bind("Modelo") %>' > 
                        </telerik:RadTextBox>  
                        <asp:RequiredFieldValidator ID="rfvModelo" runat="server" ErrorMessage="*" ValidationGroup="equipoAlta" ControlToValidate="txtModelo"></asp:RequiredFieldValidator>                                                                                         
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "Modelo")%>                                                
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn DataField="Marca" FilterControlAltText="Filter colMarca column" HeaderText="Marca" UniqueName="Marca">
                    <EditItemTemplate>
                        <telerik:RadTextBox ID="txtMarca" runat="server" Width="100px" Text='<%# Bind("Marca") %>' > 
                        </telerik:RadTextBox>  
                        <asp:RequiredFieldValidator ID="rfvMarca" runat="server" ErrorMessage="*" ValidationGroup="equipoAlta" ControlToValidate="txtMarca"></asp:RequiredFieldValidator>                                                                                         
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "Marca")%>                                                
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn DataField="FechaUltimaCalibracion" FilterControlAltText="Filter FechaUltimaCalibracion column" 
                HeaderText="Fecha última calibración" UniqueName="FechaUltimaCalibracion">
					<EditItemTemplate>
						<telerik:RadDatePicker ID="FechaUltimaCalibracionRadDatePicker" runat="server" 
							DbSelectedDate='<%# Bind("FechaUltimaCalibracion") %>'>
						</telerik:RadDatePicker>
						<asp:RequiredFieldValidator ID="rfvFechaUltimaCalibracionRadDatePicker" runat="server" ClientIDMode="Static"
											ControlToValidate="FechaUltimaCalibracionRadDatePicker" 
											ErrorMessage="*" 
											ForeColor="Red" InitialValue="" />
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="FechaUltimaCalibracionRadDatePickerLabel" runat="server" Text='<%# Eval("FechaUltimaCalibracion","{0:dd/MM/yyyy}") %>'></asp:Label>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ValidationSettings EnableValidation="true" ValidationGroup="equipoAlta"></ValidationSettings>
        <FilterMenu EnableImageSprites="False">
        </FilterMenu>
    </telerik:RadGrid>
</div>
<br/><br/>
<div>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label18" runat="server" Text="Fecha de medición ex ante" Font-Size="Small" />
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <telerik:RadDatePicker ID="RadTxtFechaMedicionExAnte" runat="server" >
				</telerik:RadDatePicker>
            </td>
            <td>
                
            </td>
            <td rowspan="4">
                <table>
                    <tr>
                        <td colspan="2" style="width: 280px">
                            <telerik:RadAsyncUpload runat="server" id="UploadedArchivoMedicion" 
                                MaxFileSize="10000000"
                                Style="margin-left: 34px"
                                Localization-Select="Examinar" Width="80px"
                                OnClientFilesUploaded="postackControl" 
                                onfileuploaded="UploadedArchivoMedicion_FileUploaded" 
                                onclientfileuploadfailed="OnClientValidationFailedUploadArchivo" 
                                onclientvalidationfailed="OnClientValidationFailedUploadArchivo">
                                <FileFilters>
                                    <telerik:FileFilter Description="Excel(xls;xlsx)" Extensions="xls,xlsx" />
                                </FileFilters>
                                <Localization Select="Examinar" />
                            </telerik:RadAsyncUpload>
                        </td>
                        <td>
                            <asp:Image ID="imgOkCuestionario" runat="server" ImageUrl="~/CentralModule/images/icono_correcto.png" Height="25px" Width="27px" Visible="False" />
                            <asp:ImageButton runat="server" ID="verEquipoViejo" 
                                ImageUrl="~/CentralModule/images/visualizar.png" 
                                onclientclick="OnClientClicking;poponload();"/>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label17" runat="server" Text="Fecha de medición ex post" Font-Size="Small" />
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <telerik:RadDatePicker ID="RadTxtFechaMedicionExPost" runat="server" >
				</telerik:RadDatePicker>                
            </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label19" runat="server" Text="Nombre de quien realizó la medición" Font-Size="Small" />&nbsp;&nbsp;&nbsp;
            </td>
            <td colspan="2">
                <telerik:RadComboBox ID="RadCmbNombreMedicion" runat="server" Width="350px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label20" runat="server" Text="Comentarios Generales" Font-Size="Small" />
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;
            </td>
            <td colspan="2">
                <telerik:RadTextBox ID="RadTxtCometGenerales" runat="server" Height="120px" 
                    TextMode="MultiLine" Width="350px">
                </telerik:RadTextBox>
            </td>            
        </tr>
    </table>    
</div>
<div>
    <table style="width: 100%">
        <tr>
            <td>
                <br/>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 38%">               
                <telerik:RadButton ID="RadBtnSalir" runat="server" Text="Salir" 
                    onclick="RadBtnSalir_Click" OnClientClicking="OnClientClicking">
                </telerik:RadButton>
            </td>
            <td style="width: 24%; text-align: center">
                <telerik:RadButton ID="RadBtnGuardar" runat="server" Text="Guardar" 
                    onclick="RadBtnGuardar_Click" OnClientClicking="OnClientClicking">
                </telerik:RadButton>
            </td>
            <td style="text-align: left; width: 38%">               
                <telerik:RadButton ID="RadBtnFinalizar" runat="server" Text="Finalizar" 
                    onclick="RadBtnFinalizar_Click" OnClientClicking="OnClientClicking">
                </telerik:RadButton>
            </td>
        </tr>
    </table>
</div>
<div style="display: none">
    <asp:Button ID="btnRefresh2" runat="server" Text="Button" 
        onclick="btnRefresh2_Click" />
    <asp:Button ID="hidBtnFinaliza" runat="server" Text="Button" 
        onclick="hidBtnFinaliza_Click" />
    <asp:HiddenField ID="HidActualiza2Ok" runat="server" />
    <asp:HiddenField ID="HidstateLoad2" runat="server" />
    <asp:HiddenField ID="HidIdMedicion" runat="server" />
</div>
<telerik:RadWindowManager ID="rwmVentana" runat="server" Skin="Office2010Silver">                                  
</telerik:RadWindowManager>  
