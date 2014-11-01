<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRateandCost.aspx.cs"
    Inherits="PAEEEM.AddRateandCost" Title="Agregar Tarifas y Costos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Resources/Script/Calendar/WdatePicker.js" type="text/javascript"></script>

    <link href="../Resources/Script/Calendar/skin/default/datepicker.css" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 15%;
            color: #333333;
            font-size:16px;
        }
        .Label_1
        {
            color: Maroon;
            font-size:14pt;
        }
        .TextBox
        {
            width: 50%;
        }
        .DropDownList
        {
            width: 50%;
        }
        .Button
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <div>
        <br>
        <asp:Image runat="server" ImageUrl="../SupplierModule/images/t_altatarifa.png" />
        </div>
            <div id="dtFecha" align="right">                
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
            &nbsp;<asp:TextBox ID="txtFecha" runat="server" Enabled="False" BorderWidth="0"/></div>
            <br />
            <div>
                <asp:Label ID="lblEstado" runat="server" Font-Size="11pt" Text="Estado" CssClass="Label"></asp:Label>
                &nbsp;<asp:DropDownList ID="drpEstado" runat="server" Font-Size="11px" CssClass="DropDownList">
                </asp:DropDownList>
                 &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpEstado"
                    ErrorMessage="¡ Debe Ingresar un Estado!" Text="*"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="lblPeriodo" runat="server" Font-Size="11pt" Text="Período" CssClass="Label"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPeriodo" runat="server" Font-Size="11px" CssClass="Wdate" 
                    onclick="WdatePicker({dateFmt:'yyyy-MM',lang:'en'})"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="resName3" runat="server" ControlToValidate="txtPeriodo"
                    ErrorMessage="¡ Debe Ingresar un Período !" Font-Size="11px" Text="*"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="lblTarifa" runat="server" Text="Tarifa" Font-Size="11pt" CssClass="Label"></asp:Label>
                &nbsp;<asp:DropDownList ID="drpTarifa" runat="server" Font-Size="11px" AutoPostBack="true"
                    CssClass="DropDownList" onselectedindexchanged="drpTarifa_SelectedIndexChanged">
                </asp:DropDownList>
                 &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpTarifa"
                    ErrorMessage="¡ Debe Ingresar una Tarifa!" Text="*"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="lblCostoFijo" runat="server" Font-Size="11pt" Text="Costo Fijo" CssClass="Label"></asp:Label>
                &nbsp;<asp:TextBox ID="txtCostoFijo" runat="server" Font-Size="11px" CssClass="TextBox"></asp:TextBox>
                &nbsp;<asp:Label ID="Label3" runat="server" Font-Size="11pt" Text="Kw/h"></asp:Label>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="11px" runat="server" ControlToValidate="txtCostoFijo"
                    ErrorMessage="¡ Debe Ingresar un Costo Fijo !" Text="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                    ControlToValidate="txtCostoFijo" ErrorMessage="(*) Campo Vacío o con Formato Inválido" Font-Size="11px" ValidationExpression="-?[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>
            </div>
            <div>
                <asp:Label ID="lblCostoBasico" runat="server" Font-Size="11pt" Text="Costo Básico" CssClass="Label"></asp:Label>
                &nbsp;<asp:TextBox ID="txtCostoBasico" runat="server" Font-Size="11px" CssClass="TextBox"></asp:TextBox>
                &nbsp;<asp:Label ID="Label5" runat="server" Font-Size="11pt" Text="Kw/h"></asp:Label>
                 &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCostoBasico"
                    ErrorMessage="¡ Debe Ingresar un Costo Básico !" Text="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ControlToValidate="txtCostoBasico" ErrorMessage="(*) Campo Vacío o con Formato Inválido" Font-Size="11px" ValidationExpression="-?[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>
            </div>
            <div>
                <asp:Label ID="lblCostoIntermedio" runat="server" Text="Costo Intermedio" Font-Size="11pt" CssClass="Label"></asp:Label>
                &nbsp;<asp:TextBox ID="txtCostoIntermedio" runat="server" Font-Size="11px" CssClass="TextBox"></asp:TextBox>
                &nbsp;<asp:Label ID="Label7" runat="server" Text="Kw/h" Font-Size="11pt" ></asp:Label>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCostoIntermedio"
                    ErrorMessage="¡ Debe Ingresar un Costo Intermedio !" Text="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" Font-Size="11px" runat="server"
                    ControlToValidate="txtCostoIntermedio" ErrorMessage="(*) Campo Vacío o con Formato Inválido"
                    ValidationExpression="-?[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>
            </div>
            <div>
                <asp:Label ID="lblCostoExcedente" runat="server" Font-Size="11pt" Text="Costo Excedente" CssClass="Label"></asp:Label>
                &nbsp;<asp:TextBox ID="txtCostoExcedente" runat="server" Font-Size="11px" CssClass="TextBox"></asp:TextBox>
                &nbsp;<asp:Label ID="Label9" runat="server" Text="Kw/h" Font-Size="11pt"></asp:Label>
                 &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCostoExcedente"
                    ErrorMessage="¡ Debe Ingresar un Costo Excedente !" Text="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                    ControlToValidate="txtCostoExcedente" ErrorMessage="(*) Campo Vacío o con Formato Inválido" Font-Size="11px" ValidationExpression="-?[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>
            </div>
             <div>
                <asp:Label ID="LblTarifaDemanda" runat="server" Font-Size="11pt" Text="Tarifa Demanda (Tarifa OM, HM)" CssClass="Label"></asp:Label>
                &nbsp;<asp:TextBox ID="txtTarifaDemanda" runat="server" Font-Size="11px" CssClass="TextBox"></asp:TextBox>
                &nbsp;<asp:Label ID="Label2" runat="server" Text="$/Kw" Font-Size="11pt"></asp:Label>
                 &nbsp;
                 &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidatorTarifaDemanda" runat="server" ControlToValidate="txtTarifaDemanda"
                    ErrorMessage="¡ Debe Ingresar una Tarifa Demanda!" Text="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidatorTarifaDemanda" runat="server"
                    ControlToValidate="txtTarifaDemanda" ErrorMessage="(*) Campo Vacío o con Formato Inválido" Font-Size="11px" ValidationExpression="-?[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>
            </div>  
             <div>
                <asp:Label ID="LblCostoTarifaConsumo" runat="server" Font-Size="11pt" Text="Costo Tarifa Consumo (Tarifa OM, HM)" CssClass="Label"></asp:Label>
                &nbsp;<asp:TextBox ID="txtCostoTarifaConsumo" runat="server" Font-Size="11px" CssClass="TextBox"></asp:TextBox>
                &nbsp;<asp:Label ID="Label4" runat="server" Text="$/Kwh" Font-Size="11pt"></asp:Label>
                 &nbsp;
                 &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidatorTarifaConsumo" runat="server" ControlToValidate="txtCostoTarifaConsumo"
                    ErrorMessage="¡ Debe Ingresar una Tarifa Consumo!" Text="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidatorTarifaConsumo" runat="server"
                    ControlToValidate="txtCostoTarifaConsumo" ErrorMessage="(*) Campo Vacío o con Formato Inválido" Font-Size="11px" ValidationExpression="-?[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>
            </div>                      
            <br />
            <div>
                <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="Button" OnClick="btnSave_Click" OnClientClick="return confirm('Confirmar Guardar Tarifa y Costo');" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnGo" runat="server" Text="Salir" CssClass="Button" onclick="btnGo_Click"  OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?')"/>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
