<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCapturaComplementariaEquiposAlta.ascx.cs" Inherits="PAEEEM.SupplierModule.Controls.wucCapturaComplementariaEquiposAlta" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<style type="text/css">
        .auto-style1 {
            width: 238px;
        }
         .Button_1
        {
            width: 261px;
        }
        .etiqueta {
            Width: 150px;
            color: rgb(102, 102, 102);
            font-size: 11PX;
        }

        .auto-style2 {
            width: 138px;
        }

        .tr1 {
            background-color: #99CCFF;
            text-align: center;
        }

        .tr2 {
            background-color: #E1F0FF;
            text-align: center;
        }

        .auto-style4 {
            width: 113px;
        }

        .auto-style5 {
            width: 10px;
        }
        .Button_1 {}
        .auto-style6 {
            width: 168px;
        }
        .auto-style10 {
            width: 82px;
        }
        .auto-style11 {
            width: 117px;
        }
    </style>
<script type="text/javascript">

    var pre = "wucInformacionComplementaria1_";

    function CalculaHorasTrabajadasDia(entrada, salida) {
        var horasTrabajadas;

        if ((entrada == 0.00) || (salida == 0.00)) {
            horasTrabajadas = 0.00;
        }
        else {
            if (entrada > salida) {
                var horasEntrada = 24 - entrada;
                var horasSalida = 0.00 + salida;

                horasTrabajadas = horasEntrada + horasSalida;
            }

            else {
                horasTrabajadas = salida - entrada;
            }
        }


        return horasTrabajadas;
    }

    function CalculaTotalHorasTrabajadas() {
        var totalHorasTrabajadas = 0.00;
        var entrada;
        var salida;
        var lunes = document.getElementById(pre + 'ChkLunes');
        var martes = document.getElementById(pre + 'ChkMartes');
        var miercoles = document.getElementById(pre + 'ChkMiercoles');
        var jueves = document.getElementById(pre + 'ChkJueves');
        var viernes = document.getElementById(pre + 'ChkViernes');
        var sabado = document.getElementById(pre + 'ChkSabado');
        var domingo = document.getElementById(pre + 'ChkDomingo');

        if (lunes.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioLunes').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinLunes').value);

            if (document.getElementById(pre + 'DDXInicioLunes').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinLunes').value == "")
                salida = 0.00;

            var horasDia = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia;
            document.getElementById(pre + 'HiddenFieldLunes').value = horasDia;
        }

        if (martes.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioMartes').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinMartes').value);

            if (document.getElementById(pre + 'DDXInicioMartes').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinMartes').value == "")
                salida = 0.00;

            var horasDia2 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia2;
            document.getElementById(pre + 'HiddenFieldMartes').value = horasDia2;
        }

        if (miercoles.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioMiercoles').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinMiercoles').value);

            if (document.getElementById(pre + 'DDXInicioMiercoles').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinMiercoles').value == "")
                salida = 0.00;

            var horasDia3 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia3;
            document.getElementById(pre + 'HiddenFieldMiercoles').value = horasDia3;
        }

        if (jueves.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioJueves').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinJueves').value);

            if (document.getElementById(pre + 'DDXInicioJueves').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinJueves').value == "")
                salida = 0.00;

            var horasDia4 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia4;
            document.getElementById(pre + 'HiddenFieldJueves').value = horasDia4;
        }

        if (viernes.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioViernes').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinViernes').value);

            if (document.getElementById(pre + 'DDXInicioViernes').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinViernes').value == "")
                salida = 0.00;

            var horasDia5 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia5;
            document.getElementById(pre + 'HiddenFieldViernes').value = horasDia5;
        }

        if (sabado.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioSabado').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinSabado').value);

            if (document.getElementById(pre + 'DDXInicioSabado').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinSabado').value == "")
                salida = 0.00;

            var horasDia6 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia6;
            document.getElementById(pre + 'HiddenFieldSabado').value = horasDia6;
        }

        if (domingo.checked == true) {

            entrada = parseFloat(document.getElementById(pre + 'DDXInicioDomingo').value);
            salida = parseFloat(document.getElementById(pre + 'DDXFinDomingo').value);

            if (document.getElementById(pre + 'DDXInicioDomingo').value == "")
                entrada = 0.00;
            if (document.getElementById(pre + 'DDXFinDomingo').value == "")
                salida = 0.00;

            var horasDia7 = CalculaHorasTrabajadasDia(entrada, salida);
            totalHorasTrabajadas = totalHorasTrabajadas + horasDia7;
            document.getElementById(pre + 'HiddenFieldDomingo').value = horasDia7;
        }

        document.getElementById(pre + 'TxtHorasSemana').value = totalHorasTrabajadas.toFixed(1);
        CalculaHorasAnio();
    }

    function CalculaHorasAnio() {

        var horasSemana = parseFloat(document.getElementById(pre + 'TxtHorasSemana').value);
        var semanasAnio = parseFloat(document.getElementById(pre + 'TxtSemanasAnio').value);

        if (document.getElementById(pre + 'TxtHorasSemana').value == "")
            horasSemana = 0.00;

        if (document.getElementById(pre + 'TxtSemanasAnio').value == "")
            semanasAnio = 0.00;

        var horasAnio = horasSemana * semanasAnio;

        document.getElementById(pre + 'TxtHorasAnio').value = horasAnio.toFixed(1);
    }

    function HabilitaHorarios(inicio, fin, check1) {
        var casilla = document.getElementById(pre + check1);

        if (casilla.checked == true) {
            document.getElementById(pre + inicio).disabled = false;
            document.getElementById(pre + fin).disabled = false;
        } else {
            document.getElementById(pre + inicio).disabled = true;
            document.getElementById(pre + fin).disabled = true;
        }

    }
</script>

   <div id="Div1" runat="server">
                <asp:Image ID="Image6" runat="server" ImageUrl="~/SupplierModule/images/t11.png" />
                <br />
                <br />
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Check List de Crédito" CssClass="Button_1" ID="btnDisplayCreditCheckList"
                                OnClick="btnDisplayCreditCheckList_Click" />
                        </td>
                        <td id="Td1" class="auto-style6" runat="server"></td>
                        <td>
                            <asp:Button runat="server" Text="Contrato de Financiamiento" CssClass="Button_1"
                                ID="btnDisplayCreditContract" OnClick="btnDisplayCreditContract_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Acta Entrega - Recepción de Equipos" CssClass="Button_1"
                                ID="btnDisplayEquipmentAct" OnClick="btnDisplayEquipmentAct_Click" />
                        </td>
                        <td id="Td2" class="auto-style6" runat="server"></td>
                        <td class="auto-style6">
                            <asp:Button runat="server" Text="Solicitud Crédito" CssClass="Button_1" ID="btnDisplayCreditRequest1"
                                OnClick="btnDisplayCreditRequest1_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Pagaré" CssClass="Button_1" ID="btnDisplayPromissoryNote"
                                OnClick="btnDisplayPromissoryNote_Click" />
                        </td>
                        <td id="Td3" class="auto-style6" runat="server"></td>
                        <td class="auto-style6">
                            <asp:Button runat="server" Text="Endoso en Garantía" CssClass="Button_1" ID="btnDisplayGuaranteeEndorsement"
                                OnClick="btnDisplayGuaranteeEndorsement_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Carta Presupuesto de Inversión" CssClass="Button_1"
                                ID="btnDisplayQuota1" OnClick="btnDisplayQuota1_Click" />
                        </td>
                        <td id="Td4" class="auto-style6" runat="server"></td>
                        <td class="auto-style6">
                            <asp:Button runat="server" Text="Carta Compromiso Obligado Solidario" CssClass="Button_1"
                                ID="btnDisplayGuarantee" OnClick="btnDisplayGuarantee_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Tabla de Amortización" CssClass="Button_1" ID="btnDisplayRepaymentSchedule"
                                OnClick="btnDisplayRepaymentSchedule_Click" />
                        </td>
                        <td id="Td5" class="auto-style6" runat="server"></td>
                        <td class="auto-style6">
                            <asp:Button runat="server" Text="Recibo de Incentivo Energético (Descuento)" CssClass="Button_1" Enabled="false"
                                ID="btnDisplayDisposalBonusReceipt" OnClick="btnDisplayDisposalBonusReceipt_Click" />

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Pre-Boleta CAyD" CssClass="Button_1" ID="btnDisplayReceiptToSettle" Enabled="false"
                                OnClick="btnDisplayReceiptToSettle_Click" />
                        </td>
                        <td id="Td6" class="auto-style6" runat="server"></td>
                        <td class="auto-style6">
                            <asp:Button runat="server" Text="Tabla de Amortización - Pagaré" CssClass="Button_1"
                                ID="BtnAmortPag" OnClick="btnAmortPag_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnBoletaBajaEficiencia" runat="server" CssClass="Button_1" Enabled="false"
                                OnClick="btnBoletaBajaEficiencia_Click" Text="Boleta Ingreso Equipo" Visible="False" />
                        </td>
                        <td id="Td7" class="auto-style6" runat="server"></td>
                        <td id="Td8" class="auto-style6" runat="server"> </td>
                    </tr>
                </table>
                <br />
                <asp:Image ID="Image5" runat="server" ImageUrl="~/SupplierModule/images/t_equipos.png" />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" ID="lblFotoFachada" Text="Fotografía fachada: "></asp:Label>
                <br />
                <br />
                <div id="divGridEquiposAlta" style="align-content: center;" align="center" runat="server">
                    <asp:GridView Width="100%" runat="server" ID="grdEquiposAlta" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        AllowPaging="True" PageSize="10" DataKeyNames="CveTecnologia">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EditRowStyle CssClass="GridViewEditStyle" />
                        <Columns>
                            <asp:BoundField DataField="idProducto" Visible="False" />
                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Información Complementaria" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="BtnImgEditar" runat="server" ImageUrl="~/CentralModule/images/editar-icono.png" OnClick="BtnImgEditar_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Con/Sin Información" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbSelect" Enabled="False"  runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="Precio_Distribuidor" HeaderText="Precio Distribuidor" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="Precio_Unitario" HeaderText="Precio Unitario" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="Precio_total_sinIva" HeaderText="Precio Total s/IVA" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="Gastos_instalacion" HeaderText="Gastos de Instalación" ItemStyle-Width="5%" />
                        </Columns>
                    </asp:GridView>


                    <fieldset id="Fieldset1" runat="server">
                        <legend style="color: #0066FF">
                            <asp:Label ID="lblEquipoAlta" runat="server" /></legend>
                        <asp:HiddenField ID="hidEquipoAlta" runat="server" />
                        <asp:Label ID="lblTipoEquipo" align="left" runat="server"></asp:Label>
                        <table style="width: 100%">
                            <tr>
                                <td class="auto-style19">
                                               <asp:Image ID="Image7" runat="server" Height="16" ImageUrl="~/SupplierModule/images/arrow.png" />
                                    <asp:Label runat="server" Text="Información Equipo Alta Eficiencia" CssClass="Label1"
                                        ForeColor="#333333" ID="Label6"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label7" runat="server" CssClass="Label,etiqueta" Text="Fotografía Equipo Nuevo: " />
                                </td>
                                <td>
                                    <%--                                <uc1:wucCargarFoto ID="wucCargarFoto1" runat="server" />--%>
                                </td>
                            </tr>
                            <tr>
                                 <td align="right" class="auto-style19"> 
                    <asp:Button runat="server" ID="Button1" Text="Guardar Datos"/>
                            </td>
                            </tr>
                        </table>

                        <%--  <table id="Table1" runat="server" align="center" style="border: 1px solid #000000; clip: rect(1px, 1px, 1px, 1px); width: 80%;">
                        <tr>
                            <td align="center" style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">Horarios de Operación </td>
                            <td align="center" class="auto-style4" style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="Lunes" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox2" runat="server" Checked="True" Text="Martes" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox3" runat="server" Checked="True" Text="Miércoles" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox4" runat="server" Checked="True" Text="Jueves" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox5" runat="server" Checked="True" Text="Viernes" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox6" runat="server" Checked="True" Text="Sábado" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox7" runat="server" Checked="True" Text="Domingo" />
                            </td>
                        </tr>
                        <tr class="tr1">
                            <td align="center">Inicio</td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList2" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList3" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList4" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList5" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList6" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList7" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList8" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                        </tr>
                        <tr class="tr2" style="align-content: center">
                            <td>Fin</td>
                            <td>
                                <asp:DropDownList ID="DropDownList9" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList10" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList11" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList12" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList13" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList14" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList15" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                        </tr>
                    </table>--%>
                

                    <table style="width: 100%;">
                        <tr class="trh">
                            <td>Horarios de Operación
                            </td>
                            <td>Lunes
                    &nbsp;
                    <asp:CheckBox ID="ChkLunes" runat="server" onChange="HabilitaHorarios('DDXInicioLunes', 'DDXFinLunes', 'ChkLunes'); CalculaTotalHorasTrabajadas();" />
                            </td>
                            <td>Martes
                    &nbsp;
                    <asp:CheckBox ID="ChkMartes" runat="server" onChange="HabilitaHorarios('DDXInicioMartes', 'DDXFinMartes', 'ChkMartes'); CalculaTotalHorasTrabajadas();" />
                            </td>
                            <td>Miercoles
                    &nbsp;
                    <asp:CheckBox ID="ChkMiercoles" runat="server" onChange="HabilitaHorarios('DDXInicioMiercoles', 'DDXFinMiercoles', 'ChkMiercoles'); CalculaTotalHorasTrabajadas();" />
                            </td>
                            <td>Jueves
                    &nbsp;
                    <asp:CheckBox ID="ChkJueves" runat="server" onChange="HabilitaHorarios('DDXInicioJueves', 'DDXFinJueves', 'ChkJueves'); CalculaTotalHorasTrabajadas();" />
                            </td>
                            <td>Viernes
                    &nbsp;
                    <asp:CheckBox ID="ChkViernes" runat="server" onChange="HabilitaHorarios('DDXInicioViernes', 'DDXFinViernes', 'ChkViernes'); CalculaTotalHorasTrabajadas();" />
                            </td>
                            <td>Sabado
                    &nbsp;
                    <asp:CheckBox ID="ChkSabado" runat="server" onChange="HabilitaHorarios('DDXInicioSabado', 'DDXFinSabado', 'ChkSabado'); CalculaTotalHorasTrabajadas();" />
                            </td>
                            <td>Domingo
                    &nbsp;
                    <asp:CheckBox ID="ChkDomingo" runat="server" onChange="HabilitaHorarios('DDXInicioDomingo', 'DDXFinDomingo', 'ChkDomingo'); CalculaTotalHorasTrabajadas();" />
                            </td>
                        </tr>

                        <tr class="tr2">
                            <td>Inicio
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioLunes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioMartes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioMiercoles" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioJueves" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioViernes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioSabado" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioDomingo" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr class="tr1">
                            <td>Fin
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXFinLunes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXFinMartes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXFinMiercoles" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXFinJueves" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXFinViernes" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXFinSabado" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXFinDomingo" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadas();" Font-Size="XX-Small"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="ok" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldLunes" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldMartes" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldMiercoles" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldJueves" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldViernes" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldSabado" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldDomingo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">Horas a la semana
                    &nbsp;
                    <asp:TextBox ID="TxtHorasSemana" Enabled="False" runat="server"></asp:TextBox>
                            </td>
                            <td colspan="3">Semanas al año
                    &nbsp;
                    <asp:TextBox ID="TxtSemanasAnio" runat="server" MaxLength="2" Text="52" onChange="CalculaHorasAnio();"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                                    runat="server"
                                    TargetControlID="TxtSemanasAnio"
                                    FilterMode="ValidChars"
                                    ValidChars="0123456789" />
                            </td>
                            <td colspan="2">Horas al año
                    &nbsp;
                    <asp:TextBox ID="TxtHorasAnio" Enabled="False" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                            </fieldset>
                </div>
            </div>
 <%-- <div id="divEquiposAlta" runat="server">
             <asp:Image ID="Image6" runat="server" ImageUrl="~/SupplierModule/images/t11.png"/>
                <br/>
       <br/>
                <table align="center">
                    <tr>
                        <td>
                         <asp:Button runat="server" Text="Check List de Crédito" CssClass="Button_1" ID="btnDisplayCreditCheckList"
                                    OnClick="btnDisplayCreditCheckList_Click" />
                            </td>
                        <td id="Td1" class="auto-style6" runat="server"></td>
                        <td>
                             <asp:Button runat="server" Text="Contrato de Financiamiento" CssClass="Button_1"
                                    ID="btnDisplayCreditContract" OnClick="btnDisplayCreditContract_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:Button runat="server" Text="Acta Entrega - Recepción de Equipos" CssClass="Button_1"
                                    ID="btnDisplayEquipmentAct" OnClick="btnDisplayEquipmentAct_Click"  />
                        </td> <td id="Td2" class="auto-style6" runat="server"></td>
                        <td class="auto-style6">
                             <asp:Button runat="server" Text="Solicitud Crédito" CssClass="Button_1" ID="btnDisplayCreditRequest1"
                                    OnClick="btnDisplayCreditRequest1_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Pagaré" CssClass="Button_1" ID="btnDisplayPromissoryNote"
                                    OnClick="btnDisplayPromissoryNote_Click" />
                        </td> <td id="Td3" class="auto-style6" runat="server"></td>
                        <td class="auto-style6">
                              <asp:Button runat="server" Text="Endoso en Garantía" CssClass="Button_1" ID="btnDisplayGuaranteeEndorsement"
                                    OnClick="btnDisplayGuaranteeEndorsement_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Carta Presupuesto de Inversión" CssClass="Button_1"
                                    ID="btnDisplayQuota1" OnClick="btnDisplayQuota1_Click"  />
                        </td> <td id="Td4" class="auto-style6" runat="server"></td>
                        <td class="auto-style6">
                              <asp:Button runat="server" Text="Carta Compromiso Obligado Solidario" CssClass="Button_1"
                                    ID="btnDisplayGuarantee" OnClick="btnDisplayGuarantee_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:Button runat="server" Text="Tabla de Amortización" CssClass="Button_1" ID="btnDisplayRepaymentSchedule"
                                    OnClick="btnDisplayRepaymentSchedule_Click"  />
                        </td> <td id="Td5" class="auto-style6" runat="server"></td>
                        <td class="auto-style6">
                             <asp:Button runat="server" Text="Recibo de Incentivo Energético (Descuento)" CssClass="Button_1" Enabled="false"
                                    ID="btnDisplayDisposalBonusReceipt" OnClick="btnDisplayDisposalBonusReceipt_Click"  />
                               
                        </td>
                    </tr>
                    <tr>
                        <td>
                              <asp:Button runat="server" Text="Pre-Boleta CAyD" CssClass="Button_1" ID="btnDisplayReceiptToSettle" Enabled="false" 
                                    OnClick="btnDisplayReceiptToSettle_Click" />
                        </td> <td id="Td6" class="auto-style6" runat="server"></td>
                        <td class="auto-style6">
                            <asp:Button runat="server" Text="Tabla de Amortización - Pagaré" CssClass="Button_1"
                                    ID="BtnAmortPag" OnClick="btnAmortPag_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                               <asp:Button ID="btnPrint" runat="server" CssClass="Button_1" Enabled="false"
                                OnClick="btnPrint_Click" Text="Boleta Ingreso Equipo" Visible="False" />
                        </td> <td id="Td7" class="auto-style6" runat="server"></td>
                    </tr>
                </table>
       <br/>
                <asp:Image ID="Image5" runat="server" ImageUrl="~/SupplierModule/images/t_equipos.png" />
                <br/>
                <br/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" ID="lblFotoFachada" Text="Foto de la fachada: "></asp:Label>
                <br/>
                <br/>
                    <asp:GridView Width="100%" runat="server" ID="grdEquiposAlta" AutoGenerateColumns="False" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="10" DataKeyNames="CveTecnologia">
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EditRowStyle CssClass="GridViewEditStyle" />
                    <Columns>
                        <asp:BoundField DataField="idProducto" Visible="False"></asp:BoundField>
                        <asp:TemplateField HeaderText="Capturar Información Complementaria" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:ImageButton ID="BtnImgEditar" runat="server" ImageUrl="~/CentralModule/images/anadir-mas-icono.png" OnClick="BtnImgEditar_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-Width="15%"/>
                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-Width="15%"/>
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-Width="5%"/>
                        <asp:BoundField DataField="Precio_Distribuidor" HeaderText="Precio Distribuidor" ItemStyle-Width="5%"/>
                        <asp:BoundField DataField="Precio_Unitario" HeaderText="Precio Unitario" ItemStyle-Width="5%"/>
                        <asp:BoundField DataField="Precio_total_sinIva" HeaderText="Precio Total s/IVA" ItemStyle-Width="5%"/>
                        <asp:BoundField DataField="Gastos_instalacion" HeaderText="Gastos de Instalación" ItemStyle-Width="5%"/>
                    </Columns>
                </asp:GridView>
                    
 <asp:Panel runat="server" ID="datosComplementarios" Visible="True">
            <fieldset id="Fieldset1" runat="server">
                <legend style="color: #0066FF"><asp:Label ID="lblEquipoAlta" runat="server"/></legend>
                    <asp:HiddenField ID="hidEquipoAlta" runat="server" />
                 <asp:Label  ID="lblTipoEquipo" align="left" runat="server"></asp:Label>
                <table width="100%">
                        <tr>
                            <td>
                                <img alt="" src="~/SupplierModule/images/arrow.png" width="16" height="16"/>
                                <asp:Label runat="server" Text="Información Equipo Alta Eficiencia" CssClass="Label1"
                                    ForeColor="#333333" ID="Label6"></asp:Label>
                                </td>
                            </tr>
                    <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" CssClass="Label,etiqueta" Text="Fotografía Equipo Nuevo: " />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                               
                    <table id="Table1" runat="server" align="center" style="border: 1px solid #000000; clip: rect(1px, 1px, 1px, 1px); width: 80%;">
                        <tr>
                            <td align="center" style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">Horarios de Operación </td>
                            <td align="center" class="auto-style4" style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="Lunes" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox2" runat="server" Checked="True" Text="Martes" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox3" runat="server" Checked="True" Text="Miércoles" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox4" runat="server" Checked="True" Text="Jueves" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox5" runat="server" Checked="True" Text="Viernes" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox6" runat="server" Checked="True" Text="Sábado" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckBox7" runat="server" Checked="True" Text="Domingo" />
                            </td>
                        </tr>
                        <tr class="tr1">
                            <td align="center">Inicio</td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList2" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList3" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList4" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList5" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList6" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList7" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList8" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                        </tr>
                        <tr class="tr2" style="align-content: center">
                            <td>Fin</td>
                            <td>
                                <asp:DropDownList ID="DropDownList9" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList10" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList11" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList12" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList13" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList14" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList15" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
     </asp:Panel>

       </div>--%>
