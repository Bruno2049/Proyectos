<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReceiptionOldEquipmentRegistry.aspx.cs"
    Inherits="PAEEEM.DisposalModule.ReceiptionOldEquipmentRegistry" Title="Registro de Ingreso de Equipo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Resources/Script/Calendar/WdatePicker.js" type="text/javascript"></script>
    <link href="../Resources/Script/Calendar/skin/default/datepicker.css" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 100%;
            color: #333333;
            font-size: 16px;
        }
        .TextBox
        {
            width: 50%;
        }
        .Button
        {
            width: 120px;
        }
        .style1
        {
            height: 26px;
        }
        .DropDownList
        {
            width: 50%;
        }
    </style>

    <script type="text/javascript">
      function check() {
            var hiddenButton = window.document.getElementById("<%=hidConfirm.ClientID%>");
            var comboBox = window.document.getElementById("<%=drpConformidad.ClientID%>");
            
            if (comboBox.value == "0") {
                if (confirm('Confirmar Selección. ¡ Esto puede causar la posible Cancelación del Crédito !')) {
                    hiddenButton.click();
                }
                else {
                    comboBox.value = "1";
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <br>
                <asp:Label ID="lblTitle" runat="server" Text="EQUIPO A RECIBIR" Font-Size="X-Large"></asp:Label>
            </div>
            <div id="dtFecha" align="right">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                &nbsp;<div>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="Wdate" Width="200px" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                </div>
            </div>
            <br />
            <div>
                <table width="100%">
                    <tr>
                        <td width="20%">
                            <asp:Label ID="lblPreOldEquipmentID" runat="server" Text="Folio Pre-boleta" CssClass="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPreOldEquipmentID" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblDistribuidor" runat="server" Text="Proveedor" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtDistribuidor" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblNameCeri" runat="server" Text="Nombre Comercial" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtNameCeri" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblCliente" runat="server" Text="Beneficiario" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtCliente" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblPrograma" runat="server" Text="Programa" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtPrograma" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblTecnología" runat="server" Text="Tecnología" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtTecnología" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblEstatus" runat="server" Text="Estatus" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtEstatus" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblCoordinación" runat="server" Text="Coordinación" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtCoordinación" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblZona" runat="server" Text="Zona" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtZona" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblMarca" runat="server" Text="Marca" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtMarca" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server"  ControlToValidate="txtMarca"
                                                ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="btnSave" ID="RequiredFieldValidator1">
                                </asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                     <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblModel" runat="server" Text="Modelo" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtModel" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server"  ControlToValidate="txtModel"
                                                ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="btnSave" ID="RequiredFieldValidator2">
                                </asp:RequiredFieldValidator>

                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblCapacidad" runat="server" Text="Capacidad" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <%--<asp:TextBox ID="txtCapacidad" runat="server" CssClass="TextBox"></asp:TextBox>--%>
                                <asp:DropDownList ID="drpCapacidad" runat="server" CssClass="DropDownList" 
                                    ToolTip="(*) Campo Vacío o con Formato Inválido" Height="16px" Width="93px" ></asp:DropDownList>
                                <%--<asp:Label ID="LabelUnits" runat="server" Text="Label"></asp:Label>--%>
                             <div>
                                         <asp:RequiredFieldValidator runat="server"  ControlToValidate="drpCapacidad"
                                                ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="btnSave" ID="RegularExpressionValidator2">
                                            </asp:RequiredFieldValidator>
                                            
                            </div>
 
                            </div>
 
 
 
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblColor" runat="server" Text="Color" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtColor" runat="server" CssClass="TextBox" ToolTip="(*) Campo Vacío o con Formato Inválido" ></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server"  ControlToValidate="txtColor"
                                                ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="btnSave" ID="RegularExpressionValidator1">
                                </asp:RequiredFieldValidator>
                            </div> 
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblAntigüedad" runat="server" Text="Antigüedad" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtAntigüedad" runat="server" CssClass="TextBox"></asp:TextBox>&nbsp;Años
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAntigüedad" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                    ValidationGroup="btnSave" ID="ValidateAntiguedad">
                                </asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblConformidad" runat="server" Text="Conformidad" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpConformidad" runat="server" AutoPostBack="false" CssClass="DropDownList">
                                    <asp:ListItem Text="Sin Conformidad" Value="0" />
                                    <asp:ListItem Text="En Conformidad" Value="1" Selected="True" />
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>                 
                </table>
            </div>
            <br />
            <div>
                <asp:Button ID="btnSave" runat="server" Text="Registrar" CssClass="Button" OnClientClick="return confirm('Confirmar Guardar Registro de Ingreso de Equipo')"
                    OnClick="btnSave_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?')"
                    OnClick="btnCancel_Click" />
                <asp:Button ID="hidConfirm" runat="server" Style="display: none;" OnClick="hidConfirm_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
