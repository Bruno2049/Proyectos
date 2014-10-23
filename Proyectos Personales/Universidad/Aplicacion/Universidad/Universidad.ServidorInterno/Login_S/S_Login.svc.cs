﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Universidad.LogicaNegocios.LoginL;
using Newtonsoft.Json;

namespace Universidad.ServidorInterno.Login_S
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "S_Login" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione S_Login.svc o S_Login.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class S_Login : IS_Login
    {
        public string  LoginAdministrador(string Usuario, string Contrasena)
        {
            var Login = LoginL.ClassInstance.LoginAdminitradorUsuarios(Usuario, Contrasena);
            var JLogin = JsonConvert.SerializeObject(Login);
            return JLogin;
        }
    }
}
