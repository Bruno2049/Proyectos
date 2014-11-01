using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.AccesoDatos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.LogicaNegocios.SolicitudCredito
{
    public class CatalogosSolicitud
    {
        private static readonly CatalogosSolicitud _classInstance = new CatalogosSolicitud();

        public static CatalogosSolicitud ClassInstance { get { return _classInstance; } }

        public CatalogosSolicitud()
        {
        }

        public static List<CAT_SECTOR_ECONOMICO> ObtenCatSectorEconomico()
        {
            List<CAT_SECTOR_ECONOMICO> catSectorEconomico = null;
            var sectorEconomico = new AccesoDatos.SolicitudCredito.SECTOR_ECONOMICO();

            catSectorEconomico = sectorEconomico.ObtenerSectorEconomico();

            return catSectorEconomico;
        }

        public bool ValidaEstratificacionPYME(string noCredito)
        {
            var credito = AccesoDatos.SolicitudCredito.CREDITO_DAL.ClassInstance.BuscaCredito(noCredito);
            var reglasEstratificacion = AccesoDatos.SolicitudCredito.ESTRATIFICACION_MIPYMES.ClassInstance.RegresaListaEstratificacion();

            var empleadosCredito = credito.No_Empleados;
            var ventasCredito = credito.Mt_Ventas_Mes_Empresa * 12;
            var estratificacion = (empleadosCredito * 0.1m) + ((ventasCredito / 1000000) * 0.9m);

            var topeMaximoCombinado = reglasEstratificacion.Where(
                                e =>
                                (e.Cve_Sector != null || e.Cve_Sector == credito.Cve_Sector) &&
                                empleadosCredito >= e.Minimo_Trabajadores &&
                                empleadosCredito <= e.Maximo_Trabajadores &&
                                ventasCredito >= e.Minimo_Monto_Ventas_Anuales &&
                                ventasCredito <= e.Maximo_Monto_Ventas_Anuales)
                            .Max(e => (decimal?)e.Tope_Maximo_Combinado) ?? 0;

            return estratificacion <= topeMaximoCombinado;
        }

        public bool ValidaEstratificacionPYME(int noEmpleados, decimal ventas, int cveSector)
        {
            var reglasEstratificacion = AccesoDatos.SolicitudCredito.ESTRATIFICACION_MIPYMES.ClassInstance.RegresaListaEstratificacion();

            decimal ventasAnuales = ventas * 12;
            var estratificacion = (noEmpleados * 0.1m) + ((ventasAnuales / 1000000) * 0.9m);

            var topeMaximoCombinado = reglasEstratificacion.Where(
                                e => e.Cve_Sector == cveSector &&
                                noEmpleados >= e.Minimo_Trabajadores &&
                                noEmpleados <= e.Maximo_Trabajadores &&
                                ventasAnuales >= e.Minimo_Monto_Ventas_Anuales &&
                                ventasAnuales <= e.Maximo_Monto_Ventas_Anuales)
                            .Max(e => (decimal?)e.Tope_Maximo_Combinado) ?? 0;

            return estratificacion <= topeMaximoCombinado;
        }

        public static bool ValidaClasificacionPyme(K_DATOS_PYME datosPyme)
        {
            var reglasEstratificacion = AccesoDatos.SolicitudCredito.ESTRATIFICACION_MIPYMES.ClassInstance.RegresaListaEstratificacion();

            var ventasAnuales = (decimal) (datosPyme.Prom_Vtas_Mensuales*12);
            var clasificacion = (datosPyme.No_Empleados * 0.1m) + ((ventasAnuales / 1000000) * 0.9m);

            var topeMaximoCombinado = reglasEstratificacion.Where(
                                e => e.Cve_Sector == datosPyme.Cve_Sector_Economico &&
                                datosPyme.No_Empleados >= e.Minimo_Trabajadores &&
                                datosPyme.No_Empleados <= e.Maximo_Trabajadores &&
                                ventasAnuales >= e.Minimo_Monto_Ventas_Anuales &&
                                ventasAnuales <= e.Maximo_Monto_Ventas_Anuales)
                            .Max(e => (decimal?)e.Tope_Maximo_Combinado) ?? 0;

            return clasificacion <= topeMaximoCombinado;
        }

        public static CAT_TIPO_INDUSTRIA ObtenTipoIndustria(int idTipoIndustria)
        {
            var resultado = CapturaSolicitud.ObtenTipoIndustria(idTipoIndustria);

            return resultado;
        }

        public static List<CAT_ESTADO> ObtenCatEstadosRepublica()
        {
            var resultado = CapturaSolicitud.ObtenCatEstadosRepublica();

            return resultado;
        }

        public static CAT_ESTADO ObtenEstadoXId(int idEstado)
        {
            var resultado = CapturaSolicitud.ObtenEstadoXId(idEstado);

            return resultado;
        }

        public static List<CAT_DELEG_MUNICIPIO> ObtenDelegMunicipios(int idEstado)
        {
            var resultado = CapturaSolicitud.ObtenDelegMunicipios(idEstado);

            return resultado;
        }

        public static CAT_DELEG_MUNICIPIO ObtenMunicipioXId(int idMunicipio)
        {
            var resultado = CapturaSolicitud.ObtenMunicipioXId(idMunicipio);

            return resultado;
        }

        public static List<CAT_CODIGO_POSTAL_SEPOMEX> ObtenCatCodigoPostals(int idEstado, int idDelMunicipio)
        {
            var resultado = CapturaSolicitud.ObtenCatCodigoPostals(idEstado, idDelMunicipio);

            return resultado;
        }

        public static CAT_CODIGO_POSTAL_SEPOMEX ObtenColoniaSepomex(int cveCp)
        {
            var resultado = CapturaSolicitud.ObtenColoniaSepomex(cveCp);

            return resultado;
        }

        public static List<Colonia> ObtenColoniasXCp(string codigoPostal)
        {
            var resultado = CapturaSolicitud.ObtenColoniasXCp(codigoPostal);

            return resultado;
        }

        public static List<CAT_TIPO_INDUSTRIA> ObtenCatTipoIndustrias()
        {
            var resultado = CapturaSolicitud.ObtenCatTipoIndustrias();

            return resultado;
        }

        public static List<CAT_SEXO> ObtenCatSexos()
        {
            var resultado = CapturaSolicitud.ObtenCatSexos();

            return resultado;
        }

        public static List<CAT_CLI_REGIMEN_CONYUGAL> ObtenCatRegimenConyugal()
        {
            var resultado = CapturaSolicitud.ObtenCatRegimenConyugal();

            return resultado;
        }

        public static List<CAT_CLI_TIPO_ACREDITACION> ObtenCatTipoAcreditacion()
        {
            var resultado = CapturaSolicitud.ObtenCatTipoAcreditacion();

            return resultado;
        }

        public static List<CAT_CLI_TIPO_IDENTIFICACION> ObtenCatIdentificacion()
        {
            var resultado = CapturaSolicitud.ObtenCatIdentificacion();

            return resultado;
        }

        public static List<CAT_TIPO_PROPIEDAD> ObtenCatTipoPropiedad()
        {
            var resultado = CapturaSolicitud.ObtenCatTipoPropiedad();

            return resultado;
        }

        public static List<CAT_ESTADO_CIVIL> ObtenCatEstadoCivil()
        {
            var resultado = CapturaSolicitud.ObtenCatEstadoCivil();

            return resultado;
        }

        public static List<CAT_TIPO_SOCIEDAD> ObtenCatTipoSociedad()
        {
            var resultado = CapturaSolicitud.ObtenCatTipoSociedad();

            return resultado;
        }

        public static List<CAT_TIPO_PRODUCTO> ObtenTipoProducto(int idTecnologia)
        {
            var resultado = CapturaSolicitud.ObtenTipoProducto(idTecnologia);

            return resultado;
        }

        public static List<Valor_Catalogo> ObtenHorariosTrabajo()
        {
            var lstHorarios = new List<Valor_Catalogo>();

            var horario = new Valor_Catalogo { CveValorCatalogo = "24", DescripcionCatalogo = "00:00 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "0.5", DescripcionCatalogo = "00:30 a.m" };
            lstHorarios.Add(horario);

             horario = new Valor_Catalogo { CveValorCatalogo = "1", DescripcionCatalogo = "01:00 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo {CveValorCatalogo = "1.5", DescripcionCatalogo = "01:30 a.m"};
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo {CveValorCatalogo = "2", DescripcionCatalogo = "02:00 a.m"};
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo {CveValorCatalogo = "2.5", DescripcionCatalogo = "02:30 a.m"};
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo {CveValorCatalogo = "3", DescripcionCatalogo = "03:00 a.m"};
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo {CveValorCatalogo = "3.5", DescripcionCatalogo = "03:30 a.m"};
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo {CveValorCatalogo = "4", DescripcionCatalogo = "04:00 a.m"};
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo {CveValorCatalogo = "4.5", DescripcionCatalogo = "04:30 a.m"};
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo {CveValorCatalogo = "5", DescripcionCatalogo = "05:00 a.m"};
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo {CveValorCatalogo = "5.5", DescripcionCatalogo = "05:30 a.m"};
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo {CveValorCatalogo = "6", DescripcionCatalogo = "06:00 a.m"};
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo {CveValorCatalogo = "6.5", DescripcionCatalogo = "06:30 a.m"};
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "7", DescripcionCatalogo = "07:00 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "7.5", DescripcionCatalogo = "07:30 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "8", DescripcionCatalogo = "08:00 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "8.5", DescripcionCatalogo = "08:30 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "9", DescripcionCatalogo = "09:00 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "9.5", DescripcionCatalogo = "09:30 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "10", DescripcionCatalogo = "10:00 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "10.5", DescripcionCatalogo = "10:30 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "11", DescripcionCatalogo = "11:00 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "11.5", DescripcionCatalogo = "11:30 a.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "12", DescripcionCatalogo = "12:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "12.5", DescripcionCatalogo = "12:30 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "13", DescripcionCatalogo = "13:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "13.5", DescripcionCatalogo = "13:30 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "14", DescripcionCatalogo = "14:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "14.5", DescripcionCatalogo = "14:30 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "15", DescripcionCatalogo = "15:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "15.5", DescripcionCatalogo = "15:30 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "16", DescripcionCatalogo = "16:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "16.5", DescripcionCatalogo = "16:30 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "17", DescripcionCatalogo = "17:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "17.5", DescripcionCatalogo = "17:30 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "18", DescripcionCatalogo = "18:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "18.5", DescripcionCatalogo = "18:30 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "19", DescripcionCatalogo = "19:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "19.5", DescripcionCatalogo = "19:30 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "20", DescripcionCatalogo = "20:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "20.5", DescripcionCatalogo = "20:30 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "21", DescripcionCatalogo = "21:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "21.5", DescripcionCatalogo = "21:30 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "22", DescripcionCatalogo = "22:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "22.5", DescripcionCatalogo = "22:30 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "23", DescripcionCatalogo = "23:00 p.m" };
            lstHorarios.Add(horario);

            horario = new Valor_Catalogo { CveValorCatalogo = "23.5", DescripcionCatalogo = "23:30 p.m" };
            lstHorarios.Add(horario);
            
            return lstHorarios;
        }
    }
}
