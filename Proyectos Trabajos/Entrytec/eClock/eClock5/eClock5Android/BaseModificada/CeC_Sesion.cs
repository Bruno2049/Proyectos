using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace eClock5Android.BaseModificada
{
    class CeC_Sesion : eClockBase.CeC_SesionBase
    {
        private static CeC_Sesion m_SesionActual = null;

        public static eClockBase.CeC_SesionBase ObtenSesion(object VentanaActual)
        {
            if (m_SesionActual == null)
            {
                //m_SesionActual = new CeC_Sesion();
                string Guardado = eClockBase.CeC_SesionBase.CargarDatosString();
                if (Guardado.Length > 0)
                {
                    m_SesionActual = JsonConvert.DeserializeObject<CeC_Sesion>(Guardado);
                    if (!m_SesionActual.MantenerSesion)
                        m_SesionActual.SESION_SEGURIDAD = "";
                }
                else
                    m_SesionActual = new CeC_Sesion();
            }
            return (eClockBase.CeC_SesionBase)m_SesionActual;
        }

        private CeC_Label Lbl_Estado = null;
        public override void MuestraMensaje(string Mensaje, int TimeOutSegundos = -1)
        {
            base.MuestraMensaje(Mensaje, TimeOutSegundos);
            if (Lbl_Estado == null)
                return;
            if (TimeOutSegundos > 0)
            {
                ///Codigo para Animación

            }
            else
            {
                //Codigo sin Animación
            }
            Lbl_Estado.m_Actividad.RunOnUiThread(() =>
            {
                Lbl_Estado.m_Etiqueta.Text = Mensaje;
            });

            
        }
        public override bool AsignaControlMensaje(object Control)
        {
            //Asigna un mensaje a la pantalla
            Lbl_Estado = Control as CeC_Label;
            return base.AsignaControlMensaje(Control);
        }

        public class CeC_Label 
        {
            public TextView m_Etiqueta;
            public Activity m_Actividad;
            public CeC_Label(TextView Etiqueta, Activity Actividad)
            {
                m_Etiqueta = Etiqueta;
                m_Actividad = Actividad;
            }
        }
    }
}
