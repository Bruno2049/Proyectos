<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialModificaciones.aspx.cs" Inherits="PAEEEM.SolicitudCredito.HistorialModificaciones" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.3.2.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:updatepanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br/><br/>
            <%--AGREGADO--%>
            <asp:label runat="server" id="lblSinRegistros" style="font-size: medium; font-stretch: extra-expanded" Visible="false" Text="No hay historial" ></asp:label>
            <div>
                <asp:GridView ID="grdMovsSolicitud" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="20" DataKeyNames="IdSecuencia" >
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EditRowStyle CssClass="GridViewEditStyle" />
                    <Columns>
                        <asp:BoundField HeaderText="MOVIMIENTO" DataField="Descripcion" />
                        <asp:BoundField HeaderText="MOTIVO" DataField="Motivo" />
                        <asp:BoundField HeaderText="USUARIO" DataField="NombreUsuario" />
                        <asp:BoundField HeaderText="PERFIL" DataField="NombreRol" />
                        <asp:BoundField HeaderText="FECHA" DataField="Fecha"  DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="right"/>
                        <asp:BoundField HeaderText="HORA" DataField="Hora" ItemStyle-HorizontalAlign="right"/>
                        <asp:BoundField HeaderText="OBSERVACIONES" DataField="Observaciones" />
                    </Columns>
                </asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="8"
                                AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                                PageIndexBoxType="DropDownList" CustomInfoHTML="Página:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                                UrlPaging="false"
                                FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
                                CurrentPageButtonClass="cpb" OnPageChanged="AspNetPager_PageChanged" >
                    </webdiyer:AspNetPager>
                <br/>
                <table style="width: 100%">
                    <tr> 
                        <td style="text-align: center">
                            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" 
                                OnClick="btnRegresar_OnClick"/>
                        </td>

                        <td style="text-align: center">
                            <asp:Button ID="BtnSalir" runat="server" Text="Salir" 
                                onclick="BtnSalir_Click" />
                        </td>                     
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>
