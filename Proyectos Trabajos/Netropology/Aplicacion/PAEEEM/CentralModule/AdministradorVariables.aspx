<%@ Page Title=""  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministradorVariables.aspx.cs" Inherits="PAEEEM.CentralModule.AdministradorVariables" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="Stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="Label1" runat="server" Font-Size="Large" Font-Bold="True" Text="VARIABLES GLOBALES" />
    </h2>
    <hr class="rule" />
    <div id="div_VariablesGlobales" style="width: 100%" align="center" runat="server">
        <asp:UpdatePanel ID="UpdatePanelVG" runat="server" EnableViewState="false" >
            <ContentTemplate>
                <asp:GridView ID="gvVariablesGlobales"  runat="server" aling="center" Width="80%" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="11pt" AllowPaging="True" OnRowEditing="EditVG"  OnRowCancelingEdit="CancelEditVG" OnRowUpdating="GuardaDatosVG" 
                     CssClass="GridViewStyle" PageSize="15">
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyleNet" />
                    <PagerSettings Visible="false" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EditRowStyle CssClass="GridViewEditStyle" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="IdParametro" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdP" runat="server" Text='<%# Eval("IdParametro")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="IdSeccion" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdS" runat="server" Text='<%# Eval("IdSeccion")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="ID_Prog_Proy" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdPP" runat="server" Text='<%# Eval("ID_Prog_Proy")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="55%" HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Descripcion")%>'  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="30%" HeaderText="Valor" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblValor" runat="server" Text='<%# Eval("valor")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtValor" runat="server" Text='<%# Eval("valor")%>' MaxLength="11"/><br />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Image" ShowEditButton="true"  ItemStyle-HorizontalAlign="Center" EditImageUrl="~/CentralModule/images/editar-icono.png" HeaderText="Editar" UpdateText="Actualizar"  CancelImageUrl="~/images/Cancelar.png" UpdateImageUrl="~/images/Actualizar.png" />                       
                    </Columns>
                </asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>
        <webdiyer:AspNetPager ID="AspNetPager2" CssClass="pagerDRUPAL" runat="server" PageSize="15" OnPageChanged="AspNetPager2_PageChanged"
                                AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                                PageIndexBoxType="DropDownList" CustomInfoHTML="Página:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                                UrlPaging="false" FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                                CurrentPageButtonClass="cpb" >
                            </webdiyer:AspNetPager>
    </div>
    <br />
            <table style="width: 100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSalir" Text="Salir" runat="server" Width="81px" OnClick="btnSalir_Click" />
                    </td>
                </tr>
            </table>
</asp:Content>
