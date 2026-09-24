using Spectre.Console;
using System.Diagnostics;

namespace Coding.Tracker
{
    internal class Program
    {
        private static CodeSessionService service;
        private static CodeSessionController controller;
        static void Main(string[] args)
        {
            service = new CodeSessionService();
            service.CreateDatabase();
            controller = new CodeSessionController(service);

            StartSession();

            ReadAllData();
        }

        public static void ReadAllData()
        {
            var reads = service.ReadAllData();
            foreach (var r in reads)
                Console.WriteLine(r.ToString());
        }

        public static void StartSession()
        {
            Console.WriteLine("To start session press any key...");
            Console.ReadKey();

            controller.StartSession();

            Console.Clear();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var lastSecond = 0d;
            while (!Console.KeyAvailable)
            {
                var currentSecond = stopwatch.Elapsed.TotalSeconds;        
                if (currentSecond != lastSecond)
                {
                    lastSecond = currentSecond;
                    var elapsed = stopwatch.Elapsed;
                    Console.Write($"\rElapsed: {(int)elapsed.TotalHours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}. Press any key to stop");
                }             
            }
            Console.ReadKey(true);
            Console.WriteLine();
            stopwatch.Stop();
            controller.StopSession();
        }
    }
}
