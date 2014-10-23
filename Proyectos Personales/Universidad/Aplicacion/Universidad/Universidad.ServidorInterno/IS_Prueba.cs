using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Universidad.ServidorInterno
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IS_Prueba" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IS_Prueba
    {
        [OperationContract]
        void DoWork();

        [OperationContract] 
        int Prueba();
    }
}
