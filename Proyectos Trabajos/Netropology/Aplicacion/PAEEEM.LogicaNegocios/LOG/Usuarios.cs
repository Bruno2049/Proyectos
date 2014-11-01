using System;

namespace PAEEEM.LogicaNegocios.LOG
{
    [Serializable]
    public class Usuarios
    {
            public int Id_Usuario { get; set; }
            public int Id_Rol { get; set; }
            public string Nombre_Usuario { get; set; }
            public string Contrasena { get; set; }
            public string CorreoElectronico { get; set; }
            public string Numero_Telefono { get; set; }
            public string Nombre_Completo_Usuario { get; set; }
            public string Estatus { get; set; }
            public int Id_Departamento { get; set; }
            public string Tipo_Usuario { get; set; }
    }
}
