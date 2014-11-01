<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambiarRPU.aspx.cs" Inherits="PAEEEM.SupplierModule.CambiarRPU" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .Label {
            width: 160px;
            color: #333333;
            font-size: 16px;
        }

        .Label_1 {
            width: 100px;
            color: #333333;
            font-size: 16px;
        }

        .DropDownList {
            width: 330px;
        }

        .Button {
            width: 120px;
        }

        .CenterButton {
            width: 120px;
            margin-right: 5px;
        }

        .auto-style2 {
            width: 167px;
            text-align: left;
        }

        .auto-style3 {
            width: 249px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Contenedor" style="width: 100%" align="center">
                <br />
                <h2 style="align-content: center">
                    <asp:Label ID="Label1" runat="server" Font-Size="Large" Font-Bold="True" Text="Cambio de RPU Distribuidor" />
                </h2>
                <fieldset>
                    <table style="width: 54%">
                        <tr>
                            <td class="auto-style2">
                                <asp:Label runat="server" CssClass="td_label" Text="Nombre o Razón Social:" />
                            </td>
                            <td class="auto-style3">
                                <asp:TextBox ID="TXB_Nombre_Razon" runat="server" Enabled="false" TextMode="SingleLine" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label CssClass="td_label" runat="server" Text="Número de Crédito:" />
                            </td>
                            <td class="auto-style3">
                                <asp:TextBox ID="TXB_No_Credito" runat="server" Enabled="false" TextMode="SingleLine" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label CssClass="td_label" runat="server" Text="Antigüo RPU:" />
                            </td>
                            <td class="auto-style3">
                                <asp:TextBox ID="TXB_Antiguo_RPU" runat="server" Enabled="false" TextMode="SingleLine" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label CssClass="td_label" runat="server" Text="Nuevo RPU:" />
                            </td>
                            <td class="auto-style3">
                                <asp:TextBox ID="TXB_Nuevo_RPU" runat="server" TextMode="SingleLine" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2"></td>
                            <td class="auto-style3"></td>
                        </tr>
                        <br />
                        <tr style="margin: auto;">
                            <td class="auto-style2">
                                <asp:Button ID="BTN_Enviar" runat="server" CssClass="Button" OnClick="BTN_Enviar_Click" Text="Enviar a validación" Width="150px" />
                            </td>
                            <td class="auto-style3">
                                <asp:Button ID="BTN_Salir" runat="server" CssClass="Button" OnClick="BTN_Salir_Click" Text="Salir" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="LBL_ErrorCorreo" runat="server" Text=""></asp:Label>
                            </td>
                            <tr>
                                <td class="auto-style2"></td>
                                <td class="auto-style3"></td>
                            </tr>
                            <br />
                        </tr>
                        <td colspan="2" style="text-align: left">
                            <asp:Label ID="Label223" runat="server" Font-Bold="true" Font-Size="Small">
                                    Nota: Para validar el nuevo RPU, deberá enviar por correo electrónico, copia del Depósito de Garantía y Comprobante de Pago,
                                    al encargado del programa en la Zona Fide que le corresponde.
                            </asp:Label>
                        </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
