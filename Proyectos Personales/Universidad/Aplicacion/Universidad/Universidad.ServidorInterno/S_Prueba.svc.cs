using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Universidad.ServidorInterno
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "S_Prueba" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione S_Prueba.svc o S_Prueba.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class S_Prueba : IS_Prueba
    {
        public void DoWork()
        {
        }

        public int Prueba()
        {
            return 1;
        }
    }
}
