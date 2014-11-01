using System;
using PAEEEM.Helpers.Circulo;
using System.Web.Services.Protocols;

namespace PAEEEM.Helpers
{
    public class CCHelper
    {
        ctPersona Persona = new ctPersona();
        ctConsulta Consulta = new ctConsulta();
        ctResultado Resultado = new ctResultado();
        ctUsuarioCC Usuario = new ctUsuarioCC();

        #region Properties IN
        // public string ClaveOtorgante { set { Usuario.ClaveOtorgante = value; } }
        // public string NombreUsuario { set { Usuario.NombreUsuario = value; } }
        // public string Password { set { Usuario.Password = value; } }

        public string Nombres { set { Persona.Nombres = value; } }
        public string ApellidoPaterno { set { Persona.ApellidoPaterno = value; } }
        public string ApellidoMaterno { set { Persona.ApellidoMaterno = value; } }
        public string RFC { set { Persona.RFC = value; } }
        public DateTime FechaNacimiento { set { Persona.FechaNacimiento = value; } }
        public string Sexo { set { Persona.Sexo = value; } } // ??

        public string Calle { set { Persona.Domicilio.Calle = value; } }
        public string Numero { set { Persona.Domicilio.Numero = value; } }
        public string NumeroInterior { set { Persona.Domicilio.NumeroInterior = value; } }
        public string ColoniaPoblacion { set { Persona.Domicilio.ColoniaPoblacion = value; } }
        public string CP { set { Persona.Domicilio.CP = value; } }
        public string DelegacionMunicipio { set { Persona.Domicilio.DelegacionMunicipio = value; } }
        public string Ciudad { set { Persona.Domicilio.Ciudad = value; } }
        public string Estado { set { Persona.Domicilio.Estado = value; } }

        public float ImporteContrato { set { Consulta.ImporteContrato = value; } }
        public string NumeroFirma
        {
            set
            {
                Consulta.FolioConsultaOtorgante = "Folio" + value;
                Consulta.NumeroFirma = value;
            }
        }
        #endregion

        #region properties out
        public string folio { get; set; }
        #endregion

        public CCHelper()
        {
            Persona.CURP = string.Empty;
            Persona.Domicilio = new ctDomicilio();
            Persona.Domicilio.Estado = "CODIGOESTADO";

            Usuario.ClaveOtorgante  = "0001251020";//"0001251020"; // "0001251020"; // <add key="Cir_Otorgante" value="0001251020"/>
            Usuario.NombreUsuario ="FGA4306FID"; //"CGR7791FID"; // "CGR7791FID"; // <add key="Cir_Usuario" value="FGA4306FID"/>
            Usuario.Password = "4306FFICAG";//"7791CFICRG"; // "7791CFICRG"; // <add key="Cir_Pass" value="4306FFICAG"/>
        }

        public string ConsultaCirculo()
        {
            wsCC log = new wsCC();
            string mop;

            try
            {
                log.Timeout = 30000; // RSA timeout in milliseconds
                Resultado = log.ConsultarMOP(Usuario, Consulta, Persona);
                folio = Resultado.folio;   
                mop = Resultado.MOP;
            }
            catch (SoapException ex)
            {
                if (ex.Message == "Respuesta incorrecta de circulo de crédito, intente más tarde. Respuesta incorrecta de circulo de crédito, intente más tarde. Elemento cuentas vacio.")
                {
                    folio = "0000001";
                    mop = "0";
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la consulta Web a Circulo de crédito.", ex);
            }
            finally
            {
                log.Abort();
                log.Dispose();
                log = null;
            }

            return mop;
        }
    }
}
