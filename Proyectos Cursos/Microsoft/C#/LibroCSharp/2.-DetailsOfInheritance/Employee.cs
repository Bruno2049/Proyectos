using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._DetailsOfInheritance
{
    public abstract partial class Employee
    {
        #region Clases, enums, interface

        public enum BenefitPackageLevel
        {
            Standard, Bronce, Silver, Gold, Platinium
        }        

        #endregion

        #region Campos y Propiedades

        protected double EmpDeductions;

        public double Deductions
        {
            get 
            {
                ComputePayDeduction();
                return EmpDeductions;
            }
            set { EmpDeductions = value; }
        }

        protected BenefitPackageLevel EmpBenefitPack;

        public BenefitPackageLevel BenefitsPack
        {
            get { return EmpBenefitPack; }
            set { EmpBenefitPack = value; }
        }

        protected string EmpName;

        public string Name
        {
            get 
            {
                if (EmpName == null)
                {
                    EmpName = "(Sin Nombre)";
                }
                return EmpName;
            }
            set 
            {
                if (value.Length < 15)
                {
                    EmpName = value;
                }
                else 
                {
                    Console.WriteLine("The name is too long");
                }

            }
        }

        protected int EmpID;

        public int ID
        {
            get { return EmpID; }
            set { EmpID = value; }
        }

        protected double EmpCurrPay;

        public double CurrPay
        {
            get { return EmpCurrPay; }
            set { EmpCurrPay = value; }
        }

        protected int EmpAge;

        public int Age
        {
            get { return EmpAge; }
            set { EmpAge = value; }
        }
        #endregion

        #region Constructores
        public Employee()
        { 
        }

        public Employee(string name, int id, double pay)
        {
            EmpName = name;
            EmpID = id;
            EmpCurrPay = pay;
        }

        public Employee(string FullName, int EmpAge, int EmpID, double EmpCurrPay, string EmpSSN, int EmpNumbOfOpts)
        {
            ID = EmpID;
            Age = EmpAge;
            Name = FullName;
            CurrPay =  EmpCurrPay;
        }
        #endregion

        #region Metodos

        public virtual void GiveBonus(double amount)
        {
            EmpCurrPay += amount;
        }

        protected void ComputePayDeduction()
        {
            switch (EmpBenefitPack)
            {
                case BenefitPackageLevel.Standard:
                    EmpDeductions = 100;
                    break;

                case BenefitPackageLevel.Bronce:
                    EmpDeductions = 200;
                    break;

                case BenefitPackageLevel.Silver:
                    EmpDeductions = 300;
                    break;

                case BenefitPackageLevel.Gold:
                    EmpDeductions = 500;
                    break;

                case BenefitPackageLevel.Platinium:
                    EmpDeductions = 1000;
                    break;

                default:
                    EmpDeductions = 0;
                    break;
            }
        }

        public void GivePromotion()
        { 
            //increase pay
            //Give new parking space in company garage...

            Console.WriteLine("{0} was promoted! ", Name);
        }

        public virtual void DisplayStats()
        {
            Console.WriteLine("\n**************** Status **************** ");
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine("ID: {0}", ID);
            Console.WriteLine("Age: {0}", Age);
            Console.WriteLine("Pay: {0}", CurrPay);
            Console.WriteLine("PackBenefits: {0}", BenefitsPack);
            Console.WriteLine("Deducciones: {0}", Deductions);
        }
        #endregion
    }        
}
