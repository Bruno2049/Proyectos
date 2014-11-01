<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SupervisionDateRegistry.aspx.cs"
    Inherits="PAEEEM.RegionalModule.SupervisionDateRegistry" Title="Registro de Inspección de Equipos" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />

    <script src="../Resources/Script/Calendar/WdatePicker.js" type="text/javascript"></script>

    <link href="../Resources/Script/Calendar/skin/default/datepicker.css" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 90px;
            color: #333333;
            font-size: 16px;
        }
        .DropDownList
        {
            width: 250px;
        }
        .Button
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="container">
                <div align="center">
                    <br>
                    <!--<asp:Label ID="lblTitle" runat="server" Text="BUSQUEDA DE BOLETAS PARA INSPECCION"></asp:Label>-->
                </div>
                <br />
                <img src="../images/t_boletas.png" />
<table width="100%">
    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td colspan="3" style="text-align: center">
                            <asp:Literal ID="literal1" runat='server' Text="Fecha Recepción" />                           
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="lblProgram" Text="Programa" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpProgram" runat="server" AutoPostBack="true" CssClass="DropDownList"
                                    OnSelectedIndexChanged="drpProgram_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblFromDate" Text="Desde" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="Wdate" Width="200px" onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'ctl00_MainContent_txtToDate\')}'})"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblToDate" Text="Hasta" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="Wdate" Width="200px" onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_MainContent_txtFromDate\')}'})"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="lblTechnology" runat="server" CssClass="Label" Text="Tecnología" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpTechnology" runat="server" CssClass="DropDownList"  AutoPostBack="true"
                                    onselectedindexchanged="drpTechnology_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblCAyD" Text="CAyD" Font-Size="11pt" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpCAyD" AutoPostBack="true" Font-Size="11px" runat="server"
                                    onselectedindexchanged="drpCAyD_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnSearch" runat="server" Text="Buscar" CssClass="Button" OnClick="btnSearch_Click" />
                        </td>
                        <td>
                        </td>
                        <td style="text-align: left">
                            <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClick="btnCancel_Click" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"/>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right">
               
                        </td>
                        <td>
                        </td>
                        <td style="text-align: left">

                        </td>
                        <td>
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: left">
                             <asp:Label ID="lblAll" runat="server" Text="Seleccionar Todos" Visible="false" />
                             <asp:CheckBox ID="ckbSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="ckbSelectAll_CheckedChanged" Visible="false" />
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:GridView ID="grdReceivedOldEquipment" runat="server" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" AllowPaging="True" PageSize="20" 
                        DataKeyNames="Id_Credito_Sustitucion">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Id_Folio" HeaderText="Folio Ingreso" DataFormatString="{0:000000}"/>
                            <asp:BoundField DataField="No_Capacidad" HeaderText="Capacidad"></asp:BoundField>
                            <asp:BoundField DataField="Dx_Marca" HeaderText="Marca"></asp:BoundField>
                            <asp:BoundField DataField="No_Credito" HeaderText="Crédito"></asp:BoundField>
                            <%--<asp:BoundField DataField="No_Serial" HeaderText="No. Serie" />--%>
                            <asp:BoundField DataField="ProviderComercialName" HeaderText="Proveedor" />
                            <asp:BoundField DataField="Dx_Color" HeaderText="Color" />
                            <asp:BoundField DataField="Dt_Fecha_Recepcion" HeaderText="Fecha Recepción" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="Dt_Fecha_Inhabilitacion" HeaderText="Fecha Inhabilitación"
                                DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="1" HeaderText="Gas" DataFormatString="{0:dd-MM-yyyy}" Visible="false"/>
                            <asp:BoundField DataField="2" HeaderText="Aceite" DataFormatString="{0:dd-MM-yyyy}" Visible="false"/>
                            <asp:BoundField DataField="3" HeaderText="Cobre" DataFormatString="{0:dd-MM-yyyy}" Visible="false"/>
                            <asp:BoundField DataField="4" HeaderText="Aluminio" DataFormatString="{0:dd-MM-yyyy}" Visible="false"/>
                            <asp:BoundField DataField="5" HeaderText="Fierro" DataFormatString="{0:dd-MM-yyyy}" Visible="false"/>
                            <asp:BoundField DataField="6" HeaderText="Otros" DataFormatString="{0:dd-MM-yyyy}" Visible="false" />
                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbSelect" runat="server" />
                                </ItemTemplate>
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
                <table width="100%" id="Registry" runat="server">
                    <tr>
                        <td width="28%">
                            <asp:Label ID="lblInspect" runat="server" Text="Seleccione la Fecha de Inspección"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInSpect" runat="server" CssClass="Wdate" Width="200px" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnRegistry" runat="server" Text="Registrar" CssClass="Button" OnClientClick=" return confirm('Confirmar, ya que será Registrada la Fecha de Inspección en los Equipos seleccionados');"
                                OnClick="btnRegistry_Click" />
                        </td>
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
