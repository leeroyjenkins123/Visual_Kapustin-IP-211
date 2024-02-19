using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal interface Oper
    {
        public double Conclusion(double n, double m) { return 0; }
        public double Conclusion(double n)
        {
            return n;
        }
    }

    internal class Floor : Oper
    {
        public double Conclusion(double n)
        {
            return Math.Floor(n);
        }
    }
    internal class Ceil : Oper
    {
        public double Conclusion(double n)
        {
            return Math.Ceiling(n);
        }
    }
    internal class Lg : Oper
    {
        public double Conclusion(double n)
        {
            return Math.Log10(n);
        }
    }
    internal class Ln : Oper
    {
        public double Conclusion(double n)
        {
            return Math.Log(n);
        }
    }
    internal class Sin : Oper
    {
        public double Conclusion(double n)
        {
            return Math.Sin(n);
        }
    }
    internal class Cos : Oper
    {
        public double Conclusion(double n)
        {
            return Math.Cos(n);
        }
    }
    internal class Tan : Oper
    {
        public double Conclusion(double n)
        {
            return Math.Tan(n);
        }
    }
    internal class Plus : Oper
    {
        public double Conclusion(double n, double m)
        {
            return n + m;
        }
    }
    internal class Minus : Oper
    {
        public double Conclusion(double n, double m)
        {
            return n - m;
        }
    }
    internal class Divide : Oper
    {
        public double Conclusion(double n, double m)
        {
            if (m == 0)
            {
                throw new DivideByZeroException();
            }
            return n / m;
        }
    }
    internal class Multipy : Oper
    {
        public double Conclusion(double n, double m)
        {
            return n * m;
        }
    }
    internal class Factorial : Oper
    {
        public double Conclusion(double n)
        {
            if (n > 15)
            {
                throw new ArgumentException();
            }
            if (n == 1 || n == 0)
            {
                return 1;
            }
            return n * Conclusion(n - 1);
        }
    }
    internal class Mod : Oper
    {
        public double Conclusion(double n, double m)
        {
            if (m == 0)
            {
                throw new DivideByZeroException();
            }
            return n % m;
        }
    }
    internal class Pow : Oper
    {
        public double Conclusion(double n, double m)
        {
            double value = Math.Pow(n, m);
            if (value > double.MaxValue)
            {
                throw new ArgumentException();
            }
            return value;
        }
    }
}