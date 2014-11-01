using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    public class LayOutExcel
    {
        // Campos comunes para todos los Layouts
        public string NoRegistro { get; set; }
        public string Fabricante { get; set; }
        public string TipoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string PrecioMaximo { get; set; }
        public string Capacidad { get; set; }

        // Campos add para subEstatciones

        public string RelacionTrans { get; set; }
        public string Precio { get; set; }
        public string PrecioTotalModTransformador { get; set; }
        public string PrecioConectorT { get; set; }
        public string PrecioAisladorTension { get; set; }
        public string PrecioAisladorSoporte { get; set; }
        public string PrecioCabGuiaApatarayos { get; set; }
        public string PrecioCabGuiaCortaCto { get; set; }
        public string PrecioCabGuiaTransform { get; set; }
        public string PrecioApartarrayos { get; set; }
        public string PrecioCortaCto { get; set; }
        public string PrecioFusible { get; set; }
        public string PrecioTotalModMediaTension { get; set; }
        public string PrecioCabTREqMT { get; set; }
        public string PrecioHerrajes { get; set; }
        public string PrecioPoste { get; set; }
        public string PrecioSistemaTierra { get; set; }
        public string PrecioCabConST { get; set; }
        public string PrecioTotalModAcceYProt { get; set; }
        public string PrecioTotalSubestacion { get; set; }
        
        // Con Acometida
        public string PrecioFusibleMT { get; set; }
        public string PrecioCabGuiaConAcometida { get; set; }
        public string PrecioConexMTAcometida { get; set; }

        // Integrarse a la Red
        public string PrecioEmpalmes { get; set; }
        public string PrecioExtremos { get; set; }

        //Banco de capacitores
        public string TipoEncapsulado { get; set; }
        public string TipoDielectrico { get; set; }
        public string IncluyeProteccionInterna { get; set; }
        public string TipoProteccionExterna { get; set; }
        public string MaterialCubierta { get; set; }
        public string Perdidas { get; set; }
        public string ProteccionInternaSobrecorriente { get; set; }
        public string ProteccionVsFuego { get; set; }
        public string ProteccionVsExplosion { get; set; }
        public string Anclaje { get; set; }
        public string TerminalTierra { get; set; }
        public string TipoControlador { get; set; }
        public string ProteccionVsSobreCorriente { get; set; }
        public string ProteccionVsSobreTemperatura { get; set; }
        public string ProteccionVsSobreVoltaje { get; set; }
        public string BloqueoDisplay { get; set; }
        public string TipoComunicacion { get; set; }
        public string ProteccionGabiente { get; set; }










      
      
      
        
        
        
        

    }
}
