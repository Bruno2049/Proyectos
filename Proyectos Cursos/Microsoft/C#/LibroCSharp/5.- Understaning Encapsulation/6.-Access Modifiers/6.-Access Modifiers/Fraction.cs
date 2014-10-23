using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._Access_Modifiers
{
    class Fraction
    {
        int num, den;

        public Fraction (int NUM, int DEN)
        {
            this.num=NUM;
            this.den=DEN;
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction(a.num * b.den + b.num * a.den, a.den * b.den);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(a.num * b.num, a.den * b.den);
        }
        public static implicit operator double(Fraction f)
        {
            return (double)f.num / f.den;
        }
    }
}
