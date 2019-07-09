using System;
using System.Threading.Tasks;

namespace AsyncProgramming1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            const long veryLongNumber = 56546546543L;

            Task<int> t = RunCountingAsync(0, 50);
            Task<int> q = RunCountingAsync(51, 100);
            Task<bool> r = IsPrimeAsync(veryLongNumber);
            Task<bool> s = IsPrimeAsync(144L);

            Task<int>[] lstCounting = new Task<int>[] { t, q };
            Task<bool>[] lstPrimes = new Task<bool>[] { r, s };

            int i = 0;
            do
            {
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("\nI continue main thread {0}.", System.Threading.Thread.CurrentThread.ManagedThreadId);
            } while (++i < 20);

            Task.WaitAll(lstCounting);
            Task.WaitAll(lstPrimes);

            int sum = 0;
            foreach (Task<int> tsk in lstCounting)
                sum += tsk.Result;

            Console.WriteLine($"\nSoučet je {sum}");

            Console.WriteLine($"\n{veryLongNumber} je prvočíslo: {r.Result}");

            Console.ReadLine();
        }

        private static void Delay(int miliseconds)
        {
            System.Threading.Thread.Sleep(miliseconds);
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
                    Delay(200);
                    sum += i;
                }

            });
            return sum;
        }

        private static async Task<bool> IsPrimeAsync(long number)
        {
            bool result = true;
            await Task.Run(() =>
            {
                long d = 2;
                while (d * d <= number)
                {
                    if (number % d++ == 0)
                    {
                        result = false;
                        // d has been incremented, thus d-1
                        Console.WriteLine($"\n{number} je součinem {d - 1} a {number / (d - 1)}");
                    }
                }
            });
            return result;
        }
    }
}