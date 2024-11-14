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

            uint range = n / tasks;
            List<Task<ulong>> tasklist = new List<Task<ulong>>();

            for (uint i = 0; i< tasks; i++)
            {
                uint local_i = i;
                tasklist.Add(
                    Task.Run( () => FattorialeParziale( range* local_i + 1, (local_i + 1)*range ) )
                );
            }
            if( n % tasks!=0)
                tasklist.Add(
                    Task.Run(() => FattorialeParziale(tasks*range + 1, n))
                );
            
            Task.WaitAll(tasklist.ToArray());

            ulong risultato = 1;
            tasklist.ForEach(t => risultato*=t.Result);

            Console.WriteLine(risultato);

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
