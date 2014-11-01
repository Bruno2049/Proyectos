using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Terminales
{
    public class TerminalDir
    {


        public TerminalDir()
        {
        }
        public enum tipo
        {
            USB,
            Serial,
            RS485,
            Modem,
            Red
        }

        public tipo TipoConexion;
        public int Puerto = 0;
        public string Direccion = "";
        public string NoTerminal = "";
        public int Velocidad = 0;
        public string Clave = "";
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
                case tipo.USB: cadenafinal = "USB" + ":" + Direccion + ":" + NoTerminal + ":" + Clave;
                    break;
                case tipo.Serial: cadenafinal = "Serial" + ":" + Puerto.ToString() + ":" + Velocidad.ToString() + ":" + NoTerminal + ":" + Clave;
                    break;
                case tipo.RS485: cadenafinal = "RS485" + ":" + Puerto.ToString() + ":" + Velocidad.ToString() + ":" + NoTerminal + ":" + Clave;
                    break;
                case tipo.Modem: cadenafinal = "Modem" + ":" + Direccion + ":" + Puerto.ToString() + ":" + Velocidad.ToString() + ":" + NoTerminal + ":" + Clave;
                    break;
                case tipo.Red: cadenafinal = "Red" + ":" + Direccion + ":" + Puerto.ToString() + ":" + NoTerminal + ":" + Clave;
                    break;
            }
            return cadenafinal;
        }
        /// <summary>
        /// Recibe la cadena concatenada y guarda los valores segun sea USB, RS485,MODEM,RED
        /// </summary>
        /// <param name="CadenaConexion"></param>
        /// <returns></returns>
        public bool CargarCadenaConexion(string CadenaConexion)
        {
            
            //        TipoConexion = Convert.ToInt32(ConjuntoDatos[0]);
            try
            {
                CadenaConexion = CadenaConexion.Trim();
                switch (CeC.ObtenColumnaSeparador(CadenaConexion, ":", 0))
                {

                    case "USB":
                        {
                            TipoConexion = tipo.USB;
                            
                            Puerto = -1;
                            Direccion = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 1);
                            NoTerminal = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 2);
                            Clave = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 3);
                            Velocidad = 0;
                        }
                        break;
                    case "Serial":
                        {
                            TipoConexion = tipo.Serial;
                            Direccion = "";
                            Puerto = CeC.Convierte2Int( CeC.ObtenColumnaSeparador(CadenaConexion, ":", 1));
                            Velocidad = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(CadenaConexion, ":", 2));
                            NoTerminal = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 3);
                            Clave = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 4);
                        }
                        break;
                    case "RS485":
                        {
                            TipoConexion = tipo.RS485;
                            Direccion = "";
                            Puerto = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(CadenaConexion, ":", 1));
                            Velocidad = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(CadenaConexion, ":", 2));
                            NoTerminal = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 3);
                            Clave = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 4);

                        }
                        break;
                    case "Modem":
                        {
                            TipoConexion = tipo.Modem;

                            Direccion = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 1);
                            Puerto = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(CadenaConexion, ":", 2));
                            Velocidad = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(CadenaConexion, ":", 3));
                            NoTerminal = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 4);
                            Clave = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 5);
                        }
                        break;
                    case "Red":
                        {
                            TipoConexion = tipo.Red;
                            Velocidad = -1;
                            Direccion = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 1);
                            Puerto = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(CadenaConexion, ":", 2));                            
                            NoTerminal = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 3);
                            Clave = CeC.ObtenColumnaSeparador(CadenaConexion, ":", 4);
                        }
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}