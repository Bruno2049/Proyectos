<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapturaCompAltaEquipos.aspx.cs" Inherits="PAEEEM.CentralModule.CapturaCompAltaEquipos" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>

<asp:Content runat="server" ID="ContenHead" ContentPlaceHolderID="head">
    <link href="../Resources/Css/Site.css" rel="stylesheet" />
    <script type="text/javascript">

        var pre = "ctl00_MainContent_";

        function uploadCompleteAltaEquipoNuevo(sender) {

            var hiden = document.getElementById(pre+"HidActualizaOk");
            hiden.value = "Nuevo";
            
            var hidenState = document.getElementById(pre + "HidstateLoad");
            hidenState.value = "ok";

            var divok = $get("<%=btnRefresh.ClientID%>");
            divok.click();
            
        }

        function uploadErrorAltaEquipoNuevo(sender) {
            var hiden = document.getElementById(pre + "HidActualizaOk");
            hiden.value = "Nuevo";

            var hidenState = document.getElementById(pre + "HidstateLoad");
            hidenState.value = "ko";

            var divok = $get("<%=btnRefresh.ClientID%>");
            divok.click();

        }

        function uploadCompleteAltaEquipoViejo(sender) {
            var hiden = document.getElementById(pre + "HidActualizaOk");
            hiden.value = "Viejo";

            var hidenState = document.getElementById(pre + "HidstateLoad");
            hidenState.value = "ok";

            var divok = $get("<%=btnRefresh.ClientID%>");
            divok.click();
         }

         function uploadErrorAltaEquipoViejo(sender) {
             var hiden = document.getElementById(pre + "HidActualizaOk");
             hiden.value = "Viejo";

             var hidenState = document.getElementById(pre + "HidstateLoad");
             hidenState.value = "ko";

             var divok = $get("<%=btnRefresh.ClientID%>");
            divok.click();
        }
      
        function uploadCompleteFachada(sender) {
            var hiden = document.getElementById(pre + "HidActualizaOk");
            hiden.value = "Fachada";
            
            var hidenState = document.getElementById(pre + "HidstateLoad");
            hidenState.value = "ok";

            var divok = $get("<%=btnRefresh.ClientID%>");
            divok.click();
        }
        function uploadErrorFachada(sender) {
            var hiden = document.getElementById(pre + "HidActualizaOk");
            hiden.value = "Fachada";
            
            var hidenState = document.getElementById(pre + "HidstateLoad");
            hidenState.value = "ko";

            var divok = $get("<%=btnRefresh.ClientID%>");
            divok.click();
        }

       function CalculaHorasTrabajadasDiaAltaEquipoViejo() {
            var total = 0; //total semanal de horas trabajadas

            if (document.getElementById(pre + 'hlabor1').value != "" && 
               document.getElementById(pre + 'DDXInicioLunesAltaEquipoViejo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlabor1').value);
            if (document.getElementById(pre + 'hlabor2').value != "" &&
                document.getElementById(pre + 'DDXInicioMartesAltaEquipoViejo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlabor2').value);
            if (document.getElementById(pre + 'hlabor3').value != "" &&
                document.getElementById(pre + 'DDXInicioMiercolesAltaEquipoViejo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlabor3').value);
            if (document.getElementById(pre + 'hlabor4').value != "" &&
                document.getElementById(pre + 'DDXInicioJuevesAltaEquipoViejo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlabor4').value);
            if (document.getElementById(pre + 'hlabor5').value != "" &&
                document.getElementById(pre + 'DDXInicioViernesAltaEquipoViejo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlabor5').value);
            if (document.getElementById(pre + 'hlabor6').value != "" &&
                document.getElementById(pre + 'DDXInicioSabadoAltaEquipoViejo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlabor6').value);
            if (document.getElementById(pre + 'hlabor7').value != "" &&
                document.getElementById(pre + 'DDXInicioDomingoAltaEquipoViejo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlabor7').value);

            //total = parseInt(total);

            document.getElementById(pre + 'TxtHorasSemanaAltaEquipoViejo').value = total;
            CalculaHorasAnioAltaEquipoViejo();
        }
       function CalculaHorasAnioAltaEquipoViejo() {

            var horasSemana = parseFloat(document.getElementById(pre + 'TxtHorasSemanaAltaEquipoViejo').value);
            var semanasAnio = parseFloat(document.getElementById(pre + 'noSemanasAltaEquipoViejo').value);

            if (document.getElementById(pre + 'TxtHorasSemanaAltaEquipoViejo').value == "")
                horasSemana = 0.00;

            if (document.getElementById(pre + 'noSemanasAltaEquipoViejo').value == "")
                semanasAnio = 0.00;

            var horasAnio = horasSemana * semanasAnio;

            document.getElementById(pre + 'TxtHorasAnioAltaEquipoViejo').value = horasAnio.toFixed(1);
        }
       function CalculaTotalHorasTrabajadasAltaEquipoNuevo() {
            var total = 0.00;
           
            if (document.getElementById(pre + 'hlaborNuevo1').value != "" &&
               document.getElementById(pre + 'DDXInicioLunesAltaEquipoNuevo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlaborNuevo1').value);
            if (document.getElementById(pre + 'hlaborNuevo2').value != "" &&
                document.getElementById(pre + 'DDXInicioMartesAltaEquipoNuevo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlaborNuevo2').value);
            if (document.getElementById(pre + 'hlaborNuevo3').value != "" &&
                document.getElementById(pre + 'DDXInicioMiercolesAltaEquipoNuevo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlaborNuevo3').value);
            if (document.getElementById(pre + 'hlaborNuevo4').value != "" &&
                document.getElementById(pre + 'DDXInicioJuevesAltaEquipoNuevo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlaborNuevo4').value);
            if (document.getElementById(pre + 'hlaborNuevo5').value != "" &&
                document.getElementById(pre + 'DDXInicioViernesAltaEquipoNuevo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlaborNuevo5').value);
            if (document.getElementById(pre + 'hlaborNuevo6').value != "" &&
                document.getElementById(pre + 'DDXInicioSabadoAltaEquipoNuevo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlaborNuevo6').value);
            if (document.getElementById(pre + 'hlaborNuevo7').value != "" &&
                document.getElementById(pre + 'DDXInicioDomingoAltaEquipoNuevo').value != "Seleccione")
                total = total + parseInt(document.getElementById(pre + 'hlaborNuevo7').value);

            document.getElementById(pre + 'TxtHorasSemanaAltaEquipoNuevo').value = total.toFixed(1); // .toFixed(1);
            CalculaHorasAnioAltaEquipoNuevo();
        }
       function CalculaHorasAnioAltaEquipoNuevo() {

            var horasSemana = parseFloat(document.getElementById(pre + 'TxtHorasSemanaAltaEquipoNuevo').value);
            var semanasAnio = parseFloat(document.getElementById(pre + 'TxtSemanasAnioAltaEquipoNuevo').value);

            if (document.getElementById(pre + 'TxtHorasSemanaAltaEquipoNuevo').value == "")
                horasSemana = 0.00;

            if (document.getElementById(pre + 'TxtSemanasAnioAltaEquipoNuevo').value == "")
                semanasAnio = 0.00;

            var horasAnio = horasSemana * semanasAnio;

            document.getElementById(pre + 'TxtHorasAnioAltaEquipoNuevo').value = horasAnio.toFixed(1);
        }

        
    </script>

    <style type="text/css">
        .auto-style1 {
            width: 269px;
        }

        .auto-style6 {
            width: 168px;
        }

        .auto-style10 {
            width: 190px;
        }
    </style>

</asp:Content>
<asp:Content runat="server" ID="contentMaint" ContentPlaceHolderID="MainContent">
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbtnVer"/>
            <asp:PostBackTrigger ControlID="verEquipoViejo" />
            <asp:PostBackTrigger ControlID="verEquipoNuevo" />
        </Triggers>
        <ContentTemplate>
            <div style="display: none">
                <asp:Button ID="btnRefresh" runat="server" Text="Button" OnClick="Button1_Click" />
                 <asp:HiddenField ID="HidActualizaOk" runat="server" />
                 <asp:HiddenField ID="HidstateLoad" runat="server" />
            </div>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/SupplierModule/images/t11.png" />
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="width: 180px; height: 25px">
                        <asp:Label ID="Label2" runat="server" CssClass="Label,etiqueta" Text="Nombre comercial" />
                    </td>
                    <td style="width: 200px; height: 25px;">
                        <asp:TextBox ID="txtRazonSocialAltaEquipos" runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="200px" />
                    </td>
                    <td style="width: 150px; height: 25px;">
                        <asp:Label ID="Label3" runat="server" CssClass="Label,etiqueta" Text="Número Crédito" />
                    </td>
                    <td style="height: 25px">
                        <asp:TextBox ID="txtCreditoNumAltaEquipos" runat="server" CssClass="TextBox" Enabled="False" Font-Size="11px" Width="200px" />
                    </td>
                </tr>
            </table>
            <br />
            <table align="center" runat="server">
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Check List de Crédito" ID="btnDisplayCreditCheckList" Width="267px"
                            OnClick="btnDisplayCreditCheckList_Click" />
                    </td>
                    <td id="Td1" class="auto-style6" runat="server"></td>
                    <td>
                        <asp:Button runat="server" Text="Contrato de Financiamiento" Width="267px"
                            ID="btnDisplayCreditContract" OnClick="btnDisplayCreditContract_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Acta Entrega - Recepción de Equipos" Width="267px"
                            ID="btnDisplayEquipmentAct" OnClick="btnDisplayEquipmentAct_Click" />
                    </td>
                    <td  runat="server"></td>
                    <td >
                        <asp:Button runat="server" Text="Solicitud Crédito" ID="btnDisplayCreditRequest1" Width="267px"
                            OnClick="btnDisplayCreditRequest1_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Pagaré" ID="btnDisplayPromissoryNote" Width="267px"
                            OnClick="btnDisplayPromissoryNote_Click" />
                    </td>
                    <td id="Td3" runat="server"></td>
                    <td >
                        <asp:Button runat="server" Text="Endoso en Garantía" ID="btnDisplayGuaranteeEndorsement" Width="267px"
                            OnClick="btnDisplayGuaranteeEndorsement_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Carta Presupuesto de Inversión" Width="267px"
                            ID="btnDisplayQuota1" OnClick="btnDisplayQuota1_Click" />
                    </td>
                    <td id="Td4"  runat="server"></td>
                    <td>
                        <asp:Button runat="server" Text="Carta Compromiso Obligado Solidario" Width="267px"
                            ID="btnDisplayGuarantee" OnClick="btnDisplayGuarantee_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Tabla de Amortización" ID="btnDisplayRepaymentSchedule" Width="267px"
                            OnClick="btnDisplayRepaymentSchedule_Click" />
                    </td>
                    <td></td>
                    <td>
                        <asp:Button runat="server" Text="Recibo de Incentivo Energético (Descuento)" Enabled="false" Width="267px"
                            ID="btnDisplayDisposalBonusReceipt" OnClick="btnDisplayDisposalBonusReceipt_Click" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Pre-Boleta CAyD" ID="btnDisplayReceiptToSettle" Enabled="false" Width="267px"
                            OnClick="btnDisplayReceiptToSettle_Click" />
                    </td>
                    <td runat="server"></td>
                    <td >
                        <asp:Button runat="server" Text="Tabla de Amortización - Pagaré" Width="267px"
                            ID="BtnAmortPag" OnClick="btnAmortPag_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnBoletaBajaEficiencia" runat="server" Enabled="false" Width="267px"
                            OnClick="btnBoletaBajaEficiencia_Click" Text="Boleta Ingreso Equipo" Visible="False" />
                    </td>
                    <td id="Td7" class="auto-style6" runat="server"></td>
                    <td id="Td8" class="auto-style6" runat="server"></td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Image runat="server" ImageUrl="~/SupplierModule/images/t8.png" />
            <table>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblFoto" runat="server" CssClass="Label,etiqueta" Text="Fotografía Fachada del Negocio/Local: " />
                    </td>
                    <td>
                        <asp:AsyncFileUpload OnClientUploadError="uploadErrorFachada"
                            OnClientUploadComplete="uploadCompleteFachada" runat="server"
                            ID="AsyncFileUploadFachada" Width="400px" UploaderStyle="Modern"
                            CompleteBackColor="White" 
                            UploadingBackColor="#CCFFFF" ThrobberID="imgLoaderFachada"
                            OnUploadedComplete="FileUploadCompleteFachada" Style="margin-left: 34px" />
                    </td>
                    <td>
                        <asp:Image ID="imgLoaderFachada" runat="server" ImageUrl="~/CentralModule/images/loader.gif" />
                        <asp:Image ID="imgOKLoaderFachada" runat="server" ImageUrl="~/CentralModule/images/icono_correcto.png" Height="25px" Width="27px" Visible="False" />
                        <asp:ImageButton runat="server" ID="imgbtnVer" ImageUrl="~/CentralModule/images/visualizar.png" OnClick="imgbtnVer_Click" />
                        <asp:Label ID="lblMesgFachada" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <div id="divGridEquiposBaja" style="align-content: center;" align="center" runat="server">
                <asp:GridView Width="100%" runat="server" ID="grdEquiposAlta" AutoGenerateColumns="False" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="6" DataKeyNames="Id_Credito_Producto,idConsecutivo" OnRowDataBound="grdEquiposAlta_RowDataBound">
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EditRowStyle CssClass="GridViewEditStyle" />
                    <Columns>
                        <asp:BoundField DataField="Id_Credito_Producto" Visible="False" />
                        <asp:BoundField DataField="idConsecutivo" Visible="False" />
                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="Información Complementaria" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="BtnImgEditar" runat="server" ImageUrl="~/CentralModule/images/editar-icono.png" OnClick="BtnImgEditarAltaEquipo_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Con/Sin Información" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbSelect" Enabled="False" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Tecnologia" HeaderText="Tecnología" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Producto" HeaderText="Producto" ItemStyle-Width="15%"/>
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right" />
                        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right" />
                        <%--<asp:BoundField DataField="Precio Distribuidor" HeaderText="Cantidad" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right" />--%>
                        <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right" />
                        <asp:BoundField DataField="Gastos_Instalacion" HeaderText="Gastos Instalacion" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right" />
                        <asp:BoundField DataField="ImporteTotalSinIva" HeaderText="Importe Total s/Iva" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right" />
                        <asp:BoundField DataField="Cve_Tecnologia" Visible="True" />
                    </Columns>
                </asp:GridView>

            </div>
            <br />

            <asp:Panel runat="server" ID="datosComplementariosAlta" Visible="True" CssClass="PanelVisible">
                    <asp:HiddenField ID="hiddenRowIndexEquiboAlta" runat="server" />
                    <table style="width: 100%">
                        <tr>
                            <td colspan="8">
                                <asp:Image ID="Image3" runat="server" Height="16" ImageUrl="~/SupplierModule/images/arrow.png" />
                                <asp:Label runat="server" Text="Información Equipo Alta" CssClass="Label1" ForeColor="#333333" ID="Label5" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoMov" runat="server" CssClass="Label,etiqueta" />
                                <br />
                                <asp:Label ID="lblInformacionEA" runat="server" />
                            </td>
                        </tr>
                    </table>
            <asp:Panel ID="EquipoViejo" runat="server">
                <fieldset class="legend_info">
                    <legend style="font-size: 14px; align-content: initial">Equipo Viejo</legend>
                    <table>
                        <tr>
                            <td colspan="2" class="auto-style10">
                                <asp:Label ID="Label1" runat="server" CssClass="Label,etiqueta" Text="Fotografía Equipo Viejo: " />
                            </td>
                            <td colspan="2">
                                <asp:AsyncFileUpload OnClientUploadError="uploadErrorAltaEquipoViejo"
                                    OnClientUploadComplete="uploadCompleteAltaEquipoViejo" runat="server"
                                    ID="AsyncFileUploadAltaEquipoViejo" Width="400px" UploaderStyle="Modern"
                                    CompleteBackColor="White" 
                                    UploadingBackColor="#CCFFFF" ThrobberID="imgLoaderEquipoViejo"
                                    OnUploadedComplete="FileUploadCompleteAltaEquipoViejo" />
                            </td>
                            <td>
                                <asp:Image ID="imgLoaderEquipoViejo" runat="server" ImageUrl="~/CentralModule/images/loader.gif" />
                                <asp:Image ID="imgOKLoaderEquipoViejo" runat="server" ImageUrl="~/CentralModule/images/icono_correcto.png" Height="25px" Width="27px" Visible="False" />
                                <asp:ImageButton runat="server" ID="verEquipoViejo" ImageUrl="~/CentralModule/images/visualizar.png" OnClick="verEquipoViejo_Click" />
                               
                            </td>
                        </tr>
                    </table>
                    <table id="tbl" runat="server" style="width: 100%;">
                        <tr class="trh">
                            <td>Horarios de Operación
                            </td>
                            <td>Lunes
                    &nbsp;
                   </td>
                            <td>Martes
                    &nbsp;
                            </td>
                            <td>Miercoles
                    &nbsp;
                            </td>
                            <td>Jueves
                    &nbsp;
                            </td>
                            <td>Viernes
                    &nbsp;
                            </td>
                            <td>Sabado
                    &nbsp;
                            </td>
                            <td>Domingo
                    &nbsp;
                            </td>
                        </tr>
                        <tr class="tr2">
                            <td>Inicio
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioLunesAltaEquipoViejo" runat="server" Width="93px" Font-Size="Small" Height="17px"></asp:DropDownList>

                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioMartesAltaEquipoViejo" runat="server" Width="93px" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioMiercolesAltaEquipoViejo" runat="server" Width="93px" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();"></asp:DropDownList>

                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioJuevesAltaEquipoViejo" runat="server" Width="93px" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" ></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioViernesAltaEquipoViejo" runat="server" Width="93px" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();"></asp:DropDownList>

                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioSabadoAltaEquipoViejo" runat="server" Width="93px" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioDomingoAltaEquipoViejo" runat="server" Width="93px" Font-Size="Small" Height="17px" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tr1">
                            <td>Horas de Operación
                            </td>
                            <td>
                                 <telerik:RadNumericTextBox ID="hlabor1" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" runat="server" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                
                              

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor2" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor3" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor4" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor5" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox> 

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor6" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlabor7" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldLunesAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldMartesAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldMiercolesAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldJuevesAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldViernesAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldSabadoAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldDomingoAltaEquipoViejo" runat="server" Visible="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">Horas a la semana
                    &nbsp;
                    <asp:TextBox ID="TxtHorasSemanaAltaEquipoViejo" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                            <td colspan="3">Semanas al año
                    &nbsp;
                    <telerik:RadNumericTextBox ID="noSemanasAltaEquipoViejo" runat="server" MaxLength="2" MaxValue="52" MinValue="1" Value="52" onChange="CalculaHorasTrabajadasDiaAltaEquipoViejo();">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="2">Horas al año
                    &nbsp;
                                <asp:TextBox ID="TxtHorasAnioAltaEquipoViejo" runat="server" Enabled="False" />
                            </td>
                        </tr>
                    </table>

                </fieldset>
            </asp:Panel>

            <asp:Panel ID="EquipoNuevo" runat="server">
                <fieldset class="legend_info">
                    <legend style="font-size: 14px; align-content: initial">Equipo Nuevo </legend>
                    <table>
                        <tr>
                            <td colspan="2" class="auto-style10">
                                <asp:Label ID="Label6" runat="server" CssClass="Label,etiqueta" Text="Fotografía Equipo Nuevo: " />
                            </td>
                            <td colspan="2">
                                <asp:AsyncFileUpload OnClientUploadError="uploadErrorAltaEquipoNuevo"
                                    OnClientUploadComplete="uploadCompleteAltaEquipoNuevo" runat="server"
                                    ID="AsyncFileUploadEquipoNuevo" Width="400px" UploaderStyle="Modern"
                                    CompleteBackColor="White" 
                                    UploadingBackColor="#CCFFFF" ThrobberID="imgLoaderEquipoNuevo"
                                    OnUploadedComplete="FileUploadCompleteAltaEquipoNuevo" />
                            </td>
                            <td>
                                <asp:Image ID="imgLoaderEquipoNuevo" runat="server" ImageUrl="~/CentralModule/images/loader.gif" />
                                <asp:Image ID="imgOkEquipoNuevo" runat="server" ImageUrl="~/CentralModule/images/icono_correcto.png" Height="25px" Width="27px" Visible="False" />
                                <asp:ImageButton runat="server" ID="verEquipoNuevo" ImageUrl="~/CentralModule/images/visualizar.png" OnClick="verEquipoNuevo_Click" />
                               
                            </td>
                        </tr>
                    </table>

                    <table style="width: 100%;">
                        <tr class="trh">
                            <td>Horarios de Operación
                            </td>
                            <td>Lunes
                    &nbsp;
                                </td>
                            <td>Martes
                    &nbsp;
                            </td>
                            <td>Miercoles
                    &nbsp;
                            </td>
                            <td>Jueves
                    &nbsp;
                            </td>
                            <td>Viernes
                    &nbsp;
                            </td>
                            <td>Sabado
                    &nbsp;
                            </td>
                            <td>Domingo
                    &nbsp;
                            </td>
                        </tr>
                        <tr class="tr2">
                            <td>Inicio
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioLunesAltaEquipoNuevo" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioMartesAltaEquipoNuevo" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioMiercolesAltaEquipoNuevo" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioJuevesAltaEquipoNuevo" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioViernesAltaEquipoNuevo" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioSabadoAltaEquipoNuevo" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDXInicioDomingoAltaEquipoNuevo" runat="server" Width="65px" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" Font-Size="Small"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr class="tr1">
                            <td>Horas laborables
                            </td>
                            <td>
                                 <telerik:RadNumericTextBox ID="hlaborNuevo1" MaxLength="2" MaxValue="24" MinValue="1" name="hlaborNuevo1" runat="server" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                
                              

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo2" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo3" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo4" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo5" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox> 

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo6" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="hlaborNuevo7" runat="server" MaxLength="2" MaxValue="24" MinValue="1" name="hlabor1" Width="7em" Type="Number" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();" >
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>

                            </td>
                        </tr>
                       
                         <tr>
                            <td>
                                <asp:HiddenField ID="ok" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldLunesAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldMartesAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldMiercolesAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldJuevesAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldViernesAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldSabadoAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldDomingoAltaEquipoNuevo" runat="server" Visible="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">Horas a la semana
                    &nbsp;
                    <asp:TextBox ID="TxtHorasSemanaAltaEquipoNuevo" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                            <td colspan="3">Semanas al año
                    &nbsp;
                     <telerik:RadNumericTextBox ID="TxtSemanasAnioAltaEquipoNuevo" runat="server" MaxLength="2" MaxValue="52" MinValue="1" Value="52" onChange="CalculaTotalHorasTrabajadasAltaEquipoNuevo();">
                       <NumberFormat DecimalDigits="0" />
                     </telerik:RadNumericTextBox>
                      <asp:RequiredFieldValidator runat="server"
                                    ControlToValidate="TxtSemanasAnioAltaEquipoNuevo"
                                    ErrorMessage="Se debe capturar las Semanas al año"
                                    ValidationGroup="Basica"
                                    Display="Dynamic"
                                    Text="*"
                                    EnableClientScript="true"
                                    ID="RequiredFieldValidator4"
                                    InitialValue="" />
                            </td>
                            <td colspan="2">Horas al año
                    &nbsp;
                                <asp:TextBox ID="TxtHorasAnioAltaEquipoNuevo" runat="server" Enabled="False" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:ValidationSummary runat="server" ID="ValidaDatosEquipobaja" CssClass="failureNotification" ValidationGroup="Basica" Font-Size="Small" HeaderText="Resumen:" />
            <p style="text-align: center; width: 873px">
                <asp:Button ID="btnGuardarDatosCompEquipoBaja" runat="server" OnClick="btnGuardarDatosCompEquipoBaja_Click" Text="Guardar Datos" />
            </p>
             <asp:HiddenField ID="hidDataKey0" runat="server" />
             <asp:HiddenField ID="hidDataKey" runat="server" />
             <asp:HiddenField ID="HidTipoMovimiento" runat="server" />
            </asp:Panel>

        </ContentTemplate>

    </asp:UpdatePanel>
   
</asp:Content>
