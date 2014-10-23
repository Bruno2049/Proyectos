using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_CustomException
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Simple Exception Example *****");
            Console.WriteLine("=> Creating a car and stepping on it!");
            Car myCar = new Car("Zippy", 20);
            myCar.CrankTunes(true);
            try
            {
                for (int i = 0; i < 10; i++)
                    myCar.Accelerate(10);

            }
            catch (CarIsDeadException ex)
            {
                ex.HelpLink = "http://www.CarsRUs.com";
                // Stuff in custom data regarding the error.
                ex.Data.Add(" TimeStamp ",
                string.Format(" The car exploded at {0} ", DateTime.Now));
                ex.Data.Add(" Cause ", " You have a lead foot.");
                CrearLog Log = new CrearLog();
                Log.GuardaError(TipoMensage.Excepcion, "Error en el metodo Accelerate ya que excedio el limie de velocidad", ex);
            }
            Console.ReadLine();
        }
    }
}
