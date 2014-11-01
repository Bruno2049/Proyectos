<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SupplierAuthorization.aspx.cs"
    Inherits="PAEEEM.CentralModule.SupplierAuthorization" Title="Autorización de Proveedores" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CajaDialogo
        {
            background-color: #99ffcc;
            border-width: 4px;
            border-style: outset;
            border-color: Yellow;
            padding: 0px;
            width: 200px;
            font-weight: bold;
            font-style: italic;
        }

            .CajaDialogo div
            {
                margin: 5px;
                text-align: center;
            }
        /* .... */ .FondoAplicacion
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .Label
        {
            width: 120px;
            color: #333333;
            font-size: 16px;
            text-align: right;
        }

        .DropDownList
        {
            width: 200px;
        }

        .Button
        {
            width: 150px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var pre = "ctl00_MainContent_";
        function mpeSeleccionOnOk() {
            var ddlCiudades = document.getElementById(pre + "ddlCiudades");
            var ddlMeses = document.getElementById(pre + "ddlMeses");
            var ddlAnualidades = document.getElementById(pre + "ddlAnualidades");
            var txtSituacion = document.getElementById(pre + "txtSituacion");

            txtSituacion.value = ddlCiudades.options[ddlCiudades.selectedIndex].value + ", " +
                ddlMeses.options[ddlMeses.selectedIndex].value + " de " +
                ddlAnualidades.options[ddlAnualidades.selectedIndex].value;
            var btn = document.getElementById(pre + "btnActive");

            //if (btn) {
            //    btn.click();
            //}
        }
        function mpeSeleccionOnCancel() {
            var txtSituacion = document.getElementById(pre + "txtSituacion");
            txtSituacion.value = "";
            txtSituacion.style.backgroundColor = "#FFFF99";
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
    <asp:Panel ID="panel" runat="server">
        <%--<asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>--%>
        <div id="container">
            <br>
            <div>

                <asp:Image runat="server" ImageUrl="../images/t_esei.png" />

            </div>

            <div align="right">
                <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
                    <asp:Literal ID="lblFecha" runat='server' />
            </div>
            <br />
            <table width="100%">
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="lblRegional" Text="Regional" runat="server" Font-Size="11pt" CssClass="Label" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpRegional" runat="server" Font-Size="11px" AutoPostBack="true"
                                CssClass="DropDownList" OnSelectedIndexChanged="drpRegional_SelectedIndexChanged" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:Label ID="lblZona" Text="Zona" runat="server" Font-Size="11pt" CssClass="Label" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpZona" runat="server" Font-Size="11px" AutoPostBack="true"
                                CssClass="DropDownList" OnSelectedIndexChanged="drpZona_SelectedIndexChanged" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:Label ID="lblTipo" Text="Tipo" Font-Size="11pt" runat="server" CssClass="Label" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpTipo" AutoPostBack="true" Font-Size="11px" runat="server"
                                CssClass="DropDownList" OnSelectedIndexChanged="drpTipo_SelectedIndexChanged">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="M" Text="Proveedor Matriz"></asp:ListItem>
                                <asp:ListItem Value="SB_F" Text="Proveedor Sucursal Física"></asp:ListItem>
                                <asp:ListItem Value="SB_V" Text="Proveedor Sucursal Virtual"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:Label ID="lblEstatus" Text="Estatus" Font-Size="11pt" runat="server" CssClass="Label" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpEstatus" AutoPostBack="true" Font-Size="11px" runat="server"
                                CssClass="DropDownList" OnSelectedIndexChanged="drpEstatus_SelectedIndexChanged" />
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <div>
                <asp:GridView ID="grvSupplier" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="10" DataKeyNames="ID,Cve_Estatus_Proveedor,Type" OnDataBound="grvSupplier_DataBound">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="Clave"></asp:BoundField>
                        <asp:BoundField DataField="Dx_Razon_Social" HeaderText="Nombre o Razón Social" />
                        <asp:BoundField DataField="Dx_Nombre_Comercial" HeaderText="Nombre Comercial" />
                        <asp:BoundField DataField="Type" HeaderText="Tipo" Visible="False" />
                        <asp:BoundField DataField="type_Desc" HeaderText="Tipo" />
                        <asp:BoundField DataField="Dx_Estatus_Proveedor" HeaderText="Estatus" />
                        <%-- ADD BY EDU --%>                      
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:DropDownList ID="LSB_Acciones" runat="server" Enabled="False" AutoPostBack="true">
                                    <asp:ListItem>Elegir Opcion</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbSelect" runat="server" OnCheckedChanged="ckbSelect_OnCheckedChanged" AutoPostBack="True" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                </asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="10"
                    AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                    PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                    UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
                    FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                    CurrentPageButtonClass="cpb">
                </webdiyer:AspNetPager>
            </div>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
            <div style="display: none">
                <asp:Button ID="HiddenButton" BackColor="#FFFFFF" OnClick="HiddenButton_OnClick" runat="server" Width="0px" />
            </div>
            <br />
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Button runat="server" ID="BtnAceptar" Text="Aceptar" OnClick="BtnAceptar_OnClick" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%">
                <tr>
                    <td width="20%" align="center">
                        <asp:Button ID="btnActive" Visible="False" runat="server" Text="Activar" OnClientClick="return confirm('Confirmar Activar los Proveedores Seleccionados')"
                            OnClick="btnActive_Click" />
                        <%----%>
                    </td>
                    <td width="20%" align="center">
                        <asp:Button ID="btnDeActive" Visible="False" runat="server" Text="Inactivar" OnClientClick="return confirm('Confirmar Inactivar a los Proveedores Seleccionados')"
                            OnClick="btnDeActive_Click" />
                    </td>
                    <td width="20%" align="center">
                        <asp:Button ID="btnReActive" Visible="False" runat="server" Text="Reactivar"
                            OnClientClick="return confirm('Confirmar Reactivar Proveedores Seleccionados')"
                            OnClick="btnReActive_Click" />
                    </td>
                    <td width="20%" align="center">
                        <asp:Button ID="btnCancel" Visible="False" runat="server" Text="Cancelar" OnClientClick="return confirm('Confirmar Cancelar Definitivamente los Proveedores Seleccionados')"
                            OnClick="btnCancel_Click" />
                    </td>
                    <td width="10%"></td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

    <%-- <asp:Panel ID="pnlSeleccionarDatos" runat="server" CssClass="CajaDialogo" Style="display: none;">
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;">
                <asp:Label ID="Label4" runat="server" Text="Seleccionar datos" />
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Ciudad:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCiudades" runat="server">
                                <asp:ListItem>Zamora</asp:ListItem>
                                <asp:ListItem>Teruel</asp:ListItem>
                                <asp:ListItem>Salamanca</asp:ListItem>
                                <asp:ListItem>Sevilla</asp:ListItem>
                                <asp:ListItem>Lugo</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Mes:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMeses" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Año:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAnualidades" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar"  />
                &nbsp;&nbsp;
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
            </div>
        </asp:Panel>
    <asp:HiddenField runat="server" ID="HIdden"/>
        <cc1:ModalPopupExtender ID="mpeSeleccion" runat="server" TargetControlID="HIdden"
            PopupControlID="pnlSeleccionarDatos" OkControlID="btnAceptar" CancelControlID="btnCancelar"
            OnOkScript="mpeSeleccionOnOk()" OnCancelScript="mpeSeleccionOnCancel()" DropShadow="True" 
            BackgroundCssClass="FondoAplicacion" /> --%>
</asp:Content>
