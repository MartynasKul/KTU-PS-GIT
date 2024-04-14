using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        int n = 5;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Console.WriteLine($"Skirtingos kombinacijos sukurti {n}:");
        DisplayCombinations(n, "");
        sw.Stop();
        Console.WriteLine("Laikas, per kurį įvykdyta programa: {0} ticks", sw.ElapsedTicks);
    }

    static void DisplayCombinations(int n, string prefix)
    {
        if (n == 0)
        {
            Console.WriteLine(prefix);
            return;
        }

        if (n >= 1)
        {
            DisplayCombinations(n - 1, prefix + "1 ");
        }

        if (n >= 3)
        {
            DisplayCombinations(n - 3, prefix + "3 ");
        }

        if (n >= 4)
        {
            DisplayCombinations(n - 4, prefix + "4 ");
        }
    }
}
