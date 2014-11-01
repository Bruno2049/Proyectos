<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCapturaComplementariaEquiposBaja.ascx.cs" Inherits="PAEEEM.SupplierModule.Controls.wucCapturaComplementariaEquiposBaja" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
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
<div id="divForm" style="align-content: center">
    <br>
    <asp:Image ID="Image1" runat="server" ImageUrl="~/SupplierModule/images/t_validar_servicio.png" />
    <br />
    <table style="width: 100%">
        <tr>
            <td style="width: 180px; height: 25px">
                <asp:Label ID="Label2" runat="server" CssClass="Label,etiqueta" Text="Nombre comercial" />
            </td>
            <td style="width: 200px; height: 25px;">
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="200px" />
            </td>
            <td style="width: 150px; height: 25px;">
                <asp:Label ID="Label3" runat="server" CssClass="Label,etiqueta" Text="Número Crédito" />
            </td>
            <td style="height: 25px">
                <asp:TextBox ID="txtCreditoNum" runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="200px" />
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/SupplierModule/images/t10.png" />

            </td>
        </tr>
    </table>
    <br />
    <br />
            <div id="divGridEquiposBaja" style="align-content: center;" align="center" runat="server">
                <asp:GridView Width="100%" runat="server" ID="grdAdministraTecnologias" AutoGenerateColumns="False" CssClass="GridViewStyle"
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
                        <asp:BoundField DataField="Tecnologia" HeaderText="Tecnología" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="Producto" HeaderText="Tipo de Producto" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right" />
                        <asp:BoundField DataField="Unidades" HeaderText="Unidades" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right" />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right" />
                         <asp:BoundField DataField="idProducto" Visible="False" />
                    </Columns>
                </asp:GridView>
            </div>
            <br />
    <br />
    <br/>
    <asp:Panel runat="server" ID="datosComplementarios" Visible="True">
        <fieldset id="Fieldset1" runat="server">
            <legend style="color: #0066FF">
                <asp:Label ID="lblInformacionEB" runat="server"></asp:Label></legend>
            <asp:HiddenField ID="hiddenIdProducto" runat="server" />
            <table style="width: 100%">
                <tr>
                    <td colspan="8">
                        <img alt="" src="~/SupplierModule/images/arrow.png" width="16" height="16" />
                        <asp:Label runat="server" Text="Información Equipo Baja Eficiencia a Disponer" CssClass="Label1"
                            ForeColor="#333333" ID="Label1"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCAyD" runat="server" Text="CAyD: " CssClass="Label,etiqueta" />
                    </td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="drpCAyD" runat="server" Class="DropDownList" Font-Size="11px" Width="207px" Height="16px" />
                    </td>
                    <td class="auto-style5">
                        <asp:Label ID="lblColor" runat="server" CssClass="Label,etiqueta" Text="Color: " />
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtColor" runat="server" Height="20px" Width="127px" />
                    </td>
                    <td>
                        <asp:Label ID="lblMarca" runat="server" CssClass="Label,etiqueta" Text="Marca: " />
                    </td>
                    <td>
                        <asp:TextBox ID="txtMarca" runat="server" Width="183px" />
                    </td>
                    <td>
                        <asp:Label ID="lblModelo" runat="server" CssClass="Label,etiqueta" Text="Modelo: " />
                    </td>
                    <td>
                        <asp:TextBox ID="txtModelo" runat="server" Width="149px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblFoto" runat="server" CssClass="Label,etiqueta" Text="Fotografía Equipo Baja Eficiencia: " />
                    </td>
                    <td colspan="4">
                        <%--                                <uc1:wucCargarFoto ID="wucCargarFoto1" runat="server" />--%>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Image ID="Image4" runat="server" Height="16" ImageUrl="~/SupplierModule/images/arrow.png" />
                        <asp:Label ID="Label4" runat="server" CssClass="Label1" ForeColor="#333333" Text="Registrar Horarios de Equipos" />
                    </td>
                </tr>
            </table>
            <%--  <table id="TbHorarios" runat="server" align="center" style="border: 1px solid #000000; clip: rect(1px, 1px, 1px, 1px); width: 80%;">
                        <tr>
                            <td align="center" style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">Horarios de Operación </td>
                            <td align="center" class="auto-style4" style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="checkLunes" runat="server" Checked="True" Text="Lunes" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckMartes" runat="server" Checked="True" Text="Martes" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckMiercoles" runat="server" Checked="True" Text="Miércoles" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckJueves" runat="server" Checked="True" Text="Jueves" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckViernes" runat="server" Checked="True" Text="Viernes" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckSabado" runat="server" Checked="True" Text="Sábado" />
                            </td>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #3333FF">
                                <asp:CheckBox ID="CheckDomingo" runat="server" Checked="True" Text="Domingo" />
                            </td>
                        </tr>
                        <tr class="tr1">
                            <td align="center">Inicio</td>
                            <td align="center">
                                <asp:DropDownList ID="drpInicioLunes" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="drpInicioMartes" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="drpInicioMiercoles" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="drpInicioJueves" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="drpInicioViernes" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="drpInicioSabado" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="drpInicioDomingo" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                        </tr>
                        <tr class="tr2" style="align-content: center">
                            <td>Fin</td>
                            <td>
                                <asp:DropDownList ID="drpFinLunes" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFinMartes" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFinMiercoles" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFinJueves" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFinViernes" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFinSabado" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFinDomingo" runat="server" Class="DropDownList" Font-Size="11px" Width="99px" />
                            </td>
                        </tr>
                           </table>
            --%>
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
                    <asp:TextBox ID="TxtHorasSemana" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                    <td colspan="3">Semanas al año
                    &nbsp;
                    <asp:TextBox ID="TxtSemanasAnio" Text="52" runat="server" MaxLength="2" onChange="CalculaHorasAnio();"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                            runat="server"
                            TargetControlID="TxtSemanasAnio"
                            FilterMode="ValidChars"
                            ValidChars="0123456789" />
                    </td>
                    <td colspan="2">Horas al año
                    &nbsp;
                    <asp:TextBox ID="TxtHorasAnio" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
            </table>

        </fieldset>
    </asp:Panel>


</div>
