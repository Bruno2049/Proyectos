using System;
using PAEEEM.AccesoDatos.Catalogos;
using System.Globalization;


namespace PAEEEM.LogicaNegocios.Trama
{
    //CLASE QUE CONTENDRA TODAS LAS OPERACIONES DEL PARSEO DE LA TRAMA
    public class TipoOperacionesTrama
    {
        
        //OPERACION DE FACTURA QUE PUEDE SER MENSUAL O BIMESTRAL        
        public string GetFacturaValor(int inicio, int longitud, string trama) 
        {
            var valor = "";

            if (string.IsNullOrEmpty(trama))
                return valor;

            var contenido = trama.Substring(inicio - 1, longitud);

            if (!string.IsNullOrEmpty(contenido))
            {
                var numero = 0;
                var esNumero = int.TryParse(contenido,out numero);

                if (esNumero)
                {                   
                    if (numero == 1)
                    {
                        //PERIODO DE PAGO
                        var periodoPago = new PeriodoPago().ObtienePorCondicion(p => p.Cve_Periodo_Pago == 1);
                        valor = periodoPago.Dx_Ciclo;
                    }
                    else
                    {
                        //PERIODO DE PAGO
                        var periodoPago = new PeriodoPago().ObtienePorCondicion(p => p.Cve_Periodo_Pago == 2);
                        valor = periodoPago.Dx_Ciclo;
                    }                                                               
                }
            }

            return valor;
        }

        //OPERACION PARA OBTENER LA CLAVE DEL ESTADO        
        public string GetClaveEstadoValor(int inicio, int longitud, string trama) 
        {
            var valor = "";

            if (string.IsNullOrEmpty(trama))
                return valor;

            var contenido = trama.Substring(inicio - 1, longitud);

            if(!string.IsNullOrEmpty(contenido))
            {
                var estado = new Estado().ObtienePorCondicion(c => c.Dx_Cve_Trama == contenido);
                valor = estado.Dx_Nombre_Estado;
            }

            return valor;
        }

        //OPERACION PARA OBTENER LA CLAVE DEL IVA       
        public int GetClaveIvaValor(int inicio, int longitud, string trama) 
        {
            var valor = 0;

            if (string.IsNullOrEmpty(trama))
                return valor;

            var contenido = trama.Substring(inicio - 1, longitud);

            if (!string.IsNullOrEmpty(contenido))
            {

                if (contenido == "6")
                    valor = int.Parse(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 3 && p.IDSECCION == 1).VALOR);
                else
                    valor = int.Parse(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 4 && p.IDSECCION == 1).VALOR);

            }

            return valor;
        }

        //OPERACION PARA OBTENER LA TARIFA       
        public string GetTarifaValor(int inicio, int longitud, string trama) 
        {
            string valor = "";

            if (string.IsNullOrEmpty(trama))
                return valor;

            valor = trama.Substring(inicio - 1, longitud);
                                               
            return valor;
        }

        //OPERACION PARA OBTENER EL ESTATUS DEL USUARIO       
        public string GetEstatusUsuarioValor(int inicio, int longitud, string trama) 
        {
            var valor = "";


            if (string.IsNullOrEmpty(trama))
                return valor;

            var contenido = trama.Substring(inicio - 1, longitud);

            var estatus = new EstatusUsuario().ObtienePorCondicion(c=> c.ID_ESTATUS == contenido);
            if (estatus != null)
                valor = estatus.DESCRIPCION;

            return valor;
        }

        //OPERACION PARA OBTENER EL RPU        
        public string GetRpuValor(int inicio, int longitud, string trama) 
        {
            string valor = null;

            if (trama.Length == 0)
                return valor;

            valor = trama.Substring(inicio - 1, longitud);

            if (string.IsNullOrEmpty(valor))
                return null;
            else
                return valor;  //string.Format("{0: ### ### ### ###}", Convert.ToInt32(
            
        }
                             

        //OPERACION PARA OBTENER LA FECHA DE CONSUMO                
        public DateTime? GetFechaMesAnioValor(int inicio, int longitud, string trama) 
        {
            DateTime? valor = null;

            if (string.IsNullOrEmpty(trama))
                return valor;

            var contenido = trama.Substring(inicio - 1, longitud);

            if(!string.IsNullOrEmpty(contenido))
                if (contenido.Length >= 6)
                {
                    contenido = contenido.Insert(4, "/");
                    try
                    {
                        valor = Convert.ToDateTime(contenido);
                    }
                    catch (FormatException)
                    {
                        valor = null;
                    }
                    
                }

            return valor;
        }

        //OPERACION PARA CONVERTIR UNA CADENA QUE CONTIENE DIAMESANIO
        public DateTime? GetFechaDiaMesAnioValor(int inicio, int longitud, string trama)
        {
            DateTime? valor = null;

            if (string.IsNullOrEmpty(trama))
                return valor;

            var contenido = trama.Substring(inicio - 1, longitud);

            if (!string.IsNullOrEmpty(contenido))
                if (contenido.Length >= 8)
                {
                    contenido = contenido.Insert(4, "/").Insert(7,"/");

                    try
                    {
                        valor = Convert.ToDateTime(contenido);
                    }
                    catch (FormatException)
                    {
                        valor = null;                        
                    }
                }
            return valor;
        }


        //OPERACION PARA OBTENER EL PERIODO MES        
        public DateTime GetPeriodoMesValor(int inicioMes, int longitudMes, string anio, string trama)
        {
            var valor = new DateTime();

            if (string.IsNullOrEmpty(trama))
                return valor;


            var contenido = trama.Substring(inicioMes - 1, longitudMes);

            if (!string.IsNullOrEmpty(contenido))
            {
                var mes = new Mes().ObtienePorCondicion(c=> c.ABREVIACION == contenido);
                if(mes != null)
                    if (!string.IsNullOrEmpty(mes.ABREVIACION))
                    {
                        
                        if (!string.IsNullOrEmpty(anio))
                        {
                            var fecha = string.Format("{0}/{1}/{2}", mes.VALOR, "01",anio);
                            valor = Convert.ToDateTime(fecha, CultureInfo.InvariantCulture);
                        }
                    }

            }

            return valor;
        }

        //OPERACION PARA OBTENER EL PERIODO AÑO        
        public int GetOPeriodoAnioValor(int inicio, int longitud, string trama) 
        {          
            throw new Exception("NO REGRESA NINGUN VALOR");
        }

        //OPERACION PARA OBTENER EL FACTOR DE POTENCIA        
        public decimal GetFactorPotenciaValor(int inicio, int longitud, string trama) 
        {
            var valor = 0.0M;

            if (string.IsNullOrEmpty(trama))
                return valor;

            var contenido = trama.Substring(inicio - 1, longitud).TrimStart('0');

            if(!string.IsNullOrEmpty(contenido))
            {
                int dato = 0;
                bool esNumero = int.TryParse(contenido,out dato);

                if (esNumero)
                {
                    var parametro = new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 1);
                    valor = Convert.ToDecimal(dato)/Convert.ToDecimal(parametro.VALOR);
                }
            }


            return valor;
        }

        //OPERACION PARA OBTENER NO. DE ADEUDOS, KVArh, Consumo y Demanda
        public int GetCadenaNumerico(int inicio, int longitud, string trama) 
        {
             int valor = 0;
             bool esNumero = false;

            if (string.IsNullOrEmpty(trama))
                return valor;

            var contenido = trama.Substring(inicio - 1, longitud).TrimStart('0');

            if (!string.IsNullOrEmpty(contenido))
                esNumero = int.TryParse(contenido, out valor);

            if (!esNumero)
                valor = 0;

            return valor;
        }
        
        //OPERACION PARA OBTENER EL NO DE CUENTA
        public string GetNoCuenta(string noCuenta, string trama)
        {
            string valor = null;            

            if (string.IsNullOrEmpty(trama))
                return valor;

            var infoGen = new InformacionParseo().ObtienePorCondicion(info => info.ID_INFO == 166); 
            var contenido = trama.Substring(infoGen.INICIAL - 1, infoGen.LONGITUD);
            if (!string.IsNullOrEmpty(contenido))
            {
                var valorTz = contenido == "01"
                              ? new TipoZona().ObtienePorCondicion(tz => tz.IDZONA == 1)
                              : new TipoZona().ObtienePorCondicion(p => p.IDZONA == 2);
                valor = string.Format("{0} - {1}", noCuenta, valorTz.NOMBRE);
            }

            return valor;
        }        

        //OPERACION PARA OBTENER EL TIPO DE FACTURACION



        //OPERACION PARA OBTENER EL ORIGEN
        public string GetOrigen(string idOrigen)
        {
            string valor = null;

            if (!string.IsNullOrEmpty(idOrigen))
            {
                if (idOrigen == "01" || idOrigen == "1")
                    valor = new Origen().ObtienePorCondicion(p => p.ID_VALOR == 1).CAMPO;
                else
                    valor = new Origen().ObtienePorCondicion(p => p.ID_VALOR == 0).CAMPO;
            }

            return valor;
        }


        //OPERACION PARA OBTENER ESTATUS COMUNICACION SICOM
        public string GetEstatusCmnSicom(string claveStatusSicom)
        {
            string valor = null;

            if (string.IsNullOrEmpty(claveStatusSicom))
                return valor;

            valor = new ComunicacionSicom().ObtienePorCondicion(cs => cs.CLAVE_ESTATUS_COM == claveStatusSicom).DESCRIPCION;


            return valor;
        }


        //OPERACION PARA OBTENER LA CLAVE DEL MUNICIPIO
        public string GetMunicipio(string claveMunicipio)
        {
            string valor = null;

            if (string.IsNullOrEmpty(claveMunicipio))
                return valor;

            var delgMun = new DelegacionMunicipio().ObtienePorCondicion(p => p.Cve_Clave_Municipio == claveMunicipio);
            valor = string.Format("{0} - {1}", claveMunicipio.Remove(0, 1), delgMun == null ? "" : delgMun.Dx_Deleg_Municipio);

            return valor;
        }


    }
}
