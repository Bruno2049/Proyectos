using System;
using System.Data;
using System.Configuration;
using System.Net;

/// <summary>
/// Descripción breve de CeC_Terminales
/// </summary>
public class CeC_Terminales
{
	public CeC_Terminales()
	{
	}
     public enum tipo {
            USB,
            Serial,
            RS485,
            Modem,
            Red
        }

    public tipo TipoConexion;
    public int Puerto=0;
    public string Direccion="";
    public int NoTerminal=0;
    public int Velocidad=0;
        /// <summary>
    /// Recibe los datos de la direccion y para cada tipo regresa una cadena de direccion diferente con los datos
    /// Tipo de Direccion, Nombre, Puerto, Velocidad, ID, Telefono separados por dos puntos
    /// En Tipo de Direccion se usara: 1:USB 2:Serial 3:485 4:modem 5:Red
    /// Nombre, Telefono y Direccion se gardan en la variable direccion
    /// <returns></returns> Cadena Concatenada

    public string ObtenCadenaConexion()
    {
        string cadenafinal = "";
        switch (TipoConexion)
        {
            case tipo.USB: cadenafinal = "USB" + ":" + Direccion + ":" + NoTerminal.ToString();
                break;
            case tipo.Serial: cadenafinal = "Serial" + ":" + Puerto.ToString() + ":" + Velocidad.ToString() + ":" + NoTerminal.ToString();
                break;
            case tipo.RS485: cadenafinal = "RS485" + ":" + Puerto.ToString() + ":" + Velocidad.ToString() + ":" + NoTerminal.ToString();
                break;
            case tipo.Modem: cadenafinal = "Modem" + ":" + Direccion + ":" + Puerto.ToString() + ":" + Velocidad.ToString() + ":" + NoTerminal.ToString();
                break;
            case tipo.Red: cadenafinal = "Red" + ":" + Direccion + ":" + Puerto.ToString() + ":" + NoTerminal.ToString();
                break;
        }
        return cadenafinal;
    }
    /// <summary>
    /// Recibe la cadena concatenada y guarda los valores segun sea USB, RS485,MODEM,RED
    /// </summary>
    /// <param name="concatenada"></param>
    /// <returns></returns>
    public bool CargarCadenaConexion(string concatenada)
    {
        concatenada = concatenada.Trim();
        string[] ConjuntoDatos = concatenada.Split(new char[] { ':' });
//        TipoConexion = Convert.ToInt32(ConjuntoDatos[0]);
        try
        {
            switch (ConjuntoDatos[0])
            {

                case "USB":
                    {
                        TipoConexion = tipo.USB;
                        Direccion = ConjuntoDatos[1];
                        Puerto = -1;
                        NoTerminal = 0;
                        Velocidad = 0;
                        if (ConjuntoDatos.Length > 2)
                            NoTerminal = Convert.ToInt32(ConjuntoDatos[2]);
                    }
                    break;
                case "Serial":
                    {
                        TipoConexion = tipo.Serial;
                        Puerto = Convert.ToInt32(ConjuntoDatos[1]);
                        Velocidad = Convert.ToInt32(ConjuntoDatos[2]);
                        Direccion = "";
                        NoTerminal = 0;
                        if (ConjuntoDatos.Length > 3)
                            NoTerminal = Convert.ToInt32(ConjuntoDatos[3]);
                    }
                    break;
                case "RS485":
                    {
                        TipoConexion = tipo.RS485;
                        Puerto = Convert.ToInt32(ConjuntoDatos[1]);
                        Velocidad = Convert.ToInt32(ConjuntoDatos[2]);
                        NoTerminal = Convert.ToInt32(ConjuntoDatos[3]);
                        Direccion = "";

                    }
                    break;
                case "Modem":
                    {
                        TipoConexion = tipo.Modem;
                        Direccion = ConjuntoDatos[1];
                        Puerto = Convert.ToInt32(ConjuntoDatos[2]);
                        Velocidad = Convert.ToInt32(ConjuntoDatos[3]);
                        NoTerminal = 0;
                        if (ConjuntoDatos.Length > 4)
                            NoTerminal = Convert.ToInt32(ConjuntoDatos[4]);
                    }
                    break;
                case "Red":
                    {
                        TipoConexion = tipo.Red;
                        Direccion = ConjuntoDatos[1];
                        try
                        {
                            IPAddress.Parse(Direccion);
                        }
                        catch
                        {
                            try
                            {
                                IPHostEntry ipEntry = Dns.GetHostByName(Direccion);
                                IPAddress[] addr = ipEntry.AddressList;

                                for (int i = 0; i < addr.Length; i++)
                                {
                                    Direccion = addr[i].ToString();
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                
                            }
                        }
                        
                        Puerto = Convert.ToInt32(ConjuntoDatos[2]);
                        NoTerminal = 0;
                        Velocidad = -1;
                        if (ConjuntoDatos.Length > 3)
                            NoTerminal = Convert.ToInt32(ConjuntoDatos[3]);
                    }
                    break;
            }
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
        //
		// TODO: Agregar aquí la lógica del constructor
		//

}
	

    
   


