using System;
using System.Threading;
using ThreadingNS.ExtensionNS;

/// <summary>
/// Foreground threads musí vždy skončit před tím, než skončí hlavní thread
/// Background threads neovlivňují ukončení programu, skončí v rozpracovaném stavu,
/// pokud už nedokončily svou práci
/// </summary>

namespace ThreadingNS
{
    namespace ExtensionNS
    {
        /// <summary>
        /// Extension třída rozšiřuje metody dostupné pro typ int,
        /// musí být statická
        /// </summary>
        public static class Extensions
        {
            public static bool IsPrime(this int i)
            {
                int d = 2;
                bool isP = true;
                while (d * d <= i)
                    if (i % d++ == 0)
                        return false;
                return isP;
            }
        }
    }

    class Program
    {
        // ParameterizedThreadStart vyžaduje funkci, která je void a přijímá 
        // jeden argument typu object
        static void GenerujPrvočísla(object od)
        {
            int i = (int) od;
            while (true)
            {
                // volání rozšiřené metody pro typ int
                if (i.IsPrime())
                {
                    Console.WriteLine("Prime " + i);
                    Thread.Sleep(20);
                }
                i++;
            }
        }

        static void Main()
        {
            Console.WriteLine(Extensions.IsPrime(20));
            Console.WriteLine(Extensions.IsPrime(23));

            Thread t1 = new Thread(
                    () =>
                    {
                        for (int i = 0; i < 18; i++)
                        {
                            if (i % 2 == 0)
                            {
                                Console.WriteLine(Thread.CurrentThread.Name + " : " + i);
                                Thread.Sleep(200);
                            }
                        }
                    }
            )
            {
                Name = "Sudé",
                // As the program finishes before the Sude thread finished, it will never finish
                IsBackground = true
            };


            Thread t2 = new Thread(
                    () =>
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (i % 2 != 0)
                            {
                                Console.WriteLine(Thread.CurrentThread.Name + " : " + i);
                                Thread.Sleep(200);
                            }
                        }
                        Console.WriteLine("Poslední liché už bylo, končíme.");
                    }
            )
            {
                Name = "Liché",
                // This counting will always finish, main thread will fait for foreground thread to end.
                IsBackground = false
            };

            ParameterizedThreadStart prvocisla = new ParameterizedThreadStart(GenerujPrvočísla);
            Thread t3 = new Thread(prvocisla);
            t3.IsBackground = true;
            
            t1.Start();
            t2.Start();
            // Generate primes in the background and start from 20
            t3.Start(20);

            Console.WriteLine("Hello World!");
            
        }
    }
}
