using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace UZD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Pirmosios lygties sprendimas
            var laikas1 = new System.Diagnostics.Stopwatch();


            Console.WriteLine("Pirmoji lygtis:T(n)=2*T(n/5)+N^3");
            int[] M1 = new int[]{29, 96, 28, 27, 95, 77, 89, 91, 47, 31, 82, 51, 15, 20, 86, 3, 19, 5, 16, 65, 27, 70, 98, 5,
                51, 14, 12, 43, 17, 69, 52, 70,77, 41, 73, 38,99,67,93,54,56,25,44,30,86,88,2,34,97, 69 };
            int l1 = M1.Length;

            
            laikas1.Start();
            Utils.Pirmoji(M1, l1);
            laikas1.Stop();
            for (int i = 0; i < l1; i++)
            {
                Console.Write(M1[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine($"Execution Time: {laikas1.Elapsed} ms");
            Console.WriteLine();
            //Antrosios lygties sprendimas
            var laikas2 = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Antroji lygtis: T(n)=T(n/6)+T(n/9)+N^3");
            int[] M2 = new int[]{97, 80, 46, 3, 58, 99, 6, 5, 86, 68, 40, 5, 64, 36, 19, 91, 14, 52, 63, 4, 16, 55, 81, 88, 81,
                2, 17, 50, 89, 85, 56, 92, 67, 87, 4, 65, 27, 13, 69, 59, 77, 79, 79, 37, 9, 92, 62, 85, 14, 94};
            int l2 = M2.Length;

            laikas2.Start();
            Utils.Antroji(M2, l2);
            laikas2.Stop();
            for (int i = 0; i < l2; i++)
            {
                Console.Write(M2[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine($"Execution Time: {laikas2.Elapsed} ms");
            Console.WriteLine();

            // Treciosios lygties sprendimas
            Console.WriteLine("Trecioji lygtis: T(n)=T(n-8)+T(n-2)+N");
            var laikas3 = new System.Diagnostics.Stopwatch();
            int[] M3 = new int[]{97, 80, 46, 3, 58, 99, 6, 5, 86, 68, 40, 5, 64, 36, 19, 91, 14, 52, 63, 4, 16, 55, 81, 88, 81,
                2, 17, 50, 89, 85, 56, 92, 67, 87, 4, 65, 27, 13, 69, 59, 77, 79, 79, 37, 9, 92, 62, 85, 14, 94};
            int l3 = M3.Length;

            laikas3.Start();
            Utils.Trecioji(M3, l3);
            laikas3.Start();
            for (int i = 0; i < l3; i++)
            {
                Console.Write(M3[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine($"Execution Time: {laikas3.Elapsed} ms");
            int bongo = 1;

        }
        class Utils 
        {
            public static void Pirmoji(int[] M, int n) 
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            M[k]++;
                        }
                    }
                }

                if (n < 5)
                {
                    return;
                }

                else
                {
                    Pirmoji(M, n / 5);
                    Pirmoji(M, n / 5);
                }
            }

            public static void Antroji(int[] M, int n)
            { 
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            M[k]++;
                        }
                    }
                }

                if (n < 8 || n < 9)
                {
                    return;
                }

                else
                {
                    Antroji(M, n / 6);
                    Antroji(M, n / 9);
                }
            }

            public static void Trecioji(int[] M, int n) 
            {
                for(int i = 0; i < n; i++)
                {
                    M[i]++;
                }

                if (n <0)
                {
                    return;
                }

                else
                {
                    Trecioji(M, n-8);
                    Trecioji(M, n-2);
                }
            }
        
        }
    }
}
