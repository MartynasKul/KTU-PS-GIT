using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
	static void Main()
	{
		var stopWatch = new Stopwatch();
		stopWatch.Start();
		int[] chapters = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 125, 150, 175, 200, 250, 300 }; // Pavyzdiniam testui, duota 16 skyriu puslapiu kiekiai ir viso puslapiu: 1750
		int numParts = 6; // Pavyzdiniui testui i kiek daliu padalinti rinktiniu rastu rinkini

		List<int> partPages = DivideIntoParts(chapters, numParts);
		PrintPartPages(partPages);
		stopWatch.Stop();
		Console.WriteLine("Laikas milisekundėmis: {0} ms", stopWatch.ElapsedMilliseconds.ToString());
	}

	static List<int> DivideIntoParts(int[] chapters, int numParts)
	{
		int totalPages = 0;
		foreach (int pages in chapters)
		{
			totalPages += pages;
            
		}
        Console.WriteLine(totalPages);
		int targetPagesPerPart = totalPages / numParts;
		int currentPartPages = 0;
		int currentPartIndex = 1;
		List<int> partPages = new List<int>();

		foreach (int pages in chapters)
		{
			currentPartPages += pages;

			if (currentPartPages > targetPagesPerPart)
			{
				currentPartPages -= pages;
				partPages.Add(currentPartPages);
				currentPartPages = pages;
				currentPartIndex++;
			}
		}

		partPages.Add(currentPartPages);
		return partPages;
	}

	static void PrintPartPages(List<int> partPages)
	{
		Console.WriteLine("Puslapių kiekis kiekvienoje dalyje:");
		for (int i = 0; i < partPages.Count; i++)
		{
			Console.WriteLine("Dalis {0}: {1} psl", i + 1, partPages[i]);
		}
	}
}