using System;
using System.Threading.Tasks;

namespace AsyncProgramming1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Task t = RunCountingAsync();
            int i = 0;
            Console.WriteLine("Hello World!");
            do { 
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("\nI continue main thread {0}.", System.Threading.Thread.CurrentThread.ManagedThreadId);
            } while (++i < 20);

            t.Wait(20000);
            Console.ReadLine();
        }

        static async Task RunCountingAsync()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.Write("\n{0}, side thread {1}", i, System.Threading.Thread.CurrentThread.ManagedThreadId);
                    System.Threading.Thread.Sleep(200);
                }
            });
        }
    }
}