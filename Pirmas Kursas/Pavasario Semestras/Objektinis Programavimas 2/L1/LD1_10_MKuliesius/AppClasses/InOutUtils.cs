using System;
using System.Collections.Generic;
using System.IO;

namespace LD1_10_MKuliesius.AppClasses
{
    public class InOutUtils
    {
        public static string[] ReadData(string path) 
        {
            string[] lines = File.ReadAllLines(path);

            return lines;
        }
        public static void PrintToFile(List<Moles> Moles, string path) 
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string outputPath=Path.Combine(baseDirectory,path);
            string directory =  Path.GetDirectoryName(outputPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
         
            using (StreamWriter writer = new StreamWriter(outputPath)) 
            {
                writer.WriteLine(Moles.Count);
                foreach (Moles m in Moles)
                { 
                    writer.WriteLine(m.ToString());
                }
            }
        }
    }
}