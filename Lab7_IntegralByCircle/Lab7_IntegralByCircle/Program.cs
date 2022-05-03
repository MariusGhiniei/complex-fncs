using System;
using System.Numerics;
using System.Threading.Channels;

namespace Lab7_IntegralByCircle // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public static double factorial(int x)
        {
            if (x == 0) return 1;
            double fact = 1;
            for (int i = 1; i <= x; i++)
                fact *= i;
            return 1.0/fact;
        }
        public static myComplex taylor_exp(myComplex z)
        {
            double theta = Math.Atan2(z.im,z.re);
            myComplex sum = new myComplex(0, 0);

            for (int i = 0; i <= 1000; i++)
            {
                double res = Math.Pow(theta, i) * factorial(i);
                if (i % 4 == 0)
                    sum.re += res;
                else if (i % 4 == 1)
                    sum.im += res;
                else if (i % 4 == 2)
                    sum.re -= res;
                else if (i % 4 == 3)
                    sum.im -= res;
            }
            return sum;
        }
        
        static void Main(string[] args)
        {
            myComplex unit = new myComplex(0, 1);
            myComplex negUnit = new myComplex(0, -1);
            myComplex z = new myComplex(3, -2);
            Console.WriteLine("Valoarea exponentialei cu metoda lui Euler: {0} + {1}i",
                z.Exp().Re,z.Exp().Im);
            Console.WriteLine("Valoarea exponentialei cu seria Taylor: {0} + {1}i",
                taylor_exp(z).Re, 
                taylor_exp(z).Im);
            Console.WriteLine("Valoarea sinsului este: {0} + {1}i",
                myComplex.sin(unit*z.Theta).Re, myComplex.sin(negUnit*z.Theta).Im);
            Console.WriteLine("Valoarea cosinusului este: {0} + {1}i",
                myComplex.cos(unit*z.Theta).re, myComplex.cos(negUnit*z.Theta).im);

        }
    }
}