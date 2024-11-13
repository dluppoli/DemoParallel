using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoParallel
{
    internal class Program
    {
        private static Stopwatch sw = new Stopwatch();
        private static List<TimeSpan> laps = new List<TimeSpan>();
        private static bool appRunning = true;
        static void Main(string[] args)
        {
            Thread ui = new Thread(updateUI);
            ui.Start();

            string scelta = "";
            while(scelta !="X")
            {
                scelta = Console.ReadLine();
                switch(scelta)
                {
                    case "S":
                        if( sw.IsRunning )
                            sw.Stop();
                        else
                            sw.Start();
                        break;
                    case "A":
                        if( !sw.IsRunning)
                        {
                            sw.Reset();
                            laps.Clear();
                        }
                        break;
                    case "G":
                        if( sw.IsRunning )
                            laps.Add(sw.Elapsed);
                        break;
                }
            }
            appRunning = false;
        }

        static void updateUI()
        {
            while (appRunning)
            {
                Console.Clear();
                if (sw.IsRunning)
                    Console.WriteLine("S-Stop    G-Giro     X-Exit");
                else
                    Console.WriteLine("S-Start   A-Azzera   X-Exit");
                Console.WriteLine();
                Console.WriteLine(sw.Elapsed);
                Console.WriteLine();

                TimeSpan? prev = null;
                foreach (var l in laps)
                {
                    if( prev == null )
                        Console.WriteLine(l);
                    else
                        Console.WriteLine(l - prev);

                    prev = l;
                }
                Thread.Sleep(100);
            }
        }
    }
}
