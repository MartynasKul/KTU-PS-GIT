using System.Diagnostics;


namespace Lab1Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "Data.csv";
            Stopwatch sw = new Stopwatch();

            sw.Start();
            List<int> T1Data = Read(path);
            List<int> T2Data = Read(path);
            List<int> T3Data = Read(path);
            int number;
            int[] dataAmm = new int[] { 5000, 10000, 50000, 100000, 150000, 300000 };
            int[] dataAmm1 = new int[] { 50, 100, 500, 1000, 1500, 3000 };

            sw.Stop();
            Console.WriteLine("Duomenys įvesti per:" + sw.ElapsedMilliseconds + " ms");
            sw.Reset();

            //----------------------------------------------------------
            Console.WriteLine("Pirma lygtis:\n");

            for (int i = 0; i < dataAmm1.Length; i++) 
            {
                number = 0;
                sw.Start();
                T1(dataAmm1[i], T1Data, ref number);
                sw.Stop();
                Console.WriteLine("Testas nr {0}:" + sw.ElapsedMilliseconds + " ms", i+1);
                sw.Reset();
            }

            //----------------------------------------------------------

            Console.WriteLine("Antra lygtis:\n");

            for (int i = 0; i < dataAmm1.Length; i++)
            {
                number = 0;
                sw.Start();
                T2(dataAmm1[i], T2Data, ref number);
                sw.Stop();
                Console.WriteLine("Testas nr {0}:" + sw.ElapsedMilliseconds + " ms", i + 1);
                sw.Reset();
            }

            //----------------------------------------------------------
            Console.WriteLine("Trečia lygtis:\n");

            for (int i = 0; i < dataAmm1.Length; i++)
            {
                number = 0;
                sw.Start();
                T3(dataAmm1[i], T3Data, ref number);
                sw.Stop();
                Console.WriteLine("Testas nr {0}:" + sw.ElapsedMilliseconds + " ms", i + 1);
                sw.Reset();
            }

        }

        static List<int> Read(string path) 
        {
            List<int> list = new List<int>();

            string[] lines = File.ReadAllLines(path);
            foreach(string line in lines)
            {
                string[] bit = line.Split(';');
                for(int i = 0; i < bit.Length; i++) 
                {
                    list.Add(int.Parse(bit[i]));
                }
            }
            return list;
        }

        static void T1(int n, List<int> TData, ref int number)
        {
            if (n < 5) 
            {
                return;
            }
            for(int i = 0;i < n; i++) 
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        number += TData[k];
                    }
                }
            }

            T1(n / 5, TData, ref number);
            T1(n / 5, TData, ref number);
        }

        static void T2(int n, List<int> TData, ref int number) 
        {
            if (n < 6 || n < 9)                      
            {                                        
                return;                              
            }                                        
            for (int i = 0; i < n; i++)              
            {                                        
                for (int j = 0; j < n; j++)          
                {                                    
                    for (int k = 0; k < n; k++)      
                    {                                
                        number += TData[k];          
                    }                                
                }                                    
            }                                        
                                                     
            T2(n / 6, TData, ref number);            
            T2(n / 9, TData, ref number);            
        }

        static void T3(int n, List<int> TData, ref int number) 
        {
            if (n < 2 || n < 8)
            {
                return;
            }
            for (int i = 0; i < n; i++)
            {
                number += TData[i];
            }

            T3(n - 8, TData, ref number);
            T3(n - 2, TData, ref number);
        }
    }
}