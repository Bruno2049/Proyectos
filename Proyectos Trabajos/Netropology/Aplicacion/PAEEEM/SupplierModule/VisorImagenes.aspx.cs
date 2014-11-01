using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using PAEEEM.Entidades;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using System.Drawing;

namespace PAEEEM.SupplierModule
{
    public partial class VisorImagenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string hidIdCreditoSustitucion = Request.QueryString["IdCreditoSustitucion"];
                string hidIdConsecutivo = Request.QueryString["IdConsecutivo"];
                string CreditNumber = Request.QueryString["CreditNumber"];
                int idTipoFoto = int.Parse(Request.QueryString["idTipoFoto"]);
                
                CRE_FOTOS oFoto = null;
                if (idTipoFoto == 4)
                {
                    var oExistfoto = new CRE_FOTOS
                    {
                        No_Credito = CreditNumber,
                        idTipoFoto = 2,
                        idCreditoProducto = int.Parse(hidIdCreditoSustitucion),
                        idConsecutivoFoto = int.Parse(hidIdConsecutivo)
                    };
                    oFoto = InfoCompAltaBajaEquipos.ObtenertImagenCreditoProducto(oExistfoto);
                }

                else if (idTipoFoto == 3) 
                {
                    var oExistfoto = new CRE_FOTOS
                    {
                        No_Credito = CreditNumber,
                        idTipoFoto = idTipoFoto,
                        idCreditoProducto = int.Parse(hidIdCreditoSustitucion),
                        idConsecutivoFoto = int.Parse(hidIdConsecutivo)
                    };
                    oFoto = InfoCompAltaBajaEquipos.ObtenertImagenCreditoProducto(oExistfoto);
                }
                else if(idTipoFoto == 2)
                {

                    var oExistfoto = new CRE_FOTOS
                                     {
                                         No_Credito = CreditNumber,
                                         idTipoFoto = idTipoFoto,
                                         IdCreditoSustitucion = int.Parse(hidIdCreditoSustitucion),
                                     };
                    oFoto = InfoCompAltaBajaEquipos.ObtenertImagenCreditoSustitucion(oExistfoto);
                }
                else if (idTipoFoto == 1)
                {
                    var oExistfoto = new CRE_FOTOS()
                    {
                        No_Credito = CreditNumber,
                        idTipoFoto = 1,
                        idConsecutivoFoto = 1
                    };
                     oFoto = InfoCompAltaBajaEquipos.ObtenertImagenFachada(oExistfoto);
                }

                if (oFoto != null)
                {
                    MemoryStream str = new MemoryStream();
                    str.Write(oFoto.Foto, 0, oFoto.Foto.Length);
                    Bitmap bit = new Bitmap(str);

                    Response.ContentType = "image/jpeg";
                    bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                    
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                        string.Format("alert('{0}');", "No hay Foto para este producto..."), true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                        string.Format("alert('{0}');", ex.Message), true);
            }
        }
    }
}