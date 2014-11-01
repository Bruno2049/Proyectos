using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTA: puede usar el comando "Cambiar nombre" del menú "Refactorizar" para cambiar el nombre de interfaz "IS_Solicitudes" en el código y en el archivo de configuración a la vez.
[ServiceContract]
public interface IS_Solicitudes
{
	[OperationContract]
	void DoWork();
}
