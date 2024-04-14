using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        public static DataMonitor dataMonitor;
        public static ResultMonitor resultMonitor;
        public static List<Character> characters;
        private static int finishedThreads = 0;
        private static int totalThreads = 0;
        private static object threadsLock = new object();
        static void Main(string[] args)
        {
            string FileName = "IFF-1-9_LiaudanskisN_L1_dat_1.json";
            //string FileName = "IFF-1-9_LiaudanskisN_L1_dat_2.json";
            //string FileName = "IFF-1-9_LiaudanskisN_L1_dat_3.json";
            characters = InOutUtils.ReadData(FileName);
            dataMonitor = new DataMonitor(characters.Count / 3); //turi buti mazesnis nei puse duomenu kiekio
            resultMonitor = new ResultMonitor(characters.Count);



            totalThreads = 2;
            Thread[] workingThreads = new Thread[totalThreads];

           
            for (int i = 0; i < workingThreads.Length; i++)
            {
                workingThreads[i] = new Thread(new ThreadStart(WorkingThread));
                workingThreads[i].Name = "Working thread " + i;
                workingThreads[i].Start();
            }


            for (int i = 0; i < characters.Count; i++)
            {
                dataMonitor.AddItem(characters[i]);
            }
            dataMonitor.SetFinished(); 


            Monitor.Enter(threadsLock);
            try
            {
                while (totalThreads != finishedThreads)
                {
                    Monitor.Wait(threadsLock);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Monitor.Exit(threadsLock);
            }


            InOutUtils.PrintToFile("IFF-1-9_LiaudanskisN_L1_rez.txt", resultMonitor, "Results");

            return;
        }
        public static void WorkingThread()
        {
            while(!dataMonitor.finished || !dataMonitor.IsEmpty()) 
            {
                Character character = dataMonitor.RemoveItem();
                if(character != null) 
                {
                    
                    long processedData = 0;
                    long max = 999999999999999;
                    for (int i = 0; i < character.Age; i++) 
                    {
                        for (double y = 0; y < character.Power; y += 0.1) 
                        {
                            processedData += (int)character.Name[(int)Math.Floor(i * y) % character.Name.Length] * 2; 
                        }
                    }
                    processedData = max - processedData;
                    Thread.Sleep(500);
                    ProcessedCharacter processedcharacter = new ProcessedCharacter(character, processedData);
                    if (processedcharacter.Power < 100)
                    {
                        resultMonitor.AddItem(processedcharacter);
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
