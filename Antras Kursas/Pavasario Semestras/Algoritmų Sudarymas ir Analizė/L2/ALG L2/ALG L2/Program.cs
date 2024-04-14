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
                Console.WriteLine("Testo trukme, su {0} duomenim:" + sw.ElapsedMilliseconds + " ms", i);
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

        public static long methodToAnalysis(int[] arr, int ind)
        {
            long n = ind;
            long k = n;

            for (int i = 0; i < n * n; i++)
            {
                if (arr[0] > 0)
                {
                    for (int j = 0; j < n ; j++)
                    {
                        k -= 2;
                    }

                    for (int j = 0; j < n * n / 2; j++)
                    {
                        k += 3;
                    }
                }
            }
            return k;
        }
    }
}