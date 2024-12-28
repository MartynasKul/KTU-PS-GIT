using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace Lab1
{
    static class InOutUtils
    {
        public static List<Character> ReadData(string FileName)
        {
            string data = File.ReadAllText(FileName);
            var instance = Activator.CreateInstance<List<Character>>();
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(data)))
            {
                var serializer = new DataContractJsonSerializer(instance.GetType());
                return (List<Character>)serializer.ReadObject(ms);
            }
        }
        public static void PrintToFile(string fileName, ResultMonitor monitor, string header)
        {
            string[] lines = new string[monitor.getCount() + 5];
            lines[0] = header;
            lines[1] = new string('-', 109);
            lines[2] = String.Format("| {0,-35} | {1,-7} | {2,-10} | {3,-10} |",
            "Name", "Age", "Power", "Result");
            lines[3] = new string('-', 109);
            for (int i = 0; i < monitor.getCount(); i++)
            {
                lines[i + 4] = monitor.Getcharacter(i).ToString();
            }
            lines[lines.Length - 1] = new string('-', 109);
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }
    }
}
