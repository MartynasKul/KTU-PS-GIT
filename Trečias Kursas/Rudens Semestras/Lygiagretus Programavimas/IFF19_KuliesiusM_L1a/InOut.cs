using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Lab1A
{
    internal class InOut
    {
        public static List<Car> Read(string fileName) 
        {
            string data = File.ReadAllText(fileName);
            var instance = Activator.CreateInstance<List<Car>>();
            using (var read = new MemoryStream(Encoding.Unicode.GetBytes(data))) 
            { 
                var serializer = new DataContractJsonSerializer(instance.GetType());
                return (List<Car>)serializer.ReadObject(read);
            
            }
        }

        public static void Print(string fileName, string header, ResultMonitor monitor)
        {
            if (monitor.getCount()<1)
            {
                string[] lines = new string[2];
                lines[0] = header;
                lines[1] = "Rezultatu pagal duomenis nera";

                File.WriteAllLines(fileName, lines, Encoding.UTF8);
            }
            else
            { 
                string[] lines = new string[monitor.getCount() + 6];
                lines[0] = header;
                lines[1] = "Rezultatu kiekis: " + monitor.getCount();
                lines[2] = new string('-', 132);
                lines[3] = String.Format(" | {0,-35} | {1,-35} | {2,-5} | {3,-5}kw | {4,-10} | {5, -20} | ", "Make", "Model", "Year", "Power", "Plate", "Result");
                lines[4] = new string('-', 132);
                for (int i = 0; i < monitor.getCount(); i++) 
                {
                    lines[i + 5] = monitor.GetCar(i).ToString();
                }
                lines[lines.Length-1] = new string('-', 132);
                File.WriteAllLines(fileName, lines,Encoding.UTF8);
            }
        }

    }
}
