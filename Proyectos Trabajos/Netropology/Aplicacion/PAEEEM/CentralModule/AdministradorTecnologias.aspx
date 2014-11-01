<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministradorTecnologias.aspx.cs" 
    Inherits="PAEEEM.CentralModule.AdministradorTecnologias" MasterPageFile="../Site.Master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

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
    </style>
    <script type="text/javascript">
        var pre = "ctl00_MainContent_";

        function ValidaTarifas() {
            var cont = 0;

            var containerRef = document.getElementById(pre + 'ChkLstTarifas');
            var inputRefArray = containerRef.getElementsByTagName('input');

            for (var i = 0; i < inputRefArray.length; i++) {
                var inputRef = inputRefArray[i];

                if (inputRef.type.substr(0, 8) == 'checkbox') {
                    if (inputRef.checked == true)
                        cont++;
                }
            }

            if (cont == 0) {
                alert('Debe Seleccionar al menos una Tarifa');
                return false;
            }
            else {
                return ValidaTecnologias();
            }

            //return true;
        }

        function ValidaTecnologias() {
            var ddxCombinacionTecnologias = document.getElementById(pre + 'DDXCombinacionTecnologias');
            var hidden = document.getElementById(pre + 'HiddenEstatus').value;
            var mensaje = '';
            var cont = 0;

            if (hidden == '0')
                mensaje = 'Confirmar Guardar la Tecnología';

            if (hidden == '1')
                mensaje = 'Confirmar Actualizar Tecnología';

            if (ddxCombinacionTecnologias.value == 1) {
                
                var containerRef = document.getElementById(pre + 'ChkLstTecnologiasCombiladas');
                var inputRefArray = containerRef.getElementsByTagName('input');

                for (var i = 0; i < inputRefArray.length; i++) {
                    var inputRef = inputRefArray[i];

                    if (inputRef.type.substr(0, 8) == 'checkbox') {
                        if (inputRef.checked == true)
                            cont++;
                    }
                }

                if (cont == 0) {
                    alert('Debe Seleccionar al menos una Tecnología a combinar');
                    ddxCombinacionTecnologias.focus();
                    return false;
                }
                else {
                    return confirm(mensaje);
                }
            }
            else {
                //return true;
                return confirm(mensaje);
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
            <div style="width: 100%">
                <br>
                <table style="width: 100%">
                    <tr>
                        <%--<td>
                            &nbsp;
                        </td>--%>
                        <td>
                          <h2>  <asp:Label ID="lblTitle" runat="server" Text="ADMINISTRACION DE TECNOLOGIAS" Font-Size="Large" Font-Bold="True"></asp:Label>
                            <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                            <%--<asp:ImageButton ID="ImgBtnHome" runat="server" ImageUrl="~/CentralModule/images/home-icon.jpg" OnClick="ImgBtnHome_Click" Visible="False" />                --%>
                            <asp:LinkButton ID="ImgBtnHome" runat="server" Font-Bold="True" Font-Size="Large" OnClick="LinkButton1_Click" Visible="False">ADMINISTRACION DE TECNOLOGIAS</asp:LinkButton></h2>
                            <hr class="rule" />
                        </td>
                    </tr>
                </table>
                
                <br /><br />
            </div>

            <asp:Panel ID="PanelGridTecnologias" runat="server">
                <div id="container">
                    <asp:ImageButton ID="BtnImgAgregar" runat="server" ImageUrl="~/CentralModule/images/plus.jpg" AlternateText="Agregar Tecnología" OnClick="BtnImgAgregar_Click" />
                    
                    <br /><br />
                    <asp:GridView runat="server" ID="grdAdministraTecnologias" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        AllowPaging="True" PageSize="8" DataKeyNames="CveTecnologia" OnRowCreated="grdAdministraTecnologias_RowCreated">
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                                <PagerSettings Visible="False" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />

                        <Columns>
                            <asp:BoundField DataField="CveTecnologia" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="DxNombreGeneral" HeaderText="Tecnología"></asp:BoundField>
                            <asp:BoundField DataField="DxEstatus" HeaderText="Estatus"></asp:BoundField>
                            <asp:BoundField DataField="DxCveCC" HeaderText="Clave"></asp:BoundField>
                            <asp:BoundField DataField="DxTipoMovimiento" HeaderText="Tipo"></asp:BoundField>
                            <asp:BoundField DataField="Monto_Chatarrizacion" HeaderText="Monto" 
                                ItemStyle-HorizontalAlign="Center" 
                                ItemStyle-VerticalAlign="Middle"
                                DataFormatString="{0:C}">

                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                            </asp:BoundField>
                            <asp:BoundField DataField="DxEquiposBaja" HeaderText="Baja"></asp:BoundField>
                            <asp:BoundField DataField="DxEquiposAlta" HeaderText="Alta"></asp:BoundField>
                            <asp:BoundField DataField="DxFactorSustitucion" HeaderText="Factor de Sustitución" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Tarifa02" HeaderText="2"></asp:BoundField>
                            <asp:BoundField DataField="Tarifa03" HeaderText="3"></asp:BoundField>
                            <asp:BoundField DataField="TarifaOM" HeaderText="OM"></asp:BoundField>
                            <asp:BoundField DataField="TarifaHM" HeaderText="HM"></asp:BoundField>
                            <asp:BoundField DataField="MontoIncentivo" HeaderText="Monto" 
                                ItemStyle-HorizontalAlign="Center" 
                                ItemStyle-VerticalAlign="Middle"
                                DataFormatString="{0:P}">

                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                            </asp:BoundField>
                            <asp:BoundField DataField="DxTecnologias" HeaderText="Combinación con otra Tecnología" 
                                ItemStyle-HorizontalAlign="Center" 
                                ItemStyle-VerticalAlign="Middle"
                                HeaderStyle-Width="5%">
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="BtnImgEditar" runat="server" ImageUrl="~/CentralModule/images/editar-icono.png" OnClick="BtnImgEditar_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField> 
                        </Columns>

                    </asp:GridView>
                    
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="8"
                                AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                                PageIndexBoxType="DropDownList" CustomInfoHTML="Página:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                                UrlPaging="false"
                                FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                                CurrentPageButtonClass="cpb" OnPageChanged="AspNetPager_PageChanged" >
                    </webdiyer:AspNetPager>
            </div>
            </asp:Panel>
            <br/><br/>
            <asp:Panel ID="PanelActualizaTecnologia" runat="server" Visible="False">
                <div id="container2" align="center">
                <table>
                    <tr>
                        <td colspan="2" align="center" class="trh">
                            <asp:Label ID="LblTitulo" runat="server"></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Nombre:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtNombreTecnologia" runat="server" Width="200px" ValidationGroup="Save" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtNombreTecnologia" 
                                ErrorMessage="El nombre de la Tecnología es requerido"
                                ValidationGroup="Save"
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredTxtNombreTecnologia">
                             </asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="FTETxtNombreTecnologia" 
                                runat="server" 
                                TargetControlID="TxtNombreTecnologia" 
                                FilterType="Custom" 
                                FilterMode="ValidChars" 
                                ValidChars="0123456789.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " FilterInterval="50" />
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Clave:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtClaveTecnologia" runat="server" ValidationGroup="Save" Width="200px" MaxLength="5"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FTETxtClaveTecnologia" 
                                runat="server" 
                                TargetControlID="TxtClaveTecnologia" 
                                FilterType="Custom" 
                                FilterMode="ValidChars" 
                                ValidChars="0123456789.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" 
                                FilterInterval="4" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtClaveTecnologia" 
                                ErrorMessage="La clave de la Tecnología es requerida"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredTxtClaveTecnologia">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr class="tr2">
                        <td>
                            Estatus:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDXEstatusTecnologia" runat="server" CssClass="DropDownList"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="DDXEstatusTecnologia" 
                                ErrorMessage="El Estatus de la Tecnología es Requerido"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredDDXEstatusTecnologia" 
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Tipo:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDXTipoTecnologia" runat="server" CssClass="DropDownList"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="DDXTipoTecnologia" 
                                ErrorMessage="El Tipo es Requerido"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredDDXTipoTecnologia" 
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Chatarrización:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDXChatarrizacion" runat="server" CssClass="DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDXChatarrizacion_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="DDXChatarrizacion" 
                                ErrorMessage="Se debe seleccionar si aplica la Chatarrización"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredDDXChatarrizacion" 
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Monto Chatarrización:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtMontoChatarrizacion" Width="200px" runat="server" Enabled="False" MaxLength="20">0.00</asp:TextBox>                           
                            <asp:FilteredTextBoxExtender ID="FTETxtMontoChatarrizacion" 
                                runat="server" 
                                TargetControlID="TxtMontoChatarrizacion" 
                                FilterType="Custom" 
                                FilterMode="ValidChars" 
                                ValidChars="0123456789." FilterInterval="10" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtMontoChatarrizacion" 
                                ErrorMessage="Se debe capturar el Monto de Chatarrización"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredTxtMontoChatarrizacion"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Equipos Baja:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDXEquiposBaja" runat="server" CssClass="DropDownList"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="DDXEquiposBaja" 
                                ErrorMessage="Se debe seleccionar el Número de equipos de baja"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredDDXEquiposBaja" 
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Equipos Alta:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDXEquiposAlta" runat="server" CssClass="DropDownList"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="DDXEquiposAlta" 
                                ErrorMessage="Se debe seleccionar el Número de equipos de alta"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredDDXEquiposAlta" 
                                InitialValue="-1">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Factor Sustitución:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDXFactorSustucion" runat="server" CssClass="DropDownList"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="DDXFactorSustucion" 
                                ErrorMessage="Se debe seleccionar el Factor de Sustitución"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredDDXFactorSustucion" 
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Tarifa:
                        </td>
                        <td>

                            <asp:CheckBoxList ID="ChkLstTarifas" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="ChkLstTarifas_SelectedIndexChanged" Font-Size="X-Small">
                            </asp:CheckBoxList>

                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Algoritmo:
                        </td>
                        <td>

                            <asp:CheckBoxList ID="ChkLstAgoritmos" runat="server" RepeatDirection="Horizontal" Enabled="False" Font-Size="X-Small">
                            </asp:CheckBoxList>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Incentivo:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDXIncentivo" 
                                runat="server" 
                                CssClass="DropDownList" 
                                OnSelectedIndexChanged="DDXIncentivo_SelectedIndexChanged" 
                                AutoPostBack="True">

                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="DDXIncentivo" 
                                ErrorMessage="Se debe seleccionar si hay Incentivo"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredDDXIncentivo"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Incentivo(Monto):
                        </td>
                        <td>
                            <asp:TextBox ID="TxtMontoIncentivo" Width="189px" runat="server" Enabled="False" MaxLength="5" >0.00</asp:TextBox>
                            %                            
                            <asp:FilteredTextBoxExtender ID="FTETxtMontoIncentivo" 
                                runat="server" 
                                TargetControlID="TxtMontoIncentivo" 
                                FilterMode="ValidChars" 
                                ValidChars="0123456789." />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="TxtMontoIncentivo" 
                                ErrorMessage="Se capturar el Monto del Incentivo"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredTxtMontoIncentivo"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Plantilla:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDXTipoPlantilla" runat="server" CssClass="DropDownList"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="DDXTipoPlantilla" 
                                ErrorMessage="Se debe seleccionar la Plantilla para la Tecnología"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator1"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr2">
                        <td>
                            Imprimir Leyenda Descriptiva:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDXLeyendaDescriptiva" runat="server" CssClass="DropDownList"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="DDXLeyendaDescriptiva" 
                                ErrorMessage="Se debe seleccionar si se imprime Leyenda Descriptiva"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredFieldValidator2"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            Combinación con Tecnologías:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDXCombinacionTecnologias" 
                                runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="DDXCombinacionTecnologias_SelectedIndexChanged" 
                                CssClass="DropDownList">

                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" 
                                ControlToValidate="DDXIncentivo" 
                                ErrorMessage="Se debe seleccionar si hay Combinación de Tecnologías"
                                ValidationGroup="Save" 
                                Display="Dynamic" 
                                Text="*"
                                EnableClientScript="true" 
                                ID="RequiredDDXCombinacionTecnologias"
                                InitialValue="">
                             </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:HiddenField ID="HiddenEstatus" runat="server" Value="0" />
                            <br />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <%--<td>
                        </td>--%>
                        <td colspan="2" style="vertical-align: middle; text-align: center" align="center">
                            <asp:CheckBoxList ID="ChkLstTecnologiasCombiladas" runat="server" Visible="False" Font-Size="X-Small" ForeColor="#333333" BackColor="#F7F6F3" Width="100%"></asp:CheckBoxList>
                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="NuevaTecnologiaValidationSummary" runat="server" CssClass="failureNotification" 
                                 ValidationGroup="Save" Font-Size="X-Small" HeaderText="Resumén Captura:"/>
                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" 
                                OnClientClick="return ValidaTarifas();" OnClick="BtnGuardar_Click" />
                        </td>

                        <td align="center">
                            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </div>
            </asp:Panel>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
