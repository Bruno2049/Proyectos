<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CreditAuthorization.aspx.cs"
    Inherits="PAEEEM.CreditAuthorization" Title="Autorización de Solicitudes de Crédito" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 60px;
            color: #333333;
            font-size:16px;
            
        }
        .LabelStyle2
        {
            width: 250px;
            color: #333333;
            font-size:16px;

        }
        /** .DropDownList
        {
            width: 240px;
        }**/.Button
        {
            width: 120px;
        }
        .TextBox
        {
            width: 120px;
        }
        .dvpercent
        {
            width: 98%;
        }
        .part1
        {
            width: 100%;
            float: left;
        }
        .part2
        {
            width: 100%;
            float: right;
            text-align: right;
        }
        .divbtn
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <div id="container">
                <div class="dvpercent">
                <div><br> </div>
                    <div class="part1" style="background-image: url('../Resources/Images/t_aprovacion.png'); background-repeat:no-repeat; height:26px">
                        <asp:Label ID="lblApproveTitle" Text="APROBACION DE CREDITOS" runat="server" CssClass="LabelStyle2"
                            Font-Bold="True" Visible="false" />
                    <div class="part2" >
                    <table width="100%">
                    <tr> 
                    <td width="800px"></td>
                    <td width="45px">
                    <asp:Label ID="lblFecha" Text="Fecha" runat="server" CssClass="Label"  Width="45px"/>
                  
                    </td>
                    
                    <td width="90px">
                      <asp:TextBox ID="txtFecha" runat="server" Enabled="false" Width="90px"></asp:TextBox>
                 
                    </td>
                    </tr>
                    </table>
                                            
                    </div>
                </div>
                <br />
             
                    
                <table width="100%">
                <tr>
                <td>
                
                        <asp:Label ID="lblDistribute" Text="Proveedor" Font-Size="11pt" runat="server" CssClass="Label" />
                                               
                  
                </td>
                
                <td width="500px">
               			<asp:DropDownList ID="ddlDistribuidor" Font-Size="11px" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlDistribuidor_SelectedIndexChanged" Width="320px"/> 
                </td>
                <td width="800px"></td><td></td><td width="400px"></td>
                <td >
                     <asp:Label ID="Label1" Text="Sucursal" Font-Size="11pt" runat="server" CssClass="Label" Width="80px" />
                                                
                    
                </td>
                
                <td>
                <asp:DropDownList ID="drpBranch" runat="server" AutoPostBack="true"  Font-Size="11px"
                            onselectedindexchanged="drpBranch_SelectedIndexChanged" Width="320px"/>
                </td>
                                
                </tr>
                <tr>
                <td>
               
                <asp:Label ID="lblEstatus" Text="Estatus" Font-Size="11pt" runat="server" CssClass="Label" />
                </td>
                
                <td width="500px">
                <asp:DropDownList ID="ddlEstatus" AutoPostBack="true" Font-Size="11px" runat="server" 
                OnSelectedIndexChanged="ddlEstatus_SelectedIndexChanged" Width="320px"/>
                </td>
                <td width="800px"></td> <td></td><td width="400px"></td>
                <td>
                
                <asp:Label ID="lblTecno" Text="Tecnología" runat="server" Font-Size="11pt" CssClass="Label" />
                </td>
                
                <td>
                <asp:DropDownList ID="ddlTecno" AutoPostBack="true" runat="server" Font-Size="11px" 
                OnSelectedIndexChanged="ddlTecno_SelectedIndexChanged" Width="320px"/>
                </td>
                </tr>
                </table>
                
                    
                    
                </div>
               
                        
                        
                    </div>
                    
             
                <br />
                <div class="dvpercent">
                
              
                    <asp:GridView ID="gvCredit" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                        AllowPaging="True" PageSize="20" DataKeyNames="No_Credito">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" HorizontalAlign="Right" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:HyperLinkField HeaderText="Número Crédito" HeaderStyle-Font-Underline="true"
                                DataTextField="No_Credito" DataTextFormatString="{0:0000}" DataNavigateUrlFields="No_Credito,Cve_Estatus_Credito"
                                DataNavigateUrlFormatString="../RegionalModule/CreditReview.aspx?creditno={0} &statusid={1} &Flag =A" />
                            <asp:BoundField HeaderText="Nombre o Razón Social" DataField="Dx_Razon_Social" />
                            <asp:BoundField HeaderText="Monto Crédito Solicitado" DataField="Mt_Monto_Solicitado" DataFormatString="{0:C2}"  ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField HeaderText="Fecha Alta" DataField="Dt_Fecha_Pendiente" DataFormatString="{0: yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Fecha Último Estatus" DataField="Dt_Fecha_Estatus" DataFormatString="{0: yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="Estatus" DataField="Dx_Estatus_Credito"  />
                            <asp:TemplateField ShowHeader="True" HeaderText="Seleccionar" >
                               <ItemTemplate>
                               <div align="center">
                                    <asp:CheckBox ID="ckbSelect" runat="server"></asp:CheckBox></div>
                               </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                   
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20" CustomInfoTextAlign="Right" CustomInfoSectionWidth="33%"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="false" FirstPageText="Primero" LastPageText="Ultimo" NextPageText="Siguiente"
                        PrevPageText="Prev" CurrentPageButtonClass="cpb" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging">
                    </webdiyer:AspNetPager>
                </div>
                <div class="dvpercent" align="right">
                    <div class="divbtn" align="right" style="float:right;">
                <table width="100%">
                <tr><td width="342px"></td><td>
                    <td>   <asp:Button ID="btnRevision" runat="server" Text="En Revisión" OnClientClick="return confirm('Confirmar Cambiar Estatus a: &quot;En Revisión &quot;');"
                            CssClass="Button" OnClick="btnRevision_Click" />
                       </td><td> <asp:Button ID="btnAutorizado" runat="server" Text="Autorizar" OnClientClick="return confirm('Confirmar la Autorización de la Solicitud de Crédito Seleccionada');"
                            CssClass="Button" OnClick="btnAutorizado_Click" /> 
						
                       </td><td> <asp:Button ID="btnRechazado" runat="server" Text="Rechazar" CssClass="Button" OnClientClick="return confirm('Confirmar el Rechazo de la Solicitud de Crédito Seleccionada');"
                            OnClick="btnRechazado_Click" />
                       </td><td><asp:Button ID="btnPendiente" runat="server" Text="Pendiente" CssClass="Button" 
                            OnClientClick="return confirm('Confirmar dejar Pendiente la  Solicitud de Crédito Seleccionada');" 
                            onclick="btnPendiente_Click" />
                       <td></td>
                       </td><td><asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Button" 
                            OnClientClick="return confirm('Confirmar Cancelar la  Solicitud de Crédito Seleccionada');" 
                            onclick="btnCancelar_Click" />
                       </td>
            </td>  </tr>  </table>
                    </div>
                </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
