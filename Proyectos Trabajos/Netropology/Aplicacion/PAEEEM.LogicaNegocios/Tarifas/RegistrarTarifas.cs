using System;
using System.Collections.Generic;
using System.Globalization;
using PAEEEM.AccesoDatos.Tarifas;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.LogicaNegocios.Tarifas
{
    public class RegistrarTarifas
    {
        
        public static List<CAT_TARIFA> TiposTarifa()
        {
            var tiposTarifas = new AccesoDatos.Catalogos.Tarifas().ObtieneTiposTarifas();
            return tiposTarifas;
        }

        public static List<CAT_REGIONES_TARIFAS> Regiones()
        {
            var regiones = new AccesoDatos.Catalogos.Tarifas().ObtieneRegiones();
            return regiones;
        }

        public static List<DetalleTarifa> RetornaDatosTarifas(int numTarifa, string fecha,int region)
        {
            List<DetalleTarifa> datos = null;
            switch (numTarifa)
            {
                case 1:
                    datos = new AccesoDatos.Catalogos.Tarifas().ObtieneDatosTarifa02(fecha);
                    break;
                case 2:
                    datos = new AccesoDatos.Catalogos.Tarifas().ObtieneDatosTarifa03(fecha);
                    break;
                case 3:
                    datos = region == -1 ? new AccesoDatos.Catalogos.Tarifas().ObtieneDatosTarifa0M(fecha) : new AccesoDatos.Catalogos.Tarifas().ObtieneDatosTarifa0M(fecha,region);
                    break;
                case 4:
                    datos = region == -1 ? new AccesoDatos.Catalogos.Tarifas().ObtieneDatosTarifaHm(fecha) : new AccesoDatos.Catalogos.Tarifas().ObtieneDatosTarifaHm(fecha,region);
                    break;
            }
            return datos;
        }

        public static string ValidaDatosTarifa02(int idTarifa, double cargoFijo, double cargoFirst50KWh,
            double cargoMayor50KWh, double cargoKWhAdicional, string fecha, string usuario, int idUsuario, int idRol, int idEmpresa, int idRegionUsuario, int idZona)
        {
            var validacion = "";
            if (cargoFijo != 0 && cargoFirst50KWh != 0 && cargoKWhAdicional != 0 && cargoMayor50KWh != 0)
            {
                var existe = Tarifa02.ObtienePorId(idTarifa);
                if (existe != null)
                {
                    try
                    {
                        var datos = new K_TARIFA_02
                        {
                            ID_TARIFA_02 = idTarifa,
                            MT_COSTO_KWH_FIJO = cargoFijo,
                            MT_COSTO_KWH_BASICO = cargoFirst50KWh,
                            MT_COSTO_KWH_INTERMEDIO = cargoMayor50KWh,
                            MT_COSTO_KWH_EXCEDENTE = cargoKWhAdicional ,
                            FECHA_APLICABLE = fecha,
                            ADICIONADO_POR = existe.ADICIONADO_POR,
                            FECHA_ADICION = existe.FECHA_ADICION,
                            MODIFICADO_POR = usuario,
                            FECHA_MODIFICACION = DateTime.Now
                        };
                        var actualiza = Tarifa02.Actualizar(datos);
                        if (actualiza)
                        {
                            var cambiosDatos = Insertlog.GetCambiosDatos(existe, datos);
                            /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO TARIFAS EN LOG*/
                            Insertlog.InsertarLog(idUsuario, idRol, idEmpresa, //idRegionUsuario,idZona
                                "TARIFAS", "CAMBIOS", "TARIFA 02: " + datos.FECHA_APLICABLE,
                                "Motivos??", "", cambiosDatos[0], cambiosDatos[1]);
                            validacion = "Actualizo Correctamente";
                        }
                    }
                    catch (Exception e)
                    {
                        validacion = "Ocurrió un error al Actualizar. Motivo:" +
                                     e.Message.ToString(CultureInfo.InvariantCulture);
                    }
                }
                else //Inserta
                {
                    try
                    {
                        var datos = new K_TARIFA_02
                        {
                            ID_TARIFA_02 = idTarifa,
                            MT_COSTO_KWH_FIJO = cargoFijo,
                            MT_COSTO_KWH_BASICO = cargoFirst50KWh,
                            MT_COSTO_KWH_INTERMEDIO = cargoMayor50KWh,
                            MT_COSTO_KWH_EXCEDENTE = cargoKWhAdicional,
                            FECHA_APLICABLE = fecha,
                            ADICIONADO_POR = usuario,
                            FECHA_ADICION = DateTime.Now
                        };
                        var dato = Tarifa02.Insertar(datos);
                        if (dato.ID_TARIFA_02 != 0)
                        {
                            /*INSERTAR EVENTO ALTA DEL TIPO DE PROCESO TARIFAS EN LOG*/
                            Insertlog.InsertarLog(idUsuario, idRol, idEmpresa,//idRegionUsuario,idZona
                                "TARIFAS", "ALTA", "TARIFA 02: " + datos.FECHA_APLICABLE,
                                "", "Fecha Alta: " + DateTime.Now, "", "");
                            validacion = "Insertó Correctamente";
                        }
                    }
                    catch (Exception e)
                    {
                        validacion = "Ocurrió un error al insertar. Motivo:" +
                                     e.Message.ToString(CultureInfo.InvariantCulture);
                    }

                }
            }
            else
            {
                validacion = "Capture todos los datos que solicita la tarifa";
            }
            return validacion;
        }

        public static string ValidaDatosTarifa03(int idTarifa, double cargoDemandaMax, double cargoAdicional,
            string fecha, string usuario, int idUsuario, int idRol, int idEmpresa, int idRegionUsuario, int idZona)
        {
            var validacion = "";
            if (cargoDemandaMax != 0 && cargoAdicional != 0)
            {
                var existe = Tarifa03.ObtienePorId(idTarifa);
                if (existe != null)
                {
                    try
                    {
                        var datos = new K_TARIFA_03
                        {
                            ID_TARIFA_03 = idTarifa,
                            MT_CARGO_ADICIONAL = cargoAdicional,
                            MT_CARGO_DEMANDA_MAX = cargoDemandaMax,
                            FECHA_APLICABLE = fecha,
                            ADICIONADO_POR = existe.ADICIONADO_POR,
                            FECHA_ADICION = existe.FECHA_ADICION,
                            MODIFICADO_POR = usuario,
                            FECHA_MODIFICACION = DateTime.Now
                        };
                        var actualiza = Tarifa03.Actualizar(datos);
                        if (actualiza)
                        {
                            var cambiosDatos = Insertlog.GetCambiosDatos(existe, datos);
                            /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO TARIFAS EN LOG*/
                            Insertlog.InsertarLog(idUsuario, idRol, idEmpresa, //idRegionUsuario,idZona
                                "TARIFAS", "CAMBIOS", "TARIFA 03: " + datos.FECHA_APLICABLE,
                                "Motivos??", "", cambiosDatos[0], cambiosDatos[1]);
                            validacion = "Actualizo Correctamente";
                        }
                    }
                    catch (Exception e)
                    {
                        validacion = "Ocurrió un error al Actualizar. Motivo:" +
                                     e.Message.ToString(CultureInfo.InvariantCulture);
                    }
                }
                else //Inserta
                {
                    try
                    {
                        var datos = new K_TARIFA_03
                        {
                            ID_TARIFA_03 = idTarifa,
                            MT_CARGO_ADICIONAL = cargoAdicional,
                            MT_CARGO_DEMANDA_MAX = cargoDemandaMax,
                            FECHA_APLICABLE = fecha,
                            ADICIONADO_POR = usuario,
                            FECHA_ADICION = DateTime.Now
                        };
                        var dato = Tarifa03.Insertar(datos);
                        if (dato.ID_TARIFA_03 != 0)
                        {
                            /*INSERTAR EVENTO ALTA DEL TIPO DE PROCESO TARIFAS EN LOG*/
                            Insertlog.InsertarLog(idUsuario, idRol, idEmpresa, //idRegionUsuario,idZona
                                "TARIFAS", "ALTA","TARIFA 03:" + dato.FECHA_APLICABLE,
                                "", "Fecha Alta: " + DateTime.Now, "", "");
                            validacion = "Insertó Correctamente";
                        }
                    }
                    catch (Exception e)
                    {
                        validacion = "Ocurrió un error al insertar. Motivo:" +
                                     e.Message.ToString(CultureInfo.InvariantCulture);
                    }

                }
            }
            else
            {
                validacion = "Capture todos los datos que solicita la tarifa";
            }
            return validacion;
        }

        public static string ValidaDatosTarifaOm(int idTarifa, int idRegion, double cargoKwDemandaMax, double cargoKWhDemandaConsumida,string fecha, string usuario, int idUsuario, int idRol, int idEmpresa, int idRegionUsuario, int idZona)
        {
            var validacion = "";
            if (cargoKwDemandaMax != 0 && cargoKWhDemandaConsumida != 0)
            {
                var existe = TarifaOm.ObtienePorId(idTarifa);
                if (existe != null) //UPDATE
                {
                    try
                    {
                        var datos = new K_TARIFA_OM
                        {
                            ID_TARIFA_OM = idTarifa,
                            ID_REGION = idRegion,
                            MT_CARGO_KWH_CONSUMO = cargoKWhDemandaConsumida,
                            MT_CARGO_KW_DEMANDA = cargoKwDemandaMax,
                            FECHA_APLICABLE = fecha,
                            ADICIONADO_POR = existe.ADICIONADO_POR,
                            FECHA_ADICION = existe.FECHA_ADICION,
                            MODIFICADO_POR = usuario,
                            FECHA_MODIFICACION = DateTime.Now
                        };
                        var actualiza = TarifaOm.Actualizar(datos);
                        if (actualiza)
                        {
                            var cambiosDatos = Insertlog.GetCambiosDatos(existe, datos);
                            /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO TARIFAS EN LOG*/
                            var region = Catregionestarifas.GetRegion(datos.ID_REGION);
                            Insertlog.InsertarLog(idUsuario, idRol, idEmpresa, //idRegionUsuario,idZona
                                "TARIFAS", "CAMBIOS", "TARIFA OM: " + datos.FECHA_APLICABLE,
                                "Motivos??", region.Descripcion, cambiosDatos[0], cambiosDatos[1]);

                            validacion = "Actualizo Correctamente";
                        }
                    }
                    catch (Exception e)
                    {
                        validacion = "Ocurrió un error al Actualizar. Motivo:" +
                                     e.Message.ToString(CultureInfo.InvariantCulture);
                    }
                }
                else //INSERT
                {
                    try
                    {
                        var datos = new K_TARIFA_OM
                        {
                            ID_TARIFA_OM = idTarifa,
                            ID_REGION = idRegion,
                            MT_CARGO_KWH_CONSUMO = cargoKWhDemandaConsumida,
                            MT_CARGO_KW_DEMANDA = cargoKwDemandaMax,
                            FECHA_APLICABLE = fecha,
                            ADICIONADO_POR = usuario,
                            FECHA_ADICION = DateTime.Now
                        };
                        var dato = TarifaOm.Insertar(datos);
                        if (dato.ID_TARIFA_OM != 0)
                        {
                            /*INSERTAR EVENTO ALTA DEL TIPO DE PROCESO TARIFAS EN LOG*/
                            var region =Catregionestarifas.GetRegion(datos.ID_REGION);
                            Insertlog.InsertarLog(idUsuario, idRol,idEmpresa,//idRegionUsuario,idZona
                                "TARIFAS", "ALTA", "TARIFA OM: " + dato.FECHA_APLICABLE,
                                "", "Fecha Alta: " + DateTime.Now,"", region.Descripcion);
                            validacion = "Insertó Correctamente";
                        }
                    }
                    catch (Exception e)
                    {
                        validacion = "Ocurrió un error al insertar. Motivo:" +
                                     e.Message.ToString(CultureInfo.InvariantCulture);
                    }
                }
            }
            else
            {
                validacion = "Capture todos los datos que solicita la tarifa";
            }
            return validacion;
        }

        public static string ValidaDatosTarifaHm(int idTarifa, int idRegion, double cargoKwDemandaFac, double cargoKWhEnergiaBase,
            double cargoKWhEnergiaIntermedia, double cargoKWhEnergiaPunta,
            string fecha, string usuario,int idUsuario, int idRol, int idEmpresa, int idRegionUsuario, int idZona)
        {
            var validacion = "";
            if (cargoKwDemandaFac != 0 && cargoKWhEnergiaBase != 0 && cargoKWhEnergiaIntermedia != 0 &&
                cargoKWhEnergiaPunta != 0)
            {
                var promedioTarifa = Math.Round((cargoKWhEnergiaBase + cargoKWhEnergiaIntermedia + cargoKWhEnergiaPunta)/3,4);
                var existe = TarifaHm.ObtienePorId(idTarifa);
                if (existe != null)
                {
                    try
                    {
                        var datos = new K_TARIFA_HM
                        {
                            ID_TARIFA_HM = idTarifa,
                            ID_REGION = idRegion,
                            MT_CARGO_DEMANDA = cargoKwDemandaFac,
                            MT_CARGO_BASE = cargoKWhEnergiaBase,
                            MT_CARGO_INTERMEDIA = cargoKWhEnergiaIntermedia,
                            MT_CARGO_PUNTA = cargoKWhEnergiaPunta,
                            FECHA_APLICABLE = fecha,
                            PROMEDIO_TARIFA = promedioTarifa,
                            ADICIONADO_POR = existe.ADICIONADO_POR,
                            FECHA_ADICION = existe.FECHA_ADICION,
                            MODIFICADO_POR = usuario,
                            FECHA_MODIFICACION = DateTime.Now
                        };
                        var actualiza = TarifaHm.Actualizar(datos);
                        if (actualiza)
                        {
                            var cambiosDatos = Insertlog.GetCambiosDatos(existe, datos);
                            /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO TARIFAS EN LOG*/
                            Insertlog.InsertarLog(idUsuario, idRol, idEmpresa, //idRegionUsuario,idZona
                                "TARIFAS", "CAMBIOS", "TARIFA HM: " + datos.FECHA_APLICABLE,
                                "Motivos??", "", cambiosDatos[0], cambiosDatos[1]);
                            validacion = "Actualizó Correctamente";
                        }
                    }
                    catch (Exception e)
                    {
                        validacion = "Ocurrió un error al Actualizar. Motivo:" +
                                     e.Message.ToString(CultureInfo.InvariantCulture);
                    }
                }
                else //Inserta
                {
                    try
                    {
                        var datos = new K_TARIFA_HM
                        {
                            ID_TARIFA_HM = idTarifa,
                            ID_REGION = idRegion,
                            MT_CARGO_DEMANDA = cargoKwDemandaFac,
                            MT_CARGO_BASE = cargoKWhEnergiaBase,
                            MT_CARGO_INTERMEDIA = cargoKWhEnergiaIntermedia,
                            MT_CARGO_PUNTA = cargoKWhEnergiaPunta,
                            FECHA_APLICABLE = fecha,
                            PROMEDIO_TARIFA = promedioTarifa,
                            ADICIONADO_POR = usuario,
                            FECHA_ADICION = DateTime.Now
                        };
                        var dato = TarifaHm.Insertar(datos);
                        if (dato.ID_TARIFA_HM != 0)
                        {
                            /*INSERTAR EVENTO ALTA DEL TIPO DE PROCESO TARIFAS EN LOG*/
                            var region = Catregionestarifas.GetRegion(dato.ID_REGION);
                            Insertlog.InsertarLog(idUsuario, idRol, idEmpresa,//idRegionUsuario,idZona
                                "TARIFAS", "ALTA", "TARIFA HM: " + dato.FECHA_APLICABLE,
                                "",region.Descripcion, "", "");
                            validacion = "Insertó Correctamente";
                        }
                    }
                    catch (Exception e)
                    {
                        validacion = "Ocurrió un error al insertar. Motivo:" +
                                     e.Message.ToString(CultureInfo.InvariantCulture);
                    }
                }
            }
            else
            {
                validacion = "Capture todos los datos que solicita la tarifa";
            }
            return validacion;
        }
    }
}
