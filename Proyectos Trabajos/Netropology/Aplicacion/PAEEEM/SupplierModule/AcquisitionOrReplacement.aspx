<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcquisitionOrReplacement.aspx.cs"
    Inherits="PAEEEM.SupplierModule.AcquisitionOrReplacement" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Label
        {
            width: 170px;
            color: Maroon;
        }
        .Label1
        {
            width: 300px;
            color: Maroon;
        }
        .TextBox
        {
            width: 250px;
        }
        .DropDownList
        {
            width: 250px;
        }
        .Button
        {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtFecha" runat="server" Enabled="False"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:Label ID="lblRazonSocial" runat="server" Text="Nombre o razón social" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="TextBox" 
                    Enabled="False"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblCredito" runat="server" Text="Número de Crédito" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;<asp:TextBox ID="txtCreditoNum" runat="server" CssClass="TextBox" 
                    Enabled="False"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:Label ID="Label4" runat="server" Text="DATOS DEL O LOS EQUIPOS A DISPONER" CssClass="Label1"></asp:Label>
            </div>
            <br />
            <div>
                <asp:Label ID="lblAcqOrRep" runat="server" Text="Fg_adquisicion_sust" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="radAcquisition" runat="server" OnCheckedChanged="radAcquisition_CheckedChanged" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="radReplacement" runat="server" OnCheckedChanged="radReplacement_CheckedChanged" />
            </div>
            <br />
            <div>
                <asp:Label ID="Label6" runat="server" Text="Información Disposición Equipo" CssClass="Label1"></asp:Label>
            </div>
            <br />
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label7" runat="server" Text="Tecnología" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label8" runat="server" Text="Número de Unidades" CssClass="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label9" runat="server" Text="Centro de Disposición" CssClass="Label"></asp:Label>
            </div>
            <div>
                <asp:Table ID="tbAcqOrRep" runat="server">
                    <asp:TableRow ID="tr1">
                        <asp:TableCell Width="260px">
                            <asp:DropDownList ID="drpTechnology1" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell Width="300px">
                           <%-- <asp:DropDownList ID="drpUnidades1" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>--%>
                            <asp:TextBox ID="txtUnidades1" runat="server"></asp:TextBox>&nbsp;&nbsp;
                            <asp:RegularExpressionValidator ID="revUnidades1" runat="server"
                    ControlToValidate="txtUnidades1" ErrorMessage="No es un valor válido." ValidationExpression="^[0-9]*[1-9][0-9]*$"></asp:RegularExpressionValidator>
                        </asp:TableCell>
                        <asp:TableCell Width="260px">
                            <asp:DropDownList ID="drpDisposalCenter1" runat="server" CssClass="DropDownList">
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell Width="260px">
                            <asp:Button ID="btnAddRecord" runat="server" Text="Agregar Tecnología" OnClick="btnAddRecord_Click"
                                CssClass="Button" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
            <br />
            <div>
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClientClick="return confirm('¿ Desea regresar a la pantalla previa ?')" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" OnClientClick="return confirm('¿ Desea continuar con la Integración del Expediente ?')"
                    OnClick="btnSiguiente_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSalir" runat="server" Text="Salir" OnClientClick="return confirm('¿ Desea salir de ésta pantalla ?')" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
