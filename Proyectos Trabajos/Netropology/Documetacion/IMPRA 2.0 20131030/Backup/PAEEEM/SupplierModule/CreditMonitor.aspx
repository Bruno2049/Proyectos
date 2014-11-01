<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreditMonitor.aspx.cs" Inherits="PAEEEM.CreditMonitor" Title="Monitor de Crédito"%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
        .Label
        {
            width: 160px;
            color: #333333;
            font-size:16px;
        }
        .Label_1
        {
            width: 100px;
            color:#333333;   
            font-size:16px;                     
        }
        .DropDownList
        {
            width:330px    
        }
        .Button
        {
            width:120px;
        }
        .CenterButton
        {
            width:120px;
            margin-left:50%;
            margin-right:auto;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="container">
            
            <div>
            <br>
            <asp:Image runat="server" ImageUrl="images/t_monitor.png"/>
            </div>
            
                <div align="right">
                    <asp:Label Text="Fecha" runat="server" CssClass="Label"/>&nbsp 
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <table width="100%">
                <tr>
                                <td width="162px"><div><asp:Label Text="Fecha Ingreso" runat="server"  Font-Size="11pt" CssClass="Label" Width="90px" /></div>
                </td>
                                <td><div><asp:DropDownList ID="drpFechaDate" runat="server" Font-Size="11px" AutoPostBack="true" 
                        CssClass="DropDownList" 
                        onselectedindexchanged="drpFechaDate_SelectedIndexChanged" DataTextFormatString="{0:dd-MM-yyyy}"/></div>
                </td>
                                 <td width="160px"><div></div>
                </td>
                                 <td width="55px"><div> <asp:Label Text="Estatus" Font-Size="11pt" runat="server" CssClass="Label_1" Width="50px" /></div>
                </td>
                                 <td><div><asp:DropDownList ID="drpEstatus" AutoPostBack="true" Font-Size="11px" runat="server" 
                        CssClass="DropDownList" 
                        onselectedindexchanged="drpEstatus_SelectedIndexChanged" /></div>
                </td>

                </tr>
                 <tr>
                                <td width="162px"><div><asp:Label Text="Nombre o Razón Social" runat="server" Font-Size="11pt"  CssClass="Label"/></div>
                </td>
                                <td><div><asp:DropDownList ID="drpRazonSocial" runat="server" Font-Size="11px" AutoPostBack="true" 
                        CssClass="DropDownList" 
                        onselectedindexchanged="drpRazonSocial_SelectedIndexChanged"/></div>
                </td>
                                 <td width="160px"><div></div>
                </td>
                                 <td width="55px"><div> <asp:Label Text="Tecnología" Font-Size="11pt" runat="server" CssClass="Label_1"/></div>
                </td>
                                 <td><div><asp:DropDownList ID="drpTechnology" AutoPostBack="true" Font-Size="11px" runat="server" 
                        CssClass="DropDownList" 
                        onselectedindexchanged="drpTechnology_SelectedIndexChanged" /></div>
                </td>

                </tr>

                </table>
              
                <br />
                <div>
                    <asp:GridView ID="grdCredit" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="20" DataKeyNames="No_Credito"
                    OnRowCommand="OnRowCommand" OnRowCreated="OnRowCreated" OnDataBound="OnDataBound">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:HyperLinkField DataTextField="No_Credito" DataNavigateUrlFields="No_Credito, Cve_Estatus_Credito" 
                             DataNavigateUrlFormatString="../RegionalModule/CreditReview.aspx?creditno={0}&statusid={1}&Flag=M" HeaderText="Número Crédito" HeaderStyle-Font-Underline="true"/>
                        <asp:BoundField DataField="Dt_Fecha_Pendiente" HeaderText="Fecha Ingreso" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Width="80px"/>
                        <asp:BoundField DataField="Dx_Razon_Social" HeaderText="Nombre o Razón Social" />
                        <asp:BoundField DataField="Dx_Nombre_Repre_Legal" HeaderText="Representante" />
                        <asp:BoundField DataField="Dx_Tel_Fisc" HeaderText="Teléfono" />
                        <asp:BoundField DataField="Mt_Monto_Solicitado" HeaderText="Monto Solicitado" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="Dx_Estatus_Credito" HeaderText="Estatus" />                  
                        <asp:TemplateField HeaderText="Validar e Integrar">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkValidate" runat="server" CausesValidation="false" CommandName="Validate"
                                    OnClientClick="return confirm('Confirmar Realizar Validación e Integración del Expediente');" Text="Validar e Integrar" />
                                <asp:HiddenField ID="hdMOP" runat="server" Value='<%# Bind("No_MOP") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Editar" CommandArgument='<%# Bind("No_Credito") %>'
                                    OnClientClick="return confirm('Confirmar Editar crédito');" OnClick="btnEdit_Click" Visible="false"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                           <ItemTemplate>
                           <div align="center">
                                <asp:CheckBox ID="ckbSelect" runat="server"></asp:CheckBox></div>
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
                    UrlPaging="false" OnPageChanged="AspNetPager_PageChanged"  OnPageChanging="AspNetPager_PageChanging" FirstPageText="Primero"
                    LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior" CurrentPageButtonClass="cpb">
                </webdiyer:AspNetPager>
                </div>
                <br />
                <div align="right">
                    <table>
                    <tr>
                    <td>
                        <asp:Button ID="btnAddCredit" Text="Nueva Solicitud" runat="server" OnClick="btnAddCredit_Click" OnClientClick="return confirm(' ¿ Desea Realizar el Alta de una Nueva Solicitud de Crédito ?');" CssClass="CenterButton"/>
                    </td>
                    <td>
                        <asp:Button ID="btnCancelCredit" Text="Cancelar" runat="server" OnClick="btnCancelCredit_Click" OnClientClick="return confirm(' ¿ Está Seguro que Desea Cancelar las Solicitudes de Crédito seleccionadas ?');" CssClass="CenterButton"/>
                    </td>
                    </tr>
                    </table>
                </div>
            </div></ContentTemplate></asp:UpdatePanel></asp:Content>