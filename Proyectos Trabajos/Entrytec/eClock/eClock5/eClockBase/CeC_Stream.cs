using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase
{
    public class CeC_Stream
    {
        public static CeC_Stream MetodoStream = null;

        public virtual string ObtenPath(string ArchivoNombre)
        {
            return ArchivoNombre;
        }
        public static string sObtenPath(string ArchivoNombre)
        {
            return MetodoStream.ObtenPath(ArchivoNombre);
        }

        public virtual System.IO.StreamWriter AgregaTexto(string ArchivoNombre)
        {
            return null;
        }
        public static System.IO.StreamWriter sAgregaTexto(string ArchivoNombre)
        {
            try
            {
                return MetodoStream.AgregaTexto(ArchivoNombre);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return null;
        }
        public virtual System.IO.StreamWriter NuevoTexto(string ArchivoNombre)
        {
            return null;
        }
        public static System.IO.StreamWriter sNuevoTexto(string ArchivoNombre)
        {
            try
            {
                return MetodoStream.NuevoTexto(ArchivoNombre);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return null;
        }
        public virtual System.IO.StreamReader LeerTexto(string ArchivoNombre)
        {
            return null;
        }


        public static System.IO.StreamReader sLeerTexto(string ArchivoNombre)
        {
            try
            {
                return MetodoStream.LeerTexto(ArchivoNombre);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return null;
        }

        public virtual bool ExisteArchivo(string ArchivoNombre)
        {
            return false;
        }
        public static bool sExisteArchivo(string ArchivoNombre)
        {
            return MetodoStream.ExisteArchivo(ArchivoNombre);
        }


        public virtual bool AgregaTexto(string ArchivoNombre, string Texto)
        {
            return false;
        }
        public static bool sAgregaTexto(string ArchivoNombre, string Texto)
        {
            try
            {
                return MetodoStream.AgregaTexto(ArchivoNombre, Texto);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return false;
        }
        public virtual bool NuevoTexto(string ArchivoNombre, string Texto)
        {
            return false;
        }
        public static bool sNuevoTexto(string ArchivoNombre, string Texto)
        {
            try
            {
                return MetodoStream.NuevoTexto(ArchivoNombre, Texto);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return false;
        }
        /// <summary>
        /// Este metodo se encarga de retornar un Objeto DateTime()
        /// </summary>
        /// <param name="ArchivoNombre">Por el momento no es Utilizado</param>
        /// <returns>Retornar un Objeto DateTime()</returns>
        public virtual DateTime FechaHoraModificacion(string ArchivoNombre)
        {
            return new DateTime();
        }
        /// <summary>
        /// Este metodo se encarga de verificar la ultima fecha de modificacion
        /// </summary>
        /// <param name="ArchivoNombre">No se utiliza para una funcion espesifica pero es requerido</param>
        /// <returns>Retornar un Objeto DateTime()</returns>
        public static DateTime sFechaHoraModificacion(string ArchivoNombre)
        {
            return MetodoStream.FechaHoraModificacion(ArchivoNombre);
        }
        public virtual string LeerString(string ArchivoNombre)
        {
            return null;
        }

        public static string sLeerString(string ArchivoNombre)
        {
            try
            {
                return MetodoStream.LeerString(ArchivoNombre);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return "";
        }
        /// <summary>
        /// Este se encargaria de realizar una compaacion y regresaria un objeto en byte
        /// </summary>
        /// <param name="ArchivoNombre">No se utiliza en la fincion pero sera requerido</param>
        /// <returns></returns>
        public virtual byte[] LeerBytes(string ArchivoNombre)
        {
            return null;
        }

        /// <summary>
        /// Este metodo se encargara de realizar una verificacion de la imagen
        /// </summary>
        /// <param name="ArchivoNombre">
        /// Este parametro por el momento no es utilizado pero es requerido
        /// </param>
        /// <returns>Por el momento solo retornara un nulo</returns>
        public static byte[] sLeerBytes(string ArchivoNombre)
        {
            try
            {
                return MetodoStream.LeerBytes(ArchivoNombre);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }

            return null;
        }

        /// <summary>
        /// Este metodo se encarga de verificar 
        /// </summary>
        /// <param name="ArchivoNombre"></param>
        /// <param name="Datos"></param>
        /// <returns>Por el momento solo retorna un false </returns>
        public virtual bool NuevoBytes(string ArchivoNombre, byte[] Datos)
        {
            return false;
        }

        /// <summary>
        /// Este metodo se encargara de verificar y realizar una comparacion entre el archivo guarado y la imagen obtenida 
        /// </summary>
        /// <param name="ArchivoNombre">Indica el nombre de el archivo a comparar</param>
        /// <param name="Datos"> Indica el arreglo de byte para la comparacion y verificar que sea el mas reciente</param>
        /// <returns>regresa un booleano flase ya que en no se realiza una operacion adicional en este momento</returns>
        public static bool sNuevoBytes(string ArchivoNombre, byte[] Datos)
        {
            try
            {
                return MetodoStream.NuevoBytes(ArchivoNombre, Datos);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }

            return false;
        }

        public static bool sEjecuta(string ArchivoNombre)
        {
            try
            {
                if (ArchivoNombre == null || ArchivoNombre == "")
                    return false;
                return MetodoStream.Ejecuta(ArchivoNombre);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }

            return false;
        }

        public virtual bool Ejecuta(string ArchivoNombre)
        {
            return true;
        }

    }
}
