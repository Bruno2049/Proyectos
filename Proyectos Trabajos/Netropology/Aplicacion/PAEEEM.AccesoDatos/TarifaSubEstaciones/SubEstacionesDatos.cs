using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;
using PAEEEM.Helpers;
using PAEEEM.Entidades.SubEstaciones;

namespace PAEEEM.AccesoDatos.TarifaSubEstaciones
{
    public class SubEstacionesDatos
    {
        private static readonly SubEstacionesDatos _classInstance = new SubEstacionesDatos();

        public static SubEstacionesDatos ClassInstance
        {
            get { return _classInstance; }
        }

        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public SubEstacionesDatos()
        {
        }

        //public CAT_ACCIONES BuscaAcciones(int Permitidas)
        //{
        //    CAT_ACCIONES Acciones;

        //    using (var r = new Repositorio<CAT_ACCIONES>())
        //    {
        //        Acciones = r.Extraer(me => me.Rol_Permitido == Permitidas);
        //    }

        //    return Acciones;
        //}

        public CambioRPU IrPorDatos(string No_Credito)
        {
            var Datos = (from credito in _contexto.CRE_Credito
                         join Cliente in _contexto.CLI_Cliente on credito.IdCliente equals
                             Cliente.IdCliente
                         where Cliente.Id_Branch == credito.Id_Branch && Cliente.IdCliente == credito.IdCliente && credito.No_Credito == No_Credito
                         select new CambioRPU
                         {
                             No_Credito = credito.No_Credito,
                             Razon_Social = Cliente.Razon_Social,
                             Nombre = Cliente.Nombre + " " + Cliente.Ap_Paterno + " " + Cliente.Ap_Materno,
                             Antiguo_RPU = credito.RPU,
                             Nuevo_RPU = ""
                         }
                         ).ToList().FirstOrDefault();

            return Datos;
        }

        public Cambio_RPU_Entidad ObtenDatosRpuDist(string noCredito)
        {
            var datos = (from us in _contexto.Cambio_RPU
                         where us.No_Credito == noCredito
                         select new Cambio_RPU_Entidad
                         {
                             No_Credito = us.No_Credito,
                             Usuario_Distribuidor = us.Usuario_Distribuidor,
                             RPU_Distribuidor = us.RPU_Distribuidor,
                             Fecha_Captura_Dist = us.Fecha_Captura_Dist,
                             Usuario_Jefe_Zona = us.Usuario_Jefe_Zona,
                             RPU_Jefe_Zona = us.RPU_Jefe_Zona,
                             Fecha_Captura_Zona = us.Fecha_Captura_Zona,
                             Proceso_Iniciado = us.Proceso_Iniciado
                         }).ToList().FirstOrDefault();
            return datos;
        }

        public Correos ObtenCorreoJefeZona(string usuarioJefeZona, string usuarioDist)
        {
            var correos  = (from us in _contexto.US_USUARIO
                                   join pro in _contexto.CAT_PROVEEDOR on us.Id_Departamento equals pro.Id_Proveedor
                                   join bra in _contexto.CAT_PROVEEDORBRANCH on us.Id_Departamento equals bra.Id_Branch
                                   join zn in _contexto.CAT_ZONA on pro.Cve_Zona equals zn.Cve_Zona
                                   join us2 in _contexto.US_USUARIO on pro.Cve_Zona equals us2.Id_Departamento
                            where us.Nombre_Usuario == usuarioDist
                            && us2.Nombre_Usuario == usuarioJefeZona
                                 && us2.Tipo_Usuario == "Z_O"
                                 && us2.Estatus == "A"
                                   select new Correos
                                   {
                                       Usuario = us.Nombre_Usuario,
                                       Correo = us.CorreoElectronico,
                                       NombreZona = zn.Dx_Nombre_Zona,
                                       NombreJefeZona = zn.Dx_Nombre_Responsable,
                                       RazonSocialDistribuidor = us.Tipo_Usuario == "S" ? pro.Dx_Nombre_Comercial : bra.Dx_Nombre_Comercial
                                   }
                                      ).ToList().FirstOrDefault();
            return correos;
        }

        public bool EsSubestacion(string No_credito)
        {
            var EsSubEstacion = (from CProducto in _contexto.K_CREDITO_PRODUCTO
                                 join Credito in _contexto.CRE_Credito on CProducto.No_Credito equals Credito.No_Credito
                                 join Prod in _contexto.CAT_PRODUCTO on CProducto.Cve_Producto equals Prod.Cve_Producto
                                 join Tec in _contexto.CAT_TECNOLOGIA on Prod.Cve_Tecnologia equals Tec.Cve_Tecnologia
                                 where Tec.Cve_Tecnologia == 5 && Credito.No_Credito == No_credito

                                 select new EsSubEstacion
                                 {
                                     No_Credit = Credito.No_Credito,
                                     Nombre_producto = Prod.Dx_Nombre_Producto,
                                     Nombre_General = Tec.Dx_Nombre_General
                                 }
                ).ToList().FirstOrDefault();

            if (EsSubEstacion == null)
            {
                return false;
            }

            else
            {
                return true;
            }

        }

        public Correos ObtenCorreoZona(string usuario, string tipoUsuario)
        {
            if (tipoUsuario == "S")
            {
                var correoDistS = (from us in _contexto.US_USUARIO
                                   join pro in _contexto.CAT_PROVEEDOR on us.Id_Departamento equals pro.Id_Proveedor
                                   join zn in _contexto.CAT_ZONA on pro.Cve_Zona equals zn.Cve_Zona
                                   join us2 in _contexto.US_USUARIO on pro.Cve_Zona equals us2.Id_Departamento
                                where us.Nombre_Usuario == usuario
                                 && us2.Tipo_Usuario == "Z_O"
                                 && us2.Estatus == "A"

                                   select new Correos
                                   {
                                       Usuario = us2.Nombre_Usuario,
                                       Correo = us2.CorreoElectronico,
                                       NombreZona = zn.Dx_Nombre_Zona,
                                       NombreJefeZona = zn.Dx_Nombre_Responsable,
                                       RazonSocialDistribuidor = pro.Dx_Nombre_Comercial
                                   }
                                      ).ToList().FirstOrDefault();
                return correoDistS;
            }

            if (tipoUsuario != "S_B") return null;

            var correoDistSb = (from us in _contexto.US_USUARIO
                join bra in _contexto.CAT_PROVEEDORBRANCH on us.Id_Departamento equals bra.Id_Branch
                join zn  in _contexto.CAT_ZONA on bra.Cve_Zona equals zn.Cve_Zona
                join us2 in _contexto.US_USUARIO on bra.Cve_Zona equals us2.Id_Departamento
                where
                    us.Nombre_Usuario == usuario 
                    && us2.Tipo_Usuario == "Z_O" 
                    && us2.Estatus == "A"

                select new Correos
                {
                    Usuario = us2.Nombre_Usuario,
                    Correo = us2.CorreoElectronico,
                    NombreZona = zn.Dx_Nombre_Zona,
                    NombreJefeZona = zn.Dx_Nombre_Responsable,
                    RazonSocialDistribuidor = bra.Dx_Nombre_Comercial
                }
                ).ToList().FirstOrDefault();
            return correoDistSb;
        }

        public Cambio_RPU InsertaRPUDistr(Cambio_RPU DistribuidorViejo)
        {
            Cambio_RPU Distribuidor = null;

            using (var r = new Repositorio<Cambio_RPU>())
            {
                Distribuidor = r.Agregar(DistribuidorViejo);
            }

            return Distribuidor;
        }

        public Cambio_RPU HayRegistroDeCambioRPU(Expression<Func<Cambio_RPU, bool>> No_Credito)
        {
            Cambio_RPU Verifica = null;

            using (var r = new Repositorio<Cambio_RPU>())
            {
                Verifica = r.Extraer(No_Credito);
            }

            return Verifica;
        }

        public bool HayRegistroDeNuevoRPUDist(string NO_Credito)
        {
            var Existe = HayRegistroDeCambioRPU(c => c.No_Credito == NO_Credito);

            if (Existe == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool HayRegistroDeNuevoRPUJefeZona(string NO_Credito)
        {
            var Existe = HayRegistroDeCambioRPU(c => c.No_Credito == NO_Credito);

            if (Existe != null && (Existe.RPU_Jefe_Zona != null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Cambio_RPU ActualizaRPUJefeZona(string No_credito, string RPU_Jefe_Zona, out bool actualizo, out bool elimino)
        {
            actualizo = false;
            elimino = false;
            Cambio_RPU datos;
            var existe = HayRegistroDeCambioRPU(c => c.No_Credito == No_credito);

            if (existe == null) return null;

            if (existe.RPU_Distribuidor == RPU_Jefe_Zona)
            {
                existe.RPU_Jefe_Zona = RPU_Jefe_Zona;
                existe.Fecha_Captura_Zona = DateTime.Now;
                existe.Proceso_Iniciado = 1;

                using (var r = new Repositorio<Cambio_RPU>())
                {
                    actualizo = r.Actualizar(existe);
                    datos = existe;
                }
            }

            else
            {
                using (var r = new Repositorio<Cambio_RPU>())
                {
                    existe.RPU_Jefe_Zona = RPU_Jefe_Zona;
                    datos = existe;
                    elimino = r.Eliminar(existe);
                }
            }

            return datos;
        }

        public bool RPUActivo(string No_Credito)
        {
            var Existe = HayRegistroDeCambioRPU(c => c.No_Credito == No_Credito);

            if (Existe.Proceso_Iniciado != 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
