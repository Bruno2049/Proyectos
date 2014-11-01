<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidarVendedores.aspx.cs" Inherits="PAEEEM.Vendedores.ValidarVendedores" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function poponload(curp) {
            var testwindow = window.open('VisorImagenes.aspx?tipo=2&Id=' + curp + '', '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
            testwindow.moveTo(0, 0);
        }


        function OnClientClicking(sender, args) {
            var event = args.get_domEvent();
            if (event.keyCode == 13) {
                args.set_cancel(true);
            }
        }

        function confirmCallBackFn(arg) {
            if (arg == true) {
                var oButton = document.getElementById("ctl00_MainContent_" + "HiddenButton");
                oButton.click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelEncabezado" runat="server" Text="Validacion de Registro de vendedores"
        Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
    <hr class="ruleNet" />

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2010Blue"></telerik:RadAjaxLoadingPanel>
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel2" LoadingPanelID="RadAjaxLoadingPanel1" runat="server" Width="100%">--%>
    <fieldset class="fieldset_Netro">
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="CURP:" Font-Size="X-SMALL"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Nombre del Vendedor:" Font-Size="X-SMALL"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Estatus:" Font-Size="X-SMALL"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Region:" Font-Size="X-SMALL"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Zona:" Font-Size="X-SMALL"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadTextBox ID="txtCURP" runat="server"></telerik:RadTextBox>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtNombre" runat="server"></telerik:RadTextBox>
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cmbEstatus"></telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cmbRegion"></telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cmbZona"></telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Distribuidor NC:" Font-Size="X-SMALL"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Distribuidor RS:" Font-Size="X-SMALL"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadTextBox ID="txtDistNC" runat="server"></telerik:RadTextBox>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtDistRS" runat="server"></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <br />
                    <telerik:RadButton ID="btnBuscar" OnClick="btnBuscar_OnClick" runat="server" Text="Buscar" />
                    &nbsp;&nbsp;
                        <telerik:RadButton ID="btnSalir" runat="server" Text="Salir" OnClick="btnSalir_OnClick"></telerik:RadButton>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />

    <telerik:RadGrid ID="rgVendedores" runat="server" AutoGenerateColumns="False" Visible="True" AllowSorting="true"
        OnPageSizeChanged="rgVendedores_OnPageSizeChanged"
        OnPageIndexChanged="rgVendedores_OnPageIndexChanged"
        OnSortCommand="rgVendedores_OnSortCommand"
        GridLines="None" Skin="Office2010Silver" CellSpacing="0" PageSize="5" AllowPaging="True">
        <PagerStyle Mode="NextPrevAndNumeric" Position="Bottom"></PagerStyle>
        <ClientSettings EnableRowHoverStyle="True">
            <Selecting CellSelectionMode="None" />
        </ClientSettings>
        <MasterTableView ClientDataKeyNames="IdVendedor,CURP" NoMasterRecordsText="No se encontrarón Vendedores" Font-Size="X-SMALL"
            AutoGenerateColumns="False">

            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

            <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

            <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>

            <Columns>
                <telerik:GridBoundColumn DataField="IdDistribuidor" HeaderText="Id DISTRIBUIDOR" UniqueName="IdDistribuidor"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DistrNC" HeaderText="DISTRIBUIDOR NC"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DistrRS" HeaderText="DISTRIBUIDOR RS"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IdVendedor" HeaderText="ID VENDEDOR" UniqueName="IdVendedor"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Nombre" HeaderText="NOMBRE DEL VENDEDOR"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FechaNacimiento" HeaderText="FECHA NACIMIENTO" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Curp" HeaderText="CURP" UniqueName="Curp"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Region" HeaderText="REGION"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Zona" HeaderText="ZONA"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TipoIdentificacion" HeaderText="TIPO IDENTIFICACIÓN OFICIAL"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NoIdentificacion" HeaderText="NO. IDENTIFICACION"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AccesoSistema" HeaderText="ACCESO AL SISTEMA" UniqueName="AccesoSistema" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Incidencia" HeaderText="ANOMALIA"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Anomalia" HeaderText="TIPO ANOMALIA"></telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="ACCIONES">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="LSB_Acciones" runat="server" OnSelectedIndexChanged="LSB_Acciones_OnSelectedIndexChanged" AutoPostBack="True">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Elegir opción" Value="0" />
                            </Items>
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Estatus" HeaderText="ESTATUS" UniqueName="Estatus"></telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="ARCHIVO">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="verImagen" CommandName="Ver" ToolTip="Ver" Height="20px" Width="20px"
                            ImageUrl="~/CirculoCredito/imagenes/Buscar.png" Visible="True" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="SELECCIONAR" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="ckbSelect" runat="server" OnCheckedChanged="ckbSelect_OnCheckedChanged" AutoPostBack="True" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>

            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
            </EditFormSettings>
        </MasterTableView>

    </telerik:RadGrid>
    <br />
    <%--</telerik:RadAjaxPanel>--%>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button runat="server" ID="BtnAceptar" OnClick="BtnAceptar_OnClick" Text="Aceptar" />
            </td>
        </tr>
    </table>

    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server" Width="100%">
        <telerik:RadWindow ID="modalPopupCancelar" MaxHeight="420px" MaxWidth="500px" MinHeight="420px" MinWidth="500px" runat="server" Width="500px" Height="420px" Modal="true" Title="COMFIRMAR" AutoSize="False">
            <ContentTemplate>
                <div style="padding: 10px; text-align: center; background-color: lightskyblue;">
                    <table>
                        <tr>
                            <td>
                                <p style="text-align: center; font-style: italic; font-size: 24px">
                                    SI CANCELA EL VENDEDOR NO PODRA SER REGISTRADO NUEVAMENTE
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Label ID="observaciones" runat="server" Text="Anomalia:" /><br />
                                <asp:TextBox ID="txtAnomalia" runat="server" Height="110px" Width="360px" TextMode="MultiLine" MaxLength="100"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="reg" runat="server"
                                    ErrorMessage="Debe ingresar hasta un maximo de 100 caracteres"
                                    ValidationExpression="^([\S\s]{0,100})$"
                                    ControlToValidate="txtAnomalia"
                                    Display="Dynamic"></asp:RegularExpressionValidator><br />
                                <asp:Label ID="Label8" runat="server" Text="Maximo 100 caracteres" Font-Size="X-Small" /><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <asp:Button ID="btnAceptarVendedor" runat="server" Text="Aceptar" Enabled="True" OnClick="btnAceptarVendedor_OnClick" />&nbsp;&nbsp;
                                <asp:Button ID="btnCancelarVendedor" runat="server" Text="Cancelar" OnClick="btnCancelarVendedor_OnClick" Enabled="True" />
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>

        </telerik:RadWindow>

        <telerik:RadWindow ID="modalPopupUsuarios" MaxHeight="350px" MaxWidth="600px" MinHeight="350px" MinWidth="600px" runat="server" Width="600px" Height="350px" Modal="true" Title="COMFIRMAR" AutoSize="False">
            <ContentTemplate>
                <div>
                    <br>
                    <asp:Image ID="Image1" runat="server" ImageUrl="../SupplierModule/images/t_altausu.png" />
                </div>
                <br />
                <div>
                    <asp:Label ID="Label9" Text="Usuario" runat="server" CssClass="Label" />&nbsp
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="TextBox" Width="242px" />&nbsp<span style="color: Red; font-style: italic">(Campo Obligatorio)</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" ForeColor="black"
                        ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Label ID="Label10" Text="Correo Electrónico" runat="server" CssClass="Label" />&nbsp
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="TextBox" Width="279px" />&nbsp<span style="color: Red; font-style: italic">(Campo Obligatorio)</span>
                    <br />
                    <asp:RegularExpressionValidator ID="EmailValidator" ControlToValidate="txtEmail" ForeColor="black"
                        ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$"
                        runat="server" ValidationGroup="Save"></asp:RegularExpressionValidator>
                </div>
                <div>
                    <asp:Label ID="Label11" Text="Teléfono" runat="server" CssClass="Label" />&nbsp
                <asp:TextBox ID="txtPhone" runat="server" CssClass="TextBox" Width="248px" />&nbsp<span style="color: Red; font-style: italic">(10 Dígitos Numéricos)</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="black"
                        ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                        ValidationGroup="Save" ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="PhoneValidator" ControlToValidate="txtPhone" ForeColor="black"
                        ErrorMessage="(*) Campo Vacío o con Formato Inválido" runat="server" ValidationExpression="^((\d{10}))$"
                        ValidationGroup="Save" />
                </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <br />
                                <asp:Button ID="btnAceptarUsuario" runat="server" Text="Aceptar" Enabled="True" OnClick="btnAceptarUsuario_OnClick" />&nbsp;&nbsp;
                                <asp:Button ID="btnCancelarUsuario" runat="server" Text="Cancelar" OnClick="btnCancelarUsuario_OnClick" Enabled="True" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>

    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>

    <div style="display: none">
        <asp:Button ID="HiddenButton" BackColor="#FFFFFF" runat="server" Width="0px" OnClick="HiddenButton_OnClick" />
    </div>
</asp:Content>
