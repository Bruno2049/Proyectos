using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encapsulation
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("*****  Class Types *****\n");

            Car myCar = new Car();
            myCar.petName = "Henry";
            myCar.currSpeed = 10;

            for (int i = 0; i <= 10; i++)
            {
                myCar.SpeedUp(5);
                myCar.PrintState();
            }

            Console.WriteLine("*****  Class Types *****\n");

            // Make a Car called Chuck going 10 MPH.
            Car chuck = new Car();
            chuck.PrintState();
            // Make a Car called Mary going 0 MPH.
            Car mary = new Car("Mary");
            mary.PrintState();
            // Make a Car called Daisy going 75 MPH.
            Car daisy = new Car("Daisy", 75);
            daisy.PrintState();
            Console.ReadLine();

            Console.WriteLine("*****  Class Constructors *****\n");
            Motorcycle mc = new Motorcycle();
            mc.PopAWheely();

            Console.WriteLine("*****  Rules of Encaptulation *****\n");
            Auto Viper = new Auto();
            Viper.TurnOnRadio(false);

            Console.WriteLine("***** Encaptulation rules *****");
            Employee Emp = new Employee("Anan", 4563, 24);            
            //Error: Emp.Name = ""; Es un error ya que el atributo es privado y viola la regla de encapsulamiento
            //Correcto:

            Emp.SetName("Lilian");
            Console.WriteLine("El nombre de el empleado es: {0}", Emp.GetName());
            Emp.GiveBonus(234);

            Emp.DisplayStats();

        }
    }
}
