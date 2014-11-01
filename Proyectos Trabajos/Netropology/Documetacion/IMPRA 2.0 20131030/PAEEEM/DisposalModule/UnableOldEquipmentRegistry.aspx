<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnableOldEquipmentRegistry.aspx.cs"
    Inherits="PAEEEM.DisposalModule.UnableOldEquipmentRegistry" Title="Registro de Inhabilitación de Equipos" %>

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
            <div>
         
         			<br>
            		<asp:Image runat="server" ImageUrl="../images/t_inhabilitacion_equipos.png" />
                          
                </div>
            <div>
                <br>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/busqueda_inhabilitacion.png" />
            </div>
            <div align="right">
                <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
                <asp:Literal ID="literalFecha" runat='server' />
            </div>
            <br />
            <table width="100%">
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="2" style="text-align: right">
                        <asp:Literal ID="literal1" runat='server' Text="Fecha Recepción" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="Label2" Text="Programa" runat="server" CssClass="Label" /></div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpProgram" runat="server" AutoPostBack="true" CssClass="DropDownList"
                                OnSelectedIndexChanged="drpProgram_SelectedIndexChanged" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:Label ID="Label3" Text="Desde" runat="server" CssClass="Label" /></div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="Wdate" Width="200px"
                            onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'ctl00_MainContent_txtToDate\')}'})" ></asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:Label ID="Label4" Text="Hasta" runat="server" CssClass="Label" /></div>
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
                            <asp:Label ID="Label5" Text="No. Crédito" runat="server" CssClass="Label" /></div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpCredit" runat="server" AutoPostBack="true" CssClass="DropDownList"
                                OnSelectedIndexChanged="drpCredit_SelectedIndexChanged" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:Label ID="Label6" Text="Proveedor" runat="server" CssClass="Label" /></div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpDistributor" runat="server" CssClass="DropDownList" 
                                AutoPostBack="True" 
                                onselectedindexchanged="drpDistributor_SelectedIndexChanged" />
                        </div>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="Label7" Text="Folio Ingreso" runat="server" CssClass="Label" /></div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpInteralCode" runat="server" CssClass="DropDownList" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:Label ID="Label8" Text="Tecnología" runat="server" CssClass="Label" /></div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpTechnology" runat="server" CssClass="DropDownList" 
                                AutoPostBack="True" 
                                onselectedindexchanged="drpTechnology_SelectedIndexChanged" />
                        </div>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="Label9" Text="Tipo" runat="server" CssClass="Label" /></div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpType" runat="server" CssClass="DropDownList" 
                                AutoPostBack="True" onselectedindexchanged="drpType_SelectedIndexChanged">
                                <asp:ListItem Text="" Value="-1" />
                                <asp:ListItem Text="Sin Inhabilitar" Value="0" />
                                <asp:ListItem Text="Inhabilitados" Value="1" />
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td>
                    </td>
                    <td>
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
                        <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" 
                            OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?')" 
                            onclick="btnCancel_Click"/>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="grdReceivedOldEquipment" runat="server" AutoGenerateColumns="False"
                CssClass="GridViewStyle" AllowPaging="True" PageSize="20" OnRowCreated="grdReceivedOldEquipment_RowCreated"
                OnDataBound="grdReceivedOldEquipment_DataBound">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Dx_Nombre_Programa" HeaderText="Programa"></asp:BoundField>
                    <asp:BoundField DataField="Dx_Nombre_General" HeaderText="Tecnología" />
                    <asp:BoundField DataField="Id_Folio" HeaderText="Folio Ingreso" />
                    <asp:BoundField DataField="No_Credito" HeaderText="No. Crédito" />
                    <asp:BoundField DataField="Dt_Fecha_Recepcion" HeaderText="Fecha Recepción" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="Dx_Marca" HeaderText="Marca"></asp:BoundField>
                    <asp:BoundField DataField="No_Capacidad" HeaderText="Capacidad"></asp:BoundField>
                    <asp:BoundField DataField="Dx_Color" HeaderText="Color"></asp:BoundField>
                    <asp:BoundField DataField="ProviderComercialName" HeaderText="Proveedor"></asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Id_Credito_Sustitucion" HeaderText="Id_Credito_Sustitucion">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Con Foto">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfIsUpload" runat="server" Value='<%# Bind("IsUpload") %>' />
                            <asp:Image ID="imgFoto" runat="server" />
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
            <br />
            <table width="100%">
                <tr>
                    <td width="28%">
                        <asp:Label ID="lblInhabilitacionDate" runat="server" Text="Seleccione la Fecha de Inhabilitación"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtInhabilitacionDate" runat="server" CssClass="Wdate" Width="200px"
                            onfocus="WdatePicker()"></asp:TextBox>
                            <%--{minDate:'%y-%M-#{%d-1}'} RSA Temp change TODO enable again--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnRegistry" runat="server" Text="Registrar" CssClass="Button" OnClick="btnRegistry_Click" OnClientClick="return confirm('¿ Será Registrada la Fecha de Inhabilitación seleccionada a los Equipos elegidos !. Confirmar')"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
