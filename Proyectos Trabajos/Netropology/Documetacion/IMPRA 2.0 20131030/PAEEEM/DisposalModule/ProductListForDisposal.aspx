<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ProductListForDisposal.aspx.cs"
    Inherits="PAEEEM.DisposalModule.ProductListForDisposal" Title="Entrega-Recepción de Equipo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
            var last=null;
            function judge(obj)
            {
              if(last==null)
              {
                 last=obj.id;
              }
              else
              {
                var lo=document.getElementById(last);
                lo.checked=false;
                last=obj.name;
              }
              obj.checked="checked";
            }
    </script>

    <style type="text/css">
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
                <div>
                    <br>
                    <asp:Image ID="Image1" runat="server" ImageUrl="" />
                </div>
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <div>
                    <asp:GridView ID="grdProductList" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        AllowPaging="True" PageSize="20" DataKeyNames="No_Credito,Cve_Tecnologia, Fg_Conformidad,Peso_Producto"
                        OnDataBound="grdProductList_DataBound" OnRowDataBound="grdProductList_RowDataBound">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Dx_Nombre_General" HeaderText="Tecnología"></asp:BoundField>
                            <asp:BoundField DataField="Dx_Tipo_Producto" HeaderText="Tipo Producto" />
                            <asp:BoundField DataField="Dx_Modelo_Producto" HeaderText="Modelo" />
                            <asp:BoundField DataField="Dx_Marca" HeaderText="Marca" />
                            <asp:BoundField DataField="No_Serial" HeaderText="No. Serie" />
                            <asp:BoundField DataField="Dx_Color" HeaderText="Color" />
                            <asp:BoundField DataField="Dx_Peso" HeaderText="Peso" />
                            <asp:BoundField DataField="Ft_Capacidad" HeaderText="Capacidad" />
                            <asp:BoundField DataField="Dx_Antiguedad" HeaderText="Antiguedad" />
                            <asp:TemplateField HeaderText="Conformidad" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                        BorderStyle="None" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="Y">Si</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Producto Peso" >
                                <ItemTemplate>
                                    <asp:TextBox ID="TxtPeso" runat="server" AutoPostBack="true"  ontextchanged="TxtPeso_TextChanged" Enabled="false" Width="100%">
                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Seleccionar" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="true" OnPageChanged="AspNetPager_PageChanged" FirstPageText="Primero"
                        LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior" CurrentPageButtonClass="cpb">
                    </webdiyer:AspNetPager>
                </div>
                <br />
                <div>
                    <asp:Button ID="btnEdit" Text="Editar" runat="server" OnClientClick="return confirm(' ¿ Desea editar los datos del equipo seleccionado ?');"
                        CssClass="Button" OnClick="btnEdit_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnUploadImage" runat="server" Text="Cargar Fotografía" CssClass="Button"
                        OnClientClick="return confirm(' Confirmar la carga de imágenes que soportarán los Equipos Recibidos.  Sólo pueden ser hasta 3 imágenes en formato .jpeg, .png o .bmp.');"
                        OnClick="btnUploadImage_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnPrint" runat="server" Text="Imprimir Boleta" CssClass="Button"
                        OnClientClick="return confirm('Confirmar Imprimir Boleta de Entrega-Recepción del equipo seleccionado');"
                        OnClick="btnPrint_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
