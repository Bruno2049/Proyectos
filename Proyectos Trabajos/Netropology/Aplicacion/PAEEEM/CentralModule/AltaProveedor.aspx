<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaProveedor.aspx.cs" Inherits="PAEEEM.CentralModule.AltaProveedor" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function poponload(nocre, tipo, consulta) {
            var testwindow = window.open('VisorImagenes.aspx?Id=' + nocre + '&Tipo=' + tipo + '&Referencia=' + consulta + '', '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
            testwindow.moveTo(0, 0);
        }

        function confirmacion() {
            var selec = confirm("Si usted sale se cancelara el registro y no se guardara información, ¿ Desea continuar ?");
            if (selec)
                //alert("Salir");
                navigate("../RegionalModule/SupplierMonitor.aspx");

            return selec;
        }

        function poponload1() {
            var tipo = "1";
            var testwindow = window.open('VisorImagenes.aspx?Id=' + tipo, '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
            testwindow.moveTo(0, 0);
        }

        function poponload2() {
            var tipo = "2";
            var testwindow = window.open('VisorImagenes.aspx?Id=' + tipo, '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
            testwindow.moveTo(0, 0);
        }

        function poponload3() {
            var tipo = "3";
            var testwindow = window.open('VisorImagenes.aspx?Id=' + tipo, '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
            testwindow.moveTo(0, 0);
        }

        function poponload4() {
            var tipo = "4";
            var testwindow = window.open('VisorImagenes.aspx?Id=' + tipo, '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
            testwindow.moveTo(0, 0);
        }

        function poponload5() {
            var tipo = "5";
            var testwindow = window.open('VisorImagenes.aspx?Id=' + tipo, '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
            testwindow.moveTo(0, 0);
        }



        function OnClientClicking(sender, args) {
            var event = args.get_domEvent();
            if (event.keyCode == 13) {
                args.set_cancel(true);
            }

        }

    </script>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            var pre = "ctl00_MainContent_";
            function postackControl() {
                var oButton = document.getElementById(pre + "btnRefresh2");
                oButton.click();
            }

        </script>
    </telerik:RadScriptBlock>
    <style type="text/css">
        .auto-style1
        {
            width: 199px;
        }

        .auto-style2
        {
            width: 188px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel2" Width="100%">
        <div>
            <%--style="width: 100%"--%>
            <fieldset class="fieldset_Netro">
                <table style="height: 100px">

                    <tr>
                        <td colspan="4">
                            <asp:Label runat="server" Text="ALTA PROVEEDOR, DISTRIBUIDOR O ESEI" Font-Bold="true" ForeColor="#00A3D9"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lbl_tipoProveedor" runat="server" Text="TIPO DE PROVEEDOR: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="rad_cmbTipoPro" AutoPostBack="true" OnSelectedIndexChanged="rad_cmbTipoPro_SelectedIndexChanged"></telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="TIPO DE PERSONA: " ID="lbl_TipoPersona" Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="rad_cmbTipoPersona" AutoPostBack="true" OnSelectedIndexChanged="rad_cmbTipoPersona_SelectedIndexChanged"></telerik:RadComboBox>
                        </td>
                        <%--<td>

                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          
                      </td> --%>
                    </tr>
                </table>

                <table style="height: 100px">


                    <tr>
                        <td colspan="6">
                            <asp:Label ID="Label1" runat="server" Text="DATOS GENERALES" Font-Bold="true" ForeColor="#00A3D9"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label runat="server" Text="NOMBRE(S): " ID="lbl_RAD_Nombre_Razon" Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtNombre" Style="text-transform: uppercase"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_txtNombre" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator1"> </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="APELLIDO PATERNO: " ID="lbl_RAD_ApePAT" Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtApePat" Style="text-transform: uppercase"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_txtApePat" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator2"> </asp:RequiredFieldValidator>

                        </td>
                        <td>
                            <asp:Label ID="lbl_RAD_ApeMat" runat="server" Text="APELLIDO MATERNO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtApeMat" Style="text-transform: uppercase"></telerik:RadTextBox>
                        </td>

                    </tr>

                    <tr>

                        <td>
                            <asp:Label ID="lbl_RAD_Fecha" runat="server" Text="FECHA DE NACIMIENTO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDatePicker runat="server" ID="RAD_DTP" AutoPostBack="true" DateInput-AutoPostBack="true" OnSelectedDateChanged="RAD_DTP_SelectedDateChanged"></telerik:RadDatePicker>
                        </td>

                        <td>
                            <asp:Label ID="Label4" runat="server" Text="RFC: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtRFC" AutoPostBack="true" OnTextChanged="RAD_txtRFC_TextChanged" Style="text-transform: uppercase"></telerik:RadTextBox>
                        </td>


                    </tr>
                </table>

                <table style="height: 100px">
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="Label5" runat="server" Text="DOMICILIO DEL NEGOCIO" Font-Bold="true" ForeColor="#00A3D9"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label runat="server" Text="CÓDIGO POSTAL: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox runat="server" ID="RAD_txtNCodPos" OnTextChanged="RAD_txtCodPos_TextChanged" AutoPostBack="true" MaxLength="5" NumberFormat-AllowRounding="false" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_txtNCodPos" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator3"> </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="ESTADO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RAD_cmbEstados" AutoPostBack="true" OnSelectedIndexChanged="RAD_cmbEstados_SelectedIndexChanged1"></telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_cmbEstados" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator4"> </asp:RequiredFieldValidator>
                        </td>

                        <td>
                            <asp:Label ID="Label7" runat="server" Text="DELEGACIÓN O MUNICIPIO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" AutoPostBack="true" ID="RAD_cmbDelMun" OnSelectedIndexChanged="RAD_cmbDelMun_SelectedIndexChanged"></telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_cmbDelMun" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator5"> </asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="COLONIA: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RAD_cmbColonia" OnSelectedIndexChanged="RAD_cmbColonia_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_cmbColonia" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator6"> </asp:RequiredFieldValidator>

                        </td>

                        <td>
                            <asp:Label ID="Label9" runat="server" Text="CALLE: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtCalle" Style="text-transform: uppercase"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_txtCalle" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator7"> </asp:RequiredFieldValidator>
                        </td>

                        <td>
                            <asp:Label ID="Label10" runat="server" Text="NUMERO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>&nbsp;<telerik:RadTextBox runat="server" ID="RAD_txtNumero"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_txtNumero" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator8"> </asp:RequiredFieldValidator>
                        </td>

                    </tr>
                </table>

                <table style="height: 100px">
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="Label11" runat="server" Text="DOMICILIO FISCAL" Font-Bold="true" ForeColor="#00A3D9"></asp:Label>
                        </td>
                    </tr>


                    <tr>
                        <td colspan="4">
                            <asp:Label runat="server" Text="¿EL DOMICILIO FISCAL ES EL MISMO QUE EL DOMICILIO DEL NEGOCIO? " Font-Size="Small"> </asp:Label>&nbsp;
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RAD_cmbSiNoDomicilio" AutoPostBack="true" OnSelectedIndexChanged="RAD_cmbSiNoDomicilio_SelectedIndexChanged"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="CODIGO POSTAL: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox runat="server" ID="RAD_FIS_nCodPos" OnTextChanged="RAD_FIS_CodPos_TextChanged" AutoPostBack="true" MaxLength="5" NumberFormat-AllowRounding="false" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_FIS_nCodPos" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator9"> </asp:RequiredFieldValidator>

                        </td>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="ESTADO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RAD_FIS_cmbEstados" AutoPostBack="true" OnSelectedIndexChanged="RAD_FIS_cmbEstados_SelectedIndexChanged"></telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_FIS_cmbEstados" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator10"> </asp:RequiredFieldValidator>
                        </td>

                        <td>
                            <asp:Label ID="Label14" runat="server" Text="DELEGACIÓN O MUNICIPIO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RAD_FIS_DelegOMuni" AutoPostBack="true" OnSelectedIndexChanged="RAD_FIS_DelegOMuni_SelectedIndexChanged"></telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_FIS_DelegOMuni" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator11"> </asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="COLONIA: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RAD_FIS_Colonia" AutoPostBack="true" OnSelectedIndexChanged="RAD_FIS_Colonia_SelectedIndexChanged"></telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_FIS_Colonia" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator12"> </asp:RequiredFieldValidator>
                        </td>

                        <td>
                            <asp:Label ID="Label16" runat="server" Text="CALLE: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_FIS_calle" Style="text-transform: uppercase"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_FIS_calle" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator13"> </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="NUMERO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_FIS_NUMERO"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_FIS_NUMERO" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator14"> </asp:RequiredFieldValidator>
                        </td>


                    </tr>
                </table>

                <table style="height: auto">
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="Label18" runat="server" Text="INFORMACIÓN DEL RESPONSABLE/REPRESENTANTE LEGAL" Font-Bold="true" ForeColor="#00A3D9" Font-Size="Small"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label runat="server" Text="RESPONSABLE: " Font-Bold="true" Font-Size="Small"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label runat="server" Text="NOMBRE(S): " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtNomRES" Style="text-transform: uppercase"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_txtNomRES" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator15"> </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lbl_apePat_RES" runat="server" Text="APELLIDO PATERNO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtAPRES" Style="text-transform: uppercase"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_txtAPRES" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator16"> </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lbl_apeMat_RES" runat="server" Text="APELLIDO MATERNO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtAMRES" Style="text-transform: uppercase"></telerik:RadTextBox>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text="CORREO ELECTRONICO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_ktxtCorreoRES"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_ktxtCorreoRES" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="txtRepresentativeEmailRequired"> </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$"
                                ControlToValidate="RAD_ktxtCorreoRES" ErrorMessage="(*) Formato Inválido" ID="RegularExpressionValidator10"> </asp:RegularExpressionValidator>

                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text="TELÉFONO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox runat="server" ID="RAD_NtxtTelefonoRES" MaxLength="10" NumberFormat-AllowRounding="false" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_NtxtTelefonoRES" ErrorMessage="(*) Campos Obligatorios"
                                ValidationGroup="Save" ID="RequiredFieldValidator17"> </asp:RequiredFieldValidator>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label23" runat="server" Text="REPRESENTANTE LEGAL: " Font-Bold="true"></asp:Label>

                        </td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text="NOMBRE(S): " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtNomRL" Style="text-transform: uppercase"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_txtNomRL" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator18"> </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lbl_apePat_RL" runat="server" Text="APELLIDO PATERNO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtAPRL" Style="text-transform: uppercase"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RAD_txtAPRL" ErrorMessage="(*)"
                                ValidationGroup="Save" ID="RequiredFieldValidator19"> </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lbl_apeMat_RL" runat="server" Text="APELLIDO MATERNO: " Font-Size="Small"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtAMRL" Style="text-transform: uppercase"></telerik:RadTextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label27" runat="server" Text="INFORMACIÓN FINACIERA" Font-Bold="true" ForeColor="#00A3D9"></asp:Label>

                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <asp:Label runat="server" Text="INFORMACIÓN FINANCIERA DEPOSITO" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label runat="server" Text="NOMBRE DEL BANCO: "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtNombreBanco" Style="text-transform: uppercase">
                            </telerik:RadTextBox>

                        </td>
                        <td>
                            <asp:Label runat="server" Text="NÚMERO DE CUENTA CLABE: "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="RAD_txtNumClabe"></telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label28" runat="server" Text="CARGA DOCUMENTOS" Font-Bold="true" ForeColor="#00A3D9"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="auto-style2">
                            <asp:Label runat="server" Text="PODER NOTARIAL: "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadAsyncUpload runat="server" ID="RAD_UPPoderNot" AllowedFileExtensions=".pdf" OnFileUploaded="RAD_UPPoderNot_FileUploaded"
                                Style="margin-left: 14px" Height="17px" Width="263px"
                                OnClientFilesUploaded="postackControl">
                            </telerik:RadAsyncUpload>
                            <asp:Image ID="ok1" runat="server" Height="25px" Width="27px" Visible="false" />
                            <asp:ImageButton Visible="false" runat="server" ID="verPN" OnClientClick="OnClientClicking;poponload1()" />
                            <asp:ImageButton Visible="false" runat="server" ID="ver_POD_TEMP" OnClientClick="OnClientClicking;poponload4()" />

                        </td>
                        <td class="auto-style1">
                            <asp:Label ID="Label29" runat="server" Text="ACTA CONSTITUTIVA: "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadAsyncUpload runat="server" ID="RAD_UPActa_Const" AllowedFileExtensions=".pdf"
                                OnFileUploaded="RAD_UPActa_Const_FileUploaded" OnClientFilesUploaded="postackControl"
                                Style="margin-left: 14px" Height="17px" Width="263px">
                            </telerik:RadAsyncUpload>
                            <asp:Image ID="ok2" runat="server" Height="25px" Width="27px" Visible="false" />
                            <asp:ImageButton Visible="false" runat="server" ID="verAC" OnClientClick="OnClientClicking;poponload2()" OnClick="verAC_Click" />
                            <asp:ImageButton Visible="false" runat="server" ID="ver_ACTA_TEMP" OnClientClick="OnClientClicking;poponload5()" OnClick="verAC_Click" />
                        </td>
                    </tr>
                </table>
                <table style="width: 519px">
                    <tr>
                        <td>
                            <asp:Label ID="Label30" runat="server" Text="GENERAL" Font-Bold="true" ForeColor="#00A3D9"></asp:Label>
                        </td>
                    </tr>

                    <tr>

                        <td>
                            <asp:Label runat="server" Text="IVA: "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="IVA"></telerik:RadComboBox>
                        </td>
                        <td>&nbsp;&nbsp;<asp:Label runat="server" Text="ZONA:  "></asp:Label>
                        </td>
                        <td style="text-align: right">
                            <telerik:RadComboBox runat="server" ID="rad_CMB_ZONA"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_Motivos" runat="server" Text="MOTIVOS" Font-Bold="true" Visible="false" ForeColor="#00A3D9"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4" style="text-align: center">
                            <telerik:RadTextBox ID="txt_Motivos_Editar" runat="server" TextMode="MultiLine" Skin="Windows7" Visible="false">
                            </telerik:RadTextBox>
                         <%--   <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_Motivos_Editar" ErrorMessage="(*) Campo Obligatorio"
                                ValidationGroup="Save" ID="RequiredFieldValidator20"> </asp:RequiredFieldValidator>--%>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                        <td style="text-align: right">
                            <telerik:RadButton ID="RAD_btnFinalizar" runat="server" Text="FINALIZAR" OnClick="RAD_btnFinalizar_Click">
                            </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadButton runat="server" Visible="false" ID="RAD_EDITAR_FINALIZAR" Text="FINALIZAR" OnClick="RAD_EDITAR_FINALIZAR_Click"></telerik:RadButton>
                        </td>
                        <td style="text-align: right">
                            <telerik:RadButton ID="RAD_btnSalir" runat="server" Text="SALIR" OnClientClicked="confirmacion" Style="top: 0px; left: 1px">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>

        <div id="div_RGV" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" Text="REGISTRO DE SUCURSALES FISÍCAS" Font-Size="Large" Visible="false" ID="lbl_FIS"> </asp:Label>
                    </td>
                </tr>
            </table>

            <telerik:RadGrid ID="RG_SUC_FIS" runat="server"
                Visible="false"
                AutoGenerateColumns="False"
                CellSpacing="0"
                GridLines="None"
                Skin="Office2010Silver"
                Culture="es-MX"
                OnInsertCommand="RG_SUC_FIS_InsertCommand"
                OnUpdateCommand="RG_SUC_FIS_UpdateCommand"
                OnNeedDataSource="RG_SUC_FIS_NeedDataSource"
                OnItemDataBound="RG_SUC_FIS_ItemDataBound"
                Font-Size="Small"
                AllowPaging="true" PageSize="20">
                <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" />
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting CellSelectionMode="None" />
                </ClientSettings>
                <MasterTableView DataKeyNames="Id_Branch" EditMode="EditForms" NoMasterRecordsText="No hay información de Sucursales Fisícas"
                    CommandItemDisplay="Top" AllowAutomaticUpdates="False">
                    <CommandItemSettings ExportToPdfText="Export to PDF"
                        AddNewRecordText="Agregar Registro" />
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" FilterControlAltText="Filter EditCommandColumn column">
                            <HeaderStyle Width="40" />
                        </telerik:GridEditCommandColumn>

                        <telerik:GridTemplateColumn DataField="Id_Proveedor" FilterControlAltText="Filter colIdEquipoMedicion column" HeaderText="ID SUCURSAL FÍSICA" UniqueName="idSucursal" Visible="true">
                            <EditItemTemplate>
                                <asp:Label ID="txt_FIS_idBranch" runat="server" Text='<%# Bind("Id_Branch") %>' Enabled="false"> 
                                </asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Id_Branch")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="NombreComercial" FilterControlAltText="Filter colNombreComercial column" HeaderText="NOMBRE COMERCIAL" UniqueName="nombreComercial">

                            <EditItemTemplate>
                                <telerik:RadTextBox ID="txtNombreComercial_edit" runat="server" Width="300px" Style="text-transform: uppercase" OnTextChanged="txtNombreComercial_TextChanged" AutoPostBack="true" Text='<%# Bind("Dx_Nombre_Comercial") %>'>
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfv_nombreComercial" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="txtNombreComercial_edit"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Dx_Nombre_Comercial")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="codigoPostal" FilterControlAltText="Filter colcodigoPostal column" HeaderText="CODIGO POSTAL" UniqueName="codigoPostal">

                            <EditItemTemplate>
                                <telerik:RadNumericTextBox ID="GV_txt_CodigoPostal_FIS_edit" runat="server" Width="100px" Text='<%# Bind("Dx_Domicilio_Part_CP") %>' OnTextChanged="GV_txt_CodigoPostal_FIS_TextChanged" AutoPostBack="true" MaxLength="5" NumberFormat-AllowRounding="false" NumberFormat-GroupSeparator="">
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvCodigoPostal" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="GV_txt_CodigoPostal_FIS_edit"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Dx_Domicilio_Part_CP")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="estado" FilterControlAltText="Filter colEstado column" HeaderText="ESTADO" UniqueName="Estado">

                            <EditItemTemplate>
                                <telerik:RadComboBox ID="FIS_estado_GV" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FIS_estado_GV_SelectedIndexChanged"></telerik:RadComboBox>
                                <asp:Label ID="lbl_ESTADO_FIS" Text='<%# Bind("Cve_Estado_Part") %>' runat="server" Visible="false"></asp:Label>

                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Dx_Nombre_Estado")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="delegacionoMunicipio" FilterControlAltText="Filter delegacionoMunicipio column" HeaderText="DELEGACION O MUNICIPIO" UniqueName="delegacionoMunicipio">

                            <EditItemTemplate>
                                <telerik:RadComboBox ID="GV_cmbDelegoMunic" runat="server" AutoPostBack="true" OnSelectedIndexChanged="GV_cmbDelegoMunic_SelectedIndexChanged">
                                </telerik:RadComboBox>
                                <asp:Label ID="lbl_DELEG_FIS" Text='<%# Bind("Cve_Deleg_Municipio_Part") %>' runat="server" Visible="false"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Dx_Deleg_Municipio")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="colonia" FilterControlAltText="Filter colonia column" HeaderText="COLONIA" UniqueName="colonia">
                            <EditItemTemplate>
                                <telerik:RadComboBox ID="gv_fisica_cmbcolonia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gv_fisica_cmbcolonia_SelectedIndexChanged">
                                </telerik:RadComboBox>
                                <asp:Label ID="lbl_Colonia_FIS" Text='<%# Bind("Dx_Domicilio_Part_CP") %>' runat="server" Visible="false"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Dx_Colonia")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="calle" FilterControlAltText="Filter calle column" HeaderText="CALLE" UniqueName="calle">

                            <EditItemTemplate>
                                <telerik:RadTextBox ID="gv_fisica_cmbcalle" runat="server" Style="text-transform: uppercase" Text='<%# Bind("Dx_Domicilio_Part_Calle") %>'>
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfv_gv_cmbcalle" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="gv_fisica_cmbcalle" />
                            </EditItemTemplate>
                            <ItemTemplate>

                                <%#DataBinder.Eval(Container.DataItem, "Dx_Domicilio_Part_Calle")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="numero" FilterControlAltText="Filter numero column" HeaderText="NUMERO" UniqueName="numero">

                            <EditItemTemplate>
                                <telerik:RadTextBox ID="gv_fisica_txtnumero" runat="server" Text='<%# Bind("Dx_Domicilio_Part_Num") %>'>
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfv_gv_txtnumero" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="gv_fisica_txtnumero" />
                            </EditItemTemplate>
                            <ItemTemplate>

                                <%#DataBinder.Eval(Container.DataItem, "Dx_Domicilio_Part_Num")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="nombreRes" FilterControlAltText="Filter nombreRes column" HeaderText="NOMBRE (RESPONSABLE)" UniqueName="nombreRes">

                            <EditItemTemplate>
                                <telerik:RadTextBox ID="gv_fisica_txtNombre" runat="server" Width="300px" Style="text-transform: uppercase" Text='<%# Bind("Dx_Nombre_Repre") %>'>
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfv_ge" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="gv_fisica_txtNombre" />
                            </EditItemTemplate>
                            <ItemTemplate>

                                <%#DataBinder.Eval(Container.DataItem, "Dx_Nombre_Repre")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                         <telerik:GridTemplateColumn DataField="apRes" FilterControlAltText="Filter apRes column" HeaderText="APELLIDO PATERNO (RESPONSABLE)" UniqueName="apRes">

                            <EditItemTemplate>
                                <telerik:RadTextBox ID="gv_fisica_txtAP_res" runat="server" Width="300px" Style="text-transform: uppercase" Text='<%# Bind("Apellido_Paterno_Resp") %>'>
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfv_gv_txtN" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="gv_fisica_txtAP_res" />
                            </EditItemTemplate>
                            <ItemTemplate>

                                <%#DataBinder.Eval(Container.DataItem, "Apellido_Paterno_Resp")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                          <telerik:GridTemplateColumn DataField="amRes" FilterControlAltText="Filter amRes column" HeaderText="APELLIDO MATERNO (RESPONSABLE)" UniqueName="amRes">

                            <EditItemTemplate>
                                <telerik:RadTextBox ID="gv_fisica_txtAM_res" runat="server" Width="300px" Style="text-transform: uppercase" Text='<%# Bind("Apellido_Materno_Resp") %>'>
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfv_gv_re" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="gv_fisica_txtAM_res" />
                            </EditItemTemplate>
                            <ItemTemplate>

                                <%#DataBinder.Eval(Container.DataItem, "Apellido_Materno_Resp")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <%--  --%>
                        <telerik:GridTemplateColumn DataField="correo" FilterControlAltText="Filter correo column" HeaderText="CORREO ELECTRONICO" UniqueName="correo">

                            <EditItemTemplate>
                                <telerik:RadTextBox ID="gv_fisica_txtcorreo" runat="server" Text='<%# Bind("Dx_Email_Repre") %>'>
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfv_gv_txtcorreo" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="gv_fisica_txtcorreo" />

                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$"
                                    ControlToValidate="gv_fisica_txtcorreo" ErrorMessage="(*) Formato Inválido" ID="RegularExpressionValidator99"> </asp:RegularExpressionValidator>

                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Dx_Email_Repre")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="telefono" FilterControlAltText="Filter telefono column" HeaderText="TELEFONO" UniqueName="telefono">

                            <EditItemTemplate>
                                <telerik:RadNumericTextBox ID="gv_fisica_txttelefono" runat="server" Text='<%# Bind("Dx_Telefono_Repre") %>' MaxLength="10" NumberFormat-AllowRounding="false" NumberFormat-GroupSeparator="">
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfv_gv_txttelefono" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="gv_fisica_txttelefono" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Dx_Telefono_Repre")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>


                        <telerik:GridTemplateColumn DataField="nombreRL" FilterControlAltText="Filter nombreRL column" HeaderText="NOMBRE REPRESENTANTE LEGAL" UniqueName="nombreRL">

                            <EditItemTemplate>
                                <telerik:RadTextBox ID="gv_fisica_txtnombreRL" runat="server" Width="300px" Style="text-transform: uppercase" Text='<%# Bind("Dx_Nombre_Repre_Legal") %>'>
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfv_gv_txtnombreRL" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="gv_fisica_txtnombreRL" />
                            </EditItemTemplate>
                            <ItemTemplate>

                                <%#DataBinder.Eval(Container.DataItem, "Dx_Nombre_Repre_Legal")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <%--  --%>
                           <telerik:GridTemplateColumn DataField="apRL" FilterControlAltText="Filter apRL column" HeaderText="APELLIDO PATERNO (REP. LEG.)" UniqueName="apRL">

                            <EditItemTemplate>
                                <telerik:RadTextBox ID="gv_fisica_txtAP_RL" runat="server" Width="300px" Style="text-transform: uppercase" Text='<%# Bind("Apellido_Paterno_Rep_Legal") %>'>
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvbre" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="gv_fisica_txtAP_RL" />
                            </EditItemTemplate>
                            <ItemTemplate>

                                <%#DataBinder.Eval(Container.DataItem, "Apellido_Paterno_Rep_Legal")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                          <telerik:GridTemplateColumn DataField="amRL" FilterControlAltText="Filter amRL column" HeaderText="APELLIDO MATERNO (REP. LEG.)" UniqueName="amRL">

                            <EditItemTemplate>
                                <telerik:RadTextBox ID="gv_fisica_txtAM_RL" runat="server" Width="300px" Style="text-transform: uppercase" Text='<%# Bind("Apellido_Materno_Rep_Legal") %>'>
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="re" runat="server" ErrorMessage="*" ValidationGroup="SucFisAlta" ControlToValidate="gv_fisica_txtAM_RL" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Apellido_Materno_Rep_Legal")%>
                                </ItemTemplate>
                              </telerik:GridTemplateColumn>
                        <%--  --%>

                        <telerik:GridTemplateColumn DataField="podernotarial" FilterControlAltText="Filter podernotarial column" HeaderText="PODER NOTARIAL" UniqueName="podernotarial">

                            <EditItemTemplate>
                                <telerik:RadAsyncUpload Visible="true"
                                    runat="server"
                                    ID="cargaPoderNotarial"
                                    Style="margin-left: auto" Width="80%"
                                    AllowedFileExtensions=".pdf"
                                    MaxFileInputsCount="1"
                                    OnFileUploaded="cargaPoderNotarial_FileUploaded"
                                    OnClientFilesUploaded="postackControl">
                                </telerik:RadAsyncUpload>
                                <asp:ImageButton Visible="false" runat="server" ID="gv_verPODERNOT" OnClientClick="OnClientClicking;poponload3()" />
                                
                                <asp:ImageButton runat="server" ID="gv_EliminarPoder"
                                    ImageUrl="~/CirculoCredito/imagenes/delete.gif" OnClick="gv_EliminarPoder_Click" Visible="false" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton Visible="false" runat="server" ID="gv_verPODERNOT_e" OnClientClick="OnClientClicking;poponload3()" ImageUrl="~/CentralModule/images/visualizar.png" />
                                <asp:ImageButton Visible="false" runat="server" ID="ver_actaNotarial_FIS" ImageUrl = "~/CentralModule/images/visualizar.png" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="zona" FilterControlAltText="Filter zona column" HeaderText="ZONA" UniqueName="zona">

                            <EditItemTemplate>
                                <telerik:RadComboBox ID="gv_fisica_cmb_zona" runat="server">
                                </telerik:RadComboBox>
                                <asp:Label ID="lbl_Zona_FIS" Text='<%# Bind("Cve_Zona") %>' runat="server" Visible="false"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>

                                <%#DataBinder.Eval(Container.DataItem, "Dx_Nombre_Zona")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn DataField="motivos" FilterControlAltText="Filter motivos column" Visible = "False"  UniqueName="motivoFIS">

                                        <EditItemTemplate>                                           
                                                <telerik:RadTextBox ID="txt_Motivos_Editar_FIS" runat="server" EmptyMessage="MOTIVOS" TextMode="MultiLine" Skin="Windows7" Visible="false" Width="300px">
                                                </telerik:RadTextBox>
                                                                                           
                                        </EditItemTemplate>                                       
                                    </telerik:GridTemplateColumn>

                    </Columns>
                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ValidationSettings EnableValidation="true" ValidationGroup="SucFisAlta"></ValidationSettings>
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
            </telerik:RadGrid>

            <table>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" Font-Size="Large" Text="REGISTRO DE SUCURSALES VIRTUALES" Visible="false" ID="lbl_VIR"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="RG_SUC_VIRTUALES" runat="server"
                            AutoGenerateColumns="false"
                            Visible="false"
                            CellSpacing="0"
                            GridLines="None"
                            Skin="Office2010Silver"
                            Culture="es-MX"
                            OnInsertCommand="RG_SUC_VIRTUALES_InsertCommand"
                            OnNeedDataSource="RG_SUC_VIRTUALES_NeedDataSource"
                            OnItemDataBound="RG_SUC_VIRTUALES_ItemDataBound"
                            OnUpdateCommand="RG_SUC_VIRTUALES_UpdateCommand"
                            Font-Size="Small" PageSize="20">
                            <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom" />
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting CellSelectionMode="None" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="Id_Branch" EditMode="EditForms" NoMasterRecordsText="No hay información de Sucursales virtuales"
                                CommandItemDisplay="Top" AllowAutomaticUpdates="False">
                                <CommandItemSettings ExportToPdfText="Export to PDF"
                                    AddNewRecordText="Agregar Registro" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" FilterControlAltText="Filter EditCommandColumn column">
                                        <HeaderStyle Width="40" />
                                    </telerik:GridEditCommandColumn>

                                    <telerik:GridTemplateColumn DataField="Id_Sucursal_VIR" FilterControlAltText="Filter colIdEquipoMedicion column" HeaderText="ID SUCURSAL VIRTUAL" UniqueName="idSucursalVir">
                                        <EditItemTemplate>
                                            <asp:Label ID="GV_VIR_ID" runat="server" Text='<%# Bind("Id_Branch") %>' Enabled="false">
                             
                                            </asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Id_Branch")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="region" FilterControlAltText="Filter region column" HeaderText="REGIÓN" UniqueName="regionVir">
                                        <EditItemTemplate>
                                            <telerik:RadComboBox ID="GV_VIR_cmbregion" runat="server" OnSelectedIndexChanged="GV_VIR_cmbregion_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                                            <asp:Label ID="lbl_REGION_VIRTUAL" Text='<%# Bind("Cve_Region") %>' runat="server" Visible="false"></asp:Label>

                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Dx_Nombre_Region")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="zona" FilterControlAltText="Filter zona column" HeaderText="ZONA" UniqueName="zonaVir">
                                        <EditItemTemplate>
                                            <telerik:RadComboBox ID="gv_VIR_cmbZona" runat="server">
                                            </telerik:RadComboBox>
                                            <asp:Label ID="lbl_ZONA_VIRTUAL" Text='<%# Bind("Cve_Zona") %>' runat="server" Visible="false"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Dx_Nombre_Zona")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="asociaraVir" FilterControlAltText="Filter asociaraVir column" HeaderText="ASOCIAR A" UniqueName="asociaraVir">
                                        <EditItemTemplate>
                                            <telerik:RadComboBox ID="CMB_asociarA" runat="server" OnSelectedIndexChanged="CMB_asociarA_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="rfvGV_cmbAsociar" runat="server" ValidationGroup="GV_SUC_VIRTUAL" ControlToValidate="CMB_asociarA" ErrorMessage="*" />
                                            <asp:Label ID="lbl_idVinculado_VIRTUAL" Text='<%# Bind("id_Dependencia") %>' runat="server" Visible="false"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "DX_Nombre_Vinculado")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="estado" FilterControlAltText="Filter estado column" HeaderText="ESTADO" UniqueName="estadoVir">
                                        <EditItemTemplate>
                                            <telerik:RadComboBox ID="gv_VIR_ESTADO" runat="server" Enabled="false">
                                            </telerik:RadComboBox>
                                            <asp:Label ID="lbl_Estado_VIRTUAL" Text='<%# Bind("Cve_Estado_Part") %>' runat="server" Visible="false"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Dx_Nombre_Estado")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="delegMunicipio" FilterControlAltText="Filter estado column" HeaderText="DELEGACIÓ O MUNICIPIO" UniqueName="delegVir">
                                        <EditItemTemplate>
                                            <telerik:RadComboBox ID="gv_VIR_DELEG" runat="server" Enabled="false">
                                            </telerik:RadComboBox>
                                            <asp:Label ID="lbl_Delegacion_VIRTUAL" Text='<%# Bind("Cve_Deleg_Municipio_Part") %>' runat="server" Visible="false"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Dx_Deleg_Municipio")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="colonia" FilterControlAltText="Filter estado column" HeaderText="COLONIA" UniqueName="coloniaVir">
                                        <EditItemTemplate>
                                            <telerik:RadComboBox ID="gv_VIR_colonia" runat="server" Enabled="false">
                                            </telerik:RadComboBox>
                                            <asp:Label ID="lbl_Colonia_VIRTUAL" Text='<%# Bind("Dx_Domicilio_Part_CP") %>' runat="server" Visible="false"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Dx_Colonia")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>


                                    <telerik:GridTemplateColumn DataField="NombreComercial" FilterControlAltText="Filter colNombreComercial column" HeaderText="NOMBRE COMERCIAL" UniqueName="nombreComercial">

                                        <EditItemTemplate>
                                            <telerik:RadTextBox ID="txt_VIR_NombreComercial" Width="300px" runat="server" Style="text-transform: uppercase" OnTextChanged="txt_VIR_NombreComercial_TextChanged" AutoPostBack="true" Text='<%# Bind("Dx_Nombre_Comercial") %>'>
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="rfv_nombreComercial" runat="server" ErrorMessage="*" ValidationGroup="GV_SUC_VIRTUAL" ControlToValidate="txt_VIR_NombreComercial"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Dx_Nombre_Comercial")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="IdSucFIS" FilterControlAltText="Filter IdSucFIS column" HeaderText="ID SUCURSAL FISICA" UniqueName="IdSucFIS">

                                        <EditItemTemplate>
                                            <asp:Label ID="lbl_IDFIS" runat="server" Text='<%# Bind("id_S_FIS") %>'></asp:Label>
                                                                                         
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "id_S_FIS")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                     <telerik:GridTemplateColumn DataField="motivos" FilterControlAltText="Filter motivos column" Visible = "False"  UniqueName="motivoCol">

                                        <EditItemTemplate>                                           
                                                <telerik:RadTextBox ID="txt_Motivos_Editar_Virtual" runat="server" EmptyMessage="MOTIVOS" TextMode="MultiLine" Skin="Windows7" Visible="false" Width="300px">
                                                </telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txt_Motivos_Editar_Virtual" ErrorMessage="(*) Campo Obligatorio">
                                <ValidationGroup="Save" ID="RequiredFieldValidator20"> </asp:RequiredFieldValidator>                                            
                                        </EditItemTemplate>                                       
                                    </telerik:GridTemplateColumn>


                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <ValidationSettings EnableValidation="true" ValidationGroup="GV_SUC_VIRTUAL"></ValidationSettings>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
            <div style="text-align: center">
                <telerik:RadButton Text="SALIR" runat="server" ID="btn_Salir_Sucursal" Visible="false" OnClick="btn_Salir_Sucursal_Click"></telerik:RadButton>
            </div>
        </div>
        <asp:HiddenField ID="loadPDF" runat="server" />
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Office2010Silver">
    </telerik:RadAjaxLoadingPanel>
    <div style="display: none">
        <asp:Button ID="btnRefresh2" runat="server" Text="Button"
            OnClick="btnRefresh2_Click" />
        <telerik:RadTextBox ID="hiddentext" runat="server"></telerik:RadTextBox>
    </div>

    <telerik:RadWindowManager ID="RWM_vent" runat="server" Skin="Office2010Silver">
    </telerik:RadWindowManager>
</asp:Content>
