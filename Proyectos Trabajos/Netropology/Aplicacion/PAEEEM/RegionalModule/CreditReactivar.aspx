<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreditReactivar.aspx.cs" Inherits="PAEEEM.RegionalModule.CreditReactivar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="Stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../Resources/Css/TablaNet.css" rel="stylesheet" type="text/css" />

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

    <script type="text/javascript">

        function confirmCallBackFn(arg) {
            if (arg == true) {
                var oButton = document.getElementById("ctl00_MainContent_" + "HiddenButton");
                oButton.click();
            }
        }

        function Validarfiltros() {
            var rpu = document.getElementById('<%= TxtNoRPU.ClientID %>');
            var credit = document.getElementById('<%= TxtNoCredito.ClientID %>');
            var cli = document.getElementById('<%= TxtCliente.ClientID %>');
            var nomCom = document.getElementById('<%= TxtNomComercial.ClientID %>');
            var reg = document.getElementById('<%= DDXRegion.ClientID %>');
            var zon = document.getElementById('<%= DDXZona.ClientID %>');

            if (rpu.value.length > 0 || credit.value.length > 0 || cli.value.length > 0 || nomCom.value.length > 0 || reg.value != "0" || zon.value != "0") {

                document.getElementById('<%=btnBuscar.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%=btnBuscar.ClientID%>').disabled = true;
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="3" style="color: #0099FF; font-weight: bold; font-size:large;">REACTIVACIÓN DE CREDITOS</td>
            </tr>
        </table>
        <br/>
        <br/> 
        <table style="width: 100%; background:#99CCFF; height: 71px;" >
            <tr>
                <td colspan="3" align="center">RPU:
                    <asp:TextBox ID="TxtNoRPU"  runat="server" Width="111px" Enabled="true"/>
                   
                </td>
                <td colspan="3" align="center">NO.CREDITO:
                    <asp:TextBox ID="TxtNoCredito" runat="server" Width="150px"  Enabled="true" />
                </td>
                <td colspan="3" align="center">CLIENTE:
                    <asp:TextBox ID="TxtCliente" runat="server" Width="170px"  Enabled="true" />
                </td>
                <td colspan="3" align="center">NOMBRE COMERCIAL:
                    <asp:TextBox ID="TxtNomComercial" runat="server" Width="170px"  Enabled="true" />
                </td>
                <td colspan="3" align="center">REGIÓN:
                    <asp:DropDownList ID="DDXRegion" runat="server" Width="140px" AutoPostBack="True" Height="16px"  OnTextChanged="DDXRegion_TextChanged" OnSelectedIndexChanged="DDXRegion_SelectedIndexChanged"/>
                </td>
                <td colspan="3" align="center">ZONA:
                    <asp:DropDownList ID="DDXZona" runat="server" Width="140px" AutoPostBack="True" OnTextChanged="DDXZona_TextChanged" />
                </td>
            </tr>
        </table>
        <table style="width: 100%" >
             <tr>
                <td align="center">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Enabled="False"  />
                </td>
            </tr>
        </table>

    </div>
        <br />
        <br />
    <div>
        <table style="width: 100%">
            <tr>
                <td>
                    <div id="div_Reactivar" style="width: 100%" align="center" runat="server">
                    <asp:UpdatePanel ID="UpdatePaneReactivar"  runat="server">
                    <ContentTemplate>
                    <asp:GridView runat="server" ID="gvReactivar" AutoGenerateColumns="False" CssClass="GridViewStyle"
                            AllowPaging="True" >
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                                    <PagerSettings Visible="False" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />

                        <Columns>
                                <asp:BoundField DataField="RPU" HeaderText="RPU"></asp:BoundField>
                                <asp:BoundField DataField="NoCredito" HeaderText="No de Credito"></asp:BoundField>
                                <asp:BoundField DataField="Cliente" HeaderText="Cliente"></asp:BoundField>
                                <asp:BoundField DataField="NomComercial" HeaderText="Nombre Comercial"></asp:BoundField>
			                  <%--  <asp:BoundField DataField="FechCancelacion" HeaderText="Fecha de Cancelacion"></asp:BoundField>--%>
                                <asp:TemplateField ItemStyle-Width="9%" HeaderText="Fecha de Cancelacion"  >
                                    <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# Convert.ToDateTime(Eval("FechCancelacion")).ToShortDateString()%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
			                    <asp:BoundField DataField="UltimoStatus" HeaderText="Último Estatus"></asp:BoundField>
			                    <asp:BoundField DataField="MotivoCancel" HeaderText="Motivo de Cancelación"></asp:BoundField>
			                    <asp:BoundField DataField="Region" HeaderText="Región"></asp:BoundField>
			                    <asp:BoundField DataField="Zona" HeaderText="Zona"></asp:BoundField>
                                <asp:TemplateField ShowHeader="True" HeaderText="Reactivar">
                                    <ItemTemplate>
                                        <div>
                                        <asp:CheckBox ID="ckbSelect" runat="server" AutoPostBack="true"  OnCheckedChanged="ckbSelect_CheckedChanged"></asp:CheckBox>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="10"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Página:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="false"
                        FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                        CurrentPageButtonClass="cpb" OnPageChanged="AspNetPager_PageChanged" >
                    </webdiyer:AspNetPager>
                    
                    
                    <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false" /> 
                    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager> 
                    <div style="display: none">
                        <asp:Button ID="HiddenButton" BackColor="#FFFFFF" OnClick="HiddenButton_Click" runat="server" Width="0px" />
                    </div>
                    <br />
                    <br />
                    <table style="width: 100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnReactivar" runat="server" Text="Reactivar" OnClick="btnReactivar_Click" Enabled="False"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSalir" runat="server" Text="Salir" OnClick="btnSalir_Click" />
                            </td>
                        </tr>
                    </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
          </td>
         </tr>
       </table>              
    </div>

</asp:Content>
