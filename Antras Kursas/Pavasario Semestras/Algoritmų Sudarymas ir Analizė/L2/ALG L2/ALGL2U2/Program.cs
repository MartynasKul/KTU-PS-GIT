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

            for(int i=50; i<=1500;i+=50)
            {
                sw.Start();
                Console.WriteLine(methodToAnalysis(i, arr));
                sw.Stop();
                Console.WriteLine("Testo trukme, su {0} duomenim:" + sw.ElapsedTicks + " ticks", i);
                sw.Reset();
            }
            
        }

         static int[] Read(string path) 
        {
            int[] arr = new int[1500];

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

        public static long methodToAnalysis (int n, int[] arr)
        {
            long k = 0;
            Random randNum = new Random();

            for (int i = 0; i < n; i++)
            {
                k += arr[i] + FF3(i, arr);
            }
            return k;
        }

        public static long FF3(int n, int [] arr)
        {
            if (n > 0 && arr.Length > n && arr[n] > 0)
            {
                return FF3(n - 1, arr) + FF3(n - 3, arr);
            }
            return n;
        }
    }
}