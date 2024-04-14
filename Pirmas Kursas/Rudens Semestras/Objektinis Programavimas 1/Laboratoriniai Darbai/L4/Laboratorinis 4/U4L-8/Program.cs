using System;
using System.IO;
using System.Text;

/// <summary>
/// Uzduotis
/// </summary>
namespace U4L_8
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Knyga.txt"; // duom failas
            const string CFr = "Rodikliai.txt"; // rez failas

            if (File.Exists(CFr))
            {
                File.Delete(CFr);
            }
            string[] lines = File.ReadAllLines(CFd, Encoding.UTF8);

            TaskUtils.Analize(lines, CFr);

            Console.ReadKey();
        }
    }
}
