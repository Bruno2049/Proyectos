using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PAEEEM.AccesoDatos.Validacion_RFC_A;
using PAEEEM.Entidades.Validacion_RFC_E;
using PAEEEM.Entidades;
using PAEEEM.Helpers;

namespace PAEEEM.LogicaNegocios.Validacion_RFC_L
{
    public class Validacion_RFC_L
    {
        private static readonly Validacion_RFC_L _classInstance = new Validacion_RFC_L();

        public static Validacion_RFC_L ClassInstance
        {
            get { return _classInstance; }
        }

        public Validacion_RFC_L()
        {
        }

        public bool Almacena_Solicitud(string RFC, int ID_Dist, int ID_Dep_Us_Dist, string US_Tipo_US, string Nombre_Razon, string Fecha_Nac_Reg, byte Tipo_Persona, byte[] Comprobante, DateTime Fecha_Solicitud)
        {
            CRE_ValidacionRFC Solicitud = new CRE_ValidacionRFC();

            Solicitud.RFC = RFC;
            Solicitud.Id_Distribuidor = ID_Dist;
            if (US_Tipo_US == "S")
            {
                Solicitud.Id_Proveedor = ID_Dep_Us_Dist;
            }
            else if (US_Tipo_US == "S_B")
            {
                Solicitud.Id_Branch = ID_Dep_Us_Dist;
            }
            DateTime FechaRegistro = Convert.ToDateTime(Fecha_Nac_Reg);
            Solicitud.Fecha_Nac_Reg = FechaRegistro;
            Solicitud.Nombre_RZ = Nombre_Razon;
            Solicitud.Cve_Tipo_Sociedad = Tipo_Persona;
            Solicitud.Comprobante = Comprobante;
            Solicitud.Fecha_Solicitud = Fecha_Solicitud;
            Solicitud.Estatus_Validacion = 1;


            return Validacion_RFC_A.ClassInstance.Inserta_Solicitud(Solicitud);

        }

        public List<CorreosValidacionRFC> Obten_JefeZona(int ID_Dist, string Tipo_Usuario)
        {
            return Validacion_RFC_A.ClassInstance.Obten_JefeZona(ID_Dist, Tipo_Usuario);
        }

        public CRE_ValidacionRFC Obten_EstatusValidacion(string RFC, string Nombre_Completo, DateTime Fecha_Nac_Reg, byte Cve_Tipo_Sociedad)
        {
            return Validacion_RFC_A.ClassInstance.ObtenerValidacion(RFC, Nombre_Completo, Fecha_Nac_Reg, Cve_Tipo_Sociedad);
        }

        public CRE_ValidacionRFC Obten_EstatusValidacionRFC(string RFC)
        {
            return Validacion_RFC_A.ClassInstance.ObtenerValidacionRFC(RFC);
        }

        public ProcesoValidacionRFC ObtenRechazado(string RFC)
        {
            return Validacion_RFC_A.ClassInstance.EsRechazado(RFC);
        }
        //

        public List<GridZona> ObtenGridRFCVali(int ID_JefeZona)
        {
            return Validacion_RFC_A.ClassInstance.ObtenGridRFCVali(ID_JefeZona);
        }

        public bool ActualizaRegistro(int ID_Validacion, int id_Jefezona, string Motivos, int Estatus)
        {
            return Validacion_RFC_A.ClassInstance.ActualizaSolicitud(ID_Validacion, id_Jefezona, Motivos, Estatus);
        }

        public CorreosValidacionRFC ObtenCorreoDist(int ID_JefeZona)
        {
            return Validacion_RFC_A.ClassInstance.ObtenCorreoDist(ID_JefeZona);
        }

        public CRE_ValidacionRFC TraeRegistro(int ID_Validacion)
        {
            return Validacion_RFC_A.ClassInstance.TraeRegistro(ID_Validacion);
        }
    }
}

