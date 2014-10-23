using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._DetailsOfInheritance
{
    public class SalesPerson : Employee
    {
        protected int EmpSalesNumber;
        
        public int SalesNumber 
        { 
            get { return EmpSalesNumber; } 
            set { EmpSalesNumber = value; }
        }

        public sealed override void GiveBonus(double amount)
        {
            int salesBonus = 0;
            if (EmpSalesNumber >= 0 && EmpSalesNumber <= 100)
                salesBonus = 10;
            else
            {
                if (EmpSalesNumber >= 101 && EmpSalesNumber <= 200)
                    salesBonus = 15;
                else
                    salesBonus = 20;
            }
            base.GiveBonus(amount * salesBonus);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void DisplayStats()
        {            
            base.DisplayStats();
            Console.WriteLine("The Sales Number: {0}",SalesNumber);
        }
    }
}
