using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EratosteneParallel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 100_000_000;
            //EratosteneSequenziale(n);
            //EratosteneParalleloThread(n);
            EratosteneParalleloTask(n);
            EratosteneParallelFor(n);
            Console.ReadLine();
        }

        static void EratosteneSequenziale(int n)
        {
            bool[] numeri = new bool[n+1];
            for(int i = 0; i < numeri.Length;i++)
                numeri[i] = true;
            
            numeri[0] = false;
            numeri[1] = false;

            var sw = Stopwatch.StartNew();
            for(int i = 2; i<= Math.Sqrt(n); i++)
            {
                for (int j = i * 2; j <= n; j += i)
                    numeri[j] = false;
            }
            sw.Stop();
            Console.WriteLine($"Trovati {numeri.Count(num => num==true)} numeri primi in {sw.ElapsedMilliseconds}ms");
        }

        static void EratosteneParalleloThread(int n)
        {
            bool[] numeri = new bool[n + 1];
            for (int i = 0; i < numeri.Length; i++)
                numeri[i] = true;

            numeri[0] = false;
            numeri[1] = false;


            List<Thread> threads = new List<Thread>();

            var sw = Stopwatch.StartNew();
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                int lolcal_i = i;
                var t = new Thread(p =>
                {
                    for (int j = lolcal_i * 2; j <= n; j += lolcal_i)
                        numeri[j] = false;
                });
                t.Start();
                threads.Add(t);
            }
            threads.ForEach(t => t.Join());

            sw.Stop();
            Console.WriteLine($"Trovati {numeri.Count(num => num == true)} numeri primi in {sw.ElapsedMilliseconds}ms");
        }

        static void EratosteneParalleloTask(int n)
        {
            bool[] numeri = new bool[n + 1];
            for (int i = 0; i < numeri.Length; i++)
                numeri[i] = true;

            numeri[0] = false;
            numeri[1] = false;


            List<Task> tasks = new List<Task>();

            var sw = Stopwatch.StartNew();
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                int lolcal_i = i;
                var t = Task.Run(() =>
                {
                    for (int j = lolcal_i * 2; j <= n; j += lolcal_i)
                        numeri[j] = false;
                });
                tasks.Add(t);
            }
            Task.WaitAll(tasks.ToArray());

            sw.Stop();
            Console.WriteLine($"Trovati {numeri.Count(num => num == true)} numeri primi in {sw.ElapsedMilliseconds}ms");
        }

        static void EratosteneParallelFor(int n)
        {
            bool[] numeri = new bool[n + 1];
            for (int i = 0; i < numeri.Length; i++)
                numeri[i] = true;

            numeri[0] = false;
            numeri[1] = false;


            var sw = Stopwatch.StartNew();
            //for (int i = 2; i < Math.Sqrt(n)+1; i++)
            Parallel.For(2, (int)Math.Sqrt(n) + 1, i =>
            {
                for (int j = i * 2; j <= n; j += i)
                    numeri[j] = false;
            });

            sw.Stop();
            Console.WriteLine($"Trovati {numeri.Count(num => num == true)} numeri primi in {sw.ElapsedMilliseconds}ms");
        }
    }
}
