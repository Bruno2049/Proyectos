<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddAndEditProduct.aspx.cs"
    Inherits="PAEEEM.CentralModule.AddAndEditProduct" Title="Agregar/Editar Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />
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
        .DropDownList
        {
            width: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <br>
                <asp:Label ID="lblTitle" runat="server" Text="Agregar/Editar Producto" Font-Size="Larger"></asp:Label>
            </div>
            <div id="dtFecha" align="right">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                &nbsp;<div>
                    <asp:Label ID="lblFechaDate" runat="server"></asp:Label>
                </div>
            </div>
            <br />
            <div>
                <table width="100%">
                    <tr>
                        <td width="20%">
                            <asp:Label ID="lblManufacture" runat="server" Text="Fabricante (*)" CssClass="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpManufacture" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="drpManufacture" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                ValidationGroup="Save" ID="Required">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblTechnology" runat="server" Text="Tecnología (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpTechnology" runat="server" CssClass="DropDownList" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpTechnology_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="drpTechnology" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                    ValidationGroup="Save" ID="Required1">
                                </asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                </table>
                <!-- RSA 2012-09-11 No aplican para SE Start -->
                <asp:Panel ID="PanelNotSE" runat="server">
                    <table width="100%">
                        <tr>
                            <td width="20%">
                                <asp:Label ID="lblTipoProduct" runat="server" Text="Tipo de Producto (*)" CssClass="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpTipoProduct" runat="server" CssClass="DropDownList">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="drpTipoProduct" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                    ValidationGroup="Save" ID="RequiredFieldValidator1">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblNameProduct" runat="server" Text="Nombre Producto (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="txtNameProduct" runat="server" CssClass="TextBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNameProduct" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredFieldValidator3">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <!-- RSA 2012-09-11 No aplican para SE End -->
                <table width="100%">
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblMarca" runat="server" Text="Marca (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpMarca" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpMarca" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredFieldValidator2">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblModel" runat="server" Text="Modelo Producto (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtModel" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtModel" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                    ValidationGroup="Save" ID="RequiredFieldValidator4">
                                </asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                </table>
                <!-- RSA 2012-09-11 Nuevos para SE Start -->
                <asp:Panel ID="PanelSE" runat="server" Visible="false" Enabled="false">
                    <table width="100%">
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_TIPO" runat="server" Text="Tipo (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_TIPO" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_TIPO" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_TIPO">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_TRANSFORMADOR" runat="server" Text="Transformador (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_TRANSFORMADOR" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
<%--                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_TRANSFORMADOR" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_TRANSFORMADOR">
                                    </asp:RequiredFieldValidator>
--%>                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_TRANSFORM_FASE" runat="server" Text="Fase del Transformador (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_TRANSFORM_FASE" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_TRANSFORM_FASE" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_TRANSFORM_FASE">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_TRANSFORM_MARCA" runat="server" Text="Marca del Transformador (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_TRANSFORM_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_TRANSFORM_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_TRANSFORM_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_TRANSFORM_RELACION" runat="server" Text="Relación de Transformación (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_TRANSFORM_RELACION" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_TRANSFORM_RELACION" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_TRANSFORM_RELACION">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_APARTARRAYO" runat="server" Text="Apartarrayo (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_APARTARRAYO" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_APARTARRAYO" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_APARTARRAYO">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_APARTARRAYO_MARCA" runat="server" Text="Marca del Apartarrayo (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_APARTARRAYO_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_APARTARRAYO_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_APARTARRAYO_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_CORTACIRCUITO" runat="server" Text="Cortacircuito – Fusible (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_CORTACIRCUITO" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_CORTACIRCUITO" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_CORTACIRCUITO">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_CORTACIRC_MARCA" runat="server" Text="Marca Cortacircuito – Fusible (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_CORTACIRC_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_CORTACIRC_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_CORTACIRC_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_INTERRUPTOR" runat="server" Text="Interruptor Termomagnético (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_INTERRUPTOR" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_INTERRUPTOR" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_INTERRUPTOR">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_INTERRUPTOR_MARCA" runat="server" Text="Marca Interruptor Termomagnético (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_INTERRUPTOR_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_INTERRUPTOR_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_INTERRUPTOR_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_CONDUCTOR" runat="server" Text="Conductor de Tierra (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_CONDUCTOR" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_CONDUCTOR" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_CONDUCTOR">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_CONDUCTOR_MARCA" runat="server" Text="Marca Conductor de Tierra (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_CONDUCTOR_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_CONDUCTOR_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_CONDUCTOR_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_COND_CONEXION" runat="server" Text="Conductor de Conexión a Centro de Carga (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_COND_CONEXION" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_COND_CONEXION" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_COND_CONEXION">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <asp:Label ID="lblSE_COND_CONEXION_MARCA" runat="server" Text="Marca Conductor de Conexión (*)" CssClass="Label"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="drpSE_COND_CONEXION_MARCA" runat="server" CssClass="DropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpSE_COND_CONEXION_MARCA" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                        ValidationGroup="Save" ID="RequiredSE_COND_CONEXION_MARCA">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>                
                    </table>
                </asp:Panel>
                <table width="100%">
                    <!-- RSA 2012-09-11 Nuevos para SE End -->
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblMaxPrice" runat="server" Text="Precio Máximo (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtMaxPrice" runat="server" CssClass="TextBox" ToolTip="(2 Dígitos Decimales)"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(.[0-9]{2})?$"
                                    ControlToValidate="txtMaxPrice" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save" ID="RegularExpressionValidator5">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblEficience" runat="server" Text="Eficiencia de Energía (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtEficience" runat="server" CssClass="TextBox" ToolTip="(4 Dígitos Decimales)"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(.[0-9]{4})?$"
                                    ControlToValidate="txtEficience" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save" ID="RegularExpressionValidator1">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblConsumer" runat="server" Text="Max Consumo de 24h (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtConsumer" runat="server" CssClass="TextBox" ToolTip="(2 Dígitos Decimales)"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(.[0-9]{2})?$"
                                    ControlToValidate="txtConsumer" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save" ID="RegularExpressionValidator2">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblCapacidad" runat="server" Text="Capacidad (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpCapacidad" runat="server" CssClass="DropDownList">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="drpCapacidad" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                                    ValidationGroup="Save" ID="RequiredFieldValidator5">
                                </asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="lblAhorroConsumo" runat="server" Text="Ahorro Consumo Kwh/Mes (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtAhorroConsumo" runat="server" CssClass="TextBox" ToolTip="(4 Dígitos Decimales)"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(.[0-9]{4})?$"
                                    ControlToValidate="txtAhorroConsumo" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save" ID="RegularExpressionValidator3">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <asp:Label ID="LblAhorroDemanda" runat="server" Text="Ahorro Demanda Kw (*)" CssClass="Label"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtAhorroDemanda" runat="server" CssClass="TextBox" ToolTip="(2 Dígitos Decimales)"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]+(.[0-9]{2})?$"
                                    ControlToValidate="txtAhorroDemanda" ErrorMessage="(*) Campo Vacío o con Formato Inválido" ValidationGroup="Save" ID="RegularExpressionValidator4">
                                </asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div>
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="Button" OnClick="btnSave_Click"
                    OnClientClick="return confirm('Confirmar Guardar Producto');" />
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?')"
                    OnClick="btnCancel_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
