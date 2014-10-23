using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._DetailsOfInheritance
{
    public class Manager : Employee
    {
        public Manager(string FullName, int EmpAge, int EmpID,double EmpCurrPay, string EmpSSN, int EmpNumbOfOpts) : base(FullName, EmpAge, EmpID, EmpCurrPay, EmpSSN, EmpNumbOfOpts)
        {
            EmpStockOptions = EmpNumbOfOpts;
            ID = EmpID;
            Age = EmpAge;
            Name = FullName;
            CurrPay =  EmpCurrPay;
        }
        protected int EmpStockOptions;

        public int StockOptions 
        {
            get
            {
                return EmpStockOptions;
            }
            set
            {
                EmpStockOptions = value;
            }
        }

        public override void GiveBonus(double amount)
        {
            Random r = new Random();
            EmpStockOptions += r.Next(500);
            base.GiveBonus(amount);
        }
    }
}
