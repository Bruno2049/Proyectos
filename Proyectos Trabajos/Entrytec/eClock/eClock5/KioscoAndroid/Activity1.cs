using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using eClock5Android;
using eClock5Android.BaseModificada;
namespace KioscoAndroid
{
    [Activity(Label = "KioscoAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
			TextView Lbl_Resultado = FindViewById<TextView>(Resource.Id.Lbl_Resultado);


			TextView Lbl_Estado = FindViewById<TextView>(Resource.Id.Lbl_Estado);

			eClockBase.CeC_Stream.MetodoStream = new CeC_StreamFile();
			eClockBase.CeC_LogDestino.StreamWriter = CeC_StreamFile.sAgregaTexto("eClock5.log");



			Lbl_Estado.Text = "Prueba";


            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
			Button Btn_LogIn = FindViewById<Button>(Resource.Id.Btn_LogIn);
			Btn_LogIn.Click += delegate {
				eClockBase.CeC_SesionBase Sesion = eClock5Android.BaseModificada.CeC_Sesion.ObtenSesion(this);
				Sesion.AsignaControlMensaje(new CeC_Sesion.CeC_Label(Lbl_Estado, this));
				eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
				cSesion.LogeoFinalizado += delegate(object sender, EventArgs e)
				{

						RunOnUiThread(() =>						{
						if(Sesion.EstaLogeado())
							Lbl_Resultado.Text = "En hora buena";
						else
							Lbl_Resultado.Text = "Error de usuario Intente nuevamente";
						});

				};
				cSesion.CreaSesion_InicioAdv("gventura@entrytec.com.mx", "152152", false);
			};

        }
    }
}

