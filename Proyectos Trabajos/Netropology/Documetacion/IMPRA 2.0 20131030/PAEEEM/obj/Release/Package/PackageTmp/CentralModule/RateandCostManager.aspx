<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RateandCostManager.aspx.cs"
    Inherits="PAEEEM.RateandCostManager" Title="Monitor de Tarifas y Costos" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Resources/Script/Calendar/WdatePicker.js" type="text/javascript"></script>

    <link href="../Resources/Script/Calendar/skin/default/datepicker.css" type="text/css" />
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .Label
        {
            color: #333333;
            font-size:16px;
        }
        .Label_1
        {
            color: Maroon;
            font-size:14pt;
        }
        .DropDownList
        {
            width: 18%;
        }
        .Button
        {
            width: 160px;
            margin-left:400%;
        }
        #dtFecha
        {
            float:right;    
        }
        #DropDownGroup
        {
            float:left;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <div>
        <br>
        <asp:Image runat="server"  ImageUrl="../SupplierModule/images/t_administracion.png" />
        </div>
        <table width="100%">
        <tr>
        <td width="890px">
        
        </td>
        <td align="right">
                       
                <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label"></asp:Label>
               </td><td> <asp:TextBox ID="txtFecha" runat="server" Enabled="False" BorderWidth="0" Width="50px"></asp:TextBox>
         
        </td>
        </tr>
        </table>
            
            <div id="DropDownGroup">
            <table width="100%">
            <tr>
            <td>
           
            <asp:Label ID="lblEstado" runat="server" Font-Size="11pt" Text="Estado"  CssClass="Label"></asp:Label>
            </td>
            <td>
            <asp:DropDownList ID="drpEstado" runat="server" Font-Size="11px"  CssClass="DropDownList" 
            AutoPostBack="True" onselectedindexchanged="drpEstado_SelectedIndexChanged" Width="200px"></asp:DropDownList>
            </td>
            <td width="150px">
            
            </td>
            <td>
            <asp:Label ID="lblPeriodo" runat="server" Font-Size="11pt" Text="Período" CssClass="Label"></asp:Label>
            </td>
            <td>
            <asp:TextBox ID="txtPeriodo" runat="server"  CssClass="Wdate" Font-Size="11px"
             onclick="WdatePicker({dateFmt:'yyyy-MM',lang:'en'})" 
              ontextchanged="txtPeriodo_TextChanged" AutoPostBack="True" Width="200px" ></asp:TextBox>
            </td>
            <td width="150px">
            
            </td>

            <td>
            <asp:Label ID="lblTarifa" runat="server" Text="Tarifa" Font-Size="11pt" CssClass="Label"></asp:Label>
            </td>
            <td>
            <asp:DropDownList ID="drpTarifa" runat="server" CssClass="DropDownList" Font-Size="11px" 
            AutoPostBack="True" onselectedindexchanged="drpTarifa_SelectedIndexChanged" Width="200px">
            </asp:DropDownList>
            </td>
            </tr>
            </table>
            
            </div>
            <br />
           <br />
            <div>
                <asp:GridView ID="gvTarifaCosto" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyNames="Fl_Tarifa_Costo" AllowSorting="True" 
                    CssClass="GridViewStyle" OnRowCommand="OnRowCommand" OnRowCreated="OnRowCreated">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EditRowStyle CssClass="GridViewEditStyle" />
                    <Columns>
                        <asp:BoundField DataField="Dx_Nombre_Estado" HeaderText="Estado" />
                        <asp:BoundField DataField="Dt_Periodo_Tarifa_Costo" HeaderText="Período" DataFormatString="{0: MM-yyyy}"/>
                        <asp:BoundField DataField="Dx_Tarifa" HeaderText="Tarifa" />
                        <asp:BoundField DataField="Mt_Costo_Kw_h_Fijo" HeaderText="Costo Fijo Kw/h" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="Mt_Costo_Kw_h_Basico" HeaderText="Costo Básico Kw/h" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="Mt_Costo_Kw_h_Intermedio" HeaderText="Costo Interm. Kw/h" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="Mt_Costo_Kw_h_Excedente" HeaderText="Costo Excedente Kw/h" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right"/>

                        <asp:BoundField DataField="Mt_Tarifa_Demanda" HeaderText="Tarifa Demanda" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="Mt_Costo_Tarifa_Consumo" HeaderText="Costo Tarifa Consumo" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="linkButtonEdit" Text="Editar" CommandName="Editar" runat="server" OnClientClick="return confirm('Confirmar Editar Tarifa/Costo');"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div>
                <webdiyer:AspNetPager ID="AspNetPager1" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                    AlwaysShow="True" ShowCustomInfoSection="Left" ShowDisabledButtons="false" ShowPageIndexBox="always"
                    PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                    UrlPaging="false" OnPageChanged="AspNetPager1_PageChanged" FirstPageText="Primero"
                    LastPageText="Pasado" NextPageText="Próximo" PrevPageText="Prev" CurrentPageButtonClass="cpb" OnPageChanging="AspNetPager_PageChanging">
                </webdiyer:AspNetPager>
            </div>
            <br />
            <div align="right">
                <asp:Button ID="btnAdd" runat="server" Text="Agregar Tarifa y Costo" CssClass="Button" OnClientClick="return confirm('Confirmar Agregar una Nueva Tarifa/Costo')"
                    OnClick="btnAdd_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
