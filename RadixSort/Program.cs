using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RadixSort
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            uint[] numbers = new uint[5_000];
            Random rnd = new Random();
            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = (uint)rnd.Next(10_000_000);
            
            Stopwatch sw = Stopwatch.StartNew();
            uint[] results = await RadixSortAsync(numbers);
            sw.Stop();
            Console.WriteLine($"RadixSort in {sw.Elapsed}");

            sw.Reset();
            sw.Restart();
            var result2 = numbers.OrderBy(o=>o).ToArray();
            sw.Stop();
            Console.WriteLine($"Linq in {sw.Elapsed}");

            /*foreach (var r in results)
            {
                Console.WriteLine(r);
            }*/
            Console.ReadLine();
        }

        static Task<uint[]> RadixSortAsync(uint[] numbers, uint? digit=null)
        {
            return Task.Run(() =>
            {
                if (numbers.Length == 0) return numbers;

                /*bool tuttiUguali = true;
                var elemento = numbers[0];
                foreach(var number in numbers)
                {
                    if (number != elemento)
                    {
                        tuttiUguali = false;
                        break;
                    }
                }
                if(tuttiUguali) return numbers;*/

                if( numbers.Distinct().Count() == 1 ) return numbers;

                //Preparo i bucket
                List<uint>[] buckets = new List<uint>[10];
                for (int i = 0; i < buckets.Length; i++)
                    buckets[i] = new List<uint>();

                //Determinare il numero massimo di cifre
                if (digit == null)
                {
                    digit = 0;
                    foreach (var n in numbers)
                    {
                        uint cifre = (uint)Math.Floor(Math.Log10(n));
                        if (cifre > digit) digit = cifre;
                    }
                }

                //Distribuisco i numeri nei bucket considerando la cifra digit
                foreach (var n in numbers)
                {
                    uint cifra = (n / (uint)Math.Pow(10, (double)digit)) % 10;
                    buckets[cifra].Add(n);
                }

                List<uint> results = new List<uint>();
                if (digit > 0)
                {
                    List<Task<uint[]>> tasks = new List<Task<uint[]>>();
                    foreach (var bucket in buckets.Where(w => w.Count() > 0))
                    {
                        tasks.Add(RadixSortAsync(bucket.ToArray(), digit - 1));
                    }
                    Task.WaitAll(tasks.ToArray());

                    foreach (var task in tasks)
                    {
                        results.AddRange(task.Result);
                    }
                }
                else
                {
                    foreach(var bucket in buckets)
                        results.AddRange(bucket);
                }
                return results.ToArray();
            });
        }
    }
}
