﻿namespace SunCorp.Entities.Entities
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UsUsuarios
    {
        [DataMember]
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
    }
}