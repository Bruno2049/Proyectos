using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Validacion_RFC_E;
using System.Linq.Expressions;

namespace PAEEEM.AccesoDatos.Validacion_RFC_A
{
    public class Validacion_RFC_A
    {
        private static readonly Validacion_RFC_A _classInstance = new Validacion_RFC_A();

        public static Validacion_RFC_A ClassInstance
        {
            get { return _classInstance; }
        }

        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public Validacion_RFC_A()
        {
        }

        public US_USUARIO Obten_Proveedor(int ID_Dist)
        {
            US_USUARIO Detalle;

            using (var r = new Repositorio<US_USUARIO>())
            {
                Detalle = r.Extraer(me => me.Id_Usuario == ID_Dist);
            }

            return Detalle;
        }

        public bool Inserta_Solicitud(CRE_ValidacionRFC Solicitud)
        {

            using (var r = new Repositorio<CRE_ValidacionRFC>())
            {
                var Nueva = r.Agregar(Solicitud);

                if (Nueva != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }


        }

        public List<CorreosValidacionRFC> Obten_JefeZona(int ID_Dist, string Tipo_usuario)
        {
            if (Tipo_usuario == "S")
            {
                var CorreoDistS = (from US in _contexto.US_USUARIO
                                   join Pro in _contexto.CAT_PROVEEDOR on US.Id_Departamento equals Pro.Id_Proveedor
                                   join US2 in _contexto.US_USUARIO on Pro.Cve_Zona equals US2.Id_Departamento
                                   where
                                   US.Id_Usuario == ID_Dist && US2.Tipo_Usuario == "Z_O" && US2.Id_Rol == 2 && US2.Estatus == "A"

                                   select new CorreosValidacionRFC
                                   {
                                       NombreJefeZona = US2.Nombre_Completo_Usuario,
                                       NombreDistribuidor = US.Nombre_Completo_Usuario,
                                       Correo = US2.CorreoElectronico
                                   }
                                      ).ToList();
                return CorreoDistS;
            }

            else if (Tipo_usuario == "S_B")
            {
                var CorreoDistS_B = (from US in _contexto.US_USUARIO
                                     join Bra in _contexto.CAT_PROVEEDORBRANCH on US.Id_Departamento equals Bra.Id_Branch
                                     join US2 in _contexto.US_USUARIO on Bra.Cve_Zona equals US2.Id_Departamento
                                     where
                                     US.Id_Usuario == ID_Dist && US2.Id_Rol == 2 && US2.Tipo_Usuario == "Z_O" && US2.Estatus == "A"

                                     select new CorreosValidacionRFC
                                     {
                                         NombreJefeZona = US2.Nombre_Completo_Usuario,
                                         NombreDistribuidor = US.Nombre_Completo_Usuario,
                                         Correo = US2.CorreoElectronico
                                     }
                                      ).ToList();
                return CorreoDistS_B;
            }
            return null;
        }

        public CRE_ValidacionRFC ObtenerValidacion(string RFC, string Nombre_Completo, DateTime Fecha_Nac_Reg, byte Tipo_Persona)
        {
            CRE_ValidacionRFC Solicitud = null;

            using (var r = new Repositorio<CRE_ValidacionRFC>())
            {
                Solicitud = r.Extraer(me => me.RFC == RFC && me.Nombre_RZ == Nombre_Completo && me.Fecha_Nac_Reg == Fecha_Nac_Reg && me.Cve_Tipo_Sociedad == Tipo_Persona);
            }

            return Solicitud;
        }

        public CRE_ValidacionRFC ObtenerValidacionRFC(string RFC)
        {
            CRE_ValidacionRFC Solicitud = null;

            using (var r = new Repositorio<CRE_ValidacionRFC>())
            {
                Solicitud = r.Extraer(me => me.RFC == RFC);
            }

            return Solicitud;
        }

        public ProcesoValidacionRFC EsRechazado(string RFC)
        {
            var Rechazado = (from CV in _contexto.CRE_ValidacionRFC
                             where CV.RFC == RFC && CV.Estatus_Validacion == 3
                             orderby CV.Fecha_Validacion descending
                             select new ProcesoValidacionRFC
                             {
                                 RFC = CV.RFC,
                                 Estatus_Validacion = CV.Estatus_Validacion,
                                 Comentarios = CV.Comentarios_Validacion,
                                 Fecha_Solicitud = CV.Fecha_Solicitud,
                                 Fecha_Validacion = CV.Fecha_Validacion
                             }).ToList().First();
            return Rechazado;

        }

        public List<CRE_ValidacionRFC> ObtieneTodos(int ID_Departamento)
        {
            List<CRE_ValidacionRFC> listParametros = null;
            List<US_USUARIO> Distribuidores = null;
            //var  a = (from US in _contexto.US_USUARIO
            //              where US.Id_Departamento == ID_Departamento && US.Id_Rol==3 && US.Estatus=="A" )

            using (var e = new Repositorio<US_USUARIO>())
            {
                Distribuidores = e.Filtro(r => r.Id_Departamento == ID_Departamento && r.Id_Rol == 3 && r.Estatus == "A");
            }

            using (var r = new Repositorio<CRE_ValidacionRFC>())
            {
                listParametros = r.Filtro(e => e.Estatus_Validacion == 1);
            }

            return listParametros;
        }
        //
        public List<GridZona> ObtenGridRFCVali(int ID_JefeZona)
        {
            var solicDistribuidores = (from dis in _contexto.US_USUARIO
                                       join mpro in _contexto.CAT_PROVEEDOR on new { dis.Id_Departamento, dis.Tipo_Usuario, Cve_Estatus_Proveedor = 2 } equals new { Id_Departamento = (int?)mpro.Id_Proveedor, Tipo_Usuario = "S", mpro.Cve_Estatus_Proveedor } into m
                                       from mpro in m.DefaultIfEmpty()
                                       join spro in _contexto.CAT_PROVEEDORBRANCH on new { dis.Id_Departamento, dis.Tipo_Usuario, Cve_Estatus_Proveedor = 2 } equals new { Id_Departamento = (int?)spro.Id_Branch, Tipo_Usuario = "S_B", spro.Cve_Estatus_Proveedor } into s
                                       from spro in s.DefaultIfEmpty()
                                       join jzo in _contexto.US_USUARIO on new { Cve_Zona = dis.Tipo_Usuario == "S" ? mpro.Cve_Zona : spro.Cve_Zona, Tipo_Usuario = "Z_O" } equals new { Cve_Zona = jzo.Id_Departamento, jzo.Tipo_Usuario }
                                       where
                                           dis.Estatus == "A" &&
                                           jzo.Id_Usuario == ID_JefeZona &&
                                           dis.CRE_ValidacionRFC.Any(d => d.Estatus_Validacion == 1)
                                       select new
                                       {
                                           distribuidor = dis,
                                           solicitudes = dis.CRE_ValidacionRFC.Where(r => r.Estatus_Validacion == 1).Select(rfc => new GridZona
                                           {
                                               ID_Validacion = rfc.Id_Validacion,
                                               Distribuidor_NC = rfc.US_USUARIO.Tipo_Usuario == "S" ? rfc.CAT_PROVEEDOR.Dx_Nombre_Comercial : rfc.CAT_PROVEEDORBRANCH.Dx_Razon_Social,
                                               Distribuidos_RS = rfc.US_USUARIO.Tipo_Usuario == "S" ? rfc.CAT_PROVEEDOR.Dx_Razon_Social : rfc.CAT_PROVEEDORBRANCH.Dx_Razon_Social,
                                               Tipo_Persona = rfc.Cve_Tipo_Sociedad == 1 ? "Fisica" : "MORAL",
                                               Nombre_RazonSocial = rfc.Nombre_RZ,
                                               Fecha_NacRegistro = rfc.Fecha_Nac_Reg,
                                               RFC = rfc.RFC,
                                               Comprobante = rfc.Comprobante
                                           })
                                       }).SelectMany(rfc => rfc.solicitudes).ToList();

            return solicDistribuidores;
        }

        public CRE_ValidacionRFC BuscaRegistro(Expression<Func<CRE_ValidacionRFC, bool>> Id_Validacion)
        {
            CRE_ValidacionRFC Verifica = null;

            using (var r = new Repositorio<CRE_ValidacionRFC>())
            {
                Verifica = r.Extraer(Id_Validacion);
            }

            return Verifica;
        }

        public CRE_ValidacionRFC TraeRegistro(int Id_Validacion)
        {
            return BuscaRegistro(a => a.Id_Validacion == Id_Validacion);
        }


        public bool ActualizaSolicitud(int ID_Validacion, int id_Jefezona, string Motivos, int Estatus)
        {
            var Registro = BuscaRegistro(a => a.Id_Validacion == ID_Validacion);

            if (Estatus == 2)
            {
                bool ActualizadoValido;
                Registro.Id_JefeZona = id_Jefezona;
                Registro.Estatus_Validacion = 2;
                Registro.Fecha_Validacion = DateTime.Now;

                CRE_RFCValido Valido = new CRE_RFCValido();
                Valido.RFC = Registro.RFC;
                Valido.Id_Validacion = Registro.Id_Validacion;

                using (var r = new Repositorio<CRE_RFCValido>())
                {
                    var Nueva = r.Agregar(Valido);
                }

                using (var r = new Repositorio<CRE_ValidacionRFC>())
                {
                    ActualizadoValido = r.Actualizar(Registro);
                }

                return ActualizadoValido;
            }

            else if (Estatus == 3)
            {
                bool ActualizadoRechazado;

                Registro.Estatus_Validacion = 3;
                Registro.Fecha_Validacion = DateTime.Now;
                Registro.Comentarios_Validacion = Motivos;
                Registro.Id_JefeZona = id_Jefezona;
                using (var r = new Repositorio<CRE_ValidacionRFC>())
                {
                    ActualizadoRechazado = r.Actualizar(Registro);
                }

                return ActualizadoRechazado;
            }
            return false;
        }

        public CorreosValidacionRFC ObtenCorreoDist(int Id_Distribuidor)
        {
            var Correo = (from U in _contexto.US_USUARIO
                          where U.Id_Usuario == Id_Distribuidor
                          select new CorreosValidacionRFC
                          {
                              Correo = U.CorreoElectronico,
                              NombreDistribuidor = U.Nombre_Completo_Usuario,
                          }).FirstOrDefault();
            return Correo;
        }
    }
}

