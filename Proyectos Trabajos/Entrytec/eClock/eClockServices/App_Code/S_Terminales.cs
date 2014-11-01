using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using Newtonsoft.Json;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Terminales
{
    // Para usar HTTP GET, agregue el atributo [WebGet]. (El valor predeterminado de ResponseFormat es WebMessageFormat.Json)
    // Para crear una operación que devuelva XML,
    //     agregue [WebGet(ResponseFormat=WebMessageFormat.Xml)]
    //     e incluya la siguiente línea en el cuerpo de la operación:
    //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
    [OperationContract]
    public void DoWork()
    {
    }

    /// <summary>
    /// Guarda los datos con respecto a las propiedades de contenedo deserializando;
    /// una vez guardados los datos se procede a serializar los objetos anteriormente guardados
    /// de las terminales configuradas.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Datos"></param>
    /// <returns>Vacio si no fu correcto</returns>
    [OperationContract]
    public String GuardaDatos(string SesionSeguridad, string Datos)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;

        try
        {
            string DatosJson = eClockBase.Controladores.CeC_ZLib.ZJson2Json(Datos);
            List<eClockBase.Modelos.Model_TERMINALES> Terminales = null;
            if (DatosJson.Length > 0 && DatosJson[0] == '[')
                Terminales = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Model_TERMINALES>>(DatosJson);
            else
            {
                Terminales = new List<eClockBase.Modelos.Model_TERMINALES>();
                Terminales.Add(JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TERMINALES>(DatosJson));
            }
            int NoModificados = 0;
            foreach (eClockBase.Modelos.Model_TERMINALES Terminal in Terminales)
            {
                try
                {

                    bool EsNuevo = true;
                    if (Terminal.TERMINAL_ID > 0)
                    {
                        EsNuevo = false;
                    }

                    if (Terminal.TERMINAL_MODELO != null && Terminal.TERMINAL_MODELO != "")
                    {
                        if (EsNuevo || CeC_Terminales.ObtenTerminalModelo(Terminal.TERMINAL_ID) != Terminal.TERMINAL_MODELO)
                        {
                            eClockBase.Modelos.Model_MODELOS Modelo = new eClockBase.Modelos.Model_MODELOS();
                            Modelo.MODELO_ID = CeC.Convierte2Int(Terminal.TERMINAL_MODELO);
                            string DatosModelo = CeC_Tabla.ObtenDatos("EC_MODELOS", "MODELO_ID", Modelo, Sesion);
                            Modelo = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Model_MODELOS>(DatosModelo);
                            //MODELO_ID, MODELO_MARCA, MODELO_MODELO, MODELO_COMENTARIO, MODELO_CAMPO_LLAVE, 
                            //MODELO_CAMPO_ADICIONAL, TIPO_TECNOLOGIA_ID, TIPO_TECNOLOGIA_ADD_ID, 
                            //MODELO_NO_HUELLAS, MODELO_NO_TARJETAS, MODELO_NO_ROSTROS, MODELO_NO_PALMAS, MODELO_NO_IRIS, MODELO_TERMINAL_ID, MODELO_BORRADO
                            Terminal.TERMINAL_CAMPO_LLAVE = Modelo.MODELO_CAMPO_LLAVE;
                            Terminal.TERMINAL_CAMPO_ADICIONAL = Modelo.MODELO_CAMPO_ADICIONAL;
                            Terminal.TIPO_TECNOLOGIA_ID = Modelo.TIPO_TECNOLOGIA_ID;
                            Terminal.TIPO_TECNOLOGIA_ADD_ID = Modelo.TIPO_TECNOLOGIA_ADD_ID;
                            Terminal.TERMINAL_NO_HUELLAS = Modelo.MODELO_NO_HUELLAS;
                            Terminal.TERMINAL_NO_TARJETAS = Modelo.MODELO_NO_TARJETAS;
                            Terminal.TERMINAL_NO_ROSTROS = Modelo.MODELO_NO_ROSTROS;
                            Terminal.TERMINAL_NO_PALMAS = Modelo.MODELO_NO_PALMAS;
                            Terminal.TERMINAL_NO_IRIS = Modelo.MODELO_NO_IRIS;
                            Terminal.MODELO_TERMINAL_ID = Modelo.MODELO_TERMINAL_ID;
                        }
                    }

                    int TerminalID = CeC_Tabla.GuardaDatos("EC_TERMINALES", "TERMINAL_ID", JsonConvert.SerializeObject(Terminal), EsNuevo, Sesion, Sesion.SUSCRIPCION_ID);
                    if (EsNuevo)
                        Terminal.TERMINAL_ID = TerminalID;
                    if (TerminalID > 0)
                        NoModificados++;
                }
                catch { }
            }
            return NoModificados.ToString();
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }
}