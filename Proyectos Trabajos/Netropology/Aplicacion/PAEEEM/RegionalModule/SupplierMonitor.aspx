<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SupplierMonitor.aspx.cs"
    Inherits="PAEEEM.RegionalModule.SupplierMonitor" Title="Monitor de Proveedores" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 50px;
            color: #333333;
            font-size: 16px;
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
        function confirmCallBackFn(arg) {
            if (arg == true) {
                var oButton = document.getElementById("ctl00_MainContent_" + "HiddenButton");
                oButton.click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="container">
                <div>
                    <br/>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/supplierMonitor.png" />
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
                                <asp:Label ID="lblTipo" Text="Tipo" Font-Size="11pt" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpTipo" AutoPostBack="true" Font-Size="11px" runat="server"
                                    CssClass="DropDownList" OnSelectedIndexChanged="drpTipo_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="" Selected="True"/>
                                    <asp:ListItem Text="Proveedor Matriz" Value="M"/>
                                    <asp:ListItem Text="Proveedor Sucursal" Value="B"/>
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
                    <asp:GridView ID="grvSupplierMonitor" runat="server" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" AllowPaging="True" PageSize="20" DataKeyNames="ID,Type" 
                        ondatabound="grvSupplierMonitor_DataBound">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Tipo">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("Type").ToString()=="Proveedor" ? "Matriz" :Eval("Type").ToString()=="SB_F" ? "Sucursal Fisica" : "Sucursal Virtual" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Dx_Razon_Social" HeaderText="Nombre o Razón Social" />
                            <asp:BoundField DataField="Dx_Nombre_Comercial" HeaderText="Nombre Comercial" />
                            <asp:BoundField DataField="Dx_Nombre_Zona" HeaderText="Zona" />
                            <asp:BoundField DataField="Dx_Estatus_Proveedor" HeaderText="Estatus" />
                            <%--<asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="btnEdit_Click" OnClientClick="return confirm('Confirmar Editar Proveedor seleccionado');"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asignar Productos" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnAssignProduct" runat="server" Text="Asignar Productos" 
                                        onclick="btnAssignProduct_Click" OnClientClick="return confirm('Confirmar Asignar Productos al Proveedor seleccionado');"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asignar CAyD" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnAssignDisposal" runat="server" Text="Asignar CAyD" 
                                        onclick="btnAssignDisposal_Click" OnClientClick="return confirm('Confirmar Asignar CAyD al Proveedor seleccionado');"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
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
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
                        FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                        CurrentPageButtonClass="cpb">
                    </webdiyer:AspNetPager>
                </div>
                <br />
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
            <div style="display: none">
                <asp:Button ID="HiddenButton" BackColor="#FFFFFF" OnClick="HiddenButton_OnClick" runat="server" Width="0px" />
            </div>
            <br />
            <table width="100%">
                <tr>
                    <td align="center" style="width: 50%">
                        
                    </td>
                    <td align="right">
                        <asp:Button runat="server" ID="BtnAceptar" Text="Aceptar" OnClick="BtnAceptar_OnClick" /> &nbsp;&nbsp;
                        <asp:Button ID="btnAdd" Text="Agregar Proveedor" runat="server" 
                        CssClass="Button" onclick="btnAdd_Click" OnClientClick="return confirm('Confirmar Agregar un Nuevo Proveedor');"/>
                    </td>
                </tr>
            </table>
                <div align="center">
                    
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
