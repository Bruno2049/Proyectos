using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using eClockBase;
using eClockBase.Modelos;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class Campos
    {
        
        CeC_SesionBase m_SesionBase;
        ES_Campos.S_CamposClient m_S_Campos = null;
        public Campos(CeC_SesionBase SesionBase)
        {
            m_SesionBase = SesionBase;            
            m_S_Campos = new ES_Campos.S_CamposClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Campos.svc"));
  
        }

        /// <summary>
        /// Función que se encarga de pasar como parametro el TIPO_PERSONA_ID, el cuál usando una estructura
        /// de datos llamada Model_CAMPOS_DATOS, mandara a llamar una fucnión que nos devolvera los datos
        /// con la estructura antes mencionada y pasarlos como un listado.
        /// </summary>
        /// <param name="TIPO_PERSONA_ID"></param>
        /// <returns></returns>
        public delegate void ListaCamposFinalizadoArgs(List<eClockBase.Modelos.Model_CAMPOS_DATOS> CamposDatos);
        public event ListaCamposFinalizadoArgs ListaCamposFinalizado;

        public void ListaCampos(int TIPO_PERSONA_ID)
        {
            m_S_Campos.ListaCamposCompleted += m_S_Campos_ListaCamposCompleted;
            m_S_Campos.ListaCamposAsync(m_SesionBase.SESION_SEGURIDAD, TIPO_PERSONA_ID, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA, m_SesionBase.IDIOMA);

        }

        void m_S_Campos_ListaCamposCompleted(object sender, ES_Campos.ListaCamposCompletedEventArgs e)
        {

            try
            {
                string Resultado = e.Result;
                string IniciaCon = Resultado.Substring(0, 1);
                bool Modelo = false;
                switch (IniciaCon)
                {
                    case "{":
                        Resultado = "[" + Resultado + "]";
                        Modelo = true;
                        break;
                    case "[":
                        Modelo = true;
                        break;
                    default:
                        if (Resultado == "")
                        {

                        }
                        break;
                }
                if (Modelo)
                {
                    List<Model_CAMPOS_DATOS> ObtenerCamposDatos = eClockBase.Controladores.CeC_ZLib.Json2Object<List<Model_CAMPOS_DATOS>>(Resultado);
                    if (ListaCamposFinalizado != null)
                        ListaCamposFinalizado(ObtenerCamposDatos);
                }
                return;
            }
            catch { }
            m_SesionBase.MuestraMensaje("Error de Red"); 
        }

        public delegate void GuardaArgs(int Guardados);
        public event GuardaArgs GuardaEvento;
        /*public void GuardarCampos(Model_CAMPOS_DATOS Modelo){
            
            m_SesionBase.MuestraMensaje("Guardando Datos");
            m_Sesion.GuardaDatos("EC_CAMPOS_DATOS", "CAMPO_DATO_ID", JsonConvert.SerializeObject(Modelo), m_SesionBase.SUSCRIPCION_ID, true); 
        }*/
        public void Guardar(Model_CAMPOS_DATOS Modelo)
        {
            List<Model_CAMPOS_DATOS> Modelos = new List<Model_CAMPOS_DATOS>();
            Modelos.Add(Modelo);
            Guardar(Modelos);
        }
        public void Guardar(List<Model_CAMPOS_DATOS> Modelos)
        {
            m_SesionBase.MuestraMensaje("Guardando Datos");
            m_S_Campos.GuardaCamposCompleted += m_S_Campos_GuardaCamposCompleted;
            m_S_Campos.GuardaCamposAsync(m_SesionBase.SESION_SEGURIDAD, Newtonsoft.Json.JsonConvert.SerializeObject(Modelos));
        }

        void m_S_Campos_GuardaCamposCompleted(object sender, ES_Campos.GuardaCamposCompletedEventArgs e)
        {
            try
            {
                
                if (e.Result != null)
                {
                    if (e.Result != "ERROR")
                    {

                        m_SesionBase.MuestraMensaje("Guardado", 1);
                        if (GuardaEvento != null)
                            GuardaEvento(CeC.Convierte2Int(e.Result));
                        return;
                    }
                }
                if (GuardaEvento != null)
                    GuardaEvento(-1);
                m_SesionBase.MuestraMensaje("Error al Guardar", 5);

                return;
            }
            catch {  }
            if (GuardaEvento != null)
                GuardaEvento(-1);
            m_SesionBase.MuestraMensaje("Error de red", 5);
        }



    }
}
