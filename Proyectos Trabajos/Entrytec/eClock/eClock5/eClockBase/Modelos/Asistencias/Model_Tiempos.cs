using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Asistencias
{
    public class Model_Tiempos
    {
        public int SegundosEsperados { get; set; }
        public int SegundosEstancia { get; set; }
        public int SegundosTrabajados { get; set; }
        public int SegundosComida { get; set; }
        public int SegundosDeuda { get; set; }
        public int SegundosHESis { get; set; }
        public int SegundosHECal { get; set; }
        public int SegundosHeApl { get; set; }
        public decimal HorasSimples { get; set; }
        public decimal HorasDobles { get; set; }
        public decimal HorasTriples { get; set; }
        public Model_Tiempos()
        {
        }
        public Model_Tiempos(int iSegundosEsperados, int iSegundosEstancia, int iSegundosTrabajados, int iSegundosComida, int iSegundosDeuda,
            int iSegundosHESis, int iSegundosHECal, int iSegundosHeApl,
            decimal dHorasSimples, decimal dHorasDobles, decimal dHorasTriples)
        {
            SegundosEsperados = iSegundosEsperados;
            SegundosEstancia = iSegundosEstancia;
            SegundosTrabajados = iSegundosTrabajados;
            SegundosComida = iSegundosComida;
            SegundosDeuda = iSegundosDeuda;
            
            SegundosHESis = iSegundosHESis;
            SegundosHECal = iSegundosHECal;
            SegundosHeApl = iSegundosHeApl;
            HorasSimples = dHorasSimples;
            HorasDobles = dHorasDobles;
            HorasTriples = dHorasTriples;
        }
    }
}
