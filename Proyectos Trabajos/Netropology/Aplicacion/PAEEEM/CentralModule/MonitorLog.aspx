<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonitorLog.aspx.cs" Inherits="PAEEEM.CentralModule.MonitorLog" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.3.2.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        var pre = "ctl00_MainContent_";
        function muestra_calendario(v) {
            var cfecha = showModalDialog("images/calendario.htm", 0, "dialogWidth:282px;dialogHeight:295px;center:yes");
            if (cfecha == -1 || cfecha == null || cfecha == "")
                document.getElementById(pre + v).value = "";
            else
                document.getElementById(pre + v).value = cfecha;
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
            width: 100%;
            align-content: center;
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

        .auto-style1 {
            width: 149px;
        }

        .auto-style7 {
            width: 85px;
        }

        .auto-style9 {
            width: 169px;
        }

        .auto-style10 {
            width: 54px;
            height: 38px;
        }

         .auto-style11 {
            width: 42px;
        }

        .auto-style12 {
            text-align: right;
            height: 38px;
            width: 848px;
        }

        .auto-style14 {
            width: 102px;
        }

        .auto-style15 {
            width: 123px;
        }

        .auto-style16 {
            width: 178px;
        }

        .auto-style18 {
            width: 14px;
        }
        .auto-style19 {
            width: 130px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h2>
                <asp:Label ID="Label14" runat="server" Font-Size="Large" Font-Bold="True" Text="MONITOR LOG" />
            </h2>
            <hr class="rule" />
            <fieldset class="legend_info">
                <legend style="font-size: 14px;">Elije el criterio de búsqueda</legend>
                <table class="table.formTable">
                    <tr>
                        <td class="auto-style16">
                            <asp:Label ID="Label2" CssClass="td_label" runat="server" Font-Bold="True"
                                Font-Size="10pt" Text=" Fecha Inicio: " />
                        </td>
                        <td>
                            <input id="txtFdesde" runat="server" readonly="readonly" style="width: 87px" type="text" />
                            <img id="Img1" onclick=" muestra_calendario('txtFdesde')" runat="server" src="~/CentralModule/images/Calendario.bmp" style="width: 17px; height: 17px" alt="" />
                        </td>
                        <td class="auto-style15">
                            <asp:Label ID="Label3" CssClass="td_label" runat="server" Font-Bold="True"
                                Font-Size="10pt" Text=" Fecha Fin: " />
                        </td>
                        <td class="auto-style19">
                            <input id="txtFhasta" runat="server" readonly="readonly" style="width: 87px" type="text" />
                            <img id="Img2" onclick=" muestra_calendario('txtFhasta')" runat="server" src="~/CentralModule/images/Calendario.bmp" style="width: 17px; height: 17px" alt="" />
                        </td>
                        <td class="auto-style14">
                            <asp:Label ID="Label1" CssClass="td_label" Text="Usuario: " Font-Size="10pt" Font-Bold="True" runat="server" />
                        </td>
                        <td class="auto-style18">
                            <asp:TextBox runat="server" ID="txtUsuario" Width="125px" />
                        </td>
                        <td class="auto-style7">
                            <asp:Label ID="lblTipoTarifa" CssClass="td_label" Text="Empresa: " Font-Size="10pt" Font-Bold="True" runat="server" />
                        </td>
                        <td class="auto-style9">
                            <asp:DropDownList ID="drpEmpresa" runat="server" Font-Size="11px" AutoPostBack="True" Width="112px" />
                        </td>
                        <td class="auto-style7">
                            <asp:Label ID="Label4" CssClass="td_label" Text="Sucursal: " Font-Size="10pt" Font-Bold="True" runat="server" />
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox runat="server" ID="txtSucursal" Width="103px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style16">
                            <asp:Label ID="Label5" CssClass="td_label" Text="Proceso: " Font-Size="10pt" Font-Bold="True" runat="server" />
                        </td>
                        <td class="auto-style1">
                            <asp:DropDownList ID="drpTipoProceso" runat="server" Font-Size="11px" AutoPostBack="True" Width="116px" Height="16px" OnSelectedIndexChanged="drpTipoProceso_SelectedIndexChanged" />
                        </td>
                        <td class="auto-style15">
                            <asp:Label ID="Label6" CssClass="td_label" Text="Tarea: " Font-Size="10pt" Font-Bold="True" runat="server" />
                        </td>
                        <td class="auto-style19">
                            <asp:DropDownList ID="drpTareas" runat="server" Font-Size="11px" AutoPostBack="True" Width="109px" />
                        </td>
                        <td class="auto-style14">
                            <asp:Label ID="Label8" CssClass="td_label" Text="Rol: " Font-Size="10pt" Font-Bold="True" runat="server" />
                        </td>
                        <td class="auto-style18">
                            <asp:DropDownList ID="drpRoles" runat="server" Font-Size="11px" AutoPostBack="True" Width="136px" />
                        </td>
                        <td class="auto-style7">
                            <asp:Label ID="Label9" CssClass="td_label" Text="Region: " Font-Size="10pt" Font-Bold="True" runat="server" />
                        </td>
                        <td class="auto-style9">
                            <asp:DropDownList ID="drpRegion" runat="server" Font-Size="11px" AutoPostBack="True" Width="113px" Height="21px" />
                        </td>
                        <td class="auto-style7">
                            <asp:Label ID="Label10" CssClass="td_label" Text="Zona: " Font-Size="10pt" Font-Bold="True" runat="server" />
                        </td>
                        <td class="auto-style1">
                            <asp:DropDownList ID="DrpZona" runat="server" Font-Size="11px" AutoPostBack="True" Width="99px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style16">
                            <asp:Label ID="Label7" CssClass="td_label" Text="No. Solicitud: " Font-Size="10pt" Font-Bold="True" runat="server" />
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox runat="server" ID="txtNumSolicitud" Width="106px" />
                        </td>
                        <td class="auto-style15">
                            <asp:Label ID="Label11" CssClass="td_label" Text="RPU: " Font-Size="10pt" Font-Bold="True" runat="server" />
                        </td>
                        <td class="auto-style19">
                            <asp:TextBox runat="server" ID="txtRPU" />
                        </td>
                        <td class="auto-style14">
                            <asp:Label ID="Label12" CssClass="td_label" Text="Nota: " Font-Size="10pt" Font-Bold="True" runat="server" />
                        </td>
                        <td class="auto-style18">
                            <asp:TextBox runat="server" ID="txtNota" Width="137px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style16"></td>
                        <td></td>
                        <td class="auto-style15"></td>
                        <td colspan="5" dir="rtl">
                            <asp:Button ID="btnBuscar" runat="server" CssClass="Button" OnClick="btnBuscar_Click" Text="Buscar" Width="80px" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <div id="divReporte" style="width: 100%;" align="center" runat="server">
                <table  visible="false" id="tbButton">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblMensaje" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvReporte" runat="server" Width="100%" AutoGenerateColumns="False" Font-Names="Arial"
                    Font-Size="11pt" PageSize="20" AllowPaging="True" CssClass="GridViewStyle">
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EditRowStyle CssClass="GridViewEditStyle" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="20px" HeaderText="IdLog" Visible="True" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblIdLog" runat="server" Text='<%# Eval("idSecuencia")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Fecha/Hora" Visible="True" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("Fecha")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Usuario">
                            <ItemTemplate>
                                <asp:Label ID="lblUsuario" runat="server" Text='<%# Eval("nombre_usuario")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="80px" HeaderText="Rol">
                            <ItemTemplate>
                                <asp:Label ID="lblRol" runat="server" Text='<%# Eval("Nombre_Rol")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="80px" HeaderText="Empresa">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpresa" runat="server" Text='<%# Eval("Empresa")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Región">
                            <ItemTemplate>
                                <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("Region")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Zona">
                            <ItemTemplate>
                                <asp:Label ID="lblZona" runat="server" Text='<%# Eval("Zona")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="90px" HeaderText="Proceso">
                            <ItemTemplate>
                                <asp:Label ID="lblProceso" runat="server" Text='<%# Eval("PROCESO")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="90px" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="70px" HeaderText="Tarea">
                            <ItemTemplate>
                                <asp:Label ID="lblTarea" runat="server" Text='<%# Eval("Tarea")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="90px" HeaderText="Dato Modificado">
                            <ItemTemplate>
                                <asp:Label ID="lblDatoModificado" runat="server" Text='<%# Eval("DatoModificado")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="90px" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="100px" HeaderText="Motivo">
                            <ItemTemplate>
                                <asp:Label ID="lblMotivo" runat="server" Text='<%# Eval("Motivo")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="90px" HeaderText="Nota">
                            <ItemTemplate>
                                <asp:Label ID="lblNota" runat="server" Text='<%# Eval("Nota")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="90px" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                </asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                    AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                    PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                    UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
                    FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                    CurrentPageButtonClass="cpb">
                </webdiyer:AspNetPager>
            </div>
           <table width="100%" id="tbExportar" runat="server">
        <tr>
            <td class="tbright" style="font-size: 14px; color: #2F2F2F; font-weight: bold; font-family: Arial, Tahoma;">&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label13" runat="server" Text="Exportar a: "></asp:Label>
            </td>
            <td class="auto-style11">
                <asp:ImageButton ID="imgExportaExcel" ImageUrl="~/CentralModule/images/ImgExcel.gif" runat="server" OnClick="imgExportaExcel_Click" Height="35px" ToolTip="Exportar a Excel" />
            </td>
            <td class="auto-style11">
                <asp:ImageButton ID="imgExportaPDF" ImageUrl="~/CentralModule/images/Pdf.png" runat="server" OnClick="imgExportaPDF_Click" Height="35px" ToolTip="Exportar a PDF" />
            </td>
        </tr>
    </table>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgExportaExcel" />
<asp:PostBackTrigger ControlID="imgExportaPDF"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgExportaPDF"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="imgExportaPDF"></asp:PostBackTrigger>
        </Triggers>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgExportaPDF" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

