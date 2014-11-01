<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambiarRPUZona.aspx.cs" Inherits="PAEEEM.RegionalModule.CambiarRPUZona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
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

        .auto-style6 {
            width: 167px;
            text-align: left;
        }

        .auto-style7 {
            width: 249px;
            align-self: inherit;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Contenedor" style="width: 100%; text-align: center;">
                <br />
                <h2 style="align-content: center">
                    <asp:Label ID="Label1" runat="server" Font-Size="Large" Font-Bold="True" Text="Cambio de RPU Zona" />
                </h2>
                <fieldset>
                    <table style="width: 54%">
                        <tr>
                            <td class="auto-style6">
                                <asp:Label runat="server" CssClass="td_label" Text="Nombre o Razón Social:" />
                            </td>
                            <td class="auto-style7">
                                <asp:TextBox runat="server" Enabled="false" ID="TXB_Nombre_Razon" TextMode="SingleLine" Width="300px" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">
                                <asp:Label CssClass="td_label" runat="server" Text="Número de Crédito:" />
                            </td>
                            <td class="auto-style7">
                                <asp:TextBox runat="server" Enabled="false" ID="TXB_No_Credito" TextMode="SingleLine" Width="300px" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">
                                <asp:Label CssClass="td_label" runat="server" Text="Antigüo RPU:" />
                            </td>
                            <td class="auto-style7">
                                <asp:TextBox runat="server" Enabled="false" ID="TXB_Antiguo_RPU" TextMode="SingleLine" Width="300px" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">
                                <br />
                                <br />

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style6">
                                <asp:Label CssClass="td_label" runat="server" Text="Nuevo RPU:" />
                            </td>
                            <td class="auto-style7">
                                <asp:TextBox runat="server" ID="TXB_Nuevo_RPU" TextMode="SingleLine" Width="300px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style6"></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">
                                <asp:Button ID="BTN_Enviar" Text="Enviar a validación" runat="server" CssClass="Button" OnClick="BTN_Enviar_Click" Width="150px" /></td>
                            <td class="auto-style7">
                                <asp:Button ID="BTN_Salir" Text="Salir" runat="server" CssClass="Button" OnClick="BTN_Salir_Click" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">
                                <asp:Label ID="LBL_ErrorCorreo" Text="" runat="server" />
                            </td>
                        </tr>
                    </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
