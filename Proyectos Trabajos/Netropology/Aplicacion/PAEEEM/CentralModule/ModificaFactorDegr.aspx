<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificaFactorDegr.aspx.cs" Inherits="PAEEEM.CentralModule.ModificaFactorDegr" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />
    <script src="~/Funciones.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" enableviewstate="false">
        
        function lockScreen() {
            var lock = document.getElementById('lock');
            lock.style.width = '300px';
            lock.style.height = '30px';
            lock.style.top = document.body.offsetHeight / 1.5 - lock.style.height.replace('px', '') / 2 + 'px';
            lock.style.left = document.body.offsetWidth / 2 - lock.style.width.replace('px', '') / 2 + 'px';
            if (lock)
                lock.className = 'LockOn';
        }

        function NumCheck(e, field) {
            var key = e.keyCode ? e.keyCode : e.which;
            // backspace 
            if (key == 8) return true;
            // 0-9 
            if (key > 47 && key < 58) {
                if (field.value == "") return true;
                var regexp = /.[0-9]{4}/;
                return !(regexp.test(field.value));
            } // . 
            if (key == 46) {
                if (field.value == "") return false;
                regexp = /^[0-9]/;
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

        .auto-style1 {
            width: 26px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div id="lock" class="LockOff">
                    <img src="../images/progress.gif" alt="¡ En Proceso, Por favor Espere !" style="vertical-align: middle; position: relative;" />
                </div>
            <h2>
                <asp:Label ID="Label1" runat="server" Font-Size="Large" Font-Bold="True" Text="MODIFICAR FACTOR DE DEGRADACIÓN" />
            </h2>
            <hr class="rule" />
            <fieldset class="legend_info">
                <table class="table.formTable" align="center">
                    <tr>
                        <td>
                            <asp:Label ID="Label2" CssClass="td_label" Text="Región: " runat="server" />
                            <asp:DropDownList ID="drpRegion" runat="server" Font-Size="11px" Class="DropDownList" Width="146px" Height="16px" AutoPostBack="True" OnSelectedIndexChanged="drpRegion_SelectedIndexChanged" >

                                <asp:ListItem Value="Region"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td class="auto-style1"></td>
                        <td>
                            <asp:Label ID="lblTipoTarifa" CssClass="td_label" Text="Tecnología: " runat="server" />
                            <asp:DropDownList ID="drpTecnologia" runat="server" Font-Size="11px" Class="DropDownList" Width="159px" Height="18px" AutoPostBack="True" OnSelectedIndexChanged="drpTecnologia_SelectedIndexChanged" >
                                <asp:ListItem Value="Tecnologia"></asp:ListItem>
                            </asp:DropDownList>
                        </td>                        
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button id="BTN_Buscar" runat="server" Text="Buscar" OnClick="BTN_Buscar_Click"/></td>                        
                    </tr>
                </table>
            </fieldset>
            <div id="div_Tarifa_OM" style="width: 100%" align="center" runat="server">
                <!-- -->
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvFactorDegradacion" runat="server" aling="center" Width="100%" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="11pt" AllowPaging="True" OnRowEditing="EditCustomer" OnRowCancelingEdit="CancelEdit" OnRowUpdating="GuardaDatos"
                            CssClass="GridViewStyle" PageSize="40">
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <EditRowStyle CssClass="GridViewEditStyle" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="5%" HeaderText="IdRegionClima" Visible="False" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdRegionClima" runat="server" Text='<%# Eval("IDREGION_BIOCLIMA")%>' CssClass="Label" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="25%" HeaderText="Region">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdRegion" runat="server" Text='<%# Eval("REGION")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%" HeaderText="CveTecnologia" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCveTec" runat="server" Text='<%# Eval("Cve_Tecnologia")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="25%" HeaderText="Tecnología">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNomTec" runat="server" Text='<%# Eval("Dx_Nombre_General")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Factor Degradacion" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactorDeg" runat="server" Text='<%# String.Format("{0:G2}",DataBinder.Eval(Container.DataItem,"FACTOR_DEGRADACION1"))%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFactorDeg" runat="server" onkeypress="return NumCheck(event,this);" Text='<%# Eval("FACTOR_DEGRADACION1")%>' />
                                        <asp:RegularExpressionValidator
                                            ID="REtxtFactorDeg"
                                            runat="server"
                                            Font-Size="8"
                                            ControlToValidate="txtFactorDeg"
                                            ValidationExpression="(^\d{1,5}\.\d{1,2})|(^\d{1,5})"
                                            ErrorMessage="Formato del monto 99999.99" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="25%" HeaderText="Última modificación" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUser" runat="server" Text='<%#Eval("MODIFICADO_POR") ?? Eval("ADICIONADO_POR")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Image" ShowEditButton="True" ItemStyle-HorizontalAlign="Center" EditImageUrl="~/CentralModule/images/editar-icono.png" HeaderText="Editar" UpdateText="Actualizar" CancelImageUrl="~/images/Cancelar.png" UpdateImageUrl="~/images/Actualizar.png"  />
                            </Columns>
                        </asp:GridView>
                        
                        <webdiyer:AspNetPager runat="server" ID="AspNetPager" CssClass="pagerDRUPAL" PageSize="20"
                                AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                                PageIndexBoxType="DropDownList" CustomInfoHTML="Página:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                                UrlPaging="false"
                                FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                                CurrentPageButtonClass="cpb" OnPageChanged="AspNetPager_PageChanged"></webdiyer:AspNetPager>
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
