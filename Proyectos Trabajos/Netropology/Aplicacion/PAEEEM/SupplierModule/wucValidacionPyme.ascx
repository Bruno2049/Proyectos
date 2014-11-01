<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucValidacionPyme.ascx.cs" Inherits="PAEEEM.SupplierModule.wucValidacionPyme" %>

<div>
    <asp:Panel runat="server" ID="panelDatosEmpresa">
        <table style="width: 80%">
            <tr>
                <td colspan="5">
                    <asp:Image ID="Image1" runat="server" ImageUrl="images/t1.png" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <br/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Número de Servicio RPU:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TxtNoRPU" runat="server" Width="164px"></asp:TextBox>
                </td>            
            </tr>
            <tr>
                <td colspan="5">
                    <br/>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="color: #0099FF; font-weight: bold;">
                    DATOS DEL NEGOCIO
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <br/>
                </td>
            </tr>
            <tr>
                <td>
                    Giro de la Empresa:
                </td>
                <td>
                    <asp:DropDownList ID="DDXGiroEmpresa" runat="server"></asp:DropDownList>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td>
                    Nombre Comercial:
                </td>
                <td>
                    <asp:TextBox ID="TxtNombreComercial" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Sector:
                </td>
                <td>
                    <asp:DropDownList ID="DDXSector" runat="server"></asp:DropDownList>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td>
                    Número de Empleados:
                </td>
                <td>
                    <asp:TextBox ID="TxtNoEmpleados" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Total de Gastos Mensuales:
                </td>
                <td>
                    <asp:TextBox ID="TxtGastosMensuales" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td>
                    Promedio Ventas Anuales:
                </td>
                <td>
                    <asp:TextBox ID="TxtVentasAnuales" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <br/>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="color: #0099FF; font-weight: bold;">
                    DATOS DEL DOMICILIO DEL NEGOCIO
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <br/>
                </td>
            </tr>
            <tr>
                <td>
                    Codigo Postal:
                </td>
                <td>
                    <asp:TextBox ID="TxtCP" runat="server"></asp:TextBox>
                    &nbsp;
                    <asp:ImageButton ID="ImgBtnBuscarCP" runat="server" ImageUrl="~/SupplierModule/images/buscar.png" OnClick="ImgBtnBuscarCP_Click" />
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td>
                    Estado:
                </td>
                <td>
                    <asp:DropDownList ID="DDXEstado" runat="server" OnSelectedIndexChanged="DDXEstado_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Delegación o Municipio:
                </td>
                <td>
                    <asp:DropDownList ID="DDXMunicipio" runat="server" OnSelectedIndexChanged="DDXMunicipio_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td>
                    Colonia:
                </td>
                <td>
                    <asp:DropDownList ID="DDXColonia" runat="server" OnSelectedIndexChanged="DDXColonia_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="DDXColoniaHidden" runat="server" Visible="False"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel runat="server" ID="panelBusquedaColonia" Visible="False">
        <div style="width: 100%">
            <asp:GridView ID="grdColonias" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                AllowPaging="True" PageSize="8" DataKeyNames="CveCp" OnRowCreated="grdCamposCustomizables_RowCreated" Width="100%">
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
                                            <asp:ImageButton ID="BtnImgSeleccionar" runat="server" ImageUrl="~/SupplierModule/images/Select.png" 
                                                OnClientClick="return confirm('Esta seguro de seleccioar el Asentamiento');" OnClick="BtnImgSeleccionar_Click"/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
        </div>
        <div style="width: 100%; text-align: right;">
            <asp:Button ID="BtnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresar_Click" />
        </div>
    </asp:Panel>
</div>