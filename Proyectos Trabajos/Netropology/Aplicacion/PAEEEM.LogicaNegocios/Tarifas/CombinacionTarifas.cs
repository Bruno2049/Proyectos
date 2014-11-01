using System;
using System.Linq;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.Tarifas;
using PAEEEM.Entidades.Tarifas;

namespace PAEEEM.LogicaNegocios.Tarifas
{
    public class CombinacionTarifas
    {
        private readonly decimal _paramUnoFact = 0.0M;
        private readonly int _claveIva = 0;
        private readonly decimal _divisor = 0.0M;
        private readonly int _tipoFacturacion = 0;

        public CombinacionTarifas(int claveIva, int tipoFacturacion)
        {
            _claveIva = claveIva;
            _tipoFacturacion = tipoFacturacion;
            _divisor = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 2).VALOR);

            _paramUnoFact = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 10).VALOR);

        }



        #region PROCESO DE SUBESTACIONES ELECTRICAS

        
        
        public CompFacturacion Tarifa_OM(CompFacturacion comfacturacion)
        {            
            //OPERACIONES PARA LOS CONCEPTOS            
            var cptokWh = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1);//kWh
            var cptokW = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2);//kW 
            var cptoFPotencia = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4);//factor potencia
            var cptoBonificacion = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 5);//BONIFICACION
            var cptoPenalizacion = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 6);//PENALIZACION
            var cptoFact = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 18);//FACT
            

            cptokWh.Facturacion = Math.Round(cptokWh.CPromedioODemMax*cptokWh.CargoAdicional,4);
            cptokW.Facturacion = Math.Round(cptokW.CPromedioODemMax*cptokW.CargoAdicional,4);           

            //MONTO DE FACTURACION MENSUAL SIN IVA
            //VALIDACION DEL FACTOR DE POTENCIA SIEMPRE SERA 0.9000M
            var fact = Math.Round((cptokW.Facturacion + cptokWh.Facturacion) + ((_paramUnoFact) * (cptokW.Facturacion + cptokWh.Facturacion)), 2);
                      
                                    
            comfacturacion.ConceptosFacturacion.Clear();                                                                       

            comfacturacion.ConceptosFacturacion.Add(cptokWh);
            comfacturacion.ConceptosFacturacion.Add(cptokW);
            comfacturacion.ConceptosFacturacion.Add(cptoFPotencia);
            comfacturacion.ConceptosFacturacion.Add(cptoBonificacion);
            comfacturacion.ConceptosFacturacion.Add(cptoPenalizacion);
            comfacturacion.ConceptosFacturacion.Add(cptoFact);


            var subTotal = Math.Round(comfacturacion.ConceptosFacturacion.Sum(p => p.Facturacion),2);
            var iva = Math.Round(subTotal * (_claveIva/_divisor),2); //CLAVE IVA
            var total = Math.Round(subTotal + iva,2);
            var pagoFacturaBiOMe = Math.Round(total * Convert.ToInt32(_tipoFacturacion),2);//1 = FACTURACION           
            
            comfacturacion.Subtotal = subTotal;
            comfacturacion.Iva = iva;
            comfacturacion.Total = total;
            comfacturacion.PagoFactBiMen = pagoFacturaBiOMe;
            comfacturacion.MontoFactMensualSNIVA = fact;

            return comfacturacion;
        }


        public CompFacturacion Tarifa_HM(CompFacturacion facturacion)
        {            
            //OPERACIONES PARA LOS CONCEPTOS            
            var cptokWh = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1);//kWh
            var cptokW = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2);//kW 
            var cptoFPotencia = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4);//factor potencia
            var cptoBonificacion = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 5);//BONIFICACION
            var cptoPenalizacion = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 6);//PENALIZACION
            var cptoFact = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 18);//FACT


            cptokWh.Facturacion = Math.Round(cptokWh.CPromedioODemMax * cptokWh.CargoAdicional, 4);
            cptokW.Facturacion = Math.Round(cptokW.CPromedioODemMax * cptokW.CargoAdicional, 4);

            //MONTO DE FACTURACION MENSUAL SIN IVA
            //VALIDACION DEL FACTOR DE POTENCIA SIEMPRE SERA 0.9000M
            var fact = Math.Round(cptokW.Facturacion + cptokWh.Facturacion,2);

                      
            facturacion.ConceptosFacturacion.Clear();

            facturacion.ConceptosFacturacion.Add(cptokWh);
            facturacion.ConceptosFacturacion.Add(cptokW);
            facturacion.ConceptosFacturacion.Add(cptoFPotencia);
            facturacion.ConceptosFacturacion.Add(cptoBonificacion);
            facturacion.ConceptosFacturacion.Add(cptoPenalizacion);
            facturacion.ConceptosFacturacion.Add(cptoFact);

            var subTotal = Math.Round(facturacion.ConceptosFacturacion.Sum(p => p.Facturacion),2);
            var iva = Math.Round(subTotal * (_claveIva /_divisor),2); //CLAVE IVA
            var total = Math.Round(subTotal + iva,2);
            var pagoFacturaBiOMe = Math.Round(total * Convert.ToInt32(_tipoFacturacion),2);//1 = FACTURACION
           

            facturacion.Subtotal = subTotal;
            facturacion.Iva = iva;
            facturacion.Total = total;
            facturacion.PagoFactBiMen = pagoFacturaBiOMe;
            facturacion.MontoFactMensualSNIVA = fact;

            return facturacion;
        }

        #endregion



        #region PROCESO DE BANCO DE CAPACITORES        

        

        #endregion
    }
}
