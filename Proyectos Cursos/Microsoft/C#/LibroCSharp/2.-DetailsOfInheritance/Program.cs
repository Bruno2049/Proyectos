using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2._DetailsOfInheritance
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("***** Herencia de empleados ******\n");

            SalesPerson Fred = new SalesPerson();
            Fred.Age = 31;
            Fred.ID = 34;
            Fred.Name = "Fred";
            Fred.SalesNumber = 50;
            Fred.CurrPay = 4000;
            Fred.GiveBonus(200);
            Fred.SalesNumber = 500;
            Fred.BenefitsPack = Employee.BenefitPackageLevel.Bronce;
                        
            Fred.DisplayStats();

            Manager Chucky = new Manager("Chucky", 50, 92, 100000, "333-23-2322", 9000);
            Chucky.BenefitsPack = Employee.BenefitPackageLevel.Platinium;

            Chucky.DisplayStats();

            PTSalesPerson Emp = new PTSalesPerson();
            Emp.Name = "Adrian";
            Emp.ID = 32;
            Emp.Age = 43;
            Emp.CurrPay = 5000;
            Emp.GiveBonus(300);
            Emp.BenefitsPack = Employee.BenefitPackageLevel.Gold;

            Emp.DisplayStats();

            Console.WriteLine("***** Casting class example ******");
            object Frank = new Manager("Frank Zappa", 9, 3000, 40000, "111-11-1111", 5);
            Employee MoonUnit = new Manager("Frank Zappa", 9, 3000, 40000, "111-11-1111", 5);
            SalesPerson Jill = new PTSalesPerson();
            List<object> Casting = new List<object>();
            Casting.Add(Frank);
            Casting.Add(MoonUnit);
            Casting.Add(Jill);

            foreach (object Lista in Casting)
            {
                Console.WriteLine("EL objeto es de tipo: {0}",Lista.GetType().ToString());
            }

            Jill.GivePromotion();
            MoonUnit.GivePromotion();
            ((Manager)Frank).GivePromotion();

            //Comportaminto de Object
            Console.WriteLine("*****Comportaminto de Object******");
            Console.WriteLine("ToString: {0}", Jill.ToString());
            Console.WriteLine("Hash code: {0}", Jill.GetHashCode());
            Console.WriteLine("Type: {0}", Jill.GetType());

            SalesPerson p2 = Jill;
            object o = p2;

            if (o.Equals(Jill) && p2.Equals(o))
            {
                Console.WriteLine("Same instance!");
            }
        }
    }
}
