using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Descripción breve de WS_Importacion
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WS_Importacion : System.Web.Services.WebService {

    public WS_Importacion () {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Obtiene el Persona_ID a partir del Persona_Link_ID
    /// </summary>
    [WebMethod(Description = "Obtiene el identificador de la persona", EnableSession = true)]
    public int ObtenPersonaID(int Persona_Link_ID)
    {
        return CeC_Personas.ObtenPersonaID(Persona_Link_ID);
    }

    [WebMethod(Description = "Asigna una foto a una persona", EnableSession = true)]
    public bool AsignaFoto(int Persona_ID, byte[] Foto)
    {
        return CeC_Personas.AsignaFoto(Persona_ID, Foto);
    }
}

