using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab1A
{
    internal class Program
    {
        public static DataMonitor dataMonitor;
        public static ResultMonitor resultMonitor;
        public static List<Car> cars;
        private static int finishedThreads = 0;
        private static int totalThreads = 0;
        private static object threadsLock = new object();

        static void Main(string[] args)
        {
            // Duomenu failai
            //string fileName = "IFF19_KuliesiusM_L1_dat_1.json"; // dalis duomenu atitinka kriteriju
            //string fileName = "IFF19_KuliesiusM_L1_dat_2.json"; // Visi duomenu atitinka kriteriju
            string fileName = "IFF19_KuliesiusM_L1_dat_3.json"; // Ne vienas is duomenu atitinka kriteriju

            cars = InOut.Read(fileName);
            dataMonitor = new DataMonitor(cars.Count / 3); // nes turi buti maziau nei puse duomenu kiekio
            resultMonitor = new ResultMonitor(cars.Count);


            totalThreads = 2;
            Thread[] workingThreads = new Thread[totalThreads];

            for (int i = 0; i < workingThreads.Length; i++) 
            {
                workingThreads[i] = new Thread(new ThreadStart(WorkingThread));
                workingThreads[i].Name = "Working thread " + i;
                workingThreads[i].Start();
            }

            for (int i = 0; i < cars.Count; i++) 
            {
                dataMonitor.Add(cars[i]);
            }

            dataMonitor.SetFinished();

           // Monitor.Enter(threadsLock);
           // try
           // {
           //     while (totalThreads != finishedThreads)
           //     {
           //         Monitor.Wait(threadsLock);
           //     }
           // }
           // catch (Exception e)
           // {
           //     Console.WriteLine(e.ToString());
           // }
           // finally 
           // {
           //     Monitor.Exit(threadsLock);
           // }




            for (int i = 0; i < workingThreads.Length; i++)
            {
                workingThreads[i].Join();
            }

            InOut.Print("LD1Result.txt", "Gauti rezultatai", resultMonitor);
            Console.WriteLine("Darbas baigtas");
            return;
        }

        public static void WorkingThread() 
        {
            while (!dataMonitor.finished || !dataMonitor.IsEmpty()) 
            {
                Car car = dataMonitor.Remove();
                if(car != null)
                {
                    //string processedData = $"{car.Make.Substring(0, 2)}{car.Model.Substring(0, 2)}" + $"{(car.Year % 100):D2}{car.Plate.Substring(0, 2)}{car.Power}";

                    string makeSubstring = car.Make.Length >= 2 ? car.Make.Substring(0, 2) : "";
                    string modelSubstring = car.Model.Length >= 2 ? car.Model.Substring(0, 2) : "";
                    string plateSubstring = car.Plate.Length >= 2 ? car.Plate.Substring(0, 2) : "";

                    // Simulate a time-consuming calculation involving strings
                    string processedData = $"{makeSubstring}{modelSubstring}{(car.Year % 100):D2}{plateSubstring}{car.Power}";

                    Thread.Sleep(500);
                    ProcessedCar processedCar = new ProcessedCar(car, processedData);
                    if (DateTime.Now.Year - processedCar.Year < 30) 
                    {
                        resultMonitor.Add(processedCar);                    
                    }
                }
            }
            Monitor.Enter(threadsLock);
            try
            {
                finishedThreads++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally 
            {
                Monitor.Pulse(threadsLock); 
                Monitor.Exit(threadsLock);  
            }
        }
    }
}

