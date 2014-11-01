<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucBusquedaColonias.ascx.cs" Inherits="PAEEEM.SupplierModule.Controls.wucBusquedaColonias" %>

<div>
    Hola
    <fieldset class="login">
        <legend style="border-color: #3333FF; color: #0066FF">ASENTAMIENTOS ENCONTRADOS</legend>
        <table style="text-align: center; width: 100%">
                <tr>
                    <td style="text-align: center">
                        <asp:GridView ID="grdColonias" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                    AllowPaging="True" PageSize="8" DataKeyNames="CveCp" Width="100%" HorizontalAlign="Center">
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle2" />
                                    <HeaderStyle BackColor="#0033CC" ForeColor="White" Font-Size="Small" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle2" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="CveCp" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="CodigoPostal" HeaderText="C.P."></asp:BoundField>
                                        <asp:BoundField DataField="DxColonia" HeaderText="Asentamiento"></asp:BoundField>
                                        <asp:BoundField DataField="DxTipoColonia" HeaderText="Tipo Asentamiento"></asp:BoundField>
                                        <asp:BoundField DataField="DxDelegacionMunicipio" HeaderText="Delegación/Municipio"></asp:BoundField>
                                        <asp:BoundField DataField="DxEstado" HeaderText="Estado"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckbSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="BtnColonia" runat="server" Text="Button" OnClick="BtnColonia_Click" />
                    </td>
                </tr>
            </table>
    </fieldset>   
</div>