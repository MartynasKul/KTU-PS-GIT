using System.Diagnostics;

class Program
{
	static void Main()
	{
		var stopWatch = new Stopwatch();
		stopWatch.Start();
		int[] chapters = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 150, 170, 200, 250, 300 }; // Pavyzdiniam testavimui kiekvieno rinkinio skyriaus puslapiai surasomi
		int numParts = 5; // Pavyzdiniam testavimui parenkama i kiek daliu dalinti puslapius

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

		int targetPagesPerPart = totalPages / numParts;
		List<int> partPages = new List<int>();
		Divide(chapters, targetPagesPerPart, 0, 0, partPages);
		return partPages;
	}

	static void Divide(int[] chapters, int targetPagesPerPart, int index, int currentPartPages, List<int> partPages)
	{
		if (index >= chapters.Length)
		{
			partPages.Add(currentPartPages);
			return;
		}

		if (currentPartPages + chapters[index] <= targetPagesPerPart)
		{
			Divide(chapters, targetPagesPerPart, index + 1, currentPartPages + chapters[index], partPages);
		}
		else
		{
			partPages.Add(currentPartPages);
			Divide(chapters, targetPagesPerPart, index, 0, partPages);
		}
	}

	static void PrintPartPages(List<int> partPages)
	{
		Console.WriteLine("Puslapių kiekis kiekvienoje rinkinio dalyje:");
		for (int i = 0; i < partPages.Count; i++)
		{
			Console.WriteLine("Dalis {0}: {1} psl.", i + 1, partPages[i]);
		}
	}
}