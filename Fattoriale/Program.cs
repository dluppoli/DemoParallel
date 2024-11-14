using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattoriale
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint n = 10; //3628800
            uint tasks = 3;


            Console.WriteLine(FattorialeSincrono(n));

            ulong r1 = FattorialeParziale(1, 5);
            ulong r2 = FattorialeParziale(6, n);
            Console.WriteLine(r1*r2);

            Console.ReadLine();
        }

        static ulong FattorialeParziale(uint start, uint stop)
        {
            uint risultato = 1;
            for (uint i = start; i <= stop; i++)
                risultato *= i;

            return risultato;
        }


        static ulong FattorialeSincrono(uint n)
        {
            uint risultato = 1;
            for (uint i = 2; i <= n; i++)
                risultato *= i;

            return risultato;   
        }
    }
}
