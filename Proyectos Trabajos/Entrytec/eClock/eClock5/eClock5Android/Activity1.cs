using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using eClock5Android.BaseModificada;

namespace eClock5Android
{
    [Activity(Label = "eClock5Android", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar")]
    public class Activity1 : Activity
    {
        int count = 1;
        eClockBase.CeC_SesionBase m_Sesion = null;
        TextView Lbl;
        Button button;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            button = FindViewById<Button>(Resource.Id.MyButton);
            Lbl = FindViewById<TextView>(Resource.Id.textView1);

            eClockBase.CeC_Stream.MetodoStream = new CeC_StreamFile();
            eClockBase.CeC_LogDestino.StreamWriter = CeC_StreamFile.sAgregaTexto("eClock5.log");

            m_Sesion = CeC_Sesion.ObtenSesion(this);
            m_Sesion.AsignaControlMensaje(new CeC_Sesion.CeC_Label(Lbl, this));
            Lbl.Text = "Prueba";
            button.Click += delegate
            {

                //button.Text = string.Format("{0} clicks!", count++);
                eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(m_Sesion);
                CSesion.LogeoFinalizado += CSesion_LogeoFinalizado;
                /*
                CSesion.LogeoFinalizado += (sender, e) =>
                {
                    string Resultado = e.ToString();
                    StartActivity(typeof(Actividades.ActividadBienvenido));
                };
                */
                EditText NombreUsuario = FindViewById<EditText>(Resource.Id.editText1);
                EditText Pass = FindViewById<EditText>(Resource.Id.editText2);
                CSesion.CreaSesion_InicioAdv(NombreUsuario.Text, Pass.Text, false);

            };
        }

        void CSesion_LogeoFinalizado(object sender, EventArgs e)
        {
            if (Lbl.Text == "Correcto")
            {
                MostrarDatos();
                //StartActivity(typeof(Actividades.ActividadBienvenido));
            }
            /*
            RunOnUiThread(() => 
            {
                
                //Button button = FindViewById<Button>(Resource.Id.MyButton);
                button.Text = "CSesion_LogeoFinalizado";
                Lbl = FindViewById<TextView>(Resource.Id.textView1);
                Lbl.Text = "CSesion_LogeoFinalizado"; 
            });
            /*Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Text = "CSesion_LogeoFinalizado";
            //m_Sesion.MuestraMensaje("CSesion_LogeoFinalizado");
            //Lbl = FindViewById<TextView>(Resource.Id.textView1);
            //Lbl.Text = "CSesion_LogeoFinalizado";
            */
        }
        public void MostrarDatos()
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            Sesion.AsignaControlMensaje(Lbl);
            eClockBase.Controladores.Sesion SE = new eClockBase.Controladores.Sesion(Sesion);
            SE.ObtenDatosPersonaEvent += SE_ObtenDatosPersonaEvent;
            SE.ObtenDatosPersona();
        }

        void SE_ObtenDatosPersonaEvent(eClockBase.Modelos.Sesion.Model_DatosPersona Datos)
        {
            if (Datos != null)
            {
                var second = new Intent(this, typeof(Actividades.ActividadBienvenido));
                second.PutExtra("Nombre", Datos.PERSONA_NOMBRE);
                second.PutExtra("NumEmpleado", eClockBase.CeC.Convierte2String(Datos.PERSONA_LINK_ID));
                second.PutExtra("UltimaSesion", eClockBase.CeC.Convierte2String(Datos.UltimaSesion));
                second.PutExtra("Foto",Datos.PERSONA_IMA);
                StartActivity(second);
            }
        }
    }
}