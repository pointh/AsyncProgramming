using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncProgramming1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Task<int> t = RunCountingAsync(0, 50);
            Task<int> q = RunCountingAsync(51, 100);

            Task[] lst = new Task[] { t, q };

            int i = 0;
            do { 
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("\nI continue main thread {0}.", System.Threading.Thread.CurrentThread.ManagedThreadId);
            } while (++i < 20);

            Task.WaitAll(lst);

            int sum = 0;
            foreach (Task<int> tsk in lst)
                sum += tsk.Result;

            Console.WriteLine($"\nSoučet je {sum}");
            
            Console.ReadLine();
        }

        static async Task<int> RunCountingAsync(int k, int l)
        {
            int sum = 0;

            await Task.Run(() =>
            {
                for (int i = k; i <= l; i++)
                {
                    Console.Write("\n{0}, side thread {1}, pracuji na součtu od {2} do {3}", 
                        i, System.Threading.Thread.CurrentThread.ManagedThreadId, k, l);
                    System.Threading.Thread.Sleep(200);
                    sum += i;
                }
                
            });
            return sum;
        }
    }
}