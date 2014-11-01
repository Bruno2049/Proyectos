using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PAEEEM.AccesoDatos.Log;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.LogicaNegocios.LOG
{
    public class Insertlog
    {
        public static void InsertarLog(int idUsuario, int idRol, int empresaRegionZona, string tipoProceso,
            string tarea, string tareaLoteCredito, string motivo, string nota, string datosAnteriores,
            string datosActuales)
        {
            try
            {
                var infoProcesos = GetIdTareaProceso(tipoProceso, tarea);

                var informacion = new K_LOG
                {
                    FECHA_ADICION = DateTime.Now,
                    IDUSUARIO = idUsuario,
                    IDROL = idRol,

                    IDEMPRESA = empresaRegionZona,
                    IDREGION = empresaRegionZona,
                    IDZONA = empresaRegionZona,

                    IDTIPOPROCESO = infoProcesos.IDTIPOPROCESO,
                    IDTAREA = infoProcesos.IDTAREA,
                    TAREA_LOTE_NOCRED = tareaLoteCredito,
                    MOTIVO = motivo,
                    NOTA = nota,
                    DATOSANTERIORES = datosAnteriores,
                    DATOSACTUALES = datosActuales,
                    
                };

                Registar.Insertar(informacion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static void InsertarLogPresupuesto(int idUsuario, int idRol, int empresaRegionZona, string tipoProceso,
            string tarea, string tareaLoteCredito, string motivo, string nota, string datosAnteriores,
            string datosActuales,int intentoAlta, int intentoBaja)
        {
            try
            {
                var infoProcesos = GetIdTareaProceso(tipoProceso, tarea);

                var informacion = new K_LOG
                {
                    FECHA_ADICION = DateTime.Now,
                    IDUSUARIO = idUsuario,
                    IDROL = idRol,

                    IDEMPRESA = empresaRegionZona,
                    IDREGION = empresaRegionZona,
                    IDZONA = empresaRegionZona,

                    IDTIPOPROCESO = infoProcesos.IDTIPOPROCESO,
                    IDTAREA = infoProcesos.IDTAREA,
                    TAREA_LOTE_NOCRED = tareaLoteCredito,
                    MOTIVO = motivo,
                    NOTA = nota,
                    DATOSANTERIORES = datosAnteriores,
                    DATOSACTUALES = datosActuales,
                    Secuencia_E_Alta = intentoAlta,
                    Secuencia_E_Baja = intentoBaja
                };

                Registar.Insertar(informacion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static CAT_TAREAS_PROCESOS GetIdTareaProceso(string tipoProceso, string tarea)
        {
            var proceso = new CAT_TAREAS_PROCESOS();
            var informacionProceso = Registar.GetIdProceso(tipoProceso, tarea);
            
            foreach (var t  in informacionProceso)
            {
                proceso = new CAT_TAREAS_PROCESOS
                {
                    IDTIPOPROCESO = t.IDTIPOPROCESO,
                    IDTAREA = t.IDTAREA
                };
            }
            return proceso;
        }

        public static int GetIdUser(string nombreUsuario)
        {
            var id = new US_USUARIO();
            var list = Registar.GetIdUserName(nombreUsuario);
            foreach (var t in list)
            {
                id = new US_USUARIO
                {
                    Id_Usuario = t.Id_Usuario
                };
            }
            return id.Id_Usuario;
        }

        public static int GetIdProducto()
        {
            var idProducto = Registar.GetIdProducto();
            return idProducto;
        }

        public static int GetIdProveedor(string descEmpresa)
        {
            var id = 0;
            switch (descEmpresa)
            {
                case "DisponsalCenter":
                    id = Registar.GetIdProveedor();
                    break;
                case "DisponsalCenterBranch":
                    id = Registar.GetIdBranchProveedor();
                    break;
            }
            return id;

        }

        public static int GetIdEmpresa(string descEmpresa)
        {
            var id = 0;
            switch (descEmpresa)
            {
                case "DisponsalCenter":
                    id = Registar.GetIdCentroDisp();
                    break;
                case "DisponsalCenterBranch":
                    id = Registar.GetIdCentroDispBranch();
                    break;
            }
            return id;

        }

        public static Dictionary<string, object> GetRowFromObject(object obj)
        {
            var t = obj.GetType();

            //Con los BindingFlags no nos funciona, por eso se obtienen todas y posteriomente se filtra en el Foreach.
            var propertyInfos = t.GetProperties();

            var row = new Dictionary<string, object>();
            foreach (var propertyInfo in propertyInfos)
            {
                if (((!propertyInfo.CanRead) || (!propertyInfo.CanWrite)) ||
                    (!propertyInfo.PropertyType.Attributes.HasFlag(TypeAttributes.Public))) continue;
                var value = propertyInfo.GetValue(obj, new object[] {});
                row.Add(propertyInfo.Name, value);
            }
            return row;
        }

        public static string[] GetCambiosDatos(Object datosAnteriores, Object datosActuales)
        {
            try
            {
                string anterior = null;
                string actual = null;
                var objetoAnterior = GetRowFromObject(datosAnteriores);
                var objetoActual = GetRowFromObject(datosActuales);

                foreach (var parAnt in objetoAnterior)
                {
                    var ant = parAnt;
                    var ant1 = parAnt;
                    var parAnt1 = parAnt;
                    foreach (var parAct in from parAct in objetoActual
                        where ant.Key == parAct.Key
                        where ant1.Value != null && parAct.Value != null
                        where parAnt1.Value.ToString() != parAct.Value.ToString()
                        select parAct)
                    {
                        anterior +=
                            anterior == ""
                                ? parAnt.Key + ": " + parAnt.Value
                                : "|| " + parAnt.Key + ": " + parAnt.Value;
                        actual += actual == ""
                            ? parAct.Key + ": " + parAct.Value
                            : "|| " + parAct.Key + ": " + parAct.Value;
                    }
                }

                string[] t = {anterior, actual};

                return t;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string[] GetCambiosDatos(Object datosAnteriores, Object datosActuales, string nameObjeto)
        {
            var anterior = nameObjeto;
            var actual = nameObjeto;
            var objetoAnterior = GetRowFromObject(datosAnteriores);
            var objetoActual = GetRowFromObject(datosActuales);

            foreach (var parAnt in objetoAnterior)
            {
                var ant = parAnt;
                var ant1 = parAnt;
                var parAnt1 = parAnt;
                foreach (var parAct in from parAct in objetoActual
                    where ant.Key == parAct.Key
                    where ant1.Value != null && parAct.Value != null
                    where parAnt1.Value.ToString() != parAct.Value.ToString()
                    select parAct)
                {
                    anterior +=
                        anterior == ""
                            ? parAnt.Key + ": " + parAnt.Value
                            : "|| " + parAnt.Key + ": " + parAnt.Value;
                    actual += actual == ""
                        ? parAct.Key + ": " + parAct.Value
                        : "|| " + parAct.Key + ": " + parAct.Value;
                }
            }

            string[] t = {anterior, actual};

            return t;
        }

        public static string GetDatosObjeto(Object objeto)
        {
            var d = GetRowFromObject(objeto);
            return d.Aggregate("",
                (current, ciclo) =>
                    current + (current == "" ? ciclo.Key + ": " + ciclo.Value : "|| " + ciclo.Key + ": " + ciclo.Value));
        }

        public static string[] InsertaCambiosTarifasTecnologia(List<Tarifa_Tecnologia> tarifasTecnologiaAnterior, List<Tarifa_Tecnologia> tarifasTecnologiaActual, int idUser, int idRol, int idDep)
        {
            var  cambios = new[] {(string) null, null};
            var i = 0;
            var tarifaTecnologiaAn = new Tarifa_Tecnologia();
            foreach (var tarifaTecnologiaAc in tarifasTecnologiaActual)
            {
                if (i < tarifasTecnologiaAnterior.Count)
                {
                    tarifaTecnologiaAn = tarifasTecnologiaAnterior[i];
                }
                if (tarifaTecnologiaAc.CveTarifa == tarifaTecnologiaAn.CveTarifa)
                {
                    if (tarifaTecnologiaAn.Estatus != tarifaTecnologiaAc.Estatus)
                    {
                        var datosTarifa = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ObtieneCat_Tarifa(tarifaTecnologiaAc.CveTarifa);
                         cambios = GetCambiosDatos(tarifaTecnologiaAn, tarifaTecnologiaAc,"Tarifa " + datosTarifa.Dx_Tarifa +": ");
                         InsertarLog(idUser,idRol,idDep, "TECNOLOGIA", "CAMBIOS", "Tarifa "+tarifaTecnologiaAn.DxTarifa, "MOTIVOTarifasS??","", cambios[0], cambios[1]);
                    }
                }
                else
                {
                    var datosTarifa = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ObtieneCat_Tarifa(tarifaTecnologiaAc.CveTarifa);
                    var newTarifa = GetDatosObjeto(tarifaTecnologiaAc);
                    cambios[1] = " Nueva Tarifa: " + datosTarifa.Dx_Tarifa + ": " + newTarifa + "||";
                    InsertarLog(idUser, idRol, idDep, "TECNOLOGIA", "CAMBIOS", "Tarifa " +  datosTarifa.Dx_Tarifa, "MOTIVOTarifasS??", "", "", cambios[1]);
                }
                i++;
            }

            return cambios;
        }

        public static string[] InsertaCambiosCombinacionTecnologia(List<Combinacion_Tecnologia> combinacionTecnologiaAnterior, List<Combinacion_Tecnologia> combinacionTecnologiaActual, string Tecnologia, int idUser, int idRol, int idDep)
        {
            var cambios = new[] { null, (string) null };
            var i = 0;
            var combTecnologiaAn = new Combinacion_Tecnologia();
            foreach (var combTecnologiaAc in combinacionTecnologiaActual)
            {
                if (i < combinacionTecnologiaAnterior.Count)
                {
                    combTecnologiaAn = combinacionTecnologiaAnterior[i];
                }
                if (combTecnologiaAc.CveTecnologiaCombinada == combTecnologiaAn.CveTecnologiaCombinada)
                {
                    if (combTecnologiaAn.Estatus != combTecnologiaAc.Estatus)
                    {
                        cambios = GetCambiosDatos(combTecnologiaAn, combTecnologiaAc,
                            "Tecnología " + combTecnologiaAn.TecnologíaCombinada + ": ");
                        InsertarLog(idUser, idRol, idDep, "TECNOLOGIA", "CAMBIOS", Tecnologia, "MOTIVOSCombinacion??",
                            "", cambios[0], cambios[1]);
                    }
                }
                else
                {
                    var newCombinacion = GetDatosObjeto(combTecnologiaAc);
                    cambios[1] += "Nueva Combinación Tecnologia " + combTecnologiaAc.TecnologíaCombinada + ": " + newCombinacion + "||";
                    InsertarLog(idUser, idRol, idDep, "TECNOLOGIA", "CAMBIOS", Tecnologia, "MOTIVOSCombinacion??", "",
                        "", cambios[1]);
                }
                i++;
            }

            return cambios;
        }
    }
}




    

