#define DOUBLE

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PARALELINIS
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            var random = new Random();
            int Min = 0;
            int Max = 1000;
            Random randNum = new Random();
            int[] numberArray = Enumerable.Repeat(0, 300).Select(i => randNum.Next(Min, Max)).ToArray(); // uzpildo duomenimis

            Console.WriteLine("Elementu skaicius masyve: {0}, didziausias masyvo elementas: {1}", numberArray.Length, numberArray.Max());

            // MTA testas
            stopWatch.Start();
            methodToAnalysis(numberArray);
            stopWatch.Stop();
            Console.WriteLine("Time in milliseconds for methodToAnalysis: {0} ticks", stopWatch.ElapsedTicks.ToString());
            stopWatch.Reset();
            
            // MTAP testas
            stopWatch.Start();
            methodToAnalysisParallel(numberArray);
            stopWatch.Stop();
            Console.WriteLine("Time in milliseconds for methodToAnalysisParallel: {0} ticks", stopWatch.ElapsedTicks.ToString());
            stopWatch.Reset();

            // MTA2 testas
            stopWatch.Start();
            methodToAnalysis2(numberArray.Length, numberArray);
            stopWatch.Stop();
            Console.WriteLine("Time in milliseconds for methodToAnalysis2: {0} ticks", stopWatch.ElapsedTicks.ToString());
            stopWatch.Reset();
            
            // MTA2P testas
            stopWatch.Start();
            methodToAnalysis2Parallel(numberArray.Length, numberArray);
            stopWatch.Stop();
            Console.WriteLine("Time in milliseconds for methodToAnalysis2Parallel: {0} ticks", stopWatch.ElapsedTicks.ToString());
        }


        public static long methodToAnalysis(int[] arr)
        {
            long n = arr.Length;
            long k = n;

            for (int i = 0; i < n; i++)
            {
                if (arr[i] > 0)
                {
                    for (int j = 0; j < n * n / 2; j++)
                    {
                        k -= 2;
                    }
                }
            }
            return k;
        }

        public static long methodToAnalysisParallel(int[] arr)
        {
            object monitor = new object();
            int total_sum = arr.Length;
            long n = arr.Length;

            Parallel.For(0, arr.Length, i =>
            {
                if (arr[i] > 0)
                {
                     Parallel.For(0, n * n / 2, j =>
			        {
				        int aa = 2;
				        lock (monitor) total_sum -= aa;
	
			        });
                }
            });
            
            return total_sum;
        }

        public static long methodToAnalysis2 (int n, int[] arr)
        {
            long k = 0;
            for (int i = 0; i < n; i++)
            {
                k += k;
                k += FF9(i, arr);
            }
            k += FF9(n, arr);
            k += FF9(n/2, arr);
            return k;
        }

        public static long methodToAnalysis2Parallel (int n, int[] arr)
        {
            object monitor = new object();
            long k = 0;

            Parallel.For(0, n, i =>
	        {
                k += k;
                k += FF9(i, arr);
            });

            k += FF9(n, arr);
            k += FF9(n/2, arr);
            return k;
        }

        public static long FF9(int n, int[] arr)
        {
            if (n > 1 && arr.Length > n && arr[0] < 0)
            {
                return FF9(n - 2, arr) + FF9(n - 1, arr) + FF9(n / n, arr);
            }
            return n;
        }

    }
}
