using System.Diagnostics;

namespace ALG_L2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "Data.csv";
            Stopwatch sw = new Stopwatch();

            int[] arr=Read(path);

            for(int i=50; i<=300;i+=50)
            {
                sw.Start();
                Console.WriteLine(methodToAnalysis(arr, i));
                sw.Stop();
                Console.WriteLine("Testo trukme, su {0} duomenim:" + sw.ElapsedTicks + " ticks", i);
                sw.Reset();
            }
            
        }

         static int[] Read(string path) 
        {
            int[] arr = new int[300];

            string[] lines = File.ReadAllLines(path);
            foreach(string line in lines)
            {
                string[] bit = line.Split(';');
                for(int i = 0; i < bit.Length; i++) 
                {
                    arr[i]=int.Parse(bit[i]);
                   
                }
            }
            return arr;
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

        public static long methodToAnalysis2(int n, int[] arr)
        {
            long k = 0;
            for (int i = 0; i < n; i++)
            {
                k += k;
                k += FF9(i, arr);
            }
            k += FF9(n, arr);
            k += FF9(n / 2, arr);
            return k;
        }

        public static long methodToAnalysis2Parallel(int n, int[] arr)
        {
            object monitor = new object();
            long k = 0;

            Parallel.For(0, n, i =>
            {
                k += k;
                k += FF9(i, arr);
            });

            k += FF9(n, arr);
            k += FF9(n / 2, arr);
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