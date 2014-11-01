using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.CambioTarifa
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var proceso = new ProcesoCambioTarifa();
                proceso.ValidaCreditosCambioTarifa();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            
        }
    }
}
