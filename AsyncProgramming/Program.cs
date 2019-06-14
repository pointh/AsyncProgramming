using System;
using System.Threading.Tasks;

namespace AsyncProgramming1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Task t = RunCountingAsync();

            Console.WriteLine("Hello World!");
            do
            {
                Console.WriteLine("I continue my thread.");
            } while (t.Wait(20000));

            Console.ReadLine();
        }

        static async Task RunCountingAsync()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.Write(i + " ");
                    System.Threading.Thread.Sleep(500);
                }
            });
        }
    }
}