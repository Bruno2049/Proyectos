<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidacionRFC.aspx.cs" Inherits="PAEEEM.CentralModule.ValidacionRFC" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    </style>
    <script type="text/javascript">
        function MostrarPDF(Id_Validacion) {

            var oWindowCust = $find('<%= RadWindow2.ClientID %>');
            var URL = "VisorPdf.aspx?Token=" + Id_Validacion;
            oWindowCust.setUrl(URL);
            oWindowCust.show();
        }

        function CerrarRadWinMot() {
            var Boton = document.getElementById('<%= Hidden.ClientID %>');
            Boton.click();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h2>
                <asp:Label ID="Label1" runat="server" Font-Size="Large" Font-Bold="True" Text="Validacion de RFC" />
            </h2>
            <hr class="rule" />
            <div id="ValidacionRFC" style="width: 100%" align="center" runat="server">
                <!-- -->
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>

                        <telerik:RadGrid runat="server" ID="GridViewRFC" AutoGenerateColumns="False" CellSpacing="0" AllowSorting="True"
                            AllowPaging="True" GridLines="None" OnPageSizeChanged="GridViewRFC_OnPageSizeChanged" OnPageIndexChanged="GridViewRFC_OnPageIndexChanged">

                            <PagerStyle Mode="NextPrevNumericAndAdvanced" Position="Bottom" AlwaysVisible="true"></PagerStyle>
                            <ClientSettings>
                                <Selecting CellSelectionMode="None" />
                            </ClientSettings>
                            <MasterTableView NoDetailRecordsText="No hay ninguna solicitud para validar" DataKeyNames="ID_Validacion">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Distribuidor_NC" UniqueName="Distribuidor_NC" HeaderText="Distribuidor NC" />
                                    <telerik:GridBoundColumn DataField="Distribuidos_RS" UniqueName="Distribuidos_RS" HeaderText="Distribuidor RS" />
                                    <telerik:GridBoundColumn DataField="Tipo_Persona" UniqueName="Tipo_Persona" HeaderText="Tipo de Persona" />
                                    <telerik:GridBoundColumn DataField="Nombre_RazonSocial" UniqueName="Nombre_RazonSocial" HeaderText="Nombre o Razon Social" />
                                    <telerik:GridBoundColumn DataField="Fecha_NacRegistro" UniqueName="Fecha_NacRegistro" DataFormatString="{0:d}" HeaderText="Fecha Nacimiento o Registro" />
                                    <telerik:GridBoundColumn DataField="RFC" UniqueName="RFC" HeaderText="RFC a Validar" />
                                    <telerik:GridTemplateColumn SortExpression="Lupa" UniqueName="Lupa" HeaderText="Ver">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/SupplierModule/images/buscar.png" ID="Lupa" runat="server" OnClientClick='<%# DataBinder.Eval(Container.DataItem, "ID_Validacion", "MostrarPDF({0}); return false")%>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn SortExpression="Seleccion" UniqueName="Seleccion" HeaderText="Seleccion">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CBX_Selecion" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <table width="100%" border="0" cellpadding="20" cellspacing="20">
                                        <tr>
                                            <td align="center">
                                                <h3 style="color: Black">No hay ninguna solicitud para validar</h3>
                                            </td>
                                        </tr>
                                    </table>
                                </NoRecordsTemplate>
                            </MasterTableView>

                        </telerik:RadGrid>
                        <%--<webdiyer:AspNetPager runat="server" ID="AspNetPager" CssClass="pagerDRUPAL" PageSize="20"
                                AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                                PageIndexBoxType="DropDownList" CustomInfoHTML="Página:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                                UrlPaging="false"
                                FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                                CurrentPageButtonClass="cpb" OnPageChanged="AspNetPager_PageChanged"></webdiyer:AspNetPager>--%>

                        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

                        <telerik:RadWindow ID="RadWindow1" runat="server" Width="550px" Height="550px" Modal="true" OnClientClose="CerrarRadWinMot">
                            <ContentTemplate>
                                <div style="padding: 10px; text-align: center; align-content: center; width: 100%; height: 100%;">
                                    <table style="text-align: center; width: 500px; vertical-align: central;">
                                        <tr>

                                            <td>

                                                <asp:Image ID="Image1" runat="server" ImageUrl="images/Alert.jpg" Width="50px" Height="50px" />
                                                Capture el motivos por el cual fue echazada la solicitud
                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <asp:TextBox runat="server" TextMode="MultiLine" Rows="5" MaxLength="150" ID="TBX_Motivos" Height="300px" Width="450px" />
                                                <asp:TextBox runat="server" ID="TXTRFC" Visible="false" />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnFinalizar" runat="server" OnClick="btnFinalizar_Click" Text="Enviar Validacion" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </telerik:RadWindow>
                        <telerik:RadWindow ID="RadWindow2" runat="server" Width="700px" Height="500px" Modal="true" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <table style="width: 100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnValidar" Text="Confirma" runat="server" OnClick="btnValidar_Click"  />
                        <asp:Button ID="btnRechazar" Text="Rechazar" runat="server" OnClick="btnRechazar_Click" />
                        <asp:Button ID="btnSalir" Text="Salir" runat="server" Width="81px" OnClick="btnSalir_Click" />
                        <asp:Button ID="Hidden" runat="server" Style="display: none" OnClick="Hidden_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
