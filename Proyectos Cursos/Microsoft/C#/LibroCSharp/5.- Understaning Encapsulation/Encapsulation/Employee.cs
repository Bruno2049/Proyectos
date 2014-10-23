using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation
{
    class Employee
    {
        private string EmpName;
        private float CurrPay;
        private int EmpID;
        //Accessor (get method)
        public string GetName()
        {
            return EmpName;
        }

        //Mutator (set method)
        public void SetName(string name)
        {
            if (name.Length < 16)
            {
                this.EmpName = name;
            }
            else
            {
                Console.WriteLine("El Nombre es demaciado largo");
            }
        }

        private string Name
        {
            get 
            {
                return EmpName;
            }
            set 
            {
                if (value.Length > 16)
                    Console.WriteLine("El Nombre es demaciado largo");

                this.EmpName = value;
            }
        }        

        public Employee()
        { 
        }

        public Employee(string name, int id, float pay)
        {
            this.EmpName = name;
            this.EmpID = id;
            this.CurrPay = pay;
        }

        public void GiveBonus(float amount)
        {
            CurrPay += amount;
        }

        public void DisplayStats()
        {
            Console.WriteLine("Name: {0}", EmpName);
            Console.WriteLine("ID: {0}", EmpName);
            Console.WriteLine("Pay: {0}", CurrPay);
        }
    }
}
