using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Webkit;


namespace KioscoAndroid.Actividades
{
    [Activity(Label = "eClock Mobile", MainLauncher = true, Icon = "@drawable/icon")]
    public class Act_Login : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Poner la vista principal
            SetContentView(Resource.Layout.L_Login);
            // Instanciar objetos xml de la interface login en variables
            EditText Tbx_Usuario = FindViewById<EditText>(Resource.Id.Tbx_Usuario);
            EditText Tbx_Password = FindViewById<EditText>(Resource.Id.Tbx_Password);
            Button Btn_Ingresar = FindViewById<Button>(Resource.Id.Btn_Ingresar);
            Btn_Ingresar.Click += delegate
            {
                // Validar los campos que no esten vacios.
                validar(Tbx_Password.Text, Tbx_Usuario.Text);
                // Iniciar la interacción con el login.




            };
        }
        public void validar(String Tbx_Password, String Tbx_Usuario)
        {
            if (Tbx_Password.Equals("") || Tbx_Usuario.Equals(""))
            {
                RunOnUiThread(() =>
                {
                    AlertDialog.Builder builder;
                    builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Atención");
                    builder.SetMessage("No debe haber campos vacios");
                    builder.SetCancelable(false);
                    builder.SetPositiveButton("OK", delegate { /*Finish();*/ });
                    builder.Show();
                }
                );
            }

        }


    }
}