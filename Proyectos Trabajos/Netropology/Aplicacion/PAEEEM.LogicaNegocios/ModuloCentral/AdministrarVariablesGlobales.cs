using System;
using System.Collections.Generic;
using System.Globalization;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class AdministrarVariablesGlobales
    {
        public static List<DetalleVariablesGlobales> Variables()
        {
            var variables = new AccesoDatos.Catalogos.VariablesGlobales().ObtenerVariablesGlobales();
            return variables;
        }

        public static string actualizar(int idp, int ids, string valor)
        {
            var validacion = "";
            if (valor!="")
            {
                var existe = VariablesGlobales.ObtienePorId(idp,ids);
                if (existe != null)
                {
                    try
                    {
                        var datos = new TR_PARAMETROS_GLOBALES
                        {
                            IDPARAMETRO = idp,
                            IDSECCION = ids,
                            DESCRIPCION = existe.DESCRIPCION,
                            VALOR = valor,
                            ESTATUS = existe.ESTATUS,
                            FECHA_ADICION = existe.FECHA_ADICION,
                            ADICIONADO_POR = existe.ADICIONADO_POR,
                            PARAMETRO_MODIFICABLE=existe.PARAMETRO_MODIFICABLE
                        };
                        var actualiza = VariablesGlobales.Actualizar(datos);
                        if (actualiza)
                        {
                            validacion = "Actualizó Correctamente";
                        }
                    }
                    catch (Exception e)
                    {
                        validacion = "Ocurrió un error al Actualizar. Motivo:" +
                                     e.Message.ToString(CultureInfo.InvariantCulture);
                    }
                }
            }
            else
            {
                validacion = "Capture el valor de la variable";
            }
            return validacion;
        }

        public static string actualizarPro(byte idpp, string valor)
        {
            var validacion = "";
            if (valor != "")
            {
                var existe = VariablesGlobales.ObtienePorIdPro(idpp);
                if (existe != null)
                {
                    try
                    {
                        //if (Id_TF_TI == 1)
                            existe.Mt_Fondo_Total_Programa = decimal.Parse(valor);
                        //else if (Id_TF_TI == 2)
                        //    existe.Mt_Fondo_Disponible_Incentivo = decimal.Parse(valor);

                        var actualiza = VariablesGlobales.ActualizarPro(existe);
                        if (actualiza)
                        {
                            validacion = "Actualizó Correctamente";
                        }
                    }
                    catch (Exception e)
                    {
                        validacion = "Ocurrió un error al Actualizar. Motivo:" +
                                     e.Message.ToString(CultureInfo.InvariantCulture);
                    }
                }
            }
            else
            {
                validacion = "Capture el valor de la variable";
            }
            return validacion;
        }
    }
}
