<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleCredito.aspx.cs" Inherits="PAEEEM.SolicitudCredito.DetalleCredito" %>

<%@ PreviousPageType VirtualPath="~/SolicitudCredito/DatosCredito.aspx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HyperLink runat="server" ID="HLRegresar" Font-Bold="true" Text="Regresar" NavigateUrl="#" Font-Underline="true"></asp:HyperLink>
    <table style="padding: 0px; margin: 0px; border-width: 0px; background-color: #3399FF">
        <tr>
            <td width="1100"></td>
        </tr>
    </table>

    <div id="divD">
        <asp:Table runat="server" Visible="false" ID="InfoDistr" Width="100%">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Label ID="Label12" runat="server" Text="INFORMACIÓN DEL DISTRIBUIDOR" Style="font-weight: 700"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label13" runat="server" Text="RAZÓN SOCIAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblRazonSocialDist" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label14" runat="server" Text="NOMBRE COMERCIAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNombreComercialDist" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label16" runat="server" Text="CONTACTO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblContactoDist" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label18" runat="server" Text="CORREO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCorreoDist" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small" ColumnSpan="2">
                    <asp:Label ID="Label20" runat="server" Text="TELÉFONO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTelefonoDist" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Label ID="Label15" runat="server" Text="DATOS DOMICILIOS" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Label ID="Label29" runat="server" Text="FISCAL" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label17" runat="server" Text="CÓDIGO POSTAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCPDistF" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label21" runat="server" Text="ESTADO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEstDistF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label23" runat="server" Text="DELEGACIÓN O MUNICIPIO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblDMDistF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label25" runat="server" Text="COLONIA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblColDistF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label27" runat="server" Text="CALLE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCaDistF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label30" runat="server" Text="NUMERO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumDistF" Font-Size="X-Small" runat="server" Text="000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Label ID="Label32" runat="server" Text="NEGOCIO" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label33" runat="server" Text="CÓDIGO POSTAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCPDistN" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label35" runat="server" Text="ESTADO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEstDistN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label37" runat="server" Text="DELEGACIÓN O MUNICIPIO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblDMDistN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label39" runat="server" Text="COLONIA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblColDistN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label41" runat="server" Text="CALLE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCaDistN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label43" runat="server" Text="NÚMERO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumDistN" Font-Size="X-Small" runat="server" Text="000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

    <table style="padding: 0px; margin: 0px; border-width: 0px; background-color: #3399FF">
        <tr>
            <td width="1100"></td>
        </tr>
    </table>

    <div>
        <asp:Table runat="server" ID="links" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:HyperLink ID="HlinkEquipos" runat="server" NavigateUrl="#" Text="EQUIPOS" Font-Underline="true"></asp:HyperLink>&nbsp;&nbsp;
                    <asp:HyperLink ID="HlinkHistModificaciones" runat="server" NavigateUrl="#" Text="HISTORIAL DE MODIFICACIONES" Font-Underline="true"></asp:HyperLink>
                    <%--<asp:LinkButton ID="linkHistorialModificaciones" runat="server" Text="HISTORIAL DE MODIFICACIONES" Font-Underline="true"></asp:LinkButton>--%>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <div>
        <asp:Table runat="server" ID="InfoSolicitud" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="3">
                    <asp:Label ID="Label45" runat="server" Text="INFORMACIÓN SOLICITUD" Style="font-weight: 700"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="3">
                    <asp:Label ID="Label2" runat="server" Text="No. Solicitud:  " Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblNoSolicitud" runat="server" Text="000000000" Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="3">
                    <asp:Label ID="Label46" runat="server" Text="RPU:  " Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblRPUtbl" runat="server" Text="000000000" Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label49" runat="server" Text="TARIFA:  " Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTarifatbl" runat="server" Text="0000" Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label48" runat="server" Text="ESTATUS:  " Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblEstatustbl" runat="server" Text="00000000000000000" Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label4" runat="server" Text="Fecha Primer amortizacion:  " Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblFechaAmortizacion" runat="server" Text="" Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    <table id="Table6" width="100%" runat="server">
                    <tr>
                        <td class="auto-style11">
                            <asp:ImageButton ID="imgExportaPDF" ImageUrl="~/CentralModule/images/Pdf.png" runat="server" OnClick="imgExportaPDF_Click" Height="35px" ToolTip="Exportar a PDF" /><br />
                            <asp:Label ID="Label3" runat="server" Text="Tabla Amortizacion" Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="3">
                    <asp:Label ID="Label52" runat="server" Text="ETAPAS DEL CREDITO" Style="font-weight: 700"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="3">
                    <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" BorderStyle="Ridge" GridLines="Both">
                        <asp:TableHeaderRow Font-Size="X-Small" BackColor="#c0c0c0">
                            <asp:TableHeaderCell>USUARIO QUE MODIFICÓ</asp:TableHeaderCell>
                            <asp:TableHeaderCell>ÚLTIMA MODIFICACIÓN</asp:TableHeaderCell>
                            <asp:TableHeaderCell>PENDIENTE</asp:TableHeaderCell>
                            <asp:TableHeaderCell>POR ENTREGAR</asp:TableHeaderCell>
                            <asp:TableHeaderCell>EN REVISIÓN</asp:TableHeaderCell>
                            <asp:TableHeaderCell>AUTORIZADO</asp:TableHeaderCell>
                            <asp:TableHeaderCell>RECHAZADO</asp:TableHeaderCell>
                            <asp:TableHeaderCell>MOP NO VALIDO</asp:TableHeaderCell>
                            <asp:TableHeaderCell>CANCELADO</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblUsuModtbl" runat="server" Text="0000000000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblUltModtbl" runat="server" Text="00/00/0000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblPendtbl" runat="server" Text="00/00/0000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblPEntrtbl" runat="server" Text="00/00/0000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblERevtbl" runat="server" Text="00/00/0000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblAuttbl" runat="server" Text="00/00/0000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblRechtbl" runat="server" Text="00/00/0000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblMOPNoValtbl" runat="server" Text="00/00/0000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblCanctbl" runat="server" Text="00/00/0000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                    <asp:Label ID="Label62" runat="server" Text="CONSULTA CREDITICIA" Font-Size="Smaller" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                    <%--HISTORIAL DE CONSULTAS CREDITICIAS--%>
                    <asp:Table runat="server" HorizontalAlign="Center" BorderStyle="Ridge" GridLines="Both">
                        <asp:TableHeaderRow Font-Size="X-Small" BackColor="#c0c0c0">
                            <asp:TableHeaderCell>No. Consulta</asp:TableHeaderCell>
                            <asp:TableHeaderCell>MOP</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Folio</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Fecha Consulta</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblNoConsulta" runat="server" Text="0000000000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblMOP" runat="server" Text="0000000000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblFolio" runat="server" Text="0000000000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblFecConsulta" runat="server" Text="0000000000" Font-Size="Smaller" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>

                    </asp:Table>
                    <asp:HyperLink ID="HyperLink3" runat="server" Text="Consultar Histórico Consultas" Font-Underline="true"></asp:HyperLink>&nbsp;&nbsp;
                </asp:TableCell><asp:TableCell>
                    <asp:Table ID="Table3" runat="server" BorderStyle="Ridge" GridLines="Both">
                        <asp:TableRow BackColor="#c0c0cf">
                            <asp:TableCell>
                                <asp:Label ID="Label66" runat="server" Text="No. INTELISIS" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblNoIntetbl" runat="server" Text="0000000000" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow BackColor="#cccccc">
                            <asp:TableCell>
                                <asp:Label ID="Label67" runat="server" Text="FECHA PAGO DISTRIBUIDOR" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblfecPagDisttbl" runat="server" Text="00/00/0000" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                            </asp:TableCell>

                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label70" runat="server" Text="MONTO PAGADO" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblMonPagtbl" runat="server" Text="0000000000" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <table style="padding: 0px; margin: 0px; border-width: 0px; background-color: #3399FF">
        <tr>
            <td width="1100"></td>
        </tr>
    </table>
    <div>
        <asp:Table runat="server" ID="InfoCli" Visible="true" Width="100%">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label72" runat="server" Text="DATOS DEL CLIENTE" Style="font-weight: 700"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label74" runat="server" Text="TIPO DE PERSONA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTipPersCli" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="lblNombreRSCli" runat="server" Text="NOMBRE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNomRSCli" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="35%" Style="font-size: x-small">
                    <asp:Label ID="Label78" runat="server" Text="NOMBRE COMERCIAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNomComCli" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="15%" Style="font-size: x-small">
                    <asp:Label ID="Label80" runat="server" Text="RFC: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblRFCCli" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <%--NO VA CURP--%>
                    <asp:Label ID="lblCURPCliT" runat="server" Text="CURP: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCURPCliD" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="lblFecNacRegCliT" runat="server" Text="FECHA DE NACIMIENTO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblFecNacRegCliD" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="35%" Style="font-size: x-small">
                    <asp:Label ID="Label85" runat="server" Text="GIRO DE LA EMPRESA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblGiroEmpCli" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="15%" Style="font-size: x-small">
                    <asp:Label ID="Label87" runat="server" Text="SECTOR: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblSectorCli" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <%--NO VA ESTADO CIVIL NI REGIMEN MATRIMONIAL--%>
                    <asp:Label ID="lblEstCivCliT" runat="server" Text="ESTADO CIVIL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEstCivCliD" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>

                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="lblRegMatCliT" runat="server" Text="REGIMEN MATRIMONIAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblRegMatCliD" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>

                <asp:TableCell Width="35%" Style="font-size: x-small">
                    <asp:Label ID="Label93" runat="server" Text="EMAIL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEmailCli" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="15%" Style="font-size: x-small">
                    <asp:Label ID="Label95" runat="server" Text="TIPO DE IDENTIFICACIÓN: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTipIdenCli" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label97" runat="server" Text="NÚMERO DE IDENTIFICACIÓN: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumIdenCli" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>

                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <%--NO VA NOMBRE CONYUGE--%>
                    <asp:Label ID="lblNomConyCliT" runat="server" Text="NOMBRE CONYUGE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNomConyCliD" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="35%" Style="font-size: x-small">
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <div>
    </div>
    <table style="padding: 0px; margin: 0px; border-width: 0px; background-color: #3399FF">
        <tr>
            <td width="1100"></td>
        </tr>
    </table>
    <div>
        <asp:Table runat="server" ID="InfoDomCli" Visible="true" Width="100%">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label101" runat="server" Text="DATOS DOMICILIOS" Style="font-weight: 700"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label102" runat="server" Text="FISCAL" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label103" runat="server" Text="CÓDIGO POSTAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCPCliF" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label105" runat="server" Text="ESTADO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEstCliF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label107" runat="server" Text="DELEGACIÓN O MUNICIPIO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblDelMunCliF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>

                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label109" runat="server" Text="COLONIA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblColCliF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label111" runat="server" Text="CALLE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCalleCliF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>

                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label128" runat="server" Text="NUMERO EXTERIOR: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumExtCliF" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label130" runat="server" Text="NUMERO INTERIOR: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumIntCliF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label132" runat="server" Text="TIPO DE PROPIEDAD: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTipProCliF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label134" runat="server" Text="TELÉFONO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTelCliF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label136" runat="server" Text="REFERENCIAS DEL DOMICILIO: " Font-Size="X-Small" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblReferDomF" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label115" runat="server" Text="NEGOCIO" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label116" runat="server" Text="CÓDIGO POSTAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCPCliN" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label118" runat="server" Text="ESTADO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEstCliN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label120" runat="server" Text="DELEGACIÓN O MUNICIPIO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblDelMunCliN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>

                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label122" runat="server" Text="COLONIA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblColCliN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label124" runat="server" Text="CALLE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCalleCliN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label138" runat="server" Text="NÚMERO EXTERIOR: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumExtCliN" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label140" runat="server" Text="NÚMERO INTERIOR: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumIntCliN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label142" runat="server" Text="TIPO DE PROPIEDAD: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTipProCliN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="25%" Style="font-size: x-small">
                    <asp:Label ID="Label144" runat="server" Text="TELÉFONO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTelCliN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label146" runat="server" Text="REFERENCIAS DEL DOMICILIO: " Font-Size="X-Small" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblReferDomN" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <table style="padding: 0px; margin: 0px; border-width: 0px; background-color: #3399FF">
        <tr>
            <td width="1100"></td>
        </tr>
    </table>
    <div>
        <asp:Table runat="server" ID="InfoReprLegal" Visible="true">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label148" runat="server" Text="INFORMACIÓN REPRESENTANTE LEGAL" Style="font-weight: 700"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label149" runat="server" Text="NOMBRE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNomRL" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label151" runat="server" Text="EMAIL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEmailRL" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label157" runat="server" Text="TELÉFONO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTelRL" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Label ID="Label160" runat="server" Text="PODER NOTARIAL" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label161" runat="server" Text="NUMERO DE ESCRITURAS: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumEscRL" Font-Size="X-Small" runat="server" Text="000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label163" runat="server" Text="FECHA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblFecEscRL" Font-Size="X-Small" runat="server" Text="0000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label165" runat="server" Text="NOMBRE NOTARIO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNomNotRL" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label167" runat="server" Text="NÚMERO NOTARÍA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumNotRL" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>

                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label169" runat="server" Text="ESTADO: " Font-Bold="true" Font-Size="X-Small"></asp:Label>
                    <asp:Label ID="lblEstRL" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label171" runat="server" Text="DELAGACIÓN O MUNICIPIO: " Font-Bold="true" Font-Size="X-Small"></asp:Label>
                    <asp:Label ID="lblDelMunRL" Font-Size="X-Small" runat="server" Text="000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <table style="padding: 0px; margin: 0px; border-width: 0px; background-color: #3399FF">
        <tr>
            <td width="1100"></td>
        </tr>
    </table>
    <div>
        <asp:Table runat="server" ID="InfoObliSoliFis" Width="100%">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label153" runat="server" Text="INFORMACIÓN OBLIGADO SOLIDARIO" Style="font-weight: 700"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label154" runat="server" Text="TIPO DE PERSONA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTipPerOS" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label156" runat="server" Text="NOMBRE/RAZÓN SOCIAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNomOS" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label175" runat="server" Text="RFC: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblRFCOS" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label177" runat="server" Text="CURP: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCURPOS" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label173" runat="server" Text="FECHA DE NACIMIENTO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblFecNacOS" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label179" runat="server" Text="TELÉFONO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTelOS" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label182" runat="server" Text="CÓDIGO POSTAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCPOS" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label183" runat="server" Text="ESTADO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEstOS" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label185" runat="server" Text="DELAGACIÓN O MUNICIPIO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblDelMunOS" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label187" runat="server" Text="COLONIA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblColOS" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label189" runat="server" Text="CALLE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCalleOS" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label191" runat="server" Text="NÚMERO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumOS" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <%--OBLI SOLI MOR--%>
        <asp:Table runat="server" ID="InfoObliSoliMor" Width="100%">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Label ID="Label245" runat="server" Text="INFORMACIÓN OBLIGADO SOLIDARIO" Font-Size="Smaller" Style="font-weight: 700"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label1" runat="server" Text="TIPO DE PERSONA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTipPerOSM" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="lblRSOS" runat="server" Text="RAZÓN SOCIAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label26" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label34" runat="server" Text="NOMBRE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNomOSM" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label38" runat="server" Text="E-MAIL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEmailOS" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label42" runat="server" Text="TELÉFONO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblTelOSM" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Label ID="Label246" runat="server" Text="PODER NOTARIAL DEL REPRESENTANTE LEGAL DEL OBLIGADO SOLIDARIO" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label247" runat="server" Text="NÚMERO ESCRITURA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumEscOSP" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label249" runat="server" Text="FECHA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblFecOSP" Font-Size="X-Small" runat="server" Text="000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label251" runat="server" Text="NOMBRE DEL NOTARIO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNomNotOSP" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label253" runat="server" Text="ESTADO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEstOSP" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label255" runat="server" Text="DELEGACIÓN O MUNICIPIO: " Font-Size="X-Small" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblDelMunOSP" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label257" runat="server" Text="NÚMERO DE NOTARÍA: " Font-Size="X-Small" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumNotOSP" Font-Size="X-Small" runat="server" Text="000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Label ID="Label259" runat="server" Text="ACTA CONSTITUTIVA DEL OBLIGADO SOLIDARIO" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label260" runat="server" Text="NÚMERO ESCRITURA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumEscOSA" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label262" runat="server" Text="FECHA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblFecOSA" Font-Size="X-Small" runat="server" Text="000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label264" runat="server" Text="NOMBRE DEL NOTARIO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNomNotOSA" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label266" runat="server" Text="ESTADO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEstOSA" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label268" runat="server" Text="DELEGACIÓN O MUNICIPIO: " Font-Size="X-Small" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblDelMunOSA" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label270" runat="server" Text="NÚMERO DE NOTARÍA: " Font-Size="X-Small" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumNotOSA" Font-Size="X-Small" runat="server" Text="000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

    <table style="padding: 0px; margin: 0px; border-width: 0px; background-color: #3399FF">
        <tr>
            <td width="1100"></td>
        </tr>
    </table>

    <div>
        <asp:Table runat="server" ID="InfoTrama">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Label ID="Label193" runat="server" Text="INFORMACIÓN TRAMA" Style="font-weight: 700"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Label ID="Label218" runat="server" Text="ORIGEN SOLICITUD" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>

                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label194" runat="server" Text="USUARIO CFE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblUsuCFEO" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label196" runat="server" Text="RPU: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblRPUO" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label198" runat="server" Text="NÚMERO DE CUENTA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumCuenO" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label200" runat="server" Text="FECHA DE CONSULTA DE LA TRAMA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblFecConsO" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>

                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label206" runat="server" Text="CÓDIGO POSTAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCPO" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label208" runat="server" Text="ESTADO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEstO" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label210" runat="server" Text="DELAGACIÓN O MUNICIPIO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblDelMunO" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>

                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label212" runat="server" Text="COLONIA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblColO" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label214" runat="server" Text="CALLE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCalleO" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <%--<asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label216" runat="server" Text="NUMERO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumO" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>--%>
            </asp:TableRow>
            <asp:TableRow>

                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label202" runat="server" Text="FECHA INICIO PERÍODO: " Font-Bold="true" Font-Size="X-Small"></asp:Label>
                    <asp:Label ID="lblFecInPer" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label204" runat="server" Text="FECHA FIN PERÍODO: " Font-Bold="true" Font-Size="X-Small"></asp:Label>
                    <asp:Label ID="lblFecFinPer" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>

                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Table ID="Table4" runat="server" BorderStyle="Ridge" GridLines="Both" BackColor="#cccccc">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label223" Font-Size="X-Small" Font-Bold="true" runat="server">
                                    Nota: Los datos de esta sección son los obtenidos por la trama al momento de dar de alta el credito,
                                    estos pueden variar o sufrir modificaciones por algún cambio por parte del usuario de CFE.<br />
                                    En caso de no tener información solicitar al área de sistemas.
                                </asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:Label ID="Label224" runat="server" Text="ACTUALIZADA" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label225" runat="server" Text="USUARIO CFE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblUsuCFEA" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label227" runat="server" Text="RPU: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblRPUA" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label229" runat="server" Text="NÚMERO DE CUENTA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumCuenA" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label231" runat="server" Text="FECHA DE CONSULTA DE LA TRAMA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblFecConsA" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>

                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label233" runat="server" Text="CÓDIGO POSTAL: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCPA" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label235" runat="server" Text="ESTADO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEstA" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label237" runat="server" Text="DELAGACIÓN O MUNICIPIO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblDelMunA" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>

                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label239" runat="server" Text="COLONIA: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblColA" Font-Size="X-Small" runat="server" Text="00000000000000000000"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label241" runat="server" Text="CALLE: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCalleA" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>
                <%--<asp:TableCell Width="33%" Style="font-size: x-small">
                    <asp:Label ID="Label243" runat="server" Text="NUMERO: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblNumA" Font-Size="X-Small" runat="server" Text="00000000"></asp:Label>
                </asp:TableCell>--%>
            </asp:TableRow>
        </asp:Table>
    </div>
    <table style="padding: 0px; margin: 0px; border-width: 0px; background-color: #3399FF">
        <tr>
            <td width="1100"></td>
        </tr>
    </table>


    <div id="div_resultadosBusqueda" style="width: 100%" align="center" runat="server" visible="false">

        <asp:UpdatePanel ID="UpdatePanelRB" runat="server" Visible="true">
            <ContentTemplate>
                
                <asp:GridView DataKeyNames="No_Credito" ID="gvTablaAmortizacion" runat="server" aling="center" Width="80%" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="11pt" AllowPaging="True" BorderStyle="Groove"
                    CssClass="GridViewStyle">
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" BorderStyle="Solid" />
                    <HeaderStyle BackColor="Silver" />
                    <PagerSettings Visible="false" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EditRowStyle CssClass="GridViewEditStyle" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="No_Credito" ControlStyle-Font-Size="X-Small" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblRPU" runat="server" Text='<%# Eval("No_Credito")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="No_Pago" ControlStyle-Font-Size="X-Small" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblNOMRS" runat="server" Text='<%# Eval("No_Pago")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="Dt_Fecha" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblRFC" runat="server" Text='<%# Eval("Dt_Fecha")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="No_Días_Periodo" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblEC" runat="server" Text='<%# Eval("No_Dias_Periodo")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="Mt_Capital" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblRS" runat="server" Text='<%# Eval("Mt_Capital")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%" HeaderText="Mt_Interes" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblNC" runat="server" Text='<%# Eval("Mt_Interes")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="11%" HeaderText="Mt_IVA" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblR" runat="server" Text='<%# Eval("Mt_IVA")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="11%" HeaderText="Mt_Pago" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblZ" runat="server" Text='<%# Eval("Mt_Pago")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="11%" HeaderText="Mt_Amortizacion" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblZa" runat="server" Text='<%# Eval("Mt_Amortizacion")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="11%" HeaderText="Mt_Saldo" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblZs" runat="server" Text='<%# Eval("Mt_Saldo")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="11%" HeaderText="Dt_Fecha_Credito_Amortización" Visible="true" ControlStyle-Font-Size="X-Small">
                            <ItemTemplate>
                                <asp:Label ID="lblZ3" runat="server" Text='<%# Eval("Dt_Fecha_Credito_Amortización")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>

            <Triggers>
                <asp:PostBackTrigger ControlID="imgExportaPDF"></asp:PostBackTrigger>
            </Triggers>

        </asp:UpdatePanel>
</div>


    <%--<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">--%>
    <%--    <asp:Button ID="boton" Text=" boton" runat="server" OnClick="boton_Click" />
    <asp:Button ID="boton2" Text="Boton2" runat="server" OnClick="boton2_Click" />
    <asp:HyperLink  ID="HyperLink1" runat="server" Text="Consultar Historico Consultas" Font-Underline="true"></asp:HyperLink>&nbsp;&nbsp;--%>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" Width="500px" Height="300px"></telerik:RadWindowManager>
    <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" LoadingPanelID="ralpConfiguration">
        <telerik:RadWindow ID="RadWindow1" runat="server" Width="500px" Height="300px" Modal="true">
            <ContentTemplate>
                <div style="padding: 10px; text-align: center; align-content: center; width: inherit; height: inherit;">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="DGV_HistoricoConsultas" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                                    AllowPaging="True" PageSize="20" DataKeyNames="NO_RPU" OnSelectedIndexChanged="DGV_HistoricoConsultas_SelectedIndexChanged">
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <EditRowStyle CssClass="GridViewEditStyle" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Número de Solicitud" DataField="No_Solicitud" />
                                        <asp:BoundField HeaderText="Folio" DataField="Folio" />
                                        <asp:BoundField HeaderText="MOP" DataField="MOP" />
                                        <asp:BoundField HeaderText="Fecha de Consulta" DataField="Fecha_Consulta" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                <asp:Button ID="btnFinalizar" runat="server" Text="Cerrar" OnClick="btnFinalizar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </telerik:RadAjaxPanel>
</asp:Content>
