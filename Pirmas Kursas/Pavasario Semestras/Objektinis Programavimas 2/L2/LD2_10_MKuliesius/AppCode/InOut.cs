using System.Collections.Generic;
using System.IO;

namespace LD2_10_MKuliesius.AppCode
{
    public class InOut
    {
        /// <summary>
        /// Reads workers information
        /// </summary>
        /// <param name="fileName"> data file</param>
        /// <returns></returns>
        public static LinkedList<Worker> ReadWorkerFile(string fileName)
        {
            LinkedList<Worker> workers = new LinkedList<Worker>();
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 4) // Assuming each line has 4 parts
                {
                    string dateStr = parts[0].Trim();
                    string workerName = parts[1].Trim();
                    string detailCode = parts[2].Trim();
                    int quantity = int.Parse(parts[3].Trim());

                    Worker worker = new Worker(workerName, dateStr, detailCode, quantity);

                    // Add the worker to the linked list
                    workers.Add(worker);
                }
            }
            return workers;
        }

        /// <summary>
        /// Reads details info
        /// </summary>
        /// <param name="fileName"> Data file</param>
        /// <returns></returns>
        public static LinkedList<Detail> ReadDetailFile(string fileName)
        {
            LinkedList<Detail> details = new LinkedList<Detail>();
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if(parts.Length == 3) 
                {
                    string code = parts[0].Trim();
                    string name = parts[1].Trim();
                    decimal price = decimal.Parse(parts[2].Trim());

                    Detail det = new Detail(code, name, price);
                    details.Add(det);
                }
            }
            return details;
        }

        /// <summary>
        /// Writes initial Worker detail
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="header"></param>
        /// <param name="list"></param>
        public static void WriteInitialWorkerData(string fileName, string header, LinkedList<Worker> list) 
        {
            string dashes = new string('-', 105);
            List<string> lines = new List<string>
            {
                header,
                dashes,
                string.Format($"| {"Data", -10} | {"Vardas", -20} | {"Det. Kodas", 5} | {"Pagamino", 4} | {"Viso dirbo", 4} | {"Viso pagamino", 4} | {"Viso uzdirbo", 4} |\n"),
                dashes
            };
            foreach (Worker worker in list) 
            {
                lines.Add(worker.ToString() + "\n");
            }
            lines.Add(dashes);
            lines.Add("\n");
            File.AppendAllLines(fileName, lines);
        }
        /// <summary>
        /// Writes initial data of detail
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="header"></param>
        /// <param name="list"></param>
        public static void WriteInitialDetailData(string fileName, string header, LinkedList<Detail> list) 
        {
            string dashes = new string('-', 45);
            List<string> lines = new List<string>
            {
                header,
                dashes,
                string.Format($"| {"Pavadinimas",-15} | {"Det. Kodas", 12} | {"Kaina", 8} |"),
                dashes
            };
            foreach (Detail detail in list) 
            {
                lines.Add(detail.ToString());
            }
            lines.Add(dashes);
            lines.Add("\n");
            File.AppendAllLines(fileName, lines);
        }

        /// <summary>
        /// Copies the existing result file.
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>An array of lines</returns>
        public static string[] SaveCurrentResultFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            return lines;
        }

        public static List<string> WriteInitialWorkerDataList( string header, LinkedList<Worker> list)
        {
            string dashes = new string('-', 105);
            List<string> lines = new List<string>
            {
                header+"\n",
                dashes + "\n",
                string.Format($"| {"Data", -10} | {"Vardas", -20} | {"Det. Kodas", 5} | {"Pagamino", 4} | {"Viso dirbo", 4} | {"Viso pagamino", 4} | {"Viso uzdirbo", 4} |\n"),
                dashes+"\n"
            };
            foreach (Worker worker in list)
            {
                lines.Add(worker.ToString()+"\n");
            }
            lines.Add(dashes+"\n");
            lines.Add("\n");


            return lines;
        }

        public static List<string> WriteInitialDetailDataList( string header, LinkedList<Detail> list)
        {
            string dashes = new string('-', 45);
            List<string> lines = new List<string>
            {
                header+ "\n",
                dashes+ "\n",
                string.Format($"| {"Pavadinimas",-15} | {"Det. Kodas", 12} | {"Kaina", 8} |")+ "\n",
                dashes+ "\n"
            };
            foreach (Detail detail in list)
            {
                lines.Add(detail.ToString() + "\n");
            }
            lines.Add(dashes + "\n");
            lines.Add("\n");
            
            return lines;
        }

    }
}