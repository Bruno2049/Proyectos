using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using Universidad.Entidades;
using Universidad.LogicaNegocios.GestionCatalogos;


namespace Universidad.ServidorInterno.GestionCatalogos
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "S_GestionCatalogos" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione S_GestionCatalogos.svc o S_GestionCatalogos.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class S_GestionCatalogos : IS_GestionCatalogos
    {
        public string ObtenTablaUsCatTipoUsuarios()
        {
            var Lista = Universidad.LogicaNegocios.GestionCatalogos.GestionCatalogos.ClassInstance.ObtenListaCatTiposUsuario();
            string JLista = Newtonsoft.Json.JsonConvert.SerializeObject(Lista);
            return JLista;
        }

        //public US_CAT_TIPO_USUARIO InsertaCatTipoUsuario(US_CAT_TIPO_USUARIO registroCatTipoUsuario,
        //    string NombreUsuario)
        //{
        //    return
        //        LogicaNegocios.GestionCatalogos.GestionCatalogos.ClassInstance.InsertaRegistroCatTipoUsuario(
        //            registroCatTipoUsuario, NombreUsuario);
        //}

        public string ObtenCatTipoUsuario(int Id_TipoUsuario)
        {
            var TipoUsuario = LogicaNegocios.GestionCatalogos.GestionCatalogos.ClassInstance.ObtenTipoUsuario(Id_TipoUsuario);

            string JObject = JsonConvert.SerializeObject(TipoUsuario);

            return JObject;
        }
    }
}
