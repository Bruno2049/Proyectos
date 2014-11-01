using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
#if !NETFX_CORE
using System.Windows.Media.Animation;
#else
using Windows.UI.Xaml.Media.Animation;
#endif

namespace KioscoAndroid.BaseModificada
{
    /// <summary>
    /// 
    /// </summary>
    public class CeC_Sesion : eClockBase.CeC_SesionBase
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
                    m_SesionActual = eClockBase.Controladores.CeC_ZLib.Json2Object<CeC_Sesion>(Guardado);
                    m_SesionActual.SUSCRIPCION_ID_SELECCIONADA = -1;
                    if (!m_SesionActual.MantenerSesion)
                        m_SesionActual.SESION_SEGURIDAD = "";
                }
                else
                    m_SesionActual = new CeC_Sesion();
            }
            return (eClockBase.CeC_SesionBase)m_SesionActual;
        }

        private System.Windows.Controls.TextBlock Lbl_Estado = null;
        public override void MuestraMensaje(string Mensaje, int TimeOutSegundos = -1)
        {
            base.MuestraMensaje(Mensaje, TimeOutSegundos);
            if (Lbl_Estado == null)
                return;
            if (TimeOutSegundos > 0)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 1;
                da.To = 0;
                da.Duration = new System.Windows.Duration(TimeSpan.FromSeconds(TimeOutSegundos));
                Lbl_Estado.BeginAnimation(System.Windows.Controls.TextBlock.OpacityProperty, da);

            }
            else
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 1;
                da.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(1));
                Lbl_Estado.BeginAnimation(System.Windows.Controls.TextBlock.OpacityProperty, da);
                //Lbl_Estado.Opacity = 1;
            }
            Lbl_Estado.Text = Mensaje;
        }
        public override bool AsignaControlMensaje(object Control)
        {
            Lbl_Estado = Control as System.Windows.Controls.TextBlock;
            return base.AsignaControlMensaje(Control);
        }
    }
}
