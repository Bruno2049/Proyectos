using System;
using System.Collections.Generic;
using PAEEEM.Entidades.AltaBajaEquipos;


namespace PAEEEM.Entidades.Alta_Equipos
{
    [Serializable]
    public class EquipoBajaEficiencia
    {
        //public EquipoBajaEficiencia()
        //{
        //    //EquiposAltaEficiencia = new List<EquipoAltaEficiencia>();
        //}


        public int ID { get; set; }
        public int Cve_Grupo { get; set; }
        public int Cve_GrupoOrig { get; set; }
        public string Dx_Grupo { get; set; }
        public string Dx_GrupoOrig { get; set; }
        public int Cve_Tecnologia { get; set; }
        public string Dx_Tecnologia { get; set; }
        public int Ft_Tipo_Producto { get; set; }
        public string Dx_Tipo_Producto { get; set; }
        public int Cve_Consumo { get; set; }
        public string Dx_Consumo { get; set; }
        public int Cve_Unidad { get; set; }
        public string Dx_Unidad { get; set; }
        public decimal MontoChatarrizacion { get; set; }
        
        public string Tarifa { get; set; }        
        public int Cantidad { get; set; }

        public decimal AhorroConsumo { get; set; }
        public decimal AhorroDemanda { get; set; }
        //PROPIEDAD PARA DESCRIBIR INFORMACION DETALLADA DE LA TECNOLOGA
        public CompDetalleTecnologia DetalleTecnologia { get; set; }
    }

    [Serializable]
    public class EquipoAltaEficiencia
    {
        public int ID { get; set; }
        public int ID_Baja { get; set; }
        public int FtTipoProducto { get; set; }
        public int Cve_Grupo { get; set; }
        public string Dx_Grupo { get; set; }
        public int Cve_Tecnologia { get; set; }
        public string Dx_Tecnologia { get; set; }
        public int Cve_Marca { get; set; }
        public string Dx_Marca { get; set; }
        public int Cve_Modelo { get; set; }
        public string Dx_Modelo { get; set; }
        public string Dx_Sistema { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Distribuidor { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Importe_Total_Sin_IVA { get; set; }
        public decimal Importe_Total { get; set; }
        public decimal KwhAhorro { get; set; }
        public decimal KwAhorro { get; set; }
        public decimal No_Capacidad { get; set; }
        public double CapacidadAquipo { get; set; }

        public decimal MontoChatarrizacion { get; set; }
        public decimal MontoIncentivo { get; set; }
        public decimal Gasto_Instalacion { get; set; }
    }

    [Serializable]
    public class CapacidadesAltaBaja
    {
        public int Cve_Grupo { get; set; }
        public string Dx_Grupo { get; set; }
    }
}
