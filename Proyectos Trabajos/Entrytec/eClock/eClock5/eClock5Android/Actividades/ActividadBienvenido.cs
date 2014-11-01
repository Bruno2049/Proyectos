using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace eClock5Android.Actividades
{
    [Activity(Label = "My Activity")]
    public class ActividadBienvenido : Activity
    {
        TextView Lbl_NombreEmpleado, Lbl_NumEmpleado, Lbl_UltimaSesion, Lbl_Estado;
        Button btnChkIn, btnChkOuT;
      
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Create your application here
            SetContentView(Resource.Layout.Bienvenido);

            Lbl_NombreEmpleado = FindViewById<TextView>(Resource.Id.lbl_NombreEmpleado);
            Lbl_NumEmpleado = FindViewById<TextView>(Resource.Id.Lbl_NumEmpleado);
            Lbl_UltimaSesion = FindViewById<TextView>(Resource.Id.Lbl_UltimaSesion);
            Lbl_Estado = FindViewById<TextView>(Resource.Id.Lbl_Estado);

            btnChkIn = FindViewById<Button>(Resource.Id.btnAgregarCheckada);

            MostrarDatos();
            btnChkIn.Click += btnChkIn_Click;
           
        }

        void btnChkIn_Click(object sender, EventArgs e)
        {
            eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Accesos a = new eClockBase.Controladores.Accesos(Sesion);
            
            
        }
        void MostrarDatos()
        {
            Lbl_NombreEmpleado.Text = Intent.GetStringExtra("Nombre");
            Lbl_NumEmpleado.Text = Intent.GetStringExtra("NumEmpledo");
            Lbl_UltimaSesion.Text = Intent.GetStringExtra("UltimaSesion");
        }
        void MostrarFoto(byte[] Foto)
        {
 
        }
    }
}