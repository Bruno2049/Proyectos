using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Vendedores
{
    [Serializable]
    public class DatosVendedoresRegZon
    {
        public int IdVendedor { get; set; }
        public string Nombre { get; set; }
        public string Curp { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NoIdentificacion { get; set; }
        public string Estatus { get; set; }
        public string AccesoSistema { get; set; }
        public string Incidencia { get; set; }
        public string Anomalia { get; set; }
        public byte[] Archivo { get; set; }
        public int IdDistribuidor { get; set; }
        public string DistrNC { get; set; }
        public string DistrRS { get; set; }
        public string Region { get; set; }
        public string Zona { get; set; }

            // Id DISTRIBUIDOR"></telerik:GridBoundColumn>
            // DISTRIBUIDOR NC"></telerik:GridBoundColumn>
            // DISTRIBUIDOR RS"></telerik:GridBoundColumn>
            // ID VENDEDOR"></telerik:GridBoundColumn>
            // NOMBRE DEL VENDEDOR"></telerik:GridBoundColumn>
            // FECHA NACIMIENTO"></telerik:GridBoundColumn>
            // CURP"></telerik:GridBoundColumn>
            // REGION"></telerik:GridBoundColumn>
            // ZONA"></telerik:GridBoundColumn>
            // TIPO IDENTIFICACIÓN OFICIAL"></telerik:GridBoundColumn>
            // NO. IDENTIFICACION"></telerik:GridBoundColumn>
            // ACCESO AL SISTEMA"></telerik:GridBoundColumn>
            // ANOMALIA"></telerik:GridBoundColumn>
            // TIPO ANOMALIA"></telerik:GridBoundColumn>
            // ESTATUS"></telerik:GridBoundColumn>
        
        //            <telerik:GridTemplateColumn HeaderText="ACCIONES">
        //                <ItemTemplate>
        //                    <telerik:RadComboBox ID="LSB_Acciones" runat="server">
        //                        <Items>
        //                            <telerik:RadComboBoxItem runat="server" Text="Seleccione" Value="0" />
        //                        </Items>
        //                    </telerik:RadComboBox>
        //                </ItemTemplate>
        //            </telerik:GridTemplateColumn>
        
        //            <telerik:GridButtonColumn CommandName="View" Text="VER" UniqueName="colView" HeaderText="ARCHIVO"
        //                ButtonType="ImageButton" ImageUrl="~/images/lupa.png" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
        //            </telerik:GridButtonColumn>

        //            <telerik:GridTemplateColumn HeaderText="SELECCIONAR" ItemStyle-HorizontalAlign="Center">
        //                    <ItemTemplate>
        //                        <asp:CheckBox ID="ckbSelect" runat="server"  AutoPostBack="True" />
        //                    </ItemTemplate>
        //                </telerik:GridTemplateColumn>
    }
}
