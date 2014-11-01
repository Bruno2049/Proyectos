<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CatalogConfiguration.aspx.cs"
    Inherits="PAEEEM.CentralModule.CatalogConfiguration" Title="Configuración de Catálogos" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
	<link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
	<asp:UpdatePanel runat="server" ID="catalog">
        <ContentTemplate>
            <div>
         
         			<br>
            		<asp:Image runat="server" ImageUrl="../images/t_catalogo.png" />
                          
                </div>
            <br />
            <div>
                <asp:Label runat="server" ID="lblCatalog" Text="Seleccione un Catálogo" />&nbsp;&nbsp;&nbsp;&nbsp
                <asp:DropDownList runat="server" ID="drpCatalog" AutoPostBack="true" Width="20%" 
                    onselectedindexchanged="drpCatalog_SelectedIndexChanged"/>
            </div>
            <br />
            <div align="center">
                <asp:GridView runat="server" ID="gridViewFields" AutoGenerateColumns="false" 
                    CssClass="GridViewStyle">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:BoundField ConvertEmptyStringToNull="true" HeaderText="Nombre Campo" DataField="ColumnName" />
                        <asp:BoundField ConvertEmptyStringToNull="true" HeaderText="Tipo Dato" DataField="DataType" />
                        <asp:TemplateField HeaderText="Es Llave Primaria" >
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="checkPrimary" AutoPostBack="true" OnCheckedChanged="checkboxPrimary_CheckedChanged"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Es Llave Foránea" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="checkForeign" AutoPostBack="true" OnCheckedChanged="ForeignKey_CheckedChanged"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dependencia" HeaderStyle-Wrap="false" HeaderStyle-Width="220px">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="drpParentTable" AutoPostBack="true" Width="100%" Enabled="false" OnSelectedIndexChanged="parentTable_SelectedIndexChanged" OnDataBound="parentTable_DataBound"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID Campo" HeaderStyle-Wrap="false" >
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="drpParentColumn" AutoPostBack="false" Width="100%" Enabled="false" OnDataBound="ParentColumn_DataBound"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre Campo" >
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="drpDisplayColumn" AutoPostBack="false" Width="100%" Enabled="false" OnDataBound="DisplayColumn_DataBound"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mostrar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="checkDisplay"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre a Visualizar" >
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtDisplayName" Width="100%"/>
                            </ItemTemplate>
                        </asp:TemplateField>      
                        <asp:TemplateField HeaderText="Es Identidad" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="checkIdentity" Enabled="false"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Es Obligatorio" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="checkMandatory" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Es Correo Electrónico" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="checkEmail" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Es Teléfono" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="checkPhone" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Es Código Postal" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="checkZipCode" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <div align="center">
                <asp:Button runat="server" ID="btnSetup" Text="Guardar" 
                    OnClientClick="return confirm('Confirmar Guardar Configuración');" 
                    onclick="btnSetup_Click" Visible="false"/>&nbsp;&nbsp;&nbsp;&nbsp
                <asp:Button runat="server" ID="btnCancel" Text="Salir" 
                    OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');" 
                    onclick="btnCancel_Click" Visible="false"/>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
