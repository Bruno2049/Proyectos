<%@ Page AutoEventWireup="true" CodeBehind="RegisterTarifa.aspx.cs" Inherits="PAEEEM.CentralModule.RegisterTarifa" Language="C#" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="~/Funciones.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" enableviewstate="false">
        function NumCheck(e, field) {
            var key = e.keyCode ? e.keyCode : e.which;
            // backspace 
            if (key == 8) return true;
            // 0-9 
            if (key > 47 && key < 58) {
                if (field.value == "") return true;
                var regexp = /.[0-9]{4}$/;
                return !(regexp.test(field.value));
            } // . 
            if (key == 46) {
                if (field.value == "") return false;
                regexp = /^[0-9]+$/;
                return regexp.test(field.value);
            } // other key 
            return false;
        }

    </script>
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="Stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .table {
            border: 2px solid rgb(0, 149, 159);
            border-spacing: 0 0;
            text-align: center;
            font-family: Arial, Geneva, sans-serif;
        }

        .Label {
            color: #333333;
            font-size: 16px;
        }

        .Label_1 {
            color: Maroon;
            font-size: 14pt;
        }

        .DropDownList {
            font-size: 18pt;
            margin-left: 37px;
        }

        .Button {
            width: 160px;
            margin-left: 400%;
        }

        #dtFecha {
            float: right;
        }

        #DropDownGroup {
            float: left;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h2>
                <asp:Label runat="server" Font-Size="Large" Font-Bold="True" Text="REGISTRAR TARIFAS" />
            </h2>
            <hr class="rule" />
            <fieldset class="legend_info">
                <legend style="font-size: 14px; align-content: initial">Elije la tarifa a Registrar/Modificar
                </legend>
                <table class="table.formTable" align="center">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" CssClass="td_label" Text="Couta aplicable " runat="server" />
                            <asp:Label ID="lblMesAnioActual" Font-size="16px" ForeColor="#2F2F2F" Font-Weidth="Bold" Font-Family="Arial, Tahoma" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTipoTarifa" CssClass="td_label" Text="Tipo Tarifa " runat="server" />
                            <asp:DropDownList ID="drpTipoTarifa" runat="server" Font-Size="11px" Class="DropDownList"
                                AutoPostBack="True" Width="93px" OnSelectedIndexChanged="dprTipoTarifa_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div id="div_Tarifa_OM" style="width: 100%" align="center" runat="server">
                <asp:UpdatePanel ID="UpdatePanelOM"  runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvTarifa_OM" runat="server" aling="center" Width="100%" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="11pt" AllowPaging="True" OnRowEditing="EditCustomerOm" OnRowCancelingEdit="CancelEditOm" OnRowUpdating="GuardaDatosTarifaOm"
                            CssClass="GridViewStyle">
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <EditRowStyle CssClass="GridViewEditStyle" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="5%" HeaderText="IdRegion" Visible="False" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdTarifaOM" runat="server" Text='<%# Eval("IdTarifaOm")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%" HeaderText="IdRegion" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdRegionOm" runat="server" Text='<%# Eval("IdRegion")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="25%" HeaderText="Región Tarifaria">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDESCRIPCION" runat="server" Text='<%# Eval("Descripcion")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="18%" HeaderText="Cargo por demanda máxima" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcargoKWDemandaMax" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoKwDemanda"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtcargoKWDemandaMax" runat="server" onkeypress="return NumCheck(event,this);" Text='<%# Eval("MtCargoKwDemanda")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="REtxtcargoKWDemandaMax"
                                            runat="server"
                                            Font-Size ="8"
                                            ControlToValidate="txtcargoKWDemandaMax"
                                            ValidationExpression="(^\d{1,5}\.\d{1,3}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.999" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="18%" HeaderText="Cargo por consumo" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCargoKWhDemandaConsumida" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoKwhConsumo"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCargoKWhDemandaConsumida" onkeypress="return NumCheck(event,this);" runat="server" Text='<%# Eval("MtCargoKwhConsumo")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="REtxtCargoKWhDemandaConsumida"
                                            runat="server"
                                            Font-Size ="8"
                                            ControlToValidate="txtCargoKWhDemandaConsumida"
                                            ValidationExpression="(^\d{1,5}\.\d{1,3}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.999" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="25%" HeaderText="Última modificación" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUser" runat="server" Text='<%# Eval("Usuario")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Image" ShowEditButton="True" ItemStyle-HorizontalAlign="Center" EditImageUrl="~/CentralModule/images/editar-icono.png" HeaderText="Editar" UpdateText="Actualizar" CancelImageUrl="~/images/Cancelar.png" UpdateImageUrl="~/images/Actualizar.png" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="div_Tarifa_HM" style="width: 100%" align="center" runat="server">
                <asp:UpdatePanel ID="UpdatePanelHM" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvTarifa_HM" runat="server" Width="100%" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="11pt" AllowPaging="True" OnRowEditing="EditCustomerHm" OnRowCancelingEdit="CancelEditHm" OnRowUpdating="GuardaDatosTarifaHm" CssClass="GridViewStyle">
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <EditRowStyle CssClass="GridViewEditStyle" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="5%" HeaderText="IdRegion" Visible="False" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdTarifaHm" runat="server" Text='<%# Eval("IdTarifaHm")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%" HeaderText="ID_Region" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdRegionHM" runat="server" Text='<%# Eval("IdRegion")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Región Tarifaria">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDESCRIPCION" runat="server" Text='<%# Eval("Descripcion")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" HeaderText="Cargo por demanda" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcargoKWDemandaFac" runat="server" Text='<%# String.Format("{0:C4}",DataBinder.Eval(Container.DataItem,"MtCargoDemanda"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtcargoKWDemandaFac" onkeypress="return NumCheck(event,this);" runat="server" Text='<%# Eval("MtCargoDemanda")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="REtxtcargoKWDemandaFac"
                                            runat="server"
                                            Font-Size ="8"
                                            ControlToValidate="txtcargoKWDemandaFac"
                                            ValidationExpression="(^\d{1,5}\.\d{1,4}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.9999" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" HeaderText="Consumo base" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCargoKWhEnergiaBase" runat="server" Text='<%# String.Format("{0:C4}",DataBinder.Eval(Container.DataItem,"MtCargoBase"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCargoKWhEnergiaBase" runat="server" onkeypress="return NumCheck(event,this);" Text='<%# Eval("MtCargoBase")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="REtxtCargoKWhEnergiaBase"
                                            runat="server"
                                            Font-Size ="8"
                                            ControlToValidate="txtCargoKWhEnergiaBase"
                                            ValidationExpression="(^\d{1,5}\.\d{1,4}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.9999" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" HeaderText="Cargo por consumo intermedio" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCargoKWhEnergiaIntermedia" runat="server" Text='<%# String.Format("{0:C4}",DataBinder.Eval(Container.DataItem,"MtCargoIntermedia"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCargoKWhEnergiaIntermedia" runat="server" onkeypress="return NumCheck(event,this);" Text='<%# Eval("MtCargoIntermedia")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="REtxtCargoKWhEnergiaIntermedia"
                                            runat="server"
                                            Font-Size ="8"
                                            ControlToValidate="txtCargoKWhEnergiaIntermedia"
                                            ValidationExpression="(^\d{1,5}\.\d{1,4}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.9999" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" HeaderText="Cargo por consumo punta" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCargoKWhEnergiaPunta" runat="server" Text='<%# String.Format("{0:C4}",DataBinder.Eval(Container.DataItem,"MtCargoPunta"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCargoKWhEnergiaPunta" runat="server" onkeypress="return NumCheck(event,this);" Text='<%# Eval("MtCargoPunta")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator1"
                                            runat="server"
                                            Font-Size ="8"
                                            ControlToValidate="txtCargoKWhEnergiaPunta"
                                            ValidationExpression="(^\d{1,5}\.\d{1,4}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.9999" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" HeaderText="Promedio Tarifa" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPromedio" runat="server" Text='<%# String.Format("{0:C4}",DataBinder.Eval(Container.DataItem,"Promedio"))%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="15%" HeaderText="Última modificación" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUser" runat="server" Text='<%# Eval("Usuario")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" ButtonType="Image" ItemStyle-HorizontalAlign="Center" EditImageUrl="~/CentralModule/images/editar-icono.png" HeaderText="Editar" UpdateText="Actualizar" CancelImageUrl="~/images/Cancelar.png" UpdateImageUrl="~/images/Actualizar.png" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="div_Tarifa_02" style="width: 100%" align="center" runat="server">
                <asp:UpdatePanel ID="UpdatePanelTarifa02" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvTarifa_02" runat="server" Width="100%" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="11pt" AllowPaging="True" OnRowEditing="EditCustomer" OnRowCancelingEdit="CancelEdit" OnRowUpdating="GuardaDatosTarifa02" CssClass="GridViewStyle">
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <EditRowStyle CssClass="GridViewEditStyle" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="10px" HeaderText="ID_TARIFA_02" Visible="False" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID_TARIFA_02" runat="server" Text='<%# Eval("IdTarifa02")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Cargo Fijo" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCargoFijo" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCostoKwhFijo"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCargoFijo" onkeypress="return NumCheck(event,this);" runat="server" Text='<%# Eval("MtCostoKwhFijo")%>' />
                                         <asp:RegularExpressionValidator
                                            ID="REtxtCargoFijo"
                                            runat="server"
                                             Font-Size ="8"
                                            ControlToValidate="txtCargoFijo"
                                            ValidationExpression="(^\d{1,5}\.\d{1,3}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.999" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Consumo básico" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCargoFirst50KWh" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCostoKwhBasico"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCargoFirst50KWh" runat="server" onkeypress="return NumCheck(event,this);" Text='<%# Eval("MtCostoKwhBasico")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="REtxtCargoFirst50KWh"
                                            runat="server"
                                            Font-Size ="8"
                                            ControlToValidate="txtCargoFirst50KWh"
                                            ValidationExpression="(^\d{1,5}\.\d{1,3}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.999" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Consumo intermedio" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCargoMayor50KWh" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCostoKwhIntermedio"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCargoMayor50KWh" onkeypress="return NumCheck(event,this);" runat="server" Text='<%# Eval("MtCostoKwhIntermedio")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="REtxtCargoMayor50KWh"
                                            runat="server"
                                            Font-Size ="8"
                                            ControlToValidate="txtCargoMayor50KWh"
                                            ValidationExpression="(^\d{1,5}\.\d{1,3}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.999" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Consumo excedente" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCargoKWhAdicional" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCostoKwhExcedente"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCargoKWhAdicional" onkeypress="return NumCheck(event,this);" runat="server" Text='<%# Eval("MtCostoKwhExcedente")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="REtxtCargoKWhAdicional"
                                            runat="server"
                                            Font-Size ="8"
                                            ControlToValidate="txtCargoKWhAdicional"
                                            ValidationExpression="(^\d{1,5}\.\d{1,3}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.999" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="30px" HeaderText="Última modificación" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUser" runat="server" Text='<%# Eval("Usuario")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" ItemStyle-HorizontalAlign="Center" ButtonType="Image" EditImageUrl="~/CentralModule/images/editar-icono.png" HeaderText="Editar" UpdateText="Actualizar" CancelImageUrl="~/images/Cancelar.png" UpdateImageUrl="~/images/Actualizar.png" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="div_Tarifa_03" style="width: 100%" align="center" runat="server">
                <asp:UpdatePanel ID="UpdatePanelTarifa_03" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvTarifa_03" runat="server" Width="100%" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="11pt" AllowPaging="True" OnRowEditing="EditCustomer03" OnRowCancelingEdit="CancelEdit03" OnRowUpdating="GuardaDatosTarifa03" CssClass="GridViewStyle">
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <EditRowStyle CssClass="GridViewEditStyle" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="ID_TARIFA_03" Visible="False" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID_TARIFA_03" runat="server" Text='<%# Eval("IdTarifa03")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="15%" HeaderText="Cargo por demanda máxima" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCargoDemandaMax" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoDemandaMax"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCargoDemandaMax" onkeypress="return NumCheck(event,this);" runat="server" Text='<%# Eval("MtCargoDemandaMax")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="REtxtCargoDemandaMax"
                                            runat="server"
                                            Font-Size ="8"
                                            ControlToValidate="txtCargoDemandaMax"
                                            ValidationExpression="(^\d{1,5}\.\d{1,3}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.999" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="15%" HeaderText="Cargo por Consumo" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCargoAdicional" runat="server" Text='<%# String.Format("{0:C3}",DataBinder.Eval(Container.DataItem,"MtCargoAdicional"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCargoAdicional" onkeypress="return NumCheck(event,this);" runat="server" Text='<%# Eval("MtCargoAdicional")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="REtxtCargoAdicional"
                                            runat="server"
                                            Font-Size ="8"
                                            ControlToValidate="txtCargoAdicional"
                                            ValidationExpression="(^\d{1,5}\.\d{1,3}$)|(^\d{1,5}$)"
                                            ErrorMessage="Formato del monto 99999.999" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="32%" HeaderText="Última modificación" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUser" runat="server" Text='<%# Eval("Usuario")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" ItemStyle-Width="28%" ItemStyle-HorizontalAlign="center" ButtonType="Image" EditImageUrl="~/CentralModule/images/editar-icono.png" HeaderText="Editar" UpdateText="Actualizar" CancelImageUrl="~/images/Cancelar.png" UpdateImageUrl="~/images/Actualizar.png" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <table style="width: 100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSalir" Text="Salir" runat="server" Width="81px" OnClick="btnSalir_Click1" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
